using CryptoMarketClient.Common;
using DevExpress.Utils.DirectXPaint;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public class MyChartControl : ChartControl {
        public MyChartControl()
            : base() {
        }
        protected override void OnHandleCreated(EventArgs e) {
            if(!DesignMode) {
                ((IDirectXClient)this).UseDirectXPaint = SettingsStore.Default.UseDirectXForCharts;
            }
            base.OnHandleCreated(e);
        }
        protected override void OnPaint(PaintEventArgs e) {
            //if(DataSource != null) {
            //    lock(DataSource) {
            //        base.OnPaint(e);
            //    }
            //}
            //else {
                base.OnPaint(e);
            //}
        }
        protected override bool DisableCustomDrawEventsOptimization => false;
    }
}
