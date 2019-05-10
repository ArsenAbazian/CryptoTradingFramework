using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Strategies {
    public class StrategyDataItemInfo {
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
        public bool HasAnnotationStringFormat { get; private set; }
        public string AnnotationAnchorField { get; set; }
        public DataVisibility Visibility { get; set; } = DataVisibility.Both;
        public bool UseCustomTimeUnit { get; set; } = false;
        public StrategyDateTimeMeasureUnit TimeUnit { get; set; }
        public int TimeUnitMeasureMultiplier { get; set; } = 1;
        string dataSourcePath;
        public string DataSourcePath {
            get { return dataSourcePath; }
            set {
                dataSourcePath = value;
                if(!string.IsNullOrEmpty(dataSourcePath))
                    Visibility = DataVisibility.Chart;
            }
        }
        public object DataSource { get; set; }
    }

    public enum ChartType { CandleStick, Line, Area, Bar, Dot, Annotation, StepLine }
    public enum DataType { DateTime, Numeric }
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
