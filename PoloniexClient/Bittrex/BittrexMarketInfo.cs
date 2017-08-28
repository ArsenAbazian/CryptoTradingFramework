using CryptoMarketClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoMarketClient.Bittrex {
    public class BittrexMarketInfo : ITicker {
        public int Index { get; set; }
        public string MarketCurrency { get; set; }
        public string BaseCurrency { get; set; }
        public string MarketCurrencyLong { get; set; }
        public string BaseCurrencyLong { get; set; }
        public decimal MinTradeSize { get; set; }
        public string MarketName { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public decimal HighestBid { get; set; }
        public decimal LowestAsk { get; set; }
        public decimal Last { get; set; }
        public DateTime Time { get; set; }
        public decimal Volume { get; set; }
        public decimal BaseVolume { get; set; }
        public int OpenBuyOrders { get; set; }
        public int OpenSellOrders { get; set; }
        public decimal PrevDay { get; set; }
        public string DisplayMarketName { get; set; }
        public decimal Hr24High { get; set; }
        public decimal Hr24Low { get; set; }
        public decimal Change { get; set; }
        public decimal Spread { get { return LowestAsk - HighestBid; } }
        public decimal BidChange { get; set; }
        public decimal AskChange { get; set; }
        public int CandleStickPeriodMin { get; set; } = 1;
        public decimal Fee { get { return 0.25m * 0.01m; } }

        public decimal BaseCurrencyBalance { get { return BaseBalanceInfo == null ? 0 : BaseBalanceInfo.Available; } }
        public decimal MarketCurrencyBalance { get { return MarketBalanceInfo == null ? 0 : MarketBalanceInfo.Available; } }
        public bool MarketCurrencyEnabled { get { return MarketCurrencyInfo == null ? false : MarketCurrencyInfo.IsActive; } }

        BittrexAccountBalanceInfo baseBalanceInfo, marketBalanceInfo;
        protected BittrexAccountBalanceInfo BaseBalanceInfo {
            get {
                if(baseBalanceInfo == null)
                    baseBalanceInfo = BittrexModel.Default.Balances.FirstOrDefault((b) => b.Currency == BaseCurrency);
                return baseBalanceInfo;
            }
        }

        protected BittrexAccountBalanceInfo MarketBalanceInfo {
            get {
                if(marketBalanceInfo == null)
                    marketBalanceInfo = BittrexModel.Default.Balances.FirstOrDefault((b) => b.Currency == MarketCurrency);
                return marketBalanceInfo;
            }
        }

        BittrexCurrencyInfo marketCurrencyInfo;
        protected BittrexCurrencyInfo MarketCurrencyInfo {
            get {
                if(marketCurrencyInfo == null)
                    marketCurrencyInfo = BittrexModel.Default.Currencies.FirstOrDefault(c => c.Currency == MarketCurrency);
                return marketCurrencyInfo;
            }
        }

        public List<TickerHistoryItem> History { get; } = new List<TickerHistoryItem>();
        public List<TradeHistoryItem> TradeHistory { get; } = new List<TradeHistoryItem>();
        public List<CandleStickData> CandleStickData { get; set; } = new List<CandleStickData>();

        public event EventHandler HistoryItemAdd;
        public event EventHandler Changed;
        public event EventHandler TradeHistoryAdd;

        public void UpdateHistoryItem() {
            TickerUpdateHelper.UpdateHistoryItem(this);
        }

        void ITicker.RaiseHistoryItemAdded() {
            if(HistoryItemAdd != null)
                HistoryItemAdd(this, EventArgs.Empty);
        }

        protected void RaiseChanged() {
            if(Changed != null)
                Changed(this, EventArgs.Empty);
        }

        void ITicker.OnChanged(OrderBookUpdateInfo info) {
            RaiseChanged();
        }
        OrderBook orderBook;
        public OrderBook OrderBook {
            get {
                if(orderBook == null)
                    orderBook = new OrderBook(this);
                return orderBook;
            }
        }

        string ITicker.Name => MarketName;
        void ITicker.GetOrderBookSnapshot(int depth) {
            BittrexModel.Default.GetOrderBook(this, depth);
        }
        void ITicker.GetOrderBookSnapshot() {
            BittrexModel.Default.GetOrderBook(this, ModelBase.OrderBookDepth);
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
            UpdateHelper.SubscribeTickerUpdates();
        }
        void ITicker.UnsubscribeTickerUpdates() {
            UpdateHelper.UnsubscribeTickerUpdates();
        }
        void ITicker.SubscribeTradeUpdates() {
            UpdateHelper.SubscribeTradeUpdates();
        }
        void ITicker.UnsubscribeTradeUpdates() {
            UpdateHelper.UnsubscribeTradeUpdates();
        }
        void ITicker.UpdateOrderBook() {
            BittrexModel.Default.GetOrderBook(this, 50);
        }
        void ITicker.UpdateTicker() {
            BittrexModel.Default.GetTicker(this);
        }
        void ITicker.UpdateTrades() {
            if(TradeHistory.Count == 0)
                BittrexModel.Default.GetTrades(this);
            else 
                BittrexModel.Default.UpdateTrades(this);
        }
        public void RaiseTradeHistoryAdd() {
            if(TradeHistoryAdd != null)
                TradeHistoryAdd(this, EventArgs.Empty);
        }
        protected WebClient WebClient { get; } = new MyWebClient();
        public string DownloadString(string address) {
            try {
                return BittrexModel.Default.GetWebClient().DownloadString(address);
            }
            catch { }
            return string.Empty;
        }
        public bool UpdateArbitrageOrderBook(int depth) {
            bool res = BittrexModel.Default.UpdateArbitrageOrderBook(this, depth);
            if(res) {
                HighestBid = OrderBook.Bids[0].Value;
                LowestAsk = OrderBook.Asks[0].Value;
                Time = DateTime.Now;
                UpdateHistoryItem();
            }
            return res;
        }
        public Task<string> GetOrderBookStringAsync(int depth) {
            return BittrexModel.Default.GetWebClient().DownloadStringTaskAsync(BittrexModel.Default.GetOrderBookString(this, depth));
        }
        public void ProcessArbitrageOrderBook(string text) {
            BittrexModel.Default.UpdateOrderBook(this, text, TickerArbitrageInfo.Depth);
        }
        public bool UpdateBalance(CurrencyType type) {
            return BittrexModel.Default.GetBalance(type == CurrencyType.MarketCurrency? MarketCurrency: BaseCurrency);
        }
        public bool Buy(decimal rate, decimal amount) {
            return BittrexModel.Default.BuyLimit(this, rate, amount) != null;
        }
        public bool Sell(decimal rate, decimal amount) {
            return BittrexModel.Default.SellLimit(this, rate, amount) != null;
        }
        public string GetDepositAddress(CurrencyType type) {
            if(type == CurrencyType.BaseCurrency) {
                if(BaseBalanceInfo == null)
                    return null;
                if(!string.IsNullOrEmpty(BaseBalanceInfo.CryptoAddress))
                    return BaseBalanceInfo.CryptoAddress;
                return BittrexModel.Default.CheckCreateDeposit(BaseCurrency);
            }
            if(MarketBalanceInfo == null)
                return null;
            if(!string.IsNullOrEmpty(MarketBalanceInfo.CryptoAddress))
                return BaseBalanceInfo.CryptoAddress;
            return BittrexModel.Default.CheckCreateDeposit(MarketCurrency);
        }
        string GetCurrency(CurrencyType currencyType) {
            return currencyType == CurrencyType.BaseCurrency ? BaseCurrency : MarketCurrency;
        }
        public bool Withdraw(CurrencyType currencyType, string address, decimal amount) {
            string currency = GetCurrency(currencyType);
            return BittrexModel.Default.Withdraw(currency, amount, address, "");
        }
        public string HostName { get { return "Bittrex"; } }
        public string WebPageAddress { get { return "https://bittrex.com/Market/Index?MarketName=" + MarketName; } }
    }

    public class BittrexCurrencyInfo {
        public string Currency { get; set; }
        public string CurrencyLong { get; set; }
        public int MinConfirmation { get; set; }
        public decimal TxFree { get; set; }
        public bool IsActive { get; set; }
        public string CoinType { get; set; }
        public string BaseAddress { get; set; }
    }
}
