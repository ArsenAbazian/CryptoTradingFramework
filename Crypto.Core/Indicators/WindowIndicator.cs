using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoMarketClient;

namespace Crypto.Core.Indicators {
    public class WindowIndicator : SingleValueIndicator {
        int length = 9;
        [DefaultValue(9)]
        public int Length {
            get { return length; }
            set {
                if(Length == value)
                    return;
                length = value;
                OnPropertiesChanged();
            }
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

        public override void Calculate() {
            Result.Clear();
            int count = Count;
            for(int i = 0; i < count; i++) {
                Result.Add(Calculate(i));
            }
        }
    }
}
