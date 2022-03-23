using Crypto.Core.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Kraken {
    public class KrakenIncrementalUpdateDataProvider : IIncrementalUpdateDataProvider {
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

        public void ApplySnapshot(JsonHelperToken root, Ticker ticker) {
            ticker.OrderBook.BeginUpdate();
            ticker.OrderBook.IsDirty = false;
            try {
                List<OrderBookEntry> bids = ticker.OrderBook.Bids;
                List<OrderBookEntry> asks = ticker.OrderBook.Asks;

                bids.Clear();
                asks.Clear();

                JsonHelperToken[] ja = root.Items[1].Properties[0].Items;
                JsonHelperToken[] jb = root.Items[1].Properties[1].Items;

                for(int i = 0; i < jb.Length; i++)
                    bids.Add(new OrderBookEntry() { ValueString = jb[i].Items[0].Value, AmountString = jb[i].Items[1].Value });

                for(int i = 0; i < ja.Length; i++)
                    asks.Add(new OrderBookEntry() { ValueString = ja[i].Items[0].Value, AmountString = ja[i].Items[1].Value });
            }
            finally {
                ticker.OrderBook.EndUpdate();
            }
        }

        public void ApplySnapshot(JObject jObject, Ticker ticker) {
            ticker.OrderBook.BeginUpdate();
            ticker.OrderBook.IsDirty = false;
            try {
                List<OrderBookEntry> bids = ticker.OrderBook.Bids;
                List<OrderBookEntry> asks = ticker.OrderBook.Asks;

                bids.Clear();
                asks.Clear();

                JArray ja = jObject.Value<JArray>("as");
                JArray jb = jObject.Value<JArray>("bs");

                foreach(JArray item in jb)
                    bids.Add(new OrderBookEntry() { ValueString = item[0].ToString(), AmountString = item[1].ToString() });

                foreach(JArray item in ja)
                    asks.Add(new OrderBookEntry() { ValueString = item[0].ToString(), AmountString = item[1].ToString() });
            }
            finally {
                ticker.OrderBook.EndUpdate();
            }
        }
    }
}
