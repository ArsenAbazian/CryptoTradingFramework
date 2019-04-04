using Crypto.Core.Strategies;
using CryptoMarketClient.Forms.Instruments;
using CryptoMarketClient.Strategies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crypto.UI.Strategies.Custom {
    public partial class WalletInvestorDataForm : StrategyDataForm {
        public WalletInvestorDataForm() {
            InitializeComponent();
        }
        protected override void OnStrategyChanged() {
            base.OnStrategyChanged();
            this.walletInvestorItemsControl1.Strategy = (WalletInvestorForecastStrategy)Strategy;
        }
    }
}
