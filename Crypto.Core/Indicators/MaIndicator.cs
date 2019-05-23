using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Indicators {
    public class MaIndicator : WindowIndicator {

        protected override void OnInputDataChanged() {
            base.OnInputDataChanged();
            Source = IndicatorSource.Value;
        }

        public override TimeBaseValue Calculate(int forIndex) {
            double sum = 0;

            if(forIndex < Length)
                return new IndicatorValue() { Time = GetTime(forIndex), Value = double.NaN, Source = GetValueBySource(forIndex) };

            if(Source == IndicatorSource.Value) {
                for(int j = forIndex; j >= forIndex - Length; j--)
                    sum += ((IndicatorValue)InputData[j]).Value;
            }
            else if(Source == IndicatorSource.Close) {
                for(int j = forIndex; j >= forIndex - Length; j--)
                    sum += Ticker.CandleStickData[j].Close;
            }
            else if(Source == IndicatorSource.StochK) {
                for(int j = forIndex; j >= forIndex - Length; j--)
                    sum += ((StochasticValue)InputData[j]).K;
            }
            else if(Source == IndicatorSource.StochD) {
                for(int j = forIndex; j >= forIndex - Length; j--)
                    sum += ((StochasticValue)InputData[j]).D;
            }
            else if(Source == IndicatorSource.Open) {
                for(int j = forIndex; j >= forIndex - Length; j--)
                    sum += Ticker.CandleStickData[j].Open;
            }
            else if(Source == IndicatorSource.High) {
                for(int j = forIndex; j >= forIndex - Length; j--)
                    sum += Ticker.CandleStickData[j].High;
            }
            else if(Source == IndicatorSource.Low) {
                for(int j = forIndex; j >= forIndex - Length; j--)
                    sum += Ticker.CandleStickData[j].Low;
            }
            return new IndicatorValue() { Time = GetTime(forIndex), Value = sum / Length, Source = GetValueBySource(forIndex) };
        }
    }

    public enum IndicatorSource {
        Open,
        Close,
        High,
        Low,
        Value,
        StochK,
        StochD
    }
}
