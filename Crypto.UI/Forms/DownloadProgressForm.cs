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

namespace Crypto.UI.Forms {
    public partial class DownloadProgressForm : XtraForm {
        public DownloadProgressForm() {
            InitializeComponent();
        }

        public event EventHandler Cancel;
        private void sbCancel_Click(object sender, EventArgs e) {
            Close();
            if(Cancel != null)
                Cancel(this, e);
        }
        public void SetProgress(string text, int progress) {
            if(IsDisposed)
                return;
            if(InvokeRequired) {
                BeginInvoke(new MethodInvoker(() => {
                    this.lbCaption.Text = text;
                    this.progressBarControl1.EditValue = progress;
                }));
            }
            else {
                this.lbCaption.Text = text;
                this.progressBarControl1.EditValue = progress;
                Application.DoEvents();
            }
        }
        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            this.layoutControl1.Root.BestFit();
            this.Height = this.layoutControl1.Root.MinSize.Height;
        }
    }
}
