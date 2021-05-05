namespace StockMarketsGapScaner {
    partial class TinkoffGapScannerForm {
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
            DevExpress.Sparkline.BarSparklineView barSparklineView1 = new DevExpress.Sparkline.BarSparklineView();
            DevExpress.Sparkline.BarSparklineView barSparklineView2 = new DevExpress.Sparkline.BarSparklineView();
            DevExpress.Sparkline.AreaSparklineView areaSparklineView1 = new DevExpress.Sparkline.AreaSparklineView();
            DevExpress.Sparkline.AreaSparklineView areaSparklineView2 = new DevExpress.Sparkline.AreaSparklineView();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TinkoffGapScannerForm));
            this.colIsReady = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTicker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNyseClosePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNyseAfterMarketPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTinkoffInvestCurrentBid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLastUpdateTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colXetraPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGapPerc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.repositoryItemCheckEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemSparklineEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit();
            this.repositoryItemSparklineEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit();
            this.repositoryItemSparklineEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit();
            this.repositoryItemSparklineEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit();
            this.svgImageCollection1 = new DevExpress.Utils.SvgImageCollection(this.components);
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.svgImageCollection2 = new DevExpress.Utils.SvgImageCollection(this.components);
            this.bbOpenWeb = new DevExpress.XtraBars.BarButtonItem();
            this.bbMinimalProfitSpread = new DevExpress.XtraBars.BarButtonItem();
            this.bbUpdateBot = new DevExpress.XtraBars.BarButtonItem();
            this.bsShowTickerChart = new DevExpress.XtraBars.BarSubItem();
            this.biAddTicker = new DevExpress.XtraBars.BarButtonItem();
            this.biOpen = new DevExpress.XtraBars.BarButtonItem();
            this.biSave = new DevExpress.XtraBars.BarButtonItem();
            this.biNew = new DevExpress.XtraBars.BarButtonItem();
            this.biRemove = new DevExpress.XtraBars.BarButtonItem();
            this.biEdit = new DevExpress.XtraBars.BarButtonItem();
            this.biUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.biStop = new DevExpress.XtraBars.BarButtonItem();
            this.biStatus = new DevExpress.XtraBars.BarStaticItem();
            this.biCopyFrom = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.rpPoloniex = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup5 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
            this.xtraOpenFileDialog1 = new DevExpress.XtraEditors.XtraOpenFileDialog(this.components);
            this.svgImageCollection3 = new DevExpress.Utils.SvgImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection3)).BeginInit();
            this.SuspendLayout();
            // 
            // colIsReady
            // 
            this.colIsReady.FieldName = "IsReady";
            this.colIsReady.MinWidth = 40;
            this.colIsReady.Name = "colIsReady";
            this.colIsReady.Width = 150;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = typeof(StockMarketsGapScaner.TinkoffGapScannerInfo);
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(12);
            this.gridControl1.Location = new System.Drawing.Point(0, 277);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(12);
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
            this.gridControl1.Size = new System.Drawing.Size(1466, 600);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTicker,
            this.colName,
            this.colNyseClosePrice,
            this.colNyseAfterMarketPrice,
            this.colTinkoffInvestCurrentBid,
            this.colIsReady,
            this.colLastUpdateTime,
            this.colXetraPrice,
            this.colGap,
            this.colGapPerc});
            this.gridView1.DetailHeight = 673;
            this.gridView1.FixedLineWidth = 4;
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Column = this.colIsReady;
            gridFormatRule1.Name = "Error";
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.PredefinedName = "Red Fill";
            formatConditionRuleValue1.Value1 = false;
            gridFormatRule1.Rule = formatConditionRuleValue1;
            this.gridView1.FormatRules.Add(gridFormatRule1);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            // 
            // colTicker
            // 
            this.colTicker.FieldName = "Ticker";
            this.colTicker.MinWidth = 40;
            this.colTicker.Name = "colTicker";
            this.colTicker.Visible = true;
            this.colTicker.VisibleIndex = 0;
            this.colTicker.Width = 150;
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.MinWidth = 40;
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            this.colName.Width = 150;
            // 
            // colNyseClosePrice
            // 
            this.colNyseClosePrice.FieldName = "NyseClosePrice";
            this.colNyseClosePrice.MinWidth = 40;
            this.colNyseClosePrice.Name = "colNyseClosePrice";
            this.colNyseClosePrice.Visible = true;
            this.colNyseClosePrice.VisibleIndex = 2;
            this.colNyseClosePrice.Width = 150;
            // 
            // colNyseAfterMarketPrice
            // 
            this.colNyseAfterMarketPrice.FieldName = "NyseAfterMarketPrice";
            this.colNyseAfterMarketPrice.MinWidth = 40;
            this.colNyseAfterMarketPrice.Name = "colNyseAfterMarketPrice";
            this.colNyseAfterMarketPrice.Visible = true;
            this.colNyseAfterMarketPrice.VisibleIndex = 3;
            this.colNyseAfterMarketPrice.Width = 150;
            // 
            // colTinkoffInvestCurrentBid
            // 
            this.colTinkoffInvestCurrentBid.FieldName = "TinkoffInvestCurrentBid";
            this.colTinkoffInvestCurrentBid.MinWidth = 40;
            this.colTinkoffInvestCurrentBid.Name = "colTinkoffInvestCurrentBid";
            this.colTinkoffInvestCurrentBid.Visible = true;
            this.colTinkoffInvestCurrentBid.VisibleIndex = 5;
            this.colTinkoffInvestCurrentBid.Width = 150;
            // 
            // colLastUpdateTime
            // 
            this.colLastUpdateTime.DisplayFormat.FormatString = "hh:mm:ss.fff";
            this.colLastUpdateTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colLastUpdateTime.FieldName = "LastUpdateTime";
            this.colLastUpdateTime.MinWidth = 40;
            this.colLastUpdateTime.Name = "colLastUpdateTime";
            this.colLastUpdateTime.Visible = true;
            this.colLastUpdateTime.VisibleIndex = 8;
            this.colLastUpdateTime.Width = 150;
            // 
            // colXetraPrice
            // 
            this.colXetraPrice.FieldName = "XetraPrice";
            this.colXetraPrice.MinWidth = 40;
            this.colXetraPrice.Name = "colXetraPrice";
            this.colXetraPrice.Visible = true;
            this.colXetraPrice.VisibleIndex = 4;
            this.colXetraPrice.Width = 150;
            // 
            // colGap
            // 
            this.colGap.Caption = "GAP";
            this.colGap.FieldName = "Gap";
            this.colGap.MinWidth = 40;
            this.colGap.Name = "colGap";
            this.colGap.Visible = true;
            this.colGap.VisibleIndex = 6;
            this.colGap.Width = 150;
            // 
            // colGapPerc
            // 
            this.colGapPerc.Caption = "GAP %";
            this.colGapPerc.FieldName = "GapPerc";
            this.colGapPerc.MinWidth = 40;
            this.colGapPerc.Name = "colGapPerc";
            this.colGapPerc.Visible = true;
            this.colGapPerc.VisibleIndex = 7;
            this.colGapPerc.Width = 150;
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
            // repositoryItemCheckEdit3
            // 
            this.repositoryItemCheckEdit3.AutoHeight = false;
            this.repositoryItemCheckEdit3.Name = "repositoryItemCheckEdit3";
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
            // repositoryItemCheckEdit4
            // 
            this.repositoryItemCheckEdit4.AutoHeight = false;
            this.repositoryItemCheckEdit4.Name = "repositoryItemCheckEdit4";
            // 
            // repositoryItemSparklineEdit1
            // 
            this.repositoryItemSparklineEdit1.Name = "repositoryItemSparklineEdit1";
            this.repositoryItemSparklineEdit1.ValueRange.IsAuto = false;
            barSparklineView1.Color = System.Drawing.Color.Green;
            this.repositoryItemSparklineEdit1.View = barSparklineView1;
            // 
            // repositoryItemSparklineEdit2
            // 
            this.repositoryItemSparklineEdit2.Name = "repositoryItemSparklineEdit2";
            barSparklineView2.Color = System.Drawing.Color.Red;
            this.repositoryItemSparklineEdit2.View = barSparklineView2;
            // 
            // repositoryItemSparklineEdit3
            // 
            this.repositoryItemSparklineEdit3.Name = "repositoryItemSparklineEdit3";
            areaSparklineView1.Color = System.Drawing.Color.Green;
            areaSparklineView1.ScaleFactor = 2F;
            this.repositoryItemSparklineEdit3.View = areaSparklineView1;
            // 
            // repositoryItemSparklineEdit4
            // 
            this.repositoryItemSparklineEdit4.Name = "repositoryItemSparklineEdit4";
            areaSparklineView2.Color = System.Drawing.Color.Red;
            areaSparklineView2.ScaleFactor = 2F;
            this.repositoryItemSparklineEdit4.View = areaSparklineView2;
            // 
            // svgImageCollection1
            // 
            this.svgImageCollection1.Add("information", "image://devav/actions/about.svg");
            this.svgImageCollection1.Add("connected", "image://devav/actions/apply.svg");
            this.svgImageCollection1.Add("disconnected", "image://devav/actions/close.svg");
            this.svgImageCollection1.Add("download", "image://devav/actions/download.svg");
            this.svgImageCollection1.Add("connecting", "image://devav/actions/reverssort.svg");
            this.svgImageCollection1.Add("datadelay", "image://devav/people/employeenotice.svg");
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "Not Connected";
            this.barStaticItem1.Id = 41;
            this.barStaticItem1.ImageOptions.ImageIndex = 0;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "File";
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Images = this.svgImageCollection2;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.bbOpenWeb,
            this.bbMinimalProfitSpread,
            this.bbUpdateBot,
            this.bsShowTickerChart,
            this.biAddTicker,
            this.biOpen,
            this.biSave,
            this.biNew,
            this.biRemove,
            this.biEdit,
            this.biUpdate,
            this.biStop,
            this.biStatus,
            this.biCopyFrom,
            this.barButtonItem1});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Margin = new System.Windows.Forms.Padding(12);
            this.ribbonControl1.MaxItemId = 44;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpPoloniex});
            this.ribbonControl1.Size = new System.Drawing.Size(1466, 277);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // svgImageCollection2
            // 
            this.svgImageCollection2.Add("information", "image://devav/actions/about.svg");
            this.svgImageCollection2.Add("connected", "image://devav/actions/apply.svg");
            this.svgImageCollection2.Add("disconnected", "image://devav/actions/close.svg");
            this.svgImageCollection2.Add("download", "image://devav/actions/download.svg");
            this.svgImageCollection2.Add("connecting", "image://devav/actions/reverssort.svg");
            this.svgImageCollection2.Add("datadelay", "image://devav/people/employeenotice.svg");
            // 
            // bbOpenWeb
            // 
            this.bbOpenWeb.Caption = "Open Market in Web";
            this.bbOpenWeb.Id = 8;
            this.bbOpenWeb.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbOpenWeb.ImageOptions.SvgImage")));
            this.bbOpenWeb.Name = "bbOpenWeb";
            this.bbOpenWeb.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // bbMinimalProfitSpread
            // 
            this.bbMinimalProfitSpread.Caption = "Minimal Profit Spread";
            this.bbMinimalProfitSpread.Id = 21;
            this.bbMinimalProfitSpread.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbMinimalProfitSpread.ImageOptions.SvgImage")));
            this.bbMinimalProfitSpread.Name = "bbMinimalProfitSpread";
            this.bbMinimalProfitSpread.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // bbUpdateBot
            // 
            this.bbUpdateBot.Caption = "Register Telegram Bot";
            this.bbUpdateBot.Id = 25;
            this.bbUpdateBot.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbUpdateBot.ImageOptions.SvgImage")));
            this.bbUpdateBot.Name = "bbUpdateBot";
            this.bbUpdateBot.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbUpdateBot_ItemClick);
            // 
            // bsShowTickerChart
            // 
            this.bsShowTickerChart.Caption = "Show Ticker Chart";
            this.bsShowTickerChart.Id = 28;
            this.bsShowTickerChart.Name = "bsShowTickerChart";
            // 
            // biAddTicker
            // 
            this.biAddTicker.Caption = "Add Ticker";
            this.biAddTicker.Id = 33;
            this.biAddTicker.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biAddTicker.ImageOptions.SvgImage")));
            this.biAddTicker.Name = "biAddTicker";
            // 
            // biOpen
            // 
            this.biOpen.Caption = "Open";
            this.biOpen.Id = 34;
            this.biOpen.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biOpen.ImageOptions.SvgImage")));
            this.biOpen.Name = "biOpen";
            this.biOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biOpen_ItemClick);
            // 
            // biSave
            // 
            this.biSave.Caption = "Save";
            this.biSave.Id = 35;
            this.biSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biSave.ImageOptions.SvgImage")));
            this.biSave.Name = "biSave";
            // 
            // biNew
            // 
            this.biNew.Caption = "New";
            this.biNew.Id = 36;
            this.biNew.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biNew.ImageOptions.SvgImage")));
            this.biNew.Name = "biNew";
            // 
            // biRemove
            // 
            this.biRemove.Caption = "Remove Selected";
            this.biRemove.Id = 37;
            this.biRemove.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biRemove.ImageOptions.SvgImage")));
            this.biRemove.Name = "biRemove";
            // 
            // biEdit
            // 
            this.biEdit.Caption = "Edit";
            this.biEdit.Id = 38;
            this.biEdit.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biEdit.ImageOptions.SvgImage")));
            this.biEdit.Name = "biEdit";
            // 
            // biUpdate
            // 
            this.biUpdate.Caption = "Start Update";
            this.biUpdate.Id = 39;
            this.biUpdate.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biUpdate.ImageOptions.SvgImage")));
            this.biUpdate.Name = "biUpdate";
            this.biUpdate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biUpdate_ItemClick);
            // 
            // biStop
            // 
            this.biStop.Caption = "Stop \r\nUpdate";
            this.biStop.Id = 40;
            this.biStop.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biStop.ImageOptions.SvgImage")));
            this.biStop.Name = "biStop";
            this.biStop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biStop_ItemClick);
            // 
            // biStatus
            // 
            this.biStatus.Caption = "Not Connected";
            this.biStatus.Id = 41;
            this.biStatus.ImageOptions.ImageIndex = 0;
            this.biStatus.Name = "biStatus";
            this.biStatus.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // biCopyFrom
            // 
            this.biCopyFrom.Caption = "Add From";
            this.biCopyFrom.Id = 42;
            this.biCopyFrom.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biCopyFrom.ImageOptions.SvgImage")));
            this.biCopyFrom.Name = "biCopyFrom";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Update Once";
            this.barButtonItem1.Id = 43;
            this.barButtonItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem1.ImageOptions.SvgImage")));
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // rpPoloniex
            // 
            this.rpPoloniex.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup4,
            this.ribbonPageGroup5});
            this.rpPoloniex.Name = "rpPoloniex";
            this.rpPoloniex.Text = "Tickers Web Info";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.biOpen);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "File";
            // 
            // ribbonPageGroup4
            // 
            this.ribbonPageGroup4.ItemLinks.Add(this.barButtonItem1);
            this.ribbonPageGroup4.ItemLinks.Add(this.biUpdate, true);
            this.ribbonPageGroup4.ItemLinks.Add(this.biStop);
            this.ribbonPageGroup4.Name = "ribbonPageGroup4";
            this.ribbonPageGroup4.Text = "Info";
            // 
            // ribbonPageGroup5
            // 
            this.ribbonPageGroup5.ItemLinks.Add(this.bbMinimalProfitSpread);
            this.ribbonPageGroup5.ItemLinks.Add(this.bbUpdateBot);
            this.ribbonPageGroup5.ItemLinks.Add(this.bbOpenWeb);
            this.ribbonPageGroup5.Name = "ribbonPageGroup5";
            this.ribbonPageGroup5.Text = "Gap Tools";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.ItemLinks.Add(this.barStaticItem2);
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 877);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(1466, 54);
            // 
            // barStaticItem2
            // 
            this.barStaticItem2.Caption = "Not Connected";
            this.barStaticItem2.Id = 41;
            this.barStaticItem2.ImageOptions.ImageIndex = 0;
            this.barStaticItem2.Name = "barStaticItem2";
            this.barStaticItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // xtraOpenFileDialog1
            // 
            this.xtraOpenFileDialog1.FileName = "Open TickersWebInfo File";
            this.xtraOpenFileDialog1.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
            // 
            // svgImageCollection3
            // 
            this.svgImageCollection3.Add("information", "image://devav/actions/about.svg");
            this.svgImageCollection3.Add("connected", "image://devav/actions/apply.svg");
            this.svgImageCollection3.Add("disconnected", "image://devav/actions/close.svg");
            this.svgImageCollection3.Add("download", "image://devav/actions/download.svg");
            this.svgImageCollection3.Add("connecting", "image://devav/actions/reverssort.svg");
            this.svgImageCollection3.Add("datadelay", "image://devav/people/employeenotice.svg");
            // 
            // TinkoffGapScannerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1466, 931);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "TinkoffGapScannerForm";
            this.Text = "TinkoffGapScannerForm";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit repositoryItemSparklineEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit repositoryItemSparklineEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit repositoryItemSparklineEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit repositoryItemSparklineEdit4;
        private DevExpress.Utils.SvgImageCollection svgImageCollection1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.Utils.SvgImageCollection svgImageCollection2;
        private DevExpress.XtraBars.BarButtonItem bbOpenWeb;
        private DevExpress.XtraBars.BarButtonItem bbMinimalProfitSpread;
        private DevExpress.XtraBars.BarButtonItem bbUpdateBot;
        private DevExpress.XtraBars.BarSubItem bsShowTickerChart;
        private DevExpress.XtraBars.BarButtonItem biAddTicker;
        private DevExpress.XtraBars.BarButtonItem biOpen;
        private DevExpress.XtraBars.BarButtonItem biSave;
        private DevExpress.XtraBars.BarButtonItem biNew;
        private DevExpress.XtraBars.BarButtonItem biRemove;
        private DevExpress.XtraBars.BarButtonItem biEdit;
        private DevExpress.XtraBars.BarButtonItem biUpdate;
        private DevExpress.XtraBars.BarButtonItem biStop;
        private DevExpress.XtraBars.BarStaticItem biStatus;
        private DevExpress.XtraBars.BarButtonItem biCopyFrom;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpPoloniex;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup4;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup5;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem2;
        private DevExpress.XtraEditors.XtraOpenFileDialog xtraOpenFileDialog1;
        private DevExpress.Utils.SvgImageCollection svgImageCollection3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colTicker;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colNyseClosePrice;
        private DevExpress.XtraGrid.Columns.GridColumn colNyseAfterMarketPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colTinkoffInvestCurrentBid;
        private DevExpress.XtraGrid.Columns.GridColumn colIsReady;
        private DevExpress.XtraGrid.Columns.GridColumn colLastUpdateTime;
        private DevExpress.XtraGrid.Columns.GridColumn colXetraPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colGap;
        private DevExpress.XtraGrid.Columns.GridColumn colGapPerc;
    }
}