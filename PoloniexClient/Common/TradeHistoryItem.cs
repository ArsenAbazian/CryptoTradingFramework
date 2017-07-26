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
        public double Amount { get; set; }
        public double Rate { get; set; }
        public double Total { get; set; }
        public TradeFillType Fill { get; set;}
        public TradeType Type { get; set; }
        public int Id { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
        public double Current { get; set; }
    }
}
