namespace CryptoMarketClient {
    partial class StaticArbitrageForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaticArbitrageForm));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule4 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue4 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule5 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue5 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule6 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue6 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            this.colIsActual = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAltCoin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbMonitorSelected = new DevExpress.XtraBars.BarCheckItem();
            this.bbShowHistory = new DevExpress.XtraBars.BarButtonItem();
            this.bbClearSelected = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.staticArbitrageInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAltBase = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBaseUsdt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAltUsdt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDisbalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDirection = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAltBaseIndex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAltUsdtIndex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBaseUsdtIndex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExchange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBaseCoin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsUpdating = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colObtainingData = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAltBasePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAtlUsdtPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBaseUsdtPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNextOverdueMs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStartUpdateMs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLastUpdate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateTimeMs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colObtainDataSuccessCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colObtainDataCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalSpent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLastEarned = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bbShowLog = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staticArbitrageInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // colIsActual
            // 
            this.colIsActual.FieldName = "IsActual";
            this.colIsActual.Name = "colIsActual";
            // 
            // colAltCoin
            // 
            this.colAltCoin.FieldName = "AltCoin";
            this.colAltCoin.Name = "colAltCoin";
            this.colAltCoin.OptionsColumn.AllowEdit = false;
            this.colAltCoin.OptionsColumn.ReadOnly = true;
            this.colAltCoin.Visible = true;
            this.colAltCoin.VisibleIndex = 2;
            // 
            // colProfit
            // 
            this.colProfit.Caption = "Profit";
            this.colProfit.DisplayFormat.FormatString = "0.00000000";
            this.colProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colProfit.FieldName = "Profit";
            this.colProfit.Name = "colProfit";
            this.colProfit.OptionsColumn.AllowEdit = false;
            this.colProfit.Visible = true;
            this.colProfit.VisibleIndex = 6;
            // 
            // colIsSelected
            // 
            this.colIsSelected.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsSelected.FieldName = "IsSelected";
            this.colIsSelected.Name = "colIsSelected";
            this.colIsSelected.Visible = true;
            this.colIsSelected.VisibleIndex = 0;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.EditValueChanged += new System.EventHandler(this.repositoryItemCheckEdit1_EditValueChanged);
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.bbMonitorSelected,
            this.bbShowHistory,
            this.bbClearSelected,
            this.bbShowLog});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Margin = new System.Windows.Forms.Padding(4);
            this.ribbonControl1.MaxItemId = 6;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.Size = new System.Drawing.Size(2104, 278);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // bbMonitorSelected
            // 
            this.bbMonitorSelected.Caption = "Monitor Only Selected";
            this.bbMonitorSelected.Id = 2;
            this.bbMonitorSelected.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbMonitorSelected.ImageOptions.Image")));
            this.bbMonitorSelected.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbMonitorSelected.ImageOptions.LargeImage")));
            this.bbMonitorSelected.Name = "bbMonitorSelected";
            // 
            // bbShowHistory
            // 
            this.bbShowHistory.Caption = "ShowHistory";
            this.bbShowHistory.Id = 3;
            this.bbShowHistory.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbShowHistory.ImageOptions.Image")));
            this.bbShowHistory.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbShowHistory.ImageOptions.LargeImage")));
            this.bbShowHistory.Name = "bbShowHistory";
            this.bbShowHistory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbShowHistory_ItemClick);
            // 
            // bbClearSelected
            // 
            this.bbClearSelected.Caption = "Disable Operations!!!";
            this.bbClearSelected.Id = 4;
            this.bbClearSelected.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbClearSelected.ImageOptions.Image")));
            this.bbClearSelected.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbClearSelected.ImageOptions.LargeImage")));
            this.bbClearSelected.ItemAppearance.Normal.ForeColor = System.Drawing.Color.Red;
            this.bbClearSelected.ItemAppearance.Normal.Options.UseForeColor = true;
            this.bbClearSelected.Name = "bbClearSelected";
            this.bbClearSelected.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbClearSelected_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Connect";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.bbMonitorSelected);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbShowHistory);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbClearSelected);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbShowLog);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Static Arbitrage";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 879);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(2104, 54);
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.staticArbitrageInfoBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gridControl1.Location = new System.Drawing.Point(0, 278);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4);
            this.gridControl1.MenuManager = this.ribbonControl1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gridControl1.Size = new System.Drawing.Size(2104, 655);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Click += new System.EventHandler(this.gridControl1_Click);
            // 
            // staticArbitrageInfoBindingSource
            // 
            this.staticArbitrageInfoBindingSource.DataSource = typeof(Crypto.Core.Common.TriplePairArbitrageInfo);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAltBase,
            this.colBaseUsdt,
            this.colAltUsdt,
            this.colDisbalance,
            this.colDirection,
            this.colAltBaseIndex,
            this.colAltUsdtIndex,
            this.colBaseUsdtIndex,
            this.colIsSelected,
            this.colExchange,
            this.colAltCoin,
            this.colBaseCoin,
            this.colIsUpdating,
            this.colObtainingData,
            this.colAltBasePrice,
            this.colAtlUsdtPrice,
            this.colBaseUsdtPrice,
            this.colNextOverdueMs,
            this.colStartUpdateMs,
            this.colIsActual,
            this.colLastUpdate,
            this.colUpdateTimeMs,
            this.colObtainDataSuccessCount,
            this.colObtainDataCount,
            this.colCount,
            this.colAmount,
            this.colProfit,
            this.colFee,
            this.gridColumn1,
            this.colTotalSpent,
            this.colLastEarned});
            gridFormatRule4.Column = this.colIsActual;
            gridFormatRule4.ColumnApplyTo = this.colAltCoin;
            gridFormatRule4.Name = "FormatIsActual";
            formatConditionRuleValue4.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            formatConditionRuleValue4.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue4.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue4.Value1 = false;
            gridFormatRule4.Rule = formatConditionRuleValue4;
            gridFormatRule5.Column = this.colProfit;
            gridFormatRule5.ColumnApplyTo = this.colProfit;
            gridFormatRule5.Name = "FormatProfit";
            formatConditionRuleValue5.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            formatConditionRuleValue5.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue5.Condition = DevExpress.XtraEditors.FormatCondition.Greater;
            formatConditionRuleValue5.Value1 = new decimal(new int[] {
            0,
            0,
            0,
            0});
            gridFormatRule5.Rule = formatConditionRuleValue5;
            gridFormatRule6.Column = this.colIsSelected;
            gridFormatRule6.ColumnApplyTo = this.colIsSelected;
            gridFormatRule6.Name = "FormatIsSelected";
            formatConditionRuleValue6.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            formatConditionRuleValue6.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue6.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue6.Value1 = true;
            gridFormatRule6.Rule = formatConditionRuleValue6;
            this.gridView1.FormatRules.Add(gridFormatRule4);
            this.gridView1.FormatRules.Add(gridFormatRule5);
            this.gridView1.FormatRules.Add(gridFormatRule6);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            // 
            // colAltBase
            // 
            this.colAltBase.FieldName = "AltBase";
            this.colAltBase.Name = "colAltBase";
            // 
            // colBaseUsdt
            // 
            this.colBaseUsdt.FieldName = "BaseUsdt";
            this.colBaseUsdt.Name = "colBaseUsdt";
            // 
            // colAltUsdt
            // 
            this.colAltUsdt.FieldName = "AltUsdt";
            this.colAltUsdt.Name = "colAltUsdt";
            // 
            // colDisbalance
            // 
            this.colDisbalance.DisplayFormat.FormatString = "0.00000000";
            this.colDisbalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDisbalance.FieldName = "Disbalance";
            this.colDisbalance.Name = "colDisbalance";
            this.colDisbalance.OptionsColumn.AllowEdit = false;
            this.colDisbalance.OptionsColumn.ReadOnly = true;
            this.colDisbalance.Visible = true;
            this.colDisbalance.VisibleIndex = 4;
            // 
            // colDirection
            // 
            this.colDirection.FieldName = "Direction";
            this.colDirection.Name = "colDirection";
            this.colDirection.OptionsColumn.AllowEdit = false;
            this.colDirection.OptionsColumn.ReadOnly = true;
            this.colDirection.Visible = true;
            this.colDirection.VisibleIndex = 8;
            // 
            // colAltBaseIndex
            // 
            this.colAltBaseIndex.FieldName = "AltBaseIndex";
            this.colAltBaseIndex.Name = "colAltBaseIndex";
            this.colAltBaseIndex.OptionsColumn.ReadOnly = true;
            // 
            // colAltUsdtIndex
            // 
            this.colAltUsdtIndex.FieldName = "AltUsdtIndex";
            this.colAltUsdtIndex.Name = "colAltUsdtIndex";
            this.colAltUsdtIndex.OptionsColumn.ReadOnly = true;
            // 
            // colBaseUsdtIndex
            // 
            this.colBaseUsdtIndex.FieldName = "BaseUsdtIndex";
            this.colBaseUsdtIndex.Name = "colBaseUsdtIndex";
            this.colBaseUsdtIndex.OptionsColumn.ReadOnly = true;
            // 
            // colExchange
            // 
            this.colExchange.FieldName = "Exchange";
            this.colExchange.Name = "colExchange";
            this.colExchange.OptionsColumn.AllowEdit = false;
            this.colExchange.OptionsColumn.ReadOnly = true;
            this.colExchange.Visible = true;
            this.colExchange.VisibleIndex = 1;
            // 
            // colBaseCoin
            // 
            this.colBaseCoin.FieldName = "BaseCoin";
            this.colBaseCoin.Name = "colBaseCoin";
            this.colBaseCoin.OptionsColumn.AllowEdit = false;
            this.colBaseCoin.OptionsColumn.ReadOnly = true;
            this.colBaseCoin.Visible = true;
            this.colBaseCoin.VisibleIndex = 3;
            // 
            // colIsUpdating
            // 
            this.colIsUpdating.FieldName = "IsUpdating";
            this.colIsUpdating.Name = "colIsUpdating";
            // 
            // colObtainingData
            // 
            this.colObtainingData.FieldName = "ObtainingData";
            this.colObtainingData.Name = "colObtainingData";
            // 
            // colAltBasePrice
            // 
            this.colAltBasePrice.DisplayFormat.FormatString = "0.00000000";
            this.colAltBasePrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAltBasePrice.FieldName = "AltBasePrice";
            this.colAltBasePrice.Name = "colAltBasePrice";
            this.colAltBasePrice.OptionsColumn.AllowEdit = false;
            this.colAltBasePrice.OptionsColumn.ReadOnly = true;
            this.colAltBasePrice.Visible = true;
            this.colAltBasePrice.VisibleIndex = 10;
            // 
            // colAtlUsdtPrice
            // 
            this.colAtlUsdtPrice.DisplayFormat.FormatString = "0.00000000";
            this.colAtlUsdtPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAtlUsdtPrice.FieldName = "AltUsdtPrice";
            this.colAtlUsdtPrice.Name = "colAtlUsdtPrice";
            this.colAtlUsdtPrice.OptionsColumn.AllowEdit = false;
            this.colAtlUsdtPrice.OptionsColumn.ReadOnly = true;
            this.colAtlUsdtPrice.Visible = true;
            this.colAtlUsdtPrice.VisibleIndex = 9;
            // 
            // colBaseUsdtPrice
            // 
            this.colBaseUsdtPrice.DisplayFormat.FormatString = "0.00000000";
            this.colBaseUsdtPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBaseUsdtPrice.FieldName = "BaseUsdtPrice";
            this.colBaseUsdtPrice.Name = "colBaseUsdtPrice";
            this.colBaseUsdtPrice.OptionsColumn.AllowEdit = false;
            this.colBaseUsdtPrice.OptionsColumn.ReadOnly = true;
            this.colBaseUsdtPrice.Visible = true;
            this.colBaseUsdtPrice.VisibleIndex = 11;
            // 
            // colNextOverdueMs
            // 
            this.colNextOverdueMs.FieldName = "NextOverdueMs";
            this.colNextOverdueMs.Name = "colNextOverdueMs";
            // 
            // colStartUpdateMs
            // 
            this.colStartUpdateMs.FieldName = "StartUpdateMs";
            this.colStartUpdateMs.Name = "colStartUpdateMs";
            // 
            // colLastUpdate
            // 
            this.colLastUpdate.DisplayFormat.FormatString = "hh:mm:ss.fff";
            this.colLastUpdate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colLastUpdate.FieldName = "LastUpdate";
            this.colLastUpdate.Name = "colLastUpdate";
            this.colLastUpdate.OptionsColumn.AllowEdit = false;
            this.colLastUpdate.Visible = true;
            this.colLastUpdate.VisibleIndex = 12;
            // 
            // colUpdateTimeMs
            // 
            this.colUpdateTimeMs.FieldName = "UpdateTimeMs";
            this.colUpdateTimeMs.Name = "colUpdateTimeMs";
            this.colUpdateTimeMs.OptionsColumn.AllowEdit = false;
            this.colUpdateTimeMs.Visible = true;
            this.colUpdateTimeMs.VisibleIndex = 13;
            // 
            // colObtainDataSuccessCount
            // 
            this.colObtainDataSuccessCount.FieldName = "ObtainDataSuccessCount";
            this.colObtainDataSuccessCount.Name = "colObtainDataSuccessCount";
            // 
            // colObtainDataCount
            // 
            this.colObtainDataCount.FieldName = "ObtainDataCount";
            this.colObtainDataCount.Name = "colObtainDataCount";
            // 
            // colCount
            // 
            this.colCount.FieldName = "Count";
            this.colCount.Name = "colCount";
            this.colCount.OptionsColumn.ReadOnly = true;
            // 
            // colAmount
            // 
            this.colAmount.Caption = "Amount";
            this.colAmount.DisplayFormat.FormatString = "0.00000000";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 5;
            // 
            // colFee
            // 
            this.colFee.Caption = "Fee";
            this.colFee.DisplayFormat.FormatString = "0.00000000";
            this.colFee.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colFee.FieldName = "Fee";
            this.colFee.Name = "colFee";
            this.colFee.Visible = true;
            this.colFee.VisibleIndex = 14;
            // 
            // gridColumn1
            // 
            this.gridColumn1.DisplayFormat.FormatString = "0.00000000";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn1.FieldName = "MaxProfit";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 15;
            // 
            // colTotalSpent
            // 
            this.colTotalSpent.Caption = "Total Spent";
            this.colTotalSpent.DisplayFormat.FormatString = "0.00000000";
            this.colTotalSpent.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalSpent.FieldName = "TotalSpent";
            this.colTotalSpent.Name = "colTotalSpent";
            this.colTotalSpent.OptionsColumn.AllowEdit = false;
            this.colTotalSpent.Visible = true;
            this.colTotalSpent.VisibleIndex = 16;
            // 
            // colLastEarned
            // 
            this.colLastEarned.Caption = "Last Earned";
            this.colLastEarned.DisplayFormat.FormatString = "0.00000000";
            this.colLastEarned.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colLastEarned.FieldName = "LastEarned";
            this.colLastEarned.Name = "colLastEarned";
            this.colLastEarned.OptionsColumn.AllowEdit = false;
            this.colLastEarned.Visible = true;
            this.colLastEarned.VisibleIndex = 7;
            // 
            // bbShowLog
            // 
            this.bbShowLog.Caption = "Show Log";
            this.bbShowLog.Id = 5;
            this.bbShowLog.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbShowLog.ImageOptions.Image")));
            this.bbShowLog.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbShowLog.ImageOptions.LargeImage")));
            this.bbShowLog.Name = "bbShowLog";
            this.bbShowLog.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbShowLog_ItemClick);
            // 
            // StaticArbitrageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2104, 933);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "StaticArbitrageForm";
            this.Text = "StaticArbitrageForm";
            this.Load += new System.EventHandler(this.StaticArbitrageForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staticArbitrageInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource staticArbitrageInfoBindingSource;
        private DevExpress.XtraBars.BarCheckItem bbMonitorSelected;
        private DevExpress.XtraGrid.Columns.GridColumn colAltBase;
        private DevExpress.XtraGrid.Columns.GridColumn colBaseUsdt;
        private DevExpress.XtraGrid.Columns.GridColumn colAltUsdt;
        private DevExpress.XtraGrid.Columns.GridColumn colDisbalance;
        private DevExpress.XtraGrid.Columns.GridColumn colDirection;
        private DevExpress.XtraGrid.Columns.GridColumn colAltBaseIndex;
        private DevExpress.XtraGrid.Columns.GridColumn colAltUsdtIndex;
        private DevExpress.XtraGrid.Columns.GridColumn colBaseUsdtIndex;
        private DevExpress.XtraGrid.Columns.GridColumn colIsSelected;
        private DevExpress.XtraGrid.Columns.GridColumn colExchange;
        private DevExpress.XtraGrid.Columns.GridColumn colAltCoin;
        private DevExpress.XtraGrid.Columns.GridColumn colBaseCoin;
        private DevExpress.XtraGrid.Columns.GridColumn colIsUpdating;
        private DevExpress.XtraGrid.Columns.GridColumn colObtainingData;
        private DevExpress.XtraGrid.Columns.GridColumn colAltBasePrice;
        private DevExpress.XtraGrid.Columns.GridColumn colAtlUsdtPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colBaseUsdtPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colNextOverdueMs;
        private DevExpress.XtraGrid.Columns.GridColumn colStartUpdateMs;
        private DevExpress.XtraGrid.Columns.GridColumn colIsActual;
        private DevExpress.XtraGrid.Columns.GridColumn colLastUpdate;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateTimeMs;
        private DevExpress.XtraGrid.Columns.GridColumn colObtainDataSuccessCount;
        private DevExpress.XtraGrid.Columns.GridColumn colObtainDataCount;
        private DevExpress.XtraGrid.Columns.GridColumn colCount;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colProfit;
        private DevExpress.XtraGrid.Columns.GridColumn colFee;
        private DevExpress.XtraBars.BarButtonItem bbShowHistory;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalSpent;
        private DevExpress.XtraBars.BarButtonItem bbClearSelected;
        private DevExpress.XtraGrid.Columns.GridColumn colLastEarned;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraBars.BarButtonItem bbShowLog;
    }
}