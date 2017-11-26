namespace CryptoMarketClient {
    partial class TickerInfo {
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHighestBid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLowestAsk = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLast = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBaseVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHr24High = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHr24Low = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSpread = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBidChange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAskChange = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1226, 584);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(CryptoMarketClient.TickerBase);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colName,
            this.colHighestBid,
            this.colLowestAsk,
            this.colLast,
            this.colBaseVolume,
            this.colVolume,
            this.colHr24High,
            this.colHr24Low,
            this.colChange,
            this.colSpread,
            this.colBidChange,
            this.colAskChange});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.ReadOnly = true;
            // 
            // colHighestBid
            // 
            this.colHighestBid.AppearanceHeader.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.colHighestBid.AppearanceHeader.Options.UseFont = true;
            this.colHighestBid.Caption = "Bid";
            this.colHighestBid.FieldName = "HighestBid";
            this.colHighestBid.Name = "colHighestBid";
            this.colHighestBid.OptionsColumn.ReadOnly = true;
            this.colHighestBid.Visible = true;
            this.colHighestBid.VisibleIndex = 1;
            // 
            // colLowestAsk
            // 
            this.colLowestAsk.AppearanceHeader.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.colLowestAsk.AppearanceHeader.Options.UseFont = true;
            this.colLowestAsk.Caption = "Ask";
            this.colLowestAsk.FieldName = "LowestAsk";
            this.colLowestAsk.Name = "colLowestAsk";
            this.colLowestAsk.OptionsColumn.ReadOnly = true;
            this.colLowestAsk.Visible = true;
            this.colLowestAsk.VisibleIndex = 2;
            // 
            // colLast
            // 
            this.colLast.AppearanceHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colLast.AppearanceHeader.Options.UseFont = true;
            this.colLast.FieldName = "Last";
            this.colLast.Name = "colLast";
            this.colLast.OptionsColumn.ReadOnly = true;
            this.colLast.Visible = true;
            this.colLast.VisibleIndex = 0;
            // 
            // colBaseVolume
            // 
            this.colBaseVolume.AppearanceHeader.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.colBaseVolume.AppearanceHeader.Options.UseFont = true;
            this.colBaseVolume.FieldName = "BaseVolume";
            this.colBaseVolume.Name = "colBaseVolume";
            this.colBaseVolume.OptionsColumn.ReadOnly = true;
            this.colBaseVolume.Visible = true;
            this.colBaseVolume.VisibleIndex = 3;
            // 
            // colVolume
            // 
            this.colVolume.AppearanceHeader.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.colVolume.AppearanceHeader.Options.UseFont = true;
            this.colVolume.FieldName = "Volume";
            this.colVolume.Name = "colVolume";
            this.colVolume.OptionsColumn.ReadOnly = true;
            this.colVolume.Visible = true;
            this.colVolume.VisibleIndex = 4;
            // 
            // colHr24High
            // 
            this.colHr24High.AppearanceHeader.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.colHr24High.AppearanceHeader.Options.UseFont = true;
            this.colHr24High.FieldName = "Hr24High";
            this.colHr24High.Name = "colHr24High";
            this.colHr24High.OptionsColumn.ReadOnly = true;
            this.colHr24High.Visible = true;
            this.colHr24High.VisibleIndex = 5;
            // 
            // colHr24Low
            // 
            this.colHr24Low.AppearanceHeader.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.colHr24Low.AppearanceHeader.Options.UseFont = true;
            this.colHr24Low.FieldName = "Hr24Low";
            this.colHr24Low.Name = "colHr24Low";
            this.colHr24Low.OptionsColumn.ReadOnly = true;
            this.colHr24Low.Visible = true;
            this.colHr24Low.VisibleIndex = 6;
            // 
            // colChange
            // 
            this.colChange.AppearanceHeader.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.colChange.AppearanceHeader.Options.UseFont = true;
            this.colChange.DisplayFormat.FormatString = "F3";
            this.colChange.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colChange.FieldName = "Change";
            this.colChange.Name = "colChange";
            this.colChange.OptionsColumn.ReadOnly = true;
            this.colChange.Visible = true;
            this.colChange.VisibleIndex = 7;
            // 
            // colSpread
            // 
            this.colSpread.Caption = "Spread";
            this.colSpread.FieldName = "Spread";
            this.colSpread.Name = "colSpread";
            // 
            // colBidChange
            // 
            this.colBidChange.AppearanceHeader.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.colBidChange.AppearanceHeader.Options.UseFont = true;
            this.colBidChange.Caption = "Bid Change";
            this.colBidChange.DisplayFormat.FormatString = "F3";
            this.colBidChange.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBidChange.FieldName = "BidChange";
            this.colBidChange.Name = "colBidChange";
            this.colBidChange.Visible = true;
            this.colBidChange.VisibleIndex = 8;
            // 
            // colAskChange
            // 
            this.colAskChange.AppearanceHeader.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.colAskChange.AppearanceHeader.Options.UseFont = true;
            this.colAskChange.Caption = "Ask Change";
            this.colAskChange.DisplayFormat.FormatString = "F3";
            this.colAskChange.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAskChange.FieldName = "AskChange";
            this.colAskChange.Name = "colAskChange";
            this.colAskChange.Visible = true;
            this.colAskChange.VisibleIndex = 9;
            // 
            // TickerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "TickerInfo";
            this.Size = new System.Drawing.Size(1226, 584);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.BindingSource bindingSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colHighestBid;
        private DevExpress.XtraGrid.Columns.GridColumn colLowestAsk;
        private DevExpress.XtraGrid.Columns.GridColumn colLast;
        private DevExpress.XtraGrid.Columns.GridColumn colBaseVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colHr24High;
        private DevExpress.XtraGrid.Columns.GridColumn colHr24Low;
        private DevExpress.XtraGrid.Columns.GridColumn colChange;
        private DevExpress.XtraGrid.Columns.GridColumn colSpread;
        private DevExpress.XtraGrid.Columns.GridColumn colBidChange;
        private DevExpress.XtraGrid.Columns.GridColumn colAskChange;
    }
}
