using DevExpress.Utils.Serializing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraCharts;

namespace CryptoMarketClient.Strategies {
    public class GridStrategyBase : TickerStrategyBase {
        public GridStrategyBase(Ticker ticker) : base(ticker) {
        }

        protected override void OnTickCore() {
            throw new NotImplementedException();
        }
        public override string Name => "GridStrategy";

        public List<OrderInfo> BidLines { get; } = new List<OrderInfo>();
        public List<OrderInfo> AskLines { get; } = new List<OrderInfo>();

        [XtraSerializableProperty]
        public double BaseCurrencyMaximum { get; set; }
        [XtraSerializableProperty]
        public double MarketCurrencyMaximum { get; set; }

        public override object HistoryDataSource
        {
            get { throw new NotImplementedException(); }
        } 

        protected virtual void GenerateLines() { }

        public void ClearLines() {
            BidLines.Clear();
            AskLines.Clear();
        }

        protected override void Vizualize(ChartControl chart) {
            throw new NotImplementedException();
        }

        /*
        protected double CalcBuyTotalAmount(double baseCurrencyAmount, out double totalSpent, out double lowestAsk) {
            totalSpent = TotalSpent;
            double totalAmount = 0;
            lowestAsk = 0;
            for(int i = 0; i < OrderBook.Depth; i++) {
                OrderBookEntry e = Ticker.OrderBook.Asks[i];
                double amount = e.Amount;
                lowestAsk = e.Value;
                if(lowestAsk > AverageMarketPrice - GridStep)
                    break;
                if(totalSpent + 1.0025m * e.Value * amount > MaxAllowedBaseCurrencyAmount * 0.98m) {
                    amount = MaxAllowedBaseCurrencyAmount * (0.98m - totalSpent) / 1.0025m / e.Value;
                    totalSpent += amount * e.Value;
                    totalAmount += amount;
                    return totalAmount;
                }

                totalSpent += 1.0025m * e.Value * e.Amount;
                totalAmount = e.Amount;
            }
            return totalAmount;
        }

        public override bool Buy() {
            double totalSpent = 0;
            double lowestAsk = 0;
            double totalAmount = CalcBuyTotalAmount(BaseCurrencyAmount, out totalSpent, out lowestAsk);

            if(totalAmount == 0)
                return true;

            if(BuyCore(lowestAsk, totalAmount, totalSpent)) {
                Ticker.UpdateBalance(Common.CurrencyType.BaseCurrency);
                Ticker.UpdateBalance(Common.CurrencyType.MarketCurrency);
                BaseCurrencyAmount = Ticker.BaseCurrencyBalance;
                MarketCurrencyAmount = Ticker.MarketCurrencyBalance;
                TotalSpent = totalSpent;
                return true;
            }
            return false;
        }

        protected double CalcTotalSellAmount(double marketCurrencyAmount, out double highestBid, out double totalProfit) {
            double totalAmount = 0;
            totalProfit = 0;
            highestBid = 0;
            for(int i = 0; i < OrderBook.Depth; i++) {
                OrderBookEntry e = Ticker.OrderBook.Bids[i];
                if(e.Value < AverageMarketPrice + GridStep)
                    break;
                if(totalAmount + e.Amount > marketCurrencyAmount) {
                    double amount = marketCurrencyAmount - totalAmount;
                    totalAmount = marketCurrencyAmount;
                    highestBid = e.Value;
                    totalProfit += (1 - 0.0025m) * amount * e.Value;
                    break;
                }
                totalAmount += e.Amount;
                highestBid = e.Value;
                totalProfit += (1 - 0.0025m) * e.Value * e.Amount;
            }
            return totalAmount;
        }

        public override bool Sell() {
            double highestBid = 0;
            double totalProfit = 0;
            double totalAmount = CalcTotalSellAmount(MarketCurrencyAmount, out highestBid, out totalProfit);
            if(totalAmount == 0 || highestBid == 0)
                return true;
            if(SellCore(highestBid, totalAmount, totalProfit)) {
                Ticker.UpdateBalance(Common.CurrencyType.MarketCurrency);
                Ticker.UpdateBalance(Common.CurrencyType.BaseCurrency);
                TotalEarn += totalProfit;
                return true;
            }
            return false;
        }
        */
    }

    public class OrderInfo {
        public double Rate { get; set; }
        public double Amount { get; set; }
    }
}
