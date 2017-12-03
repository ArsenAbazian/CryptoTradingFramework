using CryptoMarketClient.Common;
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
    public partial class TrailingSettinsForm : Form {
        public TrailingSettinsForm() {
            InitializeComponent();
        }

        TrailingSettings settings;
        public TrailingSettings Settings {
            get { return settings; }
            set {
                if(Settings == value)
                    return;
                settings = value;
                OnSettingsChanged();
            }
        }
        void OnSettingsChanged() {
            this.trailingSettingsBindingSource.DataSource = Settings;
        }
    }
}
