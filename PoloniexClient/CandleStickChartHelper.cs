using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoloniexClient {
    public class CandleStickData {
        public DateTime Time { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
    }

    public static class CandleStickChartHelper {
        public static List<CandleStickData> CreateCandleStickData(List<PoloniexTickerHistory> list, long rangeInSeconds) {
            List<CandleStickData> res = new List<CandleStickData>();
            CandleStickData candleItem = null;
            long maxTickCount = rangeInSeconds * TimeSpan.TicksPerSecond;
            foreach(PoloniexTickerHistory item in list) {
                if(candleItem == null || (item.Time.Ticks - candleItem.Time.Ticks > maxTickCount)) {
                    candleItem = new CandleStickData();
                    candleItem.Time = item.Time;
                    candleItem.Open = item.Current;
                    candleItem.Low = candleItem.High = candleItem.Close = item.Current;
                    res.Add(candleItem);
                    continue;
                }
                candleItem.Close = item.Current;
                candleItem.Low = Math.Min(candleItem.Low, item.Current);
                candleItem.High = Math.Max(candleItem.High, item.Current);
            }
            return res;
        }
        public static void AddCandleStickData(List<CandleStickData> list, PoloniexTickerHistory item, long rangeInSeconds) {
            CandleStickData candleItem = null;
            long maxTickCount = rangeInSeconds * TimeSpan.TicksPerSecond;
            if(list.Count == 0 || (item.Time.Ticks - list[list.Count - 1].Time.Ticks) > maxTickCount) {
                candleItem = new CandleStickData();
                candleItem.Open = candleItem.Close = candleItem.High = candleItem.Low = item.Current;
                candleItem.Time = item.Time;
                list.Add(candleItem);
                return;
            }
            candleItem = list[list.Count - 1];
            candleItem.Close = item.Current;
            candleItem.Low = Math.Min(candleItem.Low, item.Current);
            candleItem.High = Math.Max(candleItem.High, item.Current);
            return;
        }
    }
}
