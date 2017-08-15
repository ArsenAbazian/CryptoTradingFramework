using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public class LogManager {
        static LogManager defaultManager;
        public static LogManager Default {
            get {
                if(defaultManager == null)
                    defaultManager = new LogManager();
                return defaultManager;
            }
        }

        LogForm form;
        protected LogForm Form {
            get {
                if(form == null) {
                    form = new LogForm();
                    form.Messages = Messages;
                }
                return form;
            }
        }

        protected List<LogMessage> Messages { get; } = new List<LogMessage>();
        public void AddMessage(LogType type, string message, string description) {
            Messages.Add(new LogMessage() { Type = type, Text = message, Description = description, Time = DateTime.Now });
            if(!Form.IsDisposed && Form.Visible)
                Form.RefreshData();
        }
        public void Add(string message) { AddMessage(LogType.Log, message, ""); }
        public void Add(string message, string description) { AddMessage(LogType.Log, message, description); }
        public void AddWarning(string message) { AddMessage(LogType.Warning, message, ""); }
        public void AddWarning(string message, string description) { AddMessage(LogType.Warning, message, description); }
        public void AddError(string message) { AddMessage(LogType.Error, message, ""); }
        public void AddError(string message, string description) { AddMessage(LogType.Error, message, description); }
        public void AddSuccess(string message) { AddMessage(LogType.Success, message, ""); }
        public void AddSuccess(string message, string description) { AddMessage(LogType.Success, message, description); }
        public void Show() {
            if(Form.IsDisposed)
                this.form = null;
            Form.Show();
        }
    }
}
