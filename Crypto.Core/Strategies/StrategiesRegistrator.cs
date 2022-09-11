using Crypto.Core.Strategies.Arbitrages.AltBtcUsdt;
using Crypto.Core.Strategies.Arbitrages.Statistical;
using Crypto.Core.Strategies.Custom;
using Crypto.Core.Strategies.Signal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Crypto.Core.Strategies {
    public static class StrategiesRegistrator {
        static List<StrategyRegistrationInfo> registered;
        public static List<StrategyRegistrationInfo> RegisteredStrategies {
            get {
                if(registered == null)
                    registered = RegisterStrategies();
                return registered;
            }
        }
        static List<StrategyRegistrationInfo> RegisterStrategies() {
            List<StrategyRegistrationInfo> list = new List<StrategyRegistrationInfo>();
            //list.Add(new StrategyRegistrationInfo() { Type = typeof(SimpleBuyLowSellHighStrategy), Group = StrategyGroup.Simple, Name = "Simple Buy Low Sell High", Description = "No Rocket Scinece, Just buy low and sell high. Thats it." });
            list.Add(new StrategyRegistrationInfo() { Type = typeof(StatisticalArbitrageStrategy), Group = StrategyGroup.Arbitrage, Name = "Statistical Arbitrage", Description = "Simple Statistical Arbitrage Strategy for Trading Pairs (usually base currency and futures)." });
            //list.Add(new StrategyRegistrationInfo() { Type = typeof(SignalNotificationStrategy), Group = StrategyGroup.Signal, Name = "Signal Notification", Description = "This strategy just send notification when corresponding param's values found" });
            list.Add(new StrategyRegistrationInfo() { Type = typeof(TripleRsiIndicatorStrategy), Group = StrategyGroup.Signal, Name = "Triple Rsi Indicator", Description = "This strategy based on triple rsi indicators values." });
            list.Add(new StrategyRegistrationInfo() { Type = typeof(MacdTrendStrategy), Group = StrategyGroup.Signal, Name = "Macd Trend", Description = "This strategy based on Macd trend." });
            //list.Add(new StrategyRegistrationInfo() { Type = typeof(CustomTickerStrategy), Group = StrategyGroup.Custom, Name = "Custom Strategy", Description = "Your own fully customizable strategy" });
            //list.Add(new StrategyRegistrationInfo() { Type = typeof(MarketMakingStrategy), Group = StrategyGroup.Custom, Name = "Stupid Market Making Strategy", Description = "No Comments" });
            list.Add(new StrategyRegistrationInfo() { Type = typeof(TriplePairStrategy), Group = StrategyGroup.Arbitrage, Name = "Triple Pair Strategy", Description = "Strategy based on tripe pair arbitrage" });
            list.Add(new StrategyRegistrationInfo() { Type = typeof(TaSimpleStrategy), Group = StrategyGroup.TecnicalAnylysis, Name = "Ta Simple Strategy", Description = "Strategy based on simple technical analysis" });
            list.Add(new StrategyRegistrationInfo() { Type = typeof(HipeBasedStrategy), Group = StrategyGroup.TecnicalAnylysis, Name = "Hipe Based Strategy", Description = "Strategy based on classic arbitrage detection, which happens on hipe" });

            Assembly entry = Assembly.GetEntryAssembly();
            AssemblyName[] name = entry.GetReferencedAssemblies();
            AssemblyName cryptoStrategies = name.FirstOrDefault(nm => nm.Name == "Crypto.Strategies");
            if(cryptoStrategies != null) {
                List<Type> infos = Assembly.Load(cryptoStrategies).GetTypes().Where(t => typeof(StrategyRegistrationInfo).IsAssignableFrom(t)).ToList();
                foreach(Type infoType in infos) {
                    StrategyRegistrationInfo info = (StrategyRegistrationInfo)infoType.GetConstructor(new Type[] { }).Invoke(new object[] { });
                    list.Add(info);
                }
            }

            return list;
        }
    }

    

    public static class StrategyGroup {
        public static string Simple = "Simple",
        Arbitrage = "Arbitrage",
        MarketMaking = "Market Making",
        Grid = "Grid",
        Signal = "Signal Notificators",
        Custom = "Custom",
        TecnicalAnylysis = "Technical Analysis",
        UserDefined = "User Defined";
    }
}
