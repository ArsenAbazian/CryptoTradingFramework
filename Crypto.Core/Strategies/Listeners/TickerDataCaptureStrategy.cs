using Crypto.Core.Helpers;
using Crypto.Core;
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

        [DisplayName("Capture Events Count")]
        public int CaptureEventsCount { get; set; } = 5000;

        [DisplayName("Capture Data")]
        public bool CaptureData { get; set; } = true;

        public static TickerDataCaptureStrategy LoadFromFile(string fileName) {
                return (TickerDataCaptureStrategy)SerializationHelper.FromFile(fileName, typeof(TickerDataCaptureStrategy));
        }

        void ISupportSerialization.OnStartSerialize() { }

        public override void Assign(StrategyBase from) {
            base.Assign(from);
            TickerDataCaptureStrategy s = from as TickerDataCaptureStrategy;
            if(s == null)
                return;
            Directory = s.Directory;
            CaptureEventsCount = s.CaptureEventsCount;
        }
        protected override void UpdateTickersList() {
            base.UpdateTickersList();
            for(int i = 0; i < Tickers.Count; i++) {
                Ticker t = Tickers[i];
                t.CaptureData = CaptureData;
                t.CaptureDirectory = Directory;
                t.CaptureDataHistory.SaveCount = CaptureEventsCount;

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