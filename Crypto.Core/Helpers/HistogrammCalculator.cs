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
        public static ResizeableArray<TimeBaseValue> CalculateByTime<T>(ResizeableArray<T> array, string timeField, TimeSpan interval) {
            if(array.Count == 0)
                return new ResizeableArray<TimeBaseValue>();
            DateTime min = GetMinTime(array, timeField);
            DateTime max = GetMaxTime(array, timeField);
            int count = (int)((max - min).TotalMilliseconds / interval.TotalMilliseconds + 0.5);
            ResizeableArray<TimeBaseValue> list = new ResizeableArray<TimeBaseValue>(count + 1);
            PropertyInfo pi = array[0].GetType().GetProperty(timeField, BindingFlags.Instance | BindingFlags.Public);
            double k = 1.0 / interval.TotalMilliseconds;
            for(int i = 0; i < count + 1; i++) {
                list[i] = new TimeBaseValue() { Time = min.AddMilliseconds((int)i * interval.TotalMilliseconds)};
            }
            
            for(int i = 0; i < array.Count; i++) {
                DateTime time = (DateTime)pi.GetValue(array[i]);
                int index = (int)((time - min).TotalMilliseconds * k + 0.5);
                list[index].Value++;
            }
            return list;
        }

        public static double[] ToDouble(ResizeableArray<TimeBaseValue> list) {
            return list.Select((i) => i.Value).ToArray();
        }

        private static DateTime GetMaxTime<T>(ResizeableArray<T> dataSource, string field) {
            if(dataSource.Count == 0)
                return DateTime.MinValue;
            PropertyInfo pi = dataSource[0].GetType().GetProperty(field, BindingFlags.Instance | BindingFlags.Public);
            return dataSource.Max(i => (DateTime)pi.GetValue(i));
        }

        private static DateTime GetMinTime<T>(ResizeableArray<T> dataSource, string field) {
            if(dataSource.Count == 0)
                return DateTime.MinValue;
            PropertyInfo pi = dataSource[0].GetType().GetProperty(field, BindingFlags.Instance | BindingFlags.Public);
            return dataSource.Min(i => (DateTime)pi.GetValue(i));
        }

        public static ResizeableArray<ArgumentValue> Calculate(object dataSource, string fieldName, double clasterizationWidth) {
            if(dataSource is double[])
                return Calculate((double[])dataSource, clasterizationWidth);
            if(dataSource is ResizeableArray<TimeBaseValue>)
                return Calculate((ResizeableArray<TimeBaseValue>)dataSource, clasterizationWidth);
            if(dataSource is ResizeableArray<object>)
                return Calculate((ResizeableArray<object>)dataSource, fieldName, clasterizationWidth);
            return null;
        }

        public static ResizeableArray<ArgumentValue> Calculate(ResizeableArray<object> dataSource, string valueField, double clasterizationWidth) {
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
            return dataSource.Min(i => {
                double val = (double)pi.GetValue(i);
                if(double.IsNaN(val))
                    return double.MaxValue;
                return val;
            });
        }
        private static double GetMin(ResizeableArray<TimeBaseValue> dataSource) {
            if(dataSource.Count == 0)
                return 0;
            double min = double.NaN;
            for(int i = 0; i < dataSource.Count; i++) {
                if(double.IsNaN(min) || min > dataSource[i].Value)
                    min = dataSource[i].Value;
            }
            return min;
        }
        private static double GetMax(ResizeableArray<TimeBaseValue> dataSource) {
            if(dataSource.Count == 0)
                return 0;
            double min = double.NaN;
            for(int i = 0; i < dataSource.Count; i++) {
                if(double.IsNaN(min) || min < dataSource[i].Value)
                    min = dataSource[i].Value;
            }
            return min;
        }

        private static double GetMax(ResizeableArray<object> dataSource, string valueField) {
            if(dataSource.Count == 0)
                return 0;
            PropertyInfo pi = dataSource[0].GetType().GetProperty(valueField, BindingFlags.Instance | BindingFlags.Public);
            return dataSource.Max(i => {
                double val = (double)pi.GetValue(i);
                if(double.IsNaN(val))
                    return double.MinValue;
                return val;
                });
        }

        public static ResizeableArray<ArgumentValue> Calculate(ResizeableArray<object> data, string valueField, int count) {
            if(data.Count == 0)
                return null;
            double minX = GetMin(data, valueField);
            double maxX = GetMax(data, valueField);
            double step = (maxX - minX) / count;

            minX = ((int)(minX / step)) * step;
            maxX = ((int)(maxX / step + 0.5)) * step;

            count = (int)((maxX - minX) / step);
            ResizeableArray<ArgumentValue> res = new ResizeableArray<ArgumentValue>(count + 1);
            for(int i = 0; i < count + 1; i++) {
                res[i] = new ArgumentValue() { X = minX + i * step };
            }
            PropertyInfo pi = data[0].GetType().GetProperty(valueField, BindingFlags.Instance | BindingFlags.Public);
            foreach(var item in data) {
                double val = (double)pi.GetValue(item);
                if(double.IsNaN(val))
                    continue;
                int index = (int)((val - minX) / step + 0.5);
                if(index >= res.Count)
                    continue;
                res[index].Y += 1.0;
            }
            double del = 100.0 / data.Count;
            for(int i = 0; i < count; i++) {
                res[i].Y *= del;
            }
            return res;
        }

        public static ResizeableArray<ArgumentValue> Calculate(double[] data, double clasterizationWidth) {
            double minX = data.Min();
            double maxX = data.Max();
            int count = (int)((maxX - minX) / clasterizationWidth) + 1;
            return Calculate(data, count);
        }

        public static ResizeableArray<ArgumentValue> Calculate(double[] data, int count) {
            double minX = data.Min();
            double maxX = data.Max();
            double step = (maxX - minX) / count;

            minX = ((int)(minX / step)) * step;
            maxX = ((int)(maxX / step + 0.5)) * step;

            count = (int)((maxX - minX) / step);
            ResizeableArray<ArgumentValue> res = new ResizeableArray<ArgumentValue>(count + 1);
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

        public static ResizeableArray<ArgumentValue> Calculate(ResizeableArray<TimeBaseValue> data, double clasterizationWidth) {
            double minX = GetMin(data);
            double maxX = GetMax(data);
            int count = (int)((maxX - minX) / clasterizationWidth) + 1;
            return Calculate(data, count);
        }

        public static ResizeableArray<ArgumentValue> Calculate(ResizeableArray<TimeBaseValue> data, int count) {
            double minX = GetMin(data);
            double maxX = GetMax(data);
            if(double.IsNaN(minX) || double.IsNaN(maxX))
                return new ResizeableArray<ArgumentValue>();
            double step = (maxX - minX) / count;

            minX = ((int)(minX / step)) * step;
            maxX = ((int)(maxX / step + 0.5)) * step;

            count = (int)((maxX - minX) / step);
            ResizeableArray<ArgumentValue> res = new ResizeableArray<ArgumentValue>(count + 1);
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
