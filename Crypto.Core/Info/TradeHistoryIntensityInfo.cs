using Crypto.Core.Indicators;
using Crypto.Core.Strategies;
using Crypto.Core;
using Crypto.Core.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Helpers {
    public class TradeHistoryIntensityInfo : IStrategyDataItemInfoOwner {
        public TradeHistoryIntensityInfo() {
            InitializeDataItems(HistogrammIntervalSec);
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        int histogrammIntervalSec = 10;
        public int HistogrammIntervalSec {
            get { return histogrammIntervalSec; }
            set {
                if(HistogrammIntervalSec == value)
                    return;
                histogrammIntervalSec = value;
                DataItemInfos.Clear();
                InitializeDataItems(HistogrammIntervalSec);
            }
        }

        public event TickerDownloadProgressEventHandler DownloadProgressChanged;

        private void InitializeDataItems(int intervalSec) {
            StrategyDataItemInfo.DataItem(DataItemInfos, "BaseCurrency").Visibility = DataVisibility.Table;
            StrategyDataItemInfo.DataItem(DataItemInfos, "MarketCurrency").Visibility = DataVisibility.Table;

            StrategyDataItemInfo preview = StrategyDataItemInfo.DataItem(DataItemInfos, "HistogrammPreview"); preview.Color = Color.FromArgb(0x40, Color.Green); preview.Visibility = DataVisibility.Table; preview.MeasureUnit = StrategyDateTimeMeasureUnit.Second; preview.MeasureUnitMultiplier = HistogrammIntervalSec;

            StrategyDataItemInfo candle = StrategyDataItemInfo.CandleStickItem(preview.Children); candle.BindingSource = "Histogramm"; candle.Visibility = DataVisibility.Chart; candle.MeasureUnit = StrategyDateTimeMeasureUnit.Second; candle.MeasureUnitMultiplier = intervalSec;
            StrategyDataItemInfo time = StrategyDataItemInfo.TimeItem(preview.Children, "Time"); time.BindingSource = "Histogramm"; time.Visibility = DataVisibility.Table;

            StrategyDataItemInfo tcount = StrategyDataItemInfo.DataItem(preview.Children, "TradeCount"); tcount.BindingSource = "Histogramm"; tcount.Visibility = DataVisibility.Both; tcount.Color = Color.FromArgb(0x80, Color.Green); tcount.ChartType = ChartType.Bar; tcount.MeasureUnit = StrategyDateTimeMeasureUnit.Second; tcount.MeasureUnitMultiplier = intervalSec; tcount.PanelName = "Trades Counts";
            StrategyDataItemInfo tvolume = StrategyDataItemInfo.DataItem(preview.Children, "TradeVolume"); tvolume.BindingSource = "Histogramm"; tvolume.Visibility = DataVisibility.Both; tvolume.Color = Color.FromArgb(0x40, Color.Green); tvolume.ChartType = ChartType.Bar; tvolume.MeasureUnit = StrategyDateTimeMeasureUnit.Second; tvolume.MeasureUnitMultiplier = intervalSec; tvolume.PanelName = "Trades Volumes"; tvolume.PanelVisible = false;

            StrategyDataItemInfo bcount = StrategyDataItemInfo.DataItem(preview.Children, "BuyCount"); bcount.BindingSource = "Histogramm"; bcount.Visibility = DataVisibility.Both; bcount.Color = Color.FromArgb(0x80, Color.Green); bcount.ChartType = ChartType.Bar; bcount.MeasureUnit = StrategyDateTimeMeasureUnit.Second; bcount.MeasureUnitMultiplier = intervalSec; bcount.PanelName = "Buys/Sells Counts";
            StrategyDataItemInfo scount = StrategyDataItemInfo.DataItem(preview.Children, "SellCount"); scount.BindingSource = "Histogramm"; scount.Visibility = DataVisibility.Both; scount.Color = Color.FromArgb(0x80, Color.Red); scount.ChartType = ChartType.Bar; scount.MeasureUnit = StrategyDateTimeMeasureUnit.Second; scount.MeasureUnitMultiplier = intervalSec; scount.PanelName = "Buys/Sells Counts";

            StrategyDataItemInfo bvolume = StrategyDataItemInfo.DataItem(preview.Children, "BuyVolume"); bvolume.BindingSource = "Histogramm"; bvolume.Visibility = DataVisibility.Both; bvolume.Color = Color.FromArgb(0x40, Color.Green); bvolume.ChartType = ChartType.Bar; bvolume.MeasureUnit = StrategyDateTimeMeasureUnit.Second; bvolume.MeasureUnitMultiplier = intervalSec; bvolume.PanelName = "Buys/Sells Volumes"; bvolume.PanelVisible = false;
            StrategyDataItemInfo svolume = StrategyDataItemInfo.DataItem(preview.Children, "SellVolume"); svolume.BindingSource = "Histogramm"; svolume.Visibility = DataVisibility.Both; svolume.Color = Color.FromArgb(0x40, Color.Red); svolume.ChartType = ChartType.Bar; svolume.MeasureUnit = StrategyDateTimeMeasureUnit.Second; svolume.MeasureUnitMultiplier = intervalSec; svolume.PanelName = "Buys/Sells Volumes"; svolume.PanelVisible = false;

            StrategyDataItemInfo dcount = StrategyDataItemInfo.DataItem(preview.Children, "TrendCount"); dcount.BindingSource = "Histogramm"; dcount.Visibility = DataVisibility.Both; dcount.Color = Color.FromArgb(0x80, Color.Green); dcount.ChartType = ChartType.Line; dcount.MeasureUnit = StrategyDateTimeMeasureUnit.Second; dcount.MeasureUnitMultiplier = intervalSec; dcount.PanelName = "Trend Counts";
            StrategyDataItemInfo dvolume = StrategyDataItemInfo.DataItem(preview.Children, "TrendVolume"); dvolume.BindingSource = "Histogramm"; dvolume.Visibility = DataVisibility.Both; dvolume.Color = Color.FromArgb(0x40, Color.Green); dvolume.ChartType = ChartType.Line; dvolume.MeasureUnit = StrategyDateTimeMeasureUnit.Second; dvolume.MeasureUnitMultiplier = intervalSec; dvolume.PanelName = "Trend Volumes"; dvolume.PanelVisible = false;

            StrategyDataItemInfo tcdr = StrategyDataItemInfo.DataItem(preview.Children, "MaxPriceDeltaInPeriodPc"); tcdr.BindingSource = "Histogramm"; tcdr.Visibility = DataVisibility.Chart; tcdr.Color = Color.FromArgb(0x80, Color.Green); tcdr.ChartType = ChartType.Dot; tcdr.SeparateWindow = true; tcdr.ArgumentDataMember = "TrendVolume"; tcdr.ArgumentScaleType = ArgumentScaleType.Numerical; tcdr.ZoomAsMap = true; tcdr.GraphWidth = 2;
            //StrategyDataItemInfo bcdr = StrategyDataItemInfo.DataItem(preview.Children, "MaxPriceDeltaInPeriodPc"); tcdr.BindingSource = "Histogramm"; tcdr.Visibility = DataVisibility.Chart; tcdr.Color = Color.FromArgb(0x80, Color.Green); tcdr.ChartType = ChartType.Dot; tcdr.SeparateWindow = true; tcdr.ArgumentDataMember = "BuyVolume"; tcdr.ArgumentScaleType = ArgumentScaleType.Numerical; tcdr.ZoomAsMap = true;
            //StrategyDataItemInfo scdr = StrategyDataItemInfo.DataItem(preview.Children, "MaxPriceDeltaInPeriodPc"); tcdr.BindingSource = "Histogramm"; tcdr.Visibility = DataVisibility.Chart; tcdr.Color = Color.FromArgb(0x80, Color.Red); tcdr.ChartType = ChartType.Dot; tcdr.SeparateWindow = true; tcdr.ArgumentDataMember = "SellVolume"; tcdr.ArgumentScaleType = ArgumentScaleType.Numerical; tcdr.ZoomAsMap = true;
        }

        public Exchange Exchange { get; set; }
        public string[] BaseCurrencies { get; set; }
        public string[] MarketCurrencies { get; set; }

        public List<StrategyDataItemInfo> DataItemInfos { get; } = new List<StrategyDataItemInfo>();
        public ResizeableArray<object> Items { get; private set; } = new ResizeableArray<object>();
        public ResizeableArray<TickerDownloadData> Result { get; private set; } = new ResizeableArray<TickerDownloadData>();
        public string Name { get { return "Tickers TradeHistory Intencity"; } }

        int IStrategyDataItemInfoOwner.MeasureUnitMultiplier { get { return 30; } set { } }
        StrategyDateTimeMeasureUnit IStrategyDataItemInfoOwner.MeasureUnit { get { return StrategyDateTimeMeasureUnit.Minute; } set { } }

        public ResizeableArray<CandleStickData> StrategySimulationProvider { get; private set; }
        public TickerDownloadData DownloadItem(string baseCurrency, string marketCurrency) {
            SimulationStrategyDataProvider provider = new SimulationStrategyDataProvider();
            Ticker ticker = Exchange.Tickers.FirstOrDefault(t => t.BaseCurrency == baseCurrency && t.MarketCurrency == marketCurrency);
            return DownloadItem(ticker);
        }
        public TickerDownloadData DownloadItem(TickerInputInfo info) {
            return DownloadItem(info, true);
        }
        public TickerDownloadData DownloadItem(TickerInputInfo info, bool downloadCandle) {
            Ticker ticker = info.Ticker;
            SimulationStrategyDataProvider provider = new SimulationStrategyDataProvider();
            provider.DownloadProgressChanged += DownloadProgressChanged;
            if(downloadCandle) {
                ResizeableArray<CandleStickData> kline = provider.DownloadCandleStickData(info);
                if(kline == null) {
                    LogManager.Default.Error("Cannot download candlesticks for " + ticker.Name);
                    return null;
                }

                LogManager.Default.Success("Downloaded candlesticks for " + ticker.Name);
                ticker.CandleStickData.AddRange(kline);
            }
            ResizeableArray<TradeInfoItem> trades = provider.DownloadTradeHistory(info, info.StartDate);
            if(trades == null) {
                LogManager.Default.Error("Cannot download trade history for " + ticker.Name);
                return null;
            }
            LogManager.Default.Success("Downloaded trade history for " + ticker.Name);
            ticker.AddTradeHistoryItem(trades);

            TickerDownloadData tradeInfo = new TickerDownloadData() { Ticker = ticker };
            tradeInfo.HistogrammIntervalSec = HistogrammIntervalSec;
            return tradeInfo;
        }
        public TickerDownloadData DownloadItem(Ticker ticker) {
            TickerInputInfo info = new TickerInputInfo() { Exchange = ticker.Exchange.Type, KlineIntervalMin = 5, Ticker = ticker, TickerName = ticker.Name };
            return DownloadItem(info);
        }

        public bool Calculate() {
            if(Exchange == null) {
                LogManager.Default.Error("Exchange not specified");
                return false;
            }
            if(!Exchange.Connect()) {
                LogManager.Default.Error("Exchange not connected");
                return false;
            }
            foreach(string baseCurr in BaseCurrencies) {
                List<Ticker> tickers = Exchange.Tickers.Where(t => t.BaseCurrency == baseCurr && (MarketCurrencies == null || MarketCurrencies.Contains(t.MarketCurrency))).ToList();
                foreach(Ticker ticker in tickers) {
                    TickerDownloadData tradeInfo = DownloadItem(ticker);
                    if(tradeInfo == null)
                        continue;
                    Result.Add(tradeInfo);
                    Items.Add(tradeInfo);
                    RaiseTickerAdded(tradeInfo);
                }
            }
            return true;
        }

        protected void RaiseTickerAdded(TickerDownloadData info) {
            if(TickerAdded != null)
                TickerAdded(this, new TickerTradeHistoryInfoEventArgs() { Info = info });
        }
        public event TickerTradeHistoryInfoEventHandler TickerAdded;

        protected bool Canceled { get; set; }
        public void Cancel() {
            Canceled = true;
        }
    }

    public delegate void TickerTradeHistoryInfoEventHandler(object sender, TickerTradeHistoryInfoEventArgs e);

    public class TickerTradeHistoryInfoEventArgs : EventArgs {
        public TickerDownloadData Info { get; set; }
    }

    public class TickerDownloadData {
        Ticker ticker;
        public Ticker Ticker {
            get { return ticker; }
            set {
                if(Ticker == value)
                    return;
                ticker = value;
                OnTickerChanged();
            }
        }

        public string Name { get { return Ticker.Name; } }
        public string BaseCurrency { get { return Ticker.BaseCurrency; } }
        public string MarketCurrency { get { return Ticker.MarketCurrency; } }

        public ResizeableArray<CandleStickData> CandleSticks { get { return Ticker.CandleStickData; } }
        public ResizeableArray<TradeInfoItem> GetTradeHistory() { return Ticker.GetTradeHistory(); }

        ResizeableArray<TimeBaseValue> histogrammPreview;
        public ResizeableArray<TimeBaseValue> HistogrammPreviewCore {
            get {
                if(histogrammPreview == null)
                    histogrammPreview = HistogrammCalculator.CalculateByTime(GetTradeHistory(), "Time", TimeSpan.FromHours(4));
                return histogrammPreview;
            }
        }

        ResizeableArray<TickerTradeHistoryInfoItem> histogramm;
        public ResizeableArray<TickerTradeHistoryInfoItem> Histogramm {
            get {
                if(histogramm == null)
                    histogramm = CalcItems();
                return histogramm;
            }
        }

        public int HistogrammIntervalSec { get; set; } = 2;
        
        ResizeableArray<TickerTradeHistoryInfoItem> CalcItems() {
            ResizeableArray<TradeInfoItem> tradeHistory = GetTradeHistory();
            if(tradeHistory.Count == 0)
                return new ResizeableArray<TickerTradeHistoryInfoItem>();
            DateTime min = tradeHistory[0].Time;
            DateTime max = tradeHistory.Last().Time;
            if(min > max) { 
                DateTime tmp = min; min = max; max = tmp;
            }
            int interval = HistogrammIntervalSec * 1000;
            int count = (int)((max - min).TotalMilliseconds / interval + 0.5);

            ResizeableArray<TickerTradeHistoryInfoItem> list = new ResizeableArray<TickerTradeHistoryInfoItem>(count + 1);
            double k = 1.0 / interval;
            for(int i = 0; i < count + 1; i++) {
                list[i] = new TickerTradeHistoryInfoItem() { Time = min.AddMilliseconds(i * interval) };
                list[i].Low = double.MaxValue;
                list[i].OpenTime = list[i].Time.AddMilliseconds(interval);
                list[i].CloseTime = list[i].Time;
            }

            for(int i = 0; i < tradeHistory.Count; i++) {
                DateTime time = tradeHistory[i].Time;
                int index = (int)((time - min).TotalMilliseconds * k);
                TickerTradeHistoryInfoItem item = list[index];
                TradeInfoItem trade = tradeHistory[i];
                item.TradeCount++;
                item.TradeVolume += trade.Amount;

                if(tradeHistory[i].Type == TradeType.Sell) {
                    item.SellCount++;
                    item.SellVolume += trade.Amount;
                }
                else {
                    item.BuyCount++;
                    item.BuyVolume += trade.Amount;
                }
                if(item.Low > trade.Rate)
                    item.Low = trade.Rate;
                if(item.High < trade.Rate)
                    item.High = trade.Rate;
                if(item.OpenTime >= trade.Time) {
                    item.OpenTime = trade.Time;
                    item.Open = trade.Rate;
                }
                if(item.CloseTime <= trade.Time) {
                    item.CloseTime = trade.Time;
                    item.Close = trade.Rate;
                }
            }
            TickerTradeHistoryInfoItem last = null;
            for(int i = 0; i < count + 1; i++) {
                if(list[i].TradeCount == 0) {
                    if(last != null)
                        list[i].Open = list[i].Close = list[i].High = list[i].Low = last.Close;
                    else
                        list[i].Open = list[i].Close = list[i].High = list[i].Low = 0;
                }
                else
                    last = list[i];
            }
            
            for(int i = 0; i < list.Count; i++) {
                CalculateTrendInfo(list, i);
            }

            return list;
        }

        public int TrendPeriodMin { get; set; } = 30;
        private void CalculateTrendInfo(ResizeableArray<TickerTradeHistoryInfoItem> list, int index) {
            DateTime startTime = list[index].Time;
            DateTime endTime = startTime.AddMinutes(TrendPeriodMin);
            double maxDelta = 0;
            double edgePrice = 0;
            for(int i = index + 1; i < list.Count && list[i].Time < endTime ; i++) {
                double delta = Math.Abs(list[i].Close - list[index].Close);
                if(delta > maxDelta) {
                    edgePrice = list[i].Close;
                    maxDelta = delta;
                }
            }
            list[index].EdgePrice = edgePrice;
        }

        double[] histogrammPreviewDouble;
        public double[] HistogrammPreview {
            get {
                if(histogrammPreviewDouble == null)
                    histogrammPreviewDouble = HistogrammCalculator.ToDouble(HistogrammPreviewCore);
                return histogrammPreviewDouble;
            }
        } 

        private void OnTickerChanged() {
        }
    }

    public class TickerTradeHistoryInfoItem {
        public DateTime Time { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }

        public double TradeCount { get; set; }
        public double TradeVolume { get; set; }
        public double SellCount { get; set; }
        public double SellVolume { get; set; }
        public double BuyCount { get; set; }
        public double BuyVolume { get; set; }

        public double TrendCount { get { return BuyCount - SellCount; } }
        public double TrendVolume { get { return BuyVolume - SellVolume; } }

        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }

        public double MaxPriceDeltaInPeriod { get { return EdgePrice - Close; } }
        public double MaxPriceDeltaInPeriodPc { get { return (EdgePrice - Close) / Close * 100; } }
        public double EdgePrice { get; set; }
    }
}
