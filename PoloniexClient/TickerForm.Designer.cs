namespace CryptoMarketClient {
    partial class TickerForm {
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
                DisposeCore();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TickerForm));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule4 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue3 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule5 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue4 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule6 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet2 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet2 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon4 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon5 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon6 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFill = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.biSell = new DevExpress.XtraBars.BarButtonItem();
            this.bePrice = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.beAmount = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rpPoloniex = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.sidePanel1 = new DevExpress.XtraEditors.SidePanel();
            this.sidePanel3 = new DevExpress.XtraEditors.SidePanel();
            this.tickerChartViewer1 = new CryptoMarketClient.TickerChartViewer();
            this.tickerInfoControl = new CryptoMarketClient.TickerInfo();
            this.orderBookControl1 = new CryptoMarketClient.OrderBookControl();
            this.sidePanel2 = new DevExpress.XtraEditors.SidePanel();
            this.tradeGridControl = new DevExpress.XtraGrid.GridControl();
            this.tradeHistoryItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAsk = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.orderBookPanel = new DevExpress.XtraEditors.SidePanel();
            this.biSellMarket = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            this.sidePanel1.SuspendLayout();
            this.sidePanel3.SuspendLayout();
            this.sidePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tradeGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tradeHistoryItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.orderBookPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // colType
            // 
            this.colType.FieldName = "Type";
            this.colType.Name = "colType";
            // 
            // colFill
            // 
            this.colFill.FieldName = "Fill";
            this.colFill.Name = "colFill";
            // 
            // colAmount
            // 
            this.colAmount.DisplayFormat.FormatString = "0.########";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 1;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.AutoSizeItems = true;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.biSell,
            this.bePrice,
            this.beAmount,
            this.biSellMarket});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Margin = new System.Windows.Forms.Padding(6);
            this.ribbonControl1.MaxItemId = 5;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpPoloniex});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemButtonEdit1});
            this.ribbonControl1.Size = new System.Drawing.Size(2266, 278);
            // 
            // biSell
            // 
            this.biSell.Caption = "Sell";
            this.biSell.Id = 1;
            this.biSell.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("biSell.ImageOptions.Image")));
            this.biSell.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("biSell.ImageOptions.LargeImage")));
            this.biSell.Name = "biSell";
            this.biSell.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.biSell.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biSell_ItemClick);
            // 
            // bePrice
            // 
            this.bePrice.Caption = "Price";
            this.bePrice.Edit = this.repositoryItemTextEdit1;
            this.bePrice.EditWidth = 180;
            this.bePrice.Id = 2;
            this.bePrice.Name = "bePrice";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.DisplayFormat.FormatString = "0.########";
            this.repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit1.EditFormat.FormatString = "0.########";
            this.repositoryItemTextEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit1.Mask.EditMask = "f8";
            this.repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // beAmount
            // 
            this.beAmount.Caption = "Amount";
            this.beAmount.Edit = this.repositoryItemButtonEdit1;
            this.beAmount.EditWidth = 180;
            this.beAmount.Id = 3;
            this.beAmount.Name = "beAmount";
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "All", 50, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repositoryItemButtonEdit1.DisplayFormat.FormatString = "0.########";
            this.repositoryItemButtonEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemButtonEdit1.EditFormat.FormatString = "0.########";
            this.repositoryItemButtonEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemButtonEdit1.Mask.EditMask = "f8";
            this.repositoryItemButtonEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // rpPoloniex
            // 
            this.rpPoloniex.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.rpPoloniex.Name = "rpPoloniex";
            this.rpPoloniex.Text = "Poloniex";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.AllowTextClipping = false;
            this.ribbonPageGroup1.ItemLinks.Add(this.bePrice);
            this.ribbonPageGroup1.ItemLinks.Add(this.beAmount);
            this.ribbonPageGroup1.ItemLinks.Add(this.biSell);
            this.ribbonPageGroup1.ItemLinks.Add(this.biSellMarket);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.ShowCaptionButton = false;
            this.ribbonPageGroup1.Text = "Sell";
            // 
            // sidePanel1
            // 
            this.sidePanel1.Controls.Add(this.sidePanel3);
            this.sidePanel1.Controls.Add(this.tickerInfoControl);
            this.sidePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidePanel1.Location = new System.Drawing.Point(490, 278);
            this.sidePanel1.Margin = new System.Windows.Forms.Padding(6);
            this.sidePanel1.Name = "sidePanel1";
            this.sidePanel1.Size = new System.Drawing.Size(1220, 630);
            this.sidePanel1.TabIndex = 1;
            this.sidePanel1.Text = "sidePanel1";
            // 
            // sidePanel3
            // 
            this.sidePanel3.Controls.Add(this.tickerChartViewer1);
            this.sidePanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidePanel3.Location = new System.Drawing.Point(0, 208);
            this.sidePanel3.Margin = new System.Windows.Forms.Padding(6);
            this.sidePanel3.Name = "sidePanel3";
            this.sidePanel3.Size = new System.Drawing.Size(1220, 422);
            this.sidePanel3.TabIndex = 2;
            this.sidePanel3.Text = "sidePanel3";
            // 
            // tickerChartViewer1
            // 
            this.tickerChartViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tickerChartViewer1.Location = new System.Drawing.Point(0, 0);
            this.tickerChartViewer1.Margin = new System.Windows.Forms.Padding(14);
            this.tickerChartViewer1.Name = "tickerChartViewer1";
            this.tickerChartViewer1.Size = new System.Drawing.Size(1220, 422);
            this.tickerChartViewer1.TabIndex = 0;
            this.tickerChartViewer1.Ticker = null;
            // 
            // tickerInfoControl
            // 
            this.tickerInfoControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.tickerInfoControl.Location = new System.Drawing.Point(0, 0);
            this.tickerInfoControl.Margin = new System.Windows.Forms.Padding(14);
            this.tickerInfoControl.Name = "tickerInfoControl";
            this.tickerInfoControl.Size = new System.Drawing.Size(1220, 208);
            this.tickerInfoControl.TabIndex = 0;
            this.tickerInfoControl.Ticker = null;
            // 
            // orderBookControl1
            // 
            this.orderBookControl1.Asks = null;
            this.orderBookControl1.Bids = null;
            this.orderBookControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderBookControl1.Location = new System.Drawing.Point(0, 0);
            this.orderBookControl1.Name = "orderBookControl1";
            this.orderBookControl1.OrderBookCaption = "";
            this.orderBookControl1.Size = new System.Drawing.Size(489, 630);
            this.orderBookControl1.TabIndex = 3;
            this.orderBookControl1.TickerCollection = null;
            // 
            // sidePanel2
            // 
            this.sidePanel2.Controls.Add(this.tradeGridControl);
            this.sidePanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.sidePanel2.Location = new System.Drawing.Point(1710, 278);
            this.sidePanel2.Margin = new System.Windows.Forms.Padding(6);
            this.sidePanel2.Name = "sidePanel2";
            this.sidePanel2.Size = new System.Drawing.Size(556, 630);
            this.sidePanel2.TabIndex = 3;
            this.sidePanel2.Text = "sidePanel2";
            // 
            // tradeGridControl
            // 
            this.tradeGridControl.DataSource = this.tradeHistoryItemBindingSource;
            this.tradeGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tradeGridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.tradeGridControl.Location = new System.Drawing.Point(1, 0);
            this.tradeGridControl.MainView = this.gridView1;
            this.tradeGridControl.Margin = new System.Windows.Forms.Padding(6);
            this.tradeGridControl.MenuManager = this.ribbonControl1;
            this.tradeGridControl.Name = "tradeGridControl";
            this.tradeGridControl.Size = new System.Drawing.Size(555, 630);
            this.tradeGridControl.TabIndex = 0;
            this.tradeGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // tradeHistoryItemBindingSource
            // 
            this.tradeHistoryItemBindingSource.DataSource = typeof(CryptoMarketClient.TradeHistoryItem);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.Row.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridView1.Appearance.Row.Options.UseForeColor = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTime,
            this.colAmount,
            this.colRate,
            this.colTotal,
            this.colFill,
            this.colType,
            this.colId,
            this.colBid,
            this.colAsk,
            this.colCurrent});
            gridFormatRule4.ApplyToRow = true;
            gridFormatRule4.Column = this.colType;
            gridFormatRule4.Name = "FormatRulesTradeBuy";
            formatConditionRuleValue3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            formatConditionRuleValue3.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue3.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue3.Value1 = CryptoMarketClient.TradeType.Buy;
            gridFormatRule4.Rule = formatConditionRuleValue3;
            gridFormatRule5.ApplyToRow = true;
            gridFormatRule5.Column = this.colType;
            gridFormatRule5.Name = "FormatRulesTradeSell";
            formatConditionRuleValue4.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            formatConditionRuleValue4.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue4.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue4.Value1 = CryptoMarketClient.TradeType.Sell;
            gridFormatRule5.Rule = formatConditionRuleValue4;
            gridFormatRule6.Column = this.colFill;
            gridFormatRule6.ColumnApplyTo = this.colAmount;
            gridFormatRule6.Name = "FormatRulesFillType";
            formatConditionIconSet2.CategoryName = "Ratings";
            formatConditionIconSetIcon4.PredefinedName = "Stars3_1.png";
            formatConditionIconSetIcon4.Value = new decimal(new int[] {
            67,
            0,
            0,
            0});
            formatConditionIconSetIcon4.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon5.PredefinedName = "Stars3_2.png";
            formatConditionIconSetIcon5.Value = new decimal(new int[] {
            33,
            0,
            0,
            0});
            formatConditionIconSetIcon5.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon6.PredefinedName = "Stars3_3.png";
            formatConditionIconSetIcon6.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSet2.Icons.Add(formatConditionIconSetIcon4);
            formatConditionIconSet2.Icons.Add(formatConditionIconSetIcon5);
            formatConditionIconSet2.Icons.Add(formatConditionIconSetIcon6);
            formatConditionIconSet2.Name = "Stars3";
            formatConditionIconSet2.ValueType = DevExpress.XtraEditors.FormatConditionValueType.Percent;
            formatConditionRuleIconSet2.IconSet = formatConditionIconSet2;
            gridFormatRule6.Rule = formatConditionRuleIconSet2;
            this.gridView1.FormatRules.Add(gridFormatRule4);
            this.gridView1.FormatRules.Add(gridFormatRule5);
            this.gridView1.FormatRules.Add(gridFormatRule6);
            this.gridView1.GridControl = this.tradeGridControl;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            // 
            // colTime
            // 
            this.colTime.DisplayFormat.FormatString = "hh:mm:ss.fff";
            this.colTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTime.FieldName = "Time";
            this.colTime.Name = "colTime";
            this.colTime.Visible = true;
            this.colTime.VisibleIndex = 0;
            // 
            // colRate
            // 
            this.colRate.DisplayFormat.FormatString = "0.########";
            this.colRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colRate.FieldName = "Rate";
            this.colRate.Name = "colRate";
            this.colRate.Visible = true;
            this.colRate.VisibleIndex = 2;
            // 
            // colTotal
            // 
            this.colTotal.FieldName = "Total";
            this.colTotal.Name = "colTotal";
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            this.colId.Visible = true;
            this.colId.VisibleIndex = 3;
            // 
            // colBid
            // 
            this.colBid.Caption = "Bid";
            this.colBid.DisplayFormat.FormatString = "0.########";
            this.colBid.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBid.FieldName = "Bid";
            this.colBid.Name = "colBid";
            this.colBid.Visible = true;
            this.colBid.VisibleIndex = 4;
            // 
            // colAsk
            // 
            this.colAsk.Caption = "Ask";
            this.colAsk.DisplayFormat.FormatString = "0.########";
            this.colAsk.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAsk.FieldName = "Ask";
            this.colAsk.Name = "colAsk";
            this.colAsk.Visible = true;
            this.colAsk.VisibleIndex = 5;
            // 
            // colCurrent
            // 
            this.colCurrent.Caption = "Current";
            this.colCurrent.DisplayFormat.FormatString = "0.########";
            this.colCurrent.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCurrent.FieldName = "Current";
            this.colCurrent.Name = "colCurrent";
            this.colCurrent.Visible = true;
            this.colCurrent.VisibleIndex = 6;
            // 
            // orderBookPanel
            // 
            this.orderBookPanel.Controls.Add(this.orderBookControl1);
            this.orderBookPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.orderBookPanel.Location = new System.Drawing.Point(0, 278);
            this.orderBookPanel.Margin = new System.Windows.Forms.Padding(6);
            this.orderBookPanel.Name = "orderBookPanel";
            this.orderBookPanel.Size = new System.Drawing.Size(490, 630);
            this.orderBookPanel.TabIndex = 1;
            this.orderBookPanel.Text = "sidePanel2";
            // 
            // biSellMarket
            // 
            this.biSellMarket.Caption = "Sell Market";
            this.biSellMarket.Id = 4;
            this.biSellMarket.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("biSellMarket.ImageOptions.Image")));
            this.biSellMarket.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("biSellMarket.ImageOptions.LargeImage")));
            this.biSellMarket.Name = "biSellMarket";
            this.biSellMarket.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.biSellMarket.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biSellMarket_ItemClick);
            // 
            // TickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2266, 908);
            this.Controls.Add(this.sidePanel1);
            this.Controls.Add(this.sidePanel2);
            this.Controls.Add(this.orderBookPanel);
            this.Controls.Add(this.ribbonControl1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "TickerForm";
            this.Text = "ProcessWMPaint Msg = 15  Time = 8";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            this.sidePanel1.ResumeLayout(false);
            this.sidePanel3.ResumeLayout(false);
            this.sidePanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tradeGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tradeHistoryItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.orderBookPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpPoloniex;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraEditors.SidePanel sidePanel1;
        private DevExpress.XtraEditors.SidePanel sidePanel3;
        private TickerInfo tickerInfoControl;
        private DevExpress.XtraEditors.SidePanel sidePanel2;
        private DevExpress.XtraGrid.GridControl tradeGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource tradeHistoryItemBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colTime;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colRate;
        private DevExpress.XtraGrid.Columns.GridColumn colTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colFill;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colBid;
        private DevExpress.XtraGrid.Columns.GridColumn colAsk;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrent;
        private TickerChartViewer tickerChartViewer1;
        private DevExpress.XtraEditors.SidePanel orderBookPanel;
        private OrderBookControl orderBookControl1;
        private DevExpress.XtraBars.BarButtonItem biSell;
        private DevExpress.XtraBars.BarEditItem bePrice;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarEditItem beAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraBars.BarButtonItem biSellMarket;
    }
}