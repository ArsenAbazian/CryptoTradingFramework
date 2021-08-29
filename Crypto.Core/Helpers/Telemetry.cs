using Crypto.Core.Common;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core {
    public class Telemetry {
        static Telemetry defaultTelemetry;
        public static Telemetry Default {
            get {
                if(defaultTelemetry == null)
                    defaultTelemetry = new Telemetry();
                return defaultTelemetry;
            }
        }

        void InitializeTelemetry() {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

#if !CORE
            TelemetryConfiguration.Active.InstrumentationKey = "c54faeb2-109e-40c8-ad03-58aa69c17592";
            Microsoft.ApplicationInsights.TelemetryClient tc = new Microsoft.ApplicationInsights.TelemetryClient();
            tc.InstrumentationKey = "c54faeb2-109e-40c8-ad03-58aa69c17592";
            tc.Context.User.Id = Environment.UserName;
            tc.Context.Session.Id = Guid.NewGuid().ToString();
            tc.Context.Device.OperatingSystem = Environment.OSVersion.ToString();
            tc.Context.Component.Version = Assembly.GetEntryAssembly().GetName().Version.ToString();

            InnerClient = tc;
            try
            {
                LogFile = new StreamWriter("log.txt");
            }
            catch(Exception)
            {
                LogFile = null;
            }
#endif
        }

        protected StreamWriter LogFile { get; private set; }
        public Telemetry() {
            InitializeTelemetry();
        }
        [XmlIgnore]
        public TelemetryClient InnerClient { get; set; }

        void LogToFile(Exception e) {
            if (LogFile == null)
                return;
            try {
                LogFile.WriteLine(DateTime.Now.ToString() + " " + e.ToString());
                LogFile.FlushAsync();
            }
            catch(Exception) { }
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e) {
            if(InnerClient == null)
                return;
            InnerClient.TrackException(e.Exception);
            InnerClient.Flush();
            LogToFile(e.Exception);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            if(InnerClient == null)
                return;
            InnerClient.TrackException(e.ExceptionObject as Exception);
            InnerClient.Flush();
            LogToFile(e.ExceptionObject as Exception);
        }

        public void TrackException(Exception e) {
            LogManager.Default.Error("exception", e.Message);
            if(InnerClient == null)
                return;
            InnerClient.TrackException(e);
            Task.Run(() => { InnerClient.Flush(); });
        }

        public void TrackException(Exception e, string[,] parameters) {
            for(int index = 0; index < parameters.GetLength(0); index++) {
                e.Data.Add(parameters[index, 0], parameters[index, 1]);
            }
            TrackException(e);
        }
        public void TrackEvent(string str, bool flush) {
            if(InnerClient == null)
                return;
            InnerClient.TrackEvent(str);
            if(flush)
                InnerClient.Flush();
        }
        public void TrackEvent(string str) { TrackEvent(str, true); }
        public void TrackEvent(string str, string[,] parameters, bool flush) {
            Dictionary<string, string> dr = new Dictionary<string, string>();
            for(int index = 0; index < parameters.GetLength(0); index++) {
                dr.Add(parameters[index, 0], parameters[index, 1]);
            }
            if(InnerClient == null)
                return;
            InnerClient.TrackEvent(str, dr);
            if(flush)
                Task.Run(() => { InnerClient.Flush(); });
        }
        protected int Count { get; set; }
        public void TrackEvent(string str, string[] parameters, bool flush) {
            Dictionary<string, string> dr = new Dictionary<string, string>();
            for(int index = 0; index < parameters.GetLength(0); index += 2) {
                if(string.IsNullOrEmpty(parameters[index + 1]))
                    continue;
                dr.Add(parameters[index], parameters[index + 1]);
            }
            if(InnerClient == null)
                return;
            InnerClient.TrackEvent(str, dr);
            if(flush) {
                if(Count > 10) {
                    Task.Run(() => { InnerClient.Flush(); });
                    Count = 0;
                }
                else Count++;
            }
        }
        public void Flush() {
            if(InnerClient != null)
                InnerClient.Flush();
        }
        public void TrackEvent(LogType type, object owner, string text, string description) {
            string name = owner == null ? string.Empty : owner.ToString();
            TrackEvent(name, new string[] { "type", type.ToString(), "owner", name, "message", text, "description", description }, true);
            LogManager.Default.Add(type, owner, null, text, description);
        }
        //public void TrackEvent(LogType type, Exchange exchange, string connName, string text, string description) {
        //    string tickerName = connName;
        //    string exchangeName = exchange == null ? string.Empty : exchange.Type.ToString();
        //    TrackEvent(exchange.Type.ToString(), new string[] { "type", type.ToString(), "exchange", exchangeName, "ticker", tickerName, "message", text, "description", description }, true);
        //    object obj = ticker == null ? (object)exchange : (object)ticker;
        //    LogManager.Default.Log(type, obj, null, text, description);
        //}
    }
}
