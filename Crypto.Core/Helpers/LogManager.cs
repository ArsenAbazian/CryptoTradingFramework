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
        public void AddMessage(LogType type, Exchange exchange, Ticker ticker, string message, string description) {
            string tickerName = ticker == null ? string.Empty : ticker.Name;
            AddMessage(type, exchange, tickerName, message, description);
        }
        public void AddMessage(LogType type, Exchange exchange, string name, string message, string description) {
            string tickerName = name;
            Messages.Add(new LogMessage() {
                Type = type,
                Exchange = exchange == null ? string.Empty : exchange.Type.ToString(),
                Ticker = tickerName,
                Text = message,
                Description = description,
                Time = DateTime.UtcNow
            });
            RefreshVisual();
        }
        public void Add(string message) { AddMessage(LogType.Log, null, (Ticker)null, message, ""); }
        public void Add(Exchange exchange, Ticker ticker, string message) { AddMessage(LogType.Log, exchange, ticker, message, ""); }
        public void Add(string message, string description) { AddMessage(LogType.Log, null, (Ticker)null, message, description); }
        public void Add(Exchange exchange, Ticker ticker, string message, string description) { AddMessage(LogType.Log, exchange, ticker, message, description); }
        public void AddWarning(Exchange exchange, Ticker ticker, string message) { AddMessage(LogType.Warning, exchange, ticker, message, ""); }
        public void AddWarning(string message) { AddWarning(null, null, message); }
        public void AddWarning(string message, string description) { AddWarning(null, null, message, description); }
        public void AddWarning(Exchange exchange, Ticker ticker, string message, string description) { AddMessage(LogType.Warning, exchange, ticker, message, description); }
        public void AddError(string message) { AddMessage(LogType.Error, null, (Ticker)null, message, ""); }
        public void AddError(string message, string description) { AddError(null, null, message, description); }
        public void AddError(Exchange exchange, Ticker ticker, string message) { AddMessage(LogType.Error, exchange, ticker, message, ""); }
        public void AddError(Exchange exchange, Ticker ticker, string message, string description) { AddMessage(LogType.Error, exchange, ticker, message, description); }
        public void AddSuccess(string message) { AddMessage(LogType.Success, null, (Ticker)null, message, ""); }
        public void AddSuccess(Exchange exchange, Ticker ticker, string message) { AddMessage(LogType.Success, exchange, ticker, message, ""); }
        public void AddSuccess(Exchange exchange, Ticker ticker, string message, string description) { AddMessage(LogType.Success, exchange, ticker, message, description); }
        protected virtual void RefreshVisual() {

        }
        public virtual void ShowLogForm() {
            throw new NotImplementedException();
        }
    }
}
