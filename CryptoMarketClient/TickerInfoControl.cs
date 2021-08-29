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
using CryptoMarketClient.Strategies;
using Crypto.Core;

namespace CryptoMarketClient {
    public partial class TickerInfo : XtraUserControl {
        public TickerInfo() {
            InitializeComponent();
            this.gridControl1.ForceInitialize();
            
        }

        Ticker ticker;
        public Ticker Ticker {
            get { return ticker; }
            set {
                if(Ticker == value)
                    return;
                Ticker prev = Ticker;
                ticker = value;
                OnTickerChanged(prev);
            }
        }
        
        void OnTickerChanged(Ticker prev) {
            if(prev != null)
                prev.HistoryChanged -= Ticker_Changed;
            if(Ticker != null)
                Ticker.HistoryChanged += Ticker_Changed;
            List<Ticker> list = new List<Ticker>();
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
        }

        public int CalcBestHeight() {
            return 0;
        }

        public void UpdateBestHeight() {
        }

        private void gridView1_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e) {
        }
        public void UpdateData() {
            this.gridView1.RefreshRow(0);
        }

        private void gridControl1_PaintEx(object sender, DevExpress.XtraGrid.PaintExEventArgs e) {
            SkinElement elem = GridSkins.GetSkin(this.gridControl1.LookAndFeel.ActiveLookAndFeel)[GridSkins.SkinGridLine];
            e.Cache.DrawLine(e.Cache.GetPen(elem.Color.ForeColor), 
                new Point(e.ClipRectangle.X, e.ClipRectangle.Top),
                new Point(e.ClipRectangle.Right, e.ClipRectangle.Top));
        }
    }
}
