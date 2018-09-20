using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Exchanges.Binance {
    public class BinanceIncrementalUpdateDataProvider : IIncrementalUpdateDataProvider {
        public void Update(Ticker ticker, IncrementalUpdateInfo info) {
            List<OrderBookEntry> bids = ticker.OrderBook.Bids;

            OrderBook orderBook = ticker.OrderBook;
            foreach(string[] item in info.BidsUpdates)
                orderBook.ApplyIncrementalUpdate(OrderBookEntryType.Bid, item[0], item[1]);
            foreach(string[] item in info.AsksUpdates)
                orderBook.ApplyIncrementalUpdate(OrderBookEntryType.Ask, item[0], item[1]);

            ticker.OnApplyIncrementalUpdate();
        }
        public void ApplySnapshot(Dictionary<string, object> jObject, Ticker ticker) {
            
        }
    }
}
