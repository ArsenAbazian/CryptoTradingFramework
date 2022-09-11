using Crypto.Core.Exchanges.Bittrex;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using WebSocket4Net;
using static Crypto.Core.Exchanges.Bittrex.SignalWebSocket;

namespace Crypto.Core.Common {
    public class SocketConnectionInfo {
        public SocketConnectionInfo(Exchange exchange, Ticker ticker, string address, SocketType socketType, SocketSubscribeType connType) {
            if(connType == SocketSubscribeType.OrderBook ||
                connType == SocketSubscribeType.TradeHistory ||
                connType == SocketSubscribeType.Kline) {
                if(ticker == null)
                    throw new Exception("Ticker should not be null");
            }
            Ticker = ticker;
            Exchange = exchange;
            Name = ticker == null ? Exchange.Name : ticker.Name;
            Type = connType;
            SocketType = socketType;
            Address = address;
        }
        public SocketConnectionInfo(string name, SocketSubscribeType type) {
            Name = name;
            Type = type;
        }

        public Action<string, object> OnMessage { get; set; }
        public string Address { get; set; }
        public Exchange Exchange { get; set; }
        [DisplayName("Exchange")]
        public ExchangeType ExchangeType { get { return Exchange.Type; } }
        public bool IsDelayed(int ms) {
            return (DateTime.Now - LastActiveTime).TotalMilliseconds > ms;
        }
        DateTime lastActiveTime;
        public DateTime LastActiveTime {
            get {
                if(Socket != null) {
                    return lastActiveTime > Socket.LastActiveTime? lastActiveTime: Socket.LastActiveTime;
                }
                if(Signal != null)
                    return lastActiveTime > Signal.LastActiveTime? lastActiveTime: Signal.LastActiveTime;
                return DateTime.MinValue;
            }
            set {
                if(LastActiveTime == value)
                    return;
                lastActiveTime = value;
            }
        }
        public TimeSpan ConnectionTime { get { return LastActiveTime - OpenTime; } }
        [DisplayName("Connection Lost Count")]
        public int ConnectionLostCount { get; set; }
        public bool ClosedByUser { get; set; }
        public string Name { get; set; }
        public SocketSubscribeType Type { get; set; }
        public SocketType SocketType { get; set; }
        public List<WebSocketSubscribeInfo> Subscribtions { get; } = new List<WebSocketSubscribeInfo>();
        public Ticker Ticker { get; set; }
        public string Adress { get; set; }
        public WebSocket Socket { get; set; }
        public SignalWebSocket Signal { get; set; }
        SocketConnectionState state = SocketConnectionState.None;
        public SocketConnectionState State {
            get { return state; }
            private set {
                if(State == value)
                    return;
                SocketConnectionState prev = State;
                state = value;
                OnStateChanged(prev);
            }
        }

        public event ConnectionInfoChangedEventHandler StateChanged;
        private void OnStateChanged(SocketConnectionState prev) {
            ConnectionInfoChangedEventArgs e = new ConnectionInfoChangedEventArgs() { PrevState = prev, NewState = State };
            if(Exchange != null)
                Exchange.OnSocketInfoStateChanged(this, e);
            if(StateChanged != null) {
                StateChanged(this, e);
            }
            if(State == SocketConnectionState.Error)
                History.Add(new SocketInfoHistoryItem() { Time = DateTime.Now, State = State, Message = LastError });
            else
                History.Add(new SocketInfoHistoryItem() { Time = DateTime.Now, State = State, Message = "State Changed" });
        }

        string lastError;
        public string LastError {
            get { return lastError; }
            set {
                if(LastError == value)
                    return;
                lastError = value;
            }
        }

        public List<SocketInfoHistoryItem> History { get; } = new List<SocketInfoHistoryItem>();

        public CandleStickIntervalInfo KlineInfo { get; set; }
        public bool Reconnecting { get; set; }

        public bool IsOpened { get; private set; }
        public bool IsOpening { get; private set; }
        protected DateTime OpenTime { get; set; }
        public void Open() {
            IsOpening = true;
            CreateSocket();
            SubscribeEvents();

            //LastActiveTime = DateTime.Now;
            State = SocketConnectionState.Connecting;
            if(Socket != null)
                Socket.Open();
            else if(Signal != null)
                Signal.Connect();
            if(OpenTime == DateTime.MinValue)
                OpenTime = DateTime.Now;
            IsOpening = false;
            IsOpened = true;
            //LastActiveTime = DateTime.Now;
            if(!Reconnecting)
                ForceUpdateSubscribtions();
            LastActiveTime = DateTime.Now; // set for first time
        }

        private void OnSocketMessageReceived(object sender, MessageReceivedEventArgs e) {
            LastActiveTime = DateTime.Now;
            MessageCount++;
        }

        public object Key { get { return Socket != null ? (object)Socket : (object)Signal; } }

        protected int RefCount { get; private set; }
        public void AddRef() { RefCount++; }
        public bool HasRef() {
            if(RefCount > 0)
                return true;
            for(int i = 0; i < Subscribtions.Count; i++) {
                if(Subscribtions[i].RefCount > 0)
                    return true;
            }
            return false;
        }
        public void Release() {
            if(RefCount > 0)
                RefCount--;
            if(!HasRef())
                Close();
        }
        protected void Close() {
            ClosedByUser = true;
            try {
                if(Socket != null)
                    Socket.Close();
                else if(Signal != null)
                    Signal.Shutdown();
            }
            catch(Exception e) {
                Telemetry.Default.TrackEvent(LogType.Error, Ticker, "error closing socket", e.Message.ToString());
            }
            State = SocketConnectionState.Disconnecting;
            IsOpened = false;
        }

        public void SubscribeToMarketsState(Action<object> action) {
            Signal.SubscribeToMarketsState().ContinueWith((x) => { action(x.Result); });
        }

        protected void SubscribeSignal(string channel, string message, Action<string, object> action, Action<List<SocketResponse>> onResult) {
            if(action != null)
                Signal.AddMessageHandler<object>(message, action);
            Signal.Subscribe(new string[] { channel }).ContinueWith(x => onResult(x.Result));
        }

        protected void UnsubscribeSignal(string channel, Action<List<SocketResponse>> onResult) {
            Signal.Unsubscribe(new string[] { channel }).ContinueWith(x => onResult(x.Result));
        }

        public void Dispose() {
            if(Socket != null)
                Socket.Dispose();
            if(Signal != null)
                Signal.Shutdown();
            Socket = null;
            Signal = null;
        }

        public void Subscribe(WebSocketSubscribeInfo info) {
            // s.Request.channel == info.Request.channel && s.Request.command == info.Request.command && s.Request.userID == info.Request.userID
            WebSocketSubscribeInfo exist = Subscribtions.FirstOrDefault(s => s.RequestEquals(info));
            if(exist != null) {
                exist.AddRef();
                return;
            }
            info.ShouldUpdateSubscribtion = true;
            Subscribtions.Add(info);
            info.AddRef();
            SubscribeCore(info);
        }

        string CheckResult(List<SocketResponse> result) {
            StringBuilder b = new StringBuilder();
            foreach(var r in result) {
                if(!string.IsNullOrEmpty(r.ErrorCode)) {
                    b.Append(r.ErrorCode);
                    b.Append(' ');
                }
            }
            if(b.Length > 0)
                return b.ToString();
            return null;
        }

        void SubscribeCore(WebSocketSubscribeInfo info) {
            object logOwner = info.Ticker == null ? (object)Exchange : (object)info.Ticker;
            if(!info.ShouldUpdateSubscribtion)
                return;
            if(State != SocketConnectionState.Connected) {
                info.ShouldUpdateSubscribtion = true;
                return;
            }
            info.ShouldUpdateSubscribtion = false;
            if(SocketType == SocketType.WebSocket) {
                if(info.Type == SocketSubscribeType.OrderBook) {
                    info.Ticker.IsOrderBookSubscribed = true;
                    info.Ticker.OrderBook.Clear();
                    LogManager.Default.Add(LogType.Log, logOwner, Convert.ToString(logOwner), "OrderBook channel subscibed", "");
                }
                else if(info.Type == SocketSubscribeType.TradeHistory) {
                    info.Ticker.IsTradeHistorySubscribed = true;
                    info.Ticker.ClearTradeHistory();
                    LogManager.Default.Add(LogType.Log, logOwner, Convert.ToString(logOwner), "TradeHistory channel subscibed", "");
                }
                else if(info.Type == SocketSubscribeType.Kline) {
                    info.Ticker.IsKlineSubscribed = true;
                    info.Ticker.CandleStickData.Clear();
                    LogManager.Default.Add(LogType.Log, logOwner, Convert.ToString(logOwner), "Kline channel subscibed", "");
                }
                string command = info.GetRequestString();
                Socket.Send(command);
                if(info.AfterConnect != null)
                    info.AfterConnect();
            }
            else {
                SubscribeSignal(info.Channel, info.MessageToHandle, info.OnMessage,
                    res => {
                        string error = CheckResult(res);
                        if(error != null)
                            LogManager.Default.Add(LogType.Error, this, info.Type.ToString(), "SignalR scribe error", error);
                        else {
                            LogManager.Default.Add(LogType.Log, Exchange, Exchange.Type.ToString(), "Channel subscribed", info.Channel);
                            if(info.AfterConnect != null)
                                info.AfterConnect();
                        }
                    });
            }
        }

        void UnsubscribeCore(WebSocketSubscribeInfo info) {
            object logOwner = info.Ticker == null ? (object)Exchange : (object)info.Ticker;
            if(info.Type == SocketSubscribeType.OrderBook) {
                info.Ticker.IsOrderBookSubscribed = false;
                LogManager.Default.Add(LogType.Log, logOwner, Exchange.Type.ToString(), "OrderBook channel unsubscibed", "");
            }
            else if(info.Type == SocketSubscribeType.TradeHistory) {
                info.Ticker.IsTradeHistorySubscribed = false;
                LogManager.Default.Add(LogType.Log, logOwner, Exchange.Type.ToString(), "TradeHistory channel unsubscibed", "");
            }
            else if(info.Type == SocketSubscribeType.Kline) {
                info.Ticker.IsKlineSubscribed = false;
                LogManager.Default.Add(LogType.Log, logOwner, Exchange.Type.ToString(), "Kline channel unsubscibed", "");
            }

            if(SocketType == SocketType.WebSocket) {
                string command = info.GetRequestString();
                Debug.WriteLine("send request = " + command);
                Socket.Send(command);
            }
            else {
                UnsubscribeSignal(info.Channel,
                    res => {
                        string error = CheckResult(res);
                        if(error != null)
                            LogManager.Default.Add(LogType.Error, this, info.Type.ToString(), "SignalR Unsubscribe Error", error);
                        else {
                            if(info.Type == SocketSubscribeType.OrderBook) {
                                info.Ticker.IsOrderBookSubscribed = false;
                            }
                            if(info.Type == SocketSubscribeType.TradeHistory) {
                                info.Ticker.IsTradeHistorySubscribed = false;
                            }
                            if(info.Type == SocketSubscribeType.Kline) {
                                info.Ticker.IsKlineSubscribed = false;
                            }
                        }
                    });
            }
        }

        protected DateTime StartWaitTime { get; set; }
        protected int WaitSeconds { get; set; }
        public void ReconnectAfter(int seconds) {
            StartWaitTime = DateTime.Now;
            State = SocketConnectionState.Waiting;
        }
        public bool CheckCanReconnectNow() {
            return (DateTime.Now - StartWaitTime).TotalSeconds > WaitSeconds;
        }

        public void Unsubscribe(WebSocketSubscribeInfo info) {
            if(Signal != null)
                return;
            //s.Request.channel == info.Request.channel && s.Request.userID == info.Request.userID
            WebSocketSubscribeInfo found = Subscribtions.FirstOrDefault(s => s.Type == info.Type && s.Ticker == info.Ticker);
            if(found == null)
                return;
            found.Release();
            if(found.RefCount > 0)
                return;
            Subscribtions.Remove(found);
            UnsubscribeCore(info);
        }

        private void SubscribeToExchangeDeltas(Ticker ticker, Action<Ticker> action) {
            Signal.SubscribeToExchangeDeltas(ticker.Name).ContinueWith(t => { action(ticker); });
        }

        protected virtual void SubscribeEvents() {
            if(Exchange == null)
                return;
            if(SocketType == SocketType.Signal)
                SubscribeSignalEvents();
            else if(SocketType == SocketType.WebSocket)
                SubscribeWebSocketEvents();
        }

        protected virtual void UnsubscribeEvents() {
            if(Exchange == null)
                return;
            if(SocketType == SocketType.Signal)
                UnsubscribeSignalEvents();
            else if(SocketType == SocketType.WebSocket)
                UnsubscribeWebSocketEvents();
        }

        private void UnsubscribeSignalEvents() {
            Signal.Received -= OnSignalReceived;
            Signal.StateChanged -= OnSignalStateChanged;
            Signal.Closed -= OnSignalClosed;
            Signal.Error -= OnSignalError;
            if(Type == SocketSubscribeType.Tickers) {
                Signal.Error -= Exchange.OnSignalSocketError;
                Signal.Closed -= Exchange.OnSignalConnectionClosed;
                Signal.StateChanged -= Exchange.OnSignalStateChanged;
                Signal.Received -= Exchange.OnSignalReceived;
            }
        }

        private void SubscribeSignalEvents() {
            Signal.Received += OnSignalReceived;
            Signal.StateChanged += OnSignalStateChanged;
            Signal.Error += OnSignalError;
            Signal.Closed += OnSignalClosed;
            if(Type == SocketSubscribeType.Tickers) {
                Signal.Error += Exchange.OnSignalSocketError;
                Signal.Closed += Exchange.OnSignalConnectionClosed;
                Signal.StateChanged += Exchange.OnSignalStateChanged;
                Signal.Received += Exchange.OnSignalReceived;
            }
        }

        private void OnSignalClosed() {
            State = SocketConnectionState.Disconnected;
        }

        private void OnSignalError(Exception e) {
            LastError = e.Message;
            State = SocketConnectionState.Error;
        }

        private void OnSignalStateChanged(Microsoft.AspNet.SignalR.Client.StateChange e) {
            if(e.NewState == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected) {
                State = SocketConnectionState.Connected;
                UpdateSubscribtions();
            }
            else if(e.NewState == Microsoft.AspNet.SignalR.Client.ConnectionState.Connecting)
                State = SocketConnectionState.Connecting;
            else if(e.NewState == Microsoft.AspNet.SignalR.Client.ConnectionState.Disconnected)
                State = SocketConnectionState.Disconnected;
            else if(e.NewState == Microsoft.AspNet.SignalR.Client.ConnectionState.Reconnecting)
                State = SocketConnectionState.Connecting;
        }

        private void OnSignalReceived(string obj) {
            LastActiveTime = DateTime.Now;
        }

        public int MessageCount { get; set; }
        private void SubscribeWebSocketEvents() {
            Socket.MessageReceived += OnSocketMessageReceived;
            Socket.Opened += OnSocketOpened;
            Socket.Closed += OnSocketClosed;
            Socket.Error += OnSocketError;
            if(Type == SocketSubscribeType.Tickers) {
                Socket.Error += Exchange.OnTickersSocketError;
                Socket.Opened += Exchange.OnTickersSocketOpened;
                Socket.Closed += Exchange.OnTickersSocketClosed;
                Socket.MessageReceived += Exchange.OnTickersSocketMessageReceived;
            }
            else if(Type == SocketSubscribeType.OrderBook) {
                Socket.Error += Exchange.OnOrderBookSocketError;
                Socket.Opened += Exchange.OnOrderBookSocketOpened;
                Socket.Closed += Exchange.OnOrderBookSocketClosed;
                Socket.MessageReceived += Exchange.OnOrderBookSocketMessageReceived;
            }
            else if(Type == SocketSubscribeType.TradeHistory) {
                Socket.Error += Exchange.OnTradeHistorySocketError;
                Socket.Opened += Exchange.OnTradeHistorySocketOpened;
                Socket.Closed += Exchange.OnTradeHistorySocketClosed;
                Socket.MessageReceived += Exchange.OnTradeHistorySocketMessageReceived;
            }
            else if(Type == SocketSubscribeType.Kline) {
                Socket.Error += Exchange.OnKlineSocketError;
                Socket.Opened += Exchange.OnKlineSocketOpened;
                Socket.Closed += Exchange.OnKlineSocketClosed;
                Socket.MessageReceived += Exchange.OnKlineSocketMessageReceived;
            }
        }

        private void OnSocketOpened(object sender, EventArgs e) {
            State = SocketConnectionState.Connected;
            UpdateSubscribtions();
        }

        private void OnSocketClosed(object sender, EventArgs e) {
            State = SocketConnectionState.Disconnected;
        }

        private void OnSocketError(object sender, SuperSocket.ClientEngine.ErrorEventArgs e) {
            LastError = e.Exception.Message;
            State = SocketConnectionState.Error;
        }

        private void UnsubscribeWebSocketEvents() {
            Socket.MessageReceived -= OnSocketMessageReceived;
            Socket.Opened -= OnSocketOpened;
            Socket.Closed -= OnSocketClosed;
            Socket.Error -= OnSocketError;
            if(Type == SocketSubscribeType.Tickers) {
                Socket.Error -= Exchange.OnTickersSocketError;
                Socket.Opened -= Exchange.OnTickersSocketOpened;
                Socket.Closed -= Exchange.OnTickersSocketClosed;
                Socket.MessageReceived -= Exchange.OnTickersSocketMessageReceived;
            }
            else if(Type == SocketSubscribeType.OrderBook) {
                Socket.Error -= Exchange.OnOrderBookSocketError;
                Socket.Opened -= Exchange.OnOrderBookSocketOpened;
                Socket.Closed -= Exchange.OnOrderBookSocketClosed;
                Socket.MessageReceived -= Exchange.OnOrderBookSocketMessageReceived;
            }
            else if(Type == SocketSubscribeType.TradeHistory) {
                Socket.Error -= Exchange.OnTradeHistorySocketError;
                Socket.Opened -= Exchange.OnTradeHistorySocketOpened;
                Socket.Closed -= Exchange.OnTradeHistorySocketClosed;
                Socket.MessageReceived -= Exchange.OnTradeHistorySocketMessageReceived;
            }
            else if(Type == SocketSubscribeType.Kline) {
                Socket.Error -= Exchange.OnKlineSocketError;
                Socket.Opened -= Exchange.OnKlineSocketOpened;
                Socket.Closed -= Exchange.OnKlineSocketClosed;
                Socket.MessageReceived -= Exchange.OnKlineSocketMessageReceived;
            }
        }
        void CreateSocket() {
            if(SocketType == SocketType.WebSocket) {
                Socket = new WebSocket(Address, "");
                Socket.EnableAutoSendPing = true;
                Socket.AutoSendPingInterval = 5;
                Socket.Security.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            }
            else {
                Signal = new SignalWebSocket(Address);
                //Signal.UpdateBalanceState = UpdateBalanceState;
                //Signal.UpdateExchangeState = UpdateExchangeState;
                //Signal.UpdateOrderState = UpdateOrderState;
                //Signal.UpdateSummaryState = UpdateSummaryState;
            }
        }
        protected object LogObject { get { return Ticker != null ? (object)Ticker : Exchange; } }
        public void Reconnect() {
            if(Reconnecting)
                return;
            ConnectionLostCount++;
            LogManager.Default.Add(LogType.Warning, LogObject, Exchange.Type.ToString(), "Reconnecting", Type.ToString(), false);;
            Reconnecting = true;
            try {
                Close();
                UnsubscribeEvents();
                CreateSocket();
                SubscribeEvents();
                Open();
                ClosedByUser = false;
                ForceUpdateSubscribtions();
            }
            catch(Exception e) {
                Telemetry.Default.TrackEvent(LogType.Error, LogObject, "Exception while reconnecting", Type.ToString());
                Telemetry.Default.TrackException(e);
            }
            finally {
                Reconnecting = false;
            }
        }

        public void ForceUpdateSubscribtions() {
            for(int i = 0; i < Subscribtions.Count; i++) {
                WebSocketSubscribeInfo info = Subscribtions[i];
                info.ShouldUpdateSubscribtion = true;
                LogManager.Default.Add(LogType.Log, LogObject, Exchange.ToString(), "Updating subscribtion", info.Type.ToString());
                SubscribeCore(info);
            }
        }
        public void UpdateSubscribtions() {
            for(int i = 0; i < Subscribtions.Count; i++) {
                WebSocketSubscribeInfo info = Subscribtions[i];
                LogManager.Default.Add(LogType.Log, LogObject, Exchange.ToString(), "Updating subscribtion", info.Type.ToString());
                SubscribeCore(info);
            }
        }

        protected internal void ClearRef() {
            RefCount = 0;
            for(int i = 0; i < Subscribtions.Count; i++) {
                Subscribtions[i].ClearRef();
            }
        }

        public void Simulate() {
            CreateSocket();
            State = SocketConnectionState.Connected;
        }
    }

    //public class WebSocketCommandInfo {
    //    public string command { get; set; }
    //    public string channel { get; set; }
    //    public string userID { get; set; }
    //    public string messageToHandle { get; set; }
    //}

    public class WebSocketSubscribeInfo {
        public WebSocketSubscribeInfo() { }
        public WebSocketSubscribeInfo(SocketSubscribeType type, Ticker ticker, string channelName, string messageToHandle) : 
            this(type, ticker) {
            Channel = channelName;
            MessageToHandle = messageToHandle;
            //this.channel = channelName;
            //this.messageToHandle = messageToHandle;
        }
        public WebSocketSubscribeInfo(SocketSubscribeType type, Ticker ticker) {
            Type = type;
            Ticker = ticker;
            //Request = new WebSocketCommandInfo();
            //if(Ticker != null)
            //    channel = Ticker.Name;
        }
        public bool ShouldUpdateSubscribtion { get; set; }
        public Action AfterConnect { get; set; }
        public Action<string, object> OnMessage { get; set; }
        public Ticker Ticker { get; set; }
        public SocketSubscribeType Type { get; set; }
        public object Request { get; set; }
        protected internal string RequestString { get; set; }
        protected internal string GetRequestString() {
            if(Request == null)
                return null;
            if(Request is string)
                return (string)Request;
            return JsonConvert.SerializeObject(Request);
        }

        //public string command { get { return Request.command; } set { Request.command = value; } }
        //public string channel { get { return Request.channel; } set { Request.channel = value; } }
        //public string userID { get { return Request.userID; } set { Request.userID = value; } }
        //public string messageToHandle { get { return Request.messageToHandle; } set { Request.messageToHandle = value; } }

        public string Channel { get; set; }
        public string MessageToHandle { get; set; }

        public int RefCount { get; private set; }

        public void AddRef() { RefCount++; }
        public void Release() {
            if(RefCount > 0)
                RefCount--;
        }

        public void ClearRef() {
            RefCount = 0;
        }

        internal bool RequestEquals(WebSocketSubscribeInfo info) {
            string request = GetRequestString();
            string irequest = info.GetRequestString();
            return request == irequest;
        }
    }

    public enum SocketSubscribeType {
        Tickers,
        OrderBook,
        TradeHistory,
        Kline,
        Hearthbeat,
        Ticker
    }

    public enum SocketType {
        WebSocket,
        Signal
    }

    public class SocketInfoHistoryItem {
        public DateTime Time { get; set; }
        public SocketConnectionState State { get; set; }
        public string Message { get; set; }
    }

    public class ConnectionInfoChangedEventArgs : EventArgs {
        public SocketConnectionState PrevState { get; set; }
        public SocketConnectionState NewState { get; set; }
    }
    public delegate void ConnectionInfoChangedEventHandler(object sender, ConnectionInfoChangedEventArgs e);
}
