using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Crypto.Core.Strategies;
using Crypto.Core.Strategies.Custom;
using CryptoMarketClient.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CryptoService.Core {
    public class Program {
        public static void Main(string[] args) {
            LogManager.Default.Log(LogType.Success, null, "starting.", "");
            Debug.WriteLine("starting...");

            StrategiesManager manager = new StrategiesManager();

            TaSimpleStrategy strategy = new TaSimpleStrategy();
            strategy.TickerInfo = new Crypto.Core.Common.TickerNameInfo() { BaseCurrency = "USDT", MarketCurrency = "BTC", Exchange = CryptoMarketClient.ExchangeType.Poloniex };
            strategy.DemoMode = true;
            strategy.MaxAllowedDeposit = 1000;

            manager.Strategies.Add(strategy);
            if(!manager.Initialize(new RealtimeStrategyDataProvider())) {
                LogManager.Default.Log(LogType.Error, manager, "strategies manager did not initialized.", "");
                Debug.WriteLine("strategies manager did not initialized.");
                return;
            }
            LogManager.Default.Log(LogType.Success, manager, "start strategies manager.", "");
            if(!manager.Start()) {
                LogManager.Default.Log(LogType.Error, manager, "strategies manager did not start.", "");
                Debug.WriteLine("strategies manager did not start.");
                return;
            }
            LogManager.Default.Log(LogType.Success, manager, "strategies manager started.", "");
            Console.WriteLine("strategies manager successuflly started.");
            while(true) {
                lock(manager) {
                    if(!manager.Running)
                        break;
                }
                lock(strategy) {
                    Debug.WriteLine(DateTime.Now.ToLongTimeString() + " deposit = " + strategy.MaxActualDeposit + " earned = " + strategy.Earned);
                }
            }
            LogManager.Default.Log(LogType.Success, manager, "strategies manager stopped.", "");
            Console.WriteLine("strategies manager successuflly stopped.");
        }
    }
}
