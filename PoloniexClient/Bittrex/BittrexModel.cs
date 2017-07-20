using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Bittrex {
    public class BittrexModel {
        static BittrexModel defaultModel;
        public static BittrexModel Default {
            get {
                if(defaultModel == null)
                    defaultModel = new BittrexModel();
                return defaultModel;
            }
        }

        public List<BittrexMarketInfo> Markets { get; } = new List<BittrexMarketInfo>();
        public void GetMarketsInfo() {
            string address = "https://bittrex.com/api/v1.1/public/getmarkets";
            string text;
            using(WebClient client = new WebClient()) {
                text = client.DownloadString(address);
            }
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

        public List<BittrexCurrencyInfo> Currencies { get; } = new List<BittrexCurrencyInfo>();
        public void GetCurrenciesInfo() {
            string address = "https://bittrex.com/api/v1.1/public/getcurrencies";
            string text;
            using(WebClient client = new WebClient()) {
                text = client.DownloadString(address);
            }
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
            string address = string.Format("https://bittrex.com/api/v1.1/public/getticker?market={0}", Uri.EscapeDataString(info.MarketName));
            string text;
            using(WebClient client = new WebClient()) {
                text = client.DownloadString(address);
            }
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
                }
            }
        }

        public void GetMarketsSummaryInfo() {
            string address = string.Format("https://bittrex.com/api/v1.1/public/getmarketsummaries");
            string text;
            using(WebClient client = new WebClient()) {
                text = client.DownloadString(address);
            }
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
                        info.TimeStamp = obj.Value<DateTime>("TimeStamp");
                        info.HighestBid = obj.Value<double>("Bid");
                        info.LowestAsk = obj.Value<double>("Ask");
                        info.OpenBuyOrders = obj.Value<int>("OpenBuyOrders");
                        info.OpenSellOrders = obj.Value<int>("OpenSellOrders");
                        info.PrevDay = obj.Value<double>("PrevDay");
                        info.Created = obj.Value<DateTime>("Created");
                        info.DisplayMarketName = obj.Value<string>("DisplayMarketName");
                    }
                }
            }
        }

        public void GetOrderBook(BittrexMarketInfo info, int depth) {
            string address = string.Format("https://bittrex.com/api/v1.1/public/getticker?market={0}&type=both&depth={1}", Uri.EscapeDataString(info.MarketName), depth);
            string text;
            using(WebClient client = new WebClient()) {
                text = client.DownloadString(address);
            }
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
                }
            }
        }
    }
}
