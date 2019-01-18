using Crypto.Core.Strategies;
using DevExpress.XtraBars;
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

namespace CryptoMarketClient.Strategies {
    public partial class StrategiesCollectionForm : XtraForm {
        public StrategiesCollectionForm() {
            Manager = StrategiesManager.Defaut;
            InitializeComponent();
            InitializeAddStrategiesMenu();
        }
        
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            this.strategyBaseBindingSource.DataSource = Manager.Strategies;
        }

        protected StrategiesManager Manager { get; private set; }
        private void biStart_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(!Manager.Initialized) {
                if(!Manager.Initialize(new RealtimeStrategyDataProvider())) {
                    XtraMessageBox.Show("There are troubles initializing manager with RealtimeStrategyDataProvider. Check log for detailed information.");
                    return;
                }
            }
            if(!Manager.Start()) {
                XtraMessageBox.Show("There are troubles starting strategies manager. Check log for detailed information.");
                return;
            }
            this.gridView1.RefreshData();
        }

        private void biStop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(XtraMessageBox.Show("Do you really want to stop active strategies?", "Stopping", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                return;
            if(!Manager.Stop()) {
                XtraMessageBox.Show("There are troubles stopping strategies manager. Check log for detailed information.");
                return;
            }
            this.gridView1.RefreshData();
        }

        void InitializeAddStrategiesMenu() {
            foreach(var strategy in StrategiesRegistrator.RegisteredStrategies) {
                CreateStrategyGroupSubMenu(strategy);
            }
        }
        void CreateStrategyGroupSubMenu(StrategyRegistrationInfo info) {
            string[] path = info.Group.Split('.');
            BarSubItem root = this.siAdd;
            for(int i = 0; i < path.Length; i++) {
                root = GetPath(root, path[i]);
            }
            GetOrCreateCommand(root, info);
        }

        private void GetOrCreateCommand(BarSubItem root, StrategyRegistrationInfo info) {
            foreach(BarItemLink link in root.ItemLinks) {
                if(link.Item.Tag == info)
                    return;
            }
            BarButtonItem item = new BarButtonItem(this.barManager1, info.Name);
            item.ItemClick += OnStrategyInfoItemClick;
            item.Description = info.Description;
            item.Tag = info;
            root.ItemLinks.Add(item);
        }

        private void OnStrategyInfoItemClick(object sender, ItemClickEventArgs e) {
            StrategyRegistrationInfo info = (StrategyRegistrationInfo)e.Item.Tag;
            StrategyBase strategy = info.Create();
            if(!StrategyConfigurationManager.Default.ConfigureDialog(strategy))
                return;
            Manager.Add(strategy);
            Manager.Save();
            this.gridView1.RefreshData();
        }

        BarSubItem GetPath(BarSubItem root, string path) {
            foreach(BarItemLink link in root.ItemLinks) {
                if(link.Caption == path)
                    return (BarSubItem)link.Item;
            }
            BarSubItem item = new BarSubItem(this.barManager1, path);
            item.MenuDrawMode = MenuDrawMode.LargeImagesTextDescription;
            root.ItemLinks.Add(item);
            return item;
        }

        private void gridControl1_Click(object sender, EventArgs e) {

        }
        List<StrategyBase> GetSelectedItems() {
            int[] rowHandle = this.gridView1.GetSelectedRows();
            List<StrategyBase> selected = new List<StrategyBase>();
            for(int index = 0; index < rowHandle.Length; index++)
                selected.Add((StrategyBase)this.gridView1.GetRow(rowHandle[index]));
            return selected;
        }

        private void biRemove_ItemClick(object sender, ItemClickEventArgs e) {
            List<StrategyBase> selected = GetSelectedItems();
            if(selected.Count == 0) {
                XtraMessageBox.Show("Nothing selected.");
                return;
            }
            if(XtraMessageBox.Show("Do you really want to remove selected strategies? (May be better to deactivate them?)", "Removing", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                return;

        }

        private void biEdit_ItemClick(object sender, ItemClickEventArgs e) {
            StrategyBase strategy = (StrategyBase)this.gridView1.GetFocusedRow();
            if(strategy == null) {
                XtraMessageBox.Show("No strategy selected for editing.");
                return;
            }
            StrategyBase cloned = strategy.Clone();
            if(!StrategyConfigurationManager.Default.ConfigureDialog(cloned))
                return;
            strategy.Assign(cloned);
            Manager.Save();
            this.gridView1.RefreshData();
        }

        private void repositoryItemCheckEdit1_EditValueChanged(object sender, EventArgs e) {
            StrategyBase strategy = (StrategyBase)this.gridView1.GetFocusedRow();
            strategy.Enabled = ((CheckEdit)sender).Checked;
            this.gridView1.CloseEditor();
            Manager.Save();
        }

        private void repositoryItemCheckEdit2_EditValueChanged(object sender, EventArgs e) {
            StrategyBase strategy = (StrategyBase)this.gridView1.GetFocusedRow();
            strategy.DemoMode = ((CheckEdit)sender).Checked;
            this.gridView1.CloseEditor();
            Manager.Save();
        }
    }
}
