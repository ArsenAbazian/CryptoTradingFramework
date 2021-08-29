using Crypto.Core;
using Crypto.Core.Common;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace CryptoMarketClient {
    public partial class GrabDataSettingsForm : XtraForm {
        public GrabDataSettingsForm() {
            InitializeComponent();
            Settings = new GrabDataSettings();
            this.grabDataSettingsBindingSource.DataSource = Settings;
        }

        private void simpleButton2_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public GrabDataSettings Settings { get; set; }
        public Ticker Ticker { get; set; }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            if(this.folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                Settings.DirectoryName = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            Settings.Ticker = Ticker;
            if(!Directory.Exists(Settings.DirectoryName)) {
                XtraMessageBox.Show("Invalid directory name.");
                return;
            }
            if(!Settings.GrabChart && !Settings.GrabTradeHistory) {
                XtraMessageBox.Show("None of grab parameters is selected.");
                return;
            }
            if(Settings.GrabChart) {
                Settings.CandleStickData = Ticker.Exchange.GetCandleStickData(Ticker, 5, Settings.StarTime, (DateTime.Now.Ticks - Settings.StarTime.Ticks) / TimeSpan.TicksPerSecond);
                if(Settings.CandleStickData == null || Settings.CandleStickData.Count == 0) {
                    XtraMessageBox.Show("Error: can't download candlestick data. Try again.");
                    return;
                }
            }
            if(Settings.GrabTradeHistory) {
                Settings.TradeData = Ticker.Exchange.GetTrades(Ticker, Settings.StarTime);
                if(Settings.TradeData == null || Settings.TradeData.Count == 0) {
                    XtraMessageBox.Show("Error: can't download trade data. Try again.");
                    return;
                }
            }

            XmlSerializer ser = new XmlSerializer(typeof(GrabDataSettings));
            using(FileStream fs = new FileStream(Settings.FileName, FileMode.Create)) {
                ser.Serialize(fs, Settings);
            }
            XtraMessageBox.Show("Done. After that directory will be open in explorer");
            System.Diagnostics.Process.Start(Settings.DirectoryName);
        }
    }
}
