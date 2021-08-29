using Crypto.Core.Common;
using Crypto.Core.Helpers;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public partial class LogForm : XtraForm {
        public LogForm() {
            InitializeComponent();
        }
        public ResizeableArray<LogMessage> Messages {
            get { return (ResizeableArray<LogMessage>)this.logMessageBindingSource.DataSource; }
            set { this.logMessageBindingSource.DataSource = value; }
        }
        public void RefreshData() {
            this.gridControl.RefreshDataSource();
        }
    }
}
