using Crypto.Core.Common;
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
        [InputParameter(10, 1000, 1)]
        public double MinSupportBreakValue { get; set; } = 200;
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
        protected virtual void OpenBreakUpPosition(double value) {
            OpenPositionInfo info = OpenLongPosition("BU", value, MaxAllowedDeposit * 0.2 / value, AllowTrailing);
            CombinedStrategyDataItem item = (CombinedStrategyDataItem)StrategyData.Last();
            if(info != null) item.Mark = info.Mark;
            if(AllowDAC) {
                InitializeDac(info);
            }
        }
        protected virtual void OpenPingPongPosition(double value, double resistance, double support) {
            OpenPositionInfo info = OpenLongPosition("PP", value, MaxAllowedDeposit * 0.2 / value, false);
            if(info == null)
                return;
            info.Tag2 = SRIndicator.Support.Last();
            double spread = resistance - support;
            info.CloseValue = resistance - spread * 0.1;
            CombinedStrategyDataItem item = (CombinedStrategyDataItem)StrategyData.Last();
            item.Mark = info.Mark;

            if(AllowDAC) {
                InitializeDac(info);
            }
        }
        protected void InitializeDac(OpenPositionInfo info) {
            double value = info.OpenValue;
            double dacStart = value * (100.0 - DACStartDownPercent) / 100.0;
            double dacEnd = SRIndicator2.GetLastMinSupport(5);
            if(dacEnd > dacStart)
                dacEnd = dacStart - (SRIndicator2.Resistance.Last().Value - SRIndicator2.Support.Last().Value);
            if((dacStart - dacEnd) / dacEnd < DACMinSpreadPercent)  // below one percent
                dacEnd = dacStart - dacStart * DACMinSpreadPercent / 100;

            info.AllowDAC = true;
            info.InitializeDAC(dacStart, dacEnd, info.Amount, 3);
        }
        protected override void ProcessTickerCore() {
            CombinedStrategyDataItem item = StrategyData.Count > 0 ? (CombinedStrategyDataItem)StrategyData.Last() : null;
            if(Ticker.CandleStickData.Count < SimulationStartItemsCount) /// need back data for simulation
                return;
            if(item == null || !string.IsNullOrEmpty(item.Mark)) // processed
                return;
            SRValue lr = SRIndicator.Resistance.Last();
            SRValue ls = SRIndicator.Support.Last();
            item.SRSpread = lr.Value - ls.Value;
            double lastAsk = Ticker.OrderBook.Asks[0].Value;
            double spreadFromResistance = (lastAsk - lr.Value);
            double spreadFromSupport = (ls.Value - lastAsk);

            item.BreakPercent = spreadFromResistance / item.SRSpread * 100;
            item.SpreadBaseResistance = spreadFromResistance;
            item.SRSpread = lr.Value - ls.Value;

            if(spreadFromResistance > MinResistanceBreakValue && spreadFromResistance < MinResistanceBreakValue + 100) { // to high 
                if(!AlreadyOpenedBreakUpPosition()) {
                    OpenBreakUpPosition(lastAsk);
                }
            }
            else if(spreadFromSupport > MinSupportBreakValue) { // to low
                if(!AlreadyOpenedBreakDownPosition()) {
                    OpenBreakDownPosition(lastAsk);
                }
            }
            else {
                SRValue lr2 = SRIndicator2.Resistance.Last();
                SRValue ls2 = SRIndicator2.Support.Last();
                double spread2 = lr2.Value - ls2.Value;
                double pc = (lastAsk - ls2.Value) / spread2;
                item.PercentChangeFromSupport = Math.Min(item.PercentChangeFromSupport, pc);
                if(spread2 > ls2.Value * 0.01 && pc > 0 && pc < 0.1 && ls.Length > 20) { // minimal spread
                    if(!AlreadyOpenedPingPongPosition()) {
                        OpenPingPongPosition(lastAsk, lr2.Value, ls2.Value);
                    }
                }
            }
            ProcessLongPositions();
            item.Earned = Earned;

            return;
        }

        private void OpenBreakDownPosition(double value) {
            OpenPositionInfo info = OpenLongPosition("RW", value, MaxAllowedDeposit * 0.2 / value, AllowTrailing);
            CombinedStrategyDataItem item = (CombinedStrategyDataItem)StrategyData.Last();
            if(info != null) item.Mark = info.Mark;
            if(AllowDAC)
                InitializeDac(info);
        }

        private bool AlreadyOpenedBreakDownPosition() {
            if(OpenedOrders.Count > 0 && OpenedOrders.Last().CurrentValue > OpenedOrders.Last().StopLoss)
                return true;
            foreach(OpenPositionInfo info in OpenedOrders) {
                if(info.Mark != "RW")
                    continue;
                SRValue res = (SRValue)info.Tag2;
                if(info.Type == OrderType.Buy && SRIndicator.BelongsSameSupportLevel(res))
                    return true;
            }
            return false;
        }
        protected override void InitializeDataItems() {
            base.InitializeDataItems();
            DataItem("Mark").Visibility = DataVisibility.Table;
            StrategyDataItemInfo info = DataItem("PercentChangeFromSupport");
            info.PanelName = "PcChangeFromSupport";
            info.Color = Color.FromArgb(0x40, Color.Green);

            StrategyDataItemInfo resValue = DataItem("Value"); resValue.BindingSource = "SRIndicator2.Resistance"; resValue.Color = Color.FromArgb(0x40, Color.Magenta); resValue.ChartType = ChartType.StepLine;
            StrategyDataItemInfo supValue = DataItem("Value"); supValue.BindingSource = "SRIndicator2.Support"; supValue.Color = Color.FromArgb(0x40, Color.Green); supValue.ChartType = ChartType.StepLine;
            AnnotationItem("Mark", null, Color.Green, "Value").AnnotationText = "{Mark}-{Index}";
            DataItemInfos.Remove(DataItemInfos.FirstOrDefault(i => i.FieldName == "Break"));

            StrategyDataItemInfo bp = DataItem("SpreadBaseResistance"); bp.Color = Color.Green; bp.ChartType = ChartType.Bar; bp.PanelName = "BreakPercent";
            StrategyDataItemInfo spread = DataItem("SRSpread"); spread.Color = Color.Red; spread.ChartType = ChartType.Bar; spread.PanelName = "BreakPercent";
            StrategyDataItemInfo.HistogrammItem(DataItemInfos, 1, "SpreadBaseResistance", Color.Green).ChartType = ChartType.Bar;
        }

        protected bool AlreadyOpenedPingPongPosition() {
            if(OpenedOrders.Count > 0 && OpenedOrders.Last().CurrentValue > OpenedOrders.Last().StopLoss)
                return true;
            CombinedStrategyDataItem last = (CombinedStrategyDataItem)StrategyData.Last();
            foreach(OpenPositionInfo info in OpenedOrders) {
                if(info.Mark != "PP")
                    continue;
                SRValue res = (SRValue)info.Tag2;
                if(info.Type == OrderType.Buy && (SRIndicator2.BelongsSameSupportLevel(res) || last.Index - info.DataItemIndex < 5))
                    return true;
            }
            return false;
        }

        protected bool AlreadyOpenedBreakUpPosition() {
            if(OpenedOrders.Count > 0 && OpenedOrders.Last().CurrentValue > OpenedOrders.Last().StopLoss)
                return true;
            CombinedStrategyDataItem last = (CombinedStrategyDataItem)StrategyData.Last();
            foreach(OpenPositionInfo info in OpenedOrders) {
                if(info.Mark != "BU")
                    continue;
                SRValue res = (SRValue)info.Tag2;
                if(info.Type == OrderType.Buy && SRIndicator.BelongsSameResistanceLevel(res))
                    return true;
            }
            return false;
        }

        protected override void OnOpenLongPosition(OpenPositionInfo info) {
            info.Tag = StrategyData.Last();
            if(info.Mark == "BU" || info.Mark == "PP") {
                info.Tag2 = SRIndicator.Resistance.Last();
            }
            else if(info.Mark == "RW") {
                info.Tag2 = SRIndicator.Support.Last();
            }
            CombinedStrategyDataItem item = (CombinedStrategyDataItem)StrategyData.Last();
            item.Value = info.CurrentValue;
        }
        
        protected override CombinedStrategyDataItem AddStrategyData() {
            CombinedStrategyDataItem item = base.AddStrategyData();
            if(item != null && SRIndicator2.Support.Count > 0)
                item.PercentChangeFromSupport = Ticker.OrderBook.Asks[0].Value - SRIndicator2.Support.Last().Value; // big value
            return item;
        }

        protected override void CloseLongPosition(OpenPositionInfo info) {
            TradingResult res = MarketSell(Ticker.OrderBook.Bids[0].Value, info.Amount);
            if(res != null) {
                double earned = res.Total - CalcFee(res.Total);
                MaxAllowedDeposit += earned;
                info.UpdateCurrentValue(DataProvider.CurrentTime, res.Value);
                info.Earned += earned;
                info.Amount -= res.Amount;
                info.Total -= res.Total;
                info.CloseValue = res.Value;
                CombinedStrategyDataItem item = (CombinedStrategyDataItem)info.Tag;
                item.Closed = true;
                item.CloseLength = ((CombinedStrategyDataItem)StrategyData.Last()).Index - item.Index;
                CombinedStrategyDataItem last = (CombinedStrategyDataItem)StrategyData.Last();
                if(info.Amount < 0.000001) {
                    OpenedOrders.Remove(info);
                    last.ClosedPositions.Add(info);
                    info.CloseTime = DataProvider.CurrentTime;
                    Earned += info.Earned - info.Spent;
                }
                last.ClosedOrder = true;
                last.Value = Ticker.OrderBook.Bids[0].Value;
                item.Profit = earned - info.Spent;
                last.Mark = "Close " + info.Mark;
            }
        }
    }
}
