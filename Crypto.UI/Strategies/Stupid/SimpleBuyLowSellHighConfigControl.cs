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
using Crypto.Core.Common;
using Crypto.Core;
using Crypto.Core.Strategies;

namespace CryptoMarketClient.Strategies.Stupid {
    public partial class SimpleBuyLowSellHighConfigControl : StrategySpecificConfigurationControlBase {
        public SimpleBuyLowSellHighConfigControl() {
            InitializeComponent();
        }

        protected override void OnStrategyChanged() {
            //TODO check that ticker correctly initiallized on editing.
            List<TickerNameInfo> tickerNameList = Exchange.GetTickersNameInfo();
            if(tickerNameList == null || tickerNameList.Count == 0) {
                XtraMessageBox.Show("Tickers list not initialized. Please close editing form (do not press OK button) and then restart application.");
                return;
            }
            this.tickerNameInfoBindingSource.DataSource = tickerNameList;
            TickerStrategyBase ts = (TickerStrategyBase)Strategy;
            if(ts.TickerInfo != null)
                ts.TickerInfo = tickerNameList.FirstOrDefault(t => t.Ticker == ts.TickerInfo.Ticker);
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
