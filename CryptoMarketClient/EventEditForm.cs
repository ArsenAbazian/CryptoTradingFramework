using Crypto.Core.Common;
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
    public partial class EventEditForm : XtraForm {
        public EventEditForm() {
            InitializeComponent();
        }

        public TickerEvent Event {
            get { return this.dataLayoutControl1.DataSource as TickerEvent; }
            set { this.dataLayoutControl1.DataSource = value; }
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            if(Event == null)
                return;
            if(Event.Color == Color.Empty) {
                XtraMessageBox.Show("Please specify Color", "Event");
                return;
            }
            if(string.IsNullOrEmpty(Text)) {
                XtraMessageBox.Show("Please specify Text", "Event");
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
            return;
        }
    }
}
