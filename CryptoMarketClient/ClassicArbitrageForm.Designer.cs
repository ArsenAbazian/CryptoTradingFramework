using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace CryptoMarketClient {
    partial class ClassicArbitrageForm {
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule5 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue5 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule6 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue6 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule7 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue7 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule8 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue8 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.Sparkline.BarSparklineView barSparklineView3 = new DevExpress.Sparkline.BarSparklineView();
            DevExpress.Sparkline.BarSparklineView barSparklineView4 = new DevExpress.Sparkline.BarSparklineView();
            DevExpress.Sparkline.AreaSparklineView areaSparklineView3 = new DevExpress.Sparkline.AreaSparklineView();
            DevExpress.Sparkline.AreaSparklineView areaSparklineView4 = new DevExpress.Sparkline.AreaSparklineView();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassicArbitrageForm));
            this.colProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsActual = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMarketCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
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
            this.bbGridStrategy = new DevExpress.XtraBars.BarButtonItem();
            this.bbShowTickerStrategies = new DevExpress.XtraBars.BarButtonItem();
            this.bbUpdateBot = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.bsShowTickerChart = new DevExpress.XtraBars.BarSubItem();
            this.bsStrategies = new DevExpress.XtraBars.BarSubItem();
            this.bsShowOrderBookHistory = new DevExpress.XtraBars.BarSubItem();
            this.bcShowNonZeroAmout = new DevExpress.XtraBars.BarCheckItem();
            this.biSelectExchanges = new DevExpress.XtraBars.BarButtonItem();
            this.biStart = new DevExpress.XtraBars.BarButtonItem();
            this.biStop = new DevExpress.XtraBars.BarButtonItem();
            this.biShowLog = new DevExpress.XtraBars.BarButtonItem();
            this.bcShowOnlySelected = new DevExpress.XtraBars.BarCheckItem();
            this.biRemoveUnselected = new DevExpress.XtraBars.BarButtonItem();
            this.biOpen = new DevExpress.XtraBars.BarButtonItem();
            this.biSave = new DevExpress.XtraBars.BarButtonItem();
            this.biNew = new DevExpress.XtraBars.BarButtonItem();
            this.biSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.biRemoveSelected = new DevExpress.XtraBars.BarButtonItem();
            this.rpArbitrage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup7 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup6 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.sidePanel1 = new DevExpress.XtraEditors.SidePanel();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.xtraOpenFileDialog1 = new DevExpress.XtraEditors.XtraOpenFileDialog(this.components);
            this.xtraSaveFileDialog1 = new DevExpress.XtraEditors.XtraSaveFileDialog(this.components);
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
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
            this.colProfit.MinWidth = 50;
            this.colProfit.Name = "colProfit";
            this.colProfit.OptionsColumn.AllowEdit = false;
            this.colProfit.OptionsColumn.ReadOnly = true;
            this.colProfit.Visible = true;
            this.colProfit.VisibleIndex = 12;
            this.colProfit.Width = 375;
            // 
            // colIsActual
            // 
            this.colIsActual.Caption = "IsActual";
            this.colIsActual.FieldName = "IsActual";
            this.colIsActual.MinWidth = 50;
            this.colIsActual.Name = "colIsActual";
            this.colIsActual.OptionsColumn.AllowEdit = false;
            this.colIsActual.Width = 187;
            // 
            // colMarketCurrency
            // 
            this.colMarketCurrency.Caption = "Market";
            this.colMarketCurrency.ColumnEdit = this.repositoryItemTextEdit1;
            this.colMarketCurrency.FieldName = "MarketCurrency";
            this.colMarketCurrency.MinWidth = 50;
            this.colMarketCurrency.Name = "colMarketCurrency";
            this.colMarketCurrency.OptionsColumn.AllowEdit = false;
            this.colMarketCurrency.Visible = true;
            this.colMarketCurrency.VisibleIndex = 4;
            this.colMarketCurrency.Width = 196;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            // 
            // colLowestAskEnabled
            // 
            this.colLowestAskEnabled.Caption = "LowestAsk Enabled";
            this.colLowestAskEnabled.ColumnEdit = this.repositoryItemCheckEdit3;
            this.colLowestAskEnabled.FieldName = "LowestAskEnabled";
            this.colLowestAskEnabled.MinWidth = 50;
            this.colLowestAskEnabled.Name = "colLowestAskEnabled";
            this.colLowestAskEnabled.Width = 187;
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
            this.colLowestAskHost.MinWidth = 50;
            this.colLowestAskHost.Name = "colLowestAskHost";
            this.colLowestAskHost.OptionsColumn.AllowEdit = false;
            this.colLowestAskHost.OptionsColumn.ReadOnly = true;
            this.colLowestAskHost.Visible = true;
            this.colLowestAskHost.VisibleIndex = 5;
            this.colLowestAskHost.Width = 166;
            // 
            // colHighestBidEnabled
            // 
            this.colHighestBidEnabled.Caption = "HighestBid Enabled";
            this.colHighestBidEnabled.ColumnEdit = this.repositoryItemCheckEdit2;
            this.colHighestBidEnabled.FieldName = "HighestBidEnabled";
            this.colHighestBidEnabled.MinWidth = 50;
            this.colHighestBidEnabled.Name = "colHighestBidEnabled";
            this.colHighestBidEnabled.Width = 187;
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
            this.colHighestBidHost.MinWidth = 50;
            this.colHighestBidHost.Name = "colHighestBidHost";
            this.colHighestBidHost.OptionsColumn.AllowEdit = false;
            this.colHighestBidHost.OptionsColumn.ReadOnly = true;
            this.colHighestBidHost.Visible = true;
            this.colHighestBidHost.VisibleIndex = 6;
            this.colHighestBidHost.Width = 159;
            // 
            // colUpdateTime
            // 
            this.colUpdateTime.Caption = "UpdateTime Ms";
            this.colUpdateTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colUpdateTime.FieldName = "UpdateTimeMs";
            this.colUpdateTime.MinWidth = 50;
            this.colUpdateTime.Name = "colUpdateTime";
            this.colUpdateTime.OptionsColumn.AllowEdit = false;
            this.colUpdateTime.Width = 187;
            // 
            // colProfitUSD
            // 
            this.colProfitUSD.Caption = "In USD";
            this.colProfitUSD.DisplayFormat.FormatString = "0.00000000";
            this.colProfitUSD.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colProfitUSD.FieldName = "MaxProfitUSD";
            this.colProfitUSD.MinWidth = 50;
            this.colProfitUSD.Name = "colProfitUSD";
            this.colProfitUSD.OptionsColumn.AllowEdit = false;
            this.colProfitUSD.OptionsColumn.ReadOnly = true;
            this.colProfitUSD.Visible = true;
            this.colProfitUSD.VisibleIndex = 13;
            this.colProfitUSD.Width = 414;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.tickerArbitrageInfoBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(15, 16, 15, 16);
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
            this.repositoryItemSparklineEdit4,
            this.repositoryItemTextEdit1});
            this.gridControl1.Size = new System.Drawing.Size(3530, 868);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseDown);
            // 
            // tickerArbitrageInfoBindingSource
            // 
            this.tickerArbitrageInfoBindingSource.DataSource = typeof(Crypto.Core.TickerCollection);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.Row.FontSizeDelta = 1;
            this.gridView1.Appearance.Row.Options.UseFont = true;
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
            this.gridView1.CustomizationFormBounds = new System.Drawing.Rectangle(1258, 1342, 629, 309);
            this.gridView1.DetailHeight = 888;
            this.gridView1.FixedLineWidth = 4;
            gridFormatRule5.Column = this.colProfit;
            gridFormatRule5.ColumnApplyTo = this.colProfit;
            gridFormatRule5.Name = "ArbitrageSpreadRule";
            formatConditionRuleValue5.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            formatConditionRuleValue5.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            formatConditionRuleValue5.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue5.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue5.Condition = DevExpress.XtraEditors.FormatCondition.Greater;
            formatConditionRuleValue5.Value1 = 0D;
            gridFormatRule5.Rule = formatConditionRuleValue5;
            gridFormatRule6.Column = this.colIsActual;
            gridFormatRule6.ColumnApplyTo = this.colMarketCurrency;
            gridFormatRule6.Name = "FormatNotActual";
            formatConditionRuleValue6.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            formatConditionRuleValue6.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            formatConditionRuleValue6.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue6.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue6.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue6.Value1 = false;
            gridFormatRule6.Rule = formatConditionRuleValue6;
            gridFormatRule7.Column = this.colLowestAskEnabled;
            gridFormatRule7.ColumnApplyTo = this.colLowestAskHost;
            gridFormatRule7.Enabled = false;
            gridFormatRule7.Name = "LowestAskEnabledRule";
            formatConditionRuleValue7.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            formatConditionRuleValue7.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue7.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue7.Value1 = false;
            gridFormatRule7.Rule = formatConditionRuleValue7;
            gridFormatRule8.Column = this.colHighestBidEnabled;
            gridFormatRule8.ColumnApplyTo = this.colHighestBidHost;
            gridFormatRule8.Enabled = false;
            gridFormatRule8.Name = "HighestBidEnabledRule";
            formatConditionRuleValue8.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            formatConditionRuleValue8.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue8.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue8.Value1 = false;
            gridFormatRule8.Rule = formatConditionRuleValue8;
            this.gridView1.FormatRules.Add(gridFormatRule5);
            this.gridView1.FormatRules.Add(gridFormatRule6);
            this.gridView1.FormatRules.Add(gridFormatRule7);
            this.gridView1.FormatRules.Add(gridFormatRule8);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colSpread, DevExpress.Data.ColumnSortOrder.Descending)});
            this.gridView1.Click += new System.EventHandler(this.gridView1_Click);
            // 
            // colIsSelected
            // 
            this.colIsSelected.Caption = "Selected";
            this.colIsSelected.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsSelected.FieldName = "IsSelected";
            this.colIsSelected.MaxWidth = 75;
            this.colIsSelected.MinWidth = 75;
            this.colIsSelected.Name = "colIsSelected";
            this.colIsSelected.Visible = true;
            this.colIsSelected.VisibleIndex = 0;
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
            this.colBaseCurrency.MinWidth = 50;
            this.colBaseCurrency.Name = "colBaseCurrency";
            this.colBaseCurrency.OptionsColumn.AllowEdit = false;
            this.colBaseCurrency.Visible = true;
            this.colBaseCurrency.VisibleIndex = 3;
            this.colBaseCurrency.Width = 171;
            // 
            // colLastUpdate
            // 
            this.colLastUpdate.DisplayFormat.FormatString = "HH:mm:ss.fff";
            this.colLastUpdate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colLastUpdate.FieldName = "LastUpdate";
            this.colLastUpdate.MinWidth = 50;
            this.colLastUpdate.Name = "colLastUpdate";
            this.colLastUpdate.OptionsColumn.AllowEdit = false;
            this.colLastUpdate.Visible = true;
            this.colLastUpdate.VisibleIndex = 2;
            this.colLastUpdate.Width = 279;
            // 
            // colTickers
            // 
            this.colTickers.FieldName = "Tickers";
            this.colTickers.MinWidth = 50;
            this.colTickers.Name = "colTickers";
            this.colTickers.OptionsColumn.AllowEdit = false;
            this.colTickers.OptionsColumn.ReadOnly = true;
            this.colTickers.Width = 187;
            // 
            // colCount
            // 
            this.colCount.FieldName = "Count";
            this.colCount.MinWidth = 50;
            this.colCount.Name = "colCount";
            this.colCount.OptionsColumn.AllowEdit = false;
            this.colCount.OptionsColumn.ReadOnly = true;
            this.colCount.Width = 187;
            // 
            // colLowestAskTicker
            // 
            this.colLowestAskTicker.FieldName = "LowestAskTicker";
            this.colLowestAskTicker.MinWidth = 50;
            this.colLowestAskTicker.Name = "colLowestAskTicker";
            this.colLowestAskTicker.OptionsColumn.AllowEdit = false;
            this.colLowestAskTicker.OptionsColumn.ReadOnly = true;
            this.colLowestAskTicker.Width = 187;
            // 
            // colHighestBidTicker
            // 
            this.colHighestBidTicker.FieldName = "HighestBidTicker";
            this.colHighestBidTicker.MinWidth = 50;
            this.colHighestBidTicker.Name = "colHighestBidTicker";
            this.colHighestBidTicker.OptionsColumn.AllowEdit = false;
            this.colHighestBidTicker.OptionsColumn.ReadOnly = true;
            this.colHighestBidTicker.Width = 187;
            // 
            // colLowestAsk
            // 
            this.colLowestAsk.DisplayFormat.FormatString = "0.00000000";
            this.colLowestAsk.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colLowestAsk.FieldName = "LowestAsk";
            this.colLowestAsk.MinWidth = 50;
            this.colLowestAsk.Name = "colLowestAsk";
            this.colLowestAsk.OptionsColumn.AllowEdit = false;
            this.colLowestAsk.OptionsColumn.ReadOnly = true;
            this.colLowestAsk.Visible = true;
            this.colLowestAsk.VisibleIndex = 7;
            this.colLowestAsk.Width = 170;
            // 
            // colHighestBid
            // 
            this.colHighestBid.DisplayFormat.FormatString = "0.00000000";
            this.colHighestBid.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHighestBid.FieldName = "HighestBid";
            this.colHighestBid.MinWidth = 50;
            this.colHighestBid.Name = "colHighestBid";
            this.colHighestBid.OptionsColumn.AllowEdit = false;
            this.colHighestBid.OptionsColumn.ReadOnly = true;
            this.colHighestBid.Visible = true;
            this.colHighestBid.VisibleIndex = 8;
            this.colHighestBid.Width = 271;
            // 
            // colSpread
            // 
            this.colSpread.DisplayFormat.FormatString = "0.00000000#";
            this.colSpread.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSpread.FieldName = "Spread";
            this.colSpread.MinWidth = 50;
            this.colSpread.Name = "colSpread";
            this.colSpread.OptionsColumn.AllowEdit = false;
            this.colSpread.Visible = true;
            this.colSpread.VisibleIndex = 9;
            this.colSpread.Width = 366;
            // 
            // colAmount
            // 
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "Amount";
            this.colAmount.MinWidth = 50;
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            this.colAmount.OptionsColumn.ReadOnly = true;
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 10;
            this.colAmount.Width = 375;
            // 
            // colBuyTotal
            // 
            this.colBuyTotal.DisplayFormat.FormatString = "0.00000000";
            this.colBuyTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBuyTotal.FieldName = "BuyTotal";
            this.colBuyTotal.MinWidth = 50;
            this.colBuyTotal.Name = "colBuyTotal";
            this.colBuyTotal.OptionsColumn.AllowEdit = false;
            this.colBuyTotal.Visible = true;
            this.colBuyTotal.VisibleIndex = 11;
            this.colBuyTotal.Width = 375;
            // 
            // colTotal
            // 
            this.colTotal.DisplayFormat.FormatString = "0.00000000";
            this.colTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotal.FieldName = "Total";
            this.colTotal.MinWidth = 50;
            this.colTotal.Name = "colTotal";
            this.colTotal.OptionsColumn.AllowEdit = false;
            this.colTotal.Width = 187;
            // 
            // colLowestAksFee
            // 
            this.colLowestAksFee.FieldName = "LowestAksFee";
            this.colLowestAksFee.MinWidth = 50;
            this.colLowestAksFee.Name = "colLowestAksFee";
            this.colLowestAksFee.OptionsColumn.AllowEdit = false;
            this.colLowestAksFee.OptionsColumn.ReadOnly = true;
            this.colLowestAksFee.Width = 187;
            // 
            // colHighestBidFee
            // 
            this.colHighestBidFee.FieldName = "HighestBidFee";
            this.colHighestBidFee.MinWidth = 50;
            this.colHighestBidFee.Name = "colHighestBidFee";
            this.colHighestBidFee.OptionsColumn.AllowEdit = false;
            this.colHighestBidFee.OptionsColumn.ReadOnly = true;
            this.colHighestBidFee.Width = 187;
            // 
            // colTotalFee
            // 
            this.colTotalFee.DisplayFormat.FormatString = "0.00000000";
            this.colTotalFee.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalFee.FieldName = "TotalFee";
            this.colTotalFee.MinWidth = 50;
            this.colTotalFee.Name = "colTotalFee";
            this.colTotalFee.OptionsColumn.AllowEdit = false;
            this.colTotalFee.OptionsColumn.ReadOnly = true;
            this.colTotalFee.Width = 187;
            // 
            // colAvailableAmount
            // 
            this.colAvailableAmount.Caption = "AvailableAmount";
            this.colAvailableAmount.FieldName = "AvailableAmount";
            this.colAvailableAmount.MinWidth = 50;
            this.colAvailableAmount.Name = "colAvailableAmount";
            this.colAvailableAmount.OptionsColumn.AllowEdit = false;
            this.colAvailableAmount.Width = 187;
            // 
            // colAvailableProfit
            // 
            this.colAvailableProfit.Caption = "AvailableProfit";
            this.colAvailableProfit.DisplayFormat.FormatString = "0.00000000";
            this.colAvailableProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAvailableProfit.FieldName = "AvailableProfit";
            this.colAvailableProfit.MinWidth = 50;
            this.colAvailableProfit.Name = "colAvailableProfit";
            this.colAvailableProfit.OptionsColumn.AllowEdit = false;
            this.colAvailableProfit.Width = 187;
            // 
            // colAvailableProfitUSD
            // 
            this.colAvailableProfitUSD.Caption = "AvailableProfitUSD";
            this.colAvailableProfitUSD.DisplayFormat.FormatString = "0.00000000";
            this.colAvailableProfitUSD.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAvailableProfitUSD.FieldName = "AvailableProfitUSD";
            this.colAvailableProfitUSD.MinWidth = 50;
            this.colAvailableProfitUSD.Name = "colAvailableProfitUSD";
            this.colAvailableProfitUSD.OptionsColumn.AllowEdit = false;
            this.colAvailableProfitUSD.Width = 187;
            // 
            // colLowestBidAskRelation
            // 
            this.colLowestBidAskRelation.Caption = "Lowest Bid/Ask";
            this.colLowestBidAskRelation.ColumnEdit = this.repositoryItemProgressBar1;
            this.colLowestBidAskRelation.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colLowestBidAskRelation.FieldName = "LowestBidAskRelation";
            this.colLowestBidAskRelation.MinWidth = 50;
            this.colLowestBidAskRelation.Name = "colLowestBidAskRelation";
            this.colLowestBidAskRelation.OptionsColumn.AllowEdit = false;
            this.colLowestBidAskRelation.Width = 187;
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
            this.colHighestBidAskRelation.MinWidth = 50;
            this.colHighestBidAskRelation.Name = "colHighestBidAskRelation";
            this.colHighestBidAskRelation.Width = 187;
            // 
            // colSuppressNotification
            // 
            this.colSuppressNotification.Caption = "Disable";
            this.colSuppressNotification.ColumnEdit = this.repositoryItemCheckEdit4;
            this.colSuppressNotification.FieldName = "Disabled";
            this.colSuppressNotification.MaxWidth = 75;
            this.colSuppressNotification.MinWidth = 75;
            this.colSuppressNotification.Name = "colSuppressNotification";
            this.colSuppressNotification.Visible = true;
            this.colSuppressNotification.VisibleIndex = 1;
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
            this.colBidShift.MinWidth = 50;
            this.colBidShift.Name = "colBidShift";
            this.colBidShift.Width = 187;
            // 
            // colAskShift
            // 
            this.colAskShift.Caption = "Ask Shift";
            this.colAskShift.DisplayFormat.FormatString = "0.#####";
            this.colAskShift.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAskShift.FieldName = "AskShift";
            this.colAskShift.MinWidth = 50;
            this.colAskShift.Name = "colAskShift";
            this.colAskShift.Width = 187;
            // 
            // colHipe
            // 
            this.colHipe.Caption = "BidHipe";
            this.colHipe.ColumnEdit = this.repositoryItemSparklineEdit1;
            this.colHipe.FieldName = "BidHipes";
            this.colHipe.MinWidth = 50;
            this.colHipe.Name = "colHipe";
            this.colHipe.OptionsColumn.AllowEdit = false;
            this.colHipe.Width = 187;
            // 
            // repositoryItemSparklineEdit1
            // 
            this.repositoryItemSparklineEdit1.Name = "repositoryItemSparklineEdit1";
            this.repositoryItemSparklineEdit1.ValueRange.IsAuto = false;
            barSparklineView3.Color = System.Drawing.Color.Green;
            this.repositoryItemSparklineEdit1.View = barSparklineView3;
            // 
            // colSellHipe
            // 
            this.colSellHipe.Caption = "AskHipe";
            this.colSellHipe.ColumnEdit = this.repositoryItemSparklineEdit2;
            this.colSellHipe.FieldName = "AskHipes";
            this.colSellHipe.MinWidth = 50;
            this.colSellHipe.Name = "colSellHipe";
            this.colSellHipe.OptionsColumn.AllowEdit = false;
            this.colSellHipe.Width = 187;
            // 
            // repositoryItemSparklineEdit2
            // 
            this.repositoryItemSparklineEdit2.Name = "repositoryItemSparklineEdit2";
            barSparklineView4.Color = System.Drawing.Color.Red;
            this.repositoryItemSparklineEdit2.View = barSparklineView4;
            // 
            // colBidEnergy
            // 
            this.colBidEnergy.Caption = "Bid Energies";
            this.colBidEnergy.ColumnEdit = this.repositoryItemSparklineEdit3;
            this.colBidEnergy.FieldName = "BidEnergies";
            this.colBidEnergy.MinWidth = 50;
            this.colBidEnergy.Name = "colBidEnergy";
            this.colBidEnergy.OptionsColumn.AllowEdit = false;
            this.colBidEnergy.Width = 187;
            // 
            // repositoryItemSparklineEdit3
            // 
            this.repositoryItemSparklineEdit3.Name = "repositoryItemSparklineEdit3";
            areaSparklineView3.Color = System.Drawing.Color.Green;
            areaSparklineView3.ScaleFactor = 2F;
            this.repositoryItemSparklineEdit3.View = areaSparklineView3;
            // 
            // colAskEnergy
            // 
            this.colAskEnergy.Caption = "Ask Energies";
            this.colAskEnergy.ColumnEdit = this.repositoryItemSparklineEdit4;
            this.colAskEnergy.FieldName = "AskEnergies";
            this.colAskEnergy.MinWidth = 50;
            this.colAskEnergy.Name = "colAskEnergy";
            this.colAskEnergy.OptionsColumn.AllowEdit = false;
            this.colAskEnergy.Width = 187;
            // 
            // repositoryItemSparklineEdit4
            // 
            this.repositoryItemSparklineEdit4.Name = "repositoryItemSparklineEdit4";
            areaSparklineView4.Color = System.Drawing.Color.Red;
            areaSparklineView4.ScaleFactor = 2F;
            this.repositoryItemSparklineEdit4.View = areaSparklineView4;
            // 
            // colTotalBalance
            // 
            this.colTotalBalance.Caption = "TotalBalance";
            this.colTotalBalance.FieldName = "TotalBalance";
            this.colTotalBalance.MinWidth = 50;
            this.colTotalBalance.Name = "colTotalBalance";
            this.colTotalBalance.OptionsColumn.AllowEdit = false;
            this.colTotalBalance.Width = 379;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.EmptyAreaImageOptions.ImagePadding = new System.Windows.Forms.Padding(37, 40, 37, 40);
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
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
            this.bbGridStrategy,
            this.bbShowTickerStrategies,
            this.bbUpdateBot,
            this.barButtonItem2,
            this.barButtonItem3,
            this.bsShowTickerChart,
            this.bsStrategies,
            this.bsShowOrderBookHistory,
            this.bcShowNonZeroAmout,
            this.biSelectExchanges,
            this.biStart,
            this.biStop,
            this.biShowLog,
            this.bcShowOnlySelected,
            this.biRemoveUnselected,
            this.biOpen,
            this.biSave,
            this.biNew,
            this.biSaveAs,
            this.barButtonItem4,
            this.barSubItem1,
            this.biRemoveSelected,
            this.barSubItem2});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Margin = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.ribbonControl1.MaxItemId = 47;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.OptionsMenuMinWidth = 412;
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpArbitrage});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEdit1,
            this.repositoryItemSpinEdit2,
            this.repositoryItemSpinEdit3});
            this.ribbonControl1.Size = new System.Drawing.Size(3530, 364);
            // 
            // bbAllCurrencies
            // 
            this.bbAllCurrencies.Caption = "Show Positive Profits";
            this.bbAllCurrencies.Id = 3;
            this.bbAllCurrencies.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbAllCurrencies.ImageOptions.SvgImage")));
            this.bbAllCurrencies.Name = "bbAllCurrencies";
            this.bbAllCurrencies.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)((DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.bbAllCurrencies.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bbAllCurrencies_CheckedChanged);
            // 
            // bbTryArbitrage
            // 
            this.bbTryArbitrage.Caption = "Try Arbitrage!";
            this.bbTryArbitrage.Id = 5;
            this.bbTryArbitrage.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbTryArbitrage.ImageOptions.SvgImage")));
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
            this.bbMonitorSelected.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbMonitorSelected.ImageOptions.SvgImage")));
            this.bbMonitorSelected.Name = "bbMonitorSelected";
            this.bbMonitorSelected.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            // 
            // bbOpenWeb
            // 
            this.bbOpenWeb.Caption = "Open Markets in Web";
            this.bbOpenWeb.Id = 8;
            this.bbOpenWeb.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbOpenWeb.ImageOptions.SvgImage")));
            this.bbOpenWeb.Name = "bbOpenWeb";
            this.bbOpenWeb.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
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
            this.bbSyncWithLowestAsk.Caption = "Send To Lowest Ask Market";
            this.bbSyncWithLowestAsk.Id = 16;
            this.bbSyncWithLowestAsk.Name = "bbSyncWithLowestAsk";
            this.bbSyncWithLowestAsk.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbSyncWithLowestAsk_ItemClick);
            // 
            // btShowCombinedBidAsk
            // 
            this.btShowCombinedBidAsk.Caption = "Ticker Chart";
            this.btShowCombinedBidAsk.Id = 19;
            this.btShowCombinedBidAsk.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btShowCombinedBidAsk.ImageOptions.SvgImage")));
            this.btShowCombinedBidAsk.Name = "btShowCombinedBidAsk";
            this.btShowCombinedBidAsk.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btShowCombinedBidAsk_ItemClick);
            // 
            // bbShowOrderBookHistory
            // 
            this.bbShowOrderBookHistory.Caption = "Ticker History";
            this.bbShowOrderBookHistory.Id = 20;
            this.bbShowOrderBookHistory.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbShowOrderBookHistory.ImageOptions.SvgImage")));
            this.bbShowOrderBookHistory.Name = "bbShowOrderBookHistory";
            this.bbShowOrderBookHistory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbShowOrderBookHistory_ItemClick);
            // 
            // bbMinimalProfitSpread
            // 
            this.bbMinimalProfitSpread.Caption = "Minimal Profit Spread";
            this.bbMinimalProfitSpread.Id = 21;
            this.bbMinimalProfitSpread.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbMinimalProfitSpread.ImageOptions.SvgImage")));
            this.bbMinimalProfitSpread.Name = "bbMinimalProfitSpread";
            this.bbMinimalProfitSpread.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbMinimalProfitSpread_ItemClick);
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
            this.bbUpdateBot.Caption = "Update Telegram Bot";
            this.bbUpdateBot.Id = 25;
            this.bbUpdateBot.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbUpdateBot.ImageOptions.SvgImage")));
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
            this.bcShowNonZeroAmout.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bcShowNonZeroAmout.ImageOptions.SvgImage")));
            this.bcShowNonZeroAmout.Name = "bcShowNonZeroAmout";
            this.bcShowNonZeroAmout.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bcShowNonZeroAmout_CheckedChanged);
            // 
            // biSelectExchanges
            // 
            this.biSelectExchanges.Caption = "Select Exchanges";
            this.biSelectExchanges.Id = 33;
            this.biSelectExchanges.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biSelectExchanges.ImageOptions.SvgImage")));
            this.biSelectExchanges.Name = "biSelectExchanges";
            this.biSelectExchanges.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biSelectExchanges_ItemClick);
            // 
            // biStart
            // 
            this.biStart.Caption = "Start";
            this.biStart.Id = 34;
            this.biStart.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biStart.ImageOptions.SvgImage")));
            this.biStart.Name = "biStart";
            this.biStart.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biStart_ItemClick);
            // 
            // biStop
            // 
            this.biStop.Caption = "Stop";
            this.biStop.Id = 35;
            this.biStop.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biStop.ImageOptions.SvgImage")));
            this.biStop.Name = "biStop";
            this.biStop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biStop_ItemClick);
            // 
            // biShowLog
            // 
            this.biShowLog.Caption = "Show Log";
            this.biShowLog.Id = 36;
            this.biShowLog.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biShowLog.ImageOptions.SvgImage")));
            this.biShowLog.Name = "biShowLog";
            this.biShowLog.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biShowLog_ItemClick);
            // 
            // bcShowOnlySelected
            // 
            this.bcShowOnlySelected.Caption = "Show Only Selected";
            this.bcShowOnlySelected.Id = 37;
            this.bcShowOnlySelected.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bcShowOnlySelected.ImageOptions.SvgImage")));
            this.bcShowOnlySelected.Name = "bcShowOnlySelected";
            this.bcShowOnlySelected.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bcShowOnlySelected_CheckedChanged);
            // 
            // biRemoveUnselected
            // 
            this.biRemoveUnselected.Caption = "Remove Unselected";
            this.biRemoveUnselected.Id = 38;
            this.biRemoveUnselected.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biRemoveUnselected.ImageOptions.SvgImage")));
            this.biRemoveUnselected.Name = "biRemoveUnselected";
            this.biRemoveUnselected.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biRemoveUnselected_ItemClick);
            // 
            // biOpen
            // 
            this.biOpen.Caption = "Open";
            this.biOpen.Id = 39;
            this.biOpen.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biOpen.ImageOptions.SvgImage")));
            this.biOpen.Name = "biOpen";
            this.biOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biOpen_ItemClick);
            // 
            // biSave
            // 
            this.biSave.Caption = "Save";
            this.biSave.Id = 40;
            this.biSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biSave.ImageOptions.SvgImage")));
            this.biSave.Name = "biSave";
            this.biSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biSave_ItemClick);
            // 
            // biNew
            // 
            this.biNew.Caption = "New";
            this.biNew.Id = 41;
            this.biNew.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biNew.ImageOptions.SvgImage")));
            this.biNew.Name = "biNew";
            this.biNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biNew_ItemClick);
            // 
            // biSaveAs
            // 
            this.biSaveAs.Caption = "Save As";
            this.biSaveAs.Id = 42;
            this.biSaveAs.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biSaveAs.ImageOptions.SvgImage")));
            this.biSaveAs.Name = "biSaveAs";
            this.biSaveAs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biSaveAs_ItemClick);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "All History";
            this.barButtonItem4.Id = 43;
            this.barButtonItem4.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem4.ImageOptions.SvgImage")));
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem4_ItemClick);
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "Filter";
            this.barSubItem1.Id = 44;
            this.barSubItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barSubItem1.ImageOptions.SvgImage")));
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbAllCurrencies),
            new DevExpress.XtraBars.LinkPersistInfo(this.bcShowNonZeroAmout),
            new DevExpress.XtraBars.LinkPersistInfo(this.bcShowOnlySelected)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // biRemoveSelected
            // 
            this.biRemoveSelected.Caption = "Remove Selected";
            this.biRemoveSelected.Id = 45;
            this.biRemoveSelected.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biRemoveSelected.ImageOptions.SvgImage")));
            this.biRemoveSelected.Name = "biRemoveSelected";
            this.biRemoveSelected.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biRemoveSelected_ItemClick);
            // 
            // rpArbitrage
            // 
            this.rpArbitrage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup7,
            this.ribbonPageGroup6,
            this.ribbonPageGroup1,
            this.ribbonPageGroup2,
            this.ribbonPageGroup3});
            this.rpArbitrage.Name = "rpArbitrage";
            this.rpArbitrage.Text = "Classic Arbitrage";
            // 
            // ribbonPageGroup7
            // 
            this.ribbonPageGroup7.ItemLinks.Add(this.biNew);
            this.ribbonPageGroup7.ItemLinks.Add(this.biOpen);
            this.ribbonPageGroup7.ItemLinks.Add(this.biSave);
            this.ribbonPageGroup7.ItemLinks.Add(this.biSaveAs);
            this.ribbonPageGroup7.Name = "ribbonPageGroup7";
            this.ribbonPageGroup7.Text = "File";
            // 
            // ribbonPageGroup6
            // 
            this.ribbonPageGroup6.ItemLinks.Add(this.biSelectExchanges);
            this.ribbonPageGroup6.ItemLinks.Add(this.biStart);
            this.ribbonPageGroup6.ItemLinks.Add(this.biStop);
            this.ribbonPageGroup6.ItemLinks.Add(this.bbTryArbitrage);
            this.ribbonPageGroup6.ItemLinks.Add(this.bbOpenWeb);
            this.ribbonPageGroup6.Name = "ribbonPageGroup6";
            this.ribbonPageGroup6.Text = "Run";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.AllowTextClipping = false;
            this.ribbonPageGroup1.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonPageGroup1.ItemLinks.Add(this.biRemoveUnselected, true);
            this.ribbonPageGroup1.ItemLinks.Add(this.biRemoveSelected);
            this.ribbonPageGroup1.ItemLinks.Add(this.btShowCombinedBidAsk, true);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbShowOrderBookHistory);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem4);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbMonitorSelected, true);
            this.ribbonPageGroup1.ItemLinks.Add(this.barSubItem1);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "View / Filter";
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
            this.ribbonPageGroup3.ItemLinks.Add(this.biShowLog);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "Tools";
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
            this.sidePanel1.Location = new System.Drawing.Point(0, 364);
            this.sidePanel1.Margin = new System.Windows.Forms.Padding(10, 11, 10, 11);
            this.sidePanel1.Name = "sidePanel1";
            this.sidePanel1.Size = new System.Drawing.Size(3530, 868);
            this.sidePanel1.TabIndex = 6;
            this.sidePanel1.Text = "sidePanel1";
            // 
            // popupMenu1
            // 
            this.popupMenu1.ItemLinks.Add(this.bsShowTickerChart);
            this.popupMenu1.ItemLinks.Add(this.bsShowOrderBookHistory);
            this.popupMenu1.ItemLinks.Add(this.bbTryArbitrage);
            this.popupMenu1.ItemLinks.Add(this.bbOpenWeb);
            this.popupMenu1.ItemLinks.Add(this.barSubItem2);
            this.popupMenu1.Name = "popupMenu1";
            this.popupMenu1.Ribbon = this.ribbonControl1;
            // 
            // xtraOpenFileDialog1
            // 
            this.xtraOpenFileDialog1.FileName = "xtraOpenFileDialog1";
            this.xtraOpenFileDialog1.Filter = "Xml Files|*.xml|All Files|*.*";
            // 
            // xtraSaveFileDialog1
            // 
            this.xtraSaveFileDialog1.FileName = "xtraSaveFileDialog1";
            this.xtraSaveFileDialog1.Filter = "Xml Files|*.xml|All Files|*.*";
            // 
            // barSubItem2
            // 
            this.barSubItem2.Caption = "Manual Arbitrage";
            this.barSubItem2.Id = 46;
            this.barSubItem2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbBuy),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbSendToHighestBid),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbSell),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbSyncWithLowestAsk)});
            this.barSubItem2.Name = "barSubItem2";
            // 
            // ClassicArbitrageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 33F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(3530, 1232);
            this.Controls.Add(this.sidePanel1);
            this.Controls.Add(this.ribbonControl1);
            this.Margin = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.Name = "ClassicArbitrageForm";
            this.Text = "Classic Arbitrage";
            this.Load += new System.EventHandler(this.TickerArbitrageForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
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
        private DevExpress.XtraBars.Ribbon.RibbonPage rpArbitrage;
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
        private DevExpress.XtraGrid.Columns.GridColumn colBidShift;
        private DevExpress.XtraGrid.Columns.GridColumn colAskShift;
        private DevExpress.XtraBars.BarButtonItem bbGridStrategy;
        private BarButtonItem bbShowTickerStrategies;
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
        private BarButtonItem biSelectExchanges;
        private BarButtonItem biStart;
        private BarButtonItem biStop;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup6;
        private BarButtonItem biShowLog;
        private BarCheckItem bcShowOnlySelected;
        private BarButtonItem biRemoveUnselected;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private BarButtonItem biOpen;
        private BarButtonItem biSave;
        private BarButtonItem biNew;
        private DevExpress.XtraEditors.XtraOpenFileDialog xtraOpenFileDialog1;
        private DevExpress.XtraEditors.XtraSaveFileDialog xtraSaveFileDialog1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup7;
        private BarButtonItem biSaveAs;
        private BarButtonItem barButtonItem4;
        private BarSubItem barSubItem1;
        private BarButtonItem biRemoveSelected;
        private BarSubItem barSubItem2;
    }
}