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
using DevExpress.Data;
using CryptoMarketClient.Common;

namespace CryptoMarketClient {
    public partial class LogMessagesControl : XtraUserControl {
        public LogMessagesControl() {
            InitializeComponent();
        }

        //protected override void OnHandleCreated(EventArgs e) {
        //    base.OnHandleCreated(e);
        //    //this.gcLog.DataSource = new RealTimeSource() { DataSource = LogManager.Default.Messages };
        //}
    }
}
