using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public enum OrderBookEntryType { Bid, Ask }
    public class OrderBookEntry {
        public decimal Value { get; set; }
        public decimal Amount { get; set; }
        public decimal Volume { get; set; }
        public decimal VolumePercent { get; set; }
        public void Clear() {
            Value = 0;
            Amount = 0;
        }
    }
}
