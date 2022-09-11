using Crypto.Core.Common;
using Crypto.Core.Helpers;
using Crypto.Core.Indicators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Crypto.Core.Strategies.Custom {
    public class TaSimpleStrategy : CustomTickerStrategy {

        public TaSimpleStrategy() {
        }

        public override string TypeName => "Simple Technical Analysis Strategy";

        [Browsable(false)]
        public override int CandleStickIntervalMin { get; set; }
        
        [Category("BreakUp")]
        [StrategyProperty("BreakUp")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BreakUpSettings BreakUpSettings { get; set; } = new BreakUpSettings();
        [Category("BreakDown")]
        [StrategyProperty("BreakDown")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BreakDownSettings BreakDownSettings { get; set; } = new BreakDownSettings();
        [Category("PingPong")]
        [StrategyProperty("PingPong")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PingPongSettings PingPongSettings { get; set; } = new PingPongSettings();

        public int Range { get; set; } = 3;
        public double ThresoldPerc { get; set; } = 0.6;

        [XmlIgnore]
        protected SupportResistanceIndicator SRIndicator { get; private set; }
        [XmlIgnore]
        protected SupportResistanceIndicator SRIndicator2 { get; private set; }

        public override string HelpWebPage => "https://github.com/ArsenAbazian/CryptoTradingFramework/wiki/TaSimpleStrategy";

        public override void Assign(StrategyBase from) {
            base.Assign(from);
            TaSimpleStrategy ss = from as TaSimpleStrategy;
            if(ss == null)
                return;

            BreakUpSettings = ss.BreakUpSettings.Clone();
            BreakDownSettings = ss.BreakDownSettings.Clone();
            PingPongSettings = ss.PingPongSettings.Clone();
        }

        protected override void OnTickCore() {
            ProcessTicker(Ticker);
        }

        public override List<StrategyValidationError> Validate() {
            List<StrategyValidationError> res = base.Validate();
            if(StrategyInfo.Tickers.Count != 1) {
                res.Add(new StrategyValidationError() { DataObject = this, PropertyName = "Tickers", Description = "Right now only one ticker is suported per strategy", Value = StrategyInfo.Tickers.Count.ToString() });
            }
            return res;
        }

        public override bool Start() {
            bool res = base.Start();
            if(res) {
                SRIndicator = new SupportResistanceIndicator() { Ticker = Ticker, Range = Range, ThresoldPerc = ThresoldPerc };
                SRIndicator2 = new SupportResistanceIndicator() { Ticker = Ticker, Range = Range,ThresoldPerc = ThresoldPerc * 3 };
                SRIndicator.Calculate();
                SRIndicator2.Calculate();
                for(int i = 0; i < Ticker.CandleStickData.Count - 1; i++) { 
                    AddStrategyData(i);
                    LastCount = Ticker.CandleStickData.Count;
                }
            }
            return res;
        }

        
        protected virtual void OpenBreakUpPosition(double value) {
            OpenPositionInfo info = OpenLongPosition(Ticker, "BU", value, 
                BreakUpSettings.GetAllowedDeposit(MaxAllowedDeposit) / value, BreakUpSettings.AllowTrailing, 
                BreakUpSettings.TrailingStopLossPc, 
                BreakUpSettings.MinProfitPc);
            if(info == null)
                return;
            if(BreakUpSettings.AllowDCA) {
                InitializeDac(info, BreakUpSettings);
            }
        }
        protected virtual void OpenPingPongPosition(double value, double resistance, double support) {
            OpenPositionInfo info = OpenLongPosition(Ticker, "PP", value, 
                PingPongSettings.GetAllowedDeposit(MaxAllowedDeposit) / value, 
                PingPongSettings.AllowTrailing,
                PingPongSettings.TrailingStopLossPc,
                PingPongSettings.MinProfitPc);
            if(info == null)
                return;
            double spread = resistance - support;
            info.CloseValue = resistance - spread * 0.1;
            if(PingPongSettings.AllowDCA) {
                InitializeDac(info, PingPongSettings);
            }
        }
        protected void InitializeBreakDownDac(OpenPositionInfo info, TaSettingsBase settings) {
            double value = info.OpenValue;
            double dacStart = value * (100.0 - settings.DCAStartPercent) * 0.01;
            double dacEnd = SRIndicator2.GetLastMinSupport(5);
            dacEnd = SRIndicator2.GetSupportBelow(dacEnd);
            if(dacEnd == 0 || dacEnd > dacStart)
                dacEnd = dacStart - (SRIndicator2.Resistance.Last().Value - SRIndicator2.Support.Last().Value);
            if(dacEnd > value * (100 - settings.DCAEndPercent) * 0.01)
                dacEnd = value * (100 - settings.DCAEndPercent) * 0.01;

            info.AllowDCA = true;
            info.InitializeDCA(dacStart, dacEnd, info.Amount, 3, settings.DCAStartAmountFraction, settings.DCAEndAmountFraction);
        }
        protected void InitializeDac(OpenPositionInfo info, TaSettingsBase settings) {
            double value = info.OpenValue;
            double dacStart = value * (100.0 - settings.DCAStartPercent) * 0.01;
            double dacEnd = SRIndicator2.GetLastMinSupport(5);
            if(dacEnd > dacStart)
                dacEnd = dacStart - (SRIndicator2.Resistance.Last().Value - SRIndicator2.Support.Last().Value);
            if(dacEnd > value * (100 - settings.DCAEndPercent) * 0.01)
                dacEnd = value * (100 - settings.DCAEndPercent) * 0.01;

            info.AllowDCA = true;
            info.InitializeDCA(dacStart, dacEnd, info.Amount, 3, settings.DCAStartAmountFraction, settings.DCAEndAmountFraction);
        }
        protected virtual void UpdateCurrentLoss() {
            if(StrategyData.Count > 0) {
                CombinedStrategyDataItem last = (CombinedStrategyDataItem)StrategyData.Last();
                last.CurrentLoss = Math.Min(last.CurrentLoss, OpenedOrders.Sum(o => o.CurrentLoss));
            }
        }
        protected int LastCount { get; set; } = -1;
        protected virtual void ProcessTicker(Ticker ticker) {
            UpdateCurrentLoss();
            if(Ticker.CandleStickData.Count > 1 && LastCount != Ticker.CandleStickData.Count) {
                LastCount = Ticker.CandleStickData.Count;
                AddStrategyData();
            }
            ProcessTickerCore();
        }
        protected int StrategyStartItemsCount { get { return 100; } }
        protected virtual void ProcessTickerCore() {
            if(Ticker.CandleStickData.Count < StrategyStartItemsCount) /// need back data for simulation
                return;
            if(Ticker.OrderBook.IsDirty || Ticker.OrderBook.Asks.Count == 0)
                return;
            CombinedStrategyDataItem item = (CombinedStrategyDataItem)StrategyData.Last();
            if(!string.IsNullOrEmpty(item.Mark)) // processed
                return;

            if(SRIndicator.Resistance.Count == 0 || SRIndicator.Support.Count == 0 ||
               SRIndicator2.Resistance.Count == 0 || SRIndicator2.Support.Count == 0)
                return;

            SRValue lr = SRIndicator.Resistance.Last();
            SRValue ls = SRIndicator.Support.Last();
            //double spread = lr.Value - ls.Value;
            double lastAsk = Ticker.OrderBook.Asks[0].Value;
            double spreadFromResistance = lastAsk - SRIndicator2.Resistance.Last().Value; //(lastAsk - lr.Value);
            double spreadFromSupport = (ls.Value - lastAsk);

            //item.BreakPercent = spreadFromResistance / item.SRSpread * 100;
            item.SpreadBaseResistance = spreadFromResistance;
            item.SRSpread = (SRIndicator2.Resistance.Last().Value - SRIndicator.Support.Last().Value) / SRIndicator2.Support.Last().Value * 100;

            if(BreakUpSettings.Enable && spreadFromResistance > BreakUpSettings.MinBreakValue && spreadFromResistance < BreakUpSettings.MaxBreakValue) { // to high 
                if(!AlreadyOpenedBreakUpPosition())
                    OpenBreakUpPosition(lastAsk);
                //EnableTrailingForPingPong();
            }
            else if(BreakDownSettings.Enable && spreadFromSupport > BreakDownSettings.MinSupportBreakValue) { // to low
                double probableBottom = GetProbableBottom(lastAsk);
                double openValue = 0;
                if(probableBottom != 0)
                    openValue = probableBottom + (ls.Value - probableBottom) * 0.1; // 10% addition
                if(openValue == 0 || openValue >= lastAsk) { 
                    probableBottom = 0;
                    openValue = lastAsk;
                }
                if(!AlreadyOpenedBreakDownPosition(openValue))
                    OpenBreakDownPosition(openValue, ls.Value, probableBottom != 0);
            }
            else {
                SRValue lr2 = SRIndicator2.Resistance.Last();
                SRValue ls2 = SRIndicator2.Support.Last();
                double spread2 = lr2.Value - ls2.Value;
                double pc = (lastAsk - ls2.Value) / spread2;
                if(PingPongSettings.Enable && 
                    item.SRSpread > PingPongSettings.MinimalPriceRangeInPc && 
                    pc > 0 && pc < 0.1 && ls.Length > PingPongSettings.SupportLevelLength) { // minimal spread
                    if(!AlreadyOpenedPingPongPosition()) {
                        OpenPingPongPosition(lastAsk, lr2.Value, ls2.Value);
                    }
                }
            }

            ProcessDelayedPositions();
            ProcessLongPositions();
            item.Earned = Earned;

            return;
        }

        //private void EnableTrailingForPingPong() {
        //    foreach(var item in OpenedOrders) {
        //        if(item.Mark == "PP")
        //            item.AllowTrailing = true;
        //    }
        //}

        protected double GetProbableBottom(double lastAsk) {
            double foundResistance = 0;
            for(int i = SRIndicator2.Resistance.Count - 1; i > 0; i--) {
                double spread = lastAsk - SRIndicator2.Resistance[i].Value;
                if(spread > 0) {
                    foundResistance = SRIndicator2.Resistance[i].Value;
                    break;
                }
            }
            double foundSupport = 0;
            for(int i = SRIndicator2.Support.Count - 1; i > 0; i--) {
                double spread = lastAsk - SRIndicator2.Support[i].Value;
                if(spread > 0) {
                    foundSupport = SRIndicator2.Support[i].Value;
                    break;
                }
            }
            return Math.Max(foundSupport, foundResistance);
        }

        protected override DelayedPositionInfo AddDelayedPosition(string mark, double value, double amount, double closeValue, int liveTimeLength) {
            DelayedPositionInfo res = base.AddDelayedPosition(mark, value, amount, closeValue, liveTimeLength);
            if(res != null) {
                CombinedStrategyDataItem last = (CombinedStrategyDataItem)StrategyData.Last();
                //last.Mark = mark + " DL";
                last.Value = value;
            }
            return res;
        }

        private void OpenBreakDownPosition(double value, double closeValue, bool delayed) {
            double amount = BreakDownSettings.GetAllowedDeposit(MaxAllowedDeposit) / value;
            if(delayed) {
                AddDelayedPosition("RW", value, amount, closeValue, BreakDownSettings.DelayedPeriodLength);
                return;
            }
            OpenPositionInfo info = OpenLongPosition(Ticker, "RW", value, amount, BreakDownSettings.AllowTrailing, BreakDownSettings.TrailingStopLossPc, BreakDownSettings.MinProfitPc);
            if(info == null)
                return;
            info.CloseValue = closeValue;
            if(BreakDownSettings.AllowDCA)
                InitializeDac(info, BreakDownSettings);
        }

        protected override void OpenDelayedPosition(DelayedPositionInfo info) {
            base.OpenDelayedPosition(info);
            if(info.Mark == "RW") {
                OpenBreakDownPosition(Ticker.OrderBook.Asks[0].Value, info.CloseValue, false);
            }
        }

        private bool AlreadyOpenedBreakDownPosition(double value) {
            //if(OpenedOrders.Count > 0 && OpenedOrders.Last().CurrentValue > OpenedOrders.Last().StopLoss)
            //    return true;
            foreach(DelayedPositionInfo info in DelayedPositions) {
                if(info.Mark == "RW" && Math.Abs(info.Price - value) / value < 0.005)
                    return true;
            }
            foreach(OpenPositionInfo info in OpenedOrders) {
                if(info.Mark != "RW")
                    continue;
                SRValue res = SRIndicator.FindSupportByTime(info.CandlestickTime);
                if(res != null && info.Type == OrderType.Buy && SRIndicator.BelongsSameSupportLevel(res))
                    return true;
            }
            return false;
        }

        protected virtual CombinedStrategyDataItem AddStrategyData(int candleStickIndex) {
            CombinedStrategyDataItem item = new CombinedStrategyDataItem();
            StrategyData.Add(item);

            item.Index = StrategyData.Count;
            item.Earned = Earned;
            item.AvailableDeposit = MaxAllowedDeposit;
            item.Candle = Ticker.CandleStickData[candleStickIndex];
            return item;
        }

        protected virtual CombinedStrategyDataItem AddStrategyData() {
            return AddStrategyData(Ticker.CandleStickData.Count - 2);
        }

        protected override void InitializeDataItems() {
            base.InitializeDataItems();

            DataItem("Index").Visibility = DataVisibility.Table;
            TimeItem("Time").LabelPattern = "hh:mm";
            CandleStickItem().Name = "KLine";

            //StrategyDataItemInfo atr = DataItem("Atr"); atr.Name = "Atr Indicator"; atr.Color = System.Drawing.Color.Red; atr.ChartType = ChartType.Line; atr.PanelIndex = 1;
            StrategyDataItemInfo resValue = DataItem("Value"); resValue.Name = "L1 Resistance"; resValue.BindingSource = "SRIndicator.Resistance"; resValue.Color = Color.FromArgb(0x80, Color.Red); resValue.ChartType = ChartType.StepLine; resValue.Name = "Resistance Fast";
            StrategyDataItemInfo supValue = DataItem("Value"); supValue.Name = "L1 Support"; supValue.BindingSource = "SRIndicator.Support"; supValue.Color = Color.FromArgb(0x80, Color.Blue); supValue.ChartType = ChartType.StepLine; supValue.Name = "Support Fast";

            //AnnotationItem("Support", "Support", System.Drawing.Color.Green, "SRLevel");
            //AnnotationItem("Resistance", "Resistance", System.Drawing.Color.Red, "SRLevel");
            //StrategyDataItemInfo br = AnnotationItem("Break", "Break", System.Drawing.Color.Blue, "Value"); br.Visibility = DataVisibility.Both; br.AnnotationText = "Br"; //"Br={BreakPercent:0.00} Closed={Closed} CloseStickCount={CloseLength}";
            //StrategyDataItemInfo bp = DataItem("BreakPercent"); bp.Color = Color.Green; bp.ChartType = ChartType.Bar; bp.PanelName = "BreakPercent";

            //StrategyDataItemInfo cl = AnnotationItem("ClosedOrder", "Closed", System.Drawing.Color.Green, "Value"); cl.Visibility = DataVisibility.Both; cl.AnnotationText = "Cl"; //"Br={BreakPercent:0.00} Closed={Closed} CloseStickCount={CloseLength}";


            //StrategyDataItemInfo sc = DataItem("SupportChange", Color.FromArgb(0x40, Color.Green), 1); sc.PanelIndex = 4; sc.ChartType = ChartType.Area;
            //StrategyDataItemInfo rc = DataItem("ResistanceChange", Color.FromArgb(0x40, Color.Red), 1); rc.PanelIndex = 4; rc.ChartType = ChartType.Area;
            //StrategyDataItemInfo bp2 = DataItem("BreakPercent2"); bp2.Color = Color.FromArgb(0x20, Color.Green);  bp2.ChartType = ChartType.Area; bp2.PanelIndex = 5;

            //DataItem("Closed").Visibility = DataVisibility.Table;
            //DataItem("CloseLength").Visibility = DataVisibility.Table;

            //br = AnnotationItem("Break", "Break", System.Drawing.Color.Blue, "Atr"); br.PanelIndex = 1; br.Visibility = DataVisibility.Chart; br.AnnotationText = "Br"; //"Br={BreakPercent:0.00} Closed={Closed} CloseStickCount={CloseLength}";

            StrategyDataItemInfo sp = DataItem("SRSpread"); sp.Color = Color.FromArgb(0x20, Color.Green); sp.ChartType = ChartType.Area; sp.PanelName = "Price Range"; sp.PanelVisible = false;

            DataItem("CurrentLoss", Color.Red).Visibility = DataVisibility.Table;
            DataItem("Profit", Color.Green).Visibility = DataVisibility.Table;
            var ear = DataItem("Earned", Color.FromArgb(0x70, Color.LightBlue)); ear.PanelName = "Deposit"; ear.ChartType = ChartType.Area;
            var loss = DataItem("CurrentLoss", Color.FromArgb(0x70, Color.Pink)); loss.PanelName = "Deposit"; loss.ChartType = ChartType.Area; //loss.Reversed = true;
            var dep = DataItem("AvailableDeposit", Color.FromArgb(0x70, Color.Green)); dep.PanelName = "Deposit"; dep.GraphWidth = 1; dep.ChartType = ChartType.Line; //loss.Reversed = true;

            var mark = DataItem("Mark"); mark.Visibility = DataVisibility.Table; mark.Type = DataType.ListInString;
            //StrategyDataItemInfo info = DataItem("PercentChangeFromSupport");
            //info.PanelName = "PcChangeFromSupport";
            //info.Color = Color.FromArgb(0x40, Color.Green);

            resValue = DataItem("Value"); resValue.GraphWidth = 2; resValue.Name = "L2 Resistance"; resValue.BindingSource = "SRIndicator2.Resistance"; resValue.Color = Color.FromArgb(0x80, Color.Magenta); resValue.ChartType = ChartType.StepLine; resValue.Name = "Resistance Slow";
            supValue = DataItem("Value"); supValue.GraphWidth = 2; supValue.Name = "L2 Support"; supValue.BindingSource = "SRIndicator2.Support"; supValue.Color = Color.FromArgb(0x80, Color.Green); supValue.ChartType = ChartType.StepLine; supValue.Name = "Support Slow";
            mark = AnnotationItem("Mark", null, Color.Green, "Value"); mark.AnnotationText = "{Mark}"; mark.Type = DataType.ListInString;
            //DataItemInfos.Remove(DataItemInfos.FirstOrDefault(i => i.FieldName == "Break"));

            //StrategyDataItemInfo bp = DataItem("SpreadBaseResistance"); bp.Color = Color.Green; bp.ChartType = ChartType.Bar; bp.PanelName = "BreakPercent";
            //StrategyDataItemInfo spread = DataItem("SRSpread"); spread.Color = Color.Red; spread.ChartType = ChartType.Bar; spread.PanelName = "BreakPercent";
            //StrategyDataItemInfo.HistogrammItem(DataItemInfos, 1, "SpreadBaseResistance", Color.Green).ChartType = ChartType.Bar;
        }

        protected bool AlreadyOpenedPingPongPosition() {
            if(OpenedOrders.Count > 0 && OpenedOrders.Last().CurrentValue > OpenedOrders.Last().StopLoss)
                return true;
            CombinedStrategyDataItem last = (CombinedStrategyDataItem)StrategyData.Last();
            foreach(OpenPositionInfo info in OpenedOrders) {
                if(info.Mark != "PP")
                    continue;
                SRValue res = SRIndicator2.FindSupportByTime(info.CandlestickTime);
                if(res != null && info.Type == OrderType.Buy && (SRIndicator2.BelongsSameSupportLevel(res) || last.Index - info.DataItemIndex < 5))
                    return true;
            }
            return false;
        }

        protected bool AlreadyOpenedBreakUpPosition() {
            //if(OpenedOrders.Count > 0 && OpenedOrders.Last().CurrentValue > OpenedOrders.Last().StopLoss)
            //    return true;
            CombinedStrategyDataItem last = (CombinedStrategyDataItem)StrategyData.Last();
            foreach(OpenPositionInfo info in OpenedOrders) {
                if(info.Mark == "BU" && last.Index - info.DataItemIndex < 30)
                    return true;
                //if(info.Mark != "BU")
                //    continue;
                //SRValue res = (SRValue)info.Tag2;
                //if(info.Type == OrderType.Buy && SRIndicator.BelongsSameResistanceLevel(res))
                //    return true;
            }
            return false;
        }

        protected override void OnOpenLongPosition(OpenPositionInfo info) {
            CombinedStrategyDataItem item = (CombinedStrategyDataItem)StrategyData.Last();
            info.CandlestickTime = item.Time;
            item.Value = info.CurrentValue;
        }

        protected TaSettingsBase GetSettings(OpenPositionInfo info) {
            if(info.Mark == "BU")
                return BreakUpSettings;
            if(info.Mark == "RW")
                return BreakDownSettings;
            if(info.Mark == "PP")
                return PingPongSettings;
            return null;
        }

        protected override bool ProcessDCA(OpenPositionInfo info) {
            TaSettingsBase st = GetSettings(info);
            if(st != null && st.EnableStopLoss)
                return false;
            return base.ProcessDCA(info);
        }

        protected override bool ShouldCloseLongPosition(OpenPositionInfo info) {
            if(base.ShouldCloseLongPosition(info))
                return true;
            TaSettingsBase st = GetSettings(info);
            if(st != null && st.EnableStopLoss && info.CurrentChangeInPc < -st.StopLossPc)
                return true;
            //if(info.Mark == "BU") {
            //    // if going down
            //    double pcUp = (info.CurrentValue - info.OpenValue) / info.OpenValue * 100;
            //    double pcDown = (info.MaxValue - info.CurrentValue) / info.CurrentValue * 100;
            //    if(pcDown > pcUp && pcDown > 1)
            //        return true;
            //}
            return false;
        }

        public override void CloseLongPosition(OpenPositionInfo info) {
            TradingResult res = MarketSell(Ticker, Ticker.OrderBook.Bids[0].Value, info.Amount);
            if(res != null) {
                double earned = res.Total - CalcFee(Ticker, res.Total);
                MaxAllowedDeposit += earned;
                info.UpdateCurrentValue(DataProvider.CurrentTime, res.Value);
                info.Earned += earned;
                info.Amount -= res.Amount;
                info.Total -= res.Total;
                info.CloseValue = res.Value;
                CombinedStrategyDataItem item = (CombinedStrategyDataItem)StrategyData.FirstOrDefault(i => ((CombinedStrategyDataItem)i).Time == info.CandlestickTime);
                if(item != null) {
                    item.Closed = true;
                    item.CloseLength = ((CombinedStrategyDataItem)StrategyData.Last()).Index - item.Index;
                }
                CombinedStrategyDataItem last = (CombinedStrategyDataItem)StrategyData.Last();
                if(info.Amount < 0.000001) {
                    OpenedOrders.Remove(info);
                    last.ClosedPositions.Add(info);
                    info.CloseTime = DataProvider.CurrentTime;
                    Earned += info.Earned - info.Spent;
                }
                last.ClosedOrder = true;
                last.Value = Ticker.OrderBook.Bids[0].Value;
                if(item != null)
                    item.Profit = earned - info.Spent;
                last.AddMark("Close " + info.Mark);
            }
        }
    }

    public class TaSettingsBase {
        public bool Enable { get; set; }
        public bool AllowDCA { get; set; }
        public bool AllowTrailing { get; set; }
        public double AllowedDepositPc { get; set; } = 20;
        public double TrailingStopLossPc { get; set; } = 5;
        public double MinProfitPc { get; set; } = 10;
        public double GetAllowedDeposit(double totalDeposit) {
            return totalDeposit * AllowedDepositPc * 0.01;
        }

        [InputParameter(1, 50, 0.1)]
        public double DCAStartPercent { get; set; } = 1;
        [InputParameter(1, 50, 1)]
        public double DCAStartAmountFraction { get; set; } = 30;
        [InputParameter(1, 50, 0.1)]
        public double DCAEndPercent { get; set; } = 5;
        [InputParameter(1, 50, 1)]
        public double DCAEndAmountFraction { get; set; } = 90;
        [InputParameter(2, 5, 1)]
        public int DCALevelCount { get; set; } = 3;

        public bool EnableStopLoss { get; set; }
        public double StopLossPc { get; set; } = 2.7;
    }

    [ParameterObject]
    public class BreakUpSettings : TaSettingsBase {
        public BreakUpSettings() {
            AllowTrailing = true;
            EnableStopLoss = true;
        }
        [InputParameter(0, 1000, 1)]
        public double MinBreakValue { get; set; } = 70;
        [InputParameter(0, 1000, 1)]
        public double MaxBreakValue { get; set; } = 170;

        public BreakUpSettings Clone() {
            return (BreakUpSettings)MemberwiseClone();
        }
    }

    [ParameterObject]
    public class BreakDownSettings : TaSettingsBase {
        public BreakDownSettings() {
            AllowDCA = true;
            AllowTrailing = true;
        }
        [InputParameter(10, 1000, 1)]
        public double MinSupportBreakValue { get; set; } = 200;
        [InputParameter(1, 100, 1)]
        public int DelayedPeriodLength { get; set; } = 10;

        public BreakDownSettings Clone() {
            return (BreakDownSettings)MemberwiseClone();
        }
    }

    [ParameterObject]
    public class PingPongSettings : TaSettingsBase {
        public PingPongSettings() {
            AllowDCA = true;
            AllowTrailing = false;
            EnableStopLoss = true;
        }
        public double LowBoundPc { get; set; } = 0.1;
        public double HighBoundPc { get; set; } = 0.1;
        public int SupportLevelLength { get; set; } = 20;
        public double MinimalPriceRangeInPc { get; set; } = 2;

        public PingPongSettings Clone() {
            return (PingPongSettings)MemberwiseClone();
        }
    }

    public interface IDetailInfoProvider {
        string DetailString { get; }
    }

    public interface ILinkedObjectProvider {
        object GetLinkedObject();
    }

    public interface ILinkedChildrenProvider {
        int Count { get; }
        object GetChild(int index);
    }

    public interface IOpenedPositionsProvider {
        ResizeableArray<OpenPositionInfo> OpenedPositions { get; }
        ResizeableArray<OpenPositionInfo> ClosedPositions { get; }
        string Mark { get; set; }
        void AddMark(string mark);
    }

    public class CombinedStrategyDataItem : IDetailInfoProvider, ILinkedObjectProvider, IOpenedPositionsProvider, ILinkedChildrenProvider {
        public DateTime Time { get { return Candle.Time; } }
        public double Open { get { return Candle.Open; } }
        public double Close { get { return Candle.Close; } }
        public double High { get { return Candle.High; } }
        public double Low { get { return Candle.Low; } }
        public CandleStickData Candle { get; set; }
        public int Index { get; set; }

        //public double StartPrice { get; set; }
        //public double Spread { get { return StartPrice - EndPrice; } }
        //public double EndPrice { get; set; }
        //public SRValue SRValue { get; set; }
        //public double Atr { get; set; }
        //public int Index { get; set; }
        //public bool Resistance { get { return SRValue == null ? false : SRValue.Type == SupResType.Resistance; } }
        //public bool Support { get { return SRValue == null ? false : SRValue.Type == SupResType.Support; } }

        //public double ResistancePower { get { return SRValue == null || SRValue.Type != SupResType.Resistance ? 0 : SRValue.Power; } }
        //public double SupportPower { get { return SRValue == null || SRValue.Type != SupResType.Support ? 0 : SRValue.Power; } }

        //public double ResistanceChange { get { return SRValue == null || SRValue.Type != SupResType.Resistance ? 0 : SRValue.ChangePc; } }
        //public double SupportChange { get { return SRValue == null || SRValue.Type != SupResType.Support ? 0 : SRValue.ChangePc; } }

        //public double ResistanceLength { get { return SRValue == null || SRValue.Type != SupResType.Resistance ? 0 : SRValue.Length; } }
        //public double SupportLength { get { return SRValue == null || SRValue.Type != SupResType.Support ? 0 : SRValue.Length; } }

        //public double BreakResistanceLevel { get; set; }
        //public double BreakSupportLevel { get; set; }
        //public double BreakSupportPower { get; set; }
        //public double BreakPercent { get; set; }
        //public double BreakPercent2 { get; set; }

        //public double SRLevel { get { return SRValue == null ? 0 : SRValue.Value; } }
        //public bool Break { get; set; }
        public double Value { get; set; }
        //public bool BreakUp { get; set; }
        //public int ResIndex { get; set; }
        //public int SupIndex { get; set; }

        public int CloseLength { get; set; }
        public bool Closed { get; set; }
        public bool ClosedOrder { get; set; }
        public double Profit { get; set; }
        [XmlIgnore]
        public double SRSpread { get; internal set; }
        //public object BreakValue { get; internal set; }

        public double Earned { get; set; }
        public double CurrentLoss { get; set; }
        public double AvailableDeposit { get; set; }
        public string Mark {
            get; set;
        }
        public void AddMark(string mark) {
            if(string.IsNullOrEmpty(Mark)) {
                Mark = mark;
                return;
            }
            Mark += "," + mark;
        }

        [XmlIgnore]
        public OpenPositionInfo OpenedPosition {
            get { return OpenedPositions.Count > 0 ? OpenedPositions[0] : null; }
        }

        ResizeableArray<OpenPositionInfo> closedPositions;
        [XmlIgnore]
        public ResizeableArray<OpenPositionInfo> ClosedPositions {
            get {
                if(closedPositions == null)
                    closedPositions = new ResizeableArray<OpenPositionInfo>();
                return closedPositions;
            }
        }

        ResizeableArray<OpenPositionInfo> openedPositions;
        [XmlIgnore]
        public ResizeableArray<OpenPositionInfo> OpenedPositions {
            get {
                if(openedPositions == null)
                    openedPositions = new ResizeableArray<OpenPositionInfo>();
                return openedPositions;
            }
        }

        public string DetailString {
            get {
                StringBuilder b = new StringBuilder();
                b.Append("<b>Index: " + Index + "</b><br>");
                if(OpenedPositions.Count > 0) {
                    b.AppendLine("<b>opened positions</b><br>");
                    foreach(OpenPositionInfo info in OpenedPositions) {
                        b.Append(info.ToHtmlString());
                        b.AppendLine();
                    }
                }
                if(ClosedPositions.Count > 0) {
                    b.AppendLine("<b>closed positions</b><br>");
                    foreach(OpenPositionInfo info in ClosedPositions) {
                        b.Append(info.ToHtmlString());
                        b.AppendLine();
                    }
                }
                return b.ToString();
            }
        }

        public double PercentChangeFromSupport { get; set; }
        [XmlIgnore]
        public double SpreadBaseResistance { get; internal set; }

        object ILinkedObjectProvider.GetLinkedObject() {
            if(OpenedPosition != null)
                return OpenedPosition;
            if(ClosedPositions.Count > 0)
                return ClosedPositions[0];
            return null;
        }

        int ILinkedChildrenProvider.Count {
            get {
                if(OpenedPosition == null)
                    return 0;
                return OpenedPosition.DACPositions.Count;
            }
        }
        object ILinkedChildrenProvider.GetChild(int index) {
            return OpenedPosition.DACPositions[index];
        }
    }
}
