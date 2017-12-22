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
        public decimal Amount { get; set; }
        public decimal Total { get; set; }
        public OrderType Type { get; set; }

        public void Calculate() {
            Date = DateTime.UtcNow;
            decimal amount = 0;
            decimal total = 0;
            foreach(TradeEntry e in Trades) {
                amount += e.Amount;
                total += e.Total;
            }
            Amount = amount;
            Total = total;
        }
    }

    public class TradeEntry {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
        public long Id { get; set; }
        public OrderType Type { get; set; }
    }
}
