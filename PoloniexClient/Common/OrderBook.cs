using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public class OrderBook {
        public OrderBook(ITicker owner) {
            Owner = owner;
            Bids = new List<OrderBookEntry>();
            Asks = new List<OrderBookEntry>();
            Updates = new List<OrderBookUpdateInfo>();
        }

        public ITicker Owner { get; private set; }
        public int SeqNumber { get; set; }
        public List<OrderBookEntry> Bids { get; private set; }
        public List<OrderBookEntry> Asks { get; private set; }
        public List<OrderBookUpdateInfo> Updates { get; private set; }
        public bool IsActualState { get { return SeqNumber != 0 && Updates.Count == 0; } }

        public void Clear() {
            Asks.Clear();
            Bids.Clear();
        }

        public void OnRecvUpdate(OrderBookUpdateInfo info) {
            bool prevActualState = IsActualState;
            if(IsExpected(info.SeqNo)) {
                ApplyInfo(info);
                ApplyQueueUpdates();
                return;
            }
            if(IsOutdated(info.SeqNo))
                return;
            if(Updates.Count == 0 || Updates.Last().SeqNo == info.SeqNo - 1) {
                AddUpdateToQueue(info);
            }
            else {
                Debug.WriteLine("Error: update with seq no = " + info.SeqNo + " is skipped. Last seq no in que = " + Updates.Last().SeqNo);
                return;
            }
        }
        void AddUpdateToQueue(OrderBookUpdateInfo info) {
            if(Updates.Count == 0)
                GetSnapshot();
            Updates.Add(info);
        }
        void GetSnapshot() {
            Owner.GetOrderBookSnapshot();
        }
        protected internal void ApplyQueueUpdates() {
            if(Updates.Count == 0)
                return;
            while(Updates.Count > 0) {
                OrderBookUpdateInfo info = Updates.First();
                if(info.SeqNo <= SeqNumber) {
                    Updates.Remove(info);
                    continue;
                }
                if(info.SeqNo == SeqNumber + 1) {
                    ApplyInfo(info);
                    Updates.RemoveAt(0);
                }
            }
        }
        bool IsOutdated(int seqNo) {
            return seqNo <= SeqNumber;
        }
        protected internal void ApplyInfo(OrderBookUpdateInfo info) {
            if(info.Action == OrderBookUpdateType.Remove)
                OnRemove(info);
            else if(info.Action == OrderBookUpdateType.Modify)
                OnModify(info);
        }
        protected internal void OnModify(OrderBookUpdateInfo info) {
            OrderBookEntry entry = info.Type == OrderBookEntryType.Ask ?
                Asks.FirstOrDefault((e) => e.Value == info.Entry.Value) :
                Bids.FirstOrDefault((e) => e.Value == info.Entry.Value);
            if(entry == null) {
                OnAdd(info);
                return;
            }
            entry.Amount = info.Entry.Amount;
            info.Entry = entry;
            RaiseOnChanged(info);
            OnChangedCore(info);
        }
        protected internal void OnAdd(OrderBookUpdateInfo info) {
            info.Action = OrderBookUpdateType.Add;
            if(info.Type == OrderBookEntryType.Bid)
                OnAddBid(info);
            else
                OnAddAsk(info);
            RaiseOnChanged(info);
            OnChangedCore(info);
        }
        protected internal void OnAddAsk(OrderBookUpdateInfo info) {
            OrderBookEntry before = Asks.FirstOrDefault((e) => e.Value < info.Entry.Value);
            if(before == null) {
                Asks.Insert(0, info.Entry);
                return;
            }
            Asks.Insert(Asks.IndexOf(before), info.Entry);
        }
        protected internal void ForceAddAsk(OrderBookUpdateInfo info) {
            Asks.Insert(0, info.Entry);
        }
        protected internal void ForceAddBid(OrderBookUpdateInfo info) {
            Bids.Add(info.Entry);
        }
        void OnAddBid(OrderBookUpdateInfo info) {
            OrderBookEntry before = Bids.FirstOrDefault((e) => e.Value < info.Entry.Value);
            if(before == null) {
                Bids.Add(info.Entry);
                return;
            }
            Bids.Insert(Bids.IndexOf(before), info.Entry);
        }
        void OnRemove(OrderBookUpdateInfo info) {
            if(info.Type == OrderBookEntryType.Bid) {
                OrderBookEntry entry = Bids.FirstOrDefault((e) => e.Value == info.Entry.Value);
                if(entry == null) {
                    Debug.WriteLine("Error removing: '" + info.Type + "' with rate " + info.Entry.Value + " not found");
                    return;
                }
                Bids.Remove(entry);
            }
            else {
                OrderBookEntry entry = Asks.FirstOrDefault((e) => e.Value == info.Entry.Value);
                if(entry == null) {
                    Debug.WriteLine("Error removing: '" + info.Type + "' with rate " + info.Entry.Value + " not found");
                    return;
                }
                Asks.Remove(entry);
            }
            RaiseOnChanged(info);
            OnChangedCore(info);
        }
        void OnChangedCore(OrderBookUpdateInfo info) {
            Owner.OnChanged(info);
        }
        protected internal void RaiseOnChanged(OrderBookUpdateInfo info) {
            OrderBookEventArgs e = new OrderBookEventArgs() { Update = info };
            if(OnChanged != null)
                OnChanged(this, e);
        }
        public event OrderBookEventHandler OnChanged;
        bool IsExpected(int seqNo) {
            return seqNo == SeqNumber + 1;
        }
    }

    public delegate void OrderBookEventHandler(object sender, OrderBookEventArgs e);

    public class OrderBookEventArgs : EventArgs {
        public OrderBookUpdateInfo Update { get; set; }
    }
}
