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
    public partial class OrderBookVolumeHistoryForm : XtraForm {
        public OrderBookVolumeHistoryForm() {
            InitializeComponent();
            this.repositoryItemProgressBar1.DrawBackground = DevExpress.XtraEditors.Repository.RepositoryItemBaseProgressBar.DrawBackgroundType.True;
        }
        TickerBase ticker;
        public TickerBase Ticker {
            get {
                return ticker;
            }
            set {
                ticker = value;
                OnTickerChanged();
            }
        }
        void OnTickerChanged() {
            orderBookVolumeHistoryItemBindingSource.DataSource = ticker.OrderBook.VolumeHistory;
        }

        //protected override void OnTimerUpdate(object sender, EventArgs e) {
        //    base.OnTimerUpdate(sender, e);
        //    //this.tradeGridControl.RefreshDataSource();
        //}
    }
}
