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
using DevExpress.XtraGrid.Views.WinExplorer;
using DevExpress.LookAndFeel;

namespace TileViewSelector {
    public partial class ExchangeCollectionControl : XtraUserControl {
        public ExchangeCollectionControl() {
            InitializeComponent();
            
        }

        private void onEnterClick(object sender, WinExplorerViewHtmlElementEventArgs e) {
            ((MainForm)FindForm().MdiParent).ShowExchange(((LogoInfo)this.winExplorerView1.GetRow(e.RowHandle)).Exchange);
        }

        public void SetData(object dataSource) {
            gridSelector.DataSource = dataSource;
        }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            UpdateBackground();
        }
        protected override void OnLookAndFeelChanged() {
            base.OnLookAndFeelChanged();
            UpdateBackground();
        }
        Color BlendColor(Color bg, Color c) {
            float a = 0.1f;
            return Color.FromArgb((int)(a * c.R + (1 - a) * bg.R), (int)(a * c.G + (1 - a) * bg.G), (int)(a * c.B + (1 - a) * bg.B));
        }
        void UpdateBackground() {
            Color bg = LookAndFeelHelper.GetSystemColor(UserLookAndFeel.Default, SystemColors.Window);
            Color fg = LookAndFeelHelper.GetSystemColor(UserLookAndFeel.Default, SystemColors.WindowText);
            this.winExplorerView1.Appearance.EmptySpace.BackColor = BlendColor(bg, fg);
        }

    }
}
