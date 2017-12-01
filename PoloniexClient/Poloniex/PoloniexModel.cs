using CryptoMarketClient.Common;
using CryptoMarketClient.Poloniex;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reactive.Subjects;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WampSharp.Binding;
using WampSharp.V2;
using WampSharp.V2.Rpc;

namespace CryptoMarketClient {
    public class PoloniexExchange : Exchange {
        const string PoloniexServerAddress = "wss://api.poloniex.com";

        static PoloniexExchange defaultExchange;
        public static PoloniexExchange Default {
            get {
                if(defaultExchange == null) {
                    defaultExchange = new PoloniexExchange();
                    defaultExchange.Load();
                }
                return defaultExchange;
            }
        }

        public override string Name => "Poloniex";

        public List<PoloniexCurrencyInfo> Currencies { get; } = new List<PoloniexCurrencyInfo>();

        protected IDisposable TickersSubscriber { get; set; }
        public void Connect() {
            if(TickersSubscriber != null)
                return;
        }

        public event TickerUpdateEventHandler TickerUpdate;
        protected void RaiseTickerUpdate(PoloniexTicker t) {
            TickerUpdateEventArgs e = new TickerUpdateEventArgs() { Ticker = t };
            if(TickerUpdate != null)
                TickerUpdate(this, e);
            t.RaiseChanged();
        }
        public IDisposable ConnectOrderBook(PoloniexTicker ticker) {
            return null;
        }

        public override bool UpdateCurrencies() {
            string address = "https://poloniex.com/public?command=returnCurrencies";
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
            foreach(JProperty prop in res.Children()) {
                string currency = prop.Name;
                JObject obj = (JObject)prop.Value;
                PoloniexCurrencyInfo c = Currencies.FirstOrDefault(curr => curr.Currency == currency);
                if(c == null) {
                    c = new PoloniexCurrencyInfo();
                    c.Currency = currency;
                    c.MaxDailyWithdrawal = obj.Value<decimal>("maxDailyWithdrawal");
                    c.TxFee = obj.Value<decimal>("txFee");
                    c.MinConfirmation = obj.Value<decimal>("minConf");
                    Currencies.Add(c);
                }
                c.Disabled = obj.Value<int>("disabled") != 0;
            }
            return true;
        }

        public override bool GetTickersInfo() {
            string address = "https://poloniex.com/public?command=returnTicker";
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
            foreach(JProperty prop in res.Children()) {
                PoloniexTicker t = new PoloniexTicker(this);
                t.Index = index;
                t.CurrencyPair = prop.Name;
                JObject obj = (JObject)prop.Value;
                t.Id = obj.Value<int>("id");
                t.Last = obj.Value<decimal>("last");
                t.LowestAsk = obj.Value<decimal>("lowestAsk");
                t.HighestBid = obj.Value<decimal>("highestBid");
                t.Change = obj.Value<decimal>("percentChange");
                t.BaseVolume = obj.Value<decimal>("baseVolume");
                t.Volume = obj.Value<decimal>("quoteVolume");
                t.IsFrozen = obj.Value<int>("isFrozen") != 0;
                t.Hr24High = obj.Value<decimal>("high24hr");
                t.Hr24Low = obj.Value<decimal>("low24hr");
                Tickers.Add(t);
                index++;
            }
            return true;
        }
        public override bool UpdateTickersInfo() {
            if(Tickers.Count == 0)
                return false;
            string address = "https://poloniex.com/public?command=returnTicker";
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
            foreach(JProperty prop in res.Children()) {
                PoloniexTicker t = (PoloniexTicker)Tickers.FirstOrDefault((i) => i.CurrencyPair == prop.Name);
                if(t == null)
                    continue;
                JObject obj = (JObject)prop.Value;
                t.Last = obj.Value<decimal>("last");
                t.LowestAsk = obj.Value<decimal>("lowestAsk");
                t.HighestBid = obj.Value<decimal>("highestBid");
                t.Change = obj.Value<decimal>("percentChange");
                t.BaseVolume = obj.Value<decimal>("baseVolume");
                t.Volume = obj.Value<decimal>("quoteVolume");
                t.IsFrozen = obj.Value<int>("isFrozen") != 0;
                t.Hr24High = obj.Value<decimal>("high24hr");
                t.Hr24Low = obj.Value<decimal>("low24hr");
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
            if(string.IsNullOrEmpty(text))
                return false;

            Dictionary<string, object> res2 = null;
            lock(JsonParser) {
                res2 = JsonParser.Parse(text) as Dictionary<string, object>;
                if(res2 == null)
                    return false;
            }

            List<object> bids = (List<object>)res2["bids"];
            List<object> asks = (List<object>)res2["asks"];

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
            if(string.IsNullOrEmpty(text))
                return false;
            UpdateOrderBook(ticker, text);
            return true;
        }
        protected List<TradeHistoryItem> UpdateList { get; } = new List<TradeHistoryItem>(100);
        public override bool UpdateTrades(TickerBase ticker) {
            string address = string.Format("https://poloniex.com/public?command=returnTradeHistory&currencyPair={0}", Uri.EscapeDataString(ticker.CurrencyPair));
            string text = GetDownloadString(address);
            if(string.IsNullOrEmpty(text))
                return true;
            JArray trades = (JArray)JsonConvert.DeserializeObject(text);
            if(trades.Count == 0)
                return true;

            int lastTradeId = trades.First().Value<int>("tradeID");
            int lastGotTradeId = ticker.TradeHistory.Count > 0 ? ticker.TradeHistory.First().Id : 0;
            if(lastGotTradeId == lastTradeId) {
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
            
            int index = 0;
            foreach(JObject obj in trades) {
                DateTime time = obj.Value<DateTime>("date");
                int tradeId = obj.Value<int>("tradeID");
                if(lastGotTradeId == tradeId)
                    break;

                TradeHistoryItem item = new TradeHistoryItem();

                decimal amount = obj.Value<decimal>("amount");
                decimal price = obj.Value<decimal>("rate");
                bool isBuy = obj.Value<string>("type").Length == 3;
                item.Amount = amount;
                item.Time = time;
                item.Type = isBuy ? TradeType.Buy : TradeType.Sell;
                item.Rate = price;
                item.Id = tradeId;
                item.Total = item.Amount * item.Rate;
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
            if(st.MinSellPrice == decimal.MaxValue)
                st.MinSellPrice = 0;
            if(st.MinBuyPrice == decimal.MaxValue)
                st.MinBuyPrice = 0;
            ticker.LastTradeStatisticTime = DateTime.Now;
            ticker.TradeStatistic.Add(st);
            Debug.WriteLine(ticker.TradeHistory[0].Rate + " " + ticker.TradeHistory[0].Amount);
            if(ticker.TradeStatistic.Count > 5000) {
                for(int i = 0; i < 100; i++)
                    ticker.TradeStatistic.RemoveAt(0);
            }
            return true;
        }
        public string ToQueryString(NameValueCollection nvc) {
            StringBuilder sb = new StringBuilder();

            foreach(string key in nvc.Keys) {
                if(string.IsNullOrEmpty(key)) continue;

                string[] values = nvc.GetValues(key);
                if(values == null) continue;

                foreach(string value in values) {
                    if(sb.Length > 0) sb.Append("&");
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
            }
            catch(Exception) {
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
            if(string.IsNullOrEmpty(text))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            lock(Balances) {
                foreach(JProperty prop in res.Children()) {
                    if(prop.Name == "error") {
                        Debug.WriteLine("OnGetBalances fails: " + prop.Value<string>());
                        return false;
                    }
                    JObject obj = (JObject)prop.Value;
                    PoloniexAccountBalanceInfo info = (PoloniexAccountBalanceInfo)Balances.FirstOrDefault(b => b.Currency == prop.Name);
                    if(info == null) {
                        info = new PoloniexAccountBalanceInfo();
                        info.Currency = prop.Name;
                        Balances.Add(info);
                    }
                    info.Currency = prop.Name;
                    info.LastAvailable = info.Available;
                    info.Available = obj.Value<decimal>("available");
                    info.OnOrders = obj.Value<decimal>("onOrders");
                    info.BtcValue = obj.Value<decimal>("btcValue");
                }
            }
            return true;
        }

        public bool GetDeposites() {
            Task<byte[]> task = GetDepositesAsync();
            task.Wait();
            return OnGetDeposites(task.Result);
        }
        public Task<byte[]> GetDepositesAsync() {
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
            }
            catch(Exception e) {
                Debug.WriteLine("GetDeposites failed:" + e.ToString());
                return null;
            }
        }
        public bool OnGetDeposites(byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if(string.IsNullOrEmpty(text) || text == "[]")
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            lock(Balances) {
                foreach(JProperty prop in res.Children()) {
                    if(prop.Name == "error") {
                        Debug.WriteLine("OnGetDeposites fails: " + prop.Value<string>());
                        return false;
                    }
                    PoloniexAccountBalanceInfo info = (PoloniexAccountBalanceInfo)Balances.FirstOrDefault((a) => a.Currency == prop.Name);
                    if(info == null)
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
            byte[] data = client.UploadValues(address, coll);
            return OnGetOpenedOrders(ticker, data);
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
            if(string.IsNullOrEmpty(text))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            lock(ticker.OpenedOrders) {
                ticker.OpenedOrders.Clear();
                foreach(JProperty prop in res.Children()) {
                    if(prop.Name == "error") {
                        Debug.WriteLine("OnGetOpenedOrders fails: " + prop.Value<string>());
                        return false;
                    }
                    JArray array = (JArray)prop.Value;
                    foreach(JObject obj in array) {
                        OpenedOrderInfo info = new OpenedOrderInfo();
                        info.Market = prop.Name;
                        info.OrderNumber = obj.Value<int>("orderNumber");
                        info.Type = obj.Value<string>("type") == "sell" ? OrderType.Sell : OrderType.Buy;
                        info.Value = obj.Value<decimal>("rate");
                        info.Amount = obj.Value<decimal>("amount");
                        info.Total = obj.Value<decimal>("total");
                        ticker.OpenedOrders.Add(info);
                    }
                }
            }
            return true;
        }

        public bool OnGetOpenedOrders(byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if(string.IsNullOrEmpty(text))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            lock(OpenedOrders) {
                OpenedOrders.Clear();
                foreach(JProperty prop in res.Children()) {
                    if(prop.Name == "error") {
                        Debug.WriteLine("OnGetOpenedOrders fails: " + prop.Value<string>());
                        return false;
                    }
                    JArray array = (JArray)prop.Value;
                    foreach(JObject obj in array) {
                        OpenedOrderInfo info = new OpenedOrderInfo();
                        info.Market = prop.Name;
                        info.OrderNumber = obj.Value<int>("orderNumber");
                        info.Type = obj.Value<string>("type") == "sell" ? OrderType.Sell : OrderType.Buy;
                        info.Value = obj.Value<decimal>("rate");
                        info.Amount = obj.Value<decimal>("amount");
                        info.Total = obj.Value<decimal>("total");
                        OpenedOrders.Add(info);
                    }
                }
            }
            return true;
        }

        public long BuyLimit(PoloniexTicker ticker, decimal rate, decimal amount) {
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
                return OnBuyLimit(data);
            }
            catch(Exception e) {
                Debug.WriteLine(e.ToString());
                return -1;
            }
        }

        public long SellLimit(PoloniexTicker ticker, decimal rate, decimal amount) {
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
            }
            catch(Exception e) {
                Debug.WriteLine(e.ToString());
                return -1;
            }
            
        }

        public long OnBuyLimit(byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if(string.IsNullOrEmpty(text))
                return -1;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            return res.Value<long>("orderNumber");
        }

        public string OnCreateDeposit(string currency, byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if(string.IsNullOrEmpty(text))
                return null;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            if(res.Value<int>("success") != 1)
                return null;
            string deposit = res.Value<string>("response");
            PoloniexAccountBalanceInfo info = (PoloniexAccountBalanceInfo)Balances.FirstOrDefault(b => b.Currency == currency);
            info.DepositAddress = deposit;
            return deposit;
        }

        public long OnSellLimit(byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if(string.IsNullOrEmpty(text))
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
            if(string.IsNullOrEmpty(text))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            return res.Value<int>("success") == 1;
        }

        public Task<byte[]> WithdrawAsync(string currency, decimal amount, string addr, string paymentId) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "withdraw");
            coll.Add("nonce", DateTime.Now.Ticks.ToString());
            coll.Add("currency", currency);
            coll.Add("amount", amount.ToString("0.########"));
            coll.Add("address", address);
            if(!string.IsNullOrEmpty(paymentId))
                coll.Add("paymentId", paymentId);

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", ApiKey);
            return client.UploadValuesTaskAsync(address, coll);
        }

        public bool Withdraw(string currency, decimal amount, string addr, string paymentId) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "withdraw");
            coll.Add("nonce", DateTime.Now.Ticks.ToString());
            coll.Add("currency", currency);
            coll.Add("amount", amount.ToString("0.########"));
            coll.Add("address", address);
            if(!string.IsNullOrEmpty(paymentId))
                coll.Add("paymentId", paymentId);

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", ApiKey);
            try {
                byte[] data = client.UploadValues(address, coll);
                return OnWithdraw(data);
            }
            catch(Exception) {
                return false;
            }
        }

        public bool OnWithdraw(byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if(string.IsNullOrEmpty(text))
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
            }
            catch(Exception e) {
                Debug.WriteLine(e.ToString());
                return null;
            }
        }

    }

    public delegate void TickerUpdateEventHandler(object sender, TickerUpdateEventArgs e);
    public class TickerUpdateEventArgs : EventArgs {
        public PoloniexTicker Ticker { get; set; }
    }
 }
