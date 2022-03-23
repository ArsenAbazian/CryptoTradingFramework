using Crypto.Core.Exchanges.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Kraken {
    public class KrakenCurrencyInfo : CurrencyInfo {
        public KrakenCurrencyInfo(Exchange e, string currency) : base(e, currency) { }
        
        public string AltName { get; set; }
        protected override string MethodCurrency => AltName;
    }
}
