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
        }

        //protected override void OnHandleCreated(EventArgs e) {
        //    base.OnHandleCreated(e);
        //    RangeChart.CreateControl();
        //}

        protected ChartControl RangeChart { get; } = new ChartControl();

        protected override void OnLookAndFeelChanged() {
            if(BidSeries != null)
                ((XYDiagram2DSeriesViewBase)BidSeries.View).Color = BidColor;
            if(AskSeries != null)
                ((XYDiagram2DSeriesViewBase)AskSeries.View).Color = AskColor;
            if(CurrentSeries != null)
                ((XYDiagram2DSeriesViewBase)CurrentSeries.View).Color = CurrentColor;
        }

        ITicker ticker;
        public ITicker Ticker {
            get { return ticker; }
            set {
                if(Ticker == value)
                    return;
                ITicker prev = ticker;
                this.ticker = value;
                OnTickerChanged(prev);
            }
        }
        void OnTickerChanged(ITicker prev) {
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
            if(IsHandleCreated)
                BeginInvoke(new MethodInvoker(this.chartControl1.RefreshData));
        }

        private void OrderBook_OnChanged(object sender, OrderBookEventArgs e) {
            
        }

        void CreateCandleStickDataSource() {
            Ticker.CandleStickData = CandleStickChartHelper.CreateCandleStickData(Ticker.History, Ticker.CandleStickPeriodMin * 60);
        }

        Series CreateLineSeries(List<TickerHistoryItem> list, string str, Color color) {
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

        Series CreateStepAreaSeries(List<TickerHistoryItem> list, string str, Color color) {
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
            this.chartControl1.Series.Add(AskSeries = CreateLineSeries(Ticker.History, "Ask", AskColor));
            this.chartControl1.Series.Add(BidSeries = CreateLineSeries(Ticker.History, "Bid", BidColor));
            this.chartControl1.Series.Add(CreateLastSeries());
            RangeChart.Series.Add(CurrentSeries = CreateAreaSeries(Ticker.History, "Current", CurrentColor));
            ((XYDiagram2DSeriesViewBase)RangeChart.Series[0].View).RangeControlOptions.Visible = true;

            ((XYDiagram)this.chartControl1.Diagram).EnableAxisXScrolling = true;
            ((XYDiagram)this.chartControl1.Diagram).EnableAxisXZooming = true;
            ((XYDiagram)this.chartControl1.Diagram).EnableAxisYScrolling = true;
            ((XYDiagram)this.chartControl1.Diagram).EnableAxisYZooming = true;
            ((XYDiagram)this.chartControl1.Diagram).AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Second;
            ((XYDiagram)this.chartControl1.Diagram).AxisY.WholeRange.AlwaysShowZeroLevel = false;
        }
        Series CreateLastSeries() {
            if(this.bcStock.Checked)
                return CreateStockSeries();
            else if(this.bcColoredCandle.Checked)
                return CreateCandleStickSeries();
            else if(this.bcLine.Checked)
                return CreateLineSeries(Ticker.History, "Current", Color.DarkGray);
            else if(this.bcCandle.Checked)
                return CreateCandleStickSeries();
            else if(this.bcColoredStock.Checked)
                return CreateStockSeries();
            else if(this.bcArea.Checked)
                return CreateStepAreaSeries(Ticker.History, "Current", Color.DarkGray);
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
