using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Exchanges.Bittrex {
    public class BittrexIncrementalUpdateDataProvider : IIncrementalUpdateDataProvider {
        public void Update(Ticker ticker, IncrementalUpdateInfo info) {
            foreach(string[] item in info.BidsUpdates)
                ticker.OrderBook.ApplyIncrementalUpdate(OrderBookEntryType.Bid, item[1], item[2]);
            foreach(string[] item in info.AsksUpdates)
                ticker.OrderBook.ApplyIncrementalUpdate(OrderBookEntryType.Ask, item[1], item[2]);
            foreach(string[] item in info.TradeUpdates) {
                TradeInfoItem trade = new TradeInfoItem(null, ticker) {
                    Type = item[0][0] == 'S' ? TradeType.Sell : TradeType.Buy,
                    RateString = item[1],
                    AmountString = item[2],
                    Time = new DateTime(Convert.ToInt64(item[3])).ToLocalTime()};
                ticker.TradeHistory.Insert(0, trade);
                CandleStickChartHelper.UpdateVolumes(ticker.CandleStickData, trade, ticker.CandleStickPeriodMin);
            }
            ticker.OnApplyIncrementalUpdate();
        }
        public void ApplySnapshot(Dictionary<string, object> jObject, Ticker ticker) {
            ticker.OrderBook.Clear();
            OrderBook orderBook = ticker.OrderBook;

            List<object> jbids = (List<object>)jObject["Z"];
            List<object> jasks = (List<object>)jObject["S"];

            List<OrderBookEntry> entries = orderBook.Asks;
            List<OrderBookEntry> entriesInverted = orderBook.AsksInverted;
            foreach(Dictionary<string, object> item in jasks) {
                entries.Add(new OrderBookEntry() { ValueString = (string)item["R"], AmountString = (string)item["Q"] });
                entriesInverted.Insert(0, new OrderBookEntry() { ValueString = (string)item["R"], AmountString = (string)item["Q"] });
            }

            entries = orderBook.Bids;
            foreach(Dictionary<string, object> item in jbids)
                entries.Add(new OrderBookEntry() { ValueString = (string)item["R"], AmountString = (string)item["Q"] });

            orderBook.UpdateEntries();
            orderBook.RaiseOnChanged(new IncrementalUpdateInfo());

            ticker.TradeHistory.Clear();
            List<object> jtrades = (List<object>)jObject["f"];
            foreach(Dictionary<string, object> item in jtrades) {
                TradeInfoItem t = new TradeInfoItem(null, ticker);
                t.AmountString = (string)item["Q"];
                t.RateString = (string)item["P"];
                t.Time = new DateTime(Convert.ToInt64((string)item["T"])).ToLocalTime();
                t.TimeString = t.Time.ToLongTimeString();
                t.Total = t.Rate * t.Amount;
                t.Type = ((string)item["OT"]) == "BUY"? TradeType.Buy: TradeType.Sell;
                ticker.TradeHistory.Add(t);
            }

            ticker.RaiseTradeHistoryAdd();
        }
    }
}
