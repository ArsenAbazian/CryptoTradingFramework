using Crypto.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Strategies.Stupid {
    public class MarketMakingStrategy : Custom.CustomTickerStrategy {
        public override string TypeName => "Simple Market Making";
        protected override void OnTickCore() {
            foreach(Ticker t in Tickers) {
                CheckUpdateTickerOrderBook(t);
            }
        }

        MarketMakingStrategyData lastData;
        protected virtual void CheckUpdateTickerOrderBook(Ticker t) {
            double spread = (t.OrderBook.Asks[0].Value - t.OrderBook.Bids[0].Value);
            if(this.lastData == null || this.lastData.Spread != spread) {
                this.lastData = new MarketMakingStrategyData() { Time = DataProvider.CurrentTime, Spread = spread,
                    Bid = t.OrderBook.Bids[0].Value,
                Ask = t.OrderBook.Asks[0].Value};
                double bv = t.OrderBook.Bids.Sum(e => e.Amount * e.Value);
                double av = t.OrderBook.Asks.Sum(e => e.Amount * e.Value);
                this.lastData.BidVolume = bv;
                this.lastData.AskVolume = av;
                StrategyData.Add(this.lastData);
            }
        }
        protected override void InitializeDataItems() {
            base.InitializeDataItems();
            StrategyDataItemInfo time = TimeItem("Time"); time.UseCustomTimeUnit = true; time.TimeUnit = StrategyDateTimeMeasureUnit.Millisecond; time.TimeUnitMeasureMultiplier = 1;
            StrategyDataItemInfo bid = DataItem("Bid"); bid.ChartType = ChartType.Line; bid.Color = Exchange.BidColor;
            StrategyDataItemInfo ask = DataItem("Ask"); ask.ChartType = ChartType.Line; ask.Color = Exchange.AskColor;
            StrategyDataItemInfo bidv = DataItem("BidVolume"); bidv.ChartType = ChartType.Area; bidv.Color = System.Drawing.Color.FromArgb(30, Exchange.BidColor); bidv.PanelName = "Volumes";
            StrategyDataItemInfo askv = DataItem("AskVolume"); askv.ChartType = ChartType.Area; askv.Color = System.Drawing.Color.FromArgb(30, Exchange.AskColor); askv.PanelName = "Volumes";
            DataItem("Spread").PanelName = "Spread";
        }
    }

    public class MarketMakingStrategyData {
        public DateTime Time { get; set; }
        public double Spread { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
        public double BidVolume { get; set; }
        public double AskVolume { get; set; }
    }
}
