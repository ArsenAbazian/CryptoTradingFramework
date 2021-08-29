using Crypto.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Common {
    public class GrabDataSettings {
        public DateTime StarTime { get; set; }
        public bool GrabTradeHistory { get; set; }
        public bool GrabChart { get; set; }
        public string DirectoryName { get; set; }
        public Ticker Ticker { get; set; }
        public string FileName { get { return DirectoryName + "\\" + Ticker.Name.ToLower() + ".xml"; } }
        [XmlArray("CandleStickData"), XmlArrayItem(typeof(CandleStickData), ElementName = "CandleStickData")]
        public ResizeableArray<CandleStickData> CandleStickData { get; set; }
        [XmlArray("TradeData"), XmlArrayItem(typeof(TradeInfoItem), ElementName = "TradeHistoryItem")]
        public ResizeableArray<TradeInfoItem> TradeData { get; set; }
    }
}
