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
    public partial class SimpleSettingsForm : XtraForm {
        public SimpleSettingsForm() {
            InitializeComponent();
        }

        private void tablePanel1_Paint(object sender, PaintEventArgs e) {

        }

        public object SelectedObject {
            get { return this.propertyGridControl1.SelectedObject; }
            set { 
                this.propertyGridControl1.SelectedObject = value;
                OnSelectedObjectChanged();
            }
        }

        protected virtual void OnSelectedObjectChanged() {
            if(this.propertyGridControl1.SelectedObject != null)
                Text = this.propertyGridControl1.SelectedObject.GetType().Name;
        }
    }
}
