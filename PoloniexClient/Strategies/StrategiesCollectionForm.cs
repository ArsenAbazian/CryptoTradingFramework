using Crypto.Core.Strategies;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Strategies {
    public partial class StrategiesCollectionForm : XtraForm {
        public StrategiesCollectionForm() {
            Manager = StrategiesManager.Defaut;
            Manager.DataProvider = new RealtimeStrategyDataProvider();
            InitializeComponent();
            InitializeAddStrategiesMenu();
        }
        
        protected void UpdateStatusText() {
            if(!Manager.Running) {
                this.siStatus.Caption = "<b>Stopped</b>";
                this.siStatus.ItemAppearance.Normal.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            }
            else {
                this.siStatus.Caption = "<b>Running</b>";
                this.siStatus.ItemAppearance.Normal.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Information;
            }
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            UpdateStatusText();
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
            UpdateStatusText();
            this.gridView1.RefreshData();
        }

        private void biStop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(XtraMessageBox.Show("Do you really want to stop active strategies?", "Stopping", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                return;
            if(!Manager.Stop()) {
                XtraMessageBox.Show("There are troubles stopping strategies manager. Check log for detailed information.");
                return;
            }
            UpdateStatusText();
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
            strategy.Manager = Manager;
            if(!StrategyConfigurationManager.Default.EditStrategy(strategy))
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
            if(Manager.Running) {
                XtraMessageBox.Show("Manager is running. Please stop execution first.");
                return;
            }
            foreach(StrategyBase st in selected) {
                Manager.Strategies.Remove(st);
            }
            Manager.Save();
            this.gridControl1.RefreshDataSource();
        }

        private void biEdit_ItemClick(object sender, ItemClickEventArgs e) {
            StrategyBase strategy = (StrategyBase)this.gridView1.GetFocusedRow();
            if(strategy == null) {
                XtraMessageBox.Show("No strategy selected for editing.");
                return;
            }
            StrategyBase cloned = strategy.Clone();
            if(!StrategyConfigurationManager.Default.EditStrategy(cloned))
                return;
            strategy.Assign(cloned);
            Manager.Save();
            this.gridView1.RefreshData();
        }
        protected override void OnClosing(CancelEventArgs e) {
            if(Manager.Running) {
                if(XtraMessageBox.Show("Do you really want to stop active strategies?", "Stopping", MessageBoxButtons.YesNoCancel) != DialogResult.Yes) {
                    e.Cancel = true;
                    return;
                }
                if(!Manager.Stop()) {
                    XtraMessageBox.Show("There are troubles stopping strategies manager. Check log for detailed information.");
                    e.Cancel = true;
                    return;
                }
            }
            this.gridView1.RefreshData();
            base.OnClosing(e);
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

        private void btShowData_ItemClick(object sender, ItemClickEventArgs e) {
            StrategyBase strategy = (StrategyBase)this.gridView1.GetFocusedRow();
            if(strategy == null)
                return;
            StrategyConfigurationManager.Default.ShowData(strategy);
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e) {
            if(this.gridView1.FocusedRowHandle != e.RowHandle)
                return;
            e.Appearance.BackColor = Color.FromArgb(0x10, this.gridView1.PaintAppearance.FocusedRow.BackColor);
            e.HighPriority = true;
        }

        private void biSimulation_ItemClick(object sender, ItemClickEventArgs e) {
            StrategyBase strategy = (StrategyBase)this.gridView1.GetFocusedRow();
            if(strategy == null) {
                XtraMessageBox.Show("No strategy selected.");
                return;
            }
            if(!strategy.SupportSimulation) {
                XtraMessageBox.Show("This strategy does not support simulation.");
                return;
            }

            StrategiesManager manager = new StrategiesManager();
            StrategyBase cloned = strategy.Clone();
            cloned.DemoMode = true;
            manager.Strategies.Add(cloned);

            this.siStatus.Caption = "<b>Loading data from exchanges...</b>";
            IOverlaySplashScreenHandle handle = SplashScreenManager.ShowOverlayForm(this);
            Application.DoEvents();
            manager.Initialize(new SimulationStrategyDataProvider());
            if(!manager.Start()) {
                XtraMessageBox.Show("Error starting simulation! Please check log messages");
                return;
            }
            this.siStatus.Caption = "<b>Running simulation...</b>";
            Application.DoEvents();

            Stopwatch timer = new Stopwatch();
            timer.Start();
            int elapsedSeconds = 0;
            while(manager.Running) {
                if(timer.ElapsedMilliseconds / 1000 > elapsedSeconds) {
                    elapsedSeconds = (int)(timer.ElapsedMilliseconds / 1000);
                    this.siStatus.Caption = string.Format("<b>Running simulation... {0} sec</b>", elapsedSeconds);
                    Application.DoEvents();
                }
            }
            SplashScreenManager.CloseOverlayForm(handle);
            this.siStatus.Caption = "<b>Simulation done.</b>";
            Application.DoEvents();
            StrategyConfigurationManager.Default.ShowData(cloned);
        }
    }
}
