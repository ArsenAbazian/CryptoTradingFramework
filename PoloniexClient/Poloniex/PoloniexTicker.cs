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
        public double Last { get; set; }

        double lowestAsk;
        public double LowestAsk {
            get { return lowestAsk; }
            set {
                if(value != LowestAsk)
                    DeltaAsk = value - LowestAsk;
                lowestAsk = value;
            }
        }
        double highestBid;
        public double HighestBid {
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

        public double BaseCurrencyBalance { get { return FirstCurrencyBalanceInfo == null? 0: FirstCurrencyBalanceInfo.Available; } }
        public double MarketCurrencyBalance { get { return SecondCurrencyBalanceInfo == null? 0: SecondCurrencyBalanceInfo.Available; } }
        public double Change { get; set; }
        public double BaseVolume { get; set; }
        public double Volume { get; set; }
        public bool IsFrozen { get; set; }
        public double Hr24High { get; set; }
        public double Hr24Low { get; set; }
        public DateTime Time { get; set; }
        public double BidChange { get; set; }
        public double AskChange { get; set; }
        public string HostName { get { return "Poloniex"; } }
        public double Fee { get { return 0.25 * 0.01; } }

        public double DeltaAsk { get; set; }
        public double DeltaBid { get; set; }
        public double Spread { get { return LowestAsk - HighestBid; } }
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
            return PoloniexModel.Default.UpdateArbitrageOrderBook(this, depth);
        }
        public Task<string> GetOrderBookStringAsync(int depth) {
            return PoloniexModel.Default.GetWebClient().DownloadStringTaskAsync(PoloniexModel.Default.GetOrderBookString(this, depth));
        }
        public void ProcessArbitrageOrderBook(string text) {
            PoloniexModel.Default.UpdateOrderBook(this, text);
        }
        public bool UpdateBalance(bool updateMarket) {
            return PoloniexModel.Default.GetBalance(updateMarket ? SecondCurrency : FirstCurrency);
        }
        public bool Buy(double rate, double amount) {
            return PoloniexModel.Default.BuyLimit(this, rate, amount) != -1;
        }
        public bool Sell(double rate, double amount) {
            return PoloniexModel.Default.SellLimit(this, rate, amount) != -1;
        }
    }
}
