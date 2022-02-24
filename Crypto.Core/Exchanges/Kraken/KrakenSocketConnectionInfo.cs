using Crypto.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Kraken {
    public class KrakenSocketConnectionInfo : SocketConnectionInfo {
        public KrakenSocketConnectionInfo(string name, SocketSubscribeType type) : base(name, type) { }
        public KrakenSocketConnectionInfo(Exchange e, Ticker t, string address, SocketType st, SocketSubscribeType sst) : base(e, t, address, st, sst) { }
        protected override string SerializeCommand(WebSocketSubscribeInfo info) {
            return info.Command.command;
        }
    }
}
