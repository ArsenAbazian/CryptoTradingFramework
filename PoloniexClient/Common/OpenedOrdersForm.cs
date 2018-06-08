using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Poloniex {
    public partial class OpenedOrdersForm : ThreadUpdateForm {
        public OpenedOrdersForm(Exchange exchange) {
            InitializeComponent();
            Text = exchange.Name + " - Opened Orders";
            Exchange = exchange;
            this.openedOrdersBindingSource.DataSource = Exchange.OpenedOrders;
        }

        public Exchange Exchange { get; private set; }
        protected override int UpdateInervalMs => 3000;
        protected override bool AllowUpdateInactive => true;

        protected override void OnThreadUpdate() {
            if(IsDisposed || this.gridControl1.IsDisposed) {
                AllowWorkThread = false;
                return;
            }
            Exchange.UpdateOpenedOrders();
            if(IsDisposed || this.gridControl1.IsDisposed) {
                AllowWorkThread = false;
                return;
            }
            Invoke(new Action(RefreshGrid));
        }
        void RefreshGrid() {
            this.gridControl1.RefreshDataSource();
        }
    }
}
