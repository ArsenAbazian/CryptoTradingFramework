using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Strategies {
    public interface IStrategyDataProvider {
        bool ConnectExchange(ExchangeInputInfo info);
        bool ConnectTicker(TickerInputInfo info);
        bool ListenTicker(TickerInputInfo info);
    }
}
