using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Indicators {
    public class RsiIndicator : WindowIndicator {
        public double Tolerance { get; set; }

        public override void Assign(IndicatorBase ind) {
            base.Assign(ind);
            RsiIndicator rsi = ind as RsiIndicator;
            if(rsi != null) {
                Tolerance = rsi.Tolerance;
            }
        }

        public double Calculate(IEnumerable<double> closePrices) {
            var prices = closePrices as double[] ?? closePrices.ToArray();

            double sumGain = 0;
            double sumLoss = 0;
            for(int i = 1; i < prices.Length; i++) {
                var difference = prices[i] - prices[i - 1];
                if(difference >= 0) {
                    sumGain += difference;
                }
                else {
                    sumLoss -= difference;
                }
            }

            if(sumGain == 0) return 0;
            if(Math.Abs(sumLoss) < Tolerance) return 100;

            var relativeStrength = sumGain / sumLoss;

            return 100.0 - (100.0 / (1 + relativeStrength));
        }

        public override TimeBaseValue Calculate(int forIndex) {
            DateTime dt = GetTime(forIndex);

            if(forIndex < Length)
                return new IndicatorValue() { Time = dt, Value = double.NaN, Source = GetValueBySource(forIndex) };
            int start = forIndex - Length + 1;
            int end = forIndex;

            double sumGain = 0;
            double sumLoss = 0;

            if(Source == IndicatorSource.Close) {
                for(int i = start; i <= end; i++) {
                    var difference = Ticker.CandleStickData[i].Close - Ticker.CandleStickData[i - 1].Close;
                    if(difference >= 0)
                        sumGain += difference;
                    else
                        sumLoss -= difference;
                }
            }

            if(sumGain == 0)
                return new IndicatorValue() { Time = dt, Value = 0, Source = GetValueBySource(forIndex) };
            if(Math.Abs(sumLoss) < Tolerance)
                return new IndicatorValue() { Time = dt, Value = 100, Source = GetValueBySource(forIndex) };

            var relativeStrength = sumGain / sumLoss;
            var res = 100.0 - (100.0 / (1 + relativeStrength));
            return new IndicatorValue() { Time = dt, Value = res, Source = GetValueBySource(forIndex) };
        }
    }
}
