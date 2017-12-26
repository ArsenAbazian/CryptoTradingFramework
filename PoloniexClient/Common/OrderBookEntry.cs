using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public enum OrderBookEntryType { Bid, Ask }
    public class OrderBookEntry {
        public string ValueString { get; set; }
        public string AmountString { get; set; }
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
        static double[] divider = new double[] { 1,
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
        public static double Convert(string str) {
            int value = 0;
            int fix = 0;
            int i = 0;
            for(i = 0; i < str.Length; i++) {
                if(str[i] == '.') {
                    int count = str.Length - i - 1;
                    for(int j = i + 1; j < str.Length; j++) {
                        int vv = str[j] - 0x30;
                        fix = (fix << 3) + (fix << 1) + vv;
                    }
                    return value + fix * divider[count];
                }
                if(str[i] == 'e' || str[i] == 'E') {
                    return double.Parse(str);
                }
                int v = str[i] - 0x30;
                value = (value << 3) + (value << 1) + v;
            }
            return value;
        }
    }
}
