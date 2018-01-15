using CryptoMarketClient.Analytics;
using CryptoMarketClient.Common;
using CryptoMarketClient.Strategies;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.Utils.Serializing;
using DevExpress.XtraCharts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public abstract class TickerBase : IXtraSerializable {
        public TickerBase(Exchange exchange) {
            Exchange = exchange;
            OrderBook = new OrderBook(this);
            BidAskChart = CreateChartSnapshotControl();
            OrderBookChart = CreateOrderBookChartControl();
            OrderBookSnapshot = CreateOrderBookSnapshotImage();
            BidAskSnapshot = CreateChartSnapshotImage();
            UpdateMode = TickerUpdateMode.Self;
            IsActual = true;
        }

        protected string FileName {
            get {
                return Exchange.TickersDirectory + "\\" + Name.ToLower() + ".xml";
            }
        }

        #region Settings
        public void Save() {
            SaveLayoutToXml(FileName);
        }
        public void Load() {
            if(!File.Exists(FileName))
                return;
            RestoreLayoutFromXml(FileName);
        }

        void IXtraSerializable.OnEndDeserializing(string restoredVersion) {

        }

        void IXtraSerializable.OnEndSerializing() {

        }

        void IXtraSerializable.OnStartDeserializing(LayoutAllowEventArgs e) {

        }

        void IXtraSerializable.OnStartSerializing() {
            
        }

        protected XtraObjectInfo[] GetXtraObjectInfo() {
            ArrayList result = new ArrayList();
            result.Add(new XtraObjectInfo("Ticker", this));
            return (XtraObjectInfo[])result.ToArray(typeof(XtraObjectInfo));
        }
        protected virtual bool SaveLayoutCore(XtraSerializer serializer, object path) {
            System.IO.Stream stream = path as System.IO.Stream;
            if(stream != null)
                return serializer.SerializeObjects(GetXtraObjectInfo(), stream, this.GetType().Name);
            else
                return serializer.SerializeObjects(GetXtraObjectInfo(), path.ToString(), this.GetType().Name);
        }
        protected virtual void RestoreLayoutCore(XtraSerializer serializer, object path) {
            System.IO.Stream stream = path as System.IO.Stream;
            if(stream != null)
                serializer.DeserializeObjects(GetXtraObjectInfo(), stream, this.GetType().Name);
            else
                serializer.DeserializeObjects(GetXtraObjectInfo(), path.ToString(), this.GetType().Name);
        }
        //layout
        public virtual void SaveLayoutToXml(string xmlFile) {
            SaveLayoutCore(new XmlXtraSerializer(), xmlFile);
        }
        public virtual void RestoreLayoutFromXml(string xmlFile) {
            RestoreLayoutCore(new XmlXtraSerializer(), xmlFile);
        }
        #endregion

        public Exchange Exchange { get; private set; }
        public int Index { get; set; }
        public virtual string MarketName { get; set; }
        public abstract string CurrencyPair { get; set; }
        public bool IsSelected { get; set; }
        public bool IsOpened { get; set; }

        public TickerUpdateMode UpdateMode {
            get;
            set;
        }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true)]
        public List<TrailingSettings> Trailings { get; } = new List<TrailingSettings>();

        TrailingSettings XtraCreateTrailingsItem(XtraItemEventArgs e) {
            return new TrailingSettings(this);
        }
        void XtraSetIndexTrailingsItem(XtraSetItemIndexEventArgs e) {
            if(e.NewIndex == -1) {
                Trailings.Add((TrailingSettings)e.Item.Value);
                return;
            }
            Trailings.Insert(e.NewIndex, (TrailingSettings)e.Item.Value);
        }

        public List<TradingResult> Trades { get; } = new List<TradingResult>();
        public List<TradeHistoryItem> MyTradeHistory { get; } = new List<TradeHistoryItem>();
        public List<OpenedOrderInfo> OpenedOrders { get; } = new List<OpenedOrderInfo>();
        public BindingList<TickerHistoryItem> History { get; } = new BindingList<TickerHistoryItem>();
        public List<TradeHistoryItem> TradeHistory { get; } = new List<TradeHistoryItem>();
        public BindingList<TradeStatisticsItem> TradeStatistic { get; } = new BindingList<TradeStatisticsItem>();
        public List<TickerStrategyBase> Strategies { get; } = new List<TickerStrategyBase>();
        public BindingList<CandleStickData> CandleStickData { get; set; } = new BindingList<CryptoMarketClient.CandleStickData>();
        public BindingList<CurrencyStatusHistoryItem> MarketCurrencyStatusHistory { get; set; } = new BindingList<CurrencyStatusHistoryItem>();

        Image BidAskSnapshot { get; }
        Image OrderBookSnapshot { get; }

        protected SnapshotChartControl BidAskChart { get; private set; }
        protected SnapshotChartControl OrderBookChart { get; private set; }

        public virtual void MakeBidAskSnapshot() {
            BidAskChart.Render(BidAskSnapshot);
        }
        public virtual void MakeOrderBookSnapshot() {
            OrderBookChart.Render(OrderBookSnapshot);
        }
        public Color AskColor {
            get { return System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192))))); }
        }

        public Color BidColor {
            get { return System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192))))); }
        }
        Series CreateLineSeries(string str, Color color) {
            Series s = new Series();
            s.Name = str;
            s.ArgumentDataMember = "Time";
            s.ValueDataMembers.AddRange(str);
            s.ValueScaleType = ScaleType.Numerical;
            s.ShowInLegend = true;
            StepLineSeriesView view = new StepLineSeriesView();
            view.Color = color;
            view.LineStyle.Thickness = (int)(21 * DpiProvider.Default.DpiScaleFactor);
            s.View = view;
            s.DataSource = History;
            return s;
        }
        Series CreateStepAreaSeries(OrderBookEntry[] list, Color color) {
            Series s = new Series();
            s.Name = "Amount";
            s.ArgumentDataMember = "Value";
            s.ValueDataMembers.AddRange("Amount");
            s.ValueScaleType = ScaleType.Numerical;
            s.ShowInLegend = true;
            StepAreaSeriesView view = new StepAreaSeriesView();
            view.Color = color;
            s.View = view;
            s.DataSource = list;
            return s;
        }
        protected virtual SnapshotChartControl CreateChartSnapshotControl() {
            SnapshotChartControl chart = new SnapshotChartControl();
            //chart.Series.Add(CreateLineSeries("Bid", BidColor));
            //chart.Series.Add(CreateLineSeries("Ask", AskColor));
            chart.Size = BidAskSnapshotSize;
            chart.CreateControl();
            return chart;
        }
        protected virtual SnapshotChartControl CreateOrderBookChartControl() {
            SnapshotChartControl chart = new SnapshotChartControl();
            //chart.Series.Add(CreateStepAreaSeries(OrderBook.Bids, BidColor));
            //chart.Series.Add(CreateStepAreaSeries(OrderBook.Asks, AskColor));
            chart.Size = OrderBookSnapshotSize;
            chart.CreateControl();
            return chart;
        }
        protected virtual Size BidAskSnapshotSize { get { return new Size(600, 400); } }
        protected virtual Size OrderBookSnapshotSize { get { return new Size(600, 200); } }
        protected virtual Image CreateOrderBookSnapshotImage() {
            Size sz = OrderBookSnapshotSize;
            return new Bitmap(sz.Width, sz.Height);
        }
        protected virtual Image CreateChartSnapshotImage() {
            Size sz = BidAskSnapshotSize;
            return new Bitmap(sz.Width, sz.Height);
        }

        public OrderBook OrderBook { get; private set; }
        public abstract string Name { get; }
        string lowestAskString = null;
        public string LowestAskString {
            get {
                if(lowestAskString == null)
                    lowestAskString = GetStringWithChangePercent(LowestAsk, AskChange);
                return lowestAskString;
            }
        }
        string highestBidString = null;
        public string HighestBidString {
            get {
                if(highestBidString == null)
                    highestBidString = GetStringWithChangePercent(HighestBid, BidChange);
                return highestBidString;
            }
        }
        double lowestAsk;
        public double LowestAsk {
            get { return lowestAsk; }
            set {
                if(value != LowestAsk && LowestAsk != 0) {
                    AskChange = value - LowestAsk;
                    lowestAskString = null;
                }
                lowestAsk = value;
            }
        }
        double highestBid;
        public double HighestBid {
            get { return highestBid; }
            set {
                if(value != HighestBid && HighestBid != 0) {
                    BidChange = value - HighestBid;
                    highestBidString = null;
                }
                highestBid = value;
            }
        }
        public bool IsFrozen { get; set; }
        double last;
        public double Last {
            get { return last; }
            set {
                if(value == Last)
                    return;
                if(Last != 0)
                    Change = value - Last;
                lastString = null;
                last = value;
            }
        }
        string lastString = null;
        public string LastString {
            get {
                if(lastString == null)
                    lastString = GetStringWithChangePercent(Last, Change);
                return lastString;
            }
        }
        protected virtual string GetStringWithChangePercent(double value, double change) {
            return string.Format("<b>{0:0.########}</b> <size=-2>{1:0.##}%</size>", value, change / value * 100);
        }
        public double BaseVolume { get; set; }
        public double Volume { get; set; }
        public double Hr24High { get; set; }
        public double Hr24Low { get; set; }
        public double Change { get; set; }
        public double Spread { get { return LowestAsk - HighestBid; } }
        public double BidChange { get; set; }
        public double AskChange { get; set; }
        public abstract double Fee { get; }

        public TickerBase UsdTicker { get; set; }

        public abstract double BaseCurrencyBalance { get; }
        public abstract double MarketCurrencyBalance { get; }
        public abstract BalanceBase BaseBalanceInfo { get; }
        public abstract BalanceBase MarketBalanceInfo { get; }
        public abstract double MarketCurrencyTotalBalance { get; }
        public abstract bool MarketCurrencyEnabled { get; }

        public string BaseCurrency { get; set; }
        public string MarketCurrency { get; set; }
        public abstract string HostName { get; }
        public DateTime Time { get; set; }
        public int CandleStickPeriodMin { get; set; } = 1;
        public DateTime LastTradeStatisticTime { get; set; }
        public long LastTradeId { get; set; }
        public abstract string WebPageAddress { get; }
        public bool UpdateOrderBook() {
            bool res = Exchange.UpdateOrderBook(this);
            if(res) {
                HighestBid = OrderBook.Bids[0].Value;
                LowestAsk = OrderBook.Asks[0].Value;
                Time = DateTime.UtcNow;
                UpdateHistoryItem();
                CandleStickChartHelper.AddCandleStickData(CandleStickData, History.Last(), CandleStickPeriodMin * 60);
            }
            return res;
        }
        public bool ProcessOrderBook(string text) { return Exchange.ProcessOrderBook(this, text); }
        public bool UpdateTicker() {
            bool res = Exchange.UpdateTicker(this);
            if(res) {
                Time = DateTime.UtcNow;
                UpdateHistoryItem();
                CandleStickChartHelper.AddCandleStickData(CandleStickData, History.Last(), CandleStickPeriodMin * 60);
            }
            return res;
        }
        public bool UpdateTrades() { return Exchange.UpdateTrades(this); }
        public bool UpdateOpenedOrders() { return Exchange.UpdateOpenedOrders(this); }
        public abstract string DownloadString(string address);

        TickerUpdateHelper updateHelper;
        protected TickerUpdateHelper UpdateHelper {
            get {
                if(updateHelper == null)
                    updateHelper = new TickerUpdateHelper(this);
                return updateHelper;
            }
        }

        RateLimiting.RateGate apiRate = new RateLimiting.RateGate(6, TimeSpan.FromSeconds(1));
        protected RateLimiting.RateGate ApiRate {
            get { return apiRate; }
        }

        protected internal void RaiseHistoryItemAdded() {
            if(HistoryItemAdd != null)
                HistoryItemAdd(this, EventArgs.Empty);
        }
        protected internal void RaiseChanged() {
            if(Changed != null)
                Changed(this, EventArgs.Empty);
        }

        protected internal void RaiseTradeHistoryAdd() {
            if(TradeHistoryAdd != null)
                TradeHistoryAdd(this, EventArgs.Empty);
        }

        public event EventHandler HistoryItemAdd;
        public event EventHandler TradeHistoryAdd;
        public event EventHandler Changed;

        public abstract bool UpdateBalance(CurrencyType type);
        public abstract string GetDepositAddress(CurrencyType type);
        public abstract bool Buy(double lowestAsk, double amount);
        public abstract bool Sell(double highestBid, double amount);
        public abstract bool MarketSell(double amount);
        public abstract bool Withdraw(string currency, string address, double amount);

        protected WebClient WebClient { get; } = new MyWebClient();
        public bool IsUpdating { get; set; }
        public long UpdateTimeMs { get; set; }
        public long StartUpdateTime { get; set; }
        public long LastUpdateTime { get; set; }
        public Task Task { get; set; }
        public bool IsActual { get; set; }
        public bool PrevMarketCurrencyEnabled { get; set; }

        public void UpdateHistoryItem() {
            TickerHistoryItem last = History.Count == 0 ? null : History.Last();
            if(History.Count > 72000) {
                for(int i = 0; i < 2000; i++)
                    History.RemoveAt(0);
            }
            if(last != null) {
                if(last.Ask == LowestAsk && last.Bid == HighestBid && last.Current == Last)
                    return;
                Change = ((Last - last.Current) / last.Current) * 100;
                if(last.Bid != HighestBid)
                    BidChange = (HighestBid - last.Bid) * 100;
                if(last.Ask != LowestAsk)
                    AskChange = LowestAsk - last.Ask;
            }
            History.Add(new TickerHistoryItem() { Time = Time, Ask = LowestAsk, Bid = HighestBid, Current = Last });
            RaiseHistoryItemAdded();
        }
        public void UpdateMarketCurrencyStatusHistory() {
            if(MarketCurrencyStatusHistory.Count == 0) {
                MarketCurrencyStatusHistory.Add(new CurrencyStatusHistoryItem() { Enabled = MarketCurrencyEnabled, Time = DateTime.UtcNow });
                return;
            }
            if(MarketCurrencyStatusHistory.Last().Enabled == MarketCurrencyEnabled)
                return;
            MarketCurrencyStatusHistory.Add(new CurrencyStatusHistoryItem() { Enabled = MarketCurrencyEnabled, Time = DateTime.UtcNow });
        }
        public BindingList<CandleStickData> GetCandleStickData(int candleStickPeriodMin, DateTime start, int periodInSeconds) {
            return Exchange.GetCandleStickData(this, candleStickPeriodMin, start, periodInSeconds);
        }
        public virtual bool UpdateMyTrades() {
            return Exchange.UpdateMyTrades(this);
        }
        public void UpdateTrailings() {
            lock(this) {
                foreach(TrailingSettings tr in Trailings) {
                    tr.Update();
                }
            }
            RaiseChanged();
        }
    }

    public class SnapshotChartControl : ChartControl {
        public void Render(Image image) {
            using(Graphics g = Graphics.FromImage(image)) {
                PaintEventArgs e = new PaintEventArgs(g, new Rectangle(0, 0, Width, Height));
                OnPaint(e);
            }
        }
    }

    public enum TickerUpdateMode {
        Self,
        Arbitrage
    }
}
