using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Crypto.Core;

namespace CryptoMarketClient {
    public partial class MyTradesCollectionControl : XtraUserControl {
        public MyTradesCollectionControl() {
            InitializeComponent();
            ((FormatConditionRuleValue)this.gvTrades.FormatRules[0].Rule).Appearance.ForeColor = Exchange.BidColor;
            ((FormatConditionRuleValue)this.gvTrades.FormatRules[1].Rule).Appearance.ForeColor = Exchange.AskColor;
        }

        Ticker ticker;
        public Ticker Ticker {
            get { return ticker; }
            set {
                if(Ticker == value)
                    return;
                ticker = value;
                OnTickerChanged();
            }
        }
        void OnTickerChanged() {
            if(Ticker == null)
                return;
            this.gcTrades.DataSource = Ticker.AccountTradeHistory;
            UpdateTrades();
        }

        private void bbRefreshMyTrades_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            UpdateTrades();
        }
        public async void UpdateTrades() {
            if(Ticker == null)
                return;
            Task t = Task.Factory.StartNew(() => {
                if(Ticker.UpdateAccountTrades() && this.gcTrades.IsHandleCreated) {
                    BeginInvoke(new MethodInvoker(() => this.gvTrades.RefreshData()));
                }
            });
            await t;
        }
    }
}
