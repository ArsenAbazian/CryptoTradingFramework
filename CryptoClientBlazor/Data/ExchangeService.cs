using CryptoMarketClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace CryptoClientBlazor.Data {
    public class ExchangeService {

        public Task<Ticker[]> GetTickers(Exchange e) {
            return Task.Run(() =>
            {
                e.Connect();
                if (e.IsConnected)
                    e.GetTickersInfo();
                if (e.Tickers != null && e.Tickers.Count > 0)
                    e.StartListenTickersStream();
            }).ContinueWith((t) => e.Tickers.ToArray());
            
            //return Task.FromResult(e.Tickers.ToArray());
        }

        public Task<string[]> GetMarkets(Exchange e)
        {
            if (!e.IsConnected)
                return Task.FromResult(new string[0]);
            return Task.FromResult(e.GetMarkets());
        }
    }
}
