using CryptoMarketClient.Common;
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
    public partial class StaticArbitrageHistoryForm : Form {
        public StaticArbitrageHistoryForm() {
            InitializeComponent();
        }

        private void bbShowHistory_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

        }
        StaticArbitrageInfo info;
        public StaticArbitrageInfo Info {
            get {
                return info;
            }
            set {
                info = value;
                this.gridControl1.DataSource = info.History;
                this.Text = info.Exchange + " - " + info.AltCoin + " - " + info.BaseCoin;
            }
        }
    }
}
