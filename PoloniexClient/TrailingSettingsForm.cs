using CryptoMarketClient.Common;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class TrailingSettingsForm : XtraForm {
        public TrailingSettingsForm() {
            InitializeComponent();
        }
        TrailingSettings settings;
        public TrailingSettings Settings {
            get {
                return settings;
            }
            set {
                if(Settings == value)
                    return;
                this.settings = value;
                OnSettingsChanged();
            }
        }
        void OnSettingsChanged() {
            this.tralingSettingsBindingSource.DataSource = Settings;
            Text = "Trailing Settings: " + Settings.Name;
        }

        private void simpleButton2_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            if(!this.ValidateChildren()) {
                XtraMessageBox.Show("Not all fields are filled!");
                return;
            }
            TrailingManager.Default.Items.Add(Settings);
            Close();
        }
        public void SelectedAskChanged(object sender, FocusedRowChangedEventArgs e) {
            OrderBookEntry entry = (OrderBookEntry)((GridView)sender).GetRow(e.FocusedRowHandle);
            BuyPriceTextEdit.EditValue = entry.Value;
        }
    }
}
