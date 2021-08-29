using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Strategies {
    public class StaticGridStrategy : GridStrategyBase {

        public StaticGridStrategy() : base() { }

        public int GridCount { get; set; }
        public double GridDelta { get; set; }
        public double GridFirstLineDelta { get; set; }

        protected override void GenerateLines() {
            ClearLines();
            double latest = Ticker.Last;
            double bid = latest, ask = latest;
            double bidAmount = BaseCurrencyMaximum / GridCount;
            double askAmount = MarketCurrencyMaximum / GridCount;
            for(int i = 0; i < GridCount; i++) {
                double delta = i == 0 ? GridFirstLineDelta : GridDelta;
                bid -= delta;
                ask -= delta;
                BidLines.Add(new OrderInfo() { Rate = bid, Amount = bidAmount });
                AskLines.Add(new OrderInfo() { Rate = ask, Amount = askAmount });
            }
        }
    }
}
