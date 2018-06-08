using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoMarketClient.Common;

namespace CryptoMarketClient.BitFinex {
    public class BitFinexTicker : TickerBase {
        public BitFinexTicker(BitFinexExchange exchange) : base(exchange) { }

        public override string CurrencyPair { get; set; }

        public override string Name => CurrencyPair;

        public override double Fee { get { return 0.001; } set { } }

        public override double BaseCurrencyBalance => 0;

        public override double MarketCurrencyBalance => 0;

        public override BalanceBase BaseBalanceInfo => throw new NotImplementedException();

        public override BalanceBase MarketBalanceInfo => throw new NotImplementedException();

        public override double MarketCurrencyTotalBalance => throw new NotImplementedException();

        public override bool MarketCurrencyEnabled => throw new NotImplementedException();

        public override string HostName => "BitFinex";

        public override string WebPageAddress => "https://www.bitfinex.com/t/" + MarketCurrency + ":" + BaseCurrency;

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
            //throw new NotImplementedException();
        }

        public override bool Withdraw(string currency, string address, double amount) {
            throw new NotImplementedException();
        }

        public override bool UpdateMyTrades() {
            return true;
        }
    }
}
