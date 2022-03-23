using Crypto.Core;
using Crypto.Core.Common;
using Crypto.Core.Exchanges.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Bitmex {
    public class BitmexAccountBalanceInfo : BalanceBase {
        public BitmexAccountBalanceInfo(AccountInfo account, CurrencyInfo currency) : base(account, currency) { }
    }
}
