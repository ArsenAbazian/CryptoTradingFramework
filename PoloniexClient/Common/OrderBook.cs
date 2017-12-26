using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public class OrderBook {
        public const int Depth = 200;
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

        public double BidVolume { get; private set; }
        public double AskVolume { get; private set; }
        public double BidExpectation { get; private set; }
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
                Time = DateTime.UtcNow
            });
        }
        
        protected double CalcChange(double prev, double current) {
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
        void CalcVolume(OrderBookEntry[] list, out double volume, out double exp, out double disp, int depth) {
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
            double invVolume = 1.0 / volume;
            double qexp = exp * exp;
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
            double MaxVolume = 0;
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
        public double BidEnergy { get; set; }
        public double AskEnergy { get; set; }
        public void CalcEnergy() {
            OrderBookEntry[] pb = BidsHistory.Last();
            OrderBookEntry[] pa = AskHistory.Last();

            double bidEnergy = 0;
            double askEnerty = 0;

            for(int i = 0; i < OrderBook.Depth; i++) {
                double bidSpeed = Bids[i].Value - pb[i].Value;
                double askSpeed = Asks[i].Value - pa[i].Value;
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

        public double LowestAsk { get; set; }
        public double HighestBid { get; set; }
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
