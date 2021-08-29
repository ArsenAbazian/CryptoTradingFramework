using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using Crypto.Core.Common;
using Crypto.Core;

namespace CryptoMarketClient {
    public partial class TrailingCollectionControl : UserControl {
        public TrailingCollectionControl() {
            InitializeComponent();
        }

        [DefaultValue(null)]
        public TickerChartViewer ChartControl { get; set; }

        Ticker ticker;
        public Ticker Ticker {
            get { return ticker; }
            set {
                if(Ticker == value)
                    return;
                ticker = value;
                OnTickerChanged();
            }
        }
        void OnTickerChanged() {
            if(Ticker != null)
                Ticker.Changed += OnTickerUpdated;
            this.gcTrailings.DataSource = Ticker == null ? null : Ticker.Trailings;
            if(ChartControl != null && Ticker != null) {
                for(int i = 0; i < Ticker.Trailings.Count; i++) {
                    TradingSettings settings = Ticker.Trailings[i];
                    if(settings.ShowOnChart)
                        ChartControl.AddIndicator(settings);
                }
            }
        }

        private void OnTickerUpdated(object sender, EventArgs e) {
            if(IsHandleCreated)
                BeginInvoke(new MethodInvoker(() => this.gvTrailings.RefreshData()));
        }

        protected TradingSettings CreateNewSettings() {
            TradingSettings settings = new TradingSettings(Ticker);
            settings.Mode = ActionMode.Notify;
            return settings;
        }

        protected TrailingSettinsForm CreateSettingsForm(TradingSettings settings) {
            TrailingSettinsForm form = new TrailingSettinsForm();
            form.Ticker = Ticker;
            form.Settings = settings;
            form.Owner = FindForm();
            form.CollectionControl = this;
            form.Mode = EditingMode.Edit;

            return form;
        }
        
        private void btEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TradingSettings settings = (TradingSettings)this.gvTrailings.GetRow(this.gvTrailings.FocusedRowHandle);
            TrailingSettinsForm form = CreateSettingsForm(settings);
            form.Mode = EditingMode.Edit;
            form.Accepted += OnTrailingSettingsFormAccepted;
            form.Show();
        }

        private void btAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TradingSettings settings = CreateNewSettings();
            TrailingSettinsForm form = new TrailingSettinsForm();
            form.Mode = EditingMode.Add;
            form.Settings = settings;
            form.Accepted += OnTrailingSettingsFormAccepted;
            form.Show();
        }

        private void OnTrailingSettingsFormAccepted(object sender, EventArgs e) {
            TrailingSettinsForm form = (TrailingSettinsForm)sender;
            if(form.Mode == EditingMode.Add) {
                form.Settings.Date = DateTime.UtcNow;
                Ticker.Trailings.Add(form.Settings);
                form.Settings.Start();
                Ticker.Save();
                this.gcTrailings.RefreshDataSource();
            }
            else {
                form.Settings.Change();
            }
            if(ChartControl != null) {
                if(form.Settings.ShowOnChart)
                    ChartControl.AddIndicator(form.Settings);
                else
                    ChartControl.RemoveIndicator(form.Settings);
            }
        }

        private void btRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(XtraMessageBox.Show("Are you shure to remove selected trailing?", "Remove Trailings", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                return;
            TradingSettings settings = (TradingSettings)this.gvTrailings.GetFocusedRow();
            if(settings == null)
                return;
            Ticker.Trailings.Remove(settings);
            if(ChartControl != null)
                ChartControl.RemoveIndicator(settings);
            this.gcTrailings.RefreshDataSource();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(XtraMessageBox.Show("Are you shure to remove all trailings?", "Remove Trailings", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                return;
            if(ChartControl != null) Ticker.Trailings.ForEach(t => ChartControl.RemoveIndicator(t));
            Ticker.Trailings.Clear();
            this.gcTrailings.RefreshDataSource();
        }
    }
}
