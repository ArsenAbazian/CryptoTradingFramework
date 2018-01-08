using System;
using System.Collections.Generic;
using System.Linq;
using CryptoMarketClient.Common;

namespace CryptoMarketClient.Poloniex {
    public class PoloniexAccountBalanceInfo : BalanceBase {
        public PoloniexAccountBalanceInfo(string ticker)
            : base(ticker) {
        }

        public override string Exchange => "Poloniex";
    }

    public class PoloniexAccountBalanceInfoList : CachedList<string, PoloniexAccountBalanceInfo> {
        public PoloniexAccountBalanceInfoList() {
        }

        protected override string GetKey(PoloniexAccountBalanceInfo item) {
            return item.CurrencyTicker;
        }
    }
}
