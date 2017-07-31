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
using DevExpress.Skins;
using DevExpress.XtraGrid.Views.Tile.ViewInfo;

namespace CryptoMarketClient {
    public partial class TickerInfo : XtraUserControl {
        public TickerInfo() {
            InitializeComponent();
            this.gridControl1.ForceInitialize();
            
        }

        ITicker ticker;
        public ITicker Ticker {
            get { return ticker; }
            set {
                if(Ticker == value)
                    return;
                ITicker prev = Ticker;
                ticker = value;
                OnTickerChanged(prev);
            }
        }
        
        void OnTickerChanged(ITicker prev) {
            if(prev != null)
                prev.HistoryItemAdd -= Ticker_Changed;
            if(Ticker != null)
                Ticker.HistoryItemAdd += Ticker_Changed;
            List<ITicker> list = new List<ITicker>();
            list.Add(Ticker);
            this.bindingSource.DataSource = list;
            UpdateTickerInfo();
        }

        private void Ticker_Changed(object sender, EventArgs e) {
            if(IsHandleCreated)
                BeginInvoke(new Action(UpdateTickerInfo));
        }

        void UpdateTickerInfo() {
            if(Ticker == null)
                return;
            this.colChange.AppearanceCell.ForeColor = Ticker.Change < 0 ? Color.Red : Color.Green;
            this.colLast.AppearanceCell.ForeColor = this.colChange.AppearanceCell.ForeColor;
            this.gridControl1.RefreshDataSource();
        }

        public int CalcBestHeight() {
            TileViewInfo viewInfo = (TileViewInfo)this.tileView1.GetViewInfo();
            TileItem item = viewInfo.DefaultGroup.Items[0];
            return item.ItemInfo.GetOptimizedTableSettings().ItemHeight;
        }

        public void UpdateBestHeight() {
            this.tileView1.OptionsTiles.ItemSize = new Size(this.tileView1.OptionsTiles.ItemSize.Width, this.CalcBestHeight());
            Height = this.tileView1.OptionsTiles.ItemSize.Height + 20;
        }
    }
}
