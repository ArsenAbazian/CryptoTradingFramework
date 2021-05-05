using CryptoMarketClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tinkoff.Trading.OpenApi.Models;

namespace Crypto.Core.Exchanges.Tinkoff {
    public class TinknoffInvestTicker : Ticker {
        public TinknoffInvestTicker(Exchange e, MarketInstrument instrument) : base(e) {
            Instrument = instrument;
        }

        
        public MarketInstrument Instrument { get; set; }

        public override string CurrencyPair { get => Instrument.Ticker + Instrument.Type.ToString(); set { } }
        public override string BaseCurrency { get => Instrument.Type.ToString(); set { } }
        public override string MarketCurrency { get => Instrument.Ticker; set { } }

        public override string Name => Instrument.Ticker;
        public string Description { get { return Instrument.Name; } }   

        public override double Fee { get { return 0; } set { } }

        public override string HostName => "";

        public override string WebPageAddress => "";
    }
}
