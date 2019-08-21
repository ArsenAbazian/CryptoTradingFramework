namespace Crypto.Core.Arbitrages.Dependency {
    partial class DependencyArbitrageChartForm {
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
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.XYDiagramPane xyDiagramPane1 = new DevExpress.XtraCharts.XYDiagramPane();
            DevExpress.XtraCharts.SecondaryAxisY secondaryAxisY1 = new DevExpress.XtraCharts.SecondaryAxisY();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView2 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.StepAreaSeriesView stepAreaSeriesView1 = new DevExpress.XtraCharts.StepAreaSeriesView();
            DevExpress.XtraCharts.StepAreaSeriesView stepAreaSeriesView2 = new DevExpress.XtraCharts.StepAreaSeriesView();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue2 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule3 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleDataBar formatConditionRuleDataBar1 = new DevExpress.XtraEditors.FormatConditionRuleDataBar();
            this.colMaxDeviation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.myChartControl1 = new CryptoMarketClient.MyChartControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.dependencyArbitrageHistoryItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLowerAsk = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpperBid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaxDeviationExchange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaxDeviationTicker = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myChartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagramPane1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(stepAreaSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(stepAreaSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dependencyArbitrageHistoryItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // colMaxDeviation
            // 
            this.colMaxDeviation.DisplayFormat.FormatString = "0.00000000";
            this.colMaxDeviation.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMaxDeviation.FieldName = "MaxDeviation";
            this.colMaxDeviation.MinWidth = 40;
            this.colMaxDeviation.Name = "colMaxDeviation";
            this.colMaxDeviation.Visible = true;
            this.colMaxDeviation.VisibleIndex = 1;
            this.colMaxDeviation.Width = 150;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.myChartControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(2371, 1099);
            this.splitContainerControl1.SplitterPosition = 1673;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // myChartControl1
            // 
            xyDiagram1.AxisX.DateTimeScaleOptions.MeasureUnit = DevExpress.XtraCharts.DateTimeMeasureUnit.Second;
            xyDiagram1.AxisX.GridLines.MinorVisible = true;
            xyDiagram1.AxisX.GridLines.Visible = true;
            xyDiagram1.AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
            xyDiagram1.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
            xyDiagram1.AxisX.Label.TextPattern = "{A:hh:mm:ss}";
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1;0";
            xyDiagram1.AxisY.Alignment = DevExpress.XtraCharts.AxisAlignment.Far;
            xyDiagram1.AxisY.Label.TextPattern = "{V:f8}";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.WholeRange.AlwaysShowZeroLevel = false;
            xyDiagram1.EnableAxisXScrolling = true;
            xyDiagram1.EnableAxisXZooming = true;
            xyDiagramPane1.Name = "Pane 1";
            xyDiagramPane1.PaneID = 0;
            xyDiagramPane1.Weight = 0.5D;
            xyDiagram1.Panes.AddRange(new DevExpress.XtraCharts.XYDiagramPane[] {
            xyDiagramPane1});
            secondaryAxisY1.AxisID = 0;
            secondaryAxisY1.Label.TextPattern = "{V:f8}";
            secondaryAxisY1.Name = "Secondary AxisY 1";
            secondaryAxisY1.VisibleInPanesSerializable = "0";
            xyDiagram1.SecondaryAxesY.AddRange(new DevExpress.XtraCharts.SecondaryAxisY[] {
            secondaryAxisY1});
            this.myChartControl1.Diagram = xyDiagram1;
            this.myChartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myChartControl1.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Left;
            this.myChartControl1.Legend.Name = "Default Legend";
            this.myChartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.myChartControl1.Location = new System.Drawing.Point(0, 0);
            this.myChartControl1.Name = "myChartControl1";
            series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series1.Name = "Upper";
            series1.View = lineSeriesView1;
            series2.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series2.Name = "Lower";
            series2.View = lineSeriesView2;
            series3.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series3.Name = "Deviation";
            stepAreaSeriesView1.AxisYName = "Secondary AxisY 1";
            stepAreaSeriesView1.PaneName = "Pane 1";
            series3.View = stepAreaSeriesView1;
            this.myChartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2,
        series3};
            stepAreaSeriesView2.Transparency = ((byte)(0));
            this.myChartControl1.SeriesTemplate.View = stepAreaSeriesView2;
            this.myChartControl1.Size = new System.Drawing.Size(1673, 1099);
            this.myChartControl1.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.dependencyArbitrageHistoryItemBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(688, 1099);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Click += new System.EventHandler(this.gridControl1_Click);
            // 
            // dependencyArbitrageHistoryItemBindingSource
            // 
            this.dependencyArbitrageHistoryItemBindingSource.DataSource = typeof(Crypto.Core.Common.Arbitrages.DependencyArbitrageHistoryItem);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTime,
            this.colMaxDeviation,
            this.colLowerAsk,
            this.colUpperBid,
            this.colMaxDeviationExchange,
            this.colMaxDeviationTicker});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Greater;
            formatConditionRuleValue1.Expression = "[MaxDeviation] > [Thresold]";
            formatConditionRuleValue1.PredefinedName = "Green Text";
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.ApplyToRow = true;
            gridFormatRule2.Name = "Format1";
            formatConditionRuleValue2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            formatConditionRuleValue2.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue2.Value1 = false;
            gridFormatRule2.Rule = formatConditionRuleValue2;
            gridFormatRule3.Column = this.colMaxDeviation;
            gridFormatRule3.ColumnApplyTo = this.colMaxDeviation;
            gridFormatRule3.Name = "FormatDeviation";
            formatConditionRuleDataBar1.AxisColor = System.Drawing.Color.White;
            formatConditionRuleDataBar1.DrawAxisAtMiddle = true;
            formatConditionRuleDataBar1.PredefinedName = "Green";
            gridFormatRule3.Rule = formatConditionRuleDataBar1;
            this.gridView1.FormatRules.Add(gridFormatRule1);
            this.gridView1.FormatRules.Add(gridFormatRule2);
            this.gridView1.FormatRules.Add(gridFormatRule3);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.LevelIndent = 0;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 60;
            this.gridView1.OptionsSelection.CheckBoxSelectorField = "IsSelectedInDependencyArbitrageForm";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.PreviewIndent = 0;
            // 
            // colTime
            // 
            this.colTime.DisplayFormat.FormatString = "HH:mm:ss.fff";
            this.colTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTime.FieldName = "Time";
            this.colTime.MinWidth = 40;
            this.colTime.Name = "colTime";
            this.colTime.Visible = true;
            this.colTime.VisibleIndex = 0;
            this.colTime.Width = 150;
            // 
            // colLowerAsk
            // 
            this.colLowerAsk.DisplayFormat.FormatString = "0.00000000";
            this.colLowerAsk.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colLowerAsk.FieldName = "LowerAsk";
            this.colLowerAsk.MinWidth = 40;
            this.colLowerAsk.Name = "colLowerAsk";
            this.colLowerAsk.Visible = true;
            this.colLowerAsk.VisibleIndex = 2;
            this.colLowerAsk.Width = 150;
            // 
            // colUpperBid
            // 
            this.colUpperBid.DisplayFormat.FormatString = "0.00000000";
            this.colUpperBid.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colUpperBid.FieldName = "UpperBid";
            this.colUpperBid.MinWidth = 40;
            this.colUpperBid.Name = "colUpperBid";
            this.colUpperBid.Visible = true;
            this.colUpperBid.VisibleIndex = 3;
            this.colUpperBid.Width = 150;
            // 
            // colMaxDeviationExchange
            // 
            this.colMaxDeviationExchange.FieldName = "MaxDeviationExchange";
            this.colMaxDeviationExchange.MinWidth = 40;
            this.colMaxDeviationExchange.Name = "colMaxDeviationExchange";
            this.colMaxDeviationExchange.Visible = true;
            this.colMaxDeviationExchange.VisibleIndex = 4;
            this.colMaxDeviationExchange.Width = 150;
            // 
            // colMaxDeviationTicker
            // 
            this.colMaxDeviationTicker.FieldName = "MaxDeviationTicker";
            this.colMaxDeviationTicker.MinWidth = 40;
            this.colMaxDeviationTicker.Name = "colMaxDeviationTicker";
            this.colMaxDeviationTicker.Visible = true;
            this.colMaxDeviationTicker.VisibleIndex = 5;
            this.colMaxDeviationTicker.Width = 150;
            // 
            // DependencyArbitrageChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2371, 1099);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "DependencyArbitrageChartForm";
            this.Text = "Dependency Arbitrage Chart";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagramPane1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(stepAreaSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(stepAreaSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myChartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dependencyArbitrageHistoryItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private CryptoMarketClient.MyChartControl myChartControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource dependencyArbitrageHistoryItemBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colTime;
        private DevExpress.XtraGrid.Columns.GridColumn colMaxDeviation;
        private DevExpress.XtraGrid.Columns.GridColumn colLowerAsk;
        private DevExpress.XtraGrid.Columns.GridColumn colUpperBid;
        private DevExpress.XtraGrid.Columns.GridColumn colMaxDeviationExchange;
        private DevExpress.XtraGrid.Columns.GridColumn colMaxDeviationTicker;
    }
}