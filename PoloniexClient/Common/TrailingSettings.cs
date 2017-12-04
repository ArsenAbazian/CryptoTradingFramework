using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public class TrailingSettings : INotifyPropertyChanged {
        public TrailingSettings() {
            Enabled = true;
        }

        public TickerBase Ticker { get; set; }
        public bool Enabled { get; set; }

        decimal buyPrice;
        public decimal BuyPrice {
            get { return buyPrice; }
            set {
                if(BuyPrice == value)
                    return;
                buyPrice = value;
                OnBuyPriceChanged();
            }
        }
        bool blockTotalSpend = false;
        event PropertyChangedEventHandler PropertyChangedCore;
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged {
            add { PropertyChangedCore += value; }
            remove { PropertyChangedCore -= value; }
        }

        void RaisePropertyChanged(string propName) {
            if(PropertyChangedCore != null)
                PropertyChangedCore.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        void OnBuyPriceChanged() {
            blockTotalSpend = true;
            TotalSpendInBaseCurrency = Amount * BuyPrice;
            blockTotalSpend = false;
            RaisePropertyChanged("BuyPrice");
        }

        public DateTime Date { get; set; }

        decimal amount;
        public decimal Amount {
            get { return amount; }
            set {
                if(Amount == value)
                    return;
                amount = value;
                OnAmountChanged();
            }
        }
        void OnAmountChanged() {
            blockTotalSpend = true;
            TotalSpendInBaseCurrency = Amount * BuyPrice;
            blockTotalSpend = false;
            RaisePropertyChanged("Amount");
        }

        public decimal StopLossSellPrice { get; set; }

        decimal totalSpendInBaseCurrency;
        public decimal TotalSpendInBaseCurrency {
            get { return totalSpendInBaseCurrency; }
            set {
                if(TotalSpendInBaseCurrency == value)
                    return;
                totalSpendInBaseCurrency = value;
                OnTotalSpendInBaseCurrencyChanged();
            }
        }
        void OnTotalSpendInBaseCurrencyChanged() {
            Amount = TotalSpendInBaseCurrency / BuyPrice;
            RaisePropertyChanged("TotalSpendInBaseCurrency");
        }

        public decimal ActualProfit { get { return ActualPrice - BuyPrice; } }
        public decimal ActualProfitUSD { get { return ActualProfit * UsdTicker.Last; } }

        public TickerBase UsdTicker { get; set; }

        public decimal StopLossPricePercent { get; set; } = 10m;
        public decimal StopLossStartPrice { get { return BuyPrice * (100 - StopLossPricePercent) * 0.01m; } }

        public decimal TakeProfitPercent { get; set; } = 10m;

        public decimal ActualPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal PriceDelta {
            get { return MaxPrice * TakeProfitPercent * 0.01m; }
        }
        public decimal TakeProfitPrice {
            get { return MaxPrice - PriceDelta; }
        }
        public string Name {
            get {
                if(Ticker == null)
                    return string.Empty;
                return Ticker.HostName + " - " + Ticker.Name;
            }
        }

        public bool EnableIncrementalStopLoss { get; set; }
    }
}
