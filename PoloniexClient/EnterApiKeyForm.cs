using CryptoMarketClient.Bittrex;
using CryptoMarketClient.HitBtc;
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
    public partial class EnterApiKeyForm : XtraForm {
        public EnterApiKeyForm() {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            List<ApiKeyInfo> list = new List<ApiKeyInfo>();
            list.Add(new ApiKeyInfo() { Market = "Bittrex", ApiKey = BittrexModel.Default.ApiKey, Secret = BittrexModel.Default.ApiSecret });
            list.Add(new ApiKeyInfo() { Market = "Poloniex", ApiKey = PoloniexModel.Default.ApiKey, Secret = PoloniexModel.Default.ApiSecret });
            list.Add(new ApiKeyInfo() { Market = "HitBtc", ApiKey = HitBtcModel.Default.ApiKey, Secret = HitBtcModel.Default.ApiSecret });
            this.apiKeyInfoBindingSource.DataSource = list;
            Keys = list;
        }

        protected List<ApiKeyInfo> Keys { get; set; }
        private void simpleButton2_Click(object sender, EventArgs e) {
            Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            BittrexModel.Default.ApiKey = Keys.First((k) => k.Market == "Bittrex").ApiKey.Trim();
            BittrexModel.Default.ApiSecret = Keys.First((k) => k.Market == "Bittrex").Secret.Trim();
            PoloniexModel.Default.ApiKey = Keys.First((k) => k.Market == "Poloniex").ApiKey.Trim();
            PoloniexModel.Default.ApiSecret = Keys.First((k) => k.Market == "Poloniex").Secret.Trim();
            HitBtcModel.Default.ApiKey = Keys.First((k) => k.Market == "HitBtc").ApiKey.Trim();
            HitBtcModel.Default.ApiSecret = Keys.First((k) => k.Market == "HitBtc").Secret.Trim();
            BittrexModel.Default.Save();
            PoloniexModel.Default.Save();
            HitBtcModel.Default.Save();
            Close();
        }
    }
}
