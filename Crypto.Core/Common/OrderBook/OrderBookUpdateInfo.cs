using Crypto.Core.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core {
    public enum OrderBookUpdateType { Add, Modify, Remove, RefreshAll }
    public class OrderBookUpdateInfo {
        public OrderBookEntry Entry { get; set; }
        public OrderBookEntryType Type { get; set; }
    }
    public interface IIncrementalUpdateDataProvider {
        void Update(Ticker ticker, IncrementalUpdateInfo info);
        void ApplySnapshot(JObject jObject, Ticker ticker);
        void ApplySnapshot(JsonHelperToken root, Ticker ticker);
    }

    public class IncrementalUpdateInfo {
        public IncrementalUpdateInfo() {
            Updates = new List<string[]>(5);
            Empty = true;
            Applied = false;
        }
        public bool Empty { get; private set; }
        public bool Applied { get; set; }
        public long SeqNumber { get; private set; }
        public Ticker Ticker { get; set; }
        public List<string[]> Updates { get; private set; }
        public List<string[]> BidsUpdates { get; private set; }
        public List<string[]> AsksUpdates { get; private set; }
        public List<string[]> TradeUpdates { get; private set; }
        public IIncrementalUpdateDataProvider Provider { get; set; }
        public void Fill(long seqNumber, Ticker ticker, List<string[]> items) {
            Empty = false;
            SeqNumber = seqNumber;
            Updates = items;
            Ticker = ticker;
        }
        public void Fill(long seqNumber, Ticker ticker, List<string[]> bids, List<string[]> asks, List<string[]> trades) {
            Empty = false;
            SeqNumber = seqNumber;
            BidsUpdates = bids;
            AsksUpdates = asks;
            TradeUpdates = trades;
            Ticker = ticker;
        }
        public void Clear() {
            Empty = true;
            Applied = false;           
        }
        public void Apply() {
            Provider.Update(Ticker, this);
            Applied = true;
            if(Ticker.OrderBook.IsDirty)
                Ticker.UpdateOrderBook();
        }
    }

    public class IncrementalUpdateQueue {
        public IncrementalUpdateQueue(IIncrementalUpdateDataProvider provider) {
            Queue = new IncrementalUpdateInfo[10];
            for(int i = 0; i < 10; i++)
                Queue[i] = new IncrementalUpdateInfo() { Provider = provider };
        }
        public long SeqNumber { get; private set; }
        public IncrementalUpdateInfo[] Queue { get; private set; }

        public int Count { get; private set; }
        public int Capacity { get { return Queue.Length; } }
        public IncrementalUpdateInfo this[int index] { get { return Queue[index]; } }
        public bool Push(long seqNumber, Ticker ticker, List<string[]> items) {
            long delta = seqNumber - SeqNumber;
            if(delta >= Capacity) return false;
            if(delta < 0) return true;
            Queue[delta].Fill(seqNumber, ticker, items);
            Count = (int)Math.Max(Count, delta + 1);
            return true;
        }
        public void Apply() {
            for(int i = 0; i < Count; i++) {
                if(Queue[i].Empty)
                    throw new Exception("Que is empty");
                Queue[i].Apply();
            }
            SeqNumber += Count;
            Clear();
        }
        public void Clear(long seqNumber) {
            Clear();
            SeqNumber = seqNumber;
        }
        public void Clear() {
            for(int i = 0; i < Count; i++)
                Queue[i].Clear();
            Count = 0;
        }
        public bool Push(long seqNumber, Ticker ticker, List<string[]> bids, List<string[]> asks, List<string[]> trades) {
            long delta = seqNumber - SeqNumber;
            if(delta >= Capacity) return false;
            if(delta < 0) return true;
            Queue[delta].Fill(seqNumber, ticker, bids, asks, trades);
            Count = (int)Math.Max(Count, delta + 1);
            return true;
        }
        public bool CanApply {
            get {
                for(int i = 0; i < Count; i++) { 
                    if(Queue[i].Empty) return false;
                }
                return true;
            }
        }
        public bool TooLongQueue {
            get { return Count > 180; }
        }
    }
}
