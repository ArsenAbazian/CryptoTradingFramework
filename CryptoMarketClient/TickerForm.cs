using Crypto.Core;
using Crypto.Core.Common;
using CryptoMarketClient.Helpers;
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
using Crypto.Core.Helpers;
using CryptoMarketClient.Strategies;
using Crypto.UI.Helpers;

namespace CryptoMarketClient {
    public partial class TickerForm : ThreadUpdateForm {
        public TickerForm() {
            InitializeComponent();
            this.beHr24HighLow.EditHeight = ScaleUtils.ScaleValue(50);

            GridTransparentRowHelper.Apply(this.gvAccountTrades);
            GridTransparentRowHelper.Apply(this.gvInfo);
            GridTransparentRowHelper.Apply(this.gvOpenedOrders);
            GridTransparentRowHelper.Apply(this.gvPositions);
            GridTransparentRowHelper.Apply(this.gvTrades);

            ((FormatConditionRuleValue)this.gvTrades.FormatRules[0].Rule).Appearance.ForeColor = Exchange.BidColor;
            ((FormatConditionRuleValue)this.gvTrades.FormatRules[1].Rule).Appearance.ForeColor = Exchange.AskColor;

            ((FormatConditionRuleValue)this.gvOpenedOrders.FormatRules[0].Rule).Appearance.ForeColor = Exchange.AskColor;
            ((FormatConditionRuleValue)this.gvOpenedOrders.FormatRules[1].Rule).Appearance.ForeColor = Exchange.BidColor;

            ((FormatConditionRuleValue)this.gvAccountTrades.FormatRules[0].Rule).Appearance.ForeColor = Exchange.BidColor;
            ((FormatConditionRuleValue)this.gvAccountTrades.FormatRules[1].Rule).Appearance.ForeColor = Exchange.AskColor;
        }

        protected virtual void DisposeCore() {
            Ticker = null;
        }

        protected override void OnClosing(CancelEventArgs e) {
            base.OnClosing(e);
            Ticker = null;
            this.workspaceManager1.SaveWorkspace("TickerFormDefault", "TickerFormWorkspaceDefault.xml", true);
        }

        public string MarketName { get; set; }
        protected override bool AllowUpdateInactive => true;
        protected DateTime LastUpdateTime { get; set; }
        protected override bool AutoStartThread => true;
        protected override int UpdateInervalMs => 2000;
        protected override void OnThreadUpdate() {
            Ticker t = Ticker;
            if(t == null)
                return;
            if(t != null) {
                t.UpdateOpenedOrders();
                if(this.gcOpenedOrders.DataSource != t.OpenedOrders) {
                    BeginInvoke(new MethodInvoker(() => this.gcOpenedOrders.DataSource = t.OpenedOrders ));
                }

            }
            if(!t.Exchange.SupportWebSocket(WebSocketType.Trades))
                t.UpdateTrades();
            //if(Ticker != null && !Ticker.IsUpdatingAccountTrades)
            //    Ticker.UpdateAccountTrades();
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            //try {
            //    if(System.IO.File.Exists("TickerFormWorkspaceDefault.xml")) {
            //        if(this.workspaceManager1.LoadWorkspace("TickerFormDefault", "TickerFormWorkspaceDefault.xml")) {
            //            //this.workspaceManager1.ApplyWorkspace("TickerFormDefault");
            //            //UpdateDockPanels();
            //        }
            //    }
            //}
            //catch(Exception ee) {
            //    Telemetry.Default.TrackException(ee);
            //}

            if(Ticker == null)
                return;
            Icon = CurrencyLogoProvider.GetFormIcon(Ticker.MarketCurrency);
            this.rpMain.Text = Ticker.Name;

            ThreadManager manager = new ThreadManager();
            manager.OwnerControl = this.gcTrades;
            Ticker.AccountShortTradeHistory.ThreadManager = manager;
            Ticker.ShortTradeHistory.ThreadManager = manager;
            Ticker.OrderBook.SubscribeUpdateEntries(true);
            Ticker.IsOpened = true;
            UpdateText();
            UpdateGrid();
            UpdateChart();
            UpdateDockPanels();
            UpdateBuySellSettings();
            UpdateBalances();
            if(!Ticker.Exchange.SupportWebSocket(WebSocketType.Trades))
                UpdateTrades();
            UpdateAccountTrades();
            
            Ticker.StartListenTickerStream();
            SubscribeEvents();
        }

        private void UpdateTrades() {
            Ticker.LockTrades();
            try {
                Ticker.ClearTradeHistory();
                Ticker.Exchange.UpdateTrades(Ticker);
                this.gvTrades.BestFitColumns();
            }
            finally {
                Ticker.UnlockTrades();
            }
        }

        private void UpdateAccountTrades() {
            Ticker.LockAccountTrades();
            try {
                Ticker.ClearMyTradeHistory();
                Ticker.Exchange.UpdateAccountTrades(Ticker);
                this.gvAccountTrades.BestFitColumns();
            }
            finally {
                Ticker.UnlockAccountTrades();
            }
        }

        protected bool UpdatingBalances { get; set; }
        private void RunUpdateAccountTradesTask() {
            if(Ticker == null)
                return;
            if(Ticker.IsUpdatingAccountTrades)
                return;
            Task.Factory.StartNew(() => { 
                Ticker?.UpdateAccountTrades();
            });
        }
        private void UpdateOpenedOrders() {
            if(Ticker == null)
                return;
            if(Ticker.IsUpdatingOpenedOrders)
                return;
            Task.Factory.StartNew(() => { 
                Ticker?.UpdateOpenedOrders();
            });
        }
        private void UpdateBalances() {
            if(UpdatingBalances)
                return;
            UpdatingBalances = true;
            Task.Factory.StartNew(() => { 
                Ticker?.UpdateBalance(Ticker.BaseCurrency);
                Ticker?.UpdateBalance(Ticker.MarketCurrency);
                UpdatingBalances = false;
                BeginInvoke(new MethodInvoker(() => UpdateBalanceText()));
            });
        }

        protected void UpdateBalanceText() {
            this.buySettingsControl.Settings.AvailableForBuy = Ticker.BaseCurrencyBalance;
            this.buySettingsControl.Settings.AvailableForSell = Ticker.MarketCurrencyBalance;
            this.siBalance.Caption = 
                        Ticker.BaseCurrencyDisplayName + ": " + Ticker.BaseCurrencyBalance.ToString("0.00000000") + "   " +
                        Ticker.MarketCurrencyDisplayName + ": " + Ticker.MarketCurrencyBalance.ToString("0.00000000");
            this.siUpdated.Caption = DateTime.Now.ToLongTimeString();
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
            TradingSettings s = new TradingSettings(Ticker);
            this.buySettingsControl.Settings = s;
        }
        void OnTickerChanged(Ticker prev) {
            if(prev != null) {
                prev.ShortTradeHistory.ThreadManager = null;
                prev.IsOpened = false;
                prev.OrderBook.SubscribeUpdateEntries(false);
                ClearText();
                ClearGrid();
                ClearChart();
                UnsubscribeEvents(prev);
                prev.StopListenTickerStream();
            }
            UpdateTickerInfoBar();
            this.activeTrailingCollectionControl1.Ticker = Ticker;
            this.buySettingsControl.Ticker = Ticker;
            this.eventsCollectionControl1.Ticker = Ticker;
            this.dpPositions.Visibility = Ticker != null && Ticker.Exchange.SupportPositions ? DockVisibility.Visible : DockVisibility.Hidden;
        }
        void UpdateTickerInfoBar() {
            if(Ticker == null)
                return;
            this.siCurrencyIcon.Caption = "";
            this.siCurrencyIcon.Glyph = CurrencyLogoProvider.GetLogo32Image(Ticker.MarketCurrency);
            this.siExchangeIcon.Caption = "<b><size=+3>" + Ticker.Exchange.Name + "</size></b>";
            UpdateTickerInfoBarCore();
        }
        protected BindingList<ValueInfo> ValuesList { get; } = new BindingList<ValueInfo>();
        protected Dictionary<string, ValueInfo> Values { get; } = new Dictionary<string, ValueInfo>();
        void UpdateTickerInfoBarCore() {
            //if(ValuesList.Count == 0) {
            //    ValuesList.Add(new ValueInfo() { Name = "last", Text = "Last Price:" });
            //    ValuesList.Add(new ValueInfo() { Name = "bid", Text = "Highest Bid:" });
            //    ValuesList.Add(new ValueInfo() { Name = "ask", Text = "Lowest Ask: " });
            //    ValuesList.Add(new ValueInfo() { Name = "low", Text = "24h Low:" });
            //    ValuesList.Add(new ValueInfo() { Name = "high", Text = "24h High:" });
            //    ValuesList.Add(new ValueInfo() { Name = "volume", Text = "24h Volume:" });

            //    foreach(var val in ValuesList)
            //        Values.Add(val.Name, val);
            //}

            int multiplier = 10000000;
            if(Ticker.Last >= 1000)
                multiplier = 1;
            this.repositoryItemTrackBar1.Minimum = (int)(Ticker.Hr24Low * multiplier);
            this.repositoryItemTrackBar1.Maximum = (int)(Ticker.Hr24High * multiplier);
            this.beHr24HighLow.EditValue = Ticker.Last * multiplier;

            this.siLast.Caption = "Last Price<br><b>" + Ticker.LastString + "</b>";
            //this.siBid.Caption = "Highest Bid<br>" + Ticker.HighestBidString;
            this.si24High.Caption = "24h High<br><b>" + Ticker.Hr24High.ToString("0.########") + "<b>";
            this.siHr24Low.Caption = "24h Low<br><b>" + Ticker.Hr24Low.ToString("0.########") + "<b>";
            //this.siLowestAsk.Caption = "Lowest Ask<br>" + Ticker.LowestAskString;
            this.si24Volume.Caption = "24h Volume<br><b>" + Ticker.Volume.ToString() + " " + Ticker.BaseCurrency + "</b>";

            //this.gvInfo.BeginDataUpdate();
            //try {
            //    Values["last"].Value = Ticker.LastString;
            //    Values["bid"].Value = Ticker.HighestBidString;
            //    Values["ask"].Value = Ticker.LowestAskString;
            //    Values["high"].Value = Ticker.GetString(Ticker.Hr24High);
            //    Values["low"].Value = Ticker.GetString(Ticker.Hr24Low);
            //    Values["volume"].Value = Ticker.GetString(Ticker.Volume);
            //}
            //finally {
            //    this.gvInfo.EndDataUpdate();
            //}
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
            this.eventsCollectionControl1.Ticker = null;
            this.gcOpenedOrders.DataSource = null;
        }
        void UnsubscribeEvents(Ticker prev) {
            if(prev.Exchange.SupportWebSocket(WebSocketType.Ticker))
                prev.StopListenTickerStream();
            if(prev.Exchange.SupportWebSocket(WebSocketType.Kline))
                prev.StopListenKlineStream();
            prev.OrderBook.Changed -= OnTickerOrderBookChanged;
            prev.Changed -= OnTickerChanged;
            prev.HistoryChanged -= OnTickerHistoryItemAdded;
            prev.TradeHistoryChanged -= OnTickerTradeHistoryChanged;
            prev.AccountTradeHistoryChanged -= OnAccountTradeHistoryChanged;
            prev.OpenedOrdersChanged -= OnTickerOpenedOrdersChanged;
        }

        protected bool AllowUpdateUI { get { return Ticker != null && IsHandleCreated && !IsDisposed; } }
        private void OnTickerOpenedOrdersChanged(object sender, EventArgs e) {
            if(!AllowUpdateUI)
                return;
            BeginInvoke(new MethodInvoker(() => {
                RunUpdateAccountTradesTask();
                this.gvOpenedOrders.RefreshData();
                ShowNotifications(Ticker.GetOpenedOrdersChangeNotifications());
            }));
        }

        //protected DevExpress.XtraBars.Alerter.AlertControl AlertControl { get; set; }
        void ShowNotifications(List<string> notifications) {
            foreach(string str in notifications) {
                NotificationManager.Notify(str);
            }
        }

        void UpdateGrid() {
            this.orderBookControl1.Bids = Ticker.OrderBook.Bids;
            this.orderBookControl1.Asks = Ticker.OrderBook.Asks;
            this.gcTrades.DataSource = new SortedReadOnlyArray<TradeInfoItem>(Ticker.ShortTradeHistory);
            this.gcAccountTrades.DataSource = new SortedReadOnlyArray<TradeInfoItem>(Ticker.AccountShortTradeHistory);
            //this.gcOpenedOrders.DataSource = Ticker.OpenedOrders;
        }

        void SubscribeEvents() {
            Ticker.OrderBook.Changed += OnTickerOrderBookChanged;
            Ticker.Changed += OnTickerChanged;
            Ticker.HistoryChanged += OnTickerHistoryItemAdded;
            Ticker.TradeHistoryChanged += OnTickerTradeHistoryChanged;
            Ticker.AccountTradeHistoryChanged += OnAccountTradeHistoryChanged;
            Ticker.OpenedOrdersChanged += OnTickerOpenedOrdersChanged;
        }

        private void OnAccountTradeHistoryChanged(object sender, TradeHistoryChangedEventArgs e) {
            if(IsHandleCreated) {
                BeginInvoke(new Action(() => {
                    if(!IsDisposed) {
                        this.gvAccountTrades.RefreshData();
                    }
                }));
            }
        }

        private void OnTickerTradeHistoryChanged(object sender, EventArgs e) {
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
            this.orderBookControl1.Asks = Ticker.OrderBook.Asks;
            this.orderBookControl1.RefreshAsks();
        }

        private void biSellMarket_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

        }

        private void orderBookControl1_SelectedAskRowChanged(object sender, SelectedOrderBookEntryChangedEventArgs e) {
            this.buySettingsControl.Settings.BuyPrice = e.Entry.Value;
            this.buySettingsControl.Settings.BuyAmount = e.Entry.Amount;
        }

        private void orderBookControl1_SelectedBidRowChanged(object sender, SelectedOrderBookEntryChangedEventArgs e) {
            this.buySettingsControl.Settings.SellPrice = e.Entry.Value;
            this.buySettingsControl.Settings.SellAmount = e.Entry.Amount;
        }

        private void dockManager1_ActivePanelChanged(object sender, ActivePanelChangedEventArgs e) {
            if(e.Panel == this.dpMyTrades) {
                RunUpdateAccountTradesTask();
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
                NotificationManager.Notify(LogType.Error, "Cancel Order", "Error canceling order. Check Log for last errors.");
                return;
            }
            UpdateBalances();
            Ticker.UpdateOpenedOrders();
            this.gvOpenedOrders.RefreshData();
            NotificationManager.Notify(LogType.Success, "Cancel Order", "Order canceled.");
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

        private void BiUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            UpdateBalances();
        }

        private void BuySettingsControl_Trade(object sender, TradeEventArgs e) {
            NotificationManager.Notify(LogType.Log, "Trade", "Order with id = " + e.Trade.OrderId + ". Status = " + e.Trade.OrderStatus);
            UpdateBalances();
            UpdateOpenedOrders();
        }

        private void BarButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            RunUpdateAccountTradesTask();
        }

        private void BsiStatis_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ((MainForm)MdiParent.FindForm()).ShowLogPanel();
        }

        private void eventsCollectionControl1_EventDoubleClick(object sender, TickerEventArgs e) {
            CandleStickData cd = Ticker.GetCandleStickItem(e.Event);
            if(cd == null) {
                XtraMessageBox.Show("This event is out of exist candlestick data. Please scroll to this time manually.", "Event");
                return;
            }
            this.tickerChartViewer1.NavigateTo(cd.Time);
        }

        private void biUpdateOrders_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            Ticker.Exchange.UpdateOpenedOrders(Ticker.Exchange.DefaultAccount, Ticker);
        }
    }

    public class ValueInfo : INotifyPropertyChanged {
        event PropertyChangedEventHandler changed;
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged {
            add { this.changed += value; }
            remove { this.changed -= value; }
        }

        public string Name { get; set; }
        public string Text { get; set; }
        string valueCore;
        public string Value {
            get { return valueCore; }
            set {
                if(Value == value)
                    return;
                valueCore = value;
                RaisePropertyChanged(nameof(Value));
            }
        }
        protected virtual void RaisePropertyChanged(string name) {
            if(this.changed != null)
                this.changed.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
