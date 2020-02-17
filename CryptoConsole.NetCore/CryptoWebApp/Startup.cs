using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crypto.Core.Strategies;
using Crypto.Core.Strategies.Custom;
using CryptoMarketClient;
using CryptoMarketClient.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CryptoWebApp {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
            StartStrategiesManager();
        }

        public static StrategiesManager Manager { get; set; }
        public void StartStrategiesManager() {
            LogManager.Default.Log(LogType.Success, null, "starting.", "");
            Console.WriteLine("starting...");

            StrategiesManager manager = new StrategiesManager();

            if(!PoloniexExchange.Default.Connect()) {
                LogManager.Default.Log(LogType.Error, null, "Poloniex exchange not connected.", "");
                Console.WriteLine("Poloniex exchange not connected.");
                return;
            }

            TaSimpleStrategy strategy = new TaSimpleStrategy();
            strategy.StrategyInfo.Tickers.Add(new TickerInputInfo() { 
                Exchange = ExchangeType.Poloniex, 
                TickerName = "USDT_BTC",
                UseOrderBook = true,
            });
            strategy.Enabled = true;
            strategy.DemoMode = true;
            strategy.MaxAllowedDeposit = 1000;

            manager.Strategies.Add(strategy);
            if(!manager.Initialize(new RealtimeStrategyDataProvider())) {
                LogManager.Default.Log(LogType.Error, manager, "strategies manager did not initialized.", "");
                Console.WriteLine("strategies manager did not initialized.");
                return;
            }
            LogManager.Default.Log(LogType.Success, manager, "start strategies manager.", "");
            if(!manager.Start()) {
                LogManager.Default.Log(LogType.Error, manager, "strategies manager did not start.", "");
                Console.WriteLine("strategies manager did not start.");
                return;
            }
            LogManager.Default.Log(LogType.Success, manager, "strategies manager started.", "");
            Console.WriteLine("strategies manager successuflly started.");

            Manager = manager;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
