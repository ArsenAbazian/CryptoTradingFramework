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

namespace CryptoMarketClient.Bittrex {
    public partial class BittrexMarketsForm : Form {
        public BittrexMarketsForm() {
            InitializeComponent();
        }

        protected bool HasShown { get; set; }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            BittrexModel.Default.GetMarketsInfo();
            BittrexModel.Default.GetMarketsSummaryInfo();
            this.gridControl1.DataSource = BittrexModel.Default.Markets;
            HasShown = true;
            StartUpdateTickerThread();
        }

        protected override void OnActivated(EventArgs e) {
            base.OnActivated(e);
            if(!HasShown)
                return;
            StartUpdateTickerThread();
        }

        protected Thread BidAskThread { get; set; }
        void StartUpdateTickerThread() {
            AllowWorking = true;
            BidAskThread = new Thread(OnBidAskUpdate);
            BidAskThread.Start();
        }
        protected bool AllowWorking { get; set; }
        void OnBidAskUpdate() {
            while(AllowWorking && BittrexModel.Default.IsConnected) {
                BittrexModel.Default.GetMarketsSummaryInfo();
                lock(BittrexModel.Default.Markets) {
                    if(!IsDisposed)
                        BeginInvoke(new Action(UpdateGridAll));
                }
            }
        }
        void UpdateGrid(BittrexMarketInfo info) {
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

        private void btCurrencies_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            BittrexCurrenciesForm form = new BittrexCurrenciesForm();
            form.MdiParent = MdiParent;
            form.Show();
        }

        private void btShowDetails_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
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
