using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Crypto.Core;

namespace Crypto.Core.Strategies {
    public interface IStrategyDataProvider {
        bool Connect(StrategyInputInfo info);
        bool Disconnect(StrategyInputInfo info);
        Exchange GetExchange(ExchangeType exchange);
        bool InitializeDataFor(StrategyBase s);
        AccountInfo GetAccount(Guid accountId);
        void OnTick();
        bool IsFinished { get; }
        DateTime CurrentTime { get; }
        CancellationToken Cancellation { get; set; }

        void Reset();
    }
}
