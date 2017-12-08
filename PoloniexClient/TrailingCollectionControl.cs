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
            this.trailingSettingsBindingSource.DataSource = Ticker == null? null: Ticker.SellTrailings;
        }

        private void btAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TrailingSettings settings = new TrailingSettings();
            settings.EnableIncrementalStopLoss = true;
            settings.Ticker = Ticker;
            settings.UsdTicker = Ticker.UsdTicker;
            TrailingSettinsForm form = new TrailingSettinsForm();
            form.Ticker = Ticker;
            form.Settings = settings;
            if(form.ShowDialog() != DialogResult.OK)
                return;
            Ticker.SellTrailings.Add(settings);
            this.gvTrailings.RefreshData();
        }
    }
}
