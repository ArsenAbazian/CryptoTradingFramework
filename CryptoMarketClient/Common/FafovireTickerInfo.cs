using DevExpress.Utils.Serializing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public class PinnedTickerInfo {
        [XtraSerializableProperty]
        public string BaseCurrency { get; set; }
        [XtraSerializableProperty]
        public string MarketCurrency { get; set; }
        public override string ToString() {
            return MarketCurrency + "/" + BaseCurrency;
        }
    }
}
