using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using WampSharp.V2;

namespace CryptoMarketClient {
    public class PoloniexTicker : ITicker {
        public PoloniexTicker() {
            History = new List<TickerHistoryItem>();
        }

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

        public double PercentChange { get; set; }
        public double BaseVolume { get; set; }
        public double QuoteVolume { get; set; }
        public bool IsFrozen { get; set; }
        public double Hr24High { get; set; }
        public double Hr24Low { get; set; }
        public DateTime Time { get; set; }

        public double DeltaAsk { get; set; }
        public double DeltaBid { get; set; }
        public List<TickerHistoryItem> History { get; }

        public void Assign(PoloniexTicker ticker) {
            CurrencyPair = ticker.CurrencyPair;
            Last = ticker.Last;
            LowestAsk = ticker.LowestAsk;
            HighestBid = ticker.HighestBid;
            PercentChange = ticker.PercentChange;
            BaseVolume = ticker.BaseVolume;
            QuoteVolume = ticker.QuoteVolume;
            IsFrozen = ticker.IsFrozen;
            Hr24High = ticker.Hr24High;
            Hr24Low = ticker.Hr24Low;
            Time = ticker.Time;
        }

        public void UpdateHistoryItem() {
            if(History.Count > 10000)
                History.RemoveAt(0);
            History.Add(new TickerHistoryItem() { Time = Time, Ask = LowestAsk, Bid = HighestBid, Current = Last });
            RaiseHistoryItemAdded();
        }
        public event EventHandler HistoryItemAdd;
        void RaiseHistoryItemAdded() {
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
        void ITicker.OnChanged(OrderBookUpdateInfo info) {
            RaiseChanged();
        }
        protected internal void RaiseChanged() {
            if(Changed != null)
                Changed(this, EventArgs.Empty);
        }

        public event EventHandler Changed;
        string ITicker.Name { get { return CurrencyPair; } }
    }
}
