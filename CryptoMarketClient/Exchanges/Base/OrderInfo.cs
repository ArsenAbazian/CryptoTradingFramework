using CryptoMarketClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public class OpenedOrderInfo {
        public OpenedOrderInfo(AccountInfo account, Ticker ticker) {
            Account = account;
            Ticker = ticker;
        }
        public AccountInfo Account { get; set; }
        public Ticker Ticker { get; set; }
        DateTime? date;
        public DateTime Date {
            get {
                if(date == null)
                    date = Convert.ToDateTime(DateString);
                return date.HasValue ? date.Value : DateTime.MinValue;
            }
            set {
                date = value;
                DateString = date.Value.ToString("g");
            }
        }
        string dateString;
        public string DateString {
            get { return dateString; }
            set {
                dateString = value;
                this.date = null;
            }
        }
        public string Market { get { return Ticker.MarketName; } }
        public string OrderId { get; set; }
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
                    value = FastValueConverter.Convert(ValueString);
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
                    amount = FastValueConverter.Convert(AmountString);
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
                    total = FastValueConverter.Convert(TotalString);
                }
                return total;
            }
        }

        public DateTime OrderDate {
            get { return GetOrderDate(); }
        }

        protected virtual DateTime GetOrderDate() {
            return Date;
        }
    }
}
