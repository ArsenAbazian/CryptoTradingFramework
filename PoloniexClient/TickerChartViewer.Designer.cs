using DevExpress.XtraBars;

namespace CryptoMarketClient {
    partial class TickerChartViewer {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.XYDiagramPane xyDiagramPane1 = new DevExpress.XtraCharts.XYDiagramPane();
            DevExpress.XtraCharts.SecondaryAxisY secondaryAxisY1 = new DevExpress.XtraCharts.SecondaryAxisY();
            DevExpress.XtraCharts.SecondaryAxisY secondaryAxisY2 = new DevExpress.XtraCharts.SecondaryAxisY();
            DevExpress.XtraCharts.Legend legend1 = new DevExpress.XtraCharts.Legend();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView1 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.CandleStickSeriesView candleStickSeriesView1 = new DevExpress.XtraCharts.CandleStickSeriesView();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView2 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView3 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TickerChartViewer));
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bsCandleStickPeriod = new DevExpress.XtraBars.BarSubItem();
            this.bcOneDay = new DevExpress.XtraBars.BarCheckItem();
            this.bcThreeDays = new DevExpress.XtraBars.BarCheckItem();
            this.bcOneWeek = new DevExpress.XtraBars.BarCheckItem();
            this.bcOneMonth = new DevExpress.XtraBars.BarCheckItem();
            this.bcOneMinute = new DevExpress.XtraBars.BarCheckItem();
            this.bcFiveMinutes = new DevExpress.XtraBars.BarCheckItem();
            this.bcFifteenMinutes = new DevExpress.XtraBars.BarCheckItem();
            this.bcThirtyMinutes = new DevExpress.XtraBars.BarCheckItem();
            this.bcOneHour = new DevExpress.XtraBars.BarCheckItem();
            this.bsChartType = new DevExpress.XtraBars.BarSubItem();
            this.bcStock = new DevExpress.XtraBars.BarCheckItem();
            this.bcCandle = new DevExpress.XtraBars.BarCheckItem();
            this.bcLine = new DevExpress.XtraBars.BarCheckItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.bcColoredStock = new DevExpress.XtraBars.BarCheckItem();
            this.bcColoredCandle = new DevExpress.XtraBars.BarCheckItem();
            this.bcArea = new DevExpress.XtraBars.BarCheckItem();
            this.repositoryItemCheckedComboBoxEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.sidePanel1 = new DevExpress.XtraEditors.SidePanel();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagramPane1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(candleStickSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            this.sidePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartControl1
            // 
            this.chartControl1.AutoLayout = false;
            this.chartControl1.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            xyDiagram1.AxisX.DateTimeScaleOptions.MeasureUnit = DevExpress.XtraCharts.DateTimeMeasureUnit.Minute;
            xyDiagram1.AxisX.Label.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xyDiagram1.AxisX.Label.TextPattern = "{A:d MMM h:mm:ss}";
            xyDiagram1.AxisX.StickToEnd = true;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1;0";
            xyDiagram1.AxisX.VisualRange.Auto = false;
            xyDiagram1.AxisX.VisualRange.AutoSideMargins = false;
            xyDiagram1.AxisX.VisualRange.MaxValueSerializable = "12/26/2017 16:37:00.000";
            xyDiagram1.AxisX.VisualRange.MinValueSerializable = "12/26/2017 16:28:00.000";
            xyDiagram1.AxisX.VisualRange.SideMarginsValue = 0D;
            xyDiagram1.AxisX.WholeRange.AutoSideMargins = false;
            xyDiagram1.AxisX.WholeRange.SideMarginsValue = 0D;
            xyDiagram1.AxisY.Alignment = DevExpress.XtraCharts.AxisAlignment.Far;
            xyDiagram1.AxisY.Label.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xyDiagram1.AxisY.Label.TextPattern = "{V:f8}";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.WholeRange.AlwaysShowZeroLevel = false;
            xyDiagram1.DependentAxesYRange = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.EnableAxisXScrolling = true;
            xyDiagram1.EnableAxisXZooming = true;
            xyDiagramPane1.Name = "Volume Pane";
            xyDiagramPane1.PaneID = 0;
            xyDiagramPane1.Weight = 0.3D;
            xyDiagram1.Panes.AddRange(new DevExpress.XtraCharts.XYDiagramPane[] {
            xyDiagramPane1});
            xyDiagram1.RangeControlDateTimeGridOptions.GridAlignment = DevExpress.XtraCharts.DateTimeGridAlignment.Year;
            xyDiagram1.RangeControlDateTimeGridOptions.GridMode = DevExpress.XtraCharts.ChartRangeControlClientGridMode.Manual;
            xyDiagram1.RangeControlDateTimeGridOptions.GridSpacing = 3D;
            xyDiagram1.RangeControlDateTimeGridOptions.SnapAlignment = DevExpress.XtraCharts.DateTimeGridAlignment.Minute;
            xyDiagram1.RangeControlDateTimeGridOptions.SnapMode = DevExpress.XtraCharts.ChartRangeControlClientSnapMode.Manual;
            xyDiagram1.RangeControlDateTimeGridOptions.SnapSpacing = 3D;
            secondaryAxisY1.Alignment = DevExpress.XtraCharts.AxisAlignment.Near;
            secondaryAxisY1.AxisID = 0;
            secondaryAxisY1.Label.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            secondaryAxisY1.Label.TextPattern = "{V:f8}";
            secondaryAxisY1.Name = "Secondary AxisY 1";
            secondaryAxisY1.VisibleInPanesSerializable = "0";
            secondaryAxisY2.AxisID = 1;
            secondaryAxisY2.Name = "Secondary AxisY 2";
            secondaryAxisY2.Visibility = DevExpress.Utils.DefaultBoolean.False;
            secondaryAxisY2.VisibleInPanesSerializable = "-1";
            xyDiagram1.SecondaryAxesY.AddRange(new DevExpress.XtraCharts.SecondaryAxisY[] {
            secondaryAxisY1,
            secondaryAxisY2});
            xyDiagram1.ZoomingOptions.AxisXMaxZoomPercent = 1000000D;
            this.chartControl1.Diagram = xyDiagram1;
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl1.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Left;
            this.chartControl1.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside;
            this.chartControl1.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartControl1.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
            this.chartControl1.Legend.MarkerMode = DevExpress.XtraCharts.LegendMarkerMode.CheckBoxAndMarker;
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            legend1.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Left;
            legend1.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside;
            legend1.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
            legend1.DockTargetName = "Volume Pane";
            legend1.MarkerMode = DevExpress.XtraCharts.LegendMarkerMode.CheckBoxAndMarker;
            legend1.Name = "Legend1";
            this.chartControl1.Legends.AddRange(new DevExpress.XtraCharts.Legend[] {
            legend1});
            this.chartControl1.Location = new System.Drawing.Point(0, 0);
            this.chartControl1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chartControl1.Name = "chartControl1";
            series1.Name = "Volume";
            sideBySideBarSeriesView1.AxisYName = "Secondary AxisY 2";
            sideBySideBarSeriesView1.BarWidth = 0.9D;
            sideBySideBarSeriesView1.Color = System.Drawing.Color.Silver;
            series1.View = sideBySideBarSeriesView1;
            series2.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series2.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.True;
            series2.Name = "Current";
            candleStickSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(66)))));
            candleStickSeriesView1.LineThickness = 1;
            candleStickSeriesView1.ReductionOptions.ColorMode = DevExpress.XtraCharts.ReductionColorMode.OpenToCloseValue;
            candleStickSeriesView1.ReductionOptions.FillMode = DevExpress.XtraCharts.CandleStickFillMode.AlwaysFilled;
            series2.View = candleStickSeriesView1;
            series3.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series3.LegendName = "Legend1";
            series3.Name = "Sell volume";
            sideBySideBarSeriesView2.AxisYName = "Secondary AxisY 1";
            sideBySideBarSeriesView2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            sideBySideBarSeriesView2.PaneName = "Volume Pane";
            sideBySideBarSeriesView2.RangeControlOptions.Visible = false;
            series3.View = sideBySideBarSeriesView2;
            series4.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series4.LegendName = "Legend1";
            series4.Name = "Buy volume";
            sideBySideBarSeriesView3.AxisYName = "Secondary AxisY 1";
            sideBySideBarSeriesView3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(66)))));
            sideBySideBarSeriesView3.PaneName = "Volume Pane";
            sideBySideBarSeriesView3.RangeControlOptions.Visible = false;
            series4.View = sideBySideBarSeriesView3;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2,
        series3,
        series4};
            this.chartControl1.SideBySideBarDistanceFixed = 0;
            this.chartControl1.Size = new System.Drawing.Size(1856, 1094);
            this.chartControl1.TabIndex = 4;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bsCandleStickPeriod,
            this.bcOneDay,
            this.bcThreeDays,
            this.bcOneWeek,
            this.bcOneMonth,
            this.bcOneMinute,
            this.bcFiveMinutes,
            this.bcFifteenMinutes,
            this.bcThirtyMinutes,
            this.bcOneHour,
            this.bsChartType,
            this.bcStock,
            this.barButtonItem1,
            this.barButtonItem2,
            this.bcColoredStock,
            this.bcCandle,
            this.bcColoredCandle,
            this.bcLine,
            this.bcArea,
            this.barSubItem1,
            this.barButtonItem3});
            this.barManager1.MaxItemId = 24;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckedComboBoxEdit1,
            this.repositoryItemComboBox1});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bsCandleStickPeriod),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsChartType),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // bsCandleStickPeriod
            // 
            this.bsCandleStickPeriod.Caption = "30 Minutes";
            this.bsCandleStickPeriod.Id = 2;
            this.bsCandleStickPeriod.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bsCandleStickPeriod.ImageOptions.Image")));
            this.bsCandleStickPeriod.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bsCandleStickPeriod.ImageOptions.LargeImage")));
            this.bsCandleStickPeriod.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bcOneDay),
            new DevExpress.XtraBars.LinkPersistInfo(this.bcThreeDays),
            new DevExpress.XtraBars.LinkPersistInfo(this.bcOneWeek),
            new DevExpress.XtraBars.LinkPersistInfo(this.bcOneMonth),
            new DevExpress.XtraBars.LinkPersistInfo(this.bcOneMinute, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bcFiveMinutes),
            new DevExpress.XtraBars.LinkPersistInfo(this.bcFifteenMinutes),
            new DevExpress.XtraBars.LinkPersistInfo(this.bcThirtyMinutes),
            new DevExpress.XtraBars.LinkPersistInfo(this.bcOneHour, true)});
            this.bsCandleStickPeriod.Name = "bsCandleStickPeriod";
            this.bsCandleStickPeriod.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // bcOneDay
            // 
            this.bcOneDay.Caption = "1 Day";
            this.bcOneDay.GroupIndex = 22;
            this.bcOneDay.Id = 3;
            this.bcOneDay.Name = "bcOneDay";
            this.bcOneDay.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.OnCandleStickPeriodChanged);
            // 
            // bcThreeDays
            // 
            this.bcThreeDays.Caption = "3 Days";
            this.bcThreeDays.GroupIndex = 22;
            this.bcThreeDays.Id = 4;
            this.bcThreeDays.Name = "bcThreeDays";
            this.bcThreeDays.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.OnCandleStickPeriodChanged);
            // 
            // bcOneWeek
            // 
            this.bcOneWeek.Caption = "1 Week";
            this.bcOneWeek.GroupIndex = 22;
            this.bcOneWeek.Id = 5;
            this.bcOneWeek.Name = "bcOneWeek";
            this.bcOneWeek.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.OnCandleStickPeriodChanged);
            // 
            // bcOneMonth
            // 
            this.bcOneMonth.Caption = "1 Month";
            this.bcOneMonth.GroupIndex = 22;
            this.bcOneMonth.Id = 6;
            this.bcOneMonth.Name = "bcOneMonth";
            this.bcOneMonth.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.OnCandleStickPeriodChanged);
            // 
            // bcOneMinute
            // 
            this.bcOneMinute.Caption = "1 Minute";
            this.bcOneMinute.GroupIndex = 22;
            this.bcOneMinute.Id = 7;
            this.bcOneMinute.Name = "bcOneMinute";
            this.bcOneMinute.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.OnCandleStickPeriodChanged);
            // 
            // bcFiveMinutes
            // 
            this.bcFiveMinutes.BindableChecked = true;
            this.bcFiveMinutes.Caption = "5 Minutes";
            this.bcFiveMinutes.Checked = true;
            this.bcFiveMinutes.GroupIndex = 22;
            this.bcFiveMinutes.Id = 8;
            this.bcFiveMinutes.Name = "bcFiveMinutes";
            this.bcFiveMinutes.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.OnCandleStickPeriodChanged);
            // 
            // bcFifteenMinutes
            // 
            this.bcFifteenMinutes.Caption = "15 Minutes";
            this.bcFifteenMinutes.GroupIndex = 22;
            this.bcFifteenMinutes.Id = 9;
            this.bcFifteenMinutes.Name = "bcFifteenMinutes";
            this.bcFifteenMinutes.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.OnCandleStickPeriodChanged);
            // 
            // bcThirtyMinutes
            // 
            this.bcThirtyMinutes.Caption = "30 Minutes";
            this.bcThirtyMinutes.GroupIndex = 22;
            this.bcThirtyMinutes.Id = 10;
            this.bcThirtyMinutes.Name = "bcThirtyMinutes";
            this.bcThirtyMinutes.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.OnCandleStickPeriodChanged);
            // 
            // bcOneHour
            // 
            this.bcOneHour.Caption = "1 Hour";
            this.bcOneHour.GroupIndex = 22;
            this.bcOneHour.Id = 11;
            this.bcOneHour.Name = "bcOneHour";
            this.bcOneHour.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.OnCandleStickPeriodChanged);
            // 
            // bsChartType
            // 
            this.bsChartType.Caption = "Chart Type";
            this.bsChartType.Id = 12;
            this.bsChartType.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bcStock),
            new DevExpress.XtraBars.LinkPersistInfo(this.bcCandle),
            new DevExpress.XtraBars.LinkPersistInfo(this.bcLine)});
            this.bsChartType.Name = "bsChartType";
            // 
            // bcStock
            // 
            this.bcStock.Caption = "Stock";
            this.bcStock.GroupIndex = 33;
            this.bcStock.Id = 13;
            this.bcStock.Name = "bcStock";
            this.bcStock.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.OnChartTypeChanged);
            // 
            // bcCandle
            // 
            this.bcCandle.Caption = "Candle";
            this.bcCandle.GroupIndex = 33;
            this.bcCandle.Id = 17;
            this.bcCandle.Name = "bcCandle";
            this.bcCandle.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.OnChartTypeChanged);
            // 
            // bcLine
            // 
            this.bcLine.Caption = "Line";
            this.bcLine.GroupIndex = 33;
            this.bcLine.Id = 19;
            this.bcLine.Name = "bcLine";
            this.bcLine.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.OnChartTypeChanged);
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "Indicators";
            this.barSubItem1.Id = 21;
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "Update From Server";
            this.barButtonItem3.Id = 22;
            this.barButtonItem3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.ImageOptions.Image")));
            this.barButtonItem3.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.ImageOptions.LargeImage")));
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.barDockControlTop.Size = new System.Drawing.Size(1856, 60);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 1154);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.barDockControlBottom.Size = new System.Drawing.Size(1856, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 60);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 1094);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1856, 60);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 1094);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Colored Bar";
            this.barButtonItem1.Id = 14;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Candle";
            this.barButtonItem2.Id = 15;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // bcColoredStock
            // 
            this.bcColoredStock.Caption = "Colored Stock";
            this.bcColoredStock.GroupIndex = 33;
            this.bcColoredStock.Id = 16;
            this.bcColoredStock.Name = "bcColoredStock";
            this.bcColoredStock.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.OnChartTypeChanged);
            // 
            // bcColoredCandle
            // 
            this.bcColoredCandle.BindableChecked = true;
            this.bcColoredCandle.Caption = "Colored Candle";
            this.bcColoredCandle.Checked = true;
            this.bcColoredCandle.GroupIndex = 33;
            this.bcColoredCandle.Id = 18;
            this.bcColoredCandle.Name = "bcColoredCandle";
            this.bcColoredCandle.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.OnChartTypeChanged);
            // 
            // bcArea
            // 
            this.bcArea.Caption = "Area";
            this.bcArea.GroupIndex = 33;
            this.bcArea.Id = 20;
            this.bcArea.Name = "bcArea";
            this.bcArea.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.OnChartTypeChanged);
            // 
            // repositoryItemCheckedComboBoxEdit1
            // 
            this.repositoryItemCheckedComboBoxEdit1.AutoHeight = false;
            this.repositoryItemCheckedComboBoxEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCheckedComboBoxEdit1.Name = "repositoryItemCheckedComboBoxEdit1";
            this.repositoryItemCheckedComboBoxEdit1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            this.repositoryItemComboBox1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            // 
            // sidePanel1
            // 
            this.sidePanel1.Controls.Add(this.chartControl1);
            this.sidePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidePanel1.Location = new System.Drawing.Point(0, 60);
            this.sidePanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sidePanel1.Name = "sidePanel1";
            this.sidePanel1.Size = new System.Drawing.Size(1856, 1094);
            this.sidePanel1.TabIndex = 9;
            this.sidePanel1.Text = "sidePanel1";
            // 
            // TickerChartViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sidePanel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "TickerChartViewer";
            this.Size = new System.Drawing.Size(1856, 1154);
            ((System.ComponentModel.ISupportInitialize)(xyDiagramPane1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(candleStickSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            this.sidePanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BarManager barManager1;
        private Bar bar1;
        private BarDockControl barDockControlTop;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit repositoryItemCheckedComboBoxEdit1;
        private BarSubItem bsCandleStickPeriod;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private BarCheckItem bcOneDay;
        private BarCheckItem bcThreeDays;
        private BarCheckItem bcOneWeek;
        private BarCheckItem bcOneMonth;
        private BarCheckItem bcOneMinute;
        private BarCheckItem bcFiveMinutes;
        private BarCheckItem bcFifteenMinutes;
        private BarCheckItem bcThirtyMinutes;
        private BarCheckItem bcOneHour;
        private BarSubItem bsChartType;
        private BarCheckItem bcStock;
        private BarCheckItem bcColoredStock;
        private BarCheckItem bcCandle;
        private BarCheckItem bcColoredCandle;
        private BarButtonItem barButtonItem1;
        private BarButtonItem barButtonItem2;
        private BarCheckItem bcLine;
        private BarCheckItem bcArea;
        private DevExpress.XtraEditors.SidePanel sidePanel1;
        private BarSubItem barSubItem1;
        private BarButtonItem barButtonItem3;
    }
}
