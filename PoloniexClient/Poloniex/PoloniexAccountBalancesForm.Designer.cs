namespace CryptoMarketClient.Poloniex {
    partial class PoloniexAccountBalancesForm {
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
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet1 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet1 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon1 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon2 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon3 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet2 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet2 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon4 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon5 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon6 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule3 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet3 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet3 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon7 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon8 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon9 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            this.gridControl1 = new MyGridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.poloniexAccountBalanceInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAvailable = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOnOrders = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBtcValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepositAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.poloniexAccountBalanceInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.poloniexAccountBalanceInfoBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1453, 988);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCurrency,
            this.colAvailable,
            this.colOnOrders,
            this.colBtcValue,
            this.colDepositAddress});
            gridFormatRule1.Name = "FormatRulePercentChange";
            formatConditionIconSet1.CategoryName = "Positive/Negative";
            formatConditionIconSetIcon1.PredefinedName = "Arrows3_3.png";
            formatConditionIconSetIcon1.Value = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            formatConditionIconSetIcon1.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon2.PredefinedName = "Arrows3_2.png";
            formatConditionIconSetIcon2.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon3.PredefinedName = "Arrows3_1.png";
            formatConditionIconSet1.Icons.Add(formatConditionIconSetIcon1);
            formatConditionIconSet1.Icons.Add(formatConditionIconSetIcon2);
            formatConditionIconSet1.Icons.Add(formatConditionIconSetIcon3);
            formatConditionIconSet1.Name = "PositiveNegativeArrows";
            formatConditionIconSet1.ValueType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            formatConditionRuleIconSet1.IconSet = formatConditionIconSet1;
            gridFormatRule1.Rule = formatConditionRuleIconSet1;
            gridFormatRule2.Name = "FormatRuleBidDelta";
            formatConditionIconSet2.CategoryName = "Positive/Negative";
            formatConditionIconSetIcon4.PredefinedName = "Arrows3_3.png";
            formatConditionIconSetIcon4.Value = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            formatConditionIconSetIcon4.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon5.PredefinedName = "Arrows3_2.png";
            formatConditionIconSetIcon5.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon6.PredefinedName = "Arrows3_1.png";
            formatConditionIconSet2.Icons.Add(formatConditionIconSetIcon4);
            formatConditionIconSet2.Icons.Add(formatConditionIconSetIcon5);
            formatConditionIconSet2.Icons.Add(formatConditionIconSetIcon6);
            formatConditionIconSet2.Name = "PositiveNegativeArrows";
            formatConditionIconSet2.ValueType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            formatConditionRuleIconSet2.IconSet = formatConditionIconSet2;
            gridFormatRule2.Rule = formatConditionRuleIconSet2;
            gridFormatRule3.Name = "FormatRuleAskDelta";
            formatConditionIconSet3.CategoryName = "Positive/Negative";
            formatConditionIconSetIcon7.PredefinedName = "Arrows3_3.png";
            formatConditionIconSetIcon7.Value = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            formatConditionIconSetIcon7.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon8.PredefinedName = "Arrows3_2.png";
            formatConditionIconSetIcon8.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon9.PredefinedName = "Arrows3_1.png";
            formatConditionIconSet3.Icons.Add(formatConditionIconSetIcon7);
            formatConditionIconSet3.Icons.Add(formatConditionIconSetIcon8);
            formatConditionIconSet3.Icons.Add(formatConditionIconSetIcon9);
            formatConditionIconSet3.Name = "PositiveNegativeArrows";
            formatConditionIconSet3.ValueType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            formatConditionRuleIconSet3.IconSet = formatConditionIconSet3;
            gridFormatRule3.Rule = formatConditionRuleIconSet3;
            this.gridView1.FormatRules.Add(gridFormatRule1);
            this.gridView1.FormatRules.Add(gridFormatRule2);
            this.gridView1.FormatRules.Add(gridFormatRule3);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            // 
            // poloniexAccountBalanceInfoBindingSource
            // 
            this.poloniexAccountBalanceInfoBindingSource.DataSource = typeof(CryptoMarketClient.Poloniex.PoloniexAccountBalanceInfo);
            // 
            // colCurrency
            // 
            this.colCurrency.FieldName = "Currency";
            this.colCurrency.Name = "colCurrency";
            this.colCurrency.Visible = true;
            this.colCurrency.VisibleIndex = 0;
            // 
            // colAvailable
            // 
            this.colAvailable.FieldName = "Available";
            this.colAvailable.Name = "colAvailable";
            this.colAvailable.Visible = true;
            this.colAvailable.VisibleIndex = 1;
            // 
            // colOnOrders
            // 
            this.colOnOrders.FieldName = "OnOrders";
            this.colOnOrders.Name = "colOnOrders";
            this.colOnOrders.Visible = true;
            this.colOnOrders.VisibleIndex = 2;
            // 
            // colBtcValue
            // 
            this.colBtcValue.FieldName = "BtcValue";
            this.colBtcValue.Name = "colBtcValue";
            this.colBtcValue.Visible = true;
            this.colBtcValue.VisibleIndex = 3;
            // 
            // colDepositAddress
            // 
            this.colDepositAddress.FieldName = "DepositAddress";
            this.colDepositAddress.Name = "colDepositAddress";
            this.colDepositAddress.Visible = true;
            this.colDepositAddress.VisibleIndex = 4;
            // 
            // PoloniexAccountBalancesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1453, 988);
            this.Controls.Add(this.gridControl1);
            this.Name = "PoloniexAccountBalancesForm";
            this.Text = "Poloniex Account Balances";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.poloniexAccountBalanceInfoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MyGridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource poloniexAccountBalanceInfoBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colAvailable;
        private DevExpress.XtraGrid.Columns.GridColumn colOnOrders;
        private DevExpress.XtraGrid.Columns.GridColumn colBtcValue;
        private DevExpress.XtraGrid.Columns.GridColumn colDepositAddress;
    }
}