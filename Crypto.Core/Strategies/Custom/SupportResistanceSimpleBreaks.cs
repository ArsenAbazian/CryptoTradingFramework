using Crypto.Core.Helpers;
using Crypto.Core.Indicators;
using CryptoMarketClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Strategies.Custom {
    public class SupportResistanceSimpleBreaks : SupportResistanceBasedStrategy {

        public SupportResistanceSimpleBreaks() {
            //Indicators.Add(new MacdIndicator() { DataSourcePath = "Ticker", UseOwnChart = true });
        }

        protected SRValue LastResistance { get; private set; }
        protected SRValue LastSupport { get; private set; }
        [InputParameter(10, 300, 1)]
        public double MinSupportBreakPercent { get; set; } = 15;
        [InputParameter(1, 10, 1)]
        public double MinResistanceBreakPercent { get; set; } = 5;
        [InputParameter(0, 1000, 1)]
        public double MinResistanceBreakValue { get; set; } = 70;
        [InputParameter(0.01, 2, 0.01)]
        public double MinResistanceBreakSpreadPercent { get; set; } = 0.5;
        
        public override void Assign(StrategyBase from) {
            base.Assign(from);
            SupportResistanceSimpleBreaks ss = (SupportResistanceSimpleBreaks)from;
            MinSupportBreakPercent = ss.MinSupportBreakPercent;
            MinResistanceBreakPercent = ss.MinResistanceBreakPercent;
            MinProfitPercent = ss.MinProfitPercent;
        }
        protected override void ProcessTickerCore() {
            RedWaterfallDataItem item = StrategyData.Count > 0 ? (RedWaterfallDataItem)StrategyData.Last() : null;
            if(Ticker.CandleStickData.Count < SimulationStartItemsCount) /// need back data for simulation
                return;
            if(item == null || item.Break) // processed
                return;
            SRValue lr = SRIndicator.Resistance.Last();
            item.SRSpread = lr.Value - SRIndicator.Support.Last().Value;
            double lastAsk = Ticker.OrderBook.Asks[0].Value;
            double spread = (lastAsk - lr.Value);
            item.BreakPercent = spread / item.SRSpread * 100;
            if(spread > MinResistanceBreakValue && spread < MinResistanceBreakValue + 100) { // to high 
                if(!AlreadyOpenedPosition()) {
                    item.Break = true;
                    item.Value = lastAsk;
                    OpenLongPosition(Ticker.OrderBook.Asks[0].Value);
                }
            }
            CheckCloseLongPositions();
            item.Earned = Earned;

            return;
        }

        protected bool AlreadyOpenedPosition() {
            if(OpenedOrders.Count > 0 && OpenedOrders.Last().CurrentValue > OpenedOrders.Last().StopLoss)
                return true;
            foreach(OpenPositionInfo info in OpenedOrders) {
                SRValue res = (SRValue)info.Tag2;
                if(info.Type == OrderType.Buy && SRIndicator.BelongsSameResistanceLevel(res))
                    return true;
            }
            return false;
        }

        protected override void OpenLongPosition(double value) {
            TradingResult res = MarketBuy(value, MaxAllowedDeposit * 0.2 / value); // 10 percent per deal
            RedWaterfallDataItem last = (RedWaterfallDataItem)StrategyData.Last();
            if(res != null) {
                double spent = res.Total + CalcFee(res.Total);
                OpenedOrders.Add(new OpenPositionInfo() {
                    Type = OrderType.Buy,
                    AllowTrailing = true,
                    Spent = spent,
                    StopLossPercent = TrailingStopLossPercent,
                    OpenValue = res.Value,
                    Amount = res.Amount,
                    Total = res.Total,
                    CloseValue = value + value * MinProfitPercent / 100,
                    Tag = StrategyData.Last(),
                    Tag2 = SRIndicator.Resistance.Last()
                });
                OpenedOrders.Last().CurrentValue = res.Value;
                MaxAllowedDeposit -= spent;
            }
        }

        protected override void CloseLongPosition(OpenPositionInfo info) {
            TradingResult res = MarketSell(Ticker.OrderBook.Bids[0].Value, info.Amount);
            if(res != null) {
                double earned = res.Total - CalcFee(res.Total);
                MaxAllowedDeposit += earned;
                info.Earned += earned;
                info.Amount -= res.Amount;
                info.Total -= res.Total;
                RedWaterfallDataItem item = (RedWaterfallDataItem)info.Tag;
                item.Closed = true;
                item.CloseLength = ((RedWaterfallDataItem)StrategyData.Last()).Index - item.Index;
                RedWaterfallDataItem last = (RedWaterfallDataItem)StrategyData.Last();
                if(info.Amount < 0.000001) {
                    OpenedOrders.Remove(info);
                    Earned += info.Earned - info.Spent;
                }
                last.ClosedOrder = true;
                last.Value = Ticker.OrderBook.Bids[0].Value;
            }
        }
    }
}
