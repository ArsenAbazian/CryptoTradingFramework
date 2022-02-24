using Crypto.Core.Exchanges.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Kraken {
    public class KrakenCurrencyInfo : CurrencyInfoBase {
        public KrakenCurrencyInfo(string currency) : base(currency) { }
        public List<KrakenCurrencyMethod> Methods { get; } = new List<KrakenCurrencyMethod>(3);
    }

    public class KrakenCurrencyMethod {
        public string Name { get; internal set; }
        public bool Limit { get; internal set; }
        public double Fee { get; internal set; }
        public bool GenAddress { get; internal set; }
    }
}
