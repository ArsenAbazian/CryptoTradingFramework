using CryptoMarketClient.Common;

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
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new CryptoMarketClient.MyGridControl();
            this.openedOrdersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMarket = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colOrderNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValueString = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmountString = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalString = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderDate = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.openedOrdersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // colType
            // 
            this.colType.FieldName = "Type";
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 2;
            this.colType.Width = 150;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.openedOrdersBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.gridControl1.Size = new System.Drawing.Size(1686, 944);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // openedOrdersBindingSource
            // 
            this.openedOrdersBindingSource.DataSource = typeof(CryptoMarketClient.Common.OpenedOrderInfo);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.FontSizeDelta = 2;
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.FontSizeDelta = 2;
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDate,
            this.colMarket,
            this.colOrderNumber,
            this.colType,
            this.colValueString,
            this.colAmountString,
            this.colTotalString,
            this.colValue,
            this.colAmount,
            this.colTotal,
            this.colOrderDate});
            this.gridView1.DetailHeight = 673;
            this.gridView1.FixedLineWidth = 4;
            gridFormatRule1.Column = this.colType;
            gridFormatRule1.ColumnApplyTo = this.colType;
            gridFormatRule1.Name = "BuyOrderType";
            formatConditionRuleValue1.Appearance.FontSizeDelta = 2;
            formatConditionRuleValue1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            formatConditionRuleValue1.Appearance.Options.UseFont = true;
            formatConditionRuleValue1.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue1.Value1 = CryptoMarketClient.Common.OrderType.Buy;
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.Column = this.colType;
            gridFormatRule2.ColumnApplyTo = this.colType;
            gridFormatRule2.Name = "SellOrderType";
            formatConditionRuleValue2.Appearance.FontSizeDelta = 2;
            formatConditionRuleValue2.Appearance.ForeColor = System.Drawing.Color.Red;
            formatConditionRuleValue2.Appearance.Options.UseFont = true;
            formatConditionRuleValue2.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue2.Value1 = CryptoMarketClient.Common.OrderType.Sell;
            gridFormatRule2.Rule = formatConditionRuleValue2;
            this.gridView1.FormatRules.Add(gridFormatRule1);
            this.gridView1.FormatRules.Add(gridFormatRule2);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colDate
            // 
            this.colDate.FieldName = "Date";
            this.colDate.Name = "colDate";
            this.colDate.Visible = true;
            this.colDate.VisibleIndex = 1;
            this.colDate.Width = 150;
            // 
            // colMarket
            // 
            this.colMarket.ColumnEdit = this.repositoryItemTextEdit1;
            this.colMarket.FieldName = "Market";
            this.colMarket.Name = "colMarket";
            this.colMarket.Visible = true;
            this.colMarket.VisibleIndex = 0;
            this.colMarket.Width = 150;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            // 
            // colOrderNumber
            // 
            this.colOrderNumber.FieldName = "OrderNumber";
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
            this.colValueString.VisibleIndex = 3;
            this.colValueString.Width = 150;
            // 
            // colAmountString
            // 
            this.colAmountString.Caption = "Amount";
            this.colAmountString.FieldName = "AmountString";
            this.colAmountString.Name = "colAmountString";
            this.colAmountString.Visible = true;
            this.colAmountString.VisibleIndex = 4;
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
            // colValue
            // 
            this.colValue.FieldName = "Value";
            this.colValue.Name = "colValue";
            this.colValue.OptionsColumn.ReadOnly = true;
            this.colValue.Width = 150;
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
            // colOrderDate
            // 
            this.colOrderDate.FieldName = "OrderDate";
            this.colOrderDate.Name = "colOrderDate";
            this.colOrderDate.OptionsColumn.ReadOnly = true;
            this.colOrderDate.Width = 150;
            // 
            // OpenedOrdersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1686, 944);
            this.Controls.Add(this.gridControl1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "OpenedOrdersForm";
            this.Text = "Poloniex Opened Orders";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.openedOrdersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MyGridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource openedOrdersBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colDate;
        private DevExpress.XtraGrid.Columns.GridColumn colMarket;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colValueString;
        private DevExpress.XtraGrid.Columns.GridColumn colAmountString;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalString;
        private DevExpress.XtraGrid.Columns.GridColumn colValue;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
    }
}