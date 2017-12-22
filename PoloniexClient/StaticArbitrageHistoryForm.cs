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
    public partial class StaticArbitrageHistoryForm : XtraForm {
        public StaticArbitrageHistoryForm() {
            InitializeComponent();
        }

        StaticArbitrageInfo info;
        public StaticArbitrageInfo Info {
            get {
                return info;
            }
            set {
                info = value;
                this.gridControl1.DataSource = info.History;
                Text = info.Exchange + " - " + info.AltCoin + " - " + info.BaseCoin;
            }
        }
    }
}
