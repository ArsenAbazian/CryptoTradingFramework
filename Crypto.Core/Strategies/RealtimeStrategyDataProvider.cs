using Crypto.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crypto.Core.Strategies {
    public class RealtimeStrategyDataProvider : IStrategyDataProvider {
        public RealtimeStrategyDataProvider() { }

        bool IStrategyDataProvider.IsFinished { get { return false; } }

        void IStrategyDataProvider.OnTick() { }

        bool IStrategyDataProvider.Connect(StrategyInputInfo info) {
            bool res = true;
            for(int i = 0; i < info.Tickers.Count; i++) {
                TickerInputInfo ti = info.Tickers[i];
                Exchange e = ((IStrategyDataProvider) this).GetExchange(ti.Exchange);
                if(!e.Connect())
                    return false;
                ti.Ticker = e.GetTicker(ti.TickerName);
                res &= e.Connect(ti);
            }
            return res;
        }

        CancellationToken IStrategyDataProvider.Cancellation { get; set; }

        void IStrategyDataProvider.Reset() { }

        DateTime IStrategyDataProvider.CurrentTime { get { return DateTime.UtcNow; } }

        bool IStrategyDataProvider.Disconnect(StrategyInputInfo info) {
            if(info == null)
                return true;
            bool res = true;
            for(int i = 0; i < info.Tickers.Count; i++) {
                TickerInputInfo ti = info.Tickers[i];
                Exchange e = ((IStrategyDataProvider) this).GetExchange(ti.Exchange);
                ti.Ticker = e.GetTicker(ti.TickerName);
                res &= e.Disconnect(ti);
            }
            return res;
        }

        Exchange IStrategyDataProvider.GetExchange(ExchangeType exchange) {
            return Exchange.Get(exchange);
        }

        bool IStrategyDataProvider.InitializeDataFor(StrategyBase s) {
            return true;
        }

        AccountInfo IStrategyDataProvider.GetAccount(Guid accountId) {
            return Exchange.GetAccount(accountId);
        }
    }
}
