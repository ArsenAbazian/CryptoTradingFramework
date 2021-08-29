using Crypto.Core.Strategies;
using CryptoMarketClient.Strategies;
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
    public partial class TickerStrategyParametersForm : XtraForm {
        public TickerStrategyParametersForm() {
            InitializeComponent();
        }
        TickerStrategyBase strategy;
        public TickerStrategyBase Strategy {
            get { return strategy; }
            set {
                if(Strategy == value)
                    return;
                strategy = value;
                OnStrategyChanged();
            }
        }
        void OnStrategyChanged() {
            this.propertyGridControl1.SelectedObject = Strategy;
        }

        private void simpleButton2_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
