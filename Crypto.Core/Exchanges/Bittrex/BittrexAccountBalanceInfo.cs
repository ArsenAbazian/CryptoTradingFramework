using Crypto.Core.Common;
using Crypto.Core.Exchanges.Base;

namespace Crypto.Core.Bittrex {
    public class BittrexAccountBalanceInfo : BalanceBase {
        public BittrexAccountBalanceInfo(AccountInfo info, CurrencyInfo currency) : base(info, currency) { }

        public double Pending { get; set; }
        public bool Requested { get; set; }
        public string Uuid { get; set; }
    }
}
