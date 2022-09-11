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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using XmlSerialization;

namespace Crypto.Core.Strategies {
    [Serializable]
    [ParameterObject]
    [AllowDynamicTypes]
    public abstract class StrategyBase : IStrategyDataItemInfoOwner, ISupportSerialization {
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
        public virtual string Name { get; set; }
        [Browsable(false)]
        public IStrategyDataProvider DataProvider { get { return Manager == null? null: Manager.DataProvider; } }
        [Browsable(false)]
        public List<StrategyHistoryItem> History { get; } = new List<StrategyHistoryItem>();
        [Browsable(false)]
        public List<TradingResult> TradeHistory { get; } = new List<TradingResult>();
        [Browsable(false)]
        public abstract bool SupportSimulation { get; }

        internal static string GetFileName(Guid id) { return id.ToString() + ".xml"; }
        internal static string GetFullPathName(Guid id) {
            string directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\Strategies";
            if(!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            return directory + "\\" + GetFileName(id); 
        }

        public SimulationSettings SimulationSettings { get; } = new SimulationSettings();

        public DateTime SimulationStartDate { get { return SimulationSettings.StartTime; } set { SimulationSettings.StartTime = value; } }
        public DateTime SimulationEndDate { get { return SimulationSettings.EndTime; } set { SimulationSettings.EndTime = value; } }

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

        [XmlIgnore]
        public virtual string HelpWebPage { get { return string.Empty; } }

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

        [XmlIgnore, Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string FullPath { get; set; }

        public bool Load() {
            FileName = GetFileName(Id);
            FullPath = GetFullPathName(Id);
            return SerializationHelper.Current.Load(this, GetType(), FullPath);
        }
        public bool Save() {
            FileName = GetFileName(Id);
            FullPath = GetFullPathName(Id);
            SerializationHelper.Current.Save(this, GetType(), FullPath);
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

        void ISupportSerialization.OnBeginDeserialize() {
        }

        void ISupportSerialization.OnEndDeserialize() {
        }

        void ISupportSerialization.OnBeginSerialize() {
        }

        void ISupportSerialization.OnEndSerialize() {
        }

        [Browsable(false)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public List<InputParameterInfo> ParametersToOptimize { get; } = new List<InputParameterInfo>();

        [Browsable(false)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public OutputParameterInfo OutputParameter { get; set; }

        protected TradingResult AddDemoTradingResult(double rate, double amount, OrderType type) {
            TradingResult res = new TradingResult() { Amount = amount, Type = type, Date = DateTime.Now, OrderId = "demo", Total = rate * amount };
            res.Value = rate;
            res.Trades.Add(new TradeEntry() { Amount = amount, Date = DateTime.Now, Id = "demo", Rate = rate, Total = rate * amount, Type = type });
            return res;
        }

        protected TradingResult AddDemoTradingResult(Ticker ticker, double rate, double amount, OrderType type) {
            TradingResult res = new TradingResult() { Amount = amount, Type = type, Date = DateTime.Now, OrderId = "demo", Total = ticker.SpentInBaseCurrency(rate, amount) };
            res.Value = rate;
            res.Trades.Add(new TradeEntry() { Ticker = ticker, Amount = amount, Date = DateTime.Now, Id = "demo", Rate = rate, Total = ticker.SpentInBaseCurrency(rate, amount), Type = type });
            return res;
        }

        public virtual OpenPositionInfo OpenLongPosition(Ticker ticker, string mark, double value, double amount, double minProfitPc) {
            OpenPositionInfo info = OpenLongPosition(ticker, mark, value, amount, false, 0, minProfitPc);
            if(info != null)
                info.AllowHistory = true; // DataProvider is SimulationStrategyDataProvider;
            Save();
            return info;
        }

        public virtual OpenPositionInfo OpenShortPosition(Ticker ticker, string mark, double value, double amount, double minProfitPc) {
            OpenPositionInfo info = OpenShortPosition(ticker, mark, value, amount, false, 0, minProfitPc);
            if(info != null)
                info.AllowHistory = true; // DataProvider is SimulationStrategyDataProvider;
            Save();
            return info;
        }

        public virtual OpenPositionInfo OpenLongPosition(Ticker ticker, string mark, double value, double amount, bool allowTrailing, double trailingStopLossPc, double minProfitPc) {
            return OpenLongPosition(ticker, mark, value, amount, allowTrailing, true, trailingStopLossPc, minProfitPc);
        }

        public virtual OpenPositionInfo OpenShortPosition(Ticker ticker, string mark, double value, double amount, bool allowTrailing, double trailingStopLossPc, double minProfitPc) {
            return OpenShortPosition(ticker, mark, value, amount, allowTrailing, true, trailingStopLossPc, minProfitPc);
        }

        protected virtual DelayedPositionInfo AddDelayedPosition(string mark, double value, double amount, double closeValue, int liveTimeLength) {
            if(1.05 * value * amount > MaxAllowedDeposit)
                return null;
            DelayedPositionInfo info = new DelayedPositionInfo() { Time = DataProvider.CurrentTime, Type = OrderType.Buy, Mark = mark, Amount = amount, Price = value, LiveTimeLength = liveTimeLength, DataItemIndex = StrategyData.Count - 1, CloseValue = closeValue };
            DelayedPositions.Add(info);
            return info;
        }

        public virtual double MinDepositForOpenPosition { get; set; } = 100;

        protected double CalcFee(Ticker ticker, double total) {
            return total * ticker.Fee / 100.0;
        }

        public virtual OpenPositionInfo OpenLongPosition(Ticker ticker, string mark, double value, double amount, bool allowTrailing, bool checkForMinValue, double trailingStopLossPc, double minProfitPc) {
            if(1.05 * value * amount > MaxAllowedDeposit)
                return null;
            if(checkForMinValue && value * amount < MinDepositForOpenPosition)
                return null;
            TradingResult res = MarketBuy(ticker, value, amount);
            if(res == null)
                return null;
            OpenPositionInfo info = new OpenPositionInfo() {
                Ticker = ticker,
                DataItemIndex = StrategyData.Count - 1,
                Time = DataProvider.CurrentTime,
                Type = OrderType.Buy,
                Spent = res.Total + CalcFee(ticker, res.Total),
                AllowTrailing = allowTrailing,
                StopLossPercent = trailingStopLossPc,
                OpenValue = res.Value,
                OpenAmount = res.Amount,
                Amount = res.Amount,
                Mark = mark,
                AllowHistory = (DataProvider is SimulationStrategyDataProvider),
                Total = res.Total,
                MinProfitPercent = minProfitPc,
                CloseValue = value * (1 + minProfitPc * 0.01),
            };
            info.UpdateCurrentValue(DataProvider.CurrentTime, res.Value);

            OpenedOrders.Add(info);
            OrdersHistory.Add(info);

            OnOpenLongPosition(info);
            MaxAllowedDeposit -= info.Spent;

            IOpenedPositionsProvider provider = (IOpenedPositionsProvider)StrategyData.Last();
            if(provider != null) {
                provider.OpenedPositions.Add(info);
                provider.AddMark(mark);
            }

            Save();
            return info;
        }

        public virtual TradingResult MarketBuy(Ticker ticker, double rate, double amount) {
            TradingResult res = null;
            if(!DemoMode) {
                res = ticker.MarketBuy(amount);
                if(res == null)
                    Log(LogType.Error, "", rate, amount, StrategyOperation.MarketBuy);
                return res;
            }
            else {
                res = AddDemoTradingResult(ticker, rate, amount, OrderType.Buy);
            }
            TradeHistory.Add(res);
            Log(LogType.Success, ticker.Name, rate, amount, StrategyOperation.MarketBuy);
            return res;
        }

        public virtual TradingResult MarketSell(Ticker ticker, double rate, double amount) {
            TradingResult res = null;
            if(!DemoMode) {
                res = ticker.MarketSell(amount);
                if(res == null)
                    Log(LogType.Error, "", rate, amount, StrategyOperation.MarketSell);
                return res;
            }
            else {
                res = AddDemoTradingResult(ticker, rate, amount, OrderType.Sell);
            }
            TradeHistory.Add(res);
            Log(LogType.Success, ticker.Name, rate, amount, StrategyOperation.MarketSell);
            return res;
        }
        
        public virtual TradingResult PlaceBid(Ticker ticker, double rate, double amount) {
            TradingResult res = null;
            if(!DemoMode) {
                res = ticker.Buy(rate, amount);
                if(res == null)
                    Log(LogType.Error, "", rate, amount, StrategyOperation.LimitBuy);
                return res;
            }
            else {
                res = AddDemoTradingResult(rate, amount, OrderType.Buy);
            }
            Log(LogType.Success, ticker.Name, rate, amount, StrategyOperation.LimitBuy);
            return res;
        }
        public virtual TradingResult PlaceAsk(Ticker ticker, double rate, double amount) {
            TradingResult res = null;
            if(!DemoMode) {
                res = ticker.Sell(rate, amount);
                if(res == null)
                    Log(LogType.Error, "", rate, amount, StrategyOperation.LimitSell);
                return res;
            }
            else
                res = AddDemoTradingResult(rate, amount, OrderType.Sell);
            Log(LogType.Error, ticker.Name, rate, amount, StrategyOperation.LimitSell);
            return res;
        }

        protected virtual double GetMaxAllowedShortDeposit() { return MaxAllowedDeposit; }
        public virtual OpenPositionInfo OpenShortPosition(Ticker ticker, string mark, double value, double amount, bool allowTrailing, bool checkForMinValue, double trailingStopLossPc, double minProfitPc) {
            double spent = ticker.SpentInBaseCurrency(value, amount);

            if(1.05 * spent > GetMaxAllowedShortDeposit())
                return null;
            if(checkForMinValue && spent < MinDepositForOpenPosition)
                return null;
            TradingResult res = MarketSell(ticker, value, amount);
            if(res == null)
                return null;
            OpenPositionInfo info = new OpenPositionInfo() {
                Ticker = ticker,
                DataItemIndex = StrategyData.Count - 1,
                Time = DataProvider.CurrentTime,
                Type = OrderType.Sell,
                Spent = spent + CalcFee(ticker, res.Total),
                AllowTrailing = allowTrailing,
                StopLossPercent = trailingStopLossPc,
                OpenValue = res.Value,
                OpenAmount = res.Amount,
                Amount = res.Amount,
                Mark = mark,
                AllowHistory = (DataProvider is SimulationStrategyDataProvider),
                Total = res.Total,
                MinProfitPercent = minProfitPc,
                CloseValue = value * (1 + minProfitPc * 0.01),
            };
            info.UpdateCurrentValue(DataProvider.CurrentTime, res.Value);

            OpenedOrders.Add(info);
            OrdersHistory.Add(info);

            OnOpenShortPosition(info);
            UpdateMaxAllowedShortDeposit(-info.Spent);

            IOpenedPositionsProvider provider = (IOpenedPositionsProvider)StrategyData.Last();
            if(provider != null) {
                provider.OpenedPositions.Add(info);
                provider.AddMark(mark);
            }

            Save();
            return info;
        }

        protected virtual void UpdateMaxAllowedShortDeposit(double delta) {
            MaxAllowedDeposit += delta;
        }

        protected virtual void OnOpenLongPosition(OpenPositionInfo info) {
            if(EnableNotifications) {
                SendNotification("long open " + info.Mark + " rate: " + info.OpenValue.ToString("0.00000000") + " amount: " + info.OpenAmount.ToString("0.00000000") + " web: " + info.Ticker.WebPageAddress);
            }
        }

        protected virtual void OnOpenShortPosition(OpenPositionInfo info) {
            if(EnableNotifications) {
                SendNotification("long short " + info.Mark + " rate: " + info.OpenValue.ToString("0.00000000") + " amount: " + info.OpenAmount.ToString("0.00000000") + " web: " + info.Ticker.WebPageAddress);
            }
        }

        public virtual void CloseShortPosition(OpenPositionInfo info) {
            if(EnableNotifications) {
                SendNotification("short close " + info.Mark + " rate: " + info.CloseValue.ToString("0.00000000") + " web: " + info.Ticker.WebPageAddress);
            }
        }

        public virtual void CloseLongPosition(OpenPositionInfo info) {
            if(EnableNotifications) {
                SendNotification("long close " + info.Mark + " rate: " + info.CloseValue.ToString("0.00000000") + " web: " + info.Ticker.WebPageAddress);
            }
            //TradingResult res = MarketSell(Ticker.OrderBook.Bids[0].Value, info.Amount);
            //if(res != null) {
            //    double earned = res.Total - CalcFee(res.Total);
            //    MaxAllowedDeposit += earned;
            //    info.Earned += earned;
            //    info.Amount -= res.Amount;
            //    info.Total -= res.Total;
            //    RedWaterfallDataItem item = (RedWaterfallDataItem)info.Tag;
            //    item.Closed = true;
            //    item.CloseLength = ((RedWaterfallDataItem)StrategyData.Last()).Index - item.Index;
            //    RedWaterfallDataItem last = (RedWaterfallDataItem)StrategyData.Last();
            //    if(info.Amount < 0.000001) {
            //        OpenedOrders.Remove(info);
            //        Earned += info.Earned - info.Spent;
            //    }
            //    last.ClosedOrder = true;
            //    last.Value = Ticker.OrderBook.Bids[0].Value;
            //}
        }
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
