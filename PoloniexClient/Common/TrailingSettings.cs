using DevExpress.Utils.Serializing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public class TrailingSettings : INotifyPropertyChanged {
        public TrailingSettings(TickerBase ticker) {
            Enabled = true;
            Ticker = ticker;
        }

        TickerBase ticker;
        public TickerBase Ticker {
            get { return ticker; }
            private set {
                if(Ticker == value)
                    return;
                ticker = value;
                OnTickerChanged();
            }
        }
        void OnTickerChanged() {
            TickerName = Ticker.Name;
        }
        [XtraSerializableProperty]
        public string TickerName { get; set; }
        public TickerBase UsdTicker { get { return Ticker == null ? null : Ticker.UsdTicker; } }
        [XtraSerializableProperty]
        public bool Enabled { get; set; }
        [XtraSerializableProperty]
        public ActionMode Mode { get; set; } = ActionMode.Execute;
        [XtraSerializableProperty]
        public DateTime Date { get; set; }

        event PropertyChangedEventHandler PropertyChangedCore;
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged {
            add { PropertyChangedCore += value; }
            remove { PropertyChangedCore -= value; }
        }
        void RaisePropertyChanged(string propName) {
            if(PropertyChangedCore != null)
                PropertyChangedCore.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        bool blockTotalSpend = false;
        double buyPrice;
        [XtraSerializableProperty]
        public double BuyPrice {
            get { return buyPrice; }
            set {
                if(BuyPrice == value)
                    return;
                buyPrice = value;
                OnBuyPriceChanged();
            }
        }
        void OnBuyPriceChanged() {
            blockTotalSpend = true;
            TotalSpendInBaseCurrency = Amount * BuyPrice;
            blockTotalSpend = false;
            RaisePropertyChanged("BuyPrice");
        }
        double amount;
        [XtraSerializableProperty]
        public double Amount {
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
        double totalSpendInBaseCurrency;
        [XtraSerializableProperty]
        public double TotalSpendInBaseCurrency {
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

        [XtraSerializableProperty]
        public double StopLossPricePercent { get; set; } = 10;
        public double StopLossStartPrice { get { return BuyPrice * (100 - StopLossPricePercent) * 0.01; } }

        [XtraSerializableProperty]
        public double TakeProfitStartPercent { get; set; } = 10;
        [XtraSerializableProperty]
        public double TakeProfitPercent { get; set; } = 5;

        public double ActualProfit { get { return ActualPrice - BuyPrice; } }
        public double ActualProfitUSD { get { return UsdTicker == null? 0: ActualProfit * UsdTicker.Last; } }
        [XtraSerializableProperty]
        public double ActualPrice { get; set; }
        [XtraSerializableProperty]
        public double MaxPrice { get; set; }
        public double TakeProfitStartPrice { get { return BuyPrice * (100 + TakeProfitStartPercent) * 0.01; } }
        public string Name {
            get {
                if(Ticker == null)
                    return string.Empty;
                return Ticker.HostName + " - " + Ticker.Name;
            }
        }

        [XtraSerializableProperty]
        public TrailingType Type { get; set; } = TrailingType.Sell;
        [XtraSerializableProperty]
        public TrailingState State { get; set; } = TrailingState.Analyze;
        [XtraSerializableProperty]
        public bool EnableIncrementalStopLoss { get; set; }

        public void Update() {
            if(State == TrailingState.Analyze)
                Analyze();
            else if(State == TrailingState.StopLoss)
                OnExecuteStopLoss();
            else if(State == TrailingState.TakeProfit)
                OnExecuteTakeProfit();
        }
        void Analyze() {
            if(Type == TrailingType.Sell) {
                ActualPrice = Ticker.HighestBid;
                MaxPrice = Math.Max(MaxPrice, ActualPrice);

                if(ActualPrice < StopLossStartPrice) {
                    OnExecuteStopLoss();
                    return;
                }
                else if(ActualPrice >= TakeProfitStartPrice) {
                    OnStartTakeProdit();
                    return;
                }
                else if(State == TrailingState.TakeProfit && ActualPrice < (MaxPrice * (100 - TakeProfitPercent) * 0.01)) {
                    OnExecuteTakeProfit();
                    return;
                }
            }
        }
        void OnExecuteStopLoss() {
            State = TrailingState.StopLoss;
            if(Mode == ActionMode.Notify) {
                TelegramBot.Default.SendNotification(Ticker.Exchange + " - " + Ticker.Name + " - STOPLOSS!!");
                State = TrailingState.Done;
            }
        }
        void OnStartTakeProdit() {
            State = TrailingState.TakeProfit;
            if (Mode == ActionMode.Notify) 
                TelegramBot.Default.SendNotification(Ticker.Exchange + " - " + Ticker.Name + " - Start TAKEPROFIT!!");
        }
        void OnExecuteTakeProfit() {
            State = TrailingState.TakeProfit;
            if(Mode == ActionMode.Notify) {
                TelegramBot.Default.SendNotification(Ticker.Exchange + " - " + Ticker.Name + " - TAKEPROFIT!!");
                State = TrailingState.Done;
            }
        }
    }

    public enum EditingMode { Add, Edit }
    public enum ActionMode { Execute, Notify }
    public enum TrailingType { Buy, Sell }
    public enum TrailingState { Analyze, StopLoss, TakeProfit, Done } 
}
