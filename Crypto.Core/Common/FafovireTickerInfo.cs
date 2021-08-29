using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Common {
    [Serializable]
    public class PinnedTickerInfo {
        public string BaseCurrency { get; set; }
        public string MarketCurrency { get; set; }
        public override string ToString() {
            return MarketCurrency + "/" + BaseCurrency;
        }
    }
}
