using CryptoMarketClient.Common;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public partial class TickerForm : XtraForm {
        public TickerForm() {
            InitializeComponent();
        }

        protected virtual void DisposeCore() {
            Timer.Dispose();
            Ticker = null;
        }

        public string MarketName { get; set; }
        //protected override bool AllowUpdateInactive => true;
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            Timer.InitializeLifetimeService();
        }
        System.Threading.Timer timer;
        public System.Threading.Timer Timer {
            get {
                if(timer == null) {
                    timer = new System.Threading.Timer(OnThreadUpdate);
                    timer.Change(0, 500);
                }
                return timer;
            }
        }
        protected void OnThreadUpdate(object state) {
            if(Ticker != null)
                Ticker.UpdateTicker();
            if(Ticker != null)
                Ticker.UpdateOrderBook();
            if(Ticker != null)
                Ticker.UpdateTrades();
            if(Ticker != null)
                Ticker.UpdateOpenedOrders();
            if(Ticker != null)
                Ticker.UpdateTrailings();
            if(Ticker != null)
                Ticker.Time = DateTime.UtcNow;
        }

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

        void UpdateBuySellSettings() {
            if(Ticker == null)
                return;
            TrailingSettings s = new TrailingSettings(Ticker);
            this.buySettingsControl.Settings = s;
        }
        void OnTickerChanged(TickerBase prev) {
            if(prev != null) {
                prev.IsOpened = false;
                ClearText();
                ClearGrid();
                ClearChart();
                UnsubscribeEvents(prev);
            }
            this.tickerInfoControl.Ticker = Ticker;
            this.myTradesCollectionControl1.Ticker = Ticker;
            this.activeTrailingCollectionControl1.Ticker = Ticker;
            if(Ticker == null)
                return;
            this.rpMain.Text = Ticker.Name;
            Ticker.IsOpened = true;
            UpdateTickerInfoControlHeight();
            UpdateText();
            UpdateGrid();
            UpdateChart();
            UpdateDockPanels();
            UpdateBuySellSettings();
            SubscribeEvents();
        }
        void UpdateDockPanels() {
            if(Ticker == null)
                return;
            foreach(DockPanel panel in this.dockManager1.RootPanels)
                panel.Text = Ticker.Name + " - " + panel.Text;
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
        void RefreshBidGrid() {
            this.orderBookControl1.Bids = Ticker.OrderBook.Bids;
        }
        void RefreshAskGrid() {
            this.orderBookControl1.Asks = Ticker.OrderBook.Asks;
            this.orderBookControl1.RefreshAsks();
        }

        private void biSell_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //double price = 0;
            //double amount = 0;
            //try {
            //    price = Convert.Todouble(this.bePrice.EditValue);
            //}
            //catch(Exception ee) {
            //    XtraMessageBox.Show("Can't sell: get price = " + ee.ToString());
            //    return;
            //}
            //if(Ticker.HighestBid / price > 10m) {
            //    if(XtraMessageBox.Show("It seems that you made mistake on price?", "Sell", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //        return;
            //}
            //try {
            //    amount = Convert.Todouble(this.beAmount.EditValue);
            //}
            //catch(Exception ee) {
            //    XtraMessageBox.Show("Can't sell: get amount = " + ee.ToString());
            //    return;
            //}
            //if(!Ticker.Sell(price, amount)) {
            //    XtraMessageBox.Show("Error: can't place sell " + amount + " " + Ticker.MarketCurrency + " for " + price);
            //    return;
            //}
            //XtraMessageBox.Show("Place sell " + amount + " " + Ticker.MarketCurrency + " for " + price);
        }

        private void biSellMarket_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

        }

        private void orderBookControl1_SelectedAskRowChanged(object sender, SelectedOrderBookEntryChangedEventArgs e) {
            this.buySettingsControl.Settings.BuyPrice = e.Entry.Value;
            this.buySettingsControl.Settings.Amount = e.Entry.Amount;
        }

        private void orderBookControl1_SelectedBidRowChanged(object sender, SelectedOrderBookEntryChangedEventArgs e) {

        }

        private void dockManager1_ActivePanelChanged(object sender, ActivePanelChangedEventArgs e) {
            if(e.Panel == this.dpMyTrades) {
                this.myTradesCollectionControl1.UpdateTrades();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            using(GrabDataSettingsForm form = new GrabDataSettingsForm()) {
                form.Ticker = Ticker;
                form.ShowDialog();
            }
        }
    }
}
