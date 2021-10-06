namespace CryptoMarketClient.Poloniex {
    partial class OpenedOrdersForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenedOrdersForm));
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateString = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.openedOrdersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colOrderNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValueString = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmountString = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalString = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTicker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccount = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.colCompletedString = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.openedOrdersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // colType
            // 
            this.colType.FieldName = "Type";
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 1;
            this.colType.Width = 150;
            // 
            // colDateString
            // 
            this.colDateString.Caption = "Date";
            this.colDateString.FieldName = "DateString";
            this.colDateString.MinWidth = 40;
            this.colDateString.Name = "colDateString";
            this.colDateString.Visible = true;
            this.colDateString.VisibleIndex = 0;
            this.colDateString.Width = 150;
            // 
            // colValue
            // 
            this.colValue.FieldName = "Value";
            this.colValue.Name = "colValue";
            this.colValue.OptionsColumn.ReadOnly = true;
            this.colValue.Width = 150;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.openedOrdersBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gridControl1.Location = new System.Drawing.Point(0, 58);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.gridControl1.Size = new System.Drawing.Size(1686, 844);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // openedOrdersBindingSource
            // 
            this.openedOrdersBindingSource.DataSource = typeof(Crypto.Core.Common.OpenedOrderInfo);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.FontSizeDelta = 2;
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.FontSizeDelta = 2;
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colOrderNumber,
            this.colType,
            this.colValueString,
            this.colAmountString,
            this.colTotalString,
            this.colValue,
            this.colAmount,
            this.colTotal,
            this.colTicker,
            this.colDateString,
            this.colAccount,
            this.colCompletedString});
            this.gridView1.DetailHeight = 673;
            gridFormatRule1.Column = this.colType;
            gridFormatRule1.ColumnApplyTo = this.colDateString;
            gridFormatRule1.Name = "BuyOrderType";
            formatConditionRuleValue1.Appearance.FontSizeDelta = 2;
            formatConditionRuleValue1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            formatConditionRuleValue1.Appearance.Options.UseFont = true;
            formatConditionRuleValue1.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.Value1 = Crypto.Core.Common.OrderType.Buy;
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.Column = this.colType;
            gridFormatRule2.ColumnApplyTo = this.colDateString;
            gridFormatRule2.Name = "SellOrderType";
            formatConditionRuleValue2.Appearance.FontSizeDelta = 2;
            formatConditionRuleValue2.Appearance.ForeColor = System.Drawing.Color.Red;
            formatConditionRuleValue2.Appearance.Options.UseFont = true;
            formatConditionRuleValue2.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue2.Value1 = Crypto.Core.Common.OrderType.Sell;
            gridFormatRule2.Rule = formatConditionRuleValue2;
            this.gridView1.FormatRules.Add(gridFormatRule1);
            this.gridView1.FormatRules.Add(gridFormatRule2);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupCount = 2;
            this.gridView1.LevelIndent = 0;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.PreviewIndent = 0;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colAccount, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colTicker, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colOrderNumber
            // 
            this.colOrderNumber.FieldName = "OrderId";
            this.colOrderNumber.Name = "colOrderNumber";
            this.colOrderNumber.Visible = true;
            this.colOrderNumber.VisibleIndex = 6;
            this.colOrderNumber.Width = 150;
            // 
            // colValueString
            // 
            this.colValueString.Caption = "Price";
            this.colValueString.FieldName = "ValueString";
            this.colValueString.Name = "colValueString";
            this.colValueString.Visible = true;
            this.colValueString.VisibleIndex = 2;
            this.colValueString.Width = 150;
            // 
            // colAmountString
            // 
            this.colAmountString.Caption = "Amount";
            this.colAmountString.FieldName = "AmountString";
            this.colAmountString.Name = "colAmountString";
            this.colAmountString.Visible = true;
            this.colAmountString.VisibleIndex = 3;
            this.colAmountString.Width = 150;
            // 
            // colTotalString
            // 
            this.colTotalString.Caption = "Total";
            this.colTotalString.FieldName = "TotalString";
            this.colTotalString.Name = "colTotalString";
            this.colTotalString.Visible = true;
            this.colTotalString.VisibleIndex = 5;
            this.colTotalString.Width = 150;
            // 
            // colAmount
            // 
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.ReadOnly = true;
            this.colAmount.Width = 150;
            // 
            // colTotal
            // 
            this.colTotal.FieldName = "Total";
            this.colTotal.Name = "colTotal";
            this.colTotal.OptionsColumn.ReadOnly = true;
            this.colTotal.Width = 150;
            // 
            // colTicker
            // 
            this.colTicker.FieldName = "TickerName";
            this.colTicker.MinWidth = 40;
            this.colTicker.Name = "colTicker";
            this.colTicker.Visible = true;
            this.colTicker.VisibleIndex = 6;
            this.colTicker.Width = 150;
            // 
            // colAccount
            // 
            this.colAccount.FieldName = "Account";
            this.colAccount.MinWidth = 40;
            this.colAccount.Name = "colAccount";
            this.colAccount.Visible = true;
            this.colAccount.VisibleIndex = 6;
            this.colAccount.Width = 150;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
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
            this.biUpdate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.barDockControlTop.Size = new System.Drawing.Size(1686, 58);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 902);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.barDockControlBottom.Size = new System.Drawing.Size(1686, 42);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 58);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 844);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1686, 58);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 844);
            // 
            // colCompletedString
            // 
            this.colCompletedString.Caption = "Executed";
            this.colCompletedString.FieldName = "CompletedString";
            this.colCompletedString.MinWidth = 40;
            this.colCompletedString.Name = "colCompletedString";
            this.colCompletedString.Visible = true;
            this.colCompletedString.VisibleIndex = 4;
            this.colCompletedString.Width = 150;
            // 
            // OpenedOrdersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1686, 944);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "OpenedOrdersForm";
            this.Text = "Poloniex Opened Orders";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.openedOrdersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource openedOrdersBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colValueString;
        private DevExpress.XtraGrid.Columns.GridColumn colAmountString;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalString;
        private DevExpress.XtraGrid.Columns.GridColumn colValue;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colTotal;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colTicker;
        private DevExpress.XtraGrid.Columns.GridColumn colDateString;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarStaticItem bsInfo;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem biUpdate;
        private DevExpress.XtraBars.BarButtonItem biExpandTickers;
        private DevExpress.XtraBars.BarButtonItem biCollapseTickers;
        private DevExpress.XtraBars.BarButtonItem biExpandAccounts;
        private DevExpress.XtraBars.BarButtonItem biCollapseAccounts;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.Columns.GridColumn colAccount;
        private DevExpress.XtraGrid.Columns.GridColumn colCompletedString;
    }
}