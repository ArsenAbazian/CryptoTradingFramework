using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Bittrex {
    public partial class BittrexAccountBalancesForm : ThreadUpdateForm {
        public BittrexAccountBalancesForm() {
            InitializeComponent();
            this.bittrexAccountBalanceInfoBindingSource.DataSource = BittrexModel.Default.Balances;
        }

        protected override int UpdateInervalMs => 3000;
        protected override bool AllowUpdateInactive => false;

        protected void UpdateBalances() {
            if(!BittrexModel.Default.IsConnected)
                return;
            Task<string> task = BittrexModel.Default.GetBalancesAsync();
            task.Wait();
            BittrexModel.Default.OnGetBalances(task.Result);
            this.gridControl1.RefreshDataSource();
        }

        protected override void OnThreadUpdate() {
            UpdateBalances();
        }
    }
}
