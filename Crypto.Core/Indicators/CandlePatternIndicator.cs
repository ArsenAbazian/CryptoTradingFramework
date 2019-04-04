using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Indicators {
    public class CandlePatternIndicator : SingleValueIndicator {
        public override void OnUpdateValue(int index) {
            base.OnUpdateValue(index);
        }

        public override void OnAddValue() {
            base.OnAddValue();
        }

        public override TimeBaseValue Calculate(int forIndex) {
            if(Count < 3)
                return new IndicatorValue() { Time = GetTime(forIndex), Value = double.NaN };
            return new IndicatorValue() { Time = GetTime(forIndex), Value = FindPattern() };
        }

        protected virtual double FindPattern() {
            return 0;
        }
    }
}
