using Crypto.Core.Helpers;
using Crypto.Core.Indicators;
using CryptoMarketClient.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        protected SupportResistanceIndicator SRIndicator2 { get; private set; }
        public override bool Start() {
            bool res = base.Start();
            if(res)
                SRIndicator2 = new SupportResistanceIndicator() { Ticker = Ticker, Range = Range, ClasterizationRange = ClasterizationRange, ThresoldPerc = ThresoldPerc * 3 };
            return res;
        }

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
            SRValue ls = SRIndicator.Support.Last();
            item.SRSpread = lr.Value - ls.Value;
            double lastAsk = Ticker.OrderBook.Asks[0].Value;
            double spread = (lastAsk - lr.Value);
            item.BreakPercent = spread / item.SRSpread * 100;
            
            if(spread > MinResistanceBreakValue && spread < MinResistanceBreakValue + 100) { // to high 
                if(!AlreadyOpenedPosition()) {
                    OpenPositionInfo info = OpenLongPosition(lastAsk, MaxAllowedDeposit * 0.2 / lastAsk, true);
                    if(info != null) item.Mark = info.Mark = "Break";
                }
            }
            else {
                SRValue lr2 = SRIndicator2.Resistance.Last();
                SRValue ls2 = SRIndicator2.Support.Last();
                double spread2 = lr2.Value - ls2.Value;
                double pc = (lastAsk - ls2.Value) / spread2;
                item.PercentChangeFromSupport = Math.Min(item.PercentChangeFromSupport, pc);

                if(spread2 > ls2.Value * 0.01 && pc > 0 && pc < 0.1 && ls.Length > 20) { // minimal spread
                    if(!AlreadyOpenedPosition()) {
                        OpenPositionInfo info = OpenLongPosition(lastAsk, MaxAllowedDeposit * 0.2 / lastAsk, false);
                        if(info != null) {
                            info.Tag2 = SRIndicator.Support.Last();
                            info.CloseValue = lr2.Value - spread * 0.1;
                            item.Mark = info.Mark = "PingPong";
                        }
                    }
                }
            }
            CheckCloseLongPositions();
            item.Earned = Earned;

            return;
        }

        protected override void InitializeDataItems() {
            base.InitializeDataItems();
            DataItem("Mark").Visibility = DataVisibility.Table;
            StrategyDataItemInfo info = DataItem("PercentChangeFromSupport");
            info.PanelIndex = NextDataItemPanel();
            info.Color = Color.FromArgb(0x40, Color.Green);

            StrategyDataItemInfo resValue = DataItem("Value"); resValue.BindingSource = "SRIndicator2.Resistance"; resValue.Color = Color.FromArgb(0x40, Color.Magenta); resValue.ChartType = ChartType.StepLine;
            StrategyDataItemInfo supValue = DataItem("Value"); supValue.BindingSource = "SRIndicator2.Support"; supValue.Color = Color.FromArgb(0x40, Color.Green); supValue.ChartType = ChartType.StepLine;
        }

        public int NextDataItemPanel() {
            return DataItemInfos.Max(i => i.PanelIndex) + 1;
        }

        protected bool AlreadyOpenedPosition() {
            if(OpenedOrders.Count > 0 && OpenedOrders.Last().CurrentValue > OpenedOrders.Last().StopLoss)
                return true;
            foreach(OpenPositionInfo info in OpenedOrders) {
                if(info.Mark == "PingPong") {
                    SRValue res = (SRValue)info.Tag2;
                    if(info.Type == OrderType.Buy && SRIndicator2.BelongsSameSupportLevel(res))
                        return true;
                }
                else {
                    SRValue res = (SRValue)info.Tag2;
                    if(info.Type == OrderType.Buy && SRIndicator.BelongsSameResistanceLevel(res))
                        return true;
                }
            }
            return false;
        }

        protected override void OnOpenLongPosition(OpenPositionInfo info) {
            info.Tag = StrategyData.Last();
            info.Tag2 = SRIndicator.Resistance.Last();
            //info.AllowTrailing = true;
            RedWaterfallDataItem item = (RedWaterfallDataItem)StrategyData.Last();
            item.Break = true;
            item.Value = info.CurrentValue;
        }
        
        protected override RedWaterfallDataItem AddStrategyData() {
            RedWaterfallDataItem item = base.AddStrategyData();
            if(item != null)
                item.PercentChangeFromSupport = Ticker.OrderBook.Asks[0].Value - SRIndicator.Support.Last().Value; // big value
            return item;
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
                item.Profit = earned - info.Spent;
            }
        }
    }
}
