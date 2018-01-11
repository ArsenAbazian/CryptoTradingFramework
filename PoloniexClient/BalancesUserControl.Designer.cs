namespace CryptoMarketClient {
    partial class BalancesUserControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BalancesUserControl));
            this.gridBalances = new DevExpress.XtraGrid.GridControl();
            this.viewBalances = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTicker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAvailable = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPending = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.chkAutoRefresh = new DevExpress.XtraEditors.CheckButton();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridBalances)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewBalances)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riHyperLinkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridBalances
            // 
            this.gridBalances.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBalances.Location = new System.Drawing.Point(0, 42);
            this.gridBalances.MainView = this.viewBalances;
            this.gridBalances.Name = "gridBalances";
            this.gridBalances.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riHyperLinkEdit1});
            this.gridBalances.Size = new System.Drawing.Size(867, 553);
            this.gridBalances.TabIndex = 1;
            this.gridBalances.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewBalances});
            // 
            // viewBalances
            // 
            this.viewBalances.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTicker,
            this.colCurrencyName,
            this.colBalance,
            this.colAvailable,
            this.colPending});
            this.viewBalances.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.viewBalances.GridControl = this.gridBalances;
            this.viewBalances.Name = "viewBalances";
            this.viewBalances.OptionsBehavior.AlignGroupSummaryInGroupRow = DevExpress.Utils.DefaultBoolean.False;
            this.viewBalances.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.viewBalances.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.viewBalances.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.False;
            this.viewBalances.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.False;
            this.viewBalances.OptionsBehavior.AllowPartialGroups = DevExpress.Utils.DefaultBoolean.False;
            this.viewBalances.OptionsBehavior.AllowValidationErrors = false;
            this.viewBalances.OptionsBehavior.AutoPopulateColumns = false;
            this.viewBalances.OptionsBehavior.AutoSelectAllInEditor = false;
            this.viewBalances.OptionsBehavior.AutoUpdateTotalSummary = false;
            this.viewBalances.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.viewBalances.OptionsCustomization.AllowFilter = false;
            this.viewBalances.OptionsCustomization.AllowGroup = false;
            this.viewBalances.OptionsCustomization.AllowMergedGrouping = DevExpress.Utils.DefaultBoolean.False;
            this.viewBalances.OptionsCustomization.AllowRowSizing = true;
            this.viewBalances.OptionsFilter.AllowColumnMRUFilterList = false;
            this.viewBalances.OptionsFilter.AllowFilterEditor = false;
            this.viewBalances.OptionsFilter.AllowFilterIncrementalSearch = false;
            this.viewBalances.OptionsFind.AllowFindPanel = false;
            this.viewBalances.OptionsMenu.EnableFooterMenu = false;
            this.viewBalances.OptionsMenu.EnableGroupPanelMenu = false;
            this.viewBalances.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.False;
            this.viewBalances.OptionsMenu.ShowAutoFilterRowItem = false;
            this.viewBalances.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.viewBalances.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.viewBalances.OptionsMenu.ShowSplitItem = false;
            this.viewBalances.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.viewBalances.OptionsView.AllowHtmlDrawGroups = false;
            this.viewBalances.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.viewBalances.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.viewBalances.OptionsView.ShowGroupPanel = false;
            this.viewBalances.OptionsView.ShowIndicator = false;
            this.viewBalances.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.viewBalances.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.viewBalances_PopupMenuShowing);
            this.viewBalances.DoubleClick += new System.EventHandler(this.viewBalances_DoubleClick);
            // 
            // colTicker
            // 
            this.colTicker.Caption = "Ticker";
            this.colTicker.ColumnEdit = this.riHyperLinkEdit1;
            this.colTicker.FieldName = "CurrencyTicker";
            this.colTicker.Name = "colTicker";
            this.colTicker.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colTicker.OptionsColumn.AllowIncrementalSearch = false;
            this.colTicker.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colTicker.OptionsColumn.ShowInExpressionEditor = false;
            this.colTicker.Visible = true;
            this.colTicker.VisibleIndex = 0;
            this.colTicker.Width = 144;
            // 
            // riHyperLinkEdit1
            // 
            this.riHyperLinkEdit1.AllowFocused = false;
            this.riHyperLinkEdit1.AutoHeight = false;
            this.riHyperLinkEdit1.Name = "riHyperLinkEdit1";
            this.riHyperLinkEdit1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.riHyperLinkEdit1.SingleClick = true;
            this.riHyperLinkEdit1.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.riHyperLinkEdit1_OpenLink);
            // 
            // colCurrencyName
            // 
            this.colCurrencyName.Caption = "Currency";
            this.colCurrencyName.ColumnEdit = this.riHyperLinkEdit1;
            this.colCurrencyName.FieldName = "CurrencyName";
            this.colCurrencyName.Name = "colCurrencyName";
            this.colCurrencyName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colCurrencyName.OptionsColumn.AllowIncrementalSearch = false;
            this.colCurrencyName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colCurrencyName.OptionsColumn.ShowInExpressionEditor = false;
            this.colCurrencyName.Visible = true;
            this.colCurrencyName.VisibleIndex = 1;
            this.colCurrencyName.Width = 144;
            // 
            // colBalance
            // 
            this.colBalance.Caption = "Balance";
            this.colBalance.DisplayFormat.FormatString = "f8";
            this.colBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBalance.FieldName = "Balance";
            this.colBalance.Name = "colBalance";
            this.colBalance.OptionsColumn.AllowEdit = false;
            this.colBalance.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colBalance.OptionsColumn.AllowIncrementalSearch = false;
            this.colBalance.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colBalance.OptionsColumn.ShowInExpressionEditor = false;
            this.colBalance.Visible = true;
            this.colBalance.VisibleIndex = 2;
            this.colBalance.Width = 144;
            // 
            // colAvailable
            // 
            this.colAvailable.Caption = "Available";
            this.colAvailable.DisplayFormat.FormatString = "f8";
            this.colAvailable.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAvailable.FieldName = "Available";
            this.colAvailable.Name = "colAvailable";
            this.colAvailable.OptionsColumn.AllowEdit = false;
            this.colAvailable.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colAvailable.OptionsColumn.AllowIncrementalSearch = false;
            this.colAvailable.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colAvailable.OptionsColumn.ShowInExpressionEditor = false;
            this.colAvailable.Visible = true;
            this.colAvailable.VisibleIndex = 3;
            this.colAvailable.Width = 144;
            // 
            // colPending
            // 
            this.colPending.Caption = "Pending";
            this.colPending.DisplayFormat.FormatString = "f8";
            this.colPending.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPending.FieldName = "Pending";
            this.colPending.Name = "colPending";
            this.colPending.OptionsColumn.AllowEdit = false;
            this.colPending.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colPending.OptionsColumn.AllowIncrementalSearch = false;
            this.colPending.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colPending.OptionsColumn.ShowInExpressionEditor = false;
            this.colPending.Visible = true;
            this.colPending.VisibleIndex = 4;
            this.colPending.Width = 144;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.chkAutoRefresh);
            this.panelControl1.Controls.Add(this.searchControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new System.Windows.Forms.Padding(3);
            this.panelControl1.Size = new System.Drawing.Size(867, 42);
            this.panelControl1.TabIndex = 2;
            // 
            // chkAutoRefresh
            // 
            this.chkAutoRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAutoRefresh.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.chkAutoRefresh.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.chkAutoRefresh.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("chkAutoRefresh.ImageOptions.SvgImage")));
            this.chkAutoRefresh.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.chkAutoRefresh.Location = new System.Drawing.Point(835, 6);
            this.chkAutoRefresh.Name = "chkAutoRefresh";
            this.chkAutoRefresh.Size = new System.Drawing.Size(29, 30);
            this.chkAutoRefresh.TabIndex = 1;
            this.chkAutoRefresh.CheckedChanged += new System.EventHandler(this.chkAutoRefresh_CheckedChanged);
            // 
            // searchControl1
            // 
            this.searchControl1.Client = this.gridBalances;
            this.searchControl1.Location = new System.Drawing.Point(6, 11);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Properties.Client = this.gridBalances;
            this.searchControl1.Properties.FindDelay = 500;
            this.searchControl1.Properties.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.searchControl1.Size = new System.Drawing.Size(228, 20);
            this.searchControl1.TabIndex = 0;
            // 
            // BalancesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridBalances);
            this.Controls.Add(this.panelControl1);
            this.Name = "BalancesUserControl";
            this.Size = new System.Drawing.Size(867, 595);
            this.Load += new System.EventHandler(this.BalancesUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridBalances)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewBalances)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riHyperLinkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridBalances;
        private DevExpress.XtraGrid.Views.Grid.GridView viewBalances;
        private DevExpress.XtraGrid.Columns.GridColumn colTicker;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyName;
        private DevExpress.XtraGrid.Columns.GridColumn colBalance;
        private DevExpress.XtraGrid.Columns.GridColumn colAvailable;
        private DevExpress.XtraGrid.Columns.GridColumn colPending;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckButton chkAutoRefresh;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit riHyperLinkEdit1;
    }
}
