using Crypto.Core.Strategies;
using Crypto.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XmlSerialization;

namespace Crypto.Core.Helpers {
    public static class Utils {
        public static bool DownloadCandlesticksHistory(Exchange e, string[] baseCurrFilter, string[] marketCurrency, int candleStickPeriodMin) {
            if(!e.Connect())
                return false;
            foreach(string baseCurr in baseCurrFilter) {
                List<Ticker> tickers = e.Tickers.Where(t => t.BaseCurrency == baseCurr && (marketCurrency == null || marketCurrency.Contains(t.MarketCurrency))).ToList();
                foreach(Ticker ticker in tickers) {
                    ResizeableArray<CandleStickData> data = DownloadCandleStickData(ticker, candleStickPeriodMin);
                    if(data == null)
                        return false;
                }
            }
            return true;
        }

        public static ResizeableArray<CandleStickData> DownloadCandleStickData(Ticker ticker, int candleStickPeriodMin) {
            return DownloadCandleStickData(new TickerInputInfo() { Exchange = ticker.Exchange.Type, Ticker = ticker, TickerName = ticker.Name,  KlineIntervalMin = candleStickPeriodMin });
        }

        public static ResizeableArray<CandleStickData> DownloadCandleStickData(TickerInputInfo info) {
            CachedCandleStickData savedData = new CachedCandleStickData() { Exchange = info.Exchange, TickerName = info.TickerName, IntervalMin = info.KlineIntervalMin };
            CachedCandleStickData cachedData = CachedCandleStickData.FromFile(CachedCandleStickData.Directory + "\\" + ((ISupportSerialization)savedData).FileName);
            if(cachedData != null)
                return cachedData.Items;

            DateTime start = DateTime.UtcNow;
            int intervalInSeconds = info.KlineIntervalMin * 60;
            int candleStickCount = 10000;
            start = start.AddSeconds(-intervalInSeconds * candleStickCount);
            ResizeableArray<CandleStickData> res = new ResizeableArray<CandleStickData>();
            List<ResizeableArray<CandleStickData>> splitData = new List<ResizeableArray<CandleStickData>>();
            int deltaCount = 500;

            for(int i = 0; i < candleStickCount / deltaCount; i++) {
                ResizeableArray<CandleStickData> data = info.Ticker.GetCandleStickData(info.KlineIntervalMin, start, intervalInSeconds * deltaCount);
                if(data == null || data.Count == 0)
                    continue;
                res.AddRange(data);
                Thread.Sleep(300);
                start = start.AddSeconds(intervalInSeconds * deltaCount);
            }
            cachedData = savedData;
            cachedData.Items = res;
            cachedData.Save();
            return res;
        }

        public static ResizeableArray<TradeInfoItem> DownloadTradeHistory(Ticker ticker, DateTime time) {
            return ticker.Exchange.GetTrades(ticker, time);
        }
    }

    public static class CryptoStringFormatExtension {
        public static string ToCryptoString(this double value) {
            return value.ToString("0.00000000");
        }
    }
}
