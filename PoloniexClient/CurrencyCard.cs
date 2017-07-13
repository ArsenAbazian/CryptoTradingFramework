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

namespace PoloniexClient {
    public partial class CurrencyCard : XtraUserControl {
        public CurrencyCard() {
            InitializeComponent();
        }

        PoloniexTicker ticker;
        public PoloniexTicker Ticker {
            get { return ticker; }
            set {
                if(Ticker == value)
                    return;
                PoloniexTicker prev = Ticker;
                ticker = value;
                OnTickerChanged(prev);
            }
        }
        void OnTickerChanged(PoloniexTicker prev) {
            if(prev != null)
                prev.Changed -= Ticker_Changed;
            if(Ticker != null)
                Ticker.Changed += Ticker_Changed;
            if(Ticker == null) {
                this.gridControl1.DataSource = null;
                return;
            }
            List<PoloniexTicker> list = new List<PoloniexTicker>();
            list.Add(Ticker);
            this.gridControl1.DataSource = list;
        }

        private void Ticker_Changed(object sender, EventArgs e) {
            this.gridControl1.RefreshDataSource();
        }
    }
}
