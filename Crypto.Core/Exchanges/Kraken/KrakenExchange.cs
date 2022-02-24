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
            throw new NotImplementedException();
        }

        public override TradingResult BuyShort(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public override bool Cancel(AccountInfo account, Ticker ticker, string orderId) {
            throw new NotImplementedException();
        }

        public override bool CreateDeposit(AccountInfo account, string currency) {
            throw new NotImplementedException();
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

        public override bool GetDeposite(AccountInfo account, string currency) {
            return GetDepositeMethods(account, currency);
            //return true;
        }

        public bool GetDepositeMethods(AccountInfo account, string currency) {
            try {
                return OnGetDepositeMethods(account, currency, UploadPrivateData(account, DepositMethodsApiString, DepositMethodsApiPathString, "asset=" + currency));
            }
            catch(Exception e) {
                LogManager.Default.Log(e.ToString());
                return false;
            }

        }

        protected bool HasError(AccountInfo account, JsonHelperToken root, string methodName) {
            string accountName = account == null ? "any account" : account.Name;

            if(root.PropertiesCount == 0 && root.ItemsCount == 0 && root.Type == JsonObjectType.Value) {
                LogManager.Default.Error(this, "Failed:" + methodName, "Account: " + accountName + ", message=unknown error");
                return true;
            }
            if(root.Properties[0].ItemsCount > 0) {
                LogManager.Default.Error(this, "Failed:" + methodName, "Account: " + accountName + ", message=" + root.Properties[0].Items[0].Value);
                return true;
            }
            return false;
        }

        public override CurrencyInfoBase CreateCurrency(string currency) {
            return new KrakenCurrencyInfo(currency);
        }

        protected virtual bool OnGetDepositeMethods(AccountInfo account, string currency, byte[] data) {
            JsonHelperToken root = JsonHelper.Default.Deserialize(data);
            if(HasError(account, root, nameof(OnGetDepositeMethods)))
                return false;
            if(root.Properties.Length < 2)
                return true;

            JsonHelperToken methods = root.Properties[1];
            if(methods == null)
                return false;

            KrakenCurrencyInfo currInfo = (KrakenCurrencyInfo)Currencies.FirstOrDefault(c => c.Currency == currency);
            if(currInfo == null) {
                currInfo = (KrakenCurrencyInfo)CreateCurrency(currency);
                Currencies.Add(currInfo);
            }

            foreach(var method in methods.Items) {
                string name = method.Properties[0].Value;
                KrakenCurrencyMethod m = currInfo.Methods.FirstOrDefault(mm => mm.Name == name);
                if(m == null) {
                    m = new KrakenCurrencyMethod();
                    currInfo.Methods.Add(m);
                }
                m.Name = name;
                m.Limit = method.Properties[1].ValueBool;
                m.Fee = method.Properties[2].ValueDouble;
                m.GenAddress = method.Properties[3].ValueBool;
            }

            return true;
        }

        public override bool GetDeposites(AccountInfo account) {
            try {
                return OnGetDeposites(account, UploadPrivateData(account, DepositAddressesApiString, DepositAddressesApiPathString, null));
            }
            catch(Exception e) {
                LogManager.Default.Log(e.ToString());
                return false;
            }
        }

        protected virtual bool OnGetDeposites(AccountInfo account, byte[] data) {
            JsonHelperToken root = JsonHelper.Default.Deserialize(data);
            if(HasError(account, root, nameof(OnGetDeposites)))
                return false;
            if(root.Properties.Length < 2)
                return true;
            JsonHelperToken balances = root.Properties[1];
            if(balances == null)
                return false;
            account.Balances.Clear();
            foreach(JsonHelperToken prop in balances.Properties) {
                KrakenAccountBalanceInfo b = new KrakenAccountBalanceInfo(account);
                b.Currency = prop.Name;
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
            string text = null;
            try {
                text = GetDownloadString(adress);
                
                if(string.IsNullOrEmpty(text))
                    return false;

                ClearTickers();
                JObject res = JsonConvert.DeserializeObject<JObject>(text);

                if(res.Value<JArray>("error").Count > 0)
                    return false;

                JObject tickers = res.Value<JObject>("result");
                if(tickers == null)
                    return true;
                foreach(var pair in tickers) {
                    JObject item = (JObject)pair.Value;
                    KrakenTicker ticker = new KrakenTicker(this);

                    string name = item.Value<string>("wsname");
                    string[] items = name.Split('/');
                    ticker.BaseCurrency = items[1]; // item.Value<string>("quote");
                    ticker.MarketCurrency = items[0]; // item.Value<string>("base");
                    ticker.CurrencyPair = item.Value<string>("altname");

                    JArray fees = item.Value<JArray>("fees");
                    if(fees != null)
                        ticker.Fee = Convert.ToDouble(((JArray)fees[0])[1]);

                    Tickers.Add(ticker);
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

        public override void OnAccountRemoved(AccountInfo info) {
            
        }

        public override TradingResult SellLong(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
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
            return false;
        }

        protected override SocketConnectionInfo CreateTickersSocket() {
            return new KrakenSocketConnectionInfo(this, null, BaseWebSocketAdress, SocketType.WebSocket, SocketSubscribeType.Tickers);
        }

        protected override void StartListenOrderBookCore(Ticker ticker) {
            if(TickersSocket == null) {
                StartListenTickersStream();
                if(!WaitUntil(5000, () => { return TickersSocketState == SocketConnectionState.Connected; }))
                    return;
            }
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.OrderBook, ticker) { command = "{ \"event\":\"subscribe\", \"pair\": [ \"" + ((KrakenTicker)ticker).StandardName + "\" ], \"subscription\": { \"name\": \"book\", \"depth\": 1000 } }" });
        }

        public override void StopListenOrderBook(Ticker ticker) {
            base.StopListenOrderBook(ticker);
            if(TickersSocket == null)
                return;
            TickersSocket.Unsubscribe(new WebSocketSubscribeInfo(SocketSubscribeType.OrderBook, ticker) { command = "{ \"event\":\"unsubscribe\", \"pair\": [ \"" + ((KrakenTicker)ticker).StandardName + "\" ], \"subscription\": { \"name\": \"book\", \"depth\": 1000 } }" });
        }

        protected override void StartListenTradeHistoryCore(Ticker ticker) {
            if(TickersSocket == null) {
                StartListenTickersStream();
                if(!WaitUntil(5000, () => { return TickersSocketState == SocketConnectionState.Connected; }))
                    return;
            }
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.TradeHistory, ticker) { command = "{ \"event\":\"subscribe\", \"pair\": [ \"" + ((KrakenTicker)ticker).StandardName + "\" ], \"subscription\": { \"name\": \"ticker\" } }" });
            TickersSocket.Subscribe(new WebSocketSubscribeInfo(SocketSubscribeType.TradeHistory, ticker) { command = "{ \"event\":\"subscribe\", \"pair\": [ \"" + ((KrakenTicker)ticker).StandardName + "\" ], \"subscription\": { \"name\": \"trade\" } }" });
            UpdateTrades(ticker);
        }

        public override void StopListenTradeHistory(Ticker ticker) {
            base.StopListenOrderBook(ticker);
            if(TickersSocket == null)
                return;
            TickersSocket.Unsubscribe(new WebSocketSubscribeInfo(SocketSubscribeType.TradeHistory, ticker) { command = "{ \"event\":\"unsubscribe\", \"pair\": [ \"" + ((KrakenTicker)ticker).StandardName + "\" ], \"subscription\": { \"name\": \"ticker\" } }" });
            TickersSocket.Unsubscribe(new WebSocketSubscribeInfo(SocketSubscribeType.TradeHistory, ticker) { command = "{ \"event\":\"unsubscribe\", \"pair\": [ \"" + ((KrakenTicker)ticker).StandardName + "\" ], \"subscription\": { \"name\": \"trade\" } }" });
        }

        protected internal override void OnTickersSocketOpened(object sender, EventArgs e) {
            base.OnTickersSocketOpened(sender, e);
        }

        protected internal override void OnTickersSocketMessageReceived(object sender, MessageReceivedEventArgs e) {
            base.OnTickersSocketMessageReceived(sender, e);
            System.Diagnostics.Debug.WriteLine(e.Message);

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
                ticker = Tickers.FirstOrDefault(tt => ((KrakenTicker)tt).StandardName == tickerName);
                if(ticker == null) {
                    LogManager.Default.Log(LogType.Error, this, "cannot find ticker", tickerName);
                    return;
                }
                OnOrderBookSocketMessageReceived(ticker, e.Message, root);
            }
            else if(channelName == "trade") {
                ticker = Tickers.FirstOrDefault(tt => ((KrakenTicker)tt).StandardName == tickerName);
                if(ticker == null) {
                    LogManager.Default.Log(LogType.Error, this, "cannot find ticker", tickerName);
                    return;
                }
                OnTradeSocketMessageReceived(ticker, e.Message, root);
            }
            else if(channelName == "ticker") {
                ticker = Tickers.FirstOrDefault(tt => ((KrakenTicker)tt).StandardName == tickerName);
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

        protected virtual string DepositAddressesApiString { get { return "http://api.kraken.com/0/private/DepositAddresses"; } }
        protected virtual string DepositAddressesApiPathString { get { return "/0/private/DepositAddresses"; } }

        protected virtual string DepositMethodsApiString { get { return "http://api.kraken.com/0/private/DepositMethods"; } }
        protected virtual string DepositMethodsApiPathString { get { return "/0/private/DepositMethods"; } }

        protected virtual string OpenedOrdersApiString { get { return "https://api.kraken.com/0/private/OpenOrders"; } }
        protected virtual string OpenedOrdersApiPathString { get { return "/0/private/OpenOrders"; } }

        protected virtual string AccountTradesApiString { get { return "http://api.kraken.com/0/private/TradesHistory"; } }
        protected virtual string AccountTradesApiPathString { get { return "/0/private/TradesHistory"; } }

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

        protected virtual byte[] UploadPrivateData(AccountInfo info, string adress, string path, string parameters) {
            MyWebClient client = GetWebClient();

            string nonce = GetNonce();
            string queryString = null;
            if(string.IsNullOrEmpty(parameters))
                queryString = string.Format("nonce={0}", nonce);
            else 
                queryString = string.Format("nonce={0}&{1}", nonce, parameters);
            string signature = info.GetSign(path, queryString, nonce);

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("API-Key", info.ApiKey);
            client.DefaultRequestHeaders.Add("API-Sign", signature);
            client.DefaultRequestHeaders.Add("User-Agent", "KrakenDotNet Client");

            return client.UploadData(adress, queryString);
        }

        public override bool UpdateBalances(AccountInfo info) {
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
            if(info.Balances == null || info.Balances.Count == 0) {
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "ZUSD" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "XXBT" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "XXRP" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "XLTC" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "XETH" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "XETC" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "XREP" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "XXMR" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "USDT" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "DASH" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "GNO" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "EOS" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "BCH" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "ADA" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "QTUM" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "XTZ" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "ATOM" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "SC" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "LSK" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "WAVES" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "ICX" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "BAT" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "OMG" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "LINK" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "DAI" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "PAXG" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "ALGO" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "USDC" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "TRX" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "DOT" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "OXT" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "ETH2.S" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "EHT2" });
                info.Balances.Add(new KrakenAccountBalanceInfo(info) { Currency = "USD.M" });
            }
        }

        public virtual bool OnGetBalances(AccountInfo account, byte[] data) {
            JsonHelperToken root = JsonHelper.Default.Deserialize(data);
            if(HasError(account, root, nameof(OnGetBalances)))
                return false;
            if(root.Properties.Length < 2)
                return true;
            JsonHelperToken balances = root.Properties[1];
            if(balances == null)
                return false;
            account.Balances.ForEach(b => b.Clear() );
            foreach(JsonHelperToken prop in balances.Properties) {
                KrakenAccountBalanceInfo b = (KrakenAccountBalanceInfo)account.Balances.FirstOrDefault(bb => bb.Currency == prop.Name);
                if(b == null) {
                    b = new KrakenAccountBalanceInfo(account);
                    account.Balances.Add(b);
                }
                b.Currency = prop.Name;
                b.OnOrders = 0;
                b.Available = b.Balance = prop.ValueDouble;
            }
            return true;
        }

        public override bool UpdateCurrencies() {
            return true;
        }

        public override bool UpdateOpenedOrders(AccountInfo info, Ticker ticker) {
            try {
                return OnGetOpenedOrders(info, UploadPrivateData(info, OpenedOrdersApiString, OpenedOrdersApiPathString, null));
            }
            catch(Exception e) {
                LogManager.Default.Log(e.ToString());
                return false;
            }
        }

        protected override int WebSocketAllowedDelayInterval => 10000;

        protected virtual bool OnGetOpenedOrders(AccountInfo info, byte[] data) {
            string text = UTF8Encoding.Default.GetString(data);
            JsonHelperToken root = JsonHelper.Default.Deserialize(data);
            if(HasError(info, root, nameof(OnGetOpenedOrders)))
                return false;
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
                List<OrderBookEntry> iasks = ticker.OrderBook.AsksInverted;

                bids.Clear();
                asks.Clear();
                if(iasks != null)
                    iasks.Clear();


                for(int i = 0; i < jbids.Count; i++) {
                    string[] item = jbids[i];
                    bids.Add(new OrderBookEntry() { ValueString = item[0], AmountString = item[1] });
                }
                for(int i = 0; i < jasks.Count; i++) {
                    string[] item = jasks[i];
                    OrderBookEntry e = new OrderBookEntry() { ValueString = item[0], AmountString = item[1] };
                    asks.Add(e);
                    if(iasks != null)
                        iasks.Insert(0, e);
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
            if(HasError(null, root, nameof(OnUpdateTrades)))
                return false;
            if(root.PropertiesCount < 2)
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

        protected override bool GetTradesCore(ResizeableArray<TradeInfoItem> list, Ticker ticker, DateTime start, DateTime end) {
            throw new NotImplementedException();
        }

        protected internal override void ApplyCapturedEvent(Ticker ticker, TickerCaptureDataInfo info) {
            throw new NotImplementedException();
        }

        protected internal override IIncrementalUpdateDataProvider CreateIncrementalUpdateDataProvider() {
            return new KrakenIncrementalUpdateDataProvider();
        }

        protected internal override HMAC CreateHmac(string secret) {
            return new HMACSHA512(Convert.FromBase64String(secret));
        }
    }
}
