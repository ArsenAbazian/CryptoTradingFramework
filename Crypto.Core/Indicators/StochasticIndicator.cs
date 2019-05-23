using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Indicators {
    public class StochasticIndicator : WindowIndicator {
        double smoothK = 1;
        [DefaultValue(1)]
        public double SmoothK {
            get { return smoothK; }
            set {
                if(SmoothK == value)
                    return;
                smoothK = value;
                OnPropertiesChanged();
            }
        }

        double smoothD = 3;
        [DefaultValue(3)]
        public double SmoothD {
            get { return smoothD; }
            set {
                if(SmoothD == value)
                    return;
                smoothD = value;
                OnPropertiesChanged();
            }
        }

        public override void Assign(IndicatorBase ind) {
            base.Assign(ind);
            StochasticIndicator si = ind as StochasticIndicator;
            if(si != null) {
                SmoothK = si.SmoothK;
                SmoothD = si.SmoothD;
            }
        }

        [XmlIgnore]
        public MinMaxIndicator MinIndicator { get; private set; }
        [XmlIgnore]
        public MinMaxIndicator MaxIndicator { get; private set; }
        [XmlIgnore]
        public MaIndicator MaIndicator { get; private set; }

        public override void Calculate() {
            MinIndicator = new MinMaxIndicator() { Ticker = Ticker, Length = Length, Mode = MinMaxIndicatorMode.Min, SuppressUpdateOnDataChanged = true };
            MaxIndicator = new MinMaxIndicator() { Ticker = Ticker, Length = Length, Mode = MinMaxIndicatorMode.Max, SuppressUpdateOnDataChanged = true };
            MaIndicator = new MaIndicator() { InputData = Result, Length = Length, Source = IndicatorSource.StochK, SuppressUpdateOnDataChanged = true };

            base.Calculate();
        }

        public override void OnUpdateValue(int index) {
            IndicatorValue max = (IndicatorValue)MaxIndicator.Calculate(index);
            IndicatorValue min = (IndicatorValue)MinIndicator.Calculate(index);

            double k = (GetValueBySource(index) - min.Value) / (max.Value - min.Value) * 100;
            ((StochasticValue)Result[index]).K = k;

            IndicatorValue ma = (IndicatorValue)MaIndicator.Calculate(index);
            ((StochasticValue)Result[index]).D = ma.Value;
        }

        public override TimeBaseValue Calculate(int forIndex) {
            IndicatorValue max = (IndicatorValue)MaxIndicator.Calculate(forIndex);
            MaxIndicator.Result.Add(max);
            IndicatorValue min = (IndicatorValue)MinIndicator.Calculate(forIndex);
            MinIndicator.Result.Add(min);

            IndicatorValue ma = null;
            if(forIndex < Length) {
                StochasticValue nullValue = new StochasticValue() { Time = GetTime(forIndex), K = double.NaN, D = double.NaN, Source = GetValueBySource(forIndex) };
                Result.Push(nullValue);
                ma = (IndicatorValue)MaIndicator.Calculate(forIndex);
                MaIndicator.Result.Add(ma);
                Result.Pop();
                return nullValue;
            }

            double k = (GetValueBySource(forIndex) - min.Value) / (max.Value - min.Value) * 100;
            StochasticValue res = new StochasticValue() {
                Time = GetTime(forIndex),
                K = k,
                D = 0,
                Source = GetValueBySource(forIndex)
            };
            Result.Push(res);
            ma = (IndicatorValue)MaIndicator.Calculate(forIndex);
            MaIndicator.Result.Add(ma);
            res.D = ma.Value;
            Result.Pop();

            return res;
        }
    }

    public class StochasticValue : TimeBaseValue {
        public double K { get; set; }
        public double D { get; set; }
    }
}
