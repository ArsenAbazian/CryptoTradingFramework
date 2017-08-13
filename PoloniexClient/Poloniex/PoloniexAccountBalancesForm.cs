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
    public partial class PoloniexAccountBalancesForm : ThreadUpdateForm {
        public PoloniexAccountBalancesForm() {
            InitializeComponent();
            this.poloniexAccountBalanceInfoBindingSource.DataSource = PoloniexModel.Default.Balances;
        }

        protected override int UpdateInervalMs => 3000;
        protected override bool AllowUpdateInactive => false;

        protected override void OnThreadUpdate() {
            if(!PoloniexModel.Default.IsConnected)
                return;
            UpdateBalances();
        }
        void UpdateBalances() {
            Task<byte[]> task = PoloniexModel.Default.GetBalances();
            task.Wait();
            PoloniexModel.Default.OnGetBalances(task.Result);

            Task<byte[]> task2 = PoloniexModel.Default.GetDeposites();
            task2.Wait();
            PoloniexModel.Default.OnGetDeposites(task2.Result);
            this.gridControl1.RefreshDataSource();
        }
    }
}
