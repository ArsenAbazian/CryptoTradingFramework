using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public partial class AccountInfoEditingForm : XtraForm {
        public AccountInfoEditingForm() {
            InitializeComponent();
        }

        public ExchangeAccountInfo Account {
            get { return this.exchangeAccountInfoBindingSource.DataSource as ExchangeAccountInfo; }
            set { this.exchangeAccountInfoBindingSource.DataSource = value; }
        }

        public void AddNew() {
            ExchangeAccountInfo info = new ExchangeAccountInfo();
            if(EditedAccount != null) {
                info.Name = EditedAccount.Name;
                info.Type = EditedAccount.Type;
                info.ApiKey = EditedAccount.ApiKey;
                info.Secret = EditedAccount.Secret;
            }
            Account = info;
        }

        private void btOk_Click(object sender, EventArgs e) {
            Account.Name = Account.Name.Trim();
            Account.ApiKey = Account.ApiKey.Trim();
            Account.Secret = Account.Secret.Trim();
            if(EditedAccount != null) {
                EditedAccount.Type = Account.Type;
                EditedAccount.Name = Account.Name;
                EditedAccount.Secret = Account.Secret;
                EditedAccount.ApiKey = Account.ApiKey;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        protected ExchangeAccountInfo EditedAccount { get; set; }
        public void Edit(ExchangeAccountInfo account) {
            EditedAccount = account;
            AddNew();
        }
    }
}
