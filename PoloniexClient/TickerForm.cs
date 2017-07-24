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
    public partial class TickerForm : XtraForm {
        static Color bidColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
        static Color askColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
        static Color currentColor = Color.BlueViolet;

        public TickerForm() {
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
        protected List<CandleStickData> CandleStickData { get; private set; }
        void OnTickerChanged(ITicker prev) {
            if(prev != null) {
                ClearText();
                ClearGrid();
                UnsubscribeEvents(prev);
            }
            this.currencyCard1.Ticker = Ticker;
            if(Ticker == null)
                return;
            UpdateText();
            CreateCandleStickDataSource();
            UpdateGrid();
            SubscribeEvents();
            UpdateChart();
        }
        void CreateCandleStickDataSource() {
            CandleStickData = CandleStickChartHelper.CreateCandleStickData(Ticker.History, 60);
        }
        void ClearText() {
            Text = string.Empty;
            this.ribbonPageGroup1.Text = string.Empty;
        }
        void UpdateText() {
            Text = Ticker.Name;
            this.ribbonPageGroup1.Text = Ticker.Name;
        }
        void ClearGrid() {
            this.askGridControl.DataSource = null;
            this.bidGridControl.DataSource = null;
        }
        void UnsubscribeEvents(ITicker prev) {
            prev.OrderBook.OnChanged -= OnTickerOrderBookChanged;
            prev.Changed -= OnTickerChanged;
            prev.HistoryItemAdd -= OnTickerHistoryItemAdded;
            prev.TradeHistoryAdd -= OnTickerTradeHistoryAdd;
            prev.UnsubscribeOrderBookUpdates();
            prev.UnsubscribeTickerUpdates();
        }
        void UpdateGrid() {
            this.askGridControl.DataSource = Ticker.OrderBook.Asks;
            this.bidGridControl.DataSource = Ticker.OrderBook.Bids;
        }
        void SubscribeEvents() {
            Ticker.OrderBook.OnChanged += OnTickerOrderBookChanged;
            Ticker.Changed += OnTickerChanged;
            Ticker.HistoryItemAdd += OnTickerHistoryItemAdded;
            Ticker.TradeHistoryAdd += OnTickerTradeHistoryAdd;
            if(IsHandleCreated) {
                Ticker.SubscribeOrderBookUpdates();
                Ticker.SubscribeTickerUpdates();
            }
        }

        private void OnTickerTradeHistoryAdd(object sender, EventArgs e) {
            
        }

        void UpdateChart() {
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

        private void OnTickerHistoryItemAdded(object sender, EventArgs e) {
            CandleStickChartHelper.AddCandleStickData(CandleStickData, Ticker.History[Ticker.History.Count - 1], 60);
            if(IsHandleCreated)
                BeginInvoke(new MethodInvoker(this.chartControl1.RefreshData));
        }

        private void OnTickerChanged(object sender, EventArgs e) {
            if(IsHandleCreated)
                BeginInvoke(new MethodInvoker(this.chartControl1.RefreshData));
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            if(Ticker != null) {
                Ticker.SubscribeOrderBookUpdates();
                Ticker.SubscribeTickerUpdates();    
            }
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

        private void OnTickerOrderBookChanged(object sender, OrderBookEventArgs e) {
            if(!IsHandleCreated)
                return;
            if(e.Update.Action == OrderBookUpdateType.RefreshAll) {
                BeginInvoke(new Action(RefreshAskGrid));
                BeginInvoke(new Action(RefreshBidGrid));
                return;
            }
            if(e.Update.Action == OrderBookUpdateType.Remove) {
                if(e.Update.Type == OrderBookEntryType.Ask)
                    BeginInvoke(new Action<OrderBookEventArgs>(OnRemoveAsk), e);
                else if(e.Update.Type == OrderBookEntryType.Bid)
                    BeginInvoke(new Action<OrderBookEventArgs>(OnRemoveBid), e);
                return;
            }
            if(e.Update.Action == OrderBookUpdateType.Add) {
                if(e.Update.Type == OrderBookEntryType.Ask)
                    BeginInvoke(new Action<OrderBookEventArgs>(OnAddAsk), e);
                else if(e.Update.Type == OrderBookEntryType.Bid)
                    BeginInvoke(new Action<OrderBookEventArgs>(OnAddBid), e);
                return;
            }
            if(e.Update.Action == OrderBookUpdateType.Modify) {
                if(e.Update.Type == OrderBookEntryType.Ask)
                    BeginInvoke(new Action<OrderBookEventArgs>(OnModifyAsk), e);
                else if(e.Update.Type == OrderBookEntryType.Bid)
                    BeginInvoke(new Action<OrderBookEventArgs>(OnModifyBid), e);
                return;
            }
        }
        void OnModifyBid(OrderBookEventArgs obj) {
            RefreshBidGrid();
        }
        void OnModifyAsk(OrderBookEventArgs obj) {
            RefreshAskGrid();
        }
        void OnAddBid(OrderBookEventArgs obj) {
            RefreshBidGrid();
        }
        void OnAddAsk(OrderBookEventArgs obj) {
            RefreshAskGrid();
        }
        void OnRemoveBid(OrderBookEventArgs obj) {
            RefreshBidGrid();
        }
        void OnRemoveAsk(OrderBookEventArgs obj) {
            RefreshAskGrid();
        }
        void RefreshBidGrid() {
            this.bidGridControl.RefreshDataSource();
            this.bidGridControl.Invalidate();
            this.bidGridControl.Update();
        }
        void RefreshAskGrid() {
            this.askGridControl.RefreshDataSource();
            this.askGridView.TopRowIndex = Ticker.OrderBook.Asks.Count;
            this.askGridControl.Invalidate();
            this.askGridControl.Update();
        }

        private void askGridControl_Resize(object sender, EventArgs e) {
            this.askGridView.TopRowIndex = this.askGridView.DataRowCount;
        }

        private void sidePanel2_Resize(object sender, EventArgs e) {
            this.askPanel.Height = this.orderBookPanel.Height / 2;
        }
    }
}
