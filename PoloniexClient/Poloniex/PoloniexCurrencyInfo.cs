using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Poloniex {
    public class PoloniexCurrencyInfo {
        public string Currency { get; set; }
        public double MaxDailyWithdrawal { get; set; }
        public double TxFee { get; set; }
        public double MinConfirmation { get; set; }
        public bool Disabled { get; set; }
    }
}
