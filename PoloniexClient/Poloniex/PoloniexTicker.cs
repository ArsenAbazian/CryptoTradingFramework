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
        public int Index { get; set; }
        public int Id { get; set; }

        string currensyPair;
        public string CurrencyPair {
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

        PoloniexAccountBalanceInfo firstInfo, secondInfo;
        protected PoloniexAccountBalanceInfo FirstCurrencyBalanceInfo {
            get {
                if(firstInfo == null)
                    firstInfo = PoloniexModel.Default.Balances.FirstOrDefault((b) => b.Currency == BaseCurrency);
                return firstInfo;
            }
        }
        protected PoloniexAccountBalanceInfo SecondCurrencyBalanceInfo {
            get {
                if(secondInfo == null)
                    secondInfo = PoloniexModel.Default.Balances.FirstOrDefault((b) => b.Currency == MarketCurrency);
                return secondInfo;
            }
        }

        PoloniexCurrencyInfo marketCurrencyInfo;
        protected PoloniexCurrencyInfo MarketCurrencyInfo {
            get {
                if(marketCurrencyInfo == null)
                    marketCurrencyInfo = PoloniexModel.Default.Currencies.FirstOrDefault(c => c.Currency == MarketCurrency);
                return marketCurrencyInfo;
            }
        }

        public override decimal BaseCurrencyBalance { get { return FirstCurrencyBalanceInfo == null? 0: FirstCurrencyBalanceInfo.Available; } }
        public override decimal MarketCurrencyBalance { get { return SecondCurrencyBalanceInfo == null? 0: SecondCurrencyBalanceInfo.Available; } }
        public override decimal MarketCurrencyTotalBalance { get { return SecondCurrencyBalanceInfo == null ? 0 : SecondCurrencyBalanceInfo.OnOrders + SecondCurrencyBalanceInfo.Available; } }
        public override bool MarketCurrencyEnabled { get { return MarketCurrencyInfo == null ? false : !MarketCurrencyInfo.Disabled; } }
        public override string HostName { get { return "Poloniex"; } }
        public override decimal Fee { get { return 0.25m * 0.01m; } }
        public override string Name { get { return CurrencyPair; } }
        
        public override void UpdateOrderBook(int depth) {
            PoloniexModel.Default.GetOrderBook(this, depth);
        }
        //void TickerBase.SubscribeOrderBookUpdates() {
        //    UpdateHelper.SubscribeOrderBookUpdates();
        //}
        //void TickerBase.UnsubscribeOrderBookUpdates() {
        //    UpdateHelper.UnsubscribeOrderBookUpdates();
        //}
        //void TickerBase.SubscribeTickerUpdates() {
        //    PoloniexModel.Default.Connect();
        //    //UpdateHelper.SubscribeTickerUpdates();
        //}
        //void TickerBase.UnsubscribeTickerUpdates() {
        //    //UpdateHelper.UnsubscribeTickerUpdates();
        //}
        //void TickerBase.SubscribeTradeUpdates() {
        //    UpdateHelper.SubscribeTradeUpdates();
        //}
        //void TickerBase.UnsubscribeTradeUpdates() {
        //    UpdateHelper.UnsubscribeTradeUpdates();
        //}
        //void TickerBase.UpdateOrderBook() {
        //    PoloniexModel.Default.GetOrderBook(this, 50);
        //}
        public override void UpdateTicker() {
            PoloniexModel.Default.GetTicker(this);
        }
        public override void UpdateTrades() {
            PoloniexModel.Default.UpdateTrades(this);
        }
        public override string WebPageAddress { get { return "https://poloniex.com/exchange#" + Name.ToLower(); } }
        public override string DownloadString(string address) {
            try {
                ApiRate.WaitToProceed();
                return PoloniexModel.Default.GetWebClient().DownloadString(address);
            }
            catch { }
            return string.Empty;
        }
        public override bool UpdateArbitrageOrderBook(int depth) {
            bool res = PoloniexModel.Default.UpdateArbitrageOrderBook(this, depth);
            if(res) {
                HighestBid = OrderBook.Bids[0].Value;
                LowestAsk = OrderBook.Asks[0].Value;
                Time = DateTime.Now;
                UpdateHistoryItem();
            }
            return res;
        }
        public override void ProcessOrderBook(string text) {
            PoloniexModel.Default.UpdateOrderBook(this, text);
        }
        public override void ProcessArbitrageOrderBook(string text) {
            PoloniexModel.Default.OnUpdateArbitrageOrderBook(this, text);
        }
        public override bool Buy(decimal rate, decimal amount) {
            return PoloniexModel.Default.BuyLimit(this, rate, amount) != -1;
        }
        public override bool Sell(decimal rate, decimal amount) {
            return PoloniexModel.Default.SellLimit(this, rate, amount) != -1;
        }
        public override bool UpdateBalance(CurrencyType type) {
            return PoloniexModel.Default.GetBalance(type == CurrencyType.BaseCurrency? BaseCurrency: MarketCurrency);
        }
        public override string GetDepositAddress(CurrencyType type) {
            if(type == CurrencyType.BaseCurrency) {
                if(!string.IsNullOrEmpty(FirstCurrencyBalanceInfo.DepositAddress))
                    return FirstCurrencyBalanceInfo.DepositAddress;
                return PoloniexModel.Default.CreateDeposit(BaseCurrency);
            }
            if(!string.IsNullOrEmpty(SecondCurrencyBalanceInfo.DepositAddress))
                return SecondCurrencyBalanceInfo.DepositAddress;
            return PoloniexModel.Default.CreateDeposit(MarketCurrency);
        }
        public override bool Withdraw(string currency, string address, decimal amount) {
            return PoloniexModel.Default.Withdraw(currency, amount, address, "");
        }
        public override bool UpdateTradeStatistic() {
            return PoloniexModel.Default.UpdateTradesStatistic(this, 100);
        }
    }
}
