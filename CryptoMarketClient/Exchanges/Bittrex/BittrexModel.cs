using CryptoMarketClient.Common;
using CryptoMarketClient.Exchanges.Bittrex;
using DevExpress.XtraEditors;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Bittrex {
    public class BittrexExchange : Exchange {
        static BittrexExchange defaultExchange;
        public static BittrexExchange Default {
            get {
                if(defaultExchange == null) {
                    defaultExchange = new BittrexExchange();
                    defaultExchange.Load();
                }
                return defaultExchange;
            }
        }

        protected internal override IIncrementalUpdateDataProvider CreateIncrementalUpdateDataProvider() {
            return new BittrexIncrementalUpdateDataProvider();
        }

        public override void OnAccountRemoved(AccountInfo info) {

        }

        public override string BaseWebSocketAdress => "https://socket.bittrex.com/signalr";

        public override ExchangeType Type => ExchangeType.Bittrex;

        public override bool SupportWebSocket(WebSocketType type) {
            if(type == WebSocketType.Tickers)
                return true;
            if(type == WebSocketType.Ticker)
                return true;
            return false;
        }

        public override bool GetDeposites(AccountInfo account) {
            return true;
        }

        public override Form CreateAccountForm() {
            return new AccountBalancesForm(this);
        }

        public override void ObtainExchangeSettings() { }

        protected BittrexWebSocket SignalSocket { get; private set; }
        public override void StartListenTickersStream() {
            SignalSocket = new BittrexWebSocket(BaseWebSocketAdress);
            SignalSocket.UpdateSummaryState = UpdateMarketsState;
            SignalSocket.UpdateExchangeState = UpdateExchangeState;
            SignalSocket.Error += OnError;
            SignalSocket.Closed += OnConnectionClosed;
            SignalSocket.StateChanged += OnStateChanged;
            SignalSocket.Received += OnReceived;
            TickersSocketState = SocketConnectionState.Connecting;
            SignalSocket.Connect();
            SignalSocket.SubscribeToMarketsState().ContinueWith(x => Debug.WriteLine(x));
        }

        public override void StopListenTickersStream() {
            TickersSocketState = SocketConnectionState.Disconnected;
            SignalSocket.Shutdown();
            SignalSocket.Error -= OnError;
            SignalSocket.Closed -= OnConnectionClosed;
            SignalSocket.StateChanged -= OnStateChanged;
            SignalSocket.Received -= OnReceived;
            SignalSocket = null;
        }

        protected virtual void OnStateChanged(StateChange e) {
            if(e.NewState == ConnectionState.Connected) {
                TickersSocketState = SocketConnectionState.Connected;
                foreach(Ticker ticker in SubscribedTickers)
                    StartListenTickerStream(ticker);
            }
            else if(e.NewState == ConnectionState.Connecting)
                TickersSocketState = SocketConnectionState.Connecting;
            else if(e.NewState == ConnectionState.Disconnected)
                TickersSocketState = SocketConnectionState.Disconnected;
            else if(e.NewState == ConnectionState.Reconnecting)
                TickersSocketState = SocketConnectionState.Connecting;
        }

        public override void StartListenTickerStream(Ticker ticker) {
            base.StartListenTickerStream(ticker);
            SignalSocket.SubscribeToExchangeDeltas(ticker.Name).ContinueWith(t => SignalSocket.QueryExchangeState(ticker.Name));
        }

        public override void StopListenTickerStream(Ticker ticker) {
            base.StopListenTickerStream(ticker);
        }

        protected virtual void OnReceived(string s) {

        }

        string[] marketSummaryStateInfo;
        protected string[] SocketMarketSummaryStateInfo{
            get {
                if(marketSummaryStateInfo == null) {
                    marketSummaryStateInfo = new string[] {
                        "M","H","L","V","l","m","T","B","A","G","g","PD","x"
                    };
                }
                return marketSummaryStateInfo;
            }    
        }
        protected virtual void UpdateMarketsState(BittrexSocketCommand command, string marketName, string s) {
            LastWebSocketRecvTime = DateTime.Now;
            TickersSocketState = SocketConnectionState.Connected;
            byte[] data = BittrexWebSocket.DecodeBytes(s);

            int startIndex = 0;
            if(!JSonHelper.Default.SkipSymbol(data, ':', 2, ref startIndex))
                return;
            List<string[]> items = JSonHelper.Default.DeserializeArrayOfObjects(data, ref startIndex, SocketMarketSummaryStateInfo);
            foreach(string[] item in items) {
                BittrexTicker t = (BittrexTicker)Tickers.FirstOrDefault(tt => tt.Name == item[0]);
                if(t == null)
                    continue;
                t.Hr24High = FastValueConverter.Convert(item[1]);
                t.Hr24Low = FastValueConverter.Convert(item[2]);
                t.Volume = FastValueConverter.Convert(item[3]);
                t.Last = FastValueConverter.Convert(item[4]);
                t.BaseVolume = FastValueConverter.Convert(item[5]);
                t.HighestBid = FastValueConverter.Convert(item[7]);
                t.LowestAsk = FastValueConverter.Convert(item[8]);

                t.UpdateTrailings();
            }
            RaiseTickersUpdate();
        }

        protected virtual void UpdateExchangeState(BittrexSocketCommand command, string marketName, string s) {
            //string decoded = BittrexWebSocket.Decode(s);
            LastWebSocketRecvTime = DateTime.Now;
            TickersSocketState = SocketConnectionState.Connected;
            if(command == BittrexSocketCommand.QueryExchangeState) {
                OnSnapshotRecv(marketName, BittrexWebSocket.Decode(s));
            }
            else if(command == BittrexSocketCommand.IncrementalUpdate) {
                byte[] bytes = BittrexWebSocket.DecodeBytes(s);
                string ss = BittrexWebSocket.Decode(s);
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
            string[] startItems = JSonHelper.Default.StartDeserializeObject(bytes, ref startIndex, IncrementalUpdateStartItems);
            int st = 0;
            long seqNumber = FastValueConverter.ConvertPositiveLong(startItems[1], ref st);
            Ticker ticker = Tickers.FirstOrDefault(t => t.Name == startItems[0]);

            List<string[]> bids = null, asks = null, trades = null;
            startIndex += 2; // skip ,"

            if(bytes[startIndex] == 'Z') {
                startIndex += 3; // ski[ Z":
                bids = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, OrderBookEntryItems);
            }
            startIndex+=2; // skip ,"
            if(bytes[startIndex] == 'S') {
                startIndex += 3; // ski[ S":
                asks = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, OrderBookEntryItems);
            }
            startIndex+=2; // skip ,"
            if(bytes[startIndex] == 'f') {
                startIndex += 3; // ski[ f":
                trades = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, TradesItems);
            }
            lock(Updates) {
                Updates.Push(seqNumber, ticker, bids, asks, trades);
                OnIncrementalUpdateRecv(Updates);
            }
        }
        void OnSnapshotRecv(string marketName, string s) {
            Ticker ticker = Tickers.FirstOrDefault(t => t.Name == marketName);
            if(ticker == null)
                throw new DllNotFoundException("Ticker not found " + ticker.Name);
            ticker.OrderBook.Clear();
            IIncrementalUpdateDataProvider provider = CreateIncrementalUpdateDataProvider();
            Dictionary<string, object> obj = JsonParser.Parse<Dictionary<string, object>>(s);
            long seqNumber = FastValueConverter.ConvertPositiveInteger((string)obj["N"]);
            lock(Updates) {
                Updates.Clear(seqNumber + 1);
                provider.ApplySnapshot(obj, ticker);
            }
        }

        protected virtual void UpdateOrderState(string s) {
            LastWebSocketRecvTime = DateTime.Now;
            TickersSocketState = SocketConnectionState.Connected;
        }

        protected virtual void UpdateBalancesState(string s) {
            LastWebSocketRecvTime = DateTime.Now;
            TickersSocketState = SocketConnectionState.Connected;
        }

        protected virtual void OnError(Exception e) {
            TickersSocketState = SocketConnectionState.Error;
            XtraMessageBox.Show("Socket error. Please contact developers. ->" + e.ToString());
        }

        protected virtual void OnConnectionClosed() {
            TickersSocketState = SocketConnectionState.Disconnected;
        }
        
        public override bool AllowCandleStickIncrementalUpdate => false;

        public override List<CandleStickIntervalInfo> GetAllowedCandleStickIntervals() {
            List<CandleStickIntervalInfo> list = new List<CandleStickIntervalInfo>();
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(1), Text = "1 Minute", Command = "oneMin" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(5), Text = "5 Minutes", Command = "fiveMin" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(30), Text = "30 Minutes", Command = "thirtyMin" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(60), Text = "1 Hour", Command = "hour" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(60 * 24), Text = "1 Day", Command = "day" });
            return list;
        }

        protected List<TradeInfoItem> UpdateList { get; } = new List<TradeInfoItem>(100);

        string GetInvervalCommand(int minutes) {
            if(minutes == 1)
                return "oneMin";
            if(minutes == 5)
                return "fiveMin";
            if(minutes == 30)
                return "thirtyMin";
            if(minutes == 60)
                return "hour";
            if(minutes == 60 * 24)
                return "day";
            return "fiveMin";
        }
        public override BindingList<CandleStickData> GetCandleStickData(Ticker ticker, int candleStickPeriodMin, DateTime start, long periodInSeconds) {
            long startSec = (long)(start.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            long end = startSec + periodInSeconds;

            string address = string.Format("https://bittrex.com/Api/v2.0/pub/market/GetTicks?marketName={0}&tickInterval={1}&_={2}",
                Uri.EscapeDataString(ticker.CurrencyPair), GetInvervalCommand(candleStickPeriodMin), GetNonce());
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(address);
            }
            catch(Exception) {
                return null;
            }
            if(bytes == null || bytes.Length == 0)
                return null;

            BindingList<CandleStickData> list = new BindingList<CandleStickData>();

            int startIndex = 1;
            if(!JSonHelper.Default.SkipSymbol(bytes, ':', 3, ref startIndex))
                return list;

            List<string[]> res = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "O", "H", "L", "C", "V", "T", "BV" });
            if(res == null) return list;
            foreach(string[] item in res) {
                CandleStickData data = new CandleStickData();
                data.Time = Convert.ToDateTime(item[5]).ToLocalTime();
                data.High = FastValueConverter.Convert(item[1]);
                data.Low = FastValueConverter.Convert(item[2]);
                data.Open = FastValueConverter.Convert(item[0]);
                data.Close = FastValueConverter.Convert(item[3]);
                data.Volume = FastValueConverter.Convert(item[6]);
                data.QuoteVolume = FastValueConverter.Convert(item[4]);
                data.BuySellVolume = data.Open < data.Close ? data.Volume : -data.Volume;
                data.WeightedAverage = 0;
                list.Add(data);
            }
            //List<TradeInfoItem> trades = null;
            //if(ticker.TradeHistory.Count > 0)
            //    trades = ticker.TradeHistory;
            //else
            //    trades = GetTradeVolumesForCandleStick(ticker);
            //CandleStickChartHelper.InitializeVolumes(list, trades, ticker.CandleStickPeriodMin);
            return list;
        }

        public List<TradeInfoItem> GetTradeVolumesForCandleStick(Ticker ticker) {
            List<TradeInfoItem> trades = new List<TradeInfoItem>();
            string address = string.Format("https://bittrex.com/api/v1.1/public/getmarkethistory?market={0}", Uri.EscapeDataString(ticker.MarketName));
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(address);
            }
            catch(Exception) {
                return trades;
            }
            if(bytes == null)
                return trades;

            int startIndex = 1;
            if(!JSonHelper.Default.SkipSymbol(bytes, ':', 3, ref startIndex))
                return trades;

            List<string[]> res = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "Id", "TimeStamp", "Quantity", "Price", "Total", "FillType", "OrderType" });
            if(res == null)
                return trades;
            
            lock(ticker) {
                foreach(string[] obj in res) {
                    TradeInfoItem item = new TradeInfoItem(null, ticker);
                    item.Time = Convert.ToDateTime(obj[1]).ToLocalTime();
                    item.AmountString = obj[2];
                    item.Type = obj[6].Length == 3 ? TradeType.Buy : TradeType.Sell;
                    trades.Add(item);
                }
            }
            return trades;
        }

        public override bool GetTickersInfo() {
            if(Tickers.Count > 0)
                return true;
            string address = "https://bittrex.com/api/v1.1/public/getmarkets";
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(address);
            }
            catch(Exception) {
                return false;
            }
            if(bytes == null)
                return false;

            int startIndex = 1;
            if(!JSonHelper.Default.SkipSymbol(bytes, ':', 3, ref startIndex))
                return false;

            List<string[]> res = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "MarketCurrency", "BaseCurrency", "MarketCurrencyLong", "BaseCurrencyLong", "MinTradeSize", "MarketName", "IsActive", "Created", "Notice", "IsSponsored", "LogoUrl" });
            foreach(string[] item in res) {
                BittrexTicker m = new BittrexTicker(this);

                m.MarketCurrency = item[0];
                m.BaseCurrency = item[1];
                m.MarketCurrencyLong = item[2];
                m.BaseCurrencyLong = item[3];
                m.MinTradeSize = FastValueConverter.Convert(item[4]);
                m.MarketName = item[5];
                m.IsActive = item[6].Length == 4 ? true : false;
                m.Created = Convert.ToDateTime(item[7]).ToLocalTime();
                m.LogoUrl = item[10];
                m.Index = Tickers.Count;
                Tickers.Add(m);
            }
            IsInitialized = true;
            return true;
        }
        public override bool UpdateCurrencies() {
            string address = "https://bittrex.com/api/v1.1/public/getcurrencies";
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(address);
            }
            catch(Exception) {
                return false;
            }
            if(bytes == null)
                return false;

            int startIndex = 1;
            if(!JSonHelper.Default.SkipSymbol(bytes, ':', 3, ref startIndex))
                return false;

            List<string[]> res = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "Currency", "CurrencyLong", "MinConfirmation", "TxFee", "IsActive", "CoinType", "BaseAddress", "Notice" });
            foreach(string[] item in res) {
                string currency = item[0];
                BittrexCurrencyInfo c = (BittrexCurrencyInfo)Currencies.FirstOrDefault(curr => curr.Currency == currency);
                if(c == null) {
                    c = new BittrexCurrencyInfo();
                    c.Currency = item[0];
                    c.CurrencyLong = item[1];
                    c.MinConfirmation = int.Parse(item[2]);
                    c.TxFee = FastValueConverter.Convert(item[3]);
                    c.CoinType = item[5];
                    c.BaseAddress = item[6];

                    Currencies.Add(c);
                }
                c.IsActive = item[4].Length == 4;
            }
            return true;
        }
        public bool GetCurrenciesInfo() {
            Currencies.Clear();
            return UpdateCurrencies();
        }
        public void GetTicker(BittrexTicker info) {
            string address = string.Format("https://bittrex.com/api/v1.1/public/getticker?market={0}", Uri.EscapeDataString(info.MarketName));
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(address);
            }
            catch(Exception) {
                return;
            }
            if(bytes == null)
                return;

            int startIndex = 1;
            if(!JSonHelper.Default.SkipSymbol(bytes, ':', 3, ref startIndex))
                return;

            string[] res = JSonHelper.Default.DeserializeObject(bytes, ref startIndex, new string[] { "Bid", "Ask", "Last" });
            if(res == null)
                return;
            info.HighestBid = FastValueConverter.Convert(res[0]);
            info.LowestAsk = FastValueConverter.Convert(res[1]);
            info.Last = FastValueConverter.Convert(res[2]);
            info.Time = DateTime.UtcNow;
            info.UpdateHistoryItem();
        }
        public override bool UpdateTicker(Ticker tickerBase) {
            string address = string.Format("https://bittrex.com/api/v1.1/public/getmarketsummary?market={0}", tickerBase.MarketName);
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(address);
            }
            catch(Exception) {
                return false;
            }
            if(bytes == null)
                return false;

            int startIndex = 1;
            if(!JSonHelper.Default.SkipSymbol(bytes, ':', 3, ref startIndex))
                return false;

            string[] res = JSonHelper.Default.DeserializeObject(bytes, ref startIndex, new string[] { "MarketName", "High", "Low", "Volume", "Last", "BaseVolume", "TimeStamp", "Bid", "Ask", "OpenBuyOrders", "OpenSellOrders", "PrevDay", "Created" });
            if(res == null)
                return true;

            BittrexTicker info = (BittrexTicker)tickerBase;

            info.Hr24High = FastValueConverter.Convert(res[1]);
            info.Hr24Low = FastValueConverter.Convert(res[2]);
            info.Volume = FastValueConverter.Convert(res[3]);
            info.Last = FastValueConverter.Convert(res[4]);
            info.BaseVolume = FastValueConverter.Convert(res[5]);
            info.Time = Convert.ToDateTime(res[6]).ToLocalTime();
            info.HighestBid = FastValueConverter.Convert(res[7]);
            info.LowestAsk = FastValueConverter.Convert(res[8]);
            info.OpenBuyOrders = Convert.ToInt32(res[9]);
            info.OpenSellOrders = Convert.ToInt32(res[10]);
            info.PrevDay = FastValueConverter.Convert(res[11]);
            info.Created = Convert.ToDateTime(res[12]).ToLocalTime();
            info.DisplayMarketName = res[0];
            info.UpdateHistoryItem();

            return true;
        }
        public override bool UpdateTickersInfo() {
            string address = "https://bittrex.com/api/v1.1/public/getmarketsummaries";
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(address);
            }
            catch(Exception) {
                return false;
            }
            if(bytes == null)
                return false;

            int startIndex = 1;
            if(!JSonHelper.Default.SkipSymbol(bytes, ':', 3, ref startIndex))
                return false;

            List<string[]> res = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "MarketName", "High", "Low", "Volume", "Last", "BaseVolume", "TimeStamp", "Bid", "Ask", "OpenBuyOrders", "OpenSellOrders", "PrevDay", "Created" });
            foreach(string[] item in res) {
                string marketName = item[0];
                BittrexTicker info = (BittrexTicker)Tickers.FirstOrDefault((m) => m.MarketName == marketName);
                if(info == null)
                    continue;

                info.Hr24High = FastValueConverter.Convert(item[1]);
                info.Hr24Low = FastValueConverter.Convert(item[2]);
                info.Volume = FastValueConverter.Convert(item[3]);
                info.Last = FastValueConverter.Convert(item[4]);
                info.BaseVolume = FastValueConverter.Convert(item[5]);
                info.Time = Convert.ToDateTime(item[6]).ToLocalTime();
                info.HighestBid = FastValueConverter.Convert(item[7]);
                info.LowestAsk = FastValueConverter.Convert(item[8]);
                info.OpenBuyOrders = Convert.ToInt32(item[9]);
                info.OpenSellOrders = Convert.ToInt32(item[10]);
                info.PrevDay = FastValueConverter.Convert(item[11]);
                info.Created = Convert.ToDateTime(item[12]).ToLocalTime();
                info.DisplayMarketName = item[0];
            }

            return true;
        }
        public override bool UpdateArbitrageOrderBook(Ticker info, int depth) {
            string address = GetOrderBookString(info, depth);
            byte[] data = GetDownloadBytes(address);
            if(data == null)
                return false;
            return UpdateOrderBook(info, data, false, depth);
        }
        public string GetOrderBookString(Ticker info, int depth) {
            return string.Format("https://bittrex.com/api/v1.1/public/getorderbook?market={0}&type=both&depth={1}", Uri.EscapeDataString(info.MarketName), depth * 2);
        }
        public override bool ProcessOrderBook(Ticker tickerBase, string text) {
            throw new NotImplementedException();
        }
        public bool UpdateOrderBook(BittrexTicker info, byte[] data, int depth) {
            return UpdateOrderBook(info, data, true, depth);
        }
        public override bool UpdateOrderBook(Ticker tickerBase) {
            return UpdateArbitrageOrderBook(tickerBase, OrderBook.Depth);
        }
        public bool UpdateOrderBook(Ticker ticker, byte[] bytes, bool raiseChanged, int depth) {
            if(bytes == null)
                return false;

            int startIndex = 1; // skip {
            if(!JSonHelper.Default.SkipSymbol(bytes, ':', 4, ref startIndex))
                return false;

            List<string[]> bids = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "Quantity", "Rate" });
            if(!JSonHelper.Default.FindCharWithoutStop(bytes, ':', ref startIndex))
                return false;
            if(bids == null)
                return true;
            startIndex++;
            List<string[]> asks = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "Quantity", "Rate" });
            if(asks == null)
                return true;

            ticker.OrderBook.GetNewBidAsks();
            int index = 0;
            List<OrderBookEntry> list = ticker.OrderBook.Bids;
            foreach(string[] item in bids) {
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
            foreach(string[] item in asks) {
                OrderBookEntry entry = new OrderBookEntry();
                list.Add(entry);
                entry.ValueString = item[1];
                entry.AmountString = item[0];
                index++;
                if(index >= depth)
                    break;
            }
            ticker.OrderBook.UpdateEntries();
            return true;
        }
        public void GetOrderBook(BittrexTicker info, int depth) {
            string address = string.Format("https://bittrex.com/api/v1.1/public/getorderbook?market={0}&type=both&depth={1}", Uri.EscapeDataString(info.MarketName), depth);
            byte[] data = GetDownloadBytes(address);
            if(data == null)
                return;
            UpdateOrderBook(info, data, depth);
        }
        public bool GetTrades(BittrexTicker ticker) {
            string address = string.Format("https://bittrex.com/api/v1.1/public/getmarkethistory?market={0}", Uri.EscapeDataString(ticker.MarketName));
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(address);
            }
            catch(Exception) {
                return false;
            }
            if(bytes == null)
                return false;

            int startIndex = 1;
            if(!JSonHelper.Default.SkipSymbol(bytes, ':', 3, ref startIndex))
                return false;

            List<string[]> res = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "Id", "TimeStamp", "Quantity", "Price", "Total", "FillType", "OrderType" });
            if(res == null)
                return false;
            lock(ticker) {
                ticker.TradeHistory.Clear();
                foreach(string[] obj in res) {
                    TradeInfoItem item = new TradeInfoItem(null, ticker);
                    item.Id = Convert.ToInt64(obj[0]); ;
                    item.Time = Convert.ToDateTime(obj[1]).ToLocalTime();
                    item.AmountString = obj[2];
                    item.RateString = obj[3];
                    item.Total = FastValueConverter.Convert(obj[4]);
                    item.Type = obj[6].Length == 3 ? TradeType.Buy : TradeType.Sell;
                    item.Fill = obj[5].Length == 4 ? TradeFillType.Fill : TradeFillType.PartialFill;
                    ticker.TradeHistory.Add(item);
                }
            }
            ticker.RaiseTradeHistoryAdd();
            return true;
        }
        public override bool UpdateAccountTrades(AccountInfo account, Ticker ticker) {
            string address = string.Empty;
            if(ticker != null) {
                address = string.Format("https://bittrex.com/api/v1.1/account/getorderhistory?apikey={0}&nonce={1}&market={2}",
                    Uri.EscapeDataString(account.ApiKey),
                    GetNonce(),
                    ticker.MarketName);
            }
            else {
                address = string.Format("https://bittrex.com/api/v1.1/account/getorderhistory?apikey={0}&nonce={1}", Uri.EscapeDataString(account.ApiKey), GetNonce());
            }
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", account.GetSign(address));
            try {
                byte[] bytes = client.DownloadData(address);
                return OnGetAccountTrades(account, ticker, bytes);
            }
            catch(Exception e) {
                Debug.WriteLine("error getting my trades: " + e.ToString());
                return false;
            }
        }
        protected string[] AccountTradeItems { get; } = new string[] {
            "OrderUuid", // 0
                "Exchange",  // 1
                "TimeStamp",  // 2
                "OrderType", // 3
                "Limit",     // 4
                "Quantity",  // 5
                "QuantityRemaining", // 6
                "Commission", // 7
                "Price",  // 8
                "PricePerUnit",  // 9
                "IsConditional",
                "Condition",
                "ConditionTarget",
                "ImmediateOrCancel",
        };
        bool OnGetAccountTrades(AccountInfo account, Ticker ticker, byte[] bytes) {
            if(bytes == null)
                return false;

            int startIndex = 1;
            if(!JSonHelper.Default.SkipSymbol(bytes, ':', 3, ref startIndex))
                return false;

            List<TradeInfoItem> myTrades = ticker != null ? ticker.MyTradeHistory : account.MyTrades;
            List<string[]> res = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, AccountTradeItems);
            if(res == null)
                return false;
            if(res.Count == 0)
                return true;
            lock(myTrades) {
                myTrades.Clear();
                foreach(string[] obj in res) {
                    Ticker t = ticker == null ? Tickers.FirstOrDefault(tt => tt.CurrencyPair == obj[1]) : ticker;
                    TradeInfoItem item = new TradeInfoItem(account, t);
                    item.OrderNumber = obj[0];
                    item.IdString = obj[0];
                    item.Type = obj[3] == "LIMIT_BUY" ? TradeType.Buy : TradeType.Sell;
                    item.AmountString = obj[5];
                    item.RateString = obj[9];
                    item.Fee = FastValueConverter.Convert(obj[7]);
                    item.Total = FastValueConverter.Convert(obj[8]);
                    item.TimeString = obj[2];
                    myTrades.Add(item);
                }
            }
            return true;
        }
        public override List<TradeInfoItem> GetTrades(Ticker info, DateTime starTime) {
            string address = string.Format("https://bittrex.com/api/v1.1/public/getmarkethistory?market={0}", Uri.EscapeDataString(info.MarketName));
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(address);
            }
            catch(Exception) {
                return null;
            }
            if(bytes == null)
                return null;

            int startIndex = 1;
            if(!JSonHelper.Default.SkipSymbol(bytes, ':', 3, ref startIndex))
                return null;

            List<string[]> res = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "Id", "TimeStamp", "Quantity", "Price", "Total", "FillType", "OrderType" },
                (itemIndex, paramIndex, value) => {
                    return paramIndex != 1 || Convert.ToDateTime(value).ToLocalTime() >= starTime;
                });
            if(res == null)
                return null;
            List<TradeInfoItem> list = new List<TradeInfoItem>();

            int index = 0;
            foreach(string[] obj in res) {
                TradeInfoItem item = new TradeInfoItem(null, info);
                item.Id = Convert.ToInt64(obj[0]);
                item.Time = Convert.ToDateTime(obj[1]).ToLocalTime();
                item.AmountString = obj[2];
                item.RateString = obj[3];
                item.Total = FastValueConverter.Convert(obj[4]);
                item.Type = obj[6].Length == 3 ? TradeType.Buy : TradeType.Sell;
                item.Fill = obj[5].Length == 4 ? TradeFillType.Fill : TradeFillType.PartialFill;
                list.Insert(index, item);
                index++;
            }
            return list;
        }
        public override bool UpdateTrades(Ticker info) {
            string address = string.Format("https://bittrex.com/api/v1.1/public/getmarkethistory?market={0}", Uri.EscapeDataString(info.MarketName));
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(address);
            }
            catch(Exception) {
                return false;
            }
            if(bytes == null)
                return false;

            int startIndex = 1;
            if(!JSonHelper.Default.SkipSymbol(bytes, ':', 3, ref startIndex))
                return false;

            long lastId = info.TradeHistory.Count > 0 ? info.TradeHistory.First().Id : -1;
            string lastIdString = lastId.ToString();
            List<string[]> res = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "Id", "TimeStamp", "Quantity", "Price", "Total", "FillType", "OrderType" },
                (itemIndex, paramIndex, value) => {
                    return paramIndex != 0 || lastIdString != value;
                });
            if(res == null || res.Count == 0)
                return true;

            int index = 0;
            lock(info) {
                foreach(string[] obj in res) {
                    TradeInfoItem item = new TradeInfoItem(null, info);
                    item.Id = Convert.ToInt64(obj[0]);
                    item.Time = Convert.ToDateTime(obj[1]).ToLocalTime();
                    item.AmountString = obj[2];
                    item.RateString = obj[3];
                    item.Total = FastValueConverter.Convert(obj[4]);
                    item.Type = obj[6].Length == 3 ? TradeType.Buy : TradeType.Sell;
                    item.Fill = obj[5].Length == 4 ? TradeFillType.Fill : TradeFillType.PartialFill;
                    info.TradeHistory.Insert(index, item);
                    index++;
                }
            }
            info.RaiseTradeHistoryAdd();
            return true;
        }
        public bool UpdateTradesStatistic(BittrexTicker info, int depth) {
            string address = string.Format("https://bittrex.com/api/v1.1/public/getmarkethistory?market={0}", Uri.EscapeDataString(info.MarketName));
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(address);
            }
            catch(Exception) {
                return false;
            }
            if(bytes == null)
                return false;

            int startIndex = 1;
            if(!JSonHelper.Default.SkipSymbol(bytes, ':', 3, ref startIndex))
                return false;

            string lastIdString = info.LastTradeId.ToString();
            List<string[]> res = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "Id", "TimeStamp", "Quantity", "Price", "Total", "FillType", "OrderType" },
                (itemIndex, paramIndex, value) => {
                    return paramIndex != 0 || lastIdString != value;
                });
            if(res == null)
                return false;

            int lastTradeId = Convert.ToInt32(res[0][0]);
            if(lastTradeId == info.LastTradeId) {
                info.TradeStatistic.Add(new TradeStatisticsItem() { Time = DateTime.UtcNow });
                if(info.TradeStatistic.Count > 5000) {
                    for(int i = 0; i < 100; i++)
                        info.TradeStatistic.RemoveAt(0);
                }
                return true;
            }

            TradeStatisticsItem st = new TradeStatisticsItem();
            st.MinBuyPrice = double.MaxValue;
            st.MinSellPrice = double.MaxValue;
            lock(info) {
                foreach(string[] obj in res) {
                    bool isBuy = obj[6].Length == 3;
                    double price = FastValueConverter.Convert(obj[3]);
                    double amount = FastValueConverter.Convert(obj[2]);
                    if(isBuy) {
                        st.BuyAmount += amount;
                        st.MinBuyPrice = Math.Min(st.MinBuyPrice, price);
                        st.MaxBuyPrice = Math.Max(st.MaxBuyPrice, price);
                        st.BuyVolume += amount * price;
                    }
                    else {
                        st.SellAmount += amount;
                        st.MinSellPrice = Math.Min(st.MinSellPrice, price);
                        st.MaxSellPrice = Math.Max(st.MaxSellPrice, price);
                        st.SellVolume += amount * price;
                    }
                }
            }
            if(st.MinSellPrice == double.MaxValue)
                st.MinSellPrice = 0;
            if(st.MinBuyPrice == double.MaxValue)
                st.MinBuyPrice = 0;
            info.LastTradeId = lastTradeId;
            info.TradeStatistic.Add(st);
            if(info.TradeStatistic.Count > 5000) {
                for(int i = 0; i < 100; i++)
                    info.TradeStatistic.RemoveAt(0);
            }
            return true;
        }

        public override TradingResult Buy(AccountInfo account, Ticker ticker, double rate, double amount) {
            string address = string.Format("https://bittrex.com/api/v1.1/market/buylimit?apikey={0}&nonce={1}&market={2}&quantity={3}&rate={4}",
                Uri.EscapeDataString(account.ApiKey),
                GetNonce(),
                Uri.EscapeDataString(ticker.MarketName),
                amount.ToString("0.########"),
                rate.ToString("0.########"));
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", account.GetSign(address));
            string text = client.DownloadString(address);
            return OnBuy(account, ticker, text);
        }
        public override TradingResult Sell(AccountInfo account, Ticker ticker, double rate, double amount) {
            string address = string.Format("https://bittrex.com/api/v1.1/market/selllimit?apikey={0}&nonce={1}&market={2}&quantity={3}&rate={4}",
                Uri.EscapeDataString(account.ApiKey),
                GetNonce(),
                Uri.EscapeDataString(ticker.MarketName),
                amount.ToString("0.########"),
                rate.ToString("0.########"));
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", account.GetSign(address));
            string text = client.DownloadString(address);
            return OnSell(account, ticker, text);
        }
        public override bool Cancel(AccountInfo account, string orderId) {
            string address = string.Format("https://bittrex.com/api/v1.1/market/cancel?apikey={0}&nonce={1}&uuid={2}",
                Uri.EscapeDataString(account.ApiKey),
                GetNonce(),
                orderId);
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", account.GetSign(address));
            try {
                return OnCancel(client.DownloadString(address));
            }
            catch(Exception) {
                return false;
            }
        }
        public Task<string> Canel(AccountInfo account, string orderId) {
            string address = string.Format("https://bittrex.com/api/v1.1/market/cancel?apikey={0}&nonce={1}&uuid={2}",
                Uri.EscapeDataString(account.ApiKey),
                GetNonce(),
                orderId);
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", account.GetSign(address));
            return client.DownloadStringTaskAsync(address);
        }
        public Task<string> GetOpenOrders(AccountInfo account, Ticker info) {
            string address = string.Format("https://bittrex.com/api/v1.1/market/getopenorders?apikey={0}&nonce={1}&market={2}",
                Uri.EscapeDataString(account.ApiKey),
                GetNonce(),
                info.MarketName);
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", account.GetSign(address));
            return client.DownloadStringTaskAsync(address);
        }
        public override bool UpdateOpenedOrders(AccountInfo account, Ticker ticker) {
            string address = string.Empty;
            if(ticker != null) {
                address = string.Format("https://bittrex.com/api/v1.1/market/getopenorders?apikey={0}&nonce={1}&market={2}",
                Uri.EscapeDataString(account.ApiKey),
                GetNonce(),
                ticker.MarketName);
            }
            else {
                address = string.Format("https://bittrex.com/api/v1.1/market/getopenorders?apikey={0}&nonce={1}",
                Uri.EscapeDataString(account.ApiKey),
                GetNonce());
            }
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", account.GetSign(address));
            byte[] bytes = null;
            try {
                bytes = client.DownloadData(address);
                if(ticker != null) {
                    if(!ticker.IsOpenedOrdersChanged(bytes))
                        return true;
                }
                else {
                    if(!IsOpenedOrdersChanged(bytes))
                        return true;
                }
            } catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
            return OnUpdateOrders(account, ticker, bytes);
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
        public TradingResult OnBuy(AccountInfo account, Ticker ticker, string result) {
            string res = OnUuidResult(result);
            TradingResult t = new TradingResult();
            t.Trades.Add(new TradeEntry() { Id = res });
            return t;
        }
        public TradingResult OnSell(AccountInfo account, Ticker ticker, string result) {
            string res = OnUuidResult(result);
            TradingResult t = new TradingResult();
            t.Trades.Add(new TradeEntry() { Id = res });
            return t;
        }
        public bool OnCancel(string result) {
            if(string.IsNullOrEmpty(result))
                return false;
            JObject res = JsonConvert.DeserializeObject<JObject>(result);
            foreach(JProperty prop in res.Children()) {
                if(prop.Name == "success") {
                    return prop.Value.Value<bool>();
                }
            }
            return false;
        }
        protected string[] OpenedOrderItems { get; } = new string[] {
                "Uuid",
                "OrderUuid",
                "Exchange",
                "OrderType",
                "Quantity",
                "QuantityRemaining",
                "Limit",
                "CommissionPaid",
                "Price",
                "PricePerUnit",
                "Opened",
                "Closed",
                "CancelInitiated",
                "ImmediateOrCancel",
                "IsConditional",
                "Condition",
                "ConditionTarget"
            };
        public bool OnUpdateOrders(AccountInfo account, Ticker ticker, byte[] bytes) {
            if(bytes == null)
                return false;

            int startIndex = 1;
            if(!JSonHelper.Default.SkipSymbol(bytes, ':', 3, ref startIndex)) {
                Telemetry.Default.TrackEvent("bittrex.onupdateorders", new string[] { "data", UTF8Encoding.Default.GetString(bytes) }, true);
                return false;
            }

            List<string[]> res = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, OpenedOrderItems);
            List<OpenedOrderInfo> openedOrders = ticker == null ? account.OpenedOrders : ticker.OpenedOrders;
            openedOrders.Clear();
            if(res == null || res.Count == 0)
                return true;
            
            lock(openedOrders) {
                foreach(string[] obj in res) {
                    Ticker t = ticker == null ? Tickers.FirstOrDefault(tt => tt.CurrencyPair == obj[2]): ticker;
                    BittrexOrderInfo info = new BittrexOrderInfo(account, t);

                    info.OrderId = obj[1];
                    info.OrderUuid = obj[1];
                    info.Type = obj[3] == "LIMIT_SELL" ? OrderType.Sell : OrderType.Buy;
                    info.AmountString = obj[5]; //obj[4];
                    info.QuantityRemainingString = obj[5];
                    info.LimitString = obj[6];
                    info.CommissionPaidString = obj[7];
                    info.ValueString = obj[3][0] == 'L' ? obj[6] : obj[9];
                    info.TotalString = (info.Amount * info.Value).ToString("0.########");
                    info.DateString = obj[10];
                    info.Closed = obj[11] == "null" ? DateTime.MinValue : Convert.ToDateTime(obj[11]).ToLocalTime();
                    info.CancelInitiated = obj[12].Length == 4 ? true : false;
                    info.ImmediateOrCancel = obj[13].Length == 4 ? true : false;
                    info.IsConditional = obj[14].Length == 4 ? true : false;
                    info.Condition = obj[15];
                    info.ConditionTarget = obj[16];

                    openedOrders.Add(info);
                }
            }

            if(ticker != null)
                ticker.RaiseOpenedOrdersChanged();
            return true;
        }
        
        public override bool GetBalance(AccountInfo account, string currency) {
            string address = string.Format("https://bittrex.com/api/v1.1/account/getbalance?apikey={0}&nonce={1}&currency={2}", Uri.EscapeDataString(account.ApiKey), GetNonce(), currency);
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", account.GetSign(address));
            try {
                return OnGetBalance(account, client.DownloadString(address));
            }
            catch(Exception) {
                return false;
            }
        }
        public bool OnGetBalance(AccountInfo account, string text) {
            if(string.IsNullOrEmpty(text))
                return false;
            JObject res = JsonConvert.DeserializeObject<JObject>(text);
            if(res.Value<bool>("success") == false) {
                Debug.WriteLine("OnGetBalance fails: " + res.Value<string>("message"));
                return false;
            }
            JObject obj = res.Value<JObject>("result");
            lock(account.Balances) {
                string currency = obj.Value<string>("Currency");
                BittrexAccountBalanceInfo info = (BittrexAccountBalanceInfo)account.Balances.FirstOrDefault((b) => b.Currency == currency);
                if(info == null) {
                    info = new BittrexAccountBalanceInfo(account);
                    info.Currency = obj.Value<string>("Currency");
                    info.Requested = obj.Value<bool>("Requested");
                    info.Uuid = obj.Value<string>("Uuid");
                    account.Balances.Add(info);
                }
                info.LastAvailable = info.Available;
                info.Available = obj.Value<string>("Available") == null ? 0 : obj.Value<double>("Available");
                info.Balance = obj.Value<string>("Balance") == null ? 0 : obj.Value<double>("Balance");
                info.Pending = obj.Value<string>("Pending") == null ? 0 : obj.Value<double>("Pending");
                info.DepositAddress = obj.Value<string>("CryptoAddress");
            }
            return true;
        }
        protected string[] BalanceItems { get; } = new string[] {
            "Currency",
                    "Balance",
                    "Available",
                    "Pending",
                    "CryptoAddress"
        };
        public override bool UpdateBalances(AccountInfo account) {
            if(Currencies.Count == 0) {
                if(!GetCurrenciesInfo())
                    return false;
            }
            string address = string.Format("https://bittrex.com/api/v1.1/account/getbalances?apikey={0}&nonce={1}", Uri.EscapeDataString(account.ApiKey), GetNonce());
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", account.GetSign(address));
            try {
                byte[] bytes = client.DownloadData(address);


                if(bytes == null)
                    return false;

                int startIndex = 1;
                if(!JSonHelper.Default.SkipSymbol(bytes, ':', 3, ref startIndex))
                    return false;

                List<string[]> res = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, BalanceItems);
                if(res == null) {
                    return false;
                }
                foreach(string[] item in res) {
                    BittrexAccountBalanceInfo binfo = (BittrexAccountBalanceInfo)account.Balances.FirstOrDefault(b => b.Currency == item[0]);
                    if(binfo == null) {
                        binfo = new BittrexAccountBalanceInfo(account);
                        binfo.Currency = item[0];
                        account.Balances.Add(binfo);
                    }
                    binfo.Balance = FastValueConverter.Convert(item[1]);
                    binfo.Available = FastValueConverter.Convert(item[2]);
                    binfo.Pending = FastValueConverter.Convert(item[3]);
                    binfo.DepositAddress = item[4];
                }
            }
            catch(Exception) {
                return false;
            }
            return true;
        }
        public Task<string> GetBalancesAsync(AccountInfo account) {
            string address = string.Format("https://bittrex.com/api/v1.1/account/getbalances?apikey={0}&nonce={1}", Uri.EscapeDataString(account.ApiKey), GetNonce());
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", account.GetSign(address));
            return client.DownloadStringTaskAsync(address);
        }
        public bool OnGetBalances(AccountInfo account, string text) {
            if(string.IsNullOrEmpty(text))
                return false;
            JObject res = JsonConvert.DeserializeObject<JObject>(text);
            if(res.Value<bool>("success") == false) {
                Debug.WriteLine("OnGetBalances fails: " + res.Value<string>("message"));
                return false;
            }
            JArray balances = res.Value<JArray>("result");
            lock(account.Balances) {
                account.Balances.Clear();
                foreach(JObject obj in balances) {
                    BittrexAccountBalanceInfo item = new BittrexAccountBalanceInfo(account);
                    item.Currency = obj.Value<string>("Currency");
                    item.Balance = obj.Value<double>("Balance");
                    item.Available = obj.Value<double>("Available");
                    item.Pending = obj.Value<double>("Pending");
                    item.DepositAddress = obj.Value<string>("CryptoAddress");
                    item.Requested = obj.Value<bool>("Requested");
                    item.Uuid = obj.Value<string>("Uuid");
                    account.Balances.Add(item);
                }
            }
            RaiseBalancesChanged();
            return true;
        }
        void RaiseBalancesChanged() {

        }

        public override bool Withdraw(AccountInfo account, string currency, string address, string paymentId, double amount) {
            string addr = string.Empty;
            if(string.IsNullOrEmpty(paymentId)) {
                addr = string.Format("https://bittrex.com/api/v1.1/account/withdraw?apikey={0}&nonce={1}&currency={2}&quantity={3}",
                    Uri.EscapeDataString(account.ApiKey),
                    GetNonce(),
                    Uri.EscapeDataString(currency),
                    amount.ToString("0.########"));
            }
            else {
                addr = string.Format("https://bittrex.com/api/v1.1/account/withdraw?apikey={0}&nonce={1}&currency={2}&quantity={3}&paymentid={4}",
                        Uri.EscapeDataString(account.ApiKey),
                        GetNonce(),
                        Uri.EscapeDataString(currency),
                        amount.ToString("0.########"),
                        Uri.EscapeDataString(paymentId));
            }
            WebClient client = GetWebClient();
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
        public Task<string> WithdrawAsync(AccountInfo account, string currency, string address, string paymentId, double amount) {
            string addr = string.Empty;
            if(string.IsNullOrEmpty(paymentId)) {
                addr = string.Format("https://bittrex.com/api/v1.1/account/withdraw?apikey={0}&nonce={1}&currency={2}&quantity={3}",
                    Uri.EscapeDataString(account.ApiKey),
                    GetNonce(),
                    Uri.EscapeDataString(currency),
                    amount.ToString("0.########"));
            }
            else {
                addr = string.Format("https://bittrex.com/api/v1.1/account/withdraw?apikey={0}&nonce={1}&currency={2}&quantity={3}&paymentid={4}",
                        Uri.EscapeDataString(account.ApiKey),
                        GetNonce(),
                        Uri.EscapeDataString(currency),
                        amount.ToString("0.########"),
                        Uri.EscapeDataString(paymentId));
            }
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", account.GetSign(address));
            return client.DownloadStringTaskAsync(addr);
        }
        public string OnWithdraw(string result) {
            return OnUuidResult(result);
        }

        public override string CreateDeposit(AccountInfo account, string currency) {
            string address = string.Format("https://bittrex.com/api/v1.1/account/getdepositaddress?apikey={0}&nonce={1}&currency={2}", Uri.EscapeDataString(account.ApiKey), GetNonce(), currency);
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", account.GetSign(address));
            return OnGetDeposit(account, currency, client.DownloadString(address));
        }
        string OnGetDeposit(AccountInfo account, string currency, string text) {
            if(string.IsNullOrEmpty(text))
                return null;
            JObject res = JsonConvert.DeserializeObject<JObject>(text);
            if(res.Value<bool>("success") == false) {
                string error = res.Value<string>("message");
                if(error == "ADDRESS_GENERATING")
                    LogManager.Default.AddWarning("Bittrex: OnGetDeposit fails: " + error + ". Try again later after deposit address generate.", "Currency = " + currency);
                else
                    LogManager.Default.AddError("Bittrex: OnGetDeposit fails: " + error, "Currency = " + currency);
                return null;
            }
            JObject addr = res.Value<JObject>("result");
            BittrexAccountBalanceInfo info = (BittrexAccountBalanceInfo)account.Balances.FirstOrDefault(b => b.Currency == currency);
            info.DepositAddress = addr.Value<string>("Address");
            return info.DepositAddress;
        }

        string GetNonce() {
           return ((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
        }
    }
}
