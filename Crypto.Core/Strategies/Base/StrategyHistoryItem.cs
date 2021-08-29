using Crypto.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Strategies {
    public enum StrategyOperation {
        StateChanged,
        MarketBuy,
        MarketSell,
        LimitBuy,
        LimitSell,
        Connect
    }

    [Serializable]
    public class StrategyHistoryItem {
        public LogType Type { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public StrategyOperation Operation { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }

        public double ActualDeposit { get; set; }
        public double BuyDeposit { get; set; }
        public double SellDeposit { get; set; }
    }
}
