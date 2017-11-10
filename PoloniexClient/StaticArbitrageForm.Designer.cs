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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue2 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            this.colIsActual = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbMonitorSelected = new DevExpress.XtraBars.BarCheckItem();
            this.bbShowHistory = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
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
            this.colIsSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExchange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAltCoin = new DevExpress.XtraGrid.Columns.GridColumn();
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
            // colProfit
            // 
            this.colProfit.Caption = "Profit";
            this.colProfit.DisplayFormat.FormatString = "0.########";
            this.colProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colProfit.FieldName = "Profit";
            this.colProfit.Name = "colProfit";
            this.colProfit.OptionsColumn.AllowEdit = false;
            this.colProfit.Visible = true;
            this.colProfit.VisibleIndex = 6;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.bbMonitorSelected,
            this.bbShowHistory});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Margin = new System.Windows.Forms.Padding(2);
            this.ribbonControl1.MaxItemId = 4;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.Size = new System.Drawing.Size(1052, 141);
            // 
            // bbMonitorSelected
            // 
            this.bbMonitorSelected.Caption = "Monitor Only Selected";
            this.bbMonitorSelected.Id = 2;
            this.bbMonitorSelected.Name = "bbMonitorSelected";
            // 
            // bbShowHistory
            // 
            this.bbShowHistory.Caption = "ShowHistory";
            this.bbShowHistory.Id = 3;
            this.bbShowHistory.Name = "bbShowHistory";
            this.bbShowHistory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbShowHistory_ItemClick);
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
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Static Arbitrage";
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.staticArbitrageInfoBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl1.Location = new System.Drawing.Point(0, 141);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl1.MenuManager = this.ribbonControl1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1052, 344);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // staticArbitrageInfoBindingSource
            // 
            this.staticArbitrageInfoBindingSource.DataSource = typeof(CryptoMarketClient.Common.StaticArbitrageInfo);
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
            this.gridColumn1});
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Column = this.colIsActual;
            gridFormatRule1.Name = "FormatIsActual";
            formatConditionRuleValue1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            formatConditionRuleValue1.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.Value1 = false;
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.ApplyToRow = true;
            gridFormatRule2.Column = this.colProfit;
            gridFormatRule2.Name = "Profit";
            formatConditionRuleValue2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            formatConditionRuleValue2.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Greater;
            formatConditionRuleValue2.Value1 = new decimal(new int[] {
            0,
            0,
            0,
            0});
            gridFormatRule2.Rule = formatConditionRuleValue2;
            this.gridView1.FormatRules.Add(gridFormatRule1);
            this.gridView1.FormatRules.Add(gridFormatRule2);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
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
            this.colDisbalance.DisplayFormat.FormatString = "0.########";
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
            this.colDirection.VisibleIndex = 7;
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
            // colIsSelected
            // 
            this.colIsSelected.FieldName = "IsSelected";
            this.colIsSelected.Name = "colIsSelected";
            this.colIsSelected.Visible = true;
            this.colIsSelected.VisibleIndex = 0;
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
            // colAltCoin
            // 
            this.colAltCoin.FieldName = "AltCoin";
            this.colAltCoin.Name = "colAltCoin";
            this.colAltCoin.OptionsColumn.AllowEdit = false;
            this.colAltCoin.OptionsColumn.ReadOnly = true;
            this.colAltCoin.Visible = true;
            this.colAltCoin.VisibleIndex = 2;
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
            this.colAltBasePrice.DisplayFormat.FormatString = "0.########";
            this.colAltBasePrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAltBasePrice.FieldName = "AltBasePrice";
            this.colAltBasePrice.Name = "colAltBasePrice";
            this.colAltBasePrice.OptionsColumn.AllowEdit = false;
            this.colAltBasePrice.OptionsColumn.ReadOnly = true;
            this.colAltBasePrice.Visible = true;
            this.colAltBasePrice.VisibleIndex = 9;
            // 
            // colAtlUsdtPrice
            // 
            this.colAtlUsdtPrice.DisplayFormat.FormatString = "0.########";
            this.colAtlUsdtPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAtlUsdtPrice.FieldName = "AtlUsdtPrice";
            this.colAtlUsdtPrice.Name = "colAtlUsdtPrice";
            this.colAtlUsdtPrice.OptionsColumn.AllowEdit = false;
            this.colAtlUsdtPrice.OptionsColumn.ReadOnly = true;
            this.colAtlUsdtPrice.Visible = true;
            this.colAtlUsdtPrice.VisibleIndex = 8;
            // 
            // colBaseUsdtPrice
            // 
            this.colBaseUsdtPrice.DisplayFormat.FormatString = "0.########";
            this.colBaseUsdtPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBaseUsdtPrice.FieldName = "BaseUsdtPrice";
            this.colBaseUsdtPrice.Name = "colBaseUsdtPrice";
            this.colBaseUsdtPrice.OptionsColumn.AllowEdit = false;
            this.colBaseUsdtPrice.OptionsColumn.ReadOnly = true;
            this.colBaseUsdtPrice.Visible = true;
            this.colBaseUsdtPrice.VisibleIndex = 10;
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
            this.colLastUpdate.VisibleIndex = 11;
            // 
            // colUpdateTimeMs
            // 
            this.colUpdateTimeMs.FieldName = "UpdateTimeMs";
            this.colUpdateTimeMs.Name = "colUpdateTimeMs";
            this.colUpdateTimeMs.OptionsColumn.AllowEdit = false;
            this.colUpdateTimeMs.Visible = true;
            this.colUpdateTimeMs.VisibleIndex = 12;
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
            this.colAmount.DisplayFormat.FormatString = "0.########";
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
            this.colFee.DisplayFormat.FormatString = "0.########";
            this.colFee.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colFee.FieldName = "Fee";
            this.colFee.Name = "colFee";
            this.colFee.Visible = true;
            this.colFee.VisibleIndex = 13;
            // 
            // gridColumn1
            // 
            this.gridColumn1.DisplayFormat.FormatString = "0.########";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn1.FieldName = "MaxProfit";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 14;
            // 
            // StaticArbitrageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 485);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "StaticArbitrageForm";
            this.Text = "StaticArbitrageForm";
            this.Load += new System.EventHandler(this.StaticArbitrageForm_Load);
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
    }
}