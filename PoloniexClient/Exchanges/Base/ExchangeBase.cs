using DevExpress.Utils.Serializing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Utils;
using System.Collections;
using System.Security.Cryptography;
using System.Net;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using CryptoMarketClient.Common;
using System.Text.Json;
using CryptoMarketClient.Bittrex;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using CryptoMarketClient.Binance;
using DevExpress.XtraEditors;
using CryptoMarketClient.BitFinex;
using System.Reactive.Subjects;
using WebSocket4Net;
using CryptoMarketClient.Exchanges.Base;
using CryptoMarketClient.Exchanges.Bitmex;
using SuperSocket.ClientEngine;

namespace CryptoMarketClient {
    public abstract class Exchange : IXtraSerializable {
        public static List<Exchange> Registered { get; } = new List<Exchange>();
        public static List<Exchange> Connected { get; } = new List<Exchange>();


        static Exchange() {
            string directoryName = Path.GetDirectoryName(Application.ExecutablePath) + "\\Icons";
            if(!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);

            Registered.Add(PoloniexExchange.Default);
            Registered.Add(BittrexExchange.Default);
            Registered.Add(BinanceExchange.Default);
            Registered.Add(BitmexExchange.Default);
            
            //Registered.Add(BitFinexExchange.Default);
            //Registered.Add(new YobitExchange());

            foreach(Exchange exchange in Registered)
                exchange.Load();
        }

        public static Color AskColor {
            get { return Color.Red; }
        }

        public static Color BidColor {
            get { return Color.Green; }
        }

        public DateTime LastWebSocketRecvTime { get; set; }
        public abstract bool AllowCandleStickIncrementalUpdate { get; }
        public abstract void ObtainExchangeSettings();

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

        public static int OrderBookDepth { get; set; }
        public static bool AllowTradeHistory { get; set; }
        public bool IsConnected {
            get { return Connected.Contains(this); }
            set {
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
                foreach(Ticker ticker in Tickers) {
                    ticker.Load();
                }
                return true;
            }
            return false;
        }
        public abstract bool GetTickersInfo();
        public abstract bool UpdateTickersInfo();

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true)]
        public List<AccountInfo> Accounts { get; } = new List<AccountInfo>();

        public List<CurrencyInfoBase> Currencies { get; } = new List<CurrencyInfoBase>();

        protected object XtraCreateAccountsItem(XtraItemEventArgs e) {
            return new AccountInfo();
        }

        protected void XtraSetIndexAccountsItem(XtraSetItemIndexEventArgs e) {
            Accounts.Add((AccountInfo)e.Item.Value);
        }

        public List<Ticker> Tickers { get; } = new List<Ticker>();
        //public List<OpenedOrderInfo> OpenedOrders { get { return DefaultAccount.OpenedOrders; } }
        //public List<BalanceBase> Balances { get { return DefaultAccount.Balances; } }
        //public List<TradeHistoryItem> MyTrades { get { return DefaultAccount.MyTrades; } }

        public event TickerUpdateEventHandler TickerUpdate;
        public event EventHandler TickersUpdate;
        protected void RaiseTickerUpdate(Ticker t) {
            TickerUpdateEventArgs e = new TickerUpdateEventArgs() { Ticker = t };
            if(TickerUpdate != null)
                TickerUpdate(this, e);
            t.RaiseChanged();
        }
        protected void RaiseTickersUpdate() {
            if(TickersUpdate != null)
                TickersUpdate(this, EventArgs.Empty);
        }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true)]
        public List<PinnedTickerInfo> PinnedTickers { get; set; } = new List<PinnedTickerInfo>();
        PinnedTickerInfo XtraCreatePinnedTickersItem(XtraItemEventArgs e) {
            return new PinnedTickerInfo();
        }
        void XtraSetIndexPinnedTickersItem(XtraSetItemIndexEventArgs e) {
            if(e.NewIndex == -1) {
                PinnedTickers.Add((PinnedTickerInfo)e.Item.Value);
                return;
            }
            PinnedTickers.Insert(e.NewIndex, (PinnedTickerInfo)e.Item.Value);
        }

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

        #region Settings
        public void Save() {
            SaveLayoutToXml(Name + ".xml");
        }
        public string TickersDirectory { get { return Path.GetDirectoryName(Application.ExecutablePath) + "\\Tickers\\" + Name; } }
        public void Load() {
            string dir = TickersDirectory;
            if(!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if(!File.Exists(Name + ".xml"))
                return;
            RestoreLayoutFromXml(Name + ".xml");
        }

        void IXtraSerializable.OnEndDeserializing(string restoredVersion) {
        }

        void IXtraSerializable.OnEndSerializing() {

        }

        void IXtraSerializable.OnStartDeserializing(LayoutAllowEventArgs e) {

        }

        void IXtraSerializable.OnStartSerializing() {
        }

        protected XtraObjectInfo[] GetXtraObjectInfo() {
            ArrayList result = new ArrayList();
            result.Add(new XtraObjectInfo("Exchange", this));
            return (XtraObjectInfo[])result.ToArray(typeof(XtraObjectInfo));
        }
        protected virtual bool SaveLayoutCore(XtraSerializer serializer, object path) {
            System.IO.Stream stream = path as System.IO.Stream;
            if(stream != null)
                return serializer.SerializeObjects(GetXtraObjectInfo(), stream, this.GetType().Name);
            else
                return serializer.SerializeObjects(GetXtraObjectInfo(), path.ToString(), this.GetType().Name);
        }
        protected virtual void RestoreLayoutCore(XtraSerializer serializer, object path) {
            System.IO.Stream stream = path as System.IO.Stream;
            if(stream != null)
                serializer.DeserializeObjects(GetXtraObjectInfo(), stream, this.GetType().Name);
            else
                serializer.DeserializeObjects(GetXtraObjectInfo(), path.ToString(), this.GetType().Name);
        }
        //layout
        public virtual void SaveLayoutToXml(string xmlFile) {
            SaveLayoutCore(new XmlXtraSerializer(), xmlFile);
        }
        public virtual void RestoreLayoutFromXml(string xmlFile) {
            RestoreLayoutCore(new XmlXtraSerializer(), xmlFile);
            foreach(AccountInfo info in Accounts) {
                info.Exchange = Exchange.Registered.FirstOrDefault(e => e.Type == info.Type);
            }
            UpdateDefaultAccount();
        }
        #endregion

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

        protected string GetDownloadString(Ticker ticker, string address) {
            try {
                return ticker.DownloadString(address);
            }
            catch(Exception e) {
                Console.WriteLine("WebClient exception = " + e.ToString());
                return string.Empty;
            }
        }

        protected MyWebClient[] WebClientBuffer { get; } = new MyWebClient[32];
        protected int CurrentClientIndex { get; set; }
        public MyWebClient GetWebClient() {
            //for(int i = 0; i < WebClientBuffer.Length; i++) {
            //    if(WebClientBuffer[CurrentClientIndex] == null)
            //        WebClientBuffer[CurrentClientIndex] = new MyWebClient();
            //    if(!WebClientBuffer[CurrentClientIndex].IsBusy)
            //        return WebClientBuffer[CurrentClientIndex];
            //    CurrentClientIndex++;
            //    if(CurrentClientIndex >= WebClientBuffer.Length)
            //        CurrentClientIndex = 0;
            //}
            return new MyWebClient();
        }
        protected Stopwatch Timer { get; } = new Stopwatch();
        protected List<RateLimit> RequestRate { get; set; }
        protected List<RateLimit> OrderRate { get; set; }
        public bool IsInitialized { get; set; }
        protected void CheckRequestRateLimits() {
            if(RequestRate == null)
                return;
            foreach(RateLimit limit in RequestRate)
                limit.CheckAllow();
        }
        protected void CheckOrderRateLimits() {
            if(OrderRate == null)
                return;
            foreach(RateLimit limit in OrderRate)
                limit.CheckAllow();
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
                if(we != null && we.Message.Contains("418") || we.Message.Contains("429")) {
                    IsInitialized = false;
                    XtraMessageBox.Show("Exception: " + we.ToString());
                }
                Console.WriteLine("WebClient exception = " + e.ToString());
                return null;
            }
        }
        public Int32 ToUnixTimestamp(DateTime time) {
            return (Int32)(time.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
        public DateTime FromUnixTimestamp(Int64 time) {
            return new DateTime(1970, 1, 1).AddSeconds(time);
        }
        protected byte[] GetDownloadBytes(string address) {
            try {
                CheckRequestRateLimits();
                return GetWebClient().DownloadData(address);
            }
            catch(Exception e) {
                WebException we = e as WebException;
                if(we != null && we.Message.Contains("418") || we.Message.Contains("429")) {
                    IsInitialized = false;
                    XtraMessageBox.Show("Exception: " + we.ToString());
                }
                Console.WriteLine("WebClient exception = " + e.ToString());
                return null;
            }
        }
        public bool IsTickerPinned(Ticker tickerBase) {
            return PinnedTickers.FirstOrDefault(p => p.BaseCurrency == tickerBase.BaseCurrency && p.MarketCurrency == tickerBase.MarketCurrency) != null;
        }
        public Ticker GetTicker(PinnedTickerInfo info) {
            return Tickers.FirstOrDefault(t => t.BaseCurrency == info.BaseCurrency && t.MarketCurrency == info.MarketCurrency);
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
        public abstract bool UpdateArbitrageOrderBook(Ticker tickerBase, int depth);
        public abstract bool ProcessOrderBook(Ticker tickerBase, string text);
        public abstract bool UpdateTicker(Ticker tickerBase);
        public abstract bool UpdateTrades(Ticker tickerBase);
        public abstract List<TradeInfoItem> GetTrades(Ticker ticker, DateTime starTime);
        public abstract bool UpdateOpenedOrders(AccountInfo account, Ticker ticker);
        public bool UpdateOpenedOrders(AccountInfo account) { return UpdateOpenedOrders(account, null); }
        public abstract bool UpdateCurrencies();
        public abstract bool UpdateBalances(AccountInfo info);
        public abstract bool GetBalance(AccountInfo info, string currency);
        public abstract string CreateDeposit(AccountInfo account, string currency);
        public abstract bool GetDeposites(AccountInfo account);
        public abstract TradingResult Buy(AccountInfo account, Ticker ticker, double rate, double amount);
        public abstract TradingResult Sell(AccountInfo account, Ticker ticker, double rate, double amount);
        public abstract bool Cancel(AccountInfo account, string orderId);
        public virtual BindingList<CandleStickData> GetCandleStickData(Ticker ticker, int candleStickPeriodMin, DateTime start, long periodInSeconds) {
            return new BindingList<CandleStickData>();
        }
        public abstract bool Withdraw(AccountInfo account, string currency, string adress, string paymentId, double amount);
        public abstract bool UpdateAccountTrades(AccountInfo account, Ticker ticker);
        public bool UpdateAccountTrades(AccountInfo account) { return UpdateAccountTrades(account, null); }

        public bool UpdateDefaultAccountBalances() { return UpdateBalances(DefaultAccount); }
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
        public List<TradeInfoItem> GetAllAccountTrades() {
            List<TradeInfoItem> res = new List<TradeInfoItem>();
            foreach(AccountInfo account in Accounts) {
                if(!account.Active)
                    continue;
                res.AddRange(account.MyTrades);
            }
            return res;
        }
        public string CheckCreateDeposit(AccountInfo account, string currency) {
            BalanceBase b = account.Balances.FirstOrDefault(bb => bb.Currency == currency);
            if(!string.IsNullOrEmpty(b.DepositAddress))
                return b.DepositAddress;
            return CreateDeposit(account, currency);
        }

        JsonParser jsonParser;
        protected JsonParser JsonParser {
            get {
                if(jsonParser == null)
                    jsonParser = new JsonParser();
                return jsonParser;
            }
        }
        public abstract List<CandleStickIntervalInfo> GetAllowedCandleStickIntervals();
        public static Dictionary<string, string> CurrencyLogo { get; } = new Dictionary<string, string>();
        public static Dictionary<string, Image> CurrencyLogoImage { get; } = new Dictionary<string, Image>();
        public static void BuildIconsDataBase(IEnumerable<string[]> list, bool allowDownload) {
            CurrencyLogo.Clear();
            foreach(string[] str in list) {
                if(string.IsNullOrEmpty(str[0]) || string.IsNullOrEmpty(str[1]) || str[1] == "null")
                    continue;
                if(!CurrencyLogo.ContainsKey(str[0]))
                    CurrencyLogo.Add(str[0], str[1]);
                if(!CurrencyLogoImage.ContainsKey(str[0])) {
                    Image res = LoadLogoImage(str[0], allowDownload);
                    if(res != null && !CurrencyLogoImage.ContainsKey(str[0]))
                        CurrencyLogoImage.Add(str[0], res);
                }
            }
        }
        public static Image GetLogoImage(string currencyName) {
            if(string.IsNullOrEmpty(currencyName))
                return null;
            Image res = null;
            if(CurrencyLogoImage.TryGetValue(currencyName, out res))
                return res;
            try {
                res = LoadLogoImage(currencyName, false);
                if(CurrencyLogoImage.ContainsKey(currencyName))
                    CurrencyLogoImage[currencyName] = res;
                else
                    CurrencyLogoImage.Add(currencyName, res);
            }
            catch(Exception) {
                return null;
            }
            return res;
        }
        static string GetIconFileName(string currencyName) {
            return Path.GetDirectoryName(Application.ExecutablePath) + "\\Icons\\" + currencyName + ".png";
        }
        static Image LoadLogoImage(string currencyName, bool allowDownload) {
            Image res = null;
            try {
                if(string.IsNullOrEmpty(currencyName))
                    return null;
                Debug.Write("loading logo: " + currencyName);
                string fileName = GetIconFileName(currencyName);
                if(File.Exists(fileName)) {
                    Debug.WriteLine(" - done");
                    return Image.FromFile(fileName);
                }
                if(!allowDownload)
                    return null;
                string logoUrl = null;
                if(!CurrencyLogo.TryGetValue(currencyName, out logoUrl) || string.IsNullOrEmpty(logoUrl))
                    return null;
                byte[] imageData = new WebClient().DownloadData(logoUrl);
                if(imageData == null)
                    return null;
                MemoryStream stream = new MemoryStream(imageData);
                res = Image.FromStream(stream);
                res.Save(fileName);
            }
            catch(Exception e) {
                Debug.WriteLine(" - error: " + e.Message);
                return null;
            }
            return res;
        }

        protected Dictionary<WebSocket, SocketConnectionInfo> OrderBookSockets { get; } = new Dictionary<WebSocket, SocketConnectionInfo>();
        protected Dictionary<WebSocket, SocketConnectionInfo> TradeHistorySockets { get; } = new Dictionary<WebSocket, SocketConnectionInfo>();
        protected Dictionary<WebSocket, SocketConnectionInfo> KlineSockets { get; } = new Dictionary<WebSocket, SocketConnectionInfo>();

        protected virtual string GetOrderBookSocketAddress(Ticker ticker) { return string.Empty; }
        protected virtual string GetTradeSocketAddress(Ticker ticker) { return string.Empty; }

        protected SocketConnectionInfo GetConnectionInfo(Ticker ticker, CandleStickIntervalInfo info, Dictionary<WebSocket, SocketConnectionInfo> dictionary) {
            foreach(SocketConnectionInfo i in dictionary.Values) {
                if(i.Ticker == ticker && i.KlineInfo.Interval == info.Interval) {
                    return i;
                }
            }
            return null;
        }

        protected SocketConnectionInfo GetConnectionInfo(Ticker ticker, Dictionary<WebSocket, SocketConnectionInfo> dictionary) {
            foreach(SocketConnectionInfo info in dictionary.Values) {
                if(info.Ticker == ticker) {
                    return info;
                }
            }
            return null;
        }

        protected SocketConnectionInfo CreateOrderBookWebSocket(Ticker ticker) {
            SocketConnectionInfo info = new SocketConnectionInfo();
            string adress = GetOrderBookSocketAddress(ticker);
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

        protected virtual void OnOrderBookSocketMessageReceived(object sender, MessageReceivedEventArgs e) {
        }

        protected virtual void OnOrderBookSocketClosed(object sender, EventArgs e) {
        }

        protected virtual void OnOrderBookSocketOpened(object sender, EventArgs e) {
        }

        protected virtual void OnOrderBookSocketError(object sender, SuperSocket.ClientEngine.ErrorEventArgs e) {
        }

        protected SocketConnectionInfo CreateTradesWebSocket(Ticker ticker) {
            SocketConnectionInfo info = new SocketConnectionInfo();
            string adress = GetTradeSocketAddress(ticker);
            info.Ticker = ticker;
            info.Adress = adress;
            info.Socket = new WebSocket(adress, "");
            info.Socket.Error += OnTradeHistorySocketError;
            info.Socket.Opened += OnTradeHistorySocketOpened;
            info.Socket.Closed += OnTradeHistorySocketClosed;
            info.Socket.MessageReceived += OnTradeHistorySocketMessageReceived;
            info.Open();

            return info;
        }

        protected virtual void OnTradeHistorySocketOpened(object sender, EventArgs e) {
        }

        protected virtual void OnTradeHistorySocketError(object sender, SuperSocket.ClientEngine.ErrorEventArgs e) {
        }

        protected virtual void OnTradeHistorySocketMessageReceived(object sender, MessageReceivedEventArgs e) {
        }

        protected virtual void OnTradeHistorySocketClosed(object sender, EventArgs e) {
        }

        public SocketConnectionState TickersSocketState { get; set; } = SocketConnectionState.None;
        public WebSocket WebSocket { get; private set; }
        public abstract string BaseWebSocketAdress { get; }

        public virtual void StartListenTickersStream() {
            WebSocket = new WebSocket(BaseWebSocketAdress, "");
            WebSocket.Error += OnTickersSocketError;
            WebSocket.Opened += OnTickersSocketOpened;
            WebSocket.Closed += OnTickersSocketClosed;
            WebSocket.MessageReceived += OnTickersSocketMessageReceived;
            WebSocket.DataReceived += OnTickersSocketDataReceived;
            TickersSocketState = SocketConnectionState.Connecting;
            WebSocket.Open();
        }

        public virtual void StopListenTickersStream() {
            if(WebSocket != null) {
                WebSocket.Dispose();
                WebSocket = null;
            }
        }

        protected virtual void OnTickersSocketDataReceived(object sender, WebSocket4Net.DataReceivedEventArgs e) {

        }

        protected virtual void OnTickersSocketMessageReceived(object sender, MessageReceivedEventArgs e) {
            LastWebSocketRecvTime = DateTime.Now;
            TickersSocketState = SocketConnectionState.Connected;
        }

        protected virtual void OnTickersSocketClosed(object sender, EventArgs e) {
            TickersSocketState = SocketConnectionState.Disconnected;
        }

        protected virtual void OnTickersSocketOpened(object sender, EventArgs e) {
            TickersSocketState = SocketConnectionState.Connected;
            foreach(Ticker ticker in SubscribedTickers)
                StartListenTickerStream(ticker);
        }

        protected virtual void OnTickersSocketError(object sender, SuperSocket.ClientEngine.ErrorEventArgs e) {
            TickersSocketState = SocketConnectionState.Error;
            XtraMessageBox.Show("Socket error. Please contact developers. -> " + e.Exception.ToString());
        }

        public abstract Form CreateAccountForm();
        public abstract void OnAccountRemoved(AccountInfo info);
        public virtual void StartListenTickerStream(Ticker ticker) {
            if(!SubscribedTickers.Contains(ticker))
                SubscribedTickers.Add(ticker);
            StopListenTickerStream(ticker);
        }
        public virtual void StopListenTickerStream(Ticker ticker) {
            SubscribedTickers.Remove(ticker);

            SocketConnectionInfo info = GetConnectionInfo(ticker, OrderBookSockets);
            if(info != null) {

                info.Socket.Error -= OnOrderBookSocketError;
                info.Socket.Opened -= OnOrderBookSocketOpened;
                info.Socket.Closed -= OnOrderBookSocketClosed;
                info.Socket.MessageReceived -= OnOrderBookSocketMessageReceived;
                info.Close();
                info.Dispose();
                OrderBookSockets.Remove(info.Socket);
            }

            info = GetConnectionInfo(ticker, TradeHistorySockets);
            if(info != null) {
                info.Socket.Error -= OnTradeHistorySocketError;
                info.Socket.Opened -= OnTradeHistorySocketOpened;
                info.Socket.Closed -= OnTradeHistorySocketClosed;
                info.Socket.MessageReceived -= OnTradeHistorySocketMessageReceived;
                info.Close();
                info.Dispose();
                TradeHistorySockets.Remove(info.Socket);
            }
        }
        public virtual void StartListenKlineStream(Ticker ticker, CandleStickIntervalInfo info) { }
        public virtual void StopListenKlineStream(Ticker ticker, CandleStickIntervalInfo info) { }

        IncrementalUpdateQueue updates;
        public IncrementalUpdateQueue Updates {
            get {
                if(updates == null)
                    updates = new IncrementalUpdateQueue(CreateIncrementalUpdateDataProvider());
                return updates;
            }
        }
        protected internal abstract IIncrementalUpdateDataProvider CreateIncrementalUpdateDataProvider();
        protected List<Ticker> SubscribedTickers { get; } = new List<Ticker>();

        protected void OnIncrementalUpdateRecv(IncrementalUpdateQueue updates) {
            if(updates.CanApply) {
                updates.Apply();
                TickersSocketState = SocketConnectionState.Connected;
                // apply
            }
            if(updates.TooLongQueue) {
                TickersSocketState = SocketConnectionState.TooLongQue;
                // call snapshot
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

        List<CandleStickIntervalInfo> allowedCandleStickIntervals;
        public List<CandleStickIntervalInfo> AllowedCandleStickIntervals {
            get {
                if(allowedCandleStickIntervals == null)
                    allowedCandleStickIntervals = GetAllowedCandleStickIntervals();
                return allowedCandleStickIntervals;
            }
        }

        public static Color CandleStickColor { get { return BidColor; } }
        public static Color CandleStickReductionColor { get { return AskColor; } }

        Image image;
        public Image Image {
            get {
                if(image == null) {
                    string fileName = "Images\\ExchangeImages\\" + Name + ".jpg";
                    if(File.Exists(fileName)) {
                        image = Image.FromFile(fileName);
                    }
                    else {
                        fileName = "Images\\ExchangeImages\\" + Name + ".png";
                        if(File.Exists(fileName)) {
                            image = Image.FromFile(fileName);
                        }
                    }
                }
                return image;
            }
        }

        Image icon;
        public Image Icon {
            get {
                if(icon == null) {
                    string fileName = "Images\\ExchangeImages\\" + Name + "Icon.jpg";
                    if(File.Exists(fileName)) {
                        icon = Image.FromFile(fileName);
                    }
                    else {
                        fileName = "Images\\ExchangeImages\\" + Name + "Icon.png";
                        if(File.Exists(fileName)) {
                            icon = Image.FromFile(fileName);
                        }
                    }
                }
                return icon;
            }
        }
    }

    public class CandleStickIntervalInfo {
        public string Text { get; set; }
        public string Command { get; set; }
        public TimeSpan Interval { get; set; }
    }

    public enum WebSocketType {
        Tickers,
        Ticker
    }

    public enum SocketConnectionState {
        None,
        Connecting,
        Connected,
        Disconnected,
        DelayRecv,
        Error,
        TooLongQue
    }
}
