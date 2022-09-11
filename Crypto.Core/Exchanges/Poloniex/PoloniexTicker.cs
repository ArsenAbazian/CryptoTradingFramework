using System;

namespace Crypto.Core {
    public class PoloniexTicker : Ticker {
        public PoloniexTicker() : this(null) { }
        public PoloniexTicker(PoloniexExchange exchange) : base(exchange) {
            CandleStickPeriodMin = 5;
        }

        PoloniexExchange Poloniex { get { return (PoloniexExchange)Exchange; } }
        public override bool IsListeningOrderBook {
            get { return Poloniex != null && Poloniex.TickersSocketState == SocketConnectionState.Connected && IsOrderBookSubscribed; }
        }
        public override bool IsListeningTradingHistory {
            get { return Poloniex != null && Poloniex.TickersSocketState == SocketConnectionState.Connected && IsTradeHistorySubscribed; }
        }
        public override bool IsListeningKline {
            get { return Poloniex != null && Poloniex.TickersSocketState == SocketConnectionState.Connected && IsKlineSubscribed; }
        }

        public int Id { get; set; }
        string subscriptionName = null;
        public override string SubscriptionName {
            get {
                if(subscriptionName == null)
                    subscriptionName = string.Format("{0}_{1}", MarketCurrency, BaseCurrency);
                return subscriptionName;
            }
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
            BaseCurrency = curr[0];
            MarketCurrency = curr[1];
        }

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
