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
    public class RepositoryItemTickerCollectionEditor : RepositoryItemSearchLookUpEdit, IPropertyEditor {
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
            View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "MarketCurrency", VisibleIndex = 0, });
            View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "BaseCurrency", GroupIndex = 0, VisibleIndex = 1 });
            View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "CurrencyPair", VisibleIndex = 2 });
            View.SortInfo.Add(new GridColumnSortInfo(View.Columns[1], DevExpress.Data.ColumnSortOrder.Ascending));
            ValueMember = "CurrencyPair";
            DisplayMember = "CurrencyPair";
            DataSource = e.Tickers;
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

            RepositoryItemTickerCollectionEditor ed = item as RepositoryItemTickerCollectionEditor;
            if(ed == null)
                return;
            Owner = ed.Owner;
            Property = ed.Property;
            Value = ed.Value;
        }
    }
}
