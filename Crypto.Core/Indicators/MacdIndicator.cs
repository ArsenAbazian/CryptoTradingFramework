using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Crypto.Core.Helpers;
using Crypto.Core.Strategies;
using Crypto.Core;

namespace Crypto.Core.Indicators {
    public class MacdIndicator : WindowIndicator {
        int fastLength = 12;
        [DefaultValue(12)]
        public int FastLength {
            get { return fastLength; }
            set {
                if(FastLength == value)
                    return;
                fastLength = value;
                OnPropertiesChanged();
            }
        }

        int slowLength = 26;
        [DefaultValue(26)]
        public int SlowLength {
            get { return slowLength; }
            set {
                if(SlowLength == value)
                    return;
                slowLength = value;
                OnPropertiesChanged();
            }
        }

        public override void Assign(IndicatorBase ind) {
            base.Assign(ind);
            MacdIndicator mai = ind as MacdIndicator;
            if(mai != null) {
                FastLength = mai.FastLength;
                SlowLength = mai.SlowLength;
            }
        }

        public Color FastEmaColor { get; set; } = Color.Blue;
        public Color SlowEmaColor { get; set; } = Color.Red;
        public Color SignalColor { get; set; } = Color.Green;

        [XmlIgnore]
        public ResizeableArray<IndicatorValue> HistorySig { get; } = new ResizeableArray<IndicatorValue>();

        [XmlIgnore]
        public EmaIndicator FastEmaIndicator { get; private set; }
        [XmlIgnore]
        public EmaIndicator SlowEmaIndicator { get; private set; }
        [XmlIgnore]
        public MaIndicator SignalMaIndicator { get; private set; }

        public override void OnUpdateValue(int index) {
            IndicatorValue fast = (IndicatorValue)FastEmaIndicator.Calculate(index);
            IndicatorValue slow = (IndicatorValue)SlowEmaIndicator.Calculate(index);
            double value = fast.Value - slow.Value;
            ((IndicatorValue)Result[index]).Value = value;
            IndicatorValue signal = (IndicatorValue)SignalMaIndicator.Calculate(index);
            ((IndicatorValue)SignalMaIndicator.Result[index]).Value = signal.Value;
        }

        public override void AddVisualInfo(List<StrategyDataItemInfo> items, string panelName) {
            StrategyDataItemInfo info = new StrategyDataItemInfo() {
                DataSource = Result,
                FieldName = "Value",
                Color = FastEmaColor,
                ChartType = ChartType.Line
            };

            info.PanelName = "MACD";
            items.Add(info);

            StrategyDataItemInfo info2 = new StrategyDataItemInfo() {
                BindingSource = "SignalMaIndicator.Result",
                BindingRoot = this,
                FieldName = "Value",
                Color = SlowEmaColor,
                ChartType = ChartType.Line
            };

            info2.PanelName = panelName;
            items.Add(info2);
        }

        protected void CheckCreateInnerIndicators() {
            if(FastEmaIndicator != null)
                return;
            EmaIndicator emaFast = new EmaIndicator() { Ticker = Ticker, Length = FastLength, SuppressUpdateOnDataChanged = true };
            EmaIndicator emaSlow = new EmaIndicator() { Ticker = Ticker, Length = SlowLength, SuppressUpdateOnDataChanged = true };
            MaIndicator ma = new MaIndicator() { Length = Length, InputData = Result, SuppressUpdateOnDataChanged = true };
            FastEmaIndicator = emaFast;
            SlowEmaIndicator = emaSlow;
            SignalMaIndicator = ma;
        }

        public override TimeBaseValue Calculate(int forIndex) {
            CheckCreateInnerIndicators();

            FastEmaIndicator.Result.Add(FastEmaIndicator.Calculate(forIndex));
            SlowEmaIndicator.Result.Add(SlowEmaIndicator.Calculate(forIndex));

            if(forIndex < SlowLength) {
                IndicatorValue nullValue = new IndicatorValue() { Time = GetTime(forIndex), Value = double.NaN, Source = GetValueBySource(forIndex) };
                Result.Push(nullValue);
                SignalMaIndicator.Result.Add(SignalMaIndicator.Calculate(forIndex));
                Result.Pop();
                return nullValue;
            }
            
            IndicatorValue value = new IndicatorValue() { Time = GetTime(forIndex), Value = ((IndicatorValue)FastEmaIndicator.Result[forIndex]).Value - ((IndicatorValue)SlowEmaIndicator.Result[forIndex]).Value, Source = GetValueBySource(forIndex) };
            Result.Push(value);
            IndicatorValue signal = (IndicatorValue)SignalMaIndicator.Calculate(forIndex);
            SignalMaIndicator.Result.Add(signal);
            Result.Pop();
            return value;
        }

        public override void Calculate() {
            Result.Clear();
            HistorySig.Clear();
            if(Ticker == null)
                return;
            EmaIndicator emaFast = new EmaIndicator() { Ticker = Ticker, Length = FastLength, SuppressUpdateOnDataChanged = true };
            EmaIndicator emaSlow = new EmaIndicator() { Ticker = Ticker, Length = SlowLength, SuppressUpdateOnDataChanged = true };

            emaFast.Calculate();
            emaSlow.Calculate();
            
            for(int i = 0; i < Ticker.CandleStickData.Count; i++) {
                DateTime time = Ticker.CandleStickData[i].Time;
                if(i < SlowLength) {
                    Result.Add(new IndicatorValue() { Time = time, Value = double.NaN, Source = GetValueBySource(i) });
                    continue;
                }
                Result.Add(new IndicatorValue() { Time = time, Value = ((IndicatorValue)emaFast.Result[i]).Value - ((IndicatorValue)emaSlow.Result[i]).Value, Source = GetValueBySource(i) });
            }

            MaIndicator ma = new MaIndicator() { Length = Length, InputData = Result, SuppressUpdateOnDataChanged = true };
            ma.Calculate();

            FastEmaIndicator = emaFast;
            SlowEmaIndicator = emaSlow;
            SignalMaIndicator = ma;
        }
    }
}
