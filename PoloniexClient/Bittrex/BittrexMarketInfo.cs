using CryptoMarketClient.Common;
using CryptoMarketClient.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoMarketClient.Bittrex {
    public class BittrexTicker : TickerBase {
        public BittrexTicker(Exchange exchange) : base(exchange) { }

        public string MarketCurrencyLong { get; set; }
        public string BaseCurrencyLong { get; set; }
        public double MinTradeSize { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public int OpenBuyOrders { get; set; }
        public int OpenSellOrders { get; set; }
        public double PrevDay { get; set; }
        public string DisplayMarketName { get; set; }
        public override double Fee { get { return 0.25 * 0.01; } set { } }

        public override double BaseCurrencyBalance { get { return BaseBalanceInfo == null ? 0 : BaseBalanceInfo.Available; } }
        public override double MarketCurrencyBalance { get { return MarketBalanceInfo == null ? 0 : MarketBalanceInfo.Available; } }
        public override double MarketCurrencyTotalBalance { get { return MarketBalanceInfo == null ? 0 : MarketBalanceInfo.Available; } }
        public override bool MarketCurrencyEnabled { get { return MarketCurrencyInfo == null ? false : MarketCurrencyInfo.IsActive; } }

        
        BalanceBase baseBalanceInfo, marketBalanceInfo;
        public override BalanceBase BaseBalanceInfo {
            get {
                if(baseBalanceInfo == null)
                    baseBalanceInfo = BittrexExchange.Default.Balances.FirstOrDefault((b) => b.Currency == BaseCurrency);
                return baseBalanceInfo;
            }
        }

        public override BalanceBase MarketBalanceInfo {
            get {
                if(marketBalanceInfo == null)
                    marketBalanceInfo = BittrexExchange.Default.Balances.FirstOrDefault((b) => b.Currency == MarketCurrency);
                return marketBalanceInfo;
            }
        }

        BittrexCurrencyInfo marketCurrencyInfo;
        protected BittrexCurrencyInfo MarketCurrencyInfo {
            get {
                if(marketCurrencyInfo == null)
                    marketCurrencyInfo = BittrexExchange.Default.Currencies.FirstOrDefault(c => c.Currency == MarketCurrency);
                return marketCurrencyInfo;
            }
        }

        public override string CurrencyPair { get { return MarketName; } set { MarketName = value; } }
        public override string Name { get { return MarketName; } }

        public override bool UpdateBalance(CurrencyType type) {
            return BittrexExchange.Default.GetBalance(type == CurrencyType.MarketCurrency? MarketCurrency: BaseCurrency);
        }
        public override bool Buy(double rate, double amount) {
            return BittrexExchange.Default.BuyLimit(this, rate, amount) != null;
        }
        public override bool Sell(double rate, double amount) {
            return BittrexExchange.Default.SellLimit(this, rate, amount) != null;
        }
        public override bool MarketSell(double amount) {
            return BittrexExchange.Default.SellLimit(this, Hr24Low / 2, amount) != null;
        }
        public override string GetDepositAddress(CurrencyType type) {
            if(type == CurrencyType.BaseCurrency) {
                if(BaseBalanceInfo == null)
                    return null;
                if(!string.IsNullOrEmpty(BaseBalanceInfo.DepositAddress))
                    return BaseBalanceInfo.DepositAddress;
                return BittrexExchange.Default.CheckCreateDeposit(BaseCurrency);
            }
            if(MarketBalanceInfo == null)
                return null;
            if(!string.IsNullOrEmpty(MarketBalanceInfo.DepositAddress))
                return BaseBalanceInfo.DepositAddress;
            return BittrexExchange.Default.CheckCreateDeposit(MarketCurrency);
        }
        string GetCurrency(CurrencyType currencyType) {
            return currencyType == CurrencyType.BaseCurrency ? BaseCurrency : MarketCurrency;
        }
        public override bool Withdraw(string currency, string address, double amount) {
            return BittrexExchange.Default.Withdraw(currency, amount, address, "");
        }
        public override string HostName { get { return "Bittrex"; } }
        public override string WebPageAddress { get { return "https://bittrex.com/Market/Index?MarketName=" + Name; } }
    }

    public class BittrexCurrencyInfo {
        public string Currency { get; set; }
        public string CurrencyLong { get; set; }
        public int MinConfirmation { get; set; }
        public double TxFree { get; set; }
        public bool IsActive { get; set; }
        public string CoinType { get; set; }
        public string BaseAddress { get; set; }
    }
}
