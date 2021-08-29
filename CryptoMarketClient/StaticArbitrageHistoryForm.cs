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
using Crypto.Core.Common;

namespace CryptoMarketClient {
    public partial class StaticArbitrageHistoryForm : XtraForm {
        public StaticArbitrageHistoryForm() {
            InitializeComponent();
        }

        TriplePairArbitrageInfo info;
        public TriplePairArbitrageInfo Info {
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
