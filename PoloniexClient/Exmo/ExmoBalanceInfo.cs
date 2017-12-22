using CryptoMarketClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Exmo {
    public class ExmoBalanceInfo : BalanceBase {
        public override string Exchange => "Exmo";
        public decimal Reserved { get; set; }
    }
}
