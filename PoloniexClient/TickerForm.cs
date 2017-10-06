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

        public string MarketName { get; set; }

        TickerBase ticker;
        public TickerBase Ticker {
            get {
                return ticker;
            }
            set {
                if(Ticker == value)
                    return;
                TickerBase prev = Ticker;
                ticker = value;
                OnTickerChanged(prev);
            }
        }
        
        void OnTickerChanged(TickerBase prev) {
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
            Text = MarketName + " - " + Ticker.Name;
            this.ribbonPageGroup1.Text = Ticker.Name;
        }
        void ClearGrid() {
            this.orderBookControl1.Asks = null;
            this.orderBookControl1.Bids = null;
            this.tradeHistoryItemBindingSource.DataSource = null;
        }
        void UnsubscribeEvents(TickerBase prev) {
            prev.OrderBook.OnChanged -= OnTickerOrderBookChanged;
            prev.Changed -= OnTickerChanged;
            prev.HistoryItemAdd -= OnTickerHistoryItemAdded;
            prev.TradeHistoryAdd -= OnTickerTradeHistoryAdd;
        }
        void UpdateGrid() {
            this.orderBookControl1.Bids = Ticker.OrderBook.Bids;
            this.orderBookControl1.Asks = Ticker.OrderBook.Asks;
            this.tradeHistoryItemBindingSource.DataSource = Ticker.TradeHistory;
        }
        void SubscribeEvents() {
            Ticker.OrderBook.OnChanged += OnTickerOrderBookChanged;
            Ticker.Changed += OnTickerChanged;
            Ticker.HistoryItemAdd += OnTickerHistoryItemAdded;
            Ticker.TradeHistoryAdd += OnTickerTradeHistoryAdd;
        }

        private void OnTickerTradeHistoryAdd(object sender, EventArgs e) {
            //if(IsHandleCreated)
            //    BeginInvoke(new MethodInvoker(this.tradeGridControl.RefreshDataSource));
        }
        
        private void OnTickerHistoryItemAdded(object sender, EventArgs e) {
        }

        private void OnTickerChanged(object sender, EventArgs e) {
        }

        private void OnTickerOrderBookChanged(object sender, OrderBookEventArgs e) {
            if(!IsHandleCreated)
                return;
            BeginInvoke(new Action(RefreshAskGrid));
            BeginInvoke(new Action(RefreshBidGrid));
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
            this.orderBookControl1.Bids = Ticker.OrderBook.Bids;
        }
        void RefreshAskGrid() {
            this.orderBookControl1.Asks = Ticker.OrderBook.Asks;
        }

        private void biSell_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            decimal price = 0;
            decimal amount = 0;
            try {
                price = Convert.ToDecimal(this.bePrice.EditValue);
            }
            catch(Exception ee) {
                XtraMessageBox.Show("Can't sell: get price = " + ee.ToString());
                return;
            }
            if(Ticker.HighestBid / price > 10m) {
                if(XtraMessageBox.Show("It seems that you made mistake on price?", "Sell", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    return;
            }
            try {
                amount = Convert.ToDecimal(this.beAmount.EditValue);
            }
            catch(Exception ee) {
                XtraMessageBox.Show("Can't sell: get amount = " + ee.ToString());
                return;
            }
            if(!Ticker.Sell(price, amount)) {
                XtraMessageBox.Show("Error: can't place sell " + amount + " " + Ticker.MarketCurrency + " for " + price);
                return;
            }
            XtraMessageBox.Show("Place sell " + amount + " " + Ticker.MarketCurrency + " for " + price);
        }

        private void biSellMarket_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

        }
    }
}
