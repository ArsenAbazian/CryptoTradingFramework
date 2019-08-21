using DevExpress.Utils.Serializing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Strategies {
    public class StaticGridStrategy : GridStrategyBase {

        public StaticGridStrategy(Ticker ticker) : base(ticker) { }

        [XtraSerializableProperty]
        public int GridCount { get; set; }
        [XtraSerializableProperty]
        public double GridDelta { get; set; }
        [XtraSerializableProperty]
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
