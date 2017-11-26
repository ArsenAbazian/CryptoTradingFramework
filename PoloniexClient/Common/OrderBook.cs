using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public class OrderBook {
        public const int Depth = 25;
        public OrderBook(TickerBase owner) {
            Owner = owner;
            Bids = CreateOrderBookEntries();
            Asks = CreateOrderBookEntries();
        }

        public TickerBase Owner { get; private set; }
        public int SeqNumber { get; set; }
        public OrderBookEntry[] Bids { get; private set; }
        public OrderBookEntry[] Asks { get; private set; }

        public void Clear() {
            for(int i = 0; i < Bids.Length; i++) {
                Bids[i].Clear();
                Asks[i].Clear();
            }
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

        public decimal BidVolume { get; private set; }
        public decimal AskVolume { get; private set; }
        public decimal BidExpectation { get; private set; }
        public decimal AskExpectation { get; private set; }
        public decimal BidDispersion { get; private set; }
        public decimal AskDispersion { get; private set; }
        public decimal BidAskRelation {
            get {
                if(BidVolume == 0) return 0;
                return 100 * BidVolume / (BidVolume + AskVolume);
            }
        }

        public decimal PrevBidVolume { get; private set; }
        public decimal PrevAskVolume { get; private set; }
        public decimal PrevBidExpectation { get; private set; }
        public decimal PrevAskExpectation { get; private set; }
        public decimal PrevBidDispersion { get; private set; }
        public decimal PrevAskDispersion { get; private set; }
        public decimal PrevBidAskRelation { get; private set; }

        public decimal BidVolumeChange { get; set; }
        public decimal AskVolumeChange { get; set; }
        public decimal BidExpectationChange { get; set; }
        public decimal AskExpectationChange { get; set; }
        public decimal BidDispersionChange { get; set; }
        public decimal AskDispersionChange { get; set; }
        public decimal BidAskRelationChange { get; set; }

        public List<OrderBookStatisticItem> VolumeHistory { get; } = new List<OrderBookStatisticItem>();

        public void UpdateHistory() {
            VolumeHistory.Add(new OrderBookStatisticItem() {
                LowestAsk = Asks[0].Value,
                HighestBid = Bids[0].Value,
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
                Time = DateTime.Now
            });
        }
        
        protected decimal CalcChange(decimal prev, decimal current) {
            if(prev == 0) return 0;
            return 100 * (current - prev) / prev;
        }
        public void CalcStatistics() {
            CalcVolume(OrderBook.Depth);
            CalcHipe();
            CalcEnergy();
        }
        protected void CalcVolume(int depth) {
            PrevBidVolume = BidVolume;
            PrevAskVolume = AskVolume;
            PrevBidExpectation = BidExpectation;
            PrevAskExpectation = AskExpectation;
            PrevBidDispersion = BidDispersion;
            PrevAskDispersion = AskDispersion;
            PrevBidAskRelation = BidAskRelation;

            decimal bidVolume = 0, askVolume = 0;
            decimal bidExpectation = 0, askExpectation = 0;
            decimal bidDispersion = 0, askDispersion = 0;
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
        void CalcVolume(OrderBookEntry[] list, out decimal volume, out decimal exp, out decimal disp, int depth) {
            int count = Math.Min(depth, list.Length);
            int index = 0;
            volume = 0;
            exp = 0;
            disp = 0;
            foreach(OrderBookEntry e in list) {
                volume += e.Amount;
                exp += e.Amount * e.Value;
                index++;
                if(index == count) break;
            }
            exp /= list.Length;
            if(volume == 0) {
                exp = 0;
                disp = 0;
                return;
            }

            index = 0;
            decimal invVolume = 1.0m / volume;
            decimal qexp = exp * exp;
            foreach(OrderBookEntry e in list) {
                disp += (e.Value * e.Value) * e.Amount * invVolume - qexp;
                index++;
                if(index == count) break;
            }
        }

        protected internal OrderBookEntry[] CreateOrderBookEntries() {
            return OrderBookAllocator.GetNew();
        }
        protected double MaxVolume { get; set; }
        public void UpdateEntries() {
            decimal MaxVolume = 0;
            for(int i = 0; i < Depth; i++) {
                Bids[i].Volume = Bids[i].Value * Bids[i].Amount;
                Asks[i].Volume = Asks[i].Value * Asks[i].Amount;
                MaxVolume = Math.Max(MaxVolume, Bids[i].Volume);
                MaxVolume = Math.Max(MaxVolume, Asks[i].Volume);
            }
            if(MaxVolume == 0)
                return;
            MaxVolume = 1 / MaxVolume;
            for(int i = 0; i < Depth; i++) {
                Bids[i].VolumePercent = Bids[i].Volume * MaxVolume;
                Asks[i].VolumePercent = Asks[i].Volume * MaxVolume;
            }
        }
        public decimal BidEnergy { get; set; }
        public decimal AskEnergy { get; set; }
        public void CalcEnergy() {
            OrderBookEntry[] pb = BidsHistory.Last();
            OrderBookEntry[] pa = AskHistory.Last();

            decimal bidEnergy = 0;
            decimal askEnerty = 0;

            for(int i = 0; i < OrderBook.Depth; i++) {
                decimal bidSpeed = Bids[i].Value - pb[i].Value;
                decimal askSpeed = Asks[i].Value - pa[i].Value;
                bidEnergy += Bids[i].Amount * (bidSpeed * bidSpeed);
                askEnerty += Asks[i].Amount * (askSpeed * askSpeed);
            }

            BidEnergy = bidEnergy;
            AskEnergy = askEnerty;

            for(int i = 0; i < BidEnergies.Length - 1; i++) {
                BidEnergies[i] = BidEnergies[i + 1];
                AskEnergies[i] = AskEnergies[i + 1];
            }

            BidEnergies[BidEnergies.Length - 1] = BidEnergy;
            AskEnergies[AskEnergies.Length - 1] = AskEnergy;
        }

        protected List<OrderBookEntry[]> BidsHistory { get; } = new List<OrderBookEntry[]>();
        protected List<OrderBookEntry[]> AskHistory { get; } = new List<OrderBookEntry[]>();
        public decimal BidHipe { get; set; }
        public decimal AskHipe { get; set; }
        public decimal PrevBidHipe { get { return BidHipes[BidHipes.Length - 1]; } }
        public decimal PrevAskHipe { get { return AskHipes[AskHipes.Length - 1]; } }
        public TradeStatisticsItem TradeInfo { get; set; }

        public decimal[] BidHipes { get; set; } = new decimal[22];
        public decimal[] AskHipes { get; set; } = new decimal[22];
        public decimal[] BidEnergies { get; set; } = new decimal[22];
        public decimal[] AskEnergies { get; set; } = new decimal[22];
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
            BidHipe = buyCount / 20m * 100m;
            AskHipe = sellCount / 20m * 100m;
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
            lock(OrderBookAllocator.Pool) {
                Save();
                Bids = CreateOrderBookEntries();
                Asks = CreateOrderBookEntries();
            }
        }
    }

    public delegate void OrderBookEventHandler(object sender, OrderBookEventArgs e);

    public class OrderBookEventArgs : EventArgs {
        public OrderBookUpdateInfo Update { get; set; }
    }

    public class OrderBookStatisticItem {
        public OrderBookEntry[] Bids { get; set; }
        public OrderBookEntry[] Asks { get; set; }

        public decimal LowestAsk { get; set; }
        public decimal HighestBid { get; set; }
        public decimal BidVolume { get; set; }
        public decimal AskVolume { get; set; }
        public decimal BidExpectation { get; set; }
        public decimal AskExpectation { get; set; }
        public decimal BidDispersion { get; set; }
        public decimal AskDispersion { get; set; }
        public decimal BidEnergy { get; set; }
        public decimal AskEnergy { get; set; }
        public decimal BidHipe { get; set; }
        public decimal AskHipe { get; set; }
        
        public decimal BidAskRelation {
            get {
                if(BidVolume == 0) return 0;
                return 100 * BidVolume / (BidVolume + AskVolume);
            }
        }
        public DateTime Time { get; set; }

        public TradeStatisticsItem TradeInfo { get; set; }

        public decimal MinBuyPrice { get { return TradeInfo == null ? 0 : TradeInfo.MinBuyPrice; } }
        public decimal MaxBuyPrice { get { return TradeInfo == null ? 0 : TradeInfo.MaxBuyPrice; } }
        public decimal BuyAmount { get { return TradeInfo == null ? 0 : TradeInfo.BuyAmount; } }
        public decimal BuyVolume { get { return TradeInfo == null ? 0 : TradeInfo.BuyVolume; } }
        public decimal MinSellPrice { get { return TradeInfo == null ? 0 : TradeInfo.MinSellPrice; } }
        public decimal MaxSellPrice { get { return TradeInfo == null ? 0 : TradeInfo.MaxSellPrice; } }
        public decimal SellAmount { get { return TradeInfo == null ? 0 : TradeInfo.SellAmount; } }
        public decimal SellVolume { get { return TradeInfo == null ? 0 : TradeInfo.SellVolume; } }
    }

    public static class OrderBookAllocator {
        static List<OrderBookEntry[]> pool;
        public static List<OrderBookEntry[]> Pool { get { return pool; } }

        static OrderBookAllocator() {
            pool = new List<OrderBookEntry[]>(100000);
            for(int i = 0; i < 100000; i++) {
                OrderBookEntry[] list = new OrderBookEntry[OrderBook.Depth];
                for(int j = 0; j < OrderBook.Depth; j++)
                    list[j] = new OrderBookEntry();
                pool.Add(list);
            }
        }

        public static OrderBookEntry[] GetNew() {
            if(pool.Count == 0) {
                for(int i = 0; i < 10000; i++) {
                    OrderBookEntry[] list = new OrderBookEntry[OrderBook.Depth];
                    for(int j = 0; j < OrderBook.Depth; j++)
                        list[j] = new OrderBookEntry();
                    pool.Add(list);
                }
            }
            OrderBookEntry[] res = pool.First();
            pool.RemoveAt(0);
            return res;
        }
        public static void Free(List<OrderBookEntry[]> history, int count) {
            for(int index = 0; index < count; index++) {
                pool.Add(history.First());
                history.RemoveAt(0);
            }
        }
    } 
}
