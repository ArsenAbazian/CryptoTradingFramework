using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocket4Net;

namespace CryptoMarketClient.Common {
    public class SocketConnectionInfo {
        public Ticker Ticker { get; set; }
        public string Adress { get; set; }
        public WebSocket Socket { get; set; }
        public SocketConnectionState State { get; set; }
        public string LastError { get; set; }
        public CandleStickIntervalInfo KlineInfo { get; set; }
        public void Open() {
            Socket.Open();
        }
        public void Close() {
            Socket.Close();
        }
        public void Dispose() {
            Socket.Dispose();
        }
    }
}
