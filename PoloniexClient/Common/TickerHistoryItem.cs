using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public class TickerHistoryItem {
        public DateTime Time { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public decimal Current { get; set; }
    }
}
