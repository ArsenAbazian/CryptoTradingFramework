using Crypto.Core;
using Crypto.Core.Common;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public partial class ConnectionErrorHistoryForm : XtraForm {
        public ConnectionErrorHistoryForm() {
            InitializeComponent();

            this.gridView1.FormatRules.Add(CreateRule(this.colState, SocketConnectionState.Connected, Color.Green));
            this.gridView1.FormatRules.Add(CreateRule(this.colState, SocketConnectionState.Connecting, Color.Orange));
            this.gridView1.FormatRules.Add(CreateRule(this.colState, SocketConnectionState.DelayRecv, Color.Red));
            this.gridView1.FormatRules.Add(CreateRule(this.colState, SocketConnectionState.Disconnected, Color.Red));
            this.gridView1.FormatRules.Add(CreateRule(this.colState, SocketConnectionState.Disconnecting, Color.Red));
            this.gridView1.FormatRules.Add(CreateRule(this.colState, SocketConnectionState.Error, Color.Red));
            this.gridView1.FormatRules.Add(CreateRule(this.colState, SocketConnectionState.TooLongQue, Color.Red));
            this.gridView1.FormatRules.Add(CreateRule(this.colState, SocketConnectionState.Waiting, Color.Orange));
        }

        private GridFormatRule CreateRule(GridColumn column, SocketConnectionState state, Color color) {
            GridFormatRule rule = new GridFormatRule();
            FormatConditionRuleValue cond = new FormatConditionRuleValue();

            cond.Appearance.FontStyleDelta = FontStyle.Bold;
            cond.Appearance.ForeColor = color;
            cond.Appearance.Font = this.gridView1.Appearance.Row.Font;
            cond.Condition = FormatCondition.Equal;
            cond.Value1 = state;

            rule.Tag = new object();
            rule.Name = "fmt" + state.ToString();
            rule.Column = column;
            rule.ColumnApplyTo = column;
            rule.Rule = cond;
            return rule;
        }

        SocketConnectionInfo socketInfo;
        public SocketConnectionInfo SocketInfo {
            get { return socketInfo; }
            set {
                if(SocketInfo == value)
                    return;
                socketInfo = value;
                OnSocketInfoChanged();
            }
        }

        private void OnSocketInfoChanged() {
            this.socketInfoHistoryItemBindingSource.DataSource = SocketInfo.History;
        }
    }
}
