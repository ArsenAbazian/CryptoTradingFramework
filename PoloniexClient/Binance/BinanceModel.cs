using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoMarketClient.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoMarketClient.Binance {
    public class BinanceExchange : Exchange {
        static BinanceExchange defaultExchange;
        public static BinanceExchange Default {
            get {
                if(defaultExchange == null) {
                    defaultExchange = new BinanceExchange();
                    defaultExchange.Load();
                }
                return defaultExchange;
            }
        }

        public override bool AllowCandleStickIncrementalUpdate => false;

        public override string Name => "Binance";

        public override bool CancelOrder(TickerBase ticker, OpenedOrderInfo info) {
            throw new NotImplementedException();
        }

        public override List<CandleStickIntervalInfo> GetAllowedCandleStickIntervals() {
            throw new NotImplementedException();
        }

        public override bool GetTickersInfo() {
            string address = "https://api.binance.com/api/v1/ticker/24hr";
            string text = string.Empty;
            try {
                text = GetDownloadString(address);
            }
            catch(Exception) {
                return false;
            }
            if(string.IsNullOrEmpty(text))
                return false;
            Tickers.Clear();
            JArray res = (JArray)JsonConvert.DeserializeObject(text);
            int index = 0;
            foreach(JObject item in res) {
                BinanceTicker t = new BinanceTicker(this);
                t.Index = index;
                t.CurrencyPair = item.Value<string>("symbol");
                t.Last = item.Value<double>("lastPrice");
                t.LowestAsk = item.Value<double>("askPrice");
                t.HighestBid = item.Value<double>("bidPrice");
                t.Change = item.Value<double>("priceChangePercent");
                t.BaseVolume = item.Value<double>("volume");
                t.Volume = item.Value<double>("quoteVolume");
                t.Hr24High = item.Value<double>("highPrice");
                t.Hr24Low = item.Value<double>("lowPrice");
                Tickers.Add(t);
                index++;
            }
            return true;
        }

        public override List<TradeHistoryItem> GetTrades(TickerBase ticker, DateTime starTime) {
            throw new NotImplementedException();
        }

        public override bool ProcessOrderBook(TickerBase tickerBase, string text) {
            throw new NotImplementedException();
        }

        public override bool UpdateBalances() {
            throw new NotImplementedException();
        }

        public override bool UpdateCurrencies() {
            throw new NotImplementedException();
        }

        public override bool UpdateMyTrades(TickerBase ticker) {
            throw new NotImplementedException();
        }

        public override bool UpdateOpenedOrders(TickerBase tickerBase) {
            throw new NotImplementedException();
        }

        public override bool UpdateOrderBook(TickerBase tickerBase) {
            throw new NotImplementedException();
        }

        public override bool UpdateTicker(TickerBase tickerBase) {
            throw new NotImplementedException();
        }

        public override bool UpdateTickersInfo() {
            string address = "https://api.binance.com/api/v1/ticker/24hr";
            string text = string.Empty;
            try {
                text = GetDownloadString(address);
            }
            catch(Exception) {
                return false;
            }
            if(string.IsNullOrEmpty(text))
                return false;
            Tickers.Clear();
            JArray res = (JArray)JsonConvert.DeserializeObject(text);
            foreach(JObject item in res) {
                string currencyPair = item.Value<string>("symbol");
                BinanceTicker t = (BinanceTicker)Tickers.FirstOrDefault(tr => tr.CurrencyPair == currencyPair);
                if(t == null)
                    continue;
                t.Last = item.Value<double>("lastPrice");
                t.LowestAsk = item.Value<double>("askPrice");
                t.HighestBid = item.Value<double>("bidPrice");
                t.Change = item.Value<double>("priceChangePercent");
                t.BaseVolume = item.Value<double>("volume");
                t.Volume = item.Value<double>("quoteVolume");
                t.Hr24High = item.Value<double>("highPrice");
                t.Hr24Low = item.Value<double>("lowPrice");
            }
            return true;
        }

        public override bool UpdateTrades(TickerBase tickerBase) {
            throw new NotImplementedException();
        }
    }
}
