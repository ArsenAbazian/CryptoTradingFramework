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

namespace CryptoMarketClient.Strategies {
    public partial class StrategyValidationErrorsForm : XtraForm {
        public StrategyValidationErrorsForm() {
            InitializeComponent();
        }

        List<StrategyValidationError> errors;
        public List<StrategyValidationError> Errors {
            get { return errors; }
            set {
                if(Errors == value)
                    return;
                errors = value;
                OnErrorsChanged();
            }
        }

        private void OnErrorsChanged() {
            this.strategyValidationErrorBindingSource.DataSource = Errors;
        }
    }
}
