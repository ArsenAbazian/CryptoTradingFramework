using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Strategies {
    public abstract class TickerStrategyBase  {
        public TickerStrategyBase(TickerBase ticker) {
            Ticker = ticker;
            ticker.Strategies.Add(this);
            Result = StrategyResult.Wait;
            Enabled = false;
            DemoMode = true;
        }

        public bool Enabled { get; set; }
        public StrategyResult Result { get; protected set; }
        public TickerBase Ticker { get; private set; }
        public List<StrategyHistoryItem> History { get; } = new List<StrategyHistoryItem>();
        public abstract void Calculate();
        public bool Execute() {
            switch(Result) {
                case StrategyResult.Wait:
                    return true;
                case StrategyResult.Disable:
                    Enabled = false;
                    return true;
                case StrategyResult.Buy:
                    return Buy();
                case StrategyResult.Sell:
                    return Sell();
            }
            return true;
        }
        public bool DemoMode { get; set; }
        public decimal MaxAllowedBaseCurrencyAmount { get; set; }
        public decimal BaseCurrencyAmount { get; set; }
        public decimal MarketCurrencyAmount { get; set; }
        public decimal BaseCurrencySpentAmount { get; set; }
        public abstract bool Buy();
        public abstract bool Sell();
        protected bool BuyCore(decimal lowestAsk, decimal amount, decimal total) {
            if(DemoMode || Ticker.Buy(lowestAsk, amount)) {
                History.Add(new StrategyHistoryItem() {
                    Time = DateTime.Now,
                    Amount = amount,
                    Rate = lowestAsk,
                    Operation = StrategyOperation.Buy,
                    Total = total});
                return true;
            }
            return false;
        }
        protected bool SellCore(decimal highestBid, decimal amount, decimal total) {
            if(DemoMode || Ticker.Sell(highestBid, amount)) {
                History.Add(new StrategyHistoryItem() {
                    Time = DateTime.Now,
                    Amount = amount,
                    Rate = highestBid,
                    Operation = StrategyOperation.Sell,
                    Total = total
                });
                return true;
            }
            return false;
        }
    }

    public enum StrategyResult {
        Wait,
        Buy,
        Sell,
        Disable
    }

    public enum StrategyOperation {
        Buy,
        Sell
    }

    public class StrategyHistoryItem {
        public DateTime Time { get; set; }
        public StrategyOperation Operation { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal Total { get; set; }
    }
}
