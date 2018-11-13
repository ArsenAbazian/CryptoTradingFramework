using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CryptoMarketClient.Strategies.Stupid {
    public partial class SimpleBuyLowSellHighConfigControl : StrategySpecificConfigurationControlBase {
        public SimpleBuyLowSellHighConfigControl() {
            InitializeComponent();
        }

        protected override void OnStrategyChanged() {
            this.tickerNameInfoBindingSource.DataSource = Exchange.GetTickersNameInfo();
            this.simpleBuyLowSellHighStrategyBindingSource.DataSource = Strategy;
            string faultExchanges = string.Empty;
            foreach(Exchange e in Exchange.Registered) {
                if(e.Tickers.Count == 0) {
                    if(!string.IsNullOrEmpty(faultExchanges))
                        faultExchanges += ", ";
                    faultExchanges += e.Name;
                }
            }
            if(!string.IsNullOrEmpty(faultExchanges))
                XtraMessageBox.Show("Warning: failed load tickers for the following exchanges: " + faultExchanges);
        }

        private void TickerInfoTextEdit_EditValueChanged(object sender, EventArgs e) {

        }
    }
}
