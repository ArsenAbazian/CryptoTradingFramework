using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public class TickerArbitrageInfo {
        public string BaseCurrency { get; set; }
        public string MarketCurrency { get; set; }

        public List<SimpleHistoryItem> History { get; } = new List<SimpleHistoryItem>();
        public ITicker[] Tickers { get; private set; } = new ITicker[16];
        public ITicker TickerInUSD { get; set; }
        public int Count { get; private set; }

        public void Add(ITicker ticker) {
            Tickers[Count] = ticker;
            Count++;
        }

       ITicker GetLowestAskTicker() {
            double lowAsk = Tickers[0].LowestAsk;
            ITicker lowTicker = Tickers[0];
            for(int i = 1; i < Count; i++) {
                double ask = Tickers[i].LowestAsk;
                if(lowAsk > ask) {
                    lowTicker = Tickers[i];
                    lowAsk = ask;
                }
            }
            return lowTicker;
        }

        ITicker GetHighestBidTicker() {
            double highBid = Tickers[0].HighestBid;
            ITicker highTicker = Tickers[0];
            for(int i = 1; i < Count; i++) {
                double bid = Tickers[i].HighestBid;
                if(highBid < bid) {
                    highTicker = Tickers[i];
                    highBid = bid;
                }
            }
            return highTicker;
        }

        public ITicker LowestAskTicker { get; private set; }
        public ITicker HighestBidTicker { get; private set; }
        public double Amount { get; private set; }
        public string LowestAskHost { get { return LowestAskTicker == null? string.Empty: LowestAskTicker.HostName; } }
        public string HighestBidHost { get { return HighestBidTicker == null? string.Empty: HighestBidTicker.HostName; } }

        public double LowestAsk { get { return LowestAskTicker == null ? 0 : LowestAskTicker.LowestAsk; } }
        public double HighestBid { get { return HighestBidTicker == null ? 0 : HighestBidTicker.HighestBid; } }
        public double Spread { get; set; }
        public double Total { get; set; }
        public double LowestAksFee { get { return LowestAskTicker == null ? 0 : LowestAskTicker.Fee; } }
        public double HighestBidFee { get { return HighestBidTicker == null ? 0 : HighestBidTicker.Fee; } }
        public double TotalFee { get { return Amount * (HighestBidFee * HighestBid + LowestAksFee * LowestAsk); } }
        public double Earning { get { return Total - TotalFee; } }
        public double EarningUSD {
            get {
                if(LowestAskTicker == HighestBidTicker)
                    return -10000000;
                if(TickerInUSD == null)
                    return Earning;
                return Earning * TickerInUSD.Last;
            }
        }
        private const double InvalidValue = -10000000;
        public void Update() {
            double prev = History.Count == 0 ? InvalidValue : History.Last().ValueUSD;
            LowestAskTicker = GetLowestAskTicker();
            HighestBidTicker = GetHighestBidTicker();
            if(LowestAskTicker == HighestBidTicker) {
                Spread = InvalidValue;
                Amount = 0;
                Total = 0;
            }
            else {
                UpdateSpread();
                UpdateAmount();
                UpdateTotal();
                UpdateHistory(prev);
            }
        }
        void UpdateHistory(double prev) {
            if(EarningUSD < 0 && prev < 0)
                return;
            if(Math.Abs(EarningUSD - prev) > 0.0000001)
                History.Add(new SimpleHistoryItem() { Time = DateTime.Now, Value = Earning, ValueUSD = EarningUSD });
        }
        void UpdateTotal() {
            if(Spread < 0) {
                Total = 0;
                return;
            }
            Total = Spread * Amount;
        }
        void UpdateSpread() {
            Spread = HighestBidTicker.HighestBid - LowestAskTicker.LowestAsk;
        }
        void UpdateAmount() {
            Amount = Math.Min(LowestAskTicker.OrderBook.Asks[0].Amount, HighestBidTicker.OrderBook.Bids[0].Amount);
        }
    }

    public class SimpleHistoryItem {
        public DateTime Time { get; set; }
        public double Value { get; set; }
        public double ValueUSD { get; set; }
    }
}
