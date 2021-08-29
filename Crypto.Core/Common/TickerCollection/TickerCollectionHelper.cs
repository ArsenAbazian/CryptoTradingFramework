using Crypto.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core {
    public static class TickerCollectionHelper {
        static List<List<Ticker>> GetMarketsList(List<Exchange> exchanges) {
            List<List<Ticker>> tickersList = new List<List<Ticker>>();
            foreach(Exchange exchange in exchanges) {
                List<Ticker> list = new List<Ticker>();
                list.AddRange(exchange.Tickers);
                tickersList.Add(list);
            }
            return tickersList;
        }
        public static ResizeableArray<TickerCollection> GetItems(List<Exchange> exchanges) {
            ResizeableArray<TickerCollection> arbitrageList = new ResizeableArray<TickerCollection>();
            List<List<Ticker>> markets = GetMarketsList(exchanges);
            if(markets.Count == 0)
                return arbitrageList;
            //string[] marketItems = new string[] { "ETC", "LTC", "ADA", "XRP", "EOS", "NES", "ETH", "BTC", "STR", "XMR", "DASH", "ZEC", "NXT" };
            var mFirst = markets[0];
            for(int mi = 0; mi < mFirst.Count; mi++) {
                Ticker ticker = mFirst[mi];
//if(!marketItems.Contains(ticker.MarketCurrency))
                //    continue;
                //if(ticker.BaseCurrency != "BTC")
                //    continue;
                TickerCollection info = new TickerCollection();
                info.BaseCurrency = ticker.BaseCurrency;
                info.MarketCurrency = ticker.MarketCurrency;
                info.Add(ticker);
                for(int i = 1; i < markets.Count; i++) {
                    Ticker tt = markets[i].FirstOrDefault((t) => t.BaseCurrency == ticker.BaseCurrency && t.MarketCurrency == ticker.MarketCurrency);
                    if(tt == null)
                        continue;
                    info.Add(tt);
                }
                if(info.Count < 2)
                    continue;
                info.UsdTicker = mFirst.FirstOrDefault((t) => t.MarketCurrency == info.BaseCurrency && t.BaseCurrency == "USDT");
                arbitrageList.Add(info);
            }
            for(int ai = 0; ai < arbitrageList.Count; ai++) {
                TickerCollection coll = arbitrageList[ai];
                for(int i = 0; i < coll.Count; i++) {
                    coll.Tickers[i].UpdateMode = TickerUpdateMode.Arbitrage;
                }
            }
            return arbitrageList;
        }
    }
}
