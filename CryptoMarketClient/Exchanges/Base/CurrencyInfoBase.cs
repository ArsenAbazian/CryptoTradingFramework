using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Exchanges.Base {
    public class CurrencyInfoBase {
        public string Currency { get; set; }
        public double MaxDailyWithdrawal { get; set; }
        public double TxFee { get; set; }
        public double MinConfirmation { get; set; }
        public bool Disabled { get; set; }
        public bool IsActive { get { return !Disabled; } set { Disabled = !value; } }
        public string CurrencyLong { get; set; }
        public string CoinType { get; set; }
        public string BaseAddress { get; set; }
    }
}
