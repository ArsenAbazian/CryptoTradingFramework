using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WampSharp.V2;
using WampSharp.V2.PubSub;
using WampSharp.V2.Realm;
using System.Reactive.Subjects;
using System.Diagnostics;
using DevExpress.XtraWaitForm;
using CryptoMarketClient.Bittrex;
using DevExpress.XtraEditors;

namespace CryptoMarketClient {
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm {
        

        public MainForm() {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            ModelBase.AllowTradeHistory = this.bcAllowTradeHistory.Checked;
            ModelBase.OrderBookDepth = Convert.ToInt32(this.beOrderBookDepth.EditValue);
        }

        PoloniexTickersForm tickersForm;
        public PoloniexTickersForm PoloniexTickersForm {
            get {
                if(tickersForm == null || tickersForm.IsDisposed) {
                    tickersForm = new PoloniexTickersForm();
                    tickersForm.MdiParent = this;
                }
                return tickersForm;
            }
        }

        BittrexMarketsForm bittrexMarketsForm;
        public BittrexMarketsForm BittrextMarketsForm {
            get {
                if(bittrexMarketsForm == null || bittrexMarketsForm.IsDisposed) {
                    bittrexMarketsForm = new BittrexMarketsForm();
                    bittrexMarketsForm.MdiParent = this;
                }
                return bittrexMarketsForm;
            }
        }
       
        private void bcPoloniex_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.bcPoloniex.Checked) {
                PoloniexModel.Default.IsConnected = true;
                PoloniexTickersForm.Show();
            }
            else {
                PoloniexModel.Default.IsConnected = false;
                PoloniexTickersForm.Hide();
            }
        }

        private void bcBittrex_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.bcBittrex.Checked) {
                BittrexModel.Default.IsConnected = true;
                BittrextMarketsForm.Show();
            }
            else {
                BittrexModel.Default.IsConnected = false;
                BittrextMarketsForm.Hide();
            }
        }

        private void btClassicArbitrage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(!BittrexModel.Default.IsConnected || !PoloniexModel.Default.IsConnected) {
                XtraMessageBox.Show("Please connect at least two markets to make arbitrage possible...");
                return;
            }
            TickerArbitrageForm form = new TickerArbitrageForm();
            form.MdiParent = this;
            form.Show();
        }

        private void beOrderBookDepth_EditValueChanged(object sender, EventArgs e) {
            ModelBase.OrderBookDepth = Convert.ToInt32(this.beOrderBookDepth.EditValue);
        }

        private void bcAllowTradeHistory_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ModelBase.AllowTradeHistory = this.bcAllowTradeHistory.Checked;
        }

        private void btShowApiKeys_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            using(EnterApiKeyForm form = new EnterApiKeyForm()) {
                form.ShowDialog();
            }
        }
    }
}
