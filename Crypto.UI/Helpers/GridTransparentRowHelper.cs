using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.UI.Helpers {
    public static class GridTransparentRowHelper {
        public static void Apply(GridView view) {
            view.RowStyle += OnRowStyle;
            view.Disposed += OnDisposed;
        }
        static void OnDisposed(object sender, EventArgs e) {
            ((GridView)sender).Disposed -= OnDisposed;
            ((GridView)sender).RowStyle -= OnRowStyle;
        }

        private static void OnRowStyle(object sender, RowStyleEventArgs e) {
            GridView view = ((GridView)sender);
            if(view.FocusedRowHandle != e.RowHandle)
                return;
            e.Appearance.ForeColor = view.PaintAppearance.Row.ForeColor;
            e.Appearance.BackColor = Color.FromArgb(0x20, view.PaintAppearance.FocusedRow.BackColor);
            e.HighPriority = true;
        }
    }
}
