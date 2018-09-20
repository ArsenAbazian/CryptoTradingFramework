using CryptoMarketClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Poloniex {
    public class PoloniexAccountBalanceInfo : BalanceBase {
        public PoloniexAccountBalanceInfo(AccountInfo info): base(info) { } 
    }
}
