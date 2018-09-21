using CryptoMarketClient.Exchanges.Base;
using System;

namespace CryptoMarketClient.Bittrex {
    public class BittrexTicker : Ticker {
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

        public BittrexExchange Bittrex { get { return (BittrexExchange)Exchange; } }
        public override bool IsListeningOrderBook {
            get { return Bittrex.TickersSocketState == SocketConnectionState.Connected && IsOrderBookSubscribed; }
        }
        public override bool IsListeningTradingHistory {
            get { return Bittrex.TickersSocketState == SocketConnectionState.Connected && IsTradeHistorySubscribed; }
        }
        public override bool IsListeningKline {
            get { return Bittrex.TickersSocketState == SocketConnectionState.Connected && IsKlineSubscribed; }
        }

        public override string CurrencyPair { get { return MarketName; } set { MarketName = value; } }
        public override string Name { get { return MarketName; } }

        //public override bool UpdateBalance(CurrencyType type) {
        //    return BittrexExchange.Default.GetBalance(Exchange.DefaultAccount, type == CurrencyType.MarketCurrency? MarketCurrency: BaseCurrency);
        //}
        
        //public override string GetDepositAddress(CurrencyType type) {
        //    if(type == CurrencyType.BaseCurrency) {
        //        if(BaseBalanceInfo == null)
        //            return null;
        //        if(!string.IsNullOrEmpty(BaseBalanceInfo.DepositAddress))
        //            return BaseBalanceInfo.DepositAddress;
        //        return BittrexExchange.Default.CheckCreateDeposit(BaseCurrency);
        //    }
        //    if(MarketBalanceInfo == null)
        //        return null;
        //    if(!string.IsNullOrEmpty(MarketBalanceInfo.DepositAddress))
        //        return BaseBalanceInfo.DepositAddress;
        //    return BittrexExchange.Default.CheckCreateDeposit(MarketCurrency);
        //}
        
        public override string HostName { get { return "Bittrex"; } }
        public override string WebPageAddress { get { return "https://bittrex.com/Market/Index?MarketName=" + Name; } }
        public string TradeResult { get; set; }
    }

    public class BittrexCurrencyInfo : CurrencyInfoBase {
    }
}
