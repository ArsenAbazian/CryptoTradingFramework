using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base.Handler;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Handler;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crypto.UI.CustomControls {
    public class InteractiveGridView : GridView {
        protected override BaseViewHandler CreateHandler() {
            return new InteractiveGridViewHandler(this);
        }
    }

    public class InteractiveGridViewHandler : GridHandler {
        public InteractiveGridViewHandler(GridView view) : base(view) { }

        protected IInteractiveEdit InteractiveEditor { get; private set; }
        protected GridCellInfo GetInteractiveEditorCell(MouseEventArgs ev) {
            GridHitInfo hi = ViewInfo.CalcHitInfo(ev.Location);
            if(!hi.InRowCell)
                return null;
            GridDataRowInfo ri = hi.RowInfo as GridDataRowInfo;
            if(ri == null)
                return null;

            GridCellInfo cellInfo = (GridCellInfo)ri.Cells.FirstOrDefault(c => c.Bounds.Contains(ev.Location) && c.ViewInfo is IInteractiveEdit);
            IInteractiveEdit editor = cellInfo?.ViewInfo as IInteractiveEdit;
            return cellInfo;
        }

        protected override bool OnMouseMove(MouseEventArgs ev) {
            bool res = base.OnMouseMove(ev);
            if(res)
                return res;
            GridCellInfo cellInfo = GetInteractiveEditorCell(ev);
            if(cellInfo == null) {
                if(InteractiveEditor != null)
                    InteractiveEditor.OnMouseLeave();
                return false;
            }
            IInteractiveEdit editor = (IInteractiveEdit)cellInfo.ViewInfo;
            if(editor != InteractiveEditor) {
                if(InteractiveEditor != null)
                    InteractiveEditor.OnMouseLeave();
            }
            BaseEditViewInfo ei = (BaseEditViewInfo)editor;
            InteractiveEditor = editor;
            int dx = cellInfo.Bounds.X - ei.Bounds.X;
            int dy = cellInfo.Bounds.Y - ei.Bounds.Y;
            ei.Offset(dx, dy);
            editor.OnMouseMove(ev);
            ei.Offset(-dx, -dy);
            
            return false;
        }

        protected override bool OnMouseDown(MouseEventArgs ev) {
            GridCellInfo cellInfo = GetInteractiveEditorCell(ev);
            if(cellInfo == null)
                return base.OnMouseDown(ev);
            IInteractiveEdit editor = (IInteractiveEdit)cellInfo.ViewInfo;
            
            BaseEditViewInfo ei = (BaseEditViewInfo)editor;
            int dx = cellInfo.Bounds.X - ei.Bounds.X;
            int dy = cellInfo.Bounds.Y - ei.Bounds.Y;
            ei.Offset(dx, dy);
            DXMouseEventArgs de = new DXMouseEventArgs(ev.Button, ev.Clicks, ev.X, ev.Y, ev.Delta, false);
            editor.OnMouseDown(de);
            ei.Offset(-dx, -dy);
            if(de.Handled)
                return true;
            return base.OnMouseDown(ev);
        }

        protected override bool OnMouseUp(MouseEventArgs ev) {
            GridCellInfo cellInfo = GetInteractiveEditorCell(ev);
            if(cellInfo == null)
                return base.OnMouseUp(ev);
            IInteractiveEdit editor = (IInteractiveEdit)cellInfo.ViewInfo;

            BaseEditViewInfo ei = (BaseEditViewInfo)editor;
            int dx = cellInfo.Bounds.X - ei.Bounds.X;
            int dy = cellInfo.Bounds.Y - ei.Bounds.Y;
            ei.Offset(dx, dy);
            DXMouseEventArgs de = new DXMouseEventArgs(ev.Button, ev.Clicks, ev.X, ev.Y, ev.Delta, false);
            editor.OnMouseUp(de);
            ei.Offset(-dx, -dy);
            if(de.Handled)
                return true;
            return base.OnMouseUp(ev);
        }
    }
}
