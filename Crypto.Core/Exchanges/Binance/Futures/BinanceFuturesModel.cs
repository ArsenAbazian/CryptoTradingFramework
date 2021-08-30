using Crypto.Core.Exchanges.Base;
using Crypto.Core.Helpers;
using Crypto.Core;
using Crypto.Core.Binance;
using Crypto.Core.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Binance.Futures {
    public class BinanceFuturesExchange : BinanceExchange {
        static BinanceFuturesExchange defaultExchange;
        public static new BinanceFuturesExchange Default {
            get {
                if(defaultExchange == null) {
                    defaultExchange = (BinanceFuturesExchange)Exchange.FromFile(ExchangeType.BinanceFutures, typeof(BinanceFuturesExchange));
                }
                return defaultExchange;
            }
        }

        public override ExchangeType Type => ExchangeType.BinanceFutures;
        public override string BaseWebSocketAdress => "wss://fstream.binance.com/ws/!ticker@arr";

        public override TradingResult BuyLong(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public override TradingResult BuyShort(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public override bool Cancel(AccountInfo account, string orderId) {
            throw new NotImplementedException();
        }

        public override string CreateDeposit(AccountInfo account, string currency) {
            throw new NotImplementedException();
        }

        protected override string ServerTimeApi { get { return "https://fapi.binance.com/fapi/v1/time"; } }

        protected override string BalanceApiString => "https://fapi.binance.com/fapi/v2/balance";

        public override bool UpdateBalances(AccountInfo account) {
            string queryString = string.Format("timestamp={0}&recvWindow=50000", GetNonce());
            string signature = account.GetSign(queryString);

            string address = string.Format("{0}?{1}&signature={2}", BalanceApiString, queryString, signature);
            MyWebClient client = GetWebClient();
            
            client.Headers.Clear();
            client.Headers.Add("X-MBX-APIKEY", account.ApiKey);

            try {
                return OnGetBalances(account, client.DownloadData(address));
            }
            catch(Exception e) {
                LogManager.Default.Log(e.ToString());
                return false;
            }
        }

        public override bool OnGetBalances(AccountInfo account, byte[] data) {
            string text = UTF8Encoding.Default.GetString(data);
            object conv = JsonConvert.DeserializeObject(text);
            JObject root = conv as JObject;
            if(conv == null) { 
                LogManager.Default.Error(this, "error on get balance", "empty text return");
                return false;
            }
            if(root != null && root.Value<string>("code") != null) {
                LogManager.Default.Error(this, "error on get balance", root.Value<string>("message"));
                return false;
            }
            JArray balances = conv as JArray;
            if(balances == null)
                return false;
            account.Balances.Clear();
            foreach(JObject obj in balances) {
                BinanceAccountBalanceInfo b = new BinanceAccountBalanceInfo(account);
                b.Balance = obj.Value<double>("balance");
                b.Currency = obj.Value<string>("asset");
                b.Available = obj.Value<double>("availableBalance");
                b.OnOrders = b.Balance - b.Available;
                account.Balances.Add(b);
            }
            return true;
        }

        protected override void UpdateTickerInfo(BinanceTicker t, JObject item) {
            if(t == null)
                return;
            t.Last = item.Value<double>("lastPrice");
            t.LowestAsk = 0;
            t.HighestBid = 0;
            t.Change = item.Value<double>("priceChangePercent");
            t.BaseVolume = item.Value<double>("volume");
            t.Volume = item.Value<double>("quoteVolume");
            t.Hr24High = item.Value<double>("highPrice");
            t.Hr24Low = item.Value<double>("lowPrice");
        }

         protected override string[] CreateWebSocketTickersInfoStrings() {
            return new string[] {
                        "e",
                        "E",
                        "s",
                        "p",
                        "P",
                        "w",
                        "c",
                        "Q",
                        "o",
                        "h",
                        "l",
                        "v",
                        "q",
                        "O",
                        "C",
                        "F",
                        "L",
                        "n"
                    };
        }

        protected override void On24HourTickerRecvCore(BinanceTicker t, string[] item) {
            if(t == null)
                throw new DllNotFoundException("binance symbol not found " + item[2]);
            t.Change = FastValueConverter.Convert(item[4]);
            t.HighestBid = 0;
            t.LowestAsk = 0;
            t.Last = FastValueConverter.Convert(item[6]);
            t.Hr24High = FastValueConverter.Convert(item[9]);
            t.Hr24Low = FastValueConverter.Convert(item[10]);
            t.BaseVolume = FastValueConverter.Convert(item[11]);
            t.Volume = FastValueConverter.Convert(item[12]);
        }

        public override bool GetDeposites(AccountInfo account) {
            return true;
        }

        protected override string[] CreateTradeItemString() {
            return new string[] { "id", "price", "qty", "quoteQty", "time", "isBuyerMaker" };
        }

        private TradeInfoItem InitializeTradeInfoItem(string[] item, Ticker ticker) {
            DateTime time = FromUnixTime(FastValueConverter.ConvertPositiveLong(item[4]));
            int tradeId = FastValueConverter.ConvertPositiveInteger(item[0]);

            TradeInfoItem t = new TradeInfoItem(null, ticker);
            bool isBuy = item[5][0] != 't';
            t.AmountString = item[2];
            t.Time = time;
            t.Type = isBuy ? TradeType.Buy : TradeType.Sell;
            t.RateString = item[1];
            t.Id = tradeId;
            double price = t.Rate;
            double amount = t.Amount;
            t.Total = price * amount;

            return t;
        }

        protected override string ExchangeSettingsApi { get { return "https://binance.com/fapi/v1/exchangeInfo"; } }

        public override void OnAccountRemoved(AccountInfo info) {
        }

        public override bool ProcessOrderBook(Ticker tickerBase, string text) {
            return true;
        }

        public override TradingResult SellLong(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public override TradingResult SellShort(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public override bool UpdateAccountTrades(AccountInfo account, Ticker ticker) {
            return true;
        }

        public override bool UpdateCurrencies() {
            return true;
        }

        public override bool UpdateOpenedOrders(AccountInfo account, Ticker ticker) {
            return true;
        }

        protected override string UpdateOrderBookApiString => "https://fapi.binance.com/fapi/v1/depth";

        public override bool UpdateTicker(Ticker tickerBase) {
            return true;
        }

        protected override string TickerInfoApiString => "https://fapi.binance.com/fapi/v1/ticker/24hr";

        protected override string UpdateTradesApiString => "https://fapi.binance.com/fapi/v1/trades";

        public override bool Withdraw(AccountInfo account, string currency, string adress, string paymentId, double amount) {
            throw new NotImplementedException();
        }

        protected override string AggTradesApiString => "https://fapi.binance.com/fapi/v1/aggTrades";

        protected internal override void ApplyCapturedEvent(Ticker ticker, TickerCaptureDataInfo info) {
            throw new NotImplementedException();
        }

        protected internal override IIncrementalUpdateDataProvider CreateIncrementalUpdateDataProvider() {
            return new BinanceFuturesIncrementalUpdateDataProvider();
        }
    }
}
