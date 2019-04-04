using CryptoMarketClient.Common;
using CryptoMarketClient.Exchanges.Poloniex;
using CryptoMarketClient.Helpers;
using CryptoMarketClient.Poloniex;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reactive.Subjects;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocket4Net;

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

        public PoloniexExchange() : base() {

        }

        protected internal override IIncrementalUpdateDataProvider CreateIncrementalUpdateDataProvider() {
            return new PoloniexIncrementalUpdateDataProvider();
        }

        public override void OnAccountRemoved(AccountInfo info) {

        }

        public override ExchangeType Type => ExchangeType.Poloniex;

        public override bool SupportWebSocket(WebSocketType type) {
            return type == WebSocketType.Tickers ||
                type == WebSocketType.Ticker;
        }

        private void OnGetTickerItem(PoloniexTicker item) {
            lock(item) {
                item.UpdateHistoryItem();
                RaiseTickerUpdate(item);
            }
        }

        public override Form CreateAccountForm() {
            return new AccountBalancesForm(this);
        }

        public class WebSocketSubscribeInfo {
            public string command { get; set; }
            public string channel { get; set; }
            public string userID { get; set; }
        }

        public override string BaseWebSocketAdress { get { return "wss://api2.poloniex.com"; } }

        protected override void OnTickersSocketMessageReceived(object sender, MessageReceivedEventArgs e) {
            base.OnTickersSocketMessageReceived(sender, e);
            if(e.Message[1] == '1' && e.Message[2] == '0' && e.Message[3] == '0' && e.Message[4] == '2')
                OnTickerInfoRecv(e.Message);
            else {
                int startIndex = 1;
                int value = FastValueConverter.ConvertPositiveInteger(e.Message, ref startIndex); startIndex++;

                Ticker t = Tickers.FirstOrDefault(tt => tt.Code == value);
                if(t != null)
                    OnTickerOrderBookAndTradesRecv(t, e.Message, startIndex);
            }
        }

        bool StartsWith(string message, int startIndex, string header) {
            for(int i = 0, j = startIndex; i < header.Length; i++, j++) {
                if(message[j] != header[i])
                    return false;
            }
            return true;
        }

        void OnSnapshotRecv(Ticker tickerBase, string message, int startIndex) {
            List<object> array = JsonParser.Parse<List<object>>(message);
            List<object> obj = (List<object>)((List<object>)array[2])[0];
            if(((string)obj[0]) == "i")
                OnFirstInitializeOrderBook(tickerBase, (Dictionary<string, object>)obj[1]);
        }

        void OnFirstInitializeOrderBook(Ticker ticker, Dictionary<string, object> jObject) {
            IIncrementalUpdateDataProvider provider = CreateIncrementalUpdateDataProvider();

            provider.ApplySnapshot(jObject, ticker);
        }

        protected void OnTickerOrderBookAndTradesRecv(Ticker ticker, string message, int startIndex) {
            long seqNumber = FastValueConverter.ConvertPositiveLong(message, ref startIndex); startIndex++;

            const string header = "[[\"i\",{\"currencyPair\"";
            if(StartsWith(message, startIndex, header)) {
                Updates.Clear(seqNumber + 1);
                OnSnapshotRecv(ticker, message, startIndex + header.Length);
                return;
            }

            List<string[]> items = JSonHelper.Default.DeserializeArrayOfArrays(Encoding.Default.GetBytes(message), ref startIndex, 6);
            Updates.Push(seqNumber, ticker, items);
            OnIncrementalUpdateRecv(Updates);
        }

        int DeserializePositiveInt(char[] value, ref int current, int end) {
            int sum = 0;
            for(; current < end; current++) {
                if(value[current] == ',') {
                    current++;
                    break;
                }
                sum = ((sum << 3) + (sum << 1)) + (value[current] - 0x30);
            }
            return sum;
        }

        double DeserializeDoubleInQuotes(char[] value, ref int current, int end) {
            if(value[current] != '"')
                throw new ArgumentException(value.ToString());
            current++;
            int start = current;
            for(; current < end; current++) {
                if(value[current] == '"') {
                    double res = FastValueConverter.Convert(value, start, current);
                    current += 2;
                    return res;
                }
            }
            throw new ArgumentException(value.ToString());
        }

        void OnTickerInfoRecv(string message) {
            int start = message.IndexOf('[', 5);
            if(start == -1)
                return;
            int end = message.IndexOf(']', start);

            char[] bytes = message.ToCharArray();
            int current = start + 1;

            int code = DeserializePositiveInt(bytes, ref current, end);
            PoloniexTicker ticker = (PoloniexTicker)Tickers.FirstOrDefault(t => t.Code == code);
            if(ticker == null)
                return;
            ticker.Last = DeserializeDoubleInQuotes(bytes, ref current, end);
            ticker.LowestAsk = DeserializeDoubleInQuotes(bytes, ref current, end);
            ticker.HighestBid = DeserializeDoubleInQuotes(bytes, ref current, end);
            ticker.Change = DeserializeDoubleInQuotes(bytes, ref current, end);
            ticker.BaseVolume = DeserializeDoubleInQuotes(bytes, ref current, end);
            ticker.Volume = DeserializeDoubleInQuotes(bytes, ref current, end);
            ticker.IsFrozen = DeserializePositiveInt(bytes, ref current, end) == 1;
            ticker.Hr24High = DeserializeDoubleInQuotes(bytes, ref current, end);
            ticker.Hr24Low = DeserializeDoubleInQuotes(bytes, ref current, end);

            ticker.UpdateTrailings();

            lock(ticker) {
                RaiseTickerUpdate(ticker);
            }
        }

        protected override void OnTickersSocketOpened(object sender, EventArgs e) {
            base.OnTickersSocketOpened(sender, e);
            WebSocket.Send(JsonConvert.SerializeObject(new WebSocketSubscribeInfo() { channel = "1002", command = "subscribe" }));
        }

        public override void StartListenTickerStream(Ticker ticker) {
            base.StartListenTickerStream(ticker);
            WebSocket.Send(JsonConvert.SerializeObject(new WebSocketSubscribeInfo() { channel = ticker.CurrencyPair, command = "subscribe" }));
        }

        public override void StopListenTickerStream(Ticker ticker) {
            base.StopListenTickerStream(ticker);
            WebSocket.Send(JsonConvert.SerializeObject(new WebSocketSubscribeInfo() { channel = ticker.CurrencyPair, command = "unsubscribe" }));
        }

        public override void ObtainExchangeSettings() {

        }

        public override bool AllowCandleStickIncrementalUpdate => true;

        public override List<CandleStickIntervalInfo> GetAllowedCandleStickIntervals() {
            List<CandleStickIntervalInfo> list = new List<CandleStickIntervalInfo>();
            list.Add(new CandleStickIntervalInfo() { Text = "5 Minutes", Interval = TimeSpan.FromSeconds(300) });
            list.Add(new CandleStickIntervalInfo() { Text = "15 Minutes", Interval = TimeSpan.FromSeconds(300) });
            list.Add(new CandleStickIntervalInfo() { Text = "30 Minutes", Interval = TimeSpan.FromSeconds(1800) });
            list.Add(new CandleStickIntervalInfo() { Text = "2 Hours", Interval = TimeSpan.FromSeconds(7200) });
            list.Add(new CandleStickIntervalInfo() { Text = "4 Hours", Interval = TimeSpan.FromSeconds(14400) });
            list.Add(new CandleStickIntervalInfo() { Text = "1 Day", Interval = TimeSpan.FromSeconds(86400) });

            return list;
        }

        protected IDisposable TickersSubscriber { get; set; }
        public void Connect() {
            if(TickersSubscriber != null)
                return;
        }

        public IDisposable ConnectOrderBook(PoloniexTicker ticker) {
            return null;
        }

        public override BindingList<CandleStickData> GetCandleStickData(Ticker ticker, int candleStickPeriodMin, DateTime start, long periodInSeconds) {
            long startSec = (long)(start.Subtract(epoch)).TotalSeconds;
            long end = startSec + periodInSeconds;

            string address = string.Format("https://poloniex.com/public?command=returnChartData&currencyPair={0}&period={1}&start={2}&end={3}",
                Uri.EscapeDataString(ticker.CurrencyPair), candleStickPeriodMin * 60, startSec, end);
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(address);
            }
            catch(Exception) {
                return null;
            }
            if(bytes == null || bytes.Length == 0)
                return null;

            DateTime startTime = new DateTime(1970, 1, 1);

            BindingList<CandleStickData> list = new BindingList<CandleStickData>();
            int startIndex = 0;
            List<string[]> res = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "date", "high", "low", "open", "close", "volume", "quoteVolume", "weightedAverage" });
            if(res == null) return list;
            foreach(string[] item in res) {
                CandleStickData data = new CandleStickData();
                data.Time = startTime.AddSeconds(FastValueConverter.ConvertPositiveLong(item[0]));
                data.High = FastValueConverter.Convert(item[1]);
                data.Low = FastValueConverter.Convert(item[2]);
                data.Open = FastValueConverter.Convert(item[3]);
                data.Close = FastValueConverter.Convert(item[4]);
                data.Volume = FastValueConverter.Convert(item[5]);
                data.QuoteVolume = FastValueConverter.Convert(item[6]);
                data.WeightedAverage = FastValueConverter.Convert(item[7]);
                list.Add(data);
            }

            List<TradeInfoItem> trades = GetTradeVolumesForCandleStick(ticker, startSec, end);
            CandleStickChartHelper.InitializeVolumes(list, trades, ticker.CandleStickPeriodMin);
            return list;
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
            JObject res = JsonConvert.DeserializeObject<JObject>(text);
            foreach(JProperty prop in res.Children()) {
                string currency = prop.Name;
                JObject obj = (JObject)prop.Value;
                PoloniexCurrencyInfo c = (PoloniexCurrencyInfo)Currencies.FirstOrDefault(curr => curr.Currency == currency);
                if(c == null) {
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
            JObject res = JsonConvert.DeserializeObject<JObject>(text);
            int index = 0;
            foreach(JProperty prop in res.Children()) {
                PoloniexTicker t = new PoloniexTicker(this);
                t.Index = index;
                t.CurrencyPair = prop.Name;
                if(PoloniexTickerCodesProvider.Codes.ContainsKey(t.CurrencyPair))
                    t.Code = PoloniexTickerCodesProvider.Codes[t.CurrencyPair];
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
            IsInitialized = true;
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
            JObject res = JsonConvert.DeserializeObject<JObject>(text);
            foreach(JProperty prop in res.Children()) {
                PoloniexTicker t = (PoloniexTicker)Tickers.FirstOrDefault((i) => i.CurrencyPair == prop.Name);
                if(t == null)
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
        public override bool UpdateArbitrageOrderBook(Ticker ticker, int depth) {
            string address = GetOrderBookString(ticker, depth);
            string text = ((Ticker)ticker).DownloadString(address);
            return OnUpdateArbitrageOrderBook(ticker, text, depth);
        }
        public override bool UpdateTicker(Ticker tickerBase) {
            return true;
        }
        public bool OnUpdateOrderBook(Ticker ticker, byte[] bytes) {
            if(bytes == null)
                return false;

            int startIndex = 1; // skip {
            if(!JSonHelper.Default.FindChar(bytes, ':', ref startIndex))
                return false;
            startIndex++;
            List<string[]> asks = JSonHelper.Default.DeserializeArrayOfArrays(bytes, ref startIndex, 2);
            if(!JSonHelper.Default.FindChar(bytes, ',', ref startIndex))
                return false;
            startIndex++;
            if(!JSonHelper.Default.FindChar(bytes, ':', ref startIndex))
                return false;
            startIndex++;
            List<string[]> bids = JSonHelper.Default.DeserializeArrayOfArrays(bytes, ref startIndex, 2);

            ticker.OrderBook.GetNewBidAsks();
            int index = 0;
            List<OrderBookEntry> list = ticker.OrderBook.Bids;
            foreach(string[] item in bids) {
                OrderBookEntry entry = list[index];
                entry.ValueString = item[0];
                entry.AmountString = item[1];
                index++;
                if(index >= list.Count)
                    break;
            }
            index = 0;
            list = ticker.OrderBook.Asks;
            foreach(string[] item in asks) {
                OrderBookEntry entry = list[index];
                entry.ValueString = item[0];
                entry.AmountString = item[1];
                index++;
                if(index >= list.Count)
                    break;
            }

            ticker.OrderBook.UpdateEntries();
            return true;
        }
        public bool OnUpdateArbitrageOrderBook(Ticker ticker, string text, int depth) {
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
            List<OrderBookEntry> list = ticker.OrderBook.Bids;
            foreach(List<object> item in bids) {
                OrderBookEntry entry = new OrderBookEntry();
                list.Add(entry);
                entry.ValueString = (string)item.First();
                entry.AmountString = (string)item.Last();
                index++;
                if(index >= depth)
                    break;
            }
            index = 0;
            list = ticker.OrderBook.Asks;
            foreach(List<object> item in asks) {
                OrderBookEntry entry = new OrderBookEntry();
                list.Add(entry);
                entry.ValueString = (string)item.First();
                entry.AmountString = (string)item.Last();
                index++;
                if(index >= depth)
                    break;
            }

            ticker.OrderBook.UpdateEntries();
            return true;
        }

        public string GetOrderBookString(Ticker ticker, int depth) {
            return string.Format("https://poloniex.com/public?command=returnOrderBook&currencyPair={0}&depth={1}",
                Uri.EscapeDataString(ticker.CurrencyPair), depth);
        }
        public override bool UpdateOrderBook(Ticker ticker) {
            return GetOrderBook(ticker, OrderBook.Depth);
        }
        public override bool ProcessOrderBook(Ticker tickerBase, string text) {
            UpdateOrderBook(tickerBase, text);
            return true;
        }
        public void UpdateOrderBook(Ticker ticker, string text) {
            OnUpdateArbitrageOrderBook(ticker, text, 10000);
        }
        public bool GetOrderBook(Ticker ticker, int depth) {
            string address = string.Format("https://poloniex.com/public?command=returnOrderBook&currencyPair={0}&depth={1}",
                Uri.EscapeDataString(ticker.CurrencyPair), depth);
            byte[] bytes = ((Ticker)ticker).DownloadBytes(address);
            if(bytes == null || bytes.Length == 0)
                return false;
            OnUpdateOrderBook(ticker, bytes);
            return true;
        }

        public override List<TradeInfoItem> GetTrades(Ticker ticker, DateTime starTime) {
            string address = string.Format("https://poloniex.com/public?command=returnTradeHistory&currencyPair={0}", Uri.EscapeDataString(ticker.CurrencyPair));
            string text = GetDownloadString(address);
            if(string.IsNullOrEmpty(text))
                return null;
            JArray trades = JsonConvert.DeserializeObject<JArray>(text);
            if(trades.Count == 0)
                return null;

            List<TradeInfoItem> list = new List<TradeInfoItem>();

            int index = 0;
            foreach(JObject obj in trades) {
                DateTime time = obj.Value<DateTime>("date");
                int tradeId = obj.Value<int>("tradeID");
                if(time < starTime)
                    break;

                TradeInfoItem item = new TradeInfoItem(null, ticker);
                bool isBuy = obj.Value<string>("type").Length == 3;
                item.AmountString = obj.Value<string>("amount");
                item.Time = time;
                item.Type = isBuy ? TradeType.Buy : TradeType.Sell;
                item.RateString = obj.Value<string>("rate");
                item.Id = tradeId;
                double price = item.Rate;
                double amount = item.Amount;
                item.Total = price * amount;
                list.Insert(index, item);
                index++;
            }
            ticker.RaiseTradeHistoryAdd();
            return list;
        }

        protected List<TradeInfoItem> UpdateList { get; } = new List<TradeInfoItem>(100);

        protected List<TradeInfoItem> GetTradeVolumesForCandleStick(Ticker ticker, long start, long end) {
            string address = string.Format("https://poloniex.com/public?command=returnTradeHistory&currencyPair={0}&start={1}&end={2}", Uri.EscapeDataString(ticker.CurrencyPair), start, end);
            byte[] bytes = GetDownloadBytes(address);
            if(bytes == null)
                return null;

            int startIndex = 0;
            List<string[]> trades = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "globalTradeID", "tradeID", "date", "type", "rate", "amount", "total" });

            List<TradeInfoItem> newTrades = new List<TradeInfoItem>();
            foreach(string[] obj in trades) {
                TradeInfoItem item = new TradeInfoItem(null, ticker);
                item.TimeString = obj[2];
                item.AmountString = obj[5];
                item.Type = obj[3].Length == 3 ? TradeType.Buy : TradeType.Sell;
                newTrades.Add(item);
            }
            return newTrades;
        }

        public override bool UpdateTrades(Ticker ticker) {
            string address = string.Format("https://poloniex.com/public?command=returnTradeHistory&currencyPair={0}", Uri.EscapeDataString(ticker.CurrencyPair));
            byte[] bytes = GetDownloadBytes(address);
            if(bytes == null)
                return true;

            string lastGotTradeId = ticker.TradeHistory.Count > 0 ? ticker.TradeHistory.First().IdString : null;

            int startIndex = 0;
            List<string[]> trades = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex,
                new string[] { "globalTradeID", "tradeID", "date", "type", "rate", "amount", "total" },
                (itemIndex, paramIndex, value) => { return paramIndex != 1 || value != lastGotTradeId; });

            TradeStatisticsItem st = new TradeStatisticsItem();
            st.MinBuyPrice = double.MaxValue;
            st.MinSellPrice = double.MaxValue;
            st.Time = DateTime.UtcNow;

            int index = 0;
            List<TradeInfoItem> newTrades = new List<TradeInfoItem>();
            foreach(string[] obj in trades) {
                TradeInfoItem item = new TradeInfoItem(null, ticker);
                item.IdString = obj[1];
                item.TimeString = obj[2];

                bool isBuy = obj[3].Length == 3;
                item.AmountString = obj[5];
                item.Type = isBuy ? TradeType.Buy : TradeType.Sell;
                item.RateString = obj[4];
                double price = item.Rate;
                double amount = item.Amount;
                item.Total = price * amount;
                if(isBuy) {
                    st.BuyAmount += amount;
                    st.MinBuyPrice = Math.Min(st.MinBuyPrice, price);
                    st.MaxBuyPrice = Math.Max(st.MaxBuyPrice, price);
                    st.BuyVolume += item.Total;
                }
                else {
                    st.SellAmount += amount;
                    st.MinSellPrice = Math.Min(st.MinSellPrice, price);
                    st.MaxSellPrice = Math.Max(st.MaxSellPrice, price);
                    st.SellVolume += item.Total;
                }
                newTrades.Add(item);
                ticker.TradeHistory.Insert(index, item);
                index++;
            }
            CandleStickChartHelper.UpdateVolumes(ticker.CandleStickData, newTrades, ticker.CandleStickPeriodMin);
            if(st.MinSellPrice == double.MaxValue)
                st.MinSellPrice = 0;
            if(st.MinBuyPrice == double.MaxValue)
                st.MinBuyPrice = 0;
            ticker.LastTradeStatisticTime = DateTime.UtcNow;
            ticker.TradeStatistic.Add(st);
            if(trades.Count > 0)
                ticker.RaiseTradeHistoryAdd();
            return true;
        }

        public override bool UpdateAccountTrades(AccountInfo account, Ticker ticker) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("nonce", GetNonce());
            coll.Add("command", "returnTradeHistory");
            if(ticker == null)
                coll.Add("currencyPair", "all");
            else
                coll.Add("currencyPair", ticker.MarketName);
            coll.Add("start", ToUnixTimestamp(DateTime.Now.AddYears(-1)).ToString());
            coll.Add("end", ToUnixTimestamp(DateTime.Now).ToString());

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", account.GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", account.ApiKey);
            try {
                byte[] data = client.UploadValues(address, coll);
                return OnUpdateAccountTrades(account, ticker, data);
            }
            catch(Exception e) {
                Debug.WriteLine("get my trade history exception: " + e.ToString());
                return false;
            }
        }

        string[] accountTradeItems;
        protected string[] AccountTradeItems {
            get {
                if(accountTradeItems == null)
                    accountTradeItems = new string[] { "globalTradeID", "tradeID", "date", "rate", "amount", "total", "fee", "orderNumber", "type", "category" };
                return accountTradeItems;
            }
        }

        bool OnUpdateAccountTrades(AccountInfo account, Ticker ticker, byte[] data) {
            if(data == null)
                return false;

            List<TradeInfoItem> myTrades = ticker != null ? ticker.MyTradeHistory : account.MyTrades;
            if(data.Length == 2) {
                myTrades.Clear();
                return true;
            }

            string lastGotTradeId = null;
            if(ticker != null)
                lastGotTradeId = ticker.MyTradeHistory.Count > 0 ? ticker.MyTradeHistory.First().IdString : null;
            else
                lastGotTradeId = account.MyTrades.Count > 0 ? account.MyTrades.First().IdString : null;

            int startIndex = 0;
            if(ticker == null) {
                myTrades.Clear();
                var tickers = JSonHelper.Default.DeserializeInfiniteObjectWithArrayProperty(data, ref startIndex, AccountTradeItems);
                foreach(var t in tickers) {
                    Ticker tc = Tickers.FirstOrDefault(tt => tt.CurrencyPair == t.Property);
                    foreach(string[] item in t.Items) {
                        myTrades.Add(CreateTradeInfo(account, tc, item));
                    }
                }
            }
            else {
                List<string[]> trades = JSonHelper.Default.DeserializeArrayOfObjects(data, ref startIndex, AccountTradeItems,
                    (itemIndex, paramIndex, value) => { return paramIndex != 1 || value != lastGotTradeId; });

                int index = 0;
                foreach(string[] obj in trades) {
                    myTrades.Insert(index, CreateTradeInfo(account, ticker, obj));
                    index++;
                }
            }
            return true;
        }
        protected TradeInfoItem CreateTradeInfo(AccountInfo account, Ticker ticker, string[] obj) {
            TradeInfoItem item = new TradeInfoItem(account, ticker);
            item.IdString = obj[1];
            item.TimeString = obj[2];
            item.GlobalId = obj[0];
            item.OrderNumber = obj[7];

            bool isBuy = obj[8].Length == 3;
            item.AmountString = obj[4];
            item.Type = isBuy ? TradeType.Buy : TradeType.Sell;
            item.RateString = obj[3];
            item.Total = FastValueConverter.Convert(obj[5]);
            item.Fee = FastValueConverter.Convert(obj[6]);
            return item;
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
        public override bool UpdateBalances(AccountInfo account) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "returnCompleteBalances");
            coll.Add("nonce", GetNonce());

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", account.GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", account.ApiKey);
            try {
                return OnGetBalances(account, client.UploadValues(address, coll));
            }
            catch(Exception) {
                return false;
            }
        }
        public Task<byte[]> GetBalancesAsync(AccountInfo account) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "returnCompleteBalances");
            coll.Add("nonce", GetNonce());

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", account.GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", account.ApiKey);
            return client.UploadValuesTaskAsync(address, coll);
        }
        public bool OnGetBalances(AccountInfo account, byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if(string.IsNullOrEmpty(text))
                return false;
            JObject res = JsonConvert.DeserializeObject<JObject>(text);
            lock(account.Balances) {
                foreach(JProperty prop in res.Children()) {
                    if(prop.Name == "error") {
                        Debug.WriteLine("OnGetBalances fails: " + prop.Value<string>());
                        return false;
                    }
                    JObject obj = (JObject)prop.Value;
                    PoloniexAccountBalanceInfo binfo = (PoloniexAccountBalanceInfo)account.Balances.FirstOrDefault(b => b.Currency == prop.Name);
                    if(binfo == null) {
                        binfo = new PoloniexAccountBalanceInfo(account);
                        binfo.Currency = prop.Name;
                        account.Balances.Add(binfo);
                    }
                    binfo.Currency = prop.Name;
                    binfo.LastAvailable = binfo.Available;
                    binfo.Available = obj.Value<double>("available");
                    binfo.OnOrders = obj.Value<double>("onOrders");
                    binfo.BtcValue = obj.Value<double>("btcValue");
                }
            }
            return true;
        }

        public override bool GetDeposites(AccountInfo account) {
            Task<byte[]> task = GetDepositesAsync(account);
            task.Wait();
            return OnGetDeposites(account, task.Result);
        }
        public Task<byte[]> GetDepositesAsync(AccountInfo account) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "returnDepositAddresses");
            coll.Add("nonce", GetNonce());

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", account.GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", account.ApiKey);
            try {
                return client.UploadValuesTaskAsync(address, coll);
            }
            catch(Exception e) {
                Debug.WriteLine("GetDeposites failed:" + e.ToString());
                return null;
            }
        }
        public bool OnGetDeposites(AccountInfo account, byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if(string.IsNullOrEmpty(text) || text == "[]")
                return false;
            JObject res = JsonConvert.DeserializeObject<JObject>(text);
            lock(account.Balances) {
                foreach(JProperty prop in res.Children()) {
                    if(prop.Name == "error") {
                        Debug.WriteLine("OnGetDeposites fails: " + prop.Value<string>());
                        return false;
                    }
                    PoloniexAccountBalanceInfo info = (PoloniexAccountBalanceInfo)account.Balances.FirstOrDefault((a) => a.Currency == prop.Name);
                    if(info == null)
                        continue;
                    info.DepositAddress = (string)prop.Value;
                }
            }
            return true;
        }
        string GetNonce() {
            return ((long)((DateTime.UtcNow - new DateTime(1, 1, 1)).TotalMilliseconds * 10000)).ToString();
        }
        public override bool UpdateOpenedOrders(AccountInfo account, Ticker ticker) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("nonce", GetNonce());
            coll.Add("command", "returnOpenOrders");
            coll.Add("currencyPair", ticker == null ? "all" : ticker.MarketName);

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", account.GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", account.ApiKey);
            try {
                byte[] data = client.UploadValues(address, coll);
                if(ticker != null) {
                    if(!ticker.IsOpenedOrdersChanged(data))
                        return true;
                }
                else {
                    if(IsOpenedOrdersChanged(data))
                        return true;
                }
                return OnGetOpenedOrders(account, ticker, data);
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }
        public Task<byte[]> GetOpenedOrders(AccountInfo account) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("nonce", GetNonce());
            coll.Add("command", "returnOpenOrders");
            coll.Add("currencyPair", "all");

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", account.GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", account.ApiKey);
            return client.UploadValuesTaskAsync(address, coll);
        }

        public bool OnGetOpenedOrders(AccountInfo account, Ticker ticker, byte[] data) {
            List<OpenedOrderInfo> openedOrders = ticker == null ? account.OpenedOrders : ticker.OpenedOrders;
            if(data.Length == 2) {
                lock(openedOrders) {
                    openedOrders.Clear();
                }
                return true;
            }
            string text = System.Text.Encoding.ASCII.GetString(data);
            if(string.IsNullOrEmpty(text))
                return false;

            lock(openedOrders) {
                openedOrders.Clear();
                if(ticker == null) {
                    JObject res = JsonConvert.DeserializeObject<JObject>(text);
                    foreach(JProperty prop in res.Children()) {
                        if(prop.Name == "error") {
                            Telemetry.Default.TrackEvent("poloniex.ongetopenedorders", new string[] { "error", prop.Value<string>() }, true);
                            Debug.WriteLine("OnGetOpenedOrders fails: " + prop.Value<string>());
                            return false;
                        }
                        ticker = Tickers.FirstOrDefault(tt => tt.CurrencyPair == prop.Name);
                        JArray array = (JArray)prop.Value;
                        foreach(JObject obj in array) {
                            OpenedOrderInfo info = CreateOrderInfo(account, ticker, obj);
                            openedOrders.Add(info);
                        }
                    }
                }
                else {
                    JArray array = JsonConvert.DeserializeObject<JArray>(text);
                    foreach(JObject obj in array) {
                        OpenedOrderInfo info = CreateOrderInfo(account, ticker, obj);
                        openedOrders.Add(info);
                    }
                    if(ticker != null)
                        ticker.RaiseOpenedOrdersChanged();
                }
            }
            return true;
        }

        protected OpenedOrderInfo CreateOrderInfo(AccountInfo account, Ticker ticker, JObject obj) {
            OpenedOrderInfo info = new OpenedOrderInfo(account, ticker);
            info.OrderId = obj.Value<string>("orderNumber");
            info.DateString = obj.Value<string>("date");
            info.Type = obj.Value<string>("type").Length == 4 ? OrderType.Sell : OrderType.Buy;
            info.ValueString = obj.Value<string>("rate");
            info.AmountString = obj.Value<string>("amount");
            info.TotalString = obj.Value<string>("total");
            return info;
        }

        //public bool OnGetOpenedOrders(AccountInfo account, Ticker ticker, byte[] data) {
        //    string text = System.Text.Encoding.ASCII.GetString(data);
        //    if(string.IsNullOrEmpty(text))
        //        return false;
        //    JObject res = JsonConvert.DeserializeObject<JObject>(text);
        //    lock(account.OpenedOrders) {
        //        account.OpenedOrders.Clear();
        //        foreach(JProperty prop in res.Children()) {
        //            if(prop.Name == "error") {
        //                Debug.WriteLine("OnGetOpenedOrders fails: " + prop.Value<string>());
        //                return false;
        //            }
        //            JArray array = (JArray)prop.Value;
        //            foreach(JObject obj in array) {
        //                OpenedOrderInfo info = new OpenedOrderInfo(account, ticker);
        //                info.Market = prop.Name;
        //                info.OrderId = obj.Value<string>("orderNumber");
        //                info.Type = obj.Value<string>("type") == "sell" ? OrderType.Sell : OrderType.Buy;
        //                info.ValueString = obj.Value<string>("rate");
        //                info.AmountString = obj.Value<string>("amount");
        //                info.TotalString = obj.Value<string>("total");
        //                account.OpenedOrders.Add(info);
        //            }
        //        }
        //    }
        //    return true;
        //}

        public override TradingResult Buy(AccountInfo account, Ticker ticker, double rate, double amount) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "buy");
            coll.Add("nonce", GetNonce());
            coll.Add("currencyPair", ticker.CurrencyPair);
            coll.Add("rate", rate.ToString("0.########"));
            coll.Add("amount", amount.ToString("0.########"));

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", account.GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", account.ApiKey);
            try {
                byte[] data = client.UploadValues(address, coll);
                return OnBuy(account, ticker, data);
            }
            catch(Exception e) {
                Debug.WriteLine(e.ToString());
                return null;
            }
        }

        public override TradingResult Sell(AccountInfo account, Ticker ticker, double rate, double amount) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "sell");
            coll.Add("nonce", GetNonce());
            coll.Add("currencyPair", ticker.CurrencyPair);
            coll.Add("rate", rate.ToString("0.########"));
            coll.Add("amount", amount.ToString("0.########"));

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", account.GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", account.ApiKey);
            try {
                byte[] data = client.UploadValues(address, coll);
                return OnSell(account, ticker, data);
            }
            catch(Exception e) {
                Debug.WriteLine(e.ToString());
                return null;
            }
            
        }

        public TradingResult OnBuy(AccountInfo account, Ticker ticker, byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if(string.IsNullOrEmpty(text))
                return null;
            JObject res = JsonConvert.DeserializeObject<JObject>(text);
            TradingResult tr = new TradingResult();
            tr.OrderNumber = res.Value<long>("orderNumber");
            tr.Type = OrderType.Buy;
            JArray array = res.Value<JArray>("resultingTrades");
            string tradeId = string.Empty;
            foreach(JObject trade in array) {
                TradeEntry e = new TradeEntry();
                e.Amount = trade.Value<double>("amount");
                e.Date = trade.Value<DateTime>("date");
                e.Rate = trade.Value<double>("rate");
                e.Total = trade.Value<double>("total");
                e.Id = trade.Value<string>("tradeID");
                e.Type = trade.Value<string>("type").Length == 3 ? OrderType.Buy : OrderType.Sell;
                tr.Trades.Add(e);
            }
            tr.Calculate();
            ticker.Trades.Add(tr);
            return tr;
        }

        public string OnCreateDeposit(AccountInfo account, string currency, byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if(string.IsNullOrEmpty(text))
                return null;
            JObject res = JsonConvert.DeserializeObject<JObject>(text);
            if(res.Value<int>("success") != 1)
                return null;
            string deposit = res.Value<string>("response");
            PoloniexAccountBalanceInfo info = (PoloniexAccountBalanceInfo)account.Balances.FirstOrDefault(b => b.Currency == currency);
            info.DepositAddress = deposit;
            return deposit;
        }

        public TradingResult OnSell(AccountInfo account, Ticker ticker, byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if(string.IsNullOrEmpty(text))
                return null;
            JObject res = JsonConvert.DeserializeObject<JObject>(text);
            TradingResult t = new TradingResult();
            t.Trades.Add(new TradeEntry() { Id = res.Value<string>("orderNumber") });
            return t;
        }

        public override bool Cancel(AccountInfo account, string orderId) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "cancelOrder");
            coll.Add("nonce", GetNonce());
            coll.Add("orderNumber", orderId);

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", account.GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", account.ApiKey);
            try {
                return OnCancel(client.UploadValues(address, coll));
            }
            catch(Exception) {
                return false;
            }
        }

        public Task<byte[]> CancelOrder(AccountInfo account, string orderId) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "cancelOrder");
            coll.Add("nonce", GetNonce());
            coll.Add("orderNumber", orderId);

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", account.GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", account.ApiKey);
            return client.UploadValuesTaskAsync(address, coll);
        }

        public bool OnCancel(byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if(string.IsNullOrEmpty(text))
                return false;
            JObject res = JsonConvert.DeserializeObject<JObject>(text);
            return res.Value<int>("success") == 1;
        }

        public Task<byte[]> WithdrawAsync(AccountInfo account, string currency, double amount, string addr, string paymentId) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "withdraw");
            coll.Add("nonce", GetNonce());
            coll.Add("currency", currency);
            coll.Add("amount", amount.ToString("0.########"));
            coll.Add("address", address);
            if(!string.IsNullOrEmpty(paymentId))
                coll.Add("paymentId", paymentId);

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", account.GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", account.ApiKey);
            return client.UploadValuesTaskAsync(address, coll);
        }

        public override bool Withdraw(AccountInfo account, string currency, string addr, string paymentId, double amount) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "withdraw");
            coll.Add("nonce", GetNonce());
            coll.Add("currency", currency);
            coll.Add("amount", amount.ToString("0.########"));
            coll.Add("address", address);
            if(!string.IsNullOrEmpty(paymentId))
                coll.Add("paymentId", paymentId);

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", account.GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", account.ApiKey);
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
            JObject res = JsonConvert.DeserializeObject<JObject>(text);
            return !string.IsNullOrEmpty(res.Value<string>("responce"));
        }
        public override bool GetBalance(AccountInfo info, string str) {
            return UpdateDefaultAccountBalances();
        }
        public override string CreateDeposit(AccountInfo account, string currency) {
            string address = string.Format("https://poloniex.com/tradingApi");

            NameValueCollection coll = new NameValueCollection();
            coll.Add("command", "generateNewAddress");
            coll.Add("nonce", GetNonce());
            coll.Add("currency", currency);

            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", account.GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", account.ApiKey);
            try {
                byte[] data = client.UploadValues(address, coll);
                return OnCreateDeposit(account, currency, data);
            }
            catch(Exception e) {
                Debug.WriteLine(e.ToString());
                return null;
            }
        }

    }

    public delegate void TickerUpdateEventHandler(object sender, TickerUpdateEventArgs e);
    public class TickerUpdateEventArgs : EventArgs {
        public Ticker Ticker { get; set; }
    }
 }
