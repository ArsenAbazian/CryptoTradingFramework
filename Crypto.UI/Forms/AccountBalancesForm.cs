using Crypto.Core;
using Crypto.Core.Common;
using CryptoMarketClient;
using DevExpress.Data.Filtering;
using DevExpress.XtraGrid.Views.Base;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Crypto.Core.Common {
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

        private void OnAccountBalancesCustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e) {
            if(e.Column == this.colUsdtPrice && double.IsNaN((double)e.Value))
                e.DisplayText = "<b><color=red>NO DATA</color></b>";
        }

        private void OnInfoItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            LogManager.Default.ShowLogForm();
        }

        List<BalanceBase> total = new List<BalanceBase>();
        Dictionary<ExchangeType, bool> balanceObtained = new Dictionary<ExchangeType, bool>();
        bool shouldExpandGroups = true;
        void UpdateBalances() {
            foreach(Exchange e in Exchanges) {
                this.bsInfo.Caption = "<color=green><b>Updating "+ e.Name + "</color></b>";
                if(!e.IsConnected) {
                    if(!e.Connect()) {
                        this.bsInfo.Caption = "<color=red><b>Update failed.</color></b>";
                        continue;
                    }
                }
                if(!e.UpdateAllAccountsBalances()) {
                    this.bsInfo.Caption = "<color=red><b>UpdateBalances failed.</color></b>";
                    continue;
                }
                if(this.bcDeposites.Checked) {
                    if(!e.GetAllAccountsDeposites()) {
                        this.bsInfo.Caption = "<color=red><b>GetDeposites failed</color></b>";
                        //continue;
                    }
                }
                this.bsInfo.Caption = "";
                if(!this.balanceObtained.ContainsKey(e.Type)) {
                    this.balanceObtained.Add(e.Type, true);
                    total.AddRange(e.GetAllBalances());
                    this.shouldExpandGroups = true;
                }
                if(!IsHandleCreated || IsDisposed)
                    continue;
            
                BeginInvoke(new MethodInvoker(() => {
                    if(!this.gridControl1.IsHandleCreated || this.gridControl1.IsDisposed)
                        return;
                    if(this.poloniexAccountBalanceInfoBindingSource.DataSource is Type) {
                        this.poloniexAccountBalanceInfoBindingSource.DataSource = total;
                        UpdateFilter();
                        this.gridView1.ExpandAllGroups();
                    }
                    else {
                        if(this.shouldExpandGroups)
                            this.gridView1.ExpandAllGroups();
                        this.shouldExpandGroups = false;
                        this.gridView1.RefreshData();
                    }
                }));    
            }
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

        private void gridControl1_Click(object sender, EventArgs e) {

        }
    }
}
