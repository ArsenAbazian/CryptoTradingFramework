namespace Crypto.UI.Forms {
    partial class ChartDataControl {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartDataControl));
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.biCustomize = new DevExpress.XtraBars.BarButtonItem();
            this.biReset = new DevExpress.XtraBars.BarButtonItem();
            this.bsPanes = new DevExpress.XtraBars.BarSubItem();
            this.bsIndex = new DevExpress.XtraBars.BarStaticItem();
            this.bsSeries = new DevExpress.XtraBars.BarSubItem();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.chartControl = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            this.SuspendLayout();
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
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.biCustomize,
            this.biReset,
            this.bsPanes,
            this.bsIndex,
            this.bsSeries});
            this.barManager1.MaxItemId = 6;
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
            new DevExpress.XtraBars.LinkPersistInfo(this.bsIndex),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsSeries)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.StandaloneBarDockControl = this.standaloneBarDockControl1;
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
            this.bsIndex.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bsIndex_ItemClick);
            // 
            // bsSeries
            // 
            this.bsSeries.Caption = "Series";
            this.bsSeries.Id = 5;
            this.bsSeries.Name = "bsSeries";
            this.bsSeries.GetItemData += new System.EventHandler(this.bsSeries_GetItemData);
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.AutoSize = true;
            this.standaloneBarDockControl1.CausesValidation = false;
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(0, 0);
            this.standaloneBarDockControl1.Manager = this.barManager1;
            this.standaloneBarDockControl1.Margin = new System.Windows.Forms.Padding(4);
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(1570, 60);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlTop.Size = new System.Drawing.Size(1570, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 896);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlBottom.Size = new System.Drawing.Size(1570, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 896);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1570, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 896);
            // 
            // chartControl
            // 
            this.chartControl.AutoLayout = false;
            this.chartControl.CacheToMemory = true;
            xyDiagram1.AxisX.DateTimeScaleOptions.AggregateFunction = DevExpress.XtraCharts.AggregateFunction.None;
            xyDiagram1.AxisX.DateTimeScaleOptions.MeasureUnit = DevExpress.XtraCharts.DateTimeMeasureUnit.Minute;
            xyDiagram1.AxisX.Label.TextPattern = "{A:h:mm d.MM}";
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.Alignment = DevExpress.XtraCharts.AxisAlignment.Far;
            xyDiagram1.AxisY.Label.TextPattern = "{V:f8}";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.WholeRange.AlwaysShowZeroLevel = false;
            xyDiagram1.DependentAxesYRange = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.EnableAxisXScrolling = true;
            xyDiagram1.EnableAxisXZooming = true;
            this.chartControl.Diagram = xyDiagram1;
            this.chartControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Left;
            this.chartControl.Legend.Name = "Default Legend";
            this.chartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl.Location = new System.Drawing.Point(0, 60);
            this.chartControl.Margin = new System.Windows.Forms.Padding(4);
            this.chartControl.Name = "chartControl";
            series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series1.Name = "Series 1";
            series1.View = lineSeriesView1;
            this.chartControl.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartControl.SeriesTemplate.SeriesColorizer = null;
            this.chartControl.Size = new System.Drawing.Size(1570, 836);
            this.chartControl.TabIndex = 6;
            this.chartControl.DoubleClick += new System.EventHandler(this.chartControl_DoubleClick);
            this.chartControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chartControl_MouseDown);
            this.chartControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chartControl_MouseMove);
            // 
            // ChartDataControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartControl);
            this.Controls.Add(this.standaloneBarDockControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ChartDataControl";
            this.Size = new System.Drawing.Size(1570, 896);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem biCustomize;
        private DevExpress.XtraBars.BarButtonItem biReset;
        private DevExpress.XtraBars.BarSubItem bsPanes;
        private DevExpress.XtraBars.BarStaticItem bsIndex;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraCharts.ChartControl chartControl;
        private DevExpress.XtraBars.BarSubItem bsSeries;
    }
}
