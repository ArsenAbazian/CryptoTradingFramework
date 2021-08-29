using Crypto.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Common {
    [Serializable]
    public class TickerNameInfo {
        public ExchangeType Exchange { get; set; }
        public string Ticker { get; set; }
        public string BaseCurrency { get; set; }
        public string MarketCurrency { get; set; }
        public string FullName { get { return Exchange + ": " + Ticker; } }
        public Ticker FindTicker() {
            Exchange e = Crypto.Core.Exchange.Get(Exchange);
            e.Connect();
            return e.Ticker(Ticker);
        }
        public Ticker FindTickerFor(ExchangeType exchange) {
            Exchange e2 = Crypto.Core.Exchange.Get(Exchange);
            if(!e2.Connect())
                return null;
            Ticker t = e2.Ticker(Ticker);
            Exchange e = Crypto.Core.Exchange.Get(exchange);
            if(!e.Connect())
                return null;
            return e.Ticker(t.BaseCurrency, t.MarketCurrency);
        }
        public TickerNameInfo Clone() {
            return new TickerNameInfo() { Exchange = this.Exchange, Ticker = this.Ticker, BaseCurrency = this.BaseCurrency, MarketCurrency = this.MarketCurrency };
        }
        public override string ToString() {
            return Exchange.ToString() + ":" + Ticker;
        }
    }
}
