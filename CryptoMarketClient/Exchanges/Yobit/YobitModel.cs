using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Net;
using System.Collections.Specialized;
using System.Diagnostics;
using CryptoMarketClient.Common;
using System.Windows.Forms;

namespace CryptoMarketClient.Yobit {
    public class YobitExchange : Exchange {
        static YobitExchange defaultExchange;
        public static YobitExchange Default {
            get {
                if(defaultExchange == null) {
                    defaultExchange = new YobitExchange();
                    defaultExchange.Load();
                }
                return defaultExchange;
            }
        }

        public override void OnAccountRemoved(ExchangeAccountInfo info) {
            
        }

        public override ExchangeType Type => ExchangeType.Yobit;

        public override bool SupportWebSocket(WebSocketType type) {
            return false;
        }

        public override bool GetDeposites() {
            return true;
        }

        public override Form CreateAccountForm() {
            return new AccountBalancesForm(this);
        }

        public override void ObtainExchangeSettings() {
            
        }

        public override void StartListenTickersStream() { }

        public override void StopListenTickersStream() { }

        public override bool CancelOrder(TickerBase ticker, OpenedOrderInfo info) {
            throw new NotImplementedException();
        }

        public override bool AllowCandleStickIncrementalUpdate => false;

        public List<YobitCurrencyInfo> Currencies { get; } = new List<YobitCurrencyInfo>();

        public override List<CandleStickIntervalInfo> GetAllowedCandleStickIntervals() {
            return new List<CandleStickIntervalInfo>();
        }

        public override BindingList<CandleStickData> GetCandleStickData(TickerBase ticker, int candleStickPeriodMin, DateTime start, long periodInSeconds) {
            string address = string.Format("https://yobit.net/ajax/system_chart.php");
            NameValueCollection coll = new NameValueCollection();
            coll.Add("method", "chart");
            coll.Add("nonce", DateTime.Now.Ticks.ToString());
            coll.Add("pair_id", "40001");
            coll.Add("mode", "12");

            WebClient client = GetWebClient();
            try {
                byte[] data = client.UploadValues(address, coll);
                string text = System.Text.Encoding.ASCII.GetString(data);
                if(string.IsNullOrEmpty(text))
                    return null;
                JObject res = (JObject)JsonConvert.DeserializeObject(text);
                JArray items = res.Value<JArray>("data");
                BindingList<CandleStickData> list = new BindingList<CandleStickData>();
                foreach(JArray item in items) {
                    CandleStickData c = new CandleStickData();
                    long seconds = item[0].Value<long>();
                    c.Time = new DateTime(1970, 1, 1).AddMilliseconds(seconds);
                    c.Volume = item[1].Value<double>();
                    c.QuoteVolume = item[2].Value<double>();
                    c.Open = item[3].Value<double>();
                    c.High = item[4].Value<double>();
                    c.Low = item[5].Value<double>();
                    c.Close = item[6].Value<double>();
                    list.Add(c);
                }
                return list;
            }
            catch(Exception e) {
                Debug.WriteLine("get candlestcik history exception: " + e.ToString());
                return null;
            }
        }

        public override bool GetTickersInfo() {
            string address = "https://yobit.net/api/3/info";
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
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            int index = 0;
            res = res.Value<JObject>("pairs");
            foreach(JProperty prop in res.Children()) {
                YobitTicker t = new YobitTicker(this);
                t.Index = index;
                t.CurrencyPair = prop.Name;
                JObject obj = (JObject)prop.Value;
                t.Fee = obj.Value<double>("fee");
                t.IsFrozen = obj.Value<int>("hidden") == 1;
                Tickers.Add(t);
                index++;
            }
            IsInitialized = true;
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
            return true;
        }

        public override bool UpdateOpenedOrders(TickerBase tickerBase) {
            return true;
        }

        public override bool UpdateOrderBook(TickerBase ticker) {
            string address = string.Format("https://yobit.net/api/3/depth/{0}?depth={1}",
                Uri.EscapeDataString(ticker.CurrencyPair), OrderBook.Depth);
            string text = ((TickerBase)ticker).DownloadString(address);
            if(string.IsNullOrEmpty(text))
                return false;

            Dictionary<string, object> res2 = null;
            lock(JsonParser) {
                res2 = JsonParser.Parse(text) as Dictionary<string, object>;
                if(res2 == null)
                    return false;
            }
            res2 = (Dictionary<string, object>)res2[ticker.CurrencyPair];
            List<object> bids = (List<object>)res2["bids"];
            List<object> asks = (List<object>)res2["asks"];

            ticker.OrderBook.GetNewBidAsks();
            int index = 0;
            OrderBookEntry[] list = ticker.OrderBook.Bids;
            foreach(List<object> item in bids) {
                OrderBookEntry entry = list[index];
                entry.ValueString = (string)item.First();
                entry.AmountString = (string)item.Last();
                index++;
                if(index >= list.Length)
                    break;
            }
            index = 0;
            list = ticker.OrderBook.Asks;
            foreach(List<object> item in asks) {
                OrderBookEntry entry = list[index];
                entry.ValueString = (string)item.First();
                entry.AmountString = (string)item.Last();
                index++;
                if(index >= list.Length)
                    break;
            }

            ticker.OrderBook.UpdateEntries();
            ticker.OrderBook.RaiseOnChanged(new OrderBookUpdateInfo() { Action = OrderBookUpdateType.RefreshAll });
            return true;
        }

        public override bool UpdateTicker(TickerBase tickerBase) {
            string address = "https://yobit.net/api/3/ticker/" + tickerBase.CurrencyPair;
            string text = string.Empty;
            try {
                text = GetDownloadString(address);
            }
            catch(Exception) {
                return false;
            }
            if(string.IsNullOrEmpty(text))
                return false;
           
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            res = res.Value<JObject>(tickerBase.CurrencyPair);
            tickerBase.Hr24High = res.Value<double>("high");
            tickerBase.Hr24Low = res.Value<double>("low");
            tickerBase.Volume = res.Value<double>("vol");
            tickerBase.BaseVolume = res.Value<double>("vol_cur");
            tickerBase.Last = res.Value<double>("last");
            tickerBase.HighestBid = res.Value<double>("buy");
            tickerBase.LowestAsk = res.Value<double>("ask");
            tickerBase.Time = DateTime.UtcNow;


            return true;
        }

        public override bool UpdateTickersInfo() {
            return true;
        }

        public override bool UpdateTrades(TickerBase ticker) {
            string address = string.Format("https://yobit.net/api/3/trades/{0}",
                Uri.EscapeDataString(ticker.CurrencyPair));
            string text = ((TickerBase)ticker).DownloadString(address);
            if(string.IsNullOrEmpty(text))
                return false;

            JObject obj2 = (JObject)JsonConvert.DeserializeObject(text);
            JArray trades = (JArray)obj2.Value<JArray>(ticker.CurrencyPair);
            if(trades.Count == 0)
                return true;

            int lastTradeId = trades.First().Value<int>("tid");
            long lastGotTradeId = ticker.TradeHistory.Count > 0 ? ticker.TradeHistory.First().Id : 0;
            if(lastGotTradeId == lastTradeId) {
                ticker.TradeStatistic.Add(new TradeStatisticsItem() { Time = DateTime.UtcNow });
                if(ticker.TradeStatistic.Count > 5000) {
                    for(int i = 0; i < 100; i++)
                        ticker.TradeStatistic.RemoveAt(0);
                }
                return true;
            }
            TradeStatisticsItem st = new TradeStatisticsItem();
            st.MinBuyPrice = double.MaxValue;
            st.MinSellPrice = double.MaxValue;
            st.Time = DateTime.UtcNow;

            int index = 0;
            foreach(JObject obj in trades) {
                DateTime time = new DateTime(1970, 1, 1).AddSeconds(obj.Value<long>("timestamp"));
                int tradeId = obj.Value<int>("tid");
                if(lastGotTradeId == tradeId)
                    break;

                TradeHistoryItem item = new TradeHistoryItem();

                bool isBuy = obj.Value<string>("type") == "bid";
                item.AmountString = obj.Value<string>("amount");
                item.Time = time;
                item.Type = isBuy ? TradeType.Buy : TradeType.Sell;
                item.RateString = obj.Value<string>("price");
                item.Id = tradeId;
                double price = item.Rate;
                double amount = item.Amount;
                item.Total = price * amount;
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
                ticker.TradeHistory.Insert(index, item);
                index++;
            }
            if(st.MinSellPrice == double.MaxValue)
                st.MinSellPrice = 0;
            if(st.MinBuyPrice == double.MaxValue)
                st.MinBuyPrice = 0;
            ticker.LastTradeStatisticTime = DateTime.UtcNow;
            ticker.TradeStatistic.Add(st);
            if(ticker.TradeStatistic.Count > 5000) {
                for(int i = 0; i < 100; i++)
                    ticker.TradeStatistic.RemoveAt(0);
            }
            return true;
        }
    }
}
