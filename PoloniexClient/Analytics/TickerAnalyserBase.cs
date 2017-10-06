using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Analytics {
    public class TickerAnalyserBase {
        public TickerAnalyserBase(TickerBase ticker) {
            Ticker = ticker;
        }

        public TickerBase Ticker { get; private set; }
    }
}
