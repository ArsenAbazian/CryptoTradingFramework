namespace CryptoMarketClient {
    partial class StaticArbitrageHistoryForm {
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
            DevExpress.XtraEditors.FormatConditionRuleDataBar formatConditionRuleDataBar1 = new DevExpress.XtraEditors.FormatConditionRuleDataBar();
            this.colProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.staticArbitrageInfoHistoryItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDisbalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDirection = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAltBasePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAtlUsdtPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBaseUsdtPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.colEarned = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staticArbitrageInfoHistoryItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // colProfit
            // 
            this.colProfit.DisplayFormat.FormatString = "0.00000000";
            this.colProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colProfit.FieldName = "Profit";
            this.colProfit.Name = "colProfit";
            this.colProfit.Visible = true;
            this.colProfit.VisibleIndex = 7;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Margin = new System.Windows.Forms.Padding(4);
            this.ribbonControl1.MaxItemId = 4;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.Size = new System.Drawing.Size(1782, 278);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
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
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Static Arbitrage";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.AutoHeight = true;
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 952);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(1782, 54);
            // 
            // staticArbitrageInfoHistoryItemBindingSource
            // 
            this.staticArbitrageInfoHistoryItemBindingSource.DataSource = typeof(Crypto.Core.Common.TriplePairInfoHistoryItem);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDisbalance,
            this.colDirection,
            this.colAltBasePrice,
            this.colAtlUsdtPrice,
            this.colBaseUsdtPrice,
            this.colAmount,
            this.colProfit,
            this.colFee,
            this.gridColumn1,
            this.colEarned});
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Column = this.colProfit;
            gridFormatRule1.Enabled = false;
            gridFormatRule1.Name = "Profit";
            formatConditionRuleValue1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            formatConditionRuleValue1.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Greater;
            formatConditionRuleValue1.Value1 = new decimal(new int[] {
            0,
            0,
            0,
            0});
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.Column = this.colProfit;
            gridFormatRule2.ColumnApplyTo = this.colProfit;
            gridFormatRule2.Name = "Format0";
            formatConditionRuleDataBar1.PredefinedName = "Coral Gradient";
            gridFormatRule2.Rule = formatConditionRuleDataBar1;
            this.gridView1.FormatRules.Add(gridFormatRule1);
            this.gridView1.FormatRules.Add(gridFormatRule2);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // colDisbalance
            // 
            this.colDisbalance.DisplayFormat.FormatString = "0.00000000";
            this.colDisbalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDisbalance.FieldName = "Disbalance";
            this.colDisbalance.Name = "colDisbalance";
            this.colDisbalance.OptionsColumn.ReadOnly = true;
            this.colDisbalance.Visible = true;
            this.colDisbalance.VisibleIndex = 1;
            // 
            // colDirection
            // 
            this.colDirection.FieldName = "Direction";
            this.colDirection.Name = "colDirection";
            this.colDirection.OptionsColumn.ReadOnly = true;
            this.colDirection.Visible = true;
            this.colDirection.VisibleIndex = 2;
            // 
            // colAltBasePrice
            // 
            this.colAltBasePrice.DisplayFormat.FormatString = "0.00000000";
            this.colAltBasePrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAltBasePrice.FieldName = "AltBasePrice";
            this.colAltBasePrice.Name = "colAltBasePrice";
            this.colAltBasePrice.Visible = true;
            this.colAltBasePrice.VisibleIndex = 3;
            // 
            // colAtlUsdtPrice
            // 
            this.colAtlUsdtPrice.DisplayFormat.FormatString = "0.00000000";
            this.colAtlUsdtPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAtlUsdtPrice.FieldName = "AtlUsdtPrice";
            this.colAtlUsdtPrice.Name = "colAtlUsdtPrice";
            this.colAtlUsdtPrice.Visible = true;
            this.colAtlUsdtPrice.VisibleIndex = 4;
            // 
            // colBaseUsdtPrice
            // 
            this.colBaseUsdtPrice.DisplayFormat.FormatString = "0.00000000";
            this.colBaseUsdtPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBaseUsdtPrice.FieldName = "BaseUsdtPrice";
            this.colBaseUsdtPrice.Name = "colBaseUsdtPrice";
            this.colBaseUsdtPrice.Visible = true;
            this.colBaseUsdtPrice.VisibleIndex = 5;
            // 
            // colAmount
            // 
            this.colAmount.DisplayFormat.FormatString = "0.00000000";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 6;
            // 
            // colFee
            // 
            this.colFee.DisplayFormat.FormatString = "0.00000000";
            this.colFee.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colFee.FieldName = "Fee";
            this.colFee.Name = "colFee";
            this.colFee.Visible = true;
            this.colFee.VisibleIndex = 8;
            // 
            // gridColumn1
            // 
            this.gridColumn1.DisplayFormat.FormatString = "hh:mm:ss.fff";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn1.FieldName = "Time";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.staticArbitrageInfoHistoryItemBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gridControl1.Location = new System.Drawing.Point(0, 278);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4);
            this.gridControl1.MenuManager = this.ribbonControl1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1782, 728);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // colEarned
            // 
            this.colEarned.Caption = "Earned";
            this.colEarned.FieldName = "Earned";
            this.colEarned.Name = "colEarned";
            this.colEarned.Visible = true;
            this.colEarned.VisibleIndex = 9;
            // 
            // StaticArbitrageHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1782, 1006);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "StaticArbitrageHistoryForm";
            this.Text = "Static Arbitrage";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staticArbitrageInfoHistoryItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private System.Windows.Forms.BindingSource staticArbitrageInfoHistoryItemBindingSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colDisbalance;
        private DevExpress.XtraGrid.Columns.GridColumn colDirection;
        private DevExpress.XtraGrid.Columns.GridColumn colAltBasePrice;
        private DevExpress.XtraGrid.Columns.GridColumn colAtlUsdtPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colBaseUsdtPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colProfit;
        private DevExpress.XtraGrid.Columns.GridColumn colFee;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraGrid.Columns.GridColumn colEarned;
    }
}