using DevExpress.Utils.Serializing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraCharts;

namespace CryptoMarketClient.Strategies {
    public class GridStrategyBase : TickerStrategyBase {
        public GridStrategyBase(TickerBase ticker) : base(ticker) {
        }

        protected override void OnTickCore() {
            throw new NotImplementedException();
        }
        public override string Name => "GridStrategy";

        public List<OrderInfo> BidLines { get; } = new List<OrderInfo>();
        public List<OrderInfo> AskLines { get; } = new List<OrderInfo>();

        [XtraSerializableProperty]
        public decimal BaseCurrencyMaximum { get; set; }
        [XtraSerializableProperty]
        public decimal MarketCurrencyMaximum { get; set; }

        public override object HistoryDataSource => throw new NotImplementedException();

        protected virtual void GenerateLines() { }

        public void ClearLines() {
            BidLines.Clear();
            AskLines.Clear();
        }

        protected override void Vizualize(ChartControl chart) {
            throw new NotImplementedException();
        }

        /*
        protected decimal CalcBuyTotalAmount(decimal baseCurrencyAmount, out decimal totalSpent, out decimal lowestAsk) {
            totalSpent = TotalSpent;
            decimal totalAmount = 0;
            lowestAsk = 0;
            for(int i = 0; i < OrderBook.Depth; i++) {
                OrderBookEntry e = Ticker.OrderBook.Asks[i];
                decimal amount = e.Amount;
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
            decimal totalSpent = 0;
            decimal lowestAsk = 0;
            decimal totalAmount = CalcBuyTotalAmount(BaseCurrencyAmount, out totalSpent, out lowestAsk);

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

        protected decimal CalcTotalSellAmount(decimal marketCurrencyAmount, out decimal highestBid, out decimal totalProfit) {
            decimal totalAmount = 0;
            totalProfit = 0;
            highestBid = 0;
            for(int i = 0; i < OrderBook.Depth; i++) {
                OrderBookEntry e = Ticker.OrderBook.Bids[i];
                if(e.Value < AverageMarketPrice + GridStep)
                    break;
                if(totalAmount + e.Amount > marketCurrencyAmount) {
                    decimal amount = marketCurrencyAmount - totalAmount;
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
            decimal highestBid = 0;
            decimal totalProfit = 0;
            decimal totalAmount = CalcTotalSellAmount(MarketCurrencyAmount, out highestBid, out totalProfit);
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
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
    }
}
