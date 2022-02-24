using Crypto.Core;
using Crypto.UI.Helpers;
using DevExpress.DXperience.Demos;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeMap;
using DevExpress.XtraTreeMap.Native;
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
    public partial class ExchangeMarketCapacityForm : XtraForm {
        public ExchangeMarketCapacityForm() {
            InitializeComponent();
            InitializeExchanges();
        }

        private void InitializeExchanges() {
            foreach(Exchange e in Exchange.Registered) {
                this.repositoryItemComboBox2.Items.Add(e);    
            }
        }

        Exchange exchange;
        public Exchange Exchange {
            get { return exchange; }
            set {
                if(Exchange == value)
                    return;
                exchange = value;
                OnExchangeChanged();
            }
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            BarSubItem bsi = this.biColors;

            List<string> paletteNames = DevExpress.XtraTreeMap.Palettes.GetNames();
            foreach(string palette in paletteNames) {
                Image img = PaletteUtils.CreateEditorImage(DevExpress.XtraTreeMap.Palettes.GetPalette(palette), 6);
                CheckBarItem cbiPaletteName = new CheckBarItem(barManager1, palette, cbiPaletteName_Click);
                cbiPaletteName.Glyph = img;
                bsi.ItemLinks.Add(cbiPaletteName);
            }
        }
        void cbiPaletteName_Click(object sender, ItemClickEventArgs e) {
            CheckBarItem item = e.Item as CheckBarItem;
            if(item != null) {
                TreeMapPaletteColorizerBase paletteColorizer = (TreeMapPaletteColorizerBase)treeMapControl1.Colorizer;
                paletteColorizer.Palette = DevExpress.XtraTreeMap.Palettes.GetPalette(item.Caption);
            }
        }

        protected virtual void OnExchangeChanged() {
            string selMarket = (string)this.beMarkets.EditValue;

            this.repositoryItemComboBox1.Items.Clear();
            this.tickerBindingSource.DataSource = null;
            
            if(Exchange != null && !Exchange.IsConnected) {
                using(WithProgressPanel panel = new WithProgressPanel(this)) {
                    if(!Exchange.Connect()) {
                        XtraMessageBox.Show("Failed to connect exchange");
                        return;
                    }
                    if(!Exchange.SupportCummulativeTickersUpdate)
                        XtraMessageBox.Show("This exchange does not allow to update volume for all tickers at once. Sorry, but it seems you will see empty graph :(");
                }
            }
            InitializeComboBox(selMarket);
            if(selMarket == null)
                this.beExchanges.EditValue = Exchange;
            UpdateDataSource();
            
        }

        private void UpdateDataSource() {
            this.tickerBindingSource.DataSource = null;
            if(Exchange == null)
                return;
            string marketName = Convert.ToString(this.beMarkets.EditValue);
            this.tickerBindingSource.DataSource = Exchange.Tickers.Where(t=> t.BaseCurrency == marketName).ToList();
        }

        private void InitializeComboBox(string selMarket) {
            if(Exchange == null)
                return;
            string[] markets = Exchange.GetMarkets();
            foreach(string market in markets) {
                this.repositoryItemComboBox1.Items.Add(market);
            }

            this.beMarkets.EditValue = selMarket == null? (markets.Contains("XBT") ? "XBT" : "BTC") : selMarket;
        }

        private void beMarkets_EditValueChanged(object sender, EventArgs e) {
            UpdateDataSource();
        }

        private void commandBarGalleryDropDown7_GalleryItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e) {
            this.treeMapControl1.Colorizer = new TreeMapPaletteColorizer {
                Palette = (Palette)e.Item.Tag
            };
        }

        private void beExchanges_EditValueChanged(object sender, EventArgs e) {
            Exchange = (Exchange)this.beExchanges.EditValue;
        }
    }
}
