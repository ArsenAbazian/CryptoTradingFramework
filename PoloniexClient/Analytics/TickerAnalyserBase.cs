using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Analytics {
    public class TickerAnalyserBase {
        public TickerAnalyserBase(Ticker ticker) {
            Ticker = ticker;
        }

        public Ticker Ticker { get; private set; }
    }
}
