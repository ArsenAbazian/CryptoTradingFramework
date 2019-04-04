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
        
        public override string HostName => "Binance";

        public override string WebPageAddress => "https://www.binance.com/trade.html?symbol=" + MarketCurrency + "_" + BaseCurrency;
    }
}
