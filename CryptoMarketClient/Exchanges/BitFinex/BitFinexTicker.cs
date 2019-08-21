using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoMarketClient.Common;

namespace CryptoMarketClient.BitFinex {
    public class BitFinexTicker : Ticker {
        public BitFinexTicker(BitFinexExchange exchange) : base(exchange) { }

        public override string CurrencyPair { get; set; }

        public override string Name => CurrencyPair;

        public override double Fee { get { return 0.001; } set { } }

        public override string HostName => "BitFinex";

        public override string WebPageAddress => "https://www.bitfinex.com/t/" + MarketCurrency + ":" + BaseCurrency;

        public override bool UpdateMyTrades() {
            return true;
        }
    }
}
