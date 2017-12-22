using DevExpress.Utils.Serializing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public class CurrencyStatusHistoryItem {
        [XtraSerializableProperty]
        public bool Enabled { get; set; }
        [XtraSerializableProperty]
        public DateTime Time { get; set; }
    }
}
