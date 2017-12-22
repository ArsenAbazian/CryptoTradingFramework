namespace CryptoMarketClient.Bittrex {
    partial class BittrexAccountBalancesForm {
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
            this.bittrexAccountBalanceInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAvailable = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPending = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCryptoAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRequested = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUuid = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bittrexAccountBalanceInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bittrexAccountBalanceInfoBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1407, 877);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // bittrexAccountBalanceInfoBindingSource
            // 
            this.bittrexAccountBalanceInfoBindingSource.DataSource = typeof(CryptoMarketClient.Bittrex.BittrexAccountBalanceInfo);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCurrency,
            this.colBalance,
            this.colAvailable,
            this.colPending,
            this.colCryptoAddress,
            this.colRequested,
            this.colUuid});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colCurrency
            // 
            this.colCurrency.FieldName = "Currency";
            this.colCurrency.Name = "colCurrency";
            this.colCurrency.Visible = true;
            this.colCurrency.VisibleIndex = 0;
            // 
            // colBalance
            // 
            this.colBalance.FieldName = "Balance";
            this.colBalance.Name = "colBalance";
            this.colBalance.Visible = true;
            this.colBalance.VisibleIndex = 1;
            // 
            // colAvailable
            // 
            this.colAvailable.FieldName = "Available";
            this.colAvailable.Name = "colAvailable";
            this.colAvailable.Visible = true;
            this.colAvailable.VisibleIndex = 2;
            // 
            // colPending
            // 
            this.colPending.FieldName = "Pending";
            this.colPending.Name = "colPending";
            this.colPending.Visible = true;
            this.colPending.VisibleIndex = 3;
            // 
            // colCryptoAddress
            // 
            this.colCryptoAddress.FieldName = "CryptoAddress";
            this.colCryptoAddress.Name = "colCryptoAddress";
            this.colCryptoAddress.Visible = true;
            this.colCryptoAddress.VisibleIndex = 4;
            // 
            // colRequested
            // 
            this.colRequested.FieldName = "Requested";
            this.colRequested.Name = "colRequested";
            this.colRequested.Visible = true;
            this.colRequested.VisibleIndex = 5;
            // 
            // colUuid
            // 
            this.colUuid.FieldName = "Uuid";
            this.colUuid.Name = "colUuid";
            this.colUuid.Visible = true;
            this.colUuid.VisibleIndex = 6;
            // 
            // BittrexAccauntBalancesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1407, 877);
            this.Controls.Add(this.gridControl1);
            this.Name = "BittrexAccauntBalancesForm";
            this.Text = "Bittrex Account Balances";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bittrexAccountBalanceInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.BindingSource bittrexAccountBalanceInfoBindingSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colBalance;
        private DevExpress.XtraGrid.Columns.GridColumn colAvailable;
        private DevExpress.XtraGrid.Columns.GridColumn colPending;
        private DevExpress.XtraGrid.Columns.GridColumn colCryptoAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colRequested;
        private DevExpress.XtraGrid.Columns.GridColumn colUuid;
    }
}