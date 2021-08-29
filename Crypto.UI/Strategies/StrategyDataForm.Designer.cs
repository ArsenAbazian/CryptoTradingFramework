using Crypto.Core.Common;
using DevExpress.XtraBars;
using DevExpress.XtraCharts;

namespace CryptoMarketClient.Strategies {
    partial class StrategyDataForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StrategyDataForm));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue2 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraBars.Docking2010.Views.Tabbed.DockingContainer dockingContainer1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.DockingContainer();
            this.colType1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.documentGroup1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup(this.components);
            this.document4 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document(this.components);
            this.document5 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document(this.components);
            this.document3 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document(this.components);
            this.document2 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document(this.components);
            this.document1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document(this.components);
            this.gcData = new DevExpress.XtraGrid.GridControl();
            this.gvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.biCustomize = new DevExpress.XtraBars.BarButtonItem();
            this.biReset = new DevExpress.XtraBars.BarButtonItem();
            this.bsPanes = new DevExpress.XtraBars.BarSubItem();
            this.bsIndex = new DevExpress.XtraBars.BarStaticItem();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.sbTickersData = new DevExpress.XtraBars.BarSubItem();
            this.sbTradeHistory = new DevExpress.XtraBars.BarSubItem();
            this.sbTickersDataTable = new DevExpress.XtraBars.BarSubItem();
            this.sbTradeHistoryTable = new DevExpress.XtraBars.BarSubItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dpData = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dpEvents = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.gcEvent = new DevExpress.XtraGrid.GridControl();
            this.strategyHistoryItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvEvent = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyDeposit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSellDeposit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.dpTradeHistory = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel3_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.gcTradeHistory = new DevExpress.XtraGrid.GridControl();
            this.tradingResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvTradeHistory = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colOrderNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemCheckEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.dpChart = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel4_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.chartDataControl1 = new Crypto.UI.Forms.ChartDataControl();
            this.dpPositions = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.tcPosition = new DevExpress.XtraTreeList.TreeList();
            this.biShowChart = new DevExpress.XtraBars.BarButtonItem();
            this.biShowTable = new DevExpress.XtraBars.BarButtonItem();
            this.biBoth = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemCheckEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.documentManager1 = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.tabbedView1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            this.pmShowData = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.documentGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.document4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.document5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.document3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.document2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.document1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dpData.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.dpEvents.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcEvent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.strategyHistoryItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEvent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            this.dpTradeHistory.SuspendLayout();
            this.dockPanel3_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTradeHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tradingResultBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTradeHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).BeginInit();
            this.dpChart.SuspendLayout();
            this.dockPanel4_Container.SuspendLayout();
            this.dpPositions.SuspendLayout();
            this.controlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmShowData)).BeginInit();
            this.SuspendLayout();
            // 
            // colType1
            // 
            this.colType1.FieldName = "Type";
            this.colType1.MinWidth = 40;
            this.colType1.Name = "colType1";
            this.colType1.Visible = true;
            this.colType1.VisibleIndex = 1;
            this.colType1.Width = 150;
            // 
            // documentGroup1
            // 
            this.documentGroup1.Items.AddRange(new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document[] {
            this.document4,
            this.document5,
            this.document3,
            this.document2,
            this.document1});
            // 
            // document4
            // 
            this.document4.Caption = "Events";
            this.document4.ControlName = "dpEvents";
            this.document4.FloatLocation = new System.Drawing.Point(1334, 893);
            this.document4.FloatSize = new System.Drawing.Size(880, 509);
            this.document4.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.True;
            this.document4.Properties.AllowFloat = DevExpress.Utils.DefaultBoolean.True;
            this.document4.Properties.AllowFloatOnDoubleClick = DevExpress.Utils.DefaultBoolean.True;
            // 
            // document5
            // 
            this.document5.Caption = "Opened Positions";
            this.document5.ControlName = "dpPositions";
            this.document5.FloatLocation = new System.Drawing.Point(1443, 899);
            this.document5.FloatSize = new System.Drawing.Size(200, 200);
            this.document5.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.True;
            this.document5.Properties.AllowFloat = DevExpress.Utils.DefaultBoolean.True;
            this.document5.Properties.AllowFloatOnDoubleClick = DevExpress.Utils.DefaultBoolean.True;
            // 
            // document3
            // 
            this.document3.Caption = "Trade History";
            this.document3.ControlName = "dpTradeHistory";
            this.document3.FloatLocation = new System.Drawing.Point(1449, 887);
            this.document3.FloatSize = new System.Drawing.Size(200, 200);
            this.document3.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.True;
            this.document3.Properties.AllowFloat = DevExpress.Utils.DefaultBoolean.True;
            this.document3.Properties.AllowFloatOnDoubleClick = DevExpress.Utils.DefaultBoolean.True;
            // 
            // document2
            // 
            this.document2.Caption = "Chart";
            this.document2.ControlName = "dpChart";
            this.document2.FloatLocation = new System.Drawing.Point(1452, 909);
            this.document2.FloatSize = new System.Drawing.Size(200, 200);
            this.document2.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.True;
            this.document2.Properties.AllowFloat = DevExpress.Utils.DefaultBoolean.True;
            this.document2.Properties.AllowFloatOnDoubleClick = DevExpress.Utils.DefaultBoolean.True;
            // 
            // document1
            // 
            this.document1.Caption = "Data";
            this.document1.ControlName = "dpData";
            this.document1.FloatLocation = new System.Drawing.Point(1319, 874);
            this.document1.FloatSize = new System.Drawing.Size(200, 200);
            this.document1.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.True;
            this.document1.Properties.AllowFloat = DevExpress.Utils.DefaultBoolean.True;
            this.document1.Properties.AllowFloatOnDoubleClick = DevExpress.Utils.DefaultBoolean.True;
            // 
            // gcData
            // 
            this.gcData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcData.Location = new System.Drawing.Point(0, 0);
            this.gcData.MainView = this.gvData;
            this.gcData.MenuManager = this.barManager1;
            this.gcData.Name = "gcData";
            this.gcData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit3,
            this.repositoryItemCheckEdit5,
            this.repositoryItemCheckEdit6});
            this.gcData.Size = new System.Drawing.Size(2556, 1015);
            this.gcData.TabIndex = 9;
            this.gcData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvData});
            this.gcData.DoubleClick += new System.EventHandler(this.gcData_DoubleClick);
            this.gcData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gcData_MouseDown);
            // 
            // gvData
            // 
            this.gvData.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gvData.GridControl = this.gcData;
            this.gvData.Name = "gvData";
            this.gvData.OptionsBehavior.Editable = false;
            this.gvData.OptionsDetail.EnableMasterViewMode = false;
            this.gvData.OptionsScrollAnnotations.ShowCustomAnnotations = DevExpress.Utils.DefaultBoolean.True;
            this.gvData.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvData.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.gvData.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvData.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvData.CustomScrollAnnotation += new System.EventHandler<DevExpress.XtraGrid.Views.Grid.GridCustomScrollAnnotationsEventArgs>(this.gvData_CustomScrollAnnotation);
            this.gvData.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvData_RowStyle);
            this.gvData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvData_MouseDown);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.biCustomize,
            this.biReset,
            this.bsPanes,
            this.bsIndex,
            this.biShowChart,
            this.biShowTable,
            this.biBoth,
            this.sbTradeHistory,
            this.sbTickersData,
            this.sbTickersDataTable,
            this.sbTradeHistoryTable});
            this.barManager1.MaxItemId = 11;
            // 
            // bar1
            // 
            this.bar1.BarAppearance.Hovered.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.bar1.BarAppearance.Hovered.Options.UseFont = true;
            this.bar1.BarAppearance.Normal.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.bar1.BarAppearance.Normal.Options.UseFont = true;
            this.bar1.BarAppearance.Pressed.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.bar1.BarAppearance.Pressed.Options.UseFont = true;
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar1.FloatLocation = new System.Drawing.Point(722, 393);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.biCustomize),
            new DevExpress.XtraBars.LinkPersistInfo(this.biReset),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsPanes),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsIndex)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // biCustomize
            // 
            this.biCustomize.Caption = "Customize";
            this.biCustomize.Id = 0;
            this.biCustomize.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biCustomize.ImageOptions.SvgImage")));
            this.biCustomize.Name = "biCustomize";
            this.biCustomize.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biCustomize.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biCustomize_ItemClick);
            // 
            // biReset
            // 
            this.biReset.Caption = "Reset Defaults";
            this.biReset.Id = 1;
            this.biReset.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biReset.ImageOptions.SvgImage")));
            this.biReset.Name = "biReset";
            this.biReset.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biReset.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biReset_ItemClick);
            // 
            // bsPanes
            // 
            this.bsPanes.Caption = "Panes";
            this.bsPanes.Id = 2;
            this.bsPanes.Name = "bsPanes";
            this.bsPanes.GetItemData += new System.EventHandler(this.bsPanes_GetItemData);
            // 
            // bsIndex
            // 
            this.bsIndex.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.bsIndex.Caption = "DataItem Index";
            this.bsIndex.Id = 3;
            this.bsIndex.Name = "bsIndex";
            // 
            // bar2
            // 
            this.bar2.BarName = "Custom 3";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.sbTickersData),
            new DevExpress.XtraBars.LinkPersistInfo(this.sbTickersDataTable)});
            this.bar2.Text = "Custom 3";
            // 
            // sbTickersData
            // 
            this.sbTickersData.Caption = "Tickers Data Charts";
            this.sbTickersData.Id = 8;
            this.sbTickersData.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.sbTradeHistory)});
            this.sbTickersData.Name = "sbTickersData";
            // 
            // sbTradeHistory
            // 
            this.sbTradeHistory.Caption = "Trade History";
            this.sbTradeHistory.Id = 7;
            this.sbTradeHistory.Name = "sbTradeHistory";
            // 
            // sbTickersDataTable
            // 
            this.sbTickersDataTable.Caption = "Tickers Data Table";
            this.sbTickersDataTable.Id = 9;
            this.sbTickersDataTable.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.sbTradeHistoryTable)});
            this.sbTickersDataTable.Name = "sbTickersDataTable";
            // 
            // sbTradeHistoryTable
            // 
            this.sbTradeHistoryTable.Caption = "Trade History";
            this.sbTradeHistoryTable.Id = 10;
            this.sbTradeHistoryTable.Name = "sbTradeHistoryTable";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(2564, 52);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 1118);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(2564, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 52);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 1066);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(2564, 52);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 1066);
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.MenuManager = this.barManager1;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dpData,
            this.dpEvents,
            this.dpTradeHistory,
            this.dpChart,
            this.dpPositions});
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
            "DevExpress.XtraBars.TabFormControl",
            "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl",
            "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl"});
            // 
            // dpData
            // 
            this.dpData.Controls.Add(this.dockPanel1_Container);
            this.dpData.DockedAsTabbedDocument = true;
            this.dpData.FloatLocation = new System.Drawing.Point(1319, 874);
            this.dpData.ID = new System.Guid("21fa49d0-dcfa-465b-b0f7-eaadc5fe3b1d");
            this.dpData.Name = "dpData";
            this.dpData.OriginalSize = new System.Drawing.Size(200, 200);
            this.dpData.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dpData.SavedIndex = 0;
            this.dpData.Text = "Data";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.gcData);
            this.dockPanel1_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(2556, 1015);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // dpEvents
            // 
            this.dpEvents.Controls.Add(this.dockPanel2_Container);
            this.dpEvents.DockedAsTabbedDocument = true;
            this.dpEvents.FloatLocation = new System.Drawing.Point(1334, 893);
            this.dpEvents.FloatSize = new System.Drawing.Size(880, 509);
            this.dpEvents.ID = new System.Guid("783d5547-478d-49ff-aac9-efde01a48775");
            this.dpEvents.Name = "dpEvents";
            this.dpEvents.OriginalSize = new System.Drawing.Size(200, 200);
            this.dpEvents.SavedIndex = 1;
            this.dpEvents.SavedMdiDocument = true;
            this.dpEvents.Text = "Events";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.gcEvent);
            this.dockPanel2_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(2556, 1015);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // gcEvent
            // 
            this.gcEvent.DataSource = this.strategyHistoryItemBindingSource;
            this.gcEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcEvent.Location = new System.Drawing.Point(0, 0);
            this.gcEvent.MainView = this.gvEvent;
            this.gcEvent.MenuManager = this.barManager1;
            this.gcEvent.Name = "gcEvent";
            this.gcEvent.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.gcEvent.Size = new System.Drawing.Size(2556, 1015);
            this.gcEvent.TabIndex = 7;
            this.gcEvent.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvEvent});
            this.gcEvent.Click += new System.EventHandler(this.gridControl1_Click);
            // 
            // strategyHistoryItemBindingSource
            // 
            this.strategyHistoryItemBindingSource.DataSource = typeof(Crypto.Core.Strategies.StrategyHistoryItem);
            // 
            // gvEvent
            // 
            this.gvEvent.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gvEvent.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colType,
            this.colText,
            this.colTime,
            this.colOperation,
            this.colRate,
            this.colAmount,
            this.colBuyDeposit,
            this.colSellDeposit});
            this.gvEvent.GridControl = this.gcEvent;
            this.gvEvent.Name = "gvEvent";
            this.gvEvent.OptionsBehavior.Editable = false;
            this.gvEvent.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvEvent.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.gvEvent.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvEvent.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvEvent.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // colType
            // 
            this.colType.FieldName = "Type";
            this.colType.MinWidth = 40;
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 1;
            this.colType.Width = 150;
            // 
            // colText
            // 
            this.colText.FieldName = "Text";
            this.colText.MinWidth = 40;
            this.colText.Name = "colText";
            this.colText.Visible = true;
            this.colText.VisibleIndex = 2;
            this.colText.Width = 150;
            // 
            // colTime
            // 
            this.colTime.DisplayFormat.FormatString = "dd.MM.yyyy hh:mm";
            this.colTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTime.FieldName = "Time";
            this.colTime.MinWidth = 40;
            this.colTime.Name = "colTime";
            this.colTime.Visible = true;
            this.colTime.VisibleIndex = 0;
            this.colTime.Width = 150;
            // 
            // colOperation
            // 
            this.colOperation.FieldName = "Operation";
            this.colOperation.MinWidth = 40;
            this.colOperation.Name = "colOperation";
            this.colOperation.Visible = true;
            this.colOperation.VisibleIndex = 3;
            this.colOperation.Width = 150;
            // 
            // colRate
            // 
            this.colRate.DisplayFormat.FormatString = "0.########";
            this.colRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colRate.FieldName = "Rate";
            this.colRate.MinWidth = 40;
            this.colRate.Name = "colRate";
            this.colRate.Visible = true;
            this.colRate.VisibleIndex = 4;
            this.colRate.Width = 150;
            // 
            // colAmount
            // 
            this.colAmount.DisplayFormat.FormatString = "0.########";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "Amount";
            this.colAmount.MinWidth = 40;
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 5;
            this.colAmount.Width = 150;
            // 
            // colBuyDeposit
            // 
            this.colBuyDeposit.DisplayFormat.FormatString = "0.########";
            this.colBuyDeposit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBuyDeposit.FieldName = "BuyDeposit";
            this.colBuyDeposit.MinWidth = 40;
            this.colBuyDeposit.Name = "colBuyDeposit";
            this.colBuyDeposit.Visible = true;
            this.colBuyDeposit.VisibleIndex = 6;
            this.colBuyDeposit.Width = 150;
            // 
            // colSellDeposit
            // 
            this.colSellDeposit.DisplayFormat.FormatString = "0.########";
            this.colSellDeposit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSellDeposit.FieldName = "SellDeposit";
            this.colSellDeposit.MinWidth = 40;
            this.colSellDeposit.Name = "colSellDeposit";
            this.colSellDeposit.Visible = true;
            this.colSellDeposit.VisibleIndex = 7;
            this.colSellDeposit.Width = 150;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // dpTradeHistory
            // 
            this.dpTradeHistory.Controls.Add(this.dockPanel3_Container);
            this.dpTradeHistory.DockedAsTabbedDocument = true;
            this.dpTradeHistory.FloatLocation = new System.Drawing.Point(1449, 887);
            this.dpTradeHistory.ID = new System.Guid("120de2b3-3312-4030-8de4-6792d846e457");
            this.dpTradeHistory.Name = "dpTradeHistory";
            this.dpTradeHistory.OriginalSize = new System.Drawing.Size(200, 200);
            this.dpTradeHistory.SavedIndex = 2;
            this.dpTradeHistory.SavedMdiDocument = true;
            this.dpTradeHistory.Text = "Trade History";
            // 
            // dockPanel3_Container
            // 
            this.dockPanel3_Container.Controls.Add(this.gcTradeHistory);
            this.dockPanel3_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel3_Container.Name = "dockPanel3_Container";
            this.dockPanel3_Container.Size = new System.Drawing.Size(2556, 1015);
            this.dockPanel3_Container.TabIndex = 0;
            // 
            // gcTradeHistory
            // 
            this.gcTradeHistory.DataSource = this.tradingResultBindingSource;
            this.gcTradeHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTradeHistory.Location = new System.Drawing.Point(0, 0);
            this.gcTradeHistory.MainView = this.gvTradeHistory;
            this.gcTradeHistory.MenuManager = this.barManager1;
            this.gcTradeHistory.Name = "gcTradeHistory";
            this.gcTradeHistory.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit2,
            this.repositoryItemCheckEdit3,
            this.repositoryItemCheckEdit4});
            this.gcTradeHistory.Size = new System.Drawing.Size(2556, 1015);
            this.gcTradeHistory.TabIndex = 7;
            this.gcTradeHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTradeHistory});
            // 
            // tradingResultBindingSource
            // 
            this.tradingResultBindingSource.DataSource = typeof(TradingResult);
            // 
            // gvTradeHistory
            // 
            this.gvTradeHistory.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gvTradeHistory.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colOrderNumber,
            this.colDate,
            this.colAmount1,
            this.colTotal,
            this.colType1});
            this.gvTradeHistory.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Column = this.colType1;
            gridFormatRule1.Name = "Buy";
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.PredefinedName = "Green Fill";
            formatConditionRuleValue1.Value1 = OrderType.Buy;
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.ApplyToRow = true;
            gridFormatRule2.Column = this.colType1;
            gridFormatRule2.Name = "Sell";
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue2.PredefinedName = "Red Fill";
            formatConditionRuleValue2.Value1 = OrderType.Sell;
            gridFormatRule2.Rule = formatConditionRuleValue2;
            this.gvTradeHistory.FormatRules.Add(gridFormatRule1);
            this.gvTradeHistory.FormatRules.Add(gridFormatRule2);
            this.gvTradeHistory.GridControl = this.gcTradeHistory;
            this.gvTradeHistory.Name = "gvTradeHistory";
            this.gvTradeHistory.OptionsBehavior.Editable = false;
            this.gvTradeHistory.OptionsDetail.EnableMasterViewMode = false;
            this.gvTradeHistory.OptionsDetail.ShowEmbeddedDetailIndent = DevExpress.Utils.DefaultBoolean.False;
            this.gvTradeHistory.OptionsDetail.SmartDetailExpand = false;
            this.gvTradeHistory.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvTradeHistory.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvTradeHistory.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.gvTradeHistory.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvTradeHistory.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvTradeHistory.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // colOrderNumber
            // 
            this.colOrderNumber.FieldName = "OrderId";
            this.colOrderNumber.MinWidth = 40;
            this.colOrderNumber.Name = "colOrderNumber";
            this.colOrderNumber.Visible = true;
            this.colOrderNumber.VisibleIndex = 2;
            this.colOrderNumber.Width = 150;
            // 
            // colDate
            // 
            this.colDate.DisplayFormat.FormatString = "dd.MM.yyyy hh:mm";
            this.colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDate.FieldName = "Date";
            this.colDate.MinWidth = 40;
            this.colDate.Name = "colDate";
            this.colDate.Visible = true;
            this.colDate.VisibleIndex = 0;
            this.colDate.Width = 150;
            // 
            // colAmount1
            // 
            this.colAmount1.DisplayFormat.FormatString = "0.########";
            this.colAmount1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount1.FieldName = "Amount";
            this.colAmount1.MinWidth = 40;
            this.colAmount1.Name = "colAmount1";
            this.colAmount1.Visible = true;
            this.colAmount1.VisibleIndex = 3;
            this.colAmount1.Width = 150;
            // 
            // colTotal
            // 
            this.colTotal.DisplayFormat.FormatString = "0.########";
            this.colTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotal.FieldName = "Total";
            this.colTotal.MinWidth = 40;
            this.colTotal.Name = "colTotal";
            this.colTotal.Visible = true;
            this.colTotal.VisibleIndex = 4;
            this.colTotal.Width = 150;
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            this.repositoryItemTextEdit2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            // 
            // repositoryItemCheckEdit3
            // 
            this.repositoryItemCheckEdit3.AutoHeight = false;
            this.repositoryItemCheckEdit3.Name = "repositoryItemCheckEdit3";
            // 
            // repositoryItemCheckEdit4
            // 
            this.repositoryItemCheckEdit4.AutoHeight = false;
            this.repositoryItemCheckEdit4.Name = "repositoryItemCheckEdit4";
            // 
            // dpChart
            // 
            this.dpChart.Controls.Add(this.dockPanel4_Container);
            this.dpChart.DockedAsTabbedDocument = true;
            this.dpChart.FloatLocation = new System.Drawing.Point(1452, 909);
            this.dpChart.ID = new System.Guid("4120d400-8988-43ba-8082-1de410d4eec4");
            this.dpChart.Name = "dpChart";
            this.dpChart.OriginalSize = new System.Drawing.Size(428, 200);
            this.dpChart.SavedIndex = 3;
            this.dpChart.SavedMdiDocument = true;
            this.dpChart.Text = "Chart";
            // 
            // dockPanel4_Container
            // 
            this.dockPanel4_Container.Controls.Add(this.chartDataControl1);
            this.dockPanel4_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel4_Container.Name = "dockPanel4_Container";
            this.dockPanel4_Container.Size = new System.Drawing.Size(2556, 1015);
            this.dockPanel4_Container.TabIndex = 0;
            // 
            // chartDataControl1
            // 
            this.chartDataControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartDataControl1.Location = new System.Drawing.Point(0, 0);
            this.chartDataControl1.Margin = new System.Windows.Forms.Padding(6);
            this.chartDataControl1.Name = "chartDataControl1";
            this.chartDataControl1.Size = new System.Drawing.Size(2556, 1015);
            this.chartDataControl1.TabIndex = 0;
            // 
            // dpPositions
            // 
            this.dpPositions.Controls.Add(this.controlContainer1);
            this.dpPositions.DockedAsTabbedDocument = true;
            this.dpPositions.FloatLocation = new System.Drawing.Point(1443, 899);
            this.dpPositions.ID = new System.Guid("35c19321-1d12-4d6a-a955-ab8d5c7acb4d");
            this.dpPositions.Name = "dpPositions";
            this.dpPositions.OriginalSize = new System.Drawing.Size(200, 200);
            this.dpPositions.SavedIndex = 4;
            this.dpPositions.SavedMdiDocument = true;
            this.dpPositions.Text = "Opened Positions";
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.tcPosition);
            this.controlContainer1.Location = new System.Drawing.Point(0, 0);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(2556, 1015);
            this.controlContainer1.TabIndex = 0;
            // 
            // tcPosition
            // 
            this.tcPosition.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tcPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcPosition.Location = new System.Drawing.Point(0, 0);
            this.tcPosition.MenuManager = this.barManager1;
            this.tcPosition.Name = "tcPosition";
            this.tcPosition.OptionsBehavior.Editable = false;
            this.tcPosition.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.tcPosition.OptionsView.ShowFirstLines = false;
            this.tcPosition.OptionsView.ShowHorzLines = false;
            this.tcPosition.OptionsView.ShowVertLines = false;
            this.tcPosition.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.ShowOnlyInEditor;
            this.tcPosition.Size = new System.Drawing.Size(2556, 1015);
            this.tcPosition.TabIndex = 0;
            this.tcPosition.DoubleClick += new System.EventHandler(this.OnTreeListDoubleClick);
            // 
            // biShowChart
            // 
            this.biShowChart.Caption = "Show Chart";
            this.biShowChart.Id = 4;
            this.biShowChart.Name = "biShowChart";
            this.biShowChart.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biShowChart_ItemClick);
            // 
            // biShowTable
            // 
            this.biShowTable.Caption = "Show Table";
            this.biShowTable.Id = 5;
            this.biShowTable.Name = "biShowTable";
            this.biShowTable.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biShowTable_ItemClick);
            // 
            // biBoth
            // 
            this.biBoth.Caption = "Show Both";
            this.biBoth.Id = 6;
            this.biBoth.Name = "biBoth";
            this.biBoth.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biBoth_ItemClick);
            // 
            // repositoryItemTextEdit3
            // 
            this.repositoryItemTextEdit3.AutoHeight = false;
            this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
            this.repositoryItemTextEdit3.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            // 
            // repositoryItemCheckEdit5
            // 
            this.repositoryItemCheckEdit5.AutoHeight = false;
            this.repositoryItemCheckEdit5.Name = "repositoryItemCheckEdit5";
            // 
            // repositoryItemCheckEdit6
            // 
            this.repositoryItemCheckEdit6.AutoHeight = false;
            this.repositoryItemCheckEdit6.Name = "repositoryItemCheckEdit6";
            // 
            // documentManager1
            // 
            this.documentManager1.ContainerControl = this;
            this.documentManager1.View = this.tabbedView1;
            this.documentManager1.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.tabbedView1});
            // 
            // tabbedView1
            // 
            this.tabbedView1.DocumentGroups.AddRange(new DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup[] {
            this.documentGroup1});
            this.tabbedView1.Documents.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseDocument[] {
            this.document4,
            this.document5,
            this.document3,
            this.document2,
            this.document1});
            dockingContainer1.Element = this.documentGroup1;
            this.tabbedView1.RootContainer.Nodes.AddRange(new DevExpress.XtraBars.Docking2010.Views.Tabbed.DockingContainer[] {
            dockingContainer1});
            // 
            // pmShowData
            // 
            this.pmShowData.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.biShowChart),
            new DevExpress.XtraBars.LinkPersistInfo(this.biShowTable),
            new DevExpress.XtraBars.LinkPersistInfo(this.biBoth)});
            this.pmShowData.Manager = this.barManager1;
            this.pmShowData.Name = "pmShowData";
            // 
            // StrategyDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2564, 1118);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "StrategyDataForm";
            this.Text = "StrategyDataForm";
            ((System.ComponentModel.ISupportInitialize)(this.documentGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.document4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.document5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.document3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.document2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.document1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dpData.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.dpEvents.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcEvent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.strategyHistoryItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEvent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            this.dpTradeHistory.ResumeLayout(false);
            this.dockPanel3_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcTradeHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tradingResultBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTradeHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).EndInit();
            this.dpChart.ResumeLayout(false);
            this.dockPanel4_Container.ResumeLayout(false);
            this.dpPositions.ResumeLayout(false);
            this.controlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tcPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmShowData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraGrid.GridControl gcEvent;
        private System.Windows.Forms.BindingSource strategyHistoryItemBindingSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gvEvent;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colText;
        private DevExpress.XtraGrid.Columns.GridColumn colTime;
        private DevExpress.XtraGrid.Columns.GridColumn colOperation;
        private DevExpress.XtraGrid.Columns.GridColumn colRate;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.GridControl gcTradeHistory;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTradeHistory;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit4;
        private System.Windows.Forms.BindingSource tradingResultBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colDate;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount1;
        private DevExpress.XtraGrid.Columns.GridColumn colTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colType1;
        private DevExpress.XtraGrid.Columns.GridColumn colBuyDeposit;
        private DevExpress.XtraGrid.Columns.GridColumn colSellDeposit;
        private DevExpress.XtraGrid.Views.Grid.GridView gvData;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit5;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit6;
        public DevExpress.XtraGrid.GridControl gcData;
        private BarManager barManager1;
        private Bar bar1;
        private BarDockControl barDockControlTop;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarButtonItem biCustomize;
        private BarButtonItem biReset;
        private BarSubItem bsPanes;
        private BarStaticItem bsIndex;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dpChart;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel4_Container;
        private DevExpress.XtraBars.Docking.DockPanel dpTradeHistory;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel3_Container;
        private DevExpress.XtraBars.Docking.DockPanel dpEvents;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraBars.Docking.DockPanel dpData;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.Docking2010.DocumentManager documentManager1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.Document document2;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.Document document3;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.Document document4;
        private DevExpress.XtraBars.Docking.DockPanel dpPositions;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private Crypto.UI.Forms.ChartDataControl chartDataControl1;
        private DevExpress.XtraTreeList.TreeList tcPosition;
        private PopupMenu pmShowData;
        private BarButtonItem biShowChart;
        private BarButtonItem biShowTable;
        private BarButtonItem biBoth;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.Document document5;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup documentGroup1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.Document document1;
        private Bar bar2;
        private BarSubItem sbTradeHistory;
        private BarSubItem sbTickersData;
        private BarSubItem sbTickersDataTable;
        private BarSubItem sbTradeHistoryTable;
    }
}