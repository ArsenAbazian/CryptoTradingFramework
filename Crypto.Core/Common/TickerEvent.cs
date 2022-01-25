using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Common {
    public enum TickerEventType { Default, }

    [Serializable]
    [XmlInclude(typeof(XmlColor))]
    public class TickerEvent {
        public TickerEvent() {
            Color = Color.Pink;
        }

        public TickerEventType Type { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public double Current { get; set; }
        [XmlIgnore]
        public Color Color { get; set; }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string ColorString { 
            get { return XmlColor.FromColor(Color); }
            set { Color = XmlColor.ToColor(value); }
        }

        public void Assign(TickerEvent ev) {
            Type = ev.Type;
            Text = ev.Text;
            Time = ev.Time;
            Current = ev.Current;
            Color = ev.Color;
        }

        public TickerEvent Clone() {
            return (TickerEvent)MemberwiseClone();
        }
    }

    public class XmlColor {
        private Color color_ = Color.Black;

        public XmlColor() { }
        public XmlColor(Color c) { color_ = c; }

        public static implicit operator Color(XmlColor x) {
            return x.color_;
        }

        public static implicit operator XmlColor(Color c) {
            return new XmlColor(c);
        }

        public static string FromColor(Color color) {
            if(color.IsNamedColor)
                return color.Name;

            int colorValue = color.ToArgb();

            if(((uint)colorValue >> 24) == 0xFF)
                return String.Format("#{0:X6}", colorValue & 0x00FFFFFF);
            else
                return String.Format("#{0:X8}", colorValue);
        }

        public static Color ToColor(string value) {
            try {
                if(value[0] == '#') {
                    return Color.FromArgb((value.Length <= 7 ? unchecked((int)0xFF000000) : 0) +
                        Int32.Parse(value.Substring(1), System.Globalization.NumberStyles.HexNumber));
                }
                else {
                    return Color.FromName(value);
                }
            }
            catch(Exception) {
            }

            return Color.Black;
        }

        [XmlText]
        public string Default {
            get { return FromColor(color_); }
            set { color_ = ToColor(value); }
        }
    }
}
