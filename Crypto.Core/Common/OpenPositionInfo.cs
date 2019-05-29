using Crypto.Core.Common.OrderGrid;
using Crypto.Core.Helpers;
using Crypto.Core.Indicators;
using Crypto.Core.Strategies;
using CryptoMarketClient.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Common {
    public class OpenPositionInfo {
        public DateTime Time { get; set; }
        public DateTime CloseTime { get; set; }
        public TimeSpan Length { get { return CloseTime - Time; } }

        public bool AllowDAC { get; set; }
        public OrderGridInfo DACInfo { get; set; }
        public double DACTotalAmount { get; set; }
        public void InitializeDAC(double openValue, double probableMin, double amount, int stepCount) {
            DACInfo = new OrderGridInfo();
            DACInfo.Start.AmountPercent = 30;
            DACInfo.Start.Value = openValue;
            DACInfo.End.AmountPercent = 90;
            DACInfo.End.Value = probableMin;
            DACInfo.LineCount = stepCount;
            DACInfo.Normalize();
            DACTotalAmount = amount;
        }

        [XmlIgnore]
        public GridLineInfo[] DACLines {
            get { return AllowDAC ? DACInfo.Lines : new GridLineInfo[0]; }
        }
        public bool AllowHistory { get; set; } = false;
        public TimeSpan AggregationTime { get; set; } = new TimeSpan(0, 0, 30);

        public OrderType Type { get; set; }
        public bool AllowTrailing { get; set; }
        public string MarketName { get; set; }
        public double StopLossPercent { get; set; } = 5;
        public double StopLossDelta { get; set; } = 1000;
        public double OpenValue { get; set; }
        public double CloseValue { get; set; }
        public double Change { get { return (CloseValue - OpenValue) / OpenValue * 100; } }
        double currentValue;
        public void UpdateCurrentValue(DateTime time, double value) {
            if(CurrentValue == value)
                return;
            currentValue = value;
            UpdateStopLoss();
            OnCurrentValueChanged(time, value);
        }
        public double CurrentValue {
            get { return currentValue; }
        }

        public void OnCurrentValueChanged(DateTime time, double value) {
            if(!AllowHistory)
                return;
            DateTime lastValueTime = DateTime.MinValue;
            if(ValueHistory.Count > 0)
                lastValueTime = ValueHistory.Last().Time;
            if(time - lastValueTime > AggregationTime)
                ValueHistory.Add(new TimeBaseValue() { Time = time, Value = value });
            DateTime stopLossTime = DateTime.MinValue;
            if(StopLossHistory.Count > 0)
                stopLossTime = StopLossHistory.Last().Time;
            if(time - stopLossTime > AggregationTime)
                StopLossHistory.Add(new TimeBaseValue() { Time = time, Value = StopLoss });
        }

        public ResizeableArray<TimeBaseValue> ValueHistory { get; } = new ResizeableArray<TimeBaseValue>();
        public ResizeableArray<TimeBaseValue> StopLossHistory { get; } = new ResizeableArray<TimeBaseValue>();

        protected double[] PreviewFromHistory(ResizeableArray<TimeBaseValue> history) {
            if(history.Count == 0)
                return new double[0];
            int count = Math.Min(50, history.Count);
            int delta = Math.Max(1, history.Count / count);
            double[] data = new double[count];
            for(int i = 0, j = 0; i < count && j < history.Count; i++, j += delta) {
                data[i] = history[j].Value;
            }
            return data;
        }

        double[] valuePreview;
        public double[] ValuePreview {
            get {
                if(valuePreview == null)
                    valuePreview = PreviewFromHistory(ValueHistory);
                return valuePreview;
            }
        }

        double[] stopLossPreview;
        public double[] StopLossPreview {
            get {
                if(stopLossPreview == null)
                    stopLossPreview = PreviewFromHistory(StopLossHistory);
                return stopLossPreview;
            }
        }

        public double Earned { get; set; }
        public double Spent { get; set; }
        public double Profit { get { return Earned - Spent; } }
        public double StopLoss { get; set; }
        public double Amount { get; set; }
        public double Total { get; set; }
        [XmlIgnore]
        public object Tag { get; set; }
        [XmlIgnore]
        public object Tag2 { get; set; }
        public string Mark { get; set; }


        protected double CalcStopLoss(double value) {
            double newStopLoss = value * (100 - StopLossPercent) * 0.01;
            newStopLoss = Math.Max(value - StopLossDelta, newStopLoss);
            return newStopLoss;
        }
        public void UpdateStopLoss() {
            double newStopLoss = CalcStopLoss(CurrentValue);
            newStopLoss = Math.Max(newStopLoss, OpenValue);
            StopLoss = Math.Max(newStopLoss, StopLoss);
        }

        public string ToHtmlString() {
            StringBuilder b = new StringBuilder();
            b.Append("amount: <b>" + Amount.ToString("0.########") + "<b>");
            b.Append("open price: <b>" + OpenValue.ToString("0.########") + "<b>");
            b.Append("close price: <b>" + CloseValue.ToString("0.########") + "<b>");
            b.Append("stoploss: <b>" + StopLoss.ToString("0.########") + "<b>");
            b.Append("change: <b>" + Change.ToString("0.##") + "%<b>");
            b.Append("spent: <b>" + Spent.ToString("0.########") + "<b>");
            b.Append("earned: <b>" + Earned.ToString("0.########") + "<b>");
            b.Append("profit: <b>" + Profit.ToString("0.########") + "<b>");
            return b.ToString();
        }
    }

    public class OpenPositionVisualDataProvider : IStrategyDataItemInfoOwner {
        public OpenPositionVisualDataProvider(ResizeableArray<OpenPositionInfo> items) {
            Items = items;
            InitializeDataItemInfos();
        }

        protected virtual void InitializeDataItemInfos() {
            StrategyDataItemInfo.TimeItem(DataItemInfos, "Time").Visibility = DataVisibility.Table;
            StrategyDataItemInfo.TimeItem(DataItemInfos, "CloseTime").Visibility = DataVisibility.Table;
            StrategyDataItemInfo.TimeSpanItem(DataItemInfos, "Length").Visibility = DataVisibility.Table;
            StrategyDataItemInfo.TimeItem(DataItemInfos, "Mark").Visibility = DataVisibility.Table;
            StrategyDataItemInfo.EnumItem(DataItemInfos, "Type").Visibility = DataVisibility.Table;
            StrategyDataItemInfo.DataItem(DataItemInfos, "OpenValue").Visibility = DataVisibility.Table;
            StrategyDataItemInfo.DataItem(DataItemInfos, "CloseValue").Visibility = DataVisibility.Table;
            StrategyDataItemInfo.DataItem(DataItemInfos, "Change").Visibility = DataVisibility.Table;
            StrategyDataItemInfo.DataItem(DataItemInfos, "StopLoss").Visibility = DataVisibility.Table;
            StrategyDataItemInfo.DataItem(DataItemInfos, "Spent").Visibility = DataVisibility.Table;
            StrategyDataItemInfo.DataItem(DataItemInfos, "Earned").Visibility = DataVisibility.Table;
            StrategyDataItemInfo.DataItem(DataItemInfos, "Profit").Visibility = DataVisibility.Table;

            var vp = StrategyDataItemInfo.DataItem(DataItemInfos, "ValuePreview"); vp.Visibility = DataVisibility.Table;
            var sp = StrategyDataItemInfo.DataItem(DataItemInfos, "StopLossPreview"); sp.Visibility = DataVisibility.Table;

            var vh = StrategyDataItemInfo.DataItem(DataItemInfos, "Value"); vh.Visibility = DataVisibility.Chart; vh.Type = DataType.ChartData; vh.Color = Color.Green; vh.BindingSource = "ValueHistory";
            var dac = StrategyDataItemInfo.DataItem(vh.Children, "Value", Color.Orange); dac.ChartType = ChartType.ConstantY; dac.BindingSource = "DACLines";
            var slc = StrategyDataItemInfo.DataItem(vh.Children, "Value"); slc.Visibility = DataVisibility.Chart; slc.Type = DataType.ChartData; slc.Color = Color.Red; slc.BindingSource = "StopLossHistory";
            var clLine = StrategyDataItemInfo.DataItem(vh.Children, "CloseValue"); clLine.ChartType = ChartType.ConstantY; clLine.BindingSource = "CloseValue";

            var sh = StrategyDataItemInfo.DataItem(DataItemInfos, "Value"); sh.Visibility = DataVisibility.Chart; sh.Type = DataType.ChartData; sh.Color = Color.Red; sh.BindingSource = "StopLossHistory";

            vp.DetailInfo = vh;
            sp.DetailInfo = sh;
        }

        public List<StrategyDataItemInfo> DataItemInfos { get; } = new List<StrategyDataItemInfo>();

        public ResizeableArray<OpenPositionInfo> Items { get; private set; }

        string IStrategyDataItemInfoOwner.Name => "Opened Positions";

        List<StrategyDataItemInfo> IStrategyDataItemInfoOwner.DataItemInfos => DataItemInfos;

        ResizeableArray<object> IStrategyDataItemInfoOwner.Items {
            get {
                ResizeableArray<object> res = new ResizeableArray<object>();
                for(int i = 0; i < Items.Count; i++) {
                    res.Add(Items[i]);
                }
                return res;
            }
        }
    }

    public class OpenPositionTrailingHistoryItem {
        public DateTime Time { get; set; }
        public double Current { get; set; }
        public double NextClose { get; set; }
        public double StopLoss { get; set; }
    }
}
