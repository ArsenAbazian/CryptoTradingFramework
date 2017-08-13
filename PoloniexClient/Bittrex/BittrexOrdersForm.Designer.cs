namespace CryptoMarketClient.Bittrex {
    partial class BittrexOrdersForm {
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bittrexOrderInfoBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMarketName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderUuid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExchange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantityRemaining = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLimit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCommissionPaid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPricePerUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOpened = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colClosed = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCancelInitiated = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colImmediateOrCancel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsConditional = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCondition = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConditionTarget = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bittrexOrderInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bittrexOrderInfoBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bittrexOrderInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bittrexOrderInfoBindingSource1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(865, 425);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // bittrexOrderInfoBindingSource1
            // 
            this.bittrexOrderInfoBindingSource1.DataSource = typeof(CryptoMarketClient.Bittrex.BittrexOrderInfo);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMarketName,
            this.colOrderUuid,
            this.colExchange,
            this.colOrderType,
            this.colQuantity,
            this.colQuantityRemaining,
            this.colLimit,
            this.colCommissionPaid,
            this.colPrice,
            this.colPricePerUnit,
            this.colOpened,
            this.colClosed,
            this.colCancelInitiated,
            this.colImmediateOrCancel,
            this.colIsConditional,
            this.colCondition,
            this.colConditionTarget});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupCount = 1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colMarketName, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colMarketName
            // 
            this.colMarketName.FieldName = "MarketName";
            this.colMarketName.Name = "colMarketName";
            this.colMarketName.Visible = true;
            this.colMarketName.VisibleIndex = 0;
            // 
            // colOrderUuid
            // 
            this.colOrderUuid.FieldName = "OrderUuid";
            this.colOrderUuid.Name = "colOrderUuid";
            this.colOrderUuid.Visible = true;
            this.colOrderUuid.VisibleIndex = 0;
            // 
            // colExchange
            // 
            this.colExchange.FieldName = "Exchange";
            this.colExchange.Name = "colExchange";
            this.colExchange.Visible = true;
            this.colExchange.VisibleIndex = 1;
            // 
            // colOrderType
            // 
            this.colOrderType.FieldName = "OrderType";
            this.colOrderType.Name = "colOrderType";
            this.colOrderType.Visible = true;
            this.colOrderType.VisibleIndex = 2;
            // 
            // colQuantity
            // 
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 3;
            // 
            // colQuantityRemaining
            // 
            this.colQuantityRemaining.FieldName = "QuantityRemaining";
            this.colQuantityRemaining.Name = "colQuantityRemaining";
            this.colQuantityRemaining.Visible = true;
            this.colQuantityRemaining.VisibleIndex = 4;
            // 
            // colLimit
            // 
            this.colLimit.FieldName = "Limit";
            this.colLimit.Name = "colLimit";
            this.colLimit.Visible = true;
            this.colLimit.VisibleIndex = 5;
            // 
            // colCommissionPaid
            // 
            this.colCommissionPaid.FieldName = "CommissionPaid";
            this.colCommissionPaid.Name = "colCommissionPaid";
            this.colCommissionPaid.Visible = true;
            this.colCommissionPaid.VisibleIndex = 6;
            // 
            // colPrice
            // 
            this.colPrice.FieldName = "Price";
            this.colPrice.Name = "colPrice";
            this.colPrice.Visible = true;
            this.colPrice.VisibleIndex = 7;
            // 
            // colPricePerUnit
            // 
            this.colPricePerUnit.FieldName = "PricePerUnit";
            this.colPricePerUnit.Name = "colPricePerUnit";
            this.colPricePerUnit.Visible = true;
            this.colPricePerUnit.VisibleIndex = 8;
            // 
            // colOpened
            // 
            this.colOpened.FieldName = "Opened";
            this.colOpened.Name = "colOpened";
            this.colOpened.Visible = true;
            this.colOpened.VisibleIndex = 9;
            // 
            // colClosed
            // 
            this.colClosed.FieldName = "Closed";
            this.colClosed.Name = "colClosed";
            this.colClosed.Visible = true;
            this.colClosed.VisibleIndex = 10;
            // 
            // colCancelInitiated
            // 
            this.colCancelInitiated.FieldName = "CancelInitiated";
            this.colCancelInitiated.Name = "colCancelInitiated";
            this.colCancelInitiated.Visible = true;
            this.colCancelInitiated.VisibleIndex = 11;
            // 
            // colImmediateOrCancel
            // 
            this.colImmediateOrCancel.FieldName = "ImmediateOrCancel";
            this.colImmediateOrCancel.Name = "colImmediateOrCancel";
            this.colImmediateOrCancel.Visible = true;
            this.colImmediateOrCancel.VisibleIndex = 12;
            // 
            // colIsConditional
            // 
            this.colIsConditional.FieldName = "IsConditional";
            this.colIsConditional.Name = "colIsConditional";
            this.colIsConditional.Visible = true;
            this.colIsConditional.VisibleIndex = 13;
            // 
            // colCondition
            // 
            this.colCondition.FieldName = "Condition";
            this.colCondition.Name = "colCondition";
            this.colCondition.Visible = true;
            this.colCondition.VisibleIndex = 14;
            // 
            // colConditionTarget
            // 
            this.colConditionTarget.FieldName = "ConditionTarget";
            this.colConditionTarget.Name = "colConditionTarget";
            this.colConditionTarget.Visible = true;
            this.colConditionTarget.VisibleIndex = 15;
            // 
            // bittrexOrderInfoBindingSource
            // 
            this.bittrexOrderInfoBindingSource.DataSource = typeof(CryptoMarketClient.Bittrex.BittrexOrderInfo);
            // 
            // BittrexOrdersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 425);
            this.Controls.Add(this.gridControl1);
            this.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.Name = "BittrexOrdersForm";
            this.Text = "Bittrex Opened Orders";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bittrexOrderInfoBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bittrexOrderInfoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.BindingSource bittrexOrderInfoBindingSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource bittrexOrderInfoBindingSource1;
        private DevExpress.XtraGrid.Columns.GridColumn colMarketName;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderUuid;
        private DevExpress.XtraGrid.Columns.GridColumn colExchange;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderType;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantityRemaining;
        private DevExpress.XtraGrid.Columns.GridColumn colLimit;
        private DevExpress.XtraGrid.Columns.GridColumn colCommissionPaid;
        private DevExpress.XtraGrid.Columns.GridColumn colPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colPricePerUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colOpened;
        private DevExpress.XtraGrid.Columns.GridColumn colClosed;
        private DevExpress.XtraGrid.Columns.GridColumn colCancelInitiated;
        private DevExpress.XtraGrid.Columns.GridColumn colImmediateOrCancel;
        private DevExpress.XtraGrid.Columns.GridColumn colIsConditional;
        private DevExpress.XtraGrid.Columns.GridColumn colCondition;
        private DevExpress.XtraGrid.Columns.GridColumn colConditionTarget;
    }
}