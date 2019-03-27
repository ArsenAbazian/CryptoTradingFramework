using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Crypto.Core.Strategies;

namespace CryptoMarketClient.Strategies {
    public partial class StrategySpecificConfigurationControlBase : XtraUserControl {
        public StrategySpecificConfigurationControlBase() {
            InitializeComponent();
        }

        StrategyBase strategy;
        public StrategyBase Strategy {
            get { return strategy; }
            set {
                if(Strategy == value)
                    return;
                strategy = value;
                OnStrategyChanged();
            }
        }
        protected virtual void OnStrategyChanged() {
            
        }
    }
}
