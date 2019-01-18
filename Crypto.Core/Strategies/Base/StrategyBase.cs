using CryptoMarketClient;
using CryptoMarketClient.Common;
using CryptoMarketClient.Strategies;
using CryptoMarketClient.Strategies.Stupid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Strategies {
    [XmlInclude(typeof(SimpleBuyLowSellHighStrategy))]
    [XmlInclude(typeof(GridStrategyBase))]
    [XmlInclude(typeof(StaticGridStrategy))]
    //[XmlInclude(typeof())]
    [Serializable]
    public abstract class StrategyBase {
        public StrategyBase() {
            Id = Guid.NewGuid();
            FileName = Id.ToString();
        }

        public Guid Id { get; set; }
        [XmlIgnore]
        public StrategiesManager Manager { get; set; }
        public bool Enabled { get; set; } = false;
        public bool DemoMode { get; set; } = true;
        public string Description { get; set; }
        public abstract string StateText { get; }
        public double Earned { get; set; }
        
        public abstract string TypeName { get; }
        public string Name { get; set; }
        public IStrategyDataProvider DataProvider { get { return Manager.DataProvider; } }
        public List<StrategyHistoryItem> History { get; } = new List<StrategyHistoryItem>();
        public List<TradingResult> TradeHistory { get; } = new List<TradingResult>();

        public virtual bool Start() {
            return true;
        }
        public virtual bool Stop() {
            return true;
        }

        public abstract void OnEndDeserialize();

        protected virtual void Log(LogType logType, string text, double rate, double amount, StrategyOperation operation) {
            History.Add(new StrategyHistoryItem() { Type = logType, Rate = rate, Amount = amount, Operation = operation, Time = DateTime.Now, Text = text });
        }

        AccountInfo account;
        [XmlIgnore]
        public AccountInfo Account {
            get { return account; }
            set {
                if(Account == value)
                    return;
                account = value;
                OnAccountChanged();
            }
        }
        void OnAccountChanged() {
            AccountId = Account == null ? Guid.Empty : Account.Id;
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

        void OnAccountIdChanged() {
            Account = Exchange.GetAccount(AccountId);
        }
        public double MaxAllowedDeposit { get; set; }

        public string FileName { get; set; }
        public bool Load() { throw new NotImplementedException(); }
        public bool Save() { throw new NotImplementedException(); }

        public void OnTick() {
            if(!Enabled)
                return;
            OnTickCore();
        }

        protected abstract void OnTickCore();
        public virtual bool Initialize(IStrategyDataProvider dataProvider) {
            return true;
        }
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
            Account = from.Account;
            MaxAllowedDeposit = from.MaxAllowedDeposit;
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

        public virtual List<StrategyValidationError> Validate() {
            List<StrategyValidationError> list = new List<StrategyValidationError>();

            Name = GetTrimmedString(Name);
            FileName = GetTrimmedString(FileName);
            CheckNotEmptyString(list, Name, "Name");
            CheckNotEmptyString(list, FileName, "FileName");
            CheckAccountSpecified(list);
            CheckAvailableDeposit(list);

            return list;
        }

        void CheckAvailableDeposit(List<StrategyValidationError> list) {
            if(MaxAllowedDeposit == 0)
                list.Add(new StrategyValidationError() { DataObject = this, Description = "Max allowed deposit not specified.", PropertyName = "MaxAllowedDeposit", Value = "0" });
        }
    }
}
