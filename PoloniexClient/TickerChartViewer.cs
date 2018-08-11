using CryptoMarketClient.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraBars;
using DevExpress.Skins;
using DevExpress.Utils.Svg;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using System.Threading;
using DevExpress.XtraSplashScreen;

namespace CryptoMarketClient {
    public partial class TickerChartViewer : XtraUserControl {
        bool isCandleSticksUpdate = false;

        public TickerChartViewer() {
            InitializeComponent();
            InitializeCheckItems();
            this.barManager1.ForceInitialize();
        }

        protected Form MdiParentForm { get; set; }
        protected Form ParentFormCore { get; set; }

        Ticker ticker;
        public Ticker Ticker {
            get { return ticker; }
            set {
                if(Ticker == value)
                    return;
                Ticker prev = ticker;
                this.ticker = value;
                OnTickerChanged(prev);
            }
        }
        void OnTickerChanged(Ticker prev) {
            if(prev != null) {
                //prev.HistoryItemAdd -= Ticker_HistoryItemAdd;
                prev.CandleStickChanged -= OnTickerCandleStickChanged;
                prev.EventsChanged -= Settings_EventsChanged;
                prev.StopListenKlineStream();
            }
            if(Ticker != null) {
                //Ticker.HistoryItemAdd += Ticker_HistoryItemAdd;
                Ticker.CandleStickChanged += OnTickerCandleStickChanged;
                Ticker.EventsChanged += Settings_EventsChanged;
            }
            if(Ticker != null) {
                Ticker.StopListenKlineStream();
                SetCandleStickCheckItemValues();
                UpdateDataFromServer();
                UpdateCandleStickMenu();
                UpdateChart();
                Ticker.StartListenKlineStream();
            }
            else {
                foreach(Series s in this.chartControl1.Series)
                    s.DataSource = null;
            }
        }

        private void OnTickerCandleStickChanged(object sender, EventArgs e) {
            BeginInvoke(new MethodInvoker(RefreshChartData));
        }

        void RefreshChartData() {
            this.chartControl1.RefreshData();
        }

        void UpdateCandleStickMenu() {
            if(Ticker == null)
                return;
            foreach(BarCheckItemLink link in this.bsCandleStickPeriod.ItemLinks) {
                if(((TimeSpan)link.Item.Tag).Minutes == Ticker.CandleStickPeriodMin) {
                    link.Item.Down = true;
                    this.bsCandleStickPeriod.Caption = link.Item.Caption;
                    break;
                }
            }
        }

        //private void Ticker_HistoryItemAdd(object sender, EventArgs e) {
        //    lock(Ticker.CandleStickData) {
        //        CandleStickChartHelper.AddCandleStickData(Ticker.CandleStickData, Ticker.History[Ticker.History.Count - 1], Ticker.CandleStickPeriodMin * 60);
        //    }
        //}

        //void CreateCandleStickDataSource() {
        //    Ticker.CandleStickData = CandleStickChartHelper.CreateCandleStickData(Ticker.History, Ticker.CandleStickPeriodMin * 60);
        //}

        Series CreateLineSeries(List<OrderBookStatisticItem> list, string str, Color color) {
            Series s = new Series();
            s.Name = str;
            s.ArgumentDataMember = "Time";
            s.ValueDataMembers.AddRange(str);
            s.ValueScaleType = ScaleType.Numerical;
            s.ShowInLegend = true;
            StepLineSeriesView view = new StepLineSeriesView();
            view.Color = color;
            view.LineStyle.Thickness = (int)(1 * DpiProvider.Default.DpiScaleFactor);
            s.View = view;
            s.DataSource = list;
            return s;
        }

        Series CreateStepAreaSeries(List<OrderBookStatisticItem> list, string str, Color color) {
            Series s = new Series();
            s.Name = str;
            s.ArgumentDataMember = "Time";
            s.ValueDataMembers.AddRange(str);
            s.ValueScaleType = ScaleType.Numerical;
            s.ShowInLegend = true;
            StepAreaSeriesView view = new StepAreaSeriesView();
            view.Color = color;
            s.View = view;
            s.DataSource = list;
            return s;
        }

        Series CreateAreaSeries(List<TickerHistoryItem> list, string str, Color color) {
            Series s = new Series();
            s.Name = str;
            s.ArgumentDataMember = "Time";
            s.ValueDataMembers.AddRange(str);
            s.ValueScaleType = ScaleType.Numerical;
            s.ShowInLegend = true;
            AreaSeriesView view = new AreaSeriesView();
            view.Color = color;
            s.View = view;
            s.DataSource = list;
            return s;
        }

        Series CreateBarSeries(List<OrderBookStatisticItem> list, string name, string value, Color color) {
            Series s = new Series();
            s.Name = name;
            s.ArgumentDataMember = "Time";
            s.ValueDataMembers.AddRange(value);
            s.ValueScaleType = ScaleType.Numerical;
            s.ShowInLegend = true;
            SideBySideBarSeriesView view = new SideBySideBarSeriesView();
            view.Color = color;
            view.AxisY = ((XYDiagram)this.chartControl1.Diagram).SecondaryAxesY["Hipes"];
            s.View = view;
            s.DataSource = list;
            return s;
        }

        Series CreateCandleStickSeries() {
            Series s = new Series("Last", ViewType.CandleStick);
            s.ArgumentDataMember = "Time";
            s.ArgumentScaleType = ScaleType.DateTime;
            s.ValueDataMembers.AddRange("Low", "High", "Open", "Close");
            s.ValueScaleType = ScaleType.Numerical;
            CandleStickSeriesView view = new CandleStickSeriesView();

            view.LineThickness = (int)(1 * DpiProvider.Default.DpiScaleFactor);
            view.LevelLineLength = 0.25;
            view.ReductionOptions.ColorMode = ReductionColorMode.OpenToCloseValue;
            view.ReductionOptions.FillMode = CandleStickFillMode.FilledOnReduction;
            view.ReductionOptions.Level = StockLevel.Open;
            view.ReductionOptions.Visible = true;

            s.View = view;
            s.DataSource = Ticker.CandleStickData;
            return s;
        }

        Series CreateStockSeries() {
            Series s = new Series("Last", ViewType.Stock);
            s.ArgumentDataMember = "Time";
            s.ArgumentScaleType = ScaleType.DateTime;
            s.ValueDataMembers.AddRange("Low", "High", "Open", "Close");
            s.ValueScaleType = ScaleType.Numerical;
            StockSeriesView view = new StockSeriesView();

            view.ShowOpenClose = StockType.Both;
            view.LineThickness = 2;
            view.LevelLineLength = 0.25;
            view.ReductionOptions.ColorMode = ReductionColorMode.OpenToCloseValue;
            view.ReductionOptions.Level = StockLevel.Open;
            view.ReductionOptions.Visible = true;

            s.View = view;
            s.DataSource = Ticker.CandleStickData;
            return s;
        }

        protected Series BidSeries { get; set; }
        protected Series AskSeries { get; set; }
        protected Series CurrentSeries { get; set; }
        protected bool SuppressUpdateCandlestickData { get; set; }

        void UpdateChart() {
            if(Ticker == null)
                return;
            SuppressUpdateCandlestickData = true;
            try {
                this.chartControl1.Series["BuySellVolume"].DataSource = Ticker.CandleStickData;
                this.chartControl1.Series["BuySellVolume"].ArgumentDataMember = "Time";
                this.chartControl1.Series["BuySellVolume"].ValueDataMembers.AddRange("BuySellVolume");

                CandleStickSeriesView sv = (CandleStickSeriesView)this.chartControl1.Series["Current"].View;
                sv.Color = Exchange.CandleStickColor;
                sv.ReductionOptions.Color = Exchange.CandleStickReductionColor;


                this.chartControl1.Series["Volume"].ArgumentDataMember = "Time";
                this.chartControl1.Series["Volume"].ValueDataMembers.AddRange("Volume");

                this.chartMarketDepth.Series["TotalVolumeBuy"].DataSource = Ticker.OrderBook.Bids;
                this.chartMarketDepth.Series["TotalVolumeBuy"].ArgumentDataMember = "Value";
                this.chartMarketDepth.Series["TotalVolumeBuy"].ValueDataMembers.AddRange("VolumeTotal");

                this.chartMarketDepth.Series["TotalVolumeSell"].DataSource = Ticker.OrderBook.Asks;
                this.chartMarketDepth.Series["TotalVolumeSell"].ArgumentDataMember = "Value";
                this.chartMarketDepth.Series["TotalVolumeSell"].ValueDataMembers.AddRange("VolumeTotal");

                this.chartWalls.Series["Sell volume"].DataSource = Ticker.OrderBook.Asks;
                this.chartWalls.Series["Sell volume"].ArgumentDataMember = "Value";
                this.chartWalls.Series["Sell volume"].ValueDataMembers.AddRange("Volume");
                this.chartWalls.Series["Sell volume"].View.Color = Exchange.AskColor;

                this.chartWalls.Series["Buy volume"].DataSource = Ticker.OrderBook.Bids;
                this.chartWalls.Series["Buy volume"].ArgumentDataMember = "Value";
                this.chartWalls.Series["Buy volume"].ValueDataMembers.AddRange("Volume");
                this.chartWalls.Series["Buy volume"].View.Color = Exchange.BidColor;

                ConfigurateChart(ViewType.CandleStick);
                UpdateEvents(null);
            }
            finally {
                SuppressUpdateCandlestickData = false;
            }
        }
        Series CreateLastSeries() {
            if(this.bcStock.Checked)
                return CreateStockSeries();
            else if(this.bcColoredCandle.Checked)
                return CreateCandleStickSeries();
            else if(this.bcLine.Checked)
                return CreateLineSeries(Ticker.OrderBook.VolumeHistory, "Current", Color.DarkGray);
            else if(this.bcCandle.Checked)
                return CreateCandleStickSeries();
            else if(this.bcColoredStock.Checked)
                return CreateStockSeries();
            else if(this.bcArea.Checked)
                return CreateStepAreaSeries(Ticker.OrderBook.VolumeHistory, "Current", Color.DarkGray);
            return null;
        }

        void SetCandleStickCheckItemValues() {
            if(Ticker == null)
                return;
            var intervals = Ticker.Exchange.AllowedCandleStickIntervals;
            foreach(CandleStickIntervalInfo info in intervals) {
                BarCheckItem item = new BarCheckItem(this.barManager1) { Caption = info.Text, Tag = info.Interval, GroupIndex = 22 };
                item.CheckedChanged += OnIntervalItemCheckedChanged;
                this.bsCandleStickPeriod.ItemLinks.Add(item);
            }
        }

        private void OnIntervalItemCheckedChanged(object sender, ItemClickEventArgs e) {
            BarCheckItem item = (BarCheckItem)sender;
            if(!item.Checked)
                return;
            if(((TimeSpan)item.Tag).TotalMinutes == Ticker.CandleStickPeriodMin)
                return;
            this.bsCandleStickPeriod.Caption = e.Item.Caption;
            Ticker.StopListenKlineStream();
            Ticker.CandleStickPeriodMin = (int)((TimeSpan)item.Tag).TotalMinutes;
            Ticker.CandleStickData.Clear();
            Ticker.StartListenKlineStream();
            UpdateDataFromServer();
        }

        private void OnChartTypeChanged(object sender, ItemClickEventArgs e) {
            if(!((BarCheckItem)e.Item).Checked)
                return;
            ConfigurateChart((ViewType)e.Item.Tag);
        }
        void ConfigurateChart(ViewType type) {
            this.chartControl1.Series["Current"].ChangeView(type);
            this.chartControl1.Series["Current"].DataSource = null;
            if(type == ViewType.CandleStick || type == ViewType.Stock)
                this.chartControl1.Series["Current"].BindToData(Ticker.CandleStickData, "Time", "Low", "High", "Open", "Close");
            else
                this.chartControl1.Series["Current"].BindToData(Ticker.History, "Time", "Current");
            UpdateChartProperties();
        }
        void InitializeCheckItems() {
            this.bcStock.Tag = ViewType.Stock;
            this.bcLine.Tag = ViewType.Line;
            this.bcCandle.Tag = ViewType.CandleStick;
        }

        protected int CalculateTotalIntervalInSeconds() {
            int totalWidth = this.chartControl1.ClientRectangle.Width;
            int screenCount = 3;
            int candleStickCount = (int)(totalWidth / (5 * DpiProvider.Default.DpiScaleFactor));
            int interval = Ticker.CandleStickPeriodMin * 60;
            return candleStickCount * screenCount * interval;
        }

        protected void UpdateDataFromServer() {
            int totalWidth = this.chartControl1.ClientRectangle.Width;
            int screenCount = 3;
            int candleStickCount = (int)(totalWidth / (5 * DpiProvider.Default.DpiScaleFactor));
            int interval = Ticker.CandleStickPeriodMin * 60;
            int totalInterval = candleStickCount * screenCount * interval;

            DateTime start = DateTime.UtcNow; //.Subtract(TimeSpan.FromSeconds(totalInterval));
            BackgroundUpdateCandleSticks(start);
            CandleStickSeriesView view = (CandleStickSeriesView)this.chartControl1.Series["Current"].View;
            view.AxisX.VisualRange.MinValue = start;
            view.AxisX.VisualRange.MaxValue = start.AddSeconds(candleStickCount * interval);
            UpdateChartProperties();
        }

        protected void UpdateChartProperties() {
            ((BarSeriesView)this.chartControl1.Series["Volume"].View).BarWidth = 0.6 * Ticker.CandleStickPeriodMin;
            ((BarSeriesView)this.chartControl1.Series["Volume"].View).Border.Visibility = DevExpress.Utils.DefaultBoolean.False;

            ((BarSeriesView)this.chartControl1.Series["BuySellVolume"].View).BarWidth = 0.6 * Ticker.CandleStickPeriodMin;
            ((BarSeriesView)this.chartControl1.Series["BuySellVolume"].View).Border.Visibility = DevExpress.Utils.DefaultBoolean.False;

            FinancialSeriesViewBase f = this.chartControl1.Series["Current"].View as FinancialSeriesViewBase;
            if(f != null)
                f.LevelLineLength = 0.6 / 2 * Ticker.CandleStickPeriodMin;
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e) {
            UpdateDataFromServer();
        }
        public void AddIndicator(TrailingSettings settings) {
            if(Ticker == null)
                return;
            UpdateEvents(null);
        }

        private void Settings_EventsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            UpdateEvents(e);
        }

        protected SeriesPoint CreateEventPoint(TickerEvent ev) {
            SeriesPoint pt = new SeriesPoint(ev.Time, ev.Current);
            pt.ToolTipHint = ev.Text;
            pt.Tag = ev;
            pt.Color = ev.Color;
            return pt;
        }

        void UpdateEvents(System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            Series s = this.chartControl1.Series["Events"];
            if(e == null || e.Action != System.Collections.Specialized.NotifyCollectionChangedAction.Add) {
                s.Points.Clear();
                foreach(TickerEvent ev in Ticker.Events) {
                    s.Points.Add(CreateEventPoint(ev));
                }
            }
            else {
                foreach(TickerEvent ev in e.NewItems)
                    s.Points.Add(CreateEventPoint(ev));
            }
        }

        public void RemoveIndicator(TrailingSettings settings) {
            if(Ticker == null)
                return;
            UpdateEvents(null);
        }

        Thread UpdateCandleStickThread { get; set; }
        private void chartControl1_AxisVisualRangeChanged(object sender, AxisRangeChangedEventArgs e) {
            if(Ticker == null || SuppressUpdateCandlestickData || !Ticker.Exchange.AllowCandleStickIncrementalUpdate)
                return;
            if(!(e.Axis.VisualRange.MinValue is DateTime))
                return;
            if(UpdateCandleStickThread != null && UpdateCandleStickThread.IsAlive)
                return;
            if(object.Equals(e.Axis.VisualRange.MinValue, e.Axis.WholeRange.MinValue)) {
                BackgroundUpdateCandleSticks((DateTime)e.Axis.VisualRange.MinValue);
            }
        }
        void BackgroundUpdateCandleSticks(DateTime date) {
            if (this.isCandleSticksUpdate)
                return;

            UpdateCandleStickThread = new Thread(() => {
                try {
                    int seconds = CalculateTotalIntervalInSeconds();
                    BindingList<CandleStickData> data = Ticker.GetCandleStickData(Ticker.CandleStickPeriodMin, date.AddSeconds(-seconds), seconds);
                    if(data != null) {
                        foreach(CandleStickData prev in Ticker.CandleStickData) {
                            data.Add(prev);
                        }
                        Ticker.CandleStickData = data;
                        this.chartControl1.Series["Current"].DataSource = data;
                        this.chartControl1.Series["Volume"].DataSource = data;
                        this.chartControl1.Series["BuySellVolume"].DataSource = data;
                    }
                    SplashScreenManager.CloseDefaultWaitForm();
                    this.isCandleSticksUpdate = false;
                }
                catch(Exception e) {
                    Telemetry.Default.TrackException(e, new string[,] { { "method", "update candlestick data from server" } });
                }
            });
            SplashScreenManager.ShowDefaultWaitForm("Loading chart from server...");
            this.isCandleSticksUpdate = true;
            UpdateCandleStickThread.Start();
        }

        private void ciShowWalls_CheckedChanged(object sender, ItemClickEventArgs e) {
            this.splitContainerControl1.PanelVisibility = this.ciShowWalls.Checked ? SplitPanelVisibility.Both : SplitPanelVisibility.Panel1;
        }

        private void chartControl1_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e) {
            if(e.Series.Name == "BuySellVolume") {
                if(e.SeriesPoint.Values[0] > 0)
                    e.SeriesDrawOptions.Color = Color.Green;
                else
                    e.SeriesDrawOptions.Color = Color.Red;
            }
        }
    }
}
