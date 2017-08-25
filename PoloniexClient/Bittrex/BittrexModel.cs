using CryptoMarketClient.Common;
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

        public bool GetMarketsInfo() {
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
            Markets.Clear();
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
                        BittrexMarketInfo m = new BittrexMarketInfo();
                        m.MarketCurrency = obj.Value<string>("MarketCurrency");
                        m.BaseCurrency = obj.Value<string>("BaseCurrency");
                        m.MarketCurrencyLong = obj.Value<string>("MarketCurrencyLong");
                        m.BaseCurrencyLong = obj.Value<string>("BaseCurrencyLong");
                        m.MinTradeSize = obj.Value<decimal>("MinTradeSize");
                        m.MarketName = obj.Value<string>("MarketName");
                        m.IsActive = obj.Value<bool>("IsActive");
                        m.Created = obj.Value<DateTime>("Created");
                        m.Index = Markets.Count;
                        Markets.Add(m);
                    }
                }
            }
            return true;
        }
        public bool UpdateCurrencies() {
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
                    c.TxFree = obj.Value<decimal>("TxFee");
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
                c.TxFree = obj.Value<decimal>("TxFee");
                c.IsActive = obj.Value<bool>("IsActive");
                c.CoinType = obj.Value<string>("CoinType");
                c.BaseAddress = obj.Value<string>("BaseAddress");
                Currencies.Add(c);
            }
            return true;
        }
        public void GetTicker(BittrexMarketInfo info) {
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
                    info.HighestBid = obj.Value<decimal>("Bid");
                    info.LowestAsk = obj.Value<decimal>("Ask");
                    info.Last = obj.Value<decimal>("Last");
                    info.Time = DateTime.Now;
                    info.UpdateHistoryItem();
                }
            }
        }
        public bool GetMarketsSummaryInfo() {
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
                        BittrexMarketInfo info = Markets.FirstOrDefault((m) => m.MarketName == marketName);
                        if(info == null)
                            continue;
                        info.Hr24High = obj.Value<decimal>("High");
                        info.Hr24Low = obj.Value<decimal>("Low");
                        info.Volume = obj.Value<decimal>("Volume");
                        info.Last = obj.Value<decimal>("Last");
                        info.BaseVolume = obj.Value<decimal>("BaseVolume");
                        info.Time = obj.Value<DateTime>("TimeStamp");
                        info.HighestBid = obj.Value<decimal>("Bid");
                        info.LowestAsk = obj.Value<decimal>("Ask");
                        info.OpenBuyOrders = obj.Value<int>("OpenBuyOrders");
                        info.OpenSellOrders = obj.Value<int>("OpenSellOrders");
                        info.PrevDay = obj.Value<decimal>("PrevDay");
                        info.Created = obj.Value<DateTime>("Created");
                        info.DisplayMarketName = obj.Value<string>("DisplayMarketName");
                        //info.UpdateHistoryItem();
                    }
                }
            }
            return true;
        }
        public bool UpdateArbitrageOrderBook(BittrexMarketInfo info, int depth) {
            string address = GetOrderBookString(info, depth);
            string text = GetDownloadString(info, address);
            if(string.IsNullOrEmpty(text))
                return false;
            return UpdateOrderBook(info, text, false, depth);
        }
        public string GetOrderBookString(BittrexMarketInfo info, int depth) {
            return string.Format("https://bittrex.com/api/v1.1/public/getorderbook?market={0}&type=both&depth={1}", Uri.EscapeDataString(info.MarketName), depth);
        }
        public bool UpdateOrderBook(BittrexMarketInfo info, string text, int depth) {
            return UpdateOrderBook(info, text, true, depth);
        }
        public bool UpdateOrderBook(BittrexMarketInfo info, string text, bool raiseChanged, int depth) {
            JObject res = (JObject)JsonConvert.DeserializeObject(text);
            foreach(JProperty prop in res.Children()) {
                if(prop.Name == "success") {
                    if(prop.Value.Value<bool>() == false)
                        return false;
                }
                if(prop.Name == "message")
                    continue;
                if(prop.Name == "result") {
                    lock(info) {
                        info.OrderBook.Clear();
                        JArray bids = ((JObject)prop.Value).Value<JArray>("buy");
                        JArray asks = ((JObject)prop.Value).Value<JArray>("sell");
                        int count = 0;
                        foreach(JObject obj in bids) {
                            OrderBookEntry e = new OrderBookEntry();
                            e.Value = obj.Value<decimal>("Rate");
                            e.Amount = obj.Value<decimal>("Quantity");
                            info.OrderBook.Bids.Add(e);
                            if(++count >= depth)
                                break;
                        }
                        count = 0;
                        foreach(JObject obj in asks) {
                            OrderBookEntry e = new OrderBookEntry();
                            e.Value = obj.Value<decimal>("Rate");
                            e.Amount = obj.Value<decimal>("Quantity");
                            info.OrderBook.Asks.Add(e);
                            if(++count >= depth)
                                break;
                        }
                    }
                }
            }
            if(raiseChanged)
                info.OrderBook.RaiseOnChanged(new OrderBookUpdateInfo() { Action = OrderBookUpdateType.RefreshAll });
            return true;
        }
        public void GetOrderBook(BittrexMarketInfo info, int depth) {
            string address = string.Format("https://bittrex.com/api/v1.1/public/getorderbook?market={0}&type=both&depth={1}", Uri.EscapeDataString(info.MarketName), depth);
            string text = GetDownloadString(info, address);
            if(string.IsNullOrEmpty(text))
                return;
            UpdateOrderBook(info, text, depth);
        }
        public void GetTrades(BittrexMarketInfo info) {
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
                            item.Amount = obj.Value<decimal>("Quantity");
                            item.Rate = obj.Value<decimal>("Price");
                            item.Total = obj.Value<decimal>("Total");
                            item.Type = obj.Value<string>("OrderType") == "BUY" ? TradeType.Buy : TradeType.Sell;
                            item.Fill = obj.Value<string>("FillType") == "FILL" ? TradeFillType.Fill : TradeFillType.PartialFill;
                            info.TradeHistory.Add(item);
                            TickerUpdateHelper.UpdateHistoryForTradeItem(item, info);
                        }
                    }
                    Console.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff") + " trade add. process time = " + Timer.ElapsedMilliseconds);
                    info.RaiseTradeHistoryAdd();
                }
            }
        }
        public void UpdateTrades(BittrexMarketInfo info) {
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
                            item.Amount = obj.Value<decimal>("Quantity");
                            item.Rate = obj.Value<decimal>("Price");
                            item.Total = obj.Value<decimal>("Total");
                            item.Type = obj.Value<string>("OrderType") == "BUY" ? TradeType.Buy : TradeType.Sell;
                            item.Fill = obj.Value<string>("FillType") == "FILL" ? TradeFillType.Fill : TradeFillType.PartialFill;
                            TickerUpdateHelper.UpdateHistoryForTradeItem(item, info);
                            UpdateList.Add(item);
                        }
                    }
                    Console.WriteLine(info.Time.ToString("hh:mm:ss.fff") + " trade update. process time = " + Timer.ElapsedMilliseconds);
                    info.RaiseTradeHistoryAdd();
                }
            }
        }

        public string BuyLimit(BittrexMarketInfo info, decimal rate, decimal amount) {
            string address = string.Format("https://bittrex.com/api/v1.1/market/buylimit?apikey={0}&nonce={1}&market={2}&quantity={3}&rate={4}",
                Uri.EscapeDataString(ApiKey),
                DateTime.Now,
                Uri.EscapeDataString(info.MarketName),
                amount.ToString("0.########"),
                rate.ToString("0.########"));
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", GetSign(address));
            return OnBuyLimit(client.DownloadString(address));
        }
        public string SellLimit(BittrexMarketInfo info, decimal rate, decimal amount) {
            string address = string.Format("https://bittrex.com/api/v1.1/market/selllimit?apikey={0}&nonce={1}&market={2}&quantity={3}&rate={4}",
                Uri.EscapeDataString(ApiKey),
                DateTime.Now,
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
                DateTime.Now,
                uuid);
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", GetSign(address));
            return client.DownloadStringTaskAsync(address);
        }
        public Task<string> GetOpenOrders(BittrexMarketInfo info) {
            string address = string.Format("https://bittrex.com/api/v1.1/market/getopenorders?apikey={0}&nonce={1}&market={2}",
                Uri.EscapeDataString(ApiKey),
                DateTime.Now,
                info.MarketName);
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", GetSign(address));
            return client.DownloadStringTaskAsync(address);
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
            if(!res.Value<bool>("success")) {
                Debug.WriteLine("OnAppendOpenOrders fails: " + res.Value<string>("message"));
                return false;
            }

            JArray orders = (JArray)res.Value<JArray>("result");
            foreach(JObject obj in orders) {
                BittrexOrderInfo info = new BittrexOrderInfo();

                info.OrderUuid = obj.Value<string>("OrderUuid");
                info.Exchange = obj.Value<string>("Exchange");
                info.OrderType = obj.Value<string>("OrderType") == "LIMIT_SELL" ? OrderType.LimitSell : OrderType.LimitBuy;
                info.Quantity = obj.Value<decimal>("Quantity");
                info.QuantityRemaining = obj.Value<decimal>("QuantityRemaining");
                info.Limit = obj.Value<decimal>("Limit");
                info.CommissionPaid = obj.Value<decimal>("CommissionPaid");
                info.Price = obj.Value<decimal>("Price");
                info.Opened = obj.Value<DateTime>("Opened");
                info.Closed = obj.Value<DateTime>("Closed");
                info.CancelInitiated = obj.Value<bool>("CancelInitiated");
                info.ImmediateOrCancel = obj.Value<bool>("ImmediateOrCancel");
                info.IsConditional = obj.Value<bool>("IsConditional");
                info.Condition = obj.Value<string>("Condition");
                info.ConditionTarget = obj.Value<string>("ConditionTarget");

                UpdateOrders.Add(info);
            }
            RaiseOpenedOrdersChanged();
            return true;
        }
        void RaiseOpenedOrdersChanged() {
            
        }

        public bool GetBalance(string currency) {
            string address = string.Format("https://bittrex.com/api/v1.1/account/getbalance?apikey={0}&nonce={1}&currency={2}", Uri.EscapeDataString(ApiKey), DateTime.Now, currency);
            WebClient client = GetWebClient();
            client.Headers.Clear();
            client.Headers.Add("apisign", GetSign(address));
            return OnGetBalance(client.DownloadString(address));
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
                BittrexAccountBalanceInfo info = Balances.FirstOrDefault((b) => b.Currency == currency);
                if(info == null) {
                    info = new BittrexAccountBalanceInfo();
                    info.Currency = obj.Value<string>("Currency");
                    info.Requested = obj.Value<bool>("Requested");
                    info.Uuid = obj.Value<string>("Uuid");
                    Balances.Add(info);
                }
                info.Available = obj.Value<string>("Available") == null? 0: obj.Value<decimal>("Available");
                info.Balance = obj.Value<string>("Balance") == null ? 0 : obj.Value<decimal>("Balance");
                info.Pending = obj.Value<string>("Pending") == null ? 0 : obj.Value<decimal>("Pending");
                info.CryptoAddress = obj.Value<string>("CryptoAddress");
            }
            return true;
        }
        public bool GetBalances() {
            if(Currencies.Count == 0) {
                if(!GetCurrenciesInfo())
                    return false;
            }
            WebClient client = GetWebClient();
            foreach(BittrexCurrencyInfo info in Currencies) {
                string address = string.Format("https://bittrex.com/api/v1.1/account/getbalance?apikey={0}&nonce={1}&currency={2}", Uri.EscapeDataString(ApiKey), DateTime.Now, info.Currency);
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
            string address = string.Format("https://bittrex.com/api/v1.1/account/getbalances?apikey={0}&nonce={1}", Uri.EscapeDataString(ApiKey), DateTime.Now);
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
                    item.Balance = obj.Value<decimal>("Balance");
                    item.Available = obj.Value<decimal>("Available");
                    item.Pending = obj.Value<decimal>("Pending");
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
            
        }

        public bool Withdraw(string currency, decimal amount, string address, string paymentId) {
            string addr = string.Empty;
            if(string.IsNullOrEmpty(paymentId)) {
                addr = string.Format("https://bittrex.com/api/v1.1/account/withdraw?apikey={0}&nonce={1}&currency={2}&quantity={3}",
                    Uri.EscapeDataString(ApiKey),
                    DateTime.Now,
                    Uri.EscapeDataString(currency),
                    amount.ToString("0.########"));
            }
            else {
                addr = string.Format("https://bittrex.com/api/v1.1/account/withdraw?apikey={0}&nonce={1}&currency={2}&quantity={3}&paymentid={4}",
                        Uri.EscapeDataString(ApiKey),
                        DateTime.Now,
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
        public Task<string> WithdrawAsync(string currency, decimal amount, string address, string paymentId) {
            string addr = string.Empty;
            if(string.IsNullOrEmpty(paymentId)) {
                addr = string.Format("https://bittrex.com/api/v1.1/account/withdraw?apikey={0}&nonce={1}&currency={2}&quantity={3}",
                    Uri.EscapeDataString(ApiKey),
                    DateTime.Now,
                    Uri.EscapeDataString(currency),
                    amount.ToString("0.########"));
            }
            else {
                addr = string.Format("https://bittrex.com/api/v1.1/account/withdraw?apikey={0}&nonce={1}&currency={2}&quantity={3}&paymentid={4}",
                        Uri.EscapeDataString(ApiKey),
                        DateTime.Now,
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
        
        public void StartUpdateOrders() {
            UpdateOrders.Clear();
        }
        public void EndUpdateOrders() {
            lock(Orders) {
                Orders.Clear();
                Orders.AddRange(UpdateOrders);
            }
        }
        public string CheckCreateDeposit(string currency) {
            string address = string.Format("https://bittrex.com/api/v1.1/account/getdepositaddress?apikey={0}&nonce={1}&currency={2}", Uri.EscapeDataString(ApiKey), DateTime.Now, currency);
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
            BittrexAccountBalanceInfo info = Balances.FirstOrDefault(b => b.Currency == currency);
            info.Currency = addr.Value<string>("Address");
            return info.Currency;
        }
    }
}
