using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Bitmex {
    public class BitmexTicker : Ticker {
        public BitmexTicker() : this(null) { }
        public BitmexTicker(Exchange exchange) : base(exchange) {
            QuantityFilter = new TickerFilter() { MinValue = 1, TickSize = 1 };
        }

        public double TickSize { get; set; }
        public DateTime Timestamp { get; set; }

        public override string CurrencyPair { get; set; }

        public override string Name => CurrencyPair;

        public override string MarketName { get => CurrencyPair; set => CurrencyPair = value; }

        public override double Fee { get; set; } = 0.0075;

        public override string WebPageAddress => "www.bitmex.com";

        public override bool IsListeningOrderBook {
            get { return IsOrderBookSubscribed && Exchange.GetOrderBookSocketState(this) == SocketConnectionState.Connected; }
        }
        public override bool IsListeningTradingHistory {
            get { return IsTradeHistorySubscribed && Exchange.GetTradingHistorySocketState(this) == SocketConnectionState.Connected; }
        }
        public override bool IsListeningKline {
            get { return IsTradeHistorySubscribed && Exchange.GetKlineSocketState(this) == SocketConnectionState.Connected; }
        }
        protected override Ticker FindUsdTicker() {
            string name = Name.Substring(0, 3) + "USD";
            return Exchange.Tickers.FirstOrDefault(t => t.Name == name);
        }
    }
}
