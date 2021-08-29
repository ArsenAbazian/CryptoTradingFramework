namespace CryptoMarketClient {
    partial class AnalyticsForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalyticsForm));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.biAddSymbol = new DevExpress.XtraBars.BarButtonItem();
            this.pcSymbols = new DevExpress.XtraBars.PopupControlContainer(this.components);
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcDownExchanges = new DevExpress.XtraGrid.GridControl();
            this.gvDownExchanges = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colExchange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDownloaded = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.sbOkDownload = new DevExpress.XtraEditors.SimpleButton();
            this.icbBase = new DevExpress.XtraEditors.ComboBoxEdit();
            this.icbMarket = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.biRemove = new DevExpress.XtraBars.BarButtonItem();
            this.biClear = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageCategory1 = new DevExpress.XtraBars.Ribbon.RibbonPageCategory();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcData = new DevExpress.XtraGrid.GridControl();
            this.gwData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colBase = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMarket = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcSymbols)).BeginInit();
            this.pcSymbols.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDownExchanges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDownExchanges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icbBase.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icbMarket.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gwData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.biAddSymbol,
            this.biRemove,
            this.biClear});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Margin = new System.Windows.Forms.Padding(6);
            this.ribbonControl1.MaxItemId = 4;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.PageCategories.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageCategory[] {
            this.ribbonPageCategory1});
            this.ribbonControl1.Size = new System.Drawing.Size(2288, 281);
            // 
            // biAddSymbol
            // 
            this.biAddSymbol.ActAsDropDown = true;
            this.biAddSymbol.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.biAddSymbol.Caption = "Download";
            this.biAddSymbol.DropDownControl = this.pcSymbols;
            this.biAddSymbol.Id = 1;
            this.biAddSymbol.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biAddSymbol.ImageOptions.SvgImage")));
            this.biAddSymbol.Name = "biAddSymbol";
            // 
            // pcSymbols
            // 
            this.pcSymbols.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcSymbols.Controls.Add(this.layoutControl1);
            this.pcSymbols.Location = new System.Drawing.Point(24, 33);
            this.pcSymbols.Margin = new System.Windows.Forms.Padding(6);
            this.pcSymbols.Name = "pcSymbols";
            this.pcSymbols.Ribbon = this.ribbonControl1;
            this.pcSymbols.Size = new System.Drawing.Size(1010, 683);
            this.pcSymbols.TabIndex = 0;
            this.pcSymbols.Visible = false;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcDownExchanges);
            this.layoutControl1.Controls.Add(this.sbOkDownload);
            this.layoutControl1.Controls.Add(this.icbBase);
            this.layoutControl1.Controls.Add(this.icbMarket);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(6);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1010, 683);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcDownExchanges
            // 
            this.gcDownExchanges.DataSource = typeof(ExchangeInfo);
            this.gcDownExchanges.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.gcDownExchanges.Location = new System.Drawing.Point(12, 100);
            this.gcDownExchanges.MainView = this.gvDownExchanges;
            this.gcDownExchanges.Margin = new System.Windows.Forms.Padding(6);
            this.gcDownExchanges.MenuManager = this.ribbonControl1;
            this.gcDownExchanges.Name = "gcDownExchanges";
            this.gcDownExchanges.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.gcDownExchanges.Size = new System.Drawing.Size(986, 523);
            this.gcDownExchanges.TabIndex = 5;
            this.gcDownExchanges.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDownExchanges});
            // 
            // gvDownExchanges
            // 
            this.gvDownExchanges.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSelected,
            this.colExchange,
            this.colPrice,
            this.colDownloaded});
            this.gvDownExchanges.DetailHeight = 673;
            this.gvDownExchanges.FixedLineWidth = 4;
            this.gvDownExchanges.GridControl = this.gcDownExchanges;
            this.gvDownExchanges.Name = "gvDownExchanges";
            this.gvDownExchanges.OptionsSelection.CheckBoxSelectorField = "Selected";
            this.gvDownExchanges.OptionsSelection.MultiSelect = true;
            this.gvDownExchanges.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.gvDownExchanges.OptionsView.EnableAppearanceEvenRow = true;
            this.gvDownExchanges.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvDownExchanges.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colSelected
            // 
            this.colSelected.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colSelected.FieldName = "Selected";
            this.colSelected.MinWidth = 40;
            this.colSelected.Name = "colSelected";
            this.colSelected.Visible = true;
            this.colSelected.VisibleIndex = 0;
            this.colSelected.Width = 112;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.CheckedChanged += new System.EventHandler(this.repositoryItemCheckEdit1_CheckedChanged);
            // 
            // colExchange
            // 
            this.colExchange.FieldName = "Exchange";
            this.colExchange.MinWidth = 40;
            this.colExchange.Name = "colExchange";
            this.colExchange.OptionsColumn.AllowEdit = false;
            this.colExchange.Visible = true;
            this.colExchange.VisibleIndex = 1;
            this.colExchange.Width = 404;
            // 
            // colPrice
            // 
            this.colPrice.FieldName = "Price";
            this.colPrice.MinWidth = 40;
            this.colPrice.Name = "colPrice";
            this.colPrice.OptionsColumn.AllowEdit = false;
            this.colPrice.Visible = true;
            this.colPrice.VisibleIndex = 2;
            this.colPrice.Width = 410;
            // 
            // colDownloaded
            // 
            this.colDownloaded.ColumnEdit = this.repositoryItemCheckEdit2;
            this.colDownloaded.FieldName = "Downloaded";
            this.colDownloaded.MinWidth = 40;
            this.colDownloaded.Name = "colDownloaded";
            this.colDownloaded.OptionsColumn.AllowEdit = false;
            this.colDownloaded.Visible = true;
            this.colDownloaded.VisibleIndex = 3;
            this.colDownloaded.Width = 150;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // sbOkDownload
            // 
            this.sbOkDownload.AutoWidthInLayoutControl = true;
            this.sbOkDownload.Location = new System.Drawing.Point(873, 627);
            this.sbOkDownload.Margin = new System.Windows.Forms.Padding(6);
            this.sbOkDownload.Name = "sbOkDownload";
            this.sbOkDownload.Padding = new System.Windows.Forms.Padding(40, 0, 40, 0);
            this.sbOkDownload.Size = new System.Drawing.Size(125, 44);
            this.sbOkDownload.StyleController = this.layoutControl1;
            this.sbOkDownload.TabIndex = 4;
            this.sbOkDownload.Text = "OK";
            this.sbOkDownload.Click += new System.EventHandler(this.sbOkDownload_Click);
            // 
            // icbBase
            // 
            this.icbBase.Location = new System.Drawing.Point(176, 12);
            this.icbBase.Margin = new System.Windows.Forms.Padding(6);
            this.icbBase.MenuManager = this.ribbonControl1;
            this.icbBase.Name = "icbBase";
            this.icbBase.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icbBase.Size = new System.Drawing.Size(822, 40);
            this.icbBase.StyleController = this.layoutControl1;
            this.icbBase.TabIndex = 0;
            this.icbBase.EditValueChanged += new System.EventHandler(this.icbBase_EditValueChanged);
            // 
            // icbMarket
            // 
            this.icbMarket.Location = new System.Drawing.Point(176, 56);
            this.icbMarket.Margin = new System.Windows.Forms.Padding(6);
            this.icbMarket.MenuManager = this.ribbonControl1;
            this.icbMarket.Name = "icbMarket";
            this.icbMarket.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icbMarket.Size = new System.Drawing.Size(822, 40);
            this.icbMarket.StyleController = this.layoutControl1;
            this.icbMarket.TabIndex = 6;
            this.icbMarket.EditValueChanged += new System.EventHandler(this.icbMarket_EditValueChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1010, 683);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.icbBase;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(990, 44);
            this.layoutControlItem1.Text = "Base Currency:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(161, 25);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbOkDownload;
            this.layoutControlItem2.Location = new System.Drawing.Point(861, 615);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(129, 48);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 615);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(861, 48);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcDownExchanges;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 88);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(990, 527);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.icbMarket;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 44);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(990, 44);
            this.layoutControlItem4.Text = "Market Currency:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(161, 25);
            // 
            // biRemove
            // 
            this.biRemove.Caption = "Remove Selected";
            this.biRemove.Id = 2;
            this.biRemove.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biRemove.ImageOptions.SvgImage")));
            this.biRemove.Name = "biRemove";
            this.biRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // biClear
            // 
            this.biClear.Caption = "Clear";
            this.biClear.Id = 3;
            this.biClear.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biClear.ImageOptions.SvgImage")));
            this.biClear.Name = "biClear";
            this.biClear.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biClear_ItemClick);
            // 
            // ribbonPageCategory1
            // 
            this.ribbonPageCategory1.Name = "ribbonPageCategory1";
            this.ribbonPageCategory1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonPageCategory1.Text = "Analytics";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Stat Arbitrage Analytics";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.biAddSymbol);
            this.ribbonPageGroup1.ItemLinks.Add(this.biRemove);
            this.ribbonPageGroup1.ItemLinks.Add(this.biClear);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Symbols";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 281);
            this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(6);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.pcSymbols);
            this.splitContainerControl1.Panel1.Controls.Add(this.gcData);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(2288, 898);
            this.splitContainerControl1.SplitterPosition = 732;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gcData
            // 
            this.gcData.DataSource = typeof(ExchangeInfo);
            this.gcData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcData.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.gcData.Location = new System.Drawing.Point(0, 0);
            this.gcData.MainView = this.gwData;
            this.gcData.Margin = new System.Windows.Forms.Padding(6);
            this.gcData.MenuManager = this.ribbonControl1;
            this.gcData.Name = "gcData";
            this.gcData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit3,
            this.repositoryItemCheckEdit4});
            this.gcData.Size = new System.Drawing.Size(732, 898);
            this.gcData.TabIndex = 6;
            this.gcData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gwData});
            // 
            // gwData
            // 
            this.gwData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.colBase,
            this.colMarket});
            this.gwData.DetailHeight = 673;
            this.gwData.FixedLineWidth = 4;
            this.gwData.GridControl = this.gcData;
            this.gwData.GroupCount = 2;
            this.gwData.Name = "gwData";
            this.gwData.OptionsSelection.CheckBoxSelectorField = "Selected";
            this.gwData.OptionsSelection.MultiSelect = true;
            this.gwData.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.gwData.OptionsView.EnableAppearanceEvenRow = true;
            this.gwData.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gwData.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gwData.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colBase, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colMarket, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn1
            // 
            this.gridColumn1.ColumnEdit = this.repositoryItemCheckEdit3;
            this.gridColumn1.FieldName = "Selected";
            this.gridColumn1.MinWidth = 40;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 124;
            // 
            // repositoryItemCheckEdit3
            // 
            this.repositoryItemCheckEdit3.AutoHeight = false;
            this.repositoryItemCheckEdit3.Name = "repositoryItemCheckEdit3";
            // 
            // gridColumn2
            // 
            this.gridColumn2.FieldName = "Exchange";
            this.gridColumn2.MinWidth = 40;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 404;
            // 
            // gridColumn3
            // 
            this.gridColumn3.FieldName = "Price";
            this.gridColumn3.MinWidth = 40;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 410;
            // 
            // gridColumn4
            // 
            this.gridColumn4.ColumnEdit = this.repositoryItemCheckEdit4;
            this.gridColumn4.FieldName = "Downloaded";
            this.gridColumn4.MinWidth = 40;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 150;
            // 
            // repositoryItemCheckEdit4
            // 
            this.repositoryItemCheckEdit4.AutoHeight = false;
            this.repositoryItemCheckEdit4.Name = "repositoryItemCheckEdit4";
            // 
            // colBase
            // 
            this.colBase.FieldName = "Base";
            this.colBase.MinWidth = 40;
            this.colBase.Name = "colBase";
            this.colBase.Visible = true;
            this.colBase.VisibleIndex = 4;
            this.colBase.Width = 150;
            // 
            // colMarket
            // 
            this.colMarket.FieldName = "Market";
            this.colMarket.MinWidth = 40;
            this.colMarket.Name = "colMarket";
            this.colMarket.Visible = true;
            this.colMarket.VisibleIndex = 4;
            this.colMarket.Width = 150;
            // 
            // AnalyticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2288, 1179);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "AnalyticsForm";
            this.Ribbon = this.ribbonControl1;
            this.Text = "AnalyticsForm";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcSymbols)).EndInit();
            this.pcSymbols.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDownExchanges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDownExchanges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icbBase.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icbMarket.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gwData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraBars.BarButtonItem biAddSymbol;
        private DevExpress.XtraBars.BarButtonItem biRemove;
        private DevExpress.XtraBars.BarButtonItem biClear;
        private DevExpress.XtraBars.PopupControlContainer pcSymbols;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gcDownExchanges;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDownExchanges;
        private DevExpress.XtraEditors.SimpleButton sbOkDownload;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraBars.Ribbon.RibbonPageCategory ribbonPageCategory1;
        private DevExpress.XtraEditors.ComboBoxEdit icbBase;
        private DevExpress.XtraEditors.ComboBoxEdit icbMarket;
        private DevExpress.XtraGrid.Columns.GridColumn colSelected;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colExchange;
        private DevExpress.XtraGrid.Columns.GridColumn colPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colDownloaded;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.GridControl gcData;
        private DevExpress.XtraGrid.Views.Grid.GridView gwData;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit4;
        private DevExpress.XtraGrid.Columns.GridColumn colBase;
        private DevExpress.XtraGrid.Columns.GridColumn colMarket;
    }
}