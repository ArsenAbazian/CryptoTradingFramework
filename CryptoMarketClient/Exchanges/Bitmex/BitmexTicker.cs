using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Exchanges.Bitmex {
    public class BitmexTicker : Ticker {
        public BitmexTicker(Exchange exchange) : base(exchange) { }

        public double TickSize { get; set; }
        public DateTime Timestamp { get; set; }

        public override string CurrencyPair { get; set; }

        public override string Name => CurrencyPair;

        public override double Fee { get; set; }

        public override string HostName => Exchange.Name;

        public override string WebPageAddress => throw new NotImplementedException();
    }
}
