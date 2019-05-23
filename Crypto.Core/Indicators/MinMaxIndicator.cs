using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Indicators {
    public class MinMaxIndicator : WindowIndicator {
        MinMaxIndicatorMode mode = MinMaxIndicatorMode.Min;
        [DefaultValue(MinMaxIndicatorMode.Min)]
        public MinMaxIndicatorMode Mode {
            get { return mode; }
            set {
                if(Mode == value)
                    return;
                mode = value;
                OnPropertiesChanged();
            }
        }

        public override void Assign(IndicatorBase ind) {
            base.Assign(ind);
            MinMaxIndicator mima = ind as MinMaxIndicator;
            if(mima != null) {
                Mode = mima.Mode;
            }
        }

        public override TimeBaseValue Calculate(int forIndex) {
            if(forIndex < Length)
                return new IndicatorValue() { Time = GetTime(forIndex), Value = double.NaN, Source = GetValueBySource(forIndex) };
            double value = 0;
            if(Mode == MinMaxIndicatorMode.Min) {
                value = double.MaxValue;
                if(Source == IndicatorSource.Value) {
                    for(int j = forIndex; j >= forIndex - Length; j--)
                        value = Math.Min(value, ((IndicatorValue)InputData[j]).Value);
                }
                else if(Source == IndicatorSource.Close) {
                    for(int j = forIndex; j >= forIndex - Length; j--)
                        value = Math.Min(value, Ticker.CandleStickData[j].Close);
                }
                else if(Source == IndicatorSource.StochK) {
                    for(int j = forIndex; j >= forIndex - Length; j--)
                        value = Math.Min(value, ((StochasticValue)InputData[j]).K);
                }
                else if(Source == IndicatorSource.StochD) {
                    for(int j = forIndex; j >= forIndex - Length; j--)
                        value = Math.Min(value, ((StochasticValue)InputData[j]).D);
                }
                else if(Source == IndicatorSource.Open) {
                    for(int j = forIndex; j >= forIndex - Length; j--)
                        value = Math.Min(value, Ticker.CandleStickData[j].Open);
                }
                else if(Source == IndicatorSource.High) {
                    for(int j = forIndex; j >= forIndex - Length; j--)
                        value = Math.Min(value, Ticker.CandleStickData[j].High);
                }
                else if(Source == IndicatorSource.Low) {
                    for(int j = forIndex; j >= forIndex - Length; j--)
                        value = Math.Min(value, Ticker.CandleStickData[j].Low);
                }
            }
            else {
                value = double.MinValue;
                if(Source == IndicatorSource.Value) {
                    for(int j = forIndex; j >= forIndex - Length; j--)
                        value = Math.Max(value, ((IndicatorValue)InputData[j]).Value);
                }
                else if(Source == IndicatorSource.Close) {
                    for(int j = forIndex; j >= forIndex - Length; j--)
                        value = Math.Max(value, Ticker.CandleStickData[j].Close);
                }
                else if(Source == IndicatorSource.StochK) {
                    for(int j = forIndex; j >= forIndex - Length; j--)
                        value = Math.Max(value, ((StochasticValue)InputData[j]).K);
                }
                else if(Source == IndicatorSource.StochD) {
                    for(int j = forIndex; j >= forIndex - Length; j--)
                        value = Math.Max(value, ((StochasticValue)InputData[j]).D);
                }
                else if(Source == IndicatorSource.Open) {
                    for(int j = forIndex; j >= forIndex - Length; j--)
                        value = Math.Max(value, Ticker.CandleStickData[j].Open);
                }
                else if(Source == IndicatorSource.High) {
                    for(int j = forIndex; j >= forIndex - Length; j--)
                        value = Math.Max(value, Ticker.CandleStickData[j].High);
                }
                else if(Source == IndicatorSource.Low) {
                    for(int j = forIndex; j >= forIndex - Length; j--)
                        value = Math.Max(value, Ticker.CandleStickData[j].Low);
                }
            }
            return new IndicatorValue() { Time = GetTime(forIndex), Value = value, Source = GetValueBySource(forIndex) };
        }
    }

    public enum MinMaxIndicatorMode {
        Min,
        Max
    }
}
