using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Poloniex {
    public class PoloniexAccountBalanceInfo {
        public string Currency { get; set; }
        public decimal Available { get; set; }
        public decimal LastAvailable { get; set; }
        public decimal OnOrders { get; set; }
        public decimal BtcValue { get; set; }
        public string DepositAddress { get; set; }
        public double DepositChanged {
            get {
                double max = Math.Max(Available, LastAvailable);
                double delta = Math.Abs(Available - LastAvailable);
                if(max == 0)
                    return 0;
                return (delta / max);
            }
        }
    }
}
