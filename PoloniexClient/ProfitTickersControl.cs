using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public partial class ProfitTickersControl : UserControl {
        public ProfitTickersControl() {
            InitializeComponent();
        }

        private void gridControl1_Resize(object sender, EventArgs e) {
            float koeff = (600.0f / 400.0f);
            this.tileView1.OptionsTiles.Orientation = Width > Height? Orientation.Horizontal: Orientation.Vertical;
            if(this.tileView1.OptionsTiles.Orientation == Orientation.Horizontal) {
                this.tileView1.OptionsTiles.ColumnCount = 0;
                this.tileView1.OptionsTiles.RowCount = 1;
                this.tileView1.OptionsTiles.ItemSize = new Size((int)(koeff * (Height - 20)), Height - 20);
            }
            else {
                this.tileView1.OptionsTiles.ColumnCount = 1;
                this.tileView1.OptionsTiles.RowCount = 0;
                this.tileView1.OptionsTiles.ItemSize = new Size(Width - 20, (int)((Width - 20) / koeff));
            }
        }

        public List<CombinedTickerInfo> Tickers {
            get { return (List<CombinedTickerInfo>)this.gridControl1.DataSource; }
            set { this.gridControl1.DataSource = value; }
        }
    }

    public class CombinedTickerInfo {
        public Image LowestChartImage { get; set; } 
        public Image LowestOrderBookImage { get; set; }

        public Image HighestChartImage { get; set; }
        public Image HighestOrderBookImage { get; set; }
    }
}
