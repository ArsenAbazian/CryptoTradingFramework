using Crypto.Core.Common;
using Crypto.Core.Exchanges.Bittrex;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crypto.Core.Helpers;
using Crypto.Core.Exchanges.Base;
using System.Security.Cryptography;
using System.Net.Http;
using WebSocket4Net;

namespace Crypto.Core.Bittrex {
    public class BittrexExchange : Exchange {
        static BittrexExchange defaultExchange;
        public static BittrexExchange Default {
            get {
                if(defaultExchange == null)
                    defaultExchange = (BittrexExchange)Exchange.FromFile(ExchangeType.Bittrex, typeof(BittrexExchange));
                return defaultExchange;
            }
        }

        public override Ticker CreateTicker(string name) {
            return new BittrexTicker(this) { CurrencyPair = name };
        }

        public override BalanceBase CreateAccountBalance(AccountInfo info, string currency) {
            return new BittrexAccountBalanceInfo(info, GetOrCreateCurrency(currency));
        }

        public override bool SupportSimulation => false;
        protected override ResizeableArray<TradeInfoItem> GetTradesCore(Ticker ticker, DateTime starTime, DateTime endTime) {
            return new ResizeableArray<TradeInfoItem>();
        }

        public BittrexExchange() {
            RequestRate = new List<RateLimit>();
            RequestRate.Add(new RateLimit(this) { Limit = 60, Interval = TimeSpan.TicksPerMinute });
            //RequestRate.Add(new RateLimit(this) { Limit = 1, Interval = TimeSpan.TicksPerSecond });
        }

        public override string GetSign(string text, AccountInfo info) {
            return base.GetSign(text, info);
        }

        protected override bool ShouldAddKlineListener => true;

        protected internal override IIncrementalUpdateDataProvider CreateIncrementalUpdateDataProvider() {
            return new BittrexIncrementalUpdateDataProvider();
        }

        public override string BaseWebSocketAdress => "https://socket-v3.bittrex.com/signalr";

        public override ExchangeType Type => ExchangeType.Bittrex;

        public override bool SupportWebSocket(WebSocketType type) {
            if(type == WebSocketType.Tickers)
                return true;
            if(type == WebSocketType.Ticker || type == WebSocketType.Trades || type == WebSocketType.OrderBook || type == WebSocketType.Kline)
                return true;
            return false;
        }

        public override bool GetDeposites(AccountInfo account) {
            string address = "https://api.bittrex.com/v3/deposits/open";
            try {
                return OnGetDeposites(account, DownloadPrivateData(address, account));
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }

        private bool OnGetDeposites(AccountInfo account, byte[] bytes) {
            if(bytes == null)
                return false;

            JsonHelperToken root = JsonHelper.Default.Deserialize(bytes);
            string error = CheckForErrors(root);
            if(!string.IsNullOrEmpty(error)) {
                LogManager.Default.Add(LogType.Error, this, Type.ToString(), "Failed OnGetDeposites", error);
                return false;
            }

            int itemsCount = root.ItemsCount;
            for(int i = 0; i < itemsCount; i++) {
                JsonHelperToken[] item = root.Items[i].Properties;
                var binfo = account.GetOrCreateBalanceInfo(item[1].Value);
                binfo.DepositAddress = item[3].Value;
                binfo.DepositTag = item[5].Value;
            }
            return true;
        }

        public override bool GetDeposit(AccountInfo account, CurrencyInfo currency) {
            string address = "https://api.bittrex.com/v3/deposits/open?currencySymbol=" + currency.Currency;
            try {
                return OnGetDeposites(account, DownloadPrivateData(address, account));
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }

        public override bool ObtainExchangeSettings() { return true; }

        protected override SocketConnectionInfo CreateTickersWebSocket() {
            return new SocketConnectionInfo(this, null, BaseWebSocketAdress, SocketType.Signal, SocketSubscribeType.Tickers);
        }

        public override void StartListenTickersStream() {
            base.StartListenTickersStream();
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.Tickers, null, "market_summaries", "marketSummaries") { 
                OnMessage = OnMessageReceived,
                AfterConnect = () => { GetTickersInfo(); } }
            );
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.Tickers, null, "tickers", "tickers") {
                OnMessage = OnTickersMessageReceived,
                AfterConnect = () => {  } }
            );
        }

        protected override void StartListenTradeHistoryCore(Ticker ticker) {
            if(TickersSocket == null) {
                StartListenTickersStream();
                if(!WaitUntil(5000, () => { return TickersSocketState == SocketConnectionState.Connected; }))
                    return;
            }
            string channel = string.Format("trade_{0}", ticker.MarketName);
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.TradeHistory, ticker, channel, "trade") {
                OnMessage = OnTadeHistoryMessageReceived,
                AfterConnect = () => {
                    UpdateTrades(ticker);
                }
            }) ;
        }

        protected override void StartListenKlineCore(Ticker ticker) {
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.OrderBook, ticker, string.Format("candle_{0}_{1}", ticker.MarketName, ticker.GetCandleStickCommandName()), "candle") {
                OnMessage = OnKlineMessageReceived,
                AfterConnect = () => {
                }
            });
        }

        protected override void StartListenOrderBookCore(Ticker ticker) {
            if(TickersSocket == null) {
                StartListenTickersStream();
                if(!WaitUntil(5000, () => { return TickersSocketState == SocketConnectionState.Connected; }))
                    return;
            }
            string marketSymbol = ticker.MarketName;
            int depth = 500;
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.OrderBook, ticker, string.Format("orderbook_{0}_{1}", ticker.MarketName, depth), "orderBook") {
                OnMessage = OnOrderBookMessageReceived,
                AfterConnect = () => {
                    UpdateOrderBook(ticker, depth);
                }
            });
        }

        protected override bool IsListeningOrderBook(Ticker ticker) {
            return ((BittrexTicker)ticker).IsListeningOrderBook;
        }

        public override void StopListenOrderBook(Ticker ticker, bool force) {
            base.StopListenOrderBook(ticker, force);
            if(TickersSocket == null)
                return;
            string marketSymbol = ticker.MarketName;
            int depth = 500;
            TickersSocket.Unsubscribe(new WebSocketSubscribeInfo(SocketSubscribeType.OrderBook, ticker, string.Format("orderbook_{0}_{1}", ticker.MarketName, depth), "orderBook"));
        }

        public override void StopListenTradeHistory(Ticker ticker, bool force) {
            base.StopListenTradeHistory(ticker, force);
            if(TickersSocket == null)
                return;
            string channel = string.Format("trade_{0}", ticker.MarketName);
            TickersSocket.Unsubscribe(new WebSocketSubscribeInfo(SocketSubscribeType.TradeHistory, ticker, channel, "trade"));
        }

        public override void StopListenKline(Ticker ticker, bool force) {
            base.StopListenKline(ticker, force);
            if(TickersSocket == null)
                return;
            TickersSocket.Unsubscribe(new WebSocketSubscribeInfo(SocketSubscribeType.Kline, ticker, string.Format("candle_{0}_{1}", ticker.MarketName, ticker.GetCandleStickCommandName()), "candle"));
        }

        public override void StopListenTickerStream(Ticker ticker) {
            ((BittrexTicker)ticker).IsOrderBookSubscribed = false;
            base.StopListenTickerStream(ticker);
        }
        
        protected virtual void OnTickersMessageReceived(string message, object data) {
            LastWebSocketRecvTime = DateTime.Now;
            byte[] bytes = SignalWebSocket.DecodeBytes((string)data);

            JsonHelperToken root = JsonHelper.Default.Deserialize(bytes);
            int itemsCount = root.Properties[1].ItemsCount;
            for(int i = 0; i < itemsCount; i++) {
                JsonHelperToken t = root.Properties[1].Items[i];
                Ticker ticker = Ticker(t.Properties[0].Value);
                if(ticker == null)
                    continue;
                ticker.LastString = t.Properties[1].Value;
                ticker.HighestBidString = t.Properties[2].Value;
                ticker.LowestAskString = t.Properties[3].Value;
                RaiseTickerChanged(ticker);
            }
        }

        protected virtual void OnKlineMessageReceived(string message, object data) {
            LastWebSocketRecvTime = DateTime.Now;
            byte[] bytes = SignalWebSocket.DecodeBytes((string)data);
            JsonHelperToken root = JsonHelper.Default.Deserialize(bytes);
            Ticker t = Ticker(root.Properties[2].Value);
            if(t == null)
                return;

            var delta = root.GetProperty("delta");
            if(delta == null)
                return;
            var startsAt = delta.GetProperty("startsAt");
            if(startsAt == null)
                return;
            DateTime dt = Convert.ToDateTime(startsAt.Value).ToLocalTime();
            CandleStickData cdata = t.CandleStickData.FirstOrDefault(c => c.Time == dt);
            if(cdata == null) {
                cdata = new CandleStickData();
                t.CandleStickData.Add(cdata);
            }
            cdata.Open = delta.GetProperty("open").ValueDouble;
            cdata.Close = delta.GetProperty("close").ValueDouble;
            cdata.High = delta.GetProperty("high").ValueDouble;
            cdata.Low = delta.GetProperty("low").ValueDouble;
            cdata.Volume = delta.GetProperty("volume").ValueDouble;
            cdata.QuoteVolume = delta.GetProperty("quoteVolume").ValueDouble;
        }

        protected virtual void OnTadeHistoryMessageReceived(string message, object data) {
            LastWebSocketRecvTime = DateTime.Now;
            byte[] bytes = SignalWebSocket.DecodeBytes((string)data);

            JsonHelperToken root = JsonHelper.Default.Deserialize(bytes);
            Ticker t = Ticker(root.Properties[2].Value);
            if(t == null)
                return;
            int itemsCount = root.Properties[0].ItemsCount;
            JsonHelperToken[] items = root.Properties[0].Items;
            for(int i = 0; i < itemsCount; i++) {
                TradeInfoItem item = new TradeInfoItem();
                item.IdString = items[i].Properties[0].Value;
                item.TimeString = items[i].Properties[1].Value;
                item.Time = Convert.ToDateTime(item.TimeString).ToLocalTime();
                item.AmountString = items[i].Properties[2].Value;
                item.RateString = items[i].Properties[3].Value;
                item.Type = items[i].Properties[4].Value[0] == 'B' ? TradeType.Buy : TradeType.Sell;
                t.AddTradeHistoryItem(item);
            }
        }

        protected virtual void OnOrderBookMessageReceived(string message, object data) {
            LastWebSocketRecvTime = DateTime.Now;
            byte[] bytes = SignalWebSocket.DecodeBytes((string)data);

            JsonHelperToken root = JsonHelper.Default.Deserialize(bytes);
            Ticker t = Ticker(root.Properties[0].Value);
            if(t == null)
                return;

            int sequence = root.Properties[2].ValueInt;

            int itemsCount = root.Properties[3].ItemsCount;
            List<string[]> bids = new List<string[]>(itemsCount);
            JsonHelperToken[] jitems = root.Properties[3].Items;
            for(int i = 0; i < itemsCount; i++) {
                string[] item = new string[2];
                
                JsonHelperToken jitem = jitems[i];
                item[0] = jitem.Properties[1].Value;
                item[1] = jitem.Properties[0].Value;
                bids.Add(item);
            }

            itemsCount = root.Properties[4].ItemsCount;
            List<string[]> asks = new List<string[]>(itemsCount);
            jitems = root.Properties[4].Items;
            for(int i = 0; i < itemsCount; i++) {
                string[] item = new string[2];

                JsonHelperToken jitem = jitems[i];
                item[0] = jitem.Properties[1].Value;
                item[1] = jitem.Properties[0].Value;
                asks.Add(item);
            }

            IncrementalUpdateInfo info = new IncrementalUpdateInfo();
            info.Fill(sequence, t, bids, asks, null);

            var provider = CreateIncrementalUpdateDataProvider();
            provider.Update(t, info);
        }

        protected virtual void OnMessageReceived(string message, object data) {
            LastWebSocketRecvTime = DateTime.Now;
            byte[] bytes = SignalWebSocket.DecodeBytes((string)data);
            
            JsonHelperToken root = JsonHelper.Default.Deserialize(bytes);
            int itemsCount = root.Properties[1].ItemsCount;
            for(int i = 0; i < itemsCount; i++) {
                JsonHelperToken t = root.Properties[1].Items[i];
                Ticker ticker = Ticker(t.Properties[0].Value);
                if(ticker == null)
                    continue;
                ticker.Hr24HighString = t.Properties[1].Value;
                ticker.Hr24LowString = t.Properties[2].Value;
                ticker.BaseVolumeString = t.Properties[4].Value;
                ticker.VolumeString = t.Properties[3].Value;
                //ticker.LastUpdateTime = 
            }
            
            //if(command == SignalSocketCommand.QueryExchangeState) {
            //    OnSnapshotRecv(marketName, SignalWebSocket.Decode(s));
            //}
            //else if(command == SignalSocketCommand.IncrementalUpdate) {
            //    byte[] bytes = SignalWebSocket.DecodeBytes(s);
            //    string ss = SignalWebSocket.Decode(s);
            //    OnIncrementalUpdateRecv(bytes);
            //    //List<string[]> items = JSonHelper.Default.DeserializeArrayOfArrays(Encoding.Default.GetBytes(s), , 6);
            //    //Updates.Push(seqNumber, ticker, items);
            //}
        }

        protected virtual void UpdateExchangeState(SignalSocketCommand command, string marketName, string s) {
            //string decoded = BittrexWebSocket.Decode(s);
            LastWebSocketRecvTime = DateTime.Now;
            if(command == SignalSocketCommand.QueryExchangeState) {
                OnSnapshotRecv(marketName, SignalWebSocket.Decode(s));
            }
            else if(command == SignalSocketCommand.IncrementalUpdate) {
                byte[] bytes = SignalWebSocket.DecodeBytes(s);
                string ss = SignalWebSocket.Decode(s);
                OnIncrementalUpdateRecv(bytes);
                //List<string[]> items = JSonHelper.Default.DeserializeArrayOfArrays(Encoding.Default.GetBytes(s), , 6);
                //Updates.Push(seqNumber, ticker, items);
            }
            
        }

        string[] incrementalUpdateStartItems;
        protected string[] IncrementalUpdateStartItems {
            get {
                if(incrementalUpdateStartItems == null)
                    incrementalUpdateStartItems = new string[] { "M", "N" };
                return incrementalUpdateStartItems;
            }
        }

        string[] orderBookEntryItems;
        protected string[] OrderBookEntryItems {
            get {
                if(orderBookEntryItems == null)
                    orderBookEntryItems = new string[] { "TY", "R", "Q" };
                return orderBookEntryItems;
            }
        }

        string[] tradesItems;
        protected string[] TradesItems {
            get {
                if(tradesItems == null)
                    tradesItems = new string[] { "FI", "OT", "R", "Q", "T" };
                return tradesItems;
            }
        }

        void OnIncrementalUpdateRecv(byte[] bytes) {
            int startIndex = 0;
            string[] startItems = JsonHelper.Default.StartDeserializeObject(bytes, ref startIndex, IncrementalUpdateStartItems);
            int st = 0;
            long seqNumber = FastValueConverter.ConvertPositiveLong(startItems[1], ref st);
            Ticker ticker = null;
            for(int i = 0; i < Tickers.Count; i++) {
                Ticker t = Tickers[i];
                if(t.Name == startItems[0]) {
                    ticker = t;
                    break;
                }
            }
            List<string[]> bids = null, asks = null, trades = null;
            startIndex += 2; // skip ,"

            if(bytes[startIndex] == 'Z') {
                startIndex += 3; // ski[ Z":
                bids = JsonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, OrderBookEntryItems);
            }
            startIndex+=2; // skip ,"
            if(bytes[startIndex] == 'S') {
                startIndex += 3; // ski[ S":
                asks = JsonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, OrderBookEntryItems);
            }
            startIndex+=2; // skip ,"
            if(bytes[startIndex] == 'f') {
                startIndex += 3; // ski[ f":
                trades = JsonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, TradesItems);
            }
            lock(ticker.Updates) {
                ticker.Updates.Push(seqNumber, ticker, bids, asks, trades);
                OnIncrementalUpdateRecv(ticker.Updates);
            }
        }
        void OnSnapshotRecv(string marketName, string s) {
            Ticker ticker = Tickers.FirstOrDefault(t => t.Name == marketName);
            if(ticker == null)
                throw new DllNotFoundException("Ticker not found " + ticker.Name);
            ticker.OrderBook.Clear();
            IIncrementalUpdateDataProvider provider = CreateIncrementalUpdateDataProvider();
            JObject obj = JsonConvert.DeserializeObject<JObject>(s);
            long seqNumber = FastValueConverter.ConvertPositiveInteger(obj.Value<string>("N"));
            lock(ticker.Updates) {
                ticker.Updates.Clear(seqNumber + 1);
                provider.ApplySnapshot(obj, ticker);
            }
        }

        protected virtual void UpdateOrderState(string s) {
            LastWebSocketRecvTime = DateTime.Now;
        }

        protected virtual void UpdateBalancesState(string s) {
            LastWebSocketRecvTime = DateTime.Now;
        }
        
        public override bool AllowCandleStickIncrementalUpdate => false;

        public override List<CandleStickIntervalInfo> GetAllowedCandleStickIntervals() {
            List<CandleStickIntervalInfo> list = new List<CandleStickIntervalInfo>();
            
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(1), Text = "1 Minute", Command = "MINUTE_1" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(5), Text = "5 Minutes", Command = "MINUTE_5" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(60), Text = "1 Hour", Command = "HOUR_1" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(24 * 60), Text = "1 Day", Command = "DAY_1" });
            
            return list;
        }

        protected List<TradeInfoItem> UpdateList { get; } = new List<TradeInfoItem>(100);

        string GetInvervalCommand(int minutes) {
            var info = AllowedCandleStickIntervals.FirstOrDefault(i => (int)(i.Interval.TotalMinutes) == minutes);
            if(info == null)
                return null;
            return info.Command;
        }

        public override bool SupportCandleSticksRange { get { return false; } }

        public override ResizeableArray<CandleStickData> GetRecentCandleStickData(Ticker ticker, int candleMin) {
            string command = GetInvervalCommand(candleMin);
            if(string.IsNullOrEmpty(command))
                return null;

            string address = string.Format("https://api.bittrex.com/v3/markets/{0}/candles/TRADE/{1}/recent", ticker.MarketName, command);
            return GetCandleStickData(ticker, address);
        }

        public override ResizeableArray<CandleStickData> GetCandleStickData(Ticker ticker, int candleMin, DateTime start, long periodInSeconds) {
            string command = GetInvervalCommand(candleMin);
            if(string.IsNullOrEmpty(command))
                return null;

            string address = string.Format("https://api.bittrex.com/v3/markets/{0}/candles/TRADE/{1}/historical/{2}/{3}/{4}", ticker.MarketName, command, start.Year, start.Month, start.Day);
            return GetCandleStickData(ticker, address);           
        }

        protected virtual ResizeableArray<CandleStickData> GetCandleStickData(Ticker ticker, string address) {
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
            if(root.Type == JsonObjectType.Object && root.Properties.Length > 0 && root.Properties[0].Name == "code") {
                LogManager.Default.Add(LogType.Error, this, Type.ToString(), "Failed GetCandleStickData", root.Properties[0].Value);
                return null;
            }

            ResizeableArray<CandleStickData> list = new ResizeableArray<CandleStickData>();
            for(int i = 0; i < root.ItemsCount; i++) {
                CandleStickData data = new CandleStickData();
                JsonHelperToken[] item = root.Items[i].Properties;
                data.Time = Convert.ToDateTime(item[0].Value).ToLocalTime();
                data.Open = FastValueConverter.Convert(item[1].Value);
                data.High = FastValueConverter.Convert(item[2].Value);
                data.Low = FastValueConverter.Convert(item[3].Value);
                data.Close = FastValueConverter.Convert(item[4].Value);
                data.Volume = FastValueConverter.Convert(item[5].Value);
                data.QuoteVolume = FastValueConverter.Convert(item[6].Value);
                data.BuySellVolume = data.Open < data.Close ? data.Volume : -data.Volume;
                data.WeightedAverage = 0;
                list.Add(data);
            }
            return list;
        }
        

        public override bool GetTickersInfo() {
            if(Tickers.Count > 0)
                return true;
            string address = "https://api.bittrex.com/v3/markets/tickers";
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(address);

                if(bytes == null)
                    return false;

                JsonHelperToken array = JsonHelper.Default.Deserialize(bytes);
                for(int i = 0; i < array.ItemsCount; i++) {
                    var info = array.Items[i];
                    string name = info.Properties[0].Value;
                    BittrexTicker m = (BittrexTicker)GetOrCreateTicker(name);
                                        
                    m.CurrencyPair = name;
                    string[] pairs = m.CurrencyPair.Split('-');
                    if(pairs.Length != 2)
                        continue;
                    m.MarketCurrency = pairs[0];
                    m.BaseCurrency = pairs[1];
                    m.LastString = info.Properties[1].Value;
                    m.HighestBidString = info.Properties[2].Value;
                    m.LowestAskString = info.Properties[3].Value;
                    AddTicker(m);
                }
            }
            catch(Exception) {
                return false;
            }
            IsInitialized = true;
            return true;
        }

        public override CurrencyInfo CreateCurrency(string currency) {
            return new BittrexCurrencyInfo(this, currency);
        }

        public override bool UpdateCurrencies() {
            string address = "https://api.bittrex.com/v3/currencies";
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(address);
            }
            catch(Exception) {
                return false;
            }
            if(bytes == null)
                return false;

            JsonHelperToken root = JsonHelper.Default.Deserialize(bytes);
            int itemsCount = root.ItemsCount;
            for(int i = 0; i < itemsCount; i++) {
                JsonHelperToken obj = root.Items[i];
                string name = obj.Properties[0].Value;
                var c = GetOrCreateCurrency(name);
                c.CurrencyLong = obj.Properties[1].Value;
                c.CoinType = obj.Properties[2].Value;
                c.IsActive = obj.Properties[3].Value != "OFFLINE";
                c.MinConfirmation = obj.Properties[4].ValueDouble;
                c.TxFee = obj.Properties[6].ValueDouble;
                c.BaseAddress = obj.Properties[9].Value;
            }
            return true;
        }
        public bool GetCurrenciesInfo() {
            Currencies.Clear();
            return UpdateCurrencies();
        }

        public override bool UpdateAddresses(AccountInfo account) {
            try {
                string address = "https://api.bittrex.com/v3/addresses";
                return OnUpdateAddresses(account, DownloadPrivateData(address, account));
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }

        protected virtual bool OnUpdateAddresses(AccountInfo account, byte[] data) {
            var root = JsonHelper.Default.Deserialize(data);
            string error = CheckForErrors(root);
            if(error != null) {
                LogManager.Default.Add(LogType.Error, this, Type.ToString(), "Failed " + nameof(OnUpdateAddresses), error);
                return false;
            }
            for(int i = 0; i < root.ItemsCount; i++) {
                var item = root.Items[i];
                BittrexAccountBalanceInfo info = (BittrexAccountBalanceInfo)account.Balances.FirstOrDefault(b => b.Currency == item.Properties[1].Value);
                if(info == null)
                    continue;
                info.DepositAddress = item.Properties[2].Value;
                info.Status = item.GetProperty("status")?.Value;
                info.DepositTag = item.GetProperty("cryptoAddressTag")?.Value;
            }
            return true;
        }

        public override bool UpdateTicker(Ticker ticker) {
            string address = string.Format("https://api.bittrex.com/v3/markets/{0}/summary", ticker.MarketName);
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(address);
            }
            catch(Exception) {
                return false;
            }
            if(bytes == null)
                return false;

            JsonHelperToken root = JsonHelper.Default.Deserialize(bytes);
            if(root.Properties == null)
                return false;

            BittrexTicker bticker = (BittrexTicker)ticker;
            bticker.Hr24HighString = root.Properties[1].Value;
            bticker.Hr24LowString = root.Properties[2].Value;
            bticker.BaseVolumeString = root.Properties[3].Value;
            bticker.VolumeString = root.Properties[4].Value;
            if(root.Properties[5].Name == "updatedAt") {
                bticker.Time = Convert.ToDateTime(root.Properties[5].Value);
            }
            else {
                bticker.Change = root.Properties[5].ValueDouble;
                bticker.Time = Convert.ToDateTime(root.Properties[6].Value);
            }
            bticker.DisplayMarketName = root.Properties[0].Value;

            //int startIndex = 1;
            //if(!JsonHelper.Default.SkipSymbol(bytes, ':', 3, ref startIndex))
            //    return false;

            //string[] res = JsonHelper.Default.DeserializeObject(bytes, ref startIndex, new string[] { "MarketName", "High", "Low", "Volume", "Last", "BaseVolume", "TimeStamp", "Bid", "Ask", "OpenBuyOrders", "OpenSellOrders", "PrevDay", "Created" });
            //if(res == null)
            //    return true;

            //BittrexTicker info = (BittrexTicker)tickerBase;

            //info.Hr24HighString = res[1];
            //info.Hr24LowString = res[2];
            //info.VolumeString = res[3];
            //info.LastString = res[4];
            //info.BaseVolumeString = res[5];
            //info.Time = Convert.ToDateTime(res[6]).ToLocalTime();
            //info.HighestBidString = res[7];
            //info.LowestAskString = res[8];
            //info.OpenBuyOrders = Convert.ToInt32(res[9]);
            //info.OpenSellOrders = Convert.ToInt32(res[10]);
            //info.PrevDay = FastValueConverter.Convert(res[11]);
            //info.Created = Convert.ToDateTime(res[12]).ToLocalTime();
            //info.DisplayMarketName = res[0];
            bticker.UpdateHistoryItem();

            return true;
        }
        public override bool UpdateTickersInfo() {
            string address = "https://api.bittrex.com/v3/markets/summaries";
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(address);
            }
            catch(Exception) {
                return false;
            }
            if(bytes == null)
                return false;

            JsonHelperToken array = JsonHelper.Default.Deserialize(bytes);
            for(int i = 0; i < array.ItemsCount; i++) {
                BittrexTicker m = new BittrexTicker(this);

                /*
                "symbol": "string",
                "high": "number (double)",
                "low": "number (double)",
                "volume": "number (double)",
                "quoteVolume": "number (double)",
                "percentChange": "number (double)", optional
                "updatedAt": "string (date-time)"
                */

                var info = array.Items[i];
                BittrexTicker t = (BittrexTicker)Ticker(info.Properties[0].Value);
                if(t == null)
                    continue;

                t.Hr24HighString = info.Properties[1].Value;
                t.Hr24LowString = info.Properties[2].Value;
                t.BaseVolumeString = info.Properties[4].Value;
                t.VolumeString = info.Properties[3].Value;
                if(info.Properties[5].Name == "percentChange")
                    t.Change = info.Properties[5].ValueDouble;
            }
            return true;
        }
        public override bool UpdateOrderBook(Ticker info, int depth) {
            string address = GetOrderBookString(info, depth);
            byte[] data = GetDownloadBytes(address);
            if(data == null)
                return false;
            return UpdateOrderBook(info, data, false, depth);
        }

        public override void UpdateOrderBookAsync(Ticker ticker, int depth, Action<OperationResultEventArgs> onOrderBookUpdated) {
            string address = GetOrderBookString(ticker, depth);
            GetDownloadBytesAsync(address, t => {
                OnUpdateOrderBook((BittrexTicker)ticker, t.Result, depth);
                onOrderBookUpdated(new OperationResultEventArgs() { Ticker = ticker, Result = t.Result != null });
            });
        }
        public string GetOrderBookString(Ticker info, int depth) {
            return string.Format("https://api.bittrex.com/v3/markets/{0}/orderbook?depth={1}", Uri.EscapeDataString(info.MarketName), depth);
        }
        public bool OnUpdateOrderBook(BittrexTicker info, byte[] data, int depth) {
            return UpdateOrderBook(info, data, true, depth);
        }
        public override bool UpdateOrderBook(Ticker tickerBase) {
            return UpdateOrderBook(tickerBase, OrderBook.Depth);
        }
        public bool UpdateOrderBook(Ticker ticker, byte[] bytes, bool raiseChanged, int depth) {
            if(bytes == null)
                return false;

            int startIndex = 0;
            if(!JsonHelper.Default.SkipSymbol(bytes, ':', 1, ref startIndex))
                return false;

            List<string[]> bids = JsonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "quantity", "rate" });
            if(bids == null)
                return false;
            if(!JsonHelper.Default.SkipSymbol(bytes, ':', 1, ref startIndex))
                return false;
            List<string[]> asks = JsonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "quantity", "rate" });
            if(asks == null)
                return true;

            ticker.OrderBook.BeginUpdate();
            try {
                ticker.OrderBook.GetNewBidAsks();
                int index = 0;
                List<OrderBookEntry> list = ticker.OrderBook.Bids;
                for(int i = 0; i < bids.Count; i++) {
                    string[] item = bids[i];
                    OrderBookEntry entry = new OrderBookEntry();
                    list.Add(entry);
                    entry.ValueString = item[1];
                    entry.AmountString = item[0];
                    index++;
                    if(index >= depth)
                        break;
                }
                index = 0;
                list = ticker.OrderBook.Asks;
                for(int i = 0; i < asks.Count; i++) {
                    string[] item = asks[i];
                    OrderBookEntry entry = new OrderBookEntry();
                    list.Add(entry);
                    entry.ValueString = item[1];
                    entry.AmountString = item[0];
                    index++;
                    if(index >= depth)
                        break;
                }
            }
            finally {
                ticker.OrderBook.IsDirty = false;
                ticker.OrderBook.EndUpdate();
            }
            return true;
        }
        public void GetOrderBook(BittrexTicker info, int depth) {
            string address = string.Format("https://api.bittrex.com/v3/markets/{0}/orderbook?depth={1}", Uri.EscapeDataString(info.MarketName), depth);
            byte[] data = GetDownloadBytes(address);
            if(data == null)
                return;
            OnUpdateOrderBook(info, data, depth);
        }
        
        protected byte[] DownloadPrivateData(string address, AccountInfo account) {
            MyWebClient client = GetWebClient();

            string timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            string contentHash = HashContent("");

            client.Headers.Clear();
            client.Headers.Add("Api-Key", account.ApiKey);
            client.Headers.Add("Api-Timestamp", timestamp);
            client.Headers.Add("Api-Content-Hash", contentHash);

            string sign = timestamp + address + HttpMethod.Get + contentHash;
            client.Headers.Add("Api-Signature", account.GetSign(sign));
            return client.DownloadData(address);
        }

        protected byte[] UploadPrivateData(string address, AccountInfo account, string content) {
            return UploadPrivateData(address, account, content, HttpMethod.Post);
        }

        protected byte[] UploadPrivateData(string address, AccountInfo account, string content, HttpMethod method) {
            MyWebClient client = GetWebClient();

            string timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            string contentHash = HashContent(content);

            client.Headers.Clear();
            client.Headers.Add("Api-Key", account.ApiKey);
            client.Headers.Add("Api-Timestamp", timestamp);
            client.Headers.Add("Api-Content-Hash", contentHash);
            //client.Headers.Add("Content-Type", "application/json");

            HttpRequestMessage request = new HttpRequestMessage(method, address);
            request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            string sign = timestamp + address + method + contentHash;
            client.Headers.Add("Api-Signature", account.GetSign(sign));
            
            Task<HttpResponseMessage> t = client.SendAsync(request);
            t.Wait();
            var rest = t.Result.Content.ReadAsByteArrayAsync();
            rest.Wait();
            return rest.Result;
        }

        SHA512 sha512;
        private string HashContent(string str) {
            if(this.sha512 == null)
                this.sha512 = SHA512.Create();
            byte[] bytes = this.sha512.ComputeHash(Encoding.UTF8.GetBytes(str));
            return BytesToHexString(bytes);
        }

        protected static string BytesToHexString(byte[] buff) {
            var result = string.Empty;
            foreach(var t in buff)
                result += t.ToString("X2");
            return result;
        }

        public override bool UpdateAccountTrades(AccountInfo account, Ticker ticker) {
            string address = string.Empty;
            if(ticker != null) {
                address = string.Format("https://api.bittrex.com/v3/orders/closed?marketSymbol={0}", ticker.MarketName);
            }
            else {
                address = "https://api.bittrex.com/v3/orders/closed";
            }
            try {
                return OnGetAccountTrades(account, ticker, DownloadPrivateData(address, account));
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }
        protected virtual bool OnGetAccountTrades(AccountInfo account, Ticker ticker, byte[] bytes) {
            if(bytes == null) {
                LogManager.Default.Error(this, nameof(OnGetAccountTrades), "No data received.");
                return false;
            }

            JsonHelperToken root = JsonHelper.Default.Deserialize(bytes);
            if(root.PropertiesCount > 0) {
                string code = root.Properties.FirstOrDefault(p => p.Name == "code")?.Value;
                string detail = root.Properties.FirstOrDefault(p => p.Name == "detail")?.Value;
                string data = root.Properties.FirstOrDefault(p => p.Name == "data")?.Value;
                LogManager.Default.Add(LogType.Error, this, Type.ToString(), "Failed " + nameof(OnGetAccountTrades), "code = " + code + ", detail = " + detail + ", data = " + data);
                return false;
            }

            ticker.LockAccountTrades();
            try {
                ticker.ClearMyTradeHistory();
                int itemsCount = root.ItemsCount;
                for(int i = 0; i < itemsCount; i++) {
                    JsonHelperToken[] item = root.Items[i].Properties;
                    Ticker t = ticker == null? Ticker(item[1].Value) : ticker;
                    
                    TradeInfoItem ti = new TradeInfoItem(account, t);
                    ti.IdString = item[0].Value;
                    ti.Type = item[2].Value[0] == 'B' ? TradeType.Buy : TradeType.Sell;
                    ti.AmountString = item[4].Value;
                    ti.RateString = item[5].Value;
                    ti.FeeString = item[10].Value;
                    ti.Total = ti.Rate * ti.Amount;
                    ti.TimeString = item[13].Value;
                    ticker.AddAccountTradeHistoryItem(ti);
                }
            }
            finally {
                ticker.UnlockAccountTrades();
            }
            return true;
        }
        
        public override bool UpdateTrades(Ticker ticker) {
            string address = string.Format("https://api.bittrex.com/v3/markets/{0}/trades", Uri.EscapeDataString(ticker.MarketName));
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(address);
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
            if(bytes == null) {
                LogManager.Default.Error(this, nameof(UpdateTrades), "No data received");
                return false;
            }

            JsonHelperToken root = JsonHelper.Default.Deserialize(bytes);
            lock(ticker) {
                ticker.LockTrades();
                ticker.ClearTradeHistory();
                for(int i = root.ItemsCount - 1; i >= 0; i--) {
                    JsonHelperToken obj = root.Items[i];
                    TradeInfoItem item = new TradeInfoItem(null, ticker);
                    item.IdString = obj.Properties[0].Value;
                    item.TimeString = obj.Properties[1].Value;
                    item.AmountString = obj.Properties[2].Value;
                    item.RateString = obj.Properties[3].Value;
                    item.Total = item.Rate * item.Amount;
                    item.Type = obj.Properties[4].Value == "BUY" ? TradeType.Buy : TradeType.Sell;
                    item.Fill = TradeFillType.Fill;
                    ticker.AddTradeHistoryItem(item);
                }
                ticker.UnlockTrades();
            }

            if(ticker.HasTradeHistorySubscribers) {
                ticker.RaiseTradeHistoryChanged(new TradeHistoryChangedEventArgs() { NewItems = ticker.TradeHistory });
            }
            return true;
        }

        protected override bool HasDescendingTradesList => true;

        public override TradingResult BuyLong(AccountInfo account, Ticker ticker, double rate, double amount) {
            string format = "{{ " +
                "\"marketSymbol\": \"{0}\", " +
                "\"direction\": \"BUY\", " +
                "\"type\": \"LIMIT\"," +
                "\"quantity\": \"{1}\"," +
                "\"limit\": \"{2}\"," +
                "\"timeInForce\": \"GOOD_TIL_CANCELLED\"" +
                "}}";
            string body = string.Format(format, ticker.MarketName, amount.ToString("0.00000000"), rate.ToString("0.00000000"));
            try { 
                byte[] bytes = UploadPrivateData("https://api.bittrex.com/v3/orders", account, body);
                return OnBuy(account, ticker, bytes);
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return null;
            }
        }
        public override TradingResult SellLong(AccountInfo account, Ticker ticker, double rate, double amount) {
            string format = "{{ " +
                "\"marketSymbol\": \"{0}\", " +
                "\"direction\": \"SELL\", " +
                "\"type\": \"LIMIT\"," +
                "\"quantity\": \"{1}\"," +
                "\"limit\": \"{2}\"," +
                "\"timeInForce\": \"GOOD_TIL_CANCELLED\"" +
                "}}";
            string body = string.Format(format, ticker.MarketName, amount.ToString("0.00000000"), rate.ToString("0.00000000"));
            try {
                byte[] bytes = UploadPrivateData("https://api.bittrex.com/v3/orders", account, body);
                return OnSell(account, ticker, bytes);
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
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
            
            string address = string.Format("https://api.bittrex.com/v3/orders/{0}", orderId);
            try {
                byte[] bytes = UploadPrivateData(address, account, "", HttpMethod.Delete);
                return OnCancel(account, ticker, bytes);
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }
        
        public override bool UpdateOpenedOrders(AccountInfo account, Ticker ticker) {
            string address = string.Empty;
            if(ticker != null) {
                address = string.Format("https://api.bittrex.com/v3/orders/open?marketSymbol={0}", ticker.MarketName);
            }
            else {
                address = "https://api.bittrex.com/v3/orders/open";
            }
            try {
                byte[] bytes = DownloadPrivateData(address, account);
                if(!ticker.IsOpenedOrdersChanged(bytes))
                    return true;
                ticker.OpenedOrdersData = bytes;
                return OnUpdateOrders(account, ticker, bytes);
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }
        protected string OnUuidResult(string result) {
            if(string.IsNullOrEmpty(result))
                return null;
            JObject res = JsonConvert.DeserializeObject<JObject>(result);
            foreach(JProperty prop in res.Children()) {
                if(prop.Name == "success") {
                    if(prop.Value.Value<bool>() == false)
                        return null;
                }
                if(prop.Name == "message")
                    continue;
                if(prop.Name == "result") {
                    JObject obj = (JObject)prop.Value;
                    return obj.Value<string>("uuid");
                }
            }
            return null;
        }
        public TradingResult OnBuy(AccountInfo account, Ticker ticker, byte[] bytes) {
            JsonHelperToken root = JsonHelper.Default.Deserialize(bytes);
            string error = CheckForErrors(root);
            if(error != null) {
                LogManager.Default.Add(LogType.Error, this, Type.ToString(), "Failed " + nameof(OnGetAccountTrades), error);
                return null;
            }

            TradingResult res = new TradingResult();
            res.Ticker = ticker;
            res.Type = root.GetProperty("direction").Value[0] == 'B' ? OrderType.Buy : OrderType.Sell;
            res.Amount = root.GetProperty("quantity").ValueDouble;
            res.Value = root.GetProperty("limit").ValueDouble;
            res.OrderId = root.GetProperty("").Value;

            return res;
        }
        public TradingResult OnSell(AccountInfo account, Ticker ticker, byte[] bytes) {
            JsonHelperToken root = JsonHelper.Default.Deserialize(bytes);
            string error = CheckForErrors(root);
            if(error != null) {
                LogManager.Default.Add(LogType.Error, this, Type.ToString(), "Failed " + nameof(OnGetAccountTrades), error);
                return null;
            }

            TradingResult res = new TradingResult();
            res.Ticker = ticker;
            res.Type = root.GetProperty("direction").Value[0] == 'B' ? OrderType.Buy : OrderType.Sell;
            res.Amount = root.GetProperty("quantity").ValueDouble;
            res.Value = root.GetProperty("limit").ValueDouble;
            res.OrderId = root.GetProperty("").Value;

            return res;
        }
        public bool OnCancel(AccountInfo account, Ticker ticker, byte[] bytes) {
            JsonHelperToken root = JsonHelper.Default.Deserialize(bytes);
            string error = CheckForErrors(root);
            if(error != null) {
                LogManager.Default.Add(LogType.Error, this, Type.ToString(), "Failed " + nameof(OnCancel), error);
                return false;
            }
            return root.Properties[10].Value == "CLOSED";
        }

        protected string CheckForErrors(JsonHelperToken root) {
            if(root.GetProperty("code") == null)
                return null;
            if(root.PropertiesCount > 0) {
                StringBuilder b = new StringBuilder();
                for(int i = 0; i < root.PropertiesCount; i++) {
                    b.Append(root.Properties[i].Name);
                    b.Append(": ");
                    b.Append(root.Properties[i].Value);
                    b.Append(", ");
                }
                return b.ToString();
            }
            return null;
        }

        public bool OnUpdateOrders(AccountInfo account, Ticker ticker, byte[] bytes) {
            if(bytes == null)
                return false;

            JsonHelperToken root = JsonHelper.Default.Deserialize(bytes);
            string error = CheckForErrors(root);
            if(error != null) {
                LogManager.Default.Add(LogType.Error, this, Type.ToString(), "Failed " + nameof(OnGetAccountTrades), error);
                return false;
            }

            if(ticker != null)
                ticker.SaveOpenedOrders();
            try {
                ticker.OpenedOrders.Clear();
                int itemsCount = root.ItemsCount;
                for(int i = 0; i < itemsCount; i++) {
                    JsonHelperToken[] item = root.Items[i].Properties;
                    Ticker t = ticker == null ? Ticker(item[1].Value) : ticker;

                    BittrexOrderInfo ti = new BittrexOrderInfo(account, t);
                    ti.OrderId = item[0].Value;
                    ti.OrderUuid = item[0].Value;
                    ti.Type = item[2].Value[0] == 'B' ? OrderType.Buy : OrderType.Sell;
                    ti.AmountString = item[4].Value;
                    ti.ValueString = item[5].Value;
                    ti.CommissionPaidString = item[10].Value;
                    ti.TotalString = (ti.Value * ti.Amount).ToString("0.00000000");
                    ti.DateString = item[11].Value;
                    ticker.OpenedOrders.Add(ti);
                }
            }
            finally {
                if(ticker != null)
                    ticker.RaiseOpenedOrdersChanged();
            }
            return true;
        }
        
        public override bool GetBalance(AccountInfo account, string currency) {
            try {
                byte[] bytes = DownloadPrivateData(string.Format("https://api.bittrex.com/v3/balances/{0}", currency), account);

                if(bytes == null)
                    return false;

                JsonHelperToken root = JsonHelper.Default.Deserialize(bytes);
                string error = CheckForErrors(root);
                if(!string.IsNullOrEmpty(error)) {
                    LogManager.Default.Add(LogType.Error, this, Type.ToString(), "Failed GetBalance", error);
                    return false;
                }

                JsonHelperToken[] item = root.Properties;
                var binfo = account.GetOrCreateBalanceInfo(item[0].Value);
                binfo.Balance = item[1].ValueDouble;
                binfo.Available = item[2].ValueDouble;
                binfo.OnOrders = binfo.Balance - binfo.Available;
            }
            catch(Exception) {
                return false;
            }
            return true;
        }
        
        public override bool UpdateBalances(AccountInfo account) {
            if(account == null)
                return false;
            if(Currencies.Count == 0) {
                if(!GetCurrenciesInfo())
                    return false;
                foreach(var currency in Currencies.Values)
                    account.GetOrCreateBalanceInfo(currency.Currency);
            }
            try {
                byte[] bytes = DownloadPrivateData("https://api.bittrex.com/v3/balances", account);

                if(bytes == null)
                    return false;

                JsonHelperToken root = JsonHelper.Default.Deserialize(bytes);
                string error = CheckForErrors(root);
                if(!string.IsNullOrEmpty(error)) {
                    LogManager.Default.Add(LogType.Error, this, Type.ToString(), "Failed UpdateBalances", error);
                    return false;
                }

                int itemsCount = root.ItemsCount;
                for(int i = 0; i < itemsCount; i++) {
                    JsonHelperToken[] item = root.Items[i].Properties;
                    var binfo = account.GetOrCreateBalanceInfo(item[0].Value);
                    binfo.Balance = item[1].ValueDouble;
                    binfo.Available = item[2].ValueDouble;
                    binfo.OnOrders = binfo.Balance - binfo.Available;
                }
            }
            catch(Exception) {
                return false;
            }
            return true;
        }

        public override bool Withdraw(AccountInfo account, string currency, string address, string paymentId, double amount) {
            string addr = string.Empty;
            if(string.IsNullOrEmpty(paymentId)) {
                addr = string.Format("https://bittrex.com/api/v1.1/account/withdraw?apikey={0}&nonce={1}&currency={2}&quantity={3}",
                    Uri.EscapeDataString(account.ApiKey),
                    GetNonce(),
                    Uri.EscapeDataString(currency),
                    amount.ToString("0.00000000"));
            }
            else {
                addr = string.Format("https://bittrex.com/api/v1.1/account/withdraw?apikey={0}&nonce={1}&currency={2}&quantity={3}&paymentid={4}",
                        Uri.EscapeDataString(account.ApiKey),
                        GetNonce(),
                        Uri.EscapeDataString(currency),
                        amount.ToString("0.00000000"),
                        Uri.EscapeDataString(paymentId));
            }
            MyWebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", account.GetSign(address));
            try {
                string text = client.DownloadString(addr);
                string uuid = OnWithdraw(text);
                return !string.IsNullOrEmpty(uuid);
            }
            catch(Exception) {
                return false;
            }
        }
        
        public string OnWithdraw(string result) {
            return OnUuidResult(result);
        }

        public override bool CreateDeposit(AccountInfo account, string currency) {
            try {
                string content = string.Format("{{ \"currencySymbol\": \"{0}\" }}", currency);
                return OnCreateDeposit(account, currency, UploadPrivateData("https://api.bittrex.com/v3/addresses", account, content));
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }
        
        protected virtual bool OnCreateDeposit(AccountInfo account, string currency, byte[] bytes) {
            if(bytes == null)
                return false;

            JsonHelperToken root = JsonHelper.Default.Deserialize(bytes);
            if(root.GetProperty("status") != null) {
                LogManager.Default.ShowNotification(LogType.Success, Type.ToString(), "Create Deposit", root.GetProperty("stauts").Value);
                NotificationManager.Notify(LogType.Success, Type.ToString(), "Create Deposit\n" + root.GetProperty("stauts").Value);
                return true;
            }

            if(root.PropertiesCount > 0 && root.Properties[0].Name == "code") {
                if(root.Properties[0].Value == "CRYPTO_ADDRESS_ALREADY_EXISTS") {
                    LogManager.Default.Add(LogType.Warning, this, Type.ToString(), "Create Deposit", "Crypto deposit address already exists");
                    return true;
                }
                else {
                    string error = CheckForErrors(root);
                    if(!string.IsNullOrEmpty(error)) {
                        LogManager.Default.Add(LogType.Error, this, Type.ToString(), "Failed UpdateBalances", error);
                        return false;
                    }
                }
            }

            return true;
        }

        string GetNonce() {
           return ((long)((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds)).ToString();
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
