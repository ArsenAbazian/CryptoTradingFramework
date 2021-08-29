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
using Crypto.Core.Strategies.Signal;
using Crypto.Core;
using Crypto.Core.Strategies;

namespace CryptoMarketClient.Strategies.Signal {
    public partial class MacdTrendStrategyConfigControl : StrategySpecificConfigurationControlBase {
        public MacdTrendStrategyConfigControl() {
            InitializeComponent();
            InitializeDefaults();
        }
        void InitializeDefaults() {
            var intervals = GetAllowedCandleSticksIntervals(Strategy as TickerStrategyBase);
            comboBoxEdit1.EditValue = intervals.FirstOrDefault();
        }
        protected override void OnStrategyChanged() {
            //TODO check that ticker correctly initiallized on editing.
            List<TickerNameInfo> tickerNameList = Exchange.GetTickersNameInfo();
            if(tickerNameList == null || tickerNameList.Count == 0) {
                XtraMessageBox.Show("Tickers list not initialized. Please close editing form (do not press OK button) and then restart application.");
                return;
            }
            this.tickerNameInfoBindingSource.DataSource = tickerNameList;
            
            TickerStrategyBase ts = (TickerStrategyBase) Strategy;
            
            if(ts.TickerInfo != null)
                ts.TickerInfo = tickerNameList.FirstOrDefault(t => t.Ticker == ts.TickerInfo.Ticker);

            this.signalNotificationStrategyBindingSource.DataSource = Strategy;
            if(ts.TickerInfo != null) {
                this.candleStickIntervalInfoBindingSource.DataSource = Exchange.Get(ts.TickerInfo.Exchange).AllowedCandleStickIntervals;
                this.comboBoxEdit1.EditValue = Exchange.Get(ts.TickerInfo.Exchange).AllowedCandleStickIntervals.FirstOrDefault(i => (int) (i.Interval.TotalMinutes) == ts.CandleStickIntervalMin);
            }
            CheckUnreachableExchanges();
        }

        void TickerInfoTextEdit_EditValueChanged(object sender, EventArgs e) {
            TickerNameInfo info = this.TickerInfoTextEdit.EditValue as TickerNameInfo;
            ((TickerStrategyBase)Strategy).TickerInfo = info;
            if(info == null) {
                this.candleStickIntervalInfoBindingSource.DataSource = typeof(CandleStickIntervalInfo);
                return;
            }
            Exchange ee = Exchange.Get(info.Exchange);
            this.candleStickIntervalInfoBindingSource.DataSource = ee.AllowedCandleStickIntervals;
        }

        void comboBoxEdit1_EditValueChanged(object sender, EventArgs e) {
            MacdTrendStrategy s = (MacdTrendStrategy)Strategy;
            CandleStickIntervalInfo info = this.comboBoxEdit1.EditValue as CandleStickIntervalInfo;
            if(info != null)
                s.CandleStickIntervalMin = (int)info.Interval.TotalMinutes;
            else
                s.CandleStickIntervalMin = (int)TimeSpan.FromHours(4).TotalMinutes;
        }
    }
}
