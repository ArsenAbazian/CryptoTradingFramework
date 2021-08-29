using Crypto.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.BitFinex {
    public class BitFinexOrderInfo : OpenedOrderInfo {
        public BitFinexOrderInfo(AccountInfo account, Ticker ticker) : base(account, ticker) { }
    }
}
