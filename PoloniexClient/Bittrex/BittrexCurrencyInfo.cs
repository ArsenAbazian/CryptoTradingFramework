using System;
using System.Linq;
using CryptoMarketClient.Common;

namespace CryptoMarketClient.Bittrex {
    public class BittrexCurrencyInfo {
        public string CurrencyTicker { get; set; }
        public string CurrencyName { get; set; }
        public int MinConfirmation { get; set; }
        public double TxFree { get; set; }
        public bool IsActive { get; set; }
        public string CoinType { get; set; }
        public string BaseAddress { get; set; }
    }

    public class BittrexCurrencyInfoList : CachedList<string, BittrexCurrencyInfo> {
        public BittrexCurrencyInfoList() {
        }

        protected override string GetKey(BittrexCurrencyInfo item) {
            return item.CurrencyTicker;
        }
    }
}
