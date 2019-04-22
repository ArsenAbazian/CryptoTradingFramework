using Crypto.Core.Helpers;
using CryptoMarketClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Strategies.Listeners {
    public class TickerDataCaptureStrategy : Custom.CustomTickerStrategy, ISupportSerialization {
        [DisplayName("Save To Directory")]
        public string Directory { get; set; }

        public static TickerDataCaptureStrategy LoadFromFile(string fileName) {
            return (TickerDataCaptureStrategy)SerializationHelper.FromFile(fileName, typeof(TickerDataCaptureStrategy));
        }

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

        public bool Save(string path) {
            return SerializationHelper.Save(this, typeof(TickerDataCaptureStrategy), path);
        }
    }
}
