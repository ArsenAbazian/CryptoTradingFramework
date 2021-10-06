using Crypto.Core;
using Crypto.Core.Common;
using CryptoMarketClient.Helpers;
using CryptoMarketClient.Poloniex;
using DevExpress.Data.Filtering;
using DevExpress.Skins;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
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
    public partial class TickersCollectionForm : XtraForm {
        public TickersCollectionForm(Exchange exchange) {
            InitializeComponent();
            Exchange = exchange;
            Text = Exchange.Name;
            this.ribbonPage1.Text = "Exchanges";
            this.ribbonPageGroup1.Text = exchange.Name;
            this.colIsSelected.MaxWidth = this.gridView1.RowHeight;
        }

        public Exchange Exchange { get; set; }

        System.Threading.Timer threadTimer;
        public System.Threading.Timer ThreadTimer {
            get {
                if(threadTimer == null) {
                    threadTimer = new System.Threading.Timer(OnThreadUpdate);
                    threadTimer.Change(0, 9000);
                }
                return threadTimer;
            }
        }

        System.Windows.Forms.Timer connectionCheckTimer;
        public System.Windows.Forms.Timer ConnectionCheckTimer {
            get {
                if(connectionCheckTimer == null) {
                    connectionCheckTimer = new System.Windows.Forms.Timer();
                    connectionCheckTimer.Interval = 3000;
                    connectionCheckTimer.Tick += OnConnectionTimerTick;
                }
                return connectionCheckTimer;
            }
        }

        private void OnConnectionTimerTick(object sender, EventArgs e) {
            if(Exchange.TickersSocketState == SocketConnectionState.None) {
                this.biConnectionStatus.ImageOptions.SvgImage = this.svgImageCollection1["information"];
                this.biConnectionStatus.Caption = "Initializing...";
            }
            else if(Exchange.TickersSocketState == SocketConnectionState.Connecting) {
                this.biConnectionStatus.ImageOptions.SvgImage = this.svgImageCollection1["connecting"];
                this.biConnectionStatus.Caption = "Connecting...";
            }
            else if(Exchange.TickersSocketState == SocketConnectionState.DelayRecv) {
                this.biConnectionStatus.ImageOptions.SvgImage = this.svgImageCollection1["datadelay"];
                this.biConnectionStatus.Caption = "No Data...";
                this.biReconnect.Visibility = BarItemVisibility.Always;
            }
            else if(Exchange.TickersSocketState == SocketConnectionState.Disconnected) {
                this.biConnectionStatus.ImageOptions.SvgImage = this.svgImageCollection1["disconnected"];
                this.biConnectionStatus.Caption = "Disconnected.";
                this.biReconnect.Visibility = BarItemVisibility.Always;
            }
            else if(Exchange.TickersSocketState == SocketConnectionState.Error) {
                this.biConnectionStatus.ImageOptions.SvgImage = this.svgImageCollection1["disconnected"];
                this.biConnectionStatus.Caption = "Socket Error.";
                this.biReconnect.Visibility = BarItemVisibility.Always;
            }
            else if(Exchange.TickersSocketState == SocketConnectionState.TooLongQue) {
                this.biConnectionStatus.ImageOptions.SvgImage = this.svgImageCollection1["disconnected"];
                this.biConnectionStatus.Caption = "Missing incremental update.";
                this.biReconnect.Visibility = BarItemVisibility.Always;
            }
            else if((DateTime.Now - Exchange.LastWebSocketRecvTime).TotalSeconds > 5) {
                if((DateTime.Now - Exchange.LastWebSocketRecvTime).TotalSeconds > 20) {
                    this.biConnectionStatus.ImageOptions.SvgImage = this.svgImageCollection1["disconnected"];
                    this.biConnectionStatus.Caption = "Connection Interrupted.";
                    this.biReconnect.Visibility = BarItemVisibility.Always;
                }
                else {
                    this.biConnectionStatus.ImageOptions.SvgImage = this.svgImageCollection1["datadelay"];
                    this.biConnectionStatus.Caption = "Waiting...";
                }
            }
            else {
                SetInfoConnected();
            }
        }

        private void SetInfoConnected() {
            this.biConnectionStatus.ImageOptions.SvgImage = this.biConnected.ImageOptions.SvgImage;
            this.biConnectionStatus.Caption = "Connected.";
            this.biReconnect.Visibility = BarItemVisibility.Never;
        }

        void SubscribeWebSocket() {
            Exchange.StartListenTickersStream();
            ConnectionCheckTimer.Start();
            this.gridView1.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.True;
        }

        protected int BaseCurrencyGroupIndex { get { return 897; } }
        protected virtual void InitializeBaseCurrencies() {
            var groups = Exchange.Tickers.GroupBy(t => t.BaseCurrency);
            AccordionControlElement groupElement = new AccordionControlElement() { Style = ElementStyle.Group };
            groupElement.Text = "Markets";
            this.accordionControl1.Elements.Add(groupElement);
            groupElement.Expanded = true;
            foreach(var group in groups) {
                AccordionControlElement item = new AccordionControlElement() { Style = ElementStyle.Item };
                item.Text = group.Key;
                item.Click += OnBaseCurrencyItemClick;
                if(group.Key == "BTC")
                    SelectedAccordionItem = item;
                groupElement.Elements.Add(item);
            }
            if(SelectedAccordionItem == null)
                SelectedAccordionItem = groupElement.Elements.Count > 0? groupElement.Elements[0]: null;
            this.accordionControl1.SelectElement(SelectedAccordionItem);
            UpdateTickersAccordingBaseCurrency();
        }

        protected AccordionControlElement SelectedAccordionItem { get; set; }
        private void OnBaseCurrencyItemClick(object sender, EventArgs e) {
            SelectedAccordionItem = (AccordionControlElement)sender;
            this.accordionControl1.SelectElement(SelectedAccordionItem);
            UpdateTickersAccordingBaseCurrency();
        }

        private void OnBaseCurrencyCheckedChanged(object sender, ItemClickEventArgs e) {
            UpdateTickersAccordingBaseCurrency();
        }

        protected virtual void UpdateTickersAccordingBaseCurrency() {
            string baseCurrency = GetSelectedBaseCurrency();
            List<Ticker> list = Exchange.Tickers.Where(t => t.BaseCurrency == baseCurrency).ToList();
            this.gridControl1.DataSource = list;
        }

        protected string GetSelectedBaseCurrency() {
            if(SelectedAccordionItem == null)
                return string.Empty;
            return SelectedAccordionItem.Text;
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            Exchange.Connect();
            InitializeBaseCurrencies();
            UpdateTickersAccordingBaseCurrency();
            HasShown = true;
            UpdateSelectedTickersFromExchange();
            if(!Exchange.SupportWebSocket(WebSocketType.Tickers)) {
                ThreadTimer.InitializeLifetimeService();
                SetInfoConnected();
            }
            else {
                Exchange.TickerChanged += OnWebSocketTickerUpdate;
                Exchange.TickersUpdate += OnWebSocketTickersUpdate;
                SubscribeWebSocket();
            }
        }

        private void OnWebSocketTickersUpdate(object sender, EventArgs e) {
            if(!IsHandleCreated || IsDisposed)
                return;
            BeginInvoke(new MethodInvoker(() => this.gridView1.RefreshData()));
        }

        private void OnWebSocketTickerUpdate(object sender, TickerUpdateEventArgs e) {
            UpdateRow(e.Ticker);
        }

        void UpdateSelectedTickersFromExchange() {
            UpdatePinnedItems();
        }
        protected bool IsUpdating { get; set; }
        void OnThreadUpdate(object state) {
            if(IsUpdating || !Exchange.IsInitialized)
                return;
            IsUpdating = true;
            try {
                Exchange.UpdateTickersInfo();
                for(int i = 0; i < Exchange.Tickers.Count; i++) {
                    Exchange.Tickers[i].UpdateTrailings();
                }
            }
            finally {
                IsUpdating = false;
            }
            if(IsHandleCreated)
                BeginInvoke(new Action(UpdateGridAll));
        }
        void UpdateRow(Ticker t) {
            int index = Exchange.Tickers.IndexOf(t);
            int rowHandle = this.gridView1.GetRowHandle(index);
            this.gridView1.RefreshRow(rowHandle);
        }
        void UpdateGrid(Ticker info) {
            int rowHandle = this.gridView1.GetRowHandle(info.Index);
            this.gridView1.RefreshRow(rowHandle);
        }
        void UpdateGridAll() {
            this.gridView1.RefreshData();
        }

        protected bool HasShown { get; set; }
        
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ShowDetailsForSelectedItemCore();
        }

        void ShowDetailsForSelectedItemCore() {
            ShowDetailsForSelectedItemCore((Ticker)this.gridView1.GetRow(this.gridView1.FocusedRowHandle));
        }
        void ShowDetailsForSelectedItemCore(Ticker t) {
            if(this.gridView1.FocusedRowHandle == GridControl.InvalidRowHandle)
                return;
            TickerForm form = new TickerForm();
            form.MarketName = t.HostName;
            form.Ticker = t;
            form.MdiParent = MdiParent;
            form.Show();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e) {
            ShowDetailsForSelectedItemCore();
        }

        //PoloniexAccountBalancesForm accountForm;
        //protected PoloniexAccountBalancesForm AccountForm {
        //    get {
        //        if(accountForm == null || accountForm.IsDisposed)
        //            accountForm = new PoloniexAccountBalancesForm();
        //        return accountForm;
        //    }
        //}

        Form accountForm;
        protected Form AccountForm {
            get {
                if(accountForm == null || accountForm.IsDisposed)
                    accountForm = new AccountBalancesForm(Exchange);
                return accountForm;
            }
        }

        private void bbShowBalances_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            AccountForm.MdiParent = MdiParent;
            AccountForm.Show();
            AccountForm.Activate();
        }

        OpenedOrdersForm ordersForm;
        protected OpenedOrdersForm OrdersForm {
            get {
                if(ordersForm == null || ordersForm.IsDisposed)
                    ordersForm = new OpenedOrdersForm(Exchange);
                return ordersForm;
            }
        }
        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            OrdersForm.MdiParent = MdiParent;
            OrdersForm.Show();
            OrdersForm.Activate();
        }

        protected void UpdatePinnedItems() {
            for(int i = 0; i < Exchange.PinnedTickers.Count; i++) {
                PinnedTickerInfo info = Exchange.PinnedTickers[i];
                Ticker t = Exchange.Tickers.FirstOrDefault(tt => tt.BaseCurrency == info.BaseCurrency && tt.MarketCurrency == info.MarketCurrency);
                if(t != null)
                    t.IsSelected = true;
            }
        }

        private void repositoryItemCheckEdit1_EditValueChanged(object sender, EventArgs e) {
            this.gridView1.CloseEditor();
            Ticker ticker = (Ticker)this.gridView1.GetFocusedRow();
            if(ticker.IsSelected)
                Exchange.PinnedTickers.Add(new PinnedTickerInfo() { BaseCurrency = ticker.BaseCurrency, MarketCurrency = ticker.MarketCurrency });
            else {
                PinnedTickerInfo info = Exchange.PinnedTickers.FirstOrDefault(p => p.BaseCurrency == ticker.BaseCurrency && p.MarketCurrency == ticker.MarketCurrency);
                Exchange.PinnedTickers.Remove(info);
            }
        }
        
        bool IsTickerPinned(Ticker t) {
            return Exchange.PinnedTickers.FirstOrDefault(i => i.BaseCurrency == t.BaseCurrency && i.MarketCurrency == t.MarketCurrency) != null;
        }

        private void gridControl1_Click(object sender, EventArgs e) {

        }

        private void bbMonitorOnlySelected_CheckedChanged(object sender, ItemClickEventArgs e) {

        }

        private void barManager1_ItemDoubleClick(object sender, ItemClickEventArgs e) {
            if(e.Item.Tag is PinnedTickerInfo) {
                Ticker t = Exchange.GetTicker((PinnedTickerInfo)e.Item.Tag);
                ShowDetailsForSelectedItemCore(t);
            }
        }

        private void gridView1_GetThumbnailImage(object sender, DevExpress.XtraGrid.Views.Grid.GridViewThumbnailImageEventArgs e) {
            Ticker t = (Ticker)Exchange.Tickers[e.DataSourceIndex];
            e.ThumbnailImage = new Bitmap(CurrencyLogoProvider.GetLogoImage(t.MarketCurrency), new Size(128, 128));
        }

        private void biReconnect_ItemClick(object sender, ItemClickEventArgs e) {
            BeginInvoke(new MethodInvoker(() => {
                this.biReconnect.Visibility = BarItemVisibility.Never;
                Exchange.StopListenTickersStream();
                Exchange.StartListenTickersStream();
            }));
        }

        //AccounTradesCollectionForm tradesForm;
        //protected AccounTradesCollectionForm TradesForm {
        //    get {
        //        if(tradesForm == null || tradesForm.IsDisposed)
        //            tradesForm = new AccounTradesCollectionForm(Exchange);
        //        return tradesForm;
        //    }
        //}
        private void barButtonItem2_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            OrdersForm.MdiParent = MdiParent;
            OrdersForm.Show();
            OrdersForm.Activate();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e) {
            //TradesForm.MdiParent = MdiParent;
            //TradesForm.Show();
            //TradesForm.Activate();
        }

        private void bcShowOnlyFavorite_CheckedChanged(object sender, ItemClickEventArgs e) {
            if(!this.bcShowOnlyFavorite.Checked)
                this.gridView1.ActiveFilterString = string.Empty;
            else
                this.gridView1.ActiveFilterCriteria = new BinaryOperator("IsSelected", true);
        }

        private void biBestFit_ItemClick(object sender, ItemClickEventArgs e) {
            this.gridView1.BestFitColumns();
        }
    }
}
