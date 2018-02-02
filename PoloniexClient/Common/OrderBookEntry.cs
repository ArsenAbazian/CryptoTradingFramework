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
                this.value = 0;
            }
        }
        public string amountString;
        public string AmountString {
            get { return amountString; }
            set {
                if(AmountString == value)
                    return;
                amountString = value;
                this.amount = 0;
            }
        }
        double value = 0, amount = 0;
        public double Value {
            get {
                if(value == 0) {
                    if(string.IsNullOrEmpty(ValueString))
                        return value;
                    value = FastDoubleConverter.Convert(ValueString);
                    ValueString = value.ToString("0.########");
                }
                return value;
            }
        }
        public double Amount {
            get {
                if(amount == 0) {
                    if(string.IsNullOrEmpty(AmountString))
                        return amount;
                    amount = FastDoubleConverter.Convert(AmountString);
                }
                return amount;
            }
        }
        public double Volume { get; set; }
        public double VolumePercent { get; set; }
        public void Clear() {
            this.value = 0;
            this.amount = 0;
        }
    }

    public static class FastDoubleConverter {
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
            0.00000000001
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
        static double ParseExponent(string str, double value, int startIndex) {
            int exponent = GetExponent(str, startIndex);
            if(exponent < 0)
                return value * divider[-exponent];
            return value * multiplier[exponent];
        }
        public static double Convert(string str) {
            int value = 0;
            int fix = 0;
            int length = str.Length;
            for(int i = 0; i < length; i++) {
                if(str[i] == '.') {
                    for(int j = i + 1; j < length; j++) {
                        if(str[j] == '-' || str[j] == 'e' || str[j] == 'E')
                            return ParseExponent(str, value + fix * divider[j - i - 1], j + 1);
                        fix = (fix << 3) + (fix << 1) + str[j] - 0x30;
                    }
                    return value + fix * divider[length - i - 1];
                }
                if(str[i] == '-' || str[i] == 'e' || str[i] == 'E')
                    return ParseExponent(str, value, i + 1);
                value = (value << 3) + (value << 1) + str[i] - 0x30;
            }
            return value;
        }
    }
}
