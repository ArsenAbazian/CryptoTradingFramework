using CryptoMarketClient.Common;

namespace CryptoMarketClient {
    partial class DependencyArbitrageForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DependencyArbitrageForm));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule3 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue3 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule4 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue4 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule5 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue5 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue2 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaxDeviation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colIsConnected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.biAdd = new DevExpress.XtraBars.BarButtonItem();
            this.biRemove = new DevExpress.XtraBars.BarButtonItem();
            this.biEdit = new DevExpress.XtraBars.BarButtonItem();
            this.sbShowChart = new DevExpress.XtraBars.BarSubItem();
            this.biShowChart = new DevExpress.XtraBars.BarButtonItem();
            this.biAllHistoryChart = new DevExpress.XtraBars.BarButtonItem();
            this.barCheckItem1 = new DevExpress.XtraBars.BarCheckItem();
            this.biSettings = new DevExpress.XtraBars.BarButtonItem();
            this.bbSaveHistory = new DevExpress.XtraBars.BarButtonItem();
            this.biStart = new DevExpress.XtraBars.BarButtonItem();
            this.biReconnect = new DevExpress.XtraBars.BarButtonItem();
            this.biLoadHistory = new DevExpress.XtraBars.BarButtonItem();
            this.bbExportHistory = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dpLog = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.gcLog = new DevExpress.XtraGrid.GridControl();
            this.logMessageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvLog = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExchange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTicker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.dependencyArbitrageInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSecond = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSecondName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThresold = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsSelectedInDependencyArbitrageForm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colIndex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaxDeviationTicker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaxDeviationExchange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHighestBid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLowestAsk = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradingPair = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaxDeviationTickerInfo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLongestDelay = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dpLog.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logMessageBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dependencyArbitrageInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // colType
            // 
            this.colType.FieldName = "Type";
            this.colType.MinWidth = 40;
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 0;
            this.colType.Width = 66;
            // 
            // colMaxDeviation
            // 
            this.colMaxDeviation.ColumnEdit = this.repositoryItemTextEdit1;
            this.colMaxDeviation.DisplayFormat.FormatString = "0.00000000";
            this.colMaxDeviation.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMaxDeviation.FieldName = "MaxDeviation";
            this.colMaxDeviation.MinWidth = 40;
            this.colMaxDeviation.Name = "colMaxDeviation";
            this.colMaxDeviation.OptionsColumn.AllowEdit = false;
            this.colMaxDeviation.Visible = true;
            this.colMaxDeviation.VisibleIndex = 5;
            this.colMaxDeviation.Width = 296;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            // 
            // colIsConnected
            // 
            this.colIsConnected.FieldName = "IsConnected";
            this.colIsConnected.MinWidth = 40;
            this.colIsConnected.Name = "colIsConnected";
            this.colIsConnected.OptionsColumn.AllowEdit = false;
            this.colIsConnected.Width = 150;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar3});
            this.barManager1.Controller = this.barAndDockingController1;
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.biAdd,
            this.biRemove,
            this.biEdit,
            this.biShowChart,
            this.barStaticItem1,
            this.biSettings,
            this.biReconnect,
            this.bbSaveHistory,
            this.biStart,
            this.biLoadHistory,
            this.bbExportHistory,
            this.sbShowChart,
            this.biAllHistoryChart,
            this.barCheckItem1});
            this.barManager1.MaxItemId = 16;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.biAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.biRemove),
            new DevExpress.XtraBars.LinkPersistInfo(this.biEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.sbShowChart),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.biSettings, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbSaveHistory, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.biStart, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.biReconnect),
            new DevExpress.XtraBars.LinkPersistInfo(this.biLoadHistory),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbExportHistory, true)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // biAdd
            // 
            this.biAdd.Caption = "Add New";
            this.biAdd.Id = 0;
            this.biAdd.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biAdd.ImageOptions.SvgImage")));
            this.biAdd.Name = "biAdd";
            this.biAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biAdd_ItemClick);
            // 
            // biRemove
            // 
            this.biRemove.Caption = "Remove";
            this.biRemove.Id = 1;
            this.biRemove.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biRemove.ImageOptions.SvgImage")));
            this.biRemove.Name = "biRemove";
            this.biRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biRemove_ItemClick);
            // 
            // biEdit
            // 
            this.biEdit.Caption = "Edit";
            this.biEdit.Id = 2;
            this.biEdit.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biEdit.ImageOptions.SvgImage")));
            this.biEdit.Name = "biEdit";
            this.biEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biEdit_ItemClick);
            // 
            // sbShowChart
            // 
            this.sbShowChart.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.sbShowChart.Caption = "Show Charts";
            this.sbShowChart.Id = 13;
            this.sbShowChart.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("sbShowChart.ImageOptions.SvgImage")));
            this.sbShowChart.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.biShowChart),
            new DevExpress.XtraBars.LinkPersistInfo(this.biAllHistoryChart)});
            this.sbShowChart.Name = "sbShowChart";
            this.sbShowChart.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu;
            // 
            // biShowChart
            // 
            this.biShowChart.Caption = "Current";
            this.biShowChart.Id = 3;
            this.biShowChart.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biShowChart.ImageOptions.SvgImage")));
            this.biShowChart.Name = "biShowChart";
            this.biShowChart.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biShowChart.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biShowChart_ItemClick);
            // 
            // biAllHistoryChart
            // 
            this.biAllHistoryChart.Caption = "All History";
            this.biAllHistoryChart.Id = 14;
            this.biAllHistoryChart.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biAllHistoryChart.ImageOptions.SvgImage")));
            this.biAllHistoryChart.Name = "biAllHistoryChart";
            this.biAllHistoryChart.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biAllHistoryChart_ItemClick);
            // 
            // barCheckItem1
            // 
            this.barCheckItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barCheckItem1.Caption = "barCheckItem1";
            this.barCheckItem1.Id = 15;
            this.barCheckItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barCheckItem1.ImageOptions.SvgImage")));
            this.barCheckItem1.Name = "barCheckItem1";
            this.barCheckItem1.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItem1_CheckedChanged);
            // 
            // biSettings
            // 
            this.biSettings.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.biSettings.Caption = "Settings";
            this.biSettings.Id = 6;
            this.biSettings.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biSettings.ImageOptions.SvgImage")));
            this.biSettings.Name = "biSettings";
            // 
            // bbSaveHistory
            // 
            this.bbSaveHistory.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.bbSaveHistory.Caption = "Save History";
            this.bbSaveHistory.Id = 8;
            this.bbSaveHistory.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbSaveHistory.ImageOptions.SvgImage")));
            this.bbSaveHistory.Name = "bbSaveHistory";
            this.bbSaveHistory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbSaveHistory_ItemClick);
            // 
            // biStart
            // 
            this.biStart.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.biStart.Caption = "<b>Start!</b>";
            this.biStart.Id = 9;
            this.biStart.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biStart.ImageOptions.SvgImage")));
            this.biStart.Name = "biStart";
            this.biStart.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biStart.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biStart_ItemClick);
            // 
            // biReconnect
            // 
            this.biReconnect.Caption = "Reconnect";
            this.biReconnect.Id = 7;
            this.biReconnect.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biReconnect.ImageOptions.SvgImage")));
            this.biReconnect.Name = "biReconnect";
            this.biReconnect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biReconnect_ItemClick);
            // 
            // biLoadHistory
            // 
            this.biLoadHistory.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.biLoadHistory.Caption = "Load History";
            this.biLoadHistory.Id = 10;
            this.biLoadHistory.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biLoadHistory.ImageOptions.SvgImage")));
            this.biLoadHistory.Name = "biLoadHistory";
            this.biLoadHistory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biLoadHistory_ItemClick);
            // 
            // bbExportHistory
            // 
            this.bbExportHistory.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.bbExportHistory.Caption = "Export History";
            this.bbExportHistory.Id = 11;
            this.bbExportHistory.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbExportHistory.ImageOptions.SvgImage")));
            this.bbExportHistory.Name = "bbExportHistory";
            this.bbExportHistory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbExportHistory_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barStaticItem1.Caption = "Status";
            this.barStaticItem1.Id = 4;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // barAndDockingController1
            // 
            this.barAndDockingController1.AppearancesBar.BarAppearance.Disabled.FontSizeDelta = 2;
            this.barAndDockingController1.AppearancesBar.BarAppearance.Disabled.Options.UseFont = true;
            this.barAndDockingController1.AppearancesBar.BarAppearance.Hovered.FontSizeDelta = 2;
            this.barAndDockingController1.AppearancesBar.BarAppearance.Hovered.Options.UseFont = true;
            this.barAndDockingController1.AppearancesBar.BarAppearance.Normal.FontSizeDelta = 2;
            this.barAndDockingController1.AppearancesBar.BarAppearance.Normal.Options.UseFont = true;
            this.barAndDockingController1.AppearancesBar.BarAppearance.Pressed.FontSizeDelta = 2;
            this.barAndDockingController1.AppearancesBar.BarAppearance.Pressed.Options.UseFont = true;
            this.barAndDockingController1.PropertiesBar.DefaultGlyphSize = new System.Drawing.Size(22, 22);
            this.barAndDockingController1.PropertiesBar.DefaultLargeGlyphSize = new System.Drawing.Size(44, 44);
            this.barAndDockingController1.PropertiesDocking.ViewStyle = DevExpress.XtraBars.Docking2010.Views.DockingViewStyle.Default;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.barDockControlTop.Size = new System.Drawing.Size(1924, 72);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 1011);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.barDockControlBottom.Size = new System.Drawing.Size(1924, 51);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 72);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 939);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1924, 72);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 939);
            // 
            // dockManager1
            // 
            this.dockManager1.Controller = this.barAndDockingController1;
            this.dockManager1.Form = this;
            this.dockManager1.MenuManager = this.barManager1;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dpLog});
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
            "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl"});
            // 
            // dpLog
            // 
            this.dpLog.Controls.Add(this.dockPanel1_Container);
            this.dpLog.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dpLog.ID = new System.Guid("8868ab0f-f6f0-417d-bddb-c3c1b3d42d06");
            this.dpLog.Location = new System.Drawing.Point(0, 301);
            this.dpLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dpLog.Name = "dpLog";
            this.dpLog.OriginalSize = new System.Drawing.Size(200, 369);
            this.dpLog.SavedSizeFactor = 0D;
            this.dpLog.Size = new System.Drawing.Size(1924, 710);
            this.dpLog.Text = "Log";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.gcLog);
            this.dockPanel1_Container.Location = new System.Drawing.Point(8, 51);
            this.dockPanel1_Container.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(1908, 651);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // gcLog
            // 
            this.gcLog.DataSource = this.logMessageBindingSource;
            this.gcLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcLog.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gcLog.Location = new System.Drawing.Point(0, 0);
            this.gcLog.MainView = this.gvLog;
            this.gcLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gcLog.MenuManager = this.barManager1;
            this.gcLog.Name = "gcLog";
            this.gcLog.Size = new System.Drawing.Size(1908, 651);
            this.gcLog.TabIndex = 0;
            this.gcLog.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLog});
            // 
            // logMessageBindingSource
            // 
            this.logMessageBindingSource.DataSource = typeof(CryptoMarketClient.Common.LogMessage);
            // 
            // gvLog
            // 
            this.gvLog.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colType,
            this.colTime,
            this.colText,
            this.colDescription,
            this.colExchange,
            this.colTicker});
            gridFormatRule3.ApplyToRow = true;
            gridFormatRule3.Column = this.colType;
            gridFormatRule3.Name = "FormatRuleError";
            formatConditionRuleValue3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            formatConditionRuleValue3.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue3.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue3.Value1 = CryptoMarketClient.Common.LogType.Error;
            gridFormatRule3.Rule = formatConditionRuleValue3;
            gridFormatRule4.ApplyToRow = true;
            gridFormatRule4.Column = this.colType;
            gridFormatRule4.Name = "FormatRuleWarning";
            formatConditionRuleValue4.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            formatConditionRuleValue4.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue4.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue4.Value1 = CryptoMarketClient.Common.LogType.Warning;
            gridFormatRule4.Rule = formatConditionRuleValue4;
            gridFormatRule5.ApplyToRow = true;
            gridFormatRule5.Name = "FormatRuleSuccess";
            formatConditionRuleValue5.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            formatConditionRuleValue5.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue5.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue5.Value1 = CryptoMarketClient.Common.LogType.Success;
            gridFormatRule5.Rule = formatConditionRuleValue5;
            this.gvLog.FormatRules.Add(gridFormatRule3);
            this.gvLog.FormatRules.Add(gridFormatRule4);
            this.gvLog.FormatRules.Add(gridFormatRule5);
            this.gvLog.GridControl = this.gcLog;
            this.gvLog.LevelIndent = 0;
            this.gvLog.Name = "gvLog";
            this.gvLog.OptionsBehavior.Editable = false;
            this.gvLog.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvLog.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvLog.PreviewIndent = 0;
            // 
            // colTime
            // 
            this.colTime.DisplayFormat.FormatString = "dd.MM.yyyy hh:mm:ss.fff";
            this.colTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTime.FieldName = "Time";
            this.colTime.MinWidth = 40;
            this.colTime.Name = "colTime";
            this.colTime.Visible = true;
            this.colTime.VisibleIndex = 1;
            this.colTime.Width = 258;
            // 
            // colText
            // 
            this.colText.FieldName = "Text";
            this.colText.MinWidth = 40;
            this.colText.Name = "colText";
            this.colText.Visible = true;
            this.colText.VisibleIndex = 4;
            this.colText.Width = 790;
            // 
            // colDescription
            // 
            this.colDescription.FieldName = "Description";
            this.colDescription.MinWidth = 40;
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 5;
            this.colDescription.Width = 508;
            // 
            // colExchange
            // 
            this.colExchange.FieldName = "Exchange";
            this.colExchange.MinWidth = 40;
            this.colExchange.Name = "colExchange";
            this.colExchange.Visible = true;
            this.colExchange.VisibleIndex = 2;
            this.colExchange.Width = 124;
            // 
            // colTicker
            // 
            this.colTicker.FieldName = "Ticker";
            this.colTicker.MinWidth = 40;
            this.colTicker.Name = "colTicker";
            this.colTicker.Visible = true;
            this.colTicker.VisibleIndex = 3;
            this.colTicker.Width = 124;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.dependencyArbitrageInfoBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridControl1.Location = new System.Drawing.Point(0, 72);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemCheckEdit1});
            this.gridControl1.Size = new System.Drawing.Size(1924, 229);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // dependencyArbitrageInfoBindingSource
            // 
            this.dependencyArbitrageInfoBindingSource.DataSource = typeof(Crypto.Core.Common.Arbitrages.StatisticalArbitrageStrategy);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.GroupPanel.FontSizeDelta = 2;
            this.gridView1.Appearance.GroupPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.FontSizeDelta = 2;
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.FontSizeDelta = 2;
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSecond,
            this.colSecondName,
            this.colThresold,
            this.colState,
            this.colIsSelectedInDependencyArbitrageForm,
            this.colIndex,
            this.colMaxDeviation,
            this.colMaxDeviationTicker,
            this.colMaxDeviationExchange,
            this.colIsConnected,
            this.colHighestBid,
            this.colLowestAsk,
            this.colTradingPair,
            this.colMaxDeviationTickerInfo,
            this.colLongestDelay});
            this.gridView1.CustomizationFormBounds = new System.Drawing.Rectangle(1400, 356, 520, 504);
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridFormatRule1.Column = this.colMaxDeviation;
            gridFormatRule1.ColumnApplyTo = this.colMaxDeviation;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Greater;
            formatConditionRuleValue1.Expression = "[MaxDeviation] > [Thresold]";
            formatConditionRuleValue1.PredefinedName = "Green Text";
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.ApplyToRow = true;
            gridFormatRule2.Column = this.colIsConnected;
            gridFormatRule2.Name = "Format1";
            formatConditionRuleValue2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            formatConditionRuleValue2.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue2.Value1 = false;
            gridFormatRule2.Rule = formatConditionRuleValue2;
            this.gridView1.FormatRules.Add(gridFormatRule1);
            this.gridView1.FormatRules.Add(gridFormatRule2);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.LevelIndent = 0;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 60;
            this.gridView1.OptionsSelection.CheckBoxSelectorField = "IsSelectedInDependencyArbitrageForm";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.PreviewIndent = 0;
            // 
            // colSecond
            // 
            this.colSecond.FieldName = "Second";
            this.colSecond.MinWidth = 40;
            this.colSecond.Name = "colSecond";
            this.colSecond.OptionsColumn.AllowEdit = false;
            this.colSecond.Width = 150;
            // 
            // colSecondName
            // 
            this.colSecondName.FieldName = "SecondName";
            this.colSecondName.MinWidth = 40;
            this.colSecondName.Name = "colSecondName";
            this.colSecondName.Width = 150;
            // 
            // colThresold
            // 
            this.colThresold.FieldName = "Thresold";
            this.colThresold.MinWidth = 40;
            this.colThresold.Name = "colThresold";
            this.colThresold.OptionsColumn.AllowEdit = false;
            this.colThresold.Visible = true;
            this.colThresold.VisibleIndex = 7;
            this.colThresold.Width = 298;
            // 
            // colState
            // 
            this.colState.FieldName = "State";
            this.colState.MinWidth = 40;
            this.colState.Name = "colState";
            this.colState.OptionsColumn.AllowEdit = false;
            this.colState.Visible = true;
            this.colState.VisibleIndex = 2;
            this.colState.Width = 222;
            // 
            // colIsSelectedInDependencyArbitrageForm
            // 
            this.colIsSelectedInDependencyArbitrageForm.Caption = " ";
            this.colIsSelectedInDependencyArbitrageForm.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsSelectedInDependencyArbitrageForm.FieldName = "IsSelectedInDependencyArbitrageForm";
            this.colIsSelectedInDependencyArbitrageForm.MinWidth = 40;
            this.colIsSelectedInDependencyArbitrageForm.Name = "colIsSelectedInDependencyArbitrageForm";
            this.colIsSelectedInDependencyArbitrageForm.Visible = true;
            this.colIsSelectedInDependencyArbitrageForm.VisibleIndex = 0;
            this.colIsSelectedInDependencyArbitrageForm.Width = 112;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.CheckedChanged += new System.EventHandler(this.repositoryItemCheckEdit1_CheckedChanged);
            // 
            // colIndex
            // 
            this.colIndex.FieldName = "Index";
            this.colIndex.MinWidth = 40;
            this.colIndex.Name = "colIndex";
            this.colIndex.Width = 150;
            // 
            // colMaxDeviationTicker
            // 
            this.colMaxDeviationTicker.FieldName = "MaxDeviationTicker";
            this.colMaxDeviationTicker.MinWidth = 40;
            this.colMaxDeviationTicker.Name = "colMaxDeviationTicker";
            this.colMaxDeviationTicker.Width = 150;
            // 
            // colMaxDeviationExchange
            // 
            this.colMaxDeviationExchange.FieldName = "MaxDeviationExchange";
            this.colMaxDeviationExchange.MinWidth = 40;
            this.colMaxDeviationExchange.Name = "colMaxDeviationExchange";
            this.colMaxDeviationExchange.OptionsColumn.AllowEdit = false;
            this.colMaxDeviationExchange.Width = 150;
            // 
            // colHighestBid
            // 
            this.colHighestBid.FieldName = "UpperBid";
            this.colHighestBid.MinWidth = 40;
            this.colHighestBid.Name = "colHighestBid";
            this.colHighestBid.OptionsColumn.AllowEdit = false;
            this.colHighestBid.Visible = true;
            this.colHighestBid.VisibleIndex = 3;
            this.colHighestBid.Width = 222;
            // 
            // colLowestAsk
            // 
            this.colLowestAsk.FieldName = "LowerAsk";
            this.colLowestAsk.MinWidth = 40;
            this.colLowestAsk.Name = "colLowestAsk";
            this.colLowestAsk.OptionsColumn.AllowEdit = false;
            this.colLowestAsk.Visible = true;
            this.colLowestAsk.VisibleIndex = 4;
            this.colLowestAsk.Width = 222;
            // 
            // colTradingPair
            // 
            this.colTradingPair.FieldName = "TradingPair";
            this.colTradingPair.MinWidth = 40;
            this.colTradingPair.Name = "colTradingPair";
            this.colTradingPair.OptionsColumn.AllowEdit = false;
            this.colTradingPair.Visible = true;
            this.colTradingPair.VisibleIndex = 1;
            this.colTradingPair.Width = 222;
            // 
            // colMaxDeviationTickerInfo
            // 
            this.colMaxDeviationTickerInfo.Caption = "Max Ticker Info";
            this.colMaxDeviationTickerInfo.FieldName = "MaxDeviationTickerInfo";
            this.colMaxDeviationTickerInfo.MinWidth = 40;
            this.colMaxDeviationTickerInfo.Name = "colMaxDeviationTickerInfo";
            this.colMaxDeviationTickerInfo.OptionsColumn.AllowEdit = false;
            this.colMaxDeviationTickerInfo.Visible = true;
            this.colMaxDeviationTickerInfo.VisibleIndex = 6;
            this.colMaxDeviationTickerInfo.Width = 292;
            // 
            // colLongestDelay
            // 
            this.colLongestDelay.FieldName = "LongestDelay";
            this.colLongestDelay.MinWidth = 40;
            this.colLongestDelay.Name = "colLongestDelay";
            this.colLongestDelay.Visible = true;
            this.colLongestDelay.VisibleIndex = 8;
            this.colLongestDelay.Width = 74;
            // 
            // DependencyArbitrageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1062);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.dpLog);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "DependencyArbitrageForm";
            this.Text = "Dependency Arbitrage";
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dpLog.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logMessageBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dependencyArbitrageInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem biAdd;
        private DevExpress.XtraBars.BarButtonItem biRemove;
        private DevExpress.XtraBars.BarButtonItem biEdit;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
        private DevExpress.XtraBars.BarButtonItem biShowChart;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarButtonItem biSettings;
        private System.Windows.Forms.BindingSource dependencyArbitrageInfoBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colSecond;
        private DevExpress.XtraGrid.Columns.GridColumn colSecondName;
        private DevExpress.XtraGrid.Columns.GridColumn colThresold;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colIsSelectedInDependencyArbitrageForm;
        private DevExpress.XtraGrid.Columns.GridColumn colIndex;
        private DevExpress.XtraGrid.Columns.GridColumn colMaxDeviation;
        private DevExpress.XtraGrid.Columns.GridColumn colMaxDeviationTicker;
        private DevExpress.XtraGrid.Columns.GridColumn colMaxDeviationExchange;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colIsConnected;
        private DevExpress.XtraBars.BarButtonItem biReconnect;
        private DevExpress.XtraBars.BarButtonItem bbSaveHistory;
        private DevExpress.XtraBars.BarButtonItem biStart;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colHighestBid;
        private DevExpress.XtraGrid.Columns.GridColumn colLowestAsk;
        private DevExpress.XtraGrid.Columns.GridColumn colTradingPair;
        private DevExpress.XtraGrid.Columns.GridColumn colMaxDeviationTickerInfo;
        private DevExpress.XtraBars.BarButtonItem biLoadHistory;
        private DevExpress.XtraBars.BarButtonItem bbExportHistory;
        private DevExpress.XtraBars.BarSubItem sbShowChart;
        private DevExpress.XtraBars.BarButtonItem biAllHistoryChart;
        private DevExpress.XtraBars.BarCheckItem barCheckItem1;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dpLog;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraGrid.GridControl gcLog;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLog;
        private System.Windows.Forms.BindingSource logMessageBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colTime;
        private DevExpress.XtraGrid.Columns.GridColumn colText;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colExchange;
        private DevExpress.XtraGrid.Columns.GridColumn colTicker;
        private DevExpress.XtraGrid.Columns.GridColumn colLongestDelay;
    }
}