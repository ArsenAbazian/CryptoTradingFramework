using DevExpress.Utils.Serializing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
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
                if (Ticker == value)
                    return;
                ticker = value;
                OnTickerChanged();
            }
        }
        void OnTickerChanged() {
            TickerName = Ticker.Name;
        }
        [XtraSerializableProperty]
        public DateTime StartDate { get; set; }
        [XtraSerializableProperty]
        public bool ShowOnChart { get; set; }
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
            if (PropertyChangedCore != null)
                PropertyChangedCore.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        bool blockTotalSpend = false;
        double buyPrice;
        [XtraSerializableProperty]
        public double BuyPrice {
            get { return buyPrice; }
            set {
                if (BuyPrice == value)
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
                if (Amount == value)
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
                if (TotalSpendInBaseCurrency == value)
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

        [XtraSerializableProperty]
        public double TakeProfitStartPercent { get; set; } = 20;
        [XtraSerializableProperty]
        public double TakeProfitPercent { get; set; } = 5;

        public double ActualProfit { get { return ActualPrice - BuyPrice; } }
        public double ActualProfitUSD { get { return UsdTicker == null ? 0 : ActualProfit * UsdTicker.Last; } }
        [XtraSerializableProperty]
        public double ActualPrice { get; set; }
        [XtraSerializableProperty]
        public double MaxPrice { get; set; }
        public double SellPrice { get { return GetSellPrice(); } }
        public double TakeProfitStartPrice { get { return BuyPrice * (100 + TakeProfitStartPercent) * 0.01; } }
        [XtraSerializableProperty]
        public bool IgnoreStopLoss { get; set; } = false;

        public string Name {
            get {
                if (Ticker == null)
                    return string.Empty;
                return Ticker.HostName + " - " + Ticker.Name;
            }
        }

        [XtraSerializableProperty]
        public TrailingType Type { get; set; } = TrailingType.Sell;
        [XtraSerializableProperty]
        public TrailingState State { get; set; } = TrailingState.Analyze;
        [XtraSerializableProperty]
        public bool EnableIncrementalStopLoss { get; set; } = false;
        public string IndicatorText { get { return "Trailing indicator"; } }

        public void Update() {
            if (State == TrailingState.Analyze && State == TrailingState.TakeProfit)
                Analyze();
        }
        void Analyze() {
            if (Type == TrailingType.Sell) {
                ActualPrice = Ticker.HighestBid;
                MaxPrice = Math.Max(MaxPrice, ActualPrice);

                if (ActualPrice < SellPrice) {
                    OnExecuteSell();
                    return;
                } else if (ActualPrice >= TakeProfitStartPrice) {
                    OnStartTakeProdit();
                    return;
                }
            }
        }

        double GetSellPrice() {
            if (State == TrailingState.TakeProfit)
                return MaxPrice * (100 - TakeProfitPercent) * 0.01;
            else {
                if (IgnoreStopLoss)
                    return -1;

                double basePrice = EnableIncrementalStopLoss ? MaxPrice : BuyPrice;
                return basePrice * (100 - StopLossPricePercent) * 0.01;
            }
        }

        void OnExecuteSell() {
            if (Mode == ActionMode.Notify) {
                TelegramBot.Default.SendNotification(Ticker.Exchange + " - " + Ticker.Name + " - Sell!!");
                State = TrailingState.Done;
            } else if (Mode == ActionMode.Execute) {
                if (Ticker.MarketSell(Amount))
                    State = TrailingState.Done;
                else
                    TelegramBot.Default.SendNotification($"{Ticker.Exchange}. Error!! Can't sell {Ticker.Name}");
            }

            Ticker.Events.Add(new TickerEvent() { Time = DateTime.UtcNow, Text = "Stoploss!" });

        }
        void OnStartTakeProdit() {
            State = TrailingState.TakeProfit;
            if (Mode == ActionMode.Notify)
                TelegramBot.Default.SendNotification(Ticker.Exchange + " - " + Ticker.Name + " - Start TAKEPROFIT!!");
            Ticker.Events.Add(new TickerEvent() { Time = DateTime.UtcNow, Text = "Takeprofit!" });
        }

        public void Start() {
            StartDate = DateTime.UtcNow;
            Ticker.Events.Add(new TickerEvent() {
                Text = string.Format("Trailing started! bought {0:0.########} at price {1:0.########}", Amount, BuyPrice),
                Time = Ticker.Time,
                Current = Ticker.Last
            });
        }
        public void Change() {
            Ticker.Events.Add(new TickerEvent() {
                Text = string.Format("Trailing changed! New values: amount {0:0.########}", Amount),
                Time = Ticker.Time,
                Current = Ticker.Last
            });
        }

    }

    public enum EditingMode { Add, Edit }
    public enum ActionMode { Execute, Notify }
    public enum TrailingType { Buy, Sell }
    public enum TrailingState { Analyze, TakeProfit, Done } 

    public class TickerEvent {
        public TickerEvent() {
            Color = Color.Pink;
        }

        [XtraSerializableProperty]
        public string Text { get; set; }
        [XtraSerializableProperty]
        public DateTime Time { get; set; }
        [XtraSerializableProperty]
        public double Current { get; set; }
        [XtraSerializableProperty]
        public Color Color { get; set; }
    }
}

