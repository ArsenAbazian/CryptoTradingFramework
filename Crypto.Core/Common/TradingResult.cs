using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Common {
    [Serializable]
    public class TradingResult {
        public string OrderId { get; set; }
        [XmlIgnore]
        public Ticker Ticker { get; set; }
        public List<TradeEntry> Trades { get; } = new List<TradeEntry>();
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public double Value { get; set; }
        public double Total { get; set; }
        public string OrderStatus { get; set; }
        public bool Filled { get; set; }
        public OrderType Type { get; set; }
        public PositionSide PositionSide { get; set; } = PositionSide.Long;

        public void Calculate() {
            Date = DateTime.UtcNow;
            double amount = 0;
            double total = 0;
            for(int i = 0; i < Trades.Count; i++) {
                TradeEntry e = Trades[i];
                amount += e.Amount;
                total += e.Total;
            }
            Amount = amount;
            Total = total;
        }
    }

    [Serializable]
    public class TradeEntry {
        [XmlIgnore]
        public Ticker Ticker { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public double Rate { get; set; }
        public double Total { get; set; }
        public double Fee { get; set; }
        public string FeeAsset { get; set; }
        public string Id { get; set; }
        public OrderType Type { get; set; }
    }
}
