using Crypto.Core.Strategies;
using Crypto.Core.Strategies.Base;
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
    public partial class OutputParameterSelection : Form {
        public OutputParameterSelection() {
            InitializeComponent();
            this.repositoryItemImageComboBox1.AddEnum<OutputParameterOptimizationMode>();
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
            if(Strategy == null)
                return;
            Text = Strategy.Name + " Parameters";
            List<InputParameterNode> nodes = InputParametersHelper.GetOutputNodes(Strategy);
            if(Strategy.OutputParameter != null) {
                InputParameterNode node = nodes.FirstOrDefault(n => n.FullName == Strategy.OutputParameter.FieldName);
                if(node != null)
                    node.Selected = true;
            }
            this.treeList1.DataSource = nodes;
            this.treeList1.ExpandAll();
        }

        private void sbCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
        }

        private void sbOk_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e) {
            InputParameterNode node = (InputParameterNode)this.treeList1.GetFocusedRow();
            List<InputParameterNode> nodes = this.treeList1.DataSource as List<InputParameterNode>;
            if(node.Selected)
                nodes.ForEach(n => n.Selected = n == node);
        }
    }
}
