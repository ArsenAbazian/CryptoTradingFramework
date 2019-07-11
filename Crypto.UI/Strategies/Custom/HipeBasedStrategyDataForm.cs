using Crypto.Core.Strategies.Custom;
using CryptoMarketClient.Strategies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crypto.UI.Strategies.Custom {
    public partial class HipeBasedStrategyDataForm : StrategyDataForm {
        public HipeBasedStrategyDataForm() {
            InitializeComponent();
        }
        protected override void OnStrategyChanged() {
            base.OnStrategyChanged();
            ShowTableForm(new HipeBasedStrategyItemsInfoOwner((HipeBasedStrategy)Strategy));
        }
    }
}
