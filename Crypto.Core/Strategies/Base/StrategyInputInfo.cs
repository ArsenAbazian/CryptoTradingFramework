using CryptoMarketClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Strategies {
    public class StrategyInputInfo {
        public List<ExchangeInputInfo> Exchanges { get; } = new List<ExchangeInputInfo>();
    }

    public class ExchangeInputInfo {
        public ExchangeType ExchangeType { get; set; }
        public Exchange Exchange { get; set; }
        public List<TickerInputInfo> Tickers { get; } = new List<TickerInputInfo>();
    } 

    public class TickerInputInfo {
        public TickerInputInfo() { }
        public TickerInputInfo(string currencyPair, bool needOrderBook, bool needTradeHistory, bool needKline) {
            TickerName = currencyPair;
            OrderBook = needOrderBook;
            TradeHistory = needTradeHistory;
            Kline = needKline;
        }
        public ExchangeType Exchange { get; set; }
        public Ticker Ticker { get; set; }
        public string TickerName { get; set; }
        public bool OrderBook { get; set; }
        public bool TradeHistory { get; set; }
        public bool Kline { get; set; }
        public int KlineIntervalMin { get; set; }
    }

    public class StrategyExpectedDataTemplate {

    }
}
