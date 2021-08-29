using Crypto.Core;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public partial class AccounTradesCollectionForm : XtraForm {
        public AccounTradesCollectionForm(Exchange exchange) {
            InitializeComponent();
            Text = exchange.Name + " - Trade History";
            Exchange = exchange;
        }

        protected Exchange Exchange { get; set; }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            UpdateMyTrades();
        }

        void UpdateMyTrades() {
            if(!Exchange.UpdateAllAccountsTrades()) {
                this.bsInfo.Caption = "<color=red><b>UpdateBalances failed.</color></b>";
                return;
            }
            this.bsInfo.Caption = "";
            if(!IsHandleCreated || IsDisposed)
                return;


            if(!this.gridControl1.IsHandleCreated || this.gridControl1.IsDisposed)
                return;
            this.tradeHistoryItemBindingSource.DataSource = Exchange.GetAllAccountTrades();
            this.gridView1.ExpandAllGroups();
            this.gridView1.BestFitColumns();
        }

        private void biUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            UpdateMyTrades();
        }

        private void biExpandTickers_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.gridView1.ExpandGroupLevel(1);
        }

        private void biCollapseTickers_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.gridView1.CollapseGroupLevel(1);
        }

        private void biExpandAccounts_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.gridView1.ExpandGroupLevel(0);
        }

        private void biCollapseAccounts_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.gridView1.CollapseGroupLevel(0);
        }
    }
}
