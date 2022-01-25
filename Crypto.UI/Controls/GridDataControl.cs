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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace Crypto.UI.Forms {
    public partial class GridDataControl : XtraUserControl {
        public GridDataControl() {
            InitializeComponent();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridControl Grid { get { return this.gcData; } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridView GridView { get { return this.gvData; } }

        private void gvData_RowStyle(object sender, RowStyleEventArgs e) {
            if(this.gvData.FocusedRowHandle != e.RowHandle)
                return;
            e.Appearance.ForeColor = this.gvData.PaintAppearance.Row.ForeColor;
            e.Appearance.BackColor = Color.FromArgb(0x10, this.gvData.PaintAppearance.FocusedRow.BackColor);
            e.HighPriority = true;
        }
    }
}
