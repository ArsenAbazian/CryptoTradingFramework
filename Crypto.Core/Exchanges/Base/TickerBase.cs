using Crypto.Core.Exchanges.Base;
using Crypto.Core.Helpers;
using CryptoMarketClient.Common;
using CryptoMarketClient.Exchanges.Base;
using CryptoMarketClient.Strategies;
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
using System.Xml.Serialization;

namespace CryptoMarketClient {
    [Serializable]
    public abstract class Ticker : ISupportSerialization, IComparable {
        public Ticker(Exchange exchange) {
            Exchange = exchange;
            OrderBook = new OrderBook(this);
            UpdateMode = TickerUpdateMode.Self;
            IsActual = true;
        }

        public int Code { get; set; }

        public string FileName {
            get { return Exchange.TickersDirectory + "\\" + Name.ToLower() + ".xml"; }
            set { }
        }

        public bool Load() {
            return SerializationHelper.Load(this, GetType());
        }

        [XmlIgnore]
        public TickerCaptureData CaptureDataHistory { get; } = new TickerCaptureData();
        public void CaptureDataCore(CaptureStreamType stream, CaptureMessageType msgType, string message) {
            CaptureDataHistory.Items.Add(new TickerCaptureDataInfo() { StreamType = stream, MessageType = msgType, Message = message, Time = DateTime.UtcNow });
            if(CaptureDataHistory.Items.Count % CaptureDataHistory.SaveCount == 0) {
                SaveCaptureData();
            }
        }

        protected int SaveCount { get; set; }
        protected string GetCaptureFileName() {
            return CaptureDirectory + "\\" + Exchange.Type + "_" + Name.ToLower() + "_" + DateTime.Now.ToString("dd_MM_yyyy_") + SaveCount.ToString("D3") + ".xml";
        }
        private void SaveCaptureData() {
            CaptureDataHistory.Exchange = Exchange.Type;
            CaptureDataHistory.TickerName = Name;
            XmlSerializer ser = new XmlSerializer(typeof(TickerCaptureData));
            using(FileStream f = new FileStream(GetCaptureFileName(), FileMode.OpenOrCreate)) {
                ser.Serialize(f, CaptureDataHistory);
            }
            SaveCount++;
            CaptureDataHistory.Items.Clear();
        }

        public void ApplyCapturedEvent(DateTime time) {
            if(CaptureDataHistory.Items.Count == 0)
                return;
            while(CaptureDataHistory.CurrentItem != null) {
                TickerCaptureDataInfo info = CaptureDataHistory.CurrentItem;
                if(info.Time != time)
                    return;
                Exchange.ApplyCapturedEvent(this, info);
                CaptureDataHistory.MoveNext();
            }
            //while(CaptureDataHistory.Items.Count > 0) {
            //    TickerCaptureDataInfo info = CaptureDataHistory.Items[0];
            //    if(info.Time != time)
            //        return;
            //    Exchange.ApplyCapturedEvent(this, info);
            //    CaptureDataHistory.Items.RemoveAt(0);
            //}
        }

        public bool IsOrderBookSubscribed { get; set; }
        public bool IsTradeHistorySubscribed { get; set; }
        public bool IsKlineSubscribed { get; set; }

        public CandleStickIntervalInfo GetCandleStickIntervalInfo() {
            TimeSpan interval = TimeSpan.FromMinutes(CandleStickPeriodMin);
            return Exchange.AllowedCandleStickIntervals.FirstOrDefault(i => i.Interval == interval);
        }

        public static Ticker FromFile(string fileName, Type tickerType) {
            return (Ticker)SerializationHelper.FromFile(fileName, tickerType);
        }
        public bool Save() {
            return SerializationHelper.Save(this, GetType(), null);
        }

        public bool SelectedInDependencyArbitrage { get; set; }

        public TickerFilter PriceFilter { get; set; }
        public TickerFilter QuantityFilter { get; set; }
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

        public List<TrailingSettings> Trailings { get; } = new List<TrailingSettings>();

        public List<TradingResult> Trades { get; } = new List<TradingResult>();
        public List<TradeInfoItem> MyTradeHistory { get; } = new List<TradeInfoItem>();
        public List<OpenedOrderInfo> OpenedOrders { get; } = new List<OpenedOrderInfo>();
        public BindingList<TickerHistoryItem> History { get; } = new BindingList<TickerHistoryItem>();
        public List<TradeInfoItem> TradeHistory { get; } = new List<TradeInfoItem>();
        public BindingList<TradeStatisticsItem> TradeStatistic { get; } = new BindingList<TradeStatisticsItem>();

        BindingList<CandleStickData> candleStickData;
        protected void OnCandleStickDataItemsChanged(object sender, ListChangedEventArgs e) {
            RaiseCandleStickChanged(e);
        }
        public BindingList<CandleStickData> CandleStickData {
            get {
                if(candleStickData == null) {
                    candleStickData = new BindingList<CryptoMarketClient.CandleStickData>();
                    candleStickData.ListChanged += OnCandleStickDataItemsChanged;
                }
                return candleStickData;
            }
            set {
                if(CandleStickData != null)
                    CandleStickData.ListChanged -= OnCandleStickDataItemsChanged;
                candleStickData = value;
                if(CandleStickData != null)
                    CandleStickData.ListChanged += OnCandleStickDataItemsChanged;
                RaiseCandleStickChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
        }
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

        public double HighestBidInBaseCurrency() {
            if(ContractTicker)
                return 1.0;
            return OrderBook.Bids[0].Value;
        }

        public double LowestAskInBaseCurrency() {
            if(ContractTicker)
                return 1.0;
            return OrderBook.Asks[0].Value;
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
        public bool UpdateArbitrageOrderBook(int depth) {
            if(IsUpdatingOrderBook)
                return true;
            try {
                IsUpdatingOrderBook = true;
                return Exchange.UpdateArbitrageOrderBook(this, depth);
            }
            finally {
                IsUpdatingOrderBook = false;
            }
        }
        //public bool UpdateOrderBook(int depth) {
        //    if(IsUpdatingOrderBook)
        //        return true;
        //    try {
        //        IsUpdatingOrderBook = true;
        //        bool res = Exchange.UpdateArbitrageOrderBook(this, depth);
        //        if(res) {
        //            OnApplyIncrementalUpdate();
        //        }
        //        return res;
        //    }
        //    finally {
        //        IsUpdatingOrderBook = false;
        //    }
        //}
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

        protected internal void RaiseHistoryChanged() {
            if(HistoryChanged != null)
                HistoryChanged(this, EventArgs.Empty);
        }
        protected internal void RaiseChanged() {
            if(Changed != null)
                Changed(this, EventArgs.Empty);
        }

        public bool HasTradeHistorySubscribers { get { return TradeHistoryChanged != null; } }

        protected internal void RaiseTradeHistoryChanged(TradeHistoryChangedEventArgs e) {
            e.Ticker = this;
            if(TradeHistoryChanged != null)
                TradeHistoryChanged(this, e);
        }

        public event EventHandler HistoryChanged;
        public event TradeHistoryChangedEventHandler TradeHistoryChanged;
        public event EventHandler Changed;
        public event EventHandler OpenedOrdersChanged;
        public event ListChangedEventHandler CandleStickChanged;
        public event OrderBookEventHandler OrderBookChanged {
            add { OrderBook.Changed += value; }
            remove { OrderBook.Changed -= value; }
        }
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

        public bool IsUpdating { get; set; }
        public long UpdateTimeMs { get; set; }
        public long StartUpdateTime { get; set; }
        public long LastUpdateTime { get; set; }
        public Task Task { get; set; }
        public bool IsActual { get; set; }
        public bool PrevMarketCurrencyEnabled { get; set; }

        public void UpdateHistoryItem() {
            TickerHistoryItem last = History.Count == 0 ? null : History.Last();
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
            RaiseHistoryChanged();
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
                for(int i = 0; i < Trailings.Count; i++) {
                    TrailingSettings tr = Trailings[i];
                    tr.Update();
                }
            }
            RaiseChanged();
        }


        ObservableCollection<TickerEvent> events;
        public ObservableCollection<TickerEvent> Events {
            get {
                if(events == null) {
                    events = new ObservableCollection<TickerEvent>();
                    events.CollectionChanged += OnEventsChanged;
                }
                return events;
            }
        }
        
        public event NotifyCollectionChangedEventHandler EventsChanged {
            add { Events.CollectionChanged += value; }
            remove { Events.CollectionChanged -= value; }
        }

        private void OnEventsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
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
        public CandleStickData UpdateCandleStickData(CandleStickData newData) {
            DateTime dt = newData.Time;
            int end = Math.Max(0, CandleStickData.Count - 30);
            for(int i = CandleStickData.Count - 1; i >= end; i--) {
                CandleStickData data = CandleStickData[i];
                if(dt > data.Time)
                    break;
                if(dt == data.Time) {
                    CandleStickData[i] = newData;
                    return newData;
                }
                if(dt < data.Time) {
                    CandleStickData.Insert(i, newData);
                    return newData;
                }
            }
            CandleStickData.Add(newData);
            return newData;
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
        public void RaiseCandleStickChanged(ListChangedEventArgs e) {
            if(CandleStickChanged != null)
                CandleStickChanged(this, e);
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
        public void StartListenTradingHistory() {
            Exchange.StartListenTradeHistory(this);
        }
        public void StopListenTradingHistory() {
            Exchange.StopListenTradeHistory(this);
        }

        public virtual void OnEndDeserialize() {

        }

        public virtual bool IsListeningOrderBook { get; }
        public virtual bool IsListeningTradingHistory { get; }
        public virtual bool IsListeningKline { get; }

        IncrementalUpdateQueue updates;
        public IncrementalUpdateQueue Updates {
            get {
                if(updates == null)
                    updates = new IncrementalUpdateQueue(Exchange.CreateIncrementalUpdateDataProvider());
                return updates;
            }
        }

        public string CaptureDirectory { get; set; }
        public bool CaptureData { get; set; }
        public virtual bool ContractTicker { get; set; }
        public virtual double ContractValue { get; set; }
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

    public class TradeHistoryChangedEventArgs : EventArgs {
        
        public Ticker Ticker { get; set; }
        public TradeInfoItem NewItem { get; set; }
        public List<TradeInfoItem> NewItems { get; set; } = new List<TradeInfoItem>();
    }

    public delegate void TradeHistoryChangedEventHandler(object sender, TradeHistoryChangedEventArgs e);
}
