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
            Exchange = exchange;
        }

        protected Exchange Exchange { get; set; }

        protected override int UpdateInervalMs => 5000;
        protected override bool AllowUpdateInactive => false;

        protected override void OnThreadUpdate() {
            if(!Exchange.IsConnected)
                return;
            UpdateBalances();
        }
        
        void UpdateBalances() {
            if(!Exchange.UpdateAllAccountsBalances()) {
                this.bsInfo.Caption = "<color=red><b>UpdateBalances failed.</color></b>";
                return;
            }
            if(!Exchange.GetAllAccountsDeposites()) {
                this.bsInfo.Caption = "<color=red><b>GetDeposites failed</color></b>";
                return;
            }
            this.bsInfo.Caption = "";
            if(!IsHandleCreated || IsDisposed)
                return;

            BeginInvoke(new MethodInvoker(() => {
                if(!this.gridControl1.IsHandleCreated || this.gridControl1.IsDisposed)
                    return;
                if(this.poloniexAccountBalanceInfoBindingSource.DataSource is Type) {
                    this.poloniexAccountBalanceInfoBindingSource.DataSource = Exchange.GetAllBalances();
                    this.gridView1.ExpandAllGroups();
                }
                else {
                    this.gridView1.RefreshData();
                }
            }));
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
