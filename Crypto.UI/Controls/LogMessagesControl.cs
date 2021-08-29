using System;
using DevExpress.XtraEditors;
using DevExpress.Data;
using Crypto.Core.Common;

namespace CryptoMarketClient {
    public partial class LogMessagesControl : XtraUserControl {
        public LogMessagesControl() {
            InitializeComponent();

            
        }
        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            this.gcLog.DataSource = new RealTimeSource() { DataSource = LogManager.Default.Messages };
        }
        public void RefreshData() {
            this.gvLog.RefreshData();
        }
    }
}
