using Crypto.Core.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Binance {
    public class BinanceIncrementalUpdateDataProvider : IIncrementalUpdateDataProvider {
        public void Update(Ticker ticker, IncrementalUpdateInfo info) {
            List<OrderBookEntry> bids = ticker.OrderBook.Bids;

            OrderBook orderBook = ticker.OrderBook;
            for(int i = 0; i < info.BidsUpdates.Count; i++) {
                string[] item = info.BidsUpdates[i];
                orderBook.ApplyIncrementalUpdate(OrderBookEntryType.Bid, item[0], item[1]);
            }
            for(int i = 0; i < info.AsksUpdates.Count; i++) {
                string[] item = info.AsksUpdates[i];
                orderBook.ApplyIncrementalUpdate(OrderBookEntryType.Ask, item[0], item[1]);
            }
            ticker.OnApplyIncrementalUpdate();
        }
        public void ApplySnapshot(JObject jObject, Ticker ticker) {
            
        }
        public void ApplySnapshot(JsonHelperToken root, Ticker ticker) {
            throw new NotImplementedException();
        }
    }
}
