namespace CryptoMarketClient {
    partial class AccounTradesCollectionForm {
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue2 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccounTradesCollectionForm));
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRateString = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.tradeHistoryItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmountString = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFill = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIdString = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGlobalId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTimeString = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTicker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.bsInfo = new DevExpress.XtraBars.BarStaticItem();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.biUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.biExpandTickers = new DevExpress.XtraBars.BarButtonItem();
            this.biCollapseTickers = new DevExpress.XtraBars.BarButtonItem();
            this.biExpandAccounts = new DevExpress.XtraBars.BarButtonItem();
            this.biCollapseAccounts = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tradeHistoryItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // colType
            // 
            this.colType.FieldName = "Type";
            this.colType.MinWidth = 40;
            this.colType.Name = "colType";
            this.colType.Width = 150;
            // 
            // colRateString
            // 
            this.colRateString.Caption = "Rate";
            this.colRateString.FieldName = "RateString";
            this.colRateString.MinWidth = 40;
            this.colRateString.Name = "colRateString";
            this.colRateString.Visible = true;
            this.colRateString.VisibleIndex = 1;
            this.colRateString.Width = 150;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.tradeHistoryItemBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(0);
            this.gridControl1.Location = new System.Drawing.Point(0, 72);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.gridControl1.Size = new System.Drawing.Size(1538, 1053);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // tradeHistoryItemBindingSource
            // 
            this.tradeHistoryItemBindingSource.DataSource = typeof(Crypto.Core.TradeInfoItem);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.GroupPanel.FontSizeDelta = 2;
            this.gridView1.Appearance.GroupPanel.Options.UseFont = true;
            this.gridView1.Appearance.GroupRow.FontSizeDelta = 2;
            this.gridView1.Appearance.GroupRow.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.FontSizeDelta = 2;
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.FontSizeDelta = 2;
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTime,
            this.colAmountString,
            this.colRateString,
            this.colTotal,
            this.colFee,
            this.colOrderNumber,
            this.colFill,
            this.colType,
            this.colId,
            this.colIdString,
            this.colGlobalId,
            this.colTimeString,
            this.colAccount,
            this.colTicker});
            this.gridView1.DetailHeight = 374;
            gridFormatRule1.Column = this.colType;
            gridFormatRule1.ColumnApplyTo = this.colRateString;
            gridFormatRule1.Name = "Buy";
            formatConditionRuleValue1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            formatConditionRuleValue1.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.Value1 = Crypto.Core.TradeType.Buy;
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.Column = this.colType;
            gridFormatRule2.ColumnApplyTo = this.colRateString;
            gridFormatRule2.Name = "Sell";
            formatConditionRuleValue2.Appearance.ForeColor = System.Drawing.Color.Red;
            formatConditionRuleValue2.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue2.Value1 = Crypto.Core.TradeType.Sell;
            gridFormatRule2.Rule = formatConditionRuleValue2;
            this.gridView1.FormatRules.Add(gridFormatRule1);
            this.gridView1.FormatRules.Add(gridFormatRule2);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupCount = 2;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colAccount, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colTicker, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colTime
            // 
            this.colTime.Caption = "Time";
            this.colTime.FieldName = "TimeString";
            this.colTime.MinWidth = 40;
            this.colTime.Name = "colTime";
            this.colTime.Visible = true;
            this.colTime.VisibleIndex = 0;
            this.colTime.Width = 150;
            // 
            // colAmountString
            // 
            this.colAmountString.Caption = "Amount";
            this.colAmountString.FieldName = "AmountString";
            this.colAmountString.MinWidth = 40;
            this.colAmountString.Name = "colAmountString";
            this.colAmountString.Visible = true;
            this.colAmountString.VisibleIndex = 2;
            this.colAmountString.Width = 150;
            // 
            // colTotal
            // 
            this.colTotal.FieldName = "Total";
            this.colTotal.MinWidth = 40;
            this.colTotal.Name = "colTotal";
            this.colTotal.Visible = true;
            this.colTotal.VisibleIndex = 3;
            this.colTotal.Width = 150;
            // 
            // colFee
            // 
            this.colFee.FieldName = "Fee";
            this.colFee.MinWidth = 40;
            this.colFee.Name = "colFee";
            this.colFee.Visible = true;
            this.colFee.VisibleIndex = 4;
            this.colFee.Width = 150;
            // 
            // colOrderNumber
            // 
            this.colOrderNumber.Caption = "Order Number";
            this.colOrderNumber.FieldName = "OrderId";
            this.colOrderNumber.MinWidth = 40;
            this.colOrderNumber.Name = "colOrderNumber";
            this.colOrderNumber.Visible = true;
            this.colOrderNumber.VisibleIndex = 5;
            this.colOrderNumber.Width = 150;
            // 
            // colFill
            // 
            this.colFill.FieldName = "Fill";
            this.colFill.MinWidth = 40;
            this.colFill.Name = "colFill";
            this.colFill.Visible = true;
            this.colFill.VisibleIndex = 6;
            this.colFill.Width = 150;
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.MinWidth = 40;
            this.colId.Name = "colId";
            this.colId.Width = 150;
            // 
            // colIdString
            // 
            this.colIdString.Caption = "Id";
            this.colIdString.FieldName = "IdString";
            this.colIdString.MinWidth = 40;
            this.colIdString.Name = "colIdString";
            this.colIdString.Width = 150;
            // 
            // colGlobalId
            // 
            this.colGlobalId.Caption = "Global Id";
            this.colGlobalId.FieldName = "GlobalId";
            this.colGlobalId.MinWidth = 40;
            this.colGlobalId.Name = "colGlobalId";
            this.colGlobalId.Width = 150;
            // 
            // colTimeString
            // 
            this.colTimeString.FieldName = "TimeString";
            this.colTimeString.MinWidth = 40;
            this.colTimeString.Name = "colTimeString";
            this.colTimeString.Width = 150;
            // 
            // colAccount
            // 
            this.colAccount.FieldName = "Account";
            this.colAccount.MinWidth = 40;
            this.colAccount.Name = "colAccount";
            this.colAccount.Visible = true;
            this.colAccount.VisibleIndex = 0;
            this.colAccount.Width = 150;
            // 
            // colTicker
            // 
            this.colTicker.FieldName = "Ticker";
            this.colTicker.MinWidth = 40;
            this.colTicker.Name = "colTicker";
            this.colTicker.Visible = true;
            this.colTicker.VisibleIndex = 0;
            this.colTicker.Width = 150;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            // 
            // barManager1
            // 
            this.barManager1.AllowHtmlText = true;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3,
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bsInfo,
            this.biUpdate,
            this.biExpandTickers,
            this.biCollapseTickers,
            this.biExpandAccounts,
            this.biCollapseAccounts});
            this.barManager1.MaxItemId = 6;
            this.barManager1.OptionsStubGlyphs.AllowStubGlyphs = DevExpress.Utils.DefaultBoolean.True;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bsInfo)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // bsInfo
            // 
            this.bsInfo.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.bsInfo.Id = 0;
            this.bsInfo.Name = "bsInfo";
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 3";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.biUpdate),
            new DevExpress.XtraBars.LinkPersistInfo(this.biExpandTickers),
            new DevExpress.XtraBars.LinkPersistInfo(this.biCollapseTickers),
            new DevExpress.XtraBars.LinkPersistInfo(this.biExpandAccounts),
            new DevExpress.XtraBars.LinkPersistInfo(this.biCollapseAccounts)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Custom 3";
            // 
            // biUpdate
            // 
            this.biUpdate.Caption = "Update";
            this.biUpdate.Id = 1;
            this.biUpdate.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biUpdate.ImageOptions.SvgImage")));
            this.biUpdate.ImageOptions.SvgImageSize = new System.Drawing.Size(22, 22);
            this.biUpdate.Name = "biUpdate";
            this.biUpdate.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biUpdate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biUpdate_ItemClick);
            // 
            // biExpandTickers
            // 
            this.biExpandTickers.Caption = "Expand Tickers";
            this.biExpandTickers.Id = 2;
            this.biExpandTickers.Name = "biExpandTickers";
            this.biExpandTickers.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biExpandTickers.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biExpandTickers_ItemClick);
            // 
            // biCollapseTickers
            // 
            this.biCollapseTickers.Caption = "Collapse Tickers";
            this.biCollapseTickers.Id = 3;
            this.biCollapseTickers.Name = "biCollapseTickers";
            this.biCollapseTickers.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biCollapseTickers.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biCollapseTickers_ItemClick);
            // 
            // biExpandAccounts
            // 
            this.biExpandAccounts.Caption = "Expand Accounts";
            this.biExpandAccounts.Id = 4;
            this.biExpandAccounts.Name = "biExpandAccounts";
            this.biExpandAccounts.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biExpandAccounts.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biExpandAccounts_ItemClick);
            // 
            // biCollapseAccounts
            // 
            this.biCollapseAccounts.Caption = "Collapse Accounts";
            this.biCollapseAccounts.Id = 5;
            this.biCollapseAccounts.Name = "biCollapseAccounts";
            this.biCollapseAccounts.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biCollapseAccounts.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biCollapseAccounts_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1538, 72);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 1125);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1538, 51);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 72);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 1053);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1538, 72);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 1053);
            // 
            // AccounTradesCollectionForm
            // 
            this.Appearance.FontSizeDelta = 2;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(38F, 83F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1538, 1176);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Font = new System.Drawing.Font("Tahoma", 25.875F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AccounTradesCollectionForm";
            this.Text = "Account Trades History";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tradeHistoryItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private System.Windows.Forms.BindingSource tradeHistoryItemBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colTime;
        private DevExpress.XtraGrid.Columns.GridColumn colAmountString;
        private DevExpress.XtraGrid.Columns.GridColumn colRateString;
        private DevExpress.XtraGrid.Columns.GridColumn colTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colFee;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colFill;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colIdString;
        private DevExpress.XtraGrid.Columns.GridColumn colGlobalId;
        private DevExpress.XtraGrid.Columns.GridColumn colTimeString;
        private DevExpress.XtraGrid.Columns.GridColumn colAccount;
        private DevExpress.XtraGrid.Columns.GridColumn colTicker;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarStaticItem bsInfo;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem biUpdate;
        private DevExpress.XtraBars.BarButtonItem biExpandTickers;
        private DevExpress.XtraBars.BarButtonItem biCollapseTickers;
        private DevExpress.XtraBars.BarButtonItem biExpandAccounts;
        private DevExpress.XtraBars.BarButtonItem biCollapseAccounts;
    }
}