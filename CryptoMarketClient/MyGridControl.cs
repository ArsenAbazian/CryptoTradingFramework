using CryptoMarketClient.Common;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public class MyGridControl : GridControl {
        public MyGridControl()
            : base() {
                UseDirectXPaint = SettingsStore.Default.UseDirectXForGrid? DevExpress.Utils.DefaultBoolean.True: DevExpress.Utils.DefaultBoolean.False;
        }
    }
}
