using DevExpress.Utils.Serializing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public class TickerHistoryItem {
        [XtraSerializableProperty]
        public DateTime Time { get; set; }
        [XtraSerializableProperty]
        public decimal Bid { get; set; }
        [XtraSerializableProperty]
        public decimal Ask { get; set; }
        [XtraSerializableProperty]
        public decimal Current { get; set; }
    }
}
