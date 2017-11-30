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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue2 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule3 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue3 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule4 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue4 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFill = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.biSell = new DevExpress.XtraBars.BarButtonItem();
            this.bePrice = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.beAmount = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.biSellMarket = new DevExpress.XtraBars.BarButtonItem();
            this.bbTrailing = new DevExpress.XtraBars.BarButtonItem();
            this.rpPoloniex = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.tickerChartViewer1 = new CryptoMarketClient.TickerChartViewer();
            this.tickerInfoControl = new CryptoMarketClient.TickerInfo();
            this.orderBookControl1 = new CryptoMarketClient.OrderBookControl();
            this.gcTrades = new DevExpress.XtraGrid.GridControl();
            this.tradeHistoryItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvTrades = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dpOrderBook = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dpTrades = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dpInfo = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel3_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dpOpenedOrders = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.gcOpenedOrders = new DevExpress.XtraGrid.GridControl();
            this.gvOrders = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.openedOrderInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMarket = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotal1 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTrades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tradeHistoryItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTrades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dpOrderBook.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.dpTrades.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            this.dpInfo.SuspendLayout();
            this.dockPanel3_Container.SuspendLayout();
            this.dpOpenedOrders.SuspendLayout();
            this.controlContainer1.SuspendLayout();
            this.panelContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcOpenedOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.openedOrderInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // colType
            // 
            this.colType.FieldName = "Type";
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 1;
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
            this.colAmount.VisibleIndex = 2;
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
            this.biSellMarket,
            this.bbTrailing});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Margin = new System.Windows.Forms.Padding(6);
            this.ribbonControl1.MaxItemId = 6;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpPoloniex});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemButtonEdit1});
            this.ribbonControl1.Size = new System.Drawing.Size(2075, 278);
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
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "All", 50, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repositoryItemButtonEdit1.DisplayFormat.FormatString = "0.########";
            this.repositoryItemButtonEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemButtonEdit1.EditFormat.FormatString = "0.########";
            this.repositoryItemButtonEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemButtonEdit1.Mask.EditMask = "f8";
            this.repositoryItemButtonEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
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
            // bbTrailing
            // 
            this.bbTrailing.Caption = "Sell Trailing";
            this.bbTrailing.Id = 5;
            this.bbTrailing.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbTrailing.ImageOptions.Image")));
            this.bbTrailing.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbTrailing.ImageOptions.LargeImage")));
            this.bbTrailing.Name = "bbTrailing";
            this.bbTrailing.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbTrailing_ItemClick);
            // 
            // rpPoloniex
            // 
            this.rpPoloniex.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup2,
            this.ribbonPageGroup1});
            this.rpPoloniex.Name = "rpPoloniex";
            this.rpPoloniex.Text = "Poloniex";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Buy";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.AllowTextClipping = false;
            this.ribbonPageGroup1.ItemLinks.Add(this.bePrice);
            this.ribbonPageGroup1.ItemLinks.Add(this.beAmount);
            this.ribbonPageGroup1.ItemLinks.Add(this.biSell);
            this.ribbonPageGroup1.ItemLinks.Add(this.biSellMarket);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbTrailing);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.ShowCaptionButton = false;
            this.ribbonPageGroup1.Text = "Sell";
            // 
            // tickerChartViewer1
            // 
            this.tickerChartViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tickerChartViewer1.Location = new System.Drawing.Point(393, 438);
            this.tickerChartViewer1.Margin = new System.Windows.Forms.Padding(14, 13, 14, 13);
            this.tickerChartViewer1.Name = "tickerChartViewer1";
            this.tickerChartViewer1.Size = new System.Drawing.Size(1110, 727);
            this.tickerChartViewer1.TabIndex = 0;
            this.tickerChartViewer1.Ticker = null;
            // 
            // tickerInfoControl
            // 
            this.tickerInfoControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.tickerInfoControl.Location = new System.Drawing.Point(0, 0);
            this.tickerInfoControl.Margin = new System.Windows.Forms.Padding(14, 13, 14, 13);
            this.tickerInfoControl.Name = "tickerInfoControl";
            this.tickerInfoControl.Size = new System.Drawing.Size(1094, 108);
            this.tickerInfoControl.TabIndex = 0;
            this.tickerInfoControl.Ticker = null;
            // 
            // orderBookControl1
            // 
            this.orderBookControl1.Asks = null;
            this.orderBookControl1.Bids = null;
            this.orderBookControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderBookControl1.Location = new System.Drawing.Point(0, 0);
            this.orderBookControl1.Margin = new System.Windows.Forms.Padding(2);
            this.orderBookControl1.Name = "orderBookControl1";
            this.orderBookControl1.OrderBookCaption = "";
            this.orderBookControl1.Size = new System.Drawing.Size(372, 833);
            this.orderBookControl1.TabIndex = 3;
            this.orderBookControl1.TickerCollection = null;
            // 
            // gcTrades
            // 
            this.gcTrades.DataSource = this.tradeHistoryItemBindingSource;
            this.gcTrades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTrades.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.gcTrades.Location = new System.Drawing.Point(0, 0);
            this.gcTrades.MainView = this.gvTrades;
            this.gcTrades.Margin = new System.Windows.Forms.Padding(6);
            this.gcTrades.MenuManager = this.ribbonControl1;
            this.gcTrades.Name = "gcTrades";
            this.gcTrades.Size = new System.Drawing.Size(551, 385);
            this.gcTrades.TabIndex = 0;
            this.gcTrades.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTrades});
            // 
            // tradeHistoryItemBindingSource
            // 
            this.tradeHistoryItemBindingSource.DataSource = typeof(CryptoMarketClient.TradeHistoryItem);
            // 
            // gvTrades
            // 
            this.gvTrades.Appearance.Row.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gvTrades.Appearance.Row.Options.UseForeColor = true;
            this.gvTrades.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gvTrades.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTime,
            this.colAmount,
            this.colRate,
            this.colTotal,
            this.colFill,
            this.colType,
            this.colId});
            gridFormatRule1.Column = this.colType;
            gridFormatRule1.ColumnApplyTo = this.colType;
            gridFormatRule1.Name = "FormatRulesTradeBuy";
            formatConditionRuleValue1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            formatConditionRuleValue1.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.Value1 = CryptoMarketClient.TradeType.Buy;
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.Column = this.colType;
            gridFormatRule2.ColumnApplyTo = this.colType;
            gridFormatRule2.Name = "FormatRulesTradeSell";
            formatConditionRuleValue2.Appearance.ForeColor = System.Drawing.Color.Red;
            formatConditionRuleValue2.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue2.Value1 = CryptoMarketClient.TradeType.Sell;
            gridFormatRule2.Rule = formatConditionRuleValue2;
            this.gvTrades.FormatRules.Add(gridFormatRule1);
            this.gvTrades.FormatRules.Add(gridFormatRule2);
            this.gvTrades.GridControl = this.gcTrades;
            this.gvTrades.Name = "gvTrades";
            this.gvTrades.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gvTrades.OptionsBehavior.Editable = false;
            this.gvTrades.OptionsDetail.EnableMasterViewMode = false;
            this.gvTrades.OptionsView.ShowGroupPanel = false;
            this.gvTrades.OptionsView.ShowIndicator = false;
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
            this.colRate.VisibleIndex = 3;
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
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dpOrderBook,
            this.panelContainer1,
            this.dpInfo});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl"});
            // 
            // dpOrderBook
            // 
            this.dpOrderBook.Controls.Add(this.dockPanel1_Container);
            this.dpOrderBook.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dpOrderBook.ID = new System.Guid("09e5b5a5-8f81-4084-ad97-5cb8e0447355");
            this.dpOrderBook.Location = new System.Drawing.Point(0, 278);
            this.dpOrderBook.Margin = new System.Windows.Forms.Padding(4);
            this.dpOrderBook.Name = "dpOrderBook";
            this.dpOrderBook.OriginalSize = new System.Drawing.Size(393, 200);
            this.dpOrderBook.Size = new System.Drawing.Size(393, 887);
            this.dpOrderBook.Text = "Order Book";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.orderBookControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(8, 46);
            this.dockPanel1_Container.Margin = new System.Windows.Forms.Padding(4);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(372, 833);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // dpTrades
            // 
            this.dpTrades.Controls.Add(this.dockPanel2_Container);
            this.dpTrades.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpTrades.ID = new System.Guid("cecd3d8a-da7f-4a81-90ca-fe5b5710f049");
            this.dpTrades.Location = new System.Drawing.Point(0, 0);
            this.dpTrades.Margin = new System.Windows.Forms.Padding(4);
            this.dpTrades.Name = "dpTrades";
            this.dpTrades.OriginalSize = new System.Drawing.Size(200, 200);
            this.dpTrades.Size = new System.Drawing.Size(572, 444);
            this.dpTrades.Text = "Trades";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.gcTrades);
            this.dockPanel2_Container.Location = new System.Drawing.Point(13, 46);
            this.dockPanel2_Container.Margin = new System.Windows.Forms.Padding(4);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(551, 385);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // dpInfo
            // 
            this.dpInfo.Controls.Add(this.dockPanel3_Container);
            this.dpInfo.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top;
            this.dpInfo.ID = new System.Guid("7b677dba-e9f9-46a7-b2ee-3f7b60447fd7");
            this.dpInfo.Location = new System.Drawing.Point(393, 278);
            this.dpInfo.Margin = new System.Windows.Forms.Padding(4);
            this.dpInfo.Name = "dpInfo";
            this.dpInfo.OriginalSize = new System.Drawing.Size(200, 160);
            this.dpInfo.Size = new System.Drawing.Size(1110, 160);
            this.dpInfo.Text = "Info";
            // 
            // dockPanel3_Container
            // 
            this.dockPanel3_Container.Controls.Add(this.tickerInfoControl);
            this.dockPanel3_Container.Location = new System.Drawing.Point(8, 46);
            this.dockPanel3_Container.Margin = new System.Windows.Forms.Padding(4);
            this.dockPanel3_Container.Name = "dockPanel3_Container";
            this.dockPanel3_Container.Size = new System.Drawing.Size(1094, 101);
            this.dockPanel3_Container.TabIndex = 0;
            // 
            // dpOpenedOrders
            // 
            this.dpOpenedOrders.Controls.Add(this.controlContainer1);
            this.dpOpenedOrders.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpOpenedOrders.ID = new System.Guid("147661ac-3074-41c2-a188-4f22f789a286");
            this.dpOpenedOrders.Location = new System.Drawing.Point(0, 444);
            this.dpOpenedOrders.Name = "dpOpenedOrders";
            this.dpOpenedOrders.OriginalSize = new System.Drawing.Size(200, 200);
            this.dpOpenedOrders.Size = new System.Drawing.Size(572, 443);
            this.dpOpenedOrders.Text = "Opened Orders";
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.gcOpenedOrders);
            this.controlContainer1.Location = new System.Drawing.Point(13, 46);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(551, 389);
            this.controlContainer1.TabIndex = 0;
            // 
            // panelContainer1
            // 
            this.panelContainer1.Controls.Add(this.dpTrades);
            this.panelContainer1.Controls.Add(this.dpOpenedOrders);
            this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.panelContainer1.ID = new System.Guid("f36ee932-9441-414c-acdf-e2e9486b0e3d");
            this.panelContainer1.Location = new System.Drawing.Point(1503, 278);
            this.panelContainer1.Name = "panelContainer1";
            this.panelContainer1.OriginalSize = new System.Drawing.Size(572, 200);
            this.panelContainer1.Size = new System.Drawing.Size(572, 887);
            this.panelContainer1.Text = "panelContainer1";
            // 
            // gcOpenedOrders
            // 
            this.gcOpenedOrders.DataSource = this.openedOrderInfoBindingSource;
            this.gcOpenedOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcOpenedOrders.Location = new System.Drawing.Point(0, 0);
            this.gcOpenedOrders.MainView = this.gvOrders;
            this.gcOpenedOrders.MenuManager = this.ribbonControl1;
            this.gcOpenedOrders.Name = "gcOpenedOrders";
            this.gcOpenedOrders.Size = new System.Drawing.Size(551, 389);
            this.gcOpenedOrders.TabIndex = 0;
            this.gcOpenedOrders.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOrders});
            // 
            // gvOrders
            // 
            this.gvOrders.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDate,
            this.colMarket,
            this.colOrderNumber,
            this.colType1,
            this.colValue,
            this.colAmount1,
            this.colTotal1});
            gridFormatRule3.Column = this.colType1;
            gridFormatRule3.ColumnApplyTo = this.colType1;
            gridFormatRule3.Name = "FormatSell";
            formatConditionRuleValue3.Appearance.ForeColor = System.Drawing.Color.Red;
            formatConditionRuleValue3.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue3.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue3.Value1 = 1;
            gridFormatRule3.Rule = formatConditionRuleValue3;
            gridFormatRule4.Column = this.colType1;
            gridFormatRule4.ColumnApplyTo = this.colType1;
            gridFormatRule4.Name = "FormatBuy";
            formatConditionRuleValue4.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue4.Value1 = 0;
            gridFormatRule4.Rule = formatConditionRuleValue4;
            this.gvOrders.FormatRules.Add(gridFormatRule3);
            this.gvOrders.FormatRules.Add(gridFormatRule4);
            this.gvOrders.GridControl = this.gcOpenedOrders;
            this.gvOrders.Name = "gvOrders";
            this.gvOrders.OptionsBehavior.Editable = false;
            this.gvOrders.OptionsView.ShowGroupPanel = false;
            this.gvOrders.OptionsView.ShowIndicator = false;
            // 
            // openedOrderInfoBindingSource
            // 
            this.openedOrderInfoBindingSource.DataSource = typeof(CryptoMarketClient.Common.OpenedOrderInfo);
            // 
            // colDate
            // 
            this.colDate.DisplayFormat.FormatString = "yyyy.MM.dd hh:mm:ss.fff";
            this.colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDate.FieldName = "Date";
            this.colDate.Name = "colDate";
            this.colDate.Visible = true;
            this.colDate.VisibleIndex = 4;
            // 
            // colMarket
            // 
            this.colMarket.FieldName = "Market";
            this.colMarket.Name = "colMarket";
            // 
            // colOrderNumber
            // 
            this.colOrderNumber.FieldName = "OrderNumber";
            this.colOrderNumber.Name = "colOrderNumber";
            // 
            // colType1
            // 
            this.colType1.FieldName = "Type";
            this.colType1.Name = "colType1";
            this.colType1.Visible = true;
            this.colType1.VisibleIndex = 0;
            // 
            // colValue
            // 
            this.colValue.Caption = "Price";
            this.colValue.DisplayFormat.FormatString = "0.########";
            this.colValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colValue.FieldName = "Value";
            this.colValue.Name = "colValue";
            this.colValue.Visible = true;
            this.colValue.VisibleIndex = 1;
            // 
            // colAmount1
            // 
            this.colAmount1.DisplayFormat.FormatString = "0.########";
            this.colAmount1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount1.FieldName = "Amount";
            this.colAmount1.Name = "colAmount1";
            this.colAmount1.Visible = true;
            this.colAmount1.VisibleIndex = 2;
            // 
            // colTotal1
            // 
            this.colTotal1.DisplayFormat.FormatString = "0.########";
            this.colTotal1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotal1.FieldName = "Total";
            this.colTotal1.Name = "colTotal1";
            this.colTotal1.Visible = true;
            this.colTotal1.VisibleIndex = 3;
            // 
            // TickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2075, 1165);
            this.Controls.Add(this.tickerChartViewer1);
            this.Controls.Add(this.dpInfo);
            this.Controls.Add(this.panelContainer1);
            this.Controls.Add(this.dpOrderBook);
            this.Controls.Add(this.ribbonControl1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "TickerForm";
            this.Text = "Ticker Form";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTrades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tradeHistoryItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTrades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dpOrderBook.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.dpTrades.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            this.dpInfo.ResumeLayout(false);
            this.dockPanel3_Container.ResumeLayout(false);
            this.dpOpenedOrders.ResumeLayout(false);
            this.controlContainer1.ResumeLayout(false);
            this.panelContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcOpenedOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.openedOrderInfoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpPoloniex;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private TickerInfo tickerInfoControl;
        private DevExpress.XtraGrid.GridControl gcTrades;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTrades;
        private System.Windows.Forms.BindingSource tradeHistoryItemBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colTime;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colRate;
        private DevExpress.XtraGrid.Columns.GridColumn colTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colFill;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private TickerChartViewer tickerChartViewer1;
        private OrderBookControl orderBookControl1;
        private DevExpress.XtraBars.BarButtonItem biSell;
        private DevExpress.XtraBars.BarEditItem bePrice;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarEditItem beAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraBars.BarButtonItem biSellMarket;
        private DevExpress.XtraBars.BarButtonItem bbTrailing;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dpTrades;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraBars.Docking.DockPanel dpOrderBook;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.Docking.DockPanel dpInfo;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel3_Container;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.Docking.DockPanel panelContainer1;
        private DevExpress.XtraBars.Docking.DockPanel dpOpenedOrders;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private DevExpress.XtraGrid.GridControl gcOpenedOrders;
        private DevExpress.XtraGrid.Views.Grid.GridView gvOrders;
        private System.Windows.Forms.BindingSource openedOrderInfoBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colDate;
        private DevExpress.XtraGrid.Columns.GridColumn colMarket;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colType1;
        private DevExpress.XtraGrid.Columns.GridColumn colValue;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount1;
        private DevExpress.XtraGrid.Columns.GridColumn colTotal1;
    }
}