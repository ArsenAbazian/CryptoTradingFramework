using Crypto.Core.Helpers;
using Crypto.Core.Strategies;
using Crypto.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Indicators {
    public class SupportResistanceIndicator : WindowIndicator {
        [XmlIgnore]
        public ResizeableArray<SRValue> Support { get; } = new ResizeableArray<SRValue>();
        [XmlIgnore]
        public ResizeableArray<SRValue> Resistance { get; } = new ResizeableArray<SRValue>();

        [InputParameter]
        public int Range { get; set; } = 3;
        [InputParameter]
        public double ThresoldPerc { get; set; } = 0.6;

        public override void Assign(IndicatorBase ind) {
            base.Assign(ind);
            SupportResistanceIndicator sr = ind as SupportResistanceIndicator;
            if(sr != null) {
                Range = sr.Range;
                ThresoldPerc = sr.ThresoldPerc;
            }
        }

        public override void OnUpdateValue(int index) {
            //throw new NotImplementedException();
        }
        public override TimeBaseValue Calculate(int forIndex) {
            if(forIndex < Length)
                return new SRValue() { Time = GetTime(forIndex), Value = double.NaN, Source = GetValueBySource(forIndex), Index = Result.Count };

            double[] close = GetClose(forIndex, Range);
            double[] low = close, high = close;

            int center = forIndex - 1 - Range;
            DateTime tm = GetTime(center);
            SRValue value = new SRValue() { Time = tm, Index = Result.Count, Power = 1 };
            if(IsMin(low[Range], low)) { // support
                value.Type = SupResType.Support;
                value.Value = low[Range];
                if(UpdateSupport(value))
                    Support.Add(value);
            }
            else if(IsMax(high[Range], high)) { // resistance
                value.Type = SupResType.Resistance;
                value.Value = high[Range];
                if(UpdateResistance(value))
                    Resistance.Add(value);
            }

            SRValue lastRes = GetLastResistance();
            if(lastRes != null && lastRes != value)
                lastRes.Length++;

            SRValue lastSup = GetLastSupport();
            if(lastSup != null && lastSup != value)
                lastSup.Length++;

            return value;
        }

        private SRValue GetLastResistance() {
            return Resistance.Count > 0 ? Resistance.Last() : null;
        }

        private SRValue GetLastSupport() {
            return Support.Count > 0 ? Support.Last() : null;
        }

        public double GetLastMinSupport(int countFromLast) {
            double value = double.MaxValue;
            for(int i = 0; i < countFromLast; i++) {
                int index = Support.Count - 1 - i;
                if(index < 0)
                    break;
                value = Math.Min(value, Support[index].Value);
            }
            return value;
        }

        public bool HasSameLevel(SRValue lastItem, SRValue newItem) {
            return Math.Abs(newItem.Value - lastItem.Average) / lastItem.Average < (ThresoldPerc * 0.01);
        }

        public bool BelongsSameResistanceLevel(SRValue res) {
            return Resistance.Last() == res;
            //for(int i = Resistance.Count - 1; i >= 0; i --)
        }

        public bool BelongsSameSupportLevel(SRValue res) {
            return Support.Last() == res;
            //for(int i = Resistance.Count - 1; i >= 0; i --)
        }

        public bool HasSameResistanceLevel(double value) {
            if(Resistance.Count == 0)
                return false;
            SRValue lastItem = Resistance.Last();
            return Math.Abs(value - lastItem.Average) / lastItem.Average < (ThresoldPerc * 0.01);
        }

        private bool UpdateResistance(SRValue value) {
            value.Average = value.Value;
            if(Resistance.Count == 0)
                return true;

            SRValue sr = (SRValue)Resistance.Last();
            value.ChangePc = (value.Value - sr.Value) / sr.Value * 100;
            if(!HasSameLevel(sr, value))
                return true;

            sr.Average = (sr.Average * sr.Power + value.Value) / (sr.Power + 1);
            sr.Power++;
            sr.Value = Math.Max(sr.Value, value.Value);
            return false;

            //while(true) {
            //    if(Resistance.Count == 0)
            //        return true;
            //    SRValue sr = (SRValue)Resistance.Last();
            //    if(!HasSameLevel(sr, value))
            //        return true;
            //    SRValue sr2 = Resistance.Count >= 2 ? Resistance[Resistance.Count - 2]: null;
            //    if(sr2 != null && !HasSameLevel(sr, sr2)) {
            //        sr.Power += 1.0;
            //        value.Power = sr.Power;
            //        value.Value = Math.Max(sr.Value, value.Value);
            //        return true;
            //    }
            //    //if(value.Value <= sr.Value) {
            //    //    value.Type = SupResType.None;
            //    //    sr.Power += value.Power;
            //    //    sr.Value = Math.Max(sr.Value, value.Value);
            //    //    return false;
            //    //}
            //    sr.Type = SupResType.None;
            //    Resistance.RemoveAt(Resistance.Count - 1);
            //    value.Power += sr.Power;
            //    value.Value = Math.Max(value.Value, sr.Value);
            //}
        }

        public double GetSupportBelow(double value) {
            for(int i = Support.Count - 1; i >= 0; i--) {
                if(Support[i].Value < value)
                    return Support[i].Value;
            }
            return value;
        }
        public override void Clear() {
            base.Clear();
            Support.Clear();
            Resistance.Clear();
        }
        private bool UpdateSupport(SRValue value) {
            value.Average = value.Value;
            if(Support.Count == 0)
                return true;

            SRValue sr = (SRValue)Support.Last();
            value.ChangePc = (value.Value - sr.Value) / sr.Value * 100;
            if(!HasSameLevel(sr, value))
                return true;

            sr.Average = (sr.Average * sr.Power + value.Value) / (sr.Power + 1);
            sr.Power++;
            sr.Value = Math.Min(sr.Value, value.Value);
            return false;

            //SRValue sr2 = Support.Count >= 2 ? Support[Support.Count - 2] : null;
            //if(sr2 != null && !HasSameLevel(sr, sr2)) {
            //    sr.Power++;
            //    value.Power = sr.Power;
            //    value.Value = Math.Min(sr.Value, value.Value);
            //    return true;
            //}

            //sr.Type = SupResType.None;
            //Support.RemoveAt(Support.Count - 1);
            //value.Power += sr.Power;
            //value.Value = Math.Min(sr.Value, value.Value);
        }

        private double[] GetHigh(int forIndex, int range) {
            int center = forIndex - 1 - range;
            int left = center - range;
            double[] res = new double[range * 2 + 1];
            for(int i = left; i < forIndex; i++) {
                res[i - left] = Ticker.CandleStickData[i].High;
            }
            return res;
        }

        private double[] GetLow(int forIndex, int range) {
            int center = forIndex - 1 - range;
            int left = center - range;
            double[] res = new double[range * 2 + 1];
            for(int i = left; i < forIndex; i++) {
                res[i - left] = Ticker.CandleStickData[i].Low;
            }
            return res;
        }

        private double[] GetClose(int forIndex, int range) {
            int center = forIndex - 1 - range;
            int left = center - range;
            double[] res = new double[range * 2 + 1];
            for(int i = left; i < forIndex; i++) {
                res[i - left] = Ticker.CandleStickData[i].Close;
            }
            return res;
        }

        private bool GoUp(double[] list) {
            for(int i = 0; i < list.Length - 1; i++) {
                if(list[i] > list[i + 1])
                    return false;
            }
            return true;
        }

        private bool GoDown(double[] list) {
            for(int i = 0; i < list.Length - 1; i++) {
                if(list[i] < list[i + 1])
                    return false;
            }
            return true;
        }

        private bool IsMin(double value, double[] list) {
            for(int i = 0; i < list.Length; i++) {
                if(value > list[i])
                    return false;
            }
            return true;
        }

        private bool IsMax(double value, double[] list) {
            for(int i = 0; i < list.Length; i++) {
                if(value < list[i])
                    return false;
            }
            return true;
        }

        public SRValue FindSupportByTime(DateTime candlestickTime) {
            for(int i = Support.Count - 1; i >= 0; i--) {
                if(Support[i].Time <= candlestickTime)
                    return Support[i];
            }
            return null;
        }
    }

    public class SRValue : IndicatorValue {
        public int Index { get; set; }
        public SupResType Type { get; set; }
        public int Power { get; set; }
        public double Average { get; set; }
        public double ChangePc { get; set; }
        public int Length { get; set; }
    }

    public enum SupResType {
        None,
        Support,
        Resistance
    }
}
