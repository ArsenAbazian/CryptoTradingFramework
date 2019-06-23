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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartDataControl));
            DevExpress.XtraCharts.XYDiagram xyDiagram2 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView2 = new DevExpress.XtraCharts.LineSeriesView();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bsPanes = new DevExpress.XtraBars.BarSubItem();
            this.beNavigate = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.bsNavigate = new DevExpress.XtraBars.BarSubItem();
            this.bsEvents = new DevExpress.XtraBars.BarSubItem();
            this.bsHistogramm = new DevExpress.XtraBars.BarSubItem();
            this.bsCustomize = new DevExpress.XtraBars.BarSubItem();
            this.biCustomize = new DevExpress.XtraBars.BarButtonItem();
            this.biReset = new DevExpress.XtraBars.BarButtonItem();
            this.bsShowLegend = new DevExpress.XtraBars.BarCheckItem();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.bsSeries = new DevExpress.XtraBars.BarSubItem();
            this.chartControl = new DevExpress.XtraCharts.ChartControl();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.pmShowChart = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmShowChart)).BeginInit();
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
            this.bsSeries,
            this.beNavigate,
            this.bsNavigate,
            this.bsEvents,
            this.bsHistogramm,
            this.bsCustomize,
            this.bsShowLegend});
            this.barManager1.MaxItemId = 13;
            this.barManager1.OptionsStubGlyphs.AllowStubGlyphs = DevExpress.Utils.DefaultBoolean.True;
            this.barManager1.OptionsStubGlyphs.CaseMode = DevExpress.Utils.Drawing.GlyphTextCaseMode.UpperCase;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
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
            new DevExpress.XtraBars.LinkPersistInfo(this.bsPanes),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsSeries),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsNavigate),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.beNavigate, "", false, true, true, 147),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsEvents),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsHistogramm),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsCustomize)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar1.Text = "Tools";
            // 
            // bsPanes
            // 
            this.bsPanes.Caption = "Panes";
            this.bsPanes.Id = 2;
            this.bsPanes.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.SmallImagesText;
            this.bsPanes.Name = "bsPanes";
            this.bsPanes.GetItemData += new System.EventHandler(this.bsPanes_GetItemData);
            // 
            // beNavigate
            // 
            this.beNavigate.Caption = "Navigate";
            this.beNavigate.Edit = this.repositoryItemButtonEdit1;
            this.beNavigate.Id = 6;
            this.beNavigate.Name = "beNavigate";
            this.beNavigate.EditValueChanged += new System.EventHandler(this.beNavigate_EditValueChanged);
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Left, "", -1, true, true, true, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Right)});
            this.repositoryItemButtonEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemButtonEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit1_ButtonClick);
            // 
            // bsNavigate
            // 
            this.bsNavigate.Caption = "Navigate By";
            this.bsNavigate.Id = 7;
            this.bsNavigate.Name = "bsNavigate";
            this.bsNavigate.GetItemData += new System.EventHandler(this.bsNavigate_GetItemData);
            // 
            // bsEvents
            // 
            this.bsEvents.Caption = "Filter Events";
            this.bsEvents.Id = 8;
            this.bsEvents.Name = "bsEvents";
            this.bsEvents.GetItemData += new System.EventHandler(this.bsEvents_GetItemData);
            // 
            // bsHistogramm
            // 
            this.bsHistogramm.Caption = "Show Histogramm";
            this.bsHistogramm.Id = 10;
            this.bsHistogramm.Name = "bsHistogramm";
            this.bsHistogramm.GetItemData += new System.EventHandler(this.bsHistogramm_GetItemData);
            // 
            // bsCustomize
            // 
            this.bsCustomize.Caption = "Customize";
            this.bsCustomize.Id = 11;
            this.bsCustomize.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.biCustomize),
            new DevExpress.XtraBars.LinkPersistInfo(this.biReset),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsShowLegend)});
            this.bsCustomize.Name = "bsCustomize";
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
            // bsShowLegend
            // 
            this.bsShowLegend.Caption = "Show Legend";
            this.bsShowLegend.Id = 12;
            this.bsShowLegend.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bsShowLegend.ImageOptions.SvgImage")));
            this.bsShowLegend.Name = "bsShowLegend";
            this.bsShowLegend.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bsShowLegend_CheckedChanged);
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
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(1665, 58);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlTop.Size = new System.Drawing.Size(1665, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 896);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlBottom.Size = new System.Drawing.Size(1665, 0);
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
            this.barDockControlRight.Location = new System.Drawing.Point(1665, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 896);
            // 
            // bsSeries
            // 
            this.bsSeries.Caption = "Series";
            this.bsSeries.Id = 5;
            this.bsSeries.Name = "bsSeries";
            this.bsSeries.GetItemData += new System.EventHandler(this.bsSeries_GetItemData);
            // 
            // chartControl
            // 
            this.chartControl.AutoLayout = false;
            this.chartControl.CacheToMemory = true;
            this.chartControl.CrosshairOptions.ContentShowMode = DevExpress.XtraCharts.CrosshairContentShowMode.Legend;
            xyDiagram2.AxisX.DateTimeScaleOptions.AggregateFunction = DevExpress.XtraCharts.AggregateFunction.None;
            xyDiagram2.AxisX.DateTimeScaleOptions.MeasureUnit = DevExpress.XtraCharts.DateTimeMeasureUnit.Minute;
            xyDiagram2.AxisX.Label.TextPattern = "{A:h:mm d.MM}";
            xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram2.AxisY.Alignment = DevExpress.XtraCharts.AxisAlignment.Far;
            xyDiagram2.AxisY.Label.TextPattern = "{V:f8}";
            xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram2.AxisY.WholeRange.AlwaysShowZeroLevel = false;
            xyDiagram2.DependentAxesYRange = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram2.EnableAxisXScrolling = true;
            xyDiagram2.EnableAxisXZooming = true;
            this.chartControl.Diagram = xyDiagram2;
            this.chartControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Left;
            this.chartControl.Legend.BackColor = System.Drawing.Color.Transparent;
            this.chartControl.Legend.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.chartControl.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
            this.chartControl.Legend.DockTargetName = "Default Pane";
            this.chartControl.Legend.Font = new System.Drawing.Font("Segoe UI Semibold", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chartControl.Legend.MarkerMode = DevExpress.XtraCharts.LegendMarkerMode.CheckBoxAndMarker;
            this.chartControl.Legend.MaxCrosshairContentWidth = 200;
            this.chartControl.Legend.Name = "Default Legend";
            this.chartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartControl.Location = new System.Drawing.Point(0, 58);
            this.chartControl.Margin = new System.Windows.Forms.Padding(4);
            this.chartControl.Name = "chartControl";
            series2.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series2.Name = "Series 1";
            series2.View = lineSeriesView2;
            this.chartControl.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2};
            this.chartControl.SeriesTemplate.SeriesColorizer = null;
            this.chartControl.Size = new System.Drawing.Size(1665, 838);
            this.chartControl.TabIndex = 6;
            this.chartControl.ToolTipController = this.toolTipController1;
            this.chartControl.Click += new System.EventHandler(this.chartControl_Click);
            this.chartControl.DoubleClick += new System.EventHandler(this.chartControl_DoubleClick);
            this.chartControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chartControl_MouseDown);
            this.chartControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chartControl_MouseMove);
            // 
            // toolTipController1
            // 
            this.toolTipController1.AllowHtmlText = true;
            this.toolTipController1.Appearance.Font = new System.Drawing.Font("Lucida Console", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolTipController1.Appearance.Options.UseFont = true;
            this.toolTipController1.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
            // 
            // pmShowChart
            // 
            this.pmShowChart.Manager = this.barManager1;
            this.pmShowChart.Name = "pmShowChart";
            this.pmShowChart.BeforePopup += new System.ComponentModel.CancelEventHandler(this.pmShowChart_BeforePopup);
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
            this.Size = new System.Drawing.Size(1665, 896);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmShowChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem biCustomize;
        private DevExpress.XtraBars.BarButtonItem biReset;
        private DevExpress.XtraBars.BarSubItem bsPanes;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraCharts.ChartControl chartControl;
        private DevExpress.XtraBars.BarSubItem bsSeries;
        private DevExpress.XtraBars.BarEditItem beNavigate;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraBars.BarSubItem bsNavigate;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private DevExpress.XtraBars.BarSubItem bsEvents;
        private DevExpress.XtraBars.PopupMenu pmShowChart;
        private DevExpress.XtraBars.BarSubItem bsHistogramm;
        private DevExpress.XtraBars.BarSubItem bsCustomize;
        private DevExpress.XtraBars.BarCheckItem bsShowLegend;
    }
}
