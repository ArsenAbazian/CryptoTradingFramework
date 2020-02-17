using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Crypto.Core.Strategies;
using Crypto.Core.Strategies.Custom;
using CryptoMarketClient;
using CryptoMarketClient.Common;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CryptoService.Core {
    public class Worker : BackgroundService {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger) {
            _logger = logger;
        }

        protected StrategiesManager Manager { get; set; }
        public override Task StartAsync(CancellationToken cancellationToken) {
            Task task = base.StartAsync(cancellationToken);
            return task.ContinueWith(t => {
                LogManager.Default.Log(LogType.Success, null, "starting.", "");
                _logger.LogInformation("starting...");

                Manager = new StrategiesManager();
                TaSimpleStrategy strategy = new TaSimpleStrategy();
                strategy.TickerInfo = new Crypto.Core.Common.TickerNameInfo() { BaseCurrency = "USDT", MarketCurrency = "BTC", Exchange = CryptoMarketClient.ExchangeType.Poloniex };
                strategy.DemoMode = true;
                strategy.MaxAllowedDeposit = 1000;

                Manager.Strategies.Add(strategy);
                if(!Manager.Initialize(new RealtimeStrategyDataProvider())) {
                    LogManager.Default.Log(LogType.Error, Manager, "strategies manager did not initialized.", "");
                    _logger.LogError("strategies manager did not initialized.");
                    return;
                }
                LogManager.Default.Log(LogType.Success, Manager, "start strategies manager.", "");
                if(!Manager.Start()) {
                    LogManager.Default.Log(LogType.Error, Manager, "strategies manager did not start.", "");
                    _logger.LogError("strategies manager did not start.");
                    return;
                }
                LogManager.Default.Log(LogType.Success, Manager, "strategies manager started.", "");
                _logger.LogInformation("strategies manager successuflly started.");
            });
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            
            while(!stoppingToken.IsCancellationRequested) {
                if(Manager != null && !Manager.Running) {
                    _logger.LogInformation("Start Strategies Maanger at {time}", DateTimeOffset.Now);
                    if(!Manager.Start()) {
                        _logger.LogError("Strategies Manager did not start");
                        await Task.FromCanceled(stoppingToken);
                    }
                    else {
                        _logger.LogInformation("Strategies Maanger started at {time}", DateTimeOffset.Now);
                    }
                }
                lock(Manager.Strategies[0]) {
                    TaSimpleStrategy s = (TaSimpleStrategy)Manager.Strategies[0];
                    _logger.LogInformation(DateTime.Now.ToLongTimeString() + " deposit = " + s.MaxActualDeposit + " earned = " + s.Earned);
                }
                await Task.CompletedTask;// Task.Delay(1000, stoppingToken);
            }
        }
    }
}
