using Crypto.Core;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram;
using WorkflowDiagram.Editors;

namespace Workflow.Nodes.Crypto.Editors {
    public class RepositoryItemAccountCollectionEditor : RepositoryItemSearchLookUpEdit, IPropertyEditor {
        protected virtual void InitItems() {
            View.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            View.OptionsSelection.EnableAppearanceFocusedCell = false;
            View.OptionsSelection.EnableAppearanceFocusedRow = false;
            View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "Name", VisibleIndex = 0, });
            View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "Type", GroupIndex = 0, VisibleIndex = 1 });
            View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "FullName", VisibleIndex = -1, });
            View.SortInfo.Add(new GridColumnSortInfo(View.Columns[1], DevExpress.Data.ColumnSortOrder.Ascending));
            ValueMember = "Id";
            DisplayMember = "FullName";
            DataSource = Exchange.GetAccounts();

            CustomDisplayText += RepositoryItemAccountCollectionEditor_CustomDisplayText;
        }

        private void RepositoryItemAccountCollectionEditor_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e) {
            if(object.Equals(e.Value, Guid.Empty))
                e.DisplayText = "Not Specified";
        }

        protected object Owner { get; set; }
        protected string Property { get; set; }
        protected object Value { get; set; }
        void IPropertyEditor.Initialize(object owner, string property, object value) {
            Owner = owner;
            Property = property;
            Value = value;
            InitItems();
        }

        public override void Assign(RepositoryItem item) {
            base.Assign(item);

            RepositoryItemAccountCollectionEditor ed = item as RepositoryItemAccountCollectionEditor;
            if(ed == null)
                return;
            Owner = ed.Owner;
            Property = ed.Property;
            Value = ed.Value;
        }
    }
}
