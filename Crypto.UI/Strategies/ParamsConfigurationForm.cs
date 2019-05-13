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
            this.inputParameterNodeBindingSource1.DataSource = SelectedNodes;
            Text = Strategy.Name + " Parameters";
            List<InputParameterNode> nodes = InputParametersHelper.GetNodes(Strategy);
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
            Close();
        }

        private void sbOk_Click(object sender, EventArgs e) {
            Strategy.ParametersToOptimize.Clear();
            foreach(InputParameterNode p in SelectedNodes) {
                Strategy.ParametersToOptimize.Add(new InputParameterInfo() { FieldName = p.FullName, PropertyInfo = p.Property });
            }
            if(Strategy.Manager != null)
                Strategy.Manager.Save();
            Close();
        }
    }
}
