using CryptoMarketClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public class ArbitrageInfo {
        public static int Depth { get; set; } = 7;
        public static decimal InvalidValue = -10000000;

        TickerBase lowestAskTicker;
        TickerBase highestBidTicker;

        public ArbitrageInfo(TickerCollection collection) {
            Owner = collection;
            Tickers = Owner.Tickers;
        }

        public TickerBase UsdTicker { get; set; }
        public TickerBase LowestAskTicker {
            get { return lowestAskTicker; }
            set {
                if(lowestAskTicker == value)
                    return;
                lowestAskTicker = value;
                OnLowestAskTickerChanged();
            }
        }
        protected TickerBase[] Tickers { get; private set; }
        public TickerBase HighestBidTicker {
            get { return highestBidTicker; }
            set {
                if(highestBidTicker == value)
                    return;
                highestBidTicker = value;
                OnHighestBidTickerChanged();
            }
        }
        public decimal Amount { get; set; }
        public string LowestAskHost { get; set; }
        public string HighestBidHost { get; set; }
        public decimal LowestAskBalance { get; set; }
        public decimal HighestBidBalance { get; set; }
        public decimal AvailableAmount { get; set; }
        public decimal BuyTotal { get; set; }
        public decimal LowestAsk { get; set; }
        public decimal HighestBid { get; set; }
        public decimal Spread { get; set; }
        public decimal Total { get; set; }
        public decimal LowestBidAskRelation { get; set; }
        public decimal HighestBidAskRelation { get; set; }
        public decimal TotalFee { get; set; }
        public decimal ExpectedProfitUSD { get; set; }
        public decimal MaxProfit { get; set; }
        public decimal AvailableProfit { get; set; }
        public decimal AvailableProfitUSD { get; set; }
        public decimal MaxProfitUSD { get; set; }
        public bool LowestAskEnabled { get; set; }
        public bool HighestBidEnabled { get; set; }
        public bool Ready { get { return LowestAskEnabled && HighestBidEnabled; } }
        public decimal BidShift { get; set; }
        public decimal AskShift { get; set; }

        public TickerCollection Owner { get; set; }
        public decimal PrevHipe { get; set; }
        public decimal PrevSellHipe { get; set; }
        public decimal BidHipe { get; set; }
        public decimal AskHipe { get; set; }
        
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
            decimal prev = MaxProfitUSD;
            UsdTicker = Owner.UsdTicker;

            CalculateHipe();

            LowestAskTicker = GetLowestAskTicker();
            HighestBidTicker = GetHighestBidTicker();

            if(LowestAskTicker == HighestBidTicker) {
                LowestAsk = LowestAskTicker.OrderBook.Asks[0].Value;
                HighestBid = HighestBidTicker.OrderBook.Bids[0].Value;
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

            OrderBookEntry[] bids = HighestBidTicker.OrderBook.Bids;
            OrderBookEntry[] asks = LowestAskTicker.OrderBook.Asks;

            decimal bidAmount = 0;
            for(int bidIndex = 0; bidIndex < Depth; bidIndex++) {
                bidAmount += bids[bidIndex].Amount;
                decimal bid = bids[bidIndex].Value;
                decimal askAmount = 0;
                for(int askIndex = 0; askIndex < Depth; askIndex++) {
                    decimal ask = asks[askIndex].Value;
                    if(ask > bid)
                        break;
                    askAmount += asks[askIndex].Amount;
                    decimal amount = Math.Min(bidAmount, askAmount);
                    decimal spread = bid - ask;
                    if(spread < 0)
                        break;
                    decimal total = spread * amount;
                    decimal fee = (bid + ask) * amount * 0.0025m;
                    decimal profit = total - fee;
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

            decimal buyAmount = LowestAsk == 0? 0: LowestAskBalance / LowestAsk;
            decimal sellAmount = HighestBidBalance;

            AvailableAmount = Math.Min(buyAmount, sellAmount);
            AvailableProfit = Spread * AvailableAmount - (HighestBid + LowestAsk) * AvailableAmount * 0.0025m;
            AvailableProfitUSD = CalcProfitUSD(AvailableProfit);

            if(LowestAskTicker.OrderBook.Bids[0].Value != 0) {
                decimal lowestBid = LowestAskTicker.OrderBook.Bids[0].Value;
                BidShift = HighestBidTicker.OrderBook.Bids[0].Value - lowestBid;
                BidShift /= lowestBid;

                decimal lowestAsk = LowestAskTicker.OrderBook.Asks[0].Value;
                AskShift = HighestBidTicker.OrderBook.Asks[0].Value - lowestAsk;
                AskShift /= lowestAsk;
            }
            UpdateHistory(prev);
        }
        TickerBase GetLowestAskTicker() {
            if(Owner.Count == 2)
                return Tickers[0].LowestAsk > Tickers[1].LowestAsk? Tickers[1] : Tickers[0];

            decimal lowAsk = Tickers[0].LowestAsk;
            TickerBase lowTicker = Tickers[0];
            for(int i = 1; i < Owner.Count; i++) {
                decimal ask = Tickers[i].LowestAsk;
                if(lowAsk > ask) {
                    lowTicker = Tickers[i];
                    lowAsk = ask;
                }
            }
            return lowTicker;
        }
        TickerBase GetHighestBidTicker() {
            if(Owner.Count == 2)
                return Tickers[0].HighestBid < Tickers[1].HighestBid ? Tickers[1] : Tickers[0];

            decimal highBid = Tickers[0].HighestBid;
            TickerBase highTicker = Tickers[0];
            for(int i = 1; i < Owner.Count; i++) {
                decimal bid = Tickers[i].HighestBid;
                if(highBid < bid) {
                    highTicker = Tickers[i];
                    highBid = bid;
                }
            }
            return highTicker;
        }
        decimal CalcProfitUSD(decimal profit) {
            if(LowestAskTicker == HighestBidTicker)
                return 0;
            if(UsdTicker == null)
                return profit;
            return profit * UsdTicker.Last;
        }

        void UpdateHistory(decimal prev) {
            if(MaxProfitUSD < 0 && prev < 0)
                return;
            if(Math.Abs(MaxProfitUSD - prev) < 0.0000001m)
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
            return LowestAskTicker.Buy(LowestAsk, AvailableAmount);
        }
        public bool Sell() {
            return HighestBidTicker.Sell(HighestBid, AvailableAmount);
        }
        public void SaveExpectedProfitUSD() {
            ExpectedProfitUSD = MaxProfitUSD;
        }
        public void UpateAmountByBalance() {
            decimal buyAmount = LowestAskTicker.BaseCurrencyBalance / LowestAsk;
            decimal sellAmount = HighestBidTicker.MarketCurrencyBalance;
            decimal maxAmount = Math.Min(buyAmount, sellAmount);

            Amount = maxAmount;
            BuyTotal = LowestAsk * Amount;
            Total = Spread * Amount;
        }
    }

    public class ArbitrageHistoryItem {
        public DateTime Time { get; set; }
        public decimal Amount { get; set; }
        public decimal Spread { get; set; }
        public decimal Value { get; set; }
        public decimal ValueUSD { get; set; }
    }
}
