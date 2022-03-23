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
            throw new NotImplementedException();
        }

        public override bool GetBalance(AccountInfo info, string currency) {
            throw new NotImplementedException();
        }

        public override bool GetDeposit(AccountInfo account, CurrencyInfo currency) {
            throw new NotImplementedException();
        }

        public override bool GetDeposites(AccountInfo account) {
            throw new NotImplementedException();
        }

        public override bool GetTickersInfo() {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override bool UpdateTrades(Ticker tickerBase) {
            throw new NotImplementedException();
        }

        public override bool Withdraw(AccountInfo account, string currency, string adress, string paymentId, double amount) {
            throw new NotImplementedException();
        }

        protected override bool GetTradesCore(ResizeableArray<TradeInfoItem> list, Ticker ticker, DateTime start, DateTime end) {
            throw new NotImplementedException();
        }

        protected internal override void ApplyCapturedEvent(Ticker ticker, TickerCaptureDataInfo info) {
            throw new NotImplementedException();
        }

        protected internal override IIncrementalUpdateDataProvider CreateIncrementalUpdateDataProvider() {
            return new ExmoIncUpdateDataProvider();
        }
    }
}
