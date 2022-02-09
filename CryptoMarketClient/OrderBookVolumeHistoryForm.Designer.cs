namespace CryptoMarketClient {
    partial class OrderBookVolumeHistoryForm {
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
            this.tradeGridControl = new DevExpress.XtraGrid.GridControl();
            this.orderBookVolumeHistoryItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLowestAsk = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHighestBid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBidVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAskVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBidExpectation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAskExpectation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBidDispersion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAskDispersion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBidAskRelation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.colTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeInfo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMinBuyPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaxBuyPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMinSellPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaxSellPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSellAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSellVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBidEnergy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAskEnergy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHipe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSellHipe = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tradeGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderBookVolumeHistoryItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // tradeGridControl
            // 
            this.tradeGridControl.DataSource = this.orderBookVolumeHistoryItemBindingSource;
            this.tradeGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tradeGridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.tradeGridControl.Location = new System.Drawing.Point(0, 0);
            this.tradeGridControl.MainView = this.gridView1;
            this.tradeGridControl.Margin = new System.Windows.Forms.Padding(6);
            this.tradeGridControl.Name = "tradeGridControl";
            this.tradeGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar1});
            this.tradeGridControl.Size = new System.Drawing.Size(1680, 753);
            this.tradeGridControl.TabIndex = 1;
            this.tradeGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // orderBookVolumeHistoryItemBindingSource
            // 
            this.orderBookVolumeHistoryItemBindingSource.DataSource = typeof(Crypto.Core.OrderBookStatisticItem);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.Row.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridView1.Appearance.Row.Options.UseForeColor = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLowestAsk,
            this.colHighestBid,
            this.colBidVolume,
            this.colAskVolume,
            this.colBidExpectation,
            this.colAskExpectation,
            this.colBidDispersion,
            this.colAskDispersion,
            this.colBidAskRelation,
            this.colTime,
            this.colTradeInfo,
            this.colMinBuyPrice,
            this.colMaxBuyPrice,
            this.colBuyAmount,
            this.colBuyVolume,
            this.colMinSellPrice,
            this.colMaxSellPrice,
            this.colSellAmount,
            this.colSellVolume,
            this.colBidEnergy,
            this.colAskEnergy,
            this.colHipe,
            this.colSellHipe});
            this.gridView1.GridControl = this.tradeGridControl;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            // 
            // colLowestAsk
            // 
            this.colLowestAsk.DisplayFormat.FormatString = "0.00000000";
            this.colLowestAsk.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colLowestAsk.FieldName = "Ask";
            this.colLowestAsk.Name = "colLowestAsk";
            this.colLowestAsk.Visible = true;
            this.colLowestAsk.VisibleIndex = 1;
            // 
            // colHighestBid
            // 
            this.colHighestBid.DisplayFormat.FormatString = "0.00000000";
            this.colHighestBid.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHighestBid.FieldName = "Bid";
            this.colHighestBid.Name = "colHighestBid";
            this.colHighestBid.Visible = true;
            this.colHighestBid.VisibleIndex = 2;
            // 
            // colBidVolume
            // 
            this.colBidVolume.DisplayFormat.FormatString = "0.";
            this.colBidVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBidVolume.FieldName = "BidVolume";
            this.colBidVolume.Name = "colBidVolume";
            this.colBidVolume.Visible = true;
            this.colBidVolume.VisibleIndex = 3;
            // 
            // colAskVolume
            // 
            this.colAskVolume.DisplayFormat.FormatString = "0.";
            this.colAskVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAskVolume.FieldName = "AskVolume";
            this.colAskVolume.Name = "colAskVolume";
            this.colAskVolume.Visible = true;
            this.colAskVolume.VisibleIndex = 4;
            // 
            // colBidExpectation
            // 
            this.colBidExpectation.DisplayFormat.FormatString = "0.00000000";
            this.colBidExpectation.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBidExpectation.FieldName = "BidExpectation";
            this.colBidExpectation.Name = "colBidExpectation";
            this.colBidExpectation.Visible = true;
            this.colBidExpectation.VisibleIndex = 5;
            // 
            // colAskExpectation
            // 
            this.colAskExpectation.DisplayFormat.FormatString = "0.00000000";
            this.colAskExpectation.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAskExpectation.FieldName = "AskExpectation";
            this.colAskExpectation.Name = "colAskExpectation";
            this.colAskExpectation.Visible = true;
            this.colAskExpectation.VisibleIndex = 6;
            // 
            // colBidDispersion
            // 
            this.colBidDispersion.DisplayFormat.FormatString = "0.00000000####";
            this.colBidDispersion.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBidDispersion.FieldName = "BidDispersion";
            this.colBidDispersion.Name = "colBidDispersion";
            this.colBidDispersion.Visible = true;
            this.colBidDispersion.VisibleIndex = 7;
            // 
            // colAskDispersion
            // 
            this.colAskDispersion.DisplayFormat.FormatString = "0.00000000####";
            this.colAskDispersion.FieldName = "AskDispersion";
            this.colAskDispersion.Name = "colAskDispersion";
            this.colAskDispersion.Visible = true;
            this.colAskDispersion.VisibleIndex = 8;
            // 
            // colBidAskRelation
            // 
            this.colBidAskRelation.ColumnEdit = this.repositoryItemProgressBar1;
            this.colBidAskRelation.DisplayFormat.FormatString = "0.#";
            this.colBidAskRelation.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBidAskRelation.FieldName = "BidAskRelation";
            this.colBidAskRelation.Name = "colBidAskRelation";
            this.colBidAskRelation.OptionsColumn.ReadOnly = true;
            this.colBidAskRelation.Visible = true;
            this.colBidAskRelation.VisibleIndex = 9;
            // 
            // repositoryItemProgressBar1
            // 
            this.repositoryItemProgressBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.repositoryItemProgressBar1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.repositoryItemProgressBar1.Appearance.ForeColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.repositoryItemProgressBar1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.repositoryItemProgressBar1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.repositoryItemProgressBar1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
            this.repositoryItemProgressBar1.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.repositoryItemProgressBar1.ShowTitle = true;
            this.repositoryItemProgressBar1.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            // 
            // colTime
            // 
            this.colTime.DisplayFormat.FormatString = "MM.dd hh:mm:ss.fff";
            this.colTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTime.FieldName = "Time";
            this.colTime.Name = "colTime";
            this.colTime.Visible = true;
            this.colTime.VisibleIndex = 0;
            // 
            // colTradeInfo
            // 
            this.colTradeInfo.FieldName = "TradeInfo";
            this.colTradeInfo.Name = "colTradeInfo";
            // 
            // colMinBuyPrice
            // 
            this.colMinBuyPrice.DisplayFormat.FormatString = "0.00000000";
            this.colMinBuyPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMinBuyPrice.FieldName = "MinBuyPrice";
            this.colMinBuyPrice.Name = "colMinBuyPrice";
            this.colMinBuyPrice.OptionsColumn.ReadOnly = true;
            this.colMinBuyPrice.Visible = true;
            this.colMinBuyPrice.VisibleIndex = 10;
            // 
            // colMaxBuyPrice
            // 
            this.colMaxBuyPrice.DisplayFormat.FormatString = "0.00000000";
            this.colMaxBuyPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMaxBuyPrice.FieldName = "MaxBuyPrice";
            this.colMaxBuyPrice.Name = "colMaxBuyPrice";
            this.colMaxBuyPrice.OptionsColumn.ReadOnly = true;
            this.colMaxBuyPrice.Visible = true;
            this.colMaxBuyPrice.VisibleIndex = 11;
            // 
            // colBuyAmount
            // 
            this.colBuyAmount.DisplayFormat.FormatString = "0.00000000";
            this.colBuyAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBuyAmount.FieldName = "BuyAmount";
            this.colBuyAmount.Name = "colBuyAmount";
            this.colBuyAmount.OptionsColumn.ReadOnly = true;
            this.colBuyAmount.Visible = true;
            this.colBuyAmount.VisibleIndex = 12;
            // 
            // colBuyVolume
            // 
            this.colBuyVolume.DisplayFormat.FormatString = "0.00000000";
            this.colBuyVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBuyVolume.FieldName = "BuyVolume";
            this.colBuyVolume.Name = "colBuyVolume";
            this.colBuyVolume.OptionsColumn.ReadOnly = true;
            this.colBuyVolume.Visible = true;
            this.colBuyVolume.VisibleIndex = 13;
            // 
            // colMinSellPrice
            // 
            this.colMinSellPrice.DisplayFormat.FormatString = "0.00000000";
            this.colMinSellPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMinSellPrice.FieldName = "MinSellPrice";
            this.colMinSellPrice.Name = "colMinSellPrice";
            this.colMinSellPrice.OptionsColumn.ReadOnly = true;
            this.colMinSellPrice.Visible = true;
            this.colMinSellPrice.VisibleIndex = 14;
            // 
            // colMaxSellPrice
            // 
            this.colMaxSellPrice.DisplayFormat.FormatString = "0.00000000";
            this.colMaxSellPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMaxSellPrice.FieldName = "MaxSellPrice";
            this.colMaxSellPrice.Name = "colMaxSellPrice";
            this.colMaxSellPrice.OptionsColumn.ReadOnly = true;
            this.colMaxSellPrice.Visible = true;
            this.colMaxSellPrice.VisibleIndex = 15;
            // 
            // colSellAmount
            // 
            this.colSellAmount.DisplayFormat.FormatString = "0.00000000";
            this.colSellAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSellAmount.FieldName = "SellAmount";
            this.colSellAmount.Name = "colSellAmount";
            this.colSellAmount.OptionsColumn.ReadOnly = true;
            this.colSellAmount.Visible = true;
            this.colSellAmount.VisibleIndex = 16;
            // 
            // colSellVolume
            // 
            this.colSellVolume.DisplayFormat.FormatString = "0.00000000";
            this.colSellVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSellVolume.FieldName = "SellVolume";
            this.colSellVolume.Name = "colSellVolume";
            this.colSellVolume.OptionsColumn.ReadOnly = true;
            this.colSellVolume.Visible = true;
            this.colSellVolume.VisibleIndex = 17;
            // 
            // colBidEnergy
            // 
            this.colBidEnergy.Caption = "Bid Energy";
            this.colBidEnergy.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBidEnergy.FieldName = "BidEnergy";
            this.colBidEnergy.Name = "colBidEnergy";
            this.colBidEnergy.Visible = true;
            this.colBidEnergy.VisibleIndex = 18;
            // 
            // colAskEnergy
            // 
            this.colAskEnergy.Caption = "Ask Enery";
            this.colAskEnergy.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAskEnergy.FieldName = "AskEnergy";
            this.colAskEnergy.Name = "colAskEnergy";
            this.colAskEnergy.Visible = true;
            this.colAskEnergy.VisibleIndex = 19;
            // 
            // colHipe
            // 
            this.colHipe.Caption = "BidHipe";
            this.colHipe.FieldName = "BidHipe";
            this.colHipe.Name = "colHipe";
            this.colHipe.Visible = true;
            this.colHipe.VisibleIndex = 20;
            // 
            // colHipe
            // 
            this.colSellHipe.Caption = "AskHipe";
            this.colSellHipe.FieldName = "AskHipe";
            this.colSellHipe.Name = "colSellHipe";
            this.colSellHipe.Visible = true;
            this.colSellHipe.VisibleIndex = 21;
            // 
            // OrderBookVolumeHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1680, 753);
            this.Controls.Add(this.tradeGridControl);
            this.Name = "OrderBookVolumeHistoryForm";
            this.Text = "OrderBookVolumeHistory";
            ((System.ComponentModel.ISupportInitialize)(this.tradeGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderBookVolumeHistoryItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl tradeGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource orderBookVolumeHistoryItemBindingSource;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;
        private DevExpress.XtraGrid.Columns.GridColumn colLowestAsk;
        private DevExpress.XtraGrid.Columns.GridColumn colHighestBid;
        private DevExpress.XtraGrid.Columns.GridColumn colBidVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colAskVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colBidExpectation;
        private DevExpress.XtraGrid.Columns.GridColumn colAskExpectation;
        private DevExpress.XtraGrid.Columns.GridColumn colBidDispersion;
        private DevExpress.XtraGrid.Columns.GridColumn colAskDispersion;
        private DevExpress.XtraGrid.Columns.GridColumn colBidAskRelation;
        private DevExpress.XtraGrid.Columns.GridColumn colTime;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeInfo;
        private DevExpress.XtraGrid.Columns.GridColumn colMinBuyPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colMaxBuyPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colBuyAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colBuyVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colMinSellPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colMaxSellPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colSellAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colSellVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colBidEnergy;
        private DevExpress.XtraGrid.Columns.GridColumn colAskEnergy;
        private DevExpress.XtraGrid.Columns.GridColumn colHipe;
        private DevExpress.XtraGrid.Columns.GridColumn colSellHipe;
    }
}