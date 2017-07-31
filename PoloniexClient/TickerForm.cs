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
        
        void OnTickerChanged(ITicker prev) {
            if(prev != null) {
                ClearText();
                ClearGrid();
                ClearChart();
                UnsubscribeEvents(prev);
            }
            this.tickerInfoControl.Ticker = Ticker;
            if(Ticker == null)
                return;
            UpdateTickerInfoControlHeight();
            UpdateText();
            UpdateGrid();
            UpdateChart();
            SubscribeEvents();
        }
        void UpdateTickerInfoControlHeight() {
            this.tickerInfoControl.UpdateBestHeight();
        }
        void UpdateChart() {
            this.tickerChartViewer1.Ticker = Ticker;
        }
        void ClearChart() {
            this.tickerChartViewer1.Ticker = null;
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
            this.tradeHistoryItemBindingSource.DataSource = null;
        }
        void UnsubscribeEvents(ITicker prev) {
            prev.OrderBook.OnChanged -= OnTickerOrderBookChanged;
            prev.Changed -= OnTickerChanged;
            prev.HistoryItemAdd -= OnTickerHistoryItemAdded;
            prev.TradeHistoryAdd -= OnTickerTradeHistoryAdd;
            prev.UnsubscribeOrderBookUpdates();
            prev.UnsubscribeTickerUpdates();
            prev.UnsubscribeTradeUpdates();
        }
        void UpdateGrid() {
            this.askGridControl.DataSource = Ticker.OrderBook.Asks;
            this.bidGridControl.DataSource = Ticker.OrderBook.Bids;
            this.tradeHistoryItemBindingSource.DataSource = Ticker.TradeHistory;
        }
        void SubscribeEvents() {
            Ticker.OrderBook.OnChanged += OnTickerOrderBookChanged;
            Ticker.Changed += OnTickerChanged;
            Ticker.HistoryItemAdd += OnTickerHistoryItemAdded;
            Ticker.TradeHistoryAdd += OnTickerTradeHistoryAdd;
            if(IsHandleCreated) {
                Ticker.SubscribeOrderBookUpdates();
                Ticker.SubscribeTickerUpdates();
                Ticker.SubscribeTradeUpdates();
            }
        }

        private void OnTickerTradeHistoryAdd(object sender, EventArgs e) {
            if(IsHandleCreated)
                BeginInvoke(new MethodInvoker(this.tradeGridControl.RefreshDataSource));
        }
        
        private void OnTickerHistoryItemAdded(object sender, EventArgs e) {
        }

        private void OnTickerChanged(object sender, EventArgs e) {
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            if(Ticker != null) {
                Ticker.SubscribeOrderBookUpdates();
                Ticker.SubscribeTickerUpdates();
                Ticker.SubscribeTradeUpdates();
            }
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
            //this.bidGridControl.Invalidate();
            //this.bidGridControl.Update();
        }
        void RefreshAskGrid() {
            this.askGridControl.RefreshDataSource();
            this.askGridView.TopRowIndex = Ticker.OrderBook.Asks.Count;
            //this.askGridControl.Invalidate();
            //this.askGridControl.Update();
        }

        private void askGridControl_Resize(object sender, EventArgs e) {
            this.askGridView.TopRowIndex = this.askGridView.DataRowCount;
        }

        private void sidePanel2_Resize(object sender, EventArgs e) {
            this.askPanel.Height = this.orderBookPanel.Height / 2;
        }
    }
}
