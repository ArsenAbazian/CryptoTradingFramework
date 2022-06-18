using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Exmo {
    public class ExmoTicker : Ticker {
        public ExmoTicker() : base() {
        }
        public ExmoTicker(Exchange exchange) : base(exchange) {
        }

        public override string CurrencyPair { get; set; }

        public override string Name => CurrencyPair;

        public override double Fee { get; set; }

        public override string WebPageAddress => string.Format("https://exmo.me/trade/{0}_{1}", MarketCurrency, BaseCurrency);

        internal bool IsListeningOrderBookCore { get; set; }
        internal bool IsListeningTradingHistoryCore { get; set; }

        public override bool IsListeningOrderBook => IsListeningOrderBookCore;
        public override bool IsListeningTradingHistory => IsListeningTradingHistoryCore;
    }
}
