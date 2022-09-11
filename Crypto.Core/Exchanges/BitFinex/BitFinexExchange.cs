using Crypto.Core.Common;
using Crypto.Core.Exchanges.BitFinex;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocket4Net;
using Crypto.Core.Helpers;
using Crypto.Core.Exchanges.Base;
using System.Net.Http;
using System.Security.Cryptography;

namespace Crypto.Core.BitFinex {
    public class BitFinexExchange : Exchange {
        static BitFinexExchange defaultExchange;
        public static BitFinexExchange Default {
            get {
                if(defaultExchange == null)
                    defaultExchange = (BitFinexExchange)Exchange.FromFile(ExchangeType.BitFinex, typeof(BitFinexExchange));
                return defaultExchange;
            }
        }

        public override Ticker CreateTicker(string name) {
            return new BitFinexTicker(this) { CurrencyPair = name };
        }

        public override BalanceBase CreateAccountBalance(AccountInfo info, string currency) {
            return new BitFinexAccountBalanceInfo(info, GetOrCreateCurrency(currency));
        }

        protected override ResizeableArray<TradeInfoItem> GetTradesCore(Ticker ticker, DateTime start, DateTime end) {
            string address = string.Format("https://api-pub.bitfinex.com/v2/trades/{0}/hist", Uri.EscapeDataString(ticker.MarketName));
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(address);
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return null;
            }

            if(bytes == null) {
                LogManager.Default.Error(this, nameof(GetTradesCore), "No data received");
                return null;
            }

            var root = JsonHelper.Default.Deserialize(bytes);
            int itemsCount = root.ItemsCount;
            ResizeableArray<TradeInfoItem> list = new ResizeableArray<TradeInfoItem>(itemsCount);
            for(int i = 0; i < itemsCount; i++) {
                var item = root.Items[i];
                TradeInfoItem ti = new TradeInfoItem(DefaultAccount, ticker);
                ti.IdString = item.Items[0].Value;
                ti.Time = BitFinexTime(item.Items[1]);
                if(item.Items[2].Value[0] == '-') {
                    ti.AmountString = item.Items[2].Value.Substring(1);
                    ti.Type = TradeType.Sell;
                }
                else {
                    ti.AmountString = item.Items[2].Value;
                    ti.Type = TradeType.Buy;
                }
                ti.RateString = item.Items[3].Value;
                ticker.InsertTradeHistoryItem(ti);
                list.Add(ti);
            }
            return list;
        }

        protected override bool ShouldAddKlineListener => true;

        protected internal override IIncrementalUpdateDataProvider CreateIncrementalUpdateDataProvider() {
            return new BitFinexIncrementalUpdateDataProvider();
        }

        public override string BaseWebSocketAdress => "wss://api-pub.bitfinex.com/ws/2";

        public override ExchangeType Type => ExchangeType.BitFinex;

        public override bool SupportWebSocket(WebSocketType type) {
            if(type == WebSocketType.Tickers)
                return false;
            if(type == WebSocketType.Ticker || type == WebSocketType.OrderBook || type == WebSocketType.Trades)
                return true;
            return false;
        }

        public override bool GetDeposites(AccountInfo account) {
            return true;
        }

        public override bool GetDeposit(AccountInfo account, CurrencyInfo currency) {
            return true;
        }

        public override bool ObtainExchangeSettings() { return true; }

        protected internal override void OnTickersSocketOpened(object sender, EventArgs e) {
            base.OnTickersSocketOpened(sender, e);
            //((WebSocket)sender).Send(JsonHelper.Default.Serialize(new string[] { "event", "subscribe", "channel", "ticker" }));
        }

        public override bool AllowCandleStickIncrementalUpdate => false;

        public override List<CandleStickIntervalInfo> GetAllowedCandleStickIntervals() {
            List<CandleStickIntervalInfo> list = new List<CandleStickIntervalInfo>();
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(1), Text = "1 Minute", Command = "1m" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(5), Text = "5 Minutes", Command = "5m" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(15), Text = "15 Minutes", Command = "15m" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(30), Text = "30 Minutes", Command = "30m" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromHours(1), Text = "1 Hour", Command = "1h" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromHours(3), Text = "3 Hours", Command = "3h" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromHours(6), Text = "6 Hours", Command = "6h" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromHours(12), Text = "12 Hours", Command = "12h" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromDays(1), Text = "1 Day", Command = "1D" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromDays(7), Text = "1 Week", Command = "1W" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromDays(14), Text = "14 Days", Command = "14D" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromDays(30), Text = "1 Month", Command = "1M" });
            return list;
        }

        protected List<TradeInfoItem> UpdateList { get; } = new List<TradeInfoItem>(100);

        public override ResizeableArray<CandleStickData> GetRecentCandleStickData(Ticker ticker, int candleStickPeriodMin) {
            return base.GetRecentCandleStickData(ticker, candleStickPeriodMin);
        }

        public override ResizeableArray<CandleStickData> GetCandleStickData(Ticker ticker, int candleStickPeriodMin, DateTime start, long periodInSeconds) {
            long msStart = (long)((start - epoch).TotalMilliseconds);
            long msEnd = msStart + periodInSeconds * 1000;

            string name = GetCandleStickCommandName(candleStickPeriodMin);
            string address = null; 
            if(start.Date == DateTime.Today.Date)
                address = string.Format("https://api-pub.bitfinex.com/v2/candles/trade:{0}:{1}/hist", name, Uri.EscapeDataString(ticker.CurrencyPair));
            else 
                address = string.Format("https://api-pub.bitfinex.com/v2/candles/trade:{0}:{1}/hist?start={2}&end={3}", name, Uri.EscapeDataString(ticker.CurrencyPair), msStart, msEnd);
            try {
                return OnGetCandleStickData(ticker, GetDownloadBytes(address));
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return null;
            }
        }

        private ResizeableArray<CandleStickData> OnGetCandleStickData(Ticker ticker, byte[] bytes) {
            if(bytes == null || bytes.Length == 0) {
                LogManager.Default.Error(this, nameof(GetCandleStickData), "No data received");
                return null;
            }
            var root = JsonHelper.Default.Deserialize(bytes);


            ResizeableArray<CandleStickData> list = new ResizeableArray<CandleStickData>(root.ItemsCount);
            int itemsCount = root.ItemsCount;
            for(int i = 0; i < itemsCount; i++) {
                var item = root.Items[i];
                CandleStickData data = new CandleStickData();
                data.Time = BitFinexTime(item.Items[0]);
                data.Open = item.Items[1].ValueDouble;
                data.Close = item.Items[2].ValueDouble;
                data.High = item.Items[3].ValueDouble;
                data.Low = item.Items[4].ValueDouble;
                data.Volume = item.Items[5].ValueDouble;
                list[itemsCount - 1 - i] = data;
                //list.Add(data);
            }
            return list;
        }

        protected string TickersUpdateAddress { get; set; }

        public override bool GetTickersInfo() {
            if(Tickers.Count > 0)
                return true;

            string address = "https://api-pub.bitfinex.com/v2/tickers?symbols=ALL";
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(address);
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }

            return OnUpdateTickersInfo(bytes);
        }

        protected virtual bool OnUpdateTickersInfo(byte[] bytes) {
            if(bytes == null) {
                LogManager.Default.Error(this, nameof(GetTickersInfo), "No data");
                return false;
            }
            
            var root = JsonHelper.Default.Deserialize(bytes);
            for(int i = 0; i < root.ItemsCount; i++) {
                var item = root.Items[i];
                string name = item.Items[0].Value;
                if(name[0] == 't') {
                    BitFinexTicker t = (BitFinexTicker)GetOrCreateTicker(name);
                    t.CurrencyPair = name;
                    t.MarketName = name;
                    t.HighestBidString = item.Items[1].Value;
                    t.LowestAskString = item.Items[3].Value;
                    t.LastString = item.Items[7].Value;
                    t.BaseVolumeString = item.Items[8].Value;
                    t.Hr24HighString = item.Items[9].Value;
                    t.Hr24LowString = item.Items[10].Value;
                    AddTicker(t);
                }
                else {

                }
            }
            IsInitialized = true;
            return true;
        }

        public override bool UpdateCurrencies() {
            string address = "https://api-pub.bitfinex.com/v2/conf/pub:list:currency";
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(address);
            }
            catch(Exception) {
                return false;
            }
            return OnUpdateCurrencies(bytes);
            
            //List<string[]> res = JsonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "Currency", "CurrencyLong", "MinConfirmation", "TxFee", "IsActive", "CoinType", "BaseAddress", "Notice" });
            //for(int i = 0; i < res.Count; i++) {
            //    string[] item = res[i];
            //    string currency = item[0];
            //    var c = GetOrCreateCurrency(currency);
            //    c.CurrencyLong = item[1];
            //    c.MinConfirmation = int.Parse(item[2]);
            //    c.TxFee = FastValueConverter.Convert(item[3]);
            //    c.CoinType = item[5];
            //    c.BaseAddress = item[6];
            //    c.IsActive = item[4].Length == 4;
            //}
            //return true;
        }

        protected virtual bool OnUpdateCurrencies(byte[] bytes) {
            var root = JsonHelper.Default.Deserialize(bytes);
            if(root.ItemsCount == 0)
                return true;
            int itemsCount = root.Items[0].ItemsCount;
            for(int i = 0; i < itemsCount; i++) {
                var item = root.Items[0].Items[i];
                BitFinexCurrencyInfo info = (BitFinexCurrencyInfo)GetOrCreateCurrency(item.Value);
            }
            return true;
        }

        public override CurrencyInfo CreateCurrency(string currency) {
            return new BitFinexCurrencyInfo(this, currency);
        }

        public bool GetCurrenciesInfo() {
            Currencies.Clear();
            return UpdateCurrencies();
        }
        public override bool UpdateTicker(Ticker tickerBase) {
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(string.Format("https://api.bitfinex.com/v2/ticker/{0}", ((BitFinexTicker)tickerBase).MarketName));
            }
            catch(Exception e) {
                Debug.WriteLine(e.ToString());
                return false;
            }
            if(bytes == null)
                return false;

            int startIndex = 0;
            string[] item = JsonHelper.Default.DeserializeArray(bytes, ref startIndex, 9);

            BitFinexTicker ticker = (BitFinexTicker)tickerBase;
            ticker.HighestBid = FastValueConverter.Convert(item[0]);
            ticker.LowestAsk = FastValueConverter.Convert(item[2]);
            ticker.Change = FastValueConverter.Convert(item[4]);
            ticker.Last = FastValueConverter.Convert(item[5]);
            ticker.Volume = FastValueConverter.Convert(item[6]);
            ticker.Hr24High = FastValueConverter.Convert(item[7]);
            ticker.Hr24Low = FastValueConverter.Convert(item[8]);
            ticker.Time = DateTime.Now;
            ticker.UpdateHistoryItem();

            return true;
        }
        public override bool UpdateTickersInfo() {
            return GetTickersInfo();
        }

        public override void StopListenOrderBook(Ticker ticker, bool force) {
            base.StopListenOrderBook(ticker, force);
            string marketSymbol = ticker.MarketName;
            BitFinexTicker bft = (BitFinexTicker)ticker;
            if(TickersSocket != null)
                TickersSocket.Unsubscribe(new WebSocketSubscribeInfo(SocketSubscribeType.OrderBook, ticker) {
                Request = "{ \"event\":\"unsubscribe\", \"chanId\" : \"" + bft.OrderBookSocketChannelId + "\" }"
            });
        }

        public override void StopListenTradeHistory(Ticker ticker, bool force) {
            base.StopListenTradeHistory(ticker, force);
            string marketSymbol = ticker.MarketName;
            BitFinexTicker bft = (BitFinexTicker)ticker;
            if(TickersSocket != null)
                TickersSocket.Unsubscribe(new WebSocketSubscribeInfo(SocketSubscribeType.TradeHistory, ticker) {
                Request = "{ \"event\":\"unsubscribe\", \"chanId\" : \"" + bft.TradeHistorySocketChannelId + "\" }"
            });
        }

        public override void StopListenKline(Ticker ticker, bool force) {
            base.StopListenKline(ticker, force);
            string marketSymbol = ticker.MarketName;
            BitFinexTicker bft = (BitFinexTicker)ticker;
            if(TickersSocket != null)
                TickersSocket.Unsubscribe(new WebSocketSubscribeInfo(SocketSubscribeType.Kline, ticker) {
                Request = "{ \"event\":\"unsubscribe\", \"chanId\" : \"" + bft.KlineSocketChannelId + "\" }",
            });
        }

        protected override void StartListenOrderBookCore(Ticker ticker) {
            if(TickersSocket == null) {
                StartListenTickersStream();
                if(!WaitUntil(5000, () => { return TickersSocketState == SocketConnectionState.Connected; }))
                    return;
            }
            string marketSymbol = ticker.MarketName;
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.OrderBook, ticker) { 
                Request = "{ \"event\":\"subscribe\", \"channel\" : \"book\", \"prec\" : \"P0\", \"len\": \"250\", \"symbol\" : \"" + ticker.CurrencyPair + "\" }", 
            });
        }

        protected override void StartListenKlineCore(Ticker ticker) {
            if(TickersSocket == null) {
                StartListenTickersStream();
                if(!WaitUntil(5000, () => { return TickersSocketState == SocketConnectionState.Connected; }))
                    return;
            }
            string marketSymbol = ticker.MarketName;
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.Kline, ticker) {
                Request = "{ \"event\":\"subscribe\", \"channel\" : \"candles\", \"key\" : \"trade:" + GetCandleStickCommandName(ticker.CandleStickPeriodMin) + ":" + ticker.CurrencyPair + "\" }",
            });
        }

        protected override void StartListenTradeHistoryCore(Ticker ticker) {
            if(TickersSocket == null) {
                StartListenTickersStream();
                if(!WaitUntil(5000, () => { return TickersSocketState == SocketConnectionState.Connected; }))
                    return;
            }
            string marketSymbol = ticker.MarketName;
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.TradeHistory, ticker) {
                Request = "{ \"event\":\"subscribe\", \"channel\" : \"trades\", \"symbol\" : \"" + ticker.CurrencyPair + "\" }",
            });
        }

        protected Dictionary<int, BitFinexTicker> Channels { get; } = new Dictionary<int, BitFinexTicker>();

        
        protected internal override void OnTickersSocketMessageReceived(object sender, MessageReceivedEventArgs e) {
            base.OnTickersSocketMessageReceived(sender, e);
            var root = JsonHelper.Default.Deserialize(e.Message);
            
            var pEvent = root.GetProperty("event");
            if(pEvent != null) {
                OnChannelSubscribed(root);
                return;
            }
            if(root.ItemsCount > 0) {
                int ic = root.ItemsCount;
                int channelId = root.Items[0].ValueInt;
                BitFinexTicker ticker = null;
                if(!Channels.TryGetValue(channelId, out ticker)) {
                    LogManager.Default.Error(this, Type.ToString(), "No ticker found for channel: " + channelId);
                    return;
                }
                if(ic == 2 && root.Items[1].Value == "hb")
                    return; //hearthbeat

                if(channelId == ticker.OrderBookSocketChannelId)
                    OnOrderBookSocketMessageReceived(root, ticker, e.Message);
                else if(channelId == ticker.KlineSocketChannelId)
                    OnKlineSocketMessageReceived(root, ticker, e.Message);
                else if(channelId == ticker.TradeHistorySocketChannelId)
                    OnTradeHistorySocketMessageReceived(root, ticker, e.Message);
            }
        }
        protected DateTime BitFinexTime(JsonHelperToken item) {
            return epoch.AddMilliseconds(item.ValueLong).ToLocalTime();
        }
        protected virtual void OnKlineSocketMessageReceived(JsonHelperToken root, BitFinexTicker ticker, string message) {
            if(ticker.CaptureData)
                ticker.CaptureDataCore(CaptureStreamType.KLine, CaptureMessageType.Incremental, message);
            if(root.ItemsCount == 0 || root.Items[1].ItemsCount == 0)
                return;
            bool isSnapshot = root.Items[1].Type == JsonObjectType.Array && root.Items[1].Items[0].Type == JsonObjectType.Array;
            if(isSnapshot) {
                ResizeableArray<CandleStickData> candles = new ResizeableArray<CandleStickData>(root.Items[1].ItemsCount);
                int ic = root.Items[1].ItemsCount;
                for(int i = 1; i < ic; i++) {
                    CandleStickData candle = new CandleStickData();
                    var item = root.Items[1].Items[i];
                    candle.Time = BitFinexTime(item.Items[0]);
                    UpdateCandle(candle, item);
                    candles.Add(candle);
                }
                ticker.CandleStickData = candles;
            }
            else {
                DateTime time = BitFinexTime(root.Items[1].Items[0]);
                var candle = ticker.GetCandleStickItem(time);
                if(candle == null) {
                    candle = new CandleStickData();
                    candle.Time = time;
                    ticker.CandleStickData.Add(candle);
                }
                UpdateCandle(candle, root.Items[1]);
            }
        }

        protected virtual void UpdateCandle(CandleStickData candle, JsonHelperToken item) {
            candle.Open = item.Items[1].ValueDouble;
            candle.Close = item.Items[2].ValueDouble;
            candle.High = item.Items[3].ValueDouble;
            candle.Low = item.Items[4].ValueDouble;
            candle.Volume = item.Items[5].ValueDouble;
        }

        protected virtual void ProcessOrderBookIncrementalUpdate(JsonHelperToken[] item, List<string[]> bids, List<string[]> asks) {
            if(item[2].Value[0] == '-') {
                if(item[1].Value == "0")
                    asks.Add(new string[] { item[0].Value, "0" });
                else
                    asks.Add(new string[] { item[0].Value, item[2].Value.Substring(1) });
            }
            else {
                if(item[1].Value == "0")
                    bids.Add(new string[] { item[0].Value, "0" });
                else 
                    bids.Add(new string[] { item[0].Value, item[2].Value });
            }
        }
        protected virtual void OnOrderBookSocketMessageReceived(JsonHelperToken root, BitFinexTicker ticker, string message) {
            var provider = CreateIncrementalUpdateDataProvider();
            if(ticker.OrderBook.Bids.Count == 0) {
                provider.ApplySnapshot(root, ticker);
                if(ticker.CaptureData)
                    ticker.CaptureDataCore(CaptureStreamType.OrderBook, CaptureMessageType.Snapshot, message);
                return;
            }
            if(ticker.CaptureData)
                ticker.CaptureDataCore(CaptureStreamType.OrderBook, CaptureMessageType.Incremental, message);
            int ic = root.Items[1].ItemsCount;
            var items = root.Items[1].Items;
            List<string[]> bids = new List<string[]>();
            List<string[]> asks = new List<string[]>();
            if(root.Items[1].Items[0].Type != JsonObjectType.Array) {
                var item = root.Items[1].Items;
                ProcessOrderBookIncrementalUpdate(item, bids, asks);
            }
            else {
                for(int i = 1; i < ic; i++) {
                    var item = items[i].Items;
                    ProcessOrderBookIncrementalUpdate(item, bids, asks);
                }
            }
            IncrementalUpdateInfo info = new IncrementalUpdateInfo();
            info.Fill(-1, ticker, bids, asks, null);
            provider.Update(ticker, info);
            
        }
        protected TradeInfoItem ProcessTradeInfoItem(Ticker ticker, JsonHelperToken[] item) {
            TradeInfoItem t = new TradeInfoItem(null, ticker);
            t.Time = BitFinexTime(item[1]);
            TradeType type = item[2].Value[0] == '-' ? TradeType.Sell : TradeType.Buy;
            t.AmountString = type == TradeType.Sell ? item[2].Value.Substring(1) : item[2].Value;
            t.Type = type;
            t.RateString = item[3].Value;
            return t;
        }
        protected virtual void OnTradeHistorySocketMessageReceived(JsonHelperToken root, BitFinexTicker ticker, string message) {
            if(ticker.CaptureData)
                ticker.CaptureDataCore(CaptureStreamType.TradeHistory, CaptureMessageType.Incremental, message);

            List<TradeInfoItem> newItems = new List<TradeInfoItem>();
            if(root.Items[1].Value == "te" || root.Items[1].Value == "tu") {
                // incremental
                var item = root.Items[2].Items;
                TradeInfoItem t = ProcessTradeInfoItem(ticker, item);
                ticker.AddTradeHistoryItem(t);
                newItems.Add(t);
            }
            else {
                ticker.LockTrades();
                try {
                    ticker.ClearTradeHistory();
                    int ic = root.Items[1].ItemsCount;

                    for(int i = ic - 1; i >= 0; i--) {
                        var item = root.Items[1].Items[i].Items;
                        TradeInfoItem t = ProcessTradeInfoItem(ticker, item);
                        newItems.Add(t);
                        ticker.AddTradeHistoryItem(t);
                    }
                }
                finally {
                    ticker.UnlockTrades();
                }
            }

            if(newItems.Count > 0) {
                if(ticker.HasTradeHistorySubscribers)
                    ticker.RaiseTradeHistoryChanged(new TradeHistoryChangedEventArgs() { Ticker = ticker, NewItems = newItems });
            }
        }

        protected virtual void OnChannelSubscribed(JsonHelperToken root) {
            var pEvent = root.GetProperty("event");
            var channel = root.GetProperty("channel")?.Value;
            var symbol = root.GetProperty("symbol")?.Value;
            var pChannelId = root.GetProperty("chanId");

            if(pEvent.Value == "error") {
                LogManager.Default.Error(this, Type.ToString(), "Failed request to web socket: " + channel + ", " + symbol);
                return;
            }
            if(pEvent.Value == "subscribed") {
                if(channel == "candles") {
                    var key = root.GetProperty("key")?.Value;
                    if(key != null) {
                        string[] items = key.Split(':');
                        symbol = items[items.Length - 1];
                    }
                }
                BitFinexTicker ticker = (BitFinexTicker)Ticker(symbol);
                if(ticker == null) {
                    LogManager.Default.Error(this, Type.ToString(), "Ticker not found: " + channel + ", " + symbol);
                    return;
                }
                if(!Channels.ContainsKey(pChannelId.ValueInt))
                    Channels.Add(pChannelId.ValueInt, ticker);
                else
                    Channels[pChannelId.ValueInt] = ticker;
                if(channel == "book") {
                    ticker.OrderBookSocketChannelId = pChannelId.ValueInt;
                    ticker.OrderBook.Clear();
                }
                else if(channel == "trades") {
                    ticker.TradeHistorySocketChannelId = pChannelId.ValueInt;
                    ticker.ClearTradeHistory();
                }
                else if(channel == "candles") {
                    ticker.KlineSocketChannelId = pChannelId.ValueInt;
                    ticker.CandleStickData.Clear();
                }
            }
            return;
        }

        public override bool UpdateOrderBook(Ticker info, int depth) {
            string address = GetOrderBookString(info, depth);
            try {
                return OnUpdateOrderBook(info, GetDownloadBytes(address));
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }
        public override void UpdateOrderBookAsync(Ticker ticker, int depth, Action<OperationResultEventArgs> onOrderBookUpdate) {
            string address = GetOrderBookString(ticker, depth);
            GetDownloadBytesAsync(address, t => {
                OnUpdateOrderBook(ticker, t.Result);
                onOrderBookUpdate(new OperationResultEventArgs() { Ticker = ticker, Result = t.Result != null });
            });
        }
        protected string GetOrderBookString(Ticker info, int depth) {
            return string.Format("https://api.bitfinex.com/v2/book/{0}/R0?len={1}", Uri.EscapeDataString(info.MarketName), depth);
        }
        
        public bool UpdateOrderBook(BitFinexTicker info, byte[] data, int depth) {
            return OnUpdateOrderBook(info, data);
        }
        public override bool UpdateOrderBook(Ticker tickerBase) {
            return UpdateOrderBook(tickerBase, OrderBook.Depth);
        }
        protected virtual bool OnUpdateOrderBook(Ticker ticker, byte[] bytes) {
            if(bytes == null) {
                LogManager.Default.Error(this, nameof(OnUpdateOrderBook), "No data received.");
                return false;
            }

            var root = JsonHelper.Default.Deserialize(bytes);

            ticker.OrderBook.BeginUpdate();
            try {
                ticker.OrderBook.GetNewBidAsks();

                int bc = root.ItemsCount / 2;
                var jb = root.Items;

                int ac = root.ItemsCount / 2;
                var ja = root.Items;

                List<OrderBookEntry> bids = ticker.OrderBook.Bids;
                List<OrderBookEntry> asks = ticker.OrderBook.Asks;

                for(int i = 0; i < bc; i++) {
                    var item = jb[i];
                    bids.Add(new OrderBookEntry() { ValueString = item.Items[1].Value, AmountString = item.Items[2].Value });
                }

                for(int i = 0; i < ac; i++) {
                    var item = ja[i + bc];
                    asks.Add(new OrderBookEntry() { ValueString = item.Items[1].Value, AmountString = item.Items[2].Value.Substring(1) });
                }
            }
            finally {
                ticker.OrderBook.IsDirty = false;
                ticker.OrderBook.EndUpdate();
            }
            return true;
        }
        public override bool UpdateAccountTrades(AccountInfo account, Ticker ticker) {
            if(account == null)
                return false;

            string address = null;
            string apiPath = null;
            if(ticker != null) {
                address = string.Format("https://api.bitfinex.com/v2/auth/r/trades/{0}/hist", ticker.CurrencyPair);
                apiPath = string.Format("/api/v2/auth/r/trades/{0}/hist", ticker.CurrencyPair);
            }
            else {
                address = "https://api.bitfinex.com/v2/auth/r/trades/hist";
                apiPath = "/api/v2/auth/r/trades/hist";
            }

            try {
                return OnGetAccountTrades(account, ticker, UploadPrivateData(account, address, apiPath, "{\"limit\":\"2500\"}"));
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }
        protected virtual bool OnGetAccountTrades(AccountInfo account, Ticker ticker, byte[] data) {
            if(data == null)
                return false;

            JsonHelperToken root = JsonHelper.Default.Deserialize(data);
            if(HasError(account, root, nameof(OnGetOpenedOrders), data))
                return false;

            if(ticker == null)
                return true;
            ticker.LockAccountTrades();
            try {
                ticker.ClearMyTradeHistory();
                for(int i = 0; i < root.ItemsCount; i++) {
                    var trade = root.Items[i];
                    if(trade.Items[1].Value != ticker.CurrencyPair)
                        continue;
                    TradeInfoItem ti = new TradeInfoItem(account, ticker);

                    ti.IdString = trade.Items[0].Value;
                    ti.Time = BitFinexTime(trade.Items[2]);
                    ti.RateString = trade.Items[5].Value;
                    ti.Type = TradeType.Buy;
                    ti.Fill = TradeFillType.Fill;
                    ti.FeeString = trade.Items[9].Value;
                    if(trade.Items[4].Value[0] == '-') {
                        ti.AmountString = trade.Items[4].Value.Substring(1);
                        ti.Type = TradeType.Sell;
                    }
                    ticker.AddAccountTradeHistoryItem(ti);
                }
            }
            finally {
                ticker.UnlockAccountTrades();
            }
            return true;
        }
        
        public override bool UpdateTrades(Ticker info) {
            string address = string.Format("https://api-pub.bitfinex.com/v2/trades/{0}/hist", Uri.EscapeDataString(info.MarketName));
            try {
                return OnUpdateTrades(info, GetDownloadBytes(address));
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }

        protected virtual bool OnUpdateTrades(Ticker ticker, byte[] bytes) {
            if(bytes == null) {
                LogManager.Default.Error(this, nameof(OnUpdateTrades), "No data received");
                return false;
            }

            var root = JsonHelper.Default.Deserialize(bytes);
            int itemsCount = root.ItemsCount;
            ResizeableArray<TradeInfoItem> newItems = new ResizeableArray<TradeInfoItem>(itemsCount);
            
            lock(ticker) {
                ticker.LockTrades();
                ticker.ClearTradeHistory();
                for(int i = 0; i < itemsCount; i++) {
                    var item = root.Items[i];
                    TradeInfoItem ti = new TradeInfoItem(DefaultAccount, ticker);
                    ti.IdString = item.Items[0].Value;
                    ti.Time = BitFinexTime(item.Items[1]);
                    if(item.Items[2].Value[0] == '-') {
                        ti.AmountString = item.Items[2].Value.Substring(1);
                        ti.Type = TradeType.Sell;
                    }
                    else {
                        ti.AmountString = item.Items[2].Value;
                        ti.Type = TradeType.Buy;
                    }
                    ti.RateString = item.Items[3].Value;
                    ticker.InsertTradeHistoryItem(ti);
                    newItems.Add(ti);
                }
                ticker.UnlockTrades();
            }
                
            if(ticker.HasTradeHistorySubscribers)
                ticker.RaiseTradeHistoryChanged(new TradeHistoryChangedEventArgs() { NewItems = newItems });
            return true;
        }
        public override TradingResult BuyLong(AccountInfo account, Ticker ticker, double rate, double amount) {
            string address = "https://api.bitfinex.com/v2/auth/w/order/submit";
            string apiPath = "/api/v2/auth/w/order/submit";
            string jsonQuery = string.Format("{{\"type\":\"EXCHANGE LIMIT\",\"symbol\":\"{0}\",\"price\":\"{1:0.00000000}\",\"amount\":\"{2:0.00000000}\"}}", ticker.CurrencyPair, rate, amount);
            try {
                return OnBuy(account, ticker, UploadPrivateData(account, address, apiPath, jsonQuery));
            }
            catch(Exception e) {
                LogManager.Default.Error(this, nameof(BuyLong), e.ToString());
                return null;
            }
        }
        public override TradingResult SellLong(AccountInfo account, Ticker ticker, double rate, double amount) {
            string address = "https://api.bitfinex.com/v2/auth/w/order/submit";
            string apiPath = "/api/v2/auth/w/order/submit";
            string jsonQuery = string.Format("{{\"type\":\"EXCHANGE LIMIT\",\"symbol\":\"{0}\",\"price\":\"{1:0.00000000}\",\"amount\":\"-{2:0.00000000}\"}}", ticker.CurrencyPair, rate, amount);
            try {
                return OnSell(account, ticker, UploadPrivateData(account, address, apiPath, jsonQuery));
            }
            catch(Exception e) {
                LogManager.Default.Error(this, nameof(SellLong), e.ToString());
                return null;
            }
        }
        public override TradingResult BuyShort(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public override TradingResult SellShort(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }
        public override bool Cancel(AccountInfo account, Ticker ticker, string orderId) {
            string address = "https://api.bitfinex.com/v2/auth/w/order/cancel";
            string apiPath = "/api/v2/auth/w/order/cancel";
            string jsonQuery = string.Format("{{\"id\":{0}}}", orderId);
            try {
                return OnCancel(account, ticker, UploadPrivateData(account, address, apiPath, jsonQuery));
            }
            catch(Exception e) {
                LogManager.Default.Error(this, nameof(Cancel), e.ToString());
                return false;
            }
        }
        
        public override bool UpdateOpenedOrders(AccountInfo account, Ticker ticker) {
            if(account == null)
                return false;
            string address = null, apiPath = null;
            if(ticker != null) {
                address = string.Format("https://api.bitfinex.com/v2/auth/r/orders/{0}/hist", ticker.CurrencyPair);
                apiPath = string.Format("/api/v2/auth/r/orders/{0}/hist", ticker.CurrencyPair);
            }
            else {
                address = "https://api.bitfinex.com/v2/auth/r/orders";
                apiPath = "/api/v2/auth/r/orders";
            }

            try {
                return OnGetOpenedOrders(account, ticker, UploadPrivateData(account, address, apiPath, "{\"limit\":\"2500\"}"));
            } catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }
        
        protected TradingResult OnSubmitOrder(AccountInfo account, Ticker ticker, byte[] data, OrderType type) {
            if(data == null) {
                LogManager.Default.Error(this, nameof(OnSubmitOrder), "No data received");
                return null;
            }

            var root = JsonHelper.Default.Deserialize(data);
            if(HasError(account, root, nameof(OnSubmitOrder), data))
                return null;
            if(root.ItemsCount < 8)
                return null;

            TradingResult res = new TradingResult();
            res.Date = BitFinexTime(root.Items[0]);
            res.OrderStatus = root.Items[5].Value;
            
            var tr = root.Items[4].Items[0];
            res.OrderId = tr.Items[0].Value;
            res.Value = tr.Items[16].ValueDouble;
            res.Amount = Math.Abs(tr.Items[7].ValueDouble);
            res.Type = type;
            res.Ticker = ticker;

            return res;
        }
        public TradingResult OnBuy(AccountInfo account, Ticker ticker, byte[] data) {
            return OnSubmitOrder(account, ticker, data, OrderType.Buy);
        }
        public TradingResult OnSell(AccountInfo account, Ticker ticker, byte[] data) {
            return OnSubmitOrder(account, ticker, data, OrderType.Sell);
        }
        public bool OnCancel(AccountInfo account, Ticker ticker, byte[] data) {
            if(data == null) {
                LogManager.Default.Error(this, nameof(OnCancel), "No data received");
                return false;
            }

            var root = JsonHelper.Default.Deserialize(data);
            if(HasError(account, root, nameof(OnCancel), data))
                return false;
            if(root.ItemsCount < 8)
                return false;

            return root.Items[6].Value == "SUCCESS";
        }
        public bool OnGetOpenedOrders(AccountInfo account, Ticker ticker, byte[] data) {
            if(!ticker.IsOpenedOrdersChanged(data))
                return true;
            ticker.SaveOpenedOrders();
            ticker.OpenedOrdersData = data;

            JsonHelperToken root = JsonHelper.Default.Deserialize(data);
            if(HasError(account, root, nameof(OnGetOpenedOrders), data))
                return false;
            
            try {
                account.OpenedOrders.Clear();
                for(int i = 0; i < root.ItemsCount; i++) {
                    var order = root.Items[i];
                    if(order.Items[3].Value != ticker.CurrencyPair)
                        continue;
                    if(order.Items[13].Value[0] != 'S') // if not successed but canceled or closed
                        continue;
                    OpenedOrderInfo info = new OpenedOrderInfo(account, ticker);
                    
                    info.OrderId = order.Items[0].Value;
                    info.Date = BitFinexTime(order.Items[4]);
                    info.ValueString = order.Items[16].Value;
                    info.Type = OrderType.Buy;
                    if(order.Items[7].Value[0] == '-') {
                        info.AmountString = order.Items[7].Value.Substring(1);
                        info.Type = OrderType.Sell;
                    }
                    account.OpenedOrders.Add(info);
                }
            }
            finally {
                if(ticker != null)
                    ticker.RaiseOpenedOrdersChanged();
            }
            return true;
        }
        
        public override bool GetBalance(AccountInfo account, string currency) {
            return UpdateBalances(account);
        }
        
        public override bool UpdateBalances(AccountInfo account) {
            if(account == null)
                return false;
            if(Currencies.Count == 0) {
                if(!GetCurrenciesInfo())
                    return false;
            }
            const string address = "https://api.bitfinex.com/v2/auth/r/wallets";
            const string apiPath = "/api/v2/auth/r/wallets";
            try {
                return OnGetBalances(account, UploadPrivateData(account, address, apiPath, null));
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }

        protected virtual bool OnGetBalances(AccountInfo account, byte[] data) {
            if(data == null) {
                LogManager.Default.Error(this, nameof(UpdateBalances), "No data received");
                return false;
            }

            var root = JsonHelper.Default.Deserialize(data);
            if(HasError(account, root, nameof(OnGetBalances), data))
                return false;
            account.Balances.ForEach(b => b.Clear());
            JsonHelperToken[] balances =  root.Items;
            if(balances == null) {
                balances = root.PropertiesCount > 2 ? root.Properties[1].Items : null;
                if(balances == null)
                    return true;
            }
            foreach(JsonHelperToken item in balances) {
                var b = account.GetOrCreateBalanceInfo(item.Items[1].Value);
                b.OnOrders = 0;
                b.Available = item.Items[4].ValueDouble;
                b.Balance = item.Items[2].ValueDouble;
            }
            return true;
        }

        public override bool GetDepositMethods(AccountInfo account, CurrencyInfo currency) {
            try {
                string address = "https://api-pub.bitfinex.com//v2/conf/pub:map:tx:method";
                return OnGetDepositMethods(account, currency, GetDownloadBytes(address));
            }
            catch(Exception e) {
                LogManager.Default.Log(e.ToString());
                return false;
            }
        }

        protected virtual bool OnGetDepositMethods(AccountInfo account, CurrencyInfo currency, byte[] data) {
            if(data == null) {
                LogManager.Default.Error(this, nameof(UpdateBalances), "No data received");
                return false;
            }

            var root = JsonHelper.Default.Deserialize(data);
            if(HasError(account, root, nameof(OnGetDepositMethods), data))
                return false;
            foreach(var item in root.Items[0].Items) {
                string methodName = item.Items[0].Value;
                foreach(var curr in item.Items[1].Items) {
                    var cinfo = GetOrCreateCurrency(curr.Value);
                    var method = cinfo.GetOrCreateMethod(methodName);
                    method.Currency = curr.Value;
                    method.CurrencyName = curr.Value;
                }
            }
            return true;
        }

        protected virtual bool HasError(AccountInfo account, JsonHelperToken root, string methodName, byte[] data) {
            if(root.ItemsCount == 0)
                return false;
            if(root.Items[0].Value != "error")
                return false;
            string accountName = account != null? account.Name: "any account";
            LogManager.Default.Error(this, methodName, "account: " + account + ", message = " + root.Items[2].Value);
            return true;
        }

        protected internal override HMAC CreateHmac(string secret) {
            return new HMACSHA384(ASCIIEncoding.Default.GetBytes(secret));
        }

        public override string GetSign(string path, string text, string nonce, AccountInfo info) {
            return GetSign(path + text, info);
        }

        protected virtual byte[] UploadPrivateData(AccountInfo info, string address, string apiPath, string jsonString) {
            MyWebClient client = GetWebClient();

            string nonce = GetNonce();
            string queryString = null;
            if(jsonString == null)
                jsonString = "";
            if(string.IsNullOrEmpty(jsonString))
                queryString = nonce;
            else
                queryString = string.Format("{0}{1}", nonce, jsonString);
            string signature = info.GetSign(apiPath, queryString, nonce);

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("bfx-nonce", nonce);
            client.DefaultRequestHeaders.Add("bfx-apikey", info.ApiKey);
            client.DefaultRequestHeaders.Add("bfx-signature", signature);

            StringContent data = new StringContent(jsonString, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> response = client.PostAsync(address, data);
            response.Wait(10000);
            Task<byte[]> bytes = response.Result.Content.ReadAsByteArrayAsync();
            bytes.Wait(10000);
            return bytes.Result;
        }

        public override bool Withdraw(AccountInfo account, string currency, string address, string paymentId, double amount) {
            throw new NotImplementedException();
        }

        public override bool CreateDeposit(AccountInfo account, string currency) {
            var c = GetOrCreateCurrency(currency);
            if(!GetDepositMethods(account, c))
                return false;

            var b = account.GetOrCreateBalanceInfo(currency);
            string address = string.Format("https://api.bitfinex.com/v2/auth/w/deposit/address");
            string apiPath = "/api/v2/auth/w/deposit/address";
            string jsonString = string.Format("{{\"wallet\":\"exchange\",\"method\":\"{0}\",\"op_renew\":\"0\"}}", b.CurrentMethod.Name);
            try {
                return OnCreateDeposit(account, currency, UploadPrivateData(account, address, apiPath, jsonString));
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }
        protected virtual bool OnCreateDeposit(AccountInfo account, string currency, byte[] data) {
                if(data == null) {
                LogManager.Default.Error(this, nameof(UpdateBalances), "No data received");
                return false;
            }

            var root = JsonHelper.Default.Deserialize(data);
            if(HasError(account, root, nameof(OnCreateDeposit), data))
                return false;
            
            return true;
        }

        protected string GetNonce() {
            long timestamp = (long)((DateTime.UtcNow - epoch).TotalMilliseconds * 1000);
            return timestamp.ToString();
        }
        protected internal override void ApplyCapturedEvent(Ticker ticker, TickerCaptureDataInfo info) {
            if(info.StreamType == CaptureStreamType.OrderBook)
                OnOrderBookSocketMessageReceived(this, new MessageReceivedEventArgs(info.Message));
            else if(info.StreamType == CaptureStreamType.TradeHistory)
                OnTradeHistorySocketMessageReceived(this, new MessageReceivedEventArgs(info.Message));
            else if(info.StreamType == CaptureStreamType.KLine)
                OnKlineSocketMessageReceived(this, new MessageReceivedEventArgs(info.Message));
        }
    }
}
