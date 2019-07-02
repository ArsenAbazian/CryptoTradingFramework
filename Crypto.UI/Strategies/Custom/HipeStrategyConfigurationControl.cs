using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptoMarketClient.Strategies.Custom;

namespace Crypto.UI.Strategies.Custom {
    public partial class HipeStrategyConfigurationControl : CustomStrategyConfigurationControl {
        public HipeStrategyConfigurationControl() {
            InitializeComponent();
            this.tickersGridControl.Parent = null;
            this.sidePanelWithGrid.Parent = null;
            this.Controls.Add(this.propertyGridControl1);
            this.bar1.Visible = false;
        }
    }
}
