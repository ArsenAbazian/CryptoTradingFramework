using CryptoMarketClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public class TickerArbitrageInfo {
        public static readonly int Depth = 7;
        private const decimal InvalidValue = -10000000;

        public string BaseCurrency { get; set; }
        public string MarketCurrency { get; set; }
        public bool IsActual { get; set; } = true;
        public bool IsSelected { get; set; }

        public Task UpdateTask { get; set; }
        public List<ArbitrageHistoryItem> History { get; } = new List<ArbitrageHistoryItem>();
        public ITicker[] Tickers { get; private set; } = new ITicker[16];
        public ITicker TickerInUSD { get; set; }
        public int Count { get; private set; }
        public string ShortName { get { return BaseCurrency + "-" + MarketCurrency; } }
        public string Name {
            get {
                if(HighestBidTicker == null || LowestAskTicker == null)
                    return string.Empty;
                return LowestAskTicker.HostName + "(ask)" + "/" + HighestBidTicker.HostName + "(bid)" + "  " + BaseCurrency + "-" + MarketCurrency;
            }
        }
        public ITicker LowestAskTicker { get; private set; }
        public ITicker HighestBidTicker { get; private set; }
        public decimal Amount { get; private set; }
        public string LowestAskHost { get { return LowestAskTicker == null ? string.Empty : LowestAskTicker.HostName; } }
        public string HighestBidHost { get { return HighestBidTicker == null ? string.Empty : HighestBidTicker.HostName; } }
        public decimal LowestAskBalance { get { return LowestAskTicker == null ? 0 : LowestAskTicker.BaseCurrencyBalance; } }
        public decimal HighestBidBalance { get { return HighestBidTicker == null ? 0 : HighestBidTicker.MarketCurrencyBalance; } }
        public decimal AvailableAmount { get; set; }
        public decimal BuyTotal { get; set; }
        public decimal LowestAsk { get; set; }
        public decimal HighestBid { get; set; }
        public decimal Spread { get; set; }
        public decimal Total { get; set; }
        public DateTime LastUpdate { get; set; }
        public long StartUpdateMs { get; set; }
        public long UpdateTimeMs { get; set; }
        public long NextOverdueMs { get; set; }
        public decimal LowestAksFee { get { return LowestAskTicker == null ? 0 : LowestAskTicker.Fee; } }
        public decimal HighestBidFee { get { return HighestBidTicker == null ? 0 : HighestBidTicker.Fee; } }

        decimal CalcTotalFee(decimal amount) { return amount * (HighestBidFee * HighestBid + LowestAksFee * LowestAsk); }
        public decimal TotalFee { get; set; }
        public decimal ExpectedProfitUSD { get; set; }
        public decimal MaxProfit { get; set; }
        public decimal AvailableProfit { get; set; }
        decimal CalcProfitUSD(decimal Profit) {
            if(LowestAskTicker == HighestBidTicker)
                return 0;
            if(TickerInUSD == null)
                return Profit;
            return Profit * TickerInUSD.Last;
        }
        public decimal AvailableProfitUSD {
            get { return CalcProfitUSD(AvailableProfit); }
        }
        public decimal MaxProfitUSD {
            get { return CalcProfitUSD(MaxProfit); }
        }
        public bool IsUpdating { get; set; }
        public bool ObtainingData { get; set; }
        public int ObtainDataCount { get; set; }
        public int ObtainDataSuccessCount { get; set; }

        public void Add(ITicker ticker) {
            Tickers[Count] = ticker;
            Count++;
        }
        ITicker GetLowestAskTicker() {
            decimal lowAsk = Tickers[0].LowestAsk;
            ITicker lowTicker = Tickers[0];
            for(int i = 1; i < Count; i++) {
                decimal ask = Tickers[i].LowestAsk;
                if(lowAsk > ask) {
                    lowTicker = Tickers[i];
                    lowAsk = ask;
                }
            }
            return lowTicker;
        }
        ITicker GetHighestBidTicker() {
            decimal highBid = Tickers[0].HighestBid;
            ITicker highTicker = Tickers[0];
            for(int i = 1; i < Count; i++) {
                decimal bid = Tickers[i].HighestBid;
                if(highBid < bid) {
                    highTicker = Tickers[i];
                    highBid = bid;
                }
            }
            return highTicker;
        }
        public decimal CalcTotalFee(decimal bid, decimal ask, decimal amount) {
            return amount * (HighestBidFee * bid + LowestAksFee * ask);
        }
        public bool LowestAskEnabled { get; set; }
        public bool HighestBidEnabled { get; set; }
        public void Update() {
            decimal prev = History.Count == 0 ? InvalidValue : History.Last().ValueUSD;
            LowestAskTicker = GetLowestAskTicker();
            HighestBidTicker = GetHighestBidTicker();
            if(LowestAskTicker == HighestBidTicker || LowestAskTicker.OrderBook.Asks.Count == 0 || HighestBidTicker.OrderBook.Bids.Count == 0) {
                Spread = InvalidValue;
                AvailableAmount = 0;
                Amount = 0;
                Total = 0;
                TotalFee = 0;
                MaxProfit = 0;
                LowestAskEnabled = true;
                HighestBidEnabled = true;
            }
            else {
                Calculate();
                UpdateHistory(prev);
            }
        }
        void Calculate() {
            decimal maxProfit = InvalidValue;
            HighestBid = 0.0m;
            LowestAsk = 0.0m;
            Amount = 0.0m;
            Spread = 0.0m;
            Total = 0.0m;
            MaxProfit = 0.0m;
            AvailableAmount = 0.0m;
            LowestAskEnabled = LowestAskTicker.MarketCurrencyEnabled;
            HighestBidEnabled = HighestBidTicker.MarketCurrencyEnabled;
            if(LowestAskTicker.OrderBook.Asks.Count == 0)
                return;
            if(HighestBidTicker.OrderBook.Bids.Count == 0)
                return;
            for(int bidIndex = 0; bidIndex < Depth; bidIndex++) {
                for(int askIndex = 0; askIndex < Depth; askIndex++) {
                    decimal bidAmount = CalcBidAmount(bidIndex);
                    decimal askAmount = CalcAskAmount(askIndex);
                    decimal bid = CalcBid(bidIndex);
                    decimal ask = CalcAsk(askIndex);
                    decimal amount = Math.Min(bidAmount, askAmount);

                    decimal spread = bid - ask;
                    decimal total = spread * amount;
                    decimal Profit = total - CalcTotalFee(bid, ask, amount);
                    if(Profit > maxProfit) {
                        maxProfit = Profit;
                        HighestBid = bid;
                        LowestAsk = ask;
                        Amount = amount;
                        Spread = spread;
                        BuyTotal = LowestAsk * Amount;
                        Total = Spread * Amount;
                    }
                } 
            }

            TotalFee = CalcTotalFee(Amount);
            MaxProfit = Total - TotalFee;
            
            decimal buyAmount = LowestAskBalance / LowestAsk;
            decimal sellAmount = HighestBidBalance;
            AvailableAmount = Math.Min(buyAmount, sellAmount);
            AvailableProfit = Spread * AvailableAmount - CalcTotalFee(AvailableAmount);
        }
        decimal CalcBid(int bidIndex) {
            return HighestBidTicker.OrderBook.Bids[bidIndex].Value;
        }
        decimal CalcAsk(int askIndex) {
            return LowestAskTicker.OrderBook.Asks[askIndex].Value;
        }
        decimal CalcBidAmount(int bidIndex) {
            decimal amount = 0.0m;
            for(int i = 0; i <= bidIndex; i++) {
                amount += HighestBidTicker.OrderBook.Bids[i].Amount;
            }
            return amount;
        }
        decimal CalcAskAmount(int askIndex) {
            decimal amount = 0.0m;
            for(int i = 0; i < askIndex; i++) {
                amount += LowestAskTicker.OrderBook.Asks[i].Amount;
            }
            return amount;
        }
        void UpdateHistory(decimal prev) {
            if(MaxProfitUSD < 0 && prev < 0)
                return;
            if(Math.Abs(MaxProfitUSD - prev) < 0.0000001m)
                return;
            ArbitrageHistoryItem item = new ArbitrageHistoryItem() { Time = DateTime.Now, Value = MaxProfit, ValueUSD = MaxProfitUSD, Spread = Spread, Amount = Amount };
            item.LowestAskCurrencyEnabled = LowestAskTicker.MarketCurrencyEnabled;
            item.HighestBidCurrencyEnabled = HighestBidTicker.MarketCurrencyEnabled;
            History.Add(item);
            ArbitrageStatisticsItem st = new ArbitrageStatisticsItem();
            st.Amount = Amount;
            st.LowestAskHost = LowestAskHost;
            st.LowestAskEnabled = LowestAskEnabled;
            st.LowestAsk = LowestAsk;
            st.HighestBidHost = HighestBidHost;
            st.HighestBidEnabled = HighestBidEnabled;
            st.HighestBid = HighestBid;
            st.Spread = Spread;
            st.BaseCurrency = BaseCurrency;
            st.MarketCurrency = MarketCurrency;
            st.MaxProfit = MaxProfit;
            st.MaxProfitUSD = MaxProfitUSD;
            st.Time = DateTime.Now;
            st.RateInUSD = TickerInUSD.Last;
            ArbitrageHistoryHelper.Default.History.Add(st);
            ArbitrageHistoryHelper.Default.CheckSave();
        }
        public bool Buy() {
            return LowestAskTicker.Buy(LowestAsk, Amount);
        }
        public bool Sell() {
            return HighestBidTicker.Sell(HighestBid, Amount);
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
        public decimal Value { get; set; }
        public decimal ValueUSD { get; set; }
        public decimal Spread { get; set; }
        public decimal Amount { get; set; }
        public bool LowestAskCurrencyEnabled { get; set; }
        public bool HighestBidCurrencyEnabled { get; set; }
    }

    public class SyncronizationItemInfo {
        public ITicker Source { get; set; }
        public ITicker Destination { get; set; }

        public string DestinationAddress { get; set; }
        public decimal Amount { get; set; }

        public bool HasDestinationAddress { get { return !string.IsNullOrEmpty(DestinationAddress); } }
        public bool ObtainDestinationAddress() {
            for(int i = 0; i < 3; i++) {
                DestinationAddress = Destination.GetDepositAddress(Common.CurrencyType.MarketCurrency);
                if(HasDestinationAddress)
                    return true;
            }
            return false;
        }
        public bool MakeWithdraw() {
            return Source.Withdraw(Common.CurrencyType.MarketCurrency, DestinationAddress, Amount);
        }
    }
}
