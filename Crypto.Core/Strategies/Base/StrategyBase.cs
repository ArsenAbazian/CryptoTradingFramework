using Crypto.Core.Common;
using Crypto.Core.Helpers;
using Crypto.Core.Strategies.Arbitrages.AltBtcUsdt;
using Crypto.Core.Strategies.Arbitrages.Statistical;
using Crypto.Core.Strategies.Base;
using Crypto.Core.Strategies.Custom;
using Crypto.Core.Strategies.Listeners;
using Crypto.Core.Strategies.Stupid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
    [XmlInclude(typeof(TickerDataCaptureStrategy))]
    [XmlInclude(typeof(MarketMakingStrategy))]
    [XmlInclude(typeof(StatisticalArbitrageStrategy))]
    [XmlInclude(typeof(TaSimpleStrategy))]
    [XmlInclude(typeof(HipeBasedStrategy))]
    [Serializable]
    [ParameterObject]
    public abstract class StrategyBase : IStrategyDataItemInfoOwner {
        public StrategyBase() {
            Id = Guid.NewGuid();
            FileName = Id.ToString();
            InitializeDataItems();
        }

        [Browsable(false)]
        public Guid Id { get; set; }
        [XmlIgnore]
        [Browsable(false)]
        public StrategiesManager Manager { get; set; }
        [StrategyProperty(false)]
        public bool Enabled { get; set; } = false;
        [StrategyProperty(false)]
        public bool DemoMode { get; set; } = true;
        [XmlIgnore]
        [Browsable(false)]
        public bool OptimizationMode { get; set; }
        [XmlIgnore]
        [Browsable(false)]
        public bool OptimizationParametersInitialized { get; set; }
        [StrategyProperty(false)]
        public string Description { get; set; }
        [Browsable(false)]
        public abstract string StateText { get; }
        [Browsable(false)]
        [OutputParameter]
        public double Earned { get; set; }

        [StrategyProperty(false)]
        public long ChatId { get; set; }
        [StrategyProperty(false)]
        public bool EnableNotifications { get; set; }

        protected bool SimulationMode { get { return DataProvider is SimulationStrategyDataProvider; } }

        public void SendNotification(string notification) {
            if(!EnableNotifications)
                return;
            TelegramBot.Default.SendNotification(notification, ChatId);
        }

        [Browsable(false)]
        public abstract string TypeName { get; }
        [StrategyProperty(false)]
        public string Name { get; set; }
        [Browsable(false)]
        public IStrategyDataProvider DataProvider { get { return Manager == null? null: Manager.DataProvider; } }
        [Browsable(false)]
        public List<StrategyHistoryItem> History { get; } = new List<StrategyHistoryItem>();
        [Browsable(false)]
        public List<TradingResult> TradeHistory { get; } = new List<TradingResult>();
        [Browsable(false)]
        public abstract bool SupportSimulation { get; }

        [XmlIgnore]
        [Browsable(false)]
        public ResizeableArray<object> StrategyData { get; } = new ResizeableArray<object>();
        [XmlIgnore]
        ResizeableArray<object> IStrategyDataItemInfoOwner.Items { get { return StrategyData; } }

        [XmlIgnore, Browsable(false)]
        public List<StrategyDataItemInfo> DataItemInfos { get; } = new List<StrategyDataItemInfo>();

        public StrategyDataItemInfo CandleStickItem() {
            DataItemInfos.Add(new StrategyDataItemInfo() { ChartType = ChartType.CandleStick, Visibility = DataVisibility.Chart });
            return DataItemInfos.Last();
        }

        [Browsable(false)]
        public ResizeableArray<DelayedPositionInfo> DelayedPositions { get; } = new ResizeableArray<DelayedPositionInfo>();
        [Browsable(false)]
        public ResizeableArray<OpenPositionInfo> OpenedOrders { get; } = new ResizeableArray<OpenPositionInfo>();
        [Browsable(false)]
        public ResizeableArray<OpenPositionInfo> OrdersHistory { get; } = new ResizeableArray<OpenPositionInfo>();

        [XmlIgnore]
        [Browsable(false)]
        public bool PanicMode { get; protected set; }
        public void Break() { PanicMode = true; }

        int IStrategyDataItemInfoOwner.MeasureUnitMultiplier { get { return 30; } set { } }
        StrategyDateTimeMeasureUnit IStrategyDataItemInfoOwner.MeasureUnit { get { return StrategyDateTimeMeasureUnit.Minute; } set { } }

        protected virtual void ApplyParametersToOptimize() {
            foreach(InputParameterInfo info in ParametersToOptimize) {
                info.ApplyValue();
            }
        }
        protected virtual void CheckParametersToOptimize() {
            if(OptimizationMode) {
                if(!OptimizationParametersInitialized)
                    InitializeParametersToOptimize();
                else {
                    ApplyParametersToOptimize();
                    ClearOutputParameter();
                }
                OptimizationParametersInitialized = true;
            }
        }

        protected internal virtual void OnStarted() {
            CheckParametersToOptimize();
            LogManager.Default.Add(LogType.Success, this, Name, "started.", "");
            SendNotification("started.");
        }

        public StrategyDataItemInfo AnnotationItem(string fieldName, string text, Color color, string anchor) {
            var item = DataItem(fieldName);
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

        [Browsable(false)]
        public double BoughtTotal { get; set; }
        [Browsable(false)]
        public double SoldTotal { get; set; }

        [Browsable(false)]
        public double MaxActualSellDeposit { get; set; } = 0;
        [Browsable(false)]
        public double MaxActualBuyDeposit { get; set; } = -1;

        protected virtual void Log(LogType logType, string text, double rate, double amount, StrategyOperation operation) {
            History.Add(new StrategyHistoryItem() { Type = logType, Rate = rate, Amount = amount, Operation = operation, Time = DateTime.Now, Text = text, BuyDeposit = MaxActualBuyDeposit, SellDeposit = MaxActualSellDeposit });
        }

        AccountInfo account;
        [XmlIgnore]
        [Browsable(false)]
        public AccountInfo Account {
            get {
                if((account == null || account.Id != AccountId) && DataProvider != null) 
                    account = DataProvider.GetAccount(AccountId);
                return account;
            }
        }
        Guid accountId;
        [StrategyProperty(false)]
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
        [StrategyProperty(false)]
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

        [Browsable(false)]
        public string FileName { get; set; }
        public bool Load() { throw new NotImplementedException(); }
        public bool Save() {
            if(Manager.DataProvider is SimulationStrategyDataProvider)
                return true;
            return Manager.Save();
        }

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
        
        protected void AssignValueProperties(StrategyBase from) {
            if(GetType() != from.GetType())
                return;
            PropertyInfo[] props = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach(var p in props) {
                if(!p.CanWrite)
                    continue;
                if(p.PropertyType.IsValueType || p.PropertyType == typeof(string) || p.PropertyType.IsEnum) {
                    p.SetValue(this, p.GetValue(from));
                }
            }
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

            AssignValueProperties(from);

            ParametersToOptimize.Clear();
            foreach(var item in from.ParametersToOptimize)
                ParametersToOptimize.Add((InputParameterInfo)item.Clone());
            OutputParameter = null;
            if(from.OutputParameter != null)
                OutputParameter = (OutputParameterInfo)from.OutputParameter.Clone();
        }

        public virtual void InitializeParametersToOptimize() {
            foreach(InputParameterInfo info in ParametersToOptimize) {
                Initialize(info);
            }
            if(OutputParameter != null)
                Initialize(OutputParameter);
            OptimizationParametersInitialized = true;
        }

        public virtual void ClearOutputParameter() {
            if(OutputParameter != null)
                Initialize(OutputParameter);
        }

        protected virtual void Initialize(InputParameterInfo info) {
            info.Owner = BindingHelper.GetBindingOwner(info.FieldName, this);
            info.PropertyInfo = BindingHelper.GetPropertyInfo(info.FieldName, this);
            InputParameterAttribute attr = info.PropertyInfo.GetCustomAttribute<InputParameterAttribute>();
            if(attr != null) {
                if(info.MinValueCore == null) info.MinValueCore = attr.MinValue;
                if(info.MaxValueCore == null) info.MaxValueCore = attr.MaxValue;
                if(info.ChangeCore == null) info.ChangeCore = attr.Change;
            }
            info.InitializeStartValue();
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

        protected virtual bool ShouldCheckAccount { get { return !DemoMode; } }
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

        [Browsable(false)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public List<InputParameterInfo> ParametersToOptimize { get; } = new List<InputParameterInfo>();

        [Browsable(false)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public OutputParameterInfo OutputParameter { get; set; }
    }

    public class InputParameterAttribute : Attribute {
        public InputParameterAttribute() {
            IsInput = true;
        }
    
        public InputParameterAttribute(bool isInput) {
            IsInput = isInput;
        }
        public InputParameterAttribute(int minValue, int maxValue, int change) : this(true) {
            MinValue = minValue;
            MaxValue = maxValue;
            Change = change;
        }
        public InputParameterAttribute(long minValue, long maxValue, long change) : this(true) {
            MinValue = minValue;
            MaxValue = maxValue;
            Change = change;
        }
        public InputParameterAttribute(float minValue, float maxValue, float change) : this(true) {
            MinValue = minValue;
            MaxValue = maxValue;
            Change = change;
        }
        public InputParameterAttribute(double minValue, double maxValue, double change) : this(true) {
            MinValue = minValue;
            MaxValue = maxValue;
            Change = change;
        }
        [XmlIgnore]
        public bool IsInput { get; private set; }
        public object MinValue { get; set; }
        public object MaxValue { get; set; }
        public object Change { get; set; }
    }

    public class OutputParameterAttribute : Attribute {
        public OutputParameterAttribute() {
            IsOutput = true;
        }

        public OutputParameterAttribute(bool isOutput) {
            IsOutput = isOutput;
        }
        [XmlIgnore]
        public bool IsOutput { get; private set; }
    }

    public class ParameterObjectAttribute : Attribute {
        public ParameterObjectAttribute() {
            IsInput = true;
        }

        public ParameterObjectAttribute(bool isInput) {
            IsInput = isInput;
        }
        [XmlIgnore]
        public bool IsInput { get; private set; }
    }

    public class StrategyPropertyAttribute : Attribute {
        public StrategyPropertyAttribute(string tabName) {
            TabName = tabName;
        }
        public StrategyPropertyAttribute(string tabName, bool browsable) {
            TabName = tabName;
            Browsable = browsable;
        }
        public StrategyPropertyAttribute() : this("Common") {
        }
        public StrategyPropertyAttribute(bool browsable) : this("Common") {
            Browsable = browsable;
        }
        public string TabName { get; set; }
        public bool Browsable { get; set; } = true;
    }

    public interface IInputDataWithTime {
        DateTime Time { get; set; }
    }
}
