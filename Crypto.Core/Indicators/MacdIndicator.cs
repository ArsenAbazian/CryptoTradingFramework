using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoMarketClient;

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

        public BindingList<IndicatorValue> HistorySig { get; } = new BindingList<IndicatorValue>();

        public EmaIndicator FastEmaIndicator { get; private set; }
        public EmaIndicator SlowEmaIndicator { get; private set; }
        public MaIndicator SignalMaIndicator { get; private set; }

        public override void OnUpdateValue(int index) {
            IndicatorValue fast = (IndicatorValue)FastEmaIndicator.Calculate(index);
            IndicatorValue slow = (IndicatorValue)SlowEmaIndicator.Calculate(index);
            double value = fast.Value - slow.Value;
            ((IndicatorValue)Result[index]).Value = value;
            IndicatorValue signal = (IndicatorValue)SignalMaIndicator.Calculate(index);
            ((IndicatorValue)SignalMaIndicator.Result[index]).Value = signal.Value;
        }

        public override TimeBaseValue Calculate(int forIndex) {
            FastEmaIndicator.Result.Add(FastEmaIndicator.Calculate(forIndex));
            SlowEmaIndicator.Result.Add(SlowEmaIndicator.Calculate(forIndex));

            if(forIndex < SlowLength) {
                IndicatorValue nullValue = new IndicatorValue() { Time = GetTime(forIndex), Value = double.NaN, Source = GetValueBySource(forIndex) };
                Result.Add(nullValue);
                SignalMaIndicator.Result.Add(SignalMaIndicator.Calculate(forIndex));
                Result.Remove(nullValue);
                return nullValue;
            }
            
            IndicatorValue value = new IndicatorValue() { Time = GetTime(forIndex), Value = ((IndicatorValue)FastEmaIndicator.Result[forIndex]).Value - ((IndicatorValue)SlowEmaIndicator.Result[forIndex]).Value, Source = GetValueBySource(forIndex) };
            Result.Add(value);
            IndicatorValue signal = (IndicatorValue)SignalMaIndicator.Calculate(forIndex);
            SignalMaIndicator.Result.Add(signal);
            Result.Remove(value);
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
