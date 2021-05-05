using Crypto.Core.Exchanges.Base;
using Crypto.Core.Helpers;
using CryptoMarketClient;
using CryptoMarketClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tinkoff.Trading.OpenApi.Models;
using Tinkoff.Trading.OpenApi.Network;

namespace Crypto.Core.Exchanges.Tinkoff {
    public class TinkoffExchange : Exchange {
        static TinkoffExchange defaultExchange;
        public static TinkoffExchange Default {
            get {
                if(defaultExchange == null)
                    defaultExchange = (TinkoffExchange)Exchange.FromFile(ExchangeType.Tinkoff, typeof(TinkoffExchange));
                return defaultExchange;
            }
        }

        public override bool AllowCandleStickIncrementalUpdate => false;

        public override ExchangeType Type => ExchangeType.Tinkoff;

        public override string BaseWebSocketAdress => "wss://api-invest.tinkoff.ru/openapi/md/v1/md-openapi/ws";

        protected override bool ShouldAddKlineListener => throw new NotImplementedException();

        public override TradingResult Buy(AccountInfo account, Ticker ticker, double rate, double amount) {
            throw new NotImplementedException();
        }

        public override bool Cancel(AccountInfo account, string orderId) {
            throw new NotImplementedException();
        }

        public override string CreateDeposit(AccountInfo account, string currency) {
            throw new NotImplementedException();
        }

        public override List<CandleStickIntervalInfo> GetAllowedCandleStickIntervals() {
            throw new NotImplementedException();
        }

        public override bool GetBalance(AccountInfo info, string currency) {
            throw new NotImplementedException();
        }

        public override bool GetDeposites(AccountInfo account) {
            throw new NotImplementedException();
        }

        public override bool GetTickersInfo() {
            Context cc = GetContext(DefaultAccount);
            Tickers.Clear();
            cc.MarketStocksAsync().ContinueWith(t => {
                MarketInstrumentList list = t.Result;
                foreach(MarketInstrument i in list.Instruments) {
                    Tickers.Add(new Tinkoff.TinknoffInvestTicker(this, i));
                }
                IsInitialized = true;
            });
            return true;
        }

        protected override bool GetTradesCore(ResizeableArray<TradeInfoItem> list, Ticker ticker, DateTime start, DateTime end) {
            throw new NotImplementedException();
        }

        public override bool ObtainExchangeSettings() {
            RequestRate = new List<RateLimit>();
            RequestRate.Add(new RateLimit() { Limit = 119, Interval = TimeSpan.TicksPerMinute });
            return true;
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
            return false; // TODO return true
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
            throw new NotImplementedException();
        }

        public override bool UpdateOrderBook(Ticker tickerBase) {
            return UpdateOrderBook(tickerBase, OrderBookDepth);
        }

        public override bool UpdateOrderBook(Ticker t, int depth) {
            CheckRequestRateLimits();
            Context c = GetContext(DefaultAccount);
            t.OrderBook.IsDirty = true;
            c.MarketOrderbookAsync(((TinknoffInvestTicker)t).Instrument.Figi, depth).ContinueWith(tt => {
                Orderbook o = tt.Result;
                t.Last = (double)o.LastPrice;
                t.OrderBook.BeginUpdate();
                try {
                    t.OrderBook.Clear();
                    foreach(var bid in o.Bids) {
                        t.OrderBook.Bids.Add(new OrderBookEntry() { Amount = bid.Quantity, Value = (double)bid.Price });
                    }
                    foreach(var ask in o.Asks) {
                        t.OrderBook.Asks.Add(new OrderBookEntry() { Amount = ask.Quantity, Value = (double)ask.Price });
                    }
                }
                finally {
                    t.OrderBook.IsDirty = false;
                    t.OrderBook.EndUpdate();
                    t.RaiseChanged();
                }
            });
            return true;
        }

        public override void UpdateOrderBookAsync(Ticker tickerBase, int depth, Action<OperationResultEventArgs> onOrderBookUpdated) {
            throw new NotImplementedException();
        }

        public override bool UpdateTicker(Ticker tickerBase) {
            return true;
        }

        protected Context GetContext(AccountInfo info) {
            if(info == null ||  string.IsNullOrEmpty(info.ApiKey))
                return null;
            try {
                string token = info.ApiKey;
                Connection c = ConnectionFactory.GetConnection(token);
                return c.Context;
            }
            catch(Exception) {
                return null;
            }
        }

        public override bool UpdateTickersInfo() {
            if(!IsInitialized)
                return true;
            Context cc = GetContext(DefaultAccount);
            cc.MarketStocksAsync().ContinueWith(t => {
                MarketInstrumentList list = t.Result;
                foreach(MarketInstrument i in list.Instruments) {
                    TinknoffInvestTicker ticker = (TinknoffInvestTicker)Tickers.FirstOrDefault(tt => tt.Name == i.Name);
                    if(ticker != null) {
                        ticker.Instrument = i;
                        continue;
                    }
                    Tickers.Add(new Tinkoff.TinknoffInvestTicker(this, i));
                }
            });
            return true;
        }

        public override bool UpdateTrades(Ticker tickerBase) {
            throw new NotImplementedException();
        }

        public override bool Withdraw(AccountInfo account, string currency, string adress, string paymentId, double amount) {
            throw new NotImplementedException();
        }

        protected internal override void ApplyCapturedEvent(Ticker ticker, TickerCaptureDataInfo info) {
            throw new NotImplementedException();
        }

        protected internal override IIncrementalUpdateDataProvider CreateIncrementalUpdateDataProvider() {
            return null;
        }
    }
}
