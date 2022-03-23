using Crypto.Core;
using Crypto.Core.Common;
using Crypto.Core.Exchanges.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Binance {
    public class BinanceAccountBalanceInfo : BalanceBase {
        public BinanceAccountBalanceInfo(AccountInfo info, CurrencyInfo currency) : base(info, currency) { }
    }
}
