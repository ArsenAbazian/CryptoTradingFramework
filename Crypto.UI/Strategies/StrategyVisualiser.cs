using Crypto.Core.Helpers;
using Crypto.Core.Indicators;
using Crypto.Core.Strategies;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Sparkline;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.StyleFormatConditions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Strategies {
    public class StrategyDataVisualiser {
        public StrategyDataVisualiser(IStrategyDataItemInfoOwner strategy) {
            Visual = strategy;
            UpdateDataSource();
        }

        public void Visualize(GridControl grid) {
            Grid = grid;
            InitializeTable();
        }

        public void Visualize(ChartControl chart) {
            Chart = chart;
            InitializeChart();
        }

        public void Visualize(TreeList treeList) {
            TreeList = treeList;
            InitializeTreeList();
        }

        private void UpdateDataSource() {
            foreach(var item in Visual.DataItemInfos) {
                object root = item.Value == null ? Visual : item.Value;
                if(!string.IsNullOrEmpty(item.BindingSource)) {
                    if(item.BindingRoot != null)
                        root = item.BindingRoot;
                    item.DataSource = BindingHelper.GetBindingValue(item.BindingSource, root);
                }
                else if(item.DataSource == null) {
                    item.DataSource = Visual.Items;
                }
            }
        }

        public event DataControlProvideEventHandler GetControl;
        protected void RaiseGetControl(DataControlProvideEventArgs e) {
            if(GetControl == null)
                return;
            GetControl(this, e);
        }

        public ChartControl Chart { get; set; }
        public GridControl Grid { get; set; }
        public TreeList TreeList { get; set; }
        public bool SkipSeparateItems { get; set; } = true;
        public IStrategyDataItemInfoOwner Visual { get; private set; }

        protected virtual void InitializeChart() {
            if(Chart == null)
                return;
            Chart.CacheToMemory = true;
            bool shouldRemoveDefaultSeries = Chart.Series.Count > 0;
            int seriesCount = 0;

            for(int i = 0; i < Visual.DataItemInfos.Count; i++) {
                StrategyDataItemInfo info = Visual.DataItemInfos[i];
                if(SkipSeparateItems && info.SeparateWindow)
                    continue;
                if(info.ZoomAsMap) {
                    ((XYDiagram)Chart.Diagram).EnableAxisYScrolling = true;
                    ((XYDiagram)Chart.Diagram).EnableAxisYZooming = true;
                    ((XYDiagram)Chart.Diagram).DependentAxesYRange = DefaultBoolean.False;
                }
                if(info.Type == DataType.HistogrammData)
                    info = CreateHistogrammDetailItem(info);
                if(!info.Visibility.HasFlag(DataVisibility.Chart))
                    continue;
                if(info.ChartType == ChartType.Annotation)
                    continue;
                Series s = CreateSeries(info);
                if(s == null)
                    continue;
                if(!Chart.Series.Contains(s))
                    Chart.Series.Add(s);
                OnAfterAddSeries(s);
                seriesCount++;
            }
            if(seriesCount == 0)
                return;
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

            CheckInitializeTimeAxis();
            
            for(int i = 0; i < Visual.DataItemInfos.Count; i++) {
                StrategyDataItemInfo info = Visual.DataItemInfos[i];
                if(info == Visual)
                    continue;
                if(info.SeparateWindow) {
                    if(info.Type == DataType.HistogrammData)
                        info = CreateHistogrammDetailItem(info);
                    if(info.Items == null)
                        info.Items = Visual.Items;
                    DataControlProvideEventArgs e = new DataControlProvideEventArgs() { DataItem = info };
                    RaiseGetControl(e);
                }
            }
        }

        private void OnAfterAddSeries(Series s) {
            
        }

        private StrategyDataItemInfo CreateHistogrammDetailItem(StrategyDataItemInfo info) {
            return info.CreateHistogrammDetailItem(Visual);
        }

        protected virtual void CreateConstantLines(StrategyDataItemInfo info) {
            XYDiagram dg = (XYDiagram)Chart.Diagram;
            XYDiagramPaneBase pane = CheckAddPanel(info);
            Axis axis = null;

            if(info.PanelName == "Default") {
                axis = info.ChartType == ChartType.ConstantX? (Axis)dg.AxisX: (Axis)dg.AxisY;
            }
            else {
                if(info.ChartType == ChartType.ConstantX)
                    axis = dg.SecondaryAxesX[info.PanelName];
                else
                    axis = dg.SecondaryAxesY[info.PanelName];
            }
            if(axis == null)
                return;

            if(axis != null) {
                System.Collections.IEnumerable en = info.DataSource as System.Collections.IEnumerable;
                if(en != null) {
                    PropertyInfo pi = null;
                    foreach(object item in en) {
                        if(pi == null) pi = item.GetType().GetProperty(info.FieldName, BindingFlags.Instance | BindingFlags.Public);
                        double value = Convert.ToDouble(pi.GetValue(item));
                        axis.ConstantLines.Add(new ConstantLine() { AxisValue = value, Color = info.Color, Name = item.ToString(), ShowInLegend = false });
                    }
                }
                else {
                    object value = info.DataSource == null ? info.Value : info.DataSource;
                    if(value != null)
                        axis.ConstantLines.Add(new ConstantLine() { AxisValue = value, Color = info.Color, Name = info.Name, ShowInLegend = false });
                }
                if(info.ChartType == ChartType.ConstantY) {
                    this.Chart.AxisWholeRangeChanged -= OnYAxisWholeRangeChanged;
                    this.Chart.AxisWholeRangeChanged += OnYAxisWholeRangeChanged;
                }
            }
        }

        private void OnYAxisWholeRangeChanged(object sender, AxisRangeChangedEventArgs e) {
            if(e.Axis is AxisY) {
                if(((AxisY)e.Axis).ConstantLines.Count == 0)
                    return;
                double min = Convert.ToDouble(((AxisY)e.Axis).ConstantLines.Min( a => a.AxisValue));
                double minAxis = Convert.ToDouble(e.Axis.WholeRange.MinValue);
                if(min < minAxis)
                    e.Axis.WholeRange.MinValue = min;
            }
        }

        protected int BigDataCount { get { return 30000; } }

        protected virtual Series CreateSeries(StrategyDataItemInfo info) {
            CheckAddPanel(info);
            Series res = null;
            if(info.ChartType == ChartType.ConstantX || info.ChartType == ChartType.ConstantY) {
                CreateConstantLines(info);
                return null;
            }
            if(info.ChartType == ChartType.CandleStick)
                res = CreateCandleStickSeries(info);
            if(info.ChartType == ChartType.Line || info.ChartType == ChartType.StepLine)
                res = CreateLineSeries(info);
            if(info.ChartType == ChartType.Bar)
                res = CreateBarSeries(info);
            if(info.ChartType == ChartType.Area || info.ChartType == ChartType.StepArea)
                res = CreateAreaSeries(info);
            if(info.ChartType == ChartType.Dot)
                res = CreatePointSeries(info);

            if(res != null) {
                if(res.Points != null && res.Points.Count > BigDataCount) {
                    Chart.SelectionMode = ElementSelectionMode.None;
                    Chart.RuntimeHitTesting = false;
                }
                res.DataSourceSorted = true;
                res.SeriesPointsSorting = SortingMode.None;
            }
            IResizeableArray array = res.DataSource as IResizeableArray;
            if(array != null && array.Count > BigDataCount) { // optimization
                PointSeriesView view2 = res.View as PointSeriesView;
                if(view2 != null)
                    view2.AggregateFunction = SeriesAggregateFunction.None;
            }
            XYDiagramSeriesViewBase view = (XYDiagramSeriesViewBase)res.View;
            if(info.PanelName != "Default") {
                view.Pane = ((XYDiagram)Chart.Diagram).Panes[info.PanelName];
                view.AxisY = ((XYDiagram)Chart.Diagram).SecondaryAxesY[info.AxisYName];
                if(((XYDiagram)Chart.Diagram).SecondaryAxesX[info.AxisXName] != null)
                    view.AxisX = ((XYDiagram)Chart.Diagram).SecondaryAxesX[info.AxisXName];
                res.Legend = Chart.Legends[info.PanelName];
            }
            else {
                
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
            if(info.PanelName != "Default")
                pane = ((XYDiagram)Chart.Diagram).Panes[info.PanelName];

            int index = 0;

            ResizeableArray<TextAnnotation> res = new ResizeableArray<TextAnnotation>();
            info.DataSource = res;
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
                string annotationText = string.Empty;
                if(info.HasAnnotationStringFormat)
                    annotationText = GetFormattedText(info.AnnotationText, obj);
                else
                    annotationText = Convert.ToString(value);

                TextAnnotation annotation = pane.Annotations.AddTextAnnotation(info.FieldName + "InPane" + info.PanelName, annotationText);
                res.Add(annotation);
                annotation.Tag = obj;
                annotation.ConnectorStyle = AnnotationConnectorStyle.Line;
                annotation.ShapeKind = ShapeKind.Rectangle;
                annotation.Font = new Font("Segoe UI", 6);
                ((RelativePosition)annotation.ShapePosition).Angle = 180;
                ((RelativePosition)annotation.ShapePosition).ConnectorLength = 70;
                
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
            s.Name = info.Name;
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
            s.Name = info.Name;
            s.ArgumentDataMember = GetArgumentDataMember(info);
            s.ArgumentScaleType = GetArgumentScaleType(info);
            s.ValueDataMembers.AddRange(info.FieldName);
            s.ValueScaleType = ScaleType.Numerical;
            PointSeriesView view = new PointSeriesView();
            view.Color = info.Color;
            view.PointMarkerOptions.Size = info.GraphWidth == 1 ? view.PointMarkerOptions.Size : (int)(info.GraphWidth * DpiProvider.Default.DpiScaleFactor);
            s.View = view;
            object dataSource = GetDataSource(info);
            IResizeableArray array = dataSource as IResizeableArray;
            if(array == null || array.Count < BigDataCount) {
                s.DataSource = dataSource;
            }
            else {
                view.PointMarkerOptions.BorderVisible = false;
                view.PointMarkerOptions.Kind = MarkerKind.Square;
                view.PointMarkerOptions.Size = ScaleUtils.ScaleValue(4);
                s.Points.AddRange(CreateSeriesPoints(info));
            }
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

        ScaleType GetArgumentScaleType(StrategyDataItemInfo info) {
            switch(info.ArgumentScaleType) {
                case ArgumentScaleType.DateTime:
                    return ScaleType.DateTime;
                case ArgumentScaleType.Numerical:
                    return ScaleType.Numerical;
            }
            return ScaleType.DateTime;
        }

        static Expression CreateAccessor(Type type, MemberInfo mInfo, out ParameterExpression parameter) {
            parameter = Expression.Parameter(typeof(object), "instance");
            var instance = Expression.Convert(parameter, type);
            return Expression.MakeMemberAccess(instance, mInfo);
        }

        static MemberInfo GetMemberInfo(Type type, string memberName) {
            return (MemberInfo)type.GetProperty(memberName) ??
                type.GetField(memberName);
        }

        static Func<object, object> MakeAccessor(Type type, Type propertyType, string memberName) {
            ParameterExpression parameter;
            var accessor = CreateAccessor(type, GetMemberInfo(type, memberName), out parameter);
            var expression = Expression.Convert(accessor, typeof(object));
            return Expression.Lambda<Func<object, object>>(expression, parameter).Compile();
        }

        protected virtual Series CreateAreaSeries(StrategyDataItemInfo info) {
            Series s = new Series();
            s.Name = info.Name;
            s.ArgumentDataMember = GetArgumentDataMember(info);
            s.ArgumentScaleType = GetArgumentScaleType(info);
            s.ValueDataMembers.AddRange(info.FieldName);
            s.ValueScaleType = ScaleType.Numerical;
            s.ShowInLegend = true;
            AreaSeriesView view = null;
            if(info.ChartType == ChartType.Area)
                view = new AreaSeriesView();
            else
                view = new StepAreaSeriesView();
            view.Color = info.Color;
            s.View = view;
            object dataSource = GetDataSource(info);
            IResizeableArray array = dataSource as IResizeableArray;
            if(array == null || array.Count < BigDataCount) {
                s.DataSource = dataSource;
            }
            else {
                s.Points.AddRange(CreateSeriesPoints(info));
            }
            return s;
        }

        protected virtual Series CreateBarSeries(StrategyDataItemInfo info) {
            Series s = new Series();
            Chart.Series.Add(s);
            s.Name = info.Name;
            s.ArgumentDataMember = GetArgumentDataMember(info);
            s.ArgumentScaleType = GetArgumentScaleType(info);
            s.ValueDataMembers.AddRange(info.FieldName);
            s.ValueScaleType = ScaleType.Numerical;
            s.ShowInLegend = true;
            SideBySideBarSeriesView view = new SideBySideBarSeriesView();
            s.View = view;
            view.EqualBarWidth = true;
            view.Color = info.Color;
            view.BarWidth = info.GraphWidth == 1 ? view.BarWidth : (int)(info.GraphWidth * DpiProvider.Default.DpiScaleFactor);
            view.FillStyle.FillMode = FillMode.Solid;
            view.Border.Visibility = DefaultBoolean.False;
            view.AggregateFunction = SeriesAggregateFunction.Maximum;
            object dataSource = GetDataSource(info);
            IResizeableArray array = dataSource as IResizeableArray;
            if(array == null || array.Count < BigDataCount) {
                s.DataSource = dataSource;
            }
            else {
                s.Points.AddRange(CreateSeriesPoints(info));
            }
            return s;
        }

        protected SeriesPoint[] CreateSeriesPoints(StrategyDataItemInfo info) {
            object dataSource = GetDataSource(info);
            IResizeableArray array = dataSource as IResizeableArray;

            SeriesPoint[] points = new SeriesPoint[array.Count];
            PropertyInfo ap = array.GetItem(0).GetType().GetProperty(info.GetArgumentDataMember(), BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo vp = array.GetItem(0).GetType().GetProperty(info.FieldName, BindingFlags.Public | BindingFlags.Instance);

            Func<object, object> af = MakeAccessor(array.GetItem(0).GetType(), ap.PropertyType, info.GetArgumentDataMember());
            Func<object, object> vf = MakeAccessor(array.GetItem(0).GetType(), vp.PropertyType, info.FieldName);

            if(ap.PropertyType == typeof(DateTime)) {
                for(int i = 0; i < array.Count; i++) {
                    object item = array.GetItem(i);
                    points[i] = new SeriesPoint((DateTime)af(item), (double)vf(item));
                }
            }
            else if(ap.PropertyType == typeof(double)) {
                for(int i = 0; i < array.Count; i++) {
                    object item = array.GetItem(i);
                    points[i] = new SeriesPoint((double)af(item), (double)vf(item));
                }
            }
            return points;
        }

        private Series CreateLineSeries(StrategyDataItemInfo info) {
            Series s = new Series();
            s.Name = info.Name;
            s.ArgumentDataMember = GetArgumentDataMember(info);
            s.ArgumentScaleType = GetArgumentScaleType(info);
            s.ValueDataMembers.AddRange(info.FieldName);
            s.ValueScaleType = ScaleType.Numerical;
            s.ShowInLegend = true;
            LineSeriesView view = info.ChartType == ChartType.StepLine? new StepLineSeriesView(): new LineSeriesView();
            view.LineStyle.LineJoin = System.Drawing.Drawing2D.LineJoin.Bevel;
            view.Color = info.Color;
            view.LineStyle.Thickness = (int)(info.GraphWidth * DpiProvider.Default.DpiScaleFactor);
            view.AggregateFunction = SeriesAggregateFunction.Average;
            s.View = view;
            object dataSource = GetDataSource(info);
            IResizeableArray array = dataSource as IResizeableArray;
            if(array == null || array.Count < BigDataCount) {
                s.DataSource = dataSource;
            }
            else {
                s.Points.AddRange(CreateSeriesPoints(info));
            }
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
            view.ReductionOptions.FillMode = CandleStickFillMode.AlwaysFilled;
            view.Color = DXSkinColors.ForeColors.Information;
            view.ReductionOptions.Color = DXSkinColors.ForeColors.Critical;

            view.ReductionOptions.Level = StockLevel.Open;
            view.ReductionOptions.Visible = true;
            view.AggregateFunction = SeriesAggregateFunction.Financial;
            view.LineThickness = (int)(1 * DpiProvider.Default.DpiScaleFactor);
            view.LevelLineLength = 0.25;
            
            s.View = view;
            s.CrosshairLabelPattern = "O={OV}\nH={HV}\nL={LV}\nC={CV}";

            object dataSource = GetDataSource(info);
            IResizeableArray array = dataSource as IResizeableArray;
            if(array == null || array.Count < BigDataCount) {
                s.DataSource = dataSource;
            }
            else {
                SeriesPoint[] points = new SeriesPoint[array.Count];
                PropertyInfo ap = array.GetItem(0).GetType().GetProperty(s.ArgumentDataMember, BindingFlags.Public | BindingFlags.Instance);
                PropertyInfo lp = array.GetItem(0).GetType().GetProperty("Low", BindingFlags.Public | BindingFlags.Instance);
                PropertyInfo hp = array.GetItem(0).GetType().GetProperty("High", BindingFlags.Public | BindingFlags.Instance);
                PropertyInfo op = array.GetItem(0).GetType().GetProperty("Open", BindingFlags.Public | BindingFlags.Instance);
                PropertyInfo cp = array.GetItem(0).GetType().GetProperty("Close", BindingFlags.Public | BindingFlags.Instance);

                Func<object, object> af = MakeAccessor(array.GetItem(0).GetType(), ap.PropertyType, s.ArgumentDataMember);
                Func<object, object> lf = MakeAccessor(array.GetItem(0).GetType(), lp.PropertyType, "Low");
                Func<object, object> hf = MakeAccessor(array.GetItem(0).GetType(), lp.PropertyType, "High");
                Func<object, object> of = MakeAccessor(array.GetItem(0).GetType(), lp.PropertyType, "Open");
                Func<object, object> cf = MakeAccessor(array.GetItem(0).GetType(), lp.PropertyType, "Close");

                if(ap.PropertyType == typeof(DateTime)) {
                    for(int i = 0; i < array.Count; i++) {
                        object item = array.GetItem(i);
                        points[i] = new SeriesPoint((DateTime)af(item), 
                            new double[] {
                                (double)lf(item),
                                (double)hf(item),
                                (double)of(item),
                                (double)cf(item)
                            });
                    }
                }
                s.Points.AddRange(points);
            }

            //s.DataSource = GetDataSource(info);
            return s;
        }
        
        private void CheckInitializeTimeAxis() {
            if(Chart.Series[0].ArgumentScaleType != ScaleType.DateTime) {
                ((XYDiagram)Chart.Diagram).AxisX.Label.TextPattern = "{A}";
                if(Chart.Series[0].Points.Count > 0) {
                    ((XYDiagram)Chart.Diagram).AxisX.WholeRange.SetMinMaxValues(
                        Chart.Series[0].Points.Min(i => i.ArgumentX.NumericalArgument),
                        Chart.Series[0].Points.Max(i => i.ArgumentX.NumericalArgument)
                        );
                }
                return;
            }

            int totalMinutes = Visual.MeasureUnitMultiplier;
            DateTime first = DateTime.MinValue;
            DateTime last = DateTime.MaxValue;
            
            //((XYDiagram)Chart.Diagram).AxisX.DateTimeScaleOptions.MeasureUnit = (DateTimeMeasureUnit)Enum.Parse(typeof(DateTimeMeasureUnit), Visual.MeasureUnit.ToString());

            ((XYDiagram)Chart.Diagram).AxisX.DateTimeScaleOptions.ScaleMode = ScaleMode.Automatic;
            if(Visual.Items != null && Visual.Items.Count > 1) {
                object data1 = Visual.Items[1];
                object data0 = Visual.Items[0];
                object dataLast = Visual.Items.Last();
                PropertyInfo pi = data1.GetType().GetProperty("Time", BindingFlags.Public | BindingFlags.Instance);
                if(pi != null) {
                    DateTime time1 = (DateTime)pi.GetValue(data1);
                    DateTime time0 = (DateTime)pi.GetValue(data0);
                    first = time1;
                    last = (DateTime)pi.GetValue(dataLast);
                    totalMinutes = (int)((time1 - time0).TotalMinutes);
                }
            }
            DateTimeMeasureUnit unit = (DateTimeMeasureUnit)Enum.Parse(typeof(DateTimeMeasureUnit), Visual.MeasureUnit.ToString());
            int ms = 0;
            if(unit == DateTimeMeasureUnit.Minute)
                ms = 60000;
            else if(unit == DateTimeMeasureUnit.Second)
                ms = 1000;
            else
                throw new NotImplementedException();
            ((XYDiagram)Chart.Diagram).ZoomingOptions.AxisXMaxZoomPercent = 100 * (last - first).TotalMilliseconds / (Visual.MeasureUnitMultiplier * ms);

            //StrategyDataItemInfo di = Visual.DataItemInfos.FirstOrDefault(i => i.Type == DataType.DateTime && i.FieldName == "Time");
            //if(di != null) {
            //    if(di.UseCustomTimeUnit) {
            //        ((XYDiagram)Chart.Diagram).AxisX.DateTimeScaleOptions.MeasureUnit = (DateTimeMeasureUnit)Enum.Parse(typeof(DateTimeMeasureUnit), di.TimeUnit.ToString());
            //        ((XYDiagram)Chart.Diagram).AxisX.DateTimeScaleOptions.MeasureUnitMultiplier = di.TimeUnitMeasureMultiplier;
            //    }
            //}

            //((XYDiagram)Chart.Diagram).AxisX.DateTimeScaleOptions.MeasureUnitMultiplier = totalMinutes;
            ((XYDiagram)Chart.Diagram).AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
            ((XYDiagram)Chart.Diagram).AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;

            ((XYDiagram)Chart.Diagram).AxisX.LabelVisibilityMode = AxisLabelVisibilityMode.AutoGeneratedAndCustom;
            if(first != DateTime.MinValue) {
                DateTime current = first.Date;
                while(current <= last) {
                    ConstantLine constantLine = new ConstantLine(current.ToShortDateString(), current) { Color = Color.FromArgb(0x40, Color.LightGray), ShowInLegend = false };
                    constantLine.Title.Visible = false;
                    ((XYDiagram)Chart.Diagram).AxisX.ConstantLines.Add(constantLine);
                    ((XYDiagram)Chart.Diagram).AxisX.CustomLabels.Add(new CustomAxisLabel(current.ToShortDateString(), current));
                    current = current.AddDays(1);
                }
            }

            StrategyDataItemInfo time = Visual.DataItemInfos.FirstOrDefault(i => i.Name == "Time");
            string datePattern = "{A:dd.MM hh:mm:ss}";
            if(time != null && !string.IsNullOrEmpty(time.LabelPattern))
                datePattern = "{A:" + time.LabelPattern + "}";

            if(Chart.Series[0].ArgumentScaleType == ScaleType.Numerical)
                ((XYDiagram)Chart.Diagram).AxisX.Label.TextPattern = "{A}";
            else
                ((XYDiagram)Chart.Diagram).AxisX.Label.TextPattern = datePattern;
        }

        private XYDiagramPaneBase CheckAddPanel(StrategyDataItemInfo info) {
            XYDiagram diagram = (XYDiagram)Chart.Diagram;
            if(diagram == null)
                return null;
            diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
            if(info.PanelName == "Default")
                return diagram.DefaultPane;
            XYDiagramPane pane = null;
            if(diagram.Panes[info.PanelName] != null)
                pane = diagram.Panes[info.PanelName];
            IResizeableArray items = GetDataSource(info) as IResizeableArray;
            if(items != null && !(GetArgumentValue(info, items.GetItem(0)) is DateTime)) {
                if(info.PanelName == "Default") {
                    
                }
                else {
                    SecondaryAxisX axisX = new SecondaryAxisX();
                    axisX.Name = info.AxisXName;
                    diagram.SecondaryAxesX.Add(axisX);
                }
            }

            if(pane == null || info.Reversed) {
                SecondaryAxisY axis = new SecondaryAxisY();
                axis.Assign(diagram.AxisY);
                axis.Name = info.AxisYName; 
                axis.Reverse = info.Reversed;
                diagram.SecondaryAxesY.Add(axis);
            }
            if(pane == null) {
                pane = new XYDiagramPane() { Name = info.PanelName };
                diagram.Panes.Add(pane);
                Legend l = new Legend();
                l.Assign(Chart.Legend); l.Name = info.PanelName;
                l.DockTarget = pane;
                Chart.Legends.Add(l);
            }
            if(!info.PanelVisible)
                pane.Visibility = ChartElementVisibility.Hidden;
            return pane;
        }

        private object GetArgumentValue(StrategyDataItemInfo info, object v) {
            if(v == null)
                return null;
            string name = info.ArgumentDataMember == null ? "Time" : info.ArgumentDataMember;
            PropertyInfo pi = v.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            if(pi == null)
                return null;
            return pi.GetValue(v);
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

        private TreeListFormatRule CreateRule(TreeListColumn column, bool value, Color foreColor, Color backColor) {
            TreeListFormatRule rule = new TreeListFormatRule();
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

        private void InitializeTreeList() {
            if(TreeList == null)
                return;
            int index = 0;
            IResizeableArray data = GetItems(Visual);
            object first = data != null && data.Count > 0 ? data.GetItem(0) : null;
            for(int i = 0; i < Visual.DataItemInfos.Count; i++) {
                StrategyDataItemInfo info = Visual.DataItemInfos[i];
                TreeListColumn column = new TreeListColumn();
                column.Tag = info;
                bool isVisible = info.Visibility.HasFlag(DataVisibility.Table);
                column.Visible = isVisible;
                if(isVisible)
                    column.VisibleIndex = index;
                RepositoryItem editor = CreateEditor(first, info);
                if(editor != null) {
                    TreeList.RepositoryItems.Add(editor);
                    column.ColumnEdit = editor;
                }
                column.FieldName = info.FieldName;
                column.Format.FormatType = GetFormatType(info.Type);
                column.Format.FormatString = info.FormatString;
                TreeList.Columns.Add(column);
                if(isVisible)
                    index++;
            }
            for(int i = 0; i < Visual.DataItemInfos.Count; i++) {
                StrategyDataItemInfo info = Visual.DataItemInfos[i];
                if(!string.IsNullOrEmpty(info.AnnotationText)) {
                    TreeList.FormatRules.Add(CreateRule(TreeList.Columns[info.FieldName], true, info.Color, Color.FromArgb(0x20, info.Color)));
                }
            }
            //lock(Visual.Items) {
                //List<object> data = new List<object>();
                //data.AddRange(Visual.Items);
                //TreeList.DataSource = data;
                TreeList.DataSource = data;
            //}
            TreeList.OptionsScrollAnnotations.ShowCustomAnnotations = DefaultBoolean.True;
            TreeList.CustomScrollAnnotation += OnCustomScrollAnnotations;
        }

        private IResizeableArray GetItems(IStrategyDataItemInfoOwner visual) {
            if(visual.Items != null)
                return visual.Items;
            foreach(var item in visual.DataItemInfos) {
                if(item.DataSource is IResizeableArray)
                    return (IResizeableArray)item.DataSource;
            }
            return null;
        }

        private void InitializeTable() {
            if(Grid == null)
                return;
            int index = 0;
            IResizeableArray data = GetItems(Visual);
            object first = data != null && data.Count > 0 ? data.GetItem(0) : null;
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
            //lock(data) {
                //List<object> data = new List<object>();
                //data.AddRange(Visual.Items);
                Grid.DataSource = data;
            //}
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
            if(pi.PropertyType == typeof(bool) || value is bool) {
                RepositoryItemCheckEdit res = new RepositoryItemCheckEdit();
                res.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.SvgStar2;
                return res;
            }
            double[] doubleData = null;
            if(pi.PropertyType == typeof(ResizeableArray<TimeBaseValue>) || value is ResizeableArray<TimeBaseValue>) {
                if(value == null)
                    doubleData = new double[0];
                else {
                    ResizeableArray<TimeBaseValue> data = (ResizeableArray<TimeBaseValue>)value;
                    doubleData = new double[Math.Min(data.Count, 50)];
                    for(int i = 0; i < doubleData.Length; i++)
                        doubleData[i] = data[i].Value;
                }
            }
            if(value is double[] || pi.PropertyType == typeof(double[])) {
                if(value == null)
                    doubleData = new double[0];
                else 
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

        private void OnCustomScrollAnnotations(object sender, TreeListCustomScrollAnnotationsEventArgs e) {
            ResizeableArray<object> items = Visual.Items;
            e.Annotations = new List<TreeListScrollAnnotationInfo>();
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
                    TreeListScrollAnnotationInfo info = new TreeListScrollAnnotationInfo();
                    info.Node = TreeList.GetNodeByVisibleIndex(index);
                    info.Color = aList[j].Color;
                    e.Annotations.Add(info);
                }
                index++;
            }
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

    public delegate void DataControlProvideEventHandler(object sender, DataControlProvideEventArgs e);
        
    public class DataControlProvideEventArgs : EventArgs {
        public StrategyDataItemInfo DataItem { get; set; }
        public ChartControl Chart { get; set; }
        public GridControl Grid { get; set; }
    }
}
