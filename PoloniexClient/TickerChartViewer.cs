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

namespace CryptoMarketClient {
    public partial class TickerChartViewer : XtraUserControl {
        public TickerChartViewer() {
            InitializeComponent();
            SetCandleStickCheckItemValues();
            this.barManager1.ForceInitialize();
            this.rangeControl1.Client = RangeChart;
            RangeChart.Visible = false;
            this.Controls.Add(RangeChart);
            Timer.Start();
        }

        protected ChartControl RangeChart { get; } = new ChartControl();
        Timer timer;
        protected Timer Timer {
            get {
                if(timer == null) {
                    timer = new Timer();
                    timer.Tick += OnChartUpdateTimer;
                    timer.Interval = 2000;
                }
                return timer;
            }
        }

        protected Form MdiParentForm { get; set; }
        protected Form ParentFormCore { get; set; }

        private void OnChartUpdateTimer(object sender, EventArgs e) {
            if(Ticker == null)
                return;
            if(ParentFormCore == null)
                ParentFormCore = FindForm();
            if(ParentFormCore == null || ParentFormCore.MdiParent == null || ParentFormCore.MdiParent.ActiveMdiChild != ParentFormCore)
                return;
            this.chartControl1.RefreshData();
        }

        protected override void OnLookAndFeelChanged() {
            if(BidSeries != null)
                ((XYDiagram2DSeriesViewBase)BidSeries.View).Color = Color.Green;
            if(AskSeries != null)
                ((XYDiagram2DSeriesViewBase)AskSeries.View).Color = Color.Red;
            if(CurrentSeries != null)
                ((XYDiagram2DSeriesViewBase)CurrentSeries.View).Color = CurrentColor;
        }

        TickerBase ticker;
        public TickerBase Ticker {
            get { return ticker; }
            set {
                if(Ticker == value)
                    return;
                TickerBase prev = ticker;
                this.ticker = value;
                OnTickerChanged(prev);
            }
        }
        void OnTickerChanged(TickerBase prev) {
            if(prev != null) {
                prev.OrderBook.OnChanged -= OrderBook_OnChanged;
                prev.HistoryItemAdd -= Ticker_HistoryItemAdd;
            }
            if(Ticker != null) {
                Ticker.OrderBook.OnChanged += OrderBook_OnChanged;
                Ticker.HistoryItemAdd += Ticker_HistoryItemAdd;
            }
            UpdateCandleStickMenu();
            UpdateChart();
        }
        void UpdateCandleStickMenu() {
            if(Ticker == null)
                return;
            foreach(BarCheckItemLink link in this.bsCandleStickPeriod.ItemLinks) {
                if(((int)link.Item.Tag) == Ticker.CandleStickPeriodMin) {
                    link.Item.Down = true;
                    this.bsCandleStickPeriod.Caption = link.Item.Caption;
                    break;
                }
            }
        }

        private void Ticker_HistoryItemAdd(object sender, EventArgs e) {
            CandleStickChartHelper.AddCandleStickData(Ticker.CandleStickData, Ticker.History[Ticker.History.Count - 1], Ticker.CandleStickPeriodMin * 60);
        }

        private void OrderBook_OnChanged(object sender, OrderBookEventArgs e) {
            
        }

        void CreateCandleStickDataSource() {
            Ticker.CandleStickData = CandleStickChartHelper.CreateCandleStickData(Ticker.History, Ticker.CandleStickPeriodMin * 60);
        }

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

            view.LineThickness = 2;
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
        
        public Color AskColor {
            get { return System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192))))); }
        }

        public Color BidColor {
            get { return System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192))))); }
        }

        public Color CurrentColor {
            get { return Color.FromArgb(128, Color.BlueViolet); }
        }

        protected Series BidSeries { get; set; }
        protected Series AskSeries { get; set; }
        protected Series CurrentSeries { get; set; }

        void UpdateChart() {
            if(Ticker == null)
                return;
            this.chartControl1.Series.Clear();

            this.chartControl1.Series.Add(AskSeries = CreateLineSeries(Ticker.OrderBook.VolumeHistory, "LowestAsk", Color.Red));
            this.chartControl1.Series.Add(BidSeries = CreateLineSeries(Ticker.OrderBook.VolumeHistory, "HighestBid", Color.Green));
            
            //((XYDiagram)this.chartControl1.Diagram).SecondaryAxesY.Add(new SecondaryAxisY("Hipes"));

            //this.chartControl1.Series.Add(CreateBarSeries(Ticker.OrderBook.VolumeHistory, "Bid Hipes", "BidHipe", Color.Green));
            //this.chartControl1.Series.Add(CreateBarSeries(Ticker.OrderBook.VolumeHistory, "Ask Hipes", "AskHipe", Color.Red));

            //this.chartControl1.Series.Add(CreateBarSeries(Ticker.OrderBook.VolumeHistory, "Bid Energy", "BidEnergy", Color.Green));
            //this.chartControl1.Series.Add(CreateBarSeries(Ticker.OrderBook.VolumeHistory, "Ask Energy", "AskEnergy", Color.Red));

            //RangeChart.Series.Add(CurrentSeries = CreateAreaSeries(Ticker.History, "Current", CurrentColor));
            //((XYDiagram2DSeriesViewBase)RangeChart.Series[0].View).RangeControlOptions.Visible = true;

            //((XYDiagram)this.chartControl1.Diagram).DefaultPane.Weight = 3;
            //((XYDiagram)this.chartControl1.Diagram).Panes.Add(new XYDiagramPane("Hipes") { Weight = 1 });
            //((XYDiagram)this.chartControl1.Diagram).Panes.Add(new XYDiagramPane("Energies") { Weight = 1 });

            ((XYDiagram)this.chartControl1.Diagram).EnableAxisXScrolling = true;
            ((XYDiagram)this.chartControl1.Diagram).EnableAxisXZooming = true;
            ((XYDiagram)this.chartControl1.Diagram).AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Second;
            ((XYDiagram)this.chartControl1.Diagram).AxisY.WholeRange.AlwaysShowZeroLevel = false;
            ((XYDiagram)this.chartControl1.Diagram).AxisY.Label.TextPattern = "{V:f8}";

            //((XYDiagramSeriesViewBase)this.chartControl1.Series[2].View).Pane = ((XYDiagram)this.chartControl1.Diagram).Panes[0];
            //((XYDiagramSeriesViewBase)this.chartControl1.Series[3].View).Pane = ((XYDiagram)this.chartControl1.Diagram).Panes[0];

            //((XYDiagramSeriesViewBase)this.chartControl1.Series[4].View).Pane = ((XYDiagram)this.chartControl1.Diagram).Panes[1];
            //((XYDiagramSeriesViewBase)this.chartControl1.Series[5].View).Pane = ((XYDiagram)this.chartControl1.Diagram).Panes[1];
            
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

        private void OnCandleStickPeriodChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(!((BarCheckItem)e.Item).Checked)
                return;
            this.bsCandleStickPeriod.Caption = e.Item.Caption;
            if(Ticker.CandleStickPeriodMin == (int)e.Item.Tag)
                return;
            Ticker.CandleStickPeriodMin = (int)e.Item.Tag;
            Ticker.CandleStickData.Clear();
            CandleStickChartHelper.CreateCandleStickData(Ticker);
        }
        void SetCandleStickCheckItemValues() {
            this.bcFifteenMinutes.Tag = 15;
            this.bcFiveMinutes.Tag = 5;
            this.bcOneDay.Tag = 24 * 60;
            this.bcOneHour.Tag = 60;
            this.bcOneMinute.Tag = 1;
            this.bcOneMonth.Tag = 30 * 24 * 60;
            this.bcOneWeek.Tag = 7 * 24 * 60;
            this.bcThirtyMinutes.Tag = 30;
            this.bcThreeDays.Tag = 3 * 24 * 60;
        }

        private void OnChartTypeChanged(object sender, ItemClickEventArgs e) {
            if(!((BarCheckItem)e.Item).Checked)
                return;
            this.chartControl1.Series.RemoveAt(2);
            this.chartControl1.Series.Add(CreateLastSeries());
        }
    }
}
