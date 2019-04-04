using CryptoMarketClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.HitBtc {
    public class HitBtcBalanceInfo : BalanceBase {
        public override string Exchange => "HitBtc";
        public decimal Reserved { get; set; }
    }
}
