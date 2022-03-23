using Crypto.Core;
using Crypto.Core.Common;
using Crypto.UI.Helpers;
using CryptoMarketClient.Helpers;
using CryptoMarketClient.Poloniex;
using DevExpress.Data.Filtering;
using DevExpress.Skins;
using DevExpress.Sparkline;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class TickersCollectionForm : ThreadUpdateForm {
        public TickersCollectionForm(Exchange exchange) {
            InitializeComponent();
            Exchange = exchange;
            Text = Exchange.Name;
            this.ribbonPage1.Text = "Exchanges";
            this.ribbonPageGroup1.Text = exchange.Name;
            this.colIsSelected.MaxWidth = this.gvTikers.RowHeight;
            GridTransparentRowHelper.Apply(this.gvTikers);
        }

        public Exchange Exchange { get; set; }

        private void UpdateConnectionStatus() {
            if(!Exchange.SupportWebSocket(WebSocketType.Ticker))
                return;
            if(Exchange.TickersSocketState == SocketConnectionState.None) {
                this.bsiStatus.ImageOptions.SvgImage = this.svgImageCollection1["information"];
                this.bsiStatus.Caption = "Initializing...";
            }
            else if(Exchange.TickersSocketState == SocketConnectionState.Connecting) {
                this.bsiStatus.ImageOptions.SvgImage = this.svgImageCollection1["connecting"];
                this.bsiStatus.Caption = "Connecting...";
            }
            else if(Exchange.TickersSocketState == SocketConnectionState.DelayRecv) {
                this.bsiStatus.ImageOptions.SvgImage = this.svgImageCollection1["datadelay"];
                this.bsiStatus.Caption = "No Data...";
                this.biReconnect.Visibility = BarItemVisibility.Always;
            }
            else if(Exchange.TickersSocketState == SocketConnectionState.Disconnected) {
                this.bsiStatus.ImageOptions.SvgImage = this.svgImageCollection1["disconnected"];
                this.bsiStatus.Caption = "Disconnected.";
                this.biReconnect.Visibility = BarItemVisibility.Always;
            }
            else if(Exchange.TickersSocketState == SocketConnectionState.Error) {
                this.bsiStatus.ImageOptions.SvgImage = this.svgImageCollection1["disconnected"];
                this.bsiStatus.Caption = "Socket Error.";
                this.biReconnect.Visibility = BarItemVisibility.Always;
            }
            else if(Exchange.TickersSocketState == SocketConnectionState.TooLongQue) {
                this.bsiStatus.ImageOptions.SvgImage = this.svgImageCollection1["disconnected"];
                this.bsiStatus.Caption = "Missing incremental update.";
                this.biReconnect.Visibility = BarItemVisibility.Always;
            }
            else if((DateTime.Now - Exchange.LastWebSocketRecvTime).TotalSeconds > 5) {
                if((DateTime.Now - Exchange.LastWebSocketRecvTime).TotalSeconds > 20) {
                    this.bsiStatus.ImageOptions.SvgImage = this.svgImageCollection1["disconnected"];
                    this.bsiStatus.Caption = "Connection Interrupted.";
                    this.biReconnect.Visibility = BarItemVisibility.Always;
                }
                else {
                    this.bsiStatus.ImageOptions.SvgImage = this.svgImageCollection1["datadelay"];
                    this.bsiStatus.Caption = "Waiting...";
                }
            }
            else
                SetInfoConnected();
        }

        private void OnConnectionTimerTick(object sender, EventArgs e) {
            UpdateConnectionStatus();
        }

        private void SetInfoConnected() {
            this.bsiStatus.ImageOptions.SvgImage = this.biConnected.ImageOptions.SvgImage;
            this.bsiStatus.Caption = "Connected.";
            this.biReconnect.Visibility = BarItemVisibility.Never;
        }

        void SubscribeWebSocket() {
            Exchange.StartListenTickersStream();
            this.gvTikers.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.True;
        }

        protected virtual void InitializeBaseCurrencies() {
            var groups = Exchange.Tickers.GroupBy(t => t.BaseCurrencyDisplayName);
            AccordionControlElement groupElement = new AccordionControlElement() { Style = ElementStyle.Group };
            groupElement.Text = "Markets";
            this.accordionControl1.Elements.Add(groupElement);
            groupElement.Expanded = true;
            foreach(var group in groups) {
                AccordionControlElement item = new AccordionControlElement() { Style = ElementStyle.Item };
                item.Text = group.Key;
                item.Click += OnBaseCurrencyItemClick;
                if(group.Key == "BTC" || group.Key == "XBT")
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
            List<Ticker> list = Exchange.Tickers.Where(t => t.BaseCurrencyDisplayName == baseCurrency).ToList();
            this.gcTickers.DataSource = list;
            BestFitColumns();
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
            BeginInvoke(new MethodInvoker(() => this.gvTikers.RefreshData()));
        }

        private void OnWebSocketTickerUpdate(object sender, TickerUpdateEventArgs e) {
            if(IsHandleCreated && InvokeRequired)
                BeginInvoke(new MethodInvoker(() => UpdateRow(e.Ticker)));
            else 
                UpdateRow(e.Ticker);
        }

        void UpdateSelectedTickersFromExchange() {
            UpdatePinnedItems();
        }
        protected bool IsUpdating { get; set; }
        protected override void OnThreadUpdate() {
            if(IsUpdating || !Exchange.IsInitialized)
                return;
            IsUpdating = true;
            try {
                if(!Exchange.SupportWebSocket(WebSocketType.Tickers)) {
                    if(Exchange.SupportCummulativeTickersUpdate)
                        Exchange.UpdateTickersInfo();
                }
                UpdateVisibleTickers(GetVisibleTickers());
                DataCacheManager.UpdateTasks();
            }
            finally {
                IsUpdating = false;
            }
            if(IsHandleCreated) {
                BeginInvoke(new Action(() => { 
                    UpdateConnectionStatus();
                    UpdateCachedDataCountInfo();
                    if(!Exchange.SupportWebSocket(WebSocketType.Tickers))
                        this.gvTikers.RefreshData();
                }));
            }
        }

        protected List<Ticker> PrevVisibleTickers { get; set; }
        private void UpdateVisibleTickers(List<Ticker> tickers) {
            bool supportWebSocket = Exchange.SupportWebSocket(WebSocketType.Tickers);
            if(PrevVisibleTickers != null) {
                foreach(Ticker ticker in PrevVisibleTickers) {
                    if(!tickers.Contains(ticker)) {
                        ticker.CancelSparkline();
                        if(!supportWebSocket && !Exchange.SupportCummulativeTickersUpdate)
                            ticker.CancelTickerInfo();
                    }
                }
            }
            foreach(Ticker ticker in tickers) {
                if(ticker.Sparkline == null)
                    ticker.QuerySparkline();
                if(!supportWebSocket && !Exchange.SupportCummulativeTickersUpdate)
                    ticker.QueryTickerInfo();
            }
            PrevVisibleTickers = tickers;
        }

        private List<Ticker> GetVisibleTickers() {
            var info = (GridViewInfo)this.gvTikers.GetViewInfo();
            List<Ticker> res = new List<Ticker>();
            foreach(var rowInfo in info.RowsInfo) {
                int rowHandle = rowInfo.RowHandle;
                Ticker t = this.gvTikers.GetRow(rowHandle) as Ticker;
                if(t != null)
                    res.Add(t);
            }
            return res;
        }
        void UpdateRow(Ticker t) {
            UpdateCachedDataCountInfo();
            int index = Exchange.Tickers.IndexOf(t);
            int rowHandle = this.gvTikers.GetRowHandle(index);
            this.gvTikers.RefreshRow(rowHandle);
        }
        void UpdateGrid(Ticker info) {
            int rowHandle = this.gvTikers.GetRowHandle(info.Index);
            this.gvTikers.RefreshRow(rowHandle);
        }
        void UpdateCachedDataCountInfo() {
            int count = DataCacheManager.Tasks.Count;
            this.bsCachedDataCount.Caption = count.ToString() + " in a data-queue"; 
        }

        protected bool HasShown { get; set; }
        
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ShowDetailsForSelectedItemCore();
        }

        void ShowDetailsForSelectedItemCore() {
            ShowDetailsForSelectedItemCore((Ticker)this.gvTikers.GetRow(this.gvTikers.FocusedRowHandle));
        }
        void ShowDetailsForSelectedItemCore(Ticker t) {
            if(this.gvTikers.FocusedRowHandle == GridControl.InvalidRowHandle)
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
            this.gvTikers.CloseEditor();
            Ticker ticker = (Ticker)this.gvTikers.GetFocusedRow();
            if(ticker.IsSelected)
                Exchange.PinnedTickers.Add(new PinnedTickerInfo() { BaseCurrency = ticker.BaseCurrency, MarketCurrency = ticker.MarketCurrency });
            else {
                PinnedTickerInfo info = Exchange.PinnedTickers.FirstOrDefault(p => p.BaseCurrency == ticker.BaseCurrency && p.MarketCurrency == ticker.MarketCurrency);
                Exchange.PinnedTickers.Remove(info);
            }
            Exchange.Save();
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
                this.gvTikers.ActiveFilterString = string.Empty;
            else
                this.gvTikers.ActiveFilterCriteria = new BinaryOperator("IsSelected", true);
        }

        private void biBestFit_ItemClick(object sender, ItemClickEventArgs e) {
            BestFitColumns();
        }

        protected RepositoryItemSparklineEdit greenSparkline, redSparkline;

        private void BiBestFitTable_ItemClick(object sender, ItemClickEventArgs e) {
            BestFitColumns();
        }

        protected void BestFitColumns() {
            var cols = this.gvTikers.Columns.Where(c => c.VisibleIndex > -1).OrderBy(cc => cc.VisibleIndex).ToList();
            //this.gvTikers.BeginUpdate();
            try {
                foreach(GridColumn c in cols) {
                    if(!c.Visible)
                        continue;
                    c.Width = c.GetBestWidth();// CalcColumnBestWidth(this.gvTikers, c);
                }
            }
            finally {
                //this.gvTikers.EndUpdate();
            }
        }

        private int CalcColumnBestWidth(GridView view, GridColumn c) {
            var info = ((GridViewInfo)view.GetViewInfo()).RowsInfo;
            int rowCount = info.Count;

            int maxWidth = 0;
            using(GraphicsCache cache = this.gcTickers.CreateGraphicsCache()) {
                foreach(var row in info) {
                    string text = view.GetRowCellDisplayText(row.RowHandle, c);
                    int width = (int)(cache.CalcTextSize(text, c.AppearanceCell.Font).Width) + 6;
                    maxWidth = Math.Max(width, maxWidth);
                }
            }
            return maxWidth;
        }

        private void biVolumesMap_ItemClick(object sender, ItemClickEventArgs e) {
            ExchangeMarketCapacityForm form = new ExchangeMarketCapacityForm();
            form.Exchange = Exchange;
            form.Show();
        }

        private void bsiStatus_ItemClick(object sender, ItemClickEventArgs e) {
            ((MainForm)MdiParent).ShowLogPanel();
        }

        private void GridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e) {
            if(e.Column != this.colSparkline)
                return;
            Ticker t = (Ticker)this.gvTikers.GetRow(e.RowHandle);
            if(t == null)
                return;
            if(this.greenSparkline == null) {
                this.greenSparkline = (RepositoryItemSparklineEdit)e.RepositoryItem.Clone();
                this.greenSparkline.View.Color = Exchange.BidColor;
                ((AreaSparklineView)this.greenSparkline.View).AreaOpacity = 0x20;
                this.gcTickers.RepositoryItems.Add(this.greenSparkline);
            }
            if(this.redSparkline == null) {
                this.redSparkline = (RepositoryItemSparklineEdit)e.RepositoryItem.Clone();
                this.redSparkline.View.Color = Exchange.AskColor;
                ((AreaSparklineView)this.redSparkline.View).AreaOpacity = 0x20;
                this.gcTickers.RepositoryItems.Add(this.redSparkline);
            }
            if(t.Sparkline == null || t.Sparkline.Length < 2)
                return;
            if(t.Sparkline[t.Sparkline.Length - 1] > t.Sparkline[0]) {
                e.RepositoryItem = this.greenSparkline;
            }
            else {
                e.RepositoryItem = this.redSparkline;
            }
        }
    }
}
