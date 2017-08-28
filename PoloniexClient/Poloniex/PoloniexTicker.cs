using CryptoMarketClient.Common;
using CryptoMarketClient.Poloniex;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using WampSharp.V2;

namespace CryptoMarketClient {
    public class PoloniexTicker : ITicker {
        public PoloniexTicker() {
            History = new List<TickerHistoryItem>();
        }

        public int Index { get; set; }
        public int Id { get; set; }

        string currensyPair;
        public string CurrencyPair {
            get { return currensyPair; }
            set {
                currensyPair = value;
                OnCurrencyPairChanged();
            }
        }
        void OnCurrencyPairChanged() {
            string[] curr = CurrencyPair.Split('_');
            FirstCurrency = curr[0];
            SecondCurrency = curr[1];
        }
        public string FirstCurrency {
            get; private set;
        }
        public string SecondCurrency {
            get; private set;
        }
        public decimal Last { get; set; }

        decimal lowestAsk;
        public decimal LowestAsk {
            get { return lowestAsk; }
            set {
                if(value != LowestAsk)
                    DeltaAsk = value - LowestAsk;
                lowestAsk = value;
            }
        }
        decimal highestBid;
        public decimal HighestBid {
            get { return highestBid; }
            set {
                if(value != HighestBid)
                    DeltaBid = value - HighestBid;
                highestBid = value;
            }
        }

        PoloniexAccountBalanceInfo firstInfo, secondInfo;
        protected PoloniexAccountBalanceInfo FirstCurrencyBalanceInfo {
            get {
                if(firstInfo == null)
                    firstInfo = PoloniexModel.Default.Balances.FirstOrDefault((b) => b.Currency == FirstCurrency);
                return firstInfo;
            }
        }
        protected PoloniexAccountBalanceInfo SecondCurrencyBalanceInfo {
            get {
                if(secondInfo == null)
                    secondInfo = PoloniexModel.Default.Balances.FirstOrDefault((b) => b.Currency == SecondCurrency);
                return secondInfo;
            }
        }

        PoloniexCurrencyInfo marketCurrencyInfo;
        protected PoloniexCurrencyInfo MarketCurrencyInfo {
            get {
                if(marketCurrencyInfo == null)
                    marketCurrencyInfo = PoloniexModel.Default.Currencies.FirstOrDefault(c => c.Currency == SecondCurrency);
                return marketCurrencyInfo;
            }
        }

        public decimal BaseCurrencyBalance { get { return FirstCurrencyBalanceInfo == null? 0: FirstCurrencyBalanceInfo.Available; } }
        public decimal MarketCurrencyBalance { get { return SecondCurrencyBalanceInfo == null? 0: SecondCurrencyBalanceInfo.Available; } }
        public bool MarketCurrencyEnabled { get { return MarketCurrencyInfo == null ? false : !MarketCurrencyInfo.Disabled; } }
        public decimal Change { get; set; }
        public decimal BaseVolume { get; set; }
        public decimal Volume { get; set; }
        public bool IsFrozen { get; set; }
        public decimal Hr24High { get; set; }
        public decimal Hr24Low { get; set; }
        public DateTime Time { get; set; }
        public decimal BidChange { get; set; }
        public decimal AskChange { get; set; }
        public string HostName { get { return "Poloniex"; } }
        public decimal Fee { get { return 0.25m * 0.01m; } }

        public decimal DeltaAsk { get; set; }
        public decimal DeltaBid { get; set; }
        public decimal Spread { get { return LowestAsk - HighestBid; } }
        public List<TickerHistoryItem> History { get; } = new List<TickerHistoryItem>();
        public List<TradeHistoryItem> TradeHistory { get; } = new List<TradeHistoryItem>();
        public List<CandleStickData> CandleStickData { get; set; } = new List<CandleStickData>();
        public int CandleStickPeriodMin { get; set; } = 1;

        public void Assign(PoloniexTicker ticker) {
            CurrencyPair = ticker.CurrencyPair;
            Last = ticker.Last;
            LowestAsk = ticker.LowestAsk;
            HighestBid = ticker.HighestBid;
            Change = ticker.Change;
            BaseVolume = ticker.BaseVolume;
            Volume = ticker.Volume;
            IsFrozen = ticker.IsFrozen;
            Hr24High = ticker.Hr24High;
            Hr24Low = ticker.Hr24Low;
            Time = ticker.Time;
        }

        public void UpdateHistoryItem() {
            TickerUpdateHelper.UpdateHistoryItem(this);
        }
        public event EventHandler HistoryItemAdd;
        void ITicker.RaiseHistoryItemAdded() {
            if(HistoryItemAdd != null)
                HistoryItemAdd(this, EventArgs.Empty);
        }

        OrderBook orderBook;
        public OrderBook OrderBook {
            get {
                if(orderBook == null)
                    orderBook = new OrderBook(this);
                return orderBook;
            }
        }
        public bool IsActualState { get { return OrderBook.IsActualState; } }
        public void OnChanged() {
            RaiseChanged();   
        }
        string ITicker.BaseCurrency { get { return FirstCurrency; } set { FirstCurrency = value; } }
        string ITicker.MarketCurrency { get { return SecondCurrency; } set { SecondCurrency = value; } }
        void ITicker.OnChanged(OrderBookUpdateInfo info) {
            RaiseChanged();
        }
        protected internal void RaiseChanged() {
            if(Changed != null)
                Changed(this, EventArgs.Empty);
        }

        public event EventHandler Changed;
        public event EventHandler TradeHistoryAdd;
        string ITicker.Name { get { return CurrencyPair; } }
        
        protected internal void RaiseTradeHistoryItemAdd() {
            if(TradeHistoryAdd != null)
                TradeHistoryAdd(this, EventArgs.Empty);
        }

        public void GetOrderBookSnapshot() {
            PoloniexModel.Default.GetOrderBook(this, ModelBase.OrderBookDepth);
        }
        public void GetOrderBookSnapshot(int depth) {
            PoloniexModel.Default.GetOrderBook(this, depth);
        }
        TickerUpdateHelper updateHelper;
        protected TickerUpdateHelper UpdateHelper {
            get {
                if(updateHelper == null)
                    updateHelper = new TickerUpdateHelper(this);
                return updateHelper;
            }
        }
        void ITicker.SubscribeOrderBookUpdates() {
            UpdateHelper.SubscribeOrderBookUpdates();
        }
        void ITicker.UnsubscribeOrderBookUpdates() {
            UpdateHelper.UnsubscribeOrderBookUpdates();
        }
        void ITicker.SubscribeTickerUpdates() {
            PoloniexModel.Default.Connect();
            //UpdateHelper.SubscribeTickerUpdates();
        }
        void ITicker.UnsubscribeTickerUpdates() {
            //UpdateHelper.UnsubscribeTickerUpdates();
        }
        void ITicker.SubscribeTradeUpdates() {
            UpdateHelper.SubscribeTradeUpdates();
        }
        void ITicker.UnsubscribeTradeUpdates() {
            UpdateHelper.UnsubscribeTradeUpdates();
        }
        void ITicker.UpdateOrderBook() {
            PoloniexModel.Default.GetOrderBook(this, 50);
        }
        void ITicker.UpdateTicker() {
            //PoloniexModel.Default.GetTicker(this);
        }
        void ITicker.UpdateTrades() {
            PoloniexModel.Default.UpdateTrades(this);
        }
        public string MarketName {
            get {
                return FirstCurrency + "_" + SecondCurrency;
            }
        }
        public string WebPageAddress { get { return "https://poloniex.com/exchange#" + MarketName.ToLower(); } }
        public string DownloadString(string address) {
            try {
                return PoloniexModel.Default.GetWebClient().DownloadString(address);
            }
            catch { }
            return string.Empty;
        }
        public bool UpdateArbitrageOrderBook(int depth) {
            bool res = PoloniexModel.Default.UpdateArbitrageOrderBook(this, depth);
            if(res) {
                HighestBid = OrderBook.Bids[0].Value;
                LowestAsk = OrderBook.Asks[0].Value;
                Time = DateTime.Now;
                UpdateHistoryItem();
            }
            return res;
        }
        public Task<string> GetOrderBookStringAsync(int depth) {
            return PoloniexModel.Default.GetWebClient().DownloadStringTaskAsync(PoloniexModel.Default.GetOrderBookString(this, depth));
        }
        public void ProcessArbitrageOrderBook(string text) {
            PoloniexModel.Default.OnUpdateArbitrageOrderBook(this, text);
        }
        public bool Buy(decimal rate, decimal amount) {
            return PoloniexModel.Default.BuyLimit(this, rate, amount) != -1;
        }
        public bool Sell(decimal rate, decimal amount) {
            return PoloniexModel.Default.SellLimit(this, rate, amount) != -1;
        }
        public bool UpdateBalance(CurrencyType type) {
            return PoloniexModel.Default.GetBalance(type == CurrencyType.BaseCurrency? FirstCurrency: SecondCurrency);
        }
        public string GetDepositAddress(CurrencyType type) {
            if(type == CurrencyType.BaseCurrency) {
                if(!string.IsNullOrEmpty(FirstCurrencyBalanceInfo.DepositAddress))
                    return FirstCurrencyBalanceInfo.DepositAddress;
                return PoloniexModel.Default.CreateDeposit(FirstCurrency);
            }
            if(!string.IsNullOrEmpty(SecondCurrencyBalanceInfo.DepositAddress))
                return SecondCurrencyBalanceInfo.DepositAddress;
            return PoloniexModel.Default.CreateDeposit(SecondCurrency);
        }
        public bool Withdraw(CurrencyType currencyType, string address, decimal amount) {
            string currency = currencyType == CurrencyType.BaseCurrency ? FirstCurrency : SecondCurrency;
            return PoloniexModel.Default.Withdraw(currency, amount, address, "");
        }
    }
}
