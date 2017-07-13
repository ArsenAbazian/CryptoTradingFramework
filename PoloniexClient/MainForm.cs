using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WampSharp.V2;
using WampSharp.V2.PubSub;
using WampSharp.V2.Realm;
using System.Reactive.Subjects;
using System.Diagnostics;
using DevExpress.XtraWaitForm;

namespace PoloniexClient {
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm {
        

        public MainForm() {
            InitializeComponent();
        }

        TickersForm tickersForm;
        public TickersForm TickersForm {
            get {
                if(tickersForm == null) {
                    tickersForm = new TickersForm();
                    tickersForm.MdiParent = this;
                }
                return tickersForm;
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TickersForm.Show();
        }
    }
}
