using Crypto.Core.Helpers;
using Crypto.Core.Indicators;
using Crypto.Core.Strategies;
using DevExpress.Skins;
using DevExpress.Sparkline;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Strategies {
    public class StrategyDataVisualiser {
        public void Visualize(IStrategyDataItemInfoOwner strategy, GridControl grid, ChartControl chart) {
            Grid = grid;
            Chart = chart;
            Visual = strategy;

            UpdateDataSource();
            InitializeTable();
            InitializeChart();
        }
        
        private void UpdateDataSource() {
            foreach(var item in Visual.DataItemInfos) {
                if(!string.IsNullOrEmpty(item.BindingSource)) {
                    object root = item.Value == null ? Visual : item.Value;
                    if(item.BindingRoot != null)
                        root = item.BindingRoot;
                    item.DataSource = BindingHelper.GetBindingValue(item.BindingSource, root);
                }
            }
        }

        public ChartControl Chart { get; set; }
        public GridControl Grid { get; set; }
        public IStrategyDataItemInfoOwner Visual { get; private set; }

        protected virtual void InitializeChart() {
            if(Chart == null)
                return;
            bool shouldRemoveDefaultSeries = Chart.Series.Count > 0;
            for(int i = 0; i < Visual.DataItemInfos.Count; i++) {
                StrategyDataItemInfo info = Visual.DataItemInfos[i];
                if(!info.Visibility.HasFlag(DataVisibility.Chart))
                    continue;
                if(info.ChartType == ChartType.Annotation)
                    continue;
                Series s = CreateSeries(info);
                Chart.Series.Add(s);
            }
            if(shouldRemoveDefaultSeries)
                Chart.Series.RemoveAt(0);
            for(int i = 0; i < Visual.DataItemInfos.Count; i++) {
                StrategyDataItemInfo info = Visual.DataItemInfos[i];
                if(!info.Visibility.HasFlag(DataVisibility.Chart))
                    continue;
                if(info.ChartType != ChartType.Annotation)
                    continue;
                CreateAnnotations(info);
            }

            StrategyDataItemInfo di = Visual.DataItemInfos.FirstOrDefault(i => i.Type == DataType.DateTime && i.FieldName == "Time");
            if(di != null) {
                if(di.UseCustomTimeUnit) {
                    ((XYDiagram)Chart.Diagram).AxisX.DateTimeScaleOptions.MeasureUnit = (DateTimeMeasureUnit)Enum.Parse(typeof(DateTimeMeasureUnit), di.TimeUnit.ToString());
                    ((XYDiagram)Chart.Diagram).AxisX.DateTimeScaleOptions.MeasureUnitMultiplier = di.TimeUnitMeasureMultiplier;
                }
            }
        }
        protected virtual Series CreateSeries(StrategyDataItemInfo info) {
            CheckAddPanel(info);
            Series res = null;
            if(info.ChartType == ChartType.CandleStick)
                res = CreateCandleStickSeries(info);
            if(info.ChartType == ChartType.Line || info.ChartType == ChartType.StepLine)
                res = CreateLineSeries(info);
            if(info.ChartType == ChartType.Bar)
                res = CreateBarSeries(info);
            if(info.ChartType == ChartType.Area)
                res = CreateAreaSeries(info);
            if(info.ChartType == ChartType.Dot)
                res = CreatePointSeries(info);

            XYDiagramSeriesViewBase view = (XYDiagramSeriesViewBase)res.View;
            if(info.PanelIndex > 0) {
                view.Pane = ((XYDiagram2D)Chart.Diagram).Panes[info.PanelIndex - 1];
                view.AxisY = ((XYDiagram)Chart.Diagram).SecondaryAxesY[info.PanelIndex - 1];
            }
            
            return res;
        }

        string GetFormattedText(string annotationText, object obj) {
            StringBuilder b = new StringBuilder();
            int index = annotationText.IndexOf('{');
            b.Append(annotationText.Substring(0, index));
            while(true) {
                int newIndex = annotationText.IndexOf('{', index);
                if(newIndex == -1) {
                    b.Append(annotationText.Substring(index));
                    break;
                }
                b.Append(annotationText.Substring(index, newIndex - index));
                index = newIndex;
                int end = annotationText.IndexOf('}', index);
                string path = annotationText.Substring(index + 1, end - index - 1);
                string[] items = path.Split(':');
                string formatString = items.Length > 1 ? items[1] : null;
                if(formatString == null)
                    formatString = "{0}";
                else
                    formatString = "{0:" + items[1] + "}";
                object value = BindingHelper.GetBindingValue(items[0], obj);
                b.Append(string.Format(formatString, value));
                index = end + 1;
            }
            return b.ToString();
        }
        private void CreateAnnotations(StrategyDataItemInfo info) {
            if(Visual.Items.Count == 0)
                return;
            PropertyInfo pInfo = Visual.Items[0].GetType().GetProperty(info.FieldName, BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo pAnchor = Visual.Items[0].GetType().GetProperty(info.AnnotationAnchorField, BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo pTime = Visual.Items[0].GetType().GetProperty("Time", BindingFlags.Instance | BindingFlags.Public);
            XYDiagramPaneBase pane = ((XYDiagram)Chart.Diagram).DefaultPane;
            if(info.PanelIndex != 0)
                pane = ((XYDiagram)Chart.Diagram).Panes[info.PanelIndex];

            int index = 0;

            //Series s = CreatePointSeries(info);
            //s.DataSource = null;
            for(int i = 0; i < Visual.Items.Count; i++) {
                object obj = Visual.Items[i];
                object value = pInfo.GetValue(obj);
                if(value == null) {
                    index++;
                    continue;
                }
                if(value is bool && !((bool)value)) {
                    index++;
                    continue;
                }
                DateTime time = (DateTime) pTime.GetValue(obj);
                double yValue = (double) pAnchor.GetValue(obj);
                string annotationText = value is string? Convert.ToString(value): info.AnnotationText;
                if(info.HasAnnotationStringFormat)
                    annotationText = GetFormattedText(annotationText, obj);

                TextAnnotation annotation = pane.Annotations.AddTextAnnotation(info.FieldName + "InPane" + info.PanelIndex, annotationText);
                PaneAnchorPoint point = new PaneAnchorPoint();
                point.AxisXCoordinate.AxisValue = time;
                point.AxisYCoordinate.AxisValue = yValue;
                point.Pane = pane;
                annotation.AnchorPoint = point;
                annotation.ShapeKind = ShapeKind.Rectangle;
                annotation.ShapeKind = ShapeKind.Rectangle;
                index++;
            }
        }

        protected virtual Series CreateAnnotationSeriesCore(StrategyDataItemInfo info) {
            Series s = new Series();
            s.Name = info.FieldName;
            s.ArgumentDataMember = GetArgumentDataMember(info);
            s.ValueDataMembers.AddRange(info.FieldName);
            s.ValueScaleType = ScaleType.Numerical;
            PointSeriesView view = new PointSeriesView();
            view.Color = info.Color;
            view.PointMarkerOptions.Size = info.GraphWidth == 1 ? view.PointMarkerOptions.Size : (int)(info.GraphWidth * DpiProvider.Default.DpiScaleFactor);
            s.View = view;
            return s;
        }

        protected virtual Series CreatePointSeries(StrategyDataItemInfo info) {
            Series s = new Series();
            s.Name = info.FieldName;
            s.ArgumentDataMember = GetArgumentDataMember(info);
            s.ValueDataMembers.AddRange(info.FieldName);
            s.ValueScaleType = ScaleType.Numerical;
            PointSeriesView view = new PointSeriesView();
            view.Color = info.Color;
            view.PointMarkerOptions.Size = info.GraphWidth == 1 ? view.PointMarkerOptions.Size : (int)(info.GraphWidth * DpiProvider.Default.DpiScaleFactor);
            s.View = view;
            s.DataSource = GetDataSource(info);
            return s;
        }

        private object GetDataSource(StrategyDataItemInfo info) {
            if(info.DataSource != null)
                return info.DataSource;
            if(Visual.Items != null && Visual.Items.Count > 0)
                return Visual.Items;
            return null;
        }

        protected string GetArgumentDataMember(StrategyDataItemInfo info) {
            return string.IsNullOrEmpty(info.ArgumentDataMember) ? "Time" : info.ArgumentDataMember;
        }

        protected virtual Series CreateAreaSeries(StrategyDataItemInfo info) {
            Series s = new Series();
            s.Name = info.FieldName;
            s.ArgumentDataMember = GetArgumentDataMember(info);
            s.ValueDataMembers.AddRange(info.FieldName);
            s.ValueScaleType = ScaleType.Numerical;
            s.ShowInLegend = true;
            AreaSeriesView view = new AreaSeriesView();
            view.Color = info.Color;
            s.View = view;
            s.DataSource = GetDataSource(info);
            return s;
        }

        protected virtual Series CreateBarSeries(StrategyDataItemInfo info) {
            Series s = new Series();
            s.Name = info.FieldName;
            s.ArgumentDataMember = GetArgumentDataMember(info);
            s.ValueDataMembers.AddRange(info.FieldName);
            s.ValueScaleType = ScaleType.Numerical;
            s.ShowInLegend = true;
            SideBySideBarSeriesView view = new SideBySideBarSeriesView();
            view.Color = info.Color;
            view.BarWidth = info.GraphWidth == 1 ? view.BarWidth : (int)(info.GraphWidth * DpiProvider.Default.DpiScaleFactor);
            s.View = view;
            s.DataSource = GetDataSource(info);
            return s;
        }

        private Series CreateLineSeries(StrategyDataItemInfo info) {
            Series s = new Series();
            s.Name = info.FieldName;
            s.ArgumentDataMember = GetArgumentDataMember(info);
            s.ValueDataMembers.AddRange(info.FieldName);
            s.ValueScaleType = ScaleType.Numerical;
            s.ShowInLegend = true;
            LineSeriesView view = info.ChartType == ChartType.StepLine? new StepLineSeriesView(): new LineSeriesView();
            view.Color = info.Color;
            view.LineStyle.Thickness = (int)(info.GraphWidth * DpiProvider.Default.DpiScaleFactor);
            s.View = view;
            s.DataSource = GetDataSource(info);
            return s;
        }

        protected virtual Series CreateCandleStickSeries(StrategyDataItemInfo info) {
            Series s = new Series("Kline", ViewType.CandleStick);
            s.ArgumentDataMember = "Time";
            s.ArgumentScaleType = ScaleType.DateTime;
            s.ValueDataMembers.AddRange("Low", "High", "Open", "Close");
            s.ValueScaleType = ScaleType.Numerical;
            
            CandleStickSeriesView view = new CandleStickSeriesView();

            view.LineThickness = (int)(info.GraphWidth * DpiProvider.Default.DpiScaleFactor);
            view.LevelLineLength = 0.25;
            view.ReductionOptions.ColorMode = ReductionColorMode.OpenToCloseValue;
            view.ReductionOptions.FillMode = CandleStickFillMode.FilledOnReduction;
            
            view.ReductionOptions.Level = StockLevel.Open;
            view.ReductionOptions.Visible = true;
            view.AggregateFunction = SeriesAggregateFunction.None;
            view.LineThickness = (int)(1 * DpiProvider.Default.DpiScaleFactor);
            view.LevelLineLength = 0.25;
            
            if(Visual.Items.Count == 0)
                ((XYDiagram)Chart.Diagram).AxisX.DateTimeScaleOptions.MeasureUnitMultiplier = 30;
            else {
                object data1 = Visual.Items[1];
                object data0 = Visual.Items[0];
                PropertyInfo pi = data1.GetType().GetProperty("Time", BindingFlags.Public | BindingFlags.Instance);
                if(pi == null)
                    ((XYDiagram)Chart.Diagram).AxisX.DateTimeScaleOptions.MeasureUnitMultiplier = 30;
                else {
                    DateTime time1 = (DateTime)pi.GetValue(data1);
                    DateTime time0 = (DateTime)pi.GetValue(data0);
                    ((XYDiagram)Chart.Diagram).AxisX.DateTimeScaleOptions.MeasureUnitMultiplier = (int)((time1 - time0).TotalMinutes);
                }
            }

            s.View = view;
            s.DataSource = GetDataSource(info);
            return s;
        }

        private void CheckAddPanel(StrategyDataItemInfo info) {
            XYDiagram diagram = (XYDiagram)Chart.Diagram;
            if(diagram == null)
                return;
            diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
            while(diagram.Panes.Count <= info.PanelIndex - 1) {
                SecondaryAxisY axis = new SecondaryAxisY();
                axis.Assign(diagram.AxisY);
                diagram.SecondaryAxesY.Add(axis);
                XYDiagramPane pane = new XYDiagramPane() { Name = info.Name };
                diagram.Panes.Add(pane);
            }
        }

        private GridFormatRule CreateRule(GridColumn column, bool value, Color foreColor, Color backColor) {
            GridFormatRule rule = new GridFormatRule();
            FormatConditionRuleValue cond = new FormatConditionRuleValue();

            cond.Appearance.ForeColor = foreColor;
            cond.Appearance.BackColor = backColor;
            cond.Condition = FormatCondition.Equal;
            cond.Value1 = value;

            rule.Tag = new object();
            rule.Name = column.FieldName + "Equal" + rule.GetHashCode();
            rule.Column = column;
            rule.ApplyToRow = true;
            rule.ColumnApplyTo = column;
            rule.Rule = cond;
            return rule;
        }

        private void InitializeTable() {
            if(Grid == null)
                return;
            int index = 0;
            object first = Visual.Items.Count > 0 ? Visual.Items[0] : null;
            GridView view = ((GridView)Grid.MainView);
            for(int i = 0; i < Visual.DataItemInfos.Count; i++) {
                StrategyDataItemInfo info = Visual.DataItemInfos[i];
                GridColumn column = new GridColumn();
                column.Tag = info;
                bool isVisible = info.Visibility.HasFlag(DataVisibility.Table);
                column.Visible = isVisible;
                if(isVisible)
                    column.VisibleIndex = index;
                RepositoryItem editor = CreateEditor(first, info);
                if(editor != null) {
                    Grid.RepositoryItems.Add(editor);
                    column.ColumnEdit = editor;
                }
                column.FieldName = info.FieldName;
                column.DisplayFormat.FormatType = GetFormatType(info.Type);
                column.DisplayFormat.FormatString = info.FormatString;
                ((GridView) Grid.MainView).Columns.Add(column);
                if(isVisible)
                    index++;
            }
            for(int i = 0; i < Visual.DataItemInfos.Count; i++) {
                StrategyDataItemInfo info = Visual.DataItemInfos[i];
                if(!string.IsNullOrEmpty(info.AnnotationText)) {
                    ((GridView)Grid.MainView).FormatRules.Add(CreateRule(view.Columns[info.FieldName], true, info.Color, Color.FromArgb(0x20, info.Color)));
                }
            }
            lock(Visual.Items) {
                List<object> data = new List<object>();
                data.AddRange(Visual.Items);
                Grid.DataSource = data;
            }
            view.OptionsScrollAnnotations.ShowCustomAnnotations = DefaultBoolean.True;
            view.CustomScrollAnnotation += OnCustomScrollAnnotations;
        }

        protected RepositoryItem CreateEditor(object first, StrategyDataItemInfo info) {
            if(first == null)
                return null;
            if(info.FieldName == null)
                return null;
            PropertyInfo pi = first.GetType().GetProperty(info.FieldName, BindingFlags.Instance | BindingFlags.Public);
            if(pi == null)
                return null;
            object value = pi.GetValue(first);
            if(value is bool) {
                RepositoryItemCheckEdit res = new RepositoryItemCheckEdit();
                res.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.SvgStar2;
                return res;
            }
            double[] doubleData = null;
            if(value is ResizeableArray<TimeBaseValue>) {
                ResizeableArray<TimeBaseValue> data = (ResizeableArray<TimeBaseValue>)value;
                doubleData = new double[Math.Min(data.Count, 50)];
                for(int i = 0; i < doubleData.Length; i++)
                    doubleData[i] = data[i].Value;
            }
            if(value is double[]) {
                doubleData = (double[])value;
            }
            if(doubleData != null) {
                RepositoryItemSparklineEdit res = new RepositoryItemSparklineEdit();
                res.View = new AreaSparklineView() {
                    AreaOpacity = 30,
                    HighlightStartPoint = false,
                    HighlightEndPoint = false,
                    HighlightMaxPoint = false,
                    HighlightMinPoint = false,
                    HighlightNegativePoints = false
                };
                return res;
            }
            return null;
        }

        private void OnCustomScrollAnnotations(object sender, GridCustomScrollAnnotationsEventArgs e) {
            ResizeableArray<object> items = Visual.Items;
            e.Annotations = new List<GridScrollAnnotationInfo>();
            if(items == null && e.Annotations != null) {
                e.Annotations.Clear();
                return;
            }
            List<StrategyDataItemInfo> aList = Visual.DataItemInfos.Where(i => !string.IsNullOrEmpty(i.AnnotationText)).ToList();
            if(aList.Count == 0)
                return;
            List<PropertyInfo> pList = new List<PropertyInfo>();
            foreach(StrategyDataItemInfo a in aList) {
                PropertyInfo info = items[0].GetType().GetProperty(a.FieldName, BindingFlags.Instance | BindingFlags.Public);
                pList.Add(info);
            }
            int index = 0;
            foreach(object item in items) {
                for(int j = 0; j < aList.Count; j++) {
                    object value = pList[j].GetValue(items[index], null);
                    if(value is bool && !((bool)value))
                        continue;
                    if(value == null)
                        continue;
                    GridScrollAnnotationInfo info = new GridScrollAnnotationInfo();
                    info.Index = index;
                    info.RowHandle = ((GridView)Grid.MainView).GetRowHandle(index);
                    info.Color = aList[j].Color;
                    e.Annotations.Add(info);
                }
                index++;
            }
        }

        private FormatType GetFormatType(DataType type) {
            if(type == DataType.Numeric)
                return FormatType.Numeric;
            if(type == DataType.DateTime)
                return FormatType.DateTime;
            return FormatType.Numeric;
        }
    }
}
