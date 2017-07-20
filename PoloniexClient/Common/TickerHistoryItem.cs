using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public class TickerHistoryItem {
        public DateTime Time { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
        public double Current { get; set; }
    }
}
