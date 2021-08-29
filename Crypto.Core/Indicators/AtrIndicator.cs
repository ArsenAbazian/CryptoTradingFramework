using Crypto.Core.Helpers;
using Crypto.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Indicators {
    public class AtrIndicator : WindowIndicator {
        [XmlIgnore]
        public MaIndicator MaIndicator { get; private set; }
        protected ResizeableArray<TimeBaseValue> ResultCore { get; } = new ResizeableArray<TimeBaseValue>();

        public override void OnUpdateValue(int index) {
            base.OnUpdateValue(index);
            if(index < Length) {
                return;
            }
            double value = CalculateCore(index);
            ((IndicatorValue)ResultCore[index]).Value = value;
            IndicatorValue signal = (IndicatorValue)MaIndicator.Calculate(index);
            ((IndicatorValue)MaIndicator.Result[index]).Value = signal.Value;
            Result[index].Value = signal.Value;
        }

        protected double CalculateCore(int forIndex) {
            CandleStickData curr = Ticker.CandleStickData[forIndex];
            CandleStickData prev = Ticker.CandleStickData[forIndex - 1];
            double tr = Math.Max(curr.High - curr.Low, Math.Max(Math.Abs(curr.High - prev.Close), Math.Abs(curr.Low - prev.Close)));
            return tr;
        }

        void CheckCreateMaIndicator() {
            if(MaIndicator == null)
                MaIndicator = new MaIndicator() { InputData = ResultCore, Length = Length, SuppressUpdateOnDataChanged = true };
        }
        public override TimeBaseValue Calculate(int forIndex) {
            CheckCreateMaIndicator();

            DateTime dt = GetTime(forIndex);
            if(forIndex < Length) {
                IndicatorValue nullValue = new IndicatorValue() { Time = dt, Value = double.NaN, Source = GetValueBySource(forIndex) };
                ResultCore.Add(nullValue);
                IndicatorValue val = (IndicatorValue)MaIndicator.Calculate(forIndex);
                MaIndicator.Result.Add(val);
                return nullValue;
            }

            double tr = CalculateCore(forIndex);
            IndicatorValue value = new IndicatorValue() { Time = dt, Value = tr, Source = GetValueBySource(forIndex) };
            ResultCore.Add(value);
            IndicatorValue signal = (IndicatorValue)MaIndicator.Calculate(forIndex);
            MaIndicator.Result.Add(signal);
            return signal;
        }

        public override void Calculate() {
            MaIndicator = null;
            CheckCreateMaIndicator();
            base.Calculate();
        }
    }
}
