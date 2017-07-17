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

namespace PoloniexClient.Bittrex {
    public partial class BittrexMarketsForm : Form {
        public BittrexMarketsForm() {
            InitializeComponent();
        }

        protected bool HasShown { get; set; }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            BittrexModel.Default.GetMarketsInfo();
            this.gridControl1.DataSource = BittrexModel.Default.Markets;
            HasShown = true;
            StartBidAskThread();
        }

        protected override void OnActivated(EventArgs e) {
            base.OnActivated(e);
            if(!HasShown)
                return;
            StartBidAskThread();
        }

        protected Thread BidAskThread { get; set; }
        void StartBidAskThread() {
            BidAskThread = new Thread(OnBidAskUpdate);
            BidAskThread.Start();
        }
        void OnBidAskUpdate() {
            while(true) {
                foreach(BittrexMarketInfo info in BittrexModel.Default.Markets) {
                    lock(info) {
                        BittrexModel.Default.GetMarketInfo(info);
                        BeginInvoke(new Action<BittrexMarketInfo>(UpdateGrid), info);
                    }
                }
                
            }
        }
        void UpdateGrid(BittrexMarketInfo info) {
            int rowHandle = this.gridView1.GetRowHandle(info.Index);
            this.gridView1.RefreshRow(rowHandle);
        }

        protected override void OnDeactivate(EventArgs e) {
            base.OnDeactivate(e);
            if(!HasShown)
                return;
            StopBidAskThread();
        }

        private void StopBidAskThread() {
            if(BidAskThread != null) {
                BidAskThread.Abort();
                BidAskThread = null;
            }
        }

        private void btCurrencies_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            BittrexCurrenciesForm form = new BittrexCurrenciesForm();
            form.MdiParent = MdiParent;
            form.Show();
        }
    }
}
