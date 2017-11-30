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
        public TickersCollectionForm(ModelBase model) {
            InitializeComponent();
            Model = model;
            Text = Model.Name;
        }

        public ModelBase Model { get; set; }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            Model.GetTickersInfo();
            //Model.GetBalances();
            this.gridControl1.DataSource = Model.Tickers;
            HasShown = true;
            UpdateSelectedTickersFromModel();
            StartUpdateTickersThread();
        }
        void UpdateSelectedTickersFromModel() {
            UpdatePinnedItems();
        }
        protected override void OnActivated(EventArgs e) {
            base.OnActivated(e);
            if(!HasShown || (BidAskThread != null && BidAskThread.IsAlive))
                return;
            StartUpdateTickersThread();
        }

        protected Thread BidAskThread { get; set; }
        void StartUpdateTickersThread() {
            AllowWorking = true;
            BidAskThread = new Thread(OnTickersUpdate);
            BidAskThread.Start();
        }
        protected bool AllowWorking { get; set; }
        void OnTickersUpdate() {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            int runningTasksCount = 0;
            while(AllowWorking && Model.IsConnected) {
                Model.UpdateTickersInfo();
                foreach(TickerBase ticker in Model.Tickers) {
                    while(runningTasksCount > 10)
                        Thread.Sleep(10);
                    if(ticker.UpdateMode != TickerUpdateMode.Self)
                        continue;
                    if(ticker.IsUpdating) {
                        ticker.UpdateTimeMs = timer.ElapsedMilliseconds - ticker.StartUpdateTime;
                        if(ticker.UpdateTimeMs > 2000)
                            ticker.IsActual = false;
                        continue;
                    }
                    if(timer.ElapsedMilliseconds - ticker.LastUpdateTime < 200)
                        continue;
                    runningTasksCount++;
                    ticker.IsUpdating = true;
                    ticker.StartUpdateTime = timer.ElapsedMilliseconds;

                    Task t = Task.Factory.StartNew(() => {
                        ticker.UpdateOrderBook();
                        ticker.UpdateTrades();
                        ticker.UpdateOpenedOrders();
                    }).ContinueWith(tt => {
                        ticker.UpdateTimeMs = timer.ElapsedMilliseconds - ticker.LastUpdateTime;
                        ticker.LastUpdateTime = timer.ElapsedMilliseconds;
                        if(!IsDisposed && !Disposing && IsHandleCreated)
                            BeginInvoke(new Action<TickerBase>(UpdateRow), ticker);
                        runningTasksCount--;
                        ticker.IsUpdating = false;
                        ticker.IsActual = true;
                    });
                    ticker.Task = t;
                }
                if(!IsDisposed && !Disposing && IsHandleCreated)
                    BeginInvoke(new Action(UpdateGridAll));
            }
        }
        void UpdateRow(TickerBase t) {
            int index = Model.Tickers.IndexOf(t);
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

        protected override void OnDeactivate(EventArgs e) {
            base.OnDeactivate(e);
            if(!HasShown)
              return;
            //StopBidAskThread();
        }

        private void StopBidAskThread() {
            if(BidAskThread != null) {
                AllowWorking = false;
                BidAskThread.Abort();
                BidAskThread = null;
            }
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
            foreach(PinnedTickerInfo info in Model.PinnedTickers) {
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
                if(!Model.PinnedTickers.Contains(info))
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
                TickerBase t = Model.GetTicker((PinnedTickerInfo)e.Item.Tag);
                ShowDetailsForSelectedItemCore(t);
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
            return Model.PinnedTickers.FirstOrDefault(i => i.BaseCurrency == t.BaseCurrency && i.MarketCurrency == t.MarketCurrency) != null;
        }

        private void bbAddQuickPanel_ItemClick(object sender, ItemClickEventArgs e) {
            TickerBase ticker = (TickerBase)this.gridView1.GetFocusedRow();
            Model.PinnedTickers.Add(new Common.PinnedTickerInfo() { BaseCurrency = ticker.BaseCurrency, MarketCurrency = ticker.MarketCurrency });
            UpdatePinnedItems();
            Model.Save();
        }

        private void bbRemoveQuickPanel_ItemClick(object sender, ItemClickEventArgs e) {
            TickerBase t = (TickerBase)this.gridView1.GetFocusedRow();
            PinnedTickerInfo info = Model.PinnedTickers.FirstOrDefault(i => i.BaseCurrency == t.BaseCurrency && i.MarketCurrency == t.MarketCurrency);
            Model.PinnedTickers.Remove(info);
            UpdatePinnedItems();
            Model.Save();
        }

        private void barManager1_ItemPress(object sender, ItemClickEventArgs e) {
            if(Control.MouseButtons != MouseButtons.Right)
                return;
            PinnedTickerInfo info = e.Item.Tag as PinnedTickerInfo;
            if(info == null)

                return;
            if(Model.PinnedTickers.Contains(info)) {
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
            Model.PinnedTickers.Remove(info);
            UpdatePinnedItems();
            Model.Save();
        }

        private void gridControl1_Click(object sender, EventArgs e) {

        }
    }
}
