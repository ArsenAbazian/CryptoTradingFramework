using Crypto.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Common.OrderGrid {
    [Serializable]
    public class OrderGridItemInfo {
        public double Value { get; set; }
        public double AmountPercent { get; set; }

        public void Assign(OrderGridItemInfo item) {
            Value = item.Value;
            AmountPercent = item.AmountPercent;
        }
    }

    [Serializable]
    public class OrderGridInfo {
        public OrderGridItemInfo Start { get; set; } = new OrderGridItemInfo();
        public OrderGridItemInfo End { get; set; } = new OrderGridItemInfo();
        public ResizeableArray<bool> Executed { get; set; }
        
        public OrderGridInfo() {
            Executed = new ResizeableArray<bool>(1);
        }
        
        int lineCount = 1;
        public int LineCount {
            get { return lineCount; }
            set {
                if(value < 1)
                    value = 1;
                if(LineCount == value)
                    return;
                lineCount = value;
                OnLineCountChanged();
            }
        }

        protected virtual void OnLineCountChanged() {
            Executed = new ResizeableArray<bool>(LineCount);
        }

        public virtual int GetZoneIndex(double value) {
            bool inverted = Start.Value > End.Value;
            if(!inverted) {
                if(value > End.Value)
                    return LineCount - 1;
                if(value < Start.Value)
                    return -1;
            }
            else {
                if(value < End.Value)
                    return LineCount - 1;
                if(value > Start.Value)
                    return -1;
            }
            double delta = (End.Value - Start.Value) / (LineCount - 1);
            int index = (int)((value - Start.Value) / delta);
            return index;
        }

        public virtual double GetZoneArgument(int zoneIndex) {
            double delta = (End.Value - Start.Value) / (LineCount - 1);
            return Start.Value + delta * zoneIndex;
        }

        public virtual double GetAmountInPc(double value) {
            int index = GetZoneIndex(value);
            return Start.AmountPercent + (End.AmountPercent - Start.AmountPercent) / (LineCount - 1) * index;
        }

        public virtual double GetAmountInPc(int zoneIndex) {
            return Start.AmountPercent + (End.AmountPercent - Start.AmountPercent) / (LineCount - 1) * zoneIndex;
        }

        public virtual void Assign(OrderGridInfo info) {
            Start.Assign(info.Start);
            End.Assign(info.End);
            LineCount = info.LineCount;
        }

        public void Normalize() {
            double sum = 0;
            double delta = (End.AmountPercent - Start.AmountPercent) / (LineCount - 1);

            for(int i = 0; i < LineCount; i++) {
                double amountPc = Start.AmountPercent + i * delta;
                sum += amountPc;
            }
            Start.AmountPercent = Start.AmountPercent / sum * 100;
            End.AmountPercent = End.AmountPercent / sum * 100;
        }

        public bool IsExecuted(int zoneIndex) {
            if(zoneIndex == -1)
                return false;
            return Executed[zoneIndex];
        }

        [XmlIgnore]
        public GridLineInfo[] Lines {
            get {
                GridLineInfo[] res = new GridLineInfo[LineCount];
                for(int i = 0; i < LineCount; i++) {
                    res[i] = new GridLineInfo() { Value = GetZoneArgument(i), Percent = GetAmountInPc(i), Executed = IsExecuted(i) };
                }
                return res;
            }
        }
    }

    [Serializable]
    public class GridLineInfo {
        public double Value { get; set; }
        public double Amount { get; set; }
        public double Percent { get; set; }
        public bool Executed { get; set; }

        public override string ToString() {
            return Value.ToString("0.########") + " " + Percent.ToString("0.0") + "% " + (Executed ? "Activated" : "");
        }
    }
}
