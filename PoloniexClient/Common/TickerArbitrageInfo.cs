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

        public double LowestAsk { get { return LowestAskTicker == null ? 0 : LowestAskTicker.LowestAsk; } }
        public double HighestBid { get { return HighestBidTicker == null ? 0 : HighestBidTicker.HighestBid; } }
        public double Spread {
            get {
                if(LowestAskTicker == HighestBidTicker)
                    return -10000000;
                return HighestBidTicker.HighestBid - LowestAskTicker.LowestAsk;
            }
        }

        public void UpdateLowestAskTicker() {
            LowestAskTicker = GetLowestAskTicker();
        }

        public void UpdateHighestBidTicker() {
            HighestBidTicker = GetHighestBidTicker();
        }
     
        public void Update() {
            UpdateLowestAskTicker();
            UpdateHighestBidTicker();
        }
    }
}
