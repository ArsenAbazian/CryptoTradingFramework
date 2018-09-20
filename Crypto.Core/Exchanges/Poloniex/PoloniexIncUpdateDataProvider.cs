using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Exchanges.Poloniex {
    public class PoloniexIncrementalUpdateDataProvider : IIncrementalUpdateDataProvider {
        public void Update(Ticker ticker, IncrementalUpdateInfo info) {
            foreach(string[] item in info.Updates) {
                if(item[0][0] == 'o') {
                    ticker.OrderBook.ApplyIncrementalUpdate(
                        item[1][0] == '1' ? OrderBookEntryType.Bid : OrderBookEntryType.Ask,
                        item[2],
                        item[3]);
                }
                else if(item[0][0] == 't') {
                    TradeInfoItem trade = new TradeInfoItem(null, ticker) {
                        Type = item[2][0] == '0' ? TradeType.Sell : TradeType.Buy,
                        RateString = item[3],
                        AmountString = item[4],
                        Time = new DateTime(Convert.ToInt64(item[5]))
                        };
                    ticker.TradeHistory.Insert(0, trade);
                    CandleStickChartHelper.UpdateVolumes(ticker.CandleStickData, trade, ticker.CandleStickPeriodMin);
                }
            }
            ticker.OnApplyIncrementalUpdate();
        }
        public void ApplySnapshot(JObject jObject, Ticker ticker) {
            ticker.OrderBook.Clear();
            OrderBook orderBook = ticker.OrderBook;
            JArray ob = jObject.Value<JArray>("orderBook");
            JObject asks = ob[0].Value<JObject>();
            JObject bids = ob[1].Value<JObject>();

            List<OrderBookEntry> entries = orderBook.Asks;
            List<OrderBookEntry> entriesInverted = orderBook.AsksInverted;
            foreach(JProperty item in asks.Children()) {
                entries.Add(new OrderBookEntry() { ValueString = item.Name, AmountString = item.Value.Value<string>() });
                entriesInverted.Insert(0, new OrderBookEntry() { ValueString = item.Name, AmountString = item.Value.Value<string>() });
            }

            entries = orderBook.Bids;
            foreach(JProperty item in bids.Children()) {
                entries.Add(new OrderBookEntry() { ValueString = item.Name, AmountString = item.Value.Value<string>() });
            }

            orderBook.UpdateEntries();
            orderBook.RaiseOnChanged(new IncrementalUpdateInfo());
        }
    }
}
