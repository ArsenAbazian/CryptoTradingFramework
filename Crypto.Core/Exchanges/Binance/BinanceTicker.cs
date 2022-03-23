using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crypto.Core.Common;

namespace Crypto.Core.Binance {
    public class BinanceTicker : Ticker {
        public BinanceTicker() : this(null) { }
        public BinanceTicker(BinanceExchange exchange) : base(exchange) {
            // https://github.com/binance-exchange/binance-official-api-docs/blob/master/web-socket-streams.md
            // Receiving an event that removes a price level that is not in your local order book can happen and is normal.
            OrderBook.EnableValidationOnRemove = false;
        }

        string currensyPair;
        public override string CurrencyPair {
            get { return currensyPair; }
            set { currensyPair = value; }
        }

        public override bool IsListeningOrderBook {
            get { return IsOrderBookSubscribed && Exchange.GetOrderBookSocketState(this) == SocketConnectionState.Connected; }
        }
        public override bool IsListeningTradingHistory {
            get { return IsTradeHistorySubscribed && Exchange.GetTradingHistorySocketState(this) == SocketConnectionState.Connected; }
        }
        public override bool IsListeningKline {
            get { return IsTradeHistorySubscribed && Exchange.GetKlineSocketState(this) == SocketConnectionState.Connected; }
        }

        public override string Name => CurrencyPair;

        public override string MarketName { get { return Name; } set { } }

        public override double Fee { get { return 0.1f * 0.01f; } set { } }
        
        public override string WebPageAddress => "https://www.binance.com/trade.html?symbol=" + MarketCurrency + "_" + BaseCurrency;
    }
}
