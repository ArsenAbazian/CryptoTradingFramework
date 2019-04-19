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
        public string AnnotationText { get; set; }
        public string AnnotationAnchorField { get; set; }
        public DataVisibility Visibility { get; set; } = DataVisibility.Both;
        public bool UseCustomTimeUnit { get; set; } = false;
        public StrategyDateTimeMeasureUnit TimeUnit { get; set; }
        public int TimeUnitMeasureMultiplier { get; set; } = 1;
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
