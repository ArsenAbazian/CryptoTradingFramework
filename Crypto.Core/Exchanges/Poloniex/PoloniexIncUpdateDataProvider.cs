using Crypto.Core.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Poloniex {
    public class PoloniexIncrementalUpdateDataProvider : IIncrementalUpdateDataProvider {
        public void Update(Ticker ticker, IncrementalUpdateInfo info) {
            for(int i = 0; i < info.BidsUpdates.Count; i++)
                ticker.OrderBook.ApplyIncrementalUpdate(OrderBookEntryType.Bid, info.BidsUpdates[i][0], info.BidsUpdates[i][1]);
            for(int i = 0; i < info.AsksUpdates.Count; i++)
                ticker.OrderBook.ApplyIncrementalUpdate(OrderBookEntryType.Ask, info.AsksUpdates[i][0], info.AsksUpdates[i][1]);

            //for(int i = 0; i < info.Updates.Count; i++) {
            //    string[] item = info.Updates[i];
            //    if(item[0][0] == 'o') {
            //        ticker.OrderBook.ApplyIncrementalUpdate(item[1][0] == '1' ? OrderBookEntryType.Bid : OrderBookEntryType.Ask, item[2], item[3]);
            //    }
            //    else if(item[0][0] == 't') {
            //        TradeInfoItem trade = new TradeInfoItem(null, ticker) {
            //                Type = item[2][0] == '0' ? TradeType.Sell : TradeType.Buy, RateString = item[3], AmountString = item[4], Time = ticker.Exchange.FromUnixTimestamp(Convert.ToInt64(item[5]))
            //        };
            //        if(trade.Time.Year == 1) {
            //            throw new InvalidOperationException();
            //        }
            //        ticker.AddTradeHistoryItem(trade);//.InsertTradeHistoryItem(trade);
            //        CandleStickChartHelper.UpdateVolumes(ticker.CandleStickData, trade, ticker.CandleStickPeriodMin);
            //    }
            //}
            ticker.OnApplyIncrementalUpdate();
        }

        public void ApplySnapshot(JObject jObject, Ticker ticker) {
            ticker.OrderBook.BeginUpdate();
            try {
                OrderBook orderBook = ticker.OrderBook;
                ticker.OrderBook.Clear();
                JArray ob = jObject.Value<JArray>("orderBook");
                JObject asks = ob[0].Value<JObject>();
                JObject bids = ob[1].Value<JObject>();

                List<OrderBookEntry> entries = orderBook.Asks;
                lock(entries) {
                    foreach(JProperty item in asks.Children()) {
                        entries.Add(new OrderBookEntry() { ValueString = item.Name, AmountString = item.Value.Value<string>() });
                    }
                }
                entries = orderBook.Bids;
                lock(entries) {
                    foreach(JProperty item in bids.Children()) {
                        entries.Add(new OrderBookEntry() { ValueString = item.Name, AmountString = item.Value.Value<string>() });
                    }
                }
            }
            finally {
                ticker.OrderBook.IsDirty = false;
                ticker.OrderBook.EndUpdate();
            }
        }

        public void ApplySnapshot(JsonHelperToken item, Ticker ticker) {
            long seqNumber = item.GetProperty("id").ValueLong;
            JsonHelperToken[] jb = item.GetProperty("bids").Items;
            JsonHelperToken[] ja = item.GetProperty("asks").Items;

            ticker.OrderBook.BeginUpdate();
            try {
                ticker.OrderBook.IsDirty = true;
                var bids = ticker.OrderBook.Bids;
                var asks = ticker.OrderBook.Asks;

                for(int i = 0; i < jb.Length; i++) {
                    bids.Add(new OrderBookEntry() { ValueString = jb[i].Items[0].Value, AmountString = jb[i].Items[1].Value });
                }

                for(int i = 0; i < ja.Length; i++) {
                    asks.Add(new OrderBookEntry() { ValueString = ja[i].Items[0].Value, AmountString = ja[i].Items[1].Value });
                }
            }
            finally {
                ticker.OrderBook.IsDirty = false;
                ticker.OrderBook.EndUpdate();
            }
        }
    }
}
