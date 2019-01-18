using Crypto.Core.Strategies;

namespace CryptoMarketClient.Strategies {
    partial class StrategyConfigurationForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.EnabledCheckEdit = new DevExpress.XtraEditors.ToggleSwitch();
            this.DemoModeCheckEdit = new DevExpress.XtraEditors.ToggleSwitch();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.DescriptionTextEdit = new DevExpress.XtraEditors.MemoEdit();
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.leAccounts = new DevExpress.XtraEditors.GridLookUpEdit();
            this.leAccountsView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDefault = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.tabbedControlGroup1 = new DevExpress.XtraLayout.TabbedControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForEnabled = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForDemoMode = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForDescription = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgStrategySpecific = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.statisticalArbitrageStrategyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.accountInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EnabledCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DemoModeCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DescriptionTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leAccounts.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leAccountsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForEnabled)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDemoMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgStrategySpecific)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statisticalArbitrageStrategyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accountInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Appearance.Control.FontSizeDelta = 4;
            this.dataLayoutControl1.Appearance.Control.Options.UseFont = true;
            this.dataLayoutControl1.Controls.Add(this.simpleButton1);
            this.dataLayoutControl1.Controls.Add(this.simpleButton2);
            this.dataLayoutControl1.Controls.Add(this.EnabledCheckEdit);
            this.dataLayoutControl1.Controls.Add(this.DemoModeCheckEdit);
            this.dataLayoutControl1.Controls.Add(this.textEdit1);
            this.dataLayoutControl1.Controls.Add(this.DescriptionTextEdit);
            this.dataLayoutControl1.Controls.Add(this.spinEdit1);
            this.dataLayoutControl1.Controls.Add(this.leAccounts);
            this.dataLayoutControl1.DataMember = null;
            this.dataLayoutControl1.DataSource = this.statisticalArbitrageStrategyBindingSource;
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.dataLayoutControl1.Size = new System.Drawing.Size(1390, 1144);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // simpleButton1
            // 
            this.simpleButton1.AutoWidthInLayoutControl = true;
            this.simpleButton1.Location = new System.Drawing.Point(967, 1079);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Padding = new System.Windows.Forms.Padding(70, 0, 60, 0);
            this.simpleButton1.Size = new System.Drawing.Size(190, 52);
            this.simpleButton1.StyleController = this.dataLayoutControl1;
            this.simpleButton1.TabIndex = 9;
            this.simpleButton1.Text = "OK";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.AutoWidthInLayoutControl = true;
            this.simpleButton2.Location = new System.Drawing.Point(1187, 1079);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Padding = new System.Windows.Forms.Padding(40, 0, 40, 0);
            this.simpleButton2.Size = new System.Drawing.Size(190, 52);
            this.simpleButton2.StyleController = this.dataLayoutControl1;
            this.simpleButton2.TabIndex = 10;
            this.simpleButton2.Text = "Cancel";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // EnabledCheckEdit
            // 
            this.EnabledCheckEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.statisticalArbitrageStrategyBindingSource, "Enabled", true));
            this.EnabledCheckEdit.Location = new System.Drawing.Point(328, 305);
            this.EnabledCheckEdit.Name = "EnabledCheckEdit";
            this.EnabledCheckEdit.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.EnabledCheckEdit.Properties.OffText = "Off";
            this.EnabledCheckEdit.Properties.OnText = "On";
            this.EnabledCheckEdit.Properties.ShowText = false;
            this.EnabledCheckEdit.Size = new System.Drawing.Size(1026, 59);
            this.EnabledCheckEdit.StyleController = this.dataLayoutControl1;
            this.EnabledCheckEdit.TabIndex = 5;
            // 
            // DemoModeCheckEdit
            // 
            this.DemoModeCheckEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.statisticalArbitrageStrategyBindingSource, "DemoMode", true));
            this.DemoModeCheckEdit.Location = new System.Drawing.Point(328, 382);
            this.DemoModeCheckEdit.Name = "DemoModeCheckEdit";
            this.DemoModeCheckEdit.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.DemoModeCheckEdit.Properties.OffText = "Off";
            this.DemoModeCheckEdit.Properties.OnText = "On";
            this.DemoModeCheckEdit.Properties.ShowText = false;
            this.DemoModeCheckEdit.Size = new System.Drawing.Size(1026, 59);
            this.DemoModeCheckEdit.StyleController = this.dataLayoutControl1;
            this.DemoModeCheckEdit.TabIndex = 6;
            // 
            // textEdit1
            // 
            this.textEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.statisticalArbitrageStrategyBindingSource, "Name", true));
            this.textEdit1.Location = new System.Drawing.Point(328, 89);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(1026, 54);
            this.textEdit1.StyleController = this.dataLayoutControl1;
            this.textEdit1.TabIndex = 11;
            this.textEdit1.TextChanged += new System.EventHandler(this.textEdit1_TextChanged);
            // 
            // DescriptionTextEdit
            // 
            this.DescriptionTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.statisticalArbitrageStrategyBindingSource, "Description", true));
            this.DescriptionTextEdit.Location = new System.Drawing.Point(328, 459);
            this.DescriptionTextEdit.Name = "DescriptionTextEdit";
            this.DescriptionTextEdit.Size = new System.Drawing.Size(1026, 566);
            this.DescriptionTextEdit.StyleController = this.dataLayoutControl1;
            this.DescriptionTextEdit.TabIndex = 7;
            // 
            // spinEdit1
            // 
            this.spinEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.statisticalArbitrageStrategyBindingSource, "MaxAllowedDeposit", true));
            this.spinEdit1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEdit1.Location = new System.Drawing.Point(328, 161);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEdit1.Properties.DisplayFormat.FormatString = "0.########";
            this.spinEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEdit1.Properties.EditFormat.FormatString = "0.########";
            this.spinEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEdit1.Size = new System.Drawing.Size(1026, 54);
            this.spinEdit1.StyleController = this.dataLayoutControl1;
            this.spinEdit1.TabIndex = 12;
            // 
            // leAccounts
            // 
            this.leAccounts.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.statisticalArbitrageStrategyBindingSource, "AccountId", true));
            this.leAccounts.Location = new System.Drawing.Point(328, 233);
            this.leAccounts.Name = "leAccounts";
            this.leAccounts.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leAccounts.Properties.DataSource = this.accountInfoBindingSource;
            this.leAccounts.Properties.DisplayMember = "FullName";
            this.leAccounts.Properties.PopupFormSize = new System.Drawing.Size(600, 0);
            this.leAccounts.Properties.PopupView = this.leAccountsView;
            this.leAccounts.Properties.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.leAccounts.Properties.ValueMember = "Id";
            this.leAccounts.Properties.ViewType = DevExpress.XtraEditors.Repository.GridLookUpViewType.GridView;
            this.leAccounts.Size = new System.Drawing.Size(1026, 54);
            this.leAccounts.StyleController = this.dataLayoutControl1;
            this.leAccounts.TabIndex = 13;
            // 
            // leAccountsView
            // 
            this.leAccountsView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colType,
            this.colName,
            this.colDefault,
            this.colActive});
            this.leAccountsView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.leAccountsView.GroupCount = 1;
            this.leAccountsView.Name = "leAccountsView";
            this.leAccountsView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.leAccountsView.OptionsView.ShowGroupPanel = false;
            this.leAccountsView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colType, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            // 
            // colDefault
            // 
            this.colDefault.FieldName = "Default";
            this.colDefault.Name = "colDefault";
            this.colDefault.Visible = true;
            this.colDefault.VisibleIndex = 1;
            // 
            // colActive
            // 
            this.colActive.FieldName = "Active";
            this.colActive.Name = "colActive";
            this.colActive.Visible = true;
            this.colActive.VisibleIndex = 2;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceGroup.FontSizeDelta = 4;
            this.layoutControlGroup1.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1390, 1144);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AllowDrawBackground = false;
            this.layoutControlGroup2.AppearanceGroup.FontSizeDelta = 4;
            this.layoutControlGroup2.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.tabbedControlGroup1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "autoGeneratedGroup0";
            this.layoutControlGroup2.Size = new System.Drawing.Size(1370, 1041);
            // 
            // tabbedControlGroup1
            // 
            this.tabbedControlGroup1.AppearanceGroup.FontSizeDelta = 4;
            this.tabbedControlGroup1.AppearanceGroup.Options.UseFont = true;
            this.tabbedControlGroup1.AppearanceItemCaption.FontSizeDelta = 4;
            this.tabbedControlGroup1.AppearanceItemCaption.Options.UseFont = true;
            this.tabbedControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.tabbedControlGroup1.Name = "tabbedControlGroup1";
            this.tabbedControlGroup1.SelectedTabPage = this.layoutControlGroup3;
            this.tabbedControlGroup1.Size = new System.Drawing.Size(1370, 1041);
            this.tabbedControlGroup1.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.lcgStrategySpecific});
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.AppearanceTabPage.Header.FontSizeDelta = 4;
            this.layoutControlGroup3.AppearanceTabPage.Header.Options.UseFont = true;
            this.layoutControlGroup3.AppearanceTabPage.HeaderActive.FontSizeDelta = 4;
            this.layoutControlGroup3.AppearanceTabPage.HeaderActive.Options.UseFont = true;
            this.layoutControlGroup3.AppearanceTabPage.HeaderDisabled.FontSizeDelta = 4;
            this.layoutControlGroup3.AppearanceTabPage.HeaderDisabled.Options.UseFont = true;
            this.layoutControlGroup3.AppearanceTabPage.HeaderHotTracked.FontSizeDelta = 4;
            this.layoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForEnabled,
            this.ItemForDemoMode,
            this.layoutControlItem3,
            this.ItemForDescription,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(1340, 954);
            this.layoutControlGroup3.Text = "Common";
            // 
            // ItemForEnabled
            // 
            this.ItemForEnabled.Control = this.EnabledCheckEdit;
            this.ItemForEnabled.Location = new System.Drawing.Point(0, 216);
            this.ItemForEnabled.Name = "ItemForEnabled";
            this.ItemForEnabled.Padding = new DevExpress.XtraLayout.Utils.Padding(9, 9, 9, 9);
            this.ItemForEnabled.Size = new System.Drawing.Size(1340, 77);
            this.ItemForEnabled.Text = "Enabled";
            this.ItemForEnabled.TextSize = new System.Drawing.Size(293, 39);
            // 
            // ItemForDemoMode
            // 
            this.ItemForDemoMode.Control = this.DemoModeCheckEdit;
            this.ItemForDemoMode.Location = new System.Drawing.Point(0, 293);
            this.ItemForDemoMode.Name = "ItemForDemoMode";
            this.ItemForDemoMode.Padding = new DevExpress.XtraLayout.Utils.Padding(9, 9, 9, 9);
            this.ItemForDemoMode.Size = new System.Drawing.Size(1340, 77);
            this.ItemForDemoMode.Text = "Demo Mode";
            this.ItemForDemoMode.TextSize = new System.Drawing.Size(293, 39);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.textEdit1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(9, 9, 9, 9);
            this.layoutControlItem3.Size = new System.Drawing.Size(1340, 72);
            this.layoutControlItem3.Text = "Name";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(293, 39);
            // 
            // ItemForDescription
            // 
            this.ItemForDescription.Control = this.DescriptionTextEdit;
            this.ItemForDescription.Location = new System.Drawing.Point(0, 370);
            this.ItemForDescription.Name = "ItemForDescription";
            this.ItemForDescription.Padding = new DevExpress.XtraLayout.Utils.Padding(9, 9, 9, 9);
            this.ItemForDescription.Size = new System.Drawing.Size(1340, 584);
            this.ItemForDescription.Text = "Description";
            this.ItemForDescription.TextSize = new System.Drawing.Size(293, 39);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.spinEdit1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(9, 9, 9, 9);
            this.layoutControlItem4.Size = new System.Drawing.Size(1340, 72);
            this.layoutControlItem4.Text = "Max Allowed Deposit";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(293, 39);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.leAccounts;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 144);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(9, 9, 9, 9);
            this.layoutControlItem5.Size = new System.Drawing.Size(1340, 72);
            this.layoutControlItem5.Text = "Trading Account";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(293, 39);
            // 
            // lcgStrategySpecific
            // 
            this.lcgStrategySpecific.AppearanceTabPage.Header.FontSizeDelta = 4;
            this.lcgStrategySpecific.AppearanceTabPage.Header.Options.UseFont = true;
            this.lcgStrategySpecific.Location = new System.Drawing.Point(0, 0);
            this.lcgStrategySpecific.Name = "lcgStrategySpecific";
            this.lcgStrategySpecific.Size = new System.Drawing.Size(1340, 954);
            this.lcgStrategySpecific.Text = "Strategy Specific";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.simpleButton1;
            this.layoutControlItem1.Location = new System.Drawing.Point(954, 1041);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 28, 3);
            this.layoutControlItem1.Size = new System.Drawing.Size(196, 83);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButton2;
            this.layoutControlItem2.Location = new System.Drawing.Point(1150, 1041);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(27, 3, 28, 3);
            this.layoutControlItem2.Size = new System.Drawing.Size(220, 83);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 1041);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(954, 83);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // statisticalArbitrageStrategyBindingSource
            // 
            this.statisticalArbitrageStrategyBindingSource.DataSource = typeof(Crypto.Core.Strategies.StrategyBase);
            // 
            // accountInfoBindingSource
            // 
            this.accountInfoBindingSource.DataSource = typeof(CryptoMarketClient.AccountInfo);
            // 
            // colType
            // 
            this.colType.FieldName = "Type";
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 3;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // StrategyConfigurationForm
            // 
            this.Appearance.FontSizeDelta = 4;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(35F, 77F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1390, 1144);
            this.Controls.Add(this.dataLayoutControl1);
            this.Font = new System.Drawing.Font("Tahoma", 23.875F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "StrategyConfigurationForm";
            this.Text = "Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EnabledCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DemoModeCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DescriptionTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leAccounts.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leAccountsView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForEnabled)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDemoMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgStrategySpecific)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statisticalArbitrageStrategyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accountInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private System.Windows.Forms.BindingSource statisticalArbitrageStrategyBindingSource;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem ItemForEnabled;
        private DevExpress.XtraLayout.LayoutControlItem ItemForDemoMode;
        private DevExpress.XtraLayout.LayoutControlItem ItemForDescription;
        private DevExpress.XtraLayout.TabbedControlGroup tabbedControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlGroup lcgStrategySpecific;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.ToggleSwitch EnabledCheckEdit;
        private DevExpress.XtraEditors.ToggleSwitch DemoModeCheckEdit;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.MemoEdit DescriptionTextEdit;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.SpinEdit spinEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.GridLookUpEdit leAccounts;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private System.Windows.Forms.BindingSource accountInfoBindingSource;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView leAccountsView;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colDefault;
        private DevExpress.XtraGrid.Columns.GridColumn colActive;
    }
}