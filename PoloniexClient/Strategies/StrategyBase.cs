using CryptoMarketClient.Common;
using DevExpress.XtraCharts;
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
            Enabled = false;
            DemoMode = true;
        }

        public bool Enabled { get; set; }
        public bool DemoMode { get; set; }

        public TickerBase Ticker { get; private set; }
        public abstract object HistoryDataSource { get; }
        public void OnTick() {
            if(!Enabled)
                return;
            OnTickCore();
        }
        protected abstract void OnTickCore();
        public abstract string Name { get; }
        protected virtual void Log(LogType logType, string text) {
            LogManager.Default.AddMessage(logType, Name + "-" + Ticker.HostName + "-" + Ticker.Name, text);
        }
        protected string GetRateAmountString(decimal rate, decimal amount) {
            return amount.ToString("0.########") + " by " + rate.ToString("0.########");
        }
        protected virtual bool Buy(decimal rate, decimal amount) {
            if(!DemoMode && !Ticker.Buy(rate, amount)) {
                Log(LogType.Error, "Buy " + GetRateAmountString(rate, amount) + " failed.");
                return false;
            }
            Log(LogType.Success, "Buy " + GetRateAmountString(rate, amount) + " succeded.");
            return true;
        }
        protected virtual bool Sell(decimal rate, decimal amount) {
            if(!DemoMode && !Ticker.Sell(rate, amount)) {
                Log(LogType.Error, "Sell " + GetRateAmountString(rate, amount) + " failed.");
                return false;
            }
            Log(LogType.Success, "Sell " + GetRateAmountString(rate, amount) + " succeded.");
            return true;
        }
        protected virtual bool PlaceBid(decimal rate, decimal amount) {
            if(!DemoMode && !Ticker.Buy(rate, amount)) {
                Log(LogType.Error, "Place Bid " + GetRateAmountString(rate, amount) + " failed.");
                return false;
            }
            Log(LogType.Success, "Place Bid " + GetRateAmountString(rate, amount) + " succeded.");
            return true;
        }
        protected virtual bool PlaceAsk(decimal rate, decimal amount) {
            if(!DemoMode && !Ticker.Sell(rate, amount)) {
                Log(LogType.Error, "Place Ask " + GetRateAmountString(rate, amount) + " failed.");
                return false;
            }
            Log(LogType.Success, "Place Ask " + GetRateAmountString(rate, amount) + " succeded.");
            return true;
        }

        protected abstract void Vizualize(ChartControl chart);
    }

    public enum StrategyOperation {
        BuyNow,
        SellNow,
        BuyPostponed,
        SellPostponed
    }

    public class StrategyHistoryItem {
        public DateTime Time { get; set; }
        public StrategyOperation Operation { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal Total { get; set; }
    }
}
