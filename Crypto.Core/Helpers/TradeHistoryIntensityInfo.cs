using Crypto.Core.Indicators;
using Crypto.Core.Strategies;
using CryptoMarketClient;
using CryptoMarketClient.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Helpers {
    public class TradeHistoryIntensityInfo : IStrategyDataItemInfoOwner {
        public TradeHistoryIntensityInfo() {
            InitializeDataItems();
        }

        private void InitializeDataItems() {
            StrategyDataItemInfo.DataItem(DataItemInfos, "BaseCurrency").Visibility = DataVisibility.Table;
            StrategyDataItemInfo.DataItem(DataItemInfos, "MarketCurrency").Visibility = DataVisibility.Table;
            StrategyDataItemInfo preview = StrategyDataItemInfo.DataItem(DataItemInfos, "HistogrammPreview"); preview.Color = Color.FromArgb(0x40, Color.Green); preview.Visibility = DataVisibility.Table;
            StrategyDataItemInfo candle = StrategyDataItemInfo.CandleStickItem(preview.Children, "CandleSticks"); candle.Visibility = DataVisibility.Chart; candle.BindingSource = "CandleSticks";
            StrategyDataItemInfo bars = StrategyDataItemInfo.DataItem(preview.Children, "Value"); bars.ArgumentDataMember = "Time"; bars.Visibility = DataVisibility.Chart; bars.Color = Color.FromArgb(0x40, Color.Green); bars.ChartType = ChartType.Bar; bars.BindingSource = "Histogramm"; bars.PanelName = "Trades";
        }

        public Exchange Exchange { get; set; }
        public string[] BaseCurrencies { get; set; }
        public string[] MarketCurrencies { get; set; }

        public List<StrategyDataItemInfo> DataItemInfos { get; } = new List<StrategyDataItemInfo>();
        public ResizeableArray<object> Items { get; private set; } = new ResizeableArray<object>();
        public ResizeableArray<TickerTradeHistoryInfo> Result { get; private set; } = new ResizeableArray<TickerTradeHistoryInfo>();
        public string Name { get { return "Tickers TradeHistory Intencity"; } }

        int IStrategyDataItemInfoOwner.MeasureUnitMultiplier { get { return 30; } set { } }
        StrategyDateTimeMeasureUnit IStrategyDataItemInfoOwner.MeasureUnit { get { return StrategyDateTimeMeasureUnit.Minute; } set { } }

        public ResizeableArray<CandleStickData> StrategySimulationProvider { get; private set; }
        public TickerTradeHistoryInfo DownloadItem(string baseCurrency, string marketCurrency) {
            SimulationStrategyDataProvider provider = new SimulationStrategyDataProvider();
            Ticker ticker = Exchange.Tickers.FirstOrDefault(t => t.BaseCurrency == baseCurrency && t.MarketCurrency == marketCurrency);
            return DownloadItem(ticker);
        }

        public TickerTradeHistoryInfo DownloadItem(Ticker ticker) {
            SimulationStrategyDataProvider provider = new SimulationStrategyDataProvider();
            TickerInputInfo info = new TickerInputInfo() { Exchange = ticker.Exchange.Type, KlineIntervalMin = 5, Ticker = ticker, TickerName = ticker.Name };
            ResizeableArray<CandleStickData> kline = provider.DownloadCandleStickData(info);
            if(kline == null) {
                LogManager.Default.Error("Cannot download candlesticks for " + ticker.Name);
                return null;
            }

            LogManager.Default.Success("Downloaded candlesticks for " + ticker.Name);
            ticker.CandleStickData.AddRange(kline);

            ResizeableArray<TradeInfoItem> trades = provider.DownloadTradeHistory(info, ticker.CandleStickData.First().Time);
            if(trades == null) {
                LogManager.Default.Error("Cannot download trade history for " + ticker.Name);
                return null;
            }
            LogManager.Default.Success("Downloaded trade history for " + ticker.Name);
            ticker.TradeHistory.AddRange(trades);

            TickerTradeHistoryInfo tradeInfo = new TickerTradeHistoryInfo() { Ticker = ticker };
            return tradeInfo;
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
                    TickerTradeHistoryInfo tradeInfo = DownloadItem(ticker);
                    if(tradeInfo == null)
                        continue;
                    Result.Add(tradeInfo);
                    Items.Add(tradeInfo);
                    RaiseTickerAdded(tradeInfo);
                }
            }
            return true;
        }

        protected void RaiseTickerAdded(TickerTradeHistoryInfo info) {
            if(TickerAdded != null)
                TickerAdded(this, new TickerTradeHistoryInfoEventArgs() { Info = info });
        }
        public event TickerTradeHistoryInfoEventHandler TickerAdded;
    }

    public delegate void TickerTradeHistoryInfoEventHandler(object sender, TickerTradeHistoryInfoEventArgs e);

    public class TickerTradeHistoryInfoEventArgs : EventArgs {
        public TickerTradeHistoryInfo Info { get; set; }
    }

    public class TickerTradeHistoryInfo {
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
        public ResizeableArray<TradeInfoItem> TradeHistory { get { return Ticker.TradeHistory; } }

        TimeBaseValue[] histogrammPreview;
        public TimeBaseValue[] HistogrammPreviewCore {
            get {
                if(histogrammPreview == null)
                    histogrammPreview = HistogrammCalculator.CalculateByTime(TradeHistory, "Time", TimeSpan.FromHours(4));
                return histogrammPreview;
            }
        }

        TimeBaseValue[] histogramm;
        public TimeBaseValue[] Histogramm {
            get {
                if(histogramm == null)
                    histogramm = HistogrammCalculator.CalculateByTime(TradeHistory, "Time", TimeSpan.FromSeconds(1));
                return histogramm;
            }
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
}
