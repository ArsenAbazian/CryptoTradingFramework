using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Indicators {
    public class EmaIndicator : MaIndicator {
        protected override void OnPropertiesChanged() {
            base.OnPropertiesChanged();
        }

        protected double Koeff { get; set; }

        protected override void OnLenghtChanged() {
            Koeff = 2.0 / (Length + 1.0);
            base.OnLenghtChanged();
        }
        public override void Calculate() {
            Koeff = 2.0 / (Length + 1.0);
            base.Calculate();
        }

        public override TimeBaseValue Calculate(int forIndex) {
            if(forIndex < Length + 1)
                return new IndicatorValue() { Time = GetTime(forIndex), Value = double.NaN, Source = GetValueBySource(forIndex) };
            IndicatorValue prev = (IndicatorValue)Result[forIndex - 1];
            if(double.IsNaN(prev.Value))
                prev.Value = ((IndicatorValue)base.Calculate(forIndex - 1)).Value;
            double value = prev.Value + Koeff * (GetValueBySource(forIndex) - prev.Value);
            return new IndicatorValue() { Time = GetTime(forIndex), Value = value, Source = GetValueBySource(forIndex) };
        }
    }
}
