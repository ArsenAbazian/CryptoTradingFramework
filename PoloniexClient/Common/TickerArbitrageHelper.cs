using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public static class TickerArbitrageHelper {
        static List<List<ITicker>> GetMarketsList() {
            List<List<ITicker>> tickersList = new List<List<ITicker>>();
            if(Bittrex.BittrexModel.Default.IsConnected) {
                List<ITicker> list = new List<ITicker>();
                list.AddRange(Bittrex.BittrexModel.Default.Markets);
                tickersList.Add(list);
            }
            if(PoloniexModel.Default.IsConnected) {
                List<ITicker> list = new List<ITicker>();
                list.AddRange(PoloniexModel.Default.Tickers);
                tickersList.Add(list);
            }
            return tickersList;
        }
        public static List<TickerArbitrageInfo> GetArbitrageInfoList() {
            List<TickerArbitrageInfo> arbitrageList = new List<TickerArbitrageInfo>();
            List<List<ITicker>> markets = GetMarketsList();
            foreach(ITicker ticker in markets.First()) {
                TickerArbitrageInfo info = new TickerArbitrageInfo();
                info.BaseCurrency = ticker.BaseCurrency;
                info.MarketCurrency = ticker.MarketCurrency;
                info.Add(ticker);
                bool hasFound = true;
                for(int i = 1; i < markets.Count; i++) {
                    ITicker tt = markets[i].FirstOrDefault((t) => t.BaseCurrency == ticker.BaseCurrency && t.MarketCurrency == ticker.MarketCurrency);
                    if(tt == null) {
                        hasFound = false;
                        break;
                    }
                    info.Add(tt);
                }
                if(!hasFound)
                    continue;
                arbitrageList.Add(info);
            }
            return arbitrageList;
        }
        public static void Update(List<TickerArbitrageInfo> list) {
            foreach(TickerArbitrageInfo info in list)
                info.Update();
        }
    }
}
