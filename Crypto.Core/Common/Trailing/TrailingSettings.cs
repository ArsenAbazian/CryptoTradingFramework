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
using System.Xml.Serialization;
using CryptoMarketClient.Helpers;

namespace CryptoMarketClient.Common {
    [Serializable]
    public class TrailingSettings : INotifyPropertyChanged {
        public TrailingSettings(Ticker ticker) {
            Enabled = true;
            Ticker = ticker;
        }

        Ticker ticker;
        [XmlIgnore]
        public Ticker Ticker {
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
        public DateTime StartDate { get; set; }
        public bool ShowOnChart { get; set; }
        public string TickerName { get; set; }
        [XmlIgnore]
        public Ticker UsdTicker { get { return Ticker == null ? null : Ticker.UsdTicker; } }
        public bool Enabled { get; set; }
        public ActionMode Mode { get; set; } = ActionMode.Notify;
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

        double buyPrice;
        public double TradePrice {
            get { return buyPrice; }
            set {
                if (TradePrice == value)
                    return;
                buyPrice = value;
                OnBuyPriceChanged();
            }
        }
        void OnBuyPriceChanged() {
            TotalSpendInBaseCurrency = Amount * TradePrice;
            RaisePropertyChanged("TradePrice");
        }
        double amount;
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
            TotalSpendInBaseCurrency = Amount * TradePrice;
            RaisePropertyChanged("Amount");
        }
        double totalSpendInBaseCurrency;
        public double TotalSpendInBaseCurrency {
            get { return totalSpendInBaseCurrency; }
            set {
                if (TotalSpendInBaseCurrency == value)
                    return;
                totalSpendInBaseCurrency = value;
                OnTotalSpendInBaseCurrencyChanged();
            }
        }
        bool InTotalSpendInBaseCurrencyChanged { get; set; }
        void OnTotalSpendInBaseCurrencyChanged() {
            if(InTotalSpendInBaseCurrencyChanged)
                return;
            InTotalSpendInBaseCurrencyChanged = true;
            Amount = TotalSpendInBaseCurrency / TradePrice;
            InTotalSpendInBaseCurrencyChanged = false;
            RaisePropertyChanged("TotalSpendInBaseCurrency");
        }

        public double StopLossPricePercent { get; set; } = 10;

        public double TakeProfitStartPercent { get; set; } = 20;
        public double TakeProfitPercent { get; set; } = 5;

        public double ActualProfit { get { return ActualPrice - TradePrice; } }
        public double ActualProfitUSD { get { return UsdTicker == null ? 0 : ActualProfit * UsdTicker.Last; } }
        public double ActualPrice { get; set; }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
        public double OrderPrice { get { return GetOrderPrice(); } }
        public double TakeProfitStartPrice { get { return TradePrice * (Type == TrailingType.Sell ? 100 + TakeProfitStartPercent : 100 - TakeProfitStartPercent) * 0.01; } }
        public bool IgnoreStopLoss { get; set; } = false;

        public string Name {
            get {
                if (Ticker == null)
                    return string.Empty;
                return Ticker.HostName + " - " + Ticker.Name;
            }
        }

        public TrailingType Type { get; set; } = TrailingType.Sell;
        public TrailingState State { get; set; } = TrailingState.Analyze;
        public bool EnableIncrementalStopLoss { get; set; } = false;
        public string IndicatorText { get { return "Trailing indicator"; } }

        public void Update() {
            if (State == TrailingState.Analyze || State == TrailingState.TakeProfit)
                Analyze();
        }
        void Analyze() {
            if (Type == TrailingType.Sell) {
                ActualPrice = Ticker.HighestBid;
                MaxPrice = Math.Max(MaxPrice, ActualPrice);

                if (ActualPrice < OrderPrice) {
                    OnExecuteOrder();
                    return;
                } else if (ActualPrice >= TakeProfitStartPrice && State == TrailingState.Analyze) {
                    OnStartTakeProfit();
                    return;
                }
            } else {
                ActualPrice = Ticker.LowestAsk;
                MinPrice = Math.Min(MinPrice, ActualPrice);

                if (ActualPrice <= TakeProfitStartPrice && State == TrailingState.Analyze) {
                    OnStartTakeProfit();
                    return;
                }
                if (State == TrailingState.TakeProfit && ActualPrice > OrderPrice) {
                    OnExecuteOrder();
                    return;
                }
            }
        }

        double GetOrderPrice() {
            if (Type == TrailingType.Buy)
                return State == TrailingState.TakeProfit ? MinPrice * (100 + TakeProfitPercent) * 0.01 : double.NaN;

            if (State == TrailingState.TakeProfit)
                return MaxPrice * (100 - TakeProfitPercent) * 0.01;
            else {
                if (IgnoreStopLoss)
                    return -1;

                double basePrice = EnableIncrementalStopLoss ? MaxPrice : TradePrice;
                return basePrice * (100 - StopLossPricePercent) * 0.01;
            }
        }

        void OnExecuteOrder() {
            if (Mode == ActionMode.Notify) {
                //TelegramBot.Default.SendNotification(Ticker.Exchange + " - " + Ticker.Name + " - Order done!!");
                State = TrailingState.Done;
            } else if (Mode == ActionMode.Execute) {
                TradingResult res = Type == TrailingType.Sell ? Ticker.MarketSell(Amount) : Ticker.MarketBuy(Amount);
                if (res != null)
                    State = TrailingState.Done;
                //else
                    //TelegramBot.Default.SendNotification($"{Ticker.Exchange}. Error!! Can't sell {Ticker.Name}");
            }

            Ticker.Events.Add(new TickerEvent() { Time = DateTime.UtcNow, Text = "Stoploss!" });

        }
        void OnStartTakeProfit() {
            if(State == TrailingState.TakeProfit)
                return;
            State = TrailingState.TakeProfit;
            //if (Mode == ActionMode.Notify)
            //    TelegramBot.Default.SendNotification(Ticker.Exchange + " - " + Ticker.Name + " - Start TAKEPROFIT!!");
            Ticker.Events.Add(new TickerEvent() { Time = DateTime.UtcNow, Text = "Takeprofit!" });
        }

        public void Start() {
            StartDate = DateTime.Now;
            Ticker.Events.Add(new TickerEvent() {
                Text = string.Format("Trailing started! bought {0:0.00000000} at price {1:0.00000000}", Amount, TradePrice),
                Time = Ticker.Time,
                Current = Ticker.Last
            });
            Ticker.Save();
        }
        public void Change() {
            Ticker.Events.Add(new TickerEvent() {
                Text = string.Format("Trailing changed! New values: amount {0:0.00000000}", Amount),
                Time = Ticker.Time,
                Current = Ticker.Last
            });
        }

    }

    public enum EditingMode { Add, Edit }
    public enum ActionMode { Execute, Notify }
    public enum TrailingType { Buy, Sell }
    public enum TrailingState { Analyze, TakeProfit, Done } 

    [Serializable]
    public class TickerEvent {
        public TickerEvent() {
            Color = Color.Pink;
        }

        public string Text { get; set; }
        public DateTime Time { get; set; }
        public double Current { get; set; }
        public Color Color { get; set; }
    }
}

