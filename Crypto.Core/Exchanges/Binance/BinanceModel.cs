using Crypto.Core.Common;
using Crypto.Core.Exchanges.Base;
using Crypto.Core.Exchanges.Binance;
using Crypto.Core.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WebSocket4Net;

namespace Crypto.Core.Binance {
    public class BinanceExchange : Exchange {
        static BinanceExchange defaultExchange;
        public static BinanceExchange Default {
            get {
                if(defaultExchange == null) {
                    defaultExchange = (BinanceExchange)Exchange.FromFile(ExchangeType.Binance, typeof(BinanceExchange));
                }
                return defaultExchange;
            }
        }

        protected override DateTime GetTradesRangeEndTime(DateTime start, DateTime end) {
            return start.AddMinutes(59);
        }

        protected virtual string AggTradesApiString => "https://api.binance.com/api/v1/aggTrades";

        protected override bool GetTradesCore(ResizeableArray<TradeInfoItem> list, Ticker ticker, DateTime start, DateTime end) {
            string address = string.Format("{0}?symbol={1}&limit={2}&startTime={3}&endTime={4}", AggTradesApiString,
                Uri.EscapeDataString(ticker.CurrencyPair), 1000, ToUnixTimestampMs(start), ToUnixTimestampMs(end));
            byte[] data = ticker.DownloadBytes(address);
            if(data == null || data.Length == 0)
                return false;

            int parseIndex = 0;
            List<string[]> items = JSonHelper.Default.DeserializeArrayOfObjects(data, ref parseIndex, AggTradeItemString);
            for(int i = 0; i < items.Count; i++) {
                string[] item = items[i];
                DateTime time = FromUnixTimestampMs(FastValueConverter.ConvertPositiveLong(item[5]));
                TradeInfoItem t = new TradeInfoItem(null, ticker);
                bool isBuy = item[6][0] != 't';
                t.AmountString = item[2];
                t.Time = time;
                t.Type = isBuy ? TradeType.Buy : TradeType.Sell;
                t.RateString = item[1];
                t.IdString = item[0];
                if(list.Last() == null || list.Last().Time < time)
                    list.Add(t);
            }
            return true;
        }

        protected override bool ShouldAddKlineListener => false;

        protected internal override IIncrementalUpdateDataProvider CreateIncrementalUpdateDataProvider() {
            return new BinanceIncrementalUpdateDataProvider();
        }

        public override void OnAccountRemoved(AccountInfo info) {

        }

        public override string CreateDeposit(AccountInfo account, string currency) {
            throw new NotImplementedException();
        }

        protected virtual string BalanceApiString { get { return "https://binance.com/api/v3/account"; } }
        public override bool GetBalance(AccountInfo account, string currency) {
            string queryString = string.Format("timestamp={0}", GetNonce());
            string signature = account.GetSign(queryString);

            string address = string.Format("{0}?{1}&signature={2}", BalanceApiString, queryString, signature);
            MyWebClient client = GetWebClient();

            client.Headers.Clear();
            client.Headers.Add("X-MBX-APIKEY", account.ApiKey);

            try {
                return OnGetBalances(account, client.UploadValues(address, new HttpRequestParamsCollection()));
            }
            catch(Exception e) {
                LogManager.Default.Log(e.ToString());
                return false;
            }
        }

        public override bool Withdraw(AccountInfo account, string currency, string adress, string paymentId, double amount) {
            throw new NotImplementedException();
        }

        //public override TradingResult GetOrderStatus(AccountInfo account, TradingResult order) {
        //    string queryString = string.Format("symbol={0}&orderId={1}&timestamp={2}&recvWindow=5000", 
        //        order.Ticker.Name, order.OrderId, GetNonce());
        //    string signature = account.GetSign(queryString);

        //    string address = string.Format("{0}?{1}&signature={2}",
        //        TradeApiString, queryString, signature);
        //    MyWebClient client = GetWebClient();

        //    client.Headers.Clear();
        //    client.Headers.Add("X-MBX-APIKEY", account.ApiKey);

        //    try {
        //        return OnTradeResult(account, ticker, client.DownloadData(address, queryString));
        //    }
        //    catch(Exception e) {
        //        LogManager.Default.Log(e.ToString());
        //        return null;
        //    }
        //}

        protected virtual TradingResult MakeTrade(AccountInfo account, Ticker ticker, double rate, double amount, string side, string positionSide) {
            string queryString = string.Format("symbol={0}&side={1}&quantity={2:0.########}&price={3:0.########}&timestamp={4}&type=LIMIT&timeInForce=GTC&recvWindow=5000",
                ticker.Name, side, amount, rate, GetNonce());
            string signature = account.GetSign(queryString);

            string address = string.Format("{0}?{1}&signature={2}",
                TradeApiString, queryString, signature);
            MyWebClient client = GetWebClient();

            client.Headers.Clear();
            client.Headers.Add("X-MBX-APIKEY", account.ApiKey);

            try {
                return OnTradeResult(account, ticker, client.UploadValues(address, new HttpRequestParamsCollection()));
            }
            catch(Exception e) {
                LogManager.Default.Log(e.ToString());
                return null;
            }
        }

        protected virtual string TradeApiString => "https://api.binance.com/api/v3/order";
        protected virtual string OrderStatusApiString => "https://api.binance.com/api/v3/order";

        public override TradingResult BuyLong(AccountInfo account, Ticker ticker, double rate, double amount) {
            return MakeTrade(account, ticker, rate, amount, "BUY", "LONG");
        }

        public override TradingResult BuyShort(AccountInfo account, Ticker ticker, double rate, double amount) {
            return MakeTrade(account, ticker, rate, amount, "BUY", "SHORT");
        }

        public override TradingResult SellShort(AccountInfo account, Ticker ticker, double rate, double amount) {
            return MakeTrade(account, ticker, rate, amount, "SELL", "SHORT");
        }

        protected TradingResult OnTradeResult(AccountInfo account, Ticker ticker, byte[] data) {
            if(data == null)
                return null;
            string text = Encoding.UTF8.GetString(data);
            JObject res = JsonConvert.DeserializeObject<JObject>(text);
            if(res.Value<string>("code") != null) {
                LogManager.Default.Error(this, "trade", text);
                return null;
            }
            return OnTradeResultCore(account, ticker, res);

        }

        protected virtual TradingResult OnTradeResultCore(AccountInfo account, Ticker ticker, JObject res) {
            TradingResult tr = new TradingResult();
            tr.Ticker = ticker;
            tr.OrderId = res.Value<string>("orderId");
            tr.Amount = Convert.ToDouble(res.Value<string>("executedQty"));
            tr.Value = Convert.ToDouble(res.Value<string>("price"));
            tr.Total = tr.Amount * tr.Value;
            tr.OrderStatus = res.Value<string>("status");
            tr.Filled = tr.OrderStatus == "FILLED";
            JArray fills = res.Value<JArray>("fills");
            if(fills == null)
                return tr;
            foreach(JObject item in fills) {
                TradeEntry e = new TradeEntry();
                e.Amount = Convert.ToDouble(item.Value<string>("qty"));
                e.Rate = Convert.ToDouble(item.Value<string>("price"));
                e.Fee = Convert.ToDouble(item.Value<string>("comission"));
                e.FeeAsset = item.Value<string>("commissionAsset");
                tr.Trades.Add(e);
            }
            return tr;
        }

        public override TradingResult SellLong(AccountInfo account, Ticker ticker, double rate, double amount) {
            return MakeTrade(account, ticker, rate, amount, "SELL", "LONG");
        }

        public override string BaseWebSocketAdress => "wss://stream.binance.com:9443/ws/!ticker@arr";

        public override ExchangeType Type => ExchangeType.Binance;

        public override bool SupportWebSocket(WebSocketType type) {
            if(type == WebSocketType.Tickers)
                return true;
            if(type == WebSocketType.Ticker || type == WebSocketType.Trades || type == WebSocketType.OrderBook)
                return true;
            return false;
        }

        protected override string GetKlineSocketAddress(Ticker ticker) {
            CandleStickIntervalInfo klineInfo = ticker.GetCandleStickIntervalInfo();
            string adress = "wss://stream.binance.com:9443/ws/" + ticker.Name.ToLower() + "@kline_" + klineInfo.Command;
            return adress;
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

        protected internal override void OnKlineSocketMessageReceived(object sender, MessageReceivedEventArgs e) {
            //Debug.WriteLine(e.Message);
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
            SocketConnectionInfo info = null;
            for(int i = 0; i < KlineSockets.Count; i++) {
                SocketConnectionInfo c = KlineSockets[i];
                if(c.Key == sender) {
                    info = c;
                    break;
                }
            }
            if(info == null)
                return;
            OnKlineItemRecv(info.Ticker, kline);
        }
        protected virtual void OnKlineItemRecv(Ticker ticker, string[] item) {
            long dt = FastValueConverter.ConvertPositiveLong(item[0]);
            DateTime time = FromUnixTime(dt);
            CandleStickIntervalInfo info = null;
            for(int ci = 0; ci < AllowedCandleStickIntervals.Count; ci++) {
                CandleStickIntervalInfo i = AllowedCandleStickIntervals[ci];
                if(i.Command == item[3]) {
                    info = i;
                    break;
                }
            }
            if(ticker.CandleStickPeriodMin != info.Interval.TotalMinutes)
                return;
            //Debug.WriteLine(item[6] + " " + item[7] + " " + item[8] + " " + item[9]);
            lock(ticker.CandleStickData) {
                CandleStickData data = new CandleStickData();
                data.Time = time;
                data.Open = FastValueConverter.Convert(item[6]);
                data.Close = FastValueConverter.Convert(item[7]);
                data.High = FastValueConverter.Convert(item[8]);
                data.Low = FastValueConverter.Convert(item[9]);
                data.Volume = FastValueConverter.Convert(item[10]);
                data.QuoteVolume = FastValueConverter.Convert(item[13]);
                ticker.UpdateCandleStickData(data);
            }
        }

        public override void StartListenTickerStream(Ticker ticker) {
            base.StartListenTickerStream(ticker);

        }

        //protected override void StartListenTradeHistoryCore(Ticker ticker) {

        //}

        protected override string GetOrderBookSocketAddress(Ticker ticker) {
            return "wss://stream.binance.com:9443/ws/" + ticker.Name.ToLower() + "@depth";
        }

        protected override string GetTradeSocketAddress(Ticker ticker) {
            return "wss://stream.binance.com:9443/ws/" + ticker.Name.ToLower() + "@trade";
        }



        protected string[] OrderBookStartItems { get; } = new string[] { "lastUpdateId" };

        void OnIncrementalOrderBookUpdateRecv(Ticker ticker, byte[] bytes) {
            int startIndex = 0;

            JSonHelper.Default.SkipSymbol(bytes, ':', 4, ref startIndex);
            long startId = JSonHelper.Default.ReadPositiveInteger(bytes, ref startIndex);
            JSonHelper.Default.SkipSymbol(bytes, ':', 1, ref startIndex);
            long endId = JSonHelper.Default.ReadPositiveInteger(bytes, ref startIndex);
            startIndex++;

            if(endId < ticker.OrderBook.Updates.SeqNumber)
                return;
            if(startId > ticker.OrderBook.Updates.SeqNumber) {
                ticker.UpdateOrderBook();
                return;
            }

            // https://github.com/binance-exchange/binance-official-api-docs/blob/master/web-socket-streams.md
            // Receiving an event that removes a price level that is not in your local order book can happen and is normal.
            ticker.OrderBook.EnableValidationOnRemove = false;

            JSonHelper.Default.SkipSymbol(bytes, ':', 1, ref startIndex);
            List<string[]> jbids = JSonHelper.Default.DeserializeArrayOfArrays(bytes, ref startIndex, 2);
            JSonHelper.Default.SkipSymbol(bytes, ':', 1, ref startIndex);
            List<string[]> jasks = JSonHelper.Default.DeserializeArrayOfArrays(bytes, ref startIndex, 2);

            long hackedNextSeqNumber = ticker.OrderBook.Updates.SeqNumber;
            ticker.OrderBook.Updates.Push(hackedNextSeqNumber, ticker, jbids, jasks, null);
            OnIncrementalUpdateRecv(ticker.OrderBook.Updates);
            if(ticker.OrderBook.Updates.Count == 0)
                ticker.OrderBook.Updates.Clear(endId + 1);
        }

        protected internal override void OnOrderBookSocketMessageReceived(object sender, MessageReceivedEventArgs e) {
            LastWebSocketRecvTime = DateTime.Now;
            SocketConnectionInfo info = null;
            for(int oi = 0; oi < OrderBookSockets.Count; oi++) {
                SocketConnectionInfo c = OrderBookSockets[oi];
                if(c.Key == sender) {
                    info = c;
                    break;
                }
            }
            if(info == null)
                return;
            const string hour24TickerStart = "[{\"e\"";
            const string orderBookStart = "{\"e\":\"depthUpdate\"";
            if(e.Message.StartsWith(hour24TickerStart)) {
                OnTickersSocketMessageReceived(sender, e);
                return;
            }

            else if(e.Message.StartsWith(orderBookStart)) {
                if(info.Ticker.CaptureData)
                    info.Ticker.CaptureDataCore(CaptureStreamType.OrderBook, CaptureMessageType.Incremental, e.Message);
                OnIncrementalOrderBookUpdateRecv(info.Ticker, Encoding.Default.GetBytes(e.Message));
                return;
            }
        }

        string[] tradeItems;
        protected string[] TradeItems {
            get {
                if(tradeItems == null)
                    tradeItems = new string[] { "e", "E", "s", "t", "p", "q", "b", "a", "T", "m", "M" };
                return tradeItems;
            }
        }

        protected internal override void OnTradeHistorySocketMessageReceived(object sender, MessageReceivedEventArgs e) {
            SocketConnectionInfo info = TradeHistorySockets.FirstOrDefault(c => c.Key == sender);
            if(info == null)
                return;
            if(info.Ticker.IsUpdatingTrades)
                return;
            byte[] bytes = Encoding.Default.GetBytes(e.Message);
            int startIndex = 0;
            string[] trades = JSonHelper.Default.DeserializeObject(bytes, ref startIndex, TradeItems);
            if(info.Ticker.CaptureData)
                info.Ticker.CaptureDataCore(CaptureStreamType.TradeHistory, CaptureMessageType.Incremental, e.Message);
            OnTradeHistoryItemRecv(info.Ticker, trades);
        }

        protected virtual void OnTradeHistoryItemRecv(Ticker ticker, string[] str) {
            TradeInfoItem item = new TradeInfoItem(null, ticker);
            item.IdString = str[3];
            item.RateString = str[4];
            item.AmountString = str[5];
            item.Time = FromUnixTime(FastValueConverter.ConvertPositiveLong(str[8]));
            item.Type = str[9][0] == 't' ? TradeType.Sell : TradeType.Buy;

            ticker.AddTradeHistoryItem(item);
            if(ticker.HasTradeHistorySubscribers) {
                TradeHistoryChangedEventArgs e = new TradeHistoryChangedEventArgs() { NewItem = item };
                ticker.RaiseTradeHistoryChanged(e);
            }
        }

        string[] webSocketTickersInfo;
        protected string[] WebSocketTickersInfo {
            get {
                if(webSocketTickersInfo == null)
                    webSocketTickersInfo = CreateWebSocketTickersInfoStrings();
                return webSocketTickersInfo;
            }
        }

        protected virtual string[] CreateWebSocketTickersInfoStrings() {
            return new string[] {
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

        protected internal override void OnTickersSocketMessageReceived(object sender, MessageReceivedEventArgs e) {
            base.OnTickersSocketMessageReceived(sender, e);
            byte[] data = Encoding.Default.GetBytes(e.Message);
            int startIndex = 0;
            List<string[]> items = JSonHelper.Default.DeserializeArrayOfObjects(data, ref startIndex, WebSocketTickersInfo);
            for(int i = 0; i < items.Count; i++) {
                string[] item = items[i];
                string eventType = item[0];
                if(eventType == "24hrTicker")
                    On24HourTickerRecv(item);
            }
        }

        protected virtual void On24HourTickerRecv(string[] item) {
            string symbolName = item[2];
            BinanceTicker ticker = (BinanceTicker)GetTicker(symbolName);
            if(ticker == null)
                return;
            On24HourTickerRecvCore(ticker, item);
            ticker.UpdateTrailings();

            lock(ticker) {
                RaiseTickerChanged(ticker);
            }
        }

        protected virtual void On24HourTickerRecvCore(BinanceTicker t, string[] item) {
            if(t == null)
                return;
            
            t.Change = FastValueConverter.Convert(item[4]);
            t.HighestBid = FastValueConverter.Convert(item[9]);
            t.LowestAsk = FastValueConverter.Convert(item[11]);
            t.Hr24High = FastValueConverter.Convert(item[14]);
            t.Hr24Low = FastValueConverter.Convert(item[15]);
            t.BaseVolume = FastValueConverter.Convert(item[16]);
            t.Volume = FastValueConverter.Convert(item[17]);
        }

        public override bool GetDeposites(AccountInfo account) {
            return true;
        }

        protected internal override HMAC CreateHmac(string secret) {
            return new HMACSHA256(System.Text.ASCIIEncoding.Default.GetBytes(secret));
        }

        protected virtual string ExchangeSettingsApi { get { return "https://api.binance.com/api/v1/exchangeInfo"; } }

        public override bool ObtainExchangeSettings() {
            string address = ExchangeSettingsApi;
            string text = string.Empty;
            try {
                SuppressCheckRequestLimits = true;
                text = GetDownloadString(address);
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
            finally {
                SuppressCheckRequestLimits = false;
            }
            if(string.IsNullOrEmpty(text))
                return false;

            JObject settings = JsonConvert.DeserializeObject<JObject>(text);
            JArray rateLimits = settings.Value<JArray>("rateLimits");
            RequestRate = new List<RateLimit>();
            OrderRate = new List<RateLimit>();
            string timezone = settings.Value<string>("timezone");
            long ms = settings.Value<long>("serverTime");
            DateTime serverTime = FromUnixTimestampMs(ms);

            for(int i = 0; i < rateLimits.Count; i++) {
                JObject rateLimit = (JObject)rateLimits[i];
                string rateType = rateLimit.Value<string>("rateLimitType");
                if(rateType == "REQUESTS" || rateType == "REQUEST_WEIGHT")
                    RequestRate.Add(GetRateLimit(rateLimit));
                if(rateType == "ORDERS")
                    OrderRate.Add(GetRateLimit(rateLimit));
            }
            JArray symbols = settings.Value<JArray>("symbols");
            for(int i = 0; i < symbols.Count; i++) {
                JObject s = (JObject)symbols[i];
                BinanceTicker t = new BinanceTicker(this);
                t.CurrencyPair = s.Value<string>("symbol");
                t.MarketCurrency = s.Value<string>("baseAsset");
                t.BaseCurrency = s.Value<string>("quoteAsset");
                if(Tickers.FirstOrDefault(tt => tt.CurrencyPair == t.CurrencyPair) != null)
                    continue;
                AddTicker(t);
                JArray filters = s.Value<JArray>("filters");
                for(int fi = 0; fi < filters.Count; fi++) {
                    JObject filter = (JObject)filters[fi];
                    string filterType = filter.Value<string>("filterType");
                    if(filterType == "PRICE_FILTER")
                        t.PriceFilter = new TickerFilter() { MinValue = filter.Value<double>("minPrice"), MaxValue = filter.Value<double>("maxPrice"), TickSize = filter.Value<double>("tickSize") };
                    else if(filterType == "LOT_SIZE")
                        t.QuantityFilter = new TickerFilter() { MinValue = filter.Value<double>("minQty"), MaxValue = filter.Value<double>("maxQty"), TickSize = filter.Value<double>("stepSize") };
                    else if(filterType == "MIN_NOTIONAL")
                        t.NotionalFilter = new TickerFilter() { MinValue = filter.Value<double>("minNotional"), MaxValue = double.MaxValue };
                }
            }
            return true;
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

        protected virtual string CancelOrderApiString => "https://api.binance.com/api/v3/order";
        public override bool Cancel(AccountInfo account, Ticker ticker, string orderId) {
            string queryString = string.Format("symbol={0}&orderId={1}&timestamp={2}&recvWindow=5000",
                ticker.Name, orderId, GetNonce());
            string signature = account.GetSign(queryString);

            string address = string.Format("{0}?{1}&signature={2}",
                CancelOrderApiString, queryString, signature);
            MyWebClient client = GetWebClient();

            client.Headers.Clear();
            client.Headers.Add("X-MBX-APIKEY", account.ApiKey);

            try {
                return OnCancel(account, ticker, orderId, client.Delete(address));
            }
            catch(Exception e) {
                LogManager.Default.Log(e.ToString());
                return false;
            }
        }

        protected virtual bool OnCancel(AccountInfo info, Ticker ticker, string orderId, byte[] data) {
            var res = JSonHelper.Default.Deserialize(Type + "/cancelOrder", data);
            if(res == null || res.Type == JsonObjectType.None)
                return false;
            if(res.Scheme.Names[0] == "code") {
                LogManager.Default.Log("Cancel", res.GetValue("msg"));
                return false;
            }

            string oid = res.GetValue("orderId");
            string status = res.GetValue("status");
            OpenedOrderInfo oi = ticker.OpenedOrders.FirstOrDefault(o => o.OrderId == oid);
            bool canceled = status == "CANCELED";
            ticker.RaiseOrderCanceled(new CancelOrderEventArgs() { Order = oi, Ticker = ticker, Status = status, Canceled = canceled });

            return true;
        }

        public override List<CandleStickIntervalInfo> GetAllowedCandleStickIntervals() {
            List<CandleStickIntervalInfo> list = new List<CandleStickIntervalInfo>();

            list.Add(new CandleStickIntervalInfo() { Text = "1 Minute", Command = "1m", Interval = TimeSpan.FromSeconds(60) });
            list.Add(new CandleStickIntervalInfo() { Text = "3 Minutes", Command = "3m", Interval = TimeSpan.FromSeconds(180) });
            list.Add(new CandleStickIntervalInfo() { Text = "5 Minutes", Command = "5m", Interval = TimeSpan.FromSeconds(300) });
            list.Add(new CandleStickIntervalInfo() { Text = "15 Minutes", Command = "15m", Interval = TimeSpan.FromSeconds(900) });
            list.Add(new CandleStickIntervalInfo() { Text = "30 Minutes", Command = "30m", Interval = TimeSpan.FromSeconds(1800) });
            list.Add(new CandleStickIntervalInfo() { Text = "1 Hour", Command = "1h", Interval = TimeSpan.FromSeconds(3600) });
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

        public override ResizeableArray<CandleStickData> GetCandleStickData(Ticker ticker, int candleStickPeriodMin, DateTime startUtc, long periodInSeconds) {
            //startUtc = startUtc.ToUniversalTime();
            long startSec = (long)(startUtc.Subtract(epoch)).TotalSeconds;
            long end = startSec + periodInSeconds;
            CandleStickIntervalInfo info = null;
            for(int index = 0; index < AllowedCandleStickIntervals.Count; index++) {
                CandleStickIntervalInfo i = AllowedCandleStickIntervals[index];
                if(i.Interval.TotalMinutes == candleStickPeriodMin) {
                    info = i;
                    break;
                }
            }
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

            ResizeableArray<CandleStickData> list = new ResizeableArray<CandleStickData>();
            int startIndex = 0;
            List<string[]> res = JSonHelper.Default.DeserializeArrayOfArrays(bytes, ref startIndex, 12);
            if(res == null) return list;
            for(int i = 0; i < res.Count; i++) {
                string[] item = res[i];
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
            byte[] data = ticker.DownloadBytes(address);
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

        public override bool ProcessOrderBook(Ticker tickerBase, string text) {
            return true;
        }

        protected string GetNonce() {
            long timestamp = (long)(DateTime.UtcNow - epoch).TotalMilliseconds;
            return timestamp.ToString();
        }

        protected virtual string ServerTimeApi { get { return "https://binance.com/api/v1/time"; } }
        protected long GetServerTime() {
            string adress = ServerTimeApi;
            MyWebClient client = GetWebClient();
            try {
                return FastValueConverter.ConvertPositiveLong(client.DownloadString(adress));
            }
            catch(Exception e) {
                LogManager.Default.Log(e.ToString());
                return (long)(DateTime.UtcNow - epoch).TotalMilliseconds;
            }
        }

        public override bool UpdateBalances(AccountInfo account) {
            string queryString = string.Format("timestamp={0}&recvWindow=50000", GetNonce());
            string signature = account.GetSign(queryString);

            string address = string.Format("{0}?{1}&signature={2}", BalanceApiString, queryString, signature);
            MyWebClient client = GetWebClient();

            client.Headers.Clear();
            client.Headers.Add("X-MBX-APIKEY", account.ApiKey);

            try {
                return OnGetBalances(account, client.UploadValues(address, new HttpRequestParamsCollection()));
            }
            catch(Exception e) {
                LogManager.Default.Log(e.ToString());
                return false;
            }
        }

        public virtual bool OnGetBalances(AccountInfo account, byte[] data) {
            string text = UTF8Encoding.Default.GetString(data);
            JObject root = (JObject)JsonConvert.DeserializeObject(text);
            if(root.Value<string>("code") != null) {
                LogManager.Default.Error(this, "error on get balance", text);
                return false;
            }
            JArray balances = root.Value<JArray>("balances");
            if(balances == null)
                return false;
            account.Balances.Clear();
            foreach(JObject obj in balances) {
                BinanceAccountBalanceInfo b = new BinanceAccountBalanceInfo(account);
                b.Currency = obj.Value<string>("asset");
                b.Available = obj.Value<double>("free");
                b.OnOrders = obj.Value<double>("locked");
                b.Balance = b.Available + b.OnOrders;
                account.Balances.Add(b);
            }
            return true;
        }

        public override bool UpdateCurrencies() {
            return true;
            //throw new NotImplementedException();
        }

        protected virtual string UpdateAccountTradesApiString => "https://api.binance.com/api/v3/myTrades";

        public override bool UpdateAccountTrades(AccountInfo account, Ticker ticker) {
            string queryString = string.Format("symbol={0}&limit={1}&timestamp={2}",
                Uri.EscapeDataString(ticker.CurrencyPair), 1000, GetNonce());

            string signature = account.GetSign(queryString);

            string address = string.Format("{0}?{1}&signature={2}",
                UpdateAccountTradesApiString, queryString, signature);

            MyWebClient client = GetWebClient();

            client.Headers.Clear();
            client.Headers.Add("X-MBX-APIKEY", account.ApiKey);

            byte[] data = ticker.DownloadBytes(address, client);
            if(data == null || data.Length == 0)
                return false;

            var scheme = JSonHelper.Default.GetObjectScheme(Type + "/accounttrades", data);
            if(scheme == null)
                return false;

            ResizeableArray<TradeInfoItem> newItems = null;
            ticker.LockAccountTrades();
            try {
                ticker.ClearMyTradeHistory();
                int parseIndex = 0;
                List<string[]> items = JSonHelper.Default.DeserializeArrayOfObjects(data, ref parseIndex, scheme.Names);
                newItems = new ResizeableArray<TradeInfoItem>(items.Count);
                for(int i = 0; i < items.Count; i++) {
                    string[] item = items[i];
                    TradeInfoItem t = InitializeAccountTradeInfoItem(item, ticker);
                    ticker.AddAccountTradeHistoryItem(t);
                    newItems.Add(t);
                }
            }
            finally {
                ticker.UnlockAccountTrades();
            }
            if(ticker.HasAccountTradeHistorySubscribers)
                ticker.RaiseAccountTradeHistoryChanged(new TradeHistoryChangedEventArgs() { NewItems = newItems });

            return true;
        }

        protected virtual TradeInfoItem InitializeAccountTradeInfoItem(string[] item, Ticker ticker) {
            DateTime time = FromUnixTime(FastValueConverter.ConvertPositiveLong(item[9]));

            TradeInfoItem t = new TradeInfoItem(null, ticker);
            t.OrderNumber = item[2];
            bool isBuy = item[10][0] != 't';
            t.AmountString = item[5];
            t.Time = time;
            t.Type = isBuy ? TradeType.Buy : TradeType.Sell;
            t.RateString = item[4];
            t.IdString = item[1];

            return t;
        }

        protected virtual string OpenOrdersApiString => "https://api.binance.com/api/v3/openOrders";
        public override bool UpdateOpenedOrders(AccountInfo account, Ticker ticker) {
            string queryString = string.Format("symbol={0}&timestamp={1}&recvWindow=5000",
                ticker.Name, GetNonce());
            string signature = account.GetSign(queryString);

            string address = string.Format("{0}?{1}&signature={2}",
                OpenOrdersApiString, queryString, signature);
            MyWebClient client = GetWebClient();

            client.Headers.Clear();
            client.Headers.Add("X-MBX-APIKEY", account.ApiKey);

            try {
                return OnUpdateOpenedOrders(account, ticker, client.DownloadData(address));
            }
            catch(Exception e) {
                LogManager.Default.Log(e.ToString());
                return false;
            }
        }

        protected virtual OpenedOrderInfo InitializeOpenedOrderItem(string[] item, Ticker ticker) {
            OpenedOrderInfo t = new OpenedOrderInfo(null, ticker);
            t.OrderId = item[1];
            t.Ticker = ticker;
            t.ValueString = item[4];
            t.AmountString = item[5];
            t.TotalString = item[6];
            if(item[11].Length > 0)
                t.Type = item[11][0] == 'B' ? OrderType.Buy : OrderType.Sell;
            t.Date = FromUnixTime(FastValueConverter.ConvertPositiveLong(item[14]));

            return t;
        }

        protected virtual bool OnUpdateOpenedOrders(AccountInfo account, Ticker ticker, byte[] data) {
            var scheme = JSonHelper.Default.GetObjectScheme(Type + "/openorders", data);
            if(scheme == null)
                return false;
            ticker.LockOpenOrders();
            try {
                int parseIndex = 0;
                List<string[]> items = JSonHelper.Default.DeserializeArrayOfObjects(data, ref parseIndex, scheme.Names);
                if(items == null)
                    return false;
                ticker.OpenedOrders.Clear();
                for(int i = 0; i < items.Count; i++) {
                    string[] item = items[i];
                    OpenedOrderInfo t = InitializeOpenedOrderItem(item, ticker);
                    ticker.OpenedOrders.Add(t);
                }
            }
            finally {
                ticker.UnlockOpenOrders();
            }
            ticker.RaiseOpenedOrdersChanged();

            return true;
        }

        protected virtual string UpdateOrderBookApiString => "https://api.binance.com/api/v1/depth";

        public override bool UpdateOrderBook(Ticker ticker, int depth) {
            string address = string.Format("{0}?symbol={1}&limit={2}", UpdateOrderBookApiString,
                Uri.EscapeDataString(ticker.CurrencyPair), depth);
            byte[] bytes = GetDownloadBytes(address);
            if(bytes == null || bytes.Length == 0)
                return false;
            OnUpdateOrderBook(ticker, bytes);
            return true;
        }

        public override void UpdateOrderBookAsync(Ticker ticker, int depth, Action<OperationResultEventArgs> onOrderBookUpdated) {
            string address = string.Format("{0}?symbol={0}&limit={1}", UpdateOrderBookApiString,
                Uri.EscapeDataString(ticker.CurrencyPair), depth);
            GetDownloadBytesAsync(address, t => {
                OnUpdateOrderBook(ticker, t.Result);
                onOrderBookUpdated(new OperationResultEventArgs() { Ticker = ticker, Result = t.Result != null });
            });
        }

        public override bool UpdateOrderBook(Ticker ticker) {
            string address = string.Format("https://api.binance.com/api/v1/depth?symbol={0}&limit={1}",
                Uri.EscapeDataString(ticker.CurrencyPair), 1000);
            byte[] bytes = GetDownloadBytes(address);
            if(bytes == null || bytes.Length == 0)
                return false;
            OnUpdateOrderBook(ticker, bytes);
            return true;
        }

        internal void OnUpdateOrderBook(Ticker ticker, byte[] bytes) {
            int startIndex = 0;
            string[] updateId = JSonHelper.Default.StartDeserializeObject(bytes, ref startIndex, OrderBookStartItems);

            if(ticker.CaptureData)
                ticker.CaptureDataCore(CaptureStreamType.OrderBook, CaptureMessageType.Snapshot, System.Text.ASCIIEncoding.Default.GetString(bytes));

            const string bidString = "\"bids\":";
            const string askString = "\"asks\":";

            startIndex += bidString.Length + 1;
            List<string[]> jbids = JSonHelper.Default.DeserializeArrayOfArrays(bytes, ref startIndex, 2);
            startIndex += askString.Length + 1;
            List<string[]> jasks = JSonHelper.Default.DeserializeArrayOfArrays(bytes, ref startIndex, 2);

            ticker.OrderBook.BeginUpdate();
            try {
                List<OrderBookEntry> bids = ticker.OrderBook.Bids;
                List<OrderBookEntry> asks = ticker.OrderBook.Asks;
                List<OrderBookEntry> iasks = ticker.OrderBook.AsksInverted;

                bids.Clear();
                asks.Clear();
                if(iasks != null)
                    iasks.Clear();
                if(jbids != null) {
                    for(int i = 0; i < jbids.Count; i++) {
                        string[] item = jbids[i];
                        bids.Add(new OrderBookEntry() { ValueString = item[0], AmountString = item[1] });
                    }
                }
                if(jasks != null) {
                    for(int i = 0; i < jasks.Count; i++) {
                        string[] item = jasks[i];
                        OrderBookEntry e = new OrderBookEntry() { ValueString = item[0], AmountString = item[1] };
                        asks.Add(e);
                        if(iasks != null)
                            iasks.Insert(0, e);
                    }
                }
                ticker.OrderBook.Updates.Clear(FastValueConverter.ConvertPositiveLong(updateId[0]) + 1);
            }
            finally {
                ticker.OrderBook.IsDirty = false;
                ticker.OrderBook.EndUpdate();
            }
            ticker.RaiseChanged();
        }

        public override bool UpdateTicker(Ticker tickerBase) {
            return true;
            //throw new NotImplementedException();
        }

        protected virtual string TickerInfoApiString => "https://api.binance.com/api/v1/ticker/24hr";
        public override bool UpdateTickersInfo() {
            string address = TickerInfoApiString;
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
            for(int index = 0; index < res.Count; index++) {
                JObject item = (JObject)res[index];
                string currencyPair = item.Value<string>("symbol");
                Ticker first = null;
                for(int i = 0; i < Tickers.Count; i++) {
                    Ticker tr = Tickers[i];
                    if(tr.CurrencyPair == currencyPair) {
                        first = tr;
                        break;
                    }
                }
                UpdateTickerInfo((BinanceTicker)first, item);
            }
            return true;
        }

        protected virtual void UpdateTickerInfo(BinanceTicker t, JObject item) {
            if(t == null)
                return;
            t.Last = item.Value<double>("lastPrice");
            t.LowestAsk = item.Value<double>("askPrice");
            t.HighestBid = item.Value<double>("bidPrice");
            t.Change = item.Value<double>("priceChangePercent");
            t.BaseVolume = item.Value<double>("volume");
            t.Volume = item.Value<double>("quoteVolume");
            t.Hr24High = item.Value<double>("highPrice");
            t.Hr24Low = item.Value<double>("lowPrice");
        }

        string[] openedOrdersString;
        protected string[] OpenendOrdersString {
            get {
                if(openedOrdersString == null)
                    openedOrdersString = CreateOpenedOrdersString();
                return openedOrdersString;
            }
        }

        protected virtual string[] CreateOpenedOrdersString() {
            return new string[] {
                "symbol",
                "orderId",
                "orderListId", //Unless OCO, the value will always be -1
                "clientOrderId",
                "price",
                "origQty",
                "executedQty",
                "cummulativeQuoteQty",
                "status",
                "timeInForce",
                "type",
                "side",
                "stopPrice",
                "icebergQty",
                "time",
                "updateTime",
                "isWorking",
                "origQuoteOrderQty"
            };
        }

        string[] tradeItemString;
        protected string[] TradeItemString {
            get {
                if(tradeItemString == null)
                    tradeItemString = CreateTradeItemString();
                return tradeItemString;
            }
        }

        protected virtual string[] CreateTradeItemString() {
            return new string[] { "id", "price", "qty", "time", "isBuyerMaker", "isBestMatch" };
        }

        string[] aggTradeItemString;
        protected string[] AggTradeItemString {
            get {
                if(aggTradeItemString == null)
                    aggTradeItemString = new string[] { "a", "p", "q", "f", "l", "T", "m", "M" };
                return aggTradeItemString;
            }
        }

        protected virtual string UpdateTradesApiString => "https://api.binance.com/api/v1/trades";

        public override bool UpdateTrades(Ticker ticker) {
            string address = string.Format("{0}?symbol={1}&limit={2}", UpdateTradesApiString,
                Uri.EscapeDataString(ticker.CurrencyPair), 1000);
            byte[] data = ticker.DownloadBytes(address);
            if(data == null || data.Length == 0)
                return false;

            ResizeableArray<TradeInfoItem> newItems = null;
            ticker.LockTrades();
            try {
                ticker.ClearTradeHistory();
                int parseIndex = 0;
                List<string[]> items = JSonHelper.Default.DeserializeArrayOfObjects(data, ref parseIndex, TradeItemString);
                newItems = new ResizeableArray<TradeInfoItem>(items.Count);
                for(int i = 0; i < items.Count; i++) {
                    string[] item = items[i];
                    TradeInfoItem t = InitializeTradeInfoItem(item, ticker);
                    ticker.AddTradeHistoryItem(t);
                    newItems.Add(t);
                }
            }
            finally {
                ticker.UnlockTrades();
            }
            if(ticker.HasTradeHistorySubscribers)
                ticker.RaiseTradeHistoryChanged(new TradeHistoryChangedEventArgs() { NewItems = newItems });

            return true;
        }

        protected virtual TradeInfoItem InitializeTradeInfoItem(string[] item, Ticker ticker) {
            DateTime time = FromUnixTime(FastValueConverter.ConvertPositiveLong(item[3]));

            TradeInfoItem t = new TradeInfoItem(null, ticker);
            bool isBuy = item[4][0] != 't';
            t.AmountString = item[2];
            t.Time = time;
            t.Type = isBuy ? TradeType.Buy : TradeType.Sell;
            t.RateString = item[1];
            t.IdString = item[0];

            return t;
        }

        protected internal override void ApplyCapturedEvent(Ticker ticker, TickerCaptureDataInfo info) {
            throw new NotImplementedException();
        }
    }
}
