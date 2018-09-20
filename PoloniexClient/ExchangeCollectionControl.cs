using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using CryptoMarketClient;

namespace TileViewSelector {
    public partial class ExchangeCollectionControl : XtraUserControl {
        public ExchangeCollectionControl() {
            InitializeComponent();
            this.tileViewSelector.ItemClick += OnItemClick;
        }

        private void OnItemClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e) {
            ((MainForm)FindForm().MdiParent).ShowExchange(((LogoInfo)this.tileViewSelector.GetRow(e.Item.RowHandle)).Exchange);
        }

        public void SetData(object dataSource) {
            gridSelector.DataSource = dataSource;
        }
    }
}
