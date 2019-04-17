using Crypto.Core.Indicators;
using Crypto.Core.Strategies.Signal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Strategies.Signal {
    public partial class SignalNotificationDataForm : StrategyDataForm {
        public SignalNotificationDataForm() {
            InitializeComponent();
        }

        protected override void OnStrategyChanged() {
            base.OnStrategyChanged();
            SignalNotificationStrategy s = ((SignalNotificationStrategy)Strategy);
            List<CombinedSignalDataItem> list = new List<CombinedSignalDataItem>();

            if(s.StochIndicator == null) {
                this.combinedSignalDataItemBindingSource.DataSource = list;
                return;
            }

            lock(s.StochIndicator.Result) {
                lock(s.MacdIndicator.Result) {
                    lock(s.RsiIndicator.Result) {
                        for(int i = 0; i < s.StochIndicator.Count; i++) {
                            CombinedSignalDataItem item = new CombinedSignalDataItem();
                            item.Index = i;
                            item.Time = s.StochIndicator.Result[i].Time;
                            item.Rsi = ((IndicatorValue)s.RsiIndicator.Result[i]).Value;
                            item.MacdFast = ((IndicatorValue)s.MacdIndicator.FastEmaIndicator.Result[i]).Value;
                            item.MacdSlow = ((IndicatorValue)s.MacdIndicator.SlowEmaIndicator.Result[i]).Value;
                            item.Macd = ((IndicatorValue)s.MacdIndicator.Result[i]).Value;
                            item.MacdSignal = ((IndicatorValue)s.MacdIndicator.SignalMaIndicator.Result[i]).Value;
                            item.StochK = ((StochasticValue)s.StochIndicator.Result[i]).K;
                            item.StochD = ((StochasticValue)s.StochIndicator.Result[i]).D;
                            list.Add(item);
                        }
                    }
                }
            }
            this.combinedSignalDataItemBindingSource.DataSource = list;
        }

        private void gridControl1_Click(object sender, EventArgs e) {

        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e) {
            if(this.gridView1.FocusedRowHandle != e.RowHandle)
                return;
            e.Appearance.BackColor = Color.FromArgb(0x10, this.gridView1.PaintAppearance.FocusedRow.BackColor);
            e.HighPriority = true;
        }
    }
}
