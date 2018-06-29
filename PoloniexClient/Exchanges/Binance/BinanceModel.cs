using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptoMarketClient.Common;
using CryptoMarketClient.Exchanges.Binance;
using CryptoMarketClient.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuperSocket.ClientEngine;
using WebSocket4Net;

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

        protected internal override IIncrementalUpdateDataProvider CreateIncrementalUpdateDataProvider() {
            return new BinanceIncrementalUpdateDataProvider();
        }

        public override void OnAccountRemoved(ExchangeAccountInfo info) {
            
        }

        public override string BaseWebSocketAddress => "wss://stream.binance.com:9443/ws/!ticker@arr";

        public override ExchangeType Type => ExchangeType.Binance;

        public override bool SupportWebSocket(WebSocketType type) {
            if(type == WebSocketType.Tickers)
                return true;
            if(type == WebSocketType.Ticker)
                return true;
            return false;
        }

        Dictionary<WebSocket, SocketConnectionInfo> OrderBookSockets { get; } = new Dictionary<WebSocket, SocketConnectionInfo>();

        public override void StartListenTickerStream(Ticker ticker) {
            base.StartListenTickerStream(ticker);
            StopListenTickerStream(ticker);
            SocketConnectionInfo info = CreateOrderBookWebSocket(ticker);
            OrderBookSockets.Add(info.Socket, info);
        }

        SocketConnectionInfo CreateOrderBookWebSocket(Ticker ticker) {
            SocketConnectionInfo info = new SocketConnectionInfo();
            string adress = BaseWebSocketAddress + "/" + ticker.Name.ToLower() + "@depth5";
            info.Ticker = ticker;
            info.Adress = adress;
            info.Socket = new WebSocket(adress, "");
            info.Socket.Error += OnOrderBookSocketError;
            info.Socket.Opened += OnOrderBookSocketOpened;
            info.Socket.Closed += OnOrderBookSocketClosed;
            info.Socket.MessageReceived += OnOrderBookSocketMessageReceived;
            info.Open();

            return info;
        }

        private void OnOrderBookSocketMessageReceived(object sender, MessageReceivedEventArgs e) {
            LastWebSocketRecvTime = DateTime.Now;
            SocketConnectionInfo info = OrderBookSockets[(WebSocket)sender];
            const string hour24TickerStart = "[{\"e\"";
            const string orderBookStart = "{\"lastUpdateId\"";
            if(e.Message.StartsWith(hour24TickerStart)) {
                OnTickersSocketMessageReceived(sender, e);
                return;
            }

            else if(e.Message.StartsWith(orderBookStart)) {
                OnIncrementalOrderBookUpdateRecv(OrderBookSockets[(WebSocket)sender].Ticker, Encoding.Default.GetBytes(e.Message));
                return;
            }
        }

        protected string[] OrderBookStartItems { get; } = new string[] { "lastUpdateId"};

        void OnIncrementalOrderBookUpdateRecv(Ticker ticker, byte[] bytes) {
            int startIndex = 0;
            string[] updateId = JSonHelper.Default.StartDeserializeObject(bytes, ref startIndex, OrderBookStartItems);

            long seqNumber = FastValueConverter.ConvertPositiveLong(updateId[0]);
            if(seqNumber <= ticker.OrderBook.Updates.SeqNumber)
                return;

            const string bidString = "\"bids\":";
            const string askString = "\"asks\":";

            startIndex += bidString.Length + 1;
            List<string[]> jbids = JSonHelper.Default.DeserializeArrayOfArrays(bytes, ref startIndex, 3);
            startIndex += askString.Length + 1;
            List<string[]> jasks = JSonHelper.Default.DeserializeArrayOfArrays(bytes, ref startIndex, 3);

            long hackedNextSeqNumber = ticker.OrderBook.Updates.SeqNumber;
            ticker.OrderBook.Updates.Push(hackedNextSeqNumber, ticker, jbids, jasks, null);
            OnIncrementalUpdateRecv(ticker.OrderBook.Updates);
        }

        private void OnOrderBookSocketClosed(object sender, EventArgs e) {
            SocketConnectionInfo info = OrderBookSockets[(WebSocket)sender];
            info.State = SocketConnectionState.Disconnected;
        }

        private void OnOrderBookSocketOpened(object sender, EventArgs e) {
            SocketConnectionInfo info = OrderBookSockets[(WebSocket)sender];
            info.State = SocketConnectionState.Connected;
            info.Ticker.UpdateOrderBook();
        }

        private void OnOrderBookSocketError(object sender, ErrorEventArgs e) {
            SocketConnectionInfo info = OrderBookSockets[(WebSocket)sender];
            info.State = SocketConnectionState.Error;
            info.LastError = e.Exception.ToString();
        }

        protected SocketConnectionInfo GetConnectionInfo(Ticker ticker, Dictionary<WebSocket, SocketConnectionInfo> dictionary) {
            foreach(SocketConnectionInfo info in dictionary.Values) {
                if(info.Ticker == ticker) {
                    return info;
                }
            }
            return null;
        }

        public override void StopListenTickerStream(Ticker ticker) {
            base.StopListenTickerStream(ticker);
            SocketConnectionInfo info = GetConnectionInfo(ticker, OrderBookSockets);
            if(info == null)
                return;

            info.Socket.Error -= OnOrderBookSocketError;
            info.Socket.Opened -= OnOrderBookSocketOpened;
            info.Socket.Closed -= OnOrderBookSocketClosed;
            info.Socket.MessageReceived -= OnOrderBookSocketMessageReceived;
            info.Close();
            info.Dispose();
            OrderBookSockets.Remove(info.Socket);
        }

        string[] webSocketTickersInfo;
        protected string[] WebSocketTickersInfo {
            get {
                if(webSocketTickersInfo == null) {
                    webSocketTickersInfo = new string[] {
                        "e",
                        "E",
                        "s",
                        "p",
                        "P",
                        "w",
                        "x",
                        "c",
                        "Q",
                        "b",
                        "B",
                        "a",
                        "A",
                        "o",
                        "h",
                        "l",
                        "v",
                        "q",
                        "O",
                        "C",
                        "F",
                        "L",
                        "n"
                    };
                }
                return webSocketTickersInfo;
            }
        }
        protected override void OnTickersSocketMessageReceived(object sender, MessageReceivedEventArgs e) {
            base.OnTickersSocketMessageReceived(sender, e);
            byte[] data = Encoding.Default.GetBytes(e.Message);
            int startIndex = 0;
            List<string[]> items = JSonHelper.Default.DeserializeArrayOfObjects(data, ref startIndex, WebSocketTickersInfo);
            foreach(string[] item in items) {
                string eventType = item[0];
                if(eventType == "24hrTicker")
                    On24HourTickerRecv(item);
            }
        }

        protected void On24HourTickerRecv(string[] item) {
            string symbolName = item[2];
            BinanceTicker t = (BinanceTicker)Tickers.FirstOrDefault(tt => tt.Name == symbolName);
            if(t == null)
                throw new DllNotFoundException("binance symbol not found " + symbolName);
            t.Change = FastValueConverter.Convert(item[4]);
            t.HighestBid = FastValueConverter.Convert(item[9]);
            t.LowestAsk = FastValueConverter.Convert(item[11]);
            t.Hr24High = FastValueConverter.Convert(item[14]);
            t.Hr24Low = FastValueConverter.Convert(item[15]);
            t.BaseVolume = FastValueConverter.Convert(item[16]);
            t.Volume = FastValueConverter.Convert(item[17]);

            t.UpdateTrailings();

            lock(t) {
                RaiseTickerUpdate(t);
            }
        }

        public override bool GetDeposites() {
            return true;
        }

        public override Form CreateAccountForm() {
            return null;
        }
        
        public override void ObtainExchangeSettings() {
            string address = "https://api.binance.com/api/v1/exchangeInfo";
            string text = string.Empty;
            try {
                text = GetDownloadString(address);
            }
            catch(Exception) {
                return;
            }
            if(string.IsNullOrEmpty(text))
                return;

            JObject settings = JsonConvert.DeserializeObject<JObject>(text);
            JArray rateLimits = settings.Value<JArray>("rateLimits");
            RequestRate = new List<RateLimit>();
            OrderRate = new List<RateLimit>();
            foreach(JObject rateLimit in rateLimits) {
                string rateType = rateLimit.Value<string>("rateLimitType");
                if(rateType == "REQUESTS")
                    RequestRate.Add(GetRateLimit(rateLimit));
                if(rateType == "ORDERS")
                    OrderRate.Add(GetRateLimit(rateLimit));
            }

            JArray symbols = settings.Value<JArray>("symbols");
            foreach(JObject s in symbols) {
                BinanceTicker t = new BinanceTicker(this);
                t.CurrencyPair = s.Value<string>("symbol");
                t.MarketCurrency = s.Value<string>("baseAsset");
                t.BaseCurrency = s.Value<string>("quoteAsset");
                Tickers.Add(t);

                JArray filters = s.Value<JArray>("filters");
                foreach(JObject filter in filters) {
                    string filterType = filter.Value<string>("filterType");
                    if(filterType == "PRICE_FILTER")
                        t.PriceFilter = new TickerFilter() { MinValue = filter.Value<double>("minPrice"), MaxValue = filter.Value<double>("maxPrice"), TickSize = filter.Value<double>("tickSize") };
                    else if(filterType == "LOT_SIZE")
                        t.QuantityFilter = new TickerFilter() { MinValue = filter.Value<double>("minQty"), MaxValue = filter.Value<double>("maxQty"), TickSize = filter.Value<double>("stepSize") };
                }
            }
        }
        protected RateLimit GetRateLimit(JObject jObject) {
            if(jObject.Value<string>("interval") == "MINUTE")
                return new RateLimit() { Limit = jObject.Value<int>("limit") - 1, Interval = TimeSpan.TicksPerMinute };
            else if(jObject.Value<string>("interval") == "DAY")
                return new RateLimit() { Limit = jObject.Value<int>("limit") - 1, Interval = TimeSpan.TicksPerDay };
            else if(jObject.Value<string>("interval") == "SECOND")
                return new RateLimit() { Limit = jObject.Value<int>("limit") - 1, Interval = TimeSpan.TicksPerSecond };
            return new RateLimit(); 
        }

        public override bool AllowCandleStickIncrementalUpdate => false;

        public override bool CancelOrder(Ticker ticker, OpenedOrderInfo info) {
            throw new NotImplementedException();
        }

        public override List<CandleStickIntervalInfo> GetAllowedCandleStickIntervals() {
            List<CandleStickIntervalInfo> list = new List<CandleStickIntervalInfo>();

            list.Add(new CandleStickIntervalInfo() { Text = "1 Minute", Interval = TimeSpan.FromSeconds(60) });
            list.Add(new CandleStickIntervalInfo() { Text = "3 Minutes", Interval = TimeSpan.FromSeconds(180) });
            list.Add(new CandleStickIntervalInfo() { Text = "5 Minutes", Interval = TimeSpan.FromSeconds(300) });
            list.Add(new CandleStickIntervalInfo() { Text = "15 Minutes", Interval = TimeSpan.FromSeconds(900) });
            list.Add(new CandleStickIntervalInfo() { Text = "30 Minutes", Interval = TimeSpan.FromSeconds(1800) });
            list.Add(new CandleStickIntervalInfo() { Text = "1 Hour", Interval = TimeSpan.FromSeconds(3600) });
            list.Add(new CandleStickIntervalInfo() { Text = "2 Hours", Interval = TimeSpan.FromSeconds(7200) });
            list.Add(new CandleStickIntervalInfo() { Text = "4 Hours", Interval = TimeSpan.FromSeconds(14400) });
            list.Add(new CandleStickIntervalInfo() { Text = "6 Hour", Interval = TimeSpan.FromSeconds(21600) });
            list.Add(new CandleStickIntervalInfo() { Text = "8 Hours", Interval = TimeSpan.FromSeconds(28800) });
            list.Add(new CandleStickIntervalInfo() { Text = "12 Hours", Interval = TimeSpan.FromSeconds(43200) });
            list.Add(new CandleStickIntervalInfo() { Text = "1 Day", Interval = TimeSpan.FromSeconds(86400) });
            list.Add(new CandleStickIntervalInfo() { Text = "3 Days", Interval = TimeSpan.FromSeconds(259200) });
            list.Add(new CandleStickIntervalInfo() { Text = "1 Week", Interval = TimeSpan.FromSeconds(604800) });

            return list;
        }

        public override bool GetTickersInfo() {
            bool result = UpdateTickersInfo();
            IsInitialized = true;
            return result;
        }

        public override List<TradeHistoryItem> GetTrades(Ticker ticker, DateTime starTime) {
            return new List<TradeHistoryItem>();
            //throw new NotImplementedException();
        }

        public override bool ProcessOrderBook(Ticker tickerBase, string text) {
            return true;
            //throw new NotImplementedException();
        }

        public override bool UpdateBalances() {
            return true;
            //throw new NotImplementedException();
        }

        public override bool UpdateCurrencies() {
            return true;
            //throw new NotImplementedException();
        }

        public override bool UpdateMyTrades(Ticker ticker) {
            return true;
            //throw new NotImplementedException();
        }

        public override bool UpdateOpenedOrders(Ticker tickerBase) {
            return true;
            //throw new NotImplementedException();
        }

        public override bool UpdateOrderBook(Ticker ticker) {
            string address = string.Format("https://api.binance.com/api/v1/depth?symbol={0}&limit={1}",
                Uri.EscapeDataString(ticker.CurrencyPair), 1000);
            byte[] bytes = ((Ticker)ticker).DownloadBytes(address);
            if(bytes == null || bytes.Length == 0)
                return false;
            OnUpdateOrderBook(ticker, bytes);
            return true;
        }

        void OnUpdateOrderBook(Ticker ticker, byte[] bytes) {
            int startIndex = 0;
            string[] updateId = JSonHelper.Default.StartDeserializeObject(bytes, ref startIndex, OrderBookStartItems);

            const string bidString = "\"bids\":";
            const string askString = "\"asks\":";

            startIndex += bidString.Length + 1;
            List<string[]> jbids = JSonHelper.Default.DeserializeArrayOfArrays(bytes, ref startIndex, 3);
            startIndex += askString.Length + 1;
            List<string[]> jasks = JSonHelper.Default.DeserializeArrayOfArrays(bytes, ref startIndex, 3);

            List<OrderBookEntry> bids = ticker.OrderBook.Bids;
            List<OrderBookEntry> asks = ticker.OrderBook.Asks;
            List<OrderBookEntry> iasks = ticker.OrderBook.AsksInverted;

            bids.Clear();
            asks.Clear();
            iasks.Clear();

            foreach(string[] item in jbids)
                bids.Add(new OrderBookEntry() { ValueString = item[0], AmountString = item[1] });

            foreach(string[] item in jasks) {
                OrderBookEntry e = new OrderBookEntry() { ValueString = item[0], AmountString = item[1] };
                asks.Add(e);
                iasks.Insert(0, e);
            }

            ticker.OrderBook.Updates.Clear(FastValueConverter.ConvertPositiveLong(updateId[0]) + 1);
            ticker.OrderBook.UpdateEntries();
            ticker.OrderBook.RaiseOnChanged(new IncrementalUpdateInfo());
            ticker.RaiseChanged();
        }

        public override bool UpdateTicker(Ticker tickerBase) {
            return true;
            //throw new NotImplementedException();
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
            JArray res = JsonConvert.DeserializeObject<JArray>(text);
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

        public override bool UpdateTrades(Ticker tickerBase) {
            return true;
            //throw new NotImplementedException();
        }
    }
}
