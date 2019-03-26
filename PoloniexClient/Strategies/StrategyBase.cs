using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Strategies {
    public abstract class TickerStrategyBase  {
        public TickerStrategyBase(Ticker ticker) {
            Ticker = ticker;
            ticker.Strategies.Add(this);
            Enabled = false;
            DemoMode = true;
        }

        public bool Enabled { get; set; }
        public bool DemoMode { get; set; }

        public Ticker Ticker { get; private set; }
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
        protected string GetRateAmountString(double rate, double amount) {
            return amount.ToString("0.########") + " by " + rate.ToString("0.########");
        }
        protected virtual bool Buy(double rate, double amount) {
            if(!DemoMode && Ticker.Buy(rate, amount) == null) {
                Log(LogType.Error, "Buy " + GetRateAmountString(rate, amount) + " failed.");
                return false;
            }
            Log(LogType.Success, "Buy " + GetRateAmountString(rate, amount) + " succeded.");
            return true;
        }
        protected virtual bool Sell(double rate, double amount) {
            if(!DemoMode && Ticker.Sell(rate, amount) == null) {
                Log(LogType.Error, "Sell " + GetRateAmountString(rate, amount) + " failed.");
                return false;
            }
            Log(LogType.Success, "Sell " + GetRateAmountString(rate, amount) + " succeded.");
            return true;
        }
        protected virtual bool PlaceBid(double rate, double amount) {
            if(!DemoMode && Ticker.Buy(rate, amount) == null) {
                Log(LogType.Error, "Place Bid " + GetRateAmountString(rate, amount) + " failed.");
                return false;
            }
            Log(LogType.Success, "Place Bid " + GetRateAmountString(rate, amount) + " succeded.");
            return true;
        }
        protected virtual bool PlaceAsk(double rate, double amount) {
            if(!DemoMode && Ticker.Sell(rate, amount) == null) {
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
        public double Rate { get; set; }
        public double Amount { get; set; }
        public double Total { get; set; }
    }
}
