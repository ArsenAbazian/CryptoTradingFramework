using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.UI.Controls
{
    public class ThreadSafeGridView : GridView
    {
        protected override void MakeRowVisibleCore(int rowHandle, bool invalidate)
        {
            if(GridControl.InvokeRequired)
                return;
            base.MakeRowVisibleCore(rowHandle, invalidate);
        }
    }
}
