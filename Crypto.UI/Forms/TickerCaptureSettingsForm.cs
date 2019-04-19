using Crypto.Core.Common;
using Crypto.Core.Strategies.Listeners;
using CryptoMarketClient;
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

namespace Crypto.UI.Forms {
    public partial class TickerCaptureSettingsForm : XtraForm {
        public TickerCaptureSettingsForm() {
            InitializeComponent();
        }

        public TickerDataCaptureStrategy Settings {
            get {
                if(this.bindingSource1.DataSource is TickerDataCaptureStrategy)
                    return (TickerDataCaptureStrategy)this.bindingSource1.DataSource;
                return null;
            }
            set {
                if(value == null) {
                    this.bindingSource1.DataSource = typeof(TickerDataCaptureStrategy);
                    return;
                }
                this.bindingSource1.DataSource = value;
                OnSettingsChanged();
            }
        }

        protected virtual void OnSettingsChanged() {
            if(Settings == null)
                this.customStrategyConfigurationControl1.Strategy = null;
            else 
                this.customStrategyConfigurationControl1.Strategy = Settings;
        }

        private void DirectoryButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            if(this.folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                Settings.Directory = this.folderBrowserDialog1.SelectedPath;
                DirectoryButtonEdit.Text = Settings.Directory;
            }
        }

        private void sbOk_Click(object sender, EventArgs e) {
            if(Settings.StrategyInfo.Tickers.Count == 0) {
                XtraMessageBox.Show("Please, specify at least one ticker");
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
