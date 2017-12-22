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
        public decimal Bid { get; set; }
        [XtraSerializableProperty]
        public decimal Ask { get; set; }
        [XtraSerializableProperty]
        public decimal Current { get; set; }

        public OrderBookEntry[] Bids { get; set; }
        public OrderBookEntry[] Asks { get; set; }
        public OrderBookStatisticItem OrderBookInfo { get; set; }
        public TradeStatisticsItem TradeInfo { get; set; }
        public ArbitrageStatisticsItem ArbitrageInfo { get; set; }
    }

    public class TradeStatisticsItem {
        public DateTime Time { get; set; }
        public decimal MinBuyPrice { get; set; }
        public decimal MaxBuyPrice { get; set; }
        public decimal BuyAmount { get; set; }
        public decimal BuyVolume { get; set; }
        public decimal MinSellPrice { get; set; }
        public decimal MaxSellPrice { get; set; }
        public decimal SellAmount { get; set; }
        public decimal SellVolume { get; set; }
    }
}
