using System;
using System.ComponentModel;
using System.Linq;

namespace CryptoMarketClient.Common {
    public abstract class BalanceBase : INotifyPropertyChanged {
        double available;
        protected BalanceBase(string ticker) {
            CurrencyTicker = ticker;
        }

        public abstract string Exchange { get; }
        public string CurrencyTicker { get; set; }
        public string CurrencyName { get; set; }
        public double Available {
            get { return this.available; }
            set {
                if (this.available == value)
                    return;
                this.available = value;
                RaisePropertyChanged(nameof(Available));
            }
        }
        public double LastAvailable { get; set; }
        public double OnOrders { get; set; }
        public double BtcValue { get; set; }
        public string DepositAddress { get; set; }
        public double DepositChanged {
            get {
                double max = Math.Max(Available, LastAvailable);
                if (max == 0)
                    return 0;
                double delta = Math.Abs(Available - LastAvailable);
                return (delta / max);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
