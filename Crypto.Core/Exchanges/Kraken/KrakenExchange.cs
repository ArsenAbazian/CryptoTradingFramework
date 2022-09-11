using Crypto.Core.Common;
using Crypto.Core.Exchanges.Base;
using Crypto.Core.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebSocket4Net;

namespace Crypto.Core.Exchanges.Kraken {
    public class KrakenExchange : Exchange {
        static KrakenExchange defaultExchange;
        public static KrakenExchange Default {
            get {
                if(defaultExchange == null) {
                    defaultExchange = (KrakenExchange)Exchange.FromFile(ExchangeType.Kraken, typeof(KrakenExchange));
                }
                return defaultExchange;
            }
        }

        public override Ticker CreateTicker(string name) {
            return new KrakenTicker(this) { CurrencyPair = name };
        }

        public override BalanceBase CreateAccountBalance(AccountInfo info, string currency) {
            return new KrakenAccountBalanceInfo(info, GetOrCreateCurrency(currency));
        }

        public override bool AllowCandleStickIncrementalUpdate => true;

        public override ExchangeType Type => ExchangeType.Kraken;

        public override string BaseWebSocketAdress => "wss://ws.kraken.com";

        protected override bool ShouldAddKlineListener => true;

        protected string GetNonce() {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        }

        public override ResizeableArray<CandleStickData> GetCandleStickData(Ticker ticker, int candleStickPeriodMin, DateTime startUtc, long periodInSeconds) {
            long startMin = (long)(startUtc.Subtract(epoch)).TotalMinutes;
            long end = startMin + periodInSeconds / 60;
            CandleStickIntervalInfo info = null;
            for(int index = 0; index < AllowedCandleStickIntervals.Count; index++) {
                CandleStickIntervalInfo i = AllowedCandleStickIntervals[index];
                if(i.Interval.TotalMinutes == candleStickPeriodMin) {
                    info = i;
                    break;
                }
            }
            string adress = string.Format("https://api.kraken.com/0/public/OHLC?pair={0}&interval={1}&since={2}",
                Uri.EscapeDataString(ticker.CurrencyPair), candleStickPeriodMin, startMin);
            ResizeableArray<CandleStickData> list = new ResizeableArray<CandleStickData>();
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(adress);
            }
            catch(Exception e) {
                LogManager.Default.Error(Type.ToString() + "/OHLC", e.ToString());
                return list;
            }
            if(bytes == null)
                return list;
            if(bytes.Length < 100) {
                string text = Encoding.UTF8.GetString(bytes);
                LogManager.Default.Error(Type.ToString() + "/OHLC", text);
                return list;
            }

            int startIndex = 0;
            JsonHelper.Default.SkipSymbol(bytes, ':', 3, ref startIndex);
            if(startIndex == -1)
                return list;

            List<string[]> res = JsonHelper.Default.DeserializeArrayOfArrays(bytes, ref startIndex, 8);
            if(res == null)
                return list;
            DateTime startTime = epoch;
            for(int i = 0; i < res.Count; i++) {
                string[] item = res[i];
                CandleStickData data = new CandleStickData();
                data.Time = startTime.AddSeconds(FastValueConverter.ConvertPositiveLong(item[0])).ToLocalTime();
                data.Open = FastValueConverter.Convert(item[1]);
                data.High = FastValueConverter.Convert(item[2]);
                data.Low = FastValueConverter.Convert(item[3]);
                data.Close = FastValueConverter.Convert(item[4]);
                data.Volume = FastValueConverter.Convert(item[6]);
                list.Add(data);
            }

            return list;
        }

        public override TradingResult BuyLong(AccountInfo account, Ticker ticker, double rate, double amount) {
            try {
                string parameters = string.Format(
                    "pair={0}" +
                    "&type=buy" +
                    "&ordertype=limit" +
                    "&price={1}" +
                    "&volume={2}", ticker.CurrencyPair, rate, amount);
                return OnOrderResult(account, ticker, OrderType.Buy, UploadPrivateData(account, AddOrderApiString, AddOrderApiPathString, parameters));
            }
            catch(Exception e) {
                LogManager.Default.Log(e.ToString());
                return null;
            }
        }

        public override TradingResult BuyShort(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public override bool Cancel(AccountInfo account, Ticker ticker, string orderId) {
            try {
                string parameters = string.Format(
                    "txid={0}", orderId);
                return OnCancelOrder(account, ticker, UploadPrivateData(account, CancelOrderApiString, CancelOrderApiPathString, parameters));
            }
            catch(Exception e) {
                LogManager.Default.Log(e.ToString());
                return false;
            }
        }

        protected virtual bool OnCancelOrder(AccountInfo account, Ticker ticker, byte[] bytes) {
            var root = JsonHelper.Default.Deserialize(bytes);
            if(HasError(account, root, nameof(OnOrderResult), bytes))
                return false;
            var result = root.GetProperty("result");
            return result != null;
        }

        public override bool CreateDeposit(AccountInfo account, string currency) {
            LogManager.Default.Warning(this, "CreateDeposit not supported by Kraken", "");
            return true;
        }

        public override List<CandleStickIntervalInfo> GetAllowedCandleStickIntervals() {
            List<CandleStickIntervalInfo> info = new List<CandleStickIntervalInfo>();
            info.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(1), Text = "1 Minute" });
            info.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(5), Text = "5 Minutes" });
            info.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(15), Text = "15 Minutes" });
            info.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(30), Text = "30 Minutes" });
            info.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(60), Text = "1 Hour" });
            info.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(240), Text = "4 Hours" });
            info.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(1440), Text = "1 Day" });
            info.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(10080), Text = "7 Days" });
            info.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(21600), Text = "15 Days" });
            return info;
        }

        public override bool GetBalance(AccountInfo info, string currency) {
            return UpdateBalances(info);
        }

        public override bool GetDeposit(AccountInfo account, CurrencyInfo currency) {
            if(!GetDepositMethods(account, currency))
                return false;
            try {
                KrakenCurrencyInfo info = (KrakenCurrencyInfo)currency;
                if(info.CurrentMethod == null) {
                    LogManager.Default.Error(this, nameof(GetDeposit), "No Funding Method Found");
                    return false;
                }
                string parameters = string.Format("asset={0}&method={1}", info.AltName, info.CurrentMethod.CurrencyName);
                return OnGetDeposites(account, UploadPrivateData(account, DepositAddressesApiString, DepositAddressesApiPathString, parameters));
            }
            catch(Exception e) {
                LogManager.Default.Log(e.ToString());
                return false;
            }
        }

        public override bool SupportMultipleDepositMethods { get { return true; } }

        public override bool GetDepositMethods(AccountInfo account, CurrencyInfo currency) {
            try {
                string currName = ((KrakenCurrencyInfo)currency).AltName;
                return OnGetDepositMethods(account, (KrakenCurrencyInfo)currency, UploadPrivateData(account, DepositMethodsApiString, DepositMethodsApiPathString, "asset=" + currName));
            }
            catch(Exception e) {
                LogManager.Default.Log(e.ToString());
                return false;
            }
        }

        protected bool HasError(AccountInfo account, JsonHelperToken root, string methodName, byte[] bytes) {
            string accountName = account == null ? "any account" : account.Name;
            string errors = CheckForErrors(root);
            
            if(errors != null) {
                LogManager.Default.Error(this, "Failed:" + methodName, "Account: " + accountName + ", message = " + errors);
                return true;
            }
            if(root.Type == JsonObjectType.None || root.Type == JsonObjectType.Error) {
                errors = Encoding.UTF8.GetString(bytes);
                LogManager.Default.Error(this, "Failed:" + methodName, "Account: " + accountName + ", message = " + errors);
                return true;
            }
            return false;
        }

        protected bool HasError(JsonHelperToken root, string methodName) {
            string errors = CheckForErrors(root);

            if(errors != null) {
                LogManager.Default.Error(this, "Failed:" + methodName, errors);
                return true;
            }
            if(root.Type == JsonObjectType.None || root.Type == JsonObjectType.Error) {
                errors = "unknown error";
                LogManager.Default.Error(this, "Failed:" + methodName, errors);
                return true;
            }
            return false;
        }

        public override CurrencyInfo CreateCurrency(string currency) {
            return new KrakenCurrencyInfo(this, currency);
        }

        protected virtual bool OnGetDepositMethods(AccountInfo account, KrakenCurrencyInfo currInfo, byte[] data) {
            JsonHelperToken root = JsonHelper.Default.Deserialize(data);
            if(HasError(account, root, nameof(OnGetDepositMethods), data))
                return false;

            JsonHelperToken methods = root.Properties[1];
            if(methods == null)
                return false;

            if(methods.ItemsCount == 0)
                return true;

            foreach(var method in methods.Items) {
                string name = method.GetProperty("method").Value;
                currInfo.GetOrCreateMethod(name);
                DepositMethod m = currInfo.GetOrCreateMethod(name);
                m.Limit = method.GetProperty("limit").ValueBool;
                m.GenAddress = method.GetProperty("gen-address").ValueBool;
                string[] items = name.Split(' ');
                m.Currency = items[items.Length - 1];
                m.CurrencyName = items[0];
            }
            
            return true;
        }

        public override bool GetDeposites(AccountInfo account) {
            return true;
        }

        protected virtual bool OnGetDeposites(AccountInfo account, byte[] data) {
            JsonHelperToken root = JsonHelper.Default.Deserialize(data);
            if(HasError(account, root, nameof(OnGetDeposites), data))
                return false;
            JsonHelperToken balances = root.GetProperty("result");
            if(balances == null)
                return false;
            account.Balances.Clear();
            if(balances.PropertiesCount == 0) {
                LogManager.Default.Warning(this, nameof(OnGetDeposites), "No Deposit Address Found");
                return true;
            }
            for(int i = 0; i < balances.Properties.Length; i++) {
                JsonHelperToken prop = balances.Properties[i];
                var b = account.GetOrCreateBalanceInfo(prop.Name);
                b.OnOrders = 0;
                b.Available = b.Balance = prop.ValueDouble;
                account.Balances.Add(b);
            }
            return true;
        }

        public override bool GetTickersInfo() {
            if(Tickers.Count > 0)
                return true;
            string adress = "https://api.kraken.com/0/public/AssetPairs";
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(adress);

                
                JsonHelperToken root = JsonHelper.Default.Deserialize(bytes);
                if(HasError(root, nameof(GetTickersInfo)))
                    return false;

                ClearTickers();

                JsonHelperToken tickers = root.GetProperty("result");
                if(tickers == null)
                    return true;
                foreach(var item in tickers.Properties) {
                    string name = item.GetProperty("wsname").Value;
                    string pair = item.GetProperty("altname").Value;
                    KrakenTicker ticker = (KrakenTicker)GetOrCreateTicker(pair);

                    
                    string[] items = name.Split('/');
                    ticker.WebSocketName = name;
                    ticker.BaseCurrency = item.GetProperty("quote").Value;
                    ticker.MarketCurrency = item.GetProperty("base").Value;
                    ticker.BaseCurrencyDisplayName = items[1];
                    ticker.MarketCurrencyDisplayName = items[0];
                    ticker.CurrencyPair = pair;

                    JsonHelperToken fees = item.GetProperty("fees");
                    if(fees != null && fees.ItemsCount > 0)
                        ticker.Fee = fees.Items[0].ValueDouble;

                    AddTicker(ticker);
                }
                IsInitialized = true;
                return true;
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
            
        }
        public override bool ObtainExchangeSettings() {
            RequestRate = new List<RateLimit>();
            RequestRate.Add(new RateLimit(this) { Limit = 15, Interval = TimeSpan.TicksPerSecond * 3 });
            return true;
        }

        public override TradingResult SellLong(AccountInfo account, Ticker ticker, double rate, double amount) {
            try {
                string parameters = string.Format(
                    "pair={0}" +
                    "&type=sell" +
                    "&ordertype=limit" +
                    "&price={1}" +
                    "&volume={2}", ticker.CurrencyPair, rate, amount);
                return OnOrderResult(account, ticker, OrderType.Sell, UploadPrivateData(account, AddOrderApiString, AddOrderApiPathString, parameters));
            }
            catch(Exception e) {
                LogManager.Default.Log(e.ToString());
                return null;
            }
        }

        protected virtual TradingResult OnOrderResult(AccountInfo account, Ticker ticker, OrderType type, byte[] bytes) {
            var root = JsonHelper.Default.Deserialize(bytes);
            if(HasError(account, root, nameof(OnOrderResult), bytes))
                return null;
            var result = root.GetProperty("result");
            if(result == null)
                return null;
            var descr = result.GetProperty("descr");
            var txid = result.GetProperty("txid");

            if(descr == null || txid == null)
                return null;
            return new TradingResult() { Date = DateTime.Now, OrderId = txid.Items[0].Value, Ticker = ticker, Type = type };
        }

        public override TradingResult SellShort(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public override bool SupportWebSocket(WebSocketType type) {
            if(type == WebSocketType.Tickers)
                return false;
            if(type == WebSocketType.Ticker)
                return true;
            if(type == WebSocketType.OrderBook)
                return true;
            if(type == WebSocketType.Trades)
                return true;
            if(type == WebSocketType.Kline)
                return true;
            return false;
        }

        protected override void StartListenOrderBookCore(Ticker ticker) {
            if(TickersSocket == null) {
                StartListenTickersStream();
                if(!WaitUntil(5000, () => { return TickersSocketState == SocketConnectionState.Connected; }))
                    return;
            }
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.OrderBook, ticker) { 
                Request = "{ \"event\":\"subscribe\", \"pair\": [ \"" + ((KrakenTicker)ticker).WebSocketName + "\" ], \"subscription\": { \"name\": \"book\", \"depth\": 1000 } }" 
            });
        }

        public override void StopListenOrderBook(Ticker ticker, bool force) {
            base.StopListenOrderBook(ticker);
            if(TickersSocket == null)
                return;
            TickersSocket.Unsubscribe(new WebSocketSubscribeInfo(SocketSubscribeType.OrderBook, ticker) { 
                Request = "{ \"event\":\"unsubscribe\", \"pair\": [ \"" + ((KrakenTicker)ticker).WebSocketName + "\" ], \"subscription\": { \"name\": \"book\", \"depth\": 1000 } }" 
            });
        }

        protected override void StartListenTradeHistoryCore(Ticker ticker) {
            if(TickersSocket == null) {
                StartListenTickersStream();
                if(!WaitUntil(5000, () => { return TickersSocketState == SocketConnectionState.Connected; }))
                    return;
            }
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.TradeHistory, ticker) { 
                Request = "{ \"event\":\"subscribe\", \"pair\": [ \"" + ((KrakenTicker)ticker).WebSocketName + "\" ], \"subscription\": { \"name\": \"ticker\" } }" 
            });
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.TradeHistory, ticker) { 
                Request = "{ \"event\":\"subscribe\", \"pair\": [ \"" + ((KrakenTicker)ticker).WebSocketName + "\" ], \"subscription\": { \"name\": \"trade\" } }" 
            });
            UpdateTrades(ticker);
        }

        public override void StopListenTradeHistory(Ticker ticker, bool force) {
            base.StopListenOrderBook(ticker);
            if(TickersSocket == null)
                return;
            TickersSocket.Unsubscribe(new WebSocketSubscribeInfo(SocketSubscribeType.TradeHistory, ticker) { 
                Request = "{ \"event\":\"unsubscribe\", \"pair\": [ \"" + ((KrakenTicker)ticker).WebSocketName + "\" ], \"subscription\": { \"name\": \"ticker\" } }" 
            });
            TickersSocket.Unsubscribe(new WebSocketSubscribeInfo(SocketSubscribeType.TradeHistory, ticker) { 
                Request = "{ \"event\":\"unsubscribe\", \"pair\": [ \"" + ((KrakenTicker)ticker).WebSocketName + "\" ], \"subscription\": { \"name\": \"trade\" } }" 
            });
        }

        protected internal override void OnTickersSocketOpened(object sender, EventArgs e) {
            base.OnTickersSocketOpened(sender, e);
        }

        protected internal override void OnTickersSocketMessageReceived(object sender, MessageReceivedEventArgs e) {
            base.OnTickersSocketMessageReceived(sender, e);

            JsonHelperToken root = JsonHelper.Default.Deserialize(e.Message);
            if(root.Type != JsonObjectType.Array)
                return;
            
            if(root.Items.Length < 2)
                return;
            
            JsonHelperToken[] items = root.Items;
            
            string channelName = items[items.Length - 2].Value;
            string tickerName = items[items.Length - 1].Value;
            Ticker ticker;

            if(channelName == "book-1000") {
                ticker = Tickers.FirstOrDefault(tt => ((KrakenTicker)tt).WebSocketName == tickerName);
                if(ticker == null) {
                    LogManager.Default.Log(LogType.Error, this, "cannot find ticker", tickerName);
                    return;
                }
                OnOrderBookSocketMessageReceived(ticker, e.Message, root);
            }
            else if(channelName == "trade") {
                ticker = Tickers.FirstOrDefault(tt => ((KrakenTicker)tt).WebSocketName == tickerName);
                if(ticker == null) {
                    LogManager.Default.Log(LogType.Error, this, "cannot find ticker", tickerName);
                    return;
                }
                OnTradeSocketMessageReceived(ticker, e.Message, root);
            }
            else if(channelName == "ticker") {
                ticker = Tickers.FirstOrDefault(tt => ((KrakenTicker)tt).WebSocketName == tickerName);
                if(ticker == null) {
                    LogManager.Default.Log(LogType.Error, this, "cannot find ticker", tickerName);
                    return;
                }
                OnTickerSocketMessageReceived(ticker, root);
            }
        }

        private void OnOrderBookSocketMessageReceived(Ticker ticker, string message, JsonHelperToken root) {
            //System.Diagnostics.Debug.WriteLine(message);
            if(root.Items[1].Properties[0].Name == "as")
                OnTickerOrderBookSnapshotRecv(ticker, message, root);
            else
                OnTickerOrderBookIncrementalUpdateRecv(ticker, message, root);

        }

        private void OnTickerSocketMessageReceived(Ticker ticker, JsonHelperToken root) {
            ticker.LowestAskString = root.Items[1].Properties[0].Items[0].Value;
            ticker.HighestBidString = root.Items[1].Properties[1].Items[0].Value;
            ticker.Volume = FastValueConverter.Convert(root.Items[1].Properties[3].Items[1].Value);
            ticker.Hr24Low = FastValueConverter.Convert(root.Items[1].Properties[6].Items[1].Value);
            ticker.Hr24High = FastValueConverter.Convert(root.Items[1].Properties[7].Items[1].Value);

            ticker.RaiseChanged();
        }

        private void OnTradeSocketMessageReceived(Ticker ticker, string message, JsonHelperToken root) {
            if(ticker.CaptureData)
                ticker.CaptureDataCore(CaptureStreamType.TradeHistory, CaptureMessageType.Incremental, message);
            int startIndex = 0;
            JsonHelper.Default.SkipSymbol(message, ',', 1, ref startIndex);
            List<string[]> trades = JsonHelper.Default.DeserializeArrayOfArrays(message, ref startIndex);

            List<TradeInfoItem> newItems = new List<TradeInfoItem>(trades.Count);
            for(int i = 0; i < trades.Count; i++) {
                TradeInfoItem item = new TradeInfoItem(null, ticker);
                string[] s = trades[i];
                item.AmountString = s[1];
                item.RateString = s[0];
                double sec = FastValueConverter.Convert(s[2]);
                item.Time = epoch.AddSeconds(sec).ToLocalTime();
                item.Type = s[3][0] == 'b' ? TradeType.Buy : TradeType.Sell;
                newItems.Add(item);
                ticker.AddTradeHistoryItem(item);
            }

            if(trades.Count > 0) {
                if(ticker.HasTradeHistorySubscribers)
                    ticker.RaiseTradeHistoryChanged(new TradeHistoryChangedEventArgs() { Ticker = ticker, NewItems = newItems });
            }
        }

        public List<string[]> ArrayValuesToList(JsonHelperToken array) {
            List<string[]> res = new List<string[]>(array.Items.Length);
            for(int i = 0; i < array.Items.Length; i++) {
                string[] item = new string[array.Items[i].ItemsCount];
                JsonHelperToken[] jitems = array.Items[i].Items;
                for(int j = 0; j < item.Length; j++) {
                    item[j] = jitems[j].Value;
                }
                res.Add(item);
            }
            return res;
        }

        private void OnTickerOrderBookIncrementalUpdateRecv(Ticker ticker, string message, JsonHelperToken root) {
            if(ticker.CaptureData)
                ticker.CaptureDataCore(CaptureStreamType.OrderBook, CaptureMessageType.Incremental, message);

            List<string[]> lista = null;
            List<string[]> listb = null;

            JsonHelperToken[] props = root.Items[1].Properties;
            if(props[0].Name[0] == 'a') {
                lista = ArrayValuesToList(props[0]);
                if(props.Length > 1 && props[1].Name[0] == 'b')
                    listb = ArrayValuesToList(props[1]);
            }
            else if(props[0].Name[0] == 'b') {
                listb = ArrayValuesToList(props[0]);
            }

            IncrementalUpdateInfo info = new IncrementalUpdateInfo();
            info.Fill(0, ticker, listb, lista, null);
            
            IIncrementalUpdateDataProvider provider = CreateIncrementalUpdateDataProvider();
            provider.Update(ticker, info);
        }

        protected virtual void OnTickerOrderBookSnapshotRecv(Ticker ticker, string message, JsonHelperToken root) {
            if(ticker.CaptureData)
                ticker.CaptureDataCore(CaptureStreamType.OrderBook, CaptureMessageType.Snapshot, message);
            ticker.Updates.Clear();
            
            IIncrementalUpdateDataProvider provider = CreateIncrementalUpdateDataProvider();
            provider.ApplySnapshot(root, ticker);
        }

        public override bool UpdateAccountTrades(AccountInfo account, Ticker ticker) {
            return true; 
        }

        protected virtual string BalanceApiString { get { return "https://api.kraken.com/0/private/Balance"; } }
        protected virtual string BalanceApiPathString { get { return "/0/private/Balance"; } }

        protected virtual string DepositAddressesApiString { get { return "https://api.kraken.com/0/private/DepositAddresses"; } }
        protected virtual string DepositAddressesApiPathString { get { return "/0/private/DepositAddresses"; } }

        protected virtual string DepositMethodsApiString { get { return "https://api.kraken.com/0/private/DepositMethods"; } }
        protected virtual string DepositMethodsApiPathString { get { return "/0/private/DepositMethods"; } }

        protected virtual string OpenedOrdersApiString { get { return "https://api.kraken.com/0/private/OpenOrders"; } }
        protected virtual string OpenedOrdersApiPathString { get { return "/0/private/OpenOrders"; } }

        protected virtual string AccountTradesApiString { get { return "https://api.kraken.com/0/private/TradesHistory"; } }
        protected virtual string AccountTradesApiPathString { get { return "/0/private/TradesHistory"; } }

        protected virtual string AddOrderApiString { get { return "https://api.kraken.com/0/private/AddOrder"; } }
        protected virtual string AddOrderApiPathString { get { return "/0/private/AddOrder"; } }

        protected virtual string CancelOrderApiString { get { return "https://api.kraken.com/0/private/CancelOrder"; } }
        protected virtual string CancelOrderApiPathString { get { return "/0/private/CancelOrder"; } }

        SHA256 sha;
        protected SHA256 Sha {
            get {
                if(sha == null)
                    sha = SHA256.Create();
                return sha;
            }
        }
        public override string GetSign(string path, string text, string nonce, AccountInfo info) {
            string postdata = text;
            string nonce_data = nonce + postdata;
            byte[] data = Encoding.UTF8.GetBytes(nonce_data);
            byte[] path_data = Encoding.UTF8.GetBytes(path);

            byte[] sha_data = Sha.ComputeHash(data);

            byte[] total = path_data.Concat(sha_data).ToArray();
            byte[] hash = info.HmacSha.ComputeHash(total);

            return Convert.ToBase64String(hash);
        }

        protected virtual byte[] UploadPrivateData(AccountInfo info, string address, string apiPath, string parameters) {
            MyWebClient client = GetWebClient();

            string nonce = GetNonce();
            string queryString = null;
            if(string.IsNullOrEmpty(parameters))
                queryString = string.Format("nonce={0}", nonce);
            else
                queryString = string.Format("nonce={0}&{1}", nonce, parameters);
            string signature = info.GetSign(apiPath, queryString, nonce);

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("API-Key", info.ApiKey);
            client.DefaultRequestHeaders.Add("API-Sign", signature);
            client.DefaultRequestHeaders.Add("User-Agent", "KrakenDotNet Client");

            StringContent data = new StringContent(queryString, Encoding.UTF8, "application/x-www-form-urlencoded");
            Task<HttpResponseMessage> response = client.PostAsync(address, data);
            response.Wait(10000);
            Task<byte[]> bytes = response.Result.Content.ReadAsByteArrayAsync();
            bytes.Wait(10000);
            return bytes.Result;
        }

        public override bool UpdateBalances(AccountInfo info) {
            if(info == null)
                return false;
            try {
                InitializeDefaultBalances(info);
                return OnGetBalances(info, UploadPrivateData(info, BalanceApiString, BalanceApiPathString, null));
            }
            catch(Exception e) {
                LogManager.Default.Log(e.ToString());
                return false;
            }
        }

        private void InitializeDefaultBalances(AccountInfo info) {
            if(Currencies.Count == 0)
                UpdateCurrencies();
            if(info.Balances == null || info.Balances.Count == 0) {
                info.GetOrCreateBalanceInfo("ZUSD");
                info.GetOrCreateBalanceInfo("XXBT");
                info.GetOrCreateBalanceInfo("XXRP");
                info.GetOrCreateBalanceInfo("XLTC");
                info.GetOrCreateBalanceInfo("XETH");
                info.GetOrCreateBalanceInfo("XETC");
                info.GetOrCreateBalanceInfo("XREP");
                info.GetOrCreateBalanceInfo("XXMR");
                info.GetOrCreateBalanceInfo("USDT");
                info.GetOrCreateBalanceInfo("DASH");
                info.GetOrCreateBalanceInfo("GNO");
                info.GetOrCreateBalanceInfo("EOS");
                info.GetOrCreateBalanceInfo("BCH");
                info.GetOrCreateBalanceInfo("ADA");
                info.GetOrCreateBalanceInfo("QTUM");
                info.GetOrCreateBalanceInfo("XTZ");
                info.GetOrCreateBalanceInfo("ATOM");
                info.GetOrCreateBalanceInfo("SC");
                info.GetOrCreateBalanceInfo("LSK");
                info.GetOrCreateBalanceInfo("WAVES");
                info.GetOrCreateBalanceInfo("ICX");
                info.GetOrCreateBalanceInfo("BAT");
                info.GetOrCreateBalanceInfo("OMG");
                info.GetOrCreateBalanceInfo("LINK");
                info.GetOrCreateBalanceInfo("DAI");
                info.GetOrCreateBalanceInfo("PAXG");
                info.GetOrCreateBalanceInfo("ALGO");
                info.GetOrCreateBalanceInfo("USDC");
                info.GetOrCreateBalanceInfo("TRX");
                info.GetOrCreateBalanceInfo("DOT");
                info.GetOrCreateBalanceInfo("OXT");
                info.GetOrCreateBalanceInfo("ETH2.S");
                info.GetOrCreateBalanceInfo("EHT2");
                info.GetOrCreateBalanceInfo("USD.M");
            }
        }

        public virtual bool OnGetBalances(AccountInfo account, byte[] data) {
            JsonHelperToken root = JsonHelper.Default.Deserialize(data);
            if(HasError(account, root, nameof(OnGetBalances), data))
                return false;
            JsonHelperToken balances = root.Properties[1];
            if(balances == null)
                return false;
            account.Balances.ForEach(b => b.Clear() );
            foreach(JsonHelperToken prop in balances.Properties) {
                var b = account.GetOrCreateBalanceInfo(prop.Name);
                b.OnOrders = 0;
                b.Available = b.Balance = prop.ValueDouble;
            }
            return true;
        }

        public override bool UpdateCurrencies() {
            if(Tickers.Count == 0)
                return false;
            string adress = "https://api.kraken.com/0/public/Assets";
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(adress);
                return OnUpdateCurrencies(bytes);
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }

        protected virtual string CheckForErrors(JsonHelperToken root) {
            JsonHelperToken errors = root.GetProperty("error");
            if(errors == null || errors.ItemsCount == 0)
                return null;
            StringBuilder b = new StringBuilder();
            for(int i = 0; i < errors.ItemsCount; i++) {
                b.Append(errors.Items[i].Value);
                b.Append(' ');
            }
            return b.ToString();
        }

        protected virtual bool OnUpdateCurrencies(byte[] bytes) {
            JsonHelperToken root = JsonHelper.Default.Deserialize(bytes);
            string errors = CheckForErrors(root);
            if(errors != null) {
                LogManager.Default.Add(LogType.Error, this, Type.ToString(), "Failed" + nameof(OnUpdateCurrencies), errors);
                return false;
            }
            var res = root.GetProperty("result");
            for(int i = 0; i < res.PropertiesCount; i++) {
                KrakenCurrencyInfo info = (KrakenCurrencyInfo)GetOrCreateCurrency(res.Properties[i].Name);
                info.AltName = res.Properties[i].GetProperty("altname").Value;
            }
            
            return true;
        }

        public override bool UpdateOpenedOrders(AccountInfo info, Ticker ticker) {
            try {
                return OnGetOpenedOrders(info, ticker, UploadPrivateData(info, OpenedOrdersApiString, OpenedOrdersApiPathString, null));
            }
            catch(Exception e) {
                LogManager.Default.Log(e.ToString());
                return false;
            }
        }

        public override int WebSocketAllowedDelayInterval => 10000;

        protected virtual bool OnGetOpenedOrders(AccountInfo account, Ticker ticker, byte[] data) {
            if(!ticker.IsOpenedOrdersChanged(data))
                return true;
            ticker.SaveOpenedOrders();
            ticker.OpenedOrdersData = data;

            JsonHelperToken root = JsonHelper.Default.Deserialize(data);
            if(HasError(account, root, nameof(OnGetOpenedOrders), data))
                return false;
            var result = root.GetProperty("result");
            if(result == null)
                return true;
            var open = result.GetProperty("open");
            if(open == null)
                return true;
            try {
                account.OpenedOrders.Clear();
                for(int i = 0; i < open.PropertiesCount; i++) {
                    var order = open.Properties[i];
                    OpenedOrderInfo info = new OpenedOrderInfo(account, ticker);
                    info.OrderId = order.Name;
                    info.Date = epoch.AddSeconds(order.GetProperty("opentm").ValueDouble).ToLocalTime();

                    var descr = order.GetProperty("descr");
                    info.Type = descr.GetProperty("type").Value[0] == 's' ? OrderType.Sell : OrderType.Buy;
                    info.ValueString = descr.GetProperty("price").Value;
                    info.AmountString = order.GetProperty("vol").Value;

                    account.OpenedOrders.Add(info);
                }
            }
            finally {
                if(ticker != null)
                    ticker.RaiseOpenedOrdersChanged();
            }
            return true;
        }

        public override bool UpdateOrderBook(Ticker tickerBase) {
            return UpdateOrderBook(tickerBase, 500); // max
        }

        public override bool UpdateOrderBook(Ticker ticker, int depth) {
            if(Tickers.Count == 0)
                return false;
            string adress = "https://api.kraken.com/0/public/Depth?pair=" + ticker.CurrencyPair + "&depth=" + depth;
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(adress);

                if(bytes == null) {
                    LogManager.Default.Add(LogType.Error, this, Name, "no order book data received", "bytes = null");
                    return false;
                }

                if(ticker.CaptureData)
                    ticker.CaptureDataCore(CaptureStreamType.OrderBook, CaptureMessageType.Snapshot, System.Text.ASCIIEncoding.Default.GetString(bytes));
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }

            ticker.OrderBook.BeginUpdate();
            try {
                int startIndex = 0;
                if(!JsonHelper.Default.SkipSymbol(bytes, ':', 4, ref startIndex)) {
                    LogManager.Default.Add(LogType.Error, this, Name, "can't parse order book xml", "cannot find asks");
                    return false;
                }

                List<string[]> jasks = JsonHelper.Default.DeserializeArrayOfArrays(bytes, ref startIndex, 3);
                if(jasks == null) {
                    LogManager.Default.Add(LogType.Error, this, Name, "can't parse order book xml", "cannot read asks array");
                    return false;
                }

                List<string[]> jbids = JsonHelper.Default.DeserializeArrayOfArrays(bytes, ref startIndex, 3);
                if(jbids == null) {
                    LogManager.Default.Add(LogType.Error, this, Name, "can't parse order book xml", "cannot read bids array");
                    return false;
                }

                List<OrderBookEntry> bids = ticker.OrderBook.Bids;
                List<OrderBookEntry> asks = ticker.OrderBook.Asks;

                bids.Clear();
                asks.Clear();

                for(int i = 0; i < jbids.Count; i++) {
                    string[] item = jbids[i];
                    bids.Add(new OrderBookEntry() { ValueString = item[0], AmountString = item[1] });
                }
                for(int i = 0; i < jasks.Count; i++) {
                    string[] item = jasks[i];
                    asks.Add(new OrderBookEntry() { ValueString = item[0], AmountString = item[1] });
                }

                return true;
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
            finally {
                ticker.OrderBook.IsDirty = false;
                ticker.OrderBook.EndUpdate();
            }
        }

        public override void UpdateOrderBookAsync(Ticker tickerBase, int depth, Action<OperationResultEventArgs> onOrderBookUpdated) {
            throw new NotImplementedException();
        }

        public override bool SupportCummulativeTickersUpdate { get { return false; } }
        public override bool UpdateTicker(Ticker ticker) {
            if(Tickers.Count == 0)
                return false;
            string adress = "https://api.kraken.com/0/public/Ticker?pair=" + ticker.CurrencyPair;
            string text = null;
            try {
                text = GetDownloadString(adress);

                if(string.IsNullOrEmpty(text))
                    return false;

                JObject root = JsonConvert.DeserializeObject<JObject>(text);

                if(root.Value<JArray>("error").Count > 0)
                    return false;

                JObject result = root.Value<JObject>("result");
                if(result == null)
                    return true;
                JObject tinfo = (JObject)((JProperty)result.First).Value;
                if(tinfo == null)
                    return false;

                ticker.LowestAsk = FastValueConverter.Convert(tinfo.Value<JArray>("a")[0].ToString());
                ticker.HighestBid = FastValueConverter.Convert(tinfo.Value<JArray>("b")[0].ToString());
                ticker.Last = FastValueConverter.Convert(tinfo.Value<JArray>("c")[0].ToString());
                ticker.Volume = FastValueConverter.Convert(tinfo.Value<JArray>("v")[1].ToString());
                ticker.Hr24High = FastValueConverter.Convert(tinfo.Value<JArray>("h")[0].ToString());
                ticker.Hr24Low = FastValueConverter.Convert(tinfo.Value<JArray>("l")[0].ToString());

                return true;
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }

        public override bool UpdateTickersInfo() {
            return true;
        }

        public override bool UpdateTrades(Ticker ticker) {
            string address = "https://api.kraken.com/0/public/Trades?pair=" + ticker;
            try {
                return OnUpdateTrades(ticker, GetDownloadBytes(address));
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }

        protected virtual bool OnUpdateTrades(Ticker ticker, byte[] data) {
            if(data == null)
                return false;
            JsonHelperToken root = JsonHelper.Default.Deserialize(data);
            if(HasError(null, root, nameof(OnUpdateTrades), data))
                return false;
            JsonHelperToken[] items = root.Properties[1].Properties;
            if(items == null)
                return false;
            items = items[0].Items;
            if(items == null)
                return false;

            ticker.LockTrades();
            ticker.ClearTradeHistory();
            for(int i = 0; i < items.Length; i++) {
                JsonHelperToken[] obj = items[i].Items;
                TradeInfoItem item = new TradeInfoItem(null, ticker);
                item.RateString = obj[0].Value;
                item.AmountString = obj[1].Value;
                item.Time = epoch.AddSeconds(obj[2].ValueDouble).ToLocalTime();
                item.Total = item.Rate * item.Amount;
                item.Type = obj[3].Value[0] == 'b' ? TradeType.Buy : TradeType.Sell;
                item.Fill = TradeFillType.Fill;
                ticker.AddTradeHistoryItem(item);
            }
            ticker.UnlockTrades();

            if(ticker.HasTradeHistorySubscribers)
                ticker.RaiseTradeHistoryChanged(new TradeHistoryChangedEventArgs() { NewItems = ticker.TradeHistory });

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
            return new KrakenIncrementalUpdateDataProvider();
        }

        protected internal override HMAC CreateHmac(string secret) {
            return new HMACSHA512(Convert.FromBase64String(secret));
        }
    }
}
