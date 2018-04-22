using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Common {
    public partial class AccountBalancesForm : ThreadUpdateForm {
        public AccountBalancesForm(Exchange exchange) {
            InitializeComponent();
            Text = exchange.Name + " Account Balances";
            this.gridControl1.DataSource = exchange.Balances;
            //this.poloniexAccountBalanceInfoBindingSource.DataSource = exchange.Balances;
            Exchange = exchange;
        }

        protected Exchange Exchange { get; set; }

        protected override int UpdateInervalMs => 3000;
        protected override bool AllowUpdateInactive => false;

        protected override void OnThreadUpdate() {
            if(!Exchange.IsConnected)
                return;
            UpdateBalances();
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            if(!Exchange.UpdateCurrencies()) {
                this.bsInfo.Caption = "<color=red><b>UpdateCurrencies failed.</color></b>";
            }
        }

        void UpdateBalances() {
            if(!Exchange.UpdateBalances()) {
                this.bsInfo.Caption = "<color=red><b>UpdateBalances failed.</color></b>";
                return;
            }
            if(!Exchange.GetDeposites()) {
                this.bsInfo.Caption = "<color=red><b>GetDeposites failed</color></b>";
                return;
            }
            this.bsInfo.Caption = "";
            this.gridControl1.RefreshDataSource();
        }

        private void bcShowNonZero_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.bcShowNonZero.Checked) {
                CriteriaOperator op = new BinaryOperator("NonZero", true, BinaryOperatorType.Equal);
                this.gridView1.ActiveFilterCriteria = op;
            }
            else {
                this.gridView1.ActiveFilterString = null;
            }
        }
    }
}
