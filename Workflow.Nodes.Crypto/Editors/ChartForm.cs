using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraCharts;
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

namespace Workflow.Nodes.Crypto.Editors {
    public partial class ChartForm : RibbonForm {
        public ChartForm() {
            InitializeComponent();
        }

        public ChartControl ChartControl { get { return this.chartControl1; } }
    }
}
