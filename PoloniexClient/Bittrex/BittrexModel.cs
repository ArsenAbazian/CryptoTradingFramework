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

            string text = (new UTF8Encoding()).GetString(bytes);
            List<string[]> res = DeserializeArrayOfObjects(bytes, ref startIndex, new string[] { "O", "H", "L", "C", "V", "T", "BV" });
            if(res == null) return list;
            foreach(string[] item in res) {
                CandleStickData data = new CandleStickData();
                data.Time = Convert.ToDateTime(item[5]);
                data.High = FastDoubleConverter.Convert(item[1]);
                data.Low = FastDoubleConverter.Convert(item[2]);
                data.Open = FastDoubleConverter.Convert(item[0]);
                data.Close = FastDoubleConverter.Convert(item[3]);
                data.Volume = FastDoubleConverter.Convert(item[4]);
                data.QuoteVolume = FastDoubleConverter.Convert(item[6]);
                data.WeightedAverage = 0;
                list.Add(data);
            }
            return list;
        }

        public override bool GetTickersInfo() {
            string address = "https://bittrex.com/api/v1.1/public/getmarkets";
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
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            foreach(JProperty prop in res.Children()) {
                if(prop.Name == "success") {
                    if(prop.Value.Value<bool>() == false)
                        return false;
                }
                if(prop.Name == "message")
                    continue;
                if(prop.Name == "result") {
                    JArray markets = (JArray)prop.Value;
                    foreach(JObject obj in markets) {
                        BittrexTicker m = new BittrexTicker(this);
                        m.MarketCurrency = obj.Value<string>("MarketCurrency");
                        m.BaseCurrency = obj.Value<string>("BaseCurrency");
                        m.MarketCurrencyLong = obj.Value<string>("MarketCurrencyLong");
                        m.BaseCurrencyLong = obj.Value<string>("BaseCurrencyLong");
                        m.MinTradeSize = obj.Value<double>("MinTradeSize");
                        m.MarketName = obj.Value<string>("MarketName");
                        m.IsActive = obj.Value<bool>("IsActive");
                        m.Created = obj.Value<DateTime>("Created");
                        m.Index = Tickers.Count;
                        Tickers.Add(m);
                    }
                }
            }
            return true;
        }
        public override bool UpdateCurrencies() {
            string address = "https://bittrex.com/api/v1.1/public/getcurrencies";
            string text = string.Empty;
            try {
                text = GetDownloadString(address);
            }
            catch(Exception) {
                return false;
            }
            if(string.IsNullOrEmpty(text))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            if(!res.Value<bool>("success"))
                return false;
            JArray markets = res.Value<JArray>("result");
            foreach(JObject obj in markets) {
                string currency = obj.Value<string>("Currency");
                BittrexCurrencyInfo c = Currencies.FirstOrDefault(curr => curr.Currency == currency);
                if(c == null) {
                    c = new BittrexCurrencyInfo();
                    c.Currency = currency;
                    c.CurrencyLong = obj.Value<string>("CurrencyLong");
                    c.MinConfirmation = obj.Value<int>("MinConfirmation");
                    c.CoinType = obj.Value<string>("CoinType");
                    c.BaseAddress = obj.Value<string>("BaseAddress");
                    c.TxFree = obj.Value<double>("TxFee");
                    Currencies.Add(c);
                }
                c.IsActive = obj.Value<bool>("IsActive");
            }
            return true;
        }
        public bool GetCurrenciesInfo() {
            string address = "https://bittrex.com/api/v1.1/public/getcurrencies";
            string text = string.Empty;
            try {
                text = GetDownloadString(address);
            }
            catch(Exception) {
                return false;
            }
            if(string.IsNullOrEmpty(text))
                return false;
            Currencies.Clear();
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            if(!res.Value<bool>("success"))
                return false;
            JArray markets = res.Value<JArray>("result");
            foreach(JObject obj in markets) {
                BittrexCurrencyInfo c = new BittrexCurrencyInfo();
                c.Currency = obj.Value<string>("Currency");
                c.CurrencyLong = obj.Value<string>("CurrencyLong");
                c.MinConfirmation = obj.Value<int>("MinConfirmation");
                c.TxFree = obj.Value<double>("TxFee");
                c.IsActive = obj.Value<bool>("IsActive");
                c.CoinType = obj.Value<string>("CoinType");
                c.BaseAddress = obj.Value<string>("BaseAddress");
                Currencies.Add(c);
            }
            return true;
        }
        public void GetTicker(BittrexTicker info) {
            string address = string.Format("https://bittrex.com/api/v1.1/public/getticker?market={0}", Uri.EscapeDataString(info.MarketName));
            string text = GetDownloadString(info, address);
            if(string.IsNullOrEmpty(text))
                return;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            foreach(JProperty prop in res.Children()) {
                if(prop.Name == "success") {
                    if(prop.Value.Value<bool>() == false)
                        break;
                }
                if(prop.Name == "message")
                    continue;
                if(prop.Name == "result") {
                    JObject obj = (JObject)prop.Value;
                    info.HighestBid = obj.Value<double>("Bid");
                    info.LowestAsk = obj.Value<double>("Ask");
                    info.Last = obj.Value<double>("Last");
                    info.Time = DateTime.UtcNow;
                    info.UpdateHistoryItem();
                }
            }
        }
        public override bool UpdateTicker(TickerBase tickerBase) {
            string address = string.Format("https://bittrex.com/api/v1.1/public/getmarketsummary?market={0}", tickerBase.MarketName);
            string text = string.Empty;
            try {
                text = GetDownloadString(address);
            }
            catch(Exception) {
                return false;
            }
            if(string.IsNullOrEmpty(text))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            foreach(JProperty prop in res.Children()) {
                if(prop.Name == "success") {
                    if(prop.Value.Value<bool>() == false)
                        return false;
                }
                if(prop.Name == "message")
                    continue;
                if(prop.Name == "result") {
                    JArray markets = (JArray)prop.Value;
                    foreach(JObject obj in markets) {
                        string marketName = obj.Value<string>("MarketName");
                        BittrexTicker info = (BittrexTicker)Tickers.FirstOrDefault((m) => m.MarketName == marketName);
                        if(info == null)
                            continue;
                        info.Hr24High = obj.Value<double>("High");
                        info.Hr24Low = obj.Value<double>("Low");
                        info.Volume = obj.Value<double>("Volume");
                        info.Last = obj.Value<double>("Last");
                        info.BaseVolume = obj.Value<double>("BaseVolume");
                        info.Time = obj.Value<DateTime>("TimeStamp");
                        info.HighestBid = obj.Value<double>("Bid");
                        info.LowestAsk = obj.Value<double>("Ask");
                        info.OpenBuyOrders = obj.Value<int>("OpenBuyOrders");
                        info.OpenSellOrders = obj.Value<int>("OpenSellOrders");
                        info.PrevDay = obj.Value<double>("PrevDay");
                        info.Created = obj.Value<DateTime>("Created");
                        info.DisplayMarketName = obj.Value<string>("DisplayMarketName");
                        //info.UpdateHistoryItem();
                    }
                }
            }
            return true;
        }
        public override bool UpdateTickersInfo() {
            string address = string.Format("https://bittrex.com/api/v1.1/public/getmarketsummaries");
            string text = string.Empty;
            try {
                text = GetDownloadString(address);
            }
            catch(Exception) {
                return false;
            }
            if(string.IsNullOrEmpty(text))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            foreach(JProperty prop in res.Children()) {
                if(prop.Name == "success") {
                    if(prop.Value.Value<bool>() == false)
                        return false;
                }
                if(prop.Name == "message")
                    continue;
                if(prop.Name == "result") {
                    JArray markets = (JArray)prop.Value;
                    foreach(JObject obj in markets) {
                        string marketName = obj.Value<string>("MarketName");
                        BittrexTicker info = (BittrexTicker)Tickers.FirstOrDefault((m) => m.MarketName == marketName);
                        if(info == null)
                            continue;
                        info.Hr24High = obj.Value<double>("High");
                        info.Hr24Low = obj.Value<double>("Low");
                        info.Volume = obj.Value<double>("Volume");
                        info.Last = obj.Value<double>("Last");
                        info.BaseVolume = obj.Value<double>("BaseVolume");
                        info.Time = obj.Value<DateTime>("TimeStamp");
                        info.HighestBid = obj.Value<double>("Bid");
                        info.LowestAsk = obj.Value<double>("Ask");
                        info.OpenBuyOrders = obj.Value<int>("OpenBuyOrders");
                        info.OpenSellOrders = obj.Value<int>("OpenSellOrders");
                        info.PrevDay = obj.Value<double>("PrevDay");
                        info.Created = obj.Value<DateTime>("Created");
                        info.DisplayMarketName = obj.Value<string>("DisplayMarketName");
                        //info.UpdateHistoryItem();
                    }
                }
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
                entry.ValueString = item[0];
                entry.AmountString = item[1];
                index++;
                if(index >= list.Length)
                    break;
            }
            index = 0;
            list = ticker.OrderBook.Asks;
            foreach(string[] item in asks) {
                OrderBookEntry entry = list[index];
                entry.ValueString = item[0];
                entry.AmountString = item[1];
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
            string text = string.Empty;
            try {
                text = GetDownloadString(info, address);
            }
            catch(Exception) {
                return false;
            }
            if(string.IsNullOrEmpty(text))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            if(!res.Value<bool>("success"))
                return false;
            JArray trades = res.Value<JArray>("result");
            lock(info) {
                info.TradeHistory.Clear();
                foreach(JObject obj in trades) {
                    TradeHistoryItem item = new TradeHistoryItem();
                    item.Id = obj.Value<int>("Id");
                    item.Time = obj.Value<DateTime>("TimeStamp");
                    item.AmountString = obj.Value<string>("Quantity");
                    item.RateString = obj.Value<string>("Price");
                    item.Total = obj.Value<double>("Total");
                    item.Type = obj.Value<string>("OrderType") == "BUY" ? TradeType.Buy : TradeType.Sell;
                    item.Fill = obj.Value<string>("FillType") == "FILL" ? TradeFillType.Fill : TradeFillType.PartialFill;
                    info.TradeHistory.Add(item);
                }
            }
            info.RaiseTradeHistoryAdd();
            return true;
        }
        public override bool UpdateMyTrades(TickerBase ticker) {
            string address = string.Format("https://bittrex.com/api/v1.1/market/getopenorders?apikey={0}&nonce={1}&market={2}",
                Uri.EscapeDataString(ApiKey),
                GetNonce(),
                ticker.MarketName);
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", GetSign(address));
            string text = client.DownloadString(address);
            return OnGetMyTrades(ticker, text);
        }
        bool OnGetMyTrades(TickerBase ticker, string text) {
            return true;
        }
        public override List<TradeHistoryItem> GetTrades(TickerBase info, DateTime starTime) {
            string address = string.Format("https://bittrex.com/api/v1.1/public/getmarkethistory?market={0}", Uri.EscapeDataString(info.MarketName));
            string text = string.Empty;
            try {
                text = GetDownloadString(info, address);
            }
            catch(Exception) {
                return null;
            }
            if(string.IsNullOrEmpty(text))
                return null;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            if(!res.Value<bool>("success"))
                return null;
            JArray trades = res.Value<JArray>("result");
            int index = 0;
            List<TradeHistoryItem> list = new List<TradeHistoryItem>();
            lock(info) {
                if(trades == null)
                    return list;
                foreach(JObject obj in trades) {
                    int id = obj.Value<int>("Id");
                    TradeHistoryItem item = new TradeHistoryItem();
                    item.Id = id;
                    item.Time = obj.Value<DateTime>("TimeStamp");
                    if(item.Time < starTime)
                        break;
                    item.AmountString = obj.Value<string>("Quantity");
                    item.RateString = obj.Value<string>("Price");
                    item.Total = obj.Value<double>("Total");
                    item.Type = obj.Value<string>("OrderType") == "BUY" ? TradeType.Buy : TradeType.Sell;
                    item.Fill = obj.Value<string>("FillType") == "FILL" ? TradeFillType.Fill : TradeFillType.PartialFill;
                    list.Insert(index, item);
                    index++;
                }
            }
            return list;
        }
        public override bool UpdateTrades(TickerBase info) {
            string address = string.Format("https://bittrex.com/api/v1.1/public/getmarkethistory?market={0}", Uri.EscapeDataString(info.MarketName));
            string text = string.Empty;
            try {
                text = GetDownloadString(info, address);
            }
            catch(Exception) {
                return false;
            }
            if(string.IsNullOrEmpty(text))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            if(!res.Value<bool>("success"))
                return false;
            JArray trades = res.Value<JArray>("result");
            int index = 0;
            lock(info) {
                info.TradeHistory.Clear();
                if(trades == null)
                    return true;
                long lastId = info.TradeHistory.Count > 0 ? info.TradeHistory.First().Id : -1;
                foreach(JObject obj in trades) {
                    int id = obj.Value<int>("Id");
                    if(id == lastId) {
                        foreach(TradeHistoryItem th in UpdateList)
                            info.TradeHistory.Insert(0, th);
                        break;
                    }
                    TradeHistoryItem item = new TradeHistoryItem();
                    item.Id = id;
                    item.Time = obj.Value<DateTime>("TimeStamp");
                    item.AmountString = obj.Value<string>("Quantity");
                    item.RateString = obj.Value<string>("Price");
                    item.Total = obj.Value<double>("Total");
                    item.Type = obj.Value<string>("OrderType") == "BUY" ? TradeType.Buy : TradeType.Sell;
                    item.Fill = obj.Value<string>("FillType") == "FILL" ? TradeFillType.Fill : TradeFillType.PartialFill;
                    info.TradeHistory.Insert(index, item);
                    index++;
                }
            }
            info.RaiseTradeHistoryAdd();
            return true;
        }
        public bool UpdateTradesStatistic(BittrexTicker info, int depth) {
            string address = string.Format("https://bittrex.com/api/v1.1/public/getmarkethistory?market={0}", Uri.EscapeDataString(info.MarketName));
            string text = GetDownloadString(info, address);
            if(string.IsNullOrEmpty(text))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            if(!res.Value<bool>("success"))
                return false;
            JArray trades = res.Value<JArray>("result");
            if(trades == null || trades.Count == 0)
                return true;
            int lastTradeId = trades.First().Value<int>("Id");
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
            st.Time = trades.First().Value<DateTime>("TimeStamp");
            lock(info) {
                foreach(JObject obj in trades) {
                    int id = obj.Value<int>("Id");
                    if(id == info.LastTradeId)
                        break;
                    bool isBuy = obj.Value<string>("OrderType").Length == 3;
                    double price = obj.Value<double>("Price");
                    double amount = obj.Value<double>("Quantity");
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
            return OnBuyLimit(client.DownloadString(address));
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
            return OnSellLimit(client.DownloadString(address));
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
        public override bool UpdateOpenedOrders(TickerBase tickerBase) {
            string address = string.Format("https://bittrex.com/api/v1.1/market/getopenorders?apikey={0}&nonce={1}&market={2}",
                Uri.EscapeDataString(ApiKey),
                GetNonce(),
                tickerBase.MarketName);
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", GetSign(address));
            string text = string.Empty;
            try {
                text = client.DownloadString(address);
            } catch {
                return false;
            }
            return OnUpdateOrders(tickerBase, text);
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
        public bool OnUpdateOrders(TickerBase ticker, string result) {
            if(string.IsNullOrEmpty(result))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(result);
            if(!res.Value<bool>("success")) {
                Debug.WriteLine("OnAppendOpenOrders fails: " + res.Value<string>("message"));
                return false;
            }

            ticker.OpenedOrders.Clear();
            JArray orders = res.Value<JArray>("result");
            foreach(JObject obj in orders) {
                BittrexOrderInfo info = new BittrexOrderInfo();

                info.OrderUuid = obj.Value<string>("OrderUuid");
                info.Exchange = obj.Value<string>("Exchange");
                info.OrderType = obj.Value<string>("OrderType") == "LIMIT_SELL" ? OrderType.Sell : OrderType.Buy;
                info.Quantity = obj.Value<double>("Quantity");
                info.QuantityRemaining = obj.Value<double>("QuantityRemaining");
                info.Limit = obj.Value<double>("Limit");
                info.CommissionPaid = obj.Value<double>("CommissionPaid");
                info.Price = obj.Value<double>("Price");
                info.Opened = obj.Value<DateTime>("Opened");
                info.Closed = obj.Value<DateTime>("Closed");
                info.CancelInitiated = obj.Value<bool>("CancelInitiated");
                info.ImmediateOrCancel = obj.Value<bool>("ImmediateOrCancel");
                info.IsConditional = obj.Value<bool>("IsConditional");
                info.Condition = obj.Value<string>("Condition");
                info.ConditionTarget = obj.Value<string>("ConditionTarget");

                ticker.OpenedOrders.Add(info);
            }
            RaiseOpenedOrdersChanged();
            return true;
        }
        void RaiseOpenedOrdersChanged() {

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
