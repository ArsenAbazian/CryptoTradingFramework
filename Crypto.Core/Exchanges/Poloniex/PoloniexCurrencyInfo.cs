using Crypto.Core.Exchanges.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Poloniex {
    public class PoloniexCurrencyInfo : CurrencyInfo {
        public PoloniexCurrencyInfo(Exchange e, string currency) : base(e, currency) { }
    }
}
