using CryptoMarketClient.Common;
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
using System.Threading.Tasks;

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

        public override bool UseWebSocket => false;

        public override void ObtainExchangeSettings() { }

        public override void StartListenTickersStream() { }

        public override void StopListenTickersStream() { }

        public override bool AllowCandleStickIncrementalUpdate => false;

        public override string Name => "Bittrex";

        public override List<CandleStickIntervalInfo> GetAllowedCandleStickIntervals() {
            List<CandleStickIntervalInfo> list = new List<CandleStickIntervalInfo>();
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(1), Text = "1 Minute", Command = "oneMin" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(5), Text = "5 Minutes", Command = "fiveMin" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(30), Text = "30 Minutes", Command = "thirtyMin" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(60), Text = "1 Hour", Command = "hour" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(60 * 24), Text = "1 Day", Command = "day" });
            return list;
        }

        public List<BittrexCurrencyInfo> Currencies { get; } = new List<BittrexCurrencyInfo>();
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
            if(!SkipSymbol(bytes, ':', 3, ref startIndex))
                return list;

            List<string[]> res = DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "O", "H", "L", "C", "V", "T", "BV" });
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
            if(!SkipSymbol(bytes, ':', 3, ref startIndex))
                return false;

            List<string[]> res = DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "MarketCurrency", "BaseCurrency", "MarketCurrencyLong", "BaseCurrencyLong", "MinTradeSize", "MarketName", "IsActive", "Created", "Notice", "IsSponsored", "LogoUrl"});
            foreach(string[] item in res) {
                BittrexTicker m = new BittrexTicker(this);

                m.MarketCurrency = item[0];
                m.BaseCurrency = item[1];
                m.MarketCurrencyLong = item[2];
                m.BaseCurrencyLong = item[3];
                m.MinTradeSize = FastDoubleConverter.Convert(item[4]);
                m.MarketName = item[5];
                m.IsActive = item[6].Length == 4 ? true : false;
                m.Created = Convert.ToDateTime(item[7]);
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
            if(!SkipSymbol(bytes, ':', 3, ref startIndex))
                return false;

            List<string[]> res = DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "Currency", "CurrencyLong", "MinConfirmation", "TxFee", "IsActive", "CoinType", "BaseAddress" });
            foreach(string[] item in res) {
                string currency = item[0];
                BittrexCurrencyInfo c = Currencies.FirstOrDefault(curr => curr.Currency == currency);
                if(c == null) {
                    c = new BittrexCurrencyInfo();
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
            if(!SkipSymbol(bytes, ':', 3, ref startIndex))
                return;

            string[] res = DeserializeObject(bytes, ref startIndex, new string[] { "Bid", "Ask", "Last" });
            if(res == null)
                return;
            info.HighestBid = FastDoubleConverter.Convert(res[0]);
            info.LowestAsk = FastDoubleConverter.Convert(res[1]);
            info.Last = FastDoubleConverter.Convert(res[2]);
            info.Time = DateTime.UtcNow;
            info.UpdateHistoryItem();
        }
        public override bool UpdateTicker(TickerBase tickerBase) {
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
            if(!SkipSymbol(bytes, ':', 3, ref startIndex))
                return false;

            string[] res = DeserializeObject(bytes, ref startIndex, new string[] { "MarketName", "High", "Low", "Volume", "Last", "BaseVolume", "TimeStamp", "Bid", "Ask", "OpenBuyOrders", "OpenSellOrders", "PrevDay", "Created" });
            if(res == null)
                return true;

            BittrexTicker info = (BittrexTicker)tickerBase;

            info.Hr24High = FastDoubleConverter.Convert(res[1]);
            info.Hr24Low = FastDoubleConverter.Convert(res[2]);
            info.Volume = FastDoubleConverter.Convert(res[3]);
            info.Last = FastDoubleConverter.Convert(res[4]);
            info.BaseVolume = FastDoubleConverter.Convert(res[5]);
            info.Time = Convert.ToDateTime(res[6]);
            info.HighestBid = FastDoubleConverter.Convert(res[7]);
            info.LowestAsk = FastDoubleConverter.Convert(res[8]);
            info.OpenBuyOrders = Convert.ToInt32(res[9]);
            info.OpenSellOrders = Convert.ToInt32(res[10]);
            info.PrevDay = FastDoubleConverter.Convert(res[11]);
            info.Created = Convert.ToDateTime(res[12]);
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
            if(!SkipSymbol(bytes, ':', 3, ref startIndex))
                return false;

            List<string[]> res = DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "MarketName", "High", "Low", "Volume", "Last", "BaseVolume", "TimeStamp", "Bid", "Ask", "OpenBuyOrders", "OpenSellOrders", "PrevDay", "Created" });
            foreach(string[] item in res) {
                string marketName = item[0];
                BittrexTicker info = (BittrexTicker)Tickers.FirstOrDefault((m) => m.MarketName == marketName);
                if(info == null)
                    continue;

                info.Hr24High = FastDoubleConverter.Convert(item[1]);
                info.Hr24Low = FastDoubleConverter.Convert(item[2]);
                info.Volume = FastDoubleConverter.Convert(item[3]);
                info.Last = FastDoubleConverter.Convert(item[4]);
                info.BaseVolume = FastDoubleConverter.Convert(item[5]);
                info.Time = Convert.ToDateTime(item[6]);
                info.HighestBid = FastDoubleConverter.Convert(item[7]);
                info.LowestAsk = FastDoubleConverter.Convert(item[8]);
                info.OpenBuyOrders = Convert.ToInt32(item[9]);
                info.OpenSellOrders = Convert.ToInt32(item[10]);
                info.PrevDay = FastDoubleConverter.Convert(item[11]);
                info.Created = Convert.ToDateTime(item[12]);
                info.DisplayMarketName = item[0];
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
            return string.Format("https://bittrex.com/api/v1.1/public/getorderbook?market={0}&type=both&depth={1}", Uri.EscapeDataString(info.MarketName), depth * 2);
        }
        public override bool ProcessOrderBook(TickerBase tickerBase, string text) {
            throw new NotImplementedException();
        }
        public bool UpdateOrderBook(BittrexTicker info, byte[] data, int depth) {
            return UpdateOrderBook(info, data, true, depth);
        }
        public override bool UpdateOrderBook(TickerBase tickerBase) {
            return UpdateArbitrageOrderBook(tickerBase, OrderBook.Depth);
        }
        public bool UpdateOrderBook(TickerBase ticker, byte[] bytes, bool raiseChanged, int depth) {
            if(bytes == null)
                return false;

            int startIndex = 1; // skip {
            if(!SkipSymbol(bytes, ':', 4, ref startIndex))
                return false;

            List<string[]> bids = DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "Quantity", "Rate" }, (itemIndex, paramIndex, value) => { return itemIndex < OrderBook.Depth; });
            if(!FindCharWithoutStop(bytes, ':', ref startIndex))
                return false;
            if(bids == null)
                return true;
            startIndex++;
            List<string[]> asks = DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "Quantity", "Rate" });
            if(asks == null)
                return true;

            ticker.OrderBook.GetNewBidAsks();
            int index = 0;
            OrderBookEntry[] list = ticker.OrderBook.Bids;
            foreach(string[] item in bids) {
                OrderBookEntry entry = list[index];
                entry.ValueString = item[1];
                entry.AmountString = item[0];
                index++;
                if(index >= list.Length)
                    break;
            }
            index = 0;
            list = ticker.OrderBook.Asks;
            foreach(string[] item in asks) {
                OrderBookEntry entry = list[index];
                entry.ValueString = item[1];
                entry.AmountString = item[0];
                index++;
                if(index >= list.Length)
                    break;
            }
            ticker.OrderBook.UpdateEntries();
            ticker.OrderBook.RaiseOnChanged(new OrderBookUpdateInfo() { Action = OrderBookUpdateType.RefreshAll });
            return true;
        }
        public void GetOrderBook(BittrexTicker info, int depth) {
            string address = string.Format("https://bittrex.com/api/v1.1/public/getorderbook?market={0}&type=both&depth={1}", Uri.EscapeDataString(info.MarketName), depth);
            byte[] data = GetDownloadBytes(address);
            if(data == null)
                return;
            UpdateOrderBook(info, data, depth);
        }
        public bool GetTrades(BittrexTicker info) {
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
            if(!SkipSymbol(bytes, ':', 3, ref startIndex))
                return false;

            List<string[]> res = DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "Id", "TimeStamp", "Quantity", "Price", "Total", "FillType", "OrderType" });
            if(res == null)
                return false;
            lock(info) {
                info.TradeHistory.Clear();
                foreach(string[] obj in res) {
                    TradeHistoryItem item = new TradeHistoryItem();
                    item.Id = Convert.ToInt64(obj[0]);;
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
            if(!SkipSymbol(bytes, ':', 3, ref startIndex))
                return false;

            string tradeUuid = ticker.MyTradeHistory.Count == 0 ? null : ticker.MyTradeHistory.First().IdString;
            List<string[]> res = DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { 
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
                item.TimeString = obj[1];
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
            if(!SkipSymbol(bytes, ':', 3, ref startIndex))
                return null;

            List<string[]> res = DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "Id", "TimeStamp", "Quantity", "Price", "Total", "FillType", "OrderType" }, 
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
            if(!SkipSymbol(bytes, ':', 3, ref startIndex))
                return false;

            long lastId = info.TradeHistory.Count > 0 ? info.TradeHistory.First().Id : -1;
            string lastIdString = lastId.ToString();
            List<string[]> res = DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "Id", "TimeStamp", "Quantity", "Price", "Total", "FillType", "OrderType" },
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
            if(!SkipSymbol(bytes, ':', 3, ref startIndex))
                return false;

            string lastIdString = info.LastTradeId.ToString();
            List<string[]> res = DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "Id", "TimeStamp", "Quantity", "Price", "Total", "FillType", "OrderType" },
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
        public string BuyLimit(BittrexTicker info, double rate, double amount) {
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
            info.TradeResult = text;
            return OnBuyLimit(text);
        }
        public string SellLimit(BittrexTicker info, double rate, double amount) {
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
            info.TradeResult = text;
            return OnSellLimit(text);
        }
        public override bool CancelOrder(TickerBase ticker, OpenedOrderInfo info) {
            string address = string.Format("https://bittrex.com/api/v1.1/market/cancel?apikey={0}&nonce={1}&uuid={2}",
                Uri.EscapeDataString(ApiKey),
                GetNonce(),
                ((BittrexOrderInfo)info).OrderUuid);
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", GetSign(address));
            try {
                return OnCancel(client.DownloadString(address));
            }
            catch(Exception e) {
                return false;
            }
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
        public Task<string> GetOpenOrders(BittrexTicker info) {
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
            string address = string.Format("https://bittrex.com/api/v1.1/market/getopenorders?apikey={0}&nonce={1}&market={2}",
                Uri.EscapeDataString(ApiKey),
                GetNonce(),
                ticker.MarketName);
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", GetSign(address));
            byte[] bytes = null;
            try {
                bytes = client.DownloadData(address);
                if(!ticker.IsOpenedOrdersChanged(bytes))
                    return true;
            } catch {
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
            if(!SkipSymbol(bytes, ':', 3, ref startIndex))
                return false;

            List<string[]> res = DeserializeArrayOfObjects(bytes, ref startIndex, new string[] {
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
            if(res == null || res.Count == 0)
                return true;

            ticker.OpenedOrders.Clear();
            foreach(string[] obj in res) {
                BittrexOrderInfo info = new BittrexOrderInfo();

                info.OrderUuid = obj[1];
                info.Exchange = obj[2];
                info.Type = obj[3] == "LIMIT_SELL" ? OrderType.Sell : OrderType.Buy;
                info.AmountString = obj[5]; //obj[4];
                info.QuantityRemainingString = obj[5];
                info.LimitString = obj[6];
                info.CommissionPaidString = obj[7];
                info.ValueString = obj[3][0] == 'L' ? obj[6] : obj[9];
                info.TotalString = (info.Amount * info.Value).ToString("0.########");
                info.Opened = Convert.ToDateTime(obj[10]);
                info.Closed = obj[11] == "null"? DateTime.MinValue: Convert.ToDateTime(obj[11]);
                info.CancelInitiated = obj[12].Length == 4 ? true : false;
                info.ImmediateOrCancel = obj[13].Length == 4 ? true : false;
                info.IsConditional = obj[14].Length == 4 ? true : false;
                info.Condition = obj[15];
                info.ConditionTarget = obj[16];

                ticker.OpenedOrders.Add(info);
            }

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
                BittrexAccountBalanceInfo info = (BittrexAccountBalanceInfo)Balances.FirstOrDefault((b) => b.Currency == currency);
                if(info == null) {
                    info = new BittrexAccountBalanceInfo();
                    info.Currency = obj.Value<string>("Currency");
                    info.Requested = obj.Value<bool>("Requested");
                    info.Uuid = obj.Value<string>("Uuid");
                    Balances.Add(info);
                }
                info.LastAvailable = info.Available;
                info.Available = obj.Value<string>("Available") == null ? 0 : obj.Value<double>("Available");
                info.Balance = obj.Value<string>("Balance") == null ? 0 : obj.Value<double>("Balance");
                info.Pending = obj.Value<string>("Pending") == null ? 0 : obj.Value<double>("Pending");
                info.DepositAddress = obj.Value<string>("CryptoAddress");
            }
            return true;
        }
        public override bool UpdateBalances() {
            if(Currencies.Count == 0) {
                if(!GetCurrenciesInfo())
                    return false;
            }
            WebClient client = GetWebClient();
            foreach(BittrexCurrencyInfo info in Currencies) {
                string address = string.Format("https://bittrex.com/api/v1.1/account/getbalance?apikey={0}&nonce={1}&currency={2}", Uri.EscapeDataString(ApiKey), GetNonce(), info.Currency);
                client.Headers.Clear();
                client.Headers.Add("apisign", GetSign(address));
                int tryIndex = 0;
                for(tryIndex = 0; tryIndex < 3; tryIndex++) {
                    try {
                        string text = client.DownloadString(address);
                        if(!OnGetBalance(text))
                            return false;
                    }
                    catch(Exception) {
                        continue;
                    }
                    if(tryIndex == 3)
                        return false;
                }
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
                    BittrexAccountBalanceInfo item = new BittrexAccountBalanceInfo();
                    item.Currency = obj.Value<string>("Currency");
                    item.Balance = obj.Value<double>("Balance");
                    item.Available = obj.Value<double>("Available");
                    item.Pending = obj.Value<double>("Pending");
                    item.DepositAddress = obj.Value<string>("CryptoAddress");
                    item.Requested = obj.Value<bool>("Requested");
                    item.Uuid = obj.Value<string>("Uuid");
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
            BittrexAccountBalanceInfo info = (BittrexAccountBalanceInfo)Balances.FirstOrDefault(b => b.Currency == currency);
            info.Currency = addr.Value<string>("Address");
            return info.Currency;
        }

        string GetNonce() {
           return ((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
        }
    }
}
