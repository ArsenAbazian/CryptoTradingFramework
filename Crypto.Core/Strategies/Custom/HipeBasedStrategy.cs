using Crypto.Core.Helpers;
using Crypto.Core.Indicators;
using Crypto.Core;
using Crypto.Core.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Strategies.Custom {
    public class HipeBasedStrategy : CustomTickerStrategy, ISimulationDataProvider {

        ClassicArbitrageListener listener;
        [XmlIgnore]
        protected ClassicArbitrageListener Listener {
            get {
                if(listener == null) {
                    listener = new ClassicArbitrageListener();
                    listener.ArbitrageChanged += OnArbitrageChanged;
                }
                return listener;
            }
        }

        [Browsable(false)]
        public override double MinDepositForOpenPosition { get; set; } = 100;

        [Browsable(false)]
        [XmlIgnore]
        public ResizeableArray<TickerCollection> Items { get { return Listener.ArbitrageList; } }

        [FileName("Xml Files (*.xml)|*.xml|All Files (*.*)|*.*")]
        [PropertyTab("Simulation")]
        public string SimulationDataFile { get; set; }
        ResizeableArray<IInputDataWithTime> ISimulationDataProvider.LoadData() {
            if(string.IsNullOrEmpty(SimulationDataFile))
                return null;
            if(!File.Exists(SimulationDataFile))
                return null;

            ArbitrageHistoryHelper helper = ArbitrageHistoryHelper.FromFile(SimulationDataFile);
            ResizeableArray<IInputDataWithTime> res = new ResizeableArray<IInputDataWithTime>(helper.History.Count);
            foreach(ArbitrageStatisticsItem item in helper.History) {
                res.Add(item);
            }
            
            return res;
        }

        protected override void CheckTickerSpecified(List<StrategyValidationError> list) {
            
        }

        void ISimulationDataProvider.OnNewItem(IInputDataWithTime item) {
            ArbitrageStatisticsItem st = (ArbitrageStatisticsItem)item;
            TickerCollection coll = Listener.ArbitrageList.FirstOrDefault(c => c.BaseCurrency == st.BaseCurrency && c.MarketCurrency == st.MarketCurrency);

            if(coll == null)
                return;

            coll.Arbitrage.LowestAskHost = st.LowestAskHost;
            coll.Arbitrage.HighestBidHost = st.HighestBidHost;
            coll.Arbitrage.Time = st.Time;
            coll.Arbitrage.LowestAskEnabled = st.LowestAskEnabled;
            coll.Arbitrage.HighestBidEnabled = st.HighestBidEnabled;
            coll.Arbitrage.LowestAsk = st.LowestAsk;
            coll.Arbitrage.HighestBid = st.HighestBid;
            coll.Arbitrage.Spread = st.Spread;
            coll.Arbitrage.Amount = st.Amount;
            coll.Arbitrage.MaxProfit = st.MaxProfit;
            coll.Arbitrage.MaxProfitUSD = st.MaxProfitUSD;
            if(coll.Arbitrage.UsdTicker != null)
                coll.Arbitrage.UsdTicker.Last = st.RateInUSD;

            coll.Arbitrage.History.Add(st);

            OnArbitrageChanged(Listener, new ArbitrageChangedEventArgs() { Arbitrage = coll.Arbitrage, TickersInfo = coll });
    }

        void ISimulationDataProvider.Connect() {
            Listener.EnterSimulation();
        }

        void ISimulationDataProvider.Disconnect() {
            Listener.ExitSimulation();
        }

        protected override void InitializeDataItems() {
            base.InitializeDataItems();

            TimeItem("Time");
            DataItem("BaseCurrency").Visibility = DataVisibility.Table;
            DataItem("MarketCurrency").Visibility = DataVisibility.Table;
            DataItem("LowestAsk").Visibility = DataVisibility.Table;
            DataItem("HighestBid").Visibility = DataVisibility.Table;
            DataItem("Spread").Visibility = DataVisibility.Table;
            DataItem("Profit").Visibility = DataVisibility.Table;
            DataItem("ProfitUSD").Visibility = DataVisibility.Table;
            DataItem("Mark").Visibility = DataVisibility.Table;

            var trend = DataItem("TrendPreview"); trend.Visibility = DataVisibility.Table;
            var candle = StrategyDataItemInfo.CandleStickItem(trend.Children); candle.Type = DataType.ChartData; candle.Visibility = DataVisibility.Chart; candle.BindingSource = "CandleSticks"; candle.Name = "KLine";
            trend.DetailInfo = candle;
        }

        private void OnArbitrageChanged(object sender, ArbitrageChangedEventArgs e) {
            HipeBaseStrategyItemData data = new HipeBaseStrategyItemData();

            data.Time = DataProvider.CurrentTime;
            data.BaseCurrency = e.TickersInfo.BaseCurrency;
            data.MarketCurrency = e.TickersInfo.MarketCurrency;
            data.LowestAsk = e.Arbitrage.LowestAsk;
            data.HighestBid = e.Arbitrage.HighestBid;
            data.Spread = e.Arbitrage.Spread;
            data.Profit = e.Arbitrage.MaxProfit;
            data.ProfitUSD = e.Arbitrage.MaxProfitUSD;

            if(HasPositiveArbitrage(e.Arbitrage)) {
                if(!HasAlreadyHipe(data))
                    data.Mark = "HP";
            }
            StrategyData.Add(data);
        }

        private bool HasAlreadyHipe(HipeBaseStrategyItemData data) {
            for(int i = StrategyData.Count - 1; i >= 0; i--) {
                HipeBaseStrategyItemData prev = (HipeBaseStrategyItemData)StrategyData[i];
                if((data.Time - prev.Time).TotalHours > 24)
                    return false;
                if(prev.BaseCurrency != data.BaseCurrency || prev.MarketCurrency != data.MarketCurrency)
                    continue;
                if(prev.Mark == "HP")
                    return true;
            }
            return false;
        }

        private bool HasPositiveArbitrage(ArbitrageInfo arbitrage) {
            if(arbitrage.History.Count < 5)
                return false;
            int start = arbitrage.History.Count - 1;
            for(int i = start; i > start - 5; i--) {
                if(arbitrage.History[i].MaxProfitUSD < 1)
                    return false;
                if(arbitrage.History[i].LowestAskHost != "Poloniex")
                    return false;
            }
            DateTime startTime = arbitrage.History[start - 5].Time;
            DateTime endTime = arbitrage.History[start].Time;
            if((endTime - startTime).TotalMinutes > 1)
                return false;
            if(arbitrage.History[start].HighestBid < arbitrage.History[start - 5].LowestAsk)
                return false;
            return true;
        }

        public override bool SupportSimulation => true;
        public override bool Start() {
            bool res = base.Start();
            if(res) {
                ArbitrageHistoryHelper.AllowSaveHistory = true;
                if(SimulationMode)
                    Listener.EnterSimulation();
                else 
                    Listener.Start();
            }
            return res;
        }
        
        protected override void OnTickCore() {
            // :)
        }
    }

    public class HipeBaseStrategyItemData {
        public DateTime Time { get; set; }
        public string BaseCurrency { get; set; }
        public string MarketCurrency { get; set; }
        ResizeableArray<CandleStickData> candleSticks;
        [XmlIgnore]
        public ResizeableArray<CandleStickData> CandleSticks {
            get {
                if(string.IsNullOrEmpty(Mark))
                    return null;
                if(candleSticks == null)
                    candleSticks = CreateCandleSticks();
                return candleSticks;
            }
        }
        double[] trendPreview;
        [XmlIgnore]
        public double[] TrendPreview {
            get {
                if(string.IsNullOrEmpty(Mark))
                    return null;
                if(trendPreview != null)
                    return trendPreview;
                int count = CandleSticks == null ? 0 : CandleSticks.Count;
                trendPreview = new double[count];
                for(int i = 0; i < count; i++)
                    trendPreview[i] = CandleSticks[i].Close;
                return trendPreview;
            }
        }
        private ResizeableArray<CandleStickData> CreateCandleSticks() {
            Ticker ticker = PoloniexExchange.Default.Tickers.FirstOrDefault(t => t.BaseCurrency == BaseCurrency && t.MarketCurrency == MarketCurrency);
            if(ticker == null)
                throw new Exception("Ticker not found");
            candleSticks = ticker.GetCandleStickData(5, Time, 5 * 50 * 60);
            return candleSticks;
        }

        public string Mark { get; set; }
        public double LowestAsk { get; set; }
        public double HighestBid { get; set; }
        public double Spread { get; set; }
        public double Profit { get; set; }
        public double ProfitUSD { get; set; }
    }

    public interface ISimulationDataProvider {
        ResizeableArray<IInputDataWithTime> LoadData();
        void OnNewItem(IInputDataWithTime item);
        void Connect();
        void Disconnect();
    }

    public class HipeBasedStrategyItemsInfoOwner : IStrategyDataItemInfoOwner {
        public HipeBasedStrategyItemsInfoOwner(HipeBasedStrategy strategy) {
            Strategy = strategy;
            InitializeDataItemInfos();
            InitializeItems();
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        protected virtual void InitializeItems() {
            foreach(TickerCollection t in Strategy.Items) {
                HipeBaseStrategyArbitrageDataItem item = new HipeBaseStrategyArbitrageDataItem();
                item.BaseCurrency = t.BaseCurrency;
                item.MarketCurrency = t.MarketCurrency;
                item.ArbitrageHistory = t.Arbitrage.History;
                item.ArbitragePreview = item.ArbitrageHistory.Select(h => h.MaxProfitUSD).ToArray();
                this.items.Add(item);
            }
        }

        protected virtual void InitializeDataItemInfos() {
            StrategyDataItemInfo.DataItem(this.dataItemInfos, "BaseCurrency").Visibility = DataVisibility.Table;
            StrategyDataItemInfo.DataItem(this.dataItemInfos, "MarketCurrency").Visibility = DataVisibility.Table;
            StrategyDataItemInfo preview = StrategyDataItemInfo.DataItem(this.dataItemInfos, "ArbitragePreview"); preview.Type = DataType.ChartData; preview.Visibility = DataVisibility.Table;
            StrategyDataItemInfo kline = StrategyDataItemInfo.CandleStickItem(preview.Children); kline.BindingSource = "CandleSticks"; kline.Visibility = DataVisibility.Chart;
            StrategyDataItemInfo history = StrategyDataItemInfo.DataItem(preview.Children, "Spread"); history.Color = Color.FromArgb(0x60, System.Drawing.Color.Green); history.ChartType = ChartType.Area; history.PanelName = "Arbitrage"; history.BindingSource = "ArbitrageHistory"; history.Visibility = DataVisibility.Chart;
        }

        public HipeBasedStrategy Strategy {
            get; private set;
        }

        string IStrategyDataItemInfoOwner.Name => Strategy.Name;

        List<StrategyDataItemInfo> dataItemInfos = new List<StrategyDataItemInfo>();
        List<StrategyDataItemInfo> IStrategyDataItemInfoOwner.DataItemInfos => dataItemInfos;

        ResizeableArray<object> items = new ResizeableArray<object>();
        ResizeableArray<object> IStrategyDataItemInfoOwner.Items => items;

        int IStrategyDataItemInfoOwner.MeasureUnitMultiplier { get { return 5; } set { } }
        StrategyDateTimeMeasureUnit IStrategyDataItemInfoOwner.MeasureUnit { get { return StrategyDateTimeMeasureUnit.Minute; } set { } }
    }

    public class HipeBaseStrategyArbitrageDataItem {
        public string BaseCurrency { get; set; }
        public string MarketCurrency { get; set; }
        public double[] ArbitragePreview { get; set; }

        ResizeableArray<CandleStickData> candleSticks;
        public ResizeableArray<CandleStickData> CandleSticks {
            get {
                if(candleSticks == null)
                    candleSticks = DownloadCandleSticks();
                return candleSticks;
            }
        }

        private ResizeableArray<CandleStickData> DownloadCandleSticks() {
            Ticker ticker = PoloniexExchange.Default.Tickers.FirstOrDefault(t => t.BaseCurrency == BaseCurrency && t.MarketCurrency == MarketCurrency);
            if(ticker == null)
                throw new Exception("Ticker not found");
            candleSticks = ticker.GetCandleStickData(5, GetCandleSticksStartTime(), (int)GetCandlesticksRange());
            return candleSticks;
        }

        public ResizeableArray<ArbitrageStatisticsItem> ArbitrageHistory { get; set; }
        protected DateTime GetCandleSticksStartTime() {
            return ArbitrageHistory.Count > 0 ? ArbitrageHistory[0].Time : DateTime.MinValue;
        }
        protected long GetCandlesticksRange() {
            DateTime start = GetCandleSticksStartTime();
            if(start == DateTime.MinValue)
                return 0;
            return (int)((ArbitrageHistory[ArbitrageHistory.Count - 1].Time - start).TotalSeconds) + 1;
        } 
    }
}
