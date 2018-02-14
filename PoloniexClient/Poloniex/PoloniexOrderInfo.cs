using CryptoMarketClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public class OpenedOrderInfo {
        public DateTime Date { get; set; }
        public string Market { get; set; }
        public int OrderNumber { get; set; }
        public OrderType Type { get; set; }
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

        public string totalString;
        public string TotalString {
            get { return totalString; }
            set {
                if(TotalString == value)
                    return;
                totalString = value;
                this.totalCalculated = false;
            }
        }

        double value = 0, amount = 0, total;
        bool valueCalculated, amountCalculated, totalCalculated;
        public double Value {
            get {
                if(!valueCalculated) {
                    if(string.IsNullOrEmpty(ValueString))
                        return value;
                    value = FastDoubleConverter.Convert(ValueString);
                    valueCalculated = true;
                    ValueString = value.ToString("0.########");
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
                    amount = FastDoubleConverter.Convert(AmountString);
                }
                return amount;
            }
        }
        public double Total {
            get {
                if(!totalCalculated) {
                    if(string.IsNullOrEmpty(TotalString))
                        return total;
                    totalCalculated = true;
                    total = FastDoubleConverter.Convert(TotalString);
                }
                return total;
            }
        }
    }
}
