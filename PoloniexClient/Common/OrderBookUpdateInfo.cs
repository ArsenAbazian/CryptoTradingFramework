using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public enum OrderBookUpdateType { Add, Modify, Remove, RefreshAll }
    public class OrderBookUpdateInfo {
        public OrderBookEntry Entry { get; set; }
        public OrderBookEntryType Type { get; set; }
        public int SeqNo { get; set; }
        public OrderBookUpdateType Action { get; set; }
    }
}
