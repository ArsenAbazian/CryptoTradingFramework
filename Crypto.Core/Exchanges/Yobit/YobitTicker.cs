using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoMarketClient.Common;

namespace CryptoMarketClient.Yobit {
    public class YobitTicker : TickerBase {
        public YobitTicker(Exchange e) : base(e) {
            CandleStickPeriodMin = 10;
        }

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
            BaseCurrency = curr[1].ToUpper();
            MarketCurrency = curr[0].ToUpper();
        }

        public override string Name => CurrencyPair;

        public override double Fee { get; set; }

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

        YobitCurrencyInfo marketCurrencyInfo;
        protected YobitCurrencyInfo MarketCurrencyInfo {
            get {
                if(marketCurrencyInfo == null)
                    marketCurrencyInfo = ((YobitExchange)Exchange).Currencies.FirstOrDefault(c => c.Currency == MarketCurrency);
                return marketCurrencyInfo;
            }
        }

        public override double BaseCurrencyBalance { get { return BaseBalanceInfo == null ? 0 : BaseBalanceInfo.Available; } }
        public override double MarketCurrencyBalance { get { return MarketBalanceInfo == null ? 0 : MarketBalanceInfo.Available; } }
        public override double MarketCurrencyTotalBalance { get { return MarketBalanceInfo == null ? 0 : MarketBalanceInfo.OnOrders + MarketBalanceInfo.Available; } }
        public override bool MarketCurrencyEnabled { get { return MarketCurrencyInfo == null ? false : !MarketCurrencyInfo.Disabled; } }

        public override string HostName => "Youbit";

        public override string WebPageAddress => "https://yobit.net/en/trade/" + MarketCurrency + "/" + BaseCurrency;

        public override bool Buy(double lowestAsk, double amount) {
            throw new NotImplementedException();
        }

        public override bool Sell(double highestBid, double amount) {
            throw new NotImplementedException();
        }
        public override bool MarketSell(double amount) {
            throw new NotImplementedException();
        }
        public override bool MarketBuy(double amount) {
            throw new NotImplementedException();
        }

        public override string GetDepositAddress(CurrencyType type) {
            throw new NotImplementedException();
        }
        
        public override bool UpdateBalance(CurrencyType type) {
            throw new NotImplementedException();
        }

        public override bool Withdraw(string currency, string address, double amount) {
            throw new NotImplementedException();
        }
    }
}
