using Crypto.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Kraken {
    public class KrakenAccountBalanceInfo : BalanceBase {
        public KrakenAccountBalanceInfo(AccountInfo info) : base(info) { }
    }
}
