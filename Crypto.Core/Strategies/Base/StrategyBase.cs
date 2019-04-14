using Crypto.Core.Strategies.Arbitrages.AltBtcUsdt;
using Crypto.Core.Strategies.Custom;
using CryptoMarketClient;
using CryptoMarketClient.Common;
using CryptoMarketClient.Helpers;
using CryptoMarketClient.Strategies;
using CryptoMarketClient.Strategies.Stupid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Strategies {
    [XmlInclude(typeof(SimpleBuyLowSellHighStrategy))]
    [XmlInclude(typeof(GridStrategyBase))]
    [XmlInclude(typeof(StaticGridStrategy))]
    [XmlInclude(typeof(Signal.SignalNotificationStrategy))]
    [XmlInclude(typeof(Signal.TripleRsiIndicatorStrategy))]
    [XmlInclude(typeof(Signal.MacdTrendStrategy))]
    [XmlInclude(typeof(CustomTickerStrategy))]
    [XmlInclude(typeof(WalletInvestorForecastStrategy))]
    [XmlInclude(typeof(TriplePairStrategy))]
    //[XmlInclude(typeof())]
    [Serializable]
    public abstract class StrategyBase {
        public StrategyBase() {
            Id = Guid.NewGuid();
            FileName = Id.ToString();
            InitializeDataItems();
        }

        public Guid Id { get; set; }
        [XmlIgnore]
        public StrategiesManager Manager { get; set; }
        public bool Enabled { get; set; } = false;
        public bool DemoMode { get; set; } = true;
        public string Description { get; set; }
        public abstract string StateText { get; }
        public double Earned { get; set; }

        public long ChatId { get; set; }
        public bool EnableNotifications { get; set; }

        public void SendNotification(string notification) {
            if(!EnableNotifications)
                return;
            TelegramBot.Default.SendNotification(notification, ChatId);
        }

        public abstract string TypeName { get; }
        public string Name { get; set; }
        public IStrategyDataProvider DataProvider { get { return Manager == null? null: Manager.DataProvider; } }
        public List<StrategyHistoryItem> History { get; } = new List<StrategyHistoryItem>();
        public List<TradingResult> TradeHistory { get; } = new List<TradingResult>();
        public abstract bool SupportSimulation { get; }

        [XmlIgnore]
        public List<object> StrategyData { get; } = new List<object>();

        [XmlIgnore]
        public List<StrategyDataItemInfo> DataItemInfos { get; } = new List<StrategyDataItemInfo>();

        public StrategyDataItemInfo CandleStickItem() {
            DataItemInfos.Add(new StrategyDataItemInfo() { ChartType = ChartType.CandleStick });
            return DataItemInfos.Last();
        }

        public StrategyDataItemInfo AnnotationItem(string fieldName, string text, Color color, string anchor) {
            var item = DataItem(fieldName);
            item.ChartType = ChartType.Dot;
            item.Color = color;
            item.Visibility = DataVisibility.Chart;
            item.ChartType = ChartType.Annotation;
            item.AnnotationText = text;
            item.AnnotationAnchorField = anchor;
            return item;
        }

        public StrategyDataItemInfo TimeItem(string fieldName) {
            DataItemInfos.Add(new StrategyDataItemInfo() { FieldName = fieldName, Visibility = DataVisibility.Table, Type = DataType.DateTime, FormatString = "dd.MM.yyyy hh:mm" });
            return DataItemInfos.Last();
        }

        public StrategyDataItemInfo DataItem(string fieldName) {
            DataItemInfos.Add(new StrategyDataItemInfo() { FieldName = fieldName });
            return DataItemInfos.Last();
        }

        public StrategyDataItemInfo DataItem(string fieldName, string formatString) {
            DataItemInfos.Add(new StrategyDataItemInfo() { FieldName = fieldName, FormatString = formatString });
            return DataItemInfos.Last();
        }
       
        public StrategyDataItemInfo EnumItem(string fieldName) {
            DataItemInfos.Add(new StrategyDataItemInfo() { FieldName = fieldName, Visibility = DataVisibility.Table });
            return DataItemInfos.Last();
        }

        public StrategyDataItemInfo DataItem(string fieldName, string formatString, Color color) {
            DataItemInfos.Add(new StrategyDataItemInfo() { FieldName = fieldName, FormatString = formatString, Color = color });
            return DataItemInfos.Last();
        }

        public StrategyDataItemInfo DataItem(string fieldName, Color color) {
            DataItemInfos.Add(new StrategyDataItemInfo() { FieldName = fieldName, Color = color });
            return DataItemInfos.Last();
        }

        public StrategyDataItemInfo DataItem(string fieldName, Color color, int width) {
            DataItemInfos.Add(new StrategyDataItemInfo() { FieldName = fieldName, Color = color, GraphWidth = width });
            return DataItemInfos.Last();
        }
        
        internal bool Initialize() {
            DataItemInfos.Clear();
            InitializeDataItems();
            return InitializeCore();
        }

        protected virtual void InitializeDataItems() { }

        public abstract bool InitializeCore();

        public virtual StrategyInputInfo CreateInputInfo() {
            return new StrategyInputInfo() { };
        }

        public virtual bool Start() {
            TelegramBot.Default.TryAddClient(ChatId, true, "", Id);
            return true;
        }
        public virtual bool Stop() {
            return true;
        }

        public abstract void OnEndDeserialize();

        public double BoughtTotal { get; set; }
        public double SoldTotal { get; set; }

        public double MaxActualSellDeposit { get; set; } = 0;
        public double MaxActualBuyDeposit { get; set; } = -1;

        protected virtual void Log(LogType logType, string text, double rate, double amount, StrategyOperation operation) {
            History.Add(new StrategyHistoryItem() { Type = logType, Rate = rate, Amount = amount, Operation = operation, Time = DateTime.Now, Text = text, BuyDeposit = MaxActualBuyDeposit, SellDeposit = MaxActualSellDeposit });
        }

        AccountInfo account;
        [XmlIgnore]
        public AccountInfo Account {
            get {
                if(account == null || account.Id != AccountId)
                    account = DataProvider.GetAccount(AccountId);
                return account;
            }
        }
        Guid accountId;
        public Guid AccountId {
            get { return accountId; }
            set {
                if(AccountId == value)
                    return;
                accountId = value;
                OnAccountIdChanged();
            }
        }

        protected virtual void OnAccountIdChanged() { }
        double maxAllowedDeposit;
        public double MaxAllowedDeposit {
            get { return maxAllowedDeposit; }
            set {
                if(MaxAllowedDeposit == value)
                    return;
                maxAllowedDeposit = value;
                OnMaxAllowedDepositChanged();
            }
        }

        protected virtual void OnMaxAllowedDepositChanged() {
            MaxActualBuyDeposit = -1;
        }

        public string FileName { get; set; }
        public bool Load() { throw new NotImplementedException(); }
        public bool Save() { throw new NotImplementedException(); }

        public void OnTick() {
            if(!Enabled)
                return;
            OnTickCore();
        }

        protected abstract void OnTickCore();
        public StrategyBase Clone() {
            ConstructorInfo info = GetType().GetConstructor(new Type[] { });
            StrategyBase cloned = (StrategyBase)info.Invoke(new object[] { });
            cloned.Assign(this);
            return cloned;
        }
        public virtual void Assign(StrategyBase from) {
            Manager = from.Manager;
            Enabled = from.Enabled;
            DemoMode = from.DemoMode;
            Description = from.Description;
            FileName = from.FileName;
            AccountId = from.AccountId;
            MaxAllowedDeposit = from.MaxAllowedDeposit;
            ChatId = from.ChatId;
        }

        protected string GetTrimmedString(string value) {
            return string.IsNullOrEmpty(value) ? value : value.Trim();
        }

        protected void CheckNotEmptyString(List<StrategyValidationError> list, string value, string propName) {
            if(string.IsNullOrEmpty(value))
                list.Add(new StrategyValidationError() { PropertyName = propName, Description = "This property should not be empty", Value = Convert.ToString(value), DataObject = this });
        }

        protected virtual void CheckAccountSpecified(List<StrategyValidationError> list) {
            if(AccountId == Guid.Empty)
                list.Add(new StrategyValidationError() { DataObject = this, Description = "Account not specified", PropertyName = "AccountId", Value = "[empty]" });
            else if(Account == null)
                list.Add(new StrategyValidationError() { DataObject = this, Description = "Account not found", PropertyName = "Account", Value = "[empty]" });
        }

        protected virtual bool ShouldCheckAccount { get { return true; } }
        public virtual List<StrategyValidationError> Validate() {
            List<StrategyValidationError> list = new List<StrategyValidationError>();

            Name = GetTrimmedString(Name);
            FileName = GetTrimmedString(FileName);
            CheckNotEmptyString(list, Name, "Name");
            CheckNotEmptyString(list, FileName, "FileName");
            if(ShouldCheckAccount) {
                CheckAccountSpecified(list);
            }
            CheckAvailableDeposit(list);
            return list;
        }

        void CheckAvailableDeposit(List<StrategyValidationError> list) {
            if(MaxAllowedDeposit == 0)
                list.Add(new StrategyValidationError() { DataObject = this, Description = "Max allowed deposit not specified.", PropertyName = "MaxAllowedDeposit", Value = "0" });
        }
    }
}
