using Crypto.Core.Exchanges.Base;
using Crypto.Core.Helpers;
using Crypto.Core.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XmlSerialization;

namespace Crypto.Core {
    [Serializable]
    public abstract class Ticker : ISupportSerialization, IComparable, ICachedDataOwner {
        public static bool UseHtmlString { get; set; } = true;
        public Ticker() : this(null) {
            
        }
        public Ticker(Exchange exchange) {
            Exchange = exchange;
            OrderBook = new OrderBook(this);
            UpdateMode = TickerUpdateMode.Self;
            IsActual = true;
            CandleStickPeriodMin = 30; //default
        }

        public int Code { get; set; }

        string fileName = null;
        public string FileName {
            get {
                if(fileName == null && Exchange != null) {
                    string file = Name.ToLower();
                    if(file.Contains(':'))
                        file = file.Replace(':', '_');
                    fileName = Exchange.TickersDirectory + "\\" + file + ".xml";
                }
                return fileName;
            }
            set { }
        }

        protected bool IsLoading { get; set; }
        public bool Load() {
            IsLoading = true;
            try {
                return SerializationHelper.Current.Load(this, GetType(), FileName);
            }
            finally {
                IsLoading = false;
            }
        }

        public TickerDataStatus OrderBookStatus { get { return OrderBook.IsDirty ? TickerDataStatus.Invalid : TickerDataStatus.Actual; } }

        [XmlIgnore]
        public TickerCaptureData CaptureDataHistory { get; } = new TickerCaptureData();
        public void CaptureDataCore(CaptureStreamType stream, CaptureMessageType msgType, string message) {
            bool dataValid = stream == CaptureStreamType.OrderBook ? !OrderBook.IsDirty : true;
            CaptureDataHistory.Items.Add(new TickerCaptureDataInfo() { StreamType = stream, MessageType = msgType, Message = message, Time = DateTime.UtcNow, DataValid = dataValid });
            if(CaptureDataHistory.Items.Count % CaptureDataHistory.SaveCount == 0) {
                SaveCaptureData();
            }
        }

        public double GetCurrent(DateTime time) {
            if(CandleStickData == null || CandleStickData.Count == 0)
                return Last;
            if(CandleStickData.Last().Time < time)
                return Last;
            for(int i = CandleStickData.Count - 1; i >= 0; i--) {
                if(CandleStickData[i].Time < time)
                    return CandleStickData[i + 1].High;
            }
            return Last;
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
                if(info.StreamType == CaptureStreamType.OrderBook)
                    OrderBook.IsDirty = !info.DataValid;
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
        public bool IsTickerInfoSubscribed { get; set; }

        public CandleStickIntervalInfo GetCandleStickIntervalInfo() {
            TimeSpan interval = TimeSpan.FromMinutes(CandleStickPeriodMin);
            return Exchange.AllowedCandleStickIntervals.FirstOrDefault(i => i.Interval == interval);
        }

        public static Ticker FromFile(string fileName, Type tickerType) {
            return (Ticker)SerializationHelper.Current.FromFile(fileName, tickerType);
        }
        public bool Save() {
            if(IsLoading)
                return false;
            return SerializationHelper.Current.Save(this, GetType(), null);
        }

        public bool SelectedInDependencyArbitrage { get; set; }

        public TickerFilter PriceFilter { get; set; }
        public TickerFilter QuantityFilter { get; set; } = new TickerFilter() { TickSize = 0 };
        public TickerFilter NotionalFilter { get; set; } = new TickerFilter() { MinValue = 0 };
        public virtual string ValidateTrade(double rate, double amount) {
            if(NotionalFilter.MinValue != 0 && NotionalFilter.MinValue > rate * amount)
                return "NotionalFilter not passed. MinValue = " + NotionalFilter.MinValue + " greater than " + (rate * amount).ToString("0.########") + " (total spent)";
            return string.Empty;
        }
        public virtual void CorrectTrade(ref double rate, ref double amount) {
            if(NotionalFilter.MinValue != 0) {
                if(rate * amount < NotionalFilter.MinValue)
                    amount = NotionalFilter.MinValue / rate;
            }
            if(QuantityFilter.MinValue != 0 && QuantityFilter.MinValue > amount) {
                amount = QuantityFilter.MinValue;
            }
            if(QuantityFilter.TickSize != 0) {
                int value = (int)(amount / QuantityFilter.TickSize);
                amount = value * QuantityFilter.TickSize;
            }
        }
        [XmlIgnore]
        public Exchange Exchange { get; private set; }
        public int Index { get; set; }
        public virtual string MarketName { get; set; }
        public abstract string CurrencyPair { get; set; }
        public virtual string SubscriptionName { get { return CurrencyPair; } }
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

        public List<TradingSettings> Trailings { get; } = new List<TradingSettings>();

        [XmlIgnore]
        public List<TradingResult> Trades { get; } = new List<TradingResult>();
        [XmlIgnore]
        public LinkedList<TradeInfoItem> AccountTradeHistory { get; } = new LinkedList<TradeInfoItem>();
        public ResizeableArray<TradeInfoItem> GetAccountTradeHistory() {
            ResizeableArray<TradeInfoItem> res = new ResizeableArray<TradeInfoItem>(AccountTradeHistory.Count);
            foreach(var item in AccountTradeHistory)
                res.Add(item);
            return res; 
        }

        public string GetCandleStickCommandName() {
            return Exchange.GetCandleStickCommandName(CandleStickPeriodMin);
        }

        CycleArray<TradeInfoItem> accountShortTradeHistory;
        [XmlIgnore]
        public CycleArray<TradeInfoItem> AccountShortTradeHistory { 
            get {
                if(accountShortTradeHistory == null)
                    accountShortTradeHistory = new CycleArray<TradeInfoItem>(2000);
                return accountShortTradeHistory;
            }    
        }
        protected internal OpenedOrderInfo[] PrevOpenedOrders { get; set; }
        [XmlIgnore]
        public List<OpenedOrderInfo> OpenedOrders {
            get { return Exchange.DefaultAccount?.OpenedOrders; }
        }
        [XmlIgnore]
        public LinkedList<TickerHistoryItem> History { get; } = new LinkedList<TickerHistoryItem>();

        public event CancelOrderHandler OrderCanceled;
        protected internal void RaiseOrderCanceled(CancelOrderEventArgs e) {
            if(OrderCanceled != null)
                OrderCanceled(this, e);
            if(Exchange != null)
                Exchange.RaiseOrderCanceled(e);
        }

        [XmlIgnore]
        public LinkedList<TradeInfoItem> TradeHistory { get; } = new LinkedList<TradeInfoItem>();
        public ResizeableArray<TradeInfoItem> GetTradeHistory() { 
            ResizeableArray<TradeInfoItem> res = new ResizeableArray<TradeInfoItem>(TradeHistory.Count);
            foreach(var item in TradeHistory)
                res.Add(item);
            return res; 
        } 

        CycleArray<TradeInfoItem> shortTradeHistory;
        [XmlIgnore]
        public CycleArray<TradeInfoItem> ShortTradeHistory { 
            get { 
                if(shortTradeHistory == null)
                    shortTradeHistory = new CycleArray<TradeInfoItem>(6000);
                return shortTradeHistory;
            }    
        }

        [XmlIgnore]
        public BindingList<TradeStatisticsItem> TradeStatistic { get; } = new BindingList<TradeStatisticsItem>();

        ResizeableArray<CandleStickData> candleStickData;
        protected void OnCandleStickDataItemsChanged(object sender, ListChangedEventArgs e) {
            RaiseCandleStickChanged(e);
        }
        [XmlIgnore]
        public ResizeableArray<CandleStickData> CandleStickData {
            get {
                if(candleStickData == null) {
                    candleStickData = new ResizeableArray<Crypto.Core.CandleStickData>();
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

        [XmlIgnore]
        public OrderBook OrderBook { get; private set; }
        public abstract string Name { get; }

        public void ClearMyTradeHistory() {
            if(AccountTradeHistory != null)
                AccountTradeHistory.Clear();
            if(AccountShortTradeHistory != null)
                AccountShortTradeHistory.Clear();
        }

        public void ClearTradeHistory() {
            if(TradeHistory != null)
                TradeHistory.Clear();
            if(ShortTradeHistory != null)
                ShortTradeHistory.Clear();
            if(TradeStatistic != null)
                TradeStatistic.Clear();
        }

        string lowestAskString = null;
        public string LowestAskString {
            get {
                if(lowestAskString == null)
                    lowestAskString = GetString(LowestAsk);
                return lowestAskString;
            }
            set {
                if(LowestAskString == value)
                    return;
                lowestAskString = value;
                lowestAsk = 0;
            }
        }
        string highestBidString = null;
        public string HighestBidString {
            get {
                if(highestBidString == null)
                    highestBidString = GetString(HighestBid);
                return highestBidString;
            }
            set {
                if(HighestBidString == value)
                    return;
                highestBidString = value;
                highestBid = 0;
            }
        }
        double lowestAsk;
        public double LowestAsk {
            get { 
                if(lowestAsk == 0) {
                    if(lowestAskString != null)
                        lowestAsk = FastValueConverter.Convert(lowestAskString);
                    return lowestAsk;
                }
                return lowestAsk; 
            }
            set {
                if(value == LowestAsk)
                    return;
                if(LowestAsk != 0)
                    AskChange = value - LowestAsk;
                lowestAskString = null;
                lowestAsk = value;
            }
        }
        double highestBid = 0;
        public double HighestBid {
            get { 
                if(highestBid == 0) {
                    if(highestBidString != null)
                        highestBid = FastValueConverter.Convert(highestBidString);
                }
                return highestBid; 
            }
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
            get { 
                if(last == 0) {
                    if(lastString != null)
                        last = FastValueConverter.Convert(lastString);
                }
                return last; 
            }
            set {
                if(value == Last)
                    return;
                //if(Last != 0)
                //    Change = (value - Last) / Last;
                lastString = null;
                last = value;
            }
        }

        public double Total(double value, double amount) {
            if(ContractTicker)
                return ContractValue * amount;
            return value * amount;
        }

        protected internal virtual void UpdateKline() {
            if(!IsKlineSubscribed)
                return;
            //CandleStickData last = CandleStickData.Last();
            //CandleStickPeriodMin
        }

        string lastString = null;
        public string LastString {
            get {
                if(lastString == null)
                    lastString = GetString(Last);
                return lastString;
            }
            set {
                if(LastString == value)
                    return;
                lastString = value;
                last = 0;
            }
        }
        public virtual string GetStringWithChangePercent(double value, double change) {
            if(value == 0)
                return "NO DATA YET";
            if(!UseHtmlString)
                return string.Format("{0:0.00000000}", value);
            if(change == 0)
                return string.Format("<b>{0:0.00000000}</b>", value);
            if (change >= 0) 
                return string.Format("<b>{0:0.00000000}</b> <color=green><size=-2>{1:0.##}%</size></color>", value, change);
            return string.Format("<b>{0:0.00000000}</b> <color=red><size=-2>{1:0.##}%</size></color>", value, change);
        }

        public double SpentInBaseCurrency(double rate, double amount) {
            if(ContractTicker)
                return amount * ContractValue;
            return rate * amount;
        }

        public virtual string GetString(double value) {
            return GetStringWithChangePercent(value, 0);
        }

        public double BaseVolumeInUSD {
            get {
                if(UsdTicker != null)
                    return UsdTicker.HighestBid * BaseVolume;
                return 0;
            }
        }

        double baseVolume;
        public double BaseVolume {
            get {
                if(baseVolume == 0 && baseVolumeString != null)
                    baseVolume = FastValueConverter.Convert(baseVolumeString);
                return baseVolume;
            }
            set {
                if(baseVolume == value)
                    return;
                baseVolume = value;
                baseVolumeString = null;
            }
        }

        string baseVolumeString;
        public string BaseVolumeString {
            get { 
                if(baseVolumeString == null && baseVolume != 0)
                    baseVolumeString = GetString(baseVolume);
                return baseVolumeString;
            }
            set {
                if(BaseVolumeString == value)
                    return;
                baseVolumeString = value;
                baseVolume = 0;
            }
        }

        double volume;
        public double Volume {
            get {
                if(volume == 0 && volumeString != null)
                    volume = FastValueConverter.Convert(volumeString);
                return volume;
            }
            set {
                if(volume == value)
                    return;
                volume = value;
                volumeString = null;
            }
        }

        string volumeString;
        public string VolumeString {
            get {
                if(volumeString == null && volume != 0)
                    volumeString = GetString(volume);
                return volumeString;
            }
            set {
                if(VolumeString == value)
                    return;
                volumeString = value;
                volume = 0;
            }
        }

        double hr24High;
        public double Hr24High {
            get {
                if(hr24High == 0 && hr24HighString != null)
                    hr24High = FastValueConverter.Convert(hr24HighString);
                return hr24High;
            }
            set {
                if(hr24High == value)
                    return;
                hr24High = value;
                hr24HighString = null;
            }
        }

        string hr24HighString;
        public string Hr24HighString {
            get {
                if(hr24HighString == null && hr24High != 0)
                    hr24HighString = GetString(hr24High);
                return hr24HighString;
            }
            set {
                if(Hr24HighString == value)
                    return;
                hr24HighString = value;
                hr24High = 0;
            }
        }

        double hr24Low;
        public double Hr24Low {
            get {
                if(hr24Low == 0 && hr24LowString != null)
                    hr24Low = FastValueConverter.Convert(hr24LowString);
                return hr24Low;
            }
            set {
                if(hr24Low == value)
                    return;
                hr24Low = value;
                hr24LowString = null;
            }
        }

        string hr24LowString;
        public string Hr24LowString {
            get {
                if(hr24LowString == null && hr24Low != 0)
                    hr24LowString = GetString(hr24Low);
                return hr24LowString;
            }
            set {
                if(Hr24LowString == value)
                    return;
                hr24LowString = value;
                hr24Low = 0;
            }
        }



        public double Change { get; set; }
        public double Spread { get { return LowestAsk - HighestBid; } }
        public double BidChange { get; set; }
        public double AskChange { get; set; }
        public abstract double Fee { get; set; }

        Ticker usdTicker;
        public Ticker UsdTicker {
            get {
                if(usdTicker == null)
                    usdTicker = FindUsdTicker();
                return usdTicker;
            }
            set { usdTicker = value; }
        }

        protected virtual Ticker FindUsdTicker() {
            return null;
        }

        public double ConvertTo(double value, string[] currencyNames) {
            if(currencyNames.Contains(MarketCurrency))
                return value;
            foreach(string name in currencyNames) {
                Ticker t = Exchange.Ticker(name, BaseCurrency);
                if(t != null) return t.LowestAsk * value;
            }
            return value;
        }

        BalanceBase firstInfo, secondInfo;
        [XmlIgnore]
        public BalanceBase BaseBalanceInfo {
            get {
                if(Exchange == null || Exchange.DefaultAccount == null)
                    return null;
                if(firstInfo == null)
                    Exchange.DefaultAccount.BalancesHash.TryGetValue(BaseCurrency, out firstInfo);
                return firstInfo;
            }
        }
        [XmlIgnore]
        public BalanceBase MarketBalanceInfo {
            get {
                if(Exchange == null || Exchange.DefaultAccount == null)
                    return null;
                if(secondInfo == null)
                    Exchange.DefaultAccount.BalancesHash.TryGetValue(MarketCurrency, out secondInfo);
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

        CurrencyInfo marketCurrencyInfo;
        protected CurrencyInfo MarketCurrencyInfo {
            get {
                if(marketCurrencyInfo == null && Exchange != null)
                    marketCurrencyInfo = Exchange.GetOrCreateCurrency(MarketCurrency);
                return marketCurrencyInfo;
            }
        }

        CurrencyInfo baseCurrencyInfo;
        protected CurrencyInfo BaseCurrencyInfo {
            get {
                if(baseCurrencyInfo == null && Exchange != null)
                    baseCurrencyInfo = Exchange.GetOrCreateCurrency(BaseCurrency);
                return baseCurrencyInfo;
            }
        }

        public CandleStickData GetCandleStickItem(DateTime dt) {
            if(CandleStickData.Count == 0)
                return null;
            if(dt < CandleStickData.First().Time)
                return null;
            if(dt > CandleStickData.Last().Time)
                return null;
            DateTime startTime = CandleStickData.First().Time;
            DateTime time = dt;

            int index = (int)((time - startTime).TotalMinutes / CandleStickPeriodMin);
            if(index >= CandleStickData.Count)
                return null;
            return CandleStickData[index];
        }
        
        public CandleStickData GetCandleStickItem(TickerEvent ev) {
            if(CandleStickData == null)
                return null;
            return GetCandleStickItem(ev.Time);
        }

        public double BaseCurrencyBalance { get { return BaseBalanceInfo == null ? 0 : BaseBalanceInfo.Available; } }
        public double MarketCurrencyBalance { get { return MarketBalanceInfo == null ? 0 : MarketBalanceInfo.Available; } }
        public double MarketCurrencyTotalBalance { get { return MarketBalanceInfo == null ? 0 : MarketBalanceInfo.OnOrders + MarketBalanceInfo.Available; } }
        public bool MarketCurrencyEnabled { get { return MarketCurrencyInfo == null ? false : !MarketCurrencyInfo.Disabled; } }

        public virtual string BaseCurrency { get; set; }
        public virtual string MarketCurrency { get; set; }
        
        public virtual string BaseCurrencyDisplayName { get { return BaseCurrency; } set { } }
        public virtual string MarketCurrencyDisplayName { get { return MarketCurrency; } set { } }

        public virtual string HostName { get { return Exchange?.Type.ToString(); } }
        public DateTime Time { get; set; }
        public int CandleStickPeriodMin { get; set; } = 1;
        public DateTime LastTradeStatisticTime { get; set; }
        public long LastTradeId { get; set; }
        public abstract string WebPageAddress { get; }
        public bool UpdateOrderBook(int depth) {
            if(IsUpdatingOrderBook)
                return true;
            try {
                IsUpdatingOrderBook = true;
                return Exchange.UpdateOrderBook(this, depth);
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
                if(res)
                    OnApplyIncrementalUpdate();
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

        protected bool IsUpdatingTicker { get; set; }
        public async Task<bool> UpdateAsync() {
            if(IsUpdatingTicker)
                return false;

            IsUpdatingTicker = true;
            Task<bool> t = Task.Run(() => {
                bool res = Exchange.UpdateTicker(this);
                if(res) {
                    Time = DateTime.UtcNow;
                    UpdateHistoryItem();
                }
                IsUpdatingTicker = false;
                return res;
            });
            return await t;
        }
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
        public void LockAccountTrades() {
            MyTradesUpdateCount++;
        }
        public void UnlockAccountTrades() {
            if(MyTradesUpdateCount == 0)
                return;
            MyTradesUpdateCount--;
        }

        public void LockTrades() {
            TradesUpdateCount++;
            ShortTradeHistory.BeginUpdate();
        }
        public void UnlockTrades() {
            if(TradesUpdateCount == 0)
                return;
            TradesUpdateCount--;
            ShortTradeHistory.EndUpdate();
        }
        public void LockOpenOrders() {
            OpenOrdersUpdateCount++;
        }
        public void UnlockOpenOrders() {
            if(OpenOrdersUpdateCount == 0)
                return;
            OpenOrdersUpdateCount--;
        }
        protected int TradesUpdateCount { get; set; }
        protected int MyTradesUpdateCount { get; set; }
        protected int OpenOrdersUpdateCount { get; set; }
        public bool IsUpdatingTrades { get { return TradesUpdateCount > 0; } }
        public bool IsUpdatingAccountTrades { get { return MyTradesUpdateCount > 0; } }
        public bool UpdateTrades() {
            if(IsUpdatingTrades)
                return true;
            try {
                LockTrades();
                return Exchange.UpdateTrades(this);
            }
            finally {
                UnlockTrades();
            }
        }
        public bool IsUpdatingOpenedOrders => OpenOrdersUpdateCount > 0;
        protected internal byte[] OpenedOrdersData { get; set; }
        public bool UpdateOpenedOrders() {
            if(IsUpdatingOpenedOrders)
                return true;
            try {
                LockOpenOrders();
                if(Exchange.DefaultAccount == null)
                    return false;
                return Exchange.UpdateOpenedOrders(Exchange.DefaultAccount, this);
            }
            finally {
                UnlockOpenOrders();
            }
        }

        public void UpdateLastCandleStick() {
            if(CandleStickData.Count == 0)
                return;
            CandleStickData last = CandleStickData.Last();
            DateTime newKlineData = last.Time.AddMinutes(CandleStickPeriodMin);
            DateTime timeToUpdate = last.Time;
            if(DateTime.UtcNow >= newKlineData)
                timeToUpdate = newKlineData;
            Task t = Task.Run(() => {
                ResizeableArray<CandleStickData> res = GetCandleStickData(CandleStickPeriodMin, timeToUpdate, 1000000);
                if(res == null)
                    return;
                if(res[0].Time.Year == 1970)
                    return;
                for(int i = 0; i < res.Count; i++) {
                    UpdateCandleStickData(res[i]);
                }
            });
        }

        public string DownloadString(string address) {
            try {
                //ApiRate.WaitToProceed();
                return Exchange.GetDownloadString(this, address);
            }
            catch { }
            return string.Empty;
        }
        public byte[] DownloadBytes(string address) {
            try {
                //ApiRate.WaitToProceed();
                return Exchange.GetDownloadBytes(address);
            }
            catch { }
            return null;
        }

        public byte[] DownloadBytes(string address, MyWebClient client) {
            try {
                //ApiRate.WaitToProceed();
                return Exchange.GetDownloadBytes(address, client);
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

        //RateLimiting.RateGate apiRate = new RateLimiting.RateGate(6, TimeSpan.FromSeconds(1));
        //protected RateLimiting.RateGate ApiRate {
        //    get { return apiRate; }
        //}

        protected internal void RaiseHistoryChanged() {
            if(HistoryChanged != null)
                HistoryChanged(this, EventArgs.Empty);
        }

        protected internal virtual void AddMyTradeHistoryItem(IEnumerable<TradeInfoItem> items) {
            foreach(TradeInfoItem item in items)
                AddAccountTradeHistoryItem(item);
        }

        public void AddAccountTradeHistoryItem(TradeInfoItem item) {
            AccountTradeHistory.AddLast(item);
            AccountShortTradeHistory.Add(item);
        }

        protected internal void RaiseChanged() {
            if(Changed != null)
                Changed(this, EventArgs.Empty);
        }

        public bool HasTradeHistorySubscribers { get { return TradeHistoryChanged != null; } }
        public bool HasAccountTradeHistorySubscribers { get { return AccountTradeHistoryChanged != null; } }

        protected internal void RaiseTradeHistoryChanged(TradeHistoryChangedEventArgs e) {
            e.Ticker = this;
            if(TradeHistoryChanged != null)
                TradeHistoryChanged(this, e);
        }

        protected internal void RaiseAccountTradeHistoryChanged(TradeHistoryChangedEventArgs e) {
            e.Ticker = this;
            if(AccountTradeHistoryChanged != null)
                AccountTradeHistoryChanged(this, e);
        }

        public event EventHandler HistoryChanged;
        public event TradeHistoryChangedEventHandler TradeHistoryChanged;
        public event TradeHistoryChangedEventHandler AccountTradeHistoryChanged;
        public event EventHandler Changed;
        public event EventHandler OpenedOrdersChanged;
        public event ListChangedEventHandler CandleStickChanged;
        public event OrderBookEventHandler OrderBookChanged {
            add { OrderBook.Changed += value; }
            remove { OrderBook.Changed -= value; }
        }
        protected internal void RaiseOpenedOrdersChanged() {
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
            return Exchange.BuyLong(account, this, lowestAsk, amount);
        }
        public TradingResult Sell(AccountInfo account, double highestBid, double amount) {
            return Exchange.SellLong(account, this, highestBid, amount);
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
            if(!Exchange.AllowTickerHistory)
                return;
            TickerHistoryItem last = History.Count == 0 ? null : History.Last();
            if(last != null) {
                if(last.Ask == LowestAsk && last.Bid == HighestBid && last.Current == Last)
                    return;
                //Change = ((Last - last.Current) / last.Current) * 100;
                if(last.Bid != HighestBid)
                    BidChange = (HighestBid - last.Bid) * 100;
                if(last.Ask != LowestAsk)
                    AskChange = (LowestAsk - last.Ask) * 100;
            }
            History.AddLast(new TickerHistoryItem() { Time = Time, Ask = LowestAsk, Bid = HighestBid, Current = Last });
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
        public ResizeableArray<CandleStickData> GetCandleStickData(int candleStickPeriodMin, DateTime start, int rangeInSeconds) {
            return Exchange.GetCandleStickData(this, candleStickPeriodMin, start, rangeInSeconds);
        }
        public ResizeableArray<CandleStickData> GetRecentCandleStickData(int candleStickPeriodMin) {
            return Exchange.GetRecentCandleStickData(this, candleStickPeriodMin);
        }
        public virtual bool UpdateAccountTrades() {
            if(Exchange.DefaultAccount == null)
                return false;
            return Exchange.UpdateAccountTrades(Exchange.DefaultAccount, this);
        }
        public void UpdateTrailings() {
            lock(this) {
                for(int i = 0; i < Trailings.Count; i++) {
                    TradingSettings tr = Trailings[i];
                    tr.Update();
                }
            }
            RaiseChanged();
        }

        void ICachedDataOwner.OnDataUpdated() {
            RaiseChanged();
            Exchange?.RaiseTickerChanged(this);
        }

        protected virtual int DefaultCandlestickInterval => 60;
        public double[] GetSparkline() {
            ResizeableArray<CandleStickData> dt = GetRecentCandleStickData(DefaultCandlestickInterval);
            if(dt == null)
                return this.sparklineNullValue;
            double[] data = new double[dt.Count];
            for(int i = 0; i < dt.Count; i++)
                data[i] = (dt[i].Open + dt[i].Close + dt[i].High + dt[i].Low) / 4;
            return data;
        }

        double[] sparklineNullValue = new double[0];
        double[] sparkline;
        [XmlIgnore]
        public double[] Sparkline { 
            get { return sparkline; }
            set {
                sparkline = value;
                RaiseChanged();
            }
        }

        public void CancelSparkline() {
            DataCacheManager.RemoveTask(this, nameof(Sparkline));
        }

        public bool HasSparkline => this.sparkline != null && this.sparkline.Length > 0;
        public bool QuerySparkline() {
            if(HasSparkline)
                return false;
            this.sparkline = (double[])DataCacheManager.GetData(this, nameof(Sparkline), TimeSpan.FromHours(1), this.sparklineNullValue, () => {
                return GetSparkline();
            });
            if(HasSparkline) {
                RaiseChanged();
                return true;
            }
            return false;
        }

        public void CancelTickerInfo() {
            DataCacheManager.RemoveTask(this, "TickerInfo");
        }

        public void QueryTickerInfo() {
            DataCacheManager.GetData(this, "TickerInfo", TimeSpan.FromHours(1), false, () => {
                return UpdateTicker();
            });
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
        
        protected internal virtual void InsertTradeHistoryItem(TradeInfoItem item) {
            //if(TradeHistory.Count > 0) {
            //    TradeInfoItem first = TradeHistory.First();
            //    if(first.Time > item.Time)
            //        throw new Exception("Invalid Trade History Items Order By Time");
            //    if(first.Id != 0 && first.Id != item.Id - 1)
            //        throw new Exception("Invalid Trade History Items Order By Id");
            //}
            TradeHistory.AddFirst(item);
            ShortTradeHistory.AddFirst(item);
        }

        protected internal virtual void AddTradeHistoryItem(IEnumerable<TradeInfoItem> items) {
            foreach(TradeInfoItem item in items)
                AddTradeHistoryItem(item);
        }

        protected internal virtual void AddTradeHistoryItem(TradeInfoItem item) {
            TradeHistory.AddLast(item);
            ShortTradeHistory.Add(item);
        }

        DateTime epoch = new DateTime(1970, 1, 1);
        protected internal void UpdateSimulationTradeData(TradeInfoItem ti) {
            TradeHistory.AddLast(ti);
            CandleStickData last = CandleStickData.Last();
            if(last == null || (ti.Time - last.Time).TotalMinutes > CandleStickPeriodMin) {
                if(last != null && (ti.Time - last.Time).TotalMinutes > 60) {

                }
                DateTime newTime = epoch.AddMinutes((((int)(ti.Time - epoch).TotalMinutes) / CandleStickPeriodMin) * CandleStickPeriodMin);
                last = new CandleStickData() {
                    Time = newTime,
                    Open = ti.Rate,
                    Close = ti.Rate,
                    High = ti.Rate,
                    Low = ti.Rate
                };
                CandleStickData.Add(last);
            }
            else {
                last.Close = ti.Rate;
                last.High = Math.Max(last.High, ti.Rate);
                last.Low = Math.Max(last.Low, ti.Rate);
            }
        }

        protected event NotifyCollectionChangedEventHandler eventsChanged;
        public event NotifyCollectionChangedEventHandler EventsChanged {
            add { eventsChanged += value; }
            remove { eventsChanged -= value; }
        }

        private void OnEventsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            if(Exchange == null || IsLoading)
                return;
            Save();
            RaiseEventsChanged(e);
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
            return Exchange.Cancel(Exchange.DefaultAccount, info.Ticker, info.OrderId);
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

        void ISupportSerialization.OnBeginSerialize() { }
        void ISupportSerialization.OnEndSerialize() { }
        void ISupportSerialization.OnBeginDeserialize() { }

        public virtual void OnEndDeserialize() {
            foreach(var s in Trailings) {
                s.Ticker = this;
            }
        }
        public double GetActualBidByAmount(double amount) {
            var bids = OrderBook.Bids;
            double total = 0;
            foreach(OrderBookEntry e in bids) {
                total += e.Amount;
                if(total >= amount)
                    return e.Value;
            }
            return 0;
        }

        public double GetActualAskByAmount(double amount) {
            var asks = OrderBook.Asks;
            double total = 0;
            foreach(OrderBookEntry e in asks) {
                total += e.Amount;
                if(total >= amount)
                    return e.Value;
            }
            return 0;
        }

        public virtual bool IsListeningOrderBook { get; }
        public virtual bool IsListeningTradingHistory { get; }
        public virtual bool IsListeningKline { get; }

        IncrementalUpdateQueue updates;
        [XmlIgnore]
        public IncrementalUpdateQueue Updates {
            get {
                if(updates == null && Exchange != null)
                    updates = new IncrementalUpdateQueue(Exchange.CreateIncrementalUpdateDataProvider());
                return updates;
            }
        }

        public string CaptureDirectory { get; set; }
        public bool CaptureData { get; set; }
        public virtual bool ContractTicker { get; set; }
        public virtual double ContractValue { get; set; }
        [XmlIgnore]
        public ArbitrageInfo ArbitrageInfo { get; set; }

        internal void SaveOpenedOrders() {
            PrevOpenedOrders = new OpenedOrderInfo[OpenedOrders.Count];
            for(int i = 0; i < OpenedOrders.Count; i++)
                PrevOpenedOrders[i] = OpenedOrders[i];
        }
        public List<string> GetOpenedOrdersChangeNotifications() {
            List<string> res = new List<string>();
            if(PrevOpenedOrders == null) 
                PrevOpenedOrders = new OpenedOrderInfo[0];
            for(int i = 0; i < PrevOpenedOrders.Length; i ++) {
                var po = PrevOpenedOrders[i];
                var oo = OpenedOrders.FirstOrDefault(o => o.OrderId == PrevOpenedOrders[i].OrderId);
                if(oo == null)
                    res.Add("Order " + po.OrderId + " completely filled");
                else if(oo.Amount != po.Amount) {
                    res.Add("Order " + po.OrderId + " partially filled");
                }
            }
            for(int i = 0; i < OpenedOrders.Count; i++) {
                var oo = OpenedOrders[i];
                var po = PrevOpenedOrders.FirstOrDefault(p => p.OrderId == oo.OrderId);
                if(po == null)
                    res.Add("New order '" + oo.OrderId + "' added. " + oo.TickerName + " rate = " + oo.ValueString + " amount = " + oo.AmountString);
            }
            PrevOpenedOrders = new OpenedOrderInfo[OpenedOrders.Count];
            OpenedOrders.CopyTo(PrevOpenedOrders);
            return res;
        }

        public void OnEventsChanged() {
            OnEventsChanged(Events, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        protected void RaiseEventsChanged(NotifyCollectionChangedEventArgs e) {
            if(eventsChanged != null)
                eventsChanged.Invoke(this, e);
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

    public class TradeHistoryChangedEventArgs : EventArgs {
        
        public Ticker Ticker { get; set; }
        public TradeInfoItem NewItem { get; set; }
        public IEnumerable<TradeInfoItem> NewItems { get; set; } = new List<TradeInfoItem>();
    }

    public delegate void TradeHistoryChangedEventHandler(object sender, TradeHistoryChangedEventArgs e);

    public enum TickerDataStatus { Invalid, Actual }

    public static class DataCacheManager {
        public static int MaxAllowedTaskCount { get; set; } = 3;
        
        public static Dictionary<string, DataTask> Tasks { get; private set; } = new Dictionary<string, DataTask>();
        public static Dictionary<object, Dictionary<string, object>> Data { get; set; } = new Dictionary<object, Dictionary<string, object>>();
        public static object GetData(object owner, string dataName, TimeSpan updateInterval, object nullValue, Func<object> getData) {
            Dictionary<string, object> props = GetProperties(owner);
            object res = nullValue;
            if(!props.TryGetValue(dataName, out res)) {
                props.Add(dataName, nullValue);
                res = nullValue;
            }
            if(!object.Equals(res, nullValue))
                return res;
            DataTask dt = null;
            if(!Tasks.TryGetValue(GetKey(owner, dataName), out dt))
                CreateTask(owner, dataName, getData);
            else
                dt.Priority++;
            UpdateTasks();
            return nullValue;
        }
        private static string GetKey(object owner, string dataName) {
            string key = string.Format("{0}{1}", owner.GetHashCode(), dataName);
            return key;
        }
        private static bool HasTask(object owner, string dataName) {
            string key = GetKey(owner, dataName);
            return Tasks.ContainsKey(key);
        }
        public static void SetValue(object owner, string dataName, object value) {
            Dictionary<string, object> props = GetProperties(owner);
            if(props.ContainsKey(dataName))
                props[dataName] = value;
            else 
                props.Add(dataName, value);
        }
        private static void CreateTask(object owner, string dataName, Func<object> getData) {
            string key = GetKey(owner, dataName);
            Task<object> t = new Task<object>(() => {
                object res = getData();
                SetValue(owner, dataName, res);
                OnTaskCompleted(owner);
                return res;
            });
            DataTask dt = new DataTask() { Task = t, Priority = 1 };
            if(owner is Exchange)
                dt.Exchange = (Exchange)owner;
            if(owner is Ticker)
                dt.Exchange = ((Ticker)owner).Exchange;
            Tasks.Add(GetKey(owner, dataName), dt);
            if(GetRunningTaskCount() < MaxAllowedTaskCount)
                t.Start();
        }
        private static int GetRunningTaskCount() {
            int count = 0;
            var tasks = Tasks.Values.ToList();
            for(int i = 0; i < tasks.Count; i++) {
                DataTask dt = tasks[i];
                if(dt == null)
                    continue;
                Task t = dt.Task;
                if(t.Status == TaskStatus.Canceled || t.Status == TaskStatus.Faulted)
                    continue;
                if(t.Status == TaskStatus.Created || t.Status == TaskStatus.WaitingForActivation)
                    continue;
                if(t.Status == TaskStatus.RanToCompletion)
                    continue;
                count++;
            }
            return count;
        }
        public static void UpdateTasks() {
            int count = GetRunningTaskCount();
            if(count >= MaxAllowedTaskCount)
                return;
            List<KeyValuePair<string, DataTask>> tasks = Tasks.OrderByDescending( tt => tt.Value.Priority).ToList();
            foreach(var pair in tasks) {
                DataTask dataTask = pair.Value;
                if(dataTask.Exchange != null && dataTask.Exchange.GetRequestFillPercent() > 30)
                    continue;
                Task task = pair.Value.Task;
                
                if(task.Status == TaskStatus.Canceled || task.Status == TaskStatus.Faulted || task.Status == TaskStatus.RanToCompletion) {
                    Tasks.Remove(pair.Key);
                    continue;
                }
                if(task.Status == TaskStatus.Created || task.Status == TaskStatus.WaitingForActivation) {
                    task.Start();
                    count++;
                    if(count >= MaxAllowedTaskCount)
                        break;
                }
            }
        }
        private static void OnTaskCompleted(object owner) {
            if(owner is ICachedDataOwner)
                ((ICachedDataOwner)owner).OnDataUpdated();
        }
        private static Dictionary<string, object> GetProperties(object owner) {
            Dictionary<string, object> props = null;
            if(Data.TryGetValue(owner, out props))
                return props;
            props = new Dictionary<string, object>();
            Data.Add(owner, props);
            return props;
        }

        internal static void RemoveTask(object owner, string dataName) {
            string key = GetKey(owner, dataName);
            if(Tasks.ContainsKey(key)) {
                DataTask task = Tasks[key];
                if(task.Task == null)
                    Tasks.Remove(key);
                if(task.Task != null && (task.Task.Status == TaskStatus.Created || task.Task.Status == TaskStatus.WaitingForActivation)) {
                    Tasks.Remove(key);
                }
            }
        }
    }

    public class DataTask {
        public Task Task { get; set; }
        public int Priority { get; set; }
        public Exchange Exchange { get; set; }
    }

    interface ICachedDataOwner {
        void OnDataUpdated();
    }
}
