using Crypto.Core;
using Crypto.Core.Common;
using Crypto.Core.Exchanges.Base;
using CryptoMarketClient;
using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crypto.Core.Common {
    public partial class AccountBalancesForm : ThreadUpdateForm {
        public AccountBalancesForm(Exchange exchange) {
            InitializeComponent();
            Text = exchange.Name + " Account Balances";
            Exchange = exchange;
            Exchanges.Add(Exchange);
            UpdateFilter();
            if(!Exchange.SupportMultipleDepositMethods)
                this.gridView1.Columns.Remove(this.gcMethod);
        }

        public AccountBalancesForm() {
            InitializeComponent();
            Text = "Account Balances";
        }

        protected Exchange Exchange { get; set; }
        public List<Exchange> Exchanges { get; } = new List<Exchange>();

        protected override int UpdateInervalMs => 30000;
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
        void SetStatus(string text) {
            if(!IsHandleCreated)
                return;
            if(InvokeRequired)
                BeginInvoke(new MethodInvoker(() => this.bsiStatus.Caption = text));
        }
        bool isUpdatingBalances = false;
        async Task UpdateBalances() {
            if(this.isUpdatingBalances)
                return;
            this.isUpdatingBalances = true;
            SetStatus("<color=blue><b>Updating balance...</color></b>");
            var tasks = Exchanges.Select(e => new Task<bool>(() => {
                if(!e.Connect()) {
                    LogManager.Default.Error(e, "Could not connect exchange", "");
                    return false;
                }
                if(!e.UpdateAllAccountsBalances()) {
                    LogManager.Default.Error(e, "Failed update accounts balances", "");
                    return false;
                }
                if(this.bcDeposites.Checked) {
                    if(!e.GetAllAccountsAddresses()) {
                        LogManager.Default.Error(e, "Failed to get deposit adresses", "");
                        return false;
                    }
                    if(!e.GetAllAccountsDeposites()) {
                        LogManager.Default.Error(e, "Failed to get deposit adresses", "");
                        return false;
                    }
                }
                return true;
            }));
            Task<bool>[] run = tasks.ToArray();
            try {
                foreach(var r in run)
                    r.Start();
                bool[] res = await Task.WhenAll(run).ConfigureAwait(false);
            }
            catch(Exception) {
                SetStatus("<color=red><b>Update balance failed</color></b>");
                isUpdatingBalances = false;
                return;
            }
            SetStatus(string.Format("<color=green><b>Updated at {0}</color></b>", DateTime.Now.ToLongTimeString()));
            Exchanges.ForEach(e => {
                if(!this.balanceObtained.ContainsKey(e.Type)) {
                    this.balanceObtained.Add(e.Type, true);
                    total.AddRange(e.GetAllBalances());
                    this.shouldExpandGroups = true;
                }
            });
            isUpdatingBalances = false;
            if(!IsHandleCreated || IsDisposed)
                return;

            BeginInvoke(new MethodInvoker(() => {
                if(!this.gridControl1.IsHandleCreated || this.gridControl1.IsDisposed)
                    return;
                total.RemoveAll(b => b.Account == null);
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

        private void gridControl1_MouseUp(object sender, MouseEventArgs e) {
            if(e.Button != MouseButtons.Right)
                return;
            var info = this.gridView1.CalcHitInfo(e.Location);
            if(!info.InRowCell)
                return;
            BalanceBase b = this.gridView1.GetRow(info.RowHandle) as BalanceBase;
            if(b == null)
                return;
            this.popupMenu1.Tag = b;
            this.popupMenu1.ShowPopup(this.gridControl1.PointToScreen(e.Location));
        }

        private void biUpdateDeposite_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            BalanceBase b = (BalanceBase)this.popupMenu1.Tag;
            var task = Task.Run(() => { b.GetDeposit(); }).
                ContinueWith(t => {
                    if(IsHandleCreated)
                        BeginInvoke(new MethodInvoker(() => this.gridView1.RefreshData()));
                });
        }

        private void biUpdateBalance_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

        }

        private void biCreateDeposit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            BalanceBase b = (BalanceBase)this.popupMenu1.Tag;
            var task = Task.Run(() => { b.Account.Exchange.CheckCreateDeposit(b.Account, b.Currency); }).
                ContinueWith(t => {
                    if(IsHandleCreated)
                        BeginInvoke(new MethodInvoker(() => this.gridView1.RefreshData()));
                });
        }

        private void gridView1_ShownEditor(object sender, EventArgs e) {
            if(this.gridView1.FocusedColumn != this.gcMethod)
                return;
            BalanceBase b = (BalanceBase)this.gridView1.GetFocusedRow();
            if(b.CurrencyInfo.Methods.Count == 0)
                b.CurrencyInfo.GetDepositMethods();
            ComboBoxEdit cb = (ComboBoxEdit)this.gridView1.ActiveEditor;
            cb.Properties.Items.BeginUpdate();
            cb.Properties.Items.Clear();
            cb.Properties.ReadOnly = false;
            try {
                
                foreach(DepositMethod m in b.CurrencyInfo.Methods.Values) {
                    cb.Properties.Items.Add(m);
                }
            }
            finally {
                cb.Properties.Items.EndUpdate();
            }
        }

        private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e) {
            if(e.Column == this.gcMethod) {
                BalanceBase b = (BalanceBase)this.gridView1.GetFocusedRow();
                b.GetDeposit();
            }
        }

        private void bsiStatus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            
        }
    }
}
