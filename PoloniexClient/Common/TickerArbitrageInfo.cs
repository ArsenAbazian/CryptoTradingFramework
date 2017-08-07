using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public class TickerArbitrageInfo {
        public string BaseCurrency { get; set; }
        public string MarketCurrency { get; set; }

        public ITicker[] Tickers { get; private set; } = new ITicker[16];
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

        public void UpdateLowestAskTicker() {
            LowestAskTicker = GetLowestAskTicker();
        }

        public void UpdateHighestBidTicker() {
            HighestBidTicker = GetHighestBidTicker();
        }
     
        public void Update() {
            UpdateLowestAskTicker();
            UpdateHighestBidTicker();
            UpdateSpread();
            UpdateAmount();
            UpdateTotal();
        }
        void UpdateTotal() {
            if(LowestAskTicker == HighestBidTicker) {
                Total = 0;
                return;
            }
            if(Spread < 0) {
                Total = 0;
                return;
            }
            Total = Spread * Amount;
        }
        void UpdateSpread() {
            if(LowestAskTicker == HighestBidTicker) {
                Spread = -10000000;
                return;
            }
            Spread = HighestBidTicker.HighestBid - LowestAskTicker.LowestAsk;
        }
        void UpdateAmount() {
            if(LowestAskTicker == HighestBidTicker) {
                Amount = 0;
                return;
            }
            if(LowestAskTicker.OrderBook.Asks.Count == 0 || HighestBidTicker.OrderBook.Bids.Count == 0)
                return;
            Amount = Math.Min(LowestAskTicker.OrderBook.Asks[0].Amount, HighestBidTicker.OrderBook.Bids[0].Amount);
        }
    }
}
