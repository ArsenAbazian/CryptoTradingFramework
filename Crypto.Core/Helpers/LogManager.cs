using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Common {
    public class LogManager {
        static LogManager defaultManager;
        public static LogManager Default {
            get {
                if(defaultManager == null)
                    defaultManager = new LogManager();
                return defaultManager;
            }
            set { defaultManager = value; }
        }

        public BindingList<LogMessage> Messages { get; } = new BindingList<LogMessage>();
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
