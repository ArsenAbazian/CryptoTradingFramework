using Crypto.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Strategies {
    public interface IStrategyDataItemInfoOwner {
        string Name { get; }
        List<StrategyDataItemInfo> DataItemInfos { get; }
        ResizeableArray<object> Items { get; }
    }

    public class SdiConstantLine {
        public string FieldName { get; set; }
        public string Title { get; set; }
        public object Value { get; set; }
    }

    public class StrategyDataItemInfo : IStrategyDataItemInfoOwner {
        #region static helper methods
        public static StrategyDataItemInfo TimeItem(List<StrategyDataItemInfo> dataItemInfos, string fieldName) {
            dataItemInfos.Add( new StrategyDataItemInfo() { FieldName = fieldName, Visibility = DataVisibility.Table, Type = DataType.DateTime, FormatString = "dd.MM.yyyy hh:mm" });
            return dataItemInfos.Last();
        }

        public static StrategyDataItemInfo TimeSpanItem(List<StrategyDataItemInfo> dataItemInfos, string fieldName) {
            dataItemInfos.Add(new StrategyDataItemInfo() { FieldName = fieldName, Visibility = DataVisibility.Table, Type = DataType.DateTime });
            return dataItemInfos.Last();
        }

        public static StrategyDataItemInfo DataItem(List<StrategyDataItemInfo> dataItemInfos, string fieldName) {
            dataItemInfos.Add(new StrategyDataItemInfo() { FieldName = fieldName });
            return dataItemInfos.Last();
        }

        public static StrategyDataItemInfo DataItem(List<StrategyDataItemInfo> dataItemInfos, string fieldName, string formatString) {
            dataItemInfos.Add(new StrategyDataItemInfo() { FieldName = fieldName, FormatString = formatString });
            return dataItemInfos.Last();
        }

        public static StrategyDataItemInfo EnumItem(List<StrategyDataItemInfo> dataItemInfos, string fieldName) {
            dataItemInfos.Add(new StrategyDataItemInfo() { FieldName = fieldName, Visibility = DataVisibility.Table });
            return dataItemInfos.Last();
        }

        public static StrategyDataItemInfo DataItem(List<StrategyDataItemInfo> dataItemInfos, string fieldName, string formatString, Color color) {
            dataItemInfos.Add(new StrategyDataItemInfo() { FieldName = fieldName, FormatString = formatString, Color = color });
            return dataItemInfos.Last();
        }

        public static StrategyDataItemInfo DataItem(List<StrategyDataItemInfo> dataItemInfos, string fieldName, Color color) {
            dataItemInfos.Add(new StrategyDataItemInfo() { FieldName = fieldName, Color = color });
            return dataItemInfos.Last();
        }

        public static StrategyDataItemInfo DataItem(List<StrategyDataItemInfo> dataItemInfos, string fieldName, Color color, int width) {
            dataItemInfos.Add(new StrategyDataItemInfo() { FieldName = fieldName, Color = color, GraphWidth = width });
            return dataItemInfos.Last();
        }

        public static StrategyDataItemInfo HistogrammItem(List<StrategyDataItemInfo> dataItemInfos, double clasterizationWidth, string fieldName, Color color) {
            dataItemInfos.Add(new StrategyDataItemInfo() { FieldName = fieldName, Color = color, Type = DataType.HistogrammData, OwnChart = true, Visibility = DataVisibility.Chart, ClasterizationWidth = clasterizationWidth });
            StrategyDataItemInfo info = dataItemInfos.Last();
            return info;
        }
        #endregion

        public StrategyDataItemInfo DetailInfo { get; set; }

        [XmlIgnore]
        public List<SdiConstantLine> XLines { get; } = new List<SdiConstantLine>();
        [XmlIgnore]
        public List<SdiConstantLine> YLines { get; } = new List<SdiConstantLine>();

        public bool OwnChart { get; set; }
        public string FieldName { get; set; }
        public DataType Type { get; set; } = DataType.Numeric;
        public string FormatString { get; set; } = string.Empty;
        public Color Color { get; set; } = Color.Blue;
        public int GraphWidth { get; set; } = 1;
        public ChartType ChartType { get; set; } = ChartType.Line;
        public double ClasterizationWidth { get; set; }
        public string PanelName { get; set; } = "Default";
        string name;
        public string Name { get { return string.IsNullOrEmpty(name) ? FieldName : name; } set { name = value; } }
        string annotationText;
        public string AnnotationText {
            get { return annotationText; }
            set {
                annotationText = value;
                HasAnnotationStringFormat = !string.IsNullOrEmpty(value) && annotationText.Contains('{');
            }
        }
        public ArgumentScaleType ArgumentScaleType { get; set; } = ArgumentScaleType.DateTime;
        public string ArgumentDataMember { get; set; }
        public bool HasAnnotationStringFormat { get; private set; }
        public string AnnotationAnchorField { get; set; }
        public DataVisibility Visibility { get; set; } = DataVisibility.Both;
        public bool UseCustomTimeUnit { get; set; } = false;
        public StrategyDateTimeMeasureUnit TimeUnit { get; set; }
        public int TimeUnitMeasureMultiplier { get; set; } = 1;
        [XmlIgnore]
        public object BindingRoot { get; set; }
        string dataSourcePath;
        public string BindingSource {
            get { return dataSourcePath; }
            set {
                dataSourcePath = value;
                if(!string.IsNullOrEmpty(dataSourcePath))
                    Visibility = DataVisibility.Chart;
            }
        }
        public object DataSource { get; set; }

        public List<StrategyDataItemInfo> Children { get; set; } = new List<StrategyDataItemInfo>();
        List<StrategyDataItemInfo> list;
        List<StrategyDataItemInfo> IStrategyDataItemInfoOwner.DataItemInfos {
            get {
                if(list == null) {
                    list = new List<StrategyDataItemInfo>();
                    list.Add(this);
                    list.AddRange(Children);
                }
                return list;
            }
        }

        public object Value { get; set; }
        public ResizeableArray<object> Items { get; set; }
        public bool IsChartData { get { return Type == DataType.ChartData || Type == DataType.HistogrammData; } }
    }

    public enum ChartType { CandleStick, Line, Area, Bar, Dot, Annotation, StepLine, ConstantX, ConstantY }
    public enum DataType { DateTime, Numeric, ChartData, HistogrammData }
    [Flags]
    public enum DataVisibility { None, Table, Chart, Both = Table | Chart }

    public enum StrategyDateTimeMeasureUnit {
        Millisecond = 0,
        Second = 1,
        Minute = 2,
        Hour = 3,
        Day = 4,
        Week = 5,
        Month = 6,
        Quarter = 7,
        Year = 8
    }

    public enum ArgumentScaleType {
        Numerical = 1,
        DateTime = 2
    }
}
