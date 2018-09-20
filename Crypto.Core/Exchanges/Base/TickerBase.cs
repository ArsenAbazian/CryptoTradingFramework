using CryptoMarketClient.Common;
using CryptoMarketClient.Exchanges.Base;
using CryptoMarketClient.Strategies;
using DevExpress.Utils;
using DevExpress.Utils.Serializing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public abstract class Ticker : IXtraSerializable, IComparable {
        public Ticker(Exchange exchange) {
            Exchange = exchange;
            OrderBook = new OrderBook(this);
            UpdateMode = TickerUpdateMode.Self;
            IsActual = true;
        }

        public int Code { get; set; }

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
            SuppressSave = false;
            DateTime invalid = new DateTime(2018, 1, 1);
            foreach(TrailingSettings set in Trailings) {
                if(set.StartDate < invalid)
                    set.StartDate = DateTime.Now;
                if(set.Date < invalid)
                    set.Date = DateTime.Now;
            }
        }

        void IXtraSerializable.OnEndSerializing() {

        }

        void IXtraSerializable.OnStartDeserializing(LayoutAllowEventArgs e) {
            SuppressSave = true;
        }

        void IXtraSerializable.OnStartSerializing() {
            
        }

        public bool IsOrderBookSubscribed { get; set; }
        public bool IsTradeHistorySubscribed { get; set; }
        public bool IsKlineSubscribed { get; set; }
        protected bool SuppressSave { get; set; }
        protected XtraObjectInfo[] GetXtraObjectInfo() {
            ArrayList result = new ArrayList();
            result.Add(new XtraObjectInfo("Ticker", this));
            return (XtraObjectInfo[])result.ToArray(typeof(XtraObjectInfo));
        }
        protected bool RestoringLayout { get; set; }
        protected virtual bool SaveLayoutCore(XtraSerializer serializer, object path) {
            System.IO.Stream stream = path as System.IO.Stream;
            if(stream != null)
                return serializer.SerializeObjects(GetXtraObjectInfo(), stream, this.GetType().Name);
            else
                return serializer.SerializeObjects(GetXtraObjectInfo(), path.ToString(), this.GetType().Name);
        }
        protected virtual void RestoreLayoutCore(XtraSerializer serializer, object path) {
            RestoringLayout = true;
            try {
                System.IO.Stream stream = path as System.IO.Stream;
                if(stream != null)
                    serializer.DeserializeObjects(GetXtraObjectInfo(), stream, this.GetType().Name);
                else
                    serializer.DeserializeObjects(GetXtraObjectInfo(), path.ToString(), this.GetType().Name);
            }
            finally {
                RestoringLayout = false;
            }
        }

        public CandleStickIntervalInfo GetCandleStickIntervalInfo() {
            TimeSpan interval = TimeSpan.FromMinutes(CandleStickPeriodMin);
            return Exchange.AllowedCandleStickIntervals.FirstOrDefault(i => i.Interval == interval);
        }

        //layout
        public virtual void SaveLayoutToXml(string xmlFile) {
            DateTime invalid = new DateTime(2018, 1, 1);
            foreach(TrailingSettings ts in Trailings) {
                if(ts.StartDate < invalid)
                    ts.StartDate = invalid;
            }
            SaveLayoutCore(new XmlXtraSerializer(), xmlFile);
        }
        public virtual void RestoreLayoutFromXml(string xmlFile) {
            RestoreLayoutCore(new XmlXtraSerializer(), xmlFile);
        }
        #endregion

        public bool SelectedInDependencyArbitrage { get; set; }

        public TickerFilter PriceFilter { get; set; }
        public TickerFilter QuantityFilter { get; set; }
        public List<TickerStrategyBase> Strategies { get; } = new List<TickerStrategyBase>();
        public Exchange Exchange { get; private set; }
        public int Index { get; set; }
        public virtual string MarketName { get; set; }
        public abstract string CurrencyPair { get; set; }
        public bool IsSelected { get; set; }
        public bool IsOpened { get; set; }
        //Image logo;
        //protected bool LogoLoaded { get; set; }
        //public Image Logo {
        //    get {
        //        if(!LogoLoaded) {
        //            LogoLoaded = true;
        //            logo = LoadLogoImage();
        //        }
        //        return logo;
        //    }
        //    set {
        //        logo = value;
        //    }
        //}
        //Image logo32;
        //public Image Logo32 {
        //    get {
        //        if(logo32 == null && Logo != null)
        //            logo32 = new Bitmap(Logo, LargeIconSize);
        //        return logo32;
        //    }
        //}
        public string LogoUrl { get; set; }
        //Image LoadLogoImage() {
        //    return Exchange.GetLogoImage(MarketCurrency);
        //}

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
        public List<TradeInfoItem> MyTradeHistory { get; } = new List<TradeInfoItem>();
        public List<OpenedOrderInfo> OpenedOrders { get; } = new List<OpenedOrderInfo>();
        public BindingList<TickerHistoryItem> History { get; } = new BindingList<TickerHistoryItem>();
        public List<TradeInfoItem> TradeHistory { get; } = new List<TradeInfoItem>();
        public BindingList<TradeStatisticsItem> TradeStatistic { get; } = new BindingList<TradeStatisticsItem>();
        public BindingList<CandleStickData> CandleStickData { get; set; } = new BindingList<CryptoMarketClient.CandleStickData>();
        public BindingList<CurrencyStatusHistoryItem> MarketCurrencyStatusHistory { get; set; } = new BindingList<CurrencyStatusHistoryItem>();

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
                if(value == LowestAsk)
                    return;
                if(LowestAsk != 0)
                    AskChange = value - LowestAsk;
                lowestAskString = null;
                lowestAsk = value;
            }
        }
        double highestBid;
        public double HighestBid {
            get { return highestBid; }
            set {
                if(value == HighestBid)
                    return;
                if(HighestBid != 0)
                    BidChange = value - HighestBid;
                highestBidString = null;
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
                    Change = (value - Last) / Last;
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
        public virtual string GetStringWithChangePercent(double value, double change) {
            if(change >= 0) 
                return string.Format("<b>{0:0.00000000}</b> <color=green><size=-2>{1:0.##}%</size></color>", value, change);
            return string.Format("<b>{0:0.00000000}</b> <color=red><size=-2>{1:0.##}%</size></color>", value, change);
        }
        public double BaseVolume { get; set; }
        public double Volume { get; set; }
        public double Hr24High { get; set; }
        public double Hr24Low { get; set; }
        public double Change { get; set; }
        public double Spread { get { return LowestAsk - HighestBid; } }
        public double BidChange { get; set; }
        public double AskChange { get; set; }
        public abstract double Fee { get; set; }

        public Ticker UsdTicker { get; set; }

        BalanceBase firstInfo, secondInfo;
        public BalanceBase BaseBalanceInfo {
            get {
                if(firstInfo == null)
                    firstInfo = Exchange.DefaultAccount.Balances.FirstOrDefault((b) => b.Currency == BaseCurrency);
                return firstInfo;
            }
        }
        public BalanceBase MarketBalanceInfo {
            get {
                if(secondInfo == null)
                    secondInfo = Exchange.DefaultAccount.Balances.FirstOrDefault((b) => b.Currency == MarketCurrency);
                return secondInfo;
            }
        }

        public List<BalanceBase> GetBaseBalances() {
            List<BalanceBase> list = new List<BalanceBase>();
            foreach(var account in Exchange.Accounts) {
                list.Add(account.Balances.FirstOrDefault(b => b.Currency == BaseCurrency));
            }
            return list;
        }

        public List<BalanceBase> GetMarketBalances() {
            List<BalanceBase> list = new List<BalanceBase>();
            foreach(var account in Exchange.Accounts) {
                list.Add(account.Balances.FirstOrDefault(b => b.Currency == MarketCurrency));
            }
            return list;
        }

        CurrencyInfoBase marketCurrencyInfo;
        protected CurrencyInfoBase MarketCurrencyInfo {
            get {
                if(marketCurrencyInfo == null)
                    marketCurrencyInfo = Exchange.Currencies.FirstOrDefault(c => c.Currency == MarketCurrency);
                return marketCurrencyInfo;
            }
        }

        CurrencyInfoBase baseCurrencyInfo;
        protected CurrencyInfoBase BaseCurrencyInfo {
            get {
                if(baseCurrencyInfo == null)
                    baseCurrencyInfo = Exchange.Currencies.FirstOrDefault(c => c.Currency == MarketCurrency);
                return baseCurrencyInfo;
            }
        }

        public double BaseCurrencyBalance { get { return BaseBalanceInfo == null ? 0 : BaseBalanceInfo.Available; } }
        public double MarketCurrencyBalance { get { return MarketBalanceInfo == null ? 0 : MarketBalanceInfo.Available; } }
        public double MarketCurrencyTotalBalance { get { return MarketBalanceInfo == null ? 0 : MarketBalanceInfo.OnOrders + MarketBalanceInfo.Available; } }
        public bool MarketCurrencyEnabled { get { return MarketCurrencyInfo == null ? false : !MarketCurrencyInfo.Disabled; } }

        public string BaseCurrency { get; set; }
        public string MarketCurrency { get; set; }
        public abstract string HostName { get; }
        public DateTime Time { get; set; }
        public int CandleStickPeriodMin { get; set; } = 1;
        public DateTime LastTradeStatisticTime { get; set; }
        public long LastTradeId { get; set; }
        public abstract string WebPageAddress { get; }
        public bool UpdateArbitrageOrderBook() {
            if(IsUpdatingOrderBook)
                return true;
            try {
                IsUpdatingOrderBook = true;
                return Exchange.UpdateArbitrageOrderBook(this, OrderBook.Depth);
            }
            finally {
                IsUpdatingOrderBook = false;
            }
        }
        public bool UpdateOrderBook() {
            if(IsUpdatingOrderBook)
                return true;
            try {
                IsUpdatingOrderBook = true;
                bool res = Exchange.UpdateOrderBook(this);
                if(res) {
                    OnApplyIncrementalUpdate();
                }
                return res;
            }
            finally {
                IsUpdatingOrderBook = false;
            }
        }
        public void OnApplyIncrementalUpdate() {
            if(OrderBook.Bids.Count == 0 || OrderBook.Asks.Count == 0)
                return;
            HighestBid = OrderBook.Bids[0].Value;
            LowestAsk = OrderBook.Asks[0].Value;
            Time = DateTime.UtcNow;
            UpdateHistoryItem();
            CandleStickChartHelper.AddCandleStickData(CandleStickData, History.Last(), CandleStickPeriodMin * 60);
        }
        public bool ProcessOrderBook(string text) { return Exchange.ProcessOrderBook(this, text); }
        protected bool IsUpdatingTicker { get; set; }
        public bool UpdateTicker() {
            if(IsUpdatingTicker)
                return true;
            IsUpdatingTicker = true;
            try {
                bool res = Exchange.UpdateTicker(this);
                if(res) {
                    Time = DateTime.UtcNow;
                    UpdateHistoryItem();
                    CandleStickChartHelper.AddCandleStickData(CandleStickData, History.Last(), CandleStickPeriodMin * 60);
                }
                return res;
            }
            finally {
                IsUpdatingTicker = false;
            }
        }
        protected bool IsUpdatingOrderBook { get; set; }
        protected bool IsUpdatingTrades { get; set; }
        public bool UpdateTrades() {
            if(IsUpdatingTrades)
                return true;
            try {
                IsUpdatingTrades = true;
                return Exchange.UpdateTrades(this);
            }
            finally {
                IsUpdatingTrades = false;
            }
        }
        protected bool IsUpdatingOpenedOrders { get; set; }
        protected internal byte[] OpenedOrdersData { get; set; }
        public bool UpdateOpenedOrders() {
            if(IsUpdatingOpenedOrders)
                return true;
            try {
                IsUpdatingOpenedOrders = true;
                return Exchange.UpdateOpenedOrders(Exchange.DefaultAccount, this);
            }
            finally {
                IsUpdatingOpenedOrders = false;
            }
        }
        public string DownloadString(string address) {
            try {
                ApiRate.WaitToProceed();
                return Exchange.GetWebClient().DownloadString(address);
            }
            catch { }
            return string.Empty;
        }
        public byte[] DownloadBytes(string address) {
            try {
                ApiRate.WaitToProceed();
                return Exchange.GetWebClient().DownloadData(address);
            }
            catch { }
            return null;
        }

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
        public event EventHandler OpenedOrdersChanged;
        public event EventHandler CandleStickChanged;
        public void RaiseOpenedOrdersChanged() {
            if(OpenedOrdersChanged != null)
                OpenedOrdersChanged(this, EventArgs.Empty);
        }
        public bool UpdateBalance(string symbol) {
            return UpdateBalance(Exchange.DefaultAccount, symbol);
        }
        public bool UpdateBalance(AccountInfo account, string symbol) {
            return Exchange.GetBalance(account, symbol);
        }
        public string GetDepositAddress(string symbol) {
            return GetDepositAddress(Exchange.DefaultAccount, symbol);
        }
        public string GetDepositAddress(AccountInfo account, string symbol) {
            return Exchange.CheckCreateDeposit(account, symbol);
        }
        public TradingResult MarketBuy(double amount) {
            return Buy(Hr24High, amount);
        }
        public TradingResult MarketSell(double amount) {
            return Sell(Hr24Low, amount);
        }
        public TradingResult Buy(double rate, double amount) {
            return Buy(Exchange.DefaultAccount, rate, amount);
        }
        public TradingResult Sell(double rate, double amount) {
            return Sell(Exchange.DefaultAccount, rate, amount);
        }
        public TradingResult Buy(AccountInfo account, double lowestAsk, double amount) {
            return Exchange.Buy(account, this, lowestAsk, amount);
        }
        public TradingResult Sell(AccountInfo account, double highestBid, double amount) {
            return Exchange.Sell(account, this, highestBid, amount);
        }
        public bool Withdraw(AccountInfo account, string currency, string address, string paymentId, double amount) {
            return Exchange.Withdraw(account, currency, address, paymentId, amount);
        }
        public bool Withdraw(string currencyName, string address, string paymentId, double amount) {
            return Withdraw(Exchange.DefaultAccount, currencyName, address, paymentId, amount);
        }

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
                    AskChange = (LowestAsk - last.Ask) * 100;
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
            return Exchange.UpdateAccountTrades(Exchange.DefaultAccount, this);
        }
        public void UpdateTrailings() {
            lock(this) {
                foreach(TrailingSettings tr in Trailings) {
                    tr.Update();
                }
            }
            RaiseChanged();
        }


        ObservableCollection<TickerEvent> events;
        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true)]
        public ObservableCollection<TickerEvent> Events {
            get {
                if(events == null) {
                    events = new ObservableCollection<TickerEvent>();
                    events.CollectionChanged += OnEventsChanged;
                }
                return events;
            }
        }

        protected TickerEvent XtraCreateEventsItem(XtraItemEventArgs e) {
            return new TickerEvent();
        }
        void XtraSetIndexEventsItem(XtraSetItemIndexEventArgs e) {
            if(e.NewIndex == -1) {
                Events.Add((TickerEvent)e.Item.Value);
                return;
            }
            Events.Insert(e.NewIndex, (TickerEvent)e.Item.Value);
        }
        public event NotifyCollectionChangedEventHandler EventsChanged {
            add { Events.CollectionChanged += value; }
            remove { Events.CollectionChanged -= value; }
        }

        private void OnEventsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            if(RestoringLayout)
                return;
            Save();
        }
        protected internal bool IsOpenedOrdersChanged(byte[] newBytes) {
            if(newBytes == null)
                return false;
            if(OpenedOrdersData == null || OpenedOrdersData.Length != newBytes.Length) {
                OpenedOrdersData = newBytes;
                return true;
            }
            for(int i = 0; i < newBytes.Length; i++) {
                if(OpenedOrdersData[i] != newBytes[i])
                    return true;
            }
            return false;
        }
        public bool Cancel(OpenedOrderInfo info) {
            return Exchange.Cancel(Exchange.DefaultAccount, info.OrderId);
        }
        public void StopListenTickerStream() {
            Exchange.StopListenTickerStream(this);
        }
        public void StartListenTickerStream() {
            Exchange.StartListenTickerStream(this);
        }
        public CandleStickData GetOrCreateCandleStickData(DateTime dt) {
            int end = Math.Max(0, CandleStickData.Count - 30);
            for(int i = CandleStickData.Count - 1; i >= end; i--) {
                CandleStickData data = CandleStickData[i];
                if(dt > data.Time)
                    break;
                if(dt == data.Time)
                    return data;
                if(dt < data.Time) {
                    CandleStickData d = new CandleStickData();
                    d.Time = dt;
                    CandleStickData.Insert(i, d);
                    return d;
                }
            }
            CandleStickData dd = new CandleStickData();
            dd.Time = dt;
            CandleStickData.Add(dd);
            return dd;
        }
        public override string ToString() {
            return CurrencyPair;
        }
        public void StartListenKlineStream() {
            Exchange.StartListenKline(this);
        }
        public void StopListenKlineStream() {
            Exchange.StopListenKline(this);
        }
        public void RaiseCandleStickChanged() {
            if(CandleStickChanged != null)
                CandleStickChanged(this, EventArgs.Empty);
        }

        public int CompareTo(object obj) {
            if(!(obj is Ticker))
                return 1;
            return CurrencyPair.CompareTo(((Ticker)obj).CurrencyPair);
        }
        public void StartListenOrderBook() {
            Exchange.StartListenOrderBook(this);
        }
        public void StopListenOrderBook() {
            Exchange.StopListenOrderBook(this);
        }
        public virtual bool IsListeningOrderBook { get; }

        IncrementalUpdateQueue updates;
        public IncrementalUpdateQueue Updates {
            get {
                if(updates == null)
                    updates = new IncrementalUpdateQueue(Exchange.CreateIncrementalUpdateDataProvider());
                return updates;
            }
        }
    }

    public enum TickerUpdateMode {
        Self,
        Arbitrage
    }

    public class TickerFilter {
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double TickSize { get; set; }

        public double ConstrainValue(double value) {
            if(value < MinValue) value = MinValue;
            if(value > MaxValue) value = MaxValue;
            value = ((int)(value / TickSize + 0.01)) * TickSize;
            return value;
        }
    }
}
