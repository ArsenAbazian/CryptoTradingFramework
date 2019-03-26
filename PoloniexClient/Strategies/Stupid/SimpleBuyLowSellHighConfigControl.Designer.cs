namespace CryptoMarketClient.Strategies.Stupid {
    partial class SimpleBuyLowSellHighConfigControl {
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
            this.TickerInfoTextEditView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBaseCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMarketCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BuyLevelTextEdit = new DevExpress.XtraEditors.SpinEdit();
            this.SellLevelTextEdit = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForBuyLevel = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForSellLevel = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForTickerInfo = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleBuyLowSellHighStrategyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tickerNameInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colExchange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TickerInfoTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TickerInfoTextEditView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BuyLevelTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SellLevelTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForBuyLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSellLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTickerInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleBuyLowSellHighStrategyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tickerNameInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.TickerInfoTextEdit);
            this.dataLayoutControl1.Controls.Add(this.BuyLevelTextEdit);
            this.dataLayoutControl1.Controls.Add(this.SellLevelTextEdit);
            this.dataLayoutControl1.DataSource = this.simpleBuyLowSellHighStrategyBindingSource;
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
            this.TickerInfoTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.simpleBuyLowSellHighStrategyBindingSource, "TickerInfo", true));
            this.TickerInfoTextEdit.Location = new System.Drawing.Point(108, 120);
            this.TickerInfoTextEdit.Name = "TickerInfoTextEdit";
            this.TickerInfoTextEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.DropDown)});
            this.TickerInfoTextEdit.Properties.DataSource = this.tickerNameInfoBindingSource;
            this.TickerInfoTextEdit.Properties.DisplayMember = "FullName";
            this.TickerInfoTextEdit.Properties.PopupView = this.TickerInfoTextEditView;
            this.TickerInfoTextEdit.Properties.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.TickerInfoTextEdit.Properties.ViewType = DevExpress.XtraEditors.Repository.GridLookUpViewType.GridView;
            this.TickerInfoTextEdit.Size = new System.Drawing.Size(1045, 40);
            this.TickerInfoTextEdit.StyleController = this.dataLayoutControl1;
            this.TickerInfoTextEdit.TabIndex = 6;
            this.TickerInfoTextEdit.EditValueChanged += new System.EventHandler(this.TickerInfoTextEdit_EditValueChanged);
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
            // BuyLevelTextEdit
            // 
            this.BuyLevelTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.simpleBuyLowSellHighStrategyBindingSource, "BuyLevel", true));
            this.BuyLevelTextEdit.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            458752});
            this.BuyLevelTextEdit.Location = new System.Drawing.Point(108, 16);
            this.BuyLevelTextEdit.Name = "BuyLevelTextEdit";
            this.BuyLevelTextEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.BuyLevelTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.BuyLevelTextEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.BuyLevelTextEdit.Properties.DisplayFormat.FormatString = "0.########";
            this.BuyLevelTextEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.BuyLevelTextEdit.Properties.EditFormat.FormatString = "0.########";
            this.BuyLevelTextEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.BuyLevelTextEdit.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.BuyLevelTextEdit.Properties.Mask.EditMask = "F";
            this.BuyLevelTextEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.BuyLevelTextEdit.Properties.MaxValue = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.BuyLevelTextEdit.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            458752});
            this.BuyLevelTextEdit.Size = new System.Drawing.Size(1045, 40);
            this.BuyLevelTextEdit.StyleController = this.dataLayoutControl1;
            this.BuyLevelTextEdit.TabIndex = 4;
            // 
            // SellLevelTextEdit
            // 
            this.SellLevelTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.simpleBuyLowSellHighStrategyBindingSource, "SellLevel", true));
            this.SellLevelTextEdit.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            458752});
            this.SellLevelTextEdit.Location = new System.Drawing.Point(108, 68);
            this.SellLevelTextEdit.Name = "SellLevelTextEdit";
            this.SellLevelTextEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.SellLevelTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.SellLevelTextEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SellLevelTextEdit.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.SellLevelTextEdit.Properties.Mask.EditMask = "F";
            this.SellLevelTextEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.SellLevelTextEdit.Properties.MaxValue = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.SellLevelTextEdit.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            458752});
            this.SellLevelTextEdit.Size = new System.Drawing.Size(1045, 40);
            this.SellLevelTextEdit.StyleController = this.dataLayoutControl1;
            this.SellLevelTextEdit.TabIndex = 5;
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
            this.ItemForBuyLevel,
            this.ItemForSellLevel,
            this.ItemForTickerInfo});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "autoGeneratedGroup0";
            this.layoutControlGroup2.Size = new System.Drawing.Size(1149, 812);
            // 
            // ItemForBuyLevel
            // 
            this.ItemForBuyLevel.Control = this.BuyLevelTextEdit;
            this.ItemForBuyLevel.Location = new System.Drawing.Point(0, 0);
            this.ItemForBuyLevel.Name = "ItemForBuyLevel";
            this.ItemForBuyLevel.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.ItemForBuyLevel.Size = new System.Drawing.Size(1149, 52);
            this.ItemForBuyLevel.Text = "Buy Level";
            this.ItemForBuyLevel.TextSize = new System.Drawing.Size(89, 25);
            // 
            // ItemForSellLevel
            // 
            this.ItemForSellLevel.Control = this.SellLevelTextEdit;
            this.ItemForSellLevel.Location = new System.Drawing.Point(0, 52);
            this.ItemForSellLevel.Name = "ItemForSellLevel";
            this.ItemForSellLevel.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.ItemForSellLevel.Size = new System.Drawing.Size(1149, 52);
            this.ItemForSellLevel.Text = "Sell Level";
            this.ItemForSellLevel.TextSize = new System.Drawing.Size(89, 25);
            // 
            // ItemForTickerInfo
            // 
            this.ItemForTickerInfo.Control = this.TickerInfoTextEdit;
            this.ItemForTickerInfo.Location = new System.Drawing.Point(0, 104);
            this.ItemForTickerInfo.Name = "ItemForTickerInfo";
            this.ItemForTickerInfo.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.ItemForTickerInfo.Size = new System.Drawing.Size(1149, 708);
            this.ItemForTickerInfo.Text = "Ticker";
            this.ItemForTickerInfo.TextSize = new System.Drawing.Size(89, 25);
            // 
            // simpleBuyLowSellHighStrategyBindingSource
            // 
            this.simpleBuyLowSellHighStrategyBindingSource.DataSource = typeof(CryptoMarketClient.Strategies.Stupid.SimpleBuyLowSellHighStrategy);
            // 
            // tickerNameInfoBindingSource
            // 
            this.tickerNameInfoBindingSource.DataSource = typeof(Crypto.Core.Common.TickerNameInfo);
            // 
            // colExchange
            // 
            this.colExchange.FieldName = "Exchange";
            this.colExchange.Name = "colExchange";
            this.colExchange.Visible = true;
            this.colExchange.VisibleIndex = 0;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // SimpleBuyLowSellHighConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataLayoutControl1);
            this.Name = "SimpleBuyLowSellHighConfigControl";
            this.Size = new System.Drawing.Size(1169, 832);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TickerInfoTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TickerInfoTextEditView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BuyLevelTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SellLevelTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForBuyLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSellLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTickerInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleBuyLowSellHighStrategyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tickerNameInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraEditors.GridLookUpEdit TickerInfoTextEdit;
        private DevExpress.XtraEditors.SpinEdit BuyLevelTextEdit;
        private DevExpress.XtraEditors.SpinEdit SellLevelTextEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem ItemForBuyLevel;
        private DevExpress.XtraLayout.LayoutControlItem ItemForSellLevel;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTickerInfo;
        private System.Windows.Forms.BindingSource simpleBuyLowSellHighStrategyBindingSource;
        private System.Windows.Forms.BindingSource tickerNameInfoBindingSource;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView TickerInfoTextEditView;
        private DevExpress.XtraGrid.Columns.GridColumn colExchange;
        private DevExpress.XtraGrid.Columns.GridColumn colBaseCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colMarketCurrency;
    }
}
