using Crypto.Core.Strategies.Custom;
using CryptoMarketClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Strategies.Arbitrages.Statistical {
    public class StatisticalArbitrageStrategy : CustomTickerStrategy {
        public override string TypeName => "Statistical Arbitrage";

        [Browsable(false)]
        protected double FirstTickerBid { get; set; }
        [Browsable(false)]
        protected double FirstTickerAsks { get; set; }
        [Browsable(false)]
        protected double SecondTickerBid { get; set; }
        [Browsable(false)]
        protected double SecondTickerAsk { get; set; }

        protected Ticker BottomTicker { get { return Tickers[0]; } }
        protected Ticker TopTicker { get { return Tickers[1]; } }

        public override List<StrategyValidationError> Validate() {
            List< StrategyValidationError> res = base.Validate();
            if(StrategyInfo.Tickers.Count != 2) {
                res.Add(new StrategyValidationError() { DataObject = this, Description = "Should be specified two tickers.", Value = StrategyInfo.Tickers.Count.ToString() });
            }
            return res;
        }

        public override void Assign(StrategyBase from) {
            base.Assign(from);
            StatisticalArbitrageStrategy s = from as StatisticalArbitrageStrategy;
            if(s == null)
                return;
            this.OpenSpread = s.OpenSpread;
            this.CloseSpread = s.CloseSpread;
        }

        protected StatisticalArbitrageHistoryItem LastItem { get; set; }
        public double CloseSpread { get; set; }
        public double OpenSpread { get; set; }

        public double MinProfitInBaseCurrency { get; set; } = 1;
        protected double BottomTickerBuyAmount { get; set; }
        protected double TopTickerSellAmount { get; set; }
        protected double PrevDeposit { get; set; }
        protected override void OnTickCore() {
            base.OnTickCore();

            if(BottomTicker.OrderBook.Asks.Count == 0 || TopTicker.OrderBook.Bids.Count == 0)
                return;

            double bottomAsk = BottomTicker.OrderBook.Asks[0].Value;
            double topBid = TopTicker.OrderBook.Bids[0].Value;

            if(LastItem == null || LastItem.BottomTickerAsk != bottomAsk || LastItem.TopTickerBid != topBid) {
                LastItem = new StatisticalArbitrageHistoryItem();
                LastItem.Time = DataProvider.CurrentTime;
                LastItem.BottomTickerAsk = bottomAsk;
                LastItem.TopTickerBid = topBid;
                LastItem.Earned = Earned;
                if(LastItem.Spread >= CloseSpread) {
                    LastItem.BestAmount = MaxActualDeposit / 2;
                    double totalFeeCore = 4 * Ticker.Fee / 100;
                    LastItem.TotalFee = totalFeeCore * (LastItem.BestAmount);
                    LastItem.Profit = (LastItem.Spread - CloseSpread) - LastItem.TotalFee;
                    if(LastItem.Profit < 0) {
                        LastItem.BestAmount = (LastItem.Spread - MinProfitInBaseCurrency) / totalFeeCore;
                        LastItem.TotalFee = totalFeeCore * (LastItem.BestAmount);
                        LastItem.Profit = (LastItem.Spread - CloseSpread) - LastItem.TotalFee;
                    }
                }
                StrategyData.Add(LastItem);
            }
            double spread = topBid - bottomAsk;
            if(State == CryptoMarketClient.Strategies.BuySellStrategyState.WaitingForBuy) {
                if(spread > OpenSpread) {
                    double depo = MaxActualDeposit / 2;
                    PrevDeposit = MaxActualDeposit;
                    TopTickerSellAmount = depo / topBid * (1.0 - Ticker.Fee / 100);
                    BottomTickerBuyAmount = depo / bottomAsk * (1.0 - Ticker.Fee / 100);
                    MarketBuy(BottomTicker, bottomAsk, BottomTickerBuyAmount);
                    MarketSell(TopTicker, topBid, TopTickerSellAmount);
                    MaxAllowedDeposit -= 0; // update max allowed deposits
                    State = CryptoMarketClient.Strategies.BuySellStrategyState.WaitingForSell;
                }
            }
            else {
                double bottomBid = BottomTicker.OrderBook.Bids[0].Value;
                double topAsk = TopTicker.OrderBook.Asks[0].Value;
                spread = topAsk - bottomBid;
                if(spread < CloseSpread) {
                    MarketBuy(BottomTicker, bottomBid, BottomTickerBuyAmount);
                    MarketSell(TopTicker, topAsk, TopTickerSellAmount);

                    double closeBottom = bottomBid * BottomTickerBuyAmount * (1.0 - Ticker.Fee / 100);
                    double closeTop = topAsk * TopTickerSellAmount * (1.0 - Ticker.Fee / 100); 
                    MaxAllowedDeposit = closeBottom + closeTop;
                    Earned += MaxAllowedDeposit - PrevDeposit;
                    State = CryptoMarketClient.Strategies.BuySellStrategyState.WaitingForBuy;
                }
            }
        }

        protected override void InitializeDataItems() {
            StrategyDataItemInfo info = TimeItem("Time");
            info.TimeUnit = StrategyDateTimeMeasureUnit.Millisecond;
            info.TimeUnitMeasureMultiplier = 1;
            DataItem("BottomTickerAsk").Color = Exchange.AskColor;
            DataItem("TopTickerBid").Color = Exchange.BidColor;
            DataItem("Spread").PanelIndex = 1;
            DataItem("BestAmount").Visibility = DataVisibility.Table;
            DataItem("TotalFee").Visibility = DataVisibility.Table;
            DataItem("Earned").PanelIndex = 2;
            StrategyDataItemInfo profit = DataItem("Profit");
            profit.PanelIndex = 1; profit.Color = System.Drawing.Color.Green;
        }

        protected override bool CheckLong() {
            return false;
        }

        protected override bool CheckShort() {
            return false;
        }
    }

    public class StatisticalArbitrageHistoryItem {
        public DateTime Time { get; set; }
        public double BottomTickerAsk { get; set; }
        public double TopTickerBid { get; set; }
        public double Spread { get { return TopTickerBid - BottomTickerAsk; } }
        public double TotalFee { get; set; }
        public double Profit { get; set; }
        public double BestAmount { get; set; }
        public double Earned { get; set; }
    }
}
