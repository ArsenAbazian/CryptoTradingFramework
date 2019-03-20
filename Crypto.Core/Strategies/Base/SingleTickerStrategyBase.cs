using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Crypto.Core.Common;
using Crypto.Core.Strategies;
using CryptoMarketClient.Common;

namespace CryptoMarketClient.Strategies {
    [Serializable]
    public abstract class TickerStrategyBase  : StrategyBase {
        public TickerStrategyBase() {
        }

        Ticker ticker;
        [XmlIgnore]
        public Ticker Ticker {
            get {
                if(ticker == null && TickerInfo != null)
                    ticker = GetTicker();
                return ticker;
            }
            set {
                if(Ticker == value)
                    return;
                ticker = value;
                OnTickerChanged();
            }
        }

        public override void Assign(StrategyBase from) {
            base.Assign(from);
            TickerStrategyBase st = from as TickerStrategyBase;
            if(st == null)
                return;
            TickerInfo = st.TickerInfo;
        }

        double maxActualDeposit = -1;
        public double MaxActualDeposit {
            get {
                if(maxActualDeposit < 0)
                    maxActualDeposit = GetMaxActualDepositByPriority();
                return maxActualDeposit;
            }
        }
        double GetMaxActualDepositByPriority() {
            if(Manager == null || Account == null || Ticker == null)
                return -1;
            double res = Account.GetBalance(Ticker.BaseCurrency);
            foreach(StrategyBase s in Manager.Strategies) {
                if(s == this)
                    break;
                res -= s.MaxAllowedDeposit;
            }
            return res;
        }

        TickerNameInfo tickerInfo;
        public TickerNameInfo TickerInfo {
            get { return tickerInfo; }
            set {
                if(TickerInfo == value)
                    return;
                tickerInfo = value;
                OnTickerInfoChanged();
            }
        }

        void OnTickerInfoChanged() {
            if(TickerInfo == null) {
                Ticker = null;
                return;
            }
            Ticker t = GetTicker();
            if(t != null)
                Ticker = t;
        }

        Ticker GetTicker() {
            Exchange e = Exchange.Get(TickerInfo.Exchange);
            if(e == null)
                return null;
            return e.GetTicker(TickerInfo.Ticker);
        }

        void OnTickerChanged() {
            if(Ticker == null) {
                TickerInfo = null;
                return;
            }
            TickerInfo = new TickerNameInfo() { Exchange = Ticker.Exchange.Type, Ticker = Ticker.Name };
        }

        protected virtual TickerInputInfo CreateInputInfo() {
            return new TickerInputInfo() { Exchange = TickerInfo.Exchange, TickerName = TickerInfo.Ticker, OrderBook = true, TradeHistory = true };
        }

        public override bool Start() {
            if(!base.Start())
                return false;

            TickerInputInfo inputInfo = CreateInputInfo();
            bool res = DataProvider.Connect(inputInfo);
            if(res)
                Ticker = inputInfo.Ticker;
            return res;
        }

        public override bool Stop() {
            if(!base.Stop())
                return false;
            return true;
            //return DataProvider.Disconnect(); TODO
        }

        protected TradingResult AddDemoTradingResult(double rate, double amount, OrderType type) {
            TradingResult res = new TradingResult() { Amount = amount, Type = type, Date = DateTime.Now, OrderNumber = -1, Total = rate * amount };
            res.Trades.Add(new TradeEntry() { Amount = amount, Date = DateTime.Now, Id = "demo", Rate = rate, Total = rate * amount, Type = type });
            return res;
        }

        protected virtual TradingResult MarketBuy(double rate, double amount) {
            TradingResult res = null;
            if(!DemoMode) {
                res = Ticker.Buy(rate, amount);
                if(res == null)
                    Log(LogType.Error, "", rate, amount, StrategyOperation.MarketBuy);
                return res;
            }
            else {
                res = AddDemoTradingResult(rate, amount, OrderType.Buy);
            }
            Log(LogType.Success, "", rate, amount, StrategyOperation.MarketBuy);
            return res;
        }
        protected virtual TradingResult MarketSell(double rate, double amount) {
            TradingResult res = null;
            if(!DemoMode) {
                res = Ticker.Sell(rate, amount);
                if(res == null)
                    Log(LogType.Error, "", rate, amount, StrategyOperation.MarketSell);
                return res;
            }
            else {
                res = AddDemoTradingResult(rate, amount, OrderType.Sell);
            }
            Log(LogType.Success, "", rate, amount, StrategyOperation.MarketSell);
            return res;
        }
        protected virtual TradingResult PlaceBid(double rate, double amount) {
            TradingResult res = null;
            if(!DemoMode) {
                res = Ticker.Buy(rate, amount);
                if(res == null) 
                    Log(LogType.Error, "", rate, amount, StrategyOperation.LimitBuy);
                return res;
            }
            else {
                res = AddDemoTradingResult(rate, amount, OrderType.Buy);
            }
            Log(LogType.Success, "", rate, amount, StrategyOperation.LimitBuy);
            return res;
        }
        protected virtual TradingResult PlaceAsk(double rate, double amount) {
            TradingResult res = null;
            if(!DemoMode) {
                res = Ticker.Sell(rate, amount);
                if(res == null)
                    Log(LogType.Error, "", rate, amount, StrategyOperation.LimitSell);
                return res;
            }
            else
                res = AddDemoTradingResult(rate, amount, OrderType.Sell);
            Log(LogType.Error, "", rate, amount, StrategyOperation.LimitSell);
            return res;
        }
        public override List<StrategyValidationError> Validate() {
            List<StrategyValidationError> list = base.Validate();
            CheckTickerSpecified(list);
            CheckAccountMatchExchange(list);
            return list;
        }
        void CheckTickerSpecified(List<StrategyValidationError> list) {
            if(TickerInfo == null || string.IsNullOrEmpty(TickerInfo.Ticker))
                list.Add(new StrategyValidationError() { DataObject = this, Description = string.Format("Ticker not specified.", GetTickerName()), PropertyName = "Ticker", Value = GetTickerName() });
            else if(Ticker == null)
                list.Add(new StrategyValidationError() { DataObject = this, Description = string.Format("Ticker {0} not found.", GetTickerName()), PropertyName = "Ticker", Value = GetTickerName() });
        }

        string GetTickerName() {
            return TickerInfo == null ? "[empty]" : TickerInfo.ToString();    
        }
        protected void CheckAccountMatchExchange(List<StrategyValidationError> list) {
            if(Account == null || TickerInfo == null)
                return;
            if(TickerInfo.Exchange != Account.Exchange.Type)
                list.Add(new StrategyValidationError() { DataObject = this, Description = string.Format("Ticker's exchange {0} does not match account's {1}.", TickerInfo.Exchange, Account.Exchange.Type), PropertyName = "Ticker", Value = GetTickerName() });
        }
    }
}
