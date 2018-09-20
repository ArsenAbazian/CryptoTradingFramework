using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public class TradingResult {
        public long OrderNumber { get; set; }
        public List<TradeEntry> Trades { get; } = new List<TradeEntry>();
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public double Total { get; set; }
        public OrderType Type { get; set; }

        public void Calculate() {
            Date = DateTime.UtcNow;
            double amount = 0;
            double total = 0;
            foreach(TradeEntry e in Trades) {
                amount += e.Amount;
                total += e.Total;
            }
            Amount = amount;
            Total = total;
        }
    }

    public class TradeEntry {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public double Rate { get; set; }
        public double Total { get; set; }
        public string Id { get; set; }
        public OrderType Type { get; set; }
    }
}
