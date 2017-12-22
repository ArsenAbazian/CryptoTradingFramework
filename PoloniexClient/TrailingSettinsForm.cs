using CryptoMarketClient.Common;
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
    public partial class TrailingSettinsForm : Form {
        public TrailingSettinsForm() {
            InitializeComponent();
        }

        TrailingSettings settings;
        public TrailingSettings Settings {
            get { return settings; }
            set {
                if(Settings == value)
                    return;
                settings = value;
                OnSettingsChanged();
            }
        }

        public TickerBase Ticker {
            get; set;
        }

        void OnSettingsChanged() {
            this.trailingSettingsBindingSource.DataSource = Settings;
        }

        private void OnCancelClick(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OnOkClick(object sender, EventArgs e) {
            DialogResult res = XtraMessageBox.Show("Are you shure, that all parameters are ok?", "Adding Trailing", MessageBoxButtons.YesNo);
            if(res != DialogResult.Yes)
                return;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
