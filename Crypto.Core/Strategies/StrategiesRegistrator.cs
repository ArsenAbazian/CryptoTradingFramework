using Crypto.Core.Common.Arbitrages;
using CryptoMarketClient.Strategies.Stupid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            list.Add(new StrategyRegistrationInfo() { Type = typeof(SimpleBuyLowSellHighStrategy), Group = StrategyGroup.Simple, Name = "Simple Buy Low Sell High", Description = "No Rocket Scinece, Just buy low and sell high. Thats it." });
            list.Add(new StrategyRegistrationInfo() { Type = typeof(StatisticalArbitrageStrategy), Group = StrategyGroup.Arbitrage, Name = "Statistical Arbitrage", Description = "Simple Statistical Arbitrage Strategy for Trading Pairs (usually base currency and futures)." });
            return list;
        }
    }

    public class StrategyRegistrationInfo {
        public Type Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
        public StrategyBase Create() {
            ConstructorInfo info = Type.GetConstructor(new Type[] { });
            return (StrategyBase)info.Invoke(new object[] { });
        }
    }

    public static class StrategyGroup {
        public static string Simple = "Simple",
        Arbitrage = "Arbitrage",
        MarketMaking = "Market Making",
        Grid = "Grid";
    }
}
