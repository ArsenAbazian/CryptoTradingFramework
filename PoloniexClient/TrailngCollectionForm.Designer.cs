namespace CryptoMarketClient {
    partial class TrailngCollectionForm {
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
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.trailingSettingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colTicker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmout = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStopLossSellPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActualProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActualProfitUSD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUsdTicker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStopLossPricePercent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStopLossStartPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTakeProfitPercent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActualPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaxPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriceDelta = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTakeProfitPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trailingSettingsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.trailingSettingsBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 278);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(2006, 698);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTicker,
            this.colBuyPrice,
            this.colAmout,
            this.colStopLossSellPrice,
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
            this.colName});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 1;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.Size = new System.Drawing.Size(2006, 278);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Active Trailing";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Active Trailing";
            // 
            // trailingSettingsBindingSource
            // 
            this.trailingSettingsBindingSource.DataSource = typeof(CryptoMarketClient.Common.TrailingSettings);
            // 
            // colTicker
            // 
            this.colTicker.FieldName = "Ticker";
            this.colTicker.Name = "colTicker";
            // 
            // colBuyPrice
            // 
            this.colBuyPrice.FieldName = "BuyPrice";
            this.colBuyPrice.Name = "colBuyPrice";
            this.colBuyPrice.Visible = true;
            this.colBuyPrice.VisibleIndex = 1;
            // 
            // colAmout
            // 
            this.colAmout.FieldName = "Amout";
            this.colAmout.Name = "colAmout";
            this.colAmout.Visible = true;
            this.colAmout.VisibleIndex = 2;
            // 
            // colStopLossSellPrice
            // 
            this.colStopLossSellPrice.FieldName = "StopLossSellPrice";
            this.colStopLossSellPrice.Name = "colStopLossSellPrice";
            this.colStopLossSellPrice.Visible = true;
            this.colStopLossSellPrice.VisibleIndex = 5;
            // 
            // colActualProfit
            // 
            this.colActualProfit.FieldName = "ActualProfit";
            this.colActualProfit.Name = "colActualProfit";
            this.colActualProfit.OptionsColumn.ReadOnly = true;
            this.colActualProfit.Visible = true;
            this.colActualProfit.VisibleIndex = 6;
            // 
            // colActualProfitUSD
            // 
            this.colActualProfitUSD.FieldName = "ActualProfitUSD";
            this.colActualProfitUSD.Name = "colActualProfitUSD";
            this.colActualProfitUSD.OptionsColumn.ReadOnly = true;
            this.colActualProfitUSD.Visible = true;
            this.colActualProfitUSD.VisibleIndex = 7;
            // 
            // colUsdTicker
            // 
            this.colUsdTicker.FieldName = "UsdTicker";
            this.colUsdTicker.Name = "colUsdTicker";
            // 
            // colStopLossPricePercent
            // 
            this.colStopLossPricePercent.FieldName = "StopLossPricePercent";
            this.colStopLossPricePercent.Name = "colStopLossPricePercent";
            this.colStopLossPricePercent.Visible = true;
            this.colStopLossPricePercent.VisibleIndex = 8;
            // 
            // colStopLossStartPrice
            // 
            this.colStopLossStartPrice.FieldName = "StopLossStartPrice";
            this.colStopLossStartPrice.Name = "colStopLossStartPrice";
            this.colStopLossStartPrice.OptionsColumn.ReadOnly = true;
            this.colStopLossStartPrice.Visible = true;
            this.colStopLossStartPrice.VisibleIndex = 9;
            // 
            // colTakeProfitPercent
            // 
            this.colTakeProfitPercent.FieldName = "TakeProfitPercent";
            this.colTakeProfitPercent.Name = "colTakeProfitPercent";
            this.colTakeProfitPercent.Visible = true;
            this.colTakeProfitPercent.VisibleIndex = 10;
            // 
            // colActualPrice
            // 
            this.colActualPrice.FieldName = "ActualPrice";
            this.colActualPrice.Name = "colActualPrice";
            this.colActualPrice.Visible = true;
            this.colActualPrice.VisibleIndex = 3;
            // 
            // colMaxPrice
            // 
            this.colMaxPrice.FieldName = "MaxPrice";
            this.colMaxPrice.Name = "colMaxPrice";
            this.colMaxPrice.Visible = true;
            this.colMaxPrice.VisibleIndex = 4;
            // 
            // colPriceDelta
            // 
            this.colPriceDelta.FieldName = "PriceDelta";
            this.colPriceDelta.Name = "colPriceDelta";
            this.colPriceDelta.OptionsColumn.ReadOnly = true;
            this.colPriceDelta.Visible = true;
            this.colPriceDelta.VisibleIndex = 11;
            // 
            // colTakeProfitPrice
            // 
            this.colTakeProfitPrice.FieldName = "TakeProfitPrice";
            this.colTakeProfitPrice.Name = "colTakeProfitPrice";
            this.colTakeProfitPrice.OptionsColumn.ReadOnly = true;
            this.colTakeProfitPrice.Visible = true;
            this.colTakeProfitPrice.VisibleIndex = 12;
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.ReadOnly = true;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            // 
            // ActiveTrailngCollectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2006, 976);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "ActiveTrailngCollectionForm";
            this.Text = "Active Trailings";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trailingSettingsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private System.Windows.Forms.BindingSource trailingSettingsBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colTicker;
        private DevExpress.XtraGrid.Columns.GridColumn colBuyPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colAmout;
        private DevExpress.XtraGrid.Columns.GridColumn colStopLossSellPrice;
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
    }
}