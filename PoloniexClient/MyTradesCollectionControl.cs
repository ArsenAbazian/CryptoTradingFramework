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

namespace CryptoMarketClient {
    public partial class MyTradesCollectionControl : XtraUserControl {
        public MyTradesCollectionControl() {
            InitializeComponent();
        }

        TickerBase ticker;
        public TickerBase Ticker {
            get { return ticker; }
            set {
                if(Ticker == value)
                    return;
                ticker = value;
                OnTickerChanged();
            }
        }
        void OnTickerChanged() {
            this.gcTrades.DataSource = Ticker.MyTradeHistory;
            UpdateTrades();
        }

        private void bbRefreshMyTrades_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            UpdateTrades();
        }
        public void UpdateTrades() {
            if(Ticker == null)
                return;
            if(Ticker.UpdateMyTrades())
                this.gvTrades.RefreshData();
        }
    }
}
