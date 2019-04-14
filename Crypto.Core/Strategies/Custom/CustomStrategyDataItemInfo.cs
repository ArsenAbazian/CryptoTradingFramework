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
        public int CandleStickMinutesInterval { get; set; } = 30;
        public DataVisibility Visibility { get; set; } = DataVisibility.Both;
    }

    public enum ChartType { CandleStick, Line, Area, Bar, Dot, Annotation }
    public enum DataType { DateTime, Numeric }
    [Flags]
    public enum DataVisibility { None, Table, Chart, Both = Table | Chart }
}
