using Crypto.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core {
    [Serializable]
    public class TickerHistoryItem {
        public DateTime Time { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
        public double Current { get; set; }

        [XmlIgnore]
        public OrderBookEntry[] Bids { get; set; }
        [XmlIgnore]
        public OrderBookEntry[] Asks { get; set; }
        [XmlIgnore]
        public OrderBookStatisticItem OrderBookInfo { get; set; }
        [XmlIgnore]
        public TradeStatisticsItem TradeInfo { get; set; }
        [XmlIgnore]
        public ArbitrageStatisticsItem ArbitrageInfo { get; set; }
    }

    public class TradeStatisticsItem {
        public DateTime Time { get; set; }
        public double MinBuyPrice { get; set; }
        public double MaxBuyPrice { get; set; }
        public double BuyAmount { get; set; }
        public double BuyVolume { get; set; }
        public double MinSellPrice { get; set; }
        public double MaxSellPrice { get; set; }
        public double SellAmount { get; set; }
        public double SellVolume { get; set; }
    }
}
