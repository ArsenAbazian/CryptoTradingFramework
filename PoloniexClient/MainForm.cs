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
using CryptoMarketClient.Common;
using CryptoMarketClient.HitBtc;

namespace CryptoMarketClient {
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm {
        

        public MainForm() {
            InitializeComponent();
        }

        protected override bool SupportAdvancedTitlePainting => false;

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            ModelBase.AllowTradeHistory = this.bcAllowTradeHistory.Checked;
            ModelBase.OrderBookDepth = Convert.ToInt32(this.beOrderBookDepth.EditValue);
            TelegramBot.Default.SendNotification("hello!");

            BittrexModel.Default.IsConnected = true;
            BittrextMarketsForm.Show();
            PoloniexModel.Default.IsConnected = true;
            PoloniexTickersForm.Show();
            //HitBtcModel.Default.IsConnected = true;
            //HitBtcMarketsForm.Show();

            //if(BittrexModel.Default.IsConnected && 
            //    PoloniexModel.Default.IsConnected/* && 
            //    HitBtcModel.Default.IsConnected*/) {
            //    ArbitrageForm.Show();
            //    ArbitrageForm.Activate();
            //}
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

        BittrexTickerCollectionForm bittrexMarketsForm;
        public BittrexTickerCollectionForm BittrextMarketsForm {
            get {
                if(bittrexMarketsForm == null || bittrexMarketsForm.IsDisposed) {
                    bittrexMarketsForm = new BittrexTickerCollectionForm();
                    bittrexMarketsForm.MdiParent = this;
                }
                return bittrexMarketsForm;
            }
        }

        HitBtcTickerCollectionForm hitBtcMarketsForm;
        public HitBtcTickerCollectionForm HitBtcMarketsForm {
            get {
                if(hitBtcMarketsForm == null || hitBtcMarketsForm.IsDisposed) {
                    hitBtcMarketsForm = new HitBtcTickerCollectionForm();
                    hitBtcMarketsForm.MdiParent = this;
                }
                return hitBtcMarketsForm;
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

        TickerArbitrageForm arbitrageForm;
        public TickerArbitrageForm ArbitrageForm {
            get {
                if(arbitrageForm == null || arbitrageForm.IsDisposed) {
                    arbitrageForm = new TickerArbitrageForm();
                    arbitrageForm.MdiParent = this;
                }
                return arbitrageForm;
            }
        }

        private void btClassicArbitrage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(!BittrexModel.Default.IsConnected || !PoloniexModel.Default.IsConnected) {
                XtraMessageBox.Show("Please connect at least two markets to make arbitrage possible...");
                return;
            }
            ArbitrageForm.Show();
            ArbitrageForm.Activate();
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

        private void bbShowYourTotalDeposit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            DepositsTotal form = new DepositsTotal();
            form.Show();
        }

        private void bbShowHistory_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ArbitrageHistoryForm form = new ArbitrageHistoryForm();
            form.MdiParent = this;
            form.Show();
        }

        private void bbSaveAllHistory_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

        }

        private void beArbitrageDepth_EditValueChanged(object sender, EventArgs e) {
            TickerCollection.Depth = Convert.ToInt32(this.beArbitrageDepth.EditValue);
        }

        private void bcHitBtc_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.bcBittrex.Checked) {
                HitBtcModel.Default.IsConnected = true;
                HitBtcMarketsForm.Show();
            }
            else {
                HitBtcModel.Default.IsConnected = false;
                HitBtcMarketsForm.Hide();
            }
        }

        StaticArbitrageForm staticArbitrageForm;
        public StaticArbitrageForm StaticArbitrageForm {
            get {
                if(staticArbitrageForm == null || staticArbitrageForm.IsDisposed) {
                    staticArbitrageForm = new StaticArbitrageForm();
                    staticArbitrageForm.MdiParent = this;
                }
                return staticArbitrageForm;
            }
        }

        private void bbShowStaticArbitrage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            StaticArbitrageForm.Show();
            StaticArbitrageForm.Activate();
        }
    }
}
