using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public partial class PoloniexTickersForm : XtraForm {
        public PoloniexTickersForm() {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            PoloniexModel.Default.GetTickersInfo();
            this.gridControl1.DataSource = PoloniexModel.Default.Tickers;
            HasShown = true;
            StartUpdateTickersThread();
        }
        protected override void OnActivated(EventArgs e) {
            base.OnActivated(e);
            if(!HasShown)
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
            while(AllowWorking && PoloniexModel.Default.IsConnected) {
                PoloniexModel.Default.UpdateTickersInfo();
                lock(PoloniexModel.Default.Tickers) {
                    if(!IsDisposed)
                        BeginInvoke(new Action(UpdateGridAll));
                }
            }
        }
        void UpdateGrid(PoloniexTicker info) {
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
            StopBidAskThread();
        }

        private void StopBidAskThread() {
            if(BidAskThread != null) {
                AllowWorking = false;
                BidAskThread = null;
            }
        }

        protected bool HasShown { get; set; }
        
        /*
        TickerUpdateEventArgs LastTickerEventArgs { get; set; }
        private void OnTickerUpdate(object sender, TickerUpdateEventArgs e) {
            LastTickerEventArgs = e;
            BeginInvoke(new MethodInvoker(OnTickerUpdateCore));
        }

        private void OnTickerUpdateCore() {
            int index = PoloniexModel.Default.Tickers.IndexOf(LastTickerEventArgs.Ticker);
            int rowHandle = this.gridView1.GetRowHandle(index);
            this.gridView1.RefreshRow(rowHandle);
        }
        */
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ShowDetailsForSelectedItemCore();
        }

        void ShowDetailsForSelectedItemCore() {
            if(this.gridView1.FocusedRowHandle == GridControl.InvalidRowHandle)
                return;
            ITicker t = (ITicker)this.gridView1.GetRow(this.gridView1.FocusedRowHandle);
            TickerForm form = new TickerForm();
            form.Ticker = t;
            form.MdiParent = MdiParent;
            form.Show();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e) {
            ShowDetailsForSelectedItemCore();
        }
    }
}
