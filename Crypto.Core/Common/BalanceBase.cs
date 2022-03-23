using Crypto.Core.Exchanges.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Common {
    public abstract class BalanceBase {
        public BalanceBase(AccountInfo info, CurrencyInfo currency) {
            Account = info;
            CurrencyInfo = currency;
        }

        public CurrencyInfo CurrencyInfo { get; set; }
        public bool NonZero { get { return Available != 0; } }
        public string Exchange { get { return Account == null? "": Account.Exchange.Name; } }
        public AccountInfo Account { get; set; }
        public Exchange ExchangeCore { get { return Account == null ? null : Account.Exchange; } }
        public string AccountName { get { return Account == null ? "undefined" : Account.Name; } }
        public virtual string DisplayName { get { return CurrencyInfo?.Currency; } }
        public string Currency { get { return CurrencyInfo?.Currency; } }
        public double Balance { get; set; }
        public double Available { get; set; }
        public double LastAvailable { get; set; }
        public double OnOrders { get; set; }
        public string Status { get; set; }
        public double BtcValue {
            get {
                if(Currency == "BTC" || Currency == "XBT")
                    return Available + OnOrders;
                return GetValueIn((Available + OnOrders), "BTC", "XBT");
            }
        }

        private double GetValueIn(double value, params string[] curr) {
            for(int i = 0; i < curr.Length; i++) {
                Ticker ticker = GetTicker(curr[i]);
                if(ticker != null) {
                    if(ticker.Last == 0)
                        ticker.Exchange.UpdateTicker(ticker);
                    double rate = ticker.HighestBid == 0 ? ticker.Last : ticker.HighestBid;
                    return rate * value;
                }
            }
            return 0.0;
        }

        public double UsdtPrice {
            get {
                if(ExchangeCore == null)
                    return double.NaN;
                if(Currency == "USD" || Currency == "USDT" || Currency == "USDC")
                    return 1.0;
                return GetValueIn(1.0, "USDT", "USDC", "USD");
            }
        }

        public double UsdtValue {
            get {
                if(ExchangeCore == null)
                    return double.NaN;
                if(Currency == "USD" || Currency == "USDT" || Currency == "USDC")
                    return (Available + OnOrders);
                return GetValueIn((Available + OnOrders), "USDT", "USDC", "USD");
            }
        }

        Dictionary<string, Ticker> tickers = new Dictionary<string, Ticker>();
        public Ticker GetTicker(string baseCurrency) {
            if(this.tickers.ContainsKey(baseCurrency))
                return this.tickers[baseCurrency];
            Ticker tt = ExchangeCore.Tickers.FirstOrDefault(t => t.MarketCurrency == Currency && t.BaseCurrency == baseCurrency);
            this.tickers.Add(baseCurrency, tt);
            return tt;
        }

        string depositAddress;
        public string DepositAddress {
            get {
                if(CurrencyInfo.CurrentMethod != null)
                    return CurrencyInfo.CurrentMethod.DepositAddress;
                return depositAddress;
            }
            set {
                depositAddress = value;
            }
        }

        string depositTag;
        public string DepositTag {
            get {
                if(CurrencyInfo.CurrentMethod != null)
                    return CurrencyInfo.CurrentMethod.DepositTag;
                return depositTag;
            }
            set {
                depositTag = value;
            }
        }
        
        public double DepositChanged {
            get {
                double max = Math.Max(Available, LastAvailable);
                double delta = Math.Abs(Available - LastAvailable);
                if(max == 0)
                    return 0;
                return (delta / max);
            }
        }

        public void Clear() {
            Balance = 0;
            Available = 0;
            LastAvailable = 0;
            OnOrders = 0;
            DepositAddress = null;
        }

        public bool GetDeposit() {
            return Account.Exchange.GetDeposit(this);
        }

        public DepositMethod CurrentMethod {
            get { return CurrencyInfo.CurrentMethod; }
            set { CurrencyInfo.CurrentMethod = value; }
        }
    }
}
