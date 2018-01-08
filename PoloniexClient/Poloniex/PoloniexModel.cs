using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CryptoMarketClient.Common;
using CryptoMarketClient.Poloniex;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoMarketClient {
    public class PoloniexExchange : Exchange {
        //const string PoloniexServerAddress = "wss://api.poloniex.com";

        static PoloniexExchange defaultExchange;
        public static PoloniexExchange Default {
            get {
                if (defaultExchange == null) {
                    defaultExchange = new PoloniexExchange();
                    defaultExchange.Load();
                }
                return defaultExchange;
            }
        }

        public PoloniexExchange() {
            Balances = new PoloniexAccountBalanceInfoList();
        }

        public override string Name => "Poloniex";
        public PoloniexCurrencyInfoList Currencies { get; } = new PoloniexCurrencyInfoList();
        public new PoloniexAccountBalanceInfoList Balances { get { return (PoloniexAccountBalanceInfoList)base.Balances; } protected set { base.Balances = value; } }

        protected IDisposable TickersSubscriber { get; set; }
        protected List<TradeHistoryItem> UpdateList { get; } = new List<TradeHistoryItem>(100);

        public event TickerUpdateEventHandler TickerUpdate;
        public void Connect() {
            if (TickersSubscriber != null)
                return;
        }

        public IDisposable ConnectOrderBook(PoloniexTicker ticker) {
            return null;
        }

        public override BindingList<CandleStickData> GetCandleStickData(TickerBase ticker, int candleStickPeriodMin, DateTime start, int periodInSeconds) {
            int startSec = (Int32)(start.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            int end = startSec + periodInSeconds;

            string address = string.Format("https://poloniex.com/public?command=returnChartData&currencyPair={0}&period={1}&start={2}&end={3}",
                Uri.EscapeDataString(ticker.CurrencyPair), candleStickPeriodMin * 60, startSec, end);
            string text = string.Empty;
            try {
                text = GetDownloadString(address);
            } catch (Exception) {
                return null;
            }
            if (string.IsNullOrEmpty(text))
                return null;

            DateTime startTime = new DateTime(1970, 1, 1);

            BindingList<CandleStickData> list = new BindingList<CandleStickData>();
            JArray res = (JArray)JsonConvert.DeserializeObject(text);
            foreach (JObject item in res.Children()) {
                CandleStickData data = new CandleStickData();
                data.Time = startTime.AddSeconds(item.Value<long>("date"));
                data.Open = item.Value<double>("open");
                data.Close = item.Value<double>("close");
                data.High = item.Value<double>("high");
                data.Low = item.Value<double>("low");
                data.Volume = item.Value<double>("volume");
                data.QuoteVolume = item.Value<double>("quoteVolume");
                data.WeightedAverage = item.Value<double>("weightedAverage");
                list.Add(data);
            }
            return list;
        }

        public override bool UpdateCurrencies() {
            string address = "https://poloniex.com/public?command=returnCurrencies";
            string text = string.Empty;
            try {
                text = GetDownloadString(address);
            } catch (Exception) {
                return false;
            }
            if (string.IsNullOrEmpty(text))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            foreach (JProperty prop in res.Children()) {
                string currency = prop.Name;
                JObject obj = (JObject)prop.Value;
                PoloniexCurrencyInfo c = Currencies.FirstOrDefault(curr => curr.Currency == currency);
                if (c == null) {
                    c = new PoloniexCurrencyInfo();
                    c.Currency = currency;
                    c.MaxDailyWithdrawal = obj.Value<double>("maxDailyWithdrawal");
                    c.TxFee = obj.Value<double>("txFee");
                    c.MinConfirmation = obj.Value<double>("minConf");
                    Currencies.Add(c);
                }
                c.Disabled = obj.Value<int>("disabled") != 0;
            }
            return true;
        }

        public override bool GetCurrenciesInfo() {
            throw new NotImplementedException();
        }

        public override bool GetTickersInfo() {
            string address = "https://poloniex.com/public?command=returnTicker";
            string text = string.Empty;
            try {
                text = GetDownloadString(address);
            } catch (Exception) {
                return false;
            }
            if (string.IsNullOrEmpty(text))
                return false;
            Tickers.Clear();
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            int index = 0;
            foreach (JProperty prop in res.Children()) {
                PoloniexTicker t = new PoloniexTicker(this);
                t.Index = index;
                t.CurrencyPair = prop.Name;
                JObject obj = (JObject)prop.Value;
                t.Id = obj.Value<int>("id");
                t.Last = obj.Value<double>("last");
                t.LowestAsk = obj.Value<double>("lowestAsk");
                t.HighestBid = obj.Value<double>("highestBid");
                //t.Change = obj.Value<double>("percentChange");
                t.BaseVolume = obj.Value<double>("baseVolume");
                t.Volume = obj.Value<double>("quoteVolume");
                t.IsFrozen = obj.Value<int>("isFrozen") != 0;
                t.Hr24High = obj.Value<double>("high24hr");
                t.Hr24Low = obj.Value<double>("low24hr");
                Tickers.Add(t);
                index++;
            }
            return true;
        }

        public override bool UpdateTickersInfo() {
            if (Tickers.Count == 0)
                return false;
            string address = "https://poloniex.com/public?command=returnTicker";
            string text = string.Empty;
            try {
                text = GetDownloadString(address);
            } catch (Exception) {
                return false;
            }
            if (string.IsNullOrEmpty(text))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            foreach (JProperty prop in res.Children()) {
                PoloniexTicker t = (PoloniexTicker)Tickers.FirstOrDefault((i) => i.CurrencyPair == prop.Name);
                if (t == null)
                    continue;
                JObject obj = (JObject)prop.Value;
                t.Last = obj.Value<double>("last");
                t.LowestAsk = obj.Value<double>("lowestAsk");
                t.HighestBid = obj.Value<double>("highestBid");
                //t.Change = obj.Value<double>("percentChange");
                t.BaseVolume = obj.Value<double>("baseVolume");
                t.Volume = obj.Value<double>("quoteVolume");
                t.IsFrozen = obj.Value<int>("isFrozen") != 0;
                t.Hr24High = obj.Value<double>("high24hr");
                t.Hr24Low = obj.Value<double>("low24hr");
            }
            return true;
        }

        public bool UpdateArbitrageOrderBook(PoloniexTicker ticker, int depth) {
            string address = GetOrderBookString(ticker, depth);
            string text = ((TickerBase)ticker).DownloadString(address);
            return OnUpdateArbitrageOrderBook(ticker, text);
        }

        public override bool UpdateTicker(TickerBase tickerBase) {
            return true;
        }

        public bool OnUpdateArbitrageOrderBook(TickerBase ticker, string text) {
            if (string.IsNullOrEmpty(text))
                return false;

            Dictionary<string, object> res2 = null;
            lock (JsonParser) {
                res2 = JsonParser.Parse(text) as Dictionary<string, object>;
                if (res2 == null)
                    return false;
            }

            List<object> bids = (List<object>)res2["bids"];
            List<object> asks = (List<object>)res2["asks"];

            ticker.OrderBook.GetNewBidAsks();
            int index = 0;
            OrderBookEntry[] list = ticker.OrderBook.Bids;
            foreach (List<object> item in bids) {
                OrderBookEntry entry = list[index];
                entry.ValueString = (string)item.First();
                entry.AmountString = (string)item.Last();
                index++;
                if (index >= list.Length)
                    break;
            }
            index = 0;
            list = ticker.OrderBook.Asks;
            foreach (List<object> item in asks) {
                OrderBookEntry entry = list[index];
                entry.ValueString = (string)item.First();
                entry.AmountString = (string)item.Last();
                index++;
                if (index >= list.Length)
                    break;
            }

            ticker.OrderBook.UpdateEntries();
            ticker.OrderBook.RaiseOnChanged(new OrderBookUpdateInfo() { Action = OrderBookUpdateType.RefreshAll });
            return true;
        }

        public string GetOrderBookString(TickerBase ticker, int depth) {
            return string.Format("https://poloniex.com/public?command=returnOrderBook&currencyPair={0}&depth={1}",
                Uri.EscapeDataString(ticker.CurrencyPair), depth);
        }

        public override bool UpdateOrderBook(TickerBase ticker) {
            return GetOrderBook(ticker, OrderBook.Depth);
        }

        public override bool ProcessOrderBook(TickerBase tickerBase, string text) {
            UpdateOrderBook(tickerBase, text);
            return true;
        }

        public void UpdateOrderBook(TickerBase ticker, string text) {
            OnUpdateArbitrageOrderBook(ticker, text);
            ticker.OrderBook.RaiseOnChanged(new OrderBookUpdateInfo() { Action = OrderBookUpdateType.RefreshAll });
        }

        public bool GetOrderBook(TickerBase ticker, int depth) {
            string address = string.Format("https://poloniex.com/public?command=returnOrderBook&currencyPair={0}&depth={1}",
                Uri.EscapeDataString(ticker.CurrencyPair), depth);
            string text = ((TickerBase)ticker).DownloadString(address);
            if (string.IsNullOrEmpty(text))
                return false;
            UpdateOrderBook(ticker, text);
            return true;
        }

        public override bool UpdateTrades(TickerBase ticker) {
            string address = string.Format("https://poloniex.com/public?command=returnTradeHistory&currencyPair={0}", Uri.EscapeDataString(ticker.CurrencyPair));
            string text = GetDownloadString(address);
            if (string.IsNullOrEmpty(text))
                return true;
            JArray trades = (JArray)JsonConvert.DeserializeObject(text);
            if (trades.Count == 0)
                return true;

            int lastTradeId = trades.First().Value<int>("tradeID");
            long lastGotTradeId = ticker.TradeHistory.Count > 0 ? ticker.TradeHistory.First().Id : 0;
            if (lastGotTradeId == lastTradeId) {
                ticker.TradeStatistic.Add(new TradeStatisticsItem() { Time = DateTime.UtcNow });
                if (ticker.TradeStatistic.Count > 5000) {
                    for (int i = 0; i < 100; i++)
                        ticker.TradeStatistic.RemoveAt(0);
                }
                return true;
            }
            TradeStatisticsItem st = new TradeStatisticsItem();
            st.MinBuyPrice = double.MaxValue;
            st.MinSellPrice = double.MaxValue;
            st.Time = DateTime.UtcNow;

            int index = 0;
            foreach (JObject obj in trades) {
                DateTime time = obj.Value<DateTime>("date");
                int tradeId = obj.Value<int>("tradeID");
                if (lastGotTradeId == tradeId)
                    break;

                TradeHistoryItem item = new TradeHistoryItem();

                bool isBuy = obj.Value<string>("type").Length == 3;
                item.AmountString = obj.Value<string>("amount");
                item.Time = time;
                item.Type = isBuy ? TradeType.Buy : TradeType.Sell;
                item.RateString = obj.Value<string>("rate");
                item.Id = tradeId;
                double price = item.Rate;
                double amount = item.Amount;
                item.Total = price * amount;
                if (isBuy) {
                    st.BuyAmount += amount;
                    st.MinBuyPrice = Math.Min(st.MinBuyPrice, price);
                    st.MaxBuyPrice = Math.Max(st.MaxBuyPrice, price);
                    st.BuyVolume += amount * price;
                } else {
                    st.SellAmount += amount;
                    st.MinSellPrice = Math.Min(st.MinSellPrice, price);
                    st.MaxSellPrice = Math.Max(st.MaxSellPrice, price);
                    st.SellVolume += amount * price;
                }
                ticker.TradeHistory.Insert(index, item);
                index++;
            }
            if (st.MinSellPrice == double.MaxValue)
                st.MinSellPrice = 0;
            if (st.MinBuyPrice == double.MaxValue)
                st.MinBuyPrice = 0;
            ticker.LastTradeStatisticTime = DateTime.UtcNow;
            ticker.TradeStatistic.Add(st);
            Debug.WriteLine(ticker.TradeHistory[0].Rate + " " + ticker.TradeHistory[0].Amount);
            if (ticker.TradeStatistic.Count > 5000) {
                for (int i = 0; i < 100; i++)
                    ticker.TradeStatistic.RemoveAt(0);
            }
            return true;
        }

        public override bool UpdateMyTrades(TickerBase ticker) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "returnTradeHistory");
            coll.Add("nonce", DateTime.Now.Ticks.ToString());
            coll.Add("currencyPair", ticker.MarketName);

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", ApiKey);
            try {
                byte[] data = client.UploadValues(address, coll);
                return OnUpdateMyTrades(ticker, data);
            } catch (Exception e) {
                Debug.WriteLine("get my trade history exception: " + e.ToString());
                return false;
            }
        }

        public string ToQueryString(NameValueCollection nvc) {
            StringBuilder sb = new StringBuilder();

            foreach (string key in nvc.Keys) {
                if (string.IsNullOrEmpty(key))
                    continue;

                string[] values = nvc.GetValues(key);
                if (values == null)
                    continue;

                foreach (string value in values) {
                    if (sb.Length > 0)
                        sb.Append("&");
                    sb.AppendFormat("{0}={1}", Uri.EscapeDataString(key), Uri.EscapeDataString(value));
                }
            }

            return sb.ToString();
        }

        public override bool UpdateBalances() {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "returnCompleteBalances");
            coll.Add("nonce", DateTime.Now.Ticks.ToString());

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", ApiKey);
            try {
                return OnGetBalances(client.UploadValues(address, coll));
            } catch (Exception) {
                return false;
            }
        }

        public Task<byte[]> GetBalancesAsync() {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "returnCompleteBalances");
            coll.Add("nonce", DateTime.Now.Ticks.ToString());

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", ApiKey);
            return client.UploadValuesTaskAsync(address, coll);
        }

        public bool OnGetBalances(byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if (string.IsNullOrEmpty(text))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            lock (Balances) {
                foreach (JProperty prop in res.Children()) {
                    if (prop.Name == "error") {
                        Debug.WriteLine("OnGetBalances fails: " + prop.Value<string>());
                        return false;
                    }
                    JObject obj = (JObject)prop.Value;
                    PoloniexAccountBalanceInfo info = Balances[prop.Name];
                    if (info == null) {
                        info = new PoloniexAccountBalanceInfo(prop.Name);
                        Balances.Add(info);
                    }
                    info.LastAvailable = info.Available;
                    info.Available = obj.Value<double>("available");
                    info.OnOrders = obj.Value<double>("onOrders");
                    info.BtcValue = obj.Value<double>("btcValue");
                }
            }
            return true;
        }

        public bool GetDeposits() {
            Task<byte[]> task = GetDepositsAsync();
            task.Wait();
            return OnGetDeposits(task.Result);
        }

        public Task<byte[]> GetDepositsAsync() {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "returnDepositAddresses");
            coll.Add("nonce", DateTime.Now.Ticks.ToString());

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", ApiKey);
            try {
                return client.UploadValuesTaskAsync(address, coll);
            } catch (Exception e) {
                Debug.WriteLine("GetDeposites failed:" + e.ToString());
                return null;
            }
        }

        public bool OnGetDeposits(byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if (string.IsNullOrEmpty(text) || text == "[]")
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            lock (Balances) {
                foreach (JProperty prop in res.Children()) {
                    if (prop.Name == "error") {
                        Debug.WriteLine("OnGetDeposites fails: " + prop.Value<string>());
                        return false;
                    }
                    PoloniexAccountBalanceInfo info = (PoloniexAccountBalanceInfo)Balances.FirstOrDefault((a) => a.CurrencyTicker == prop.Name);
                    if (info == null)
                        continue;
                    info.DepositAddress = (string)prop.Value;
                }
            }
            return true;
        }

        public override bool UpdateOpenedOrders(TickerBase ticker) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "returnOpenOrders");
            coll.Add("nonce", DateTime.Now.Ticks.ToString());
            coll.Add("currencyPair", ticker.MarketName);

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", ApiKey);
            try {
                byte[] data = client.UploadValues(address, coll);
                return OnGetOpenedOrders(ticker, data);
            } catch (Exception) {
                return false;
            }
        }

        public Task<byte[]> GetOpenedOrders() {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "returnOpenOrders");
            coll.Add("nonce", DateTime.Now.Ticks.ToString());
            coll.Add("currencyPair", "all");

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", ApiKey);
            return client.UploadValuesTaskAsync(address, coll);
        }

        public bool OnGetOpenedOrders(TickerBase ticker, byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if (string.IsNullOrEmpty(text))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            lock (ticker.OpenedOrders) {
                ticker.OpenedOrders.Clear();
                foreach (JProperty prop in res.Children()) {
                    if (prop.Name == "error") {
                        Debug.WriteLine("OnGetOpenedOrders fails: " + prop.Value<string>());
                        return false;
                    }
                    JArray array = (JArray)prop.Value;
                    foreach (JObject obj in array) {
                        OpenedOrderInfo info = new OpenedOrderInfo();
                        info.Market = prop.Name;
                        info.OrderNumber = obj.Value<int>("orderNumber");
                        info.Type = obj.Value<string>("type") == "sell" ? OrderType.Sell : OrderType.Buy;
                        info.Value = obj.Value<double>("rate");
                        info.Amount = obj.Value<double>("amount");
                        info.Total = obj.Value<double>("total");
                        ticker.OpenedOrders.Add(info);
                    }
                }
            }
            return true;
        }

        public bool OnGetOpenedOrders(byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if (string.IsNullOrEmpty(text))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            lock (OpenedOrders) {
                OpenedOrders.Clear();
                foreach (JProperty prop in res.Children()) {
                    if (prop.Name == "error") {
                        Debug.WriteLine("OnGetOpenedOrders fails: " + prop.Value<string>());
                        return false;
                    }
                    JArray array = (JArray)prop.Value;
                    foreach (JObject obj in array) {
                        OpenedOrderInfo info = new OpenedOrderInfo();
                        info.Market = prop.Name;
                        info.OrderNumber = obj.Value<int>("orderNumber");
                        info.Type = obj.Value<string>("type") == "sell" ? OrderType.Sell : OrderType.Buy;
                        info.Value = obj.Value<double>("rate");
                        info.Amount = obj.Value<double>("amount");
                        info.Total = obj.Value<double>("total");
                        OpenedOrders.Add(info);
                    }
                }
            }
            return true;
        }

        public bool BuyLimit(PoloniexTicker ticker, double rate, double amount) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "buy");
            coll.Add("nonce", DateTime.Now.Ticks.ToString());
            coll.Add("currencyPair", ticker.CurrencyPair);
            coll.Add("rate", rate.ToString("0.########"));
            coll.Add("amount", amount.ToString("0.########"));

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", ApiKey);
            try {
                byte[] data = client.UploadValues(address, coll);
                return OnBuyLimit(ticker, data);
            } catch (Exception e) {
                Debug.WriteLine(e.ToString());
                return false;
            }
        }

        public long SellLimit(PoloniexTicker ticker, double rate, double amount) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "sell");
            coll.Add("nonce", DateTime.Now.Ticks.ToString());
            coll.Add("currencyPair", ticker.CurrencyPair);
            coll.Add("rate", rate.ToString("0.########"));
            coll.Add("amount", amount.ToString("0.########"));

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", ApiKey);
            try {
                byte[] data = client.UploadValues(address, coll);
                return OnSellLimit(data);
            } catch (Exception e) {
                Debug.WriteLine(e.ToString());
                return -1;
            }

        }

        public bool OnBuyLimit(TickerBase ticker, byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if (string.IsNullOrEmpty(text))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            TradingResult tr = new TradingResult();
            tr.OrderNumber = res.Value<long>("orderNumber");
            tr.Type = OrderType.Buy;
            JArray array = res.Value<JArray>("resultingTrades");
            foreach (JObject trade in array) {
                TradeEntry e = new TradeEntry();
                e.Amount = trade.Value<double>("amount");
                e.Date = trade.Value<DateTime>("date");
                e.Rate = trade.Value<double>("rate");
                e.Total = trade.Value<double>("total");
                e.Id = trade.Value<long>("tradeID");
                e.Type = trade.Value<string>("type").Length == 3 ? OrderType.Buy : OrderType.Sell;
                tr.Trades.Add(e);
            }
            tr.Calculate();
            ticker.Trades.Add(tr);
            return true;
        }

        public string OnCreateDeposit(string currency, byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if (string.IsNullOrEmpty(text))
                return null;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            if (res.Value<int>("success") != 1)
                return null;
            string deposit = res.Value<string>("response");
            PoloniexAccountBalanceInfo info = (PoloniexAccountBalanceInfo)Balances.FirstOrDefault(b => b.CurrencyTicker == currency);
            info.DepositAddress = deposit;
            return deposit;
        }

        public long OnSellLimit(byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if (string.IsNullOrEmpty(text))
                return -1;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            return res.Value<long>("orderNumber");
        }

        public Task<byte[]> CancelOrder(ulong orderId) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "cancelOrder");
            coll.Add("nonce", DateTime.Now.Ticks.ToString());
            coll.Add("orderNumber", orderId.ToString());

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", ApiKey);
            return client.UploadValuesTaskAsync(address, coll);
        }

        public bool OnCancel(byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if (string.IsNullOrEmpty(text))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            return res.Value<int>("success") == 1;
        }

        public Task<byte[]> WithdrawAsync(string currency, double amount, string addr, string paymentId) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "withdraw");
            coll.Add("nonce", DateTime.Now.Ticks.ToString());
            coll.Add("currency", currency);
            coll.Add("amount", amount.ToString("0.########"));
            coll.Add("address", address);
            if (!string.IsNullOrEmpty(paymentId))
                coll.Add("paymentId", paymentId);

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", ApiKey);
            return client.UploadValuesTaskAsync(address, coll);
        }

        public bool Withdraw(string currency, double amount, string addr, string paymentId) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "withdraw");
            coll.Add("nonce", DateTime.Now.Ticks.ToString());
            coll.Add("currency", currency);
            coll.Add("amount", amount.ToString("0.########"));
            coll.Add("address", address);
            if (!string.IsNullOrEmpty(paymentId))
                coll.Add("paymentId", paymentId);

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", ApiKey);
            try {
                byte[] data = client.UploadValues(address, coll);
                return OnWithdraw(data);
            } catch (Exception) {
                return false;
            }
        }

        public bool OnWithdraw(byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if (string.IsNullOrEmpty(text))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            return !string.IsNullOrEmpty(res.Value<string>("responce"));
        }

        public bool GetBalance(string str) {
            return UpdateBalances();
        }

        public string CreateDeposit(string currency) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "generateNewAddress");
            coll.Add("nonce", DateTime.Now.Ticks.ToString());
            coll.Add("currency", currency);

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", ApiKey);
            try {
                byte[] data = client.UploadValues(address, coll);
                return OnCreateDeposit(currency, data);
            } catch (Exception e) {
                Debug.WriteLine(e.ToString());
                return null;
            }
        }

        protected void RaiseTickerUpdate(PoloniexTicker t) {
            TickerUpdateEventArgs e = new TickerUpdateEventArgs() { Ticker = t };
            if (TickerUpdate != null)
                TickerUpdate(this, e);
            t.RaiseChanged();
        }

        bool OnUpdateMyTrades(TickerBase ticker, byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if (string.IsNullOrEmpty(text))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            lock (ticker.OpenedOrders) {
                ticker.OpenedOrders.Clear();
                foreach (JProperty prop in res.Children()) {
                    if (prop.Name == "error") {
                        Debug.WriteLine("OnGetOpenedOrders fails: " + prop.Value<string>());
                        return false;
                    }
                    JArray array = (JArray)prop.Value;
                    ticker.MyTradeHistory.Clear();
                    foreach (JObject obj in array) {
                        TradeHistoryItem info = new TradeHistoryItem();
                        info.OrderNumber = obj.Value<int>("orderNumber");
                        info.Time = obj.Value<DateTime>("date");
                        info.Type = obj.Value<string>("type") == "sell" ? TradeType.Sell : TradeType.Buy;
                        info.RateString = obj.Value<string>("rate");
                        info.AmountString = obj.Value<string>("amount");
                        info.Total = obj.Value<double>("total");
                        info.Fee = obj.Value<double>("fee");
                        ticker.MyTradeHistory.Add(info);
                    }
                }
            }
            return true;
        }

    }

    public delegate void TickerUpdateEventHandler(object sender, TickerUpdateEventArgs e);
    public class TickerUpdateEventArgs : EventArgs {
        public PoloniexTicker Ticker { get; set; }
    }
}
