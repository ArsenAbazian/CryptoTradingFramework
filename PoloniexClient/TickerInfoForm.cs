using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public partial class TickerInfoForm : XtraForm {
        public TickerInfoForm() {
            InitializeComponent();
        }

        protected virtual void DisposeCore() {
            Ticker = null;
        }

        ITicker ticker;
        public ITicker Ticker {
            get {
                return ticker;
            }
            set {
                if(Ticker == value)
                    return;
                ITicker prev = Ticker;
                ticker = value;
                OnTickerChanged(prev);
            }
        }
        List<CandleStickData> CandleStickData { get; set; }
        static Color bidColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
        static Color askColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
        static Color currentColor = Color.BlueViolet;
        void OnTickerChanged(ITicker prev) {
            if(prev != null) {
                this.ribbonPageGroup1.Text = "";
                this.askGridControl.DataSource = null;
                this.bidGridControl.DataSource = null;
                prev.OrderBook.OnChanged -= OrderBook_OnChanged;
                prev.HistoryItemAdd -= Ticker_HistoryItemAdd;
            }
            this.currencyCard1.Ticker = Ticker;
            if(Ticker == null)
                return;
            CandleStickData = CandleStickChartHelper.CreateCandleStickData(Ticker.History, 60);
            Text = Ticker.Name;
            this.ribbonPageGroup1.Text = Ticker.Name;
            this.askGridControl.DataSource = Ticker.OrderBook.Asks;
            this.bidGridControl.DataSource = Ticker.OrderBook.Bids;
            Ticker.OrderBook.OnChanged += OrderBook_OnChanged;
            Ticker.Changed += Ticker_Changed;
            Ticker.HistoryItemAdd += Ticker_HistoryItemAdd;
            if(IsHandleCreated)
                Ticker.OrderBook.Connect();
            this.chartControl1.Series.Add(CreateLineSeries(Ticker.History, "Ask", Color.Red));
            this.chartControl1.Series.Add(CreateLineSeries(Ticker.History, "Bid", Color.Blue));
            this.chartControl1.Series.Add(CreateCandleStickSeries(CandleStickData));
            ((XYDiagram)this.chartControl1.Diagram).EnableAxisXScrolling = true;
            ((XYDiagram)this.chartControl1.Diagram).EnableAxisXZooming = true;
            ((XYDiagram)this.chartControl1.Diagram).EnableAxisYScrolling = true;
            ((XYDiagram)this.chartControl1.Diagram).EnableAxisYZooming = true;
            ((XYDiagram)this.chartControl1.Diagram).AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Second;
            ((XYDiagram)this.chartControl1.Diagram).AxisY.WholeRange.AlwaysShowZeroLevel = false;
        }

        private void Ticker_HistoryItemAdd(object sender, EventArgs e) {
            CandleStickChartHelper.AddCandleStickData(CandleStickData, Ticker.History[Ticker.History.Count - 1], 60);
            BeginInvoke(new MethodInvoker(this.chartControl1.RefreshData));
        }

        private void Ticker_Changed(object sender, EventArgs e) {
            BeginInvoke(new MethodInvoker(this.chartControl1.RefreshData));
        }

        Series CreateLineSeries(List<TickerHistoryItem> list, string str, Color color) {
            Series s = new Series();
            s.ArgumentDataMember = "Time";
            s.ValueDataMembers.AddRange(str);
            s.ValueScaleType = ScaleType.Numerical;
            LineSeriesView view = new LineSeriesView();
            view.Color = color;
            view.LineStyle.Thickness = 1;
            s.View = view;
            s.DataSource = list;
            return s;
        }

        
        Series CreateCandleStickSeries(List<CandleStickData> list) {
            Series s = new Series("Last", ViewType.CandleStick);
            s.ArgumentDataMember = "Time";
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
            s.DataSource = list;
            return s;
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            Ticker.OrderBook.Connect();
        }

        private void OrderBook_OnChanged(object sender, OrderBookEventArgs e) {
            Console.WriteLine("update seq no = " + e.Update.SeqNo);
            if(e.Update.Update != OrderBookUpdateType.Modify) {
                if(e.Update.Type == OrderBookEntryType.Ask)
                    BeginInvoke(new MethodInvoker(this.askGridControl.RefreshDataSource));
                else
                    BeginInvoke(new MethodInvoker(this.bidGridControl.RefreshDataSource));
                return;
            }
            if(e.Update.Type == OrderBookEntryType.Ask) {
                int index = Ticker.OrderBook.Asks.IndexOf(e.Update.Entry);
                int rowHandle = this.askGridView.GetRowHandle(index);
                //BeginInvoke(new Action<int>(this.askGridView.RefreshRow));
                BeginInvoke(new MethodInvoker(this.askGridControl.RefreshDataSource));
            }
            else {
                int index = Ticker.OrderBook.Bids.IndexOf(e.Update.Entry);
                int rowHandle = this.bidGridView.GetRowHandle(index);
                //BeginInvoke(new Action<int>(this.bidGridView.RefreshRow));
                BeginInvoke(new MethodInvoker(this.bidGridControl.RefreshDataSource));
            }
        }

        private void askGridControl_Resize(object sender, EventArgs e) {
            this.askGridView.TopRowIndex = this.askGridView.DataRowCount;
        }

        private void sidePanel2_Resize(object sender, EventArgs e) {
            this.askPanel.Height = this.orderBookPanel.Height / 2;
        }
    }
}
