using Crypto.Core.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.BitFinex {
    public class BitFinexIncrementalUpdateDataProvider : IIncrementalUpdateDataProvider {
        public void Update(Ticker ticker, IncrementalUpdateInfo info) {
            List<OrderBookEntry> bids = ticker.OrderBook.Bids;

            OrderBook orderBook = ticker.OrderBook;
            if(info.BidsUpdates != null) {
                for(int i = 0; i < info.BidsUpdates.Count; i++) {
                    string[] item = info.BidsUpdates[i];
                    orderBook.ApplyIncrementalUpdate(OrderBookEntryType.Bid, item[0], item[1]);
                }
            }
            if(info.AsksUpdates != null) {
                for(int i = 0; i < info.AsksUpdates.Count; i++) {
                    string[] item = info.AsksUpdates[i];
                    orderBook.ApplyIncrementalUpdate(OrderBookEntryType.Ask, item[0], item[1]);
                }
            }
            ticker.OnApplyIncrementalUpdate();
        }
        public void ApplySnapshot(JObject jObject, Ticker ticker) {
            throw new NotImplementedException();
        }
        public void ApplySnapshot(JsonHelperToken root, Ticker ticker) {
            ticker.OrderBook.BeginUpdate();
            try {
                ticker.OrderBook.IsDirty = true;
                int ic = root.Items[1].ItemsCount;
                var items = root.Items[1].Items;
                var bids = ticker.OrderBook.Bids;
                var asks = ticker.OrderBook.Asks;
                for(int i = 0; i < ic; i++) {
                    var item = items[i].Items;
                    if(item[2].Value[0] == '-')
                        asks.Add(new OrderBookEntry() { ValueString = item[0].Value, AmountString = item[2].Value.Substring(1) });
                    else
                        bids.Add(new OrderBookEntry() { ValueString = item[0].Value, AmountString = item[2].Value });
                }
            }
            finally {
                ticker.OrderBook.IsDirty = false;
                ticker.OrderBook.EndUpdate();
            }
        }
    }
}
