using Crypto.Core.Exchanges.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.BitFinex {
    public class BitFinexCurrencyInfo : CurrencyInfo {
        public BitFinexCurrencyInfo(Exchange e, string currency) : base(e, currency) { }
    }
}
