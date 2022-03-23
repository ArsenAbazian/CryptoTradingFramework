using Crypto.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core {
    public class OrderBook {
        public const int Depth = 20;
        public static bool AllowOrderBookHistory { get; set; } = false;
        public OrderBook(Ticker owner) {
            Owner = owner;
            Bids = CreateOrderBookEntries();
            Asks = CreateOrderBookEntries();
            IsDirty = true;
            EnableValidationOnRemove = true;
        }

        IncrementalUpdateQueue updates;
        [XmlIgnore]
        public IncrementalUpdateQueue Updates {
            get { 
                if(updates == null) {
                    if(Owner != null && Owner.Exchange != null)
                        updates = new IncrementalUpdateQueue(Owner.Exchange.CreateIncrementalUpdateDataProvider());
                }
                return updates;
            }
        }

        public Ticker Owner { get; private set; }
        public List<OrderBookEntry> Bids { get; private set; }
        public List<OrderBookEntry> Asks { get; private set; }

        public void Clear() {
            Bids.Clear();
            Asks.Clear();
        }
        protected internal void RaiseOnChanged(IncrementalUpdateInfo info) {
            OrderBookEventArgs e = new OrderBookEventArgs() { Update = info };
            e.Ticker = Owner;
            if(Changed != null)
                Changed(this, e);
        }
        public event OrderBookEventHandler Changed;

        public double BidVolume { get; private set; }
        public double AskVolume { get; private set; }
        public double BidExpectation { get; private set; }

        public void Offset(double firstBidValue) {
            double delta = firstBidValue - Bids[0].Value;
            foreach(OrderBookEntry e in Bids) { 
                e.Offset(delta);
            }
            foreach(OrderBookEntry e in Asks) { 
                e.Offset(delta);
            }
            HighestBid = Bids[0].Value;
            LowestAsk = Asks[0].Value;
        }

        public double AskExpectation { get; private set; }
        public double BidDispersion { get; private set; }
        public double AskDispersion { get; private set; }
        public double BidAskRelation {
            get {
                if(BidVolume == 0) return 0;
                return 100 * BidVolume / (BidVolume + AskVolume);
            }
        }

        public double PrevBidVolume { get; private set; }
        public double PrevAskVolume { get; private set; }
        public double PrevBidExpectation { get; private set; }
        public double PrevAskExpectation { get; private set; }
        public double PrevBidDispersion { get; private set; }
        public double PrevAskDispersion { get; private set; }
        public double PrevBidAskRelation { get; private set; }

        public double BidVolumeChange { get; set; }
        public double AskVolumeChange { get; set; }
        public double BidExpectationChange { get; set; }
        public double AskExpectationChange { get; set; }
        public double BidDispersionChange { get; set; }
        public double AskDispersionChange { get; set; }
        public double BidAskRelationChange { get; set; }

        public List<OrderBookStatisticItem> VolumeHistory { get; } = new List<OrderBookStatisticItem>();

        CycleArray<OrderBookStatisticItem> shortHistory;
        public CycleArray<OrderBookStatisticItem> ShortHistory {
            get {
                if(shortHistory == null)
                    shortHistory = new CycleArray<OrderBookStatisticItem>(50000);
                return shortHistory;
            }
        }

        public void UpdateShortHistory() {
            if(Asks.Count == 0 || Bids.Count == 0)
                return;
            while(ShortHistory.ThreadLock) ;
            ShortHistory.ThreadLock = true;
            try {
                ShortHistory.Add(new OrderBookStatisticItem() {
                    Ask = Asks[0].Value,
                    Bid = Bids[0].Value,
                    BidVolume = BidVolume,
                    AskVolume = AskVolume,
                    BidExpectation = BidExpectation,
                    AskExpectation = AskExpectation,
                    BidDispersion = BidDispersion,
                    AskDispersion = AskDispersion,
                    BidEnergy = BidEnergy,
                    AskEnergy = AskEnergy,
                    BidHipe = BidHipe,
                    AskHipe = AskHipe,
                    TradeInfo = TradeInfo,
                    Time = DateTime.UtcNow
                });
            }
            finally {
                ShortHistory.ThreadLock = false;
            }
        }
        
        public void UpdateVolumeHistory() {
            if(Asks.Count == 0 || Bids.Count == 0)
                return;
            VolumeHistory.Add(new OrderBookStatisticItem() {
                Ask = Asks[0].Value,
                Bid = Bids[0].Value,
                BidVolume = BidVolume,
                AskVolume = AskVolume,
                BidExpectation = BidExpectation,
                AskExpectation = AskExpectation,
                BidDispersion = BidDispersion,
                AskDispersion = AskDispersion,
                BidEnergy = BidEnergy,
                AskEnergy = AskEnergy,
                BidHipe = BidHipe,
                AskHipe = AskHipe,
                TradeInfo = TradeInfo,
                Time = DateTime.UtcNow
            });
        }
        
        protected double CalcChange(double prev, double current) {
            if(prev == 0) return 0;
            return 100 * (current - prev) / prev;
        }
        public void CalcStatistics() {
            CalcVolume(OrderBook.Depth);
            //CalcHipe();
            //CalcEnergy();
        }
        public void CalcVolume(int depth) {
            PrevBidVolume = BidVolume;
            PrevAskVolume = AskVolume;
            PrevBidExpectation = BidExpectation;
            PrevAskExpectation = AskExpectation;
            PrevBidDispersion = BidDispersion;
            PrevAskDispersion = AskDispersion;
            PrevBidAskRelation = BidAskRelation;

            double bidVolume = 0, askVolume = 0;
            double bidExpectation = 0, askExpectation = 0;
            double bidDispersion = 0, askDispersion = 0;
            CalcVolume(Bids, out bidVolume, out bidExpectation, out bidDispersion, depth);
            CalcVolume(Asks, out askVolume, out askExpectation, out askDispersion, depth);

            BidVolume = bidVolume;
            AskVolume = askVolume;
            BidExpectation = bidExpectation;
            AskExpectation = askExpectation;
            BidDispersion = bidDispersion;
            AskDispersion = askDispersion;

            BidVolumeChange = CalcChange(PrevBidVolume, BidVolume);
            AskVolumeChange = CalcChange(PrevAskVolume, AskVolume);
            BidExpectationChange = CalcChange(PrevBidExpectation, BidExpectation);
            AskExpectationChange = CalcChange(PrevAskExpectation, AskExpectation);
            BidDispersionChange = CalcChange(PrevBidDispersion, BidDispersion);
            AskDispersionChange = CalcChange(PrevAskDispersion, AskDispersion);
            BidAskRelationChange = CalcChange(PrevBidAskRelation, BidAskRelation);
        }

        public void Assign(OrderBook orderBook) {
            AssignArrays(Bids, orderBook.Bids);
            AssignArrays(Asks, orderBook.Asks);
            //Bids.Clear();
            //Asks.Clear();
            //for(int i = 0; i < orderBook.Bids.Count; i++) {
            //    var entry = orderBook.Bids[i];
            //    this.Bids.Add(new OrderBookEntry() { Amount = entry.Amount, Value = entry.Value });
            //}
            //for(int i = 0; i < orderBook.Asks.Count; i++) {
            //    var entry = orderBook.Asks[i];
            //    this.Asks.Add(new OrderBookEntry() { Amount = entry.Amount, Value = entry.Value });
            //}
        }

        protected void AssignArrays(List<OrderBookEntry> dest, List<OrderBookEntry> src) {
            int minCount = Math.Min(dest.Count, src.Count);
            int addCount = src.Count - dest.Count;

            if(addCount < 0) {
                addCount = -addCount;
                while(addCount > 0) {
                    dest.RemoveAt(0);
                    addCount--;
                }
            }

            var den  = dest.GetEnumerator();
            var sen = src.GetEnumerator();
            while(sen.MoveNext() && den.MoveNext()) {
                den.Current.Amount = sen.Current.Amount;
                den.Current.Value = sen.Current.Value;
            }
            for(int i = 0; i < addCount; i++, sen.MoveNext()) {
                dest.Add(new OrderBookEntry() { Amount = sen.Current.Amount, Value = sen.Current.Value });
            }
        }

        public void OffsetBids(double firstBidValue) {
            double delta = firstBidValue - Bids[0].Value;
            for(int i = 0; i < Bids.Count; i++) {
                Bids[i].Value += delta;
            }
        }

        public void OffsetAsks(double firstAskValue) {
            double delta = firstAskValue - Asks[0].Value;
            for(int i = 0; i < Asks.Count; i++) {
                Asks[i].Value += delta;
            }
        }

        void CalcVolume(List<OrderBookEntry> list, out double volume, out double exp, out double disp, int depth) {
            int count = Math.Min(depth, list.Count);
            int index = 0;
            volume = 0;
            exp = 0;
            disp = 0;
            for(int i = 0; i < list.Count; i++) {
                OrderBookEntry e = list[i];
                volume += e.Amount;
                exp += e.Amount * e.Value;
                index++;
                if(index == count) break;
            }
            exp /= list.Count;
            if(volume == 0) {
                exp = 0;
                disp = 0;
                return;
            }

            index = 0;
            double invVolume = 1.0 / volume;
            double qexp = exp * exp;
            for(int i = 0; i < list.Count; i++) {
                OrderBookEntry e = list[i];
                disp += (e.Value * e.Value) * e.Amount * invVolume - qexp;
                index++;
                if(index == count) break;
            }
        }

        public double CalcAskAmount(double maxAsk) {
            double total = 0;
            lock(Asks) {
                foreach(OrderBookEntry e in Asks) {
                    if(e.Value > maxAsk)
                        break;
                    total += e.Amount;
                }
            }
            return total;
        }

        public double CalcBidAmount(double minBid) {
            double total = 0;
            lock(Bids) {
                foreach(OrderBookEntry e in Bids) {
                    if(e.Value < minBid)
                        break;
                    total += e.Amount;
                }
            }
            return total;
        }

        protected internal List<OrderBookEntry> CreateOrderBookEntries() {
            if(!AllowOrderBookHistory) {
                return new List<OrderBookEntry>(OrderBook.Depth);
            }
            return OrderBookAllocator.GetNew();
        }
        protected double MaxVolume { get; set; }
        public void SubscribeUpdateEntries(bool value) {
            if(value)
                UpdateEntriesCount++;
            else
                UpdateEntriesCount--;
            if(UpdateEntriesCount < 0) UpdateEntriesCount = 0;
        }
        protected int UpdateEntriesCount { get; set; }
        public bool AllowAdditionalCalculations { get; set; } = true;
        public double HighestBid { get; set; }
        public double LowestAsk { get; set; }
        public bool Updating { get { return UpdateCount > 0; } }

        public void UpdateEntries() {
            LastUpdateTime = DateTime.Now;
            if(Bids.Count == 0 || Asks.Count == 0)
                return;
            HighestBid = Bids[0].Value;
            LowestAsk = Asks[0].Value;
            if(UpdateEntriesCount == 0)
                return;
            if(!AllowAdditionalCalculations)
                return;
            double MaxVolume = 0;
            double vt = 0;
            for(int i = 0; i < Bids.Count; i++) {
                OrderBookEntry e = Bids[i];
                e.Volume = e.Value * e.Amount;
                vt += e.Volume;
                e.VolumeTotal = vt;
                MaxVolume = Math.Max(MaxVolume, e.Volume);
            }
            vt = 0;
            for(int i = 0; i < Asks.Count; i++) {
                OrderBookEntry e = Asks[i];
                e.Volume = e.Value * e.Amount;
                vt += e.Volume;
                e.VolumeTotal = vt;
                MaxVolume = Math.Max(MaxVolume, e.Volume);
            }
            double mv = Asks.Count == 0 ? 0 : Asks.Last().VolumeTotal;
            vt = 0;
            if(MaxVolume == 0)
                return;
            MaxVolume = 1 / MaxVolume;
            for(int i = 0; i < Bids.Count; i++) {
                OrderBookEntry e = Bids[i];
                e.VolumePercent = e.Volume * MaxVolume;
            }
            for(int i = 0; i < Asks.Count; i++) {
                OrderBookEntry e = Asks[i];
                e.VolumePercent = e.Volume * MaxVolume;
            }
        }
        public double BidEnergy { get; set; }
        public double AskEnergy { get; set; }
        public void CalcEnergy() {
            //List<OrderBookEntry> pb = BidsHistory.Last();
            //List<OrderBookEntry> pa = AskHistory.Last();

            //double bidEnergy = 0;
            //double askEnerty = 0;

            //for(int i = 0; i < OrderBook.Depth; i++) {
            //    double bidSpeed = Bids[i].Value - pb[i].Value;
            //    double askSpeed = Asks[i].Value - pa[i].Value;
            //    bidEnergy += Bids[i].Amount * (bidSpeed * bidSpeed);
            //    askEnerty += Asks[i].Amount * (askSpeed * askSpeed);
            //}

            //BidEnergy = bidEnergy;
            //AskEnergy = askEnerty;

            //for(int i = 0; i < BidEnergies.Length - 1; i++) {
            //    BidEnergies[i] = BidEnergies[i + 1];
            //    AskEnergies[i] = AskEnergies[i + 1];
            //}

            //BidEnergies[BidEnergies.Length - 1] = BidEnergy;
            //AskEnergies[AskEnergies.Length - 1] = AskEnergy;
        }
        protected int UpdateCount { get; set; }
        public void BeginUpdate() {
            UpdateCount++;
        }
        public void EndUpdate() {
            if(UpdateCount > 0)
                UpdateCount--;
            if(UpdateCount > 0)
                return;
            UpdateEntries();
            RaiseOnChanged(new IncrementalUpdateInfo());
        }

        protected List<List<OrderBookEntry>> BidsHistory { get; } = new List<List<OrderBookEntry>>();
        protected List<List<OrderBookEntry>> AskHistory { get; } = new List<List<OrderBookEntry>>();
        public double BidHipe { get; set; }
        public double AskHipe { get; set; }
        public double PrevBidHipe { get { return BidHipes[BidHipes.Length - 1]; } }
        public double PrevAskHipe { get { return AskHipes[AskHipes.Length - 1]; } }
        public TradeStatisticsItem TradeInfo { get; set; }

        public double[] BidHipes { get; set; } = new double[22];
        public double[] AskHipes { get; set; } = new double[22];
        public double[] BidEnergies { get; set; } = new double[22];
        public double[] AskEnergies { get; set; } = new double[22];
        public bool BidHipeStarted { get { return BidHipe >= 60 && PrevBidHipe < 60; } }
        public bool BidHipeStopped { get { return BidHipe < 60 && PrevBidHipe >= 60; } }

        public bool AskHipeStarted { get { return AskHipe >= 60 && PrevAskHipe < 60; } }
        public bool AskHipeStopped { get { return AskHipe < 60 && PrevAskHipe >= 60; } }

        public void CalcHipe() {
            List<OrderBookStatisticItem> h = VolumeHistory;
            if(h.Count < 22) {
                BidHipe = 0;
                AskHipe = 0;
                return;
            }

            int index = h.Count - 1;
            int buyCount = 0, sellCount = 0;

            int hi = 0;
            for(int i = index - 1; i >= index - 21; i--, hi++) {
                if(h[i].BuyVolume > 0)
                    buyCount++;
                if(h[i].SellVolume > 0)
                    sellCount++;
                BidHipes[hi] = BidHipes[hi + 1];
                AskHipes[hi] = AskHipes[hi + 1];
            }
            BidHipe = buyCount / 20 * 100;
            AskHipe = sellCount / 20 * 100;
            BidHipes[hi] = BidHipe;
            AskHipes[hi] = AskHipe;
        }
        public void Save() {
            BidsHistory.Add(Bids);
            AskHistory.Add(Asks);
            if(BidsHistory.Count > 2000) {
                OrderBookAllocator.Free(BidsHistory, 1000);
                OrderBookAllocator.Free(AskHistory, 1000);
            }
        }
        public void GetNewBidAsks() {
            if(!AllowOrderBookHistory) {
                Bids.Clear();
                Asks.Clear();
                return;
            }
            lock(OrderBookAllocator.Pool) {
                Save();
                Bids = CreateOrderBookEntries();
                Asks = CreateOrderBookEntries();
            }
        }

        protected bool IsEqual(double v1, double v2) {
            double e = Math.Abs(v1 - v2);
            if(e < 0.000000009) return true;
            return false;
        }

        public bool IsDirty { get; set; }
        protected void ApplyIncrementalUpdate(long id, OrderBookUpdateType type, string rateString, string amountString, List<OrderBookEntry> list, bool ascending) {
            
            int index = 0;
            if(type == OrderBookUpdateType.Remove) {
                for(int i = 0; i < list.Count; i++) {
                    OrderBookEntry e = list[i];
                    if(e.Id == id) {
                        list.Remove(e);
                        return;
                    }
                }
                if(EnableValidationOnRemove)
                    IsDirty = true;
                return;
            }
            double amount = FastValueConverter.Convert(amountString);
            if(type == OrderBookUpdateType.Modify) {
                for(int i = 0; i < list.Count; i++) {
                    OrderBookEntry e = list[i];
                    if(e.Id == id) {
                        e.Amount = amount;
                        return;
                    }
                }
                if(EnableValidationOnRemove)
                    IsDirty = true;
                return;
            }
            double value = FastValueConverter.Convert(rateString);
            if(ascending) {
                for(int i = 0; i < list.Count; i++) {
                    OrderBookEntry e = list[i];
                    if(e.Value > value) {
                        OrderBookEntry ee = new OrderBookEntry() { ValueString = rateString, AmountString = amountString, Id = id };
                        list.Insert(index, ee);
                        return;
                    }
                    index++;
                }
            }
            else {
                for(int i = 0; i < list.Count; i++) {
                    OrderBookEntry e = list[i];
                    if(e.Value < value) {
                        OrderBookEntry ee = new OrderBookEntry() { ValueString = rateString, AmountString = amountString, Id = id };
                        list.Insert(index, ee);
                        return;
                    }
                    index++;
                }
            }
            OrderBookEntry addE = new OrderBookEntry() { ValueString = rateString, AmountString = amountString, Id = id };
            list.Add(addE);
        }
        protected void ApplyIncrementalUpdate(string rateString, string amountString, List<OrderBookEntry> list, bool ascending) {
            double value = FastValueConverter.Convert(rateString);
            double amount = FastValueConverter.Convert(amountString);
            int index = 0;
            if(amount == 0) {
                for(int i = 0; i < list.Count; i++) {
                    OrderBookEntry e = list[i];
                    if(IsEqual(e.Value, value)) {
                        list.Remove(e);
                        return;
                    }
                }
                if(EnableValidationOnRemove)
                    IsDirty = true;
                return;
            }
            if(ascending) {
                for(int i = 0; i < list.Count; i++) {
                    OrderBookEntry e = list[i];
                    if(IsEqual(e.Value, value)) {
                        e.AmountString = amountString;
                        return;
                    }
                    if(e.Value > value) {
                        OrderBookEntry ee = new OrderBookEntry() { ValueString = rateString, AmountString = amountString };
                        list.Insert(index, ee);
                        return;
                    }
                    index++;
                }
            }
            else {
                for(int i = 0; i < list.Count; i++) {
                    OrderBookEntry e = list[i];
                    if(IsEqual(e.Value, value)) {
                        e.AmountString = amountString;
                        return;
                    }
                    if(e.Value < value) {
                        OrderBookEntry ee = new OrderBookEntry() { ValueString = rateString, AmountString = amountString };
                        list.Insert(index, ee);
                        return;
                    }
                    index++;
                }
            }
            OrderBookEntry addE = new OrderBookEntry() { ValueString = rateString, AmountString = amountString };
            list.Add(addE);
        }

        public DateTime LastUpdateTime { get; set; }
        public bool EnableValidationOnRemove { get; internal set; }

        public void ApplyIncrementalUpdate(OrderBookEntryType type, string rateString, string amountString) {
            if(type == OrderBookEntryType.Ask) {
                ApplyIncrementalUpdate(rateString, amountString, Asks, true);
            }
            else {
                ApplyIncrementalUpdate(rateString, amountString, Bids, false);
            }
            if(UpdateCount > 0)
                return;
            UpdateEntries();
            RaiseOnChanged(new IncrementalUpdateInfo());
        }

        public void ApplyIncrementalUpdate(OrderBookEntryType type, OrderBookUpdateType updateType, long id, string rateString, string amountString) {
            if(type == OrderBookEntryType.Ask) {
                lock(Asks) {
                    ApplyIncrementalUpdate(id, updateType, rateString, amountString, Asks, true);
                }
            }
            else {
                lock(Bids) {
                    ApplyIncrementalUpdate(id, updateType, rateString, amountString, Bids, false);
                }
            }
            LastUpdateTime = DateTime.Now;
            if(UpdateCount > 0)
                return;
            UpdateEntries();
            RaiseOnChanged(new IncrementalUpdateInfo());
        }
    }

    public delegate void OrderBookEventHandler(object sender, OrderBookEventArgs e);

    public class OrderBookEventArgs : EventArgs {
        public Ticker Ticker { get; set; }
        public IncrementalUpdateInfo Update { get; set; }
    }
    
    public class OrderBookStatisticItem {
        public OrderBookEntry[] Bids { get; set; }
        public OrderBookEntry[] Asks { get; set; }

        public double Ask { get; set; }
        public double Bid { get; set; }
        public double BidVolume { get; set; }
        public double AskVolume { get; set; }
        public double BidExpectation { get; set; }
        public double AskExpectation { get; set; }
        public double BidDispersion { get; set; }
        public double AskDispersion { get; set; }
        public double BidEnergy { get; set; }
        public double AskEnergy { get; set; }
        public double BidHipe { get; set; }
        public double AskHipe { get; set; }
        
        public double BidAskRelation {
            get {
                if(BidVolume == 0) return 0;
                return 100 * BidVolume / (BidVolume + AskVolume);
            }
        }
        public DateTime Time { get; set; }

        public TradeStatisticsItem TradeInfo { get; set; }

        public double MinBuyPrice { get { return TradeInfo == null ? 0 : TradeInfo.MinBuyPrice; } }
        public double MaxBuyPrice { get { return TradeInfo == null ? 0 : TradeInfo.MaxBuyPrice; } }
        public double BuyAmount { get { return TradeInfo == null ? 0 : TradeInfo.BuyAmount; } }
        public double BuyVolume { get { return TradeInfo == null ? 0 : TradeInfo.BuyVolume; } }
        public double MinSellPrice { get { return TradeInfo == null ? 0 : TradeInfo.MinSellPrice; } }
        public double MaxSellPrice { get { return TradeInfo == null ? 0 : TradeInfo.MaxSellPrice; } }
        public double SellAmount { get { return TradeInfo == null ? 0 : TradeInfo.SellAmount; } }
        public double SellVolume { get { return TradeInfo == null ? 0 : TradeInfo.SellVolume; } }
    }

    public static class OrderBookAllocator {
        static List<List<OrderBookEntry>> pool;
        public static List<List<OrderBookEntry>> Pool { get { return pool; } }

        static OrderBookAllocator() {
            if(!OrderBook.AllowOrderBookHistory)
                return;
            pool = new List<List<OrderBookEntry>>(100000);
            for(int i = 0; i < 100000; i++) {
                List<OrderBookEntry> list = new List<OrderBookEntry>(OrderBook.Depth);
                for(int j = 0; j < OrderBook.Depth; j++)
                    list[j] = new OrderBookEntry();
                pool.Add(list);
            }
        }

        public static List<OrderBookEntry> GetNew() {
            if(pool.Count == 0) {
                for(int i = 0; i < 10000; i++) {
                    List<OrderBookEntry> list = new List<OrderBookEntry>(OrderBook.Depth);
                    pool.Add(list);
                }
            }
            List<OrderBookEntry> res = pool.First();
            pool.RemoveAt(0);
            return res;
        }
        public static void Free(List<List<OrderBookEntry>> history, int count) {
            for(int index = 0; index < count; index++) {
                pool.Add(history.First());
                history.RemoveAt(0);
            }
        }
    } 
}
