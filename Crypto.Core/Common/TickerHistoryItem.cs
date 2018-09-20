using CryptoMarketClient.Common;
using DevExpress.Utils.Serializing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public class TickerHistoryItem {
        [XtraSerializableProperty]
        public DateTime Time { get; set; }
        [XtraSerializableProperty]
        public double Bid { get; set; }
        [XtraSerializableProperty]
        public double Ask { get; set; }
        [XtraSerializableProperty]
        public double Current { get; set; }

        public OrderBookEntry[] Bids { get; set; }
        public OrderBookEntry[] Asks { get; set; }
        public OrderBookStatisticItem OrderBookInfo { get; set; }
        public TradeStatisticsItem TradeInfo { get; set; }
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
