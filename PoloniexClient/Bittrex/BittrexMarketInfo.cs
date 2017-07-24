using System;
using System.Collections.Generic;
using System.Linq;
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
        public double MinTradeSize { get; set; }
        public string MarketName { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public double HighestBid { get; set; }
        public double LowestAsk { get; set; }
        public double Last { get; set; }
        public DateTime Time { get; set; }
        public double Volume { get; set; }
        public double BaseVolume { get; set; }
        public DateTime TimeStamp { get; set; }
        public int OpenBuyOrders { get; set; }
        public int OpenSellOrders { get; set; }
        public double PrevDay { get; set; }
        public string DisplayMarketName { get; set; }
        public double Hr24High { get; set; }
        public double Hr24Low { get; set; }

        public List<TickerHistoryItem> History { get; } = new List<TickerHistoryItem>();
        public List<TradeHistoryItem> TradeHistory { get; } = new List<TradeHistoryItem>();
        public event EventHandler HistoryItemAdd;
        public event EventHandler Changed;
        public event EventHandler TradeHistoryAdd;

        public void UpdateHistoryItem() {
            TickerHistoryItem last = History.Count == 0? null: History.Last();
            if(last != null) {
                if(last.Ask == LowestAsk && last.Bid == HighestBid && last.Current == Last && (Time.Ticks - last.Time.Ticks) < TimeSpan.TicksPerMinute)
                    return;
            }
            if(History.Count > 10000)
                History.RemoveAt(0);
            History.Add(new TickerHistoryItem() { Time = Time, Ask = LowestAsk, Bid = HighestBid, Current = Last });
            RaiseHistoryItemAdded();
        }

        void RaiseHistoryItemAdded() {
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
        void ITicker.GetOrderBookSnapshot() {
            BittrexModel.Default.GetOrderBook(this, 500);
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
            BittrexModel.Default.GetOrderBook(this, 100);
        }
        void ITicker.UpdateTicker() {
            BittrexModel.Default.GetTicker(this);
        }
        void ITicker.UpdateTrades() {
            BittrexModel.Default.UpdateTrades(this);
        }
    }

    public class BittrexCurrencyInfo {
        public string Currency { get; set; }
        public string CurrencyLong { get; set; }
        public int MinConfirmation { get; set; }
        public double TxFree { get; set; }
        public bool IsActive { get; set; }
        public string CoinType { get; set; }
        public string BaseAddress { get; set; }
    }
}
