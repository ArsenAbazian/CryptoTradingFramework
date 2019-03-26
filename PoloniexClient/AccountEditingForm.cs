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
    public partial class AccountEditingForm : XtraForm {
        public AccountEditingForm() {
            InitializeComponent();
            InitializeComboBox();
        }

        public AccountInfo Account {
            get { return this.exchangeAccountInfoBindingSource.DataSource as AccountInfo; }
            set { this.exchangeAccountInfoBindingSource.DataSource = value; }
        }

        public void AddNew() {
            AccountInfo info = new AccountInfo();
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
        protected AccountInfo EditedAccount { get; set; }
        public void Edit(AccountInfo account) {
            EditedAccount = account;
            AddNew();
        }
        void InitializeComboBox() {
            foreach(Exchange e in Exchange.Registered) {
                this.TypeImageComboBoxEdit.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(e.Name, e.Type));
            }
        }
    }
}
