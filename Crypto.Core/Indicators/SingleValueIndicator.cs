using Crypto.Core.Helpers;
using Crypto.Core.Strategies;
using Crypto.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Indicators {
    public class SingleValueIndicator : IndicatorBase {

        [XmlIgnore]
        public ResizeableArray<TimeBaseValue> Result { get; } = new ResizeableArray<TimeBaseValue>();

        public Color ChartColor { get; set; } = Color.Green;

        public override void AddVisualInfo(List<StrategyDataItemInfo> items, string panelName) {
            StrategyDataItemInfo info = new StrategyDataItemInfo() {
                DataSource = Result,
                FieldName = "Value",
                Color = ChartColor,
                ChartType = ChartType.Line};
            if(UseOwnChart) {
                info.PanelName = panelName;
            }
            items.Add(info);
        }

        protected virtual void OnValueChanged() {
            throw new NotImplementedException();
        }

        public virtual TimeBaseValue Calculate(int forIndex) {
            return null;
        }

        IndicatorSource source = IndicatorSource.Close;
        [DefaultValue(IndicatorSource.Close)]
        public IndicatorSource Source {
            get { return source; }
            set {
                if(Source == value)
                    return;
                source = value;
                OnPropertiesChanged();
            }
        }

        public override void Assign(IndicatorBase ind) {
            base.Assign(ind);
            SingleValueIndicator svi = ind as SingleValueIndicator;
            if(svi != null)
                Source = svi.Source;
        }

        protected override void OnTickerChanged(Ticker prev, Ticker current) {
            base.OnTickerChanged(prev, current);
            if(prev != null)
                prev.CandleStickChanged -= OnCandleStickChanged;
            if(current != null)
                current.CandleStickChanged += OnCandleStickChanged;
        }

        protected virtual void OnCandleStickChanged(object sender, ListChangedEventArgs e) {
            if(!UseCandleStickData || SuppressUpdateOnDataChanged)
                return;
            if(e.ListChangedType == ListChangedType.Reset) {
                Calculate();
                return;
            }
            else if(e.ListChangedType == ListChangedType.ItemChanged) {
                OnUpdateValue(e.NewIndex);
                return;
            }
            else if(e.ListChangedType == ListChangedType.ItemAdded) {
                OnAddValue();
                return;
            }
        }

        public virtual void OnAddValue() {
            Result.Add(Calculate(Count - 1));
        }

        public virtual void OnUpdateValue(int index) {
            Result[index] = Calculate(index);
        }

        public DateTime GetTime(int index) {
            if(InputData != null)
                return InputData[index].Time;
            return Ticker.CandleStickData[index].Time;
        }

        public double GetValueBySource(int index) {
            if(Source == IndicatorSource.Value)
                return ((IndicatorValue)InputData[index]).Value;
            else if(Source == IndicatorSource.Close)
                return Ticker.CandleStickData[index].Close;
            else if(Source == IndicatorSource.StochK)
                return ((StochasticValue)InputData[index]).K;
            else if(Source == IndicatorSource.StochD)
                return ((StochasticValue)InputData[index]).D;
            else if(Source == IndicatorSource.Open)
                return Ticker.CandleStickData[index].Open;
            else if(Source == IndicatorSource.High)
                return Ticker.CandleStickData[index].High;
            else if(Source == IndicatorSource.Low)
                return Ticker.CandleStickData[index].Low;
            return double.NaN;
        }

        protected bool UseCandleStickData {
            get { return Source == IndicatorSource.Close || Source == IndicatorSource.Open || Source == IndicatorSource.Low || Source == IndicatorSource.High; }
        }

        [XmlIgnore]
        public int Count {
            get {
                if(UseCandleStickData) {
                    if(Ticker == null)
                        return 0;
                    return Ticker.CandleStickData.Count;
                }
                if(InputData != null)
                    return InputData.Count;
                return 0;
            }
        }

        public virtual void Clear() {
            Result.Clear();
        }

        public override void Calculate() {
            Clear();
            int count = Count;
            for(int i = 0; i < count; i++) {
                Result.Add(Calculate(i));
            }
        }

        [XmlIgnore]
        public double MathExp { get; set; }
        [XmlIgnore]
        public double Deviation { get; set; }

        public double CalculateMath() {
            double sum = 0;
            foreach(var item in Result) {
                if(double.IsNaN(item.Value))
                    continue;
                sum += item.Value;
            }
            MathExp = sum / Result.Count;
            return MathExp;
        }

        public double CalculateDev() {
            double sum = 0;
            foreach(var item in Result) {
                if(double.IsNaN(item.Value))
                    continue;
                double sub = item.Value - MathExp;
                sum += sub * sub;
            }
            sum /= Result.Count;
            Deviation = System.Math.Sqrt(sum);
            return Deviation;
        }

        protected double CalcMin() {
            double min = double.MaxValue;
            foreach(var item in Result) {
                if(double.IsNaN(item.Value))
                    continue;
                min = Math.Min(min, item.Value);
            }
            return min;
        }

        protected double CalcMax() {
            double max = double.MinValue;
            foreach(var item in Result) {
                if(double.IsNaN(item.Value))
                    continue;
                max = Math.Max(max, item.Value);
            }
            return max;
        }

        public ResizeableArray<ArgumentValue> CalcHistogramm(int count) {
            return HistogrammCalculator.Calculate(Result, count);
        }
    }
    
    public class IndicatorValue : TimeBaseValue {
        
    }

    public class TimeBaseValue {
        DateTime time;
        public DateTime Time {
            get { return time; }
            set {
                if(Time == value)
                    return;
                time = value;
                OnTimeChanged();
            }
        }

        public virtual double Source { get; set; }
        public virtual double Value { get; set; }
        protected virtual void OnTimeChanged() {
        }
    }
}
