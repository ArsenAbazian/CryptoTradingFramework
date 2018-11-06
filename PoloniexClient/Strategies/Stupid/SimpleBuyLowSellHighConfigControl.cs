using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Strategies.Stupid {
    public partial class SimpleBuyLowSellHighConfigControl : StrategySpecificConfigurationControlBase {
        public SimpleBuyLowSellHighConfigControl() {
            InitializeComponent();
        }

        protected override void OnStrategyChanged() {
            this.tickerNameInfoBindingSource.DataSource = Exchange.GetTickersNameInfo();
            this.simpleBuyLowSellHighStrategyBindingSource.DataSource = Strategy;
        }

        private void TickerInfoTextEdit_EditValueChanged(object sender, EventArgs e) {

        }
    }
}
