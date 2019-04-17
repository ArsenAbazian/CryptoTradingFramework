using Crypto.Core.Strategies;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
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

            InitializeTable();
            InitializeChart();
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
        }
        protected virtual Series CreateSeries(StrategyDataItemInfo info) {
            CheckAddPanel(info);
            Series res = null;
            if(info.ChartType == ChartType.CandleStick)
                res = CreateCandleStickSeries(info);
            if(info.ChartType == ChartType.Line)
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

        private void CreateAnnotations(StrategyDataItemInfo info) {
            if(Strategy.StrategyData.Count == 0)
                return;
            PropertyInfo pInfo = Strategy.StrategyData[0].GetType().GetProperty(info.FieldName, BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo pAnchor = Strategy.StrategyData[0].GetType().GetProperty(info.AnnotationAnchorField, BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo pTime = Strategy.StrategyData[0].GetType().GetProperty("Time", BindingFlags.Instance | BindingFlags.Public);

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
                TextAnnotation annotation = ((XYDiagram) Chart.Diagram).DefaultPane.Annotations.AddTextAnnotation(info.FieldName, info.AnnotationText);
                PaneAnchorPoint point = new PaneAnchorPoint();
                point.AxisXCoordinate.AxisValue = time;
                point.AxisYCoordinate.AxisValue = yValue;
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
            if(Strategy.StrategyData != null && Strategy.StrategyData.Count > 0)
                s.DataSource = Strategy.StrategyData;
            return s;
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
            if(Strategy.StrategyData != null && Strategy.StrategyData.Count > 0)
                s.DataSource = Strategy.StrategyData;
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
            if(Strategy.StrategyData != null && Strategy.StrategyData.Count > 0)
                s.DataSource = Strategy.StrategyData;
            return s;
        }

        private Series CreateLineSeries(StrategyDataItemInfo info) {
            Series s = new Series();
            s.Name = info.FieldName;
            s.ArgumentDataMember = "Time";
            s.ValueDataMembers.AddRange(info.FieldName);
            s.ValueScaleType = ScaleType.Numerical;
            s.ShowInLegend = true;
            StepLineSeriesView view = new StepLineSeriesView();
            view.Color = info.Color;
            view.LineStyle.Thickness = (int)(info.GraphWidth * DpiProvider.Default.DpiScaleFactor);
            s.View = view;
            if(Strategy.StrategyData != null && Strategy.StrategyData.Count > 0)
                s.DataSource = Strategy.StrategyData;
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

            
            ((XYDiagram)Chart.Diagram).AxisX.DateTimeScaleOptions.MeasureUnitMultiplier = info.CandleStickMinutesInterval;

            s.View = view;
            if(Strategy.StrategyData != null && Strategy.StrategyData.Count > 0)
                s.DataSource = Strategy.StrategyData;
            return s;
        }

        private void CheckAddPanel(StrategyDataItemInfo info) {
            XYDiagram diagram = (XYDiagram)Chart.Diagram;
            diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
            while(diagram.Panes.Count <= info.PanelIndex - 1) {
                SecondaryAxisY axis = new SecondaryAxisY();
                axis.Assign(diagram.AxisY);
                diagram.SecondaryAxesY.Add(axis);
                XYDiagramPane pane = new XYDiagramPane();
                diagram.Panes.Add(pane);

                //diagram.AxisY.SetVisibilityInPane(false, pane);
                //axis.SetVisibilityInPane(false, diagram.DefaultPane);
                //axis.SetVisibilityInPane(true, pane);
            }
        }

        private void InitializeTable() {
            int index = 0;
            for(int i = 0; i < Strategy.DataItemInfos.Count; i++) {
                StrategyDataItemInfo info = Strategy.DataItemInfos[i];
                if(!info.Visibility.HasFlag(DataVisibility.Table))
                    continue;
                GridColumn column = new GridColumn();
                column.Visible = true;
                column.VisibleIndex = index;
                column.FieldName = info.FieldName;
                column.DisplayFormat.FormatType = GetFormatType(info.Type);
                column.DisplayFormat.FormatString = info.FormatString;
                ((GridView) Grid.MainView).Columns.Add(column);
                index++;
            }
            lock(Strategy.StrategyData) {
                List<object> data = new List<object>();
                data.AddRange(Strategy.StrategyData);
                Grid.DataSource = data;
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
