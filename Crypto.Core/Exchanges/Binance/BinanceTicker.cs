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


        public override bool IsListeningOrderBook {
            get { return IsOrderBookSubscribed && Exchange.GetOrderBookSocketState(this) == SocketConnectionState.Connected; }
        }
        public override bool IsListeningTradingHistory {
            get { return IsTradeHistorySubscribed && Exchange.GetTradingHistorySocketState(this) == SocketConnectionState.Connected; }
        }
        public override bool IsListeningKline {
            get { return IsTradeHistorySubscribed && Exchange.GetKlineSocketState(this) == SocketConnectionState.Connected; }
        }

        public override string Name => CurrencyPair;

        public override string MarketName { get { return Name; } set { } }

        public override double Fee { get { return 0.1f * 0.01f; } set { } }
        
        public override string HostName => "Binance";

        public override string WebPageAddress => "https://www.binance.com/trade.html?symbol=" + MarketCurrency + "_" + BaseCurrency;
    }
}
