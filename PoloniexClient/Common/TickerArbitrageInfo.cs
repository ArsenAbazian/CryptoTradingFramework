using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public class TickerArbitrageInfo {
        public static readonly int Depth = 7;
        private const double InvalidValue = -10000000;

        public string BaseCurrency { get; set; }
        public string MarketCurrency { get; set; }
        public bool IsActual { get; set; } = true;
        public bool IsSelected { get; set; }

        public Task UpdateTask { get; set; }
        public List<SimpleHistoryItem> History { get; } = new List<SimpleHistoryItem>();
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
        public double Amount { get; private set; }
        public string LowestAskHost { get { return LowestAskTicker == null ? string.Empty : LowestAskTicker.HostName; } }
        public string HighestBidHost { get { return HighestBidTicker == null ? string.Empty : HighestBidTicker.HostName; } }
        public double LowestAskBalance { get { return LowestAskTicker == null ? 0 : LowestAskTicker.BaseCurrencyBalance; } }
        public double HighestBidBalance { get { return HighestBidTicker == null ? 0 : HighestBidTicker.MarketCurrencyBalance; } }
        public double AvailableAmount { get; set; }
        public double BuyTotal { get; set; }
        public double LowestAsk { get; set; }
        public double HighestBid { get; set; }
        public double Spread { get; set; }
        public double Total { get; set; }
        public DateTime LastUpdate { get; set; }
        public long StartUpdateMs { get; set; }
        public long UpdateTimeMs { get; set; }
        public long NextOverdueMs { get; set; }
        public double LowestAksFee { get { return LowestAskTicker == null ? 0 : LowestAskTicker.Fee; } }
        public double HighestBidFee { get { return HighestBidTicker == null ? 0 : HighestBidTicker.Fee; } }

        double CalcTotalFee(double amount) { return amount * (HighestBidFee * HighestBid + LowestAksFee * LowestAsk); }
        public double TotalFee { get; set; }
        public double ExpectedProfitUSD { get; set; }
        public double MaxProfit { get; set; }
        public double AvailableProfit { get; set; }
        double CalcProfitUSD(double Profit) {
            if(LowestAskTicker == HighestBidTicker)
                return 0;
            if(TickerInUSD == null)
                return Profit;
            return Profit * TickerInUSD.Last;
        }
        public double AvailableProfitUSD {
            get { return CalcProfitUSD(AvailableProfit); }
        }
        public double MaxProfitUSD {
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
        public double CalcTotalFee(double bid, double ask, double amount) {
            return amount * (HighestBidFee * bid + LowestAksFee * ask);
        }
        public void Update() {
            double prev = History.Count == 0 ? InvalidValue : History.Last().ValueUSD;
            LowestAskTicker = GetLowestAskTicker();
            HighestBidTicker = GetHighestBidTicker();
            if(LowestAskTicker == HighestBidTicker || 
                LowestAskTicker.OrderBook.Asks.Count == 0 || 
                HighestBidTicker.OrderBook.Bids.Count == 0) {
                Spread = InvalidValue;
                AvailableAmount = 0;
                Amount = 0;
                Total = 0;
                TotalFee = 0;
                MaxProfit = 0;
            }
            else {
                Calculate();
                UpdateHistory(prev);
            }
        }
        void Calculate() {
            double maxProfit = InvalidValue;
            HighestBid = 0.0;
            LowestAsk = 0.0;
            Amount = 0.0;
            Spread = 0.0;
            Total = 0.0;
            MaxProfit = 0.0;
            AvailableAmount = 0.0;
            if(LowestAskTicker.OrderBook.Asks.Count == 0)
                return;
            if(HighestBidTicker.OrderBook.Bids.Count == 0)
                return;
            for(int bidIndex = 0; bidIndex < Depth; bidIndex++) {
                for(int askIndex = 0; askIndex < Depth; askIndex++) {
                    double bidAmount = CalcBidAmount(bidIndex);
                    double askAmount = CalcAskAmount(askIndex);
                    double bid = CalcBid(bidIndex);
                    double ask = CalcAsk(askIndex);
                    double amount = Math.Min(bidAmount, askAmount);

                    double spread = bid - ask;
                    double total = spread * amount;
                    double Profit = total - CalcTotalFee(bid, ask, amount);
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
            
            double buyAmount = LowestAskBalance / LowestAsk;
            double sellAmount = HighestBidBalance;
            AvailableAmount = Math.Min(buyAmount, sellAmount);
            AvailableProfit = Spread * AvailableAmount - CalcTotalFee(AvailableAmount);
        }
        double CalcBid(int bidIndex) {
            return HighestBidTicker.OrderBook.Bids[bidIndex].Value;
        }
        double CalcAsk(int askIndex) {
            return LowestAskTicker.OrderBook.Asks[askIndex].Value;
        }
        double CalcBidAmount(int bidIndex) {
            double amount = 0.0;
            for(int i = 0; i <= bidIndex; i++) {
                amount += HighestBidTicker.OrderBook.Bids[i].Amount;
            }
            return amount;
        }
        double CalcAskAmount(int askIndex) {
            double amount = 0.0;
            for(int i = 0; i < askIndex; i++) {
                amount += LowestAskTicker.OrderBook.Asks[i].Amount;
            }
            return amount;
        }
        void UpdateHistory(double prev) {
            if(MaxProfitUSD < 0 && prev < 0)
                return;
            if(Math.Abs(MaxProfitUSD - prev) > 0.0000001)
                History.Add(new SimpleHistoryItem() { Time = DateTime.Now, Value = MaxProfit, ValueUSD = MaxProfitUSD, Spread = Spread, Amount = Amount });
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
            double buyAmount = LowestAskTicker.BaseCurrencyBalance / LowestAsk;
            double sellAmount = HighestBidTicker.MarketCurrencyBalance;
            double maxAmount = Math.Min(buyAmount, sellAmount);

            Amount = maxAmount;
            BuyTotal = LowestAsk * Amount;
            Total = Spread * Amount;
        }
    }

    public class SimpleHistoryItem {
        public DateTime Time { get; set; }
        public double Value { get; set; }
        public double ValueUSD { get; set; }
        public double Spread { get; set; }
        public double Amount { get; set; }
    }

    public class SyncronizationItemInfo {
        public ITicker Source { get; set; }
        public ITicker Destination { get; set; }

        public string DestinationAddress { get; set; }
        public double Amount { get; set; }

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
