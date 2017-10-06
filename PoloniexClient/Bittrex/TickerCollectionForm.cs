using DevExpress.XtraEditors;
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
    public partial class BittrexTickerCollectionForm : XtraForm {
        public BittrexTickerCollectionForm() {
            InitializeComponent();
        }

        protected bool HasShown { get; set; }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            if(!BittrexModel.Default.GetMarketsInfo()) {
                XtraMessageBox.Show("Fatal Error: Can't obtain markets info");
                BeginInvoke(new Action(Close));
                return;
            }
            if(!BittrexModel.Default.GetMarketsSummaryInfo()) {
                XtraMessageBox.Show("Fatal Error: Can't obtain markets summary info");
                BeginInvoke(new Action(Close));
                return;
            }
            this.gridControl1.DataSource = BittrexModel.Default.Markets;
            HasShown = true;
            StartUpdateTickerThread();
        }

        protected override void OnActivated(EventArgs e) {
            base.OnActivated(e);
            if(!HasShown || (BidAskThread != null && BidAskThread.IsAlive))
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
                    if(!IsDisposed && IsHandleCreated && !Disposing)
                        Invoke(new Action(UpdateGridAll));
                }
                Thread.Sleep(900); //to avoid throttling
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
                BidAskThread.Abort();
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
            TickerBase t = (TickerBase)this.gridView1.GetRow(this.gridView1.FocusedRowHandle);
            TickerForm form = new TickerForm();
            form.MarketName = "Bittrex";
            form.Ticker = t;
            form.MdiParent = MdiParent;
            form.Show();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e) {
            ShowDetailsForSelectedItemCore();
        }

        BittrexAccountBalancesForm accountForm;
        protected BittrexAccountBalancesForm AccountForm {
            get {
                if(accountForm == null || accountForm.IsDisposed)
                    accountForm = new BittrexAccountBalancesForm();
                return accountForm;
            }
        }

        private void bcShowBalance_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            AccountForm.MdiParent = MdiParent;
            AccountForm.Show();
            AccountForm.Activate();
        }

        BittrexOrdersForm ordersForm;
        protected BittrexOrdersForm OrdersForm {
            get {
                if(ordersForm == null || ordersForm.IsDisposed)
                    ordersForm = new BittrexOrdersForm();
                return ordersForm;
            }
        }

        private void bcOpenOrders_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            OrdersForm.MdiParent = MdiParent;
            OrdersForm.Show();
            OrdersForm.Activate();
        }
    }
}
