using DevExpress.Utils.Serializing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Strategies {
    public class StaticGridStrategy : GridStrategyBase {

        public StaticGridStrategy(TickerBase ticker) : base(ticker) { }

        [XtraSerializableProperty]
        public int GridCount { get; set; }
        [XtraSerializableProperty]
        public decimal GridDelta { get; set; }
        [XtraSerializableProperty]
        public decimal GridFirstLineDelta { get; set; }

        protected override void GenerateLines() {
            ClearLines();
            decimal latest = Ticker.Last;
            decimal bid = latest, ask = latest;
            decimal bidAmount = BaseCurrencyMaximum / GridCount;
            decimal askAmount = MarketCurrencyMaximum / GridCount;
            for(int i = 0; i < GridCount; i++) {
                decimal delta = i == 0 ? GridFirstLineDelta : GridDelta;
                bid -= delta;
                ask -= delta;
                BidLines.Add(new OrderInfo() { Rate = bid, Amount = bidAmount });
                AskLines.Add(new OrderInfo() { Rate = ask, Amount = askAmount });
            }
        }
    }
}
