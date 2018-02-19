using CryptoMarketClient.Common;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Utils;
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
    public partial class TradeSettingsControl : XtraUserControl {
        public TradeSettingsControl() {
            InitializeComponent();

            this.comboBoxEdit1.Properties.Items.AddRange(typeof(TrailingType).GetEnumValues());
        }

        bool showTrailingSettings = true;
        public bool ShowTrailingSettings {
            get { return showTrailingSettings; }
            set {
                if(ShowTrailingSettings == value)
                    return;
                this.showTrailingSettings = value;
                OnShowTrailingSettingsChanged();
            }
        }
        void OnShowTrailingSettingsChanged() {
            this.layoutControlGroup3.Visibility = ShowTrailingSettings ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
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
                itemForSpendBTC.Text = "Total " + Settings.Ticker.BaseCurrency;
            }
        }

        protected bool ValidateChildrenCore() {
            return true;
        }
        private void tradeButton_Click(object sender, EventArgs e) {
            if(!ValidateChildrenCore()) {
                XtraMessageBox.Show("Not all fields are filled!");
                return;
            }
            if (!this.checkEdit1.Checked) {
                if ((TrailingType)this.comboBoxEdit1.EditValue == TrailingType.Buy) {
                    if (Ticker.Buy(Settings.TradePrice, Settings.Amount)) {
                        XtraMessageBox.Show("Error buying. Please try later again.");
                        return;
                    }
                } else {
                    if (Ticker.Sell(Settings.TradePrice, Settings.Amount)) {
                        XtraMessageBox.Show("Error selling. Please try later again.");
                        return;
                    }
                }
            } else {
                Settings.Date = DateTime.UtcNow;
                Ticker.Trailings.Add(Settings);
                Settings.Start();
                if (OperationsProvider != null)
                    OperationsProvider.ShowTradingResult(Ticker);
                Ticker.Save();

                XtraMessageBox.Show("Trailing added!");
            }
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

        private void OnComboBoxEditValueChanged(object sender, System.EventArgs e) {
            if (!(this.comboBoxEdit1.EditValue is TrailingType))
                return;

            bool isBuy = (TrailingType)this.comboBoxEdit1.EditValue == TrailingType.Buy;
            this.btnTrade.Text = isBuy ? "Buy" : "Sell";
            this.ItemForBuyPrice.Text = isBuy ? "Buy Price" : "Sell Price";
            this.itemForSpendBTC.Text = isBuy ? "Spend BTC" : "Earn BTC";
            LayoutVisibility visibility = isBuy ? LayoutVisibility.Never : LayoutVisibility.Always;
            this.ItemForStopLossPricePercent.Visibility = visibility;
            this.ItemForIngoreStopLoss.Visibility = visibility;
            this.ItemForIncrementalStopLoss.Visibility = visibility;
        }
    }

    public interface ITradingResultOperationsProvider {
        void ShowTradingResult(TickerBase ticker);
    }
}
