using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.HitBtc {
    public class HitBtcModel : ModelBase {
        public override string Name => "HitBtc";

        static HitBtcModel defaultModel;
        public static HitBtcModel Default {
            get {
                if(defaultModel == null) {
                    defaultModel = new HitBtcModel();
                    defaultModel.Load();
                }
                return defaultModel;
            }
        }

        public BindingList<HitBtcCurrencyInfo> Currencies { get; } = new BindingList<HitBtcCurrencyInfo>();
        public BindingList<HitBtcBalanceInfo> Balances { get; } = new BindingList<HitBtcBalanceInfo>();
        public override bool GetTickersInfo() {
            string text = string.Empty;
            string address = "http://api.hitbtc.com/api/1/public/symbols";

            try {
                text = GetDownloadString(address);
            }
            catch(Exception) {
                return false;
            }

            Tickers.Clear();
            Dictionary<string, object> res = null;
            lock(JsonParser) {
                res = (Dictionary<string, object>)JsonParser.Parse(text);
            }
            List<object> symbols = (List<object>)res["symbols"];
            int index = 0;
            foreach(Dictionary<string, object> s in symbols) {
                HitBtcTicker t = new HitBtcTicker();
                t.Index = index;
                index++;
                t.BaseCurrency = s["currency"].ToString();
                t.MarketCurrency = s["commodity"].ToString();
                t.Step = Convert.ToDecimal(s["step"]);
                Tickers.Add(t);
            }

            return true;
        }
        public override bool UpdateTickersInfo() {
            string text = string.Empty;
            string address = "http://api.hitbtc.com/api/1/public/ticker";

            try {
                text = GetDownloadString(address);
                if(text == null)
                    return false;
            }
            catch(Exception) {
                return false;
            }

            Dictionary<string, object> res = null;
            lock(JsonParser) {
                res = (Dictionary<string, object>)JsonParser.Parse(text);
            }
            foreach(HitBtcTicker t in Tickers) {
                Dictionary<string, object> item = (Dictionary<string, object>)res[t.MarketName];
                string ask = (string)item["ask"];
                if(!string.IsNullOrEmpty(ask))
                    t.LowestAsk = Convert.ToDecimal(ask);
                string bid = (string)item["bid"];
                if(!string.IsNullOrEmpty(bid))
                    t.HighestBid = Convert.ToDecimal(bid);
                string last = (string)item["last"];
                if(!string.IsNullOrEmpty(last))
                    t.Last = Convert.ToDecimal(last);
                string low = (string)item["low"];
                if(!string.IsNullOrEmpty(low))
                    t.Hr24Low = Convert.ToDecimal(low);
                string high = (string)item["high"];
                if(!string.IsNullOrEmpty(high))
                    t.Hr24High = Convert.ToDecimal(high);
                string volume = (string)item["volume"];
                if(!string.IsNullOrEmpty(volume))
                    t.BaseVolume = Convert.ToDecimal(volume);
                string quoteVolume = (string)item["volume_quote"];
                if(!string.IsNullOrEmpty(quoteVolume))
                    t.Volume = Convert.ToDecimal(quoteVolume);
                string time = (string)item["timestamp"];
                if(!string.IsNullOrEmpty(time))
                    t.Time = new DateTime(Convert.ToInt64(time));
            }

            return true;
        }
        public bool UpdateArbitrageOrderBook(HitBtcTicker ticker, int depth) {
            string text = string.Empty;
            string address = string.Format("http://api.hitbtc.com//api/1/public/{0}/orderbook", ticker.MarketName);

            try {
                text = GetDownloadString(address);
                if(text == null)
                    return false;
            }
            catch(Exception) {
                return false;
            }

            Dictionary<string, object> res = null;
            lock(JsonParser) {
                res = (Dictionary<string, object>)JsonParser.Parse(text);
            }

            List<object> bids = (List<object>)res["bids"];
            List<object> asks = (List<object>)res["asks"];

            ticker.OrderBook.GetNewBidAsks();
            int index = 0;
            OrderBookEntry[] list = ticker.OrderBook.Bids;
            foreach(List<object> item in bids) {
                OrderBookEntry entry = list[index];
                entry.Value = (decimal)(double.Parse((string)item.First()));
                entry.Amount = (decimal)(double.Parse((string)item.Last()));
                index++;
                if(index >= list.Length)
                    break;
            }
            index = 0;
            list = ticker.OrderBook.Asks;
            foreach(List<object> item in asks) {
                OrderBookEntry entry = list[index];
                entry.Value = (decimal)(double.Parse((string)item.First()));
                entry.Amount = (decimal)(double.Parse((string)item.Last()));
                index++;
                if(index >= list.Length)
                    break;
            }

            ticker.OrderBook.UpdateEntries();
            ticker.OrderBook.RaiseOnChanged(new OrderBookUpdateInfo() { Action = OrderBookUpdateType.RefreshAll });
            return true;
        }
        public bool UpdateTradesStatistic(HitBtcTicker ticker, int count) {
            string text = string.Empty;
            string address = string.Format("http://api.hitbtc.com//api/1/public/{0}/trades?from={1}&by=trade_id&sort=desc&start_index=0&max_results={2}&side=true", ticker.MarketName, ticker.LastTradeId, count);

            try {
                text = GetDownloadString(address);
                if(text == null)
                    return false;
            }
            catch(Exception) {
                return false;
            }

            Dictionary<string, object> res = null;
            lock(JsonParser) {
                res = (Dictionary<string, object>)JsonParser.Parse(text);
            }
            List<object> trades = (List<object>)res["trades"];
            if(trades.Count == 0) {
                ticker.TradeStatistic.Add(new TradeStatisticsItem() { Time = DateTime.Now });
                if(ticker.TradeStatistic.Count > 5000) {
                    for(int i = 0; i < 100; i++)
                        ticker.TradeStatistic.RemoveAt(0);
                }
                return true;
            }

            TradeStatisticsItem st = new TradeStatisticsItem();
            st.MinBuyPrice = decimal.MaxValue;
            st.MinSellPrice = decimal.MaxValue;
            st.Time = DateTime.Now;

            foreach(List<object> obj in trades) {
                TradeHistoryItem item = new TradeHistoryItem();
                decimal amount = Convert.ToDecimal(obj[1]);
                decimal price = Convert.ToDecimal(obj[2]);
                bool isBuy = obj[4].ToString().Length == 3;
                if(isBuy) {
                    st.BuyAmount += amount;
                    st.MinBuyPrice = Math.Min(st.MinBuyPrice, price);
                    st.MaxBuyPrice = Math.Max(st.MaxBuyPrice, price);
                    st.BuyVolume += amount * price;
                }
                else {
                    st.SellAmount += amount;
                    st.MinSellPrice = Math.Min(st.MinSellPrice, price);
                    st.MaxSellPrice = Math.Max(st.MaxSellPrice, price);
                    st.SellVolume += amount * price;
                }
            }
            if(st.MinSellPrice == decimal.MaxValue)
                st.MinSellPrice = 0;
            if(st.MinBuyPrice == decimal.MaxValue)
                st.MinBuyPrice = 0;
            ticker.LastTradeId = Convert.ToInt64(((List<object>)trades.First())[0]);
            ticker.LastTradeStatisticTime = DateTime.Now;
            ticker.TradeStatistic.Add(st);
            if(ticker.TradeStatistic.Count > 5000) {
                for(int i = 0; i < 100; i++)
                    ticker.TradeStatistic.RemoveAt(0);
            }
            return true;
        }
        public static string CalculateSignature(string text, string secretKey) {
            using(var hmacsha512 = new HMACSHA512(Encoding.UTF8.GetBytes(secretKey))) {
                hmacsha512.ComputeHash(Encoding.UTF8.GetBytes(text));
                return string.Concat(hmacsha512.Hash.Select(b => b.ToString("x2")).ToArray()); // minimalistic hex-encoding and lower case
            }
        }
        private static long GetNonce() {
            return DateTime.Now.Ticks * 10 / TimeSpan.TicksPerMillisecond; // use millisecond timestamp or whatever you want
        }
        public override bool GetBalances() {
            string query = string.Format("/api/1/trading/balance?nonce={0}&apikey={1}", GetNonce(), ApiKey);

            var client = new RestClient("https://api.hitbtc.com");
            var request = new RestRequest("/api/1/trading/balance", Method.GET);
            request.AddParameter("nonce", GetNonce());
            request.AddParameter("apikey", ApiKey);

            string sign = CalculateSignature(client.BuildUri(request).PathAndQuery, ApiSecret);
            request.AddHeader("X-Signature", sign);

            try {
                var response = client.Execute(request);
                return OnGetBalances(response.Content);
            }
            catch(Exception) {
                return false;
            }
        }
        bool OnGetBalances(string text) {
            return true;
        }
    }
}
