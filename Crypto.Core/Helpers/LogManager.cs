using Crypto.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Common {
    [Serializable]
    public class LogManager : ISupportSerialization {
        static readonly string Name = "Log.xml";
        public static LogManager FromFile(string fileName) {
            LogManager res = (LogManager)SerializationHelper.FromFile(fileName, typeof(LogManager));
            if(res != null) res.FileName = Name;
            return res;
        }

        protected bool Saving { get; set; }
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
                SerializationHelper.Save(this, typeof(LogManager), null);
            }
            finally {
                Saving = false;
            }
        }

        public ResizeableArray<LogMessage> Messages { get; } = new ResizeableArray<LogMessage>();
        public void Log(string message) {
            Add(LogType.Log, null, null, message, null);
        }
        public void Success(string message) {
            Add(LogType.Success, null, null, message, null);
        }
        public void Log(string message, string description) {
            Add(LogType.Log, null, null, message, description);
        }
        public void Error(string message) {
            Add(LogType.Error, null, null, message, null);
        }
        public void Warning(string message, string description) {
            Add(LogType.Warning, null, null, message, description);
        }
        public void Warning(string message) {
            Add(LogType.Warning, null, null, message, null);
        }
        public void Error(object owner, string message, string description) {
            Add(LogType.Error, owner, null, message, description);
        }
        public void Warning(object owner, string message, string description) {
            Add(LogType.Warning, owner, null, message, description);
        }
        public void Log(object owner, string message, string description) {
            Add(LogType.Log, owner, null, message, description);
        }
        public void Error(string message, string description) {
            Add(LogType.Error, null, null, message, description);
        }
        public void Add(string message) {
            Add(LogType.Log, null, null, message, null);
        }
        public void Add(LogType type, object owner, string name, string message, string description) {
            Messages.Add(new LogMessage() {
                Type = type,
                Owner = owner,
                Name = name,
                Text = message,
                Description = description,
                Time = DateTime.UtcNow
            });
            Save();
            RefreshVisual();
        }

        public ILogVisualizer Visualiser { get; set; }
        protected virtual void RefreshVisual() {
            if(Visualiser != null)
                Visualiser.RefreshView();
        }

        public virtual void ShowLogForm() { }

        public void Log(LogType log, object owner, string message, string description) {
            Add(LogType.Log, owner, null, message, description);
        }
    }

    public interface ILogVisualizer {
        void RefreshView();
    }
}
