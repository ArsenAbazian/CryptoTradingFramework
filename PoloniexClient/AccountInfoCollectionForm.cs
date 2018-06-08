using CryptoMarketClient.Bittrex;
using CryptoMarketClient.Common;
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
    public partial class AccountInfoCollectionForm : XtraForm {
        public AccountInfoCollectionForm() {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            List<ExchangeAccountInfo> list = SettingsStore.Default.Accounts;
            //foreach(Exchange exchange in Exchange.Registered)
            //    list.Add(new ExchangeAccountInfo() { Exchange = exchange, Type = exchange.Type, ApiKey = exchange.ApiKey, Secret = exchange.ApiSecret });
            this.apiKeyInfoBindingSource.DataSource = list;
            Keys = list;
        }

        protected List<ExchangeAccountInfo> Keys { get; set; }
        private void simpleButton2_Click(object sender, EventArgs e) {
            Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            foreach(ExchangeAccountInfo info in Keys) {
                if(info.Default) {
                    info.Exchange.ApiKey = string.IsNullOrEmpty(info.ApiKey) ? "" : info.ApiKey.Trim();
                    info.Exchange.ApiSecret = string.IsNullOrEmpty(info.Secret) ? "" : info.Secret.Trim();
                }
                info.Exchange.Save();
            }
            SettingsStore.Default.SaveToXml();
            Close();
        }

        private void biAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            using(AccountInfoEditingForm form = new AccountInfoEditingForm()) {
                form.AddNew();
                form.Owner = this;
                if(form.ShowDialog() == DialogResult.OK) {
                    form.Account.Exchange = Exchange.Registered.FirstOrDefault(ee => ee.Type == form.Account.Type);
                    SettingsStore.Default.Accounts.Add(form.Account);
                    SettingsStore.Default.SaveToXml();
                    this.gridView1.RefreshData();
                }
            }
        }

        private void biEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            using(AccountInfoEditingForm form = new AccountInfoEditingForm()) {
                form.Edit((ExchangeAccountInfo)this.gridView1.GetFocusedRow());
                form.Owner = this;
                if(form.ShowDialog() == DialogResult.OK) {
                    form.Account.Exchange = Exchange.Registered.FirstOrDefault(ee => ee.Type == form.Account.Type);
                    SettingsStore.Default.SaveToXml();
                    this.gridView1.RefreshData();
                }
            }
        }

        private void biRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            int[] handles = this.gridView1.GetSelectedRows();
            if(handles == null || handles.Length == 0) {
                XtraMessageBox.Show("No account selected");
                return;
            }
            if(XtraMessageBox.Show("Do you really want to remove selected accounts?", "Removing Accounts...", MessageBoxButtons.YesNoCancel) == DialogResult.Yes) {
                return;
            }
            for(int i = 0; i < handles.Length; i++) {
                ExchangeAccountInfo info = (ExchangeAccountInfo)this.gridView1.GetRow(handles[i]);
                if(info.Exchange != null)
                    info.Exchange.OnAccountRemoved(info);
                SettingsStore.Default.Accounts.Remove(info);
            }
        }

        private void riActiveCheckEdit_CheckedChanged(object sender, EventArgs e) {
            this.gridView1.CloseEditor();
        }

        private void riDefaultCheckEdit_CheckedChanged(object sender, EventArgs e) {
            ExchangeAccountInfo focused = (ExchangeAccountInfo)this.gridView1.GetFocusedRow();
            focused.Default = true;
            List<ExchangeAccountInfo> accounts = Keys.Where(k => k.Type == focused.Type).ToList();
            foreach(ExchangeAccountInfo a in accounts) {
                if(a != focused)
                    a.Default = false;
            }
            this.gridView1.RefreshData();
            SettingsStore.Default.SaveToXml();
        }
    }
}
