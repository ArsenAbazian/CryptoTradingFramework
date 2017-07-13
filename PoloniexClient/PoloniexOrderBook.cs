using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoloniexClient {

    public enum OrderBookEntryType {
        Bid,
        Ask
    }

    public enum OrderBookUpdateType {
        Add,
        Modify,
        Remove
    }

    public class PoloniexOrderBookEntry {
        public double Rate { get; set; }
        public double Amount { get; set; }
    }

    public class PoloniexOrderBookUpdateInfo {
        public PoloniexOrderBookEntry Entry { get; set; }
        public OrderBookEntryType Type { get; set; }
        public int SeqNo { get; set; }
        public OrderBookUpdateType Update { get; set; }
    }

    public class PoloniexOrderBook {
        public PoloniexOrderBook(PoloniexTicker owner) {
            Owner = owner;
            Bids = new List<PoloniexOrderBookEntry>();
            Asks = new List<PoloniexOrderBookEntry>();
            Updates = new List<PoloniexOrderBookUpdateInfo>();
        }

        public PoloniexTicker Owner { get; private set; }
        public int SeqNumber { get; set; }
        public List<PoloniexOrderBookEntry> Bids { get; private set; }
        public List<PoloniexOrderBookEntry> Asks { get; private set; }
        public List<PoloniexOrderBookUpdateInfo> Updates { get; private set; }
        public bool IsActualState { get { return SeqNumber != 0 && Updates.Count == 0; } }

        public void OnRecvUpdate(PoloniexOrderBookUpdateInfo info) {
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
        void AddUpdateToQueue(PoloniexOrderBookUpdateInfo info) {
            if(Updates.Count == 0)
                GetSnapshot();
            Updates.Add(info);
        }
        void GetSnapshot() {
            PoloniexModel.Default.GetSnapshot(this);
        }
        protected internal void ApplyQueueUpdates() {
            if(Updates.Count == 0)
                return;
            while(Updates.Count > 0) {
                PoloniexOrderBookUpdateInfo info = Updates.First();
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
        protected internal void ApplyInfo(PoloniexOrderBookUpdateInfo info) {
            if(info.Update == OrderBookUpdateType.Remove)
                OnRemove(info);
            else if(info.Update == OrderBookUpdateType.Modify)
                OnModify(info);
        }
        protected internal void OnModify(PoloniexOrderBookUpdateInfo info) {
            PoloniexOrderBookEntry entry = info.Type == OrderBookEntryType.Ask ?
                Asks.FirstOrDefault((e) => e.Rate == info.Entry.Rate) :
                Bids.FirstOrDefault((e) => e.Rate == info.Entry.Rate);
            if(entry == null) {
                OnAdd(info);
                return;
            }
            entry.Amount = info.Entry.Amount;
            info.Entry = entry;
            RaiseOnChanged(info);
            OnChangedCore();
        }
        protected internal void OnAdd(PoloniexOrderBookUpdateInfo info) {
            info.Update = OrderBookUpdateType.Add;
            if(info.Type == OrderBookEntryType.Bid)
                OnAddBid(info);
            else
                OnAddAsk(info);
            RaiseOnChanged(info);
        }
        protected internal void OnAddAsk(PoloniexOrderBookUpdateInfo info) {
            PoloniexOrderBookEntry before = Asks.FirstOrDefault((e) => e.Rate < info.Entry.Rate);
            if(before == null) {
                Asks.Insert(0, info.Entry);
                return;
            }
            Asks.Insert(Asks.IndexOf(before), info.Entry);
        }
        protected internal void ForceAddAsk(PoloniexOrderBookUpdateInfo info) {
            Asks.Insert(0, info.Entry);
        }
        protected internal void ForceAddBid(PoloniexOrderBookUpdateInfo info) {
            Bids.Add(info.Entry);
        }
        void OnAddBid(PoloniexOrderBookUpdateInfo info) {
            PoloniexOrderBookEntry before = Bids.FirstOrDefault((e) => e.Rate < info.Entry.Rate);
            if(before == null) {
                Bids.Add(info.Entry);
                return;
            }
            Bids.Insert(Bids.IndexOf(before), info.Entry);
        }
        void OnRemove(PoloniexOrderBookUpdateInfo info) {
            if(info.Type == OrderBookEntryType.Bid) {
                PoloniexOrderBookEntry entry = Bids.FirstOrDefault((e) => e.Rate == info.Entry.Rate);
                if(entry == null) {
                    Debug.WriteLine("Error removing: '" + info.Type + "' with rate " + info.Entry.Rate + " not found");
                    return;
                }
                Bids.Remove(entry);
            }
            else {
                PoloniexOrderBookEntry entry = Asks.FirstOrDefault((e) => e.Rate == info.Entry.Rate);
                if(entry == null) {
                    Debug.WriteLine("Error removing: '" + info.Type + "' with rate " + info.Entry.Rate + " not found");
                    return;
                }
                Asks.Remove(entry);
            }
            RaiseOnChanged(info);
            OnChangedCore();
        }
        void OnChangedCore() {
            Owner.OnChanged();
        }
        void RaiseOnChanged(PoloniexOrderBookUpdateInfo info) {
            OrderBookEventArgs e = new OrderBookEventArgs() { Update = info };
            if(OnChanged != null)
                OnChanged(this, e);
        }
        public event OrderBookEventHandler OnChanged;
        bool IsExpected(int seqNo) {
            return seqNo == SeqNumber + 1;
        }
        public void Connect() {
            PoloniexModel.Default.GetSnapshot(this);
            PoloniexModel.Default.ConnectOrderBook(this);
        }
    }

    public delegate void OrderBookEventHandler(object sender, OrderBookEventArgs e);

    public class OrderBookEventArgs : EventArgs {
        public PoloniexOrderBookUpdateInfo Update { get; set; }
    }
}
