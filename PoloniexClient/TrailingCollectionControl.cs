using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptoMarketClient.Common;
using DevExpress.XtraEditors;

namespace CryptoMarketClient {
    public partial class TrailingCollectionControl : UserControl {
        public TrailingCollectionControl() {
            InitializeComponent();
        }

        TickerBase ticker;
        public TickerBase Ticker {
            get { return ticker; }
            set {
                if(Ticker == value)
                    return;
                ticker = value;
                OnTickerChanged();
            }
        }
        void OnTickerChanged() {
            Ticker.Changed += OnTickerUpdated;
            this.trailingSettingsBindingSource.DataSource = Ticker == null? null: Ticker.SellTrailings;
        }

        private void OnTickerUpdated(object sender, EventArgs e) {
            this.gvTrailings.RefreshData();
        }

        protected TrailingSettings CreateNewSettings() {
            TrailingSettings settings = new TrailingSettings();
            settings.EnableIncrementalStopLoss = true;
            settings.Ticker = Ticker;
            settings.UsdTicker = Ticker.UsdTicker;
            settings.Mode = ActionMode.Notify;
            return settings;
        }

        protected TrailingSettinsForm CreateSettingsForm(TrailingSettings settings) {
            TrailingSettinsForm form = new TrailingSettinsForm();
            form.Ticker = Ticker;
            form.Settings = settings;
            form.Owner = FindForm();
            form.CollectionControl = this;
            form.Mode = EditingMode.Add;

            return form;
        }

        private void btAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TrailingSettings settings = CreateNewSettings();
            TrailingSettinsForm form = new TrailingSettinsForm();
            form.Mode = EditingMode.Add;
            form.Settings = settings;
            form.Accepted += OnTrailingSettingsFormAccepted;
            form.Show();
        }

        private void OnTrailingSettingsFormAccepted(object sender, EventArgs e) {
            TrailingSettinsForm form = (TrailingSettinsForm)sender;
            if(form.Mode == EditingMode.Add) {
                Ticker.SellTrailings.Add(form.Settings);
                this.gcTrailings.RefreshDataSource();
            }
        }

        private void btRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(XtraMessageBox.Show("Are you shure to remove selected trailing?", "Remove Trailings", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                return;
            TrailingSettings settings = (TrailingSettings)this.gvTrailings.GetFocusedRow();
            if(settings == null)
                return;
            Ticker.SellTrailings.Remove(settings);
            this.gcTrailings.RefreshDataSource();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(XtraMessageBox.Show("Are you shure to remove all trailings?", "Remove Trailings", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                return;
            Ticker.SellTrailings.Clear();
            this.gcTrailings.RefreshDataSource();
        }
    }
}
