using CryptoMarketClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Indicators {
    public class SupportResistanceIndicator : WindowIndicator {
        public BindingList<SRValue> Support { get; } = new BindingList<SRValue>();
        public BindingList<SRValue> Resistance { get; } = new BindingList<SRValue>();

        public int Range { get; set; } = 3;
        public int ClasterizationRange { get; set; } = 24;
        public double ThresoldPerc { get; set; } = 2;
        public override TimeBaseValue Calculate(int forIndex) {
            if(forIndex < Length)
                return new SRValue() { Time = GetTime(forIndex), Value = double.NaN, Source = GetValueBySource(forIndex), Index = Result.Count };

            double[] low = GetLow(forIndex, Range);
            double[] high = GetHigh(forIndex, Range);

            int center = forIndex - 1 - Range;
            SRValue value = new SRValue() { Time = GetTime(center), Value = Ticker.CandleStickData[center].Open, Index = Result.Count };
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

            return value;
        }

        private bool UpdateResistance(SRValue value) {
            while(true) {
                if(Resistance.Count == 0)
                    return true;
                SRValue sr = (SRValue)Resistance.Last();
                if(!HasSameLevel(sr, value))
                    return true;
                SRValue sr2 = Resistance.Count >= 2 ? Resistance[Resistance.Count - 2]: null;
                if(sr2 != null && !HasSameLevel(sr, sr2))
                    return true;
                if(value.Value <= sr.Value) {
                    value.Type = SupResType.None;
                    sr.Power += 1.0;
                    return false;
                }
                sr.Type = SupResType.None;
                Resistance.RemoveAt(Resistance.Count - 1);
                value.Power += 1.0;
            }
        }

        protected bool HasSameLevel(SRValue sr1, SRValue sr2) {
            return Math.Abs(sr1.Value - sr2.Value) / sr1.Value < (ThresoldPerc * 0.01);
        }

        private bool UpdateSupport(SRValue value) {
            while(true) {
                if(Support.Count == 0)
                    return true;

                SRValue sr = (SRValue)Support.Last();
                if(!HasSameLevel(sr, value))
                    return true;
                SRValue sr2 = Support.Count >= 2 ? Support[Support.Count - 2] : null;
                if(sr2 != null && !HasSameLevel(sr, sr2))
                    return true;
                if(value.Value >= sr.Value) {
                    sr.Power += 1.0;
                    value.Type = SupResType.None;
                    return false;
                }
                sr.Type = SupResType.None;
                Support.RemoveAt(Support.Count - 1);
                value.Power += 1.0;
            }
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
    }

    public class SRValue : IndicatorValue {
        public int Index { get; set; }
        public SupResType Type { get; set; }
        public double Power { get; set; }
    }

    public enum SupResType {
        None,
        Support,
        Resistance
    }
}
