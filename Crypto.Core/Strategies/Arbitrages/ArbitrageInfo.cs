using CryptoMarketClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public class ArbitrageInfo {
        public static int Depth { get; set; } = 7;
        public static double InvalidValue = -10000000;

        Ticker lowestAskTicker;
        Ticker highestBidTicker;

        public ArbitrageInfo(TickerCollection collection) {
            Owner = collection;
            Tickers = Owner.Tickers;
        }

        public Ticker UsdTicker { get; set; }
        public Ticker LowestAskTicker {
            get { return lowestAskTicker; }
            set {
                if(lowestAskTicker == value)
                    return;
                lowestAskTicker = value;
                OnLowestAskTickerChanged();
            }
        }
        protected Ticker[] Tickers { get; private set; }
        public Ticker HighestBidTicker {
            get { return highestBidTicker; }
            set {
                if(highestBidTicker == value)
                    return;
                highestBidTicker = value;
                OnHighestBidTickerChanged();
            }
        }
        public double Amount { get; set; }
        public string LowestAskHost { get; set; }
        public string HighestBidHost { get; set; }
        public double LowestAskBalance { get; set; }
        public double HighestBidBalance { get; set; }
        public double AvailableAmount { get; set; }
        public double BuyTotal { get; set; }
        public double LowestAsk { get; set; }
        public double HighestBid { get; set; }
        public double Spread { get; set; }
        public double Total { get; set; }
        public double LowestBidAskRelation { get; set; }
        public double HighestBidAskRelation { get; set; }
        public double TotalFee { get; set; }
        public double ExpectedProfitUSD { get; set; }
        public double MaxProfit { get; set; }
        public double AvailableProfit { get; set; }
        public double AvailableProfitUSD { get; set; }
        public double MaxProfitUSD { get; set; }
        public bool LowestAskEnabled { get; set; }
        public bool HighestBidEnabled { get; set; }
        public bool Ready { get { return LowestAskEnabled && HighestBidEnabled; } }
        public double BidShift { get; set; }
        public double AskShift { get; set; }

        public TickerCollection Owner { get; set; }
        public double PrevHipe { get; set; }
        public double PrevSellHipe { get; set; }
        public double BidHipe { get; set; }
        public double AskHipe { get; set; }
        
        public bool IsBoosted { get { return BidHipe > 60 && PrevHipe <= 60; } }
        public bool IsBoostStopped { get { return PrevHipe > 60 && BidHipe <= 60; } }

        public bool IsSellBoosted { get { return AskHipe > 60 && PrevSellHipe <= 60; } }
        public bool IsSellBoostStopped { get { return PrevSellHipe > 60 && AskHipe <= 60; } }

        public void CalculateHipe() {
            PrevHipe = BidHipe;
            PrevSellHipe = AskHipe;
            BidHipe = 0;
            AskHipe = 0;
            for(int i = 0; i < Owner.Count; i++) {
                BidHipe = Math.Max(BidHipe, Tickers[i].OrderBook.BidHipe);
                AskHipe = Math.Max(AskHipe, Tickers[i].OrderBook.AskHipe);
            }
        }

        public void Calculate() {
            double prev = MaxProfitUSD;
            UsdTicker = Owner.UsdTicker;

            CalculateHipe();

            LowestAskTicker = GetLowestAskTicker();
            HighestBidTicker = GetHighestBidTicker();

            List<OrderBookEntry> bids = HighestBidTicker.OrderBook.Bids;
            List<OrderBookEntry> asks = LowestAskTicker.OrderBook.Asks;

            if(LowestAskTicker == HighestBidTicker || bids.Count == 0 || asks.Count == 0) {
                LowestAsk = asks.Count == 0? 0.0: asks[0].Value;
                HighestBid = bids.Count == 0 ? 0.0: bids[0].Value;
                Amount = 0;
                BuyTotal = 0;
                Spread = HighestBid - LowestAsk;
                Total = 0;
                TotalFee = 0;
                MaxProfit = 0;
                AvailableProfit = 0;
                MaxProfitUSD = 0;
                AvailableProfitUSD = 0;
                BidShift = 0;
                AskShift = 0;
                UpdateHistory(prev);
                return;
            }

            MaxProfit = InvalidValue;

            double bidAmount = 0;
            foreach(OrderBookEntry e in bids) { 
                bidAmount += e.Amount;
                double bid = e.Value;
                double askAmount = 0;
                for(int askIndex = 0; askIndex < asks.Count; askIndex++) {
                    double ask = asks[askIndex].Value;
                    if(ask > bid)
                        break;
                    askAmount += asks[askIndex].Amount;
                    double amount = Math.Min(bidAmount, askAmount);
                    double spread = bid - ask;
                    if(spread < 0)
                        break;
                    double total = spread * amount;
                    double fee = (bid + ask) * amount * 0.0025;
                    double profit = total - fee;
                    if(profit > MaxProfit) {
                        TotalFee = fee;
                        MaxProfit = profit;
                        HighestBid = bid;
                        LowestAsk = ask;
                        Amount = amount;
                        Spread = spread;
                        BuyTotal = LowestAsk * Amount;
                        Total = Spread * Amount;
                    }
                }
            }

            if(MaxProfit == InvalidValue) {
                MaxProfit = 0;
                Amount = 0;
            }
            MaxProfitUSD = CalcProfitUSD(MaxProfit);

            double buyAmount = LowestAsk == 0? 0: LowestAskBalance / LowestAsk;
            double sellAmount = HighestBidBalance;

            AvailableAmount = Math.Min(buyAmount, sellAmount);
            AvailableProfit = Spread * AvailableAmount - (HighestBid + LowestAsk) * AvailableAmount * 0.0025;
            AvailableProfitUSD = CalcProfitUSD(AvailableProfit);

            if(LowestAskTicker.OrderBook.Bids[0].Value != 0) {
                double lowestBid = LowestAskTicker.OrderBook.Bids[0].Value;
                BidShift = HighestBidTicker.OrderBook.Bids[0].Value - lowestBid;
                BidShift /= lowestBid;

                double lowestAsk = LowestAskTicker.OrderBook.Asks[0].Value;
                AskShift = HighestBidTicker.OrderBook.Asks[0].Value - lowestAsk;
                AskShift /= lowestAsk;
            }
            UpdateHistory(prev);
        }
        Ticker GetLowestAskTicker() {
            if(Owner.Count == 2)
                return Tickers[0].LowestAsk > Tickers[1].LowestAsk? Tickers[1] : Tickers[0];

            double lowAsk = Tickers[0].LowestAsk;
            Ticker lowTicker = Tickers[0];
            for(int i = 1; i < Owner.Count; i++) {
                double ask = Tickers[i].LowestAsk;
                if(lowAsk > ask) {
                    lowTicker = Tickers[i];
                    lowAsk = ask;
                }
            }
            return lowTicker;
        }
        Ticker GetHighestBidTicker() {
            if(Owner.Count == 2)
                return Tickers[0].HighestBid < Tickers[1].HighestBid ? Tickers[1] : Tickers[0];

            double highBid = Tickers[0].HighestBid;
            Ticker highTicker = Tickers[0];
            for(int i = 1; i < Owner.Count; i++) {
                double bid = Tickers[i].HighestBid;
                if(highBid < bid) {
                    highTicker = Tickers[i];
                    highBid = bid;
                }
            }
            return highTicker;
        }
        double CalcProfitUSD(double profit) {
            if(LowestAskTicker == HighestBidTicker)
                return 0;
            if(UsdTicker == null)
                return profit;
            return profit * UsdTicker.Last;
        }

        void UpdateHistory(double prev) {
            if(MaxProfitUSD < 0 && prev < 0)
                return;
            if(Math.Abs(MaxProfitUSD - prev) < 0.0000001)
                return;
            ArbitrageStatisticsItem st = new ArbitrageStatisticsItem();
            st.Amount = Amount;
            st.LowestAskHost = LowestAskHost;
            st.LowestAskEnabled = LowestAskEnabled;
            st.LowestAsk = LowestAsk;
            st.HighestBidHost = HighestBidHost;
            st.HighestBidEnabled = HighestBidEnabled;
            st.HighestBid = HighestBid;
            st.Spread = Spread;
            st.BaseCurrency = LowestAskTicker.BaseCurrency;
            st.MarketCurrency = LowestAskTicker.MarketCurrency;
            st.MaxProfit = MaxProfit;
            st.MaxProfitUSD = MaxProfitUSD;
            st.Time = DateTime.UtcNow;
            st.RateInUSD = UsdTicker == null? 1: UsdTicker.Last;
            if(!Owner.Disabled) {
                lock(ArbitrageHistoryHelper.Default.History) {
                    ArbitrageHistoryHelper.Default.History.Add(st);
                    ArbitrageHistoryHelper.Default.CheckSave();
                }
            }
        }

        void OnLowestAskTickerChanged() {
            if(LowestAskTicker != null) {
                LowestAskHost = LowestAskTicker.HostName;
                LowestAskBalance = LowestAskTicker.BaseCurrencyBalance;
                LowestBidAskRelation = LowestAskTicker.OrderBook.BidAskRelation;
                LowestAskEnabled = LowestAskTicker.MarketCurrencyEnabled;
            }
            else {
                LowestAskHost = null;
                LowestAskBalance = 0;
                LowestBidAskRelation = 0;
                LowestAskEnabled = false;
            }
        }
        void OnHighestBidTickerChanged() {
            if(HighestBidTicker != null) {
                HighestBidHost = HighestBidTicker.HostName;
                HighestBidBalance = HighestBidTicker.BaseCurrencyBalance;
                HighestBidAskRelation = HighestBidTicker.OrderBook.BidAskRelation;
                HighestBidEnabled = HighestBidTicker.MarketCurrencyEnabled;
            }
            else {
                HighestBidHost = null;
                HighestBidBalance = 0;
                HighestBidAskRelation = 0;
                HighestBidEnabled = false;
            }
        }
        public bool Buy() {
            return LowestAskTicker.Buy(LowestAsk, AvailableAmount) != null;
        }
        public bool Sell() {
            return HighestBidTicker.Sell(HighestBid, AvailableAmount) != null;
        }
        public void SaveExpectedProfitUSD() {
            ExpectedProfitUSD = MaxProfitUSD;
        }
        public void UpateAmountByBalance() {
            double buyAmount = LowestAskTicker.BaseCurrencyBalance / LowestAsk;
            double sellAmount = HighestBidTicker.MarketCurrencyBalance;
            double maxAmount = Math.Min(buyAmount, sellAmount);

            Amount = maxAmount;
            BuyTotal = LowestAsk * Amount;
            Total = Spread * Amount;
        }
    }

    public class ArbitrageHistoryItem {
        public DateTime Time { get; set; }
        public double Amount { get; set; }
        public double Spread { get; set; }
        public double Value { get; set; }
        public double ValueUSD { get; set; }
    }
}
