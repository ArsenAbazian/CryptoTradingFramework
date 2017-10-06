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
    public class BittrexMarketInfo : TickerBase {
        public int Index { get; set; }
        public string MarketCurrencyLong { get; set; }
        public string BaseCurrencyLong { get; set; }
        public decimal MinTradeSize { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public int OpenBuyOrders { get; set; }
        public int OpenSellOrders { get; set; }
        public decimal PrevDay { get; set; }
        public string DisplayMarketName { get; set; }
        public override decimal Fee { get { return 0.25m * 0.01m; } }

        public override decimal BaseCurrencyBalance { get { return BaseBalanceInfo == null ? 0 : BaseBalanceInfo.Available; } }
        public override decimal MarketCurrencyBalance { get { return MarketBalanceInfo == null ? 0 : MarketBalanceInfo.Available; } }
        public override decimal MarketCurrencyTotalBalance { get { return MarketBalanceInfo == null ? 0 : MarketBalanceInfo.Balance; } }
        public override bool MarketCurrencyEnabled { get { return MarketCurrencyInfo == null ? false : MarketCurrencyInfo.IsActive; } }

        
        BittrexAccountBalanceInfo baseBalanceInfo, marketBalanceInfo;
        protected BittrexAccountBalanceInfo BaseBalanceInfo {
            get {
                if(baseBalanceInfo == null)
                    baseBalanceInfo = BittrexModel.Default.Balances.FirstOrDefault((b) => b.Currency == BaseCurrency);
                return baseBalanceInfo;
            }
        }

        protected BittrexAccountBalanceInfo MarketBalanceInfo {
            get {
                if(marketBalanceInfo == null)
                    marketBalanceInfo = BittrexModel.Default.Balances.FirstOrDefault((b) => b.Currency == MarketCurrency);
                return marketBalanceInfo;
            }
        }

        BittrexCurrencyInfo marketCurrencyInfo;
        protected BittrexCurrencyInfo MarketCurrencyInfo {
            get {
                if(marketCurrencyInfo == null)
                    marketCurrencyInfo = BittrexModel.Default.Currencies.FirstOrDefault(c => c.Currency == MarketCurrency);
                return marketCurrencyInfo;
            }
        }

        public string MarketName { get; set; }
        public override string Name { get { return MarketName; } }

        public override void UpdateOrderBook(int depth) {
            BittrexModel.Default.UpdateArbitrageOrderBook(this, depth);
        }

        public override void UpdateTicker() {
            BittrexModel.Default.GetTicker(this);
        }
        public override void UpdateTrades() {
            BittrexModel.Default.UpdateTrades(this);
        }
        
        public override string DownloadString(string address) {
            try {
                ApiRate.WaitToProceed();
                return BittrexModel.Default.GetWebClient().DownloadString(address);
            }
            catch { }
            return string.Empty;
        }
        public override bool UpdateArbitrageOrderBook(int depth) {
            bool res = BittrexModel.Default.UpdateArbitrageOrderBook(this, depth);
            if(res) {
                HighestBid = OrderBook.Bids[0].Value;
                LowestAsk = OrderBook.Asks[0].Value;
                Time = DateTime.Now;
                UpdateHistoryItem();
            }
            return res;
        }
        public override void ProcessOrderBook(string text) {
            BittrexModel.Default.UpdateOrderBook(this, text, OrderBook.Depth);
        }
        public override void ProcessArbitrageOrderBook(string text) {
            BittrexModel.Default.UpdateOrderBook(this, text, OrderBook.Depth);
        }
        public override bool UpdateBalance(CurrencyType type) {
            return BittrexModel.Default.GetBalance(type == CurrencyType.MarketCurrency? MarketCurrency: BaseCurrency);
        }
        public override bool Buy(decimal rate, decimal amount) {
            return BittrexModel.Default.BuyLimit(this, rate, amount) != null;
        }
        public override bool Sell(decimal rate, decimal amount) {
            return BittrexModel.Default.SellLimit(this, rate, amount) != null;
        }
        public override string GetDepositAddress(CurrencyType type) {
            if(type == CurrencyType.BaseCurrency) {
                if(BaseBalanceInfo == null)
                    return null;
                if(!string.IsNullOrEmpty(BaseBalanceInfo.CryptoAddress))
                    return BaseBalanceInfo.CryptoAddress;
                return BittrexModel.Default.CheckCreateDeposit(BaseCurrency);
            }
            if(MarketBalanceInfo == null)
                return null;
            if(!string.IsNullOrEmpty(MarketBalanceInfo.CryptoAddress))
                return BaseBalanceInfo.CryptoAddress;
            return BittrexModel.Default.CheckCreateDeposit(MarketCurrency);
        }
        string GetCurrency(CurrencyType currencyType) {
            return currencyType == CurrencyType.BaseCurrency ? BaseCurrency : MarketCurrency;
        }
        public override bool Withdraw(string currency, string address, decimal amount) {
            return BittrexModel.Default.Withdraw(currency, amount, address, "");
        }
        public override bool UpdateTradeStatistic() {
            return BittrexModel.Default.UpdateTradesStatistic(this, 25);
        }
        public override string HostName { get { return "Bittrex"; } }
        public override string WebPageAddress { get { return "https://bittrex.com/Market/Index?MarketName=" + Name; } }
    }

    public class BittrexCurrencyInfo {
        public string Currency { get; set; }
        public string CurrencyLong { get; set; }
        public int MinConfirmation { get; set; }
        public decimal TxFree { get; set; }
        public bool IsActive { get; set; }
        public string CoinType { get; set; }
        public string BaseAddress { get; set; }
    }
}
