using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public enum TradeFillType { Fill, PartialFill }
    public enum TradeType { Buy, Sell }

    public class TradeHistoryItem {
        public DateTime Time { get; set; }
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
        public decimal Fee { get; set; }
        public long OrderNumber { get; set; }
        public TradeFillType Fill { get; set;}
        public TradeType Type { get; set; }
        public long Id { get; set; }
        public long GlobalId { get; set; }
    }
}
