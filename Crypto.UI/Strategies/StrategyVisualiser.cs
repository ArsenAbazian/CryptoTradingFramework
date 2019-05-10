using Crypto.Core.Strategies;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
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
    public class StrategyVisualizer {
        public void Visualize(StrategyBase strategy, GridControl grid, ChartControl chart) {
            Grid = grid;
            Chart = chart;
            Strategy = strategy;

            UpdateDataSource(strategy);
            InitializeTable();
            InitializeChart();
        }

        private void UpdateDataSource(StrategyBase strategy) {
            foreach(var item in strategy.DataItemInfos) {
                if(!string.IsNullOrEmpty(item.DataSourcePath)) {
                    item.DataSource = GetBindingValue(item.DataSourcePath, Strategy);
                }
            }
        }

        private object GetBindingValue(string dataSourcePath, object root) {
            string[] props = dataSourcePath.Split('.');
            object res = root;
            for(int i = 0; i < props.Length;i++) {
                PropertyInfo pInfo = res.GetType().GetProperty(props[i], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if(pInfo == null)
                    return null;
                res = pInfo.GetValue(res, null);
                if(res == null)
                    break;
            }
            return res;
        }

        public ChartControl Chart { get; private set; }
        public GridControl Grid { get; private set; }
        public StrategyBase Strategy { get; private set; }

        protected virtual void InitializeChart() {
            for(int i = 0; i < Strategy.DataItemInfos.Count; i++) {
                StrategyDataItemInfo info = Strategy.DataItemInfos[i];
                if(!info.Visibility.HasFlag(DataVisibility.Chart))
                    continue;
                if(info.ChartType == ChartType.Annotation)
                    continue;
                Series s = CreateSeries(info);
                Chart.Series.Add(s);
            }
            Chart.Series.RemoveAt(0);
            for(int i = 0; i < Strategy.DataItemInfos.Count; i++) {
                StrategyDataItemInfo info = Strategy.DataItemInfos[i];
                if(!info.Visibility.HasFlag(DataVisibility.Chart))
                    continue;
                if(info.ChartType != ChartType.Annotation)
                    continue;
                CreateAnnotations(info);
            }

            StrategyDataItemInfo di = Strategy.DataItemInfos.FirstOrDefault(i => i.Type == DataType.DateTime && i.FieldName == "Time");
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
                object value = GetBindingValue(items[0], obj);
                b.Append(string.Format(formatString, value));
                index = end + 1;
            }
            return b.ToString();
        }
        private void CreateAnnotations(StrategyDataItemInfo info) {
            if(Strategy.StrategyData.Count == 0)
                return;
            PropertyInfo pInfo = Strategy.StrategyData[0].GetType().GetProperty(info.FieldName, BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo pAnchor = Strategy.StrategyData[0].GetType().GetProperty(info.AnnotationAnchorField, BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo pTime = Strategy.StrategyData[0].GetType().GetProperty("Time", BindingFlags.Instance | BindingFlags.Public);
            XYDiagramPaneBase pane = ((XYDiagram)Chart.Diagram).DefaultPane;
            if(info.PanelIndex != 0)
                pane = ((XYDiagram)Chart.Diagram).Panes[info.PanelIndex];

            int index = 0;

            //Series s = CreatePointSeries(info);
            //s.DataSource = null;
            for(int i = 0; i < Strategy.StrategyData.Count; i++) {
                object obj = Strategy.StrategyData[i];
                bool value = (bool) pInfo.GetValue(obj);
                if(!value) {
                    index++;
                    continue;
                }
                DateTime time = (DateTime) pTime.GetValue(obj);
                double yValue = (double) pAnchor.GetValue(obj);
                //SeriesPoint pt = new SeriesPoint(time, new double[] { yValue });
                //s.Points.Add(pt);
                string annotationText = info.AnnotationText;
                if(info.HasAnnotationStringFormat)
                    annotationText = GetFormattedText(annotationText, obj);

                TextAnnotation annotation = pane.Annotations.AddTextAnnotation(info.FieldName + "InPane" + info.PanelIndex, annotationText);
                PaneAnchorPoint point = new PaneAnchorPoint();
                point.AxisXCoordinate.AxisValue = time;
                point.AxisYCoordinate.AxisValue = yValue;
                point.Pane = pane;
                annotation.AnchorPoint = point;
                annotation.ShapeKind = ShapeKind.Rectangle;
                //TextAnnotation annotation = pt.Annotations.AddTextAnnotation(info.FieldName, info.AnnotationText);
                //annotation.Text = info.AnnotationText;
                annotation.ShapeKind = ShapeKind.Rectangle;
                index++;
            }
            //Chart.Series.Add(s);
        }

        protected virtual Series CreateAnnotationSeriesCore(StrategyDataItemInfo info) {
            Series s = new Series();
            s.Name = info.FieldName;
            s.ArgumentDataMember = "Time";
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
            s.ArgumentDataMember = "Time";
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
            if(Strategy.StrategyData != null && Strategy.StrategyData.Count > 0)
                return Strategy.StrategyData;
            return null;
        }

        protected virtual Series CreateAreaSeries(StrategyDataItemInfo info) {
            Series s = new Series();
            s.Name = info.FieldName;
            s.ArgumentDataMember = "Time";
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
            s.ArgumentDataMember = "Time";
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
            s.ArgumentDataMember = "Time";
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
            
            if(Strategy.StrategyData.Count == 0)
                ((XYDiagram)Chart.Diagram).AxisX.DateTimeScaleOptions.MeasureUnitMultiplier = 30;
            else {
                object data1 = Strategy.StrategyData[1];
                object data0 = Strategy.StrategyData[0];
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
            diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
            while(diagram.Panes.Count <= info.PanelIndex - 1) {
                SecondaryAxisY axis = new SecondaryAxisY();
                axis.Assign(diagram.AxisY);
                diagram.SecondaryAxesY.Add(axis);
                XYDiagramPane pane = new XYDiagramPane() { Name = info.Name };
                diagram.Panes.Add(pane);

                //diagram.AxisY.SetVisibilityInPane(false, pane);
                //axis.SetVisibilityInPane(false, diagram.DefaultPane);
                //axis.SetVisibilityInPane(true, pane);
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
            int index = 0;
            GridView view = ((GridView)Grid.MainView);
            for(int i = 0; i < Strategy.DataItemInfos.Count; i++) {
                StrategyDataItemInfo info = Strategy.DataItemInfos[i];
                GridColumn column = new GridColumn();
                bool isVisible = info.Visibility.HasFlag(DataVisibility.Table);
                column.Visible = isVisible;
                if(isVisible)
                    column.VisibleIndex = index;
                column.FieldName = info.FieldName;
                column.DisplayFormat.FormatType = GetFormatType(info.Type);
                column.DisplayFormat.FormatString = info.FormatString;
                ((GridView) Grid.MainView).Columns.Add(column);
                if(isVisible)
                    index++;
            }
            for(int i = 0; i < Strategy.DataItemInfos.Count; i++) {
                StrategyDataItemInfo info = Strategy.DataItemInfos[i];
                if(!string.IsNullOrEmpty(info.AnnotationText)) {
                    ((GridView)Grid.MainView).FormatRules.Add(CreateRule(view.Columns[info.FieldName], true, info.Color, Color.FromArgb(0x20, info.Color)));
                }
            }
            lock(Strategy.StrategyData) {
                List<object> data = new List<object>();
                data.AddRange(Strategy.StrategyData);
                Grid.DataSource = data;
            }
            view.OptionsScrollAnnotations.ShowCustomAnnotations = DefaultBoolean.True;
            view.CustomScrollAnnotation += OnCustomScrollAnnotations;
        }

        private void OnCustomScrollAnnotations(object sender, GridCustomScrollAnnotationsEventArgs e) {
            List<object> items = Strategy.StrategyData;
            e.Annotations = new List<GridScrollAnnotationInfo>();
            if(items == null && e.Annotations != null) {
                e.Annotations.Clear();
                return;
            }
            List<StrategyDataItemInfo> aList = Strategy.DataItemInfos.Where(i => !string.IsNullOrEmpty(i.AnnotationText)).ToList();
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
                    if(!(bool)pList[j].GetValue(items[index], null))
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
