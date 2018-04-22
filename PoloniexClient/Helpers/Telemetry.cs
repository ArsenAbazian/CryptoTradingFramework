using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
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

            TelemetryConfiguration.Active.InstrumentationKey = "ec07db0b-5506-4b5d-9173-4fe758364245";
            Microsoft.ApplicationInsights.TelemetryClient tc = new Microsoft.ApplicationInsights.TelemetryClient();
            tc.InstrumentationKey = "ec07db0b-5506-4b5d-9173-4fe758364245";
            tc.Context.User.Id = Environment.UserName;
            tc.Context.Session.Id = Guid.NewGuid().ToString();
            tc.Context.Device.OperatingSystem = Environment.OSVersion.ToString();
            tc.Context.Component.Version = Assembly.GetEntryAssembly().GetName().Version.ToString();

            tc.TrackEvent("I am started!");
            tc.Flush();
            InnerClient = tc;
        }

        public Telemetry() {
            InitializeTelemetry();
        }
        public TelemetryClient InnerClient { get; set; }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e) {
            InnerClient.TrackException(e.Exception);
            InnerClient.Flush();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            InnerClient.TrackException(e.ExceptionObject as Exception);
            InnerClient.Flush();
        }

        public void TrackException(Exception e) {
            InnerClient.TrackException(e);
            InnerClient.Flush();
        }

        public void TrackException(Exception e, string[,] parameters) {
            for(int index = 0; index < parameters.GetLength(0); index++) {
                e.Data.Add(parameters[index, 0], parameters[index, 1]);
            }
            TrackException(e);
        }
        public void TrackEvent(string str, bool flush) {
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
            InnerClient.TrackEvent(str, dr);
            if(flush)
                InnerClient.Flush();
        }
        public void TrackEvent(string str, string[] parameters, bool flush) {
            Dictionary<string, string> dr = new Dictionary<string, string>();
            for(int index = 0; index < parameters.GetLength(0); index += 2) {
                dr.Add(parameters[index], parameters[index + 1]);
            }
            InnerClient.TrackEvent(str, dr);
            if(flush)
                InnerClient.Flush();
        }
        public void Flush() {
            InnerClient.Flush();
        }
    }
}
