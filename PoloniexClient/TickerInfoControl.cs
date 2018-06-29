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
                prev.HistoryItemAdd -= Ticker_Changed;
            if(Ticker != null)
                Ticker.HistoryItemAdd += Ticker_Changed;
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
            //this.gridControl1.RefreshDataSource();
        }

        public int CalcBestHeight() {
            //TileViewInfo viewInfo = (TileViewInfo)this.tileView1.GetViewInfo();
            //TileItem item = viewInfo.DefaultGroup.Items[0];
            //return item.ItemInfo.GetOptimizedTableSettings().ItemHeight;
            return 0;
        }

        public void UpdateBestHeight() {
            //this.tileView1.OptionsTiles.ItemSize = new Size(this.tileView1.OptionsTiles.ItemSize.Width, this.CalcBestHeight());
            //Height = this.tileView1.OptionsTiles.ItemSize.Height + 20;
        }

        private void gridView1_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e) {
            //using(StringFormat format = (StringFormat)e.Appearance.GetStringFormat().Clone()) {
            //    SkinElement elem = GridSkins.GetSkin(this.gridControl1.LookAndFeel.ActiveLookAndFeel)[GridSkins.SkinGridLine];
            //    e.Cache.DrawRectangle(e.Cache.GetPen(elem.Color.ForeColor), e.Bounds);
            //    format.Alignment = StringAlignment.Center;
            //    e.Cache.DrawString(e.Column.GetCaption(), e.Appearance.Font, e.Appearance.ForeColor, e.Bounds, format);
            //    e.Handled = true;
            //}
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
