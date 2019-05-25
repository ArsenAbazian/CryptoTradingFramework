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

    public class StrategyDataItemInfo : IStrategyDataItemInfoOwner {
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

        public StrategyDataItemInfo DetailInfo { get; set; }

        public string FieldName { get; set; }
        public DataType Type { get; set; } = DataType.Numeric;
        public string FormatString { get; set; } = string.Empty;
        public Color Color { get; set; } = Color.Blue;
        public int GraphWidth { get; set; } = 1;
        public ChartType ChartType { get; set; } = ChartType.Line;
        public int PanelIndex { get; set; } = 0;
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

        List<StrategyDataItemInfo> list;
        List<StrategyDataItemInfo> IStrategyDataItemInfoOwner.DataItemInfos {
            get {
                if(list == null) {
                    list = new List<StrategyDataItemInfo>();
                    list.Add(this);
                }
                return list;
            }
        }

        public object Value { get; set; }
        public ResizeableArray<object> Items { get; set; }
    }

    public enum ChartType { CandleStick, Line, Area, Bar, Dot, Annotation, StepLine }
    public enum DataType { DateTime, Numeric, ChartData }
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
}
