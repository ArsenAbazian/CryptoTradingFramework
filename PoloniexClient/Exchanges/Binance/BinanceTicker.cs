using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoMarketClient.Common;

namespace CryptoMarketClient.Binance {
    public class BinanceTicker : Ticker {
        public BinanceTicker(BinanceExchange exchange) : base(exchange) { }

        string currensyPair;
        public override string CurrencyPair {
            get { return currensyPair; }
            set {
                currensyPair = value;
                OnCurrencyPairChanged();
            }
        }
        void OnCurrencyPairChanged() {
            BaseCurrency = CurrencyPair.Substring(3);
            MarketCurrency = CurrencyPair.Substring(0, 3);
        }

        public override string Name => CurrencyPair;

        public override double Fee { get { return 0.1f * 0.01f; } set { } }

        public override double BaseCurrencyBalance { get { return BaseBalanceInfo == null ? 0 : BaseBalanceInfo.Available; } }
        public override double MarketCurrencyBalance { get { return MarketBalanceInfo == null ? 0 : MarketBalanceInfo.Available; } }
        public override double MarketCurrencyTotalBalance { get { return MarketBalanceInfo == null ? 0 : MarketBalanceInfo.OnOrders + MarketBalanceInfo.Available; } }

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

        public override bool MarketCurrencyEnabled => throw new NotImplementedException();

        public override string HostName => "Binance";

        public override string WebPageAddress => "https://www.binance.com/trade.html?symbol=" + MarketCurrency + "_" + BaseCurrency;

        public override bool Buy(double lowestAsk, double amount) {
            throw new NotImplementedException();
        }

        public override string GetDepositAddress(CurrencyType type) {
            throw new NotImplementedException();
        }

        public override bool MarketBuy(double amount) {
            throw new NotImplementedException();
        }

        public override bool MarketSell(double amount) {
            throw new NotImplementedException();
        }

        public override bool Sell(double highestBid, double amount) {
            throw new NotImplementedException();
        }

        public override bool UpdateBalance(CurrencyType type) {
            return true;
        }

        public override bool Withdraw(string currency, string address, double amount) {
            throw new NotImplementedException();
        }
    }
}
