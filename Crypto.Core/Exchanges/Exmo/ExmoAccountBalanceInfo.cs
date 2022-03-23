using Crypto.Core.Common;
using Crypto.Core.Exchanges.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Exmo {
    public class ExmoAccountBalanceInfo : BalanceBase {
        public ExmoAccountBalanceInfo(AccountInfo account, CurrencyInfo currency) : base(account, currency) {

        }
    }
}
