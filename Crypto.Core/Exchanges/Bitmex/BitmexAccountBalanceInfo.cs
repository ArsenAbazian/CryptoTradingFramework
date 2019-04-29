using CryptoMarketClient;
using CryptoMarketClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Bitmex {
    public class BitmexAccountBalanceInfo : BalanceBase {
        public BitmexAccountBalanceInfo(AccountInfo account) : base(account) { }
    }
}
