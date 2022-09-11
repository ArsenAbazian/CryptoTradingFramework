using Crypto.Core;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram.Editors;

namespace Workflow.Nodes.Crypto.Editors {
    public class RepositoryItemCandlestickCollectionEditor : RepositoryItemGridLookUpEdit, IPropertyEditor {
        protected virtual void InitItems() {
            PropertyInfo pInfo = Owner.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(ExchangeType));
            if(pInfo == null)
                return;
            ExchangeType exchange = (ExchangeType)pInfo.GetValue(Owner);
            Exchange e = Exchange.Get(exchange);
            if(!e.Connect())
                return;
            View.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            View.OptionsSelection.EnableAppearanceFocusedCell = false;
            View.OptionsSelection.EnableAppearanceFocusedRow = false;
            View.OptionsView.ShowIndicator = false;
            View.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            View.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            View.OptionsView.ShowColumnHeaders = false;
            View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "Text", VisibleIndex = 0, });
            ValueMember = "TotalMinutes";
            DisplayMember = "Text";
            DataSource = e.AllowedCandleStickIntervals;
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

            RepositoryItemCandlestickCollectionEditor ed = item as RepositoryItemCandlestickCollectionEditor;
            if(ed == null)
                return;
            Owner = ed.Owner;
            Property = ed.Property;
            Value = ed.Value;
        }
    }
}
