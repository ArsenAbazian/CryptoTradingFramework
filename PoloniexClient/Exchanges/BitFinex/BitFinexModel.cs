using CryptoMarketClient.BitFinex;
using CryptoMarketClient.Common;
using CryptoMarketClient.Helpers;
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
using WebSocket4Net;

namespace CryptoMarketClient.BitFinex {
    public class BitFinexExchange : Exchange {
        static BitFinexExchange defaultExchange;
        public static BitFinexExchange Default {
            get {
                if(defaultExchange == null) {
                    defaultExchange = new BitFinexExchange();
                    defaultExchange.Load();
                }
                return defaultExchange;
            }
        }

        public override void OnAccountRemoved(ExchangeAccountInfo info) {
            
        }

        public override string TickersWebSocketAddress => "wss://api.bitfinex.com/ws/2";

        public override ExchangeType Type => ExchangeType.BitFinex;

        public override bool SupportWebSocket(WebSocketType type) {
            if(type == WebSocketType.Tickers)
                return true;
            return false;
        }

        public override bool GetDeposites() {
            return true;
        }

        public override Form CreateAccountForm() {
            return new AccountBalancesForm(this);
        }

        public override void ObtainExchangeSettings() { }

        protected override void OnSocketOpened(object sender, EventArgs e) {
            ((WebSocket)sender).Send(JSonHelper.Default.Serialize(new string[] { "event", "subscribe", "channel", "ticker" }));
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

        public List<BitFinexCurrencyInfo> Currencies { get; } = new List<BitFinexCurrencyInfo>();
        protected List<TradeHistoryItem> UpdateList { get; } = new List<TradeHistoryItem>(100);

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
        public override BindingList<CandleStickData> GetCandleStickData(TickerBase ticker, int candleStickPeriodMin, DateTime start, long periodInSeconds) {
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
                data.Time = Convert.ToDateTime(item[5]);
                data.High = FastDoubleConverter.Convert(item[1]);
                data.Low = FastDoubleConverter.Convert(item[2]);
                data.Open = FastDoubleConverter.Convert(item[0]);
                data.Close = FastDoubleConverter.Convert(item[3]);
                data.Volume = FastDoubleConverter.Convert(item[6]);
                data.QuoteVolume = FastDoubleConverter.Convert(item[4]);
                data.WeightedAverage = 0;
                list.Add(data);
            }
            return list;
        }

        protected string TickersUpdateAddress { get; set; }

        public override bool GetTickersInfo() {
            if(Tickers.Count > 0)
                return true;

            string address = "https://api.bitfinex.com/v1/symbols";
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(address);
            }
            catch(Exception) {
                return false;
            }
            if(bytes == null)
                return false;

            string text = UTF8Encoding.Default.GetString(bytes);
            text = text.Replace('[', ' ');
            text = text.Replace(']', ' ');
            text = text.Replace('"', ' ');

            string[] tickers = text.Split(',');
            foreach(string item in tickers) {
                BitFinexTicker m = new BitFinexTicker(this);
                string currencyPair = item.Trim();

                m.MarketCurrency = currencyPair.Substring(0, 3).ToUpper();
                m.BaseCurrency = currencyPair.Substring(3, 3).ToUpper();
                m.CurrencyPair = "t" + currencyPair.ToUpper();
                m.MarketName = m.CurrencyPair;
                m.Index = Tickers.Count;
                Tickers.Add(m);
            }
            address = "https://api.bitfinex.com/v2/tickers?symbols=";
            foreach(BitFinexTicker ticker in Tickers) {
                address += ticker.CurrencyPair;
                if(Tickers.IndexOf(ticker) < Tickers.Count - 1)
                    address += ",";
            }
            TickersUpdateAddress = address;

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
                BitFinexCurrencyInfo c = Currencies.FirstOrDefault(curr => curr.Currency == currency);
                if(c == null) {
                    c = new BitFinexCurrencyInfo();
                    c.Currency = item[0];
                    c.CurrencyLong = item[1];
                    c.MinConfirmation = int.Parse(item[2]);
                    c.TxFree = FastDoubleConverter.Convert(item[3]);
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
        public void GetTicker(BitFinexTicker info) {
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
            info.HighestBid = FastDoubleConverter.Convert(res[0]);
            info.LowestAsk = FastDoubleConverter.Convert(res[1]);
            info.Last = FastDoubleConverter.Convert(res[2]);
            info.Time = DateTime.UtcNow;
            info.UpdateHistoryItem();
        }
        public override bool UpdateTicker(TickerBase tickerBase) {
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(string.Format("https://api.bitfinex.com/v2/ticker/{0}", ((BitFinexTicker)tickerBase).MarketName));
            }
            catch(Exception e) {
                Debug.WriteLine(e.ToString());
                return false;
            }
            if(bytes == null)
                return false;

            int startIndex = 0;
            string[] item = JSonHelper.Default.DeserializeArray(bytes, ref startIndex, 9);

            BitFinexTicker ticker = (BitFinexTicker)tickerBase;
            ticker.HighestBid = FastDoubleConverter.Convert(item[0]);
            ticker.LowestAsk = FastDoubleConverter.Convert(item[2]);
            ticker.Change = FastDoubleConverter.Convert(item[4]);
            ticker.Last = FastDoubleConverter.Convert(item[5]);
            ticker.Volume = FastDoubleConverter.Convert(item[6]);
            ticker.Hr24High = FastDoubleConverter.Convert(item[7]);
            ticker.Hr24Low = FastDoubleConverter.Convert(item[8]);
            ticker.Time = DateTime.Now;
            ticker.UpdateHistoryItem();

            return true;
        }
        public override bool UpdateTickersInfo() {
            byte[] bytes = null;
            try {
                bytes = GetDownloadBytes(TickersUpdateAddress);
            }
            catch(Exception e) {
                Debug.WriteLine(e.ToString());
                return false;
            }
            if(bytes == null)
                return false;

            int startIndex = 0;
            List<string[]> list = JSonHelper.Default.DeserializeArrayOfArrays(bytes, ref startIndex, 11);
            foreach(string[] item in list) {
                BitFinexTicker ticker = (BitFinexTicker)Tickers.FirstOrDefault(t => t.CurrencyPair == item[0]);
                ticker.HighestBid = FastDoubleConverter.Convert(item[1]);
                ticker.LowestAsk = FastDoubleConverter.Convert(item[3]);
                ticker.Change = FastDoubleConverter.Convert(item[6]);
                ticker.Last = FastDoubleConverter.Convert(item[7]);
                ticker.Volume = FastDoubleConverter.Convert(item[8]);
                ticker.Hr24High = FastDoubleConverter.Convert(item[9]);
                ticker.Hr24Low = FastDoubleConverter.Convert(item[10]);
            }

            return true;
        }
        public bool UpdateArbitrageOrderBook(TickerBase info, int depth) {
            string address = GetOrderBookString(info, depth);
            byte[] data = GetDownloadBytes(address);
            if(data == null)
                return false;
            return UpdateOrderBook(info, data, false, depth);
        }
        public string GetOrderBookString(TickerBase info, int depth) {
            return string.Format("https://api.bitfinex.com/v2/book/{0}/P0", Uri.EscapeDataString(info.MarketName));
        }
        public override bool ProcessOrderBook(TickerBase tickerBase, string text) {
            throw new NotImplementedException();
        }
        public bool UpdateOrderBook(BitFinexTicker info, byte[] data, int depth) {
            return UpdateOrderBook(info, data, true, depth);
        }
        public override bool UpdateOrderBook(TickerBase tickerBase) {
            return UpdateArbitrageOrderBook(tickerBase, OrderBook.Depth);
        }
        public bool UpdateOrderBook(TickerBase ticker, byte[] bytes, bool raiseChanged, int depth) {
            if(bytes == null)
                return false;

            int startIndex = 0;

            List<string[]> items = JSonHelper.Default.DeserializeArrayOfArrays(bytes, ref startIndex, 3);

            ticker.OrderBook.GetNewBidAsks();
            int bidIndex = 0, askIndex = 0;
            OrderBookEntry[] bids = ticker.OrderBook.Bids;
            OrderBookEntry[] asks = ticker.OrderBook.Asks;
            foreach(string[] item in items) {
                OrderBookEntry entry = null;
                if(item[2][0] == '-') {
                    entry = asks[askIndex];
                    entry.Amount = -FastDoubleConverter.Convert(item[2]);
                    askIndex++;
                }
                else {
                    entry = bids[bidIndex];
                    entry.AmountString = item[2];
                    bidIndex++;
                }
                entry.ValueString = item[0];
                
                if(bidIndex >= bids.Length || askIndex >= asks.Length)
                    break;
            }
            ticker.OrderBook.UpdateEntries();
            ticker.OrderBook.RaiseOnChanged(new OrderBookUpdateInfo() { Action = OrderBookUpdateType.RefreshAll });
            return true;
        }
        public void GetOrderBook(BitFinexTicker info, int depth) {
            string address = string.Format("https://bittrex.com/api/v1.1/public/getorderbook?market={0}&type=both&depth={1}", Uri.EscapeDataString(info.MarketName), depth);
            byte[] data = GetDownloadBytes(address);
            if(data == null)
                return;
            UpdateOrderBook(info, data, depth);
        }
        public bool GetTrades(BitFinexTicker info) {
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

            List<string[]> res = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "Id", "TimeStamp", "Quantity", "Price", "Total", "FillType", "OrderType" });
            if(res == null)
                return false;
            lock(info) {
                info.TradeHistory.Clear();
                foreach(string[] obj in res) {
                    TradeHistoryItem item = new TradeHistoryItem();
                    item.Id = Convert.ToInt64(obj[0]); ;
                    item.Time = Convert.ToDateTime(obj[1]);
                    item.AmountString = obj[2];
                    item.RateString = obj[3];
                    item.Total = FastDoubleConverter.Convert(obj[4]);
                    item.Type = obj[6].Length == 3 ? TradeType.Buy : TradeType.Sell;
                    item.Fill = obj[5].Length == 4 ? TradeFillType.Fill : TradeFillType.PartialFill;
                    info.TradeHistory.Add(item);
                }
            }
            info.RaiseTradeHistoryAdd();
            return true;
        }
        public override bool UpdateMyTrades(TickerBase ticker) {
            string address = string.Format("https://bittrex.com/api/v1.1/account/getorderhistory?apikey={0}&nonce={1}&market={2}",
                Uri.EscapeDataString(ApiKey),
                GetNonce(),
                ticker.MarketName);
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", GetSign(address));
            try {
                byte[] bytes = client.DownloadData(address);
                return OnGetMyTrades(ticker, bytes);
            }
            catch(Exception e) {
                Debug.WriteLine("error getting my trades: " + e.ToString());
                return false;
            }
        }
        bool OnGetMyTrades(TickerBase ticker, byte[] bytes) {
            if(bytes == null)
                return false;

            int startIndex = 1;
            if(!JSonHelper.Default.SkipSymbol(bytes, ':', 3, ref startIndex))
                return false;

            string tradeUuid = ticker.MyTradeHistory.Count == 0 ? null : ticker.MyTradeHistory.First().IdString;
            List<string[]> res = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, new string[] {
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
            },
            (itemIndex, paramIndex, value) => {
                return paramIndex != 0 || value != tradeUuid;
            });
            if(res == null)
                return false;
            if(res.Count == 0)
                return true;
            int index = 0;
            foreach(string[] obj in res) {
                TradeHistoryItem item = new TradeHistoryItem();
                item.IdString = obj[0];
                item.Type = obj[3] == "LIMIT_BUY" ? TradeType.Buy : TradeType.Sell;
                item.AmountString = obj[5];
                item.RateString = obj[9];
                item.Fee = FastDoubleConverter.Convert(obj[7]);
                item.Total = FastDoubleConverter.Convert(obj[8]);
                item.TimeString = obj[2];
                ticker.MyTradeHistory.Insert(index, item);
                index++;
            }

            return true;
        }
        public override List<TradeHistoryItem> GetTrades(TickerBase info, DateTime starTime) {
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
                    return paramIndex != 1 || Convert.ToDateTime(value) >= starTime;
                });
            if(res == null)
                return null;
            List<TradeHistoryItem> list = new List<TradeHistoryItem>();

            int index = 0;
            foreach(string[] obj in res) {
                TradeHistoryItem item = new TradeHistoryItem();
                item.Id = Convert.ToInt64(obj[0]);
                item.Time = Convert.ToDateTime(obj[1]);
                item.AmountString = obj[2];
                item.RateString = obj[3];
                item.Total = FastDoubleConverter.Convert(obj[4]);
                item.Type = obj[6].Length == 3 ? TradeType.Buy : TradeType.Sell;
                item.Fill = obj[5].Length == 4 ? TradeFillType.Fill : TradeFillType.PartialFill;
                list.Insert(index, item);
                index++;
            }
            return list;
        }
        public override bool UpdateTrades(TickerBase info) {
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
                    TradeHistoryItem item = new TradeHistoryItem();
                    item.Id = Convert.ToInt64(obj[0]);
                    item.Time = Convert.ToDateTime(obj[1]);
                    item.AmountString = obj[2];
                    item.RateString = obj[3];
                    item.Total = FastDoubleConverter.Convert(obj[4]);
                    item.Type = obj[6].Length == 3 ? TradeType.Buy : TradeType.Sell;
                    item.Fill = obj[5].Length == 4 ? TradeFillType.Fill : TradeFillType.PartialFill;
                    info.TradeHistory.Insert(index, item);
                    index++;
                }
            }
            info.RaiseTradeHistoryAdd();
            return true;
        }
        public bool UpdateTradesStatistic(BitFinexTicker info, int depth) {
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
                    double price = FastDoubleConverter.Convert(obj[3]);
                    double amount = FastDoubleConverter.Convert(obj[2]);
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
        public string BuyLimit(BitFinexTicker info, double rate, double amount) {
            string address = string.Format("https://bittrex.com/api/v1.1/market/buylimit?apikey={0}&nonce={1}&market={2}&quantity={3}&rate={4}",
                Uri.EscapeDataString(ApiKey),
                GetNonce(),
                Uri.EscapeDataString(info.MarketName),
                amount.ToString("0.########"),
                rate.ToString("0.########"));
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", GetSign(address));
            string text = client.DownloadString(address);
            //info.TradeResult = text;
            return OnBuyLimit(text);
        }
        public string SellLimit(BitFinexTicker info, double rate, double amount) {
            string address = string.Format("https://bittrex.com/api/v1.1/market/selllimit?apikey={0}&nonce={1}&market={2}&quantity={3}&rate={4}",
                Uri.EscapeDataString(ApiKey),
                GetNonce(),
                Uri.EscapeDataString(info.MarketName),
                amount.ToString("0.########"),
                rate.ToString("0.########"));
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", GetSign(address));
            string text = client.DownloadString(address);
            //info.TradeResult = text;
            return OnSellLimit(text);
        }
        public override bool CancelOrder(TickerBase ticker, OpenedOrderInfo info) {
            throw new NotImplementedException();
            //string address = string.Format("https://bittrex.com/api/v1.1/market/cancel?apikey={0}&nonce={1}&uuid={2}",
            //    Uri.EscapeDataString(ApiKey),
            //    GetNonce(),
            //    ((BitFinexOrderInfo)info).OrderUuid);
            //WebClient client = GetWebClient();
            //client.Headers.Clear();
            //client.Headers.Add("apisign", GetSign(address));
            //try {
            //    return OnCancel(client.DownloadString(address));
            //}
            //catch(Exception) {
            //    return false;
            //}
        }
        public Task<string> CancelOrder(string uuid) {
            string address = string.Format("https://bittrex.com/api/v1.1/market/cancel?apikey={0}&nonce={1}&uuid={2}",
                Uri.EscapeDataString(ApiKey),
                GetNonce(),
                uuid);
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", GetSign(address));
            return client.DownloadStringTaskAsync(address);
        }
        public Task<string> GetOpenOrders(BitFinexTicker info) {
            string address = string.Format("https://bittrex.com/api/v1.1/market/getopenorders?apikey={0}&nonce={1}&market={2}",
                Uri.EscapeDataString(ApiKey),
                GetNonce(),
                info.MarketName);
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", GetSign(address));
            return client.DownloadStringTaskAsync(address);
        }
        public override bool UpdateOpenedOrders(TickerBase ticker) {
            string address = string.Empty;
            if(ticker != null) {
                address = string.Format("https://bittrex.com/api/v1.1/market/getopenorders?apikey={0}&nonce={1}&market={2}",
                Uri.EscapeDataString(ApiKey),
                GetNonce(),
                ticker.MarketName);
            }
            else {
                address = string.Format("https://bittrex.com/api/v1.1/market/getopenorders?apikey={0}&nonce={1}",
                Uri.EscapeDataString(ApiKey),
                GetNonce());
            }
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", GetSign(address));
            byte[] bytes = null;
            try {
                bytes = client.DownloadData(address);
                if(ticker != null) {
                    if(!ticker.IsOpenedOrdersChanged(bytes))
                        return true;
                }
                else {
                    if(IsOpenedOrdersChanged(bytes))
                        return true;
                }
            } catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
            return OnUpdateOrders(ticker, bytes);
        }
        protected string OnUuidResult(string result) {
            if(string.IsNullOrEmpty(result))
                return null;
            JObject res = (JObject)JsonConvert.DeserializeObject(result);
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
        public string OnBuyLimit(string result) {
            return OnUuidResult(result);
        }
        public string OnSellLimit(string result) {
            return OnUuidResult(result);
        }
        public bool OnCancel(string result) {
            if(string.IsNullOrEmpty(result))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(result);
            foreach(JProperty prop in res.Children()) {
                if(prop.Name == "success") {
                    return prop.Value.Value<bool>();
                }
            }
            return false;
        }
        public bool OnUpdateOrders(TickerBase ticker, byte[] bytes) {
            if(bytes == null)
                return false;

            int startIndex = 1;
            if(!JSonHelper.Default.SkipSymbol(bytes, ':', 3, ref startIndex)) {
                Telemetry.Default.TrackEvent("bittrex.onupdateorders", new string[] { "data", UTF8Encoding.Default.GetString(bytes) }, true);
                return false;
            }

            List<string[]> res = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, new string[] {
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
            });
            if(ticker != null)
                ticker.OpenedOrders.Clear();
            else
                OpenedOrders.Clear();
            if(res == null || res.Count == 0)
                return true;

            List<OpenedOrderInfo> openedOrders = ticker == null ? OpenedOrders : ticker.OpenedOrders;
            lock(openedOrders) {
                foreach(string[] obj in res) {
                    BitFinexOrderInfo info = new BitFinexOrderInfo();

                    //info.OrderUuid = obj[1];
                    info.Market = obj[2];
                    info.Type = obj[3] == "LIMIT_SELL" ? OrderType.Sell : OrderType.Buy;
                    info.AmountString = obj[5]; //obj[4];
                    //info.QuantityRemainingString = obj[5];
                    //info.LimitString = obj[6];
                    //info.CommissionPaidString = obj[7];
                    info.ValueString = obj[3][0] == 'L' ? obj[6] : obj[9];
                    info.TotalString = (info.Amount * info.Value).ToString("0.########");
                    info.Date = Convert.ToDateTime(obj[10]);
                    //info.Closed = obj[11] == "null" ? DateTime.MinValue : Convert.ToDateTime(obj[11]);
                    //info.CancelInitiated = obj[12].Length == 4 ? true : false;
                    //info.ImmediateOrCancel = obj[13].Length == 4 ? true : false;
                    //info.IsConditional = obj[14].Length == 4 ? true : false;
                    //info.Condition = obj[15];
                    //info.ConditionTarget = obj[16];

                    openedOrders.Add(info);
                }
            }

            if(ticker != null)
                ticker.RaiseOpenedOrdersChanged();
            return true;
        }
        
        public bool GetBalance(string currency) {
            string address = string.Format("https://bittrex.com/api/v1.1/account/getbalance?apikey={0}&nonce={1}&currency={2}", Uri.EscapeDataString(ApiKey), GetNonce(), currency);
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", GetSign(address));
            try {
                return OnGetBalance(client.DownloadString(address));
            }
            catch(Exception) {
                return false;
            }
        }
        public bool OnGetBalance(string text) {
            if(string.IsNullOrEmpty(text))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            if(res.Value<bool>("success") == false) {
                Debug.WriteLine("OnGetBalance fails: " + res.Value<string>("message"));
                return false;
            }
            JObject obj = res.Value<JObject>("result");
            lock(Balances) {
                string currency = obj.Value<string>("Currency");
                BitFinexAccountBalanceInfo info = (BitFinexAccountBalanceInfo)Balances.FirstOrDefault((b) => b.Currency == currency);
                if(info == null) {
                    info = new BitFinexAccountBalanceInfo();
                    info.Currency = obj.Value<string>("Currency");
                    //info.Requested = obj.Value<bool>("Requested");
                    //info.Uuid = obj.Value<string>("Uuid");
                    Balances.Add(info);
                }
                info.LastAvailable = info.Available;
                info.Available = obj.Value<string>("Available") == null ? 0 : obj.Value<double>("Available");
                //info.Balance = obj.Value<string>("Balance") == null ? 0 : obj.Value<double>("Balance");
                //info.Pending = obj.Value<string>("Pending") == null ? 0 : obj.Value<double>("Pending");
                info.DepositAddress = obj.Value<string>("CryptoAddress");
            }
            return true;
        }
        public override bool UpdateBalances() {
            if(Currencies.Count == 0) {
                if(!GetCurrenciesInfo())
                    return false;
            }
            string address = string.Format("https://bittrex.com/api/v1.1/account/getbalances?apikey={0}&nonce={1}", Uri.EscapeDataString(ApiKey), GetNonce());
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", GetSign(address));
            try {
                byte[] bytes = client.DownloadData(address);


                if(bytes == null)
                    return false;

                int startIndex = 1;
                if(!JSonHelper.Default.SkipSymbol(bytes, ':', 3, ref startIndex))
                    return false;

                List<string[]> res = JSonHelper.Default.DeserializeArrayOfObjects(bytes, ref startIndex, new string[] {
                "Currency",
                "Balance",
                "Available",
                "Pending",
                "CryptoAddress"
            });

                foreach(string[] item in res) {
                    BitFinexAccountBalanceInfo info = (BitFinexAccountBalanceInfo)Balances.FirstOrDefault(b => b.Currency == item[0]);
                    if(info == null) {
                        info = new BitFinexAccountBalanceInfo();
                        info.Currency = item[0];
                        Balances.Add(info);
                    }
                    //info.Balance = FastDoubleConverter.Convert(item[1]);
                    info.Available = FastDoubleConverter.Convert(item[2]);
                    //info.Pending = FastDoubleConverter.Convert(item[3]);
                    info.DepositAddress = item[4];
                }
            }
            catch(Exception) {
                return false;
            }
            return true;
        }
        public Task<string> GetBalancesAsync() {
            string address = string.Format("https://bittrex.com/api/v1.1/account/getbalances?apikey={0}&nonce={1}", Uri.EscapeDataString(ApiKey), GetNonce());
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", GetSign(address));
            return client.DownloadStringTaskAsync(address);
        }
        public bool OnGetBalances(string text) {
            if(string.IsNullOrEmpty(text))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            if(res.Value<bool>("success") == false) {
                Debug.WriteLine("OnGetBalances fails: " + res.Value<string>("message"));
                return false;
            }
            JArray balances = res.Value<JArray>("result");
            lock(Balances) {
                Balances.Clear();
                foreach(JObject obj in balances) {
                    BitFinexAccountBalanceInfo item = new BitFinexAccountBalanceInfo();
                    item.Currency = obj.Value<string>("Currency");
                    //item.Balance = obj.Value<double>("Balance");
                    item.Available = obj.Value<double>("Available");
                    //item.Pending = obj.Value<double>("Pending");
                    item.DepositAddress = obj.Value<string>("CryptoAddress");
                    //item.Requested = obj.Value<bool>("Requested");
                    //item.Uuid = obj.Value<string>("Uuid");
                    Balances.Add(item);
                }
            }
            RaiseBalancesChanged();
            return true;
        }
        void RaiseBalancesChanged() {

        }

        public bool Withdraw(string currency, double amount, string address, string paymentId) {
            string addr = string.Empty;
            if(string.IsNullOrEmpty(paymentId)) {
                addr = string.Format("https://bittrex.com/api/v1.1/account/withdraw?apikey={0}&nonce={1}&currency={2}&quantity={3}",
                    Uri.EscapeDataString(ApiKey),
                    GetNonce(),
                    Uri.EscapeDataString(currency),
                    amount.ToString("0.########"));
            }
            else {
                addr = string.Format("https://bittrex.com/api/v1.1/account/withdraw?apikey={0}&nonce={1}&currency={2}&quantity={3}&paymentid={4}",
                        Uri.EscapeDataString(ApiKey),
                        GetNonce(),
                        Uri.EscapeDataString(currency),
                        amount.ToString("0.########"),
                        Uri.EscapeDataString(paymentId));
            }
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", GetSign(address));
            try {
                string text = client.DownloadString(addr);
                string uuid = OnWithdraw(text);
                return !string.IsNullOrEmpty(uuid);
            }
            catch(Exception) {
                return false;
            }
        }
        public Task<string> WithdrawAsync(string currency, double amount, string address, string paymentId) {
            string addr = string.Empty;
            if(string.IsNullOrEmpty(paymentId)) {
                addr = string.Format("https://bittrex.com/api/v1.1/account/withdraw?apikey={0}&nonce={1}&currency={2}&quantity={3}",
                    Uri.EscapeDataString(ApiKey),
                    GetNonce(),
                    Uri.EscapeDataString(currency),
                    amount.ToString("0.########"));
            }
            else {
                addr = string.Format("https://bittrex.com/api/v1.1/account/withdraw?apikey={0}&nonce={1}&currency={2}&quantity={3}&paymentid={4}",
                        Uri.EscapeDataString(ApiKey),
                        GetNonce(),
                        Uri.EscapeDataString(currency),
                        amount.ToString("0.########"),
                        Uri.EscapeDataString(paymentId));
            }
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", GetSign(address));
            return client.DownloadStringTaskAsync(addr);
        }
        public string OnWithdraw(string result) {
            return OnUuidResult(result);
        }

        public string CheckCreateDeposit(string currency) {
            string address = string.Format("https://bittrex.com/api/v1.1/account/getdepositaddress?apikey={0}&nonce={1}&currency={2}", Uri.EscapeDataString(ApiKey), GetNonce(), currency);
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", GetSign(address));
            return OnGetDeposit(currency, client.DownloadString(address));
        }
        string OnGetDeposit(string currency, string text) {
            if(string.IsNullOrEmpty(text))
                return null;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            if(res.Value<bool>("success") == false) {
                string error = res.Value<string>("message");
                if(error == "ADDRESS_GENERATING")
                    LogManager.Default.AddWarning("Bittrex: OnGetDeposit fails: " + error + ". Try again later after deposit address generate.", "Currency = " + currency);
                else
                    LogManager.Default.AddError("Bittrex: OnGetDeposit fails: " + error, "Currency = " + currency);
                return null;
            }
            JObject addr = res.Value<JObject>("result");
            BitFinexAccountBalanceInfo info = (BitFinexAccountBalanceInfo)Balances.FirstOrDefault(b => b.Currency == currency);
            info.Currency = addr.Value<string>("Address");
            return info.Currency;
        }

        string GetNonce() {
           return ((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
        }
    }
}
