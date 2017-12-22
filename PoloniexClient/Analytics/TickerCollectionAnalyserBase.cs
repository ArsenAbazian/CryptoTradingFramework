using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Analytics {
    public abstract class TickerCollectionAnalyserBase {
        public TickerCollectionAnalyserBase() { }
        public abstract void Calculate(TickerCollection coll);
    }
}
