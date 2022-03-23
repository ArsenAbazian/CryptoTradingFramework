using System;
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

        public override string WebPageAddress => "https://trade.kraken.com/charts/KRAKEN:" + CurrencyPair + "?period=1d";

        public override string BaseCurrencyDisplayName { get; set; }
        public override string MarketCurrencyDisplayName { get; set; }

        public string WebSocketName { get; set; }
    }
}
