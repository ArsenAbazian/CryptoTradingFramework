using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace CryptoMarketClient {
    partial class TickerArbitrageForm {
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule3 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue3 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule4 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue4 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.Sparkline.BarSparklineView barSparklineView1 = new DevExpress.Sparkline.BarSparklineView();
            DevExpress.Sparkline.BarSparklineView barSparklineView2 = new DevExpress.Sparkline.BarSparklineView();
            DevExpress.Sparkline.AreaSparklineView areaSparklineView1 = new DevExpress.Sparkline.AreaSparklineView();
            DevExpress.Sparkline.AreaSparklineView areaSparklineView2 = new DevExpress.Sparkline.AreaSparklineView();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TickerArbitrageForm));
            this.colProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsActual = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMarketCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLowestAskEnabled = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colLowestAskHost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHighestBidEnabled = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colHighestBidHost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProfitUSD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.tickerArbitrageInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIsSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colBaseCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLastUpdate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTickers = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLowestAskTicker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHighestBidTicker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLowestAsk = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHighestBid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSpread = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLowestAksFee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHighestBidFee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalFee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAvailableAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAvailableProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAvailableProfitUSD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLowestBidAskRelation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.colHighestBidAskRelation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSuppressNotification = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colBidShift = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAskShift = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHipe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSparklineEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit();
            this.colSellHipe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSparklineEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit();
            this.colBidEnergy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSparklineEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit();
            this.colAskEnergy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSparklineEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit();
            this.colTotalBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbAllCurrencies = new DevExpress.XtraBars.BarCheckItem();
            this.bbTryArbitrage = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.bbMonitorSelected = new DevExpress.XtraBars.BarCheckItem();
            this.bbOpenWeb = new DevExpress.XtraBars.BarButtonItem();
            this.bbSelectPositive = new DevExpress.XtraBars.BarButtonItem();
            this.bbBuy = new DevExpress.XtraBars.BarButtonItem();
            this.bbSell = new DevExpress.XtraBars.BarButtonItem();
            this.bbSendToHighestBid = new DevExpress.XtraBars.BarButtonItem();
            this.beBuyLowestAsk = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemSpinEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.beHighestBidSell = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemSpinEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.bbSyncWithLowestAsk = new DevExpress.XtraBars.BarButtonItem();
            this.btShowCombinedBidAsk = new DevExpress.XtraBars.BarButtonItem();
            this.bbShowOrderBookHistory = new DevExpress.XtraBars.BarButtonItem();
            this.bbMinimalProfitSpread = new DevExpress.XtraBars.BarButtonItem();
            this.bbAnalytics = new DevExpress.XtraBars.BarButtonItem();
            this.bbGridStrategy = new DevExpress.XtraBars.BarButtonItem();
            this.bbShowTickerStrategies = new DevExpress.XtraBars.BarButtonItem();
            this.bbUpdateBot = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.bsShowTickerChart = new DevExpress.XtraBars.BarSubItem();
            this.bsStrategies = new DevExpress.XtraBars.BarSubItem();
            this.bsShowOrderBookHistory = new DevExpress.XtraBars.BarSubItem();
            this.bcShowNonZeroAmout = new DevExpress.XtraBars.BarCheckItem();
            this.rpPoloniex = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup5 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.sidePanel1 = new DevExpress.XtraEditors.SidePanel();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tickerArbitrageInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            this.sidePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // colProfit
            // 
            this.colProfit.DisplayFormat.FormatString = "0.00000000";
            this.colProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colProfit.FieldName = "MaxProfit";
            this.colProfit.MinWidth = 40;
            this.colProfit.Name = "colProfit";
            this.colProfit.OptionsColumn.AllowEdit = false;
            this.colProfit.OptionsColumn.ReadOnly = true;
            this.colProfit.Visible = true;
            this.colProfit.VisibleIndex = 12;
            this.colProfit.Width = 150;
            // 
            // colIsActual
            // 
            this.colIsActual.Caption = "IsActual";
            this.colIsActual.FieldName = "IsActual";
            this.colIsActual.MinWidth = 40;
            this.colIsActual.Name = "colIsActual";
            this.colIsActual.OptionsColumn.AllowEdit = false;
            this.colIsActual.Width = 150;
            // 
            // colMarketCurrency
            // 
            this.colMarketCurrency.Caption = "Market";
            this.colMarketCurrency.FieldName = "MarketCurrency";
            this.colMarketCurrency.MinWidth = 40;
            this.colMarketCurrency.Name = "colMarketCurrency";
            this.colMarketCurrency.OptionsColumn.AllowEdit = false;
            this.colMarketCurrency.Visible = true;
            this.colMarketCurrency.VisibleIndex = 3;
            this.colMarketCurrency.Width = 150;
            // 
            // colLowestAskEnabled
            // 
            this.colLowestAskEnabled.Caption = "LowestAsk Enabled";
            this.colLowestAskEnabled.ColumnEdit = this.repositoryItemCheckEdit3;
            this.colLowestAskEnabled.FieldName = "LowestAskEnabled";
            this.colLowestAskEnabled.MinWidth = 40;
            this.colLowestAskEnabled.Name = "colLowestAskEnabled";
            this.colLowestAskEnabled.Width = 150;
            // 
            // repositoryItemCheckEdit3
            // 
            this.repositoryItemCheckEdit3.AutoHeight = false;
            this.repositoryItemCheckEdit3.Name = "repositoryItemCheckEdit3";
            // 
            // colLowestAskHost
            // 
            this.colLowestAskHost.Caption = "Buy On";
            this.colLowestAskHost.FieldName = "LowestAskHost";
            this.colLowestAskHost.MinWidth = 40;
            this.colLowestAskHost.Name = "colLowestAskHost";
            this.colLowestAskHost.OptionsColumn.AllowEdit = false;
            this.colLowestAskHost.OptionsColumn.ReadOnly = true;
            this.colLowestAskHost.Visible = true;
            this.colLowestAskHost.VisibleIndex = 5;
            this.colLowestAskHost.Width = 150;
            // 
            // colHighestBidEnabled
            // 
            this.colHighestBidEnabled.Caption = "HighestBid Enabled";
            this.colHighestBidEnabled.ColumnEdit = this.repositoryItemCheckEdit2;
            this.colHighestBidEnabled.FieldName = "HighestBidEnabled";
            this.colHighestBidEnabled.MinWidth = 40;
            this.colHighestBidEnabled.Name = "colHighestBidEnabled";
            this.colHighestBidEnabled.Width = 150;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // colHighestBidHost
            // 
            this.colHighestBidHost.Caption = "Sell On";
            this.colHighestBidHost.FieldName = "HighestBidHost";
            this.colHighestBidHost.MinWidth = 40;
            this.colHighestBidHost.Name = "colHighestBidHost";
            this.colHighestBidHost.OptionsColumn.AllowEdit = false;
            this.colHighestBidHost.OptionsColumn.ReadOnly = true;
            this.colHighestBidHost.Visible = true;
            this.colHighestBidHost.VisibleIndex = 6;
            this.colHighestBidHost.Width = 150;
            // 
            // colUpdateTime
            // 
            this.colUpdateTime.Caption = "UpdateTime Ms";
            this.colUpdateTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colUpdateTime.FieldName = "UpdateTimeMs";
            this.colUpdateTime.MinWidth = 40;
            this.colUpdateTime.Name = "colUpdateTime";
            this.colUpdateTime.OptionsColumn.AllowEdit = false;
            this.colUpdateTime.Width = 150;
            // 
            // colProfitUSD
            // 
            this.colProfitUSD.Caption = "In USD";
            this.colProfitUSD.DisplayFormat.FormatString = "0.00000000";
            this.colProfitUSD.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colProfitUSD.FieldName = "MaxProfitUSD";
            this.colProfitUSD.MinWidth = 40;
            this.colProfitUSD.Name = "colProfitUSD";
            this.colProfitUSD.OptionsColumn.AllowEdit = false;
            this.colProfitUSD.OptionsColumn.ReadOnly = true;
            this.colProfitUSD.Visible = true;
            this.colProfitUSD.VisibleIndex = 13;
            this.colProfitUSD.Width = 150;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.tickerArbitrageInfoBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2,
            this.repositoryItemCheckEdit3,
            this.repositoryItemProgressBar1,
            this.repositoryItemCheckEdit4,
            this.repositoryItemSparklineEdit1,
            this.repositoryItemSparklineEdit2,
            this.repositoryItemSparklineEdit3,
            this.repositoryItemSparklineEdit4});
            this.gridControl1.Size = new System.Drawing.Size(3832, 1768);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseDown);
            // 
            // tickerArbitrageInfoBindingSource
            // 
            this.tickerArbitrageInfoBindingSource.DataSource = typeof(CryptoMarketClient.TickerCollection);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIsSelected,
            this.colBaseCurrency,
            this.colMarketCurrency,
            this.colLastUpdate,
            this.colUpdateTime,
            this.colIsActual,
            this.colTickers,
            this.colCount,
            this.colLowestAskTicker,
            this.colHighestBidTicker,
            this.colLowestAskHost,
            this.colHighestBidHost,
            this.colLowestAsk,
            this.colHighestBid,
            this.colSpread,
            this.colAmount,
            this.colBuyTotal,
            this.colTotal,
            this.colLowestAksFee,
            this.colHighestBidFee,
            this.colTotalFee,
            this.colProfit,
            this.colProfitUSD,
            this.colAvailableAmount,
            this.colAvailableProfit,
            this.colAvailableProfitUSD,
            this.colLowestAskEnabled,
            this.colHighestBidEnabled,
            this.colLowestBidAskRelation,
            this.colHighestBidAskRelation,
            this.colSuppressNotification,
            this.colBidShift,
            this.colAskShift,
            this.colHipe,
            this.colSellHipe,
            this.colBidEnergy,
            this.colAskEnergy,
            this.colTotalBalance});
            this.gridView1.DetailHeight = 673;
            this.gridView1.FixedLineWidth = 4;
            gridFormatRule1.Column = this.colProfit;
            gridFormatRule1.ColumnApplyTo = this.colProfit;
            gridFormatRule1.Name = "ArbitrageSpreadRule";
            formatConditionRuleValue1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            formatConditionRuleValue1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            formatConditionRuleValue1.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue1.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Greater;
            formatConditionRuleValue1.Value1 = 0D;
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.Column = this.colIsActual;
            gridFormatRule2.ColumnApplyTo = this.colMarketCurrency;
            gridFormatRule2.Name = "FormatNotActual";
            formatConditionRuleValue2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            formatConditionRuleValue2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            formatConditionRuleValue2.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue2.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue2.Value1 = false;
            gridFormatRule2.Rule = formatConditionRuleValue2;
            gridFormatRule3.Column = this.colLowestAskEnabled;
            gridFormatRule3.ColumnApplyTo = this.colLowestAskHost;
            gridFormatRule3.Name = "LowestAskEnabledRule";
            formatConditionRuleValue3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            formatConditionRuleValue3.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue3.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue3.Value1 = false;
            gridFormatRule3.Rule = formatConditionRuleValue3;
            gridFormatRule4.Column = this.colHighestBidEnabled;
            gridFormatRule4.ColumnApplyTo = this.colHighestBidHost;
            gridFormatRule4.Name = "HighestBidEnabledRule";
            formatConditionRuleValue4.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            formatConditionRuleValue4.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue4.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue4.Value1 = false;
            gridFormatRule4.Rule = formatConditionRuleValue4;
            this.gridView1.FormatRules.Add(gridFormatRule1);
            this.gridView1.FormatRules.Add(gridFormatRule2);
            this.gridView1.FormatRules.Add(gridFormatRule3);
            this.gridView1.FormatRules.Add(gridFormatRule4);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colSpread, DevExpress.Data.ColumnSortOrder.Descending)});
            this.gridView1.Click += new System.EventHandler(this.gridView1_Click);
            // 
            // colIsSelected
            // 
            this.colIsSelected.Caption = "Selected";
            this.colIsSelected.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsSelected.FieldName = "IsSelected";
            this.colIsSelected.MinWidth = 40;
            this.colIsSelected.Name = "colIsSelected";
            this.colIsSelected.Visible = true;
            this.colIsSelected.VisibleIndex = 0;
            this.colIsSelected.Width = 150;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.EditValueChanged += new System.EventHandler(this.repositoryItemCheckEdit1_EditValueChanged);
            // 
            // colBaseCurrency
            // 
            this.colBaseCurrency.Caption = "Base";
            this.colBaseCurrency.FieldName = "BaseCurrency";
            this.colBaseCurrency.MinWidth = 40;
            this.colBaseCurrency.Name = "colBaseCurrency";
            this.colBaseCurrency.OptionsColumn.AllowEdit = false;
            this.colBaseCurrency.Visible = true;
            this.colBaseCurrency.VisibleIndex = 2;
            this.colBaseCurrency.Width = 150;
            // 
            // colLastUpdate
            // 
            this.colLastUpdate.DisplayFormat.FormatString = "HH:mm:ss.fff";
            this.colLastUpdate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colLastUpdate.FieldName = "LastUpdate";
            this.colLastUpdate.MinWidth = 40;
            this.colLastUpdate.Name = "colLastUpdate";
            this.colLastUpdate.OptionsColumn.AllowEdit = false;
            this.colLastUpdate.Width = 150;
            // 
            // colTickers
            // 
            this.colTickers.FieldName = "Tickers";
            this.colTickers.MinWidth = 40;
            this.colTickers.Name = "colTickers";
            this.colTickers.OptionsColumn.AllowEdit = false;
            this.colTickers.OptionsColumn.ReadOnly = true;
            this.colTickers.Width = 150;
            // 
            // colCount
            // 
            this.colCount.FieldName = "Count";
            this.colCount.MinWidth = 40;
            this.colCount.Name = "colCount";
            this.colCount.OptionsColumn.AllowEdit = false;
            this.colCount.OptionsColumn.ReadOnly = true;
            this.colCount.Width = 150;
            // 
            // colLowestAskTicker
            // 
            this.colLowestAskTicker.FieldName = "LowestAskTicker";
            this.colLowestAskTicker.MinWidth = 40;
            this.colLowestAskTicker.Name = "colLowestAskTicker";
            this.colLowestAskTicker.OptionsColumn.AllowEdit = false;
            this.colLowestAskTicker.OptionsColumn.ReadOnly = true;
            this.colLowestAskTicker.Width = 150;
            // 
            // colHighestBidTicker
            // 
            this.colHighestBidTicker.FieldName = "HighestBidTicker";
            this.colHighestBidTicker.MinWidth = 40;
            this.colHighestBidTicker.Name = "colHighestBidTicker";
            this.colHighestBidTicker.OptionsColumn.AllowEdit = false;
            this.colHighestBidTicker.OptionsColumn.ReadOnly = true;
            this.colHighestBidTicker.Width = 150;
            // 
            // colLowestAsk
            // 
            this.colLowestAsk.DisplayFormat.FormatString = "0.00000000";
            this.colLowestAsk.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colLowestAsk.FieldName = "LowestAsk";
            this.colLowestAsk.MinWidth = 40;
            this.colLowestAsk.Name = "colLowestAsk";
            this.colLowestAsk.OptionsColumn.AllowEdit = false;
            this.colLowestAsk.OptionsColumn.ReadOnly = true;
            this.colLowestAsk.Visible = true;
            this.colLowestAsk.VisibleIndex = 7;
            this.colLowestAsk.Width = 150;
            // 
            // colHighestBid
            // 
            this.colHighestBid.DisplayFormat.FormatString = "0.00000000";
            this.colHighestBid.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHighestBid.FieldName = "HighestBid";
            this.colHighestBid.MinWidth = 40;
            this.colHighestBid.Name = "colHighestBid";
            this.colHighestBid.OptionsColumn.AllowEdit = false;
            this.colHighestBid.OptionsColumn.ReadOnly = true;
            this.colHighestBid.Visible = true;
            this.colHighestBid.VisibleIndex = 8;
            this.colHighestBid.Width = 150;
            // 
            // colSpread
            // 
            this.colSpread.DisplayFormat.FormatString = "0.00000000#";
            this.colSpread.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSpread.FieldName = "Spread";
            this.colSpread.MinWidth = 40;
            this.colSpread.Name = "colSpread";
            this.colSpread.OptionsColumn.AllowEdit = false;
            this.colSpread.Visible = true;
            this.colSpread.VisibleIndex = 9;
            this.colSpread.Width = 150;
            // 
            // colAmount
            // 
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "Amount";
            this.colAmount.MinWidth = 40;
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            this.colAmount.OptionsColumn.ReadOnly = true;
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 10;
            this.colAmount.Width = 150;
            // 
            // colBuyTotal
            // 
            this.colBuyTotal.DisplayFormat.FormatString = "0.00000000";
            this.colBuyTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBuyTotal.FieldName = "BuyTotal";
            this.colBuyTotal.MinWidth = 40;
            this.colBuyTotal.Name = "colBuyTotal";
            this.colBuyTotal.OptionsColumn.AllowEdit = false;
            this.colBuyTotal.Visible = true;
            this.colBuyTotal.VisibleIndex = 11;
            this.colBuyTotal.Width = 150;
            // 
            // colTotal
            // 
            this.colTotal.DisplayFormat.FormatString = "0.00000000";
            this.colTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotal.FieldName = "Total";
            this.colTotal.MinWidth = 40;
            this.colTotal.Name = "colTotal";
            this.colTotal.OptionsColumn.AllowEdit = false;
            this.colTotal.Width = 150;
            // 
            // colLowestAksFee
            // 
            this.colLowestAksFee.FieldName = "LowestAksFee";
            this.colLowestAksFee.MinWidth = 40;
            this.colLowestAksFee.Name = "colLowestAksFee";
            this.colLowestAksFee.OptionsColumn.AllowEdit = false;
            this.colLowestAksFee.OptionsColumn.ReadOnly = true;
            this.colLowestAksFee.Width = 150;
            // 
            // colHighestBidFee
            // 
            this.colHighestBidFee.FieldName = "HighestBidFee";
            this.colHighestBidFee.MinWidth = 40;
            this.colHighestBidFee.Name = "colHighestBidFee";
            this.colHighestBidFee.OptionsColumn.AllowEdit = false;
            this.colHighestBidFee.OptionsColumn.ReadOnly = true;
            this.colHighestBidFee.Width = 150;
            // 
            // colTotalFee
            // 
            this.colTotalFee.DisplayFormat.FormatString = "0.00000000";
            this.colTotalFee.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalFee.FieldName = "TotalFee";
            this.colTotalFee.MinWidth = 40;
            this.colTotalFee.Name = "colTotalFee";
            this.colTotalFee.OptionsColumn.AllowEdit = false;
            this.colTotalFee.OptionsColumn.ReadOnly = true;
            this.colTotalFee.Width = 150;
            // 
            // colAvailableAmount
            // 
            this.colAvailableAmount.Caption = "AvailableAmount";
            this.colAvailableAmount.FieldName = "AvailableAmount";
            this.colAvailableAmount.MinWidth = 40;
            this.colAvailableAmount.Name = "colAvailableAmount";
            this.colAvailableAmount.OptionsColumn.AllowEdit = false;
            this.colAvailableAmount.Width = 150;
            // 
            // colAvailableProfit
            // 
            this.colAvailableProfit.Caption = "AvailableProfit";
            this.colAvailableProfit.DisplayFormat.FormatString = "0.00000000";
            this.colAvailableProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAvailableProfit.FieldName = "AvailableProfit";
            this.colAvailableProfit.MinWidth = 40;
            this.colAvailableProfit.Name = "colAvailableProfit";
            this.colAvailableProfit.OptionsColumn.AllowEdit = false;
            this.colAvailableProfit.Width = 150;
            // 
            // colAvailableProfitUSD
            // 
            this.colAvailableProfitUSD.Caption = "AvailableProfitUSD";
            this.colAvailableProfitUSD.DisplayFormat.FormatString = "0.00000000";
            this.colAvailableProfitUSD.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAvailableProfitUSD.FieldName = "AvailableProfitUSD";
            this.colAvailableProfitUSD.MinWidth = 40;
            this.colAvailableProfitUSD.Name = "colAvailableProfitUSD";
            this.colAvailableProfitUSD.OptionsColumn.AllowEdit = false;
            this.colAvailableProfitUSD.Width = 150;
            // 
            // colLowestBidAskRelation
            // 
            this.colLowestBidAskRelation.Caption = "Lowest Bid/Ask";
            this.colLowestBidAskRelation.ColumnEdit = this.repositoryItemProgressBar1;
            this.colLowestBidAskRelation.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colLowestBidAskRelation.FieldName = "LowestBidAskRelation";
            this.colLowestBidAskRelation.MinWidth = 40;
            this.colLowestBidAskRelation.Name = "colLowestBidAskRelation";
            this.colLowestBidAskRelation.OptionsColumn.AllowEdit = false;
            this.colLowestBidAskRelation.Width = 150;
            // 
            // repositoryItemProgressBar1
            // 
            this.repositoryItemProgressBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.repositoryItemProgressBar1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.repositoryItemProgressBar1.Appearance.ForeColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.repositoryItemProgressBar1.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.repositoryItemProgressBar1.AppearanceReadOnly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.repositoryItemProgressBar1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.repositoryItemProgressBar1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.repositoryItemProgressBar1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
            this.repositoryItemProgressBar1.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.repositoryItemProgressBar1.ShowTitle = true;
            this.repositoryItemProgressBar1.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            // 
            // colHighestBidAskRelation
            // 
            this.colHighestBidAskRelation.Caption = "Highest Bid/Ask";
            this.colHighestBidAskRelation.ColumnEdit = this.repositoryItemProgressBar1;
            this.colHighestBidAskRelation.FieldName = "HighestBidAskRelation";
            this.colHighestBidAskRelation.MinWidth = 40;
            this.colHighestBidAskRelation.Name = "colHighestBidAskRelation";
            this.colHighestBidAskRelation.Width = 150;
            // 
            // colSuppressNotification
            // 
            this.colSuppressNotification.Caption = "Disable";
            this.colSuppressNotification.ColumnEdit = this.repositoryItemCheckEdit4;
            this.colSuppressNotification.FieldName = "Disabled";
            this.colSuppressNotification.MinWidth = 40;
            this.colSuppressNotification.Name = "colSuppressNotification";
            this.colSuppressNotification.Visible = true;
            this.colSuppressNotification.VisibleIndex = 1;
            this.colSuppressNotification.Width = 150;
            // 
            // repositoryItemCheckEdit4
            // 
            this.repositoryItemCheckEdit4.AutoHeight = false;
            this.repositoryItemCheckEdit4.Name = "repositoryItemCheckEdit4";
            this.repositoryItemCheckEdit4.EditValueChanged += new System.EventHandler(this.repositoryItemCheckEdit4_EditValueChanged);
            // 
            // colBidShift
            // 
            this.colBidShift.Caption = "BidShift";
            this.colBidShift.DisplayFormat.FormatString = "0.######";
            this.colBidShift.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBidShift.FieldName = "BidShift";
            this.colBidShift.MinWidth = 40;
            this.colBidShift.Name = "colBidShift";
            this.colBidShift.Width = 150;
            // 
            // colAskShift
            // 
            this.colAskShift.Caption = "Ask Shift";
            this.colAskShift.DisplayFormat.FormatString = "0.#####";
            this.colAskShift.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAskShift.FieldName = "AskShift";
            this.colAskShift.MinWidth = 40;
            this.colAskShift.Name = "colAskShift";
            this.colAskShift.Width = 150;
            // 
            // colHipe
            // 
            this.colHipe.Caption = "BidHipe";
            this.colHipe.ColumnEdit = this.repositoryItemSparklineEdit1;
            this.colHipe.FieldName = "BidHipes";
            this.colHipe.MinWidth = 40;
            this.colHipe.Name = "colHipe";
            this.colHipe.OptionsColumn.AllowEdit = false;
            this.colHipe.Width = 150;
            // 
            // repositoryItemSparklineEdit1
            // 
            this.repositoryItemSparklineEdit1.Name = "repositoryItemSparklineEdit1";
            this.repositoryItemSparklineEdit1.ValueRange.IsAuto = false;
            barSparklineView1.Color = System.Drawing.Color.Green;
            this.repositoryItemSparklineEdit1.View = barSparklineView1;
            // 
            // colSellHipe
            // 
            this.colSellHipe.Caption = "AskHipe";
            this.colSellHipe.ColumnEdit = this.repositoryItemSparklineEdit2;
            this.colSellHipe.FieldName = "AskHipes";
            this.colSellHipe.MinWidth = 40;
            this.colSellHipe.Name = "colSellHipe";
            this.colSellHipe.OptionsColumn.AllowEdit = false;
            this.colSellHipe.Width = 150;
            // 
            // repositoryItemSparklineEdit2
            // 
            this.repositoryItemSparklineEdit2.Name = "repositoryItemSparklineEdit2";
            barSparklineView2.Color = System.Drawing.Color.Red;
            this.repositoryItemSparklineEdit2.View = barSparklineView2;
            // 
            // colBidEnergy
            // 
            this.colBidEnergy.Caption = "Bid Energies";
            this.colBidEnergy.ColumnEdit = this.repositoryItemSparklineEdit3;
            this.colBidEnergy.FieldName = "BidEnergies";
            this.colBidEnergy.MinWidth = 40;
            this.colBidEnergy.Name = "colBidEnergy";
            this.colBidEnergy.OptionsColumn.AllowEdit = false;
            this.colBidEnergy.Width = 150;
            // 
            // repositoryItemSparklineEdit3
            // 
            this.repositoryItemSparklineEdit3.Name = "repositoryItemSparklineEdit3";
            areaSparklineView1.Color = System.Drawing.Color.Green;
            areaSparklineView1.ScaleFactor = 2F;
            this.repositoryItemSparklineEdit3.View = areaSparklineView1;
            // 
            // colAskEnergy
            // 
            this.colAskEnergy.Caption = "Ask Energies";
            this.colAskEnergy.ColumnEdit = this.repositoryItemSparklineEdit4;
            this.colAskEnergy.FieldName = "AskEnergies";
            this.colAskEnergy.MinWidth = 40;
            this.colAskEnergy.Name = "colAskEnergy";
            this.colAskEnergy.OptionsColumn.AllowEdit = false;
            this.colAskEnergy.Width = 150;
            // 
            // repositoryItemSparklineEdit4
            // 
            this.repositoryItemSparklineEdit4.Name = "repositoryItemSparklineEdit4";
            areaSparklineView2.Color = System.Drawing.Color.Red;
            areaSparklineView2.ScaleFactor = 2F;
            this.repositoryItemSparklineEdit4.View = areaSparklineView2;
            // 
            // colTotalBalance
            // 
            this.colTotalBalance.Caption = "TotalBalance";
            this.colTotalBalance.FieldName = "TotalBalance";
            this.colTotalBalance.MinWidth = 40;
            this.colTotalBalance.Name = "colTotalBalance";
            this.colTotalBalance.OptionsColumn.AllowEdit = false;
            this.colTotalBalance.Visible = true;
            this.colTotalBalance.VisibleIndex = 4;
            this.colTotalBalance.Width = 150;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.AllowGlyphSkinning = true;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.bbAllCurrencies,
            this.bbTryArbitrage,
            this.barButtonItem1,
            this.bbMonitorSelected,
            this.bbOpenWeb,
            this.bbSelectPositive,
            this.bbBuy,
            this.bbSell,
            this.bbSendToHighestBid,
            this.beBuyLowestAsk,
            this.beHighestBidSell,
            this.bbSyncWithLowestAsk,
            this.btShowCombinedBidAsk,
            this.bbShowOrderBookHistory,
            this.bbMinimalProfitSpread,
            this.bbAnalytics,
            this.bbGridStrategy,
            this.bbShowTickerStrategies,
            this.bbUpdateBot,
            this.barButtonItem2,
            this.barButtonItem3,
            this.bsShowTickerChart,
            this.bsStrategies,
            this.bsShowOrderBookHistory,
            this.bcShowNonZeroAmout});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.ribbonControl1.MaxItemId = 33;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpPoloniex,
            this.ribbonPage1});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEdit1,
            this.repositoryItemSpinEdit2,
            this.repositoryItemSpinEdit3});
            this.ribbonControl1.Size = new System.Drawing.Size(3832, 259);
            // 
            // bbAllCurrencies
            // 
            this.bbAllCurrencies.Caption = "Show Positive Profits";
            this.bbAllCurrencies.Id = 3;
            this.bbAllCurrencies.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbAllCurrencies.ImageOptions.Image")));
            this.bbAllCurrencies.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbAllCurrencies.ImageOptions.LargeImage")));
            this.bbAllCurrencies.Name = "bbAllCurrencies";
            this.bbAllCurrencies.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)((DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.bbAllCurrencies.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bbAllCurrencies_CheckedChanged);
            // 
            // bbTryArbitrage
            // 
            this.bbTryArbitrage.Caption = "Try Arbitrage!";
            this.bbTryArbitrage.Id = 5;
            this.bbTryArbitrage.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbTryArbitrage.ImageOptions.Image")));
            this.bbTryArbitrage.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbTryArbitrage.ImageOptions.LargeImage")));
            this.bbTryArbitrage.Name = "bbTryArbitrage";
            this.bbTryArbitrage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbTryArbitrage_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Monitor only Seelcted Arbitrage";
            this.barButtonItem1.Id = 6;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // bbMonitorSelected
            // 
            this.bbMonitorSelected.Caption = "Monitor Only Selected";
            this.bbMonitorSelected.Id = 7;
            this.bbMonitorSelected.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbMonitorSelected.ImageOptions.Image")));
            this.bbMonitorSelected.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbMonitorSelected.ImageOptions.LargeImage")));
            this.bbMonitorSelected.Name = "bbMonitorSelected";
            this.bbMonitorSelected.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)((DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            // 
            // bbOpenWeb
            // 
            this.bbOpenWeb.Caption = "Open Markets in Web";
            this.bbOpenWeb.Id = 8;
            this.bbOpenWeb.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbOpenWeb.ImageOptions.Image")));
            this.bbOpenWeb.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbOpenWeb.ImageOptions.LargeImage")));
            this.bbOpenWeb.Name = "bbOpenWeb";
            this.bbOpenWeb.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)((DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.bbOpenWeb.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbOpenWeb_ItemClick);
            // 
            // bbSelectPositive
            // 
            this.bbSelectPositive.Caption = "Select Positive Arbitrages";
            this.bbSelectPositive.Id = 9;
            this.bbSelectPositive.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbSelectPositive.ImageOptions.Image")));
            this.bbSelectPositive.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbSelectPositive.ImageOptions.LargeImage")));
            this.bbSelectPositive.Name = "bbSelectPositive";
            this.bbSelectPositive.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbSelectPositive_ItemClick);
            // 
            // bbBuy
            // 
            this.bbBuy.Caption = "Buy LowestAsk";
            this.bbBuy.Id = 10;
            this.bbBuy.Name = "bbBuy";
            this.bbBuy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbBuy_ItemClick);
            // 
            // bbSell
            // 
            this.bbSell.Caption = "Sell Highest Bid";
            this.bbSell.Id = 11;
            this.bbSell.Name = "bbSell";
            this.bbSell.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbSell_ItemClick);
            // 
            // bbSendToHighestBid
            // 
            this.bbSendToHighestBid.Caption = "Send To Highest Bid Market";
            this.bbSendToHighestBid.Id = 12;
            this.bbSendToHighestBid.Name = "bbSendToHighestBid";
            this.bbSendToHighestBid.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbSendToHighestBid_ItemClick);
            // 
            // beBuyLowestAsk
            // 
            this.beBuyLowestAsk.Caption = "Base Currency (%)";
            this.beBuyLowestAsk.Edit = this.repositoryItemSpinEdit2;
            this.beBuyLowestAsk.EditValue = 50D;
            this.beBuyLowestAsk.EditWidth = 125;
            this.beBuyLowestAsk.Id = 14;
            this.beBuyLowestAsk.Name = "beBuyLowestAsk";
            // 
            // repositoryItemSpinEdit2
            // 
            this.repositoryItemSpinEdit2.AutoHeight = false;
            this.repositoryItemSpinEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit2.Name = "repositoryItemSpinEdit2";
            // 
            // beHighestBidSell
            // 
            this.beHighestBidSell.Caption = "Market Currency (%)";
            this.beHighestBidSell.Edit = this.repositoryItemSpinEdit3;
            this.beHighestBidSell.EditValue = 100D;
            this.beHighestBidSell.EditWidth = 125;
            this.beHighestBidSell.Id = 15;
            this.beHighestBidSell.Name = "beHighestBidSell";
            // 
            // repositoryItemSpinEdit3
            // 
            this.repositoryItemSpinEdit3.AutoHeight = false;
            this.repositoryItemSpinEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit3.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.repositoryItemSpinEdit3.Name = "repositoryItemSpinEdit3";
            // 
            // bbSyncWithLowestAsk
            // 
            this.bbSyncWithLowestAsk.Caption = "Send To Lowest Ask";
            this.bbSyncWithLowestAsk.Id = 16;
            this.bbSyncWithLowestAsk.Name = "bbSyncWithLowestAsk";
            this.bbSyncWithLowestAsk.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbSyncWithLowestAsk_ItemClick);
            // 
            // btShowCombinedBidAsk
            // 
            this.btShowCombinedBidAsk.Caption = "Combined Bid/Ask";
            this.btShowCombinedBidAsk.Id = 19;
            this.btShowCombinedBidAsk.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btShowCombinedBidAsk.ImageOptions.Image")));
            this.btShowCombinedBidAsk.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btShowCombinedBidAsk.ImageOptions.LargeImage")));
            this.btShowCombinedBidAsk.Name = "btShowCombinedBidAsk";
            this.btShowCombinedBidAsk.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btShowCombinedBidAsk_ItemClick);
            // 
            // bbShowOrderBookHistory
            // 
            this.bbShowOrderBookHistory.Caption = "OrderBook History";
            this.bbShowOrderBookHistory.Id = 20;
            this.bbShowOrderBookHistory.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbShowOrderBookHistory.ImageOptions.Image")));
            this.bbShowOrderBookHistory.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbShowOrderBookHistory.ImageOptions.LargeImage")));
            this.bbShowOrderBookHistory.Name = "bbShowOrderBookHistory";
            this.bbShowOrderBookHistory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbShowOrderBookHistory_ItemClick);
            // 
            // bbMinimalProfitSpread
            // 
            this.bbMinimalProfitSpread.Caption = "Minimal Profit Spread";
            this.bbMinimalProfitSpread.Id = 21;
            this.bbMinimalProfitSpread.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbMinimalProfitSpread.ImageOptions.Image")));
            this.bbMinimalProfitSpread.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbMinimalProfitSpread.ImageOptions.LargeImage")));
            this.bbMinimalProfitSpread.Name = "bbMinimalProfitSpread";
            this.bbMinimalProfitSpread.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbMinimalProfitSpread_ItemClick);
            // 
            // bbAnalytics
            // 
            this.bbAnalytics.Caption = "Analytics";
            this.bbAnalytics.Id = 22;
            this.bbAnalytics.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbAnalytics.ImageOptions.Image")));
            this.bbAnalytics.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbAnalytics.ImageOptions.LargeImage")));
            this.bbAnalytics.Name = "bbAnalytics";
            this.bbAnalytics.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbAnalytics_ItemClick);
            // 
            // bbGridStrategy
            // 
            this.bbGridStrategy.ActAsDropDown = true;
            this.bbGridStrategy.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.bbGridStrategy.Caption = "GridStrategy";
            this.bbGridStrategy.Id = 23;
            this.bbGridStrategy.Name = "bbGridStrategy";
            this.bbGridStrategy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbGridStrategy_ItemClick);
            // 
            // bbShowTickerStrategies
            // 
            this.bbShowTickerStrategies.Caption = "Show Ticker Strategies";
            this.bbShowTickerStrategies.Id = 24;
            this.bbShowTickerStrategies.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbShowTickerStrategies.ImageOptions.Image")));
            this.bbShowTickerStrategies.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbShowTickerStrategies.ImageOptions.LargeImage")));
            this.bbShowTickerStrategies.Name = "bbShowTickerStrategies";
            // 
            // bbUpdateBot
            // 
            this.bbUpdateBot.Caption = "UpdateBot";
            this.bbUpdateBot.Id = 25;
            this.bbUpdateBot.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbUpdateBot.ImageOptions.Image")));
            this.bbUpdateBot.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbUpdateBot.ImageOptions.LargeImage")));
            this.bbUpdateBot.Name = "bbUpdateBot";
            this.bbUpdateBot.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbUpdateBot_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "barButtonItem2";
            this.barButtonItem2.Id = 26;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "barButtonItem3";
            this.barButtonItem3.Id = 27;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // bsShowTickerChart
            // 
            this.bsShowTickerChart.Caption = "Show Ticker Chart";
            this.bsShowTickerChart.Id = 28;
            this.bsShowTickerChart.Name = "bsShowTickerChart";
            // 
            // bsStrategies
            // 
            this.bsStrategies.Caption = "Strategies";
            this.bsStrategies.Id = 29;
            this.bsStrategies.Name = "bsStrategies";
            // 
            // bsShowOrderBookHistory
            // 
            this.bsShowOrderBookHistory.Caption = "Show Order Book History";
            this.bsShowOrderBookHistory.Id = 30;
            this.bsShowOrderBookHistory.Name = "bsShowOrderBookHistory";
            // 
            // bcShowNonZeroAmout
            // 
            this.bcShowNonZeroAmout.Caption = "Show NonZero Amount";
            this.bcShowNonZeroAmout.Id = 32;
            this.bcShowNonZeroAmout.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bcShowNonZeroAmout.ImageOptions.Image")));
            this.bcShowNonZeroAmout.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bcShowNonZeroAmout.ImageOptions.LargeImage")));
            this.bcShowNonZeroAmout.Name = "bcShowNonZeroAmout";
            this.bcShowNonZeroAmout.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bcShowNonZeroAmout_CheckedChanged);
            // 
            // rpPoloniex
            // 
            this.rpPoloniex.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2,
            this.ribbonPageGroup3});
            this.rpPoloniex.Name = "rpPoloniex";
            this.rpPoloniex.Text = "Arbitrage";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.AllowTextClipping = false;
            this.ribbonPageGroup1.ItemLinks.Add(this.btShowCombinedBidAsk);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbShowOrderBookHistory);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbAllCurrencies, true);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbMonitorSelected);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbOpenWeb);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbTryArbitrage);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbAnalytics);
            this.ribbonPageGroup1.ItemLinks.Add(this.bcShowNonZeroAmout);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.ShowCaptionButton = false;
            this.ribbonPageGroup1.Text = "Arbitrage";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.bbBuy);
            this.ribbonPageGroup2.ItemLinks.Add(this.beBuyLowestAsk);
            this.ribbonPageGroup2.ItemLinks.Add(this.bbSendToHighestBid);
            this.ribbonPageGroup2.ItemLinks.Add(this.bbSell, true);
            this.ribbonPageGroup2.ItemLinks.Add(this.beHighestBidSell);
            this.ribbonPageGroup2.ItemLinks.Add(this.bbSyncWithLowestAsk);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Manual Arbitrage";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.bbMinimalProfitSpread);
            this.ribbonPageGroup3.ItemLinks.Add(this.bbUpdateBot);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "Tools";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup4,
            this.ribbonPageGroup5});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Strategies";
            // 
            // ribbonPageGroup4
            // 
            this.ribbonPageGroup4.ItemLinks.Add(this.bbGridStrategy);
            this.ribbonPageGroup4.Name = "ribbonPageGroup4";
            this.ribbonPageGroup4.Text = "Strategies";
            // 
            // ribbonPageGroup5
            // 
            this.ribbonPageGroup5.ItemLinks.Add(this.bbShowTickerStrategies);
            this.ribbonPageGroup5.Name = "ribbonPageGroup5";
            this.ribbonPageGroup5.Text = "Show/Edit";
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit1.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // sidePanel1
            // 
            this.sidePanel1.Controls.Add(this.gridControl1);
            this.sidePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidePanel1.Location = new System.Drawing.Point(0, 259);
            this.sidePanel1.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.sidePanel1.Name = "sidePanel1";
            this.sidePanel1.Size = new System.Drawing.Size(3832, 1768);
            this.sidePanel1.TabIndex = 6;
            this.sidePanel1.Text = "sidePanel1";
            // 
            // popupMenu1
            // 
            this.popupMenu1.ItemLinks.Add(this.bsShowTickerChart);
            this.popupMenu1.ItemLinks.Add(this.bsStrategies);
            this.popupMenu1.ItemLinks.Add(this.bsShowOrderBookHistory);
            this.popupMenu1.Name = "popupMenu1";
            this.popupMenu1.Ribbon = this.ribbonControl1;
            // 
            // TickerArbitrageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(3832, 2027);
            this.Controls.Add(this.sidePanel1);
            this.Controls.Add(this.ribbonControl1);
            this.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.Name = "TickerArbitrageForm";
            this.Text = "Classic Arbitrage";
            this.Load += new System.EventHandler(this.TickerArbitrageForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tickerArbitrageInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            this.sidePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private GridView gridView1;
        private System.Windows.Forms.BindingSource tickerArbitrageInfoBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colBaseCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colMarketCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colTickers;
        private DevExpress.XtraGrid.Columns.GridColumn colCount;
        private DevExpress.XtraGrid.Columns.GridColumn colLowestAskTicker;
        private DevExpress.XtraGrid.Columns.GridColumn colHighestBidTicker;
        private DevExpress.XtraGrid.Columns.GridColumn colLowestAskHost;
        private DevExpress.XtraGrid.Columns.GridColumn colHighestBidHost;
        private DevExpress.XtraGrid.Columns.GridColumn colLowestAsk;
        private DevExpress.XtraGrid.Columns.GridColumn colHighestBid;
        private DevExpress.XtraGrid.Columns.GridColumn colSpread;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colLowestAksFee;
        private DevExpress.XtraGrid.Columns.GridColumn colHighestBidFee;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalFee;
        private DevExpress.XtraGrid.Columns.GridColumn colProfit;
        private DevExpress.XtraGrid.Columns.GridColumn colProfitUSD;
        private DevExpress.XtraGrid.Columns.GridColumn colBuyTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colLastUpdate;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpPoloniex;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraEditors.SidePanel sidePanel1;
        private DevExpress.XtraBars.BarCheckItem bbAllCurrencies;
        private DevExpress.XtraBars.BarButtonItem bbTryArbitrage;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateTime;
        private DevExpress.XtraGrid.Columns.GridColumn colIsActual;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarCheckItem bbMonitorSelected;
        private DevExpress.XtraGrid.Columns.GridColumn colIsSelected;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraBars.BarButtonItem bbOpenWeb;
        private DevExpress.XtraBars.BarButtonItem bbSelectPositive;
        private DevExpress.XtraBars.BarButtonItem bbBuy;
        private DevExpress.XtraBars.BarButtonItem bbSell;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem bbSendToHighestBid;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevExpress.XtraBars.BarEditItem beBuyLowestAsk;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit2;
        private DevExpress.XtraBars.BarEditItem beHighestBidSell;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit3;
        private DevExpress.XtraBars.BarButtonItem bbSyncWithLowestAsk;
        private DevExpress.XtraGrid.Columns.GridColumn colAvailableAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colAvailableProfit;
        private DevExpress.XtraGrid.Columns.GridColumn colAvailableProfitUSD;
        private DevExpress.XtraGrid.Columns.GridColumn colLowestAskEnabled;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit3;
        private DevExpress.XtraGrid.Columns.GridColumn colHighestBidEnabled;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraBars.BarButtonItem btShowCombinedBidAsk;
        private DevExpress.XtraGrid.Columns.GridColumn colLowestBidAskRelation;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;
        private DevExpress.XtraBars.BarButtonItem bbShowOrderBookHistory;
        private DevExpress.XtraGrid.Columns.GridColumn colSuppressNotification;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit4;
        private DevExpress.XtraGrid.Columns.GridColumn colHighestBidAskRelation;
        private DevExpress.XtraBars.BarButtonItem bbMinimalProfitSpread;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.BarButtonItem bbAnalytics;
        private DevExpress.XtraGrid.Columns.GridColumn colBidShift;
        private DevExpress.XtraGrid.Columns.GridColumn colAskShift;
        private DevExpress.XtraBars.BarButtonItem bbGridStrategy;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup4;
        private BarButtonItem bbShowTickerStrategies;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup5;
        private BarButtonItem bbUpdateBot;
        private DevExpress.XtraGrid.Columns.GridColumn colHipe;
        private DevExpress.XtraGrid.Columns.GridColumn colSellHipe;
        private BarButtonItem barButtonItem2;
        private BarButtonItem barButtonItem3;
        private BarSubItem bsShowTickerChart;
        private PopupMenu popupMenu1;
        private BarSubItem bsStrategies;
        private DevExpress.XtraGrid.Columns.GridColumn colBidEnergy;
        private DevExpress.XtraGrid.Columns.GridColumn colAskEnergy;
        private DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit repositoryItemSparklineEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit repositoryItemSparklineEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit repositoryItemSparklineEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit repositoryItemSparklineEdit4;
        private BarSubItem bsShowOrderBookHistory;
        private BarCheckItem bcShowNonZeroAmout;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalBalance;
        private DevExpress.XtraGrid.GridControl gridControl1;
    }
}