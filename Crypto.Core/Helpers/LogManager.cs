using Crypto.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XmlSerialization;

namespace Crypto.Core.Common {
    [Serializable]
    public class LogManager : ISupportSerialization {
        static readonly string Name = "Log.xml";
        public static LogManager FromFile(string fileName) {
            Type type = typeof(LogManager);
            LogManager res = (LogManager)SerializationHelper.Current.FromFile(fileName, type);
            if(res != null) res.FileName = Name;
            return res;
        }

        protected bool Saving { get; set; }

        void ISupportSerialization.OnBeginSerialize() { }
        void ISupportSerialization.OnEndSerialize() { }
        void ISupportSerialization.OnBeginDeserialize() { }
        
        public virtual void OnEndDeserialize() { }
        public string FileName { get; set; }

        static LogManager defaultManager;
        public static LogManager Default {
            get {
                if(defaultManager == null)
                    defaultManager = LogManager.FromFile(Name);
                if(defaultManager == null)
                    defaultManager = new LogManager();
                return defaultManager;
            }
            set { defaultManager = value; }
        }

        public void Save() {
            if(Saving)
                return;
            Saving = true;
            try {
                SerializationHelper.Current.Save(this, typeof(LogManager), null);
            }
            finally {
                Saving = false;
            }
        }

        public ResizeableArray<LogMessage> Messages { get; } = new ResizeableArray<LogMessage>();
        public LogMessage Log(string message) {
            return Add(LogType.Log, null, null, message, null);
        }
        public LogMessage Success(string message) {
            return Add(LogType.Success, null, null, message, null);
        }
        public LogMessage Log(string message, string description) {
            return Add(LogType.Log, null, null, message, description);
        }
        public LogMessage Error(string message) {
            return Add(LogType.Error, null, null, message, null);
        }
        public LogMessage Warning(string message, string description) {
            return Add(LogType.Warning, null, null, message, description);
        }
        public LogMessage Warning(string message) {
            return Add(LogType.Warning, null, null, message, null);
        }
        public LogMessage Error(object owner, string message, string description) {
            return Add(LogType.Error, owner, null, message, description);
        }
        public LogMessage Warning(object owner, string message, string description) {
            return Add(LogType.Warning, owner, null, message, description);
        }
        public LogMessage Log(object owner, string message, string description) {
            return Add(LogType.Log, owner, null, message, description);
        }
        public LogMessage Error(string message, string description) {
            return Add(LogType.Error, null, null, message, description);
        }
        public LogMessage Add(string message) {
            return Add(LogType.Log, null, null, message, null);
        }
        public LogMessage Add(LogType type, object owner, string name, string message, string description) {
            lock(Messages) {
                Messages.Add(new LogMessage() {
                    Type = type,
                    Owner = owner,
                    Name = name,
                    Text = message,
                    Description = description,
                    Time = DateTime.Now
                });
                //Debug.WriteLine(DateTime.UtcNow.ToLongTimeString() + ": " + message);
            }
            lock(this) {
                Save();
            }
            RefreshVisual();
            if(type == LogType.Error || type == LogType.Warning)
                NotificationManager.Notify(type, name, string.Format("{0}\n{1}", message, description));
            return Messages[Messages.Count - 1];
        }

        public LogMessage Add(LogType type, object owner, string name, string message, string description, bool allowNotify) {
            lock(Messages) {
                Messages.Add(new LogMessage() {
                    Type = type,
                    Owner = owner,
                    Name = name,
                    Text = message,
                    Description = description,
                    Time = DateTime.Now
                });
                //Debug.WriteLine(DateTime.UtcNow.ToLongTimeString() + ": " + message);
            }
            lock(this) {
                Save();
            }
            RefreshVisual();
            if((type == LogType.Error || type == LogType.Warning) && allowNotify)
                NotificationManager.Notify(type, name, string.Format("{0}\n{1}", message, description));
            return Messages[Messages.Count - 1];
        }

        public ILogVisualizer Visualiser { get; set; }
        public ILogPanelOwner Viewer { get; set; }
        protected virtual void RefreshVisual() {
            if(Visualiser != null)
                Visualiser.RefreshView();
        }

        public virtual void ShowLogForm() {
            if(Viewer != null)
                Viewer.ShowLogPanel();
        }

        public void Log(LogType log, object owner, string message, string description) {
            Add(LogType.Log, owner, null, message, description);
        }

        public void ShowNotification(LogType messageType, string owner, string message, string description) {
            
        }

        public LogMessage GetLast(LogType type) {
            return Messages.LastOrDefault(msg => msg.Type == type);
        }
    }

    public interface ILogVisualizer {
        void RefreshView();
    }

    public interface ILogPanelOwner {
        void ShowLogPanel();
    }
}
