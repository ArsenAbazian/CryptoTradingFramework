using CryptoMarketClient.Exchanges.Bittrex;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using WebSocket4Net;
using static CryptoMarketClient.Exchanges.Bittrex.SignalWebSocket;

namespace CryptoMarketClient.Common {
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

        public SignalCallback UpdateExchangeState { get; set; }
        public SignalCallback UpdateOrderState { get; set; }
        public SignalCallback UpdateBalanceState { get; set; }
        public SignalCallback UpdateSummaryState { get; set; }

        public string Address { get; set; }
        public Exchange Exchange { get; set; }
        public bool IsDelayed(int ms) {
            return (DateTime.Now - LastActiveTime).TotalMilliseconds > ms;
        }
        public DateTime LastActiveTime { get; set; }
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
            protected set {
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
        }

        public string LastError { get; set; }
        public CandleStickIntervalInfo KlineInfo { get; set; }
        public bool Reconnecting { get; set; }

        public bool IsOpened { get; private set; }
        public bool IsOpening { get; private set; }
        public void Open() {
            IsOpening = true;
            CreateSocket();
            SubscribeEvents();

            LastActiveTime = DateTime.Now;
            State = SocketConnectionState.Connecting;
            if(Socket != null)
                Socket.Open();
            else if(Signal != null)
                Signal.Connect();
            IsOpening = false;
            IsOpened = true;
            LastActiveTime = DateTime.Now;
            if(!Reconnecting)
                ForceUpdateSubscribtions();
        }

        private void OnSocketMessageReceived(object sender, MessageReceivedEventArgs e) {
            LastActiveTime = DateTime.Now;
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
                Telemetry.Default.TrackEvent(LogType.Error, Exchange, Ticker, "error closing socket", e.Message.ToString());
            }
            State = SocketConnectionState.Disconnecting;
            IsOpened = false;
        }

        public void SubscribeToMarketsState(Action action) {
            Signal.SubscribeToMarketsState().ContinueWith((x) => { action(); });
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
            WebSocketSubscribeInfo exist = Subscribtions.FirstOrDefault(s => s.Command.channel == info.Command.channel && s.Command.command == info.Command.command && s.Command.userID == info.Command.userID);
            if(exist != null) {
                exist.AddRef();
                return;
            }
            info.ShouldUpdateSubscribtion = true;
            Subscribtions.Add(info);
            info.AddRef();
            SubscribeCore(info);
        }
        void SubscribeCore(WebSocketSubscribeInfo info) {
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
                    Telemetry.Default.TrackEvent(LogType.Log, Exchange, info.Ticker, "order book channel subscibed", "");
                }
                else if(info.Type == SocketSubscribeType.TradeHistory) {
                    info.Ticker.IsTradeHistorySubscribed = true;
                    info.Ticker.TradeHistory.Clear();
                    Telemetry.Default.TrackEvent(LogType.Log, Exchange, info.Ticker, "trade history channel subscibed", "");
                }
                else if(info.Type == SocketSubscribeType.Kline) {
                    info.Ticker.IsKlineSubscribed = true;
                    info.Ticker.CandleStickData.Clear();
                    Telemetry.Default.TrackEvent(LogType.Log, Exchange, info.Ticker, "kline channel subscibed", "");
                }
                string command = JsonConvert.SerializeObject(info.Command);
                Debug.WriteLine("send command = " + command);
                Socket.Send(command);
            }
            else {
                if(info.Type == SocketSubscribeType.Tickers) {
                    SubscribeToMarketsState(() => {
                        Telemetry.Default.TrackEvent(LogType.Log, Exchange, info.Ticker, "tickers channel subscibed", "");
                        if(info.AfterConnect != null)
                            info.AfterConnect();
                    });
                }
                else if(info.Type == SocketSubscribeType.OrderBook) {
                    SubscribeToExchangeDeltas(info.Ticker, (t) => {
                        t.IsOrderBookSubscribed = true;
                        QueryExchangeState(t.Name);
                        Telemetry.Default.TrackEvent(LogType.Log, Exchange, info.Ticker, "order book channel subscibed", "");
                        if(info.AfterConnect != null)
                            info.AfterConnect();
                    });
                }
                else if(info.Type == SocketSubscribeType.TradeHistory) {
                    SubscribeToExchangeDeltas(info.Ticker, (t) => {
                        t.IsOrderBookSubscribed = true;
                        QueryExchangeState(t.Name);
                        Telemetry.Default.TrackEvent(LogType.Log, Exchange, info.Ticker, "trade history channel subscibed", "");
                        if(info.AfterConnect != null)
                            info.AfterConnect();
                    });
                }
            }
        }

        void UnsubscribeCore(WebSocketSubscribeInfo info) {
            if(info.Type == SocketSubscribeType.OrderBook) {
                info.Ticker.IsOrderBookSubscribed = false;
                Telemetry.Default.TrackEvent(LogType.Log, Exchange, info.Ticker, "order book channel unsubscibed", "");
            }
            else if(info.Type == SocketSubscribeType.TradeHistory) {
                info.Ticker.IsTradeHistorySubscribed = false;
                Telemetry.Default.TrackEvent(LogType.Log, Exchange, info.Ticker, "trade history channel unsubscibed", "");
            }
            else if(info.Type == SocketSubscribeType.Kline) {
                info.Ticker.IsKlineSubscribed = false;
                Telemetry.Default.TrackEvent(LogType.Log, Exchange, info.Ticker, "kline channel unsubscibed", "");
            }

            if(SocketType == SocketType.WebSocket) {
                string command = JsonConvert.SerializeObject(info.Command);
                Debug.WriteLine("send command = " + command);
                Socket.Send(command);
            }
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
        }

        public void Unsubscribe(WebSocketSubscribeInfo info) {
            if(Signal != null)
                return;
            WebSocketSubscribeInfo found = Subscribtions.FirstOrDefault(s => s.Command.channel == info.Command.channel && s.Command.userID == info.Command.userID);
            if(found == null)
                return;
            found.Release();
            if(found.RefCount > 0)
                return;
            Subscribtions.Remove(found);
            UnsubscribeCore(info);
        }

        public void QueryExchangeState(string name) {
            Signal.QueryExchangeState(name);
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
            State = SocketConnectionState.Error;
            LastError = e.Message;
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
            State = SocketConnectionState.Error;
            LastError = e.Exception.Message;
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
                Socket.Security.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            }
            else {
                Signal = new SignalWebSocket(Address);
                Signal.UpdateBalanceState = UpdateBalanceState;
                Signal.UpdateExchangeState = UpdateExchangeState;
                Signal.UpdateOrderState = UpdateOrderState;
                Signal.UpdateSummaryState = UpdateSummaryState;
            }
        }
        public void Reconnect() {
            if(Reconnecting)
                return;

            Telemetry.Default.TrackEvent(LogType.Warning, Exchange, Ticker, "reconnecting", Type.ToString());
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
                Telemetry.Default.TrackEvent(LogType.Error, Exchange, Ticker, "exception while reconnecting", Type.ToString());
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
                Telemetry.Default.TrackEvent(LogType.Warning, Exchange, info.Ticker, "updating subscribtion", info.Type.ToString());
                SubscribeCore(info);
            }
        }
        public void UpdateSubscribtions() {
            for(int i = 0; i < Subscribtions.Count; i++) {
                WebSocketSubscribeInfo info = Subscribtions[i];
                Telemetry.Default.TrackEvent(LogType.Warning, Exchange, info.Ticker, "updating subscribtion", info.Type.ToString());
                SubscribeCore(info);
            }
        }

        protected internal void ClearRef() {
            RefCount = 0;
            for(int i = 0; i < Subscribtions.Count; i++) {
                Subscribtions[i].ClearRef();
            }
        }
    }

    public class WebSocketCommandInfo {
        public string command { get; set; }
        public string channel { get; set; }
        public string userID { get; set; }
    }

    public class WebSocketSubscribeInfo {
        public WebSocketSubscribeInfo(SocketSubscribeType type, Ticker ticker) {
            Type = type;
            Ticker = ticker;
            Command = new WebSocketCommandInfo();
            if(Ticker != null)
                channel = Ticker.Name;
        }
        public bool ShouldUpdateSubscribtion { get; set; }
        public Action AfterConnect { get; set; }
        public Ticker Ticker { get; set; }
        public SocketSubscribeType Type { get; set; }
        public WebSocketCommandInfo Command { get; set; }

        public string command { get { return Command.command; } set { Command.command = value; } }
        public string channel { get { return Command.channel; } set { Command.channel = value; } }
        public string userID { get { return Command.userID; } set { Command.userID = value; } }

        public int RefCount { get; private set; }

        public void AddRef() { RefCount++; }
        public void Release() {
            if(RefCount > 0)
                RefCount--;
        }

        public void ClearRef() {
            RefCount = 0;
        }
    }

    public enum SocketSubscribeType {
        Tickers,
        OrderBook,
        TradeHistory,
        Kline,
        Hearthbeat
    }

    public enum SocketType {
        WebSocket,
        Signal
    }

    public class ConnectionInfoChangedEventArgs : EventArgs {
        public SocketConnectionState PrevState { get; set; }
        public SocketConnectionState NewState { get; set; }
    }
    public delegate void ConnectionInfoChangedEventHandler(object sender, ConnectionInfoChangedEventArgs e);
}
