using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Bittrex {
    public partial class BittrexCurrenciesForm : Form {
        public BittrexCurrenciesForm() {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            BittrexModel.Default.GetCurrenciesInfo();
            this.gridControl1.DataSource = BittrexModel.Default.Currencies;
        }
    }
}
