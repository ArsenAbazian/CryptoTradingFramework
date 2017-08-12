using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Bittrex {
    public class BittrexModel : ModelBase {
        static BittrexModel defaultModel;
        public static BittrexModel Default {
            get {
                if(defaultModel == null) {
                    defaultModel = new BittrexModel();
                    defaultModel.Load();
                }
                return defaultModel;
            }
        }

        public override string Name => "Bittrex";

        public List<BittrexMarketInfo> Markets { get; } = new List<BittrexMarketInfo>();
        public List<BittrexOrderInfo> Orders { get; } = new List<BittrexOrderInfo>();
        protected List<BittrexOrderInfo> UpdateOrders { get; } = new List<BittrexOrderInfo>();
        public List<BittrexAccountBalanceInfo> Balances { get; } = new List<BittrexAccountBalanceInfo>();
        public List<BittrexCurrencyInfo> Currencies { get; } = new List<BittrexCurrencyInfo>();
        protected List<TradeHistoryItem> UpdateList { get; } = new List<TradeHistoryItem>(100);

        public void GetMarketsInfo() {
            string address = "https://bittrex.com/api/v1.1/public/getmarkets";
            string text = GetDownloadString(address);
            Markets.Clear();
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            foreach(JProperty prop in res.Children()) {
                if(prop.Name == "success") {
                    if(prop.Value.Value<bool>() == false)
                        break;
                }
                if(prop.Name == "message")
                    continue;
                if(prop.Name == "result") {
                    JArray markets = (JArray)prop.Value;
                    foreach(JObject obj in markets) {
                        BittrexMarketInfo m = new BittrexMarketInfo();
                        m.MarketCurrency = obj.Value<string>("MarketCurrency");
                        m.BaseCurrency = obj.Value<string>("BaseCurrency");
                        m.MarketCurrencyLong = obj.Value<string>("MarketCurrencyLong");
                        m.BaseCurrencyLong = obj.Value<string>("BaseCurrencyLong");
                        m.MinTradeSize = obj.Value<double>("MinTradeSize");
                        m.MarketName = obj.Value<string>("MarketName");
                        m.IsActive = obj.Value<bool>("IsActive");
                        m.Created = obj.Value<DateTime>("Created");
                        m.Index = Markets.Count;
                        Markets.Add(m);
                    }
                }
            }
        }
        public void GetCurrenciesInfo() {
            string address = "https://bittrex.com/api/v1.1/public/getcurrencies";
            string text = GetDownloadString(address);
            if(string.IsNullOrEmpty(text))
                return;
            Currencies.Clear();
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            foreach(JProperty prop in res.Children()) {
                if(prop.Name == "success") {
                    if(prop.Value.Value<bool>() == false)
                        break;
                }
                if(prop.Name == "message")
                    continue;
                if(prop.Name == "result") {
                    JArray markets = (JArray)prop.Value;
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
                }
            }
        }
        public void GetTicker(BittrexMarketInfo info) {
            Timer.Reset();
            Timer.Start();
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
                    info.Time = DateTime.Now;
                    info.UpdateHistoryItem();
                    Timer.Stop();
                    //Console.WriteLine(info.Time.ToString("hh:mm:ss.fff") + " ticker update last = " + info.Last + "  bid = " + info.HighestBid + "  ask = " + info.LowestAsk + ". process time = " + Timer.ElapsedMilliseconds);
                }
            }
        }
        public void GetMarketsSummaryInfo() {
            string address = string.Format("https://bittrex.com/api/v1.1/public/getmarketsummaries");
            string text = GetDownloadString(address);
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
                    JArray markets = (JArray)prop.Value;
                    foreach(JObject obj in markets) {
                        string marketName = obj.Value<string>("MarketName");
                        BittrexMarketInfo info = Markets.FirstOrDefault((m) => m.MarketName == marketName);
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
        }
        public string GetOrderBookString(BittrexMarketInfo info, int depth) {
            return string.Format("https://bittrex.com/api/v1.1/public/getorderbook?market={0}&type=both&depth={1}", Uri.EscapeDataString(info.MarketName), depth);
        }
        public void UpdateOrderBook(BittrexMarketInfo info, string text) {
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            foreach(JProperty prop in res.Children()) {
                if(prop.Name == "success") {
                    if(prop.Value.Value<bool>() == false)
                        break;
                }
                if(prop.Name == "message")
                    continue;
                if(prop.Name == "result") {
                    lock(info) {
                        info.OrderBook.Clear();
                        JArray bids = ((JObject)prop.Value).Value<JArray>("buy");
                        JArray asks = ((JObject)prop.Value).Value<JArray>("sell");
                        foreach(JObject obj in bids) {
                            OrderBookEntry e = new OrderBookEntry();
                            e.Value = obj.Value<double>("Rate");
                            e.Amount = obj.Value<double>("Quantity");
                            info.OrderBook.Bids.Add(e);
                        }
                        foreach(JObject obj in asks) {
                            OrderBookEntry e = new OrderBookEntry();
                            e.Value = obj.Value<double>("Rate");
                            e.Amount = obj.Value<double>("Quantity");
                            info.OrderBook.Asks.Add(e);
                        }
                    }
                    Timer.Stop();
                    //Console.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff") + " order book update. process time = " + Timer.ElapsedMilliseconds);
                }
            }
            info.OrderBook.RaiseOnChanged(new OrderBookUpdateInfo() { Action = OrderBookUpdateType.RefreshAll });
        }
        public void GetOrderBook(BittrexMarketInfo info, int depth) {
            Timer.Reset();
            Timer.Start();
            string address = string.Format("https://bittrex.com/api/v1.1/public/getorderbook?market={0}&type=both&depth={1}", Uri.EscapeDataString(info.MarketName), depth);
            string text = GetDownloadString(info, address);
            if(string.IsNullOrEmpty(text))
                return;
            UpdateOrderBook(info, text);
        }
        public void GetTrades(BittrexMarketInfo info) {
            Timer.Reset();
            Timer.Start();
            string address = string.Format("https://bittrex.com/api/v1.1/public/getmarkethistory?market={0}", Uri.EscapeDataString(info.MarketName));
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
                    lock(info) {
                        info.TradeHistory.Clear();
                        JArray trades = (JArray)prop.Value;
                        foreach(JObject obj in trades) {
                            TradeHistoryItem item = new TradeHistoryItem();
                            item.Id = obj.Value<int>("Id");
                            item.Time = obj.Value<DateTime>("TimeStamp");
                            item.Amount = obj.Value<double>("Quantity");
                            item.Rate = obj.Value<double>("Price");
                            item.Total = obj.Value<double>("Total");
                            item.Type = obj.Value<string>("OrderType") == "BUY" ? TradeType.Buy : TradeType.Sell;
                            item.Fill = obj.Value<string>("FillType") == "FILL" ? TradeFillType.Fill : TradeFillType.PartialFill;
                            info.TradeHistory.Add(item);
                            TickerUpdateHelper.UpdateHistoryForTradeItem(item, info);
                        }
                    }
                    Timer.Stop();
                    Console.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff") + " trade add. process time = " + Timer.ElapsedMilliseconds);
                    info.RaiseTradeHistoryAdd();
                }
            }
        }
        public void UpdateTrades(BittrexMarketInfo info) {
            Timer.Reset();
            Timer.Stop();
            string address = string.Format("https://bittrex.com/api/v1.1/public/getmarkethistory?market={0}", Uri.EscapeDataString(info.MarketName));
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
                    lock(info) {
                        JArray trades = (JArray)prop.Value;
                        UpdateList.Clear();
                        int lastId = info.TradeHistory.Count > 0 ? info.TradeHistory.First().Id : -1;
                        foreach(JObject obj in trades) {
                            int id = obj.Value<int>("Id");
                            if(id == lastId) {
                                info.TradeHistory.InsertRange(0, UpdateList);
                                break;
                            }
                            TradeHistoryItem item = new TradeHistoryItem();
                            item.Id = id;
                            item.Time = obj.Value<DateTime>("TimeStamp");
                            item.Amount = obj.Value<double>("Quantity");
                            item.Rate = obj.Value<double>("Price");
                            item.Total = obj.Value<double>("Total");
                            item.Type = obj.Value<string>("OrderType") == "BUY" ? TradeType.Buy : TradeType.Sell;
                            item.Fill = obj.Value<string>("FillType") == "FILL" ? TradeFillType.Fill : TradeFillType.PartialFill;
                            TickerUpdateHelper.UpdateHistoryForTradeItem(item, info);
                            UpdateList.Add(item);
                        }
                    }
                    Timer.Stop();
                    Console.WriteLine(info.Time.ToString("hh:mm:ss.fff") + " trade update. process time = " + Timer.ElapsedMilliseconds);
                    info.RaiseTradeHistoryAdd();
                }
            }
        }

        public Task<string> BuyLimit(BittrexMarketInfo info, double rate, double amount) {
            string address = string.Format("https://bittrex.com/api/v1.1/market/buylimit?apikey={0}&market={1}&quantity={2}&rate={3}",
                Uri.EscapeDataString(ApiKey),
                Uri.EscapeDataString(info.MarketName),
                amount.ToString("0.########"),
                rate.ToString("0.########"));
            return WebClient.DownloadStringTaskAsync(address);
        }
        public Task<string> SellLimit(BittrexMarketInfo info, double rate, double amount) {
            string address = string.Format("https://bittrex.com/api/v1.1/market/selllimit?apikey={0}&market={1}&quantity={2}&rate={3}",
                Uri.EscapeDataString(ApiKey),
                Uri.EscapeDataString(info.MarketName),
                amount.ToString("0.########"),
                rate.ToString("0.########"));
            return WebClient.DownloadStringTaskAsync(address);
        }
        public Task<string> CancelOrder(string uuid) {
            string address = string.Format("https://bittrex.com/api/v1.1/market/cancel?apikey={0}&uuid={1}",
                Uri.EscapeDataString(ApiKey),
                uuid);
            return WebClient.DownloadStringTaskAsync(address);
        }
        public Task<string> GetOpenOrders(BittrexMarketInfo info) {
            string address = string.Format("https://bittrex.com/api/v1.1/market/getopenorders?apikey={0}&market={1}",
                Uri.EscapeDataString(ApiKey),
                info.MarketName);
            return WebClient.DownloadStringTaskAsync(address);
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
        public bool OnAppendOpenOrders(string result) {
            if(string.IsNullOrEmpty(result))
                return false;
            JObject res = (JObject)JsonConvert.DeserializeObject(result);
            foreach(JProperty prop in res.Children()) {
                if(prop.Name == "success") {
                    if(prop.Value.Value<bool>() == false)
                        return false;
                }
                if(prop.Name == "message")
                    continue;
                if(prop.Name == "result") {
                    lock(Orders) {
                        JArray orders = (JArray)prop.Value;
                        foreach(JObject obj in orders) {
                            BittrexOrderInfo info = new BittrexOrderInfo();

                            info.OrderUuid = obj.Value<string>("OrderUuid");
                            info.Exchange = obj.Value<string>("Exchange");
                            info.OrderType = obj.Value<string>("OrderType") == "LIMIT_SELL" ? BittrexOrderType.LimitSell : BittrexOrderType.LimitBuy;
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

                            UpdateOrders.Add(info);
                        }
                    }
                    RaiseOpenedOrdersChanged();
                }
            }
            return true;
        }
        void RaiseOpenedOrdersChanged() {
            throw new NotImplementedException();
        }

        public Task<string> GetBalances() {
            string address = string.Format("https://bittrex.com/api/v1.1/account/getbalances?apikey={0}&nonce={1}", Uri.EscapeDataString(ApiKey), DateTime.Now);
            return WebClient.DownloadStringTaskAsync(address);
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
                    item.CryptoAddress = obj.Value<string>("CryptoAddress");
                    item.Requested = obj.Value<bool>("Requested");
                    item.Uuid = obj.Value<string>("Uuid");
                    Balances.Add(item);
                }
            }
            RaiseBalancesChanged();
            return true;
        }
        void RaiseBalancesChanged() {
            throw new NotImplementedException();
        }

        public Task<string> Withdraw(string currency, double amount, string address, string paymentId) {
            string addr = string.Empty;
            if(string.IsNullOrEmpty(paymentId)) {
                addr = string.Format("https://bittrex.com/api/v1.1/account/withdraw?apikey={0}&currency={1}&quantity={2}",
                    Uri.EscapeDataString(ApiKey),
                    Uri.EscapeDataString(currency),
                    amount.ToString("0.########"));
            }
            else {
                addr = string.Format("https://bittrex.com/api/v1.1/account/withdraw?apikey={0}&currency={1}&quantity={2}&paymentid={3}",
                        Uri.EscapeDataString(ApiKey),
                        Uri.EscapeDataString(currency),
                        amount.ToString("0.########"),
                        Uri.EscapeDataString(paymentId));
            }
            return WebClient.DownloadStringTaskAsync(addr);
        }
        public string OnWithdraw(string result) {
            return OnUuidResult(result);
        }
        
        public void StartUpdateOrders() {
            UpdateOrders.Clear();
        }
        public void EndUpdateOrders() {
            lock(Orders) {
                Orders.Clear();
                Orders.AddRange(UpdateOrders);
            }
        }
    }
}
