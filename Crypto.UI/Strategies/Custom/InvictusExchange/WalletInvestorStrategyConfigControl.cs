using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptoMarketClient.Strategies;

namespace Crypto.UI.Strategies.Custom {
    public partial class WalletInvestorStrategyConfigControl : StrategySpecificConfigurationControlBase {
        public WalletInvestorStrategyConfigControl() {
            InitializeComponent();
        }
        protected override void OnStrategyChanged() {
            base.OnStrategyChanged();
            this.walletInvestorForecastStrategyBindingSource.DataSource = Strategy;
        }
    }
}
