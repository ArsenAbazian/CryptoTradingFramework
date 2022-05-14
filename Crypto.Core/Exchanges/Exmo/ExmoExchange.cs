using Crypto.Core.Common;
using Crypto.Core.Exchanges.Base;
using Crypto.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Exmo {
    public class ExmoExchange : Exchange {
        static ExmoExchange defaultExchange;
        public static ExmoExchange Default {
            get {
                if(defaultExchange == null)
                    defaultExchange = (ExmoExchange)Exchange.FromFile(ExchangeType.EXMO, typeof(ExmoExchange));
                return defaultExchange;
            }
        }

        public override bool AllowCandleStickIncrementalUpdate => false;

        public override ExchangeType Type => ExchangeType.EXMO;

        public override string BaseWebSocketAdress => throw new NotImplementedException();

        protected override bool ShouldAddKlineListener => false;

        public override TradingResult BuyLong(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public override TradingResult BuyShort(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public override bool Cancel(AccountInfo account, Ticker ticker, string orderId) {
            throw new NotImplementedException();
        }

        public override BalanceBase CreateAccountBalance(AccountInfo info, string currency) {
            return new ExmoAccountBalanceInfo(info, GetOrCreateCurrency(currency));
        }

        public override bool CreateDeposit(AccountInfo account, string currency) {
            throw new NotImplementedException();
        }

        public override Ticker CreateTicker(string name) {
            return new ExmoTicker(this) { CurrencyPair = name };
        }

        public override List<CandleStickIntervalInfo> GetAllowedCandleStickIntervals() {
            List<CandleStickIntervalInfo> list = new List<CandleStickIntervalInfo>();
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(1), Text = "1 Minute", Command = "1" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(5), Text = "5 Minutes", Command = "5" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(15), Text = "15 Minutes", Command = "15" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(30), Text = "30 Minutes", Command = "30" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromMinutes(45), Text = "45 Minutes", Command = "45" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromHours(1), Text = "1 Hour", Command = "60" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromHours(2), Text = "3 Hours", Command = "120" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromHours(3), Text = "6 Hours", Command = "180" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromHours(4), Text = "12 Hours", Command = "240" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromDays(1), Text = "1 Day", Command = "D" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromDays(7), Text = "1 Week", Command = "W" });
            list.Add(new CandleStickIntervalInfo() { Interval = TimeSpan.FromDays(30), Text = "1 Month", Command = "M" });
            return list;
        }

        public override ResizeableArray<CandleStickData> GetCandleStickData(Ticker ticker, int candleStickPeriodMin, DateTime start, long periodInSeconds) {
            string cmd = GetCandleStickCommandName(candleStickPeriodMin);
            long startSec = ToUnixTimestamp(start);
            long endSec = startSec + periodInSeconds;
            string address = string.Format("https://api.exmo.com/v1.1/candles_history?symbol={0}&resolution={1}&from={2}&to={3}", 
                ticker.CurrencyPair, cmd, startSec, endSec);
            try {
                return OnGetCandleStickData(ticker, GetDownloadBytes(address));
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return null;
            }
        }

        protected virtual ResizeableArray<CandleStickData> OnGetCandleStickData(Ticker ticker, byte[] bytes) {
            if(bytes == null || bytes.Length == 0) {
                LogManager.Default.Error(this, nameof(GetCandleStickData), "No data received");
                return null;
            }
            var root = JsonHelper.Default.Deserialize(bytes);

            var items = root.Properties[0];
            int itemsCount = items.ItemsCount;
            ResizeableArray<CandleStickData> list = new ResizeableArray<CandleStickData>(itemsCount);
            for(int i = 0; i < itemsCount; i++) {
                var props = items.Items[i].Properties;
                CandleStickData data = new CandleStickData();
                data.Time = new DateTime(TimeSpan.TicksPerMillisecond * props[0].ValueLong);
                data.Open = props[1].ValueDouble;
                data.Close = props[2].ValueDouble;
                data.High = props[3].ValueDouble;
                data.Low = props[4].ValueDouble;
                data.Volume = props[5].ValueDouble;
                list.Add(data);
            }
            return list;
        }

        public override ResizeableArray<CandleStickData> GetRecentCandleStickData(Ticker ticker, int candleStickPeriodMin) {
            return GetCandleStickData(ticker, candleStickPeriodMin, DateTime.Now.AddMinutes(-candleStickPeriodMin * 300), candleStickPeriodMin * 300 * 60);
        }

        public override bool GetBalance(AccountInfo info, string currency) {
            return true;
        }

        public override bool GetDeposit(AccountInfo account, CurrencyInfo currency) {
            throw new NotImplementedException();
        }

        public override bool GetDeposites(AccountInfo account) {
            throw new NotImplementedException();
        }

        public override bool GetTickersInfo() {
            if(Tickers.Count > 0)
                return true;
            string address = "https://api.exmo.com/v1.1/ticker";
            byte[] bytes = null;
            try {
                bytes = UploadValues(address, new HttpRequestParamsCollection());

                if(bytes == null) {
                    LogManager.Default.Error(this, nameof(GetTickersInfo), "No data received.");
                    return false;
                }

                JsonHelperToken array = JsonHelper.Default.Deserialize(bytes);
                for(int i = 0; i < array.Properties.Length; i++) {
                    var info = array.Properties[i];
                    string name = info.Name;
                    ExmoTicker m = (ExmoTicker)GetOrCreateTicker(name);

                    m.CurrencyPair = name;
                    string[] pairs = m.CurrencyPair.Split('_');
                    if(pairs.Length != 2)
                        continue;
                    m.MarketCurrency = pairs[0];
                    m.BaseCurrency = pairs[1];
                    m.LastString = info.Properties[2].Value;
                    m.HighestBidString = info.Properties[0].Value;
                    m.LowestAskString = info.Properties[1].Value;
                    m.Hr24HighString = info.Properties[3].Value;
                    m.Hr24LowString = info.Properties[4].Value;
                    m.VolumeString = info.Properties[6].Value;
                    m.BaseVolumeString = info.Properties[7].Value;
                    AddTicker(m);
                }
            }
            catch(Exception) {
                return false;
            }
            IsInitialized = true;
            return true;
        }

        public override bool ObtainExchangeSettings() {
            return true;
        }

        public override TradingResult SellLong(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public override TradingResult SellShort(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public override bool SupportWebSocket(WebSocketType type) {
            return false;
        }

        public override bool UpdateAccountTrades(AccountInfo account, Ticker ticker) {
            throw new NotImplementedException();
        }

        public override bool UpdateBalances(AccountInfo info) {
            throw new NotImplementedException();
        }

        public override bool UpdateCurrencies() {
            throw new NotImplementedException();
        }

        public override bool UpdateOpenedOrders(AccountInfo account, Ticker ticker) {
            return true;
            //throw new NotImplementedException();
        }

        public override bool UpdateOrderBook(Ticker tickerBase) {
            throw new NotImplementedException();
        }

        public override bool UpdateOrderBook(Ticker tickerBase, int depth) {
            throw new NotImplementedException();
        }

        public override void UpdateOrderBookAsync(Ticker tickerBase, int depth, Action<OperationResultEventArgs> onOrderBookUpdated) {
            throw new NotImplementedException();
        }

        public override bool UpdateTicker(Ticker tickerBase) {
            throw new NotImplementedException();
        }

        public override bool UpdateTickersInfo() {
            return GetTickersInfo();
        }

        public override bool UpdateTrades(Ticker ticker) {
            string address = "https://api.exmo.com/v1.1/trades";
            try {
                HttpRequestParamsCollection coll = new HttpRequestParamsCollection();
                coll.Add(new KeyValuePair<string, string>("pair", ticker.CurrencyPair));
                return OnUpdateTrades(ticker, UploadValues(address, coll));
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                return false;
            }
        }

        protected virtual bool OnUpdateTrades(Ticker ticker, byte[] bytes) {
            if(bytes == null) {
                LogManager.Default.Error(this, nameof(OnUpdateTrades), "No data received");
                return false;
            }

            var root = JsonHelper.Default.Deserialize(bytes);
            if(root.PropertiesCount != 1 || root.Properties[0].Name != ticker.CurrencyPair) {
                LogManager.Default.Error(this, nameof(OnUpdateTrades), "Invalid data received");
                return false;
            }
            int itemsCount = root.Properties[0].ItemsCount;
            ResizeableArray<TradeInfoItem> newItems = new ResizeableArray<TradeInfoItem>(itemsCount);
            var items = root.Properties[0].Items;

            lock(ticker) {
                ticker.LockTrades();
                ticker.ClearTradeHistory();
                for(int i = itemsCount - 1; i >= 0; i--) {
                    var item = items[i];
                    TradeInfoItem ti = new TradeInfoItem(DefaultAccount, ticker);
                    ti.IdString = item.Properties[0].Value;
                    ti.Time = FromUnixTimestamp(item.Properties[1].ValueLong);
                    ti.Type = item.Properties[2].Value[0] == 's'? TradeType.Sell: TradeType.Buy;
                    ti.RateString = item.Properties[4].Value;
                    ti.AmountString = item.Properties[3].Value;
                    ticker.AddTradeHistoryItem(ti);
                    newItems.Add(ti);
                }
                ticker.UnlockTrades();
            }

            if(ticker.HasTradeHistorySubscribers)
                ticker.RaiseTradeHistoryChanged(new TradeHistoryChangedEventArgs() { NewItems = newItems });
            return true;
        }

        public override bool Withdraw(AccountInfo account, string currency, string adress, string paymentId, double amount) {
            throw new NotImplementedException();
        }

        protected override ResizeableArray<TradeInfoItem> GetTradesCore(Ticker ticker, DateTime starTime, DateTime endTime) {
            return null;
        }

        protected internal override void ApplyCapturedEvent(Ticker ticker, TickerCaptureDataInfo info) {
            throw new NotImplementedException();
        }

        protected internal override IIncrementalUpdateDataProvider CreateIncrementalUpdateDataProvider() {
            return new ExmoIncUpdateDataProvider();
        }
    }
}
