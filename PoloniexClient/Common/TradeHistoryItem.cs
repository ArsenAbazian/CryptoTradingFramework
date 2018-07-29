using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public enum TradeFillType { Fill, PartialFill }
    public enum TradeType { Buy, Sell }

    public class TradeHistoryItem {
        DateTime? time;
        public DateTime Time {
            get {
                if(!time.HasValue)
                    time = Convert.ToDateTime(TimeString);
                return time.Value;
            }
            set {
                time = value;
            }
        }
        public string AmountString { get; set; }
        public string RateString { get; set; }
        double rate = 0;
        public double Rate {
            get {
                if(rate == 0) {
                    if(string.IsNullOrEmpty(RateString))
                        return rate;
                    rate = FastValueConverter.Convert(RateString);
                    RateString = rate.ToString("0.########");
                }
                return rate;
            }
        }
        double amount = 0;
        public double Amount {
            get {
                if(amount == 0) {
                    if(string.IsNullOrEmpty(AmountString))
                        return amount;
                    amount = FastValueConverter.Convert(AmountString);
                }
                return amount;
            }
        }
        public double Total { get; set; }
        public double Fee { get; set; }
        public long OrderNumber { get; set; }
        public TradeFillType Fill { get; set;}
        public TradeType Type { get; set; }
        public long Id { get; set; }
        public string IdString { get; set; }
        public long GlobalId { get; set; }
        public string TimeString { get; set; }
    }
}
