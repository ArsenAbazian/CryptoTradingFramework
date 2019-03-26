using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Indicators {
    public class SingleValueIndicator : IndicatorBase {

        public BindingList<TimeBaseValue> Result { get; } = new BindingList<TimeBaseValue>();

        protected virtual void OnValueChanged() {
            throw new NotImplementedException();
        }

        public virtual TimeBaseValue Calculate(int forIndex) {
            return null;
        }
    }

    public class IndicatorValue : TimeBaseValue {
        
    }

    public class TimeBaseValue {
        DateTime time;
        public DateTime Time {
            get { return time; }
            set {
                if(Time == value)
                    return;
                time = value;
                OnTimeChanged();
            }
        }

        public virtual double Source { get; set; }
        public virtual double Value { get; set; }
        protected virtual void OnTimeChanged() {
        }
    }
}
