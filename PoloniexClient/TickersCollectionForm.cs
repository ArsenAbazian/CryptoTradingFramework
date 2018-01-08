using CryptoMarketClient.Common;
using CryptoMarketClient.Poloniex;
using DevExpress.XtraBars;
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
        }

        public Exchange Exchange { get; set; }

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

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);

            gridView1.ShowLoadingPanel();
            this.gridControl1.DataSource = Exchange.Tickers;
            HasShown = true;
            InitExchange();
        }

        async void InitExchange() {
            await Task<bool>.Factory.StartNew(() => Exchange.LoadTickers(), TaskCreationOptions.LongRunning);
            UpdateSelectedTickersFromExchange();
            Timer.InitializeLifetimeService();
            gridView1.HideLoadingPanel();
        }

        void UpdateSelectedTickersFromExchange() {
            UpdatePinnedItems();
        }
        protected bool IsUpdating { get; set; }
        void OnThreadUpdate(object state) {
            if(IsUpdating)
                return;
            IsUpdating = true;
            try {
                Exchange.UpdateTickersInfo();
                foreach(TickerBase ticker in Exchange.Tickers)
                    ticker.UpdateTrailings();
            }
            finally {
                IsUpdating = false;
            }
            BeginInvoke(new Action(UpdateGridAll));
        }
        void UpdateRow(TickerBase t) {
            int index = Exchange.Tickers.IndexOf(t);
            int rowHandle = this.gridView1.GetRowHandle(index);
            this.gridView1.RefreshRow(rowHandle);
        }
        void UpdateGrid(TickerBase info) {
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
            ShowDetailsForSelectedItemCore((TickerBase)this.gridView1.GetRow(this.gridView1.FocusedRowHandle));
        }
        void ShowDetailsForSelectedItemCore(TickerBase t) {
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

        private void bbShowBalances_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //AccountForm.MdiParent = MdiParent;
            //AccountForm.Show();
            //AccountForm.Activate();
        }

        //PoloniexOrdersForm ordersForm;
        //protected PoloniexOrdersForm OrdersForm {
        //    get {
        //        if(ordersForm == null || ordersForm.IsDisposed)
        //            ordersForm = new PoloniexOrdersForm();
        //        return ordersForm;
        //    }
        //}
        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //OrdersForm.MdiParent = MdiParent;
            //OrdersForm.Show();
            //OrdersForm.Activate();
        }
        protected void UpdatePinnedItems() {
            List<BarItem> addItems = new List<BarItem>();
            List<BarItem> removeItems = new List<BarItem>();
            foreach(PinnedTickerInfo info in Exchange.PinnedTickers) {
                BarItemLink link = this.bar1.ItemLinks.FirstOrDefault(l => l.Item.Tag == info);
                if(link == null) {
                    BarButtonItem item = new BarButtonItem(this.barManager1, info.ToString());
                    item.Tag = info;
                    addItems.Add(item);
                }
            }
            foreach(BarItemLink link in this.bar1.ItemLinks) {
                PinnedTickerInfo info = link.Item.Tag as PinnedTickerInfo;
                if(info == null) continue;
                if(!Exchange.PinnedTickers.Contains(info))
                    removeItems.Add(link.Item);
            }
            this.barManager1.BeginUpdate();
            try {
                foreach(BarItem item in removeItems) {
                    this.barManager1.Items.Remove(item);
                }
                foreach(BarItem item in addItems) {
                    this.barManager1.Items.Add(item);
                    this.bar1.ItemLinks.Add(item);
                }
            }
            finally {
                this.barManager1.EndUpdate();
            }
        }
        private void repositoryItemCheckEdit1_EditValueChanged(object sender, EventArgs e) {
            this.gridView1.PostEditor();
            TickerBase ticker = (TickerBase)this.gridView1.GetFocusedRow();
            UpdatePinnedItems();
        }

        private void barManager1_ItemClick(object sender, ItemClickEventArgs e) {
            if(e.Item.Tag is PinnedTickerInfo) {
                TickerBase t = Exchange.GetTicker((PinnedTickerInfo)e.Item.Tag);
                this.gridView1.FocusedRowHandle = this.gridView1.GetRowHandle(Exchange.Tickers.IndexOf(t));
                //ShowDetailsForSelectedItemCore(t);
            }
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e) {
            if(e.Button != MouseButtons.Right)
                return;
            TickerBase ticker = (TickerBase)this.gridView1.GetFocusedRow();
            if(IsTickerPinned(ticker)) {
                this.bbAddQuickPanel.Visibility = BarItemVisibility.Never;
                this.bbRemoveQuickPanel.Visibility = BarItemVisibility.Always;
            }
            else {
                this.bbAddQuickPanel.Visibility = BarItemVisibility.Always;
                this.bbRemoveQuickPanel.Visibility = BarItemVisibility.Never;
            }
            this.popupMenu1.ShowPopup(this.barManager1, this.gridControl1.PointToScreen(e.Location));
        }
        bool IsTickerPinned(TickerBase t) {
            return Exchange.PinnedTickers.FirstOrDefault(i => i.BaseCurrency == t.BaseCurrency && i.MarketCurrency == t.MarketCurrency) != null;
        }

        private void bbAddQuickPanel_ItemClick(object sender, ItemClickEventArgs e) {
            TickerBase ticker = (TickerBase)this.gridView1.GetFocusedRow();
            Exchange.PinnedTickers.Add(new Common.PinnedTickerInfo() { BaseCurrency = ticker.BaseCurrency, MarketCurrency = ticker.MarketCurrency });
            UpdatePinnedItems();
            Exchange.Save();
        }

        private void bbRemoveQuickPanel_ItemClick(object sender, ItemClickEventArgs e) {
            TickerBase t = (TickerBase)this.gridView1.GetFocusedRow();
            PinnedTickerInfo info = Exchange.PinnedTickers.FirstOrDefault(i => i.BaseCurrency == t.BaseCurrency && i.MarketCurrency == t.MarketCurrency);
            Exchange.PinnedTickers.Remove(info);
            UpdatePinnedItems();
            Exchange.Save();
        }

        private void barManager1_ItemPress(object sender, ItemClickEventArgs e) {
            if(Control.MouseButtons != MouseButtons.Right)
                return;
            PinnedTickerInfo info = e.Item.Tag as PinnedTickerInfo;
            if(info == null)

                return;
            if(Exchange.PinnedTickers.Contains(info)) {
                this.bbAddQuickPanel.Visibility = BarItemVisibility.Never;
                this.bbRemoveQuickPanel.Visibility = BarItemVisibility.Always;
            }
            else {
                this.bbAddQuickPanel.Visibility = BarItemVisibility.Always;
                this.bbRemoveQuickPanel.Visibility = BarItemVisibility.Never;
            }
            this.popupMenu1.ShowPopup(this.barManager1, Control.MousePosition);
        }

        private void barManager1_ShowToolbarsContextMenu(object sender, ShowToolbarsContextMenuEventArgs e) {
            e.ItemLinks.Clear();
            Point pt = Control.MousePosition;
            foreach(BarItemLink link in this.bar1.ItemLinks) {
                if(link.ScreenBounds.Contains(pt)) {
                    this.bbRemoveByRightClick.Tag = link.Item.Tag;
                    e.ItemLinks.Insert(0, this.bbRemoveByRightClick);
                    return;
                }
            }
        }

        private void bbRemoveByRightClick_ItemClick(object sender, ItemClickEventArgs e) {
            PinnedTickerInfo info = (PinnedTickerInfo)e.Item.Tag;
            Exchange.PinnedTickers.Remove(info);
            UpdatePinnedItems();
            Exchange.Save();
        }

        private void gridControl1_Click(object sender, EventArgs e) {

        }

        private void bbMonitorOnlySelected_CheckedChanged(object sender, ItemClickEventArgs e) {

        }

        private void barManager1_ItemDoubleClick(object sender, ItemClickEventArgs e) {
            if(e.Item.Tag is PinnedTickerInfo) {
                TickerBase t = Exchange.GetTicker((PinnedTickerInfo)e.Item.Tag);
                ShowDetailsForSelectedItemCore(t);
            }
        }
    }
}
