using Crypto.Core.Common;
using Crypto.Core.Exchanges.Poloniex;
using Crypto.Core.Helpers;
using Crypto.Core.Poloniex;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocket4Net;
using Crypto.Core.Exchanges.Base;

namespace Crypto.Core {
    public class PoloniexExchange : Exchange {
        static PoloniexExchange defaultExchange;
        public static PoloniexExchange Default {
            get {
                if(defaultExchange == null)
                    defaultExchange = (PoloniexExchange)Exchange.FromFile(ExchangeType.Poloniex, typeof(PoloniexExchange));
                return defaultExchange;
            }
        }

        public PoloniexExchange() : base() {
            RequestRate = new List<RateLimit>();
            RequestRate.Add(new RateLimit(this) { Limit = 6, Interval = TimeSpan.TicksPerSecond});
        }

        public override Ticker CreateTicker(string name) {
            return new PoloniexTicker(this) { CurrencyPair = name };
        }

        public override BalanceBase CreateAccountBalance(AccountInfo info, string currency) {
            return new PoloniexAccountBalanceInfo(info, GetOrCreateCurrency(currency));
        }

        protected override bool ShouldAddKlineListener => true;

        protected internal override IIncrementalUpdateDataProvider CreateIncrementalUpdateDataProvider() {
            return new PoloniexIncrementalUpdateDataProvider();
        }

        public override ExchangeType Type => ExchangeType.Poloniex;

        public override bool SupportWebSocket(WebSocketType type) {
            return type == WebSocketType.Tickers ||
                type == WebSocketType.Ticker || type == WebSocketType.OrderBook || type == WebSocketType.Kline;
        }

        public override string BaseWebSocketAdress { get { return "wss://ws.poloniex.com/ws/public"; } }
        public override int WebSocketAllowedDelayInterval => 5000; 

        protected internal override void OnTickersSocketMessageReceived(object sender, MessageReceivedEventArgs e) {
            base.OnTickersSocketMessageReceived(sender, e);

            JsonHelperToken root = JsonHelper.Default.Deserialize(e.Message);
            if(root.Type == JsonObjectType.Object) {
                if(root.GetProperty("event") != null) {
                    ProcessEvent(root);
                    return;
                }
                JsonHelperToken channel = root.GetProperty("channel");
                if(channel != null) {
                    ProcessChannel(root, e.Message);
                    return;
                }
            }
        }

        protected bool ProcessChannel(JsonHelperToken root, string message) {
            JsonHelperToken channel = root.GetProperty("channel");
            JsonHelperToken data = root.GetProperty("data");
            if(channel.Value == "ticker") {
                OnProcessTickerInfo(data);
            }
            else if(channel.Value == "trades") {
                OnProcessTrades(root, message);
            }
            else if(channel.Value == "book_lv2") {
                OnProcessOrderBookLv2(root, message);
            }
            else if(channel.Value.StartsWith("candles")) {
                OnProcessKline(data, message);
            }
            return true;
        }

        protected void OnProcessKline(JsonHelperToken data, string message) {
            foreach(var item in data.Items) {
                string name = item.GetProperty("symbol").Value;
                Ticker t = Tickers.FirstOrDefault(tt => tt.SubscriptionName == name);
                if(t == null)
                    continue;

                if(t.CaptureData)
                    t.CaptureDataCore(CaptureStreamType.TradeHistory, CaptureMessageType.Incremental, message);

                CandleStickData ti = new CandleStickData();
                
                ti.Time = PoloniexTime(item.GetProperty("startTime"));
                CandleStickData last = t.CandleStickData.Last();
                if(last != null && ti.Time < last.Time)
                    return;
                if(last != null && last.Time == ti.Time)
                    ti = last;

                ti.Open = item.GetProperty("open").ValueDouble;
                ti.Close = item.GetProperty("close").ValueDouble;
                ti.High = item.GetProperty("high").ValueDouble;
                ti.Low = item.GetProperty("low").ValueDouble;
                ti.Volume = item.GetProperty("quantity").ValueDouble;
                ti.QuoteVolume = item.GetProperty("amount").ValueDouble;

                if(ti == last)
                    return;

                t.CandleStickData.Add(ti);
                t.RaiseCandleStickChanged(new System.ComponentModel.ListChangedEventArgs(System.ComponentModel.ListChangedType.ItemAdded, t.CandleStickData.Count - 1));
            }
        }

        protected void OnProcessTrades(JsonHelperToken root, string message) {
            JsonHelperToken data = root.GetProperty("data");
            foreach(var item in data.Items) {
                string name = item.GetProperty("symbol").Value;
                Ticker t = Tickers.FirstOrDefault(tt => tt.SubscriptionName == name);
                if(t == null)
                    continue;

                if(t.CaptureData)
                    t.CaptureDataCore(CaptureStreamType.TradeHistory, CaptureMessageType.Incremental, message);

                TradeInfoItem ti = new TradeInfoItem();
                ti.AmountString = item.GetProperty("quantity").Value;
                ti.RateString = item.GetProperty("price").Value;
                ti.Time = PoloniexTime(item.GetProperty("createTime"));
                ti.Type = item.GetProperty("takerSide").Value[0] == 'b' ? TradeType.Buy : TradeType.Sell;
                t.AddTradeHistoryItem(ti);

                if(t.HasTradeHistorySubscribers) {
                    TradeHistoryChangedEventArgs e = new TradeHistoryChangedEventArgs() { NewItem = ti };
                    t.RaiseTradeHistoryChanged(e);
                }
            }
        }

        protected DateTime PoloniexTime(JsonHelperToken item) {
            return epoch.AddMilliseconds(item.ValueLong).ToLocalTime();
        }

        protected void OnProcessOrderBookLv2(JsonHelperToken root, string message) {
            JsonHelperToken data = root.GetProperty("data");
            bool snapshot = root.GetProperty("action").Value == "snapshot";
            foreach(var item in data.Items) {
                string name = item.GetProperty("symbol").Value;
                Ticker t = Tickers.FirstOrDefault(tt => tt.SubscriptionName == name);
                if(t == null)
                    continue;

                if(t.CaptureData)
                    t.CaptureDataCore(CaptureStreamType.OrderBook, snapshot? CaptureMessageType.Snapshot: CaptureMessageType.Incremental, message);

                IIncrementalUpdateDataProvider provider = CreateIncrementalUpdateDataProvider();
                if(snapshot) {
                    provider.ApplySnapshot(item, t);
                    return;
                }

                long seqNumber = item.GetProperty("id").ValueLong;
                JsonHelperToken[] jb = item.GetProperty("bids").Items;
                JsonHelperToken[] ja = item.GetProperty("asks").Items;

                List<string[]> b = new List<string[]>(jb.Length);
                List<string[]> a = new List<string[]>(ja.Length);

                for(int i = 0; i < jb.Length; i++) {
                    string[] bi = new string[2];
                    bi[0] = jb[i].Items[0].Value;
                    bi[1] = jb[i].Items[1].Value;
                    b.Add(bi);
                }

                for(int i = 0; i < ja.Length; i++) {
                    string[] ai = new string[2];
                    ai[0] = ja[i].Items[0].Value;
                    ai[1] = ja[i].Items[1].Value;
                    a.Add(ai);
                }

                IncrementalUpdateInfo info = new IncrementalUpdateInfo();
                info.Fill(seqNumber, t, b, a, null);
                provider.Update(t, info);
            }
        }

        protected void OnProcessTickerInfo(JsonHelperToken data) {
            foreach(var item in data.Items) {
                string name = item.GetProperty("symbol").Value;
                Ticker t = Tickers.FirstOrDefault(tt => tt.SubscriptionName == name);
                if(t == null)
                    continue;
                t.Change = item.GetProperty("dailyChange").ValueDouble;
                t.Hr24HighString = item.GetProperty("high").Value;
                t.Hr24LowString = item.GetProperty("low").Value;
                t.BaseVolumeString = item.GetProperty("quantity").Value;
                t.VolumeString = item.GetProperty("amount").Value;
            }
        }

        protected bool ProcessEvent(JsonHelperToken root) {
            JsonHelperToken evt = root.GetProperty("event");
            if(evt.Value == "error") {
                if(root.GetProperty("message").Value.StartsWith("Not Subscribed"))
                    return true;
                LogManager.Default.Add(LogType.Error, this, GetType().Name, root.GetProperty("message").Value, "");
                return true;
            }
            JsonHelperToken channel = root.GetProperty("channel");
            JsonHelperToken symbols = root.GetProperty("symbols");

            List<Ticker> tickers = new List<Ticker>(symbols.ItemsCount);
            foreach(JsonHelperToken item in symbols.Items) {
                Ticker t = Tickers.FirstOrDefault(tt => tt.SubscriptionName == item.Value);
                if(t != null)
                    tickers.Add(t);
            }

            bool subscribe = evt.Value == "subscribe";
            if(channel.Value == "ticker")
                tickers.ForEach(t => t.IsTickerInfoSubscribed = subscribe);
            else if(channel.Value == "book")
                tickers.ForEach(t => t.IsOrderBookSubscribed = subscribe);
            else if(channel.Value == "trades")
                tickers.ForEach(t => t.IsTradeHistorySubscribed = subscribe);
            else if(channel.Value.StartsWith("candles"))
                tickers.ForEach(t => t.IsKlineSubscribed = subscribe);
            return true;
        }

        bool StartsWith(string message, int startIndex, string header) {
            for(int i = 0, j = startIndex; i < header.Length; i++, j++) {
                if(message[j] != header[i])
                    return false;
            }
            return true;
        }

        public override bool SupportCummulativeTickersUpdate => false;

        protected internal override void OnTickersSocketOpened(object sender, EventArgs e) {
            base.OnTickersSocketOpened(sender, e);
            //TickersSocket.State = SocketConnectionState.Connecting;
            //TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.Hearthbeat, null) {
            //    Request = new PoloniexWebSocketRequest() { channel = "1010", command = "subscribe" }
            //});
            //TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.Tickers, null) {
            //    Request = new PoloniexWebSocketRequest() { channel = "1002", command = "subscribe" }
            //});
            //TickersSocket.State = SocketConnectionState.Connected;
        }
        
        public override void StartListenTickerStream(Ticker ticker) {
            base.StartListenTickerStream(ticker);
            StartListenOrderBookCore(ticker);
        }

        protected override void OnWebSocketCheckTimer(object state) {
            base.OnWebSocketCheckTimer(state);
        }

        protected override void StartListenTickerInfo(Ticker ticker) {
            if(TickersSocket == null) {
                StartListenTickersStream();
                if(!WaitUntil(5000, () => { return TickersSocketState == SocketConnectionState.Connected; }))
                    return;
            }
            if(!SimulationMode) {
                string name = ticker.SubscriptionName;
                string request = string.Format("{{ \"event\": \"subscribe\", \"channel\": [\"ticker\"], \"symbols\": [\"{0}\"] }}", name);
                TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.Ticker, ticker) {
                    Request = request
                }) ;
            }
        }

        protected override void StopListenTickerInfo(Ticker ticker) {
            if(TickersSocket == null)
                return;
            if(!SimulationMode) {
                string name = ticker.SubscriptionName;
                string request = string.Format("{{ \"event\": \"unsubscribe\", \"channel\": [\"ticker\"], \"symbols\": [\"{0}\"] }}", name);
                TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.Ticker, ticker) {
                    Request = request
                });
            }
        }

        public override void StopListenOrderBook(Ticker ticker, bool force) {
            if(TickersSocket == null)
                return;
            string name = ticker.SubscriptionName;
            string request = string.Format("{{ \"event\": \"unsubscribe\", \"channel\": [\"book_lv2\"], \"symbols\": [\"{0}\"] }}", name);
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.OrderBook, ticker) {
                Request = request,
            });
        }

        public override void StopListenTradeHistory(Ticker ticker, bool force) {
            if(TickersSocket == null)
                return;
            string name = ticker.SubscriptionName;
            string request = string.Format("{{ \"event\": \"unsubscribe\", \"channel\": [\"trades\"], \"symbols\": [\"{0}\"] }}", name);
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.TradeHistory, ticker) {
                Request = request,
            });
        }

        public override void StopListenKline(Ticker ticker, bool force) {
            if(TickersSocket == null)
                return;
            string name = ticker.SubscriptionName;
            string request = string.Format("{{ \"event\": \"unsubscribe\", \"channel\": [\"{1}\"], \"symbols\": [\"{0}\"] }}", name, GetCandleStickSubscribeChannel(ticker.CandleStickPeriodMin));
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.Kline, ticker) {
                Request = request,
            });
        }

        protected override void StartListenTradeHistoryCore(Ticker ticker) {
            if(TickersSocket == null) {
                StartListenTickersStream();
                if(!WaitUntil(5000, () => { return TickersSocketState == SocketConnectionState.Connected; }))
                    return;
            }
            string name = ticker.SubscriptionName;
            string request = string.Format("{{ \"event\": \"subscribe\", \"channel\": [\"trades\"], \"symbols\": [\"{0}\"] }}", name);
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.TradeHistory, ticker) {
                Request = request,
            });
        }

        protected override void StartListenKlineCore(Ticker ticker) {
            if(TickersSocket == null) {
                StartListenTickersStream();
                if(!WaitUntil(5000, () => { return TickersSocketState == SocketConnectionState.Connected; }))
                    return;
            }
            string name = ticker.SubscriptionName;
            string request = string.Format("{{ \"event\": \"subscribe\", \"channel\": [\"{1}\"], \"symbols\": [\"{0}\"] }}", name, GetCandleStickSubscribeChannel(ticker.CandleStickPeriodMin));
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.Kline, ticker) {
                Request = request,
            });
        }

        protected override void StartListenOrderBookCore(Ticker ticker) {
            if(TickersSocket == null) {
                StartListenTickersStream();
                if(!WaitUntil(5000, () => { return TickersSocketState == SocketConnectionState.Connected; }))
                    return;
            }
            if(!SimulationMode) {
                string name = ticker.SubscriptionName;

                string request = string.Format("{{ \"event\": \"subscribe\", \"channel\": [\"book_lv2\"], \"symbols\": [\"{0}\"] }}", name);
                TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.OrderBook, ticker) {
                    Request = request,
                });
            }
        }
        
        protected override bool IsListeningOrderBook(Ticker ticker) {
            return ((PoloniexTicker)ticker).IsListeningOrderBook;
        }

        public override void StopListenTickerStream(Ticker ticker) {
            base.StopListenTickerStream(ticker);
            ticker.IsOrderBookSubscribed = false;
            ticker.IsTradeHistorySubscribed = false;
            ticker.IsKlineSubscribed = false;
            TickersSocket.Unsubscribe(new WebSocketSubscribeInfo(SocketSubscribeType.OrderBook, ticker) {
                Request = new PoloniexWebSocketRequest { channel = ticker.CurrencyPair, command = "unsubscribe" }
            });
        }

        public override bool ObtainExchangeSettings() {
            return true;
        }

        public override bool AllowCandleStickIncrementalUpdate => true;

        public override List<CandleStickIntervalInfo> GetAllowedCandleStickIntervals() {
            List<CandleStickIntervalInfo> list = new List<CandleStickIntervalInfo>();
            list.Add(new CandleStickIntervalInfo() { Text = "5 Minutes", Interval = TimeSpan.FromSeconds(300), SubscribeChannel = "candles_minute_5" });
            list.Add(new CandleStickIntervalInfo() { Text = "15 Minutes", Interval = TimeSpan.FromSeconds(300), SubscribeChannel = "candles_minute_15" });
            list.Add(new CandleStickIntervalInfo() { Text = "30 Minutes", Interval = TimeSpan.FromSeconds(1800), SubscribeChannel = "candles_minute_30" });
            list.Add(new CandleStickIntervalInfo() { Text = "2 Hours", Interval = TimeSpan.FromSeconds(7200), SubscribeChannel = "candles_hour_2" });
            list.Add(new CandleStickIntervalInfo() { Text = "4 Hours", Interval = TimeSpan.FromSeconds(14400), SubscribeChannel = "candles_hour_4" });
            list.Add(new CandleStickIntervalInfo() { Text = "1 Day", Interval = TimeSpan.FromSeconds(86400), SubscribeChannel = "candles_day_1" });

            return list;
        }

        protected IDisposable TickersSubscriber { get; set; }

        public IDisposable ConnectOrderBook(PoloniexTicker ticker) {
            return null;
        }

        public override CurrencyInfo CreateCurrency(string currency) {
            return new PoloniexCurrencyInfo(this, currency);
        }

        public override ResizeableArray<CandleStickData> GetCandleStickData(Ticker ticker, int candleStickPeriodMin, DateTime start, long periodInSeconds) {
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

            JsonHelperToken root = JsonHelper.Default.Deserialize(bytes);

            DateTime startTime = new DateTime(1970, 1, 1);

            ResizeableArray<CandleStickData> list = new ResizeableArray<CandleStickData>(root.ItemsCount);

            //new string[] { "date", "high", "low", "open", "close", "volume", "quoteVolume", "weightedAverage" }
            int i_date = root.Items[0].GetPropertyIndex("date");
            int i_high = root.Items[0].GetPropertyIndex("high");
            int i_low = root.Items[0].GetPropertyIndex("low");
            int i_open = root.Items[0].GetPropertyIndex("open");
            int i_close = root.Items[0].GetPropertyIndex("close");
            int i_volume = root.Items[0].GetPropertyIndex("volume");
            int i_quoteVolume = root.Items[0].GetPropertyIndex("quoteVolume");
            int i_wa = root.Items[0].GetPropertyIndex("weightedAverage");

            for(int i = 0; i < root.ItemsCount; i++) {
                JsonHelperToken[] item = root.Items[i].Properties;
                CandleStickData data = new CandleStickData();
                long sec = FastValueConverter.ConvertPositiveLong(item[0].Value);
                data.Time = startTime.AddMilliseconds(sec);
                if(data.Time.Minute % candleStickPeriodMin != 0)
                    continue;
                if(list.Count > 0 && list.Last().Time == data.Time)
                    continue;
                data.High = FastValueConverter.Convert(item[i_high].Value);
                data.Low = FastValueConverter.Convert(item[i_low].Value);
                data.Open = FastValueConverter.Convert(item[i_open].Value);
                data.Close = FastValueConverter.Convert(item[i_close].Value);
                data.Volume = FastValueConverter.Convert(item[i_volume].Value);
                data.QuoteVolume = FastValueConverter.Convert(item[i_quoteVolume].Value);
                data.WeightedAverage = FastValueConverter.Convert(item[i_wa].Value);
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
                PoloniexCurrencyInfo c = (PoloniexCurrencyInfo)GetOrCreateCurrency(currency);
                c.MaxDailyWithdrawal = obj.Value<double>("maxDailyWithdrawal");
                c.TxFee = obj.Value<double>("txFee");
                c.MinConfirmation = obj.Value<double>("minConf");
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
            if(HasError(text))
                return false;
            ClearTickers();
            JObject res = JsonConvert.DeserializeObject<JObject>(text);
            int index = 0;
            foreach(JProperty prop in res.Children()) {
                PoloniexTicker t = (PoloniexTicker)GetOrCreateTicker(prop.Name);
                t.Index = index;
                t.CurrencyPair = prop.Name;
                if(PoloniexTickerCodesProvider.Codes.ContainsKey(t.CurrencyPair))
                    t.Code = PoloniexTickerCodesProvider.Codes[t.CurrencyPair];
                JObject obj = (JObject)prop.Value;
                t.Id = obj.Value<int>("id");
                t.LastString = obj.Value<string>("last");
                t.LowestAskString = obj.Value<string>("lowestAsk");
                t.HighestBidString = obj.Value<string>("highestBid");
                //t.Change = obj.Value<double>("percentChange");
                t.BaseVolumeString = obj.Value<string>("baseVolume");
                t.VolumeString = obj.Value<string>("quoteVolume");
                t.IsFrozen = obj.Value<int>("isFrozen") != 0;
                t.Hr24HighString = obj.Value<string>("high24hr");
                t.Hr24LowString = obj.Value<string>("low24hr");
                AddTicker(t);
                index++;
            }
            IsInitialized = true;
            return true;
        }

        private bool HasError(string text) {
            if(string.IsNullOrEmpty(text))
                return true;
            if(text.StartsWith("error"))
                return true;
            return false;
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
        public override bool UpdateOrderBook(Ticker ticker, int depth) {
            string address = GetOrderBookString(ticker, depth);
            byte[] data = GetDownloadBytes(address);
            return OnUpdateOrderBook(ticker, data);
        }
        public override void UpdateOrderBookAsync(Ticker ticker, int depth, Action<OperationResultEventArgs> onOrderBookUpdated) {
            string address = GetOrderBookString(ticker, depth);
            GetDownloadBytesAsync(address, t => {
                OnUpdateOrderBook(ticker, t.Result);
                onOrderBookUpdated(new OperationResultEventArgs() { Ticker = ticker, Result = t.Result != null });
            });
        }
        public override bool UpdateTicker(Ticker tickerBase) {
            return true;
        }
        public bool OnUpdateOrderBook(Ticker ticker, byte[] bytes) {
            if(bytes == null)
                return false;

            int startIndex = 1; // skip {
            if(!JsonHelper.Default.FindChar(bytes, ':', ref startIndex))
                return false;
            startIndex++;
            List<string[]> jasks = JsonHelper.Default.DeserializeArrayOfArrays(bytes, ref startIndex, 2);
            if(!JsonHelper.Default.FindChar(bytes, ',', ref startIndex))
                return false;
            startIndex++;
            if(!JsonHelper.Default.FindChar(bytes, ':', ref startIndex))
                return false;
            startIndex++;
            List<string[]> jbids = JsonHelper.Default.DeserializeArrayOfArrays(bytes, ref startIndex, 2);

            ticker.OrderBook.BeginUpdate();
            try {
                ticker.OrderBook.GetNewBidAsks();

                List<OrderBookEntry> bids = ticker.OrderBook.Bids;
                List<OrderBookEntry> asks = ticker.OrderBook.Asks;
                for(int i = 0; i < jbids.Count; i++) {
                    string[] item = jbids[i];
                    bids.Add(new OrderBookEntry() { ValueString = item[0], AmountString = item[1] });
                }
                for(int i = 0; i < jasks.Count; i++) {
                    string[] item = jasks[i];
                    asks.Add(new OrderBookEntry() { ValueString = item[0], AmountString = item[1] });
                }
            }
            finally {
                ticker.OrderBook.IsDirty = false;
                ticker.OrderBook.EndUpdate();
            }
            ticker.RaiseChanged();
            return true;
        }
        //public bool OnUpdateArbitrageOrderBook(Ticker ticker, string text, int depth) {
        //    if(string.IsNullOrEmpty(text))
        //        return false;

        //    Dictionary<string, object> res2 = null;
        //    lock(JsonParser) {
        //        res2 = JsonParser.Parse(text) as Dictionary<string, object>;
        //        if(res2 == null)
        //            return false;
        //    }

        //    List<object> bids = (List<object>)res2["bids"];
        //    List<object> asks = (List<object>)res2["asks"];

        //    ticker.OrderBook.GetNewBidAsks();
        //    int index = 0;
        //    List<OrderBookEntry> list = ticker.OrderBook.Bids;
        //    foreach(List<object> item in bids) {
        //        OrderBookEntry entry = new OrderBookEntry();
        //        list.Add(entry);
        //        entry.ValueString = (string)item.First();
        //        entry.AmountString = (string)item.Last();
        //        index++;
        //        if(index >= depth)
        //            break;
        //    }
        //    index = 0;
        //    list = ticker.OrderBook.Asks;
        //    foreach(List<object> item in asks) {
        //        OrderBookEntry entry = new OrderBookEntry();
        //        list.Add(entry);
        //        entry.ValueString = (string)item.First();
        //        entry.AmountString = (string)item.Last();
        //        index++;
        //        if(index >= depth)
        //            break;
        //    }

        //    ticker.OrderBook.UpdateEntries();
        //    return true;
        //}

        public string GetOrderBookString(Ticker ticker, int depth) {
            return string.Format("https://poloniex.com/public?command=returnOrderBook&currencyPair={0}&depth={1}",
                Uri.EscapeDataString(ticker.CurrencyPair), depth);
        }
        public override bool UpdateOrderBook(Ticker ticker) {
            return GetOrderBook(ticker, OrderBook.Depth);
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
        protected override bool HasDescendingTradesList => true;
        protected override ResizeableArray<TradeInfoItem> GetTradesCore(Ticker ticker, DateTime starTime, DateTime endTime) {
            string address = string.Format("https://poloniex.com/public?command=returnTradeHistory&currencyPair={0}&start={1}&end={2}", Uri.EscapeDataString(ticker.CurrencyPair), ToUnixTimestamp(starTime), ToUnixTimestamp(endTime));
            byte[] data = GetDownloadBytes(address);
            if(data == null || data.Length == 0)
                return null;
            var root = JsonHelper.Default.Deserialize(data);
            if(root.ItemsCount == 0)
                return null;

            ResizeableArray<TradeInfoItem> list = new ResizeableArray<TradeInfoItem>(root.ItemsCount);
            for(int i = root.ItemsCount - 1; i >= 0; i--) {
                var ji = root.Items[i];
                
                TradeInfoItem item = new TradeInfoItem(null, ticker);
                item.IdString = ji.Properties[1].Value;
                item.Time = Convert.ToDateTime(ji.Properties[2].Value).ToLocalTime();
                bool isBuy = ji.Properties[3].Value.Length == 3;
                
                item.RateString = ji.Properties[4].Value;
                item.AmountString = ji.Properties[5].Value;
                item.Type = isBuy ? TradeType.Buy : TradeType.Sell;
                list.Add(item);
            }
            return list;
        }

        protected List<TradeInfoItem> UpdateList { get; } = new List<TradeInfoItem>(100);

        protected List<TradeInfoItem> GetTradeVolumesForCandleStick(Ticker ticker, long start, long end) {
            string address = string.Format("https://poloniex.com/public?command=returnTradeHistory&currencyPair={0}&start={1}&end={2}", Uri.EscapeDataString(ticker.CurrencyPair), start, end);
            byte[] bytes = GetDownloadBytes(address);
            if(bytes == null)
                return null;

            JsonHelperToken root = JsonHelper.Default.Deserialize(bytes);
            if(root.ItemsCount == 0)
                return new List<TradeInfoItem>();

            List<TradeInfoItem> newTrades = new List<TradeInfoItem>(root.ItemsCount);
            for(int i = 0; i < root.ItemsCount; i++) {
                var obj = root.Items[i].Properties;
                TradeInfoItem item = new TradeInfoItem(null, ticker);
                item.TimeString = obj[2].Value;
                item.AmountString = obj[5].Value;
                item.Type = obj[3].Value.Length == 3 ? TradeType.Buy : TradeType.Sell;
                newTrades.Add(item);
            }
            return newTrades;
        }

        public override bool UpdateTrades(Ticker ticker) {
            string address = string.Format("https://poloniex.com/public?command=returnTradeHistory&currencyPair={0}", Uri.EscapeDataString(ticker.CurrencyPair));
            byte[] bytes = GetDownloadBytes(address);
            if(bytes == null)
                return true;
            
            var scheme = JsonHelper.Default.GetObjectScheme(Type + "/returnTradeHistory" ,bytes);
            if(scheme == null)
                return false;

            string lastGotTradeId = ticker.TradeHistory.Count > 0 ? ticker.TradeHistory.Last().IdString : null;

            //new string[] { "globalTradeID", "tradeID", "date", "type", "rate", "amount", "total" }
            int startIndex = 0;
            List<string[]> trades = JsonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, scheme.Names, 
                (itemIndex, props) => { return props[1] != lastGotTradeId; });

            ResizeableArray<TradeInfoItem> newTrades = new ResizeableArray<TradeInfoItem>(trades.Count);
            try {
                ticker.LockTrades();
                
                int i_tradeId = scheme.GetIndex("tradeID");
                int i_date = scheme.GetIndex("date");
                int i_type = scheme.GetIndex("type");
                int i_rate = scheme.GetIndex("rate");
                int i_amount = scheme.GetIndex("amount");
                int i_total = scheme.GetIndex("total");

                //ticker.ClearTradeHistory(); We don't need to clear history because we use this for incremental update (Poloniex)
                for(int i = trades.Count - 1; i >= 0; i--) {
                    string[] obj = trades[i];
                    TradeInfoItem item = new TradeInfoItem(null, ticker);
                    item.IdString = obj[i_tradeId];
                    item.TimeString = obj[i_date];
                    bool isBuy = obj[i_type].Length == 3;
                    item.Type = isBuy ? TradeType.Buy : TradeType.Sell;
                    item.AmountString = obj[i_amount];
                    item.RateString = obj[i_rate];
                    item.TotalString = obj[i_total];
                    newTrades.Add(item);
                    ticker.AddTradeHistoryItem(item);
                }
            }
            finally {
                ticker.UnlockTrades();
            }
            if(trades.Count > 0 && ticker.HasTradeHistorySubscribers) {
                TradeHistoryChangedEventArgs e = new TradeHistoryChangedEventArgs() { NewItems = newTrades };
                ticker.RaiseTradeHistoryChanged(e);
            } 
            return true;
        }

        public override bool UpdateAccountTrades(AccountInfo account, Ticker ticker) {
            string address = string.Format("https://poloniex.com/tradingApi");

            HttpRequestParamsCollection coll = new HttpRequestParamsCollection();
            coll.Add("nonce", GetNonce());
            coll.Add("command", "returnTradeHistory");
            if(ticker == null)
                coll.Add("currencyPair", "all");
            else
                coll.Add("currencyPair", ticker.MarketName);
            coll.Add("start", ToUnixTimestamp(DateTime.Now.AddYears(-1)).ToString());
            coll.Add("end", ToUnixTimestamp(DateTime.Now).ToString());

            MyWebClient client = GetWebClient();
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

        bool OnUpdateAccountTrades(AccountInfo account, Ticker ticker, byte[] data) {
            if(data == null)
                return false;

            var scheme = JsonHelper.Default.GetObjectScheme(Type + "/accounttrades", data);
            if(scheme == null)
                return false;

            List<TradeInfoItem> newTrades = null;
            ticker.LockAccountTrades();
            try {
                if(data.Length == 2) {
                    ticker.ClearMyTradeHistory();
                    return true; 
                }

                string lastGotTradeId = ticker.AccountTradeHistory.Count > 0 ? ticker.AccountTradeHistory.Last().IdString : null;
                int startIndex = 0;
                
                //new string[] { "globalTradeID", "tradeID", "date", "rate", "amount", "total", "fee", "orderNumber", "type", "category" };
                List<string[]> trades = JsonHelper.Default.DeserializeArrayOfObjects(data, ref startIndex, scheme.Names,
                    (itemIndex, props) => { return props[1] != lastGotTradeId; });
                if(trades == null || trades.Count == 0)
                    return true;
                
                newTrades = new List<TradeInfoItem>(trades.Count);

                int i_globalId = scheme.GetIndex("globalTradeID");
                int i_tradeId = scheme.GetIndex("tradeID");
                int i_date = scheme.GetIndex("date");
                int i_type = scheme.GetIndex("type");
                int i_rate = scheme.GetIndex("rate");
                int i_amount = scheme.GetIndex("amount");
                int i_total = scheme.GetIndex("total");
                int i_fee = scheme.GetIndex("fee");
                int i_orderNumber = scheme.GetIndex("orderNumber");

                for(int ti = trades.Count - 1; ti >= 0; ti--) {
                    string[] obj = trades[ti];

                    TradeInfoItem item = new TradeInfoItem(account, ticker);
                    item.IdString = obj[i_tradeId];
                    item.TimeString = obj[i_date];
                    item.GlobalId = obj[i_globalId];
                    item.OrderNumber = obj[i_orderNumber];

                    bool isBuy = obj[i_type].Length == 3;
                    item.Type = isBuy ? TradeType.Buy : TradeType.Sell;
                    item.AmountString = obj[i_amount];
                    item.RateString = obj[i_rate];
                    item.TotalString = obj[i_total];
                    item.FeeString = obj[i_fee];

                    newTrades.Add(item);
                    ticker.AddAccountTradeHistoryItem(item);
                }
            }
            finally {
                ticker.UnlockAccountTrades();
                ticker.RaiseAccountTradeHistoryChanged(new TradeHistoryChangedEventArgs() { NewItems = newTrades });
            }
            return true;
        }

        public override bool UpdateBalances(AccountInfo account) {
            if(account == null)
                return false;
            string address = string.Format("https://poloniex.com/tradingApi");

            HttpRequestParamsCollection coll = new HttpRequestParamsCollection();
            coll.Add("command", "returnCompleteBalances");
            coll.Add("nonce", GetNonce());

            MyWebClient client = GetWebClient();
            client.Headers.Clear();
            string queryString = ToQueryString(coll);
            client.Headers.Add("Key", account.ApiKey);
            client.Headers.Add("Sign", account.GetSign(queryString));

            try {
                return OnGetBalances(account, client.UploadValues(address, coll));
            }
            catch(Exception) {
                return false;
            }
        }
        public Task<byte[]> GetBalancesAsync(AccountInfo account) {
            string address = string.Format("https://poloniex.com/tradingApi");

            HttpRequestParamsCollection coll = new HttpRequestParamsCollection();
            coll.Add("command", "returnCompleteBalances");
            coll.Add("nonce", GetNonce());

            MyWebClient client = GetWebClient();
            client.Headers.Clear();
            string queryString = ToQueryString(coll);
            client.Headers.Add("Sign", account.GetSign(queryString));
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
                        Debug.WriteLine("OnGetBalances fails: " + prop.Value.Value<string>());
                        return false;
                    }
                    JObject obj = (JObject)prop.Value;
                    var binfo = account.GetOrCreateBalanceInfo(prop.Name);
                    binfo.LastAvailable = binfo.Available;
                    binfo.Available = obj.Value<double>("available");
                    binfo.OnOrders = obj.Value<double>("onOrders");
                    binfo.Balance = binfo.Available + binfo.OnOrders;
                }
            }
            return true;
        }

        public override bool GetDeposit(AccountInfo account, CurrencyInfo currency) {
            return GetDeposites(account);
        }

        public override bool GetDeposites(AccountInfo account) {
            Task<byte[]> task = GetDepositesAsync(account);
            task.Wait(10000);
            return OnGetDeposites(account, task.Result);
        }
        public Task<byte[]> GetDepositesAsync(AccountInfo account) {
            string address = string.Format("https://poloniex.com/tradingApi");

            HttpRequestParamsCollection coll = new HttpRequestParamsCollection();
            coll.Add("command", "returnDepositAddresses");
            coll.Add("nonce", GetNonce());

            MyWebClient client = GetWebClient();
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
            return ((long)((DateTime.UtcNow - new DateTime(1,1,1)).TotalMilliseconds * 10000)).ToString();
        }
        public override bool UpdateOpenedOrders(AccountInfo account, Ticker ticker) {
            if(account == null)
                return false;
            string address = string.Format("https://poloniex.com/tradingApi");

            HttpRequestParamsCollection coll = new HttpRequestParamsCollection();
            coll.Add("nonce", GetNonce());
            coll.Add("command", "returnOpenOrders");
            coll.Add("currencyPair", ticker == null ? "all" : ticker.MarketName);

            MyWebClient client = GetWebClient();
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

            HttpRequestParamsCollection coll = new HttpRequestParamsCollection();
            coll.Add("nonce", GetNonce());
            coll.Add("command", "returnOpenOrders");
            coll.Add("currencyPair", "all");

            MyWebClient client = GetWebClient();
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
                if(ticker != null)
                    ticker.SaveOpenedOrders();
                openedOrders.Clear();
                if(ticker == null) {
                    JObject res = JsonConvert.DeserializeObject<JObject>(text);
                    foreach(JProperty prop in res.Children()) {
                        if(prop.Name == "error") {
                            Telemetry.Default.TrackEvent("poloniex.ongetopenedorders", new string[] { "error", prop.Value<string>() }, true);
                            Debug.WriteLine("OnGetOpenedOrders fails: " + prop.Value<string>());
                            return false;
                        }
                        ticker = null;
                        for(int index = 0; index < Tickers.Count; index++) {
                            Ticker tt = Tickers[index];
                            if(tt.CurrencyPair == prop.Name) {
                                ticker = tt;
                                break;
                            }
                        }
                        JArray array = (JArray)prop.Value;
                        for(int i = 0; i < array.Count; i++) {
                            JObject obj = (JObject) array[i];
                            OpenedOrderInfo info = CreateOrderInfo(account, ticker, obj);
                            openedOrders.Add(info);
                        }
                    }
                }
                else {
                    object objRes = JsonConvert.DeserializeObject(text);
                    if(objRes is JObject) {
                        Debug.WriteLine(objRes.ToString());
                        return false;
                    }
                    JArray array = objRes as JArray;
                    if(array != null) {
                        for(int i = 0; i < array.Count; i++) {
                            JObject obj = (JObject) array[i];
                            OpenedOrderInfo info = CreateOrderInfo(account, ticker, obj);
                            openedOrders.Add(info);
                        }
                        if(ticker != null)
                            ticker.RaiseOpenedOrdersChanged();
                    }
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

        public override TradingResult BuyLong(AccountInfo account, Ticker ticker, double rate, double amount) {
            string address = string.Format("https://poloniex.com/tradingApi");

            HttpRequestParamsCollection coll = new HttpRequestParamsCollection();
            coll.Add("command", "buy");
            coll.Add("nonce", GetNonce());
            coll.Add("currencyPair", ticker.CurrencyPair);
            coll.Add("rate", rate.ToString("0.00000000"));
            coll.Add("amount", amount.ToString("0.00000000"));

            MyWebClient client = GetWebClient();
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

        public override TradingResult SellLong(AccountInfo account, Ticker ticker, double rate, double amount) {
            string address = string.Format("https://poloniex.com/tradingApi");

            HttpRequestParamsCollection coll = new HttpRequestParamsCollection();
            coll.Add("command", "sell");
            coll.Add("nonce", GetNonce());
            coll.Add("currencyPair", ticker.CurrencyPair);
            coll.Add("rate", rate.ToString("0.00000000"));
            coll.Add("amount", amount.ToString("0.00000000"));

            MyWebClient client = GetWebClient();
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

        public override TradingResult BuyShort(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public override TradingResult SellShort(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public TradingResult OnBuy(AccountInfo account, Ticker ticker, byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if(string.IsNullOrEmpty(text))
                return null;
            JObject res = JsonConvert.DeserializeObject<JObject>(text);
            TradingResult tr = new TradingResult();
            tr.Ticker = ticker;
            tr.OrderId = res.Value<string>("orderNumber");
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

        protected virtual bool OnCreateDeposit(AccountInfo account, string currency, byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if(string.IsNullOrEmpty(text))
                return false;
            JObject res = JsonConvert.DeserializeObject<JObject>(text);
            if(res.Value<int>("success") != 1)
                return false;
            string deposit = res.Value<string>("response");
            PoloniexAccountBalanceInfo info = (PoloniexAccountBalanceInfo)account.Balances.FirstOrDefault(b => b.Currency == currency);
            info.DepositAddress = deposit;
            return true;
        }

        public TradingResult OnSell(AccountInfo account, Ticker ticker, byte[] data) {
            string text = System.Text.Encoding.ASCII.GetString(data);
            if(string.IsNullOrEmpty(text))
                return null;
            JObject res = JsonConvert.DeserializeObject<JObject>(text);
            TradingResult t = new TradingResult();
            t.Ticker = ticker;
            t.Trades.Add(new TradeEntry() { Id = res.Value<string>("orderNumber") });
            return t;
        }

        public override bool Cancel(AccountInfo account, Ticker ticker, string orderId) {
            string address = string.Format("https://poloniex.com/tradingApi");

            HttpRequestParamsCollection coll = new HttpRequestParamsCollection();
            coll.Add("command", "cancelOrder");
            coll.Add("nonce", GetNonce());
            coll.Add("orderNumber", orderId);

            MyWebClient client = GetWebClient();
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

            HttpRequestParamsCollection coll = new HttpRequestParamsCollection();
            coll.Add("command", "cancelOrder");
            coll.Add("nonce", GetNonce());
            coll.Add("orderNumber", orderId);

            MyWebClient client = GetWebClient();
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

            HttpRequestParamsCollection coll = new HttpRequestParamsCollection();
            coll.Add("command", "withdraw");
            coll.Add("nonce", GetNonce());
            coll.Add("currency", currency);
            coll.Add("amount", amount.ToString("0.00000000"));
            coll.Add("address", address);
            if(!string.IsNullOrEmpty(paymentId))
                coll.Add("paymentId", paymentId);

            MyWebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", account.GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", account.ApiKey);
            return client.UploadValuesTaskAsync(address, coll);
        }

        public override bool Withdraw(AccountInfo account, string currency, string addr, string paymentId, double amount) {
            string address = string.Format("https://poloniex.com/tradingApi");

            HttpRequestParamsCollection coll = new HttpRequestParamsCollection();
            coll.Add("command", "withdraw");
            coll.Add("nonce", GetNonce());
            coll.Add("currency", currency);
            coll.Add("amount", amount.ToString("0.00000000"));
            coll.Add("address", address);
            if(!string.IsNullOrEmpty(paymentId))
                coll.Add("paymentId", paymentId);

            MyWebClient client = GetWebClient();
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
        public override bool CreateDeposit(AccountInfo account, string currency) {
            string address = string.Format("https://poloniex.com/tradingApi");

            HttpRequestParamsCollection coll = new HttpRequestParamsCollection();
            coll.Add("command", "generateNewAddress");
            coll.Add("nonce", GetNonce());
            coll.Add("currency", currency);

            MyWebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("Sign", account.GetSign(ToQueryString(coll)));
            client.Headers.Add("Key", account.ApiKey);
            try {
                byte[] data = client.UploadValues(address, coll);
                return OnCreateDeposit(account, currency, data);
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }
        protected override void AddRefOrderBook(Ticker ticker) {
        }
        protected override void AddRefKline(Ticker ticker) {
        }
        protected override void AddRefTradeHistory(Ticker ticker) {
        }
        protected internal override void ApplyCapturedEvent(Ticker ticker, TickerCaptureDataInfo info) {
            if(info.StreamType == CaptureStreamType.OrderBook || info.StreamType == CaptureStreamType.TradeHistory) {
                OnTickersSocketMessageReceived(this, new MessageReceivedEventArgs(info.Message));
            }
        }
    }

    public delegate void TickerUpdateEventHandler(object sender, TickerUpdateEventArgs e);
    public class TickerUpdateEventArgs : EventArgs {
        public Ticker Ticker { get; set; }
    }
 }
