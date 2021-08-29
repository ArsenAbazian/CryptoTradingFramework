using Crypto.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Poloniex {
    public class PoloniexAccountBalanceInfo : BalanceBase {
        public PoloniexAccountBalanceInfo(AccountInfo info): base(info) { } 
    }
}
