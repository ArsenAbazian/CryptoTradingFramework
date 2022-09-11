using Crypto.Core.Helpers;
using Crypto.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlSerialization;

namespace Crypto.Core.Exchanges.Base {
    public class TickerCaptureDataInfo {
        public CaptureStreamType StreamType { get; set; }
        public CaptureMessageType MessageType { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
        public bool DataValid { get; set; }
    }

    public enum CaptureStreamType {
        OrderBook,
        TradeHistory,
        KLine
    }
    public enum CaptureMessageType {
        Incremental,
        Snapshot
    }

    [Serializable]
    public class TickerCaptureData : ISupportSerialization {
        public ExchangeType Exchange { get; set; }
        public string TickerName { get; set; }
        public List<TickerCaptureDataInfo> Items { get; set; } = new List<TickerCaptureDataInfo>();
        public string FileName { get; set; }
        public int SaveCount { get; set; } = 5000;
        

        public static TickerCaptureData FromFile(string fileName) {
            TickerCaptureData res = (TickerCaptureData)SerializationHelper.Current.FromFile(fileName, typeof(TickerCaptureData));
            return res;
        }
        public bool Save(string path) {
            return SerializationHelper.Current.Save(this, GetType(), path);
        }

        void ISupportSerialization.OnBeginSerialize() { }
        void ISupportSerialization.OnEndSerialize() { }
        void ISupportSerialization.OnBeginDeserialize() { }
        void ISupportSerialization.OnEndDeserialize() { }

        public bool Load(string simulationDataFile) {
            string destDirectory = Path.GetDirectoryName(simulationDataFile);
            string simulationXml = simulationDataFile.Replace(".zip", ".xml");
            try {
                if(File.Exists(simulationXml))
                    File.Delete(simulationXml);
                ZipFile.ExtractToDirectory(simulationDataFile, destDirectory);
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }

            try {
                TickerCaptureData data = TickerCaptureData.FromFile(simulationXml);
                Exchange = data.Exchange;
                TickerName = data.TickerName;
                Items = data.Items;
                File.Delete(simulationXml);
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
            this.enumerator = null;
            return true;
        }

        protected void ResetItemsEnumerator() {
            enumerator = null;
        }

        IEnumerator<TickerCaptureDataInfo> enumerator;
        protected IEnumerator<TickerCaptureDataInfo> Enumerator {
            get {
                if(enumerator == null) {
                    enumerator = Items.GetEnumerator();
                    enumerator.MoveNext();
                }
                return enumerator;
            }
        }

        public TickerCaptureDataInfo CurrentItem {
            get {
                return Enumerator.Current;
            }
        }
        public bool MoveNext() {
            return Enumerator.MoveNext();
        }
    }
}
