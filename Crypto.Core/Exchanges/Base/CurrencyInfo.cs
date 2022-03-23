using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Base {
    public class CurrencyInfo {
        public CurrencyInfo(Exchange e, string currency) {
            Currency = currency;
            Exchange = e;
        }
        public Exchange Exchange { get; private set; }
        public virtual string Currency { get; set; }
        public double MaxDailyWithdrawal { get; set; }
        public double TxFee { get; set; }
        public double MinConfirmation { get; set; }
        public bool Disabled { get; set; }
        public bool IsActive { get { return !Disabled; } set { Disabled = !value; } }
        public string CurrencyLong { get; set; }
        public string CoinType { get; set; }
        public string BaseAddress { get; set; }

        public Dictionary<string, DepositMethod> Methods { get; } = new Dictionary<string, DepositMethod>();
        public DepositMethod GetOrCreateMethod(string name) {
            DepositMethod m = null;
            if(Methods.TryGetValue(name, out m))
                return m;
            m = new DepositMethod() { Name = name };
            Methods.Add(name, m);
            return m;
        }
        public virtual bool GetDepositMethods() {
            return Exchange.GetDepositMethods(Exchange.DefaultAccount, this);
        }

        protected virtual string MethodCurrency { get { return Currency; } }

        DepositMethod currentMethod;
        public DepositMethod CurrentMethod {
            get {
                if(currentMethod != null)
                    return currentMethod;
                if(Methods.Count == 1)
                    return Methods.Values.First();
                currentMethod = Methods.Values.FirstOrDefault(m => m.Currency == MethodCurrency);
                return currentMethod;
            }
            set {
                if(Methods.Values.Contains(value))
                    currentMethod = value;
            }
        }
    }

    public class DepositMethod {
        public string Name { get; internal set; }
        public bool Limit { get; internal set; }
        public double Fee { get; internal set; }
        public bool GenAddress { get; internal set; }
        public string Currency { get; internal set; }
        public string CurrencyName { get; internal set; }
        public string DepositAddress { get; internal set; }
        public string DepositTag { get; internal set; }
        public override string ToString() {
            return Name;
        }
    }
}
