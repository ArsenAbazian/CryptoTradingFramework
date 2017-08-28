using CryptoMarketClient.Common;
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
    public partial class ArbitrageHistoryForm : XtraForm {
        public ArbitrageHistoryForm() {
            InitializeComponent();
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            this.arbitrageStatisticsItemBindingSource.DataSource = ArbitrageHistoryHelper.Default.History;
        }
    }
}
