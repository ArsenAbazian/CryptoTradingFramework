using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public class CandleStickData : INotifyPropertyChanged {
        DateTime time;
        public DateTime Time {
            get { return time; }
            set {
                if(Time == value)
                    return;
                time = value;
                RaisePropertyChanged("Time");
            }
        }
        double open;
        public double Open {
            get { return open; }
            set {
                if(Open == value)
                    return;
                open = value;
                RaisePropertyChanged("Open");
            }
        }

        double close;
        public double Close {
            get { return close; }
            set {
                if(Close == value)
                    return;
                close = value;
                RaisePropertyChanged("Close");
            }
        }

        double high;
        public double High {
            get { return high; }
            set {
                if(High == value)
                    return;
                high = value;
                RaisePropertyChanged("High");
            }
        }

        double low;
        public double Low {
            get { return low; }
            set {
                if(Low == value)
                    return;
                low = value;
                RaisePropertyChanged("Low");
            }
        }

        double volume;
        public double Volume {
            get { return volume; }
            set {
                if(Volume == value)
                    return;
                volume = value;
                RaisePropertyChanged("Volume");
            }
        }

        public double BuySellVolume { get; set; }

        double quoteVolume;
        public double QuoteVolume {
            get { return quoteVolume; }
            set {
                if(QuoteVolume == value)
                    return;
                quoteVolume = value;
                RaisePropertyChanged("QuoteVolume");
            }
        }

        double weightedAverage;
        public double WeightedAverage {
            get { return volume; }
            set {
                if(WeightedAverage == value)
                    return;
                weightedAverage = value;
                RaisePropertyChanged("WeightedAverage");
            }
        }

        double buyVolume;
        public double BuyVolume {
            get {
                return buyVolume;
            }
            set {
                if(BuyVolume == value)
                    return;
                buyVolume = value;
                RaisePropertyChanged("BuyVolume");
            }
        }


        double sellVolume;
        public double SellVolume {
            get {
                return sellVolume;
            }
            set {
                if(SellVolume == value)
                    return;
                sellVolume = value;
                RaisePropertyChanged("SellVolume");
            }
        }

        event PropertyChangedEventHandler propertyChanged;
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged {
            add { this.propertyChanged += value; }
            remove { this.propertyChanged -= value; }
        }

        protected void RaisePropertyChanged(string propName) {
            if (this.propertyChanged != null) {
                try {
                    this.propertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
                } catch { }
            }
        }
    }

    public static class CandleStickChartHelper {
        public static void InitializeVolumes(IList<CandleStickData> candles, List<TradeInfoItem> trades, int period) {
            foreach(CandleStickData data in candles) {
                data.BuyVolume = 0;
                data.SellVolume = 0;
            }
            UpdateVolumes(candles, trades, period);
        }
        public static void UpdateVolumes(IList<CandleStickData> candles, List<TradeInfoItem> trades, int period) {
            CandleStickData saved = null;
            foreach(var trade in trades) {
                if(saved == null || saved.Time > trade.Time || saved.Time.AddMinutes(period) <= saved.Time) {
                    saved = candles.FirstOrDefault(c => c.Time <= trade.Time & c.Time.AddMinutes(period) > trade.Time);
                }
                if(saved != null) {
                    UpdateCandle(saved, trade);
                }
            }
        }
        public static void UpdateCandle(CandleStickData candle, TradeInfoItem trade) {
            if(trade.Type == TradeType.Buy)
                candle.BuyVolume += trade.Amount;
            else
                candle.SellVolume += trade.Amount;
            candle.BuySellVolume = candle.BuyVolume - candle.SellVolume;
        }
        public static void UpdateVolumes(BindingList<CandleStickData> candles, TradeInfoItem trade, int period) {
            if(candles.Count == 0)
                return;
            CandleStickData saved = candles.Last();
            if(saved.Time > trade.Time || saved.Time.AddMinutes(period) <= saved.Time) {
                UpdateCandle(saved, trade);
                return;
            }
            saved = candles.FirstOrDefault(c => c.Time <= trade.Time & c.Time.AddMinutes(period) > trade.Time);
            UpdateCandle(saved, trade);
        }
        public static BindingList<CandleStickData> CreateCandleStickData(IList<TickerHistoryItem> list, long rangeInSeconds) {
            BindingList<CandleStickData> res = new BindingList<CandleStickData>();
            CandleStickData candleItem = null;
            long maxTickCount = rangeInSeconds * TimeSpan.TicksPerSecond;
            foreach(TickerHistoryItem item in list) {
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
        public static void AddCandleStickData(IList<CandleStickData> list, TickerHistoryItem item, long rangeInSeconds) {
            if(list == null)
                return;
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
        
        //public static IList<CandleStickData> CreateCandleStickData(Ticker ticker) {
        //    ticker.CandleStickData.Clear();
        //    return CreateCandleStickData(ticker.History, ticker.CandleStickPeriodMin * 60);
        //}
    }
}
