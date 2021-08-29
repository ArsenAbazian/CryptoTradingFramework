using DevExpress.XtraEditors;

namespace CryptoMarketClient {
    partial class ConnectionForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.Sparkline.LineSparklineView lineSparklineView1 = new DevExpress.Sparkline.LineSparklineView();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionForm));
            this.gvSubscriptions = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colchannel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcommand = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRefCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coluserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.socketConnectionInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvConnections = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUpdateExchangeState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateOrderState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateBalanceState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateSummaryState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExchange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLastActiveTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConnectionLostCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colClosedByUser = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConnectionTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSocketType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTicker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAdress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLastError = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKlineInfo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReconnecting = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsOpened = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsOpening = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubsribtions = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMessageCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSparklineEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.teValueWithChange = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.biShowErrors = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.gvSubscriptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.socketConnectionInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvConnections)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teValueWithChange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // gvSubscriptions
            // 
            this.gvSubscriptions.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colchannel,
            this.colcommand,
            this.colRefCount,
            this.colType1,
            this.coluserID});
            this.gvSubscriptions.DetailHeight = 673;
            this.gvSubscriptions.FixedLineWidth = 4;
            this.gvSubscriptions.GridControl = this.gridControl1;
            this.gvSubscriptions.Name = "gvSubscriptions";
            this.gvSubscriptions.OptionsBehavior.Editable = false;
            this.gvSubscriptions.OptionsDetail.DetailMode = DevExpress.XtraGrid.Views.Grid.DetailMode.Embedded;
            this.gvSubscriptions.OptionsDetail.ShowDetailTabs = false;
            this.gvSubscriptions.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colchannel
            // 
            this.colchannel.FieldName = "channel";
            this.colchannel.MinWidth = 40;
            this.colchannel.Name = "colchannel";
            this.colchannel.Visible = true;
            this.colchannel.VisibleIndex = 0;
            this.colchannel.Width = 150;
            // 
            // colcommand
            // 
            this.colcommand.FieldName = "command";
            this.colcommand.MinWidth = 40;
            this.colcommand.Name = "colcommand";
            this.colcommand.Visible = true;
            this.colcommand.VisibleIndex = 1;
            this.colcommand.Width = 150;
            // 
            // colRefCount
            // 
            this.colRefCount.FieldName = "RefCount";
            this.colRefCount.MinWidth = 40;
            this.colRefCount.Name = "colRefCount";
            this.colRefCount.Visible = true;
            this.colRefCount.VisibleIndex = 4;
            this.colRefCount.Width = 150;
            // 
            // colType1
            // 
            this.colType1.FieldName = "Type";
            this.colType1.MinWidth = 40;
            this.colType1.Name = "colType1";
            this.colType1.Visible = true;
            this.colType1.VisibleIndex = 2;
            this.colType1.Width = 150;
            // 
            // coluserID
            // 
            this.coluserID.FieldName = "userID";
            this.coluserID.MinWidth = 40;
            this.coluserID.Name = "coluserID";
            this.coluserID.Visible = true;
            this.coluserID.VisibleIndex = 3;
            this.coluserID.Width = 150;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.socketConnectionInfoBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(24, 23, 24, 23);
            gridLevelNode1.LevelTemplate = this.gvSubscriptions;
            gridLevelNode1.RelationName = "Subscribtions";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl1.Location = new System.Drawing.Point(0, 60);
            this.gridControl1.MainView = this.gvConnections;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(24, 23, 24, 23);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSparklineEdit1,
            this.repositoryItemCheckEdit1,
            this.repositoryItemTextEdit1,
            this.teValueWithChange});
            this.gridControl1.Size = new System.Drawing.Size(1656, 821);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvConnections,
            this.gvSubscriptions});
            // 
            // socketConnectionInfoBindingSource
            // 
            this.socketConnectionInfoBindingSource.DataSource = typeof(Crypto.Core.Common.SocketConnectionInfo);
            // 
            // gvConnections
            // 
            this.gvConnections.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvConnections.Appearance.Row.Options.UseFont = true;
            this.gvConnections.Appearance.SelectedRow.Font = new System.Drawing.Font("Segoe UI", 7.875F);
            this.gvConnections.Appearance.SelectedRow.Options.UseFont = true;
            this.gvConnections.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUpdateExchangeState,
            this.colUpdateOrderState,
            this.colUpdateBalanceState,
            this.colUpdateSummaryState,
            this.colAddress,
            this.colExchange,
            this.colLastActiveTime,
            this.colConnectionLostCount,
            this.colClosedByUser,
            this.colConnectionTime,
            this.colName,
            this.colType,
            this.colSocketType,
            this.colTicker,
            this.colAdress,
            this.colState,
            this.colLastError,
            this.colKlineInfo,
            this.colReconnecting,
            this.colIsOpened,
            this.colIsOpening,
            this.colKey,
            this.colSubsribtions,
            this.colMessageCount});
            this.gvConnections.CustomizationFormBounds = new System.Drawing.Rectangle(973, 497, 520, 446);
            this.gvConnections.DetailHeight = 673;
            this.gvConnections.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gvConnections.GridControl = this.gridControl1;
            this.gvConnections.GroupCount = 1;
            this.gvConnections.Name = "gvConnections";
            this.gvConnections.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.False;
            this.gvConnections.OptionsBehavior.Editable = false;
            this.gvConnections.OptionsDetail.ShowDetailTabs = false;
            this.gvConnections.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvConnections.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.gvConnections.OptionsView.EnableAppearanceEvenRow = true;
            this.gvConnections.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvConnections.OptionsView.ShowIndicator = false;
            this.gvConnections.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvConnections.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colExchange, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvConnections.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvConnections.MasterRowGetChildList += new DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventHandler(this.gvConnections_MasterRowGetChildList);
            this.gvConnections.MasterRowGetRelationName += new DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventHandler(this.gvConnections_MasterRowGetRelationName);
            this.gvConnections.MasterRowGetRelationCount += new DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventHandler(this.gvConnections_MasterRowGetRelationCount);
            // 
            // colUpdateExchangeState
            // 
            this.colUpdateExchangeState.FieldName = "UpdateExchangeState";
            this.colUpdateExchangeState.MinWidth = 40;
            this.colUpdateExchangeState.Name = "colUpdateExchangeState";
            this.colUpdateExchangeState.Width = 150;
            // 
            // colUpdateOrderState
            // 
            this.colUpdateOrderState.FieldName = "UpdateOrderState";
            this.colUpdateOrderState.MinWidth = 40;
            this.colUpdateOrderState.Name = "colUpdateOrderState";
            this.colUpdateOrderState.Width = 150;
            // 
            // colUpdateBalanceState
            // 
            this.colUpdateBalanceState.FieldName = "UpdateBalanceState";
            this.colUpdateBalanceState.MinWidth = 40;
            this.colUpdateBalanceState.Name = "colUpdateBalanceState";
            this.colUpdateBalanceState.Width = 150;
            // 
            // colUpdateSummaryState
            // 
            this.colUpdateSummaryState.FieldName = "UpdateSummaryState";
            this.colUpdateSummaryState.MinWidth = 40;
            this.colUpdateSummaryState.Name = "colUpdateSummaryState";
            this.colUpdateSummaryState.Width = 150;
            // 
            // colAddress
            // 
            this.colAddress.FieldName = "Address";
            this.colAddress.MinWidth = 40;
            this.colAddress.Name = "colAddress";
            this.colAddress.Width = 150;
            // 
            // colExchange
            // 
            this.colExchange.FieldName = "ExchangeType";
            this.colExchange.MinWidth = 40;
            this.colExchange.Name = "colExchange";
            this.colExchange.Visible = true;
            this.colExchange.VisibleIndex = 1;
            this.colExchange.Width = 150;
            // 
            // colLastActiveTime
            // 
            this.colLastActiveTime.DisplayFormat.FormatString = "hh:mm:ss.fff";
            this.colLastActiveTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colLastActiveTime.FieldName = "LastActiveTime";
            this.colLastActiveTime.MinWidth = 40;
            this.colLastActiveTime.Name = "colLastActiveTime";
            this.colLastActiveTime.Visible = true;
            this.colLastActiveTime.VisibleIndex = 6;
            this.colLastActiveTime.Width = 192;
            // 
            // colConnectionLostCount
            // 
            this.colConnectionLostCount.FieldName = "ConnectionLostCount";
            this.colConnectionLostCount.MinWidth = 40;
            this.colConnectionLostCount.Name = "colConnectionLostCount";
            this.colConnectionLostCount.Visible = true;
            this.colConnectionLostCount.VisibleIndex = 7;
            this.colConnectionLostCount.Width = 183;
            // 
            // colClosedByUser
            // 
            this.colClosedByUser.FieldName = "ClosedByUser";
            this.colClosedByUser.MinWidth = 40;
            this.colClosedByUser.Name = "colClosedByUser";
            this.colClosedByUser.Width = 150;
            // 
            // colConnectionTime
            // 
            this.colConnectionTime.FieldName = "ConnectionTime";
            this.colConnectionTime.MinWidth = 40;
            this.colConnectionTime.Name = "colConnectionTime";
            this.colConnectionTime.Visible = true;
            this.colConnectionTime.VisibleIndex = 5;
            this.colConnectionTime.Width = 244;
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.MinWidth = 40;
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 161;
            // 
            // colType
            // 
            this.colType.FieldName = "Type";
            this.colType.MinWidth = 40;
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 2;
            this.colType.Width = 161;
            // 
            // colSocketType
            // 
            this.colSocketType.FieldName = "SocketType";
            this.colSocketType.MinWidth = 40;
            this.colSocketType.Name = "colSocketType";
            this.colSocketType.Width = 150;
            // 
            // colTicker
            // 
            this.colTicker.FieldName = "Ticker";
            this.colTicker.MinWidth = 40;
            this.colTicker.Name = "colTicker";
            this.colTicker.Visible = true;
            this.colTicker.VisibleIndex = 1;
            this.colTicker.Width = 161;
            // 
            // colAdress
            // 
            this.colAdress.FieldName = "Adress";
            this.colAdress.MinWidth = 40;
            this.colAdress.Name = "colAdress";
            this.colAdress.Width = 150;
            // 
            // colState
            // 
            this.colState.FieldName = "State";
            this.colState.MinWidth = 40;
            this.colState.Name = "colState";
            this.colState.OptionsColumn.ReadOnly = true;
            this.colState.Visible = true;
            this.colState.VisibleIndex = 3;
            this.colState.Width = 161;
            // 
            // colLastError
            // 
            this.colLastError.FieldName = "LastError";
            this.colLastError.MinWidth = 40;
            this.colLastError.Name = "colLastError";
            this.colLastError.Visible = true;
            this.colLastError.VisibleIndex = 8;
            this.colLastError.Width = 194;
            // 
            // colKlineInfo
            // 
            this.colKlineInfo.FieldName = "KlineInfo";
            this.colKlineInfo.MinWidth = 40;
            this.colKlineInfo.Name = "colKlineInfo";
            this.colKlineInfo.Width = 150;
            // 
            // colReconnecting
            // 
            this.colReconnecting.FieldName = "Reconnecting";
            this.colReconnecting.MinWidth = 40;
            this.colReconnecting.Name = "colReconnecting";
            this.colReconnecting.Width = 150;
            // 
            // colIsOpened
            // 
            this.colIsOpened.FieldName = "IsOpened";
            this.colIsOpened.MinWidth = 40;
            this.colIsOpened.Name = "colIsOpened";
            this.colIsOpened.OptionsColumn.ReadOnly = true;
            this.colIsOpened.Width = 150;
            // 
            // colIsOpening
            // 
            this.colIsOpening.FieldName = "IsOpening";
            this.colIsOpening.MinWidth = 40;
            this.colIsOpening.Name = "colIsOpening";
            this.colIsOpening.OptionsColumn.ReadOnly = true;
            this.colIsOpening.Width = 150;
            // 
            // colKey
            // 
            this.colKey.FieldName = "Key";
            this.colKey.MinWidth = 40;
            this.colKey.Name = "colKey";
            this.colKey.OptionsColumn.ReadOnly = true;
            this.colKey.Width = 150;
            // 
            // colSubsribtions
            // 
            this.colSubsribtions.FieldName = "Subscribtions";
            this.colSubsribtions.MinWidth = 40;
            this.colSubsribtions.Name = "colSubsribtions";
            this.colSubsribtions.OptionsColumn.ReadOnly = true;
            this.colSubsribtions.Width = 150;
            // 
            // colMessageCount
            // 
            this.colMessageCount.FieldName = "MessageCount";
            this.colMessageCount.MinWidth = 40;
            this.colMessageCount.Name = "colMessageCount";
            this.colMessageCount.Visible = true;
            this.colMessageCount.VisibleIndex = 4;
            this.colMessageCount.Width = 161;
            // 
            // repositoryItemSparklineEdit1
            // 
            this.repositoryItemSparklineEdit1.EditValueMember = "Current";
            this.repositoryItemSparklineEdit1.Name = "repositoryItemSparklineEdit1";
            this.repositoryItemSparklineEdit1.PointValueMember = "Time";
            lineSparklineView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            lineSparklineView1.ScaleFactor = 1F;
            this.repositoryItemSparklineEdit1.View = lineSparklineView1;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.SvgStar2;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            // 
            // teValueWithChange
            // 
            this.teValueWithChange.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.teValueWithChange.AutoHeight = false;
            this.teValueWithChange.Name = "teValueWithChange";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.biShowErrors});
            this.barManager1.MaxItemId = 1;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.biShowErrors)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // biShowErrors
            // 
            this.biShowErrors.Caption = "Show Connection History";
            this.biShowErrors.Id = 0;
            this.biShowErrors.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biShowErrors.ImageOptions.SvgImage")));
            this.biShowErrors.ItemAppearance.Hovered.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.biShowErrors.ItemAppearance.Hovered.Options.UseFont = true;
            this.biShowErrors.ItemAppearance.Normal.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.biShowErrors.ItemAppearance.Normal.Options.UseFont = true;
            this.biShowErrors.ItemAppearance.Pressed.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.biShowErrors.ItemAppearance.Pressed.Options.UseFont = true;
            this.biShowErrors.Name = "biShowErrors";
            this.biShowErrors.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biShowErrors.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biShowErrors_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1656, 60);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 881);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1656, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 60);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 821);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1656, 60);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 821);
            // 
            // ConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1656, 881);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ConnectionForm";
            this.Text = "Active Connections";
            ((System.ComponentModel.ISupportInitialize)(this.gvSubscriptions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.socketConnectionInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvConnections)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teValueWithChange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvConnections;
        private DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit repositoryItemSparklineEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit teValueWithChange;
        private System.Windows.Forms.BindingSource socketConnectionInfoBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateExchangeState;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateOrderState;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateBalanceState;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateSummaryState;
        private DevExpress.XtraGrid.Columns.GridColumn colAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colExchange;
        private DevExpress.XtraGrid.Columns.GridColumn colLastActiveTime;
        private DevExpress.XtraGrid.Columns.GridColumn colClosedByUser;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colSocketType;
        private DevExpress.XtraGrid.Columns.GridColumn colTicker;
        private DevExpress.XtraGrid.Columns.GridColumn colAdress;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colLastError;
        private DevExpress.XtraGrid.Columns.GridColumn colKlineInfo;
        private DevExpress.XtraGrid.Columns.GridColumn colReconnecting;
        private DevExpress.XtraGrid.Columns.GridColumn colIsOpened;
        private DevExpress.XtraGrid.Columns.GridColumn colIsOpening;
        private DevExpress.XtraGrid.Columns.GridColumn colKey;
        private DevExpress.XtraGrid.Columns.GridColumn colSubsribtions;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSubscriptions;
        private DevExpress.XtraGrid.Columns.GridColumn colchannel;
        private DevExpress.XtraGrid.Columns.GridColumn colcommand;
        private DevExpress.XtraGrid.Columns.GridColumn colRefCount;
        private DevExpress.XtraGrid.Columns.GridColumn colType1;
        private DevExpress.XtraGrid.Columns.GridColumn coluserID;
        private DevExpress.XtraGrid.Columns.GridColumn colMessageCount;
        private DevExpress.XtraGrid.Columns.GridColumn colConnectionTime;
        private DevExpress.XtraGrid.Columns.GridColumn colConnectionLostCount;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem biShowErrors;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}