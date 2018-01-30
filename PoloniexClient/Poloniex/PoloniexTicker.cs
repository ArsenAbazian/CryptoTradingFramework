using CryptoMarketClient.Common;
using CryptoMarketClient.Poloniex;
using CryptoMarketClient.Strategies;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using WampSharp.V2;

namespace CryptoMarketClient {
    public class PoloniexTicker : TickerBase {
        public PoloniexTicker(PoloniexExchange exchange) : base(exchange) {
            CandleStickPeriodMin = 5;
        }

        public int Id { get; set; }

        string currensyPair;
        public override string CurrencyPair {
            get { return currensyPair; }
            set {
                currensyPair = value;
                OnCurrencyPairChanged();
            }
        }
        void OnCurrencyPairChanged() {
            string[] curr = CurrencyPair.Split('_');
            BaseCurrency = curr[0];
            MarketCurrency = curr[1];
        }

        BalanceBase firstInfo, secondInfo;
        public override BalanceBase BaseBalanceInfo {
            get {
                if(firstInfo == null)
                    firstInfo = Exchange.Balances.FirstOrDefault((b) => b.Currency == BaseCurrency);
                return firstInfo;
            }
        }
        public override BalanceBase MarketBalanceInfo {
            get {
                if(secondInfo == null)
                    secondInfo = Exchange.Balances.FirstOrDefault((b) => b.Currency == MarketCurrency);
                return secondInfo;
            }
        }

        PoloniexCurrencyInfo marketCurrencyInfo;
        protected PoloniexCurrencyInfo MarketCurrencyInfo {
            get {
                if(marketCurrencyInfo == null)
                    marketCurrencyInfo = PoloniexExchange.Default.Currencies.FirstOrDefault(c => c.Currency == MarketCurrency);
                return marketCurrencyInfo;
            }
        }

        public override double BaseCurrencyBalance { get { return BaseBalanceInfo == null? 0: BaseBalanceInfo.Available; } }
        public override double MarketCurrencyBalance { get { return MarketBalanceInfo == null? 0: MarketBalanceInfo.Available; } }
        public override double MarketCurrencyTotalBalance { get { return MarketBalanceInfo == null ? 0 : MarketBalanceInfo.OnOrders + MarketBalanceInfo.Available; } }
        public override bool MarketCurrencyEnabled { get { return MarketCurrencyInfo == null ? false : !MarketCurrencyInfo.Disabled; } }
        public override string HostName { get { return "Poloniex"; } }
        public override double Fee { get { return 0.25 * 0.01; } set { } }
        public override string Name { get { return CurrencyPair; } }
        public override string MarketName { get { return CurrencyPair; } set { } }

        public override string WebPageAddress { get { return "https://poloniex.com/exchange#" + Name.ToLower(); } }
        public override bool Buy(double rate, double amount) {
            return PoloniexExchange.Default.BuyLimit(this, rate, amount);
        }
        public override bool Sell(double rate, double amount) {
            return PoloniexExchange.Default.SellLimit(this, rate, amount) != -1;
        }
        public override bool MarketSell(double amount) {
            return PoloniexExchange.Default.SellLimit(this, Hr24Low / 2, amount) != -1;
        }
        public override bool UpdateBalance(CurrencyType type) {
            return PoloniexExchange.Default.GetBalance(type == CurrencyType.BaseCurrency? BaseCurrency: MarketCurrency);
        }
        public override string GetDepositAddress(CurrencyType type) {
            if(type == CurrencyType.BaseCurrency) {
                if(!string.IsNullOrEmpty(BaseBalanceInfo.DepositAddress))
                    return BaseBalanceInfo.DepositAddress;
                return PoloniexExchange.Default.CreateDeposit(BaseCurrency);
            }
            if(!string.IsNullOrEmpty(MarketBalanceInfo.DepositAddress))
                return MarketBalanceInfo.DepositAddress;
            return PoloniexExchange.Default.CreateDeposit(MarketCurrency);
        }
        public override bool Withdraw(string currency, string address, double amount) {
            return PoloniexExchange.Default.Withdraw(currency, amount, address, "");
        }
    }
}
