using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CryptoMarketClient.Common {
    public class GrabDataSettings {
        public DateTime StarTime { get; set; }
        public bool GrabTradeHistory { get; set; }
        public bool GrabChart { get; set; }
        public string DirectoryName { get; set; }
        public Ticker Ticker { get; set; }
        public string FileName { get { return DirectoryName + "\\" + Ticker.Name.ToLower() + ".xml"; } }
        [XmlArray("CandleStickData"), XmlArrayItem(typeof(CandleStickData), ElementName = "CandleStickData")]
        public BindingList<CandleStickData> CandleStickData { get; set; }
        [XmlArray("TradeData"), XmlArrayItem(typeof(TradeInfoItem), ElementName = "TradeHistoryItem")]
        public List<TradeInfoItem> TradeData { get; set; }
    }
}
