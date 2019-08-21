using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public class LogMessage {
        public LogType Type { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }
    public enum LogType {
        Log,
        Warning,
        Error,
        Success
    }
}
