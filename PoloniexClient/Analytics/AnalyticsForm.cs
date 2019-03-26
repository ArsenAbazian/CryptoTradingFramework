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
    public partial class AnalyticsForm : XtraForm {
        public AnalyticsForm() {
            InitializeComponent();
            
        }

        Ticker ticker;
        public Ticker Ticker {
            get { return ticker; }
            set {
                if(ticker == value)
                    return;
                ticker = value;
                OnTickerChanged();
            }
        }
        void OnTickerChanged() {
            
        }
    }
}
