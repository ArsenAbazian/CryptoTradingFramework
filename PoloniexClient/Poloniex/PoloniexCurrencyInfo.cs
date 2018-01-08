using System;
using System.Linq;
using CryptoMarketClient.Common;

namespace CryptoMarketClient.Poloniex {
    public class PoloniexCurrencyInfo {
        public string Currency { get; set; }
        public double MaxDailyWithdrawal { get; set; }
        public double TxFee { get; set; }
        public double MinConfirmation { get; set; }
        public bool Disabled { get; set; }
    }

    public class PoloniexCurrencyInfoList : CachedList<string, PoloniexCurrencyInfo> {
        public PoloniexCurrencyInfoList() {
        }

        protected override string GetKey(PoloniexCurrencyInfo item) {
            return item.Currency;
        }
    }
}
