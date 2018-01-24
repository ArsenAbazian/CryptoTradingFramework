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
    public partial class BuySettingsControl : XtraUserControl {
        public BuySettingsControl() {
            InitializeComponent();
        }

        public ITradingResultOperationsProvider OperationsProvider { get; set; }

        public TickerBase Ticker { get; set; }
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
            if(Settings.Ticker != null) {
                layoutControlItem3.Text = "Total " + Settings.Ticker.BaseCurrency;
            }
        }

        protected bool ValidateChildrenCore() {
            return false;
        }
        private void simpleButton1_Click(object sender, EventArgs e) {
            if(!ValidateChildrenCore()) {
                XtraMessageBox.Show("Not all fields are filled!");
                return;
            }
            if(Ticker.Buy(Settings.BuyPrice, Settings.Amount)) {
                XtraMessageBox.Show("Error buying. Please try later again.");
                return;
            }
            if(Settings.EnableIncrementalStopLoss) {
                Settings.Date = DateTime.UtcNow;
                Ticker.Trailings.Add(Settings);
                Settings.Start();
            }
            if(OperationsProvider != null)
                OperationsProvider.ShowTradingResult(Ticker);
            Ticker.Save();
        }
        public void SelectedAskChanged(object sender, FocusedRowChangedEventArgs e) {
            OrderBookEntry entry = (OrderBookEntry)((GridView)sender).GetRow(e.FocusedRowHandle);
            BuyPriceTextEdit.EditValue = entry.Value;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e) {
            ItemForTakeProfitPercent.Enabled = ItemForTakeProfitStartPercent.Enabled = ItemForIngoreStopLoss.Enabled = this.checkEdit1.Checked;
            ItemForStopLossPricePercent.Enabled = ItemForIncrementalStopLoss.Enabled = this.checkEdit1.Checked && !this.ceIgnoreStopLoss.Checked;
        }

        private void ceIgnoreStopLoss_CheckedChanged(object sender, EventArgs e) {
            ItemForIncrementalStopLoss.Enabled = ItemForStopLossPricePercent.Enabled = !this.ceIgnoreStopLoss.Checked && this.checkEdit1.Checked;
        }
    }

    public interface ITradingResultOperationsProvider {
        void ShowTradingResult(TickerBase ticker);
    }
}
