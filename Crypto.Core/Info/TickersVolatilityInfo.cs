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
    public class TickersVolatilityInfo : IStrategyDataItemInfoOwner {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Exchange Exchange { get; set; }
        public string[] BaseCurrencies { get; set; }
        public string[] MarketCurrencies { get; set; }
        public int CandleStickPeriodMin { get; set; } = 30;
        public int HistogrammBarCount { get; set; } = 40;

        public List<StrategyDataItemInfo> DataItemInfos { get; } = new List<StrategyDataItemInfo>();
        public ResizeableArray<object> Items { get; private set; } = new ResizeableArray<object>();
        public List<TickerVolatilityInfo> Result { get; private set; } = new List<TickerVolatilityInfo>();
        public string Name { get { return "Tickers ATR"; } }

        int IStrategyDataItemInfoOwner.MeasureUnitMultiplier { get { return 30; } set { } }
        StrategyDateTimeMeasureUnit IStrategyDataItemInfoOwner.MeasureUnit { get { return StrategyDateTimeMeasureUnit.Minute; } set { } }

        public TickersVolatilityInfo() {
            DataItem("Name").Visibility = DataVisibility.Table;
            DataItem("MathExp").Visibility = DataVisibility.Table;
            DataItem("Deviation").Visibility = DataVisibility.Table;

            StrategyDataItemInfo hist = DataItem("Y"); hist.ArgumentDataMember = "X"; hist.Type = DataType.ChartData; hist.BindingSource = "Histogramm"; hist.Color = Color.FromArgb(0x40, Color.Green); hist.ChartType = ChartType.Area;
            StrategyDataItemInfo dhist = DataItem("HistogrammDouble"); dhist.Visibility = DataVisibility.Table; dhist.DetailInfo = hist;
            DataItemInfos.Remove(hist);
        }

        public StrategyDataItemInfo AnnotationItem(string fieldName, string text, Color color, string anchor) {
            var item = DataItem(fieldName);
            item.ChartType = ChartType.Dot;
            item.Color = color;
            item.Visibility = DataVisibility.Chart;
            item.ChartType = ChartType.Annotation;
            item.AnnotationText = text;
            item.AnnotationAnchorField = anchor;
            return item;
        }

        public StrategyDataItemInfo TimeItem(string fieldName) {
            DataItemInfos.Add(new StrategyDataItemInfo() { FieldName = fieldName, Visibility = DataVisibility.Table, Type = DataType.DateTime, FormatString = "dd.MM.yyyy hh:mm" });
            return DataItemInfos.Last();
        }

        public StrategyDataItemInfo DataItem(string fieldName) {
            DataItemInfos.Add(new StrategyDataItemInfo() { FieldName = fieldName });
            return DataItemInfos.Last();
        }

        public StrategyDataItemInfo DataItem(string fieldName, string formatString) {
            DataItemInfos.Add(new StrategyDataItemInfo() { FieldName = fieldName, FormatString = formatString });
            return DataItemInfos.Last();
        }

        public StrategyDataItemInfo EnumItem(string fieldName) {
            DataItemInfos.Add(new StrategyDataItemInfo() { FieldName = fieldName, Visibility = DataVisibility.Table });
            return DataItemInfos.Last();
        }

        public StrategyDataItemInfo DataItem(string fieldName, string formatString, Color color) {
            DataItemInfos.Add(new StrategyDataItemInfo() { FieldName = fieldName, FormatString = formatString, Color = color });
            return DataItemInfos.Last();
        }

        public StrategyDataItemInfo DataItem(string fieldName, Color color) {
            DataItemInfos.Add(new StrategyDataItemInfo() { FieldName = fieldName, Color = color });
            return DataItemInfos.Last();
        }

        public StrategyDataItemInfo DataItem(string fieldName, Color color, int width) {
            DataItemInfos.Add(new StrategyDataItemInfo() { FieldName = fieldName, Color = color, GraphWidth = width });
            return DataItemInfos.Last();
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
                    ResizeableArray<CandleStickData> data = Utils.DownloadCandleStickData(ticker, CandleStickPeriodMin);
                    if(data == null) {
                        LogManager.Default.Error("Cannot download candlesticks for " + ticker.Name);
                        continue;
                    }
                    LogManager.Default.Success("Downloaded candlesticks for " + ticker.Name);
                    ticker.CandleStickData.AddRange(data);
                    //foreach(var t in data)
                    //    ticker.CandleStickData.Add(t);
                    //data.ForEach(i => ticker.CandleStickData.Add(i));
                    TickerVolatilityInfo info = new TickerVolatilityInfo() { Ticker = ticker };
                    info.Calculate();
                    info.CalculateMath();
                    info.CalculateDev();
                    info.CalcHistogramm(HistogrammBarCount);
                    Result.Add(info);
                    Items.Add(info);
                    RaiseTickerAdded(info);
                }
            }
            return true;
        }

        protected void RaiseTickerAdded(TickerVolatilityInfo info) {
            if(TickerAdded != null)
                TickerAdded(this, new TickerVolatilityInfoEventArgs() { Info = info });
        }
        public event TickerVolatilityInfoEventHandler TickerAdded;
    }

    public delegate void TickerVolatilityInfoEventHandler(object sender, TickerVolatilityInfoEventArgs e);

    public class TickerVolatilityInfoEventArgs : EventArgs {
        public TickerVolatilityInfo Info { get; set; }
    }

    public class TickerVolatilityInfo {
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

        private void OnTickerChanged() {
        }
        public void Calculate() {
            Indicator = new AtrIndicator() { SuppressUpdateOnDataChanged = true, Ticker = Ticker };
            Indicator.Calculate();
        }

        public void CalculateMath() {
            MathExp = Indicator.CalculateMath();
        }

        public void CalculateDev() {
            Deviation = Indicator.CalculateDev();
        }

        public void CalcHistogramm(int count) {
            Histogramm = Indicator.CalcHistogramm(count);
            HistogrammDouble = Histogramm.Select(v => v.Y).ToArray();
        }

        public double[] HistogrammDouble { get; set; }
        public ResizeableArray<ArgumentValue> Histogramm { get; set; }
        public AtrIndicator Indicator { get; private set; }
        public double MathExp { get; set; }
        public double Deviation { get; set; }
    }
}
