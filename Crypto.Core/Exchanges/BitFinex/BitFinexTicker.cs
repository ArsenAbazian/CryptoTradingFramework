using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crypto.Core.Common;

namespace Crypto.Core.BitFinex {
    public class BitFinexTicker : Ticker {
        public BitFinexTicker() : this(null) { }
        public BitFinexTicker(BitFinexExchange exchange) : base(exchange) {
        }

        protected string[] GetCurrencyPair(string name) {
            string[] items = null;

            name = name.Substring(1);
            if(name.Contains(':'))
                items = name.Split(':');
            else {
                items = new string[2];
                items[0] = name.Substring(0, 3);
                items[1] = name.Substring(3);
            }
            return items;
        }

        string currencyPair;
        public override string CurrencyPair {
            get { return currencyPair; }
            set {
                if(CurrencyPair == value)
                    return;
                currencyPair = value;
                OnCurrencyPairChanged();
                
            }
        }

        protected virtual void OnCurrencyPairChanged() {
            string[] items = GetCurrencyPair(CurrencyPair);
            BaseCurrency = items[1];
            MarketCurrency = items[0];
        }

        public override string Name => CurrencyPair;

        public override double Fee { get { return 0.001; } set { } }

        public override string WebPageAddress => "https://www.bitfinex.com/t/" + MarketCurrency + ":" + BaseCurrency;

        public int OrderBookSocketChannelId { get; internal set; }
        public int TradeHistorySocketChannelId { get; internal set; }
        public int KlineSocketChannelId { get; internal set; }

        public override bool UpdateAccountTrades() {
            return true;
        }
    }
}
