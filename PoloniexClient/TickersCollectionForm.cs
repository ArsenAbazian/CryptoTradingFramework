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
    public partial class TickersCollectionForm : Form {
        public TickersCollectionForm() {
            InitializeComponent();
        }

        TickerCollection coll;
        public TickerCollection Collection {
            get { return coll; }
            set {
                if(Collection == value)
                    return;
                coll = value;
                OnCollectonChanged();
            }
        }
        void OnCollectonChanged() {
            
        }
    }

    public class TickerCollectionItem {
        public bool Selected { get; set; }
        public string HostName { get { return Ticker.HostName; } }
        public TickerBase Ticker { get; set; }
    }
}
