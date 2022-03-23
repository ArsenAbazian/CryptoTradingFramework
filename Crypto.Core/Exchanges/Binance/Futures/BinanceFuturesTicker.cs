using Crypto.Core;
using Crypto.Core.Binance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Binance.Futures {
    public class BinanceFuturesTicker : BinanceTicker {
        public BinanceFuturesTicker(BinanceFuturesExchange exchange) : base(exchange) {
            // https://github.com/binance-exchange/binance-official-api-docs/blob/master/web-socket-streams.md
            // Receiving an event that removes a price level that is not in your local order book can happen and is normal.
            OrderBook.EnableValidationOnRemove = false;
        }
    }
}
