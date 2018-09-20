using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public enum OrderBookEntryType { Bid, Ask }
    public class OrderBookEntry {
        public string valueString;
        public string ValueString {
            get { return valueString; }
            set {
                if(ValueString == value)
                    return;
                this.valueString = value;
                this.valueCalculated = false;
            }
        }
        public long Id { get; set; }
        public string amountString;
        public string AmountString {
            get { return amountString; }
            set {
                if(AmountString == value)
                    return;
                amountString = value;
                this.amountCalculated = false;
            }
        }
        double value = 0, amount = 0;
        bool valueCalculated, amountCalculated;
        public double Value {
            get {
                if(!valueCalculated) {
                    if(string.IsNullOrEmpty(ValueString))
                        return value;
                    value = FastValueConverter.Convert(ValueString);
                    valueCalculated = true;
                }
                return value;
            }
        }
        public double Amount {
            get {
                if(!amountCalculated) {
                    if(string.IsNullOrEmpty(AmountString))
                        return amount;
                    amountCalculated = true;
                    amount = FastValueConverter.Convert(AmountString);
                }
                return amount;
            }
            set {
                amount = value;
                AmountString = amount.ToString("0.########");
            }
        }
        public double Volume { get; set; }
        public double VolumeTotal { get; set; }
        public double VolumePercent { get; set; }
        public void Clear() {
            this.value = 0;
            this.amount = 0;
        }
    }

    public static class FastValueConverter {
        static double[] divider = new double[] {
            1,
            0.1,
            0.01,
            0.001,
            0.0001,
            0.00001,
            0.000001,
            0.0000001,
            0.00000001,
            0.000000001,
            0.0000000001,
            0.00000000001,
            0.000000000001,
            0.0000000000001,
            0.00000000000001,
            0.000000000000001,
            0.0000000000000001,
            0.00000000000000001,
            0.000000000000000001,
            0.0000000000000000001,
            0.00000000000000000001,
            0.000000000000000000001,
            0.0000000000000000000001
        };
        static double[] multiplier = new double[] {
            1,
            10,
            100,
            1000,
            10000,
            100000,
            1000000,
            10000000,
            100000000,
            1000000000
        };
        static int GetExponent(string str, int startIndex) {
            int exponent = 0;
            if(str[startIndex] == '-') {
                startIndex++;
                for(int i = startIndex; i < str.Length; i++)
                    exponent = (exponent << 3) + (exponent << 1) + str[i] - 0x30;
                return -exponent;
            }
            for(int i = startIndex; i < str.Length; i++)
                exponent = (exponent << 3) + (exponent << 1) + str[i] - 0x30;
            return exponent;
        }

        static int GetExponent(char[] str, int startIndex, int endIndex) {
            int exponent = 0;
            if(str[startIndex] == '-') {
                startIndex++;
                for(int i = startIndex; i < endIndex; i++)
                    exponent = (exponent << 3) + (exponent << 1) + str[i] - 0x30;
                return -exponent;
            }
            for(int i = startIndex; i < str.Length; i++)
                exponent = (exponent << 3) + (exponent << 1) + str[i] - 0x30;
            return exponent;
        }

        static double ParseExponent(string str, double value, int startIndex) {
            int exponent = GetExponent(str, startIndex);
            if(exponent < 0)
                return value * divider[-exponent];
            return value * multiplier[exponent];
        }

        static double ParseExponent(char[] str, double value, int startIndex, int end) {
            int exponent = GetExponent(str, startIndex, end);
            if(exponent < 0)
                return value * divider[-exponent];
            return value * multiplier[exponent];
        }

        public static int ConvertPositiveInteger(string str) {
            int start = 0;
            return ConvertPositiveInteger(str, ref start);
        }
        public static int ConvertPositiveInteger(string str, ref int startIndex) {
            int value = 0;
            try {
                int length = str.Length;
                for(int i = startIndex; i < length; i++) {
                    char c = str[i];
                    if(c == ',') {
                        startIndex = i;
                        break;
                    }
                    value = (value << 3) + (value << 1) + str[i] - 0x30;
                }
            }
            catch(Exception e) {
                Telemetry.Default.TrackEvent("convert int exception", new string[] { "value", str }, true);
                Telemetry.Default.TrackException(e, new string[,] { { "value", str } });
            }
            return value;
        }

        public static long ConvertPositiveLong(string str) {
            int stIndex = 0;
            return ConvertPositiveLong(str, ref stIndex);
        }
        public static long ConvertPositiveLong(string str, ref int startIndex) {
            long value = 0;
            try {
                int length = str.Length;
                for(int i = startIndex; i < length; i++) {
                    char c = str[i];
                    if(c == ',') {
                        startIndex = i;
                        break;
                    }
                    value = (value << 3) + (value << 1) + str[i] - 0x30;
                }
            }
            catch(Exception e) {
                Telemetry.Default.TrackEvent("convert int exception", new string[] { "value", str }, true);
                Telemetry.Default.TrackException(e, new string[,] { { "value", str } });
            }
            return value;
        }

        public static double Convert(string str) {
            try {
                if(string.IsNullOrEmpty(str))
                    return 0.0;
                long value = 0;
                long fix = 0;
                int length = str.Length;
                int startIndex = 0;
                int sign = 1;
                if(str[0] == '-') {
                    startIndex = 1;
                    sign = -1;
                }
                for(int i = startIndex; i < length; i++) {
                    char c = str[i];
                    if(c == '.' || c == ',') {
                        for(int j = i + 1; j < length; j++) {
                            if(str[j] == '-' || str[j] == 'e' || str[j] == 'E')
                                return ParseExponent(str, sign * value + fix * divider[j - i - 1], j + 1);
                            fix = (fix << 3) + (fix << 1) + str[j] - 0x30;
                        }
                        return sign * value + fix * divider[length - i - 1];
                    }
                    if(str[i] == '-' || str[i] == 'e' || str[i] == 'E')
                        return sign * ParseExponent(str, value, i + 1);
                    value = (value << 3) + (value << 1) + str[i] - 0x30;
                }
                if(value < 0) {
                    Telemetry.Default.TrackEvent("convert double exception", new string[] { "value", str, "converted", value.ToString() }, true);
                }
                return sign * value;
            }
            catch(Exception e) {
                Telemetry.Default.TrackEvent("convert double exception", new string[] { "value", str }, true);
                Telemetry.Default.TrackException(e, new string[,] { { "value", str } });
                return System.Convert.ToDouble(str);
            }
        }
        public static double Convert(char[] str, int start, int end) {
            try {
                int value = 0;
                int fix = 0;
                int length = end;
                int startIndex = start;
                int sign = 1;
                if(str[start] == '-') {
                    startIndex = start + 1;
                    sign = -1;
                }
                for(int i = startIndex; i < length; i++) {
                    char c = str[i];
                    if(c == '.' || c == ',') {
                        for(int j = i + 1; j < length; j++) {
                            if(str[j] == '-' || str[j] == 'e' || str[j] == 'E')
                                return ParseExponent(str, sign * value + fix * divider[j - i - 1], j + 1, end);
                            fix = (fix << 3) + (fix << 1) + str[j] - 0x30;
                        }
                        return sign * value + fix * divider[length - i - 1];
                    }
                    if(str[i] == '-' || str[i] == 'e' || str[i] == 'E')
                        return sign * ParseExponent(str, value, i + 1, end);
                    value = (value << 3) + (value << 1) + str[i] - 0x30;
                }
                if(value < 0) {
                    Telemetry.Default.TrackEvent("convert double exception", new string[] { "value", str.ToString(), "converted", value.ToString() }, true);
                }
                return sign * value;
            }
            catch(Exception e) {
                Telemetry.Default.TrackEvent("convert double exception", new string[] { "value", str.ToString() }, true);
                Telemetry.Default.TrackException(e, new string[,] { { "value", str.ToString() } });
                return System.Convert.ToDouble(str);
            }
        }
    }
}
