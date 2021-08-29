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
    public partial class ConnectionForm : TimerUpdateForm {
        public ConnectionForm() {
            InitializeComponent();
            InitializeFormatRulers();
            this.gvConnections.DataController.AllowIEnumerableDetails = true;
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            UpdateConnections();
        }
        protected void InitializeFormatRulers() {
            this.gvConnections.FormatRules.Add(CreateRule(this.colState, SocketConnectionState.Connected, Color.Green));
            this.gvConnections.FormatRules.Add(CreateRule(this.colState, SocketConnectionState.Connecting, Color.Orange));
            this.gvConnections.FormatRules.Add(CreateRule(this.colState, SocketConnectionState.DelayRecv, Color.Red));
            this.gvConnections.FormatRules.Add(CreateRule(this.colState, SocketConnectionState.Disconnected, Color.Red));
            this.gvConnections.FormatRules.Add(CreateRule(this.colState, SocketConnectionState.Disconnecting, Color.Red));
            this.gvConnections.FormatRules.Add(CreateRule(this.colState, SocketConnectionState.Error, Color.Red));
            this.gvConnections.FormatRules.Add(CreateRule(this.colState, SocketConnectionState.TooLongQue, Color.Red));
            this.gvConnections.FormatRules.Add(CreateRule(this.colState, SocketConnectionState.Waiting, Color.Orange));
        }

        private GridFormatRule CreateRule(GridColumn column, SocketConnectionState state, Color color) {
            GridFormatRule rule = new GridFormatRule();
            FormatConditionRuleValue cond = new FormatConditionRuleValue();

            cond.Appearance.FontStyleDelta = FontStyle.Bold;
            cond.Appearance.ForeColor = color;
            cond.Appearance.Font = this.gvConnections.Appearance.Row.Font;
            cond.Condition = FormatCondition.Equal;
            cond.Value1 = state;

            rule.Tag = new object();
            rule.Name = "fmt" + state.ToString();
            rule.Column = column;
            rule.ColumnApplyTo = column;
            rule.Rule = cond;
            return rule;
        }

        protected override int UpdateInervalMs => 3000;
        protected void UpdateConnections() {
            BindingList<SocketConnectionInfo> activeConnections = new BindingList<SocketConnectionInfo>();
            foreach(Exchange ex in Exchange.Connected) {
                activeConnections.Add(ex.TickersSocket);
                foreach(SocketConnectionInfo si in ex.OrderBookSockets)
                    activeConnections.Add(si);
                foreach(SocketConnectionInfo si in ex.TradeHistorySockets)
                    activeConnections.Add(si);
                foreach(SocketConnectionInfo si in ex.KlineSockets)
                    activeConnections.Add(si);
            }
            this.socketConnectionInfoBindingSource.DataSource = activeConnections;
            this.gvConnections.ExpandAllGroups();
        }
        protected override void OnTimerUpdate(object sender, EventArgs e) {
            base.OnTimerUpdate(sender, e);
            this.gvConnections.RefreshData();
        }
        protected override void OnActivated(EventArgs e) {
            base.OnActivated(e);
            UpdateConnections();
        }

        private void gvConnections_MasterRowGetChildList(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs e) {
            if(e.RowHandle < 0)
                return;
            SocketConnectionInfo info = (SocketConnectionInfo)this.gvConnections.GetRow(e.RowHandle);
            e.ChildList = info.Subscribtions;
        }

        private void gvConnections_MasterRowGetRelationCount(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs e) {
            e.RelationCount = 1;
        }

        private void gvConnections_MasterRowGetRelationName(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs e) {
            e.RelationName = "Subscribtions";
        }

        private void biShowErrors_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            SocketConnectionInfo info = (SocketConnectionInfo)this.gvConnections.GetRow(this.gvConnections.FocusedRowHandle);
            if(info == null)
                return;
            using(ConnectionErrorHistoryForm form = new ConnectionErrorHistoryForm()) {
                form.SocketInfo = info;
                form.ShowDialog();
            }
        }
    }
}
