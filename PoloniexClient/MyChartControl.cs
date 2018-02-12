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
                UseDirectXPaint = SettingsStore.Default.UseDirectXForGrid ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
        }
    }
}
