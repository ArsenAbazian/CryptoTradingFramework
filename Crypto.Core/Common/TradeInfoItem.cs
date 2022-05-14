using Crypto.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core {
    public enum TradeFillType { Fill, PartialFill }
    public enum TradeType { Buy, Sell }

    [Serializable]
    public class TradeInfoItem {
        
        public TradeInfoItem(AccountInfo account, Ticker ticker) {
            Account = account;
            Ticker = ticker;
        }
        public TradeInfoItem() { }

        [XmlIgnore]
        public AccountInfo Account { get; set; }
        [XmlIgnore]
        public Ticker Ticker { get; set; }

        DateTime? time;
        [XmlElement("Tm")]
        public DateTime Time {
            get {
                if(!time.HasValue)
                    time = Convert.ToDateTime(TimeString).ToLocalTime();
                return time.Value;
            }
            set {
                time = value;
                TimeString = time.Value.ToString("g");
            }
        }
        [XmlElement("Am")]
        public string AmountString { get; set; }
        [XmlElement("Rt")]
        public string RateString { get; set; }
        [XmlElement("Tt")]
        public string TotalString { get; set; }
        [XmlElement("Fe")]
        public string FeeString { get; set; }

        double rate = 0;
        [XmlIgnore]
        public double Rate {
            get {
                if(rate == 0) {
                    if(string.IsNullOrEmpty(RateString))
                        return rate;
                    rate = FastValueConverter.Convert(RateString);
                    RateString = rate.ToString("0.00000000");
                }
                return rate;
            }
        }
        double amount = 0;
        [XmlIgnore]
        public double Amount {
            get {
                if(amount == 0) {
                    if(string.IsNullOrEmpty(AmountString))
                        return amount;
                    amount = FastValueConverter.Convert(AmountString);
                }
                return amount;
            }
            set {
                amount = value;
            }
        }
        double total = double.NaN;
        [XmlIgnore]
        public double Total { 
            get { 
                if(double.IsNaN(total)) {
                    if(TotalString != null)
                        total = FastValueConverter.Convert(TotalString);
                    else 
                        total = Rate * Amount;
                }
                return total;
            }
            set {
                if(Total == value)
                    return;
                total = value;
                TotalString = total.ToString("0.00000000");
            }
        }
        double fee = double.NaN;
        public double Fee { 
            get { 
                if(double.IsNaN(fee)) {
                    if(FeeString != null)
                        fee = FastValueConverter.Convert(FeeString);
                    else 
                        fee = 0;
                }
                return fee;
            }    
        }
        [XmlElement("On")]
        public string OrderNumber { get; set; }
        [XmlElement("Fl")]
        public TradeFillType Fill { get; set;}
        [XmlElement("Tp")]
        public TradeType Type { get; set; }
        long id = -1;
        public long Id {
            get { 
                if(id == -1 && !string.IsNullOrEmpty(IdString))
                    id = FastValueConverter.ConvertPositiveLong(IdString);
                return id;
            }
        }
        [XmlElement("Id")]
        public string IdString { get; set; }
        [XmlElement("Gd")]
        public string GlobalId { get; set; }
        [XmlIgnore]
        public string TimeString { get; set; }
    }
}
