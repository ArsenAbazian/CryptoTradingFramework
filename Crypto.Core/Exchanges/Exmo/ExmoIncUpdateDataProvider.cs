using Crypto.Core.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Exmo {
    public class ExmoIncUpdateDataProvider : IIncrementalUpdateDataProvider {
        void IIncrementalUpdateDataProvider.ApplySnapshot(JObject jObject, Ticker ticker) {
            throw new NotImplementedException();
        }

        void IIncrementalUpdateDataProvider.ApplySnapshot(JsonHelperToken root, Ticker ticker) {
            ticker.OrderBook.BeginUpdate();
            try {
                ticker.OrderBook.IsDirty = true;
                var bids = ticker.OrderBook.Bids;
                var asks = ticker.OrderBook.Asks;

                var data = root.GetProperty("data");
                var jb = data.GetProperty("bid").Items;
                var ja = data.GetProperty("ask").Items;

                for(int i = 0; i < jb.Length; i++) {
                    var item = jb[i].Items;
                    bids.Add(new OrderBookEntry() { ValueString = item[0].Value, AmountString = item[1].Value });
                }
                for(int i = 0; i < ja.Length; i++) {
                    var item = ja[i].Items;
                    asks.Add(new OrderBookEntry() { ValueString = item[0].Value, AmountString = item[1].Value });
                }
            }
            finally {
                ticker.OrderBook.IsDirty = false;
                ticker.OrderBook.EndUpdate();
            }
        }

        void IIncrementalUpdateDataProvider.Update(Ticker ticker, IncrementalUpdateInfo info) {
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
    }
}
