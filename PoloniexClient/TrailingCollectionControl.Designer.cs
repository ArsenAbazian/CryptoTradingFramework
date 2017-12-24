using CryptoMarketClient.Common;

namespace CryptoMarketClient {
    partial class TrailingCollectionControl {
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue2 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule3 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue3 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule4 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue4 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrailingCollectionControl));
            this.colActualProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTrailings = new DevExpress.XtraGrid.GridControl();
            this.trailingSettingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvTrailings = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStopLossSellPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalSpendInBaseCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActualProfitUSD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUsdTicker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStopLossPricePercent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStopLossStartPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTakeProfitPercent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActualPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaxPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriceDelta = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTakeProfitPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btAdd = new DevExpress.XtraBars.BarButtonItem();
            this.btRemove = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.btEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcTrailings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trailingSettingsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTrailings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // colActualProfit
            // 
            this.colActualProfit.FieldName = "ActualProfit";
            this.colActualProfit.Name = "colActualProfit";
            this.colActualProfit.OptionsColumn.ReadOnly = true;
            this.colActualProfit.Visible = true;
            this.colActualProfit.VisibleIndex = 4;
            this.colActualProfit.Width = 67;
            // 
            // colType
            // 
            this.colType.FieldName = "Type";
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 0;
            this.colType.Width = 59;
            // 
            // gcTrailings
            // 
            this.gcTrailings.DataSource = this.trailingSettingsBindingSource;
            this.gcTrailings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTrailings.Location = new System.Drawing.Point(0, 31);
            this.gcTrailings.MainView = this.gvTrailings;
            this.gcTrailings.Name = "gcTrailings";
            this.gcTrailings.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.gcTrailings.Size = new System.Drawing.Size(618, 428);
            this.gcTrailings.TabIndex = 1;
            this.gcTrailings.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTrailings});
            // 
            // trailingSettingsBindingSource
            // 
            this.trailingSettingsBindingSource.DataSource = typeof(CryptoMarketClient.Common.TrailingSettings);
            // 
            // gvTrailings
            // 
            this.gvTrailings.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 7.875F);
            this.gvTrailings.Appearance.Row.Options.UseFont = true;
            this.gvTrailings.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gvTrailings.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colType,
            this.colName,
            this.colBuyPrice,
            this.colAmount,
            this.colStopLossSellPrice,
            this.colTotalSpendInBaseCurrency,
            this.colActualProfit,
            this.colActualProfitUSD,
            this.colUsdTicker,
            this.colStopLossPricePercent,
            this.colStopLossStartPrice,
            this.colTakeProfitPercent,
            this.colActualPrice,
            this.colMaxPrice,
            this.colPriceDelta,
            this.colTakeProfitPrice,
            this.gridColumn1});
            gridFormatRule1.Column = this.colActualProfit;
            gridFormatRule1.ColumnApplyTo = this.colActualProfit;
            gridFormatRule1.Name = "FormatRulesPositiveProfit";
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Greater;
            formatConditionRuleValue1.PredefinedName = "Green Bold Text";
            formatConditionRuleValue1.Value1 = new decimal(new int[] {
            0,
            0,
            0,
            0});
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.Name = "FormatRulesNegativeProfit";
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Less;
            formatConditionRuleValue2.PredefinedName = "Red Bold Text";
            formatConditionRuleValue2.Value1 = new decimal(new int[] {
            0,
            0,
            0,
            0});
            gridFormatRule2.Rule = formatConditionRuleValue2;
            gridFormatRule3.Column = this.colType;
            gridFormatRule3.ColumnApplyTo = this.colType;
            gridFormatRule3.Name = "SellRule";
            formatConditionRuleValue3.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue3.PredefinedName = "Red Bold Text";
            formatConditionRuleValue3.Value1 = CryptoMarketClient.Common.TrailingType.Sell;
            gridFormatRule3.Rule = formatConditionRuleValue3;
            gridFormatRule4.Column = this.colType;
            gridFormatRule4.ColumnApplyTo = this.colType;
            gridFormatRule4.Name = "BuyRule";
            formatConditionRuleValue4.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue4.PredefinedName = "Green Bold Text";
            formatConditionRuleValue4.Value1 = CryptoMarketClient.Common.TrailingType.Buy;
            gridFormatRule4.Rule = formatConditionRuleValue4;
            this.gvTrailings.FormatRules.Add(gridFormatRule1);
            this.gvTrailings.FormatRules.Add(gridFormatRule2);
            this.gvTrailings.FormatRules.Add(gridFormatRule3);
            this.gvTrailings.FormatRules.Add(gridFormatRule4);
            this.gvTrailings.GridControl = this.gcTrailings;
            this.gvTrailings.Name = "gvTrailings";
            this.gvTrailings.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gvTrailings.OptionsBehavior.Editable = false;
            this.gvTrailings.OptionsDetail.EnableMasterViewMode = false;
            this.gvTrailings.OptionsView.EnableAppearanceEvenRow = true;
            this.gvTrailings.OptionsView.ShowGroupPanel = false;
            this.gvTrailings.OptionsView.ShowIndicator = false;
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.ReadOnly = true;
            this.colName.Width = 217;
            // 
            // colBuyPrice
            // 
            this.colBuyPrice.Caption = "Buy Price";
            this.colBuyPrice.ColumnEdit = this.repositoryItemTextEdit1;
            this.colBuyPrice.FieldName = "BuyPrice";
            this.colBuyPrice.Name = "colBuyPrice";
            this.colBuyPrice.Visible = true;
            this.colBuyPrice.VisibleIndex = 1;
            this.colBuyPrice.Width = 79;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            // 
            // colAmount
            // 
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 3;
            this.colAmount.Width = 77;
            // 
            // colStopLossSellPrice
            // 
            this.colStopLossSellPrice.FieldName = "StopLossSellPrice";
            this.colStopLossSellPrice.Name = "colStopLossSellPrice";
            this.colStopLossSellPrice.Width = 287;
            // 
            // colTotalSpendInBaseCurrency
            // 
            this.colTotalSpendInBaseCurrency.FieldName = "TotalSpendInBaseCurrency";
            this.colTotalSpendInBaseCurrency.Name = "colTotalSpendInBaseCurrency";
            this.colTotalSpendInBaseCurrency.Width = 316;
            // 
            // colActualProfitUSD
            // 
            this.colActualProfitUSD.FieldName = "ActualProfitUSD";
            this.colActualProfitUSD.Name = "colActualProfitUSD";
            this.colActualProfitUSD.OptionsColumn.ReadOnly = true;
            this.colActualProfitUSD.Width = 207;
            // 
            // colUsdTicker
            // 
            this.colUsdTicker.FieldName = "UsdTicker";
            this.colUsdTicker.Name = "colUsdTicker";
            this.colUsdTicker.Width = 151;
            // 
            // colStopLossPricePercent
            // 
            this.colStopLossPricePercent.FieldName = "StopLossStartPrice";
            this.colStopLossPricePercent.Name = "colStopLossPricePercent";
            this.colStopLossPricePercent.Visible = true;
            this.colStopLossPricePercent.VisibleIndex = 5;
            this.colStopLossPricePercent.Width = 95;
            // 
            // colStopLossStartPrice
            // 
            this.colStopLossStartPrice.FieldName = "StopLossStartPrice";
            this.colStopLossStartPrice.Name = "colStopLossStartPrice";
            this.colStopLossStartPrice.OptionsColumn.ReadOnly = true;
            this.colStopLossStartPrice.Width = 101;
            // 
            // colTakeProfitPercent
            // 
            this.colTakeProfitPercent.FieldName = "TakeProfitPrice";
            this.colTakeProfitPercent.Name = "colTakeProfitPercent";
            this.colTakeProfitPercent.Visible = true;
            this.colTakeProfitPercent.VisibleIndex = 6;
            this.colTakeProfitPercent.Width = 73;
            // 
            // colActualPrice
            // 
            this.colActualPrice.FieldName = "ActualPrice";
            this.colActualPrice.Name = "colActualPrice";
            this.colActualPrice.Visible = true;
            this.colActualPrice.VisibleIndex = 2;
            this.colActualPrice.Width = 67;
            // 
            // colMaxPrice
            // 
            this.colMaxPrice.FieldName = "MaxPrice";
            this.colMaxPrice.Name = "colMaxPrice";
            this.colMaxPrice.Width = 136;
            // 
            // colPriceDelta
            // 
            this.colPriceDelta.FieldName = "PriceDelta";
            this.colPriceDelta.Name = "colPriceDelta";
            this.colPriceDelta.OptionsColumn.ReadOnly = true;
            this.colPriceDelta.Width = 116;
            // 
            // colTakeProfitPrice
            // 
            this.colTakeProfitPrice.FieldName = "TakeProfitPrice";
            this.colTakeProfitPrice.Name = "colTakeProfitPrice";
            this.colTakeProfitPrice.OptionsColumn.ReadOnly = true;
            this.colTakeProfitPrice.Width = 172;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btAdd,
            this.btRemove,
            this.btEdit,
            this.barButtonItem1});
            this.barManager1.MaxItemId = 4;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.btRemove),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.btEdit)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // btAdd
            // 
            this.btAdd.Caption = "Add";
            this.btAdd.Id = 0;
            this.btAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btAdd.ImageOptions.Image")));
            this.btAdd.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btAdd.ImageOptions.LargeImage")));
            this.btAdd.Name = "btAdd";
            this.btAdd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btAdd_ItemClick);
            // 
            // btRemove
            // 
            this.btRemove.Caption = "Remove";
            this.btRemove.Id = 1;
            this.btRemove.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btRemove.ImageOptions.Image")));
            this.btRemove.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btRemove.ImageOptions.LargeImage")));
            this.btRemove.Name = "btRemove";
            this.btRemove.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btRemove_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Remove All";
            this.barButtonItem1.Id = 3;
            this.barButtonItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.Image")));
            this.barButtonItem1.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.LargeImage")));
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // btEdit
            // 
            this.btEdit.Caption = "Edit";
            this.btEdit.Id = 2;
            this.btEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btEdit.ImageOptions.Image")));
            this.btEdit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btEdit.ImageOptions.LargeImage")));
            this.btEdit.Name = "btEdit";
            this.btEdit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlTop.Size = new System.Drawing.Size(618, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 459);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlBottom.Size = new System.Drawing.Size(618, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 428);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(618, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 428);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Name = "gridColumn1";
            // 
            // TrailingCollectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcTrailings);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TrailingCollectionControl";
            this.Size = new System.Drawing.Size(618, 459);
            ((System.ComponentModel.ISupportInitialize)(this.gcTrailings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trailingSettingsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTrailings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcTrailings;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTrailings;
        private System.Windows.Forms.BindingSource trailingSettingsBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colBuyPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colStopLossSellPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalSpendInBaseCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colActualProfit;
        private DevExpress.XtraGrid.Columns.GridColumn colActualProfitUSD;
        private DevExpress.XtraGrid.Columns.GridColumn colUsdTicker;
        private DevExpress.XtraGrid.Columns.GridColumn colStopLossPricePercent;
        private DevExpress.XtraGrid.Columns.GridColumn colStopLossStartPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colTakeProfitPercent;
        private DevExpress.XtraGrid.Columns.GridColumn colActualPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colMaxPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colPriceDelta;
        private DevExpress.XtraGrid.Columns.GridColumn colTakeProfitPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btAdd;
        private DevExpress.XtraBars.BarButtonItem btRemove;
        private DevExpress.XtraBars.BarButtonItem btEdit;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}
