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
            Exchanges.Add(Exchange);
            UpdateFilter();
        }

        public AccountBalancesForm() {
            InitializeComponent();
            Text = "Account Balances";
        }

        protected Exchange Exchange { get; set; }
        public List<Exchange> Exchanges { get; } = new List<Exchange>();

        protected override int UpdateInervalMs => 5000;
        protected override bool AllowUpdateInactive => false;

        protected override void OnThreadUpdate() {
            UpdateBalances();
        }
        
        void UpdateBalances() {
            foreach(Exchange e in Exchanges) {
                this.bsInfo.Caption = "<color=green><b>Updating "+ e.Name + "</color></b>";
                if(!e.IsConnected)
                    e.Connect();
                if(!e.UpdateAllAccountsBalances()) {
                    this.bsInfo.Caption = "<color=red><b>UpdateBalances failed.</color></b>";
                    return;
                }
                if(this.bcDeposites.Checked) {
                    if(!e.GetAllAccountsDeposites()) {
                        this.bsInfo.Caption = "<color=red><b>GetDeposites failed</color></b>";
                        return;
                    }
                }
                this.bsInfo.Caption = "";
                if(!IsHandleCreated || IsDisposed)
                    return;
            }

            BeginInvoke(new MethodInvoker(() => {
                if(!this.gridControl1.IsHandleCreated || this.gridControl1.IsDisposed)
                    return;
                if(this.poloniexAccountBalanceInfoBindingSource.DataSource is Type) {
                    List<BalanceBase> total = new List<BalanceBase>();
                    foreach(Exchange ee in Exchanges)
                        total.AddRange(ee.GetAllBalances());
                    this.poloniexAccountBalanceInfoBindingSource.DataSource = total;
                    UpdateFilter();
                    this.gridView1.ExpandAllGroups();
                }
                else {
                    this.gridView1.RefreshData();
                }
            }));
        }

        protected void UpdateFilter() {
            if(this.bcShowNonZero.Checked) {
                CriteriaOperator op = new BinaryOperator("NonZero", true, BinaryOperatorType.Equal);
                this.gridView1.ActiveFilterCriteria = op;
            }
            else {
                this.gridView1.ActiveFilterString = null;
            }
        }

        private void bcShowNonZero_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            UpdateFilter();
        }
    }
}
