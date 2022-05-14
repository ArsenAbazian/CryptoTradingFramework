using Crypto.Core.Strategies;
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

namespace Crypto.UI.Strategies {
    public partial class SimulationSettingsForm : XtraForm {
        public SimulationSettingsForm() {
            InitializeComponent();
        }

        public SimulationSettings Settings {
            get {
                if(this.simulationSettingsBindingSource.DataSource is SimulationSettings)
                    return (SimulationSettings)this.simulationSettingsBindingSource.DataSource;
                return null;
            }
            set {
                this.simulationSettingsBindingSource.DataSource = value;
            }
        }
    }
}
