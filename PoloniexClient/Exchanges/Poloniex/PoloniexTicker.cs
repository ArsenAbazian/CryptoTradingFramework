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

namespace CryptoMarketClient {
    public class PoloniexTicker : Ticker {
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

        
        public override string HostName { get { return "Poloniex"; } }
        public override double Fee { get { return 0.25 * 0.01; } set { } }
        public override string Name { get { return CurrencyPair; } }
        public override string MarketName { get { return CurrencyPair; } set { } }

        public override string WebPageAddress { get { return "https://poloniex.com/exchange#" + Name.ToLower(); } }
        //public override string GetDepositAddress(CurrencyType type) {
        //    if(type == CurrencyType.BaseCurrency) {
        //        if(!string.IsNullOrEmpty(BaseBalanceInfo.DepositAddress))
        //            return BaseBalanceInfo.DepositAddress;
        //        return PoloniexExchange.Default.CreateDeposit(BaseCurrency);
        //    }
        //    if(!string.IsNullOrEmpty(MarketBalanceInfo.DepositAddress))
        //        return MarketBalanceInfo.DepositAddress;
        //    return PoloniexExchange.Default.CreateDeposit(MarketCurrency);
        //}
    }
}
