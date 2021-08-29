using Crypto.Core;
using Crypto.Core.Common;
using DevExpress.XtraEditors;
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
    public partial class TrailingSettinsForm : XtraForm {
        public TrailingSettinsForm() {
            InitializeComponent();
            this.imageComboBoxEdit1.Properties.AddEnum<ActionMode>();

            this.cbType.Properties.Items.AddRange(typeof(TrailingType).GetEnumValues());
        }

        TradingSettings settings;
        public TradingSettings Settings {
            get { return settings; }
            set {
                if(Settings == value)
                    return;
                settings = value;
                OnSettingsChanged();
            }
        }

        public Ticker Ticker {
            get; set;
        }

        public TrailingCollectionControl CollectionControl { get; set; }
        OrderBookControl orderBook;
        public OrderBookControl OrderBookControl {
            get { return orderBook; }
            set {
                if(OrderBookControl == value)
                    return;
                OrderBookControl prev = OrderBookControl;
                orderBook = value;
                OnOrderBookChanged(prev);
            }
        }
        void OnOrderBookChanged(OrderBookControl prev) {
            if(prev != null) {
                prev.SelectedAskRowChanged -= OnSelectedAskRowChanged;
                prev.SelectedBidRowChanged -= OnSelectedBidRowChanged;
            }
            if(OrderBookControl != null) {
                OrderBookControl.SelectedAskRowChanged += OnSelectedAskRowChanged;
                OrderBookControl.SelectedBidRowChanged += OnSelectedBidRowChanged;
            }
        }

        private void OnSelectedBidRowChanged(object sender, SelectedOrderBookEntryChangedEventArgs e) {
            Settings.BuyPrice = e.Entry.Value;
        }

        void OnSelectedAskRowChanged(object sender, SelectedOrderBookEntryChangedEventArgs e) {
            Settings.BuyPrice = e.Entry.Value;
        }

        public EditingMode Mode { get; set; } = EditingMode.Add;

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
            UpdateOrAddTrailing();
            Close();
        }
        private static readonly object accepted = new object();
        public event EventHandler Accepted {
            add { Events.AddHandler(accepted, value); }
            remove { Events.RemoveHandler(accepted, value); }
        }
        void UpdateOrAddTrailing() {
            EventHandler handler = Events[accepted] as EventHandler;
            if(handler != null)
                handler.Invoke(this, EventArgs.Empty);
        }

        private void ceIgnoreStopLoss_CheckedChanged(object sender, EventArgs e) {
            ceIncrimentalStopLoss.Enabled = StopLossPricePercentTextEdit.Enabled = !ceIgnoreStopLoss.Checked;
        }
        private void cbType_EditValueChanged(object sender, System.EventArgs e) {
            if (!(this.cbType.EditValue is TrailingType))
                return;

            bool isBuy = (TrailingType)this.cbType.EditValue == TrailingType.Buy;
            this.ItemForBuyPrice.Text = isBuy ? "Buy Price" : "Sell Price";
            this.TotalSpendInBaseCurrencyTextEdit.Text = isBuy ? "Total spend in base currency" : "Total earn in base currency";
            LayoutVisibility visibility = isBuy ? LayoutVisibility.Never : LayoutVisibility.Always;
            this.ItemForStopLossPricePercent.Visibility = visibility;
            this.layoutControlItem5.Visibility = visibility;
            this.layoutControlItem6.Visibility = visibility;
        }
    }
}
