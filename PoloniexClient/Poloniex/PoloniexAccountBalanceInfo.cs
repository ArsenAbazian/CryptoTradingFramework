using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Poloniex {
    public class PoloniexAccountBalanceInfo {
        public string Currency { get; set; }
        public decimal Available { get; set; }
        public decimal OnOrders { get; set; }
        public decimal BtcValue { get; set; }
        public string DepositAddress { get; set; }
    }
}
