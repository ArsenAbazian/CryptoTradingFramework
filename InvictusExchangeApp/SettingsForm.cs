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

namespace InvictusExchangeApp {
    public partial class SettingsForm : XtraForm {
        public SettingsForm() {
            InitializeComponent();
        }

        InvictusSettings settings;
        public InvictusSettings Settings {
            get { return settings; }
            set {
                if(Settings == value)
                    return;
                settings = value;
                OnSettingsChanged();
            }
        }

        private void OnSettingsChanged() {
            this.invictusSettingsBindingSource.DataSource = Settings;
        }
    }
}
