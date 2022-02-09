using Crypto.Core;
using Crypto.UI.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crypto.UI.Forms {
    public partial class ExchangesForm : XtraForm {
        public ExchangesForm() {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            InitializeExchanges();
        }

        protected virtual void InitializeExchanges() {
            this.cbExchanges.BeginUpdate();
            try {
                this.cbExchanges.Items.Clear();
                Exchange.Registered.ForEach(e => this.cbExchanges.Items.Add(e));
            }
            finally {
                this.cbExchanges.EndUpdate();
            }
            UpdateMarkets();
        }

        List<Exchange> selExchanges = new List<Exchange>();
        public List<Exchange> SelectedExchanges {
            get { return selExchanges; }
            set {
                if(SelectedExchanges == value)
                    return;
                selExchanges = value;
                OnSelectedExchangesChanged();
            }
        }

        List<string> selMarkets = new List<string>();
        public List<string> SelectedMarktes {
            get { return selMarkets; }
            set {
                if(SelectedMarktes == value)
                    return;
                selMarkets = value;
                OnSelectedMarketsChanged();
            }
        }

        private void OnSelectedMarketsChanged() {
            UpdateMarketsSelection();
        }

        private void UpdateMarketsSelection() {
            this.cbMarkets.BeginUpdate();
            try {
                for(int i = 0; i < this.cbMarkets.Items.Count; i++) {
                    this.cbMarkets.SetItemChecked(i, SelectedMarktes.Contains((string)this.cbMarkets.Items[i].Value));
                }
            }
            finally {
                this.cbMarkets.EndUpdate();
            }
        }

        protected virtual void OnSelectedExchangesChanged() {
            UpdateMarkets();
        }

        private void OnExchangesItemCheckChanged(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e) {
            UpdateMarkets();
        }

        protected virtual void UpdateMarkets() {
            List<string[]> markets = new List<string[]>();
            List<Exchange> sl = GetCheckedExchanges();
            using(WithProgressPanel panel = new WithProgressPanel(this)) {
                foreach(Exchange e in sl) {
                    if(!e.Connect()) {
                        XtraMessageBox.Show(e.Name + ": can't connect to exchange.", "Update Markets");
                        return;
                    }
                    markets.Add(e.GetMarkets());
                }
            }
            List<string> filtered = FilterMarkets(markets);
            this.cbMarkets.BeginUpdate();
            try {
                this.cbMarkets.Items.Clear();
                for(int i = 0; i < filtered.Count; i++) {
                    this.cbMarkets.Items.Add(filtered[i]);
                }
            }
            finally {
                this.cbMarkets.EndUpdate();
            }
            UpdateMarketsSelection();
        }

        private List<Exchange> GetCheckedExchanges() {
            return this.cbExchanges.Items.Where(i => i.CheckState == CheckState.Checked).Select<CheckedListBoxItem, Exchange>(i => (Exchange)i.Value).ToList();
        }

        protected virtual List<string> FilterMarkets(List<string[]> markets) {
            List<string> res = new List<string>();
            if(markets == null || markets.Count == 0)
                return res;
            for(int i = 0; i < markets[0].Length; i++) {
                bool found = true;
                for(int j = 1; j < markets.Count; j++) {
                    if(!markets[j].Contains(markets[0][i])) {
                        found = false;
                        break;
                    }
                }
                if(found)
                    res.Add(markets[0][i]);
            }
            return res;
        }

        private void sbOk_Click(object sender, EventArgs e) {
            string error = ValidateExchanges();
            if(error != null) {
                XtraMessageBox.Show(error, "Validation");
                return;
            }
            DialogResult = DialogResult.OK;
            this.selExchanges = GetCheckedExchanges();
            this.selMarkets = this.cbMarkets.Items.Where(i => i.CheckState == CheckState.Checked).Select<CheckedListBoxItem, string>(i => (string)i.Value).ToList();
            Close();
        }

        protected virtual string ValidateExchanges() {
            if(this.cbExchanges.Items.Count(i => i.CheckState == CheckState.Checked) < 2)
                return "You should check at least 2 exchanges for classic arbitrage";
            if(this.cbMarkets.Items.Count(i => i.CheckState == CheckState.Checked) == 0)
                return "You should check at least one market classic arbitrage";
            return null;
        }
    }
}
