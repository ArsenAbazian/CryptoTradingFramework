using Crypto.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Bittrex {
    public class BittrexAccountBalanceInfo : BalanceBase {
        public BittrexAccountBalanceInfo(AccountInfo info) : base(info) { }

        public double Pending { get; set; }
        public bool Requested { get; set; }
        public string Uuid { get; set; }
    }
}
