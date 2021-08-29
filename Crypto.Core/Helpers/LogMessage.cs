using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Common {
    public class LogMessage {
        public LogType Type { get; set; }
        public string Id { get; set; }
        [XmlIgnore]
        public object Owner { get; set; }
        string name;
        [DisplayName("Owner")]
        public string Name {
            get {
                if(name != null)
                    return name;
                if(Owner != null)
                    return Owner.ToString();
                return string.Empty;
            }
            set { name = value; }
        }
        public DateTime Time { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public override string ToString() {
            return string.Format("{0}: {1}: {2} - {3}", Time, Type, Text, Description);
        }
    }
    public enum LogType {
        Log,
        Warning,
        Error,
        Success
    }
}
