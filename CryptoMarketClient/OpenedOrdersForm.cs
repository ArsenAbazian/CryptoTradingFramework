using Crypto.Core;
using Crypto.Core.Common;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CryptoMarketClient.Poloniex {
    public partial class OpenedOrdersForm : ThreadUpdateForm {
        public OpenedOrdersForm(Exchange exchange) {
            InitializeComponent();
            Text = exchange.Name + " - Opened Orders";
            Exchange = exchange;
            this.openedOrdersBindingSource.DataSource = OpenedOrders;
        }

        public Exchange Exchange { get; private set; }
        protected override int UpdateInervalMs => 3000;
        protected override bool AllowUpdateInactive => false;
        protected bool IsFirstTime { get; set; }
        protected List<OpenedOrderInfo> OpenedOrders { get; } = new List<OpenedOrderInfo>();

        protected override void OnThreadUpdate() {
            if(IsDisposed || this.gridControl1.IsDisposed) {
                AllowWorkThread = false;
                return;
            }
            List<OpenedOrderInfo> res = Exchange.UpdateAllOpenedOrders();
            //if(IsEquals(res, OpenedOrders))
            //    return;
            OpenedOrders.Clear();
            OpenedOrders.AddRange(res);
            if(IsDisposed || this.gridControl1.IsDisposed) {
                AllowWorkThread = false;
                return;
            }
            BeginInvoke(new MethodInvoker(() => {
                if(!this.gridControl1.IsHandleCreated || this.gridControl1.IsDisposed)
                    return;
                if(IsFirstTime) {
                    IsFirstTime = false;
                    this.gridView1.BestFitColumns();
                    this.gridView1.ExpandAllGroups();
                }
                this.gridView1.RefreshData();
            }));
        }
        bool IsEquals(List<OpenedOrderInfo> list1, List<OpenedOrderInfo> list2) {
            if(list1.Count != list2.Count)
                return false;
            for(int i = 0; i < list1.Count; i++) {
                if(list1[i].OrderId != list2[i].OrderId || list1[i].AmountString != list2[i].AmountString) return false;
            }
            return true;
        }
        void RefreshGrid() {
            this.gridControl1.RefreshDataSource();
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
