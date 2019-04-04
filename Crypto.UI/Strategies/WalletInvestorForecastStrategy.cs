using Crypto.Core.Strategies;
using Crypto.Core.Strategies.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.UI.Strategies {
    public class WalletInvestorForecastStrategy : StrategyBase {
        static WalletInvestorForecastStrategy() {
            StrategiesRegistrator.RegisteredStrategies.Add(
                new StrategyRegistrationInfo() {
                    Type = typeof(WalletInvestorForecastStrategy),
                    Name = "WiStrategy",
                    Description = "Forecast strategy base on walletinvestor.com"
                }
                );
        }

        public override string StateText => throw new NotImplementedException();
        public override string TypeName => "WIStrategy";
        public override bool SupportSimulation => false;

        public bool AllowTrading { get; set; }
        public bool AllowBinance { get; set; }
        public bool AllowBittrex { get; set; }
        public int CheckIntervalMin { get; set; }

        public override bool InitializeCore() {
            return true;
        }

        public override void OnEndDeserialize() {

        }

        protected override void OnTickCore() {

        }
    }
}
