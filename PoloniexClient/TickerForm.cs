using CryptoMarketClient.Common;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
            this.beHr24HighLow.EditHeight = ScaleUtils.ScaleValue(50);
        }

        protected virtual void DisposeCore() {
            Timer.Dispose();
            Ticker = null;
        }

        protected override void OnClosing(CancelEventArgs e) {
            base.OnClosing(e);
            Ticker = null;
            this.workspaceManager1.SaveWorkspace("TickerFormDefault", "TickerFormWorkspaceDefault.xml", true);
        }

        public string MarketName { get; set; }
        //protected override bool AllowUpdateInactive => true;
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            try {
                if(System.IO.File.Exists("TickerFormWorkspaceDefault.xml")) {
                    if(this.workspaceManager1.LoadWorkspace("TickerFormDefault", "TickerFormWorkspaceDefault.xml")) {
                        this.workspaceManager1.ApplyWorkspace("TickerFormDefault");
                        UpdateDockPanels();
                    }
                }
            }
            catch(Exception ee) {
                Telemetry.Default.TrackException(ee);
            }
            Ticker.UpdateTrades();
            Timer.InitializeLifetimeService();
        }
        System.Threading.Timer timer;
        public System.Threading.Timer Timer {
            get {
                if(timer == null) {
                    timer = new System.Threading.Timer(OnThreadUpdate);
                    timer.Change(0, 2000);
                }
                return timer;
            }
        }
        protected void OnThreadUpdate(object state) {
            try {
                if(Ticker != null && !Ticker.Exchange.SupportWebSocket(WebSocketType.Tickers))
                    Ticker.UpdateTicker();
                if(Ticker != null && !Ticker.Exchange.SupportWebSocket(WebSocketType.Ticker))
                    Ticker.UpdateOrderBook();
                if(Ticker != null && !Ticker.Exchange.SupportWebSocket(WebSocketType.Ticker))
                    Ticker.UpdateTrades();
                if(Ticker != null)
                    Ticker.UpdateOpenedOrders();
                if(Ticker != null)
                    Ticker.UpdateTrailings();
                if(Ticker != null)
                    Ticker.Time = DateTime.UtcNow;
                if(IsHandleCreated)
                    BeginInvoke(new MethodInvoker(UpdateTickerInfoBarCore));
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
            }
        }

        Ticker ticker;
        public Ticker Ticker {
            get {
                return ticker;
            }
            set {
                if(Ticker == value)
                    return;
                Ticker prev = Ticker;
                ticker = value;
                try {
                    OnTickerChanged(prev);
                }
                catch(Exception e) {
                    Telemetry.Default.TrackException(e);
                }
            }
        }

        void UpdateBuySellSettings() {
            if(Ticker == null)
                return;
            TrailingSettings s = new TrailingSettings(Ticker);
            this.buySettingsControl.Settings = s;
        }
        void OnTickerChanged(Ticker prev) {
            if(prev != null) {
                prev.IsOpened = false;
                prev.OrderBook.SubscribeUpdateEntries(false);
                ClearText();
                ClearGrid();
                ClearChart();
                UnsubscribeEvents(prev);
                prev.StopListenTickerStream();
            }
            UpdateTickerInfoBar();
            this.myTradesCollectionControl1.Ticker = Ticker;
            this.activeTrailingCollectionControl1.Ticker = Ticker;
            this.buySettingsControl.Ticker = Ticker;
            if(Ticker == null)
                return;
            Ticker.OrderBook.SubscribeUpdateEntries(true);
            Icon = Ticker.FormIcon;
            this.rpMain.Text = Ticker.Name;
            Ticker.IsOpened = true;
            UpdateText();
            UpdateGrid();
            UpdateChart();
            UpdateDockPanels();
            UpdateBuySellSettings();
            SubscribeEvents();
            Ticker.UpdateBalance(Ticker.MarketCurrency);
            Ticker.StartListenTickerStream();
        }
        void UpdateTickerInfoBar() {
            if(Ticker == null)
                return;
            this.siCurrencyIcon.Caption = "";
            this.siCurrencyIcon.Glyph = Ticker.Logo32;
            this.siExchangeIcon.Caption = "<b><size=+3>" + Ticker.Exchange.Name + "</size></b>";
            UpdateTickerInfoBarCore();
        }
        void UpdateTickerInfoBarCore() {
            this.siTime.Caption = DateTime.Now.ToString();
            this.repositoryItemTrackBar1.Minimum = (int)(Ticker.Hr24Low * 10000000);
            this.repositoryItemTrackBar1.Maximum = (int)(Ticker.Hr24High * 10000000);
            this.beHr24HighLow.EditValue = Ticker.Last * 10000000;
            this.siLast.Caption = "Last Price<br>" + Ticker.LastString;
            this.siBid.Caption = "Highest Bid<br>" + Ticker.HighestBidString;
            this.si24High.Caption = "24h High<br><b>" + Ticker.Hr24High.ToString() + "<b>";
            this.siHr24Low.Caption = "24h Low<br><b>" + Ticker.Hr24Low.ToString() + "<b>";
            this.siLowestAsk.Caption = "Lowest Ask<br>" + Ticker.LowestAskString;
            this.si24Volume.Caption = "24h Volume<br>" + Ticker.Volume.ToString() + " " + Ticker.BaseCurrency;
        }
        void UpdateDockPanels() {
            if(Ticker == null)
                return;
            foreach(DockPanel panel in this.dockManager1.RootPanels) {
                string[] parts = panel.Text.Split('-');
                if(parts.Length == 2) panel.Text = parts[1];
                panel.Text = Ticker.Name + " - " + panel.Text;
            }
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
            this.gcOpenedOrders.DataSource = null;
        }
        void UnsubscribeEvents(Ticker prev) {
            if(prev.Exchange.SupportWebSocket(WebSocketType.Ticker))
                prev.StopListenTickerStream();
            prev.OrderBook.OnChanged -= OnTickerOrderBookChanged;
            prev.Changed -= OnTickerChanged;
            prev.HistoryItemAdd -= OnTickerHistoryItemAdded;
            prev.TradeHistoryAdd -= OnTickerTradeHistoryAdd;
            prev.OpenedOrdersChanged -= OnTickerOpenedOrdersChanged;
        }

        private void OnTickerOpenedOrdersChanged(object sender, EventArgs e) {
            BeginInvoke(new MethodInvoker(() => { this.gvOpenedOrders.RefreshData(); }));
        }

        void UpdateGrid() {
            this.orderBookControl1.Bids = Ticker.OrderBook.Bids;
            this.orderBookControl1.Asks = Ticker.OrderBook.AsksInverted;
            this.tradeHistoryItemBindingSource.DataSource = Ticker.TradeHistory;
            this.gcOpenedOrders.DataSource = Ticker.OpenedOrders;
        }
        void SubscribeEvents() {
            if(Ticker.Exchange.SupportWebSocket(WebSocketType.Ticker)) {
                Ticker.TradeHistory.Clear();
                Ticker.Exchange.UpdateTrades(Ticker);
                Ticker.StartListenTickerStream();
            }
            Ticker.OrderBook.OnChanged += OnTickerOrderBookChanged;
            Ticker.Changed += OnTickerChanged;
            Ticker.HistoryItemAdd += OnTickerHistoryItemAdded;
            Ticker.TradeHistoryAdd += OnTickerTradeHistoryAdd;
            Ticker.OpenedOrdersChanged += OnTickerOpenedOrdersChanged;
        }

        private void OnTickerTradeHistoryAdd(object sender, EventArgs e) {
            if(IsHandleCreated) {
                BeginInvoke(new Action(() => {
                    if(!IsDisposed) {
                        this.gvTrades.RefreshData();
                    }
                }));
            }
        }
        
        private void OnTickerHistoryItemAdded(object sender, EventArgs e) {
        }

        private void OnTickerChanged(object sender, EventArgs e) {
            if(IsHandleCreated) {
                BeginInvoke(new Action(() => {
                    if(!IsDisposed) {
                        this.siBalance.Caption = "Balance: " + Ticker.MarketCurrencyBalance.ToString("0.00000000");
                        this.siUpdated.Caption = "Updated: " + Ticker.LastUpdateTime;
                    }
                }));
            }
        }

        private void OnTickerOrderBookChanged(object sender, OrderBookEventArgs e) {
            if(!IsHandleCreated)
                return;
            BeginInvoke(new Action(RefreshAskGrid));
            BeginInvoke(new Action(RefreshBidGrid));
        }
        void RefreshBidGrid() {
            this.orderBookControl1.Bids = Ticker.OrderBook.Bids;
            this.orderBookControl1.RefreshBids();
        }
        void RefreshAskGrid() {
            this.orderBookControl1.Asks = Ticker.OrderBook.AsksInverted;
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
            this.buySettingsControl.Settings.TradePrice = e.Entry.Value;
            this.buySettingsControl.Settings.Amount = e.Entry.Amount;
        }

        private void orderBookControl1_SelectedBidRowChanged(object sender, SelectedOrderBookEntryChangedEventArgs e) {
            this.buySettingsControl.Settings.TradePrice = e.Entry.Value;
            this.buySettingsControl.Settings.Amount = e.Entry.Amount;
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

        protected void MakeCancel() {
            OpenedOrderInfo info = (OpenedOrderInfo)this.gvOpenedOrders.GetFocusedRow();
            if(info == null)
                return;
            if(!Ticker.Cancel(info)) {
                XtraMessageBox.Show("Error canceling order. Try again later.");
            }
            Ticker.UpdateOpenedOrders();
            this.gvOpenedOrders.RefreshData();
        }

        private void bbCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            MakeCancel();
        }

        private void gcOpenedOrders_MouseDown(object sender, MouseEventArgs e) {
            if(e.Button != MouseButtons.Right)
                return;
            GridHitInfo hitInfo = this.gvOpenedOrders.CalcHitInfo(e.Location);
            if(hitInfo.InDataRow) {
                this.gvOpenedOrders.FocusedRowHandle = hitInfo.RowHandle;
                this.popupMenu1.ShowPopup(this.gcOpenedOrders.PointToScreen(e.Location));
            }
        }
    }
}
