using Crypto.Core;
using Crypto.Core.Common;
using Crypto.Core.Helpers;
using Crypto.UI.Helpers;
using CryptoMarketClient.Helpers;
using CryptoMarketClient.Poloniex;
using DevExpress.Data.Filtering;
using DevExpress.Skins;
using DevExpress.Sparkline;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Paint;
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
            this.repositoryItemHyperLinkEdit1.HtmlElementMouseDown += OnCommandEditMouseDown;
            this.repositoryItemHyperLinkEdit1.HtmlElementMouseUp += OnCommandEditMouseUp;
            this.repositoryItemHyperLinkEdit1.HtmlElementMouseClick += OnCommandEditClick;
            GridTransparentRowHelper.Apply(this.gvTikers);
        }

        private void gvTikers_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e) {
            if(e.Column == this.colEmpty && e.IsGetData)
                e.Value = e.Row;
        }

        private void OnCommandEditMouseUp(object sender, Crypto.UI.CustomControls.HtmlContentEditElementHandledEventArgs e) {
            if(e.ElementId == "dx-content")
                e.Handled = true;
        }

        private void OnCommandEditMouseDown(object sender, Crypto.UI.CustomControls.HtmlContentEditElementHandledEventArgs e) {
            if(e.ElementId == "dx-content")
                e.Handled = true;
        }

        private void OnCommandEditClick(object sender, Crypto.UI.CustomControls.HtmlContentEditElementEventArgs e) {
            if(e.ElementId == "dx-content") {
                ShowDetailsForSelectedItemCore((Ticker)e.DataItem);
            }
        }

        public Exchange Exchange { get; set; }

        private void UpdateConnectionStatus() {
            if(!Exchange.SupportWebSocket(WebSocketType.Tickers))
                return;
            if(Exchange.TickersSocketState == SocketConnectionState.None) {
                NotificationManager.NotifyStatus("Initializing...", this.svgImageCollection1["information"]);
            }
            else if(Exchange.TickersSocketState == SocketConnectionState.Connecting) {
                NotificationManager.NotifyStatus("Connecting...", this.svgImageCollection1["connecting"]);
            }
            else if(Exchange.TickersSocketState == SocketConnectionState.DelayRecv) {
                NotificationManager.NotifyStatus("No Data...", this.svgImageCollection1["datadelay"]);
                this.biReconnect.Visibility = BarItemVisibility.Always;
            }
            else if(Exchange.TickersSocketState == SocketConnectionState.Disconnected) {
                NotificationManager.NotifyStatus("Disconnected.", this.svgImageCollection1["disconnected"]);
                this.biReconnect.Visibility = BarItemVisibility.Always;
            }
            else if(Exchange.TickersSocketState == SocketConnectionState.Error) {
                NotificationManager.NotifyStatus("Socket Error.", this.svgImageCollection1["disconnected"]);
                this.biReconnect.Visibility = BarItemVisibility.Always;
            }
            else if(Exchange.TickersSocketState == SocketConnectionState.TooLongQue) {
                NotificationManager.NotifyStatus("Missing incremental update.", this.svgImageCollection1["disconnected"]);
                this.biReconnect.Visibility = BarItemVisibility.Always;
            }
            else if(Exchange.TickersSocketState == SocketConnectionState.Waiting) {
                NotificationManager.NotifyStatus("Waiting for data...", this.svgImageCollection1["datadelay"]);
            }
            else if((DateTime.Now - Exchange.LastWebSocketRecvTime).TotalSeconds > Exchange.WebSocketAllowedDelayInterval) {
                NotificationManager.NotifyStatus("Connection Interrupted!", this.svgImageCollection1["disconnected"]);
                this.biReconnect.Visibility = BarItemVisibility.Always;
            }
            else
                SetInfoConnected();
        }

        private void OnConnectionTimerTick(object sender, EventArgs e) {
            UpdateConnectionStatus();
        }

        private void SetInfoConnected() {
            NotificationManager.NotifyStatus("Connected.", this.biConnected.ImageOptions.SvgImage);
            this.biReconnect.Visibility = BarItemVisibility.Never;
        }

        void SubscribeWebSocket() {
            Exchange.StartListenTickersStream();
            this.gvTikers.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.True;
        }

        protected virtual void InitializeBaseCurrencies() {
            var groups = Exchange.Tickers.GroupBy(t => t.BaseCurrencyDisplayName);
            this.gcMarketFilter.DataSource = groups;
            UpdateTickersAccordingBaseCurrency();
        }

        private void OnBaseCurrencyItemCheckedChanged(object sender, ItemClickEventArgs e) {
            BarCheckItem ch = (BarCheckItem)e.Item;
            if(!ch.Checked)
                return;
            SelectedCheckItem = ch;
            UpdateTickersAccordingBaseCurrency();
        }

        protected BarCheckItem SelectedCheckItem { get; set; }

        private void OnBaseCurrencyCheckedChanged(object sender, ItemClickEventArgs e) {
            UpdateTickersAccordingBaseCurrency();
        }

        protected virtual void UpdateTickersAccordingBaseCurrency() {
            string baseCurrency = GetSelectedBaseCurrency();
            List<Ticker> list = Exchange.Tickers.Where(t => t.BaseCurrencyDisplayName == baseCurrency).ToList();
            this.gcTickers.DataSource = list;
            if(IsHandleCreated)
                BestFitColumns();
        }

        protected string GetSelectedBaseCurrency() {
            return ((IGrouping<string, Ticker>)this.wevMarketFilter.GetFocusedRow()).Key;
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
            BestFitColumns();
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
                    //UpdateConnectionStatus();
                    UpdateCachedDataCountInfo();
                    //if(!Exchange.SupportWebSocket(WebSocketType.Tickers))
                    //    this.gvTikers.RefreshData();
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
                if(!ticker.HasSparkline && ticker.QuerySparkline())
                    BeginInvoke(new MethodInvoker(() => UpdateRowCell(ticker, this.colSparkline)));
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
            UpdateRowCell(t, null);
        }
        void UpdateRowCell(Ticker t, GridColumn column) {
            UpdateCachedDataCountInfo();
            int index = Exchange.Tickers.IndexOf(t);
            int rowHandle = GetVisibleRowHandle(t); //this.gvTikers.GetRowHandle(index);
            if(rowHandle == -1) {
                this.gvTikers.RefreshData();
                return;
            }
            if(column != null)
                this.gvTikers.RefreshRowCell(rowHandle, column);
            else
                this.gvTikers.RefreshRow(rowHandle);
            this.gcTickers.Update();
        }

        private int GetVisibleRowHandle(Ticker t) {
            var ri = ((GridViewInfo)this.gvTikers.GetViewInfo()).RowsInfo;
            foreach(var row in ri) {
                if(this.gvTikers.GetRow(row.RowHandle) == t)
                    return row.RowHandle;
            }
            return -1;
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
            if(t == null)
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
            this.gcPinned.DataSource = Exchange.PinnedTickers;
            
            //AccordionControlElement groupElement = new AccordionControlElement() { Style = ElementStyle.Group };
            //groupElement.Text = "Quick Panel";
            //this.accordionControl1.Elements.Add(groupElement);
            //groupElement.Expanded = true;

            //for(int i = 0; i < Exchange.PinnedTickers.Count; i++) {
            //    PinnedTickerInfo info = Exchange.PinnedTickers[i];
            //    Ticker t = Exchange.Tickers.FirstOrDefault(tt => tt.BaseCurrency == info.BaseCurrency && tt.MarketCurrency == info.MarketCurrency);
            //    if(t != null)
            //        t.IsSelected = true;

            //    AddPinnedItem(t);
            //}
        }

        //private void AddPinnedItem(Ticker t) {
        //    AccordionControlElement groupElement = this.accordionControl1.Elements[0];

        //    AccordionControlElement elem = new AccordionControlElement(ElementStyle.Item);
        //    elem.Appearance.Normal.FontSizeDelta = 1;

        //    elem.Appearance.Hovered.FontSizeDelta = 1;

        //    elem.Appearance.Pressed.FontSizeDelta = 1;

        //    elem.Text = t.MarketCurrency + " / " + t.BaseCurrency;
        //    elem.Tag = t;
        //    elem.Click += OnPinnedItemClick;
        //    groupElement.Elements.Add(elem);
        //}

        //private void RemovePinnedItem(PinnedTickerInfo info) {
        //    Ticker t = Exchange.Tickers.FirstOrDefault(tt => tt.BaseCurrency == info.BaseCurrency && tt.MarketCurrency == info.MarketCurrency);
        //    if(t == null)
        //        return;

        //    var element = this.accordionControl1.GetElements().FirstOrDefault(e => e.Tag == t);
        //    element.Click -= OnPinnedItemClick;

        //    AccordionControlElement groupElement = this.accordionControl1.Elements[0];
        //    groupElement.Elements.Remove(element);

        //    element.Dispose();
        //}

        private void OnPinnedItemClick(object sender, EventArgs e) {
            ShowDetailsForSelectedItemCore((Ticker)((AccordionControlElement)sender).Tag);
        }

        private void repositoryItemCheckEdit1_EditValueChanged(object sender, EventArgs e) {
            this.gvTikers.CloseEditor();
            Ticker ticker = (Ticker)this.gvTikers.GetFocusedRow();
            if(ticker.IsSelected) {
                Exchange.PinnedTickers.Add(new PinnedTickerInfo() { BaseCurrency = ticker.BaseCurrency, MarketCurrency = ticker.MarketCurrency });
                //AddPinnedItem(ticker);
            }
            else {
                PinnedTickerInfo info = Exchange.PinnedTickers.FirstOrDefault(p => p.BaseCurrency == ticker.BaseCurrency && p.MarketCurrency == ticker.MarketCurrency);
                Exchange.PinnedTickers.Remove(info);

            }
            this.wevPinned.RefreshData();
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
            this.gvTikers.BeginUpdate();
            try {
                int total = 0;
                foreach(GridColumn c in cols) {
                    if(!c.Visible || c == colLeft || c == colRight)
                        continue;
                    c.Width = CalcColumnBestWidth(this.gvTikers, c);
                    total += c.Width; 
                }
                //this.colLeft.Width = total / 2;
                //this.colRight.Width = total / 2;
            }
            finally {
                this.gvTikers.EndUpdate();
            }
        }

        private int CalcColumnBestWidth(GridView view, GridColumn c) {
            var info = ((GridViewInfo)view.GetViewInfo()).RowsInfo;
            int rowCount = view.DataRowCount;
            if(c.ColumnEdit != null && !(c.ColumnEdit is RepositoryItemTextEdit))
                return c.Width;

            int maxWidth = 0;
            using(GraphicsCache cache = this.gcTickers.CreateGraphicsCache()) {
                cache.ScaleDPI = ScaleDPI;
                cache.Paint.DeviceDpi = ScaleDPI.ScaleDpi;
                StringFormat f = this.gvTikers.Appearance.Row.GetStringFormat();
                var font = this.gvTikers.Appearance.Row.Font;
                for(int i = 0; i < rowCount; i++) { 
                    string text = view.GetRowCellDisplayText(i, c);
                    int width = (int)(cache.Paint.CalcTextSize(cache.Graphics, text, font, f, -1).Width);
                    maxWidth = Math.Max(width, maxWidth);
                }
                cache.Paint.DeviceDpi = -1;
            }
            return maxWidth + 22;
        }

        private void biVolumesMap_ItemClick(object sender, ItemClickEventArgs e) {
            ExchangeMarketCapacityForm form = new ExchangeMarketCapacityForm();
            form.Exchange = Exchange;
            form.Show();
        }

        private void bsiStatus_ItemClick(object sender, ItemClickEventArgs e) {
            ((MainForm)MdiParent).ShowLogPanel();
        }

        private void gvTikers_MouseUp(object sender, MouseEventArgs e) {
            if(e.Button != MouseButtons.Right)
                return;
            Ticker t = (Ticker)this.gvTikers.GetFocusedRow();
            if(t == null)
                return;
            this.popupMenu1.Tag = t;
            this.popupMenu1.ShowPopup(Control.MousePosition);
        }

        private void gvTikers_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e) {
            if(e.Column != this.colMarketName)
                return;
            Ticker t = (Ticker)this.gvTikers.GetRow(this.gvTikers.GetRowHandle(e.ListSourceRowIndex));
            if(t == null)
                return;
            e.DisplayText = t.MarketCurrency + " / " + t.BaseCurrency;
        }

        private void gvTikers_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e) {
            e.DefaultDraw();
            if(e.Column == this.colEmpty)
                return;
            e.Cache.DrawLine(new Point(e.Bounds.X, e.Bounds.Bottom), new Point(e.Bounds.Right, e.Bounds.Bottom), Color.FromArgb(20, 0, 0, 0), 1);
        }

        private void wevMarketFilter_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) {
            UpdateTickersAccordingBaseCurrency();
        }

        private void wevPinned_HtmlElementMouseClick(object sender, DevExpress.XtraGrid.Views.WinExplorer.WinExplorerViewHtmlElementEventArgs e) {
            if(e.ElementId == "dx-content") {
                PinnedTickerInfo pi = (PinnedTickerInfo)e.DataItem;
                Ticker t = Exchange.GetTicker(pi);
                ShowDetailsForSelectedItemCore(t);
            }
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
                //((AreaSparklineView)this.greenSparkline.View).AreaOpacity = 0x20;
                this.gcTickers.RepositoryItems.Add(this.greenSparkline);
            }
            if(this.redSparkline == null) {
                this.redSparkline = (RepositoryItemSparklineEdit)e.RepositoryItem.Clone();
                this.redSparkline.View.Color = Exchange.AskColor;
                //((AreaSparklineView)this.redSparkline.View).AreaOpacity = 0x20;
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

    public class ForcedDpiFonts : DpiFonts {
        public ForcedDpiFonts() : base() { AllowFontScale = true; }
    }
}
