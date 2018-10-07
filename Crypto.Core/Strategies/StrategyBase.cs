using CryptoMarketClient;
using CryptoMarketClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Strategies {
    public abstract class StrategyBase {
        public StrategyBase() {
            Id = Guid.NewGuid();
            FileName = Id.ToString();
        }

        public Guid Id { get; set; }
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
        

        protected virtual void Log(LogType logType, string text, double rate, double amount, StrategyOperation operation) {
            History.Add(new StrategyHistoryItem() { Type = logType, Rate = rate, Amount = amount, Operation = operation, Time = DateTime.Now, Text = text });
        }

        AccountInfo account;
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
        public double MaxAvailableDeposit { get; set; }

        public string FileName { get; set; }
        public bool Load() { throw new NotImplementedException(); }
        public bool Save() { throw new NotImplementedException(); }

        public void OnTick() {
            if(!Enabled)
                return;
            OnTickCore();
        }

        protected abstract void OnTickCore();
        public bool Initialize(IStrategyDataProvider dataProvider) {
            throw new NotImplementedException();
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
        }

        protected string GetTrimmedString(string value) {
            return string.IsNullOrEmpty(value) ? value : value.Trim();
        }

        protected void CheckNotEmptyString(List<StrategyValidationError> list, string value, string propName) {
            if(string.IsNullOrEmpty(value))
                list.Add(new StrategyValidationError() { PropertyName = propName, Description = "This property should not be empty", Value = Convert.ToString(value), DataObject = this });
        }

        public virtual List<StrategyValidationError> Validate() {
            List<StrategyValidationError> list = new List<StrategyValidationError>();

            Name = GetTrimmedString(Name);
            FileName = GetTrimmedString(FileName);
            CheckNotEmptyString(list, Name, "Name");
            CheckNotEmptyString(list, FileName, "FileName");
            return list;
        }
    }
}
