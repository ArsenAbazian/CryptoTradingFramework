using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Analytics {
    public class OrderBookChangeAnalyser : TickerAnalyserBase {
        public OrderBookChangeAnalyser(TickerBase ticker, int depth) : base(ticker) {
            OrderBookDepth = depth;    
        }


        public int OrderBookDepth { get; private set; }
        protected void SaveOrderBook() {
        }
    }

    public class OrderBookChangeItem {
        public long Time { get; set; }
        public double ChangePercent { get; set; }
    }
}
