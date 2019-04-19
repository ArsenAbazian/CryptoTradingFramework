using CryptoMarketClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Strategies.Listeners {
    public class TickerDataCaptureStrategy : Custom.CustomTickerStrategy {
        [DisplayName("Save To Directory")]
        public string Directory { get; set; }

        public override void Assign(StrategyBase from) {
            base.Assign(from);
            TickerDataCaptureStrategy s = from as TickerDataCaptureStrategy;
            if(s == null)
                return;
            Directory = s.Directory;
        }
        protected override void UpdateTickersList() {
            base.UpdateTickersList();
            foreach(Ticker t in Tickers) {
                t.CaptureDirectory = Directory;
                t.CaptureData = true;
            }
        }
        protected override void OnTickCore() {
            //base.OnTickCore();
        }
    }
}
