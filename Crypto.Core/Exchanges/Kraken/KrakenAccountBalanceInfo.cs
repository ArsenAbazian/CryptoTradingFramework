using Crypto.Core.Common;
using Crypto.Core.Exchanges.Base;

namespace Crypto.Core.Exchanges.Kraken {
    public class KrakenAccountBalanceInfo : BalanceBase {
        public KrakenAccountBalanceInfo(AccountInfo info, CurrencyInfo currency) : base(info, currency) { 
        }
        public string AltName { get { return ((KrakenCurrencyInfo)CurrencyInfo)?.AltName; } }
        public override string DisplayName => AltName;
    }
}
