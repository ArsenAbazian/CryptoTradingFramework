using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptoMarketClient.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuperSocket.ClientEngine;
using WebSocket4Net;

namespace CryptoMarketClient.Exchanges.Bitmex {
    public class BitmexExchange : Exchange {
        static BitmexExchange defaultExchange;
        public static BitmexExchange Default {
            get {
                if(defaultExchange == null) {
                    defaultExchange = new BitmexExchange();
                    defaultExchange.Load();
                }
                return defaultExchange;
            }
        }


        public override bool AllowCandleStickIncrementalUpdate => false;

        public override ExchangeType Type => ExchangeType.Bitmex;

        public override string BaseWebSocketAdress => "wss://www.bitmex.com/realtime?subscribe=instrument";

        public override TradingResult Buy(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public override bool Cancel(AccountInfo account, string orderId) {
            throw new NotImplementedException();
        }

        public override Form CreateAccountForm() {
            return new AccountBalancesForm(this);
        }

        public override string CreateDeposit(AccountInfo account, string currency) {
            throw new NotImplementedException();
        }

        public override List<CandleStickIntervalInfo> GetAllowedCandleStickIntervals() {
            return new List<CandleStickIntervalInfo>();
        }

        public override bool GetBalance(AccountInfo info, string currency) {
            return true;
        }

        public override bool GetDeposites(AccountInfo account) {
            throw new NotImplementedException();
        }

        public override bool GetTickersInfo() {
            string address = "https://www.bitmex.com/api/v1/instrument?columns=symbol,rootSymbol,quoteCurrency,highPrice,lowPrice,bidPrice,askPrice,lastChangePcnt,hasLiquidity,volume,tickSize&start=0&count=500";
            string text = string.Empty;
            try {
                text = GetDownloadString(address);
            }
            catch(Exception) {
                return false;
            }
            if(string.IsNullOrEmpty(text))
                return false;
            Tickers.Clear();
            JArray res = JsonConvert.DeserializeObject<JArray>(text);
            int index = 0;
            foreach(JObject obj in res.Children()) {
                if(!obj.Value<bool>("hasLiquidity"))
                    continue;
                string pair = obj.Value<string>("symbol");
                BitmexTicker t = (BitmexTicker)Tickers.FirstOrDefault(tt => tt.CurrencyPair == pair);
                if(t == null) t = new BitmexTicker(this);
                t.Index = index;
                t.CurrencyPair = pair;
                t.MarketCurrency = obj.Value<string>("rootSymbol");
                t.BaseCurrency = obj.Value<string>("quoteCurrency");
                t.TickSize = ToDouble(obj, "tickSize");
                t.Last = ToDouble(obj, "lastPrice");
                t.Hr24High = ToDouble(obj, "highPrice");
                t.Hr24Low = ToDouble(obj, "lowPrice");
                t.HighestBid = ToDouble(obj, "bidPrice");
                t.LowestAsk = ToDouble(obj, "askPrice");
                t.Timestamp = Convert.ToDateTime(obj.Value<string>("timestamp"));
                t.Change = ToDouble(obj, "lastChangePcnt");
                t.Volume = ToDouble(obj, "volume24h");
                Tickers.Add(t);
                index++;
            }
            IsInitialized = true;
            return true;
        }

        double ToDouble(JObject obj, string name) {
            return obj.Value<string>(name) == null ? 0.0 : obj.Value<double>(name);
        }

        public override List<TradeInfoItem> GetTrades(Ticker ticker, DateTime starTime) {
            throw new NotImplementedException();
        }

        public override void ObtainExchangeSettings() {
            //throw new NotImplementedException();
        }

        public override void OnAccountRemoved(AccountInfo info) {
            throw new NotImplementedException();
        }

        public override bool ProcessOrderBook(Ticker tickerBase, string text) {
            throw new NotImplementedException();
        }

        public override TradingResult Sell(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public override bool SupportWebSocket(WebSocketType type) {
            if(type == WebSocketType.Ticker)
                return true;
            if(type == WebSocketType.Tickers)
                return true;
            return false;
        }

        public override bool UpdateAccountTrades(AccountInfo account, Ticker ticker) {
            return true;
        }

        public override bool UpdateBalances(AccountInfo info) {
            return true;
        }

        public override bool UpdateCurrencies() {
            return true;
        }

        public override bool UpdateOpenedOrders(AccountInfo account, Ticker ticker) {
            return true;
        }

        string GetOrderBookString(Ticker ticker, int depth) {
            return string.Format("https://www.bitmex.com/api/v1/orderBook/L2?symbol={0}&depth=0", ticker.CurrencyPair);
        }
        public override bool UpdateArbitrageOrderBook(Ticker tickerBase, int depth) {
            throw new NotImplementedException();
        }
        public override bool UpdateOrderBook(Ticker ticker) {
            string address = GetOrderBookString(ticker, OrderBook.Depth);
            byte[] data = GetDownloadBytes(address);
            if(data == null)
                return false;
            return UpdateOrderBook(ticker, data);
        }

        protected string[] OrderBookItems { get; } = new string[] { "symbol", "id", "side", "size", "price" };

        bool UpdateOrderBook(Ticker ticker, byte[] bytes) {
            if(bytes == null)
                return false;

            int startIndex = 0; // skip {

            List<string[]> items = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, OrderBookItems);

            List<OrderBookEntry> bids = ticker.OrderBook.Bids;
            List<OrderBookEntry> asks = ticker.OrderBook.Asks;
            List<OrderBookEntry> iasks = ticker.OrderBook.AsksInverted;
            foreach(string[] item in items) {
                OrderBookEntry entry = new OrderBookEntry();

                entry.Id = FastValueConverter.ConvertPositiveLong(item[1]);
                entry.ValueString = item[4];
                entry.AmountString = item[3];

                if(item[2][0] == 'S') {
                    iasks.Add(entry);
                    asks.Insert(0, entry);
                }
                else
                    bids.Add(entry);
            }
            ticker.OrderBook.UpdateEntries();
            return true;
        }

        public override bool UpdateTicker(Ticker tickerBase) {
            return true;
        }

        Stopwatch UpdateTickersTimer { get; } = new Stopwatch();
        public override bool UpdateTickersInfo() {
            if(!UpdateTickersTimer.IsRunning)
                UpdateTickersTimer.Start();
            if(UpdateTickersTimer.ElapsedMilliseconds < 5000)
                return true;
            bool res = GetTickersInfo();
            UpdateTickersTimer.Restart();
            return res;
        }

        public override bool UpdateTrades(Ticker tickerBase) {
            return true;
        }

        public override bool Withdraw(AccountInfo account, string currency, string adress, string paymentId, double amount) {
            throw new NotImplementedException();
        }

        protected internal override IIncrementalUpdateDataProvider CreateIncrementalUpdateDataProvider() {
            return new BitmexIncDataProvider(this);
        }

        public override void StartListenTickerStream(Ticker ticker) {
            base.StartListenTickerStream(ticker);

            SocketConnectionInfo info = CreateOrderBookWebSocket(ticker);
            OrderBookSockets.Add(info.Socket, info);
        }

        protected override string GetOrderBookSocketAddress(Ticker ticker) {
            return string.Format("wss://www.bitmex.com/realtime?subscribe=orderBookL2:{0}", ticker.CurrencyPair);
        }

        protected override string GetTradeSocketAddress(Ticker ticker) {
            return string.Format("wss://www.bitmex.com/realtime?subscribe=trade:{0}", ticker.CurrencyPair);
        }

        OrderBookUpdateType String2UpdateType(string action) {
            OrderBookUpdateType type = OrderBookUpdateType.Modify;
            if(action == "insert")
                type = OrderBookUpdateType.Add;
            if(action == "delete")
                type = OrderBookUpdateType.Remove;
            return type;
        }

        protected override void OnOrderBookSocketMessageReceived(object sender, MessageReceivedEventArgs e) {
            base.OnOrderBookSocketMessageReceived(sender, e);
            LastWebSocketRecvTime = DateTime.Now;
            SocketConnectionInfo info = OrderBookSockets[(WebSocket)sender];

            JObject obj = JsonConvert.DeserializeObject<JObject>(e.Message);
            JArray items = obj.Value<JArray>("data");
            if(items == null)
                return;

            OrderBookUpdateType type = String2UpdateType(obj.Value<string>("action"));
            foreach(JObject item in items) {
                Ticker t = info.Ticker;

                OrderBookEntryType entryType = item.Value<string>("side")[0] == 'S' ? OrderBookEntryType.Ask : OrderBookEntryType.Bid;
                string rate = type == OrderBookUpdateType.Add ? item.Value<string>("price") : null;
                string size = type != OrderBookUpdateType.Remove ? item.Value<string>("size") : null;
                t.OrderBook.ApplyIncrementalUpdate(entryType, type, item.Value<long>("id"), rate, size);
            }
        }

        protected override void OnOrderBookSocketClosed(object sender, EventArgs e) {
            SocketConnectionInfo info = OrderBookSockets[(WebSocket)sender];
            info.State = SocketConnectionState.Disconnected;
        }

        protected override void OnOrderBookSocketOpened(object sender, EventArgs e) {
            SocketConnectionInfo info = OrderBookSockets[(WebSocket)sender];
            info.State = SocketConnectionState.Connected;
            info.Ticker.UpdateOrderBook();
        }

        protected override void OnOrderBookSocketError(object sender, ErrorEventArgs e) {
            SocketConnectionInfo info = OrderBookSockets[(WebSocket)sender];
            info.State = SocketConnectionState.Error;
            info.LastError = e.Exception.ToString();
        }

        protected override void OnTickersSocketMessageReceived(object sender, MessageReceivedEventArgs e) {
            base.OnTickersSocketMessageReceived(sender, e);

            JObject res = JsonConvert.DeserializeObject<JObject>(e.Message, 
                new JsonSerializerSettings() {
                    Converters = new List<JsonConverter>(),
                    FloatParseHandling = FloatParseHandling.Double });
            if(res.Value<string>("table") == "instrument")
                OnTickersInfoRecv(res);
        }

        protected void OnTickersInfoRecv(JObject jObject) {
            JArray items = jObject.Value<JArray>("data");
            if(items == null)
                return;
            foreach(JObject item in items) {
                string tickerName = item.Value<string>("symbol");
                BitmexTicker t = (BitmexTicker)Tickers.FirstOrDefault(tt => tt.CurrencyPair == tickerName);
                if(t == null)
                    continue;

                JEnumerable<JToken> props = item.Children();
                foreach(JProperty prop in props) {
                    string name = prop.Name;
                    string value = prop.Value == null? null: prop.Value.ToString();
                    switch(name) {
                        case "lastPrice":
                            t.Last = FastValueConverter.Convert(value);
                            break;
                        case "highPrice":
                            t.Hr24High = FastValueConverter.Convert(value);
                            break;
                        case "lowPrice":
                            t.Hr24Low = FastValueConverter.Convert(value);
                            break;
                        case "bidPrice":
                            t.HighestBid = FastValueConverter.Convert(value); 
                            break;
                        case "askPrice":
                            t.LowestAsk = FastValueConverter.Convert(value);
                            break;
                        case "timestamp":
                            t.Timestamp = t.Time = Convert.ToDateTime(value);
                            break;
                        case "lastChangePcnt":
                            t.Change = FastValueConverter.Convert(value);
                            break;
                        case "volume24h":
                            t.Volume = FastValueConverter.Convert(value);
                            break;
                    }
                }
                t.UpdateTrailings();

                lock(t) {
                    RaiseTickerUpdate(t);
                }
            }
        }

        protected void OnTickerOrderBookRecv(JObject jObject) {
            
        }
    }
}
