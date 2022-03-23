﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core {
    public enum OrderBookEntryType { Bid, Ask }
    public class OrderBookEntry {
        protected string ToStringCore(double value) {
            return value.ToString("0.00000000");
        }
        public string valueString;
        public string ValueString {
            get {
                if(valueString == null)
                    valueString = ToStringCore(Value);
                return valueString;
            }
            set {
                if(this.valueString == value)
                    return;
                this.valueString = value;
                this.valueCalculated = false;
            }
        }
        public long Id { get; set; }
        public string amountString;
        public string AmountString {
            get {
                if(amountString == null)
                    amountString = amount.ToString("0.00000000");
                return amountString;
            }
            set {
                if(this.amountString == value)
                    return;
                amountString = value;
                this.amountCalculated = false;
            }
        }
        double valueCore = 0, amount = 0;
        bool valueCalculated, amountCalculated;
        public double Value {
            get {
                if(!valueCalculated && valueString != null) {
                    if(string.IsNullOrEmpty(ValueString))
                        return valueCore;
                    valueCore = FastValueConverter.Convert(ValueString);
                    valueCalculated = true;
                }
                return valueCore;
            }
            set {
                valueCore = value;
                valueString = null;
            }
        }
        public double Amount {
            get {
                if(!amountCalculated && amountString != null) {
                    if(string.IsNullOrEmpty(AmountString))
                        return amount;
                    amountCalculated = true;
                    amount = FastValueConverter.Convert(AmountString);
                }
                return amount;
            }
            set {
                amount = value;
                amountString = null;
                //AmountString = amount.ToString("0.00000000");
            }
        }

        public void Offset(double delta) {
            this.valueCore += delta;
            this.valueString = null;
        }

        string volumeString = null;
        double volume;
        public double Volume {
            get { return volume; }
            set
            {
                if (Volume == value)
                    return;
                volumeString = null;
                volume = value;
            }
        }
        public string VolumeString
        {
            get { 
                if(volumeString == null)
                    volumeString = ToStringCore(Volume);
                return volumeString;
            }
        }
        public double VolumeTotal { get; set; }
        public double VolumePercent { get; set; }
        public void Clear() {
            this.valueCore = 0;
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
        public static long ConvertLong(string str) {
            if(string.IsNullOrEmpty(str))
                return 0;
            int startIndex = 0;
            int sign = 1;
            if(str[0] == '-') {
                sign = -1;
                startIndex++;
            }
            return sign * ConvertPositiveLong(str, ref startIndex);
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

        public static long ConvertPositiveLong(byte[] str, ref int startIndex) {
            long value = 0;
            try {
                int length = str.Length;
                for(int i = startIndex; i < length; i++) {
                    byte c = str[i];
                    if(c == ',') {
                        startIndex = i;
                        break;
                    }
                    value = (value << 3) + (value << 1) + str[i] - 0x30;
                }
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
            }
            return value;
        }

        public static int ConvertInt(string str) {
            if(string.IsNullOrEmpty(str))
                return 0;
            int sign = 1;
            int startIndex = 0;
            if(str[0] == '-') {
                sign = -1;
                startIndex++;
            }
            int value = 0;
            try {
                int length = str.Length;
                for(int i = startIndex; i < length; i++) {
                    char c = str[i];
                    if(c < 0x30 || c > 0x39) {
                        startIndex = i;
                        break;
                    }
                    value = (value << 3) + (value << 1) + str[i] - 0x30;
                }
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
            }
            return value * sign;
        }

        public static double Convert(string str) {
            try {
                if(string.IsNullOrEmpty(str))
                    return 0;
                if(char.IsLetter(str[0]))
                    return 0;
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
                        return sign * (value + fix * divider[length - i - 1]);
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

        public static bool IsDigit(byte v) {
            return v >= '0' && v <= '9';
        }
    }
}
