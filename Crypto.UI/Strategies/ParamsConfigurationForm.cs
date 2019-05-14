using Crypto.Core.Strategies;
using Crypto.Core.Strategies.Base;
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
    public partial class ParamsConfigurationForm : XtraForm {
        public ParamsConfigurationForm() {
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

        public List<InputParameterNode> SelectedNodes { get; } = new List<InputParameterNode>();

        protected virtual void OnStrategyChanged() {
            if(Strategy == null)
                return;
            ConfigureInput();
            ConfigureOutput();
        }

        private void ConfigureInput() {
            this.inputParameterNodeBindingSource1.DataSource = SelectedNodes;
            Text = Strategy.Name + " Parameters";
            List<InputParameterNode> nodes = InputParametersHelper.GetInputNodes(Strategy);
            foreach(InputParameterInfo p in Strategy.ParametersToOptimize) {
                InputParameterNode node = nodes.FirstOrDefault(n => n.FullName == p.FieldName);
                if(node != null) {
                    node.Selected = true;
                    SelectedNodes.Add(node);
                }
            }
            this.treeList1.DataSource = nodes;
            this.treeList1.ExpandAll();
        }

        private void ConfigureOutput() {
            this.barEditItem1.EditValue = OutputParameterOptimizationMode.Maximize;
            List<InputParameterNode> nodes = InputParametersHelper.GetOutputNodes(Strategy);
            if(Strategy.OutputParameter != null) {
                InputParameterNode node = nodes.FirstOrDefault(n => n.FullName == Strategy.OutputParameter.FieldName);
                if(node != null)
                    node.Selected = true;
            }
            this.treeList2.DataSource = nodes;
            this.treeList2.ExpandAll();
        }

        private void treeList1_CustomNodeCellEdit(object sender, DevExpress.XtraTreeList.GetCustomNodeCellEditEventArgs e) {
            if(e.Column == this.colSelected) {
                if(e.Node.HasChildren)
                    e.RepositoryItem = this.repositoryItemTextEdit1;
            }
        }

        private void treeList1_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e) {
            if(!e.Node.HasChildren)
                e.Appearance.FontStyleDelta = FontStyle.Bold;
        }

        private void repositoryItemTextEdit1_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e) {
            e.DisplayText = "";
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e) {
            if(((CheckEdit)sender).Checked)
                SelectedNodes.Add((InputParameterNode)this.treeList1.GetFocusedRow());
            else
                SelectedNodes.Remove((InputParameterNode)this.treeList1.GetFocusedRow());
            this.treeList1.CloseEditor();
            this.gridView2.RefreshData();
        }

        private void sbCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void sbOk_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Strategy.ParametersToOptimize.Clear();
            foreach(InputParameterNode node in SelectedNodes)
                Strategy.ParametersToOptimize.Add(new InputParameterInfo() { FieldName = node.FullName, PropertyInfo = node.Property });

            InputParameterNode nodeOut = ((List<InputParameterNode>)this.treeList2.DataSource).FirstOrDefault(node => node.Selected);
            if(nodeOut == null) {
                XtraMessageBox.Show("Error: Output parameter not specified.");
                return;
            }
            OutputParameterInfo outInfo = new OutputParameterInfo() { FieldName = nodeOut.FullName, PropertyInfo = nodeOut.Property };
            outInfo.Optimization = (OutputParameterOptimizationMode)this.barEditItem1.EditValue;

            Strategy.OutputParameter = outInfo;
            if(Strategy.Manager != null)
                Strategy.Manager.Save();
            Close();
        }

        private void repositoryItemCheckEdit2_CheckedChanged(object sender, EventArgs e) {
            InputParameterNode node = (InputParameterNode)this.treeList2.GetFocusedRow();
            List<InputParameterNode> nodes = this.treeList2.DataSource as List<InputParameterNode>;
            if(node.Selected)
                nodes.ForEach(n => n.Selected = n == node);
        }

        private void repositoryItemTextEdit3_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e) {
            e.DisplayText = "";
        }

        private void treeList2_CustomNodeCellEdit(object sender, DevExpress.XtraTreeList.GetCustomNodeCellEditEventArgs e) {
            if(e.Column == this.colSelected2 && e.Node.HasChildren)
                e.RepositoryItem = this.repositoryItemTextEdit3;
        }
    }
}
