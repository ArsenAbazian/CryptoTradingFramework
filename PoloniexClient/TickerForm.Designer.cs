using CryptoMarketClient.Common;

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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject10 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject11 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject12 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions4 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject13 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject14 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject15 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject16 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions5 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject17 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject18 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject19 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject20 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue2 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule3 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue3 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule4 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue4 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFill = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.biSell = new DevExpress.XtraBars.BarButtonItem();
            this.biSellMarket = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.rpMain = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.tickerChartViewer1 = new CryptoMarketClient.TickerChartViewer();
            this.tickerInfoControl = new CryptoMarketClient.TickerInfo();
            this.orderBookControl1 = new CryptoMarketClient.OrderBookControl();
            this.gcTrades = new CryptoMarketClient.MyGridControl();
            this.tradeHistoryItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvTrades = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bbCancel = new DevExpress.XtraBars.BarButtonItem();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dpOrderBook = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dpActiveTrailings = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer4 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.activeTrailingCollectionControl1 = new CryptoMarketClient.TrailingCollectionControl();
            this.dpBuy = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer2 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.buySettingsControl = new CryptoMarketClient.TradeSettingsControl();
            this.dpOpenedOrders = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.gcOpenedOrders = new CryptoMarketClient.MyGridControl();
            this.openedOrderInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvOpenedOrders = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMarket = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotal1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dpMyTrades = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer5 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.myTradesCollectionControl1 = new CryptoMarketClient.MyTradesCollectionControl();
            this.dpTrades = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dpInfo = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel3_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.tradingResultBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tradingResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTrades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tradeHistoryItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTrades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.dpOrderBook.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.panelContainer1.SuspendLayout();
            this.dpActiveTrailings.SuspendLayout();
            this.controlContainer4.SuspendLayout();
            this.dpBuy.SuspendLayout();
            this.controlContainer2.SuspendLayout();
            this.dpOpenedOrders.SuspendLayout();
            this.controlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcOpenedOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.openedOrderInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOpenedOrders)).BeginInit();
            this.dpMyTrades.SuspendLayout();
            this.controlContainer5.SuspendLayout();
            this.dpTrades.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            this.dpInfo.SuspendLayout();
            this.dockPanel3_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tradingResultBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tradingResultBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // colType
            // 
            this.colType.FieldName = "Type";
            this.colType.MinWidth = 10;
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 1;
            this.colType.Width = 37;
            // 
            // colType1
            // 
            this.colType1.FieldName = "Type";
            this.colType1.MinWidth = 10;
            this.colType1.Name = "colType1";
            this.colType1.Visible = true;
            this.colType1.VisibleIndex = 0;
            this.colType1.Width = 37;
            // 
            // colFill
            // 
            this.colFill.FieldName = "Fill";
            this.colFill.MinWidth = 10;
            this.colFill.Name = "colFill";
            this.colFill.Width = 37;
            // 
            // colAmount
            // 
            this.colAmount.FieldName = "AmountString";
            this.colAmount.MinWidth = 10;
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 2;
            this.colAmount.Width = 37;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.AutoSizeItems = true;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.biSell,
            this.biSellMarket,
            this.barButtonItem1});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ribbonControl1.MaxItemId = 9;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpMain});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemButtonEdit1,
            this.repositoryItemButtonEdit2});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.OfficeUniversal;
            this.ribbonControl1.Size = new System.Drawing.Size(1147, 52);
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
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Grab Data";
            this.barButtonItem1.Id = 8;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // rpMain
            // 
            this.rpMain.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.rpMain.Name = "rpMain";
            this.rpMain.Text = "Poloniex";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem1);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
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
            // repositoryItemButtonEdit2
            // 
            this.repositoryItemButtonEdit2.AutoHeight = false;
            this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "25%", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "50%", -1, true, true, false, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject9, serializableAppearanceObject10, serializableAppearanceObject11, serializableAppearanceObject12, "", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "75%", -1, true, true, false, editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject13, serializableAppearanceObject14, serializableAppearanceObject15, serializableAppearanceObject16, "", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "100%", -1, true, true, false, editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject17, serializableAppearanceObject18, serializableAppearanceObject19, serializableAppearanceObject20, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
            // 
            // tickerChartViewer1
            // 
            this.tickerChartViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tickerChartViewer1.Location = new System.Drawing.Point(742, 259);
            this.tickerChartViewer1.Margin = new System.Windows.Forms.Padding(14, 13, 14, 13);
            this.tickerChartViewer1.Name = "tickerChartViewer1";
            this.tickerChartViewer1.Size = new System.Drawing.Size(0, 251);
            this.tickerChartViewer1.TabIndex = 0;
            this.tickerChartViewer1.Ticker = null;
            // 
            // tickerInfoControl
            // 
            this.tickerInfoControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.tickerInfoControl.Location = new System.Drawing.Point(0, 0);
            this.tickerInfoControl.Margin = new System.Windows.Forms.Padding(14, 13, 14, 13);
            this.tickerInfoControl.Name = "tickerInfoControl";
            this.tickerInfoControl.Size = new System.Drawing.Size(0, 144);
            this.tickerInfoControl.TabIndex = 0;
            this.tickerInfoControl.Ticker = null;
            // 
            // orderBookControl1
            // 
            this.orderBookControl1.Asks = null;
            this.orderBookControl1.Bids = null;
            this.orderBookControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderBookControl1.Location = new System.Drawing.Point(0, 0);
            this.orderBookControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.orderBookControl1.Name = "orderBookControl1";
            this.orderBookControl1.OrderBookCaption = "";
            this.orderBookControl1.Size = new System.Drawing.Size(733, 431);
            this.orderBookControl1.TabIndex = 3;
            this.orderBookControl1.TickerCollection = null;
            this.orderBookControl1.SelectedAskRowChanged += new CryptoMarketClient.SelectedOrderBookEntryChangedHandler(this.orderBookControl1_SelectedAskRowChanged);
            this.orderBookControl1.SelectedBidRowChanged += new CryptoMarketClient.SelectedOrderBookEntryChangedHandler(this.orderBookControl1_SelectedBidRowChanged);
            // 
            // gcTrades
            // 
            this.gcTrades.DataSource = this.tradeHistoryItemBindingSource;
            this.gcTrades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTrades.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.gcTrades.Location = new System.Drawing.Point(0, 0);
            this.gcTrades.MainView = this.gvTrades;
            this.gcTrades.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.gcTrades.MenuManager = this.ribbonControl1;
            this.gcTrades.Name = "gcTrades";
            this.gcTrades.Size = new System.Drawing.Size(639, 404);
            this.gcTrades.TabIndex = 0;
            this.gcTrades.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.gcTrades.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTrades});
            // 
            // tradeHistoryItemBindingSource
            // 
            this.tradeHistoryItemBindingSource.DataSource = typeof(CryptoMarketClient.TradeHistoryItem);
            // 
            // gvTrades
            // 
            this.gvTrades.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 7.875F);
            this.gvTrades.Appearance.Row.Options.UseFont = true;
            this.gvTrades.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gvTrades.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTime,
            this.colAmount,
            this.colRate,
            this.colTotal,
            this.colFill,
            this.colType,
            this.colId});
            this.gvTrades.DetailHeight = 182;
            this.gvTrades.FixedLineWidth = 1;
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
            this.gvTrades.OptionsView.EnableAppearanceEvenRow = true;
            this.gvTrades.OptionsView.ShowGroupPanel = false;
            this.gvTrades.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvTrades.OptionsView.ShowIndicator = false;
            this.gvTrades.PreviewIndent = 0;
            // 
            // colTime
            // 
            this.colTime.DisplayFormat.FormatString = "hh:mm:ss.fff";
            this.colTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTime.FieldName = "Time";
            this.colTime.MinWidth = 10;
            this.colTime.Name = "colTime";
            this.colTime.Visible = true;
            this.colTime.VisibleIndex = 0;
            this.colTime.Width = 37;
            // 
            // colRate
            // 
            this.colRate.FieldName = "RateString";
            this.colRate.MinWidth = 10;
            this.colRate.Name = "colRate";
            this.colRate.Visible = true;
            this.colRate.VisibleIndex = 3;
            this.colRate.Width = 37;
            // 
            // colTotal
            // 
            this.colTotal.FieldName = "Total";
            this.colTotal.MinWidth = 10;
            this.colTotal.Name = "colTotal";
            this.colTotal.Width = 37;
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.MinWidth = 10;
            this.colId.Name = "colId";
            this.colId.Width = 37;
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.MenuManager = this.barManager1;
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
            this.dockManager1.ActivePanelChanged += new DevExpress.XtraBars.Docking.ActivePanelChangedEventHandler(this.dockManager1_ActivePanelChanged);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl1);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbCancel});
            this.barManager1.MaxItemId = 1;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 2";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar1.FloatLocation = new System.Drawing.Point(1744, 516);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbCancel)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar1.Text = "Custom 2";
            // 
            // bbCancel
            // 
            this.bbCancel.Caption = "Cancel Order";
            this.bbCancel.Id = 0;
            this.bbCancel.Name = "bbCancel";
            this.bbCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbCancel_ItemClick);
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.AutoSize = true;
            this.standaloneBarDockControl1.CausesValidation = false;
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(0, 0);
            this.standaloneBarDockControl1.Manager = this.barManager1;
            this.standaloneBarDockControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(639, 29);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(1147, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 510);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.barDockControlBottom.Size = new System.Drawing.Size(1147, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 510);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1147, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 510);
            // 
            // dpOrderBook
            // 
            this.dpOrderBook.Controls.Add(this.dockPanel1_Container);
            this.dpOrderBook.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dpOrderBook.ID = new System.Guid("09e5b5a5-8f81-4084-ad97-5cb8e0447355");
            this.dpOrderBook.Location = new System.Drawing.Point(0, 52);
            this.dpOrderBook.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dpOrderBook.Name = "dpOrderBook";
            this.dpOrderBook.OriginalSize = new System.Drawing.Size(742, 200);
            this.dpOrderBook.Size = new System.Drawing.Size(742, 458);
            this.dpOrderBook.Text = "Order Book";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.orderBookControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(733, 431);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // panelContainer1
            // 
            this.panelContainer1.ActiveChild = this.dpActiveTrailings;
            this.panelContainer1.Controls.Add(this.dpBuy);
            this.panelContainer1.Controls.Add(this.dpOpenedOrders);
            this.panelContainer1.Controls.Add(this.dpMyTrades);
            this.panelContainer1.Controls.Add(this.dpTrades);
            this.panelContainer1.Controls.Add(this.dpActiveTrailings);
            this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.panelContainer1.ID = new System.Guid("1698e180-145b-44bc-b717-dc185a4311c6");
            this.panelContainer1.Location = new System.Drawing.Point(499, 52);
            this.panelContainer1.Name = "panelContainer1";
            this.panelContainer1.OriginalSize = new System.Drawing.Size(648, 200);
            this.panelContainer1.Size = new System.Drawing.Size(648, 458);
            this.panelContainer1.Tabbed = true;
            this.panelContainer1.Text = "panelContainer1";
            // 
            // dpActiveTrailings
            // 
            this.dpActiveTrailings.Controls.Add(this.controlContainer4);
            this.dpActiveTrailings.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpActiveTrailings.ID = new System.Guid("3f8f5646-7327-4f6e-8fb7-734e3b94c318");
            this.dpActiveTrailings.Location = new System.Drawing.Point(5, 23);
            this.dpActiveTrailings.Name = "dpActiveTrailings";
            this.dpActiveTrailings.OriginalSize = new System.Drawing.Size(314, 404);
            this.dpActiveTrailings.Size = new System.Drawing.Size(639, 404);
            this.dpActiveTrailings.Text = "Active Trailings";
            // 
            // controlContainer4
            // 
            this.controlContainer4.Controls.Add(this.activeTrailingCollectionControl1);
            this.controlContainer4.Location = new System.Drawing.Point(0, 0);
            this.controlContainer4.Name = "controlContainer4";
            this.controlContainer4.Size = new System.Drawing.Size(639, 404);
            this.controlContainer4.TabIndex = 0;
            // 
            // activeTrailingCollectionControl1
            // 
            this.activeTrailingCollectionControl1.ChartControl = this.tickerChartViewer1;
            this.activeTrailingCollectionControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activeTrailingCollectionControl1.Location = new System.Drawing.Point(0, 0);
            this.activeTrailingCollectionControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.activeTrailingCollectionControl1.Name = "activeTrailingCollectionControl1";
            this.activeTrailingCollectionControl1.Size = new System.Drawing.Size(639, 404);
            this.activeTrailingCollectionControl1.TabIndex = 0;
            this.activeTrailingCollectionControl1.Ticker = null;
            // 
            // dpBuy
            // 
            this.dpBuy.Controls.Add(this.controlContainer2);
            this.dpBuy.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpBuy.FloatSize = new System.Drawing.Size(776, 551);
            this.dpBuy.ID = new System.Guid("d224c417-887b-4895-8bb9-1918c2c72ab7");
            this.dpBuy.Location = new System.Drawing.Point(5, 23);
            this.dpBuy.Name = "dpBuy";
            this.dpBuy.OriginalSize = new System.Drawing.Size(314, 404);
            this.dpBuy.Size = new System.Drawing.Size(639, 404);
            this.dpBuy.Text = "Buy/Sell";
            // 
            // controlContainer2
            // 
            this.controlContainer2.Controls.Add(this.buySettingsControl);
            this.controlContainer2.Location = new System.Drawing.Point(0, 0);
            this.controlContainer2.Name = "controlContainer2";
            this.controlContainer2.Size = new System.Drawing.Size(639, 404);
            this.controlContainer2.TabIndex = 0;
            // 
            // buySettingsControl
            // 
            this.buySettingsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buySettingsControl.Location = new System.Drawing.Point(0, 0);
            this.buySettingsControl.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.buySettingsControl.Name = "buySettingsControl";
            this.buySettingsControl.OperationsProvider = null;
            this.buySettingsControl.Settings = null;
            this.buySettingsControl.ShowTrailingSettings = true;
            this.buySettingsControl.Size = new System.Drawing.Size(639, 404);
            this.buySettingsControl.TabIndex = 0;
            this.buySettingsControl.Ticker = null;
            // 
            // dpOpenedOrders
            // 
            this.dpOpenedOrders.Controls.Add(this.controlContainer1);
            this.dpOpenedOrders.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpOpenedOrders.FloatVertical = true;
            this.dpOpenedOrders.ID = new System.Guid("147661ac-3074-41c2-a188-4f22f789a286");
            this.dpOpenedOrders.Location = new System.Drawing.Point(5, 23);
            this.dpOpenedOrders.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dpOpenedOrders.Name = "dpOpenedOrders";
            this.dpOpenedOrders.OriginalSize = new System.Drawing.Size(314, 404);
            this.dpOpenedOrders.Size = new System.Drawing.Size(639, 404);
            this.dpOpenedOrders.Text = "Opened Orders";
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.gcOpenedOrders);
            this.controlContainer1.Controls.Add(this.standaloneBarDockControl1);
            this.controlContainer1.Location = new System.Drawing.Point(0, 0);
            this.controlContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(639, 404);
            this.controlContainer1.TabIndex = 0;
            // 
            // gcOpenedOrders
            // 
            this.gcOpenedOrders.DataSource = this.openedOrderInfoBindingSource;
            this.gcOpenedOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcOpenedOrders.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gcOpenedOrders.Location = new System.Drawing.Point(0, 29);
            this.gcOpenedOrders.MainView = this.gvOpenedOrders;
            this.gcOpenedOrders.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gcOpenedOrders.MenuManager = this.ribbonControl1;
            this.gcOpenedOrders.Name = "gcOpenedOrders";
            this.gcOpenedOrders.Size = new System.Drawing.Size(639, 375);
            this.gcOpenedOrders.TabIndex = 0;
            this.gcOpenedOrders.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.gcOpenedOrders.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOpenedOrders});
            this.gcOpenedOrders.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gcOpenedOrders_MouseDown);
            // 
            // openedOrderInfoBindingSource
            // 
            this.openedOrderInfoBindingSource.DataSource = typeof(CryptoMarketClient.Common.OpenedOrderInfo);
            // 
            // gvOpenedOrders
            // 
            this.gvOpenedOrders.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 7.875F);
            this.gvOpenedOrders.Appearance.Row.Options.UseFont = true;
            this.gvOpenedOrders.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDate,
            this.colMarket,
            this.colOrderNumber,
            this.colType1,
            this.colValue,
            this.colAmount1,
            this.colTotal1});
            this.gvOpenedOrders.DetailHeight = 182;
            this.gvOpenedOrders.FixedLineWidth = 1;
            gridFormatRule3.Column = this.colType1;
            gridFormatRule3.ColumnApplyTo = this.colType1;
            gridFormatRule3.Name = "FormatSell";
            formatConditionRuleValue3.Appearance.ForeColor = System.Drawing.Color.Red;
            formatConditionRuleValue3.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue3.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue3.Value1 = CryptoMarketClient.Common.OrderType.Sell;
            gridFormatRule3.Rule = formatConditionRuleValue3;
            gridFormatRule4.Column = this.colType1;
            gridFormatRule4.ColumnApplyTo = this.colType1;
            gridFormatRule4.Name = "FormatBuy";
            formatConditionRuleValue4.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue4.Value1 = CryptoMarketClient.Common.OrderType.Buy;
            gridFormatRule4.Rule = formatConditionRuleValue4;
            this.gvOpenedOrders.FormatRules.Add(gridFormatRule3);
            this.gvOpenedOrders.FormatRules.Add(gridFormatRule4);
            this.gvOpenedOrders.GridControl = this.gcOpenedOrders;
            this.gvOpenedOrders.Name = "gvOpenedOrders";
            this.gvOpenedOrders.OptionsBehavior.Editable = false;
            this.gvOpenedOrders.OptionsView.EnableAppearanceEvenRow = true;
            this.gvOpenedOrders.OptionsView.ShowGroupPanel = false;
            this.gvOpenedOrders.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvOpenedOrders.OptionsView.ShowIndicator = false;
            this.gvOpenedOrders.PreviewIndent = 0;
            // 
            // colDate
            // 
            this.colDate.DisplayFormat.FormatString = "yyyy.MM.dd hh:mm:ss.fff";
            this.colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDate.FieldName = "OrderDate";
            this.colDate.Caption = "Date";
            this.colDate.MinWidth = 10;
            this.colDate.Name = "colDate";
            this.colDate.Visible = true;
            this.colDate.VisibleIndex = 4;
            this.colDate.Width = 37;
            // 
            // colMarket
            // 
            this.colMarket.FieldName = "Market";
            this.colMarket.MinWidth = 10;
            this.colMarket.Name = "colMarket";
            this.colMarket.Width = 37;
            // 
            // colOrderNumber
            // 
            this.colOrderNumber.FieldName = "OrderNumber";
            this.colOrderNumber.MinWidth = 10;
            this.colOrderNumber.Name = "colOrderNumber";
            this.colOrderNumber.Width = 37;
            // 
            // colValue
            // 
            this.colValue.Caption = "Price";
            this.colValue.FieldName = "ValueString";
            this.colValue.MinWidth = 10;
            this.colValue.Name = "colValue";
            this.colValue.Visible = true;
            this.colValue.VisibleIndex = 1;
            this.colValue.Width = 37;
            // 
            // colAmount1
            // 
            this.colAmount1.Caption = "Quantity";
            this.colAmount1.FieldName = "AmountString";
            this.colAmount1.MinWidth = 10;
            this.colAmount1.Name = "colAmount1";
            this.colAmount1.Visible = true;
            this.colAmount1.VisibleIndex = 2;
            this.colAmount1.Width = 37;
            // 
            // colTotal1
            // 
            this.colTotal1.Caption = "Total";
            this.colTotal1.FieldName = "TotalString";
            this.colTotal1.MinWidth = 10;
            this.colTotal1.Name = "colTotal1";
            this.colTotal1.Visible = true;
            this.colTotal1.VisibleIndex = 3;
            this.colTotal1.Width = 37;
            // 
            // dpMyTrades
            // 
            this.dpMyTrades.Controls.Add(this.controlContainer5);
            this.dpMyTrades.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpMyTrades.ID = new System.Guid("804e2b28-254a-4f8d-a3f8-9513e0057258");
            this.dpMyTrades.Location = new System.Drawing.Point(5, 23);
            this.dpMyTrades.Name = "dpMyTrades";
            this.dpMyTrades.OriginalSize = new System.Drawing.Size(314, 404);
            this.dpMyTrades.Size = new System.Drawing.Size(639, 404);
            this.dpMyTrades.Text = "My Trades";
            // 
            // controlContainer5
            // 
            this.controlContainer5.Controls.Add(this.myTradesCollectionControl1);
            this.controlContainer5.Location = new System.Drawing.Point(0, 0);
            this.controlContainer5.Name = "controlContainer5";
            this.controlContainer5.Size = new System.Drawing.Size(639, 404);
            this.controlContainer5.TabIndex = 0;
            // 
            // myTradesCollectionControl1
            // 
            this.myTradesCollectionControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myTradesCollectionControl1.Location = new System.Drawing.Point(0, 0);
            this.myTradesCollectionControl1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.myTradesCollectionControl1.Name = "myTradesCollectionControl1";
            this.myTradesCollectionControl1.Size = new System.Drawing.Size(639, 404);
            this.myTradesCollectionControl1.TabIndex = 0;
            this.myTradesCollectionControl1.Ticker = null;
            // 
            // dpTrades
            // 
            this.dpTrades.Controls.Add(this.dockPanel2_Container);
            this.dpTrades.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpTrades.ID = new System.Guid("cecd3d8a-da7f-4a81-90ca-fe5b5710f049");
            this.dpTrades.Location = new System.Drawing.Point(5, 23);
            this.dpTrades.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dpTrades.Name = "dpTrades";
            this.dpTrades.OriginalSize = new System.Drawing.Size(314, 404);
            this.dpTrades.Size = new System.Drawing.Size(639, 404);
            this.dpTrades.Text = "Trades";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.gcTrades);
            this.dockPanel2_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel2_Container.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(639, 404);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // dpInfo
            // 
            this.dpInfo.Controls.Add(this.dockPanel3_Container);
            this.dpInfo.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top;
            this.dpInfo.ID = new System.Guid("7b677dba-e9f9-46a7-b2ee-3f7b60447fd7");
            this.dpInfo.Location = new System.Drawing.Point(742, 52);
            this.dpInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dpInfo.Name = "dpInfo";
            this.dpInfo.OriginalSize = new System.Drawing.Size(200, 207);
            this.dpInfo.Size = new System.Drawing.Size(0, 207);
            this.dpInfo.Text = "Info";
            // 
            // dockPanel3_Container
            // 
            this.dockPanel3_Container.Controls.Add(this.tickerInfoControl);
            this.dockPanel3_Container.Location = new System.Drawing.Point(-243, 23);
            this.dockPanel3_Container.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dockPanel3_Container.Name = "dockPanel3_Container";
            this.dockPanel3_Container.Size = new System.Drawing.Size(0, 179);
            this.dockPanel3_Container.TabIndex = 0;
            // 
            // tradingResultBindingSource1
            // 
            this.tradingResultBindingSource1.DataSource = typeof(CryptoMarketClient.Common.TradingResult);
            // 
            // tradingResultBindingSource
            // 
            this.tradingResultBindingSource.DataSource = typeof(CryptoMarketClient.Common.TradingResult);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbCancel)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // TickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 510);
            this.Controls.Add(this.tickerChartViewer1);
            this.Controls.Add(this.dpOrderBook);
            this.Controls.Add(this.dpInfo);
            this.Controls.Add(this.panelContainer1);
            this.Controls.Add(this.ribbonControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "TickerForm";
            this.Text = "Ticker Form";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTrades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tradeHistoryItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTrades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.dpOrderBook.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.panelContainer1.ResumeLayout(false);
            this.dpActiveTrailings.ResumeLayout(false);
            this.controlContainer4.ResumeLayout(false);
            this.dpBuy.ResumeLayout(false);
            this.controlContainer2.ResumeLayout(false);
            this.dpOpenedOrders.ResumeLayout(false);
            this.controlContainer1.ResumeLayout(false);
            this.controlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcOpenedOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.openedOrderInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOpenedOrders)).EndInit();
            this.dpMyTrades.ResumeLayout(false);
            this.controlContainer5.ResumeLayout(false);
            this.dpTrades.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            this.dpInfo.ResumeLayout(false);
            this.dockPanel3_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tradingResultBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tradingResultBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpMain;
        private TickerInfo tickerInfoControl;
        private MyGridControl gcTrades;
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
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraBars.BarButtonItem biSellMarket;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dpTrades;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraBars.Docking.DockPanel dpOrderBook;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.Docking.DockPanel dpInfo;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel3_Container;
        private DevExpress.XtraBars.Docking.DockPanel dpOpenedOrders;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private MyGridControl gcOpenedOrders;
        private DevExpress.XtraGrid.Views.Grid.GridView gvOpenedOrders;
        private System.Windows.Forms.BindingSource openedOrderInfoBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colDate;
        private DevExpress.XtraGrid.Columns.GridColumn colMarket;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colType1;
        private DevExpress.XtraGrid.Columns.GridColumn colValue;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount1;
        private DevExpress.XtraGrid.Columns.GridColumn colTotal1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit2;
        private DevExpress.XtraBars.Docking.DockPanel dpBuy;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer2;
        private TradeSettingsControl buySettingsControl;
        private DevExpress.XtraBars.Docking.DockPanel panelContainer1;
        private System.Windows.Forms.BindingSource tradingResultBindingSource;
        private System.Windows.Forms.BindingSource tradingResultBindingSource1;
        private DevExpress.XtraBars.Docking.DockPanel dpActiveTrailings;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer4;
        private TrailingCollectionControl activeTrailingCollectionControl1;
        private DevExpress.XtraBars.Docking.DockPanel dpMyTrades;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer5;
        private MyTradesCollectionControl myTradesCollectionControl1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem bbCancel;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
    }
}