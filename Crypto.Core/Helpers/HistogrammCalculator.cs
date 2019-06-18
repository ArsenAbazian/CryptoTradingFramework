using Crypto.Core.Indicators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Helpers {
    public static class HistogrammCalculator {
        public static ArgumentValue[] Calculate(object dataSource, string fieldName, double clasterizationWidth) {
            if(dataSource is double[])
                return Calculate((double[])dataSource, clasterizationWidth);
            if(dataSource is ResizeableArray<TimeBaseValue>)
                return Calculate((ResizeableArray<TimeBaseValue>)dataSource, clasterizationWidth);
            if(dataSource is ResizeableArray<object>)
                return Calculate((ResizeableArray<object>)dataSource, fieldName, clasterizationWidth);
            return null;
        }

        public static ArgumentValue[] Calculate(ResizeableArray<object> dataSource, string valueField, double clasterizationWidth) {
            double minX = GetMin(dataSource, valueField);
            double maxX = GetMax(dataSource, valueField);
            int count = 100;
            if(clasterizationWidth != 0)
                count = (int)((maxX - minX) / clasterizationWidth) + 1;
            return Calculate((ResizeableArray<object>)dataSource, valueField, count);
        }

        private static double GetMin(ResizeableArray<object> dataSource, string valueField) {
            if(dataSource.Count == 0)
                return 0;
            PropertyInfo pi = dataSource[0].GetType().GetProperty(valueField, BindingFlags.Instance | BindingFlags.Public);
            return dataSource.Min(i => (double)pi.GetValue(i));
        }

        private static double GetMax(ResizeableArray<object> dataSource, string valueField) {
            if(dataSource.Count == 0)
                return 0;
            PropertyInfo pi = dataSource[0].GetType().GetProperty(valueField, BindingFlags.Instance | BindingFlags.Public);
            return dataSource.Max(i => (double)pi.GetValue(i));
        }

        public static ArgumentValue[] Calculate(ResizeableArray<object> data, string valueField, int count) {
            if(data.Count == 0)
                return null;
            double minX = GetMin(data, valueField);
            double maxX = GetMax(data, valueField);
            double step = (maxX - minX) / count;

            minX = ((int)(minX / step)) * step;
            maxX = ((int)(maxX / step + 0.5)) * step;

            count = (int)((maxX - minX) / step);
            ArgumentValue[] res = new ArgumentValue[count + 1];
            for(int i = 0; i < count + 1; i++) {
                res[i] = new ArgumentValue() { X = minX + i * step };
            }
            PropertyInfo pi = data[0].GetType().GetProperty(valueField, BindingFlags.Instance | BindingFlags.Public);
            foreach(var item in data) {
                double val = (double)pi.GetValue(item);
                if(double.IsNaN(val))
                    continue;
                int index = (int)((val - minX) / step + 0.5);
                if(index >= res.Length)
                    continue;
                res[index].Y += 1.0;
            }
            double del = 100.0 / data.Count;
            for(int i = 0; i < count; i++) {
                res[i].Y *= del;
            }
            return res;
        }

        public static ArgumentValue[] Calculate(double[] data, double clasterizationWidth) {
            double minX = data.Min();
            double maxX = data.Max();
            int count = (int)((maxX - minX) / clasterizationWidth) + 1;
            return Calculate(data, count);
        }

        public static ArgumentValue[] Calculate(double[] data, int count) {
            double minX = data.Min();
            double maxX = data.Max();
            double step = (maxX - minX) / count;

            minX = ((int)(minX / step)) * step;
            maxX = ((int)(maxX / step + 0.5)) * step;

            count = (int)((maxX - minX) / step);
            ArgumentValue[] res = new ArgumentValue[count + 1];
            for(int i = 0; i < count + 1; i++) {
                res[i] = new ArgumentValue() { X = minX + i * step };
            }
            for(int i = 0; i < data.Length; i++) { 
                if(double.IsNaN(data[i]))
                    continue;
                int index = (int)((data[i] - minX) / step + 0.5);
                res[index].Y += 1.0;
            }
            double del = 100.0 / data.Length;
            for(int i = 0; i < count; i++) {
                res[i].Y *= del;
            }
            return res;
        }

        public static ArgumentValue[] Calculate(ResizeableArray<TimeBaseValue> data, double clasterizationWidth) {
            double minX = data.Min(i => i.Value);
            double maxX = data.Max(i => i.Value);
            int count = (int)((maxX - minX) / clasterizationWidth) + 1;
            return Calculate(data, count);
        }

        public static ArgumentValue[] Calculate(ResizeableArray<TimeBaseValue> data, int count) {
            double minX = data.Min(i => i.Value);
            double maxX = data.Max(i => i.Value);
            double step = (maxX - minX) / count;

            minX = ((int)(minX / step)) * step;
            maxX = ((int)(maxX / step + 0.5)) * step;

            count = (int)((maxX - minX) / step);
            ArgumentValue[] res = new ArgumentValue[count + 1];
            for(int i = 0; i < count + 1; i++) {
                res[i] = new ArgumentValue() { X = minX + i * step };
            }
            foreach(var item in data) {
                if(double.IsNaN(item.Value))
                    continue;
                int index = (int)((item.Value - minX) / step + 0.5);
                res[index].Y += 1.0;
            }
            double del = 100.0 / data.Count;
            for(int i = 0; i < count; i++) {
                res[i].Y *= del;
            }
            return res;
        }
    }

    public class ArgumentValue {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
