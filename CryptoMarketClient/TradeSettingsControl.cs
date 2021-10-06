using Crypto.Core;
using Crypto.Core.Common;
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
        }

        bool showTrailingSettings = true;
        public bool ShowTrailingSettings {
            get { return showTrailingSettings; }
            set {
                if(ShowTrailingSettings == value)
                    return;
                this.showTrailingSettings = value;
            }
        }

        public ITradingResultOperationsProvider OperationsProvider { get; set; }

        public Ticker Ticker { get; set; }
        TradingSettings settings;
        public TradingSettings Settings {
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
            this.tradeSettingsBindingSource.DataSource = Settings;
            if(Settings.Ticker != null) {
                itemForSpendBTC.Text = "Total " + Settings.Ticker.BaseCurrency;
            }
        }

        protected bool ValidateChildrenCore() {
            return true;
        }

        private void MakeTrade(OrderType type) {
            if(!ValidateChildrenCore()) {
                XtraMessageBox.Show("Not all fields are filled!");
                return;
            }

            string validationError = string.Empty;
            if(type == OrderType.Buy)
                validationError = Ticker.ValidateTrade(Settings.BuyPrice, Settings.BuyAmount);
            else 
                validationError = Ticker.ValidateTrade(Settings.SellPrice, Settings.SellAmount);
            if(!string.IsNullOrEmpty(validationError)) {
                XtraMessageBox.Show("Error validating trade. Values will be corrected. Error was: " + validationError);

                double rate = 0;
                double amount = 0;
                if(type == OrderType.Buy) {
                    rate = Settings.BuyPrice;
                    amount = Settings.BuyAmount;
                }
                else {
                    rate = Settings.SellPrice;
                    amount = Settings.SellAmount;
                }
                Ticker.CorrectTrade(ref rate, ref amount);
                if(type == OrderType.Buy) {
                    Settings.BuyPrice = rate;
                    Settings.BuyAmount = amount;
                }
                else {
                    Settings.SellPrice = rate;
                    Settings.SellAmount = amount;
                }
                return;
            }
            TradingResult res = null;
            if(type == OrderType.Buy) {
                res = Ticker.Buy(Settings.BuyPrice, Settings.BuyAmount);
                if(res == null) {
                    XtraMessageBox.Show("Error buying. " + LogManager.Default.Messages.Last().Description);
                    return;
                }
            }
            else {
                Settings.Enabled = false;
                res = Ticker.Sell(Settings.SellPrice, Settings.SellAmount);
                if(res == null) {
                    XtraMessageBox.Show("Error selling. " + LogManager.Default.Messages.Last().Description);
                    return;
                }
            }
            TradingResult = res;
            if(Settings.Enabled) {
                Settings.Date = DateTime.UtcNow;
                Ticker.Trailings.Add(Settings);
                Settings.Start();
                if(OperationsProvider != null)
                    OperationsProvider.ShowTradingResult(Ticker);
                Ticker.Save();

                XtraMessageBox.Show("Trailing added!");
            }
            RaiseOnTrade(res);
        }

        private static readonly object onTrade = new object();
        public event TradeEventHandler Trade {
            add { Events.AddHandler(onTrade, value); }
            remove { Events.RemoveHandler(onTrade, value); }
        }
        private void RaiseOnTrade(TradingResult trade) {
            TradeEventHandler handler = (TradeEventHandler)Events[onTrade];
            if(handler != null)
                handler(this, new TradeEventArgs() { Trade = trade });
        }
        public TradingResult TradingResult { get; set; }
        private void OnBuyButtonClick(object sender, EventArgs e) {
            MakeTrade(OrderType.Buy);
        }
        public void SelectedAskChanged(object sender, FocusedRowChangedEventArgs e) {
            OrderBookEntry entry = (OrderBookEntry)((GridView)sender).GetRow(e.FocusedRowHandle);
            BuyPriceTextEdit.EditValue = entry.Value;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e) {
            ItemForTakeProfitPercent.Enabled = ItemForTakeProfitStartPercent.Enabled = ItemForIngoreStopLoss.Enabled = this.checkEdit1.IsOn;
            ItemForStopLossPricePercent.Enabled = ItemForIncrementalStopLoss.Enabled = this.checkEdit1.IsOn && !this.ceIgnoreStopLoss.IsOn;
        }

        private void ceIgnoreStopLoss_CheckedChanged(object sender, EventArgs e) {
            ItemForIncrementalStopLoss.Enabled = ItemForStopLossPricePercent.Enabled = !this.ceIgnoreStopLoss.IsOn && this.checkEdit1.IsOn;
        }

        private void btnSell_Click(object sender, EventArgs e) {
            MakeTrade(OrderType.Sell);
        }

        private void tbDepositPercent_EditValueChanged(object sender, EventArgs e) {
            Settings.BuyPrice = Ticker.OrderBook.Asks[0].Value;
            Settings.BuyAmount = tbBaseDeposit.Value / 100.0 * Ticker.BaseCurrencyBalance / Settings.BuyPrice;
            this.layoutControlItem4.Text = tbBaseDeposit.Value + "% of Deposit";
        }

        private void trackBarControl1_EditValueChanged(object sender, EventArgs e) {
            Settings.SellPrice = Ticker.OrderBook.Bids[0].Value;
            Settings.SellAmount = this.tbSellDeposit.Value / 100.0 * Ticker.MarketCurrencyBalance;
            this.layoutControlItem7.Text = tbBaseDeposit.Value + "% of Deposit";
        }
    }

    public interface ITradingResultOperationsProvider {
        void ShowTradingResult(Ticker ticker);
    }

    public class TradeEventArgs {
        public TradingResult Trade { get; internal set; }
    }
    public delegate void TradeEventHandler(object sender, TradeEventArgs e);
}
