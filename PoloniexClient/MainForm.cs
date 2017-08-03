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

namespace CryptoMarketClient {
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm {
        

        public MainForm() {
            InitializeComponent();
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

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            PoloniexModel.Default.IsConnected = true;
            PoloniexTickersForm.Show();
        }

        private void btConnectBitrix_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            BittrexModel.Default.IsConnected = true;
            BittrextMarketsForm.Show();
        }

        private void biDisconnectPoloniex_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            PoloniexModel.Default.IsConnected = false;
        }

        private void biDisconnectBittrex_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            BittrexModel.Default.IsConnected = false;
        }
    }
}
