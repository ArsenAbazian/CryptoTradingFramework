using Crypto.Core.Binance;
using Crypto.Core.Bittrex;
using Crypto.Core.Common;
using Crypto.Core.Exchanges.Base;
using Crypto.Core.Exchanges.Bitmex;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using WebSocket4Net;
using Microsoft.AspNet.SignalR.Client;
using System.Threading;
using System.Xml.Serialization;
using Crypto.Core.Helpers;
using System.Reflection;
using Crypto.Core.Strategies;
using Crypto.Core.BitFinex;
using System.Threading.Tasks;
using Crypto.Core.Exchanges.Binance.Futures;
using Crypto.Core.Exchanges.Kraken;
using Crypto.Core.Exchanges.Exmo;
using XmlSerialization;

namespace Crypto.Core {
    public abstract class Exchange : ISupportSerialization {
        public static List<Exchange> Registered { get; } = new List<Exchange>();
        public static List<Exchange> Connected { get; } = new List<Exchange>();


        static Exchange() {
            Registered.Add(PoloniexExchange.Default);
            Registered.Add(BittrexExchange.Default);
            Registered.Add(BinanceExchange.Default);
            Registered.Add(BitmexExchange.Default);
            //Registered.Add(BinanceFuturesExchange.Default);
            Registered.Add(KrakenExchange.Default);
            Registered.Add(BitFinexExchange.Default);
            Registered.Add(ExmoExchange.Default);
        }

        public static List<AccountInfo> GetAccounts() {
            List<AccountInfo> res = new List<AccountInfo>();
            foreach(Exchange e in Registered) {
                res.AddRange(e.Accounts);
            }
            return res;
        }

        protected internal virtual void OnRequestCompleted(MyWebClient myWebClient) {
        }

        public Exchange() {
            FileName = Type.ToString() + ".xml";
        }

        public static Color AskColor {
            get { return Color.FromArgb(255, 227, 82, 89); }
        }

        public static Color BidColor {
            get { return Color.FromArgb(255, 97, 176, 165); }
        }

        public virtual bool SupportPositions { get { return false; } }
        protected List<Ticker> KLineListeners { get; } = new List<Crypto.Core.Ticker>();
        protected abstract bool ShouldAddKlineListener { get; }
        public void AddKLineListener(Ticker t) {
            if(!ShouldAddKlineListener)
                return;
            if(KLineListeners.Contains(t))
                return;
            lock(KLineListeners) {
                KLineListeners.Add(t);
            }
            StartKlineListenTimer();
        }

        public void RemoveKLineListener(Ticker t) {
            if(!KLineListeners.Contains(t))
                return;
            lock(KLineListeners) {
                KLineListeners.Remove(t);
            }
        }

        public string ToQueryString(HttpRequestParamsCollection nvc) {
            StringBuilder sb = new StringBuilder();

            foreach(var item in nvc) {
                sb.AppendFormat("&{0}={1}", Uri.EscapeDataString(item.Key), Uri.EscapeDataString(item.Value));
            }
            sb.Remove(0, 1);
            return sb.ToString();
        }

        public bool GetAllAccountsAddresses() {
            bool res = true;
            foreach(AccountInfo a in Accounts) {
                res &= UpdateAddresses(a);
            }
            return res;
        }

        public static List<TickerNameInfo> GetTickersNameInfo() {
            List<TickerNameInfo> list = new List<TickerNameInfo>();
            foreach(Exchange e in Exchange.Registered) {
                if(e.Tickers.Count == 0) {
                    e.LoadTickers();
                }
                for(int i = 0; i < e.Tickers.Count; i++) {
                    Ticker ticker = e.Tickers[i];
                    list.Add(new TickerNameInfo() { Exchange = e.Type, Ticker = ticker.Name, BaseCurrency = ticker.BaseCurrency, MarketCurrency = ticker.MarketCurrency });
                }
            }
            return list;
        }

        protected internal abstract void ApplyCapturedEvent(Ticker ticker, TickerCaptureDataInfo info);

        public static Exchange CreateExchange(ExchangeType exchange) {
            foreach(var e in Registered) {
                if(e.Type == exchange) {
                    ConstructorInfo info = e.GetType().GetConstructor(new Type[] { });
                    if(info == null)
                        return null;
                    return (Exchange)info.Invoke(new object[] { });
                }
            }
            //switch(exchange) {
            //    case ExchangeType.Binance:
            //        return new BinanceExchange();
            //    case ExchangeType.BitFinex:
            //        return new BitFinexExchange();
            //    case ExchangeType.Bitmex:
            //        return new BitmexExchange();
            //    case ExchangeType.Bittrex:
            //        return new BittrexExchange();
            //    case ExchangeType.Poloniex:
            //        return new PoloniexExchange();
            //}
            return null;
        }

        protected virtual DateTime GetTradesRangeEndTime(DateTime start, DateTime end) {
            return end;
        }
        protected TradeType String2TradeType(string s) {
            if(s[0] == 'S' || s[0] == 's' || s[0] == 'f') // isBuyer
                return TradeType.Sell;
            return TradeType.Buy;
        }
        protected PositionSide String2PositionSide(string s) {
            if(s[0] == 'S' || s[0] == 's')
                return PositionSide.Short;
            else if(s[0] == 'B' || s[0] == 'b')
                return PositionSide.Both;
            return PositionSide.Long;
        }
        protected virtual ResizeableArray<TradeInfoItem> GetTradesInverted(Ticker ticker, DateTime start, DateTime end) {
            List<ResizeableArray<TradeInfoItem>> lists = new List<ResizeableArray<TradeInfoItem>>(1000);
            ResizeableArray<TradeInfoItem> last = null;
            DateTime lastStart = start;
            while(true) {
                DateTime localEnd = GetTradesRangeEndTime(start, end);
                last = GetTradesCore(ticker, start, localEnd);
                if(CancellationToken.IsCancellationRequested)
                    return null;
                if(last == null || last.Count == 0)
                    break;
                Debug.WriteLine(ticker.CurrencyPair + " trade history downloaded " + last.First().Time + "-" + last.Last().Time + " items count = " + last.Count);
                DateTime newStart = last.First().Time.ToUniversalTime();
                if(lastStart == newStart)
                    break;
                lists.Add(last);
                if(last.Count == 0 || (last.Last() != null && newStart <= start))
                    break;
                lastStart = newStart;
                end = last.First().Time.ToUniversalTime().AddMilliseconds(-1);
            }
            if(lists.Count == 0)
                return new ResizeableArray<TradeInfoItem>();
            int totalCount = lists.Sum(il => il.Count);
            ResizeableArray<TradeInfoItem> res = new ResizeableArray<TradeInfoItem>(totalCount);
            for(int i = lists.Count - 1; i >= 0; i--) {
                if(res.Count == 0)
                    res.AddRange(lists[i]);
                else {
                    DateTime lastTime = res.Last().Time;
                    for(int j = 0; j < lists[i].Count; j++) {
                        if(lastTime >= lists[i][j].Time)
                            continue;
                        res.Add(lists[i][j]);
                    }
                }
            }
            return res;
        }
        protected virtual ResizeableArray<TradeInfoItem> GetTradesForward(Ticker ticker, DateTime start, DateTime end) {
            List<ResizeableArray<TradeInfoItem>> lists = new List<ResizeableArray<TradeInfoItem>>(1000);
            ResizeableArray<TradeInfoItem> last = null;
            DateTime regionalEnd = end.ToLocalTime();
            DateTime initStart = start;
            while(true) {
                DateTime localEnd = GetTradesRangeEndTime(start, end);
                last = GetTradesCore(ticker, start, localEnd);
                if(CancellationToken.IsCancellationRequested)
                    return null;
                if(last == null || last.Count == 0) {
                    if(start == initStart) { // too early
                        initStart = initStart.AddDays(1);
                        start = initStart;
                        if(start >= end)
                            break;
                        continue;
                    }
                    break;
                }
                DateTime ds = last.First().Time;
                DateTime de = last.Last().Time;
                Debug.WriteLine(ticker.CurrencyPair + " trade history downloaded " +  ds + "-" + de + " items count = " + last.Count);
                lists.Add(last);
                if(de >= regionalEnd)
                    break;
                start = last.Last().Time.ToUniversalTime().AddMilliseconds(1);
                if(localEnd == end)
                    break;
            }
            if(lists.Count == 0)
                return new ResizeableArray<TradeInfoItem>();
            int totalCount = lists.Sum(il => il.Count);
            ResizeableArray<TradeInfoItem> res = new ResizeableArray<TradeInfoItem>(totalCount);
            foreach(var list in lists) {
                res.AddRange(list);
            }
            return res;
        }
        protected virtual bool HasDescendingTradesList { get { return false; } }
        protected CancellationToken CancellationToken { get; set; }
        public virtual ResizeableArray<TradeInfoItem> GetTrades(Ticker ticker, DateTime start, DateTime end, CancellationToken token) {
            try {
                CancellationToken = token;
                if(HasDescendingTradesList)
                    return GetTradesInverted(ticker, start, end);
                long delta = (long)((end - start).TotalSeconds / 3);

                DateTime m1 = start.AddSeconds(delta);
                DateTime m2 = m1.AddSeconds(delta);

                ResizeableArray<TradeInfoItem> l1 = null, l2 = null, l3 = null;
                Parallel.Invoke(
                    () => l1 = GetTradesForward(ticker, start, m1),
                    () => l2 = GetTradesForward(ticker, m1.AddMilliseconds(1), m2),
                    () => l3 = GetTradesForward(ticker, m2.AddMilliseconds(1), end));
                ResizeableArray<TradeInfoItem> res = new ResizeableArray<TradeInfoItem>(l1.Count + l2.Count + l3.Count);
                res.AddRange(l1);
                ConcatTradeHistory(res, l2, end.ToLocalTime());
                ConcatTradeHistory(res, l3, end.ToLocalTime());
                return res;
            }
            finally {
                CancellationToken = CancellationToken.None;
            }
        }
        protected void ConcatTradeHistory(ResizeableArray<TradeInfoItem> l1, ResizeableArray<TradeInfoItem> l2, DateTime end) {
            DateTime dateTime = l1.Count == 0? DateTime.MinValue: l1.Last().Time;
            int startIndex = 0;
            for (int i = 0; i < l2.Count; i++) {
                if (l2[i].Time > dateTime) {
                    startIndex = i;
                    break;
                }
            }
            for (int i = startIndex; i < l2.Count; i++) {
                if (l2[i].Time > end)
                    break;
                l1.Add(l2[i]);
            }
        }
        protected abstract ResizeableArray<TradeInfoItem> GetTradesCore(Ticker ticker, DateTime start, DateTime end);

        public DateTime LastWebSocketRecvTime { get; set; }
        public abstract bool AllowCandleStickIncrementalUpdate { get; }
        public abstract bool ObtainExchangeSettings();

        Ticker btcUsdtTicker;
        public Ticker BtcUsdtTicker {
            get {
                if(btcUsdtTicker == null && Tickers.Count > 0) {
                    btcUsdtTicker = Tickers.FirstOrDefault(t => t.BaseCurrency == "USDT" && t.MarketCurrency == "BTC");
                    if(btcUsdtTicker == null)
                        btcUsdtTicker = Tickers.FirstOrDefault(t => t.BaseCurrency == "usdt" && t.MarketCurrency == "btc");
                }
                return btcUsdtTicker;
            }
        }

        protected internal virtual HMAC CreateHmac(string secret) {
            return new HMACSHA512(ASCIIEncoding.Default.GetBytes(secret));
        }

        public static int OrderBookDepth { get; set; }
        public static bool AllowTradeHistory { get; set; }
        [XmlIgnore]
        public bool IsConnected {
            get { return Connected.Contains(this); }
            protected set {
                if(IsConnected == value)
                    return;
                if(value) {
                    if(IsConnected)
                        return;
                    Connected.Add(this);
                }
                else {
                    if(!IsConnected)
                        Connected.Remove(this);
                }
                OnIsConnectedChanged();
            }
        }
        protected virtual void OnIsConnectedChanged() {
        }

        public abstract bool SupportWebSocket(WebSocketType type);

        static string Text { get { return "Yes, man is mortal, but that would be only half the trouble. The worst of it is that he's sometimes unexpectedly mortal—there's the trick!"; } }

        public bool LoadTickers() {
            if(GetTickersInfo()) {
                for(int i = 0; i < Tickers.Count; i++)
                    Tickers[i].Load();
                UpdateTickersInfo();
                Save();
                return true;
            }
            return false;
        }
        public abstract bool GetTickersInfo();
        public abstract bool UpdateTickersInfo();
        public virtual bool SupportCummulativeTickersUpdate { get { return true; } }

        public string GetCandleStickCommandName(int candleStickPeriodMin) {
            var info = AllowedCandleStickIntervals.FirstOrDefault(i => i.TotalMinutes == candleStickPeriodMin);
            return info?.Command;
        }

        public string GetCandleStickSubscribeChannel(int candleStickPeriodMin) {
            var info = AllowedCandleStickIntervals.FirstOrDefault(i => i.TotalMinutes == candleStickPeriodMin);
            return info?.SubscribeChannel;
        }

        public List<AccountInfo> Accounts { get; } = new List<AccountInfo>();

        [XmlIgnore]
        public Dictionary<string, CurrencyInfo> Currencies { get; } = new Dictionary<string, CurrencyInfo>();

        public void AddCurrency(CurrencyInfo currency) {
            if(Currencies.ContainsKey(currency.Currency)) {
                Currencies[currency.Currency] = currency;
                return;
            }
            Currencies.Add(currency.Currency, currency);
        }

        [XmlIgnore]
        public List<Ticker> Tickers { get; } = new List<Ticker>();
        [XmlIgnore]
        protected Dictionary<string, Ticker> TickerDictionary { get; } = new Dictionary<string, Ticker>();
        public void AddTicker(Ticker t) {
            if(TickerDictionary.ContainsKey(t.Name) || Tickers.Contains(t))
                return;
            Tickers.Add(t);
            TickerDictionary.Add(t.Name, t);
        }
        public void ClearTickers() {
            TickerDictionary.Clear();
            Tickers.Clear();
        }

        public abstract Ticker CreateTicker(string name);
        protected virtual Ticker GetOrCreateTicker(string name) {
            Ticker t = Ticker(name);
            if(t == null) {
                t = CreateTicker(name);
                AddTicker(t);
            }
            return t;
        }

        public event CancelOrderHandler OrderCanceled;
        protected internal void RaiseOrderCanceled(CancelOrderEventArgs e) {
            if(OrderCanceled != null)
                OrderCanceled.Invoke(this, e);
        }

        public event TickerUpdateEventHandler TickerChanged;
        public event EventHandler TickersUpdate;
        protected internal void RaiseTickerChanged(Ticker t) {
            TickerUpdateEventArgs e = new TickerUpdateEventArgs() { Ticker = t };
            if(TickerChanged != null)
                TickerChanged(this, e);
            t.RaiseChanged();
        }
        protected void RaiseTickersUpdate() {
            if(TickersUpdate != null)
                TickersUpdate(this, EventArgs.Empty);
        }

        public List<PinnedTickerInfo> PinnedTickers { get; set; } = new List<PinnedTickerInfo>();

        [XmlIgnore]
        protected byte[] OpenedOrdersData { get; set; }
        protected internal bool IsOpenedOrdersChanged(byte[] newBytes) {
            if(newBytes == null)
                return false;
            if(OpenedOrdersData == null || OpenedOrdersData.Length != newBytes.Length) {
                OpenedOrdersData = newBytes;
                return true;
            }
            for(int i = 0; i < newBytes.Length; i++) {
                if(OpenedOrdersData[i] != newBytes[i])
                    return true;
            }
            return false;
        }

        AccountInfo defaultAccount;
        [XmlIgnore]
        public AccountInfo DefaultAccount {
            get { return defaultAccount; }
            set {
                if(DefaultAccount == value)
                    return;
                defaultAccount = value;
                OnDefaultAccountChanged();
            }
        }
        void OnDefaultAccountChanged() {
            foreach(AccountInfo a in Accounts) {
                a.Default = a == DefaultAccount;
            }
            Save();
        }

        public abstract ExchangeType Type { get; }
        public string Name { get { return Type.ToString(); } }
        public string TickersDirectory { get { return SettingsStore.ApplicationDirectory + "\\Tickers\\" + Name; } }
        public string CaptureDataDirectory { get { return SettingsStore.ApplicationDirectory + "\\CapturedData\\" + Name; } }

        public static Exchange FromFile(ExchangeType type, Type t) {
            Exchange res = (Exchange)SerializationHelper.Current.FromFile(type.ToString() + ".xml", t);
            if(res == null) {
                ConstructorInfo ci = t.GetConstructor(new Type[] { });
                res = (Exchange)ci.Invoke(new object[] { });
            }
            string dir = res.TickersDirectory;
            try {
                if(!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                if(!Directory.Exists(res.CaptureDataDirectory))
                    Directory.CreateDirectory(res.CaptureDataDirectory);
            }
            catch(Exception) { 
            }
            return res;
        }

        public string FileName { get; set; }
        public bool Save() {
            return SerializationHelper.Current.Save(this, GetType(), null);
        }

        void ISupportSerialization.OnBeginSerialize() { }
        void ISupportSerialization.OnEndSerialize() { }
        void ISupportSerialization.OnBeginDeserialize() { }

        public virtual void OnEndDeserialize() {
            foreach(AccountInfo info in Accounts) {
                info.Exchange = this;
            }
            UpdateDefaultAccount();
        }        

        #region Encryption
        private string Encrypt(string toEncrypt, bool useHashing) {
            if(toEncrypt == null)
                return null;

            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            //System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            // Get the key from config file

            string key = Text;
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if(useHashing) {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        private string Decrypt(string cipherString, bool useHashing) {
            if(string.IsNullOrEmpty(cipherString))
                return string.Empty;
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            string key = Text;

            if(useHashing) {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        #endregion

        protected internal string GetDownloadString(Ticker ticker, string address) {
            try {
                return ticker.DownloadString(address);
            }
            catch(Exception e) {
                Console.WriteLine("WebClient exception = " + e.ToString());
                return string.Empty;
            }
        }
        protected internal bool BlockPrivateData { get; set; }
        protected int CurrentClientIndex { get; set; }
        public virtual MyWebClient GetWebClient() {
            MyWebClient cl = new MyWebClient(this);
            return cl;
        }
        public virtual int WebSocketCheckTimerInterval { get { return 5000; } }
        public virtual int WebSocketAllowedDelayInterval { get { return 10000; } }
        public virtual int OrderBookAllowedDelayInterval { get { return 10000; } }

        System.Threading.Timer webSocketCheckTimer;
        protected System.Threading.Timer WebSocketCheckTimer {
            get {
                if(webSocketCheckTimer == null)
                    webSocketCheckTimer = new System.Threading.Timer(new System.Threading.TimerCallback(OnWebSocketCheckTimer), this, 30000, WebSocketCheckTimerInterval);
                return webSocketCheckTimer;
            }
        }

        System.Threading.Timer klineListenTimer;
        protected System.Threading.Timer KLineListenTmer {
            get {
                if(klineListenTimer == null)
                    klineListenTimer = new System.Threading.Timer(new System.Threading.TimerCallback(OnKlineListenTimer), this, 30000, WebSocketCheckTimerInterval);
                return klineListenTimer;
            }
        }

        protected void StartSocketTimer() {
            if(!SimulationMode) {
                object item = WebSocketCheckTimer;
            }
        }

        protected void StartKlineListenTimer() {
            if(!SimulationMode) {
                object item = KLineListenTmer;
            }
        }

        void CheckSocketConnectionInfoDelay(List<SocketConnectionInfo> sockets) {
            for(int si = 0; si < sockets.Count; si++) {
                CheckConnection(sockets[si]);
            }
        }

        void CheckConnection(SocketConnectionInfo info) {
            if(SimulationMode)
                return;
            if(info.Reconnecting)
                return;
            if(info.State == SocketConnectionState.Waiting) {
                if(info.CheckCanReconnectNow()) {
                    OnConnectionLost(info);
                    return;
                }
            }
            if(!info.IsOpening && !info.IsOpened) {
                if(SimulationMode)
                    info.Simulate();
                else 
                    info.Open();
                return;
            }          
            if(info.State == SocketConnectionState.Error) {
                if(SimulationMode)
                    info.Simulate();
                else
                    info.Open();
                return;
            }
            if((DateTime.Now - info.LastActiveTime).TotalMilliseconds >= WebSocketAllowedDelayInterval) {
                OnConnectionLost(info);
            }
            if(info.Ticker != null) {
                if(info.Type == SocketSubscribeType.OrderBook && 
                    info.Ticker.OrderBook.LastUpdateTime != DateTime.MinValue && (DateTime.Now - info.Ticker.OrderBook.LastUpdateTime).TotalMilliseconds >= OrderBookAllowedDelayInterval) {
                    OnConnectionLost(info);
                    return;
                }
            }
            for(int i = 0; i < info.Subscribtions.Count; i++) {
                var s = info.Subscribtions[i];
                if(s.Ticker == null)
                    continue;
                if(s.Type == SocketSubscribeType.OrderBook || !s.Ticker.IsListeningOrderBook)
                    continue;
                if(s.Ticker.OrderBook.LastUpdateTime != DateTime.MinValue && (DateTime.Now - s.Ticker.OrderBook.LastUpdateTime).TotalMilliseconds >= OrderBookAllowedDelayInterval)
                    OnOrderBookConnectionLost(info, s);
            }
        }

        protected virtual void OnKlineListenTimer(object state) {
            lock(KLineListeners) {
                foreach(Ticker t in KLineListeners) {
                    t.UpdateLastCandleStick();
                }
            }
        }

        protected virtual void OnWebSocketCheckTimer(object state) {
            if(TickersSocket != null)
                CheckConnection(TickersSocket);
            CheckSocketConnectionInfoDelay(OrderBookSockets);
            CheckSocketConnectionInfoDelay(TradeHistorySockets);
            CheckSocketConnectionInfoDelay(KlineSockets);
        }

        protected virtual void OnOrderBookConnectionLost(SocketConnectionInfo info, WebSocketSubscribeInfo s) {
            LogManager.Default.Log(LogType.Error, s.Ticker, "Subscription connection lost", s.Type.ToString());
            s.Ticker.OrderBook.IsDirty = true;
            Reconnect(info);
        }

        protected virtual void OnConnectionLost(SocketConnectionInfo info) {
            LogManager.Default.Log(LogType.Error, info.Ticker, "Ticker socket connection lost", info.Type.ToString());
            if(info.Subscribtions.Count > 0) {
                foreach(var item in info.Subscribtions) {
                    LogManager.Default.Log(LogType.Error, item.Ticker, "Socket channel subscribtion lost", info.Type.ToString());
                }
            }
            Reconnect(info);
        }

        public void Reconnect(SocketConnectionInfo info) {
            info.Reconnect();
        }

        public void Reconnect() {
            if(TickersSocket != null)
                TickersSocket.Reconnect();
            for(int i = 0; i < OrderBookSockets.Count; i++)
                OrderBookSockets[i].Reconnect();
            for(int i = 0; i < TradeHistorySockets.Count; i++) {
                TradeHistorySockets[i].Reconnect();
            }
            for(int i = 0; i < KlineSockets.Count; i++) {
                KlineSockets[i].Reconnect();
            }
        }

        protected virtual void OnConnectionLost(WebSocket webSocket) {
            LogManager.Default.Log(LogType.Error, this, "main socket connection lost", "");
            TickersSocket.Reconnect();
        }

        protected Stopwatch Timer { get; } = new Stopwatch();
        protected List<RateLimit> RequestRate { get; set; }
        protected List<RateLimit> OrderRate { get; set; }

        bool isInitialized;
        public bool IsInitialized {
            get { return isInitialized; }
            set {
                if(IsInitialized == value)
                    return;
                isInitialized = value;
                OnIsInitializedChanged();
            }
        }

        protected virtual void OnIsInitializedChanged() {
            if(InitializedChanged != null)
                InitializedChanged.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler InitializedChanged;

        protected bool SuppressCheckRequestLimits { get; set; }
        protected virtual void CheckRequestRateLimits() {
            if(SuppressCheckRequestLimits)
                return;
            if(RequestRate == null) {
                try {
                    SuppressCheckRequestLimits = true;
                    if(!ObtainExchangeSettings() || RequestRate == null)
                        return;
                }
                finally {
                    SuppressCheckRequestLimits = false;
                }
            }
            for(int i = 0; i < RequestRate.Count; i++) {
                RequestRate[i].CheckAllow();
            }
        }
        public bool CanMakeRequest() {
            if(SuppressCheckRequestLimits)
                return true;
            if(RequestRate == null) {
                try {
                    SuppressCheckRequestLimits = true;
                    if(!ObtainExchangeSettings() || RequestRate == null)
                        return true;
                }
                finally {
                    SuppressCheckRequestLimits = false;
                }
            }
            for(int i = 0; i < RequestRate.Count; i++) {
                if(!RequestRate[i].IsAllow)
                    return false;
            }
            return true;
        }
        protected void CheckOrderRateLimits() {
            if(OrderRate == null)
                return;
            for(int i = 0; i < OrderRate.Count; i++) {
                OrderRate[i].CheckAllow();
            }
        }

        public static DateTime FromUnixTime(long unixTime) {
            return epoch.AddMilliseconds(unixTime).ToLocalTime();
        }
        protected static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        protected string GetDownloadString(string address) {
            try {
                CheckRequestRateLimits();
                return GetWebClient().DownloadString(address);
            }
            catch(Exception e) {
                WebException we = e as WebException;
                if(we != null && (we.Message.Contains("418") || we.Message.Contains("429")))
                    IsInitialized = false;
                LogManager.Default.Add(LogType.Error, this, "GetDownloadString", e.ToString(), address);
                Telemetry.Default.TrackException(e);
                return null;
            }
        }

        protected static DateTime UtcStart { get { return new DateTime(1970, 1, 1, 00, 00, 00, DateTimeKind.Utc); } }

        public static Int32 ToUnixTimestamp(DateTime time) {
            return (Int32)(time.Subtract(UtcStart)).TotalSeconds;
        }
        public static long ToUnixTimestampMs(DateTime time) {
            return (long)(time.Subtract(UtcStart)).TotalMilliseconds;
        }
        public DateTime FromUnixTimestamp(Int64 time) {
            return UtcStart.AddSeconds(time);
        }
        public DateTime FromUnixTimestampMs(Int64 time) {
            return UtcStart.AddMilliseconds(time);
        }
        protected internal byte[] GetDownloadBytes(string address) {
            try {
                CheckRequestRateLimits();
                return GetWebClient().DownloadData(address);
            }
            catch(Exception e) {
                WebException we = e as WebException;
                if(we != null && (we.Message.Contains("418") || we.Message.Contains("429")))
                    IsInitialized = false;
                Telemetry.Default.TrackException(e);
                return null;
            }
        }

        protected internal byte[] UploadValues(string address, HttpRequestParamsCollection coll) {
            try {
                CheckRequestRateLimits();
                return GetWebClient().UploadValues(address, coll);
            }
            catch(Exception e) {
                WebException we = e as WebException;
                if(we != null && (we.Message.Contains("418") || we.Message.Contains("429")))
                    IsInitialized = false;
                Telemetry.Default.TrackException(e);
                return null;
            }
        }

        protected internal byte[] GetDownloadBytes(string address, MyWebClient client) {
            try {
                CheckRequestRateLimits();
                return client.DownloadData(address);
            }
            catch(Exception e) {
                WebException we = e as WebException;
                if(we != null && (we.Message.Contains("418") || we.Message.Contains("429")))
                    IsInitialized = false;
                Telemetry.Default.TrackException(e);
                return null;
            }
        }

        protected internal async void GetDownloadBytesAsync(string address, Action<Task<byte[]>> continuationAction) {
            try {
                CheckRequestRateLimits();
                await GetWebClient().DownloadDataAsync(address).ContinueWith(continuationAction);
            }
            catch(Exception e) {
                WebException we = e as WebException;
                if(we != null && (we.Message.Contains("418") || we.Message.Contains("429")))
                    IsInitialized = false;
                Telemetry.Default.TrackException(e);
            }
        }
        public virtual bool SupportCandleSticksRange { get { return true; } }
        public bool IsTickerPinned(Ticker tickerBase) {
            return PinnedTickers.FirstOrDefault(p => p.BaseCurrency == tickerBase.BaseCurrency && p.MarketCurrency == tickerBase.MarketCurrency) != null;
        }
        public Ticker GetTicker(PinnedTickerInfo info) {
            for(int i = 0; i < Tickers.Count; i++) {
                Ticker t = Tickers[i];
                if(t.BaseCurrency == info.BaseCurrency && t.MarketCurrency == info.MarketCurrency) return t;
            }
            return null;
        }
        public List<OpenedOrderInfo> UpdateAllOpenedOrders() {
            List<OpenedOrderInfo> list = new List<OpenedOrderInfo>();
            foreach(AccountInfo account in Accounts) {
                if(!account.Active)
                    continue;
                UpdateOpenedOrders(account);
                list.AddRange(account.OpenedOrders);
            }
            return list;
        }
        public bool GetAllAccountsDeposites() {
            bool res = true;
            foreach(AccountInfo account in Accounts) {
                if(!account.Active)
                    continue;
                res &= GetDeposites(account);
            }
            return res;
        }
        public abstract bool UpdateOrderBook(Ticker tickerBase);
        public abstract bool UpdateOrderBook(Ticker tickerBase, int depth);
        public abstract void UpdateOrderBookAsync(Ticker tickerBase, int depth, Action<OperationResultEventArgs> onOrderBookUpdated);
        public abstract bool UpdateTicker(Ticker tickerBase);
        public abstract bool UpdateTrades(Ticker tickerBase);
        public ResizeableArray<TradeInfoItem> GetTrades(Ticker ticker, DateTime startTime) { 
            return GetTrades(ticker, startTime, DateTime.UtcNow, CancellationToken.None);
        }
        public abstract bool UpdateOpenedOrders(AccountInfo account, Ticker ticker);
        public bool UpdateOpenedOrders(AccountInfo account) { return UpdateOpenedOrders(account, null); }
        public abstract bool UpdateCurrencies();
        public virtual CurrencyInfo CreateCurrency(string currency) { return new CurrencyInfo(this, currency); }
        public abstract BalanceBase CreateAccountBalance(AccountInfo info, string currency);
        public abstract bool UpdateBalances(AccountInfo info);
        public virtual bool UpdateAddresses(AccountInfo account) { return true; }
        public abstract bool GetBalance(AccountInfo info, string currency);
        public virtual bool UpdatePositions(AccountInfo account, Ticker ticker) { return true; }
        public abstract bool CreateDeposit(AccountInfo account, string currency);
        public abstract bool GetDeposites(AccountInfo account);
        public bool GetDeposit(BalanceBase b) { return GetDeposit(b.Account, b.CurrencyInfo); }
        public abstract bool GetDeposit(AccountInfo account, CurrencyInfo currency);
        public TradingResult Buy(AccountInfo account, Ticker ticker, double rate, double amount) { return BuyLong(account, ticker, rate, amount); }
        public TradingResult Sell(AccountInfo account, Ticker ticker, double rate, double amount) { return SellLong(account, ticker, rate, amount); }
        public abstract TradingResult BuyLong(AccountInfo account, Ticker ticker, double rate, double amount);
        public abstract TradingResult SellLong(AccountInfo account, Ticker ticker, double rate, double amount);

        public abstract TradingResult BuyShort(AccountInfo account, Ticker ticker, double rate, double amount);
        public abstract TradingResult SellShort(AccountInfo account, Ticker ticker, double rate, double amount);
        public virtual bool AllowCheckOrderStatus { get { return false; } }
        
        public abstract bool Cancel(AccountInfo account, Ticker ticker, string orderId);
        public virtual ResizeableArray<CandleStickData> GetRecentCandleStickData(Ticker ticker, int candleStickPeriodMin) {
            return GetCandleStickData(ticker, candleStickPeriodMin, DateTime.Now.AddHours(-12), 12 * 60 * 60);
        }
        public virtual ResizeableArray<CandleStickData> GetCandleStickData(Ticker ticker, int candleStickPeriodMin, DateTime start, long periodInSeconds) {
            return new ResizeableArray<CandleStickData>();
        }
        public abstract bool Withdraw(AccountInfo account, string currency, string adress, string paymentId, double amount);
        public abstract bool UpdateAccountTrades(AccountInfo account, Ticker ticker);
        public bool UpdateAccountTrades(Ticker ticker) {
            if(DefaultAccount == null)
                return true;
            return UpdateAccountTrades(DefaultAccount, ticker); 
        }
        public bool UpdateAccountTrades(AccountInfo account) { return UpdateAccountTrades(account, null); }

        public bool UpdateDefaultAccountBalances() {
            if(DefaultAccount == null)
                return true;
            return UpdateBalances(DefaultAccount); 
        }
        public bool UpdateAllAccountsBalances() {
            bool res = true;
            foreach(AccountInfo account in Accounts) {
                if(!account.Active)
                    continue;
                res &= UpdateBalances(account);
            }
            return res;
        }
        public bool UpdateAllAccountsTrades() {
            bool res = true;
            foreach(AccountInfo account in Accounts) {
                if(!account.Active)
                    continue;
                res &= UpdateAccountTrades(account);
            }
            return res;
        }
        public string CheckCreateDeposit(AccountInfo account, string currency) {
            BalanceBase b = account.Balances.FirstOrDefault(bb => bb.Currency == currency);
            if(!string.IsNullOrEmpty(b.DepositAddress))
                return b.DepositAddress;
            if(CreateDeposit(account, currency)) {
                var info = account.Balances.FirstOrDefault(bb => bb.Currency == currency);
                if(info != null)
                    return info.DepositAddress;
            }
            return null;
        }
        
        public abstract List<CandleStickIntervalInfo> GetAllowedCandleStickIntervals();
        
        [XmlIgnore]
        public List<SocketConnectionInfo> OrderBookSockets { get; } = new List<SocketConnectionInfo>();
        [XmlIgnore]
        public List<SocketConnectionInfo> TradeHistorySockets { get; } = new List<SocketConnectionInfo>();
        [XmlIgnore]
        public List<SocketConnectionInfo> KlineSockets { get; } = new List<SocketConnectionInfo>();

        protected virtual string GetOrderBookSocketAddress(Ticker ticker) { return string.Empty; }
        protected virtual string GetTradeSocketAddress(Ticker ticker) { return string.Empty; }
        protected virtual string GetKlineSocketAddress(Ticker ticker) { return string.Empty; }

        public SocketConnectionState GetOrderBookSocketState(Ticker ticker) {
            SocketConnectionInfo info = GetConnectionInfo(ticker, OrderBookSockets);
            return info == null ? SocketConnectionState.Disconnected : info.State;
        }
        public SocketConnectionState GetTradingHistorySocketState(Ticker ticker) {
            SocketConnectionInfo info = GetConnectionInfo(ticker, TradeHistorySockets);
            return info == null ? SocketConnectionState.Disconnected : info.State;
        }
        public SocketConnectionState GetKlineSocketState(Ticker ticker) {
            SocketConnectionInfo info = GetConnectionInfo(ticker, KlineSockets);
            return info == null ? SocketConnectionState.Disconnected : info.State;
        }

        protected SocketConnectionInfo GetConnectionInfo(Ticker ticker, CandleStickIntervalInfo info, List<SocketConnectionInfo> sockets) {
            for(int si = 0; si < sockets.Count; si++) {
                SocketConnectionInfo i = sockets[si];
                if(i.Ticker == ticker && i.KlineInfo.Interval == info.Interval) {
                    return i;
                }
            }
            return null;
        }

        protected SocketConnectionInfo GetConnectionInfo(Ticker ticker, List<SocketConnectionInfo> sockets) {
            for(int si = 0; si < sockets.Count; si++) {
                SocketConnectionInfo info = sockets[si];
                if(info.Ticker == ticker) {
                    return info;
                }
            }
            return null;
        }

        protected SocketConnectionInfo CreateOrderBookWebSocket(Ticker ticker) {
            return new SocketConnectionInfo(this, ticker, GetOrderBookSocketAddress(ticker), SocketType.WebSocket, SocketSubscribeType.OrderBook);
        }

        protected internal virtual void OnOrderBookSocketMessageReceived(object sender, MessageReceivedEventArgs e) {

        }

        protected internal virtual void OnOrderBookSocketClosed(object sender, EventArgs e) {
            SocketConnectionInfo info = OrderBookSockets.FirstOrDefault(c => c.Key == sender);
            if(info != null) {
                info.Ticker.IsOrderBookSubscribed = false;
                LogManager.Default.Log(LogType.Log, info.Ticker, "order book socket closed", e.ToString());
            }
            else
                LogManager.Default.Log(LogType.Log, this, "order book socket closed", e.ToString());
        }

        protected internal virtual void OnOrderBookSocketOpened(object sender, EventArgs e) {
            SocketConnectionInfo info = OrderBookSockets.FirstOrDefault(c => c.Key == sender);
            if(info != null) {
                info.Ticker.UpdateOrderBook();
                info.Ticker.IsOrderBookSubscribed = true;
                LogManager.Default.Log(LogType.Success, info.Ticker, "order book socket opened", "");
            }
            else
                LogManager.Default.Log(LogType.Success, this, "order book socket opened", "");
        }

        protected internal virtual void OnOrderBookSocketError(object sender, SuperSocket.ClientEngine.ErrorEventArgs e) {
            SocketConnectionInfo info = OrderBookSockets.FirstOrDefault(c => c.Key == sender);
            if(info != null) {
                bool isReconnecting = info.Reconnecting;
                info.Reconnecting = false;
                info.Ticker.OrderBook.IsDirty = true;
                LogManager.Default.Log(LogType.Error, info.Ticker, "order book socket error", e.Exception.Message);
                if(!isReconnecting)
                    Reconnect(info);
                else
                    info.LastActiveTime = DateTime.Now;
            }
            else
                LogManager.Default.Log(LogType.Error, this, "order book socket error", e.Exception.Message);
        }

        protected SocketConnectionInfo CreateTradesWebSocket(Ticker ticker) {
            return new SocketConnectionInfo(this, ticker, GetTradeSocketAddress(ticker), SocketType.WebSocket, SocketSubscribeType.TradeHistory);
        }

        protected SocketConnectionInfo CreateKlineWebSocket(Ticker ticker) {
            string adress = GetKlineSocketAddress(ticker);
            if(string.IsNullOrEmpty(adress))
                return null;
            return new SocketConnectionInfo(this, ticker, GetKlineSocketAddress(ticker), SocketType.WebSocket, SocketSubscribeType.Kline);
        }

        protected internal virtual void OnKlineSocketMessageReceived(object sender, MessageReceivedEventArgs e) { }

        protected internal virtual void OnKlineSocketClosed(object sender, EventArgs e) {
            SocketConnectionInfo info = KlineSockets.FirstOrDefault(c => c.Key == sender);
            if(info != null)
                LogManager.Default.Log(LogType.Log, info.Ticker, "Kline socket closed", "");
            else
                LogManager.Default.Log(LogType.Log, this, "Kline socket closed", "");
        }

        protected internal virtual void OnKlineSocketOpened(object sender, EventArgs e) {
            SocketConnectionInfo info = KlineSockets.FirstOrDefault(c => c.Key == sender);
            LogManager.Default.Log(LogType.Success, info.Ticker, "Kline socket opened", "");
        }

        protected internal virtual void OnKlineSocketError(object sender, SuperSocket.ClientEngine.ErrorEventArgs e) {
            SocketConnectionInfo info = KlineSockets.FirstOrDefault(c => c.Key == sender);
            if(info != null) {
                bool isReconnecting = info.Reconnecting;
                info.Reconnecting = false;
                LogManager.Default.Log(LogType.Error, info.Ticker, "Kline socket error", e.Exception.Message);
                if(!isReconnecting)
                    info.Reconnect();
                else
                    info.LastActiveTime = DateTime.Now;
            }
            else
                LogManager.Default.Log(LogType.Error, this, "kline socket error", e.Exception.Message);
        }

        protected internal virtual void OnTradeHistorySocketOpened(object sender, EventArgs e) {
            SocketConnectionInfo info = TradeHistorySockets.FirstOrDefault(c => c.Key == sender);
            if(info != null) {
                info.Ticker.ClearTradeHistory();
                LogManager.Default.Log(LogType.Success, info.Ticker, "trade history socket opened", e.ToString());
            }
            else
                LogManager.Default.Log(LogType.Success, this, "trade history socket opened", e.ToString());
        }

        protected internal virtual void OnTradeHistorySocketError(object sender, SuperSocket.ClientEngine.ErrorEventArgs e) {
            SocketConnectionInfo info = TradeHistorySockets.FirstOrDefault(c => c.Key == sender);
            if(info != null) {
                bool isReconnecting = info.Reconnecting;
                info.Reconnecting = false;
                LogManager.Default.Log(LogType.Error, info.Ticker, "trade history socket error", e.Exception.Message);
                if(!isReconnecting)
                    info.Reconnect();
                info.LastActiveTime = DateTime.Now;
            }
            else
                LogManager.Default.Log(LogType.Error, this, "trade history socket error", e.Exception.Message);
        }

        protected internal virtual void OnTradeHistorySocketMessageReceived(object sender, MessageReceivedEventArgs e) {
        }

        protected internal virtual void OnTradeHistorySocketClosed(object sender, EventArgs e) {
            SocketConnectionInfo info = TradeHistorySockets.FirstOrDefault(c => c.Key == sender);
            if(info != null) {
                LogManager.Default.Log(LogType.Log, info.Ticker, "trade history socket closed", e.ToString());
            }
            else
                LogManager.Default.Log(LogType.Log, this, "trade history socket closed", e.ToString());
        }

        public SocketConnectionState TickersSocketState {
            get { return TickersSocket == null ? SocketConnectionState.Disconnected : TickersSocket.State; }
        }
        [XmlIgnore]
        public SocketConnectionInfo TickersSocket { get; protected set; }
        //public WebSocket WebSocket { get; private set; }
        public abstract string BaseWebSocketAdress { get; }

        public delegate bool WaitCondition();
        protected bool WaitUntil(long msToWait, WaitCondition condition) {
            Stopwatch w = new Stopwatch();
            w.Start();
            while(!condition() && w.ElapsedMilliseconds < msToWait) {
                Thread.Sleep(100);
                //Application.DoEvents();
            }
            return condition();
        }

        protected virtual SocketConnectionInfo CreateTickersWebSocket() {
            return new SocketConnectionInfo(this, null, BaseWebSocketAdress, SocketType.WebSocket, SocketSubscribeType.Tickers);
        }

        public virtual void StartListenTickersStream() {
            if(TickersSocket != null) {
                TickersSocket.AddRef();
                return;
            }
            TickersSocket = CreateTickersWebSocket();
            TickersSocket.StateChanged += OnTickersSocketStateChanged;
            if(!SimulationMode)
                TickersSocket.Open();
            else
                TickersSocket.Simulate();
            TickersSocket.AddRef();
        }

        private void OnTickersSocketStateChanged(object sender, ConnectionInfoChangedEventArgs e) {
            if (TickersSocketStateChanged != null)
                TickersSocketStateChanged(this, e);
        }

        public event ConnectionInfoChangedEventHandler TickersSocketStateChanged;
        
        public virtual void StopListenTickersStream() {
            StopListenTickersStream(false);
        }
        public virtual void StopListenTickersStream(bool force) {
            LogManager.Default.Log(LogType.Log, this, "stop listening tickers stream", "");
            if(TickersSocket == null)
                return;
            if(force)
                TickersSocket.ClearRef();
            TickersSocket.Release();
            if(!TickersSocket.HasRef())
                TickersSocket = null;
        }

        protected internal virtual void OnTickersSocketMessageReceived(object sender, MessageReceivedEventArgs e) {
            if(TickersSocket == null)
                return;
            LastWebSocketRecvTime = DateTime.Now;
        }

        protected internal virtual void OnTickersSocketClosed(object sender, EventArgs e) {
            if(TickersSocket == null)
                return;
            LogManager.Default.Log(LogType.Log, this, "tickers socket closed", "");
        }

        protected internal virtual void OnTickersSocketOpened(object sender, EventArgs e) {
            LogManager.Default.Log(LogType.Success, this, "tickers socket opened", "");
        }

        protected internal virtual void OnTickersSocketError(object sender, SuperSocket.ClientEngine.ErrorEventArgs e) {
            bool isReconnecting = TickersSocket.Reconnecting;
            TickersSocket.Reconnecting = false;
            LogManager.Default.Log(LogType.Error, this, "tickers socket error", e.Exception.Message);
            if(!isReconnecting)
                TickersSocket.Reconnect();
            else
                TickersSocket.LastActiveTime = DateTime.Now; // skip 10 second...
        }

        public bool Connect() {
            if(IsConnected)
                return true;
            if(!ObtainExchangeSettings())
                return false;
            if(!LoadTickers())
                return false;
            StartSocketTimer();
            IsConnected = true;
            return true;
        }

        public virtual string GetSign(HttpRequestParamsCollection coll, AccountInfo info) {
            return GetSign(coll.ToString(), info);
        }

        public virtual string GetSign(string path, string text, string nonce, AccountInfo info) {
            return GetSign(text, info);
        }

        public virtual string GetSign(string text, AccountInfo info) {
            byte[] data = Encoding.ASCII.GetBytes(text);
            byte[] hash = info.HmacSha.ComputeHash(data);
            return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
        }

        //public abstract Form CreateAccountForm();
        public virtual void OnAccountRemoved(AccountInfo info) {
            DefaultAccount = Accounts.FirstOrDefault(a => a.Default);
            if(AccountsChanged != null)
                AccountsChanged(this, EventArgs.Empty);
        }
        public event EventHandler AccountsChanged;
        public virtual void StartListenTickerStream(Ticker ticker) {
            if(!SupportWebSocket(WebSocketType.Ticker))
                return;
            StartListenTickerInfo(ticker);
            StartListenOrderBook(ticker);
            StartListenTradeHistory(ticker);
            StartListenKline(ticker);
        }

        protected virtual void StartListenTickerInfo(Ticker ticker) {
            
        }

        public override string ToString() {
            return Type.ToString();
        }
        public virtual void StopListenTickerStream(Ticker ticker) {
            StopListenTickerInfo(ticker);
            StopListenOrderBook(ticker);
            StopListenTradeHistory(ticker);
            StopListenKline(ticker);
        }

        protected virtual void StopListenTickerInfo(Ticker ticker) {
            
        }

        public void StopListenKline(Ticker ticker) {
            StopListenKline(ticker, false);
        }

        public virtual void StopListenKline(Ticker ticker, bool force) {
            ReleaseKline(ticker, force);
        }

        public void StopListenOrderBook(Ticker ticker) {
            StopListenOrderBook(ticker, false);
        }

        public virtual void StopListenOrderBook(Ticker ticker, bool force) {
            ReleaseOrderBook(ticker, force);
        }

        public void StopListenTradeHistory(Ticker ticker) {
            StopListenTradeHistory(ticker, false);
        }
        public virtual void StopListenTradeHistory(Ticker ticker, bool force) {
            ReleaseTradeHistory(ticker, force);
        }

        protected virtual void StartListenOrderBookCore(Ticker ticker) {
            SocketConnectionInfo info = CreateOrderBookWebSocket(ticker);
            OrderBookSockets.Add(info);
            if(!SimulationMode)
                info.Open();
            else
                info.Simulate();
        }

        protected virtual void StartListenKlineCore(Ticker ticker) {
            SocketConnectionInfo info = CreateKlineWebSocket(ticker);
            if(info == null)
                return;
            KlineSockets.Add(info);
            if(!SimulationMode)
                info.Open();
            else
                info.Simulate();
        }

        protected virtual void StartListenTradeHistoryCore(Ticker ticker) {
            SocketConnectionInfo info = CreateTradesWebSocket(ticker);
            TradeHistorySockets.Add(info);
            if(!SimulationMode)
                info.Open();
            else
                info.Simulate();
        }

        public virtual void StartListenOrderBook(Ticker ticker) {
            if(IsListeningOrderBook(ticker)) {
                AddRefOrderBook(ticker);
                return;
            }
            StartListenOrderBookCore(ticker);
            AddRefOrderBook(ticker);
        }

        public virtual void StartListenTradeHistory(Ticker ticker) {
            if(IsListeningTradeHistory(ticker)) {
                AddRefTradeHistory(ticker);
                return;
            }
            StartListenTradeHistoryCore(ticker);
            AddRefTradeHistory(ticker);
        }

        public virtual void StartListenKline(Ticker ticker) {
            if(IsListeningKline(ticker)) {
                AddRefKline(ticker);
                return;
            }
            StartListenKlineCore(ticker);
            AddRefKline(ticker);
        }

        IncrementalUpdateQueue updates;
        public IncrementalUpdateQueue Updates {
            get {
                if(updates == null)
                    updates = new IncrementalUpdateQueue(CreateIncrementalUpdateDataProvider());
                return updates;
            }
        }

        protected internal abstract IIncrementalUpdateDataProvider CreateIncrementalUpdateDataProvider();

        protected internal virtual void OnSignalSocketError(Exception e) {
            LogManager.Default.Log(LogType.Error, this, "on socket error", e.Message);
            TickersSocket.Reconnect();
        }

        protected internal virtual void OnSignalConnectionClosed() {
            LogManager.Default.Log(LogType.Log, this, "connection closed", "");
        }

        protected internal virtual void OnSignalStateChanged(StateChange e) {
            LogManager.Default.Log(this, "socket state changed", e.NewState.ToString());
        }

        protected internal virtual void OnSignalReceived(string s) {

        }

        protected void OnIncrementalUpdateRecv(IncrementalUpdateQueue updates) {
            if(updates.CanApply) {
                updates.Apply();
                // apply
            }
            if(updates.TooLongQueue) {
                // call snapshot
                //throw new IndexOutOfRangeException("too long que");
            }
        }
        public void UpdateDefaultAccount() {
            DefaultAccount = Accounts.FirstOrDefault(a => a.Default);
            Save();
        }
        public List<BalanceBase> GetAllBalances() {
            List<BalanceBase> res = new List<BalanceBase>();
            foreach(AccountInfo account in Accounts) {
                if(account.Active)
                    res.AddRange(account.Balances);
            }
            return res;
        }

        public List<OpenedOrderInfo> GetAllOpenedOrders() {
            List<OpenedOrderInfo> res = new List<OpenedOrderInfo>();
            foreach(AccountInfo account in Accounts) {
                if(account.Active)
                    res.AddRange(account.OpenedOrders);
            }
            return res;
        }

        public static Exchange Get(ExchangeType exchange) {
            return Registered.FirstOrDefault(e => e.Type == exchange);
        }
        public Ticker Ticker(string ticker) {
            Ticker t = null;
            TickerDictionary.TryGetValue(ticker, out t);
            return t;
        }

        public int GetRequestFillPercent() {
            int maxValue = 0;
            if(RequestRate == null)
                return 0;
            foreach(RateLimit limit in RequestRate) {
                maxValue = Math.Max(maxValue, limit.GetFillPercent());
            }
            return maxValue;
        }

        public Ticker Ticker(string baseCurrency, string marketCurrency) {
            for(int i = 0; i < Tickers.Count; i++) {
                Ticker t = Tickers[i];
                if(t.BaseCurrency == baseCurrency && t.MarketCurrency == marketCurrency) return t;
            }
            return null;
        }
        public virtual void StopListenStreams(bool force) {
            StopListenTickersStream(force);

            while(KlineSockets.Count > 0)
                StopListenKline(KlineSockets.First().Ticker, force);
            while(OrderBookSockets.Count > 0)
                StopListenOrderBook(OrderBookSockets.First().Ticker, force);
            while(TradeHistorySockets.Count > 0)
                StopListenTradeHistory(TradeHistorySockets.First().Ticker, force);
        }
        public void Disconnect() {
            IsConnected = false;
            StopListenStreams(true);
        }

        protected virtual void AddRefOrderBook(Ticker ticker) {
            SocketConnectionInfo info = OrderBookSockets.FirstOrDefault(i => i.Ticker == ticker);
            if(info != null) info.AddRef();
        }
        protected virtual void AddRefTradeHistory(Ticker ticker) {
            SocketConnectionInfo info = TradeHistorySockets.FirstOrDefault(i => i.Ticker == ticker);
            if(info != null) info.AddRef();
        }
        protected virtual void AddRefKline(Ticker ticker) {
            SocketConnectionInfo info = KlineSockets.FirstOrDefault(i => i.Ticker == ticker);
            if(info != null) info.AddRef();
        }
        protected virtual void ReleaseOrderBook(Ticker ticker) {
            ReleaseOrderBook(ticker, false);
        }
        protected virtual void ReleaseOrderBook(Ticker ticker, bool force) {
            SocketConnectionInfo info = OrderBookSockets.FirstOrDefault(i => i.Ticker == ticker);
            if(info == null)
                return;
            if(force)
                info.ClearRef();
            info.Release();
            if(!info.HasRef())
                OrderBookSockets.Remove(info);
        }
        protected virtual void ReleaseTradeHistory(Ticker ticker) {
            ReleaseTradeHistory(ticker, false);
        }
        protected virtual void ReleaseTradeHistory(Ticker ticker, bool force) {
            SocketConnectionInfo info = TradeHistorySockets.FirstOrDefault(i => i.Ticker == ticker);
            if(info == null)
                return;
            if(force)
                info.ClearRef();
            info.Release();
            if(!info.HasRef())
                TradeHistorySockets.Remove(info);
        }

        protected virtual void ReleaseKline(Ticker ticker) {
            ReleaseKline(ticker, false);
        }
        protected virtual void ReleaseKline(Ticker ticker, bool force) {
            SocketConnectionInfo info = KlineSockets.FirstOrDefault(i => i.Ticker == ticker);
            if(info == null)
                return;
            if(force)
                info.ClearRef();
            info.Release();
            if(!info.HasRef())
                KlineSockets.Remove(info);
        }

        protected virtual bool IsListeningOrderBook(Ticker ticker) {
            if(GetConnectionInfo(ticker, OrderBookSockets) != null)
                return true;
            return false;
        }

        protected virtual bool IsListeningTradeHistory(Ticker ticker) {
            if(GetConnectionInfo(ticker, TradeHistorySockets) != null)
                return true;
            return false;
        }

        protected virtual bool IsListeningKline(Ticker ticker) {
            if(GetConnectionInfo(ticker, KlineSockets) != null)
                return true;
            return false;
        }

        public void OnSocketInfoStateChanged(object sender, ConnectionInfoChangedEventArgs e) {
            
        }
        public static AccountInfo GetAccount(Guid accountId) {
            foreach(Exchange e in Exchange.Registered) {
                AccountInfo info = e.Accounts.FirstOrDefault(a => a.Id == accountId);
                if(info != null)
                    return info;
            }
            return null;
        }
        public Ticker GetTicker(string tickerName) {
            for(int i = 0; i < Tickers.Count; i++) {
                Ticker t = Tickers[i];
                if(t.Name == tickerName) return t;
            }
            return null;
        }

        public virtual bool Connect(TickerInputInfo info) {
            if(info.Ticker == null)
                return false;
            if(info.UseOrderBook) {
                StartListenOrderBook(info.Ticker);
            }
            if(info.UseTradeHistory) {
                StartListenTradeHistory(info.Ticker);
            }
            info.Ticker.CandleStickPeriodMin = info.KlineIntervalMin;
            if(info.UseKline) {
                int seconds = info.KlineIntervalMin * 60 * 1000;
                info.Ticker.CandleStickData = info.Ticker.GetCandleStickData(info.Ticker.CandleStickPeriodMin, DateTime.UtcNow.AddSeconds(-seconds), seconds);
                StartListenKline(info.Ticker);
            }
            return true;
        }

        public virtual bool Disconnect(TickerInputInfo info) {
            if(info.Ticker == null)
                return false;
            if(info.UseOrderBook)
                StopListenOrderBook(info.Ticker);
            if(info.UseTradeHistory)
                StopListenTradeHistory(info.Ticker);
            if(info.UseKline)
                StopListenKline(info.Ticker);
            return true;
        }
        
        protected bool SimulationMode { get; set; }
        public virtual bool SupportSimulation { get { return true; } }
        public virtual void EnterSimulationMode() {
            SimulationMode = true;
        }
        public virtual void ExitSimulationMode() {
            SimulationMode = false;
            TickersSocket = null;
            OrderBookSockets.Clear();
            TradeHistorySockets.Clear();
            KlineSockets.Clear();
        }

        public string[] GetMarkets()
        {
            List<string> markets = new List<string>();
            foreach(Ticker t in Tickers)
            {
                if (!markets.Contains(t.BaseCurrency))
                    markets.Add(t.BaseCurrency);
            }
            return markets.ToArray();
        }
        public CurrencyInfo GetOrCreateCurrency(string currency) {
            CurrencyInfo c = null;
            if(Currencies.TryGetValue(currency, out c))
                return c;
            c = CreateCurrency(currency);
            Currencies.Add(currency, c);
            return c;
        }

        public virtual bool GetDepositMethods(AccountInfo account, CurrencyInfo currency) {
            return false;
        }

        public virtual bool SupportMultipleDepositMethods { get { return false; } }

        List<CandleStickIntervalInfo> allowedCandleStickIntervals;
        [XmlIgnore]
        public List<CandleStickIntervalInfo> AllowedCandleStickIntervals {
            get {
                if(allowedCandleStickIntervals == null)
                    allowedCandleStickIntervals = GetAllowedCandleStickIntervals();
                return allowedCandleStickIntervals;
            }
        }

        public static Color CandleStickColor { get { return BidColor; } }
        public static Color CandleStickReductionColor { get { return AskColor; } }

        public virtual bool SupportBuySellVolume { get { return false; } }

        public bool AllowTickerHistory { get; set; } = false;
        public virtual int TradesSimulationIntervalHr { get { return 24; } }
    }

    public class CandleStickIntervalInfo {
        public CandleStickIntervalInfo() {
            Interval = TimeSpan.FromMinutes(1);
        }
        public string Text { get; set; }
        public string Command { get; set; }
        public string SubscribeChannel { get; set; }
        public int TotalMinutes { get { return (int)Interval.TotalMinutes; } set { Interval = TimeSpan.FromMinutes(value); } }
        public TimeSpan Interval { get; set; }
    }

    public enum WebSocketType {
        Tickers,
        Ticker,
        Trades,
        OrderBook,
        Kline
    }

    public enum SocketConnectionState {
        None,
        Connecting,
        Connected,
        Disconnecting,
        Disconnected,
        DelayRecv,
        Error,
        TooLongQue,
        Waiting
    }

    public class OperationResultEventArgs : EventArgs {
        public Ticker Ticker { get; set; }
        public bool Result { get; set; }
    }
}
