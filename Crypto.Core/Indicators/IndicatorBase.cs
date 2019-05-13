using Crypto.Core.Strategies;
using CryptoMarketClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Indicators {
    [InputParameterObject]
    public class IndicatorBase {
        Ticker ticker;
        public Ticker Ticker {
            get { return ticker; }
            set {
                if(ticker == value)
                    return;
                Ticker prev = ticker;
                ticker = value;
                OnTickerChanged(prev, ticker);
            }
        }

        public bool SuppressUpdateOnDataChanged { get; set; }

        BindingList<TimeBaseValue> data;
        public BindingList<TimeBaseValue> InputData {
            get { return data; }
            set {
                if(InputData == value)
                    return;
                data = value;
                OnInputHistoryChanged();
            }
        }

        protected virtual void OnInputHistoryChanged() { }

        protected virtual void OnTickerChanged(Ticker prev, Ticker current) {
            
        }

        protected virtual void OnPropertiesChanged() {
            
        }

        public virtual void Calculate() { }
    }
}
