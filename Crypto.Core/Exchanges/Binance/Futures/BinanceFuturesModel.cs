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
using WebSocket4Net;

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

        public override bool SupportPositions => true;
        public override ExchangeType Type => ExchangeType.BinanceFutures;
        public override string BaseWebSocketAdress => "wss://fstream.binance.com/ws/!ticker@arr";

        protected override string CancelOrderApiString => "https://fapi.binance.com/fapi/v1/order";

        public override bool CreateDeposit(AccountInfo account, string currency) {
            throw new NotImplementedException();
        }

        protected override string ServerTimeApi { get { return "https://fapi.binance.com/fapi/v1/time"; } }

        protected override string BalanceApiString => "https://fapi.binance.com/fapi/v2/balance";
        protected virtual string PositionApiString => "https://fapi.binance.com/fapi/v2/account";
        protected override string TradeApiString => "https://fapi.binance.com/fapi/v1/order";
        protected override string OrderStatusApiString => "https://fapi.binance.com/fapi/v1/order";
        public override bool AllowCheckOrderStatus => true;

        protected override TradingResult OnTradeResultCore(AccountInfo account, Ticker ticker, JObject res) { 
            TradingResult tr = new TradingResult();
            tr.Ticker = ticker;
            tr.OrderId = res.Value<string>("orderId");
            tr.Amount = Convert.ToDouble(res.Value<string>("executedQty"));
            tr.Value= Convert.ToDouble(res.Value<string>("price"));
            tr.Type = res.Value<string>("side")[0] == 'B'? OrderType.Buy: OrderType.Sell; 
            tr.PositionSide = String2PositionSide(res.Value<string>("positionSide"));
            tr.Total = tr.Amount * tr.Value;
            tr.OrderStatus = res.Value<string>("status");
            tr.Filled = res.Value<bool>("closePosition") == true;
            return tr;
        }

        public override bool UpdateBalances(AccountInfo account) {
            if(account == null)
                return false;
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

        public override bool UpdatePositions(AccountInfo account, Ticker ticker) {
            string queryString = string.Format("timestamp={0}&recvWindow=50000", GetNonce());
            string signature = account.GetSign(queryString);

            string address = string.Format("{0}?{1}&signature={2}", PositionApiString, queryString, signature);
            MyWebClient client = GetWebClient();
            
            client.Headers.Clear();
            client.Headers.Add("X-MBX-APIKEY", account.ApiKey);

            try {
                return OnGetPositions(account, client.DownloadData(address));
            }
            catch(Exception e) {
                LogManager.Default.Log(e.ToString());
                return false;
            }
        }

        protected virtual bool OnGetPositions(AccountInfo account, byte[] data) {
            string text = UTF8Encoding.Default.GetString(data);
            object conv = JsonConvert.DeserializeObject(text);
            JObject root = conv as JObject;
            if(conv == null) { 
                LogManager.Default.Error(this, "error on get positions", "empty text return");
                return false;
            }
            if(root != null && root.Value<string>("code") != null) {
                LogManager.Default.Error(this, "error on get positions", root.Value<string>("message"));
                return false;
            }
            JArray positions = root.Value<JArray>("positions");
            if(positions == null)
                return false;
            account.Positions.Clear();
            foreach(JObject obj in positions) {
                string symbol = obj.Value<string>("symbol");
                PositionInfo p = new PositionInfo() { Account = account, Ticker = GetTicker(symbol) };
                p.InitialMarginString = obj.Value<string>("initialMargin");
                p.MaintMarginString = obj.Value<string>("maintMargin");
                p.UnrealizedProfitString = obj.Value<string>("unrealizedProfit");
                p.PositionInitialMarginString = obj.Value<string>("positionInitialMargin");
                p.OpenOrderInitialMarginString = obj.Value<string>("openOrderInitialMargin"); 
                p.LeverageString = obj.Value<string>("leverage");
                p.Isolated = obj.Value<bool>("isolated");
                p.EntryPriceString = obj.Value<string>("entryPrice");
                p.MaxNotionalString = obj.Value<string>("maxNotional"); 
                p.Side = String2PositionSide(obj.Value<string>("positionSide"));
                p.PositionAmountString = obj.Value<string>("positionAmt");
                p.UpdateTime = FromUnixTimestampMs(obj.Value<long>("updateTime"));
                account.Positions.Add(p);
            }
            return true;
        }

        public override bool GetBalance(AccountInfo account, string currency) {
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
                BalanceBase b = account.GetOrCreateBalanceInfo(obj.Value<string>("asset"));
                b.Balance = obj.Value<double>("balance");
                b.Available = obj.Value<double>("availableBalance");
                b.OnOrders = b.Balance - b.Available;
                account.Balances.Add(b);
            }
            return true;
        }

        protected override string[] CreateOpenedOrdersString() {
            return new string[] { 
                "orderId",
		        "symbol",
		        "status",
		        "clientOrderId",
		        "price",
		        "avgPrice",
		        "origQty",
		        "executedQty",
		        "cumQuote",
		        "timeInForce",
		        "type",
		        "reduceOnly",
		        "closePosition",
		        "side",
		        "positionSide",
		        "stopPrice",
		        "workingType",
		        "priceProtect",
		        "origType",
		        "time",
		        "updateTime"
            };
        }

        protected override OpenedOrderInfo InitializeOpenedOrderItem(string[] item, Ticker ticker) {
            OpenedOrderInfo t = new OpenedOrderInfo(null, ticker);
            t.OrderId = item[0];
            t.Ticker = ticker;
            t.ValueString = item[4];
            t.AmountString = item[6];
            t.TotalString = item[7];
            t.Type = item[13][0] == 'B'? OrderType.Buy: OrderType.Sell;
            t.Date = FromUnixTime(FastValueConverter.ConvertPositiveLong(item[19]));

            return t;
        }

        protected override TradeInfoItem InitializeTradeInfoItem(string[] item, Ticker ticker) {
            DateTime time = FromUnixTime(FastValueConverter.ConvertPositiveLong(item[4]));

            TradeInfoItem t = new TradeInfoItem(null, ticker);
            bool isBuy = item[4][0] != 't';
            t.AmountString = item[2];
            t.Time = time;
            t.Type = isBuy ? TradeType.Buy : TradeType.Sell;
            t.RateString = item[1];
            t.IdString = item[0];

            return t;
        }

        protected override void UpdateTickerInfo(BinanceTicker t, JObject item) {
            if(t == null)
                return;
            t.Last = item.Value<double>("lastPrice");
            t.LowestAsk = 0;
            t.HighestBid = 0;
            t.Change = item.Value<double>("priceChangePercent");
            t.BaseVolumeString = item.Value<string>("volume");
            t.VolumeString = item.Value<string>("quoteVolume");
            t.Hr24HighString = item.Value<string>("highPrice");
            t.Hr24LowString = item.Value<string>("lowPrice");
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
            t.LastString = item[6];
            t.Hr24HighString = item[9];
            t.Hr24LowString = item[10];
            t.BaseVolumeString = item[11];
            t.VolumeString = item[12];
        }

        public override bool GetDeposites(AccountInfo account) {
            return true;
        }

        protected override string[] CreateTradeItemString() {
            return new string[] { "id", "price", "qty", "quoteQty", "time", "isBuyerMaker" };
        }

        protected override string ExchangeSettingsApi { get { return "https://binance.com/fapi/v1/exchangeInfo"; } }

        protected override string UpdateAccountTradesApiString => "https://fapi.binance.com/fapi/v1/userTrades";
        protected override TradeInfoItem InitializeAccountTradeInfoItem(string[] item, Ticker ticker) {
            DateTime time = FromUnixTime(FastValueConverter.ConvertPositiveLong(item[13]));

            TradeInfoItem t = new TradeInfoItem(null, ticker);
            t.OrderNumber = item[5];
            t.AmountString = item[7];
            t.Time = time;
            t.Type = String2TradeType(item[10]);
            t.RateString = item[6];
            t.IdString = item[3];
            return t;
        }

        public override bool UpdateCurrencies() {
            return true;
        }

        protected override string OpenOrdersApiString => "https://fapi.binance.com/fapi/v1/openOrders";

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
            if(info.StreamType == CaptureStreamType.OrderBook)
                OnOrderBookSocketMessageReceived(this, new MessageReceivedEventArgs(info.Message));
            else if(info.StreamType == CaptureStreamType.TradeHistory)
                OnTradeHistorySocketMessageReceived(this, new MessageReceivedEventArgs(info.Message));
            else if(info.StreamType == CaptureStreamType.KLine)
                OnKlineSocketMessageReceived(this, new MessageReceivedEventArgs(info.Message));
        }

        protected internal override IIncrementalUpdateDataProvider CreateIncrementalUpdateDataProvider() {
            return new BinanceFuturesIncrementalUpdateDataProvider();
        }
    }
}
