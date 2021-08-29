using Crypto.Core.Common;
using DevExpress.Data;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public partial class ArbitrageHistoryForm : TimerUpdateForm {
        public ArbitrageHistoryForm() {
            InitializeComponent();
        }
        protected override int UpdateInervalMs => 3000;
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            this.gridControl1.DataSource = new RealTimeSource() { DataSource = ArbitrageHistoryHelper.Default.History };
        }
        protected override void OnTimerUpdate(object sender, EventArgs e) {
            base.OnTimerUpdate(sender, e);
            this.gridControl1.RefreshDataSource();
        }
    }
}
