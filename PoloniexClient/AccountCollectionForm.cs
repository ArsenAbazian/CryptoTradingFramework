using CryptoMarketClient.Bittrex;
using CryptoMarketClient.Common;
using DevExpress.Utils;
using DevExpress.Utils.Serializing;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public partial class AccountCollectionForm : XtraForm {
        public AccountCollectionForm() {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            UpdateDataSource();
        }

        protected void UpdateDataSource() {
            BindingList<AccountInfo> list = GetAccounts();
            this.apiKeyInfoBindingSource.DataSource = list;
            Keys = list;
            this.gridView1.ExpandAllGroups();
        }

        protected BindingList<AccountInfo> GetAccounts() {
            BindingList<AccountInfo> res = new BindingList<AccountInfo>();
            foreach(Exchange e in Exchange.Registered) {
                foreach(var account in e.Accounts)
                    res.Add(account);
            }
            return res;
        }

        protected BindingList<AccountInfo> Keys { get; set; }
        private void cancelButton_Click(object sender, EventArgs e) {
            Close();
        }

        private void okButton_Click(object sender, EventArgs e) {
            foreach(AccountInfo info in Keys) {
                if(info.Default) {
                    info.ApiKey = info.ApiKey.Trim();
                    info.Secret = info.Secret.Trim();
                }
            }
            foreach(Exchange ee in Exchange.Registered) {
                ee.Save();
            }
            Close();
        }

        private void biAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            using(AccountEditingForm form = new AccountEditingForm()) {
                form.AddNew();
                form.Owner = this;
                if(form.ShowDialog() == DialogResult.OK) {
                    form.Account.Exchange = Exchange.Registered.FirstOrDefault(ee => ee.Type == form.Account.Type);
                    Keys.Add(form.Account);
                    form.Account.Exchange.Save();
                }
            }
        }

        private void biEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            using(AccountEditingForm form = new AccountEditingForm()) {
                form.Edit((AccountInfo)this.gridView1.GetFocusedRow());
                form.Owner = this;
                if(form.ShowDialog() == DialogResult.OK) {
                    form.Account.Exchange = Exchange.Registered.FirstOrDefault(ee => ee.Type == form.Account.Type);
                    form.Account.Exchange.Save();
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
            if(XtraMessageBox.Show("Do you really want to remove selected accounts?", "Removing Accounts...", MessageBoxButtons.YesNoCancel) != DialogResult.Yes) {
                return;
            }
            for(int i = 0; i < handles.Length; i++) {
                AccountInfo info = (AccountInfo)this.gridView1.GetRow(handles[i]);
                if(info.Exchange != null) {
                    Exchange ee = info.Exchange;
                    info.Exchange = null;
                    Keys.Remove(info);
                    ee.OnAccountRemoved(info);
                }
            }
        }

        private void riActiveCheckEdit_CheckedChanged(object sender, EventArgs e) {
            this.gridView1.CloseEditor();
        }

        private void riDefaultCheckEdit_CheckedChanged(object sender, EventArgs e) {
            AccountInfo focused = (AccountInfo)this.gridView1.GetFocusedRow();
            focused.Default = true;
            List<AccountInfo> accounts = Keys.Where(k => k.Type == focused.Type).ToList();
            foreach(AccountInfo a in accounts) {
                if(a != focused)
                    a.Default = false;
            }
            this.gridView1.RefreshData();
        }

        private void btExportToFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            AccountsData data = new AccountsData();
            data.Accounts.AddRange(Keys);
            data.FileName = this.saveFileDialog1.FileName;
            data.SaveToXml();
            System.Diagnostics.Process.Start(Path.GetDirectoryName(data.FileName));
        }

        private void biImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            AccountsData data = new AccountsData();
            data.FileName = this.saveFileDialog1.FileName;
            data.RestoreFromXml();
            int count = 0;
            foreach(AccountInfo account in data.Accounts) {
                if(Keys.FirstOrDefault(a => a.ApiKey == account.ApiKey) != null)
                    continue;
                count++;
                account.Exchange = Exchange.Registered.FirstOrDefault(ee => ee.Type == account.Type);
                Keys.Add(account);
            }
            if(count < data.Accounts.Count)
                XtraMessageBox.Show("Api Keys successfully imported. However some Api Keys skipped, because they are already added.");
            else
                XtraMessageBox.Show("All Api Keys successfully imported.");
        }
    }

    public class AccountsData : IXtraSerializable {
        public static string AccountsDataName = "Accounts";
        public string FileName { get; set; }

        protected virtual bool SaveLayoutCore(XtraSerializer serializer, object path) {
            System.IO.Stream stream = path as System.IO.Stream;
            if(stream != null)
                return serializer.SerializeObjects(
                    new XtraObjectInfo[] { new XtraObjectInfo(AccountsDataName, this) }, stream, this.GetType().Name);
            else
                return serializer.SerializeObjects(
                    new XtraObjectInfo[] { new XtraObjectInfo(AccountsDataName, this) }, path.ToString(), this.GetType().Name);
        }
        protected virtual void RestoreLayoutCore(XtraSerializer serializer, object path) {
            System.IO.Stream stream = path as System.IO.Stream;
            if(stream != null)
                serializer.DeserializeObjects(new XtraObjectInfo[] { new XtraObjectInfo(AccountsDataName, this) },
                    stream, this.GetType().Name);
            else
                serializer.DeserializeObjects(new XtraObjectInfo[] { new XtraObjectInfo(AccountsDataName, this) },
                    path.ToString(), this.GetType().Name);
        }

        public void RestoreFromXml() {
            if(!File.Exists(FileName))
                return;
            RestoreLayoutCore(new XmlXtraSerializer(), FileName);
        }

        public void SaveToXml() {
            SaveLayoutCore(new XmlXtraSerializer(), FileName);
        }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true)]
        public List<AccountInfo> Accounts { get; } = new List<AccountInfo>();

        protected AccountInfo XtraCreateAccountsItem(XtraItemEventArgs e) {
            return new AccountInfo();
        }

        protected void XtraSetIndexAccountsItem(XtraSetItemIndexEventArgs e) {
            Accounts.Add((AccountInfo)e.Item.Value);
        }

        void IXtraSerializable.OnStartSerializing() {

        }

        void IXtraSerializable.OnEndSerializing() {

        }

        void IXtraSerializable.OnStartDeserializing(LayoutAllowEventArgs e) {
        }

        void IXtraSerializable.OnEndDeserializing(string restoredVersion) {
        }
    }
}
