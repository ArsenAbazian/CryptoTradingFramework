using Crypto.Core.Strategies;
using DevExpress.XtraBars;

namespace CryptoMarketClient.Strategies {
    partial class StrategiesCollectionForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StrategiesCollectionForm));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.siAdd = new DevExpress.XtraBars.BarSubItem();
            this.biCopy = new DevExpress.XtraBars.BarButtonItem();
            this.biRemove = new DevExpress.XtraBars.BarButtonItem();
            this.biEdit = new DevExpress.XtraBars.BarButtonItem();
            this.biStrategyHelp = new DevExpress.XtraBars.BarButtonItem();
            this.biStart = new DevExpress.XtraBars.BarButtonItem();
            this.biStop = new DevExpress.XtraBars.BarButtonItem();
            this.biSimulation = new DevExpress.XtraBars.BarButtonItem();
            this.biOptimizeParams = new DevExpress.XtraBars.BarButtonItem();
            this.btShowData = new DevExpress.XtraBars.BarButtonItem();
            this.bcShowLog = new DevExpress.XtraBars.BarCheckItem();
            this.biSettings = new DevExpress.XtraBars.BarButtonItem();
            this.biCollectionFormHelp = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.siStatus = new DevExpress.XtraBars.BarStaticItem();
            this.beSimulationProgress = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.biCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dpLogPanel = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.logMessagesControl = new CryptoMarketClient.LogMessagesControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.strategyBaseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colEnabled = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colDemoMode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStateText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riTextEditState = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colEarned = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEnableNotifications = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dpLogPanel.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.strategyBaseBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riTextEditState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.AllowHtmlText = true;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.siAdd,
            this.biRemove,
            this.biEdit,
            this.siStatus,
            this.biStart,
            this.biStop,
            this.btShowData,
            this.biSimulation,
            this.bcShowLog,
            this.biOptimizeParams,
            this.beSimulationProgress,
            this.biSettings,
            this.biCollectionFormHelp,
            this.biStrategyHelp,
            this.biCopy,
            this.biCancel});
            this.barManager1.MaxItemId = 19;
            this.barManager1.OptionsStubGlyphs.AllowStubGlyphs = DevExpress.Utils.DefaultBoolean.True;
            this.barManager1.OptionsStubGlyphs.CaseMode = DevExpress.Utils.Drawing.GlyphTextCaseMode.UpperCase;
            this.barManager1.OptionsStubGlyphs.CornerRadius = 3;
            this.barManager1.OptionsStubGlyphs.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.barManager1.OptionsStubGlyphs.LetterCount = DevExpress.Utils.Drawing.GlyphTextSymbolCount.Two;
            this.barManager1.OptionsStubGlyphs.UseFont = true;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar1});
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
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.siAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.biCopy),
            new DevExpress.XtraBars.LinkPersistInfo(this.biRemove),
            new DevExpress.XtraBars.LinkPersistInfo(this.biEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.biStrategyHelp),
            new DevExpress.XtraBars.LinkPersistInfo(this.biStart, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.biStop),
            new DevExpress.XtraBars.LinkPersistInfo(this.biSimulation),
            new DevExpress.XtraBars.LinkPersistInfo(this.biOptimizeParams),
            new DevExpress.XtraBars.LinkPersistInfo(this.btShowData, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bcShowLog),
            new DevExpress.XtraBars.LinkPersistInfo(this.biSettings),
            new DevExpress.XtraBars.LinkPersistInfo(this.biCollectionFormHelp)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // siAdd
            // 
            this.siAdd.Caption = "New";
            this.siAdd.Id = 0;
            this.siAdd.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("siAdd.ImageOptions.SvgImage")));
            this.siAdd.Name = "siAdd";
            this.siAdd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // biCopy
            // 
            this.biCopy.Caption = "Clone Selected";
            this.biCopy.Id = 17;
            this.biCopy.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biCopy.ImageOptions.SvgImage")));
            this.biCopy.Name = "biCopy";
            this.biCopy.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biCopy_ItemClick);
            // 
            // biRemove
            // 
            this.biRemove.Caption = "Remove Selected";
            this.biRemove.Id = 1;
            this.biRemove.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biRemove.ImageOptions.SvgImage")));
            this.biRemove.Name = "biRemove";
            this.biRemove.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biRemove_ItemClick);
            // 
            // biEdit
            // 
            this.biEdit.Caption = "Edit";
            this.biEdit.Id = 2;
            this.biEdit.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biEdit.ImageOptions.SvgImage")));
            this.biEdit.Name = "biEdit";
            this.biEdit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            toolTipItem1.Text = "Edit Strategy. Also can be invoked by doubleclick on row.";
            superToolTip1.Items.Add(toolTipItem1);
            this.biEdit.SuperTip = superToolTip1;
            this.biEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biEdit_ItemClick);
            // 
            // biStrategyHelp
            // 
            this.biStrategyHelp.Caption = "Strategy Help Topic";
            this.biStrategyHelp.Id = 16;
            this.biStrategyHelp.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biStrategyHelp.ImageOptions.SvgImage")));
            this.biStrategyHelp.Name = "biStrategyHelp";
            this.biStrategyHelp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // biStart
            // 
            this.biStart.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.biStart.Caption = "<b>Run!</b>";
            this.biStart.Id = 4;
            this.biStart.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biStart.ImageOptions.SvgImage")));
            this.biStart.ItemAppearance.Normal.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Information;
            this.biStart.ItemAppearance.Normal.Options.UseForeColor = true;
            this.biStart.Name = "biStart";
            this.biStart.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biStart.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biStart_ItemClick);
            // 
            // biStop
            // 
            this.biStop.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.biStop.Caption = "<b>Stop</b>";
            this.biStop.Id = 5;
            this.biStop.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biStop.ImageOptions.SvgImage")));
            this.biStop.ItemAppearance.Normal.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            this.biStop.ItemAppearance.Normal.Options.UseForeColor = true;
            this.biStop.Name = "biStop";
            this.biStop.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biStop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biStop_ItemClick);
            // 
            // biSimulation
            // 
            this.biSimulation.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.biSimulation.Caption = "<b>Simulation</b>";
            this.biSimulation.Id = 8;
            this.biSimulation.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biSimulation.ImageOptions.SvgImage")));
            this.biSimulation.ItemAppearance.Hovered.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Question;
            this.biSimulation.ItemAppearance.Hovered.Options.UseForeColor = true;
            this.biSimulation.ItemAppearance.Normal.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Question;
            this.biSimulation.ItemAppearance.Normal.Options.UseForeColor = true;
            this.biSimulation.ItemAppearance.Pressed.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Question;
            this.biSimulation.ItemAppearance.Pressed.Options.UseForeColor = true;
            this.biSimulation.Name = "biSimulation";
            this.biSimulation.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biSimulation.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biSimulation_ItemClick);
            // 
            // biOptimizeParams
            // 
            this.biOptimizeParams.Caption = "Optimize Params";
            this.biOptimizeParams.Id = 11;
            this.biOptimizeParams.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biOptimizeParams.ImageOptions.SvgImage")));
            this.biOptimizeParams.ItemAppearance.Normal.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Question;
            this.biOptimizeParams.ItemAppearance.Normal.Options.UseForeColor = true;
            this.biOptimizeParams.Name = "biOptimizeParams";
            this.biOptimizeParams.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biOptimizeParams.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.biOptimizeParams.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biOptimizeParams_ItemClick);
            // 
            // btShowData
            // 
            this.btShowData.Caption = "<b>Show Data</b>";
            this.btShowData.Id = 6;
            this.btShowData.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btShowData.ImageOptions.SvgImage")));
            this.btShowData.Name = "btShowData";
            this.btShowData.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btShowData.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btShowData_ItemClick);
            // 
            // bcShowLog
            // 
            this.bcShowLog.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.bcShowLog.Caption = "<b>Show Log</b>";
            this.bcShowLog.Id = 10;
            this.bcShowLog.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bcShowLog.ImageOptions.SvgImage")));
            this.bcShowLog.Name = "bcShowLog";
            this.bcShowLog.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bcShowLog_CheckedChanged);
            // 
            // biSettings
            // 
            this.biSettings.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.biSettings.Caption = "Settings";
            this.biSettings.Id = 14;
            this.biSettings.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biSettings.ImageOptions.SvgImage")));
            this.biSettings.Name = "biSettings";
            this.biSettings.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biSettings_ItemClick);
            // 
            // biCollectionFormHelp
            // 
            this.biCollectionFormHelp.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.biCollectionFormHelp.Caption = "Help on this window";
            this.biCollectionFormHelp.Id = 15;
            this.biCollectionFormHelp.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biCollectionFormHelp.ImageOptions.SvgImage")));
            this.biCollectionFormHelp.Name = "biCollectionFormHelp";
            this.biCollectionFormHelp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.siStatus),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.beSimulationProgress, "", false, true, true, 210),
            new DevExpress.XtraBars.LinkPersistInfo(this.biCancel)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // siStatus
            // 
            this.siStatus.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.siStatus.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.siStatus.Caption = "    ";
            this.siStatus.Id = 3;
            this.siStatus.ItemAppearance.Normal.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Information;
            this.siStatus.ItemAppearance.Normal.Options.UseForeColor = true;
            this.siStatus.Name = "siStatus";
            // 
            // beSimulationProgress
            // 
            this.beSimulationProgress.AutoFillWidth = true;
            this.beSimulationProgress.Caption = "barEditItem1";
            this.beSimulationProgress.Edit = this.repositoryItemProgressBar1;
            this.beSimulationProgress.Id = 13;
            this.beSimulationProgress.Name = "beSimulationProgress";
            this.beSimulationProgress.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // repositoryItemProgressBar1
            // 
            this.repositoryItemProgressBar1.Maximum = 100000;
            this.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
            this.repositoryItemProgressBar1.ShowTitle = true;
            // 
            // biCancel
            // 
            this.biCancel.Caption = "<b><color=@Danger>Cancel Operation</color></b>";
            this.biCancel.Id = 18;
            this.biCancel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biCancel.ImageOptions.SvgImage")));
            this.biCancel.Name = "biCancel";
            this.biCancel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biCancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.biCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biCancel_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1904, 46);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 980);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1904, 46);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 46);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 934);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1904, 46);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 934);
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.HiddenPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dpLogPanel});
            this.dockManager1.MenuManager = this.barManager1;
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
            // dpLogPanel
            // 
            this.dpLogPanel.Controls.Add(this.dockPanel1_Container);
            this.dpLogPanel.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dpLogPanel.ID = new System.Guid("10bbb36d-c4c6-4135-83c3-42595d23e751");
            this.dpLogPanel.Location = new System.Drawing.Point(0, 526);
            this.dpLogPanel.Name = "dpLogPanel";
            this.dpLogPanel.OriginalSize = new System.Drawing.Size(200, 441);
            this.dpLogPanel.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dpLogPanel.SavedIndex = 0;
            this.dpLogPanel.Size = new System.Drawing.Size(1667, 441);
            this.dpLogPanel.Text = "Log";
            this.dpLogPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.logMessagesControl);
            this.dockPanel1_Container.Location = new System.Drawing.Point(8, 51);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(1651, 382);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // logMessagesControl
            // 
            this.logMessagesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logMessagesControl.Location = new System.Drawing.Point(0, 0);
            this.logMessagesControl.Name = "logMessagesControl";
            this.logMessagesControl.Size = new System.Drawing.Size(1651, 382);
            this.logMessagesControl.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.strategyBaseBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 46);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2,
            this.repositoryItemCheckEdit3,
            this.riTextEditState});
            this.gridControl1.Size = new System.Drawing.Size(1904, 934);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Click += new System.EventHandler(this.gridControl1_Click);
            // 
            // strategyBaseBindingSource
            // 
            this.strategyBaseBindingSource.DataSource = typeof(Crypto.Core.Strategies.StrategyBase);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colEnabled,
            this.colDemoMode,
            this.colDescription,
            this.colName,
            this.colStateText,
            this.colEarned,
            this.colEnableNotifications});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.gridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // colEnabled
            // 
            this.colEnabled.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colEnabled.FieldName = "Enabled";
            this.colEnabled.MinWidth = 40;
            this.colEnabled.Name = "colEnabled";
            this.colEnabled.Visible = true;
            this.colEnabled.VisibleIndex = 0;
            this.colEnabled.Width = 160;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.SvgToggle1;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.EditValueChanged += new System.EventHandler(this.repositoryItemCheckEdit1_EditValueChanged);
            // 
            // colDemoMode
            // 
            this.colDemoMode.ColumnEdit = this.repositoryItemCheckEdit2;
            this.colDemoMode.FieldName = "DemoMode";
            this.colDemoMode.MinWidth = 40;
            this.colDemoMode.Name = "colDemoMode";
            this.colDemoMode.Visible = true;
            this.colDemoMode.VisibleIndex = 1;
            this.colDemoMode.Width = 195;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.SvgToggle1;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.EditValueChanged += new System.EventHandler(this.repositoryItemCheckEdit2_EditValueChanged);
            // 
            // colDescription
            // 
            this.colDescription.ColumnEdit = this.repositoryItemTextEdit1;
            this.colDescription.FieldName = "Description";
            this.colDescription.MinWidth = 40;
            this.colDescription.Name = "colDescription";
            this.colDescription.OptionsColumn.AllowEdit = false;
            this.colDescription.Width = 554;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.MinWidth = 40;
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.OptionsColumn.ReadOnly = true;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 3;
            this.colName.Width = 361;
            // 
            // colStateText
            // 
            this.colStateText.ColumnEdit = this.riTextEditState;
            this.colStateText.FieldName = "StateText";
            this.colStateText.MinWidth = 40;
            this.colStateText.Name = "colStateText";
            this.colStateText.OptionsColumn.AllowEdit = false;
            this.colStateText.Visible = true;
            this.colStateText.VisibleIndex = 4;
            this.colStateText.Width = 366;
            // 
            // riTextEditState
            // 
            this.riTextEditState.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.riTextEditState.AutoHeight = false;
            this.riTextEditState.Name = "riTextEditState";
            // 
            // colEarned
            // 
            this.colEarned.DisplayFormat.FormatString = "0.0000000";
            this.colEarned.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colEarned.FieldName = "Earned";
            this.colEarned.MinWidth = 40;
            this.colEarned.Name = "colEarned";
            this.colEarned.OptionsColumn.AllowEdit = false;
            this.colEarned.Visible = true;
            this.colEarned.VisibleIndex = 5;
            this.colEarned.Width = 412;
            // 
            // colEnableNotifications
            // 
            this.colEnableNotifications.Caption = "Notifications";
            this.colEnableNotifications.ColumnEdit = this.repositoryItemCheckEdit3;
            this.colEnableNotifications.FieldName = "EnableNotifications";
            this.colEnableNotifications.MinWidth = 40;
            this.colEnableNotifications.Name = "colEnableNotifications";
            this.colEnableNotifications.Visible = true;
            this.colEnableNotifications.VisibleIndex = 2;
            this.colEnableNotifications.Width = 137;
            // 
            // repositoryItemCheckEdit3
            // 
            this.repositoryItemCheckEdit3.AutoHeight = false;
            this.repositoryItemCheckEdit3.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.SvgToggle1;
            this.repositoryItemCheckEdit3.Name = "repositoryItemCheckEdit3";
            this.repositoryItemCheckEdit3.EditValueChanged += new System.EventHandler(this.repositoryItemCheckEdit3_EditValueChanged);
            // 
            // StrategiesCollectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1026);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "StrategiesCollectionForm";
            this.Text = "Active Strategies";
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dpLogPanel.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.strategyBaseBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riTextEditState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private Bar bar1;
        private Bar bar3;
        private BarDockControl barDockControlTop;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarSubItem siAdd;
        private DevExpress.XtraBars.BarButtonItem biRemove;
        private DevExpress.XtraBars.BarButtonItem biEdit;
        private DevExpress.XtraBars.BarStaticItem siStatus;
        private BarButtonItem biStart;
        private BarButtonItem biStop;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource strategyBaseBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colEnabled;
        private DevExpress.XtraGrid.Columns.GridColumn colDemoMode;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colStateText;
        private DevExpress.XtraGrid.Columns.GridColumn colEarned;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private BarButtonItem btShowData;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private BarButtonItem biSimulation;
        private BarCheckItem bcShowLog;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dpLogPanel;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private LogMessagesControl logMessagesControl;
        private DevExpress.XtraGrid.Columns.GridColumn colEnableNotifications;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riTextEditState;
        private BarButtonItem biOptimizeParams;
        private BarEditItem beSimulationProgress;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;
        private BarButtonItem biSettings;
        private BarButtonItem biCollectionFormHelp;
        private BarButtonItem biStrategyHelp;
        private BarButtonItem biCopy;
        private BarButtonItem biCancel;
    }
}