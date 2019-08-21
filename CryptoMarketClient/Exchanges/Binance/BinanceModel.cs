using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptoMarketClient.Common;
using CryptoMarketClient.Exchanges.Binance;
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

        public override void OnAccountRemoved(AccountInfo info) {
            
        }

        public override string CreateDeposit(AccountInfo account, string currency) {
            throw new NotImplementedException();
        }
         
        public override bool GetBalance(AccountInfo info, string currency) {
            return true;
        }

        public override bool Withdraw(AccountInfo account, string currency, string adress, string paymentId, double amount) {
            throw new NotImplementedException();
        }

        public override TradingResult Buy(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public override TradingResult Sell(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public override string BaseWebSocketAdress => "wss://stream.binance.com:9443/ws/!ticker@arr";

        public override ExchangeType Type => ExchangeType.Binance;

        public override bool SupportWebSocket(WebSocketType type) {
            if(type == WebSocketType.Tickers)
                return true;
            if(type == WebSocketType.Ticker)
                return true;
            return false;
        }
        
        public override void StartListenKlineStream(Ticker ticker, CandleStickIntervalInfo klineInfo) {
            SocketConnectionInfo info = CreateKlineWebSocket(ticker, klineInfo);
            KlineSockets.Add(info.Socket, info);
        }

        protected virtual SocketConnectionInfo CreateKlineWebSocket(Ticker ticker, CandleStickIntervalInfo klineInfo) {
            SocketConnectionInfo info = new SocketConnectionInfo();
            string adress = "wss://stream.binance.com:9443/ws/" + ticker.Name.ToLower() + "@kline_" + klineInfo.Command;
            info.Ticker = ticker;
            info.KlineInfo = klineInfo;
            info.Adress = adress;
            info.Socket = new WebSocket(adress, "");
            info.Socket.Error += OnKlineSocketError;
            info.Socket.Opened += OnKlineSocketOpened;
            info.Socket.Closed += OnKlineSocketClosed;
            info.Socket.MessageReceived += OnKlineSocketMessageReceived;
            info.Open();

            return info;
        }

        public override void StopListenKlineStream(Ticker ticker, CandleStickIntervalInfo klineInfo) {
            SocketConnectionInfo info = GetConnectionInfo(ticker, klineInfo, KlineSockets);
            if(info == null)
                return;

            info.Socket.Error -= OnKlineSocketError;
            info.Socket.Opened -= OnKlineSocketOpened;
            info.Socket.Closed -= OnKlineSocketClosed;
            info.Socket.MessageReceived -= OnKlineSocketMessageReceived;
            info.Close();
            info.Dispose();
            KlineSockets.Remove(info.Socket);
        }

        string[] klineStartItems;
        protected string[] KlineStartItems {
            get {
                if(klineStartItems == null) {
                    klineStartItems = new string[] {
                        "e",
                        "E",
                        "s",
                    };
                }
                return klineStartItems;
            }
        }


        string[] klineItems;
        protected string[] KlineItems {
            get {
                if(klineItems == null) {
                    klineItems = new string[] {
                        "t", // Kline start time
                        "T", // Kline close time
                        "s", // Symbol
                        "i", // Interval
                        "f", // First trade ID
                        "L", // Last trade ID
                        "o", // Open price
                        "c", // Close price
                        "h", // High price
                        "l", // Low price
                        "v", // Base asset volume
                        "n", // Number of trades
                        "x", // Is this kline closed?
                        "q", // Quote asset volume
                        "V", // Taker buy base asset volume
                        "Q", // Taker buy quote asset volume
                        "B"  // Ignore 
                    };
                }
                return klineItems;
            }
        }

        private void OnKlineSocketMessageReceived(object sender, MessageReceivedEventArgs e) {
            Debug.WriteLine(e.Message);
            byte[] bytes = Encoding.Default.GetBytes(e.Message);
            int startIndex = 0;
            string[] startItems = JSonHelper.Default.StartDeserializeObject(bytes, ref startIndex, KlineStartItems);
            startIndex++; // skip,
            if(!JSonHelper.Default.FindChar(bytes, ':', ref startIndex))
                return;
            startIndex++; // skip :
            if(!JSonHelper.Default.FindChar(bytes, '{', ref startIndex))
                return;

            string[] kline = JSonHelper.Default.DeserializeObject(bytes, ref startIndex, KlineItems);
            SocketConnectionInfo info = KlineSockets[(WebSocket)sender];
            OnKlineItemRecv(info.Ticker, kline);
        }
        protected virtual void OnKlineItemRecv(Ticker ticker, string[] item) {
            long dt = FastValueConverter.ConvertPositiveLong(item[0]);
            DateTime time = FromUnixTime(dt);
            CandleStickIntervalInfo info = AllowedCandleStickIntervals.FirstOrDefault(i => i.Command == item[3]);
            if(ticker.CandleStickPeriodMin != info.Interval.TotalMinutes)
                return;
            Debug.WriteLine(item[6] + " " + item[7] + " " + item[8] + " " + item[9]);
            lock(ticker.CandleStickData) {
                CandleStickData data = ticker.GetOrCreateCandleStickData(time);
                data.Open = FastValueConverter.Convert(item[6]);
                data.Close = FastValueConverter.Convert(item[7]);
                data.High = FastValueConverter.Convert(item[8]);
                data.Low = FastValueConverter.Convert(item[9]);
                data.Volume = FastValueConverter.Convert(item[10]);
                data.QuoteVolume = FastValueConverter.Convert(item[13]);
                ticker.RaiseCandleStickChanged();
            }
        }

        private void OnKlineSocketClosed(object sender, EventArgs e) {
            SocketConnectionInfo info = KlineSockets[(WebSocket)sender];
            info.State = SocketConnectionState.Disconnected;
        }

        private void OnKlineSocketOpened(object sender, EventArgs e) {
            SocketConnectionInfo info = KlineSockets[(WebSocket)sender];
            info.State = SocketConnectionState.Connected;
        }

        private void OnKlineSocketError(object sender, ErrorEventArgs e) {
            SocketConnectionInfo info = KlineSockets[(WebSocket)sender];
            info.State = SocketConnectionState.Error;
            info.LastError = e.Exception.ToString();
        }

        public override void StartListenTickerStream(Ticker ticker) {
            base.StartListenTickerStream(ticker);
            SocketConnectionInfo info = CreateOrderBookWebSocket(ticker);
            OrderBookSockets.Add(info.Socket, info);
            SocketConnectionInfo tradeInfo = CreateTradesWebSocket(ticker);
            TradeHistorySockets.Add(tradeInfo.Socket, tradeInfo);
        }

        protected override string GetOrderBookSocketAddress(Ticker ticker) {
            return "wss://stream.binance.com:9443/ws/" + ticker.Name.ToLower() + "@depth5";
        }

        protected override string GetTradeSocketAddress(Ticker ticker) {
            return "wss://stream.binance.com:9443/ws/" + ticker.Name.ToLower() + "@trade";
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

        protected override void OnOrderBookSocketMessageReceived(object sender, MessageReceivedEventArgs e) {
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

        protected override void OnOrderBookSocketClosed(object sender, EventArgs e) {
            SocketConnectionInfo info = OrderBookSockets[(WebSocket)sender];
            info.State = SocketConnectionState.Disconnected;
        }

        protected override void OnOrderBookSocketOpened(object sender, EventArgs e) {
            SocketConnectionInfo info = OrderBookSockets[(WebSocket)sender];
            info.State = SocketConnectionState.Connected;
            info.Ticker.UpdateOrderBook();
        }

        protected override void OnOrderBookSocketError(object sender, ErrorEventArgs e) {
            SocketConnectionInfo info = OrderBookSockets[(WebSocket)sender];
            info.State = SocketConnectionState.Error;
            info.LastError = e.Exception.ToString();
        }
        
        public override void StopListenTickerStream(Ticker ticker) {
            base.StopListenTickerStream(ticker);
        }

        string[] tradeItems;
        protected string[] TradeItems {
            get {
                if(tradeItems == null)
                    tradeItems = new string[] { "e", "E", "s", "t", "p", "q", "b", "a", "T", "m", "M" };
                return tradeItems;
            }
        }

        protected override void OnTradeHistorySocketMessageReceived(object sender, MessageReceivedEventArgs e) {
            byte[] bytes = Encoding.Default.GetBytes(e.Message);
            int startIndex = 0;
            string[] trades = JSonHelper.Default.DeserializeObject(bytes, ref startIndex, TradeItems);
            SocketConnectionInfo info = TradeHistorySockets[(WebSocket)sender];
            OnTradeHistoryItemRecv(info.Ticker, trades);
        }
        
        void OnTradeHistoryItemRecv(Ticker ticker, string[] str) {
            TradeInfoItem item = new TradeInfoItem(null, ticker);
            item.Id = FastValueConverter.ConvertPositiveInteger(str[3]);
            item.RateString = str[4];
            item.AmountString = str[5];
            item.Time = FromUnixTime(FastValueConverter.ConvertPositiveLong(str[8]));
            item.Type = str[9][0] == 't' ? TradeType.Sell : TradeType.Buy;

            ticker.TradeHistory.Insert(0, item);
            ticker.RaiseTradeHistoryAdd();
        }

        protected override void OnTradeHistorySocketOpened(object sender, EventArgs e) {
            SocketConnectionInfo info = TradeHistorySockets[(WebSocket)sender];
            info.State = SocketConnectionState.Connected;
            info.Ticker.TradeHistory.Clear();
        }

        protected override void OnTradeHistorySocketClosed(object sender, EventArgs e) {
            SocketConnectionInfo info = TradeHistorySockets[(WebSocket)sender];
            info.State = SocketConnectionState.Disconnected;
        }

        protected override void OnTradeHistorySocketError(object sender, ErrorEventArgs e) {
            SocketConnectionInfo info = TradeHistorySockets[(WebSocket)sender];
            info.State = SocketConnectionState.Error;
            info.LastError = e.Exception.ToString();
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

        public override bool GetDeposites(AccountInfo account) {
            throw new NotImplementedException();
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

        public override bool AllowCandleStickIncrementalUpdate => true;

        public override bool Cancel(AccountInfo account, string orderId) {
            throw new NotImplementedException();
        }

        public override List<CandleStickIntervalInfo> GetAllowedCandleStickIntervals() {
            List<CandleStickIntervalInfo> list = new List<CandleStickIntervalInfo>();

            list.Add(new CandleStickIntervalInfo() { Text = "1 Minute", Command="1m", Interval = TimeSpan.FromSeconds(60) });
            list.Add(new CandleStickIntervalInfo() { Text = "3 Minutes", Command="3m", Interval = TimeSpan.FromSeconds(180) });
            list.Add(new CandleStickIntervalInfo() { Text = "5 Minutes", Command="5m", Interval = TimeSpan.FromSeconds(300) });
            list.Add(new CandleStickIntervalInfo() { Text = "15 Minutes", Command="15m", Interval = TimeSpan.FromSeconds(900) });
            list.Add(new CandleStickIntervalInfo() { Text = "30 Minutes", Command="30m", Interval = TimeSpan.FromSeconds(1800) });
            list.Add(new CandleStickIntervalInfo() { Text = "1 Hour", Command="1h", Interval = TimeSpan.FromSeconds(3600) });
            list.Add(new CandleStickIntervalInfo() { Text = "2 Hours", Command = "2h", Interval = TimeSpan.FromSeconds(7200) });
            list.Add(new CandleStickIntervalInfo() { Text = "4 Hours", Command = "4h", Interval = TimeSpan.FromSeconds(14400) });
            list.Add(new CandleStickIntervalInfo() { Text = "6 Hour", Command = "6h", Interval = TimeSpan.FromSeconds(21600) });
            list.Add(new CandleStickIntervalInfo() { Text = "8 Hours", Command = "8h", Interval = TimeSpan.FromSeconds(28800) });
            list.Add(new CandleStickIntervalInfo() { Text = "12 Hours", Command = "12h", Interval = TimeSpan.FromSeconds(43200) });
            list.Add(new CandleStickIntervalInfo() { Text = "1 Day", Command = "1d", Interval = TimeSpan.FromSeconds(86400) });
            list.Add(new CandleStickIntervalInfo() { Text = "3 Days", Command = "3d", Interval = TimeSpan.FromSeconds(259200) });
            list.Add(new CandleStickIntervalInfo() { Text = "1 Week", Command = "1w", Interval = TimeSpan.FromSeconds(604800) });

            return list;
        }

        public override BindingList<CandleStickData> GetCandleStickData(Ticker ticker, int candleStickPeriodMin, DateTime startUtc, long periodInSeconds) {
            long startSec = (long)(startUtc.Subtract(epoch)).TotalSeconds;
            long end = startSec + periodInSeconds;
            CandleStickIntervalInfo info = AllowedCandleStickIntervals.FirstOrDefault(i => i.Interval.TotalMinutes == candleStickPeriodMin);

            string address = string.Format("https://api.binance.com/api/v1/klines?symbol={0}&interval={1}&startTime={2}&endTime={3}&limit=10000",
                Uri.EscapeDataString(ticker.CurrencyPair), info.Command, startSec * 1000, end * 1000);
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(address);
            }
            catch(Exception) {
                return null;
            }
            if(bytes == null || bytes.Length == 0)
                return null;

            DateTime startTime = epoch;

            BindingList<CandleStickData> list = new BindingList<CandleStickData>();
            int startIndex = 0;
            List<string[]> res = JSonHelper.Default.DeserializeArrayOfArrays(bytes, ref startIndex, 12);
            if(res == null) return list;
            foreach(string[] item in res) {
                CandleStickData data = new CandleStickData();
                data.Time = startTime.AddMilliseconds(FastValueConverter.ConvertPositiveLong(item[0])).ToLocalTime();
                data.Open = FastValueConverter.Convert(item[1]);
                data.High = FastValueConverter.Convert(item[2]);
                data.Low = FastValueConverter.Convert(item[3]);
                data.Close = FastValueConverter.Convert(item[4]);
                data.Volume = FastValueConverter.Convert(item[5]);
                data.QuoteVolume = FastValueConverter.Convert(item[7]);
                data.BuyVolume = FastValueConverter.Convert(item[9]);
                data.SellVolume = data.Volume - data.BuyVolume;
                data.BuySellVolume = data.BuyVolume - data.SellVolume;
                list.Add(data);
            }

            //List<TradeInfoItem> trades = GetTradeVolumesForCandleStick(ticker, startSec * 1000, end * 1000);
            //CandleStickChartHelper.InitializeVolumes(list, trades, ticker.CandleStickPeriodMin);
            return list;
        }

        protected string[] AggTradesItem { get; } = new string[] {
            "a",         // Aggregate tradeId
            "p",        // Price
            "q",        // Quantity
            "f",         // First tradeId
            "l",         // Last tradeId
            "T", // Timestamp
            "m",          // Was the buyer the maker?
            "M"           // Was the trade the best price match?
        };

        public List<TradeInfoItem> GetTradeVolumesForCandleStick(Ticker ticker, long start, long end) {
            List<TradeInfoItem> trades = new List<TradeInfoItem>();
            string address = string.Format("https://api.binance.com/api/v1/aggTrades?symbol={0}&limit={1}&startTime={2}&endTime={3}", Uri.EscapeDataString(ticker.CurrencyPair), 1000, start, end);
            byte[] data = ((Ticker)ticker).DownloadBytes(address);
            if(data == null || data.Length == 0)
                return trades;

            int parseIndex = 0;
            List<string[]> items = JSonHelper.Default.DeserializeArrayOfObjects(data, ref parseIndex, AggTradesItem);

            for(int i = items.Count - 1; i >= 0; i--) {
                string[] item = items[i];
                DateTime time = FromUnixTime(FastValueConverter.ConvertPositiveLong(item[5]));

                TradeInfoItem t = new TradeInfoItem(null, ticker);
                bool isBuy = item[6][0] != 't';
                t.AmountString = item[2];
                t.Time = time;
                t.Type = isBuy ? TradeType.Buy : TradeType.Sell;
                trades.Add(t);
            }
            return trades;
        }

        public override bool GetTickersInfo() {
            bool result = UpdateTickersInfo();
            IsInitialized = true;
            return result;
        }

        public override List<TradeInfoItem> GetTrades(Ticker ticker, DateTime starTime) {
            string address = string.Format("https://api.binance.com/api/v1/depth?symbol={0}&limit={1}",
                Uri.EscapeDataString(ticker.CurrencyPair), 1000);
            string text = ((Ticker)ticker).DownloadString(address);
            if(string.IsNullOrEmpty(text))
                return null;

            JArray trades = JsonConvert.DeserializeObject<JArray>(text);
            if(trades.Count == 0)
                return null;

            List<TradeInfoItem> list = new List<TradeInfoItem>();
            int index = 0;
            foreach(JObject obj in trades) {
                DateTime time = new DateTime(obj.Value<Int64>("time"));
                int tradeId = obj.Value<int>("id");
                if(time < starTime)
                    break;

                TradeInfoItem item = new TradeInfoItem(null, ticker);
                bool isBuy = obj.Value<string>("type").Length == 3;
                item.AmountString = obj.Value<string>("qty");
                item.Time = time;
                item.Type = isBuy ? TradeType.Buy : TradeType.Sell;
                item.RateString = obj.Value<string>("price");
                item.Id = tradeId;
                double price = item.Rate;
                double amount = item.Amount;
                item.Total = price * amount;
                list.Add(item);
                index++;
            }
            ticker.RaiseTradeHistoryAdd();
            return list;
        }

        public override bool ProcessOrderBook(Ticker tickerBase, string text) {
            return true;
            //throw new NotImplementedException();
        }

        public override bool UpdateBalances(AccountInfo info) {
            return true;
            //throw new NotImplementedException();
        }

        public override bool UpdateCurrencies() {
            return true;
            //throw new NotImplementedException();
        }

        public override bool UpdateAccountTrades(AccountInfo account, Ticker ticker) {
            return true;
            //throw new NotImplementedException();
        }

        public override bool UpdateOpenedOrders(AccountInfo account, Ticker ticker) {
            return true;
            //throw new NotImplementedException();
        }

        public override bool UpdateArbitrageOrderBook(Ticker ticker, int depth) {
            string address = string.Format("https://api.binance.com/api/v1/depth?symbol={0}&limit={1}",
                Uri.EscapeDataString(ticker.CurrencyPair), depth);
            byte[] bytes = ((Ticker)ticker).DownloadBytes(address);
            if(bytes == null || bytes.Length == 0)
                return false;
            OnUpdateOrderBook(ticker, bytes);
            return true;
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

        string[] tradeItemString;
        protected string[] TradeItemString {
            get {
                if(tradeItemString == null)
                    tradeItemString = new string[] { "id", "price", "qty", "time", "isBuyerMaker", "isBestMatch" };
                return tradeItemString;
            }
        }

        public override bool UpdateTrades(Ticker ticker) {
            string address = string.Format("https://api.binance.com/api/v1/trades?symbol={0}&limit={1}",
                Uri.EscapeDataString(ticker.CurrencyPair), 1000);
            byte[] data = ((Ticker)ticker).DownloadBytes(address);
            if(data == null || data.Length == 0)
                return false;

            ticker.TradeHistory.Clear();
            ticker.TradeStatistic.Clear();
            
            int index = 0, parseIndex = 0;
            List<string[]> items = JSonHelper.Default.DeserializeArrayOfObjects(data, ref parseIndex, TradeItemString);

            for(int i = items.Count - 1; i >= 0; i--) {
                string[] item = items[i];
                DateTime time = FromUnixTime(FastValueConverter.ConvertPositiveLong(item[3]));
                int tradeId = FastValueConverter.ConvertPositiveInteger(item[0]);

                TradeInfoItem t = new TradeInfoItem(null, ticker);
                bool isBuy = item[4][0] != 't';
                t.AmountString = item[2];
                t.Time = time;
                t.Type = isBuy ? TradeType.Buy : TradeType.Sell;
                t.RateString = item[1];
                t.Id = tradeId;
                double price = t.Rate;
                double amount = t.Amount;
                t.Total = price * amount;
                ticker.TradeHistory.Add(t);
                index++;
            }
            ticker.RaiseTradeHistoryAdd();
            return true;
        }
    }
}
