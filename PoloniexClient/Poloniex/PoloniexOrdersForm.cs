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
    public partial class PoloniexOrdersForm : ThreadUpdateForm {
        public PoloniexOrdersForm() {
            InitializeComponent();
        }

        protected override int UpdateInervalMs => 3000;
        protected override bool AllowUpdateInactive => true;

        protected override void OnThreadUpdate() {
            Task<byte[]> task1 = PoloniexExchange.Default.GetOpenedOrders();
            task1.Wait();
            PoloniexExchange.Default.OnGetOpenedOrders(task1.Result);
            Invoke(new Action(RefreshGrid));
        }
        void RefreshGrid() {
            this.gridControl1.RefreshDataSource();
        }
    }
}
