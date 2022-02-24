﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Kraken {
    public class KrakenTicker : Ticker {
        public KrakenTicker() : base() { }
        public KrakenTicker(KrakenExchange e) : base(e) { }

        public override string CurrencyPair { get; set; }

        public override string Name => CurrencyPair;

        public override double Fee { get; set; }

        public override string HostName => "Kraken";

        public override string WebPageAddress => "https://trade.kraken.com/charts/KRAKEN:" + CurrencyPair + "?period=1d";

        string standardName;
        public string StandardName { 
            get { 
                if(standardName == null && MarketCurrency != null)
                    standardName = MarketCurrency + "/" + BaseCurrency;
                return standardName;
            } 
        }
    }
}
