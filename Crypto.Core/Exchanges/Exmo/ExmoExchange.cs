using Crypto.Core.Common;
using Crypto.Core.Exchanges.Base;
using Crypto.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebSocket4Net;

namespace Crypto.Core.Exchanges.Exmo {
    public class ExmoExchange : Exchange {
        static ExmoExchange defaultExchange;
        public static ExmoExchange Default {
            get {
                if(defaultExchange == null)
                    defaultExchange = (ExmoExchange)Exchange.FromFile(ExchangeType.EXMO, typeof(ExmoExchange));
                return defaultExchange;
            }
        }

        public override bool AllowCandleStickIncrementalUpdate => false;

        public override ExchangeType Type => ExchangeType.EXMO;

        public override string BaseWebSocketAdress => "wss://ws-api.exmo.com:443/v1/public";

        protected override bool ShouldAddKlineListener => false;

        public override TradingResult BuyLong(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public override TradingResult BuyShort(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public override bool Cancel(AccountInfo account, Ticker ticker, string orderId) {
            throw new NotImplementedException();
        }

        public override BalanceBase CreateAccountBalance(AccountInfo info, string currency) {
            return new ExmoAccountBalanceInfo(info, GetOrCreateCurrency(currency));
        }

        public override bool CreateDeposit(AccountInfo account, string currency) {
            LogManager.Default.Error(this, "CreateDeposit", "The operations is not supported for this exchange");
            return false;
        }

        public override Ticker CreateTicker(string name) {
            return new ExmoTicker(this) { CurrencyPair = name };
        }

        public override int WebSocketAllowedDelayInterval => int.MaxValue;

        public override List<CandleStickIntervalInfo> GetAllowedCandleStickIntervals() {
            List<CandleStickIntervalInfo> list = new List<CandleStickIntervalInfo>();
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(1), Text = "1 Minute", Command = "1" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(5), Text = "5 Minutes", Command = "5" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(15), Text = "15 Minutes", Command = "15" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(30), Text = "30 Minutes", Command = "30" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(45), Text = "45 Minutes", Command = "45" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromHours(1), Text = "1 Hour", Command = "60" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromHours(2), Text = "3 Hours", Command = "120" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromHours(3), Text = "6 Hours", Command = "180" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromHours(4), Text = "12 Hours", Command = "240" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromDays(1), Text = "1 Day", Command = "D" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromDays(7), Text = "1 Week", Command = "W" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromDays(30), Text = "1 Month", Command = "M" });
            return list;
        }

        public override ResizeableArray<CandleStickData> GetCandleStickData(Ticker ticker, int candleStickPeriodMin, DateTime start, long periodInSeconds) {
            string cmd = GetCandleStickCommandName(candleStickPeriodMin);
            long startSec = ToUnixTimestamp(start);
            long endSec = startSec + periodInSeconds;
            string address = string.Format("https://api.exmo.com/v1.1/candles_history?symbol={0}&resolution={1}&from={2}&to={3}", 
                ticker.CurrencyPair, cmd, startSec, endSec);
            try {
                return OnGetCandleStickData(ticker, GetDownloadBytes(address));
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return null;
            }
        }

        protected virtual ResizeableArray<CandleStickData> OnGetCandleStickData(Ticker ticker, byte[] bytes) {
            if(bytes == null || bytes.Length == 0) {
                LogManager.Default.Error(this, nameof(GetCandleStickData), "No data received");
                return null;
            }
            var root = JsonHelper.Default.Deserialize(bytes);

            var items = root.Properties[0];
            int itemsCount = items.ItemsCount;
            ResizeableArray<CandleStickData> list = new ResizeableArray<CandleStickData>(itemsCount);
            for(int i = 0; i < itemsCount; i++) {
                var props = items.Items[i].Properties;
                CandleStickData data = new CandleStickData();
                data.Time = new DateTime(TimeSpan.TicksPerMillisecond * props[0].ValueLong).ToLocalTime();
                data.Open = props[1].ValueDouble;
                data.Close = props[2].ValueDouble;
                data.High = props[3].ValueDouble;
                data.Low = props[4].ValueDouble;
                data.Volume = props[5].ValueDouble;
                list.Add(data);
            }
            return list;
        }

        public override ResizeableArray<CandleStickData> GetRecentCandleStickData(Ticker ticker, int candleStickPeriodMin) {
            return GetCandleStickData(ticker, candleStickPeriodMin, DateTime.Now.AddMinutes(-candleStickPeriodMin * 300), candleStickPeriodMin * 300 * 60);
        }

        public override bool GetBalance(AccountInfo info, string currency) {
            return true;
        }

        public override bool GetDeposit(AccountInfo account, CurrencyInfo currency) {
            return GetDeposites(account);
        }

        protected virtual bool OnGetDepositAdresses(AccountInfo account, byte[] data) {
            if(data == null) {
                LogManager.Default.Error(this, nameof(OnGetDepositAdresses), "No data received");
                return false;
            }

            var root = JsonHelper.Default.Deserialize(data);
            if(HasError(account, root, nameof(OnGetBalances), data))
                return false;
            JsonHelperToken[] balances = root.Properties;
            if(balances == null)
                return true;
            foreach(JsonHelperToken item in balances) {
                var b = account.GetOrCreateBalanceInfo(item.Name);
                string address = item.Value;
                string tag = string.Empty;
                if(address.Contains(',')) {
                    var items = address.Split(',');
                    address = items[0];
                    tag = items[1];
                }
                b.DepositAddress = address;
                b.DepositTag = tag;
            }
            return true;
        }

        public override bool GetDeposites(AccountInfo account) {
            const string address = "https://api.exmo.com/v1.1/deposit_address";
            try {
                return OnGetDepositAdresses(account, UploadPrivateData(account, address, ""));
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }

        public override bool GetTickersInfo() {
            if(Tickers.Count > 0)
                return true;
            string address = "https://api.exmo.com/v1.1/ticker";
            byte[] bytes = null;
            try {
                bytes = UploadValues(address, new HttpRequestParamsCollection());

                if(bytes == null) {
                    LogManager.Default.Error(this, nameof(GetTickersInfo), "No data received.");
                    return false;
                }

                JsonHelperToken array = JsonHelper.Default.Deserialize(bytes);
                for(int i = 0; i < array.Properties.Length; i++) {
                    var info = array.Properties[i];
                    string name = info.Name;
                    ExmoTicker m = (ExmoTicker)GetOrCreateTicker(name);

                    m.CurrencyPair = name;
                    string[] pairs = m.CurrencyPair.Split('_');
                    if(pairs.Length != 2)
                        continue;
                    m.MarketCurrency = pairs[0];
                    m.BaseCurrency = pairs[1];
                    m.LastString = info.Properties[2].Value;
                    m.HighestBidString = info.Properties[0].Value;
                    m.LowestAskString = info.Properties[1].Value;
                    m.Hr24HighString = info.Properties[3].Value;
                    m.Hr24LowString = info.Properties[4].Value;
                    m.VolumeString = info.Properties[6].Value;
                    m.BaseVolumeString = info.Properties[7].Value;
                    AddTicker(m);
                }
            }
            catch(Exception) {
                return false;
            }
            IsInitialized = true;
            return true;
        }

        public override bool ObtainExchangeSettings() {
            return true;
        }

        public override TradingResult SellLong(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public override TradingResult SellShort(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public override bool SupportWebSocket(WebSocketType type) {
            //if(type == WebSocketType.Tickers)
            //    return true;
            if(type == WebSocketType.Ticker)
                return true;
            if(type == WebSocketType.OrderBook)
                return true;
            if(type == WebSocketType.Trades)
                return true;
            return false;
        }

        public override bool UpdateAccountTrades(AccountInfo account, Ticker ticker) {
            const string address = "https://api.exmo.com/v1.1/user_trades";
            try {
                return OnGetAccountTrades(account, ticker, UploadPrivateData(account, address, string.Format("pair={0}", ticker.Name)));
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }

        protected virtual bool OnGetAccountTrades(AccountInfo account, Ticker ticker, byte[] data) {
            JsonHelperToken root = JsonHelper.Default.Deserialize(data);
            if(HasError(account, root, nameof(OnGetAccountTrades), data))
                return false;
            try {
                account.MyTrades.Clear();
                for(int c = 0; c < root.PropertiesCount; c++) {
                    var ticker_trades = root.Properties[c];
                    if(ticker_trades.Name != ticker.Name)
                        continue;
                    for(int i = 0; i < ticker_trades.ItemsCount; i++) {
                        var order = ticker_trades.Items[i];

                        TradeInfoItem info = new TradeInfoItem(account, ticker);

                        info.IdString = order.GetProperty("trade_id").Value;
                        info.Time = ExmoTime(order.GetProperty("date"));
                        info.RateString = order.GetProperty("price").Value;
                        info.AmountString = order.GetProperty("quantity").Value;
                        info.FeeString = order.GetProperty("commission_amount").Value;

                        info.Type = order.GetProperty("type").Value.EndsWith("buy") ? TradeType.Buy : TradeType.Sell;

                        account.MyTrades.Add(info);
                    }
                }
            }
            finally {
                if(ticker != null)
                    ticker.RaiseAccountTradeHistoryChanged(new TradeHistoryChangedEventArgs() { NewItems = account.MyTrades });
            }
            return true;
        }

        protected string GetNonce() {
            long timestamp = (long)((DateTime.UtcNow - epoch).TotalMilliseconds * 1000);
            return timestamp.ToString();
        }

        protected virtual byte[] UploadPrivateData(AccountInfo info, string address, string formString) {
            MyWebClient client = GetWebClient();

            string nonce = GetNonce();
            string queryString = null;
            if(formString == null)
                formString = "";
            if(string.IsNullOrEmpty(formString))
                queryString = string.Format("nonce={0}", nonce);
            else
                queryString = string.Format("nonce={0}&{1}", nonce, formString);
            string signature = info.GetSign("", queryString, nonce);

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Key", info.ApiKey);
            client.DefaultRequestHeaders.Add("Sign", signature);

            StringContent data = new StringContent(queryString, Encoding.UTF8, "application/x-www-form-urlencoded");
            Task<HttpResponseMessage> response = client.PostAsync(address, data);
            response.Wait(10000);
            Task<byte[]> bytes = response.Result.Content.ReadAsByteArrayAsync();
            bytes.Wait(10000);
            return bytes.Result;
        }

        public override bool UpdateBalances(AccountInfo account) {
            if(account == null)
                return false;
            const string address = "https://api.exmo.com/v1.1/user_info";
            try {
                return OnGetBalances(account, UploadPrivateData(account, address, ""));
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }

        protected virtual bool HasError(AccountInfo account, JsonHelperToken root, string methodName, byte[] data) {
            var res = root.GetProperty("result");
            if(res == null || res.ValueBool == true)
                return false;
            string accountName = account != null ? account.Name : "any account";
            LogManager.Default.Error(this, methodName, "account: " + account + ", message = " + root.GetProperty("error")?.Value);
            return true;
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
            JsonHelperToken[] balances = root.GetProperty("balances").Properties;
            if(balances == null)
                return true;
            foreach(JsonHelperToken item in balances) {
                var b = account.GetOrCreateBalanceInfo(item.Name);
                b.OnOrders = 0;
                b.Balance = item.ValueDouble;
                b.Available = b.Balance;
            }
            return true;
        }

        public override bool UpdateCurrencies() {
            return true;
        }

        public override bool UpdateOpenedOrders(AccountInfo account, Ticker ticker) {
            const string address = "https://api.exmo.com/v1.1/user_open_orders";
            try {
                return OnGetOpenedOrders(account, ticker, UploadPrivateData(account, address, ""));
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }

        protected DateTime ExmoTime(JsonHelperToken item) {
            return epoch.AddSeconds(item.ValueLong).ToLocalTime();
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
                for(int c = 0; c < root.PropertiesCount; c++) {
                    var ticker_orders = root.Properties[c];
                    for(int i = 0; i < ticker_orders.ItemsCount; i++) {
                        var order = ticker_orders.Items[i];

                        OpenedOrderInfo info = new OpenedOrderInfo(account, ticker);

                        info.OrderId = order.GetProperty("client_id").Value;
                        info.Date = ExmoTime(order.GetProperty("created"));
                        info.ValueString = order.GetProperty("trigger_price").Value;
                        info.AmountString = order.GetProperty("quantity").Value;

                        info.Type = order.GetProperty("type").Value.EndsWith("buy") ? OrderType.Buy : OrderType.Sell;
                        
                        account.OpenedOrders.Add(info);
                    }
                }
            }
            finally {
                if(ticker != null)
                    ticker.RaiseOpenedOrdersChanged();
            }
            return true;
        }

        public override bool UpdateOrderBook(Ticker ticker) {
            return UpdateOrderBook(ticker, 1000);
        }

        public override bool UpdateOrderBook(Ticker ticker, int depth) {
            string address = string.Format("https://api.exmo.com/v1.1/order_book?pair={0}&limit={1}", ticker.Name, depth);
            try {
                return OnUpdateOrderBook(ticker, GetDownloadBytes(address));
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }

        protected virtual bool OnUpdateOrderBook(Ticker ticker, byte[] data) {
            if(data == null) {
                LogManager.Default.Error(this, nameof(OnUpdateOrderBook), "No data received.");
                return false;
            }

            var root = JsonHelper.Default.Deserialize(data);

            ticker.OrderBook.BeginUpdate();
            try {
                ticker.OrderBook.GetNewBidAsks();

                var b = root.GetProperty("bids");
                var a = root.GetProperty("asks");
                int bc = b.ItemsCount;
                var jb = b.Items;

                int ac = a.ItemsCount;
                var ja = a.Items;

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

        public override void UpdateOrderBookAsync(Ticker ticker, int depth, Action<OperationResultEventArgs> onOrderBookUpdated) {
            string address = string.Format("https://api.exmo.com/v1.1/order_book?pair={0}&limit={1}", ticker.Name, depth);
            GetDownloadBytesAsync(address, t => {
                OnUpdateOrderBook(ticker, t.Result);
                onOrderBookUpdated(new OperationResultEventArgs() { Ticker = ticker, Result = t.Result != null });
            });
        }

        public override bool UpdateTicker(Ticker tickerBase) {
            return UpdateTickersInfo();
        }

        public override bool UpdateTickersInfo() {
            return GetTickersInfo();
        }

        public override bool UpdateTrades(Ticker ticker) {
            string address = "https://api.exmo.com/v1.1/trades";
            try {
                HttpRequestParamsCollection coll = new HttpRequestParamsCollection();
                coll.Add(new KeyValuePair<string, string>("pair", ticker.CurrencyPair));
                return OnUpdateTrades(ticker, UploadValues(address, coll));
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
            if(root.PropertiesCount != 1 || root.Properties[0].Name != ticker.CurrencyPair) {
                LogManager.Default.Error(this, nameof(OnUpdateTrades), "Invalid data received");
                return false;
            }
            int itemsCount = root.Properties[0].ItemsCount;
            ResizeableArray<TradeInfoItem> newItems = new ResizeableArray<TradeInfoItem>(itemsCount);
            var items = root.Properties[0].Items;

            lock(ticker) {
                ticker.LockTrades();
                ticker.ClearTradeHistory();
                for(int i = itemsCount - 1; i >= 0; i--) {
                    var item = items[i];
                    TradeInfoItem ti = new TradeInfoItem(DefaultAccount, ticker);
                    ti.IdString = item.Properties[0].Value;
                    ti.Time = FromUnixTimestamp(item.Properties[1].ValueLong).ToLocalTime();
                    ti.Type = item.Properties[2].Value[0] == 's'? TradeType.Sell: TradeType.Buy;
                    ti.RateString = item.Properties[4].Value;
                    ti.AmountString = item.Properties[3].Value;
                    ticker.AddTradeHistoryItem(ti);
                    newItems.Add(ti);
                }
                ticker.UnlockTrades();
            }

            if(ticker.HasTradeHistorySubscribers)
                ticker.RaiseTradeHistoryChanged(new TradeHistoryChangedEventArgs() { NewItems = newItems });
            return true;
        }

        public override bool Withdraw(AccountInfo account, string currency, string adress, string paymentId, double amount) {
            throw new NotImplementedException();
        }

        protected override ResizeableArray<TradeInfoItem> GetTradesCore(Ticker ticker, DateTime starTime, DateTime endTime) {
            return null;
        }

        protected internal override void ApplyCapturedEvent(Ticker ticker, TickerCaptureDataInfo info) {
            if(info.StreamType == CaptureStreamType.OrderBook)
                OnOrderBookSocketMessageReceived(this, new MessageReceivedEventArgs(info.Message));
            else if(info.StreamType == CaptureStreamType.TradeHistory)
                OnTradeHistorySocketMessageReceived(this, new MessageReceivedEventArgs(info.Message));
            else if(info.StreamType == CaptureStreamType.KLine)
                OnKlineSocketMessageReceived(this, new MessageReceivedEventArgs(info.Message));
        }

        protected internal override IIncrementalUpdateDataProvider CreateIncrementalUpdateDataProvider() {
            return new ExmoIncUpdateDataProvider();
        }

        protected override void StartListenTickerInfo(Ticker ticker) {
            if(TickersSocket == null)
                return;
            
            string marketSymbol = ticker.MarketName;
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.OrderBook, ticker) {
                Request = "{ \"id\": 1, \"method\":\"subscribe\", \"topics\" : [ \"spot/ticker:" + ticker.CurrencyPair + "\" ] }"
            });
        }

        protected override void StopListenTickerInfo(Ticker ticker) {
            if(TickersSocket == null)
                return;

            string marketSymbol = ticker.MarketName;
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.OrderBook, ticker) {
                Request = "{ \"id\": 1, \"method\":\"unsubscribe\", \"topics\" : [ \"spot/ticker:" + ticker.CurrencyPair + "\" ] }"
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
                Request = "{ \"id\": 1, \"method\":\"subscribe\", \"topics\" : [ \"spot/order_book_updates:" + ticker.CurrencyPair + "\" ] }"
            });
        }

        public override void StopListenOrderBook(Ticker ticker, bool force) {
            base.StopListenOrderBook(ticker, force);
            string marketSymbol = ticker.MarketName;
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.OrderBook, ticker) {
                Request = "{ \"id\": 1, \"method\":\"unsubscribe\", \"topics\" : [ \"spot/order_book_updates:" + ticker.CurrencyPair + "\" ] }"
            });
        }

        protected override void StartListenTradeHistoryCore(Ticker ticker) {
            if(TickersSocket == null) {
                StartListenTickersStream();
                if(!WaitUntil(5000, () => { return TickersSocketState == SocketConnectionState.Connected; }))
                    return;
            }
            string marketSymbol = ticker.MarketName;
            UpdateTrades(ticker);
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.TradeHistory, ticker) {
                Request = "{ \"id\": 1, \"method\":\"subscribe\", \"topics\" : [ \"spot/trades:" + ticker.CurrencyPair + "\" ] }"
            });
        }

        public override void StopListenTradeHistory(Ticker ticker, bool force) {
            base.StopListenOrderBook(ticker, force);
            string marketSymbol = ticker.MarketName;
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.TradeHistory, ticker) {
                Request = "{ \"id\": 1, \"method\":\"unsubscribe\", \"topics\" : [ \"spot/trades:" + ticker.CurrencyPair + "\" ] }"
            });
        }

        protected internal override void OnTickersSocketMessageReceived(object sender, MessageReceivedEventArgs e) {
            base.OnTickersSocketMessageReceived(sender, e);

            var root = JsonHelper.Default.Deserialize(e.Message);

            string ev = root.GetProperty("event").Value;
            if(ev == "info") {
                LogManager.Default.Add(LogType.Log, this, "WebSocket", root.GetProperty("message").Value, "");
                return;
            }

            string topic = root.GetProperty("topic").Value;
            string[] topicItems = topic.Split(':');

            string tickerName = topicItems[1];
            topic = topicItems[0];

            ExmoTicker ticker = (ExmoTicker)GetTicker(tickerName);
            if(ticker == null)
                return;

            if(ev == "subscribed") {
                if(topic == "spot/order_book_updates")
                    ticker.IsListeningOrderBookCore = true;
                else if(topic == "spot/trades")
                    ticker.IsListeningTradingHistoryCore = true;
                return;
            }
            else if(ev == "unsubscribed") {
                if(topic == "spot/order_book_updates")
                    ticker.IsListeningOrderBookCore = false;
                else if(topic == "spot/trades")
                    ticker.IsListeningTradingHistoryCore = false;
                return;
            }
            
            if(topic == "spot/order_book_updates") {
                ProcessOrderBookSocketMessage(root, ticker, e.Message, ev);
                return;
            }

            if(topic == "spot/trades") {
                ProcessTradeHistoryMessage(root, ticker, e.Message, ev);
                return;
            }

            if(topic == "spot/ticker") {
                ProcessTickerMessage(root, ticker, e.Message, ev);
                return;
            }
        }

        protected virtual void ProcessTickerMessage(JsonHelperToken root, ExmoTicker ticker, string message, string ev) {
            var data = root.GetProperty("data");

            ticker.HighestBidString = data.GetProperty("buy_price").Value;
            ticker.LowestAskString = data.GetProperty("sell_price").Value;
            ticker.Hr24HighString = data.GetProperty("high").Value;
            ticker.Hr24LowString = data.GetProperty("low").Value;
            ticker.VolumeString = data.GetProperty("vol_curr").Value;
            ticker.BaseVolumeString = data.GetProperty("vol").Value;

            ticker.RaiseChanged();
        }

        protected virtual void ProcessTradeHistoryMessage(JsonHelperToken root, ExmoTicker ticker, string message, string ev) {
            if(ticker.CaptureData)
                ticker.CaptureDataCore(CaptureStreamType.TradeHistory, CaptureMessageType.Incremental, message);

            List<TradeInfoItem> newItems = new List<TradeInfoItem>();
            var data = root.GetProperty("data");
            
            ticker.LockTrades();
            try {
                ticker.ClearTradeHistory();
                int ic = data.ItemsCount;

                for(int i = 0; i < ic; i++) {
                    var item = data.Items[i];
                    TradeInfoItem t = ProcessTradeInfoItem(ticker, item);
                    newItems.Add(t);
                    ticker.AddTradeHistoryItem(t);
                }
            }
            finally {
                ticker.UnlockTrades();
            }

            if(newItems.Count > 0) {
                if(ticker.HasTradeHistorySubscribers)
                    ticker.RaiseTradeHistoryChanged(new TradeHistoryChangedEventArgs() { Ticker = ticker, NewItems = newItems });
            }
        }

        protected virtual TradeInfoItem ProcessTradeInfoItem(ExmoTicker ticker, JsonHelperToken item) {
            TradeInfoItem info = new TradeInfoItem(null, ticker);

            info.IdString = item.GetProperty("trade_id").Value;
            info.Time = ExmoTime(item.GetProperty("date"));
            info.RateString = item.GetProperty("price").Value;
            info.AmountString = item.GetProperty("quantity").Value;

            info.Type = item.GetProperty("type").Value.EndsWith("buy") ? TradeType.Buy : TradeType.Sell;

            return info;
        }

        protected virtual void ProcessOrderBookSocketMessage(JsonHelperToken root, ExmoTicker ticker, string message, string ev) {
            var provider = CreateIncrementalUpdateDataProvider();
            if(ev == "snapshot") {
                provider.ApplySnapshot(root, ticker);
                if(ticker.CaptureData)
                    ticker.CaptureDataCore(CaptureStreamType.OrderBook, CaptureMessageType.Snapshot, message);
                return;
            }
            if(ticker.CaptureData)
                ticker.CaptureDataCore(CaptureStreamType.OrderBook, CaptureMessageType.Incremental, message);
            List<string[]> bids = new List<string[]>();
            List<string[]> asks = new List<string[]>();

            var data = root.GetProperty("data");
            var b = data.GetProperty("bid").Items;
            var a = data.GetProperty("ask").Items;
            if(a != null) {
                asks = new List<string[]>(a.Length);
                for(int i = 0; i < a.Length; i++) {
                    var item = a[i].Items;
                    asks.Add(new string[] { item[0].Value, item[1].Value, item[2].Value });
                }
            }
            if(b != null) {
                bids = new List<string[]>(b.Length);
                for(int i = 0; i < b.Length; i++) {
                    var item = b[i].Items;
                    bids.Add(new string[] { item[0].Value, item[1].Value, item[2].Value });
                }
            }
            IncrementalUpdateInfo info = new IncrementalUpdateInfo();
            info.Fill(-1, ticker, bids, asks, null);
            provider.Update(ticker, info);
        }
    }
}
