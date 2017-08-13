using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Poloniex {
    public class PoloniexAccountBalanceInfo {
        public string Currency { get; set; }
        public double Available { get; set; }
        public double OnOrders { get; set; }
        public double BtcValue { get; set; }
        public string DepositAddress { get; set; }
    }
}
