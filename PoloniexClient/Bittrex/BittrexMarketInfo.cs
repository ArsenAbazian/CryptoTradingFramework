using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public event EventHandler HistoryItemAdd;
        public  event EventHandler Changed;

        public void UpdateHistoryItem() {
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
