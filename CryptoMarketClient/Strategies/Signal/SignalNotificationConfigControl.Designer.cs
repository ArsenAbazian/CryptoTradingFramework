namespace CryptoMarketClient.Strategies.Signal {
    partial class SignalNotificationConfigControl {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.TickerInfoTextEdit = new DevExpress.XtraEditors.GridLookUpEdit();
            this.tickerNameInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TickerInfoTextEditView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colExchange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBaseCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMarketCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.signalNotificationStrategyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBoxEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.candleStickIntervalInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForTickerInfo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TickerInfoTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tickerNameInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TickerInfoTextEditView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.signalNotificationStrategyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.candleStickIntervalInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTickerInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.TickerInfoTextEdit);
            this.dataLayoutControl1.Controls.Add(this.spinEdit1);
            this.dataLayoutControl1.Controls.Add(this.comboBoxEdit1);
            this.dataLayoutControl1.DataSource = this.signalNotificationStrategyBindingSource;
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.dataLayoutControl1.Size = new System.Drawing.Size(1169, 832);
            this.dataLayoutControl1.TabIndex = 1;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // TickerInfoTextEdit
            // 
            this.TickerInfoTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.signalNotificationStrategyBindingSource, "TickerInfo", true));
            this.TickerInfoTextEdit.Location = new System.Drawing.Point(149, 16);
            this.TickerInfoTextEdit.Name = "TickerInfoTextEdit";
            this.TickerInfoTextEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.DropDown)});
            this.TickerInfoTextEdit.Properties.DataSource = this.tickerNameInfoBindingSource;
            this.TickerInfoTextEdit.Properties.DisplayMember = "FullName";
            this.TickerInfoTextEdit.Properties.PopupView = this.TickerInfoTextEditView;
            this.TickerInfoTextEdit.Properties.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.TickerInfoTextEdit.Properties.ViewType = DevExpress.XtraEditors.Repository.GridLookUpViewType.GridView;
            this.TickerInfoTextEdit.Size = new System.Drawing.Size(1004, 40);
            this.TickerInfoTextEdit.StyleController = this.dataLayoutControl1;
            this.TickerInfoTextEdit.TabIndex = 6;
            this.TickerInfoTextEdit.EditValueChanged += new System.EventHandler(this.TickerInfoTextEdit_EditValueChanged);
            // 
            // tickerNameInfoBindingSource
            // 
            this.tickerNameInfoBindingSource.DataSource = typeof(Crypto.Core.Common.TickerNameInfo);
            // 
            // TickerInfoTextEditView
            // 
            this.TickerInfoTextEditView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colExchange,
            this.colBaseCurrency,
            this.colMarketCurrency});
            this.TickerInfoTextEditView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.TickerInfoTextEditView.GroupCount = 2;
            this.TickerInfoTextEditView.Name = "TickerInfoTextEditView";
            this.TickerInfoTextEditView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.TickerInfoTextEditView.OptionsView.ShowGroupPanel = false;
            this.TickerInfoTextEditView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colExchange, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colBaseCurrency, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colExchange
            // 
            this.colExchange.FieldName = "Exchange";
            this.colExchange.Name = "colExchange";
            this.colExchange.Visible = true;
            this.colExchange.VisibleIndex = 0;
            // 
            // colBaseCurrency
            // 
            this.colBaseCurrency.FieldName = "BaseCurrency";
            this.colBaseCurrency.Name = "colBaseCurrency";
            this.colBaseCurrency.Visible = true;
            this.colBaseCurrency.VisibleIndex = 0;
            // 
            // colMarketCurrency
            // 
            this.colMarketCurrency.FieldName = "MarketCurrency";
            this.colMarketCurrency.Name = "colMarketCurrency";
            this.colMarketCurrency.Visible = true;
            this.colMarketCurrency.VisibleIndex = 0;
            this.colMarketCurrency.Width = 88;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // spinEdit1
            // 
            this.spinEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.signalNotificationStrategyBindingSource, "Length", true));
            this.spinEdit1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEdit1.Location = new System.Drawing.Point(145, 64);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEdit1.Size = new System.Drawing.Size(1012, 40);
            this.spinEdit1.StyleController = this.dataLayoutControl1;
            this.spinEdit1.TabIndex = 7;
            // 
            // signalNotificationStrategyBindingSource
            // 
            this.signalNotificationStrategyBindingSource.DataSource = typeof(Crypto.Core.Strategies.Signal.SignalNotificationStrategy);
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(145, 108);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Properties.DataSource = this.candleStickIntervalInfoBindingSource;
            this.comboBoxEdit1.Properties.DisplayMember = "Text";
            this.comboBoxEdit1.Properties.NullText = "";
            this.comboBoxEdit1.Properties.PopupSizeable = false;
            this.comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.comboBoxEdit1.Size = new System.Drawing.Size(1012, 40);
            this.comboBoxEdit1.StyleController = this.dataLayoutControl1;
            this.comboBoxEdit1.TabIndex = 8;
            this.comboBoxEdit1.EditValueChanged += new System.EventHandler(this.comboBoxEdit1_EditValueChanged);
            // 
            // candleStickIntervalInfoBindingSource
            // 
            this.candleStickIntervalInfoBindingSource.DataSource = typeof(Crypto.Core.CandleStickIntervalInfo);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1169, 832);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AllowDrawBackground = false;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForTickerInfo,
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "autoGeneratedGroup0";
            this.layoutControlGroup2.Size = new System.Drawing.Size(1149, 812);
            // 
            // ItemForTickerInfo
            // 
            this.ItemForTickerInfo.Control = this.TickerInfoTextEdit;
            this.ItemForTickerInfo.Location = new System.Drawing.Point(0, 0);
            this.ItemForTickerInfo.Name = "ItemForTickerInfo";
            this.ItemForTickerInfo.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.ItemForTickerInfo.Size = new System.Drawing.Size(1149, 52);
            this.ItemForTickerInfo.Text = "Ticker";
            this.ItemForTickerInfo.TextSize = new System.Drawing.Size(130, 25);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.spinEdit1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1149, 44);
            this.layoutControlItem1.Text = "Window Size";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(130, 25);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.comboBoxEdit1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1149, 716);
            this.layoutControlItem2.Text = "Chart Interval";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(130, 25);
            // 
            // SignalNotificationConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataLayoutControl1);
            this.Name = "SignalNotificationConfigControl";
            this.Size = new System.Drawing.Size(1169, 832);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TickerInfoTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tickerNameInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TickerInfoTextEditView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.signalNotificationStrategyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.candleStickIntervalInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTickerInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraEditors.GridLookUpEdit TickerInfoTextEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTickerInfo;
        private System.Windows.Forms.BindingSource tickerNameInfoBindingSource;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView TickerInfoTextEditView;
        private DevExpress.XtraGrid.Columns.GridColumn colExchange;
        private DevExpress.XtraGrid.Columns.GridColumn colBaseCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colMarketCurrency;
        private System.Windows.Forms.BindingSource signalNotificationStrategyBindingSource;
        private DevExpress.XtraEditors.SpinEdit spinEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.LookUpEdit comboBoxEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private System.Windows.Forms.BindingSource candleStickIntervalInfoBindingSource;
    }
}
