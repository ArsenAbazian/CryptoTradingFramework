using CryptoMarketClient.Common;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient {
    public class MyChartControl : ChartControl {
        public MyChartControl()
            : base() {
        }
        protected override void OnHandleCreated(EventArgs e) {
            if(!DesignMode)
                UseDirectXPaint = SettingsStore.Default.UseDirectXForCharts ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
            base.OnHandleCreated(e);
        }
    }
}
