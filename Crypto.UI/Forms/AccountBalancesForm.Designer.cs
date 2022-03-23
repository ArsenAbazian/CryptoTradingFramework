using DevExpress.XtraGrid.Views.Base;
using System;

namespace Crypto.Core.Common {
    partial class AccountBalancesForm {
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet1 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet1 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon1 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon2 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon3 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet2 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet2 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon4 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon5 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon6 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule3 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet3 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet3 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon7 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon8 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon9 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule4 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleDataBar formatConditionRuleDataBar1 = new DevExpress.XtraEditors.FormatConditionRuleDataBar();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountBalancesForm));
            this.colUsdtValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.poloniexAccountBalanceInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAvailable = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOnOrders = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBtcValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExchange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepositAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepositTag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNonZero = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUsdtPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gcMethod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.bcShowNonZero = new DevExpress.XtraBars.BarCheckItem();
            this.bcDeposites = new DevExpress.XtraBars.BarCheckItem();
            this.bsiStatus = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.biUpdateDeposite = new DevExpress.XtraBars.BarButtonItem();
            this.biUpdateBalance = new DevExpress.XtraBars.BarButtonItem();
            this.biCreateDeposit = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.poloniexAccountBalanceInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // colUsdtValue
            // 
            this.colUsdtValue.DisplayFormat.FormatString = "0.00";
            this.colUsdtValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colUsdtValue.FieldName = "UsdtValue";
            this.colUsdtValue.MinWidth = 16;
            this.colUsdtValue.Name = "colUsdtValue";
            this.colUsdtValue.OptionsColumn.AllowEdit = false;
            this.colUsdtValue.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "UsdtValue", "SUM={0:0.00000000}")});
            this.colUsdtValue.Visible = true;
            this.colUsdtValue.VisibleIndex = 7;
            this.colUsdtValue.Width = 246;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.poloniexAccountBalanceInfoBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(0);
            this.gridControl1.Location = new System.Drawing.Point(0, 34);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2,
            this.repositoryItemLookUpEdit1,
            this.repositoryItemComboBox1});
            this.gridControl1.Size = new System.Drawing.Size(1940, 804);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Click += new System.EventHandler(this.gridControl1_Click);
            this.gridControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseUp);
            // 
            // poloniexAccountBalanceInfoBindingSource
            // 
            this.poloniexAccountBalanceInfoBindingSource.DataSource = typeof(Crypto.Core.Common.BalanceBase);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.GroupPanel.Font = new System.Drawing.Font("Segoe UI", 10.125F);
            this.gridView1.Appearance.GroupPanel.Options.UseFont = true;
            this.gridView1.Appearance.GroupRow.Font = new System.Drawing.Font("Segoe UI", 10.125F);
            this.gridView1.Appearance.GroupRow.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI", 10.125F);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 10.125F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.ColumnPanelRowHeight = 0;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCurrency,
            this.colBalance,
            this.colAvailable,
            this.colOnOrders,
            this.colBtcValue,
            this.colExchange,
            this.colDepositAddress,
            this.colDepositTag,
            this.colNonZero,
            this.colAccount,
            this.colUsdtPrice,
            this.colUsdtValue,
            this.gcMethod});
            this.gridView1.DetailHeight = 143;
            this.gridView1.FixedLineWidth = 1;
            this.gridView1.FooterPanelHeight = 0;
            gridFormatRule1.Name = "FormatRulePercentChange";
            formatConditionIconSet1.CategoryName = "Positive/Negative";
            formatConditionIconSetIcon1.PredefinedName = "Arrows3_3.png";
            formatConditionIconSetIcon1.Value = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            formatConditionIconSetIcon1.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon2.PredefinedName = "Arrows3_2.png";
            formatConditionIconSetIcon2.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon3.PredefinedName = "Arrows3_1.png";
            formatConditionIconSet1.Icons.Add(formatConditionIconSetIcon1);
            formatConditionIconSet1.Icons.Add(formatConditionIconSetIcon2);
            formatConditionIconSet1.Icons.Add(formatConditionIconSetIcon3);
            formatConditionIconSet1.Name = "PositiveNegativeArrows";
            formatConditionIconSet1.ValueType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            formatConditionRuleIconSet1.IconSet = formatConditionIconSet1;
            gridFormatRule1.Rule = formatConditionRuleIconSet1;
            gridFormatRule2.Name = "FormatRuleBidDelta";
            formatConditionIconSet2.CategoryName = "Positive/Negative";
            formatConditionIconSetIcon4.PredefinedName = "Arrows3_3.png";
            formatConditionIconSetIcon4.Value = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            formatConditionIconSetIcon4.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon5.PredefinedName = "Arrows3_2.png";
            formatConditionIconSetIcon5.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon6.PredefinedName = "Arrows3_1.png";
            formatConditionIconSet2.Icons.Add(formatConditionIconSetIcon4);
            formatConditionIconSet2.Icons.Add(formatConditionIconSetIcon5);
            formatConditionIconSet2.Icons.Add(formatConditionIconSetIcon6);
            formatConditionIconSet2.Name = "PositiveNegativeArrows";
            formatConditionIconSet2.ValueType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            formatConditionRuleIconSet2.IconSet = formatConditionIconSet2;
            gridFormatRule2.Rule = formatConditionRuleIconSet2;
            gridFormatRule3.Name = "FormatRuleAskDelta";
            formatConditionIconSet3.CategoryName = "Positive/Negative";
            formatConditionIconSetIcon7.PredefinedName = "Arrows3_3.png";
            formatConditionIconSetIcon7.Value = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            formatConditionIconSetIcon7.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon8.PredefinedName = "Arrows3_2.png";
            formatConditionIconSetIcon8.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon9.PredefinedName = "Arrows3_1.png";
            formatConditionIconSet3.Icons.Add(formatConditionIconSetIcon7);
            formatConditionIconSet3.Icons.Add(formatConditionIconSetIcon8);
            formatConditionIconSet3.Icons.Add(formatConditionIconSetIcon9);
            formatConditionIconSet3.Name = "PositiveNegativeArrows";
            formatConditionIconSet3.ValueType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            formatConditionRuleIconSet3.IconSet = formatConditionIconSet3;
            gridFormatRule3.Rule = formatConditionRuleIconSet3;
            gridFormatRule4.Column = this.colUsdtValue;
            gridFormatRule4.ColumnApplyTo = this.colUsdtValue;
            gridFormatRule4.Name = "Format0";
            formatConditionRuleDataBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            formatConditionRuleDataBar1.Appearance.Options.UseBackColor = true;
            formatConditionRuleDataBar1.PredefinedName = null;
            formatConditionRuleDataBar1.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            gridFormatRule4.Rule = formatConditionRuleDataBar1;
            this.gridView1.FormatRules.Add(gridFormatRule1);
            this.gridView1.FormatRules.Add(gridFormatRule2);
            this.gridView1.FormatRules.Add(gridFormatRule3);
            this.gridView1.FormatRules.Add(gridFormatRule4);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupCount = 1;
            this.gridView1.GroupRowHeight = 0;
            this.gridView1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BtcValue", this.colBtcValue, "Total {0:0.00000000}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "UsdtValue", this.colUsdtValue, "Total {0:0.00}")});
            this.gridView1.LevelIndent = 0;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.PreviewIndent = 0;
            this.gridView1.RowHeight = 0;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colExchange, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView1.ViewCaptionHeight = 0;
            this.gridView1.ShownEditor += new System.EventHandler(this.gridView1_ShownEditor);
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            this.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.OnAccountBalancesCustomColumnDisplayText);
            // 
            // colCurrency
            // 
            this.colCurrency.ColumnEdit = this.repositoryItemTextEdit1;
            this.colCurrency.FieldName = "DisplayName";
            this.colCurrency.MinWidth = 10;
            this.colCurrency.Name = "colCurrency";
            this.colCurrency.OptionsColumn.AllowEdit = false;
            this.colCurrency.Visible = true;
            this.colCurrency.VisibleIndex = 0;
            this.colCurrency.Width = 133;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            // 
            // colBalance
            // 
            this.colBalance.DisplayFormat.FormatString = "0.00000000";
            this.colBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBalance.FieldName = "Balance";
            this.colBalance.MinWidth = 40;
            this.colBalance.Name = "colBalance";
            this.colBalance.OptionsColumn.AllowEdit = false;
            this.colBalance.Visible = true;
            this.colBalance.VisibleIndex = 2;
            this.colBalance.Width = 150;
            // 
            // colAvailable
            // 
            this.colAvailable.DisplayFormat.FormatString = "0.00000000";
            this.colAvailable.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAvailable.FieldName = "Available";
            this.colAvailable.MinWidth = 10;
            this.colAvailable.Name = "colAvailable";
            this.colAvailable.OptionsColumn.AllowEdit = false;
            this.colAvailable.Visible = true;
            this.colAvailable.VisibleIndex = 3;
            this.colAvailable.Width = 133;
            // 
            // colOnOrders
            // 
            this.colOnOrders.DisplayFormat.FormatString = "0.00000000";
            this.colOnOrders.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colOnOrders.FieldName = "OnOrders";
            this.colOnOrders.MinWidth = 10;
            this.colOnOrders.Name = "colOnOrders";
            this.colOnOrders.OptionsColumn.AllowEdit = false;
            this.colOnOrders.Visible = true;
            this.colOnOrders.VisibleIndex = 4;
            this.colOnOrders.Width = 133;
            // 
            // colBtcValue
            // 
            this.colBtcValue.DisplayFormat.FormatString = "0.00000000";
            this.colBtcValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBtcValue.FieldName = "BtcValue";
            this.colBtcValue.MinWidth = 10;
            this.colBtcValue.Name = "colBtcValue";
            this.colBtcValue.OptionsColumn.AllowEdit = false;
            this.colBtcValue.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BtcValue", "SUM={0:0.00000000}")});
            this.colBtcValue.Visible = true;
            this.colBtcValue.VisibleIndex = 5;
            this.colBtcValue.Width = 133;
            // 
            // colExchange
            // 
            this.colExchange.FieldName = "Exchange";
            this.colExchange.MinWidth = 14;
            this.colExchange.Name = "colExchange";
            this.colExchange.Visible = true;
            this.colExchange.VisibleIndex = 0;
            this.colExchange.Width = 98;
            // 
            // colDepositAddress
            // 
            this.colDepositAddress.FieldName = "DepositAddress";
            this.colDepositAddress.MinWidth = 10;
            this.colDepositAddress.Name = "colDepositAddress";
            this.colDepositAddress.OptionsColumn.AllowEdit = false;
            this.colDepositAddress.Visible = true;
            this.colDepositAddress.VisibleIndex = 8;
            this.colDepositAddress.Width = 335;
            // 
            // colDepositTag
            // 
            this.colDepositTag.Caption = "Dest. Tag";
            this.colDepositTag.FieldName = "DepositTag";
            this.colDepositTag.MinWidth = 40;
            this.colDepositTag.Name = "colDepositTag";
            this.colDepositTag.OptionsColumn.AllowEdit = false;
            this.colDepositTag.Visible = true;
            this.colDepositTag.VisibleIndex = 9;
            this.colDepositTag.Width = 150;
            // 
            // colNonZero
            // 
            this.colNonZero.FieldName = "NonZero";
            this.colNonZero.MinWidth = 10;
            this.colNonZero.Name = "colNonZero";
            this.colNonZero.Width = 26;
            // 
            // colAccount
            // 
            this.colAccount.FieldName = "Account";
            this.colAccount.MinWidth = 16;
            this.colAccount.Name = "colAccount";
            this.colAccount.OptionsColumn.AllowEdit = false;
            this.colAccount.Visible = true;
            this.colAccount.VisibleIndex = 1;
            this.colAccount.Width = 116;
            // 
            // colUsdtPrice
            // 
            this.colUsdtPrice.ColumnEdit = this.repositoryItemTextEdit2;
            this.colUsdtPrice.DisplayFormat.FormatString = "0.00000000";
            this.colUsdtPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colUsdtPrice.FieldName = "UsdtPrice";
            this.colUsdtPrice.MinWidth = 40;
            this.colUsdtPrice.Name = "colUsdtPrice";
            this.colUsdtPrice.OptionsColumn.AllowEdit = false;
            this.colUsdtPrice.Visible = true;
            this.colUsdtPrice.VisibleIndex = 6;
            this.colUsdtPrice.Width = 164;
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // gcMethod
            // 
            this.gcMethod.Caption = "Deposit Method";
            this.gcMethod.ColumnEdit = this.repositoryItemComboBox1;
            this.gcMethod.FieldName = "CurrentMethod";
            this.gcMethod.MinWidth = 40;
            this.gcMethod.Name = "gcMethod";
            this.gcMethod.Visible = true;
            this.gcMethod.VisibleIndex = 10;
            this.gcMethod.Width = 150;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            this.repositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            // 
            // bcShowNonZero
            // 
            this.bcShowNonZero.BindableChecked = true;
            this.bcShowNonZero.Caption = "Show Non Zero Balances";
            this.bcShowNonZero.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.bcShowNonZero.Checked = true;
            this.bcShowNonZero.Id = 0;
            this.bcShowNonZero.Name = "bcShowNonZero";
            this.bcShowNonZero.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bcShowNonZero_CheckedChanged);
            // 
            // bcDeposites
            // 
            this.bcDeposites.BindableChecked = true;
            this.bcDeposites.Caption = "Update Deposites";
            this.bcDeposites.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.bcDeposites.Checked = true;
            this.bcDeposites.Id = 2;
            this.bcDeposites.Name = "bcDeposites";
            // 
            // bsiStatus
            // 
            this.bsiStatus.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.bsiStatus.Id = 1;
            this.bsiStatus.MergeType = DevExpress.XtraBars.BarMenuMerge.Replace;
            this.bsiStatus.Name = "bsiStatus";
            this.bsiStatus.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bsiStatus_ItemClick);
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.Controller = this.barAndDockingController1;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.bsiStatus,
            this.biUpdateDeposite,
            this.biUpdateBalance,
            this.biCreateDeposit});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 4;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.OfficeUniversal;
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl1.Size = new System.Drawing.Size(1940, 0);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // barAndDockingController1
            // 
            this.barAndDockingController1.AppearancesRibbon.Item.FontSizeDelta = 1;
            this.barAndDockingController1.AppearancesRibbon.Item.Options.UseFont = true;
            this.barAndDockingController1.PropertiesBar.AllowLinkLighting = false;
            // 
            // biUpdateDeposite
            // 
            this.biUpdateDeposite.Caption = "Get Deposite Address";
            this.biUpdateDeposite.Id = 1;
            this.biUpdateDeposite.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biUpdateDeposite.ImageOptions.SvgImage")));
            this.biUpdateDeposite.Name = "biUpdateDeposite";
            this.biUpdateDeposite.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biUpdateDeposite_ItemClick);
            // 
            // biUpdateBalance
            // 
            this.biUpdateBalance.Caption = "Update Balance";
            this.biUpdateBalance.Id = 2;
            this.biUpdateBalance.Name = "biUpdateBalance";
            this.biUpdateBalance.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.biUpdateBalance.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biUpdateBalance_ItemClick);
            // 
            // biCreateDeposit
            // 
            this.biCreateDeposit.Caption = "Create Deposit Address";
            this.biCreateDeposit.Id = 3;
            this.biCreateDeposit.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biCreateDeposit.ImageOptions.SvgImage")));
            this.biCreateDeposit.Name = "biCreateDeposit";
            this.biCreateDeposit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biCreateDeposit_ItemClick);
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.ItemLinks.Add(this.bsiStatus);
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 838);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(1940, 33);
            // 
            // popupMenu1
            // 
            this.popupMenu1.ItemLinks.Add(this.biUpdateDeposite);
            this.popupMenu1.ItemLinks.Add(this.biCreateDeposit);
            this.popupMenu1.ItemLinks.Add(this.biUpdateBalance);
            this.popupMenu1.Name = "popupMenu1";
            this.popupMenu1.Ribbon = this.ribbonControl1;
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.AutoSize = true;
            this.standaloneBarDockControl1.CausesValidation = false;
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(0, 0);
            this.standaloneBarDockControl1.Manager = this.barManager1;
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(1940, 34);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.Controller = this.barAndDockingController1;
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl1);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bcShowNonZero,
            this.bcDeposites});
            // 
            // bar1
            // 
            this.bar1.BarAppearance.Hovered.FontSizeDelta = 2;
            this.bar1.BarAppearance.Hovered.Options.UseFont = true;
            this.bar1.BarAppearance.Normal.FontSizeDelta = 2;
            this.bar1.BarAppearance.Normal.Options.UseFont = true;
            this.bar1.BarAppearance.Pressed.FontSizeDelta = 2;
            this.bar1.BarAppearance.Pressed.Options.UseFont = true;
            this.bar1.BarItemVertIndent = 6;
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar1.FloatLocation = new System.Drawing.Point(113, 322);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bcShowNonZero),
            new DevExpress.XtraBars.LinkPersistInfo(this.bcDeposites)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar1.Text = "Tools";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1940, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 871);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1940, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 871);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1940, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 871);
            // 
            // AccountBalancesForm
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1940, 871);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.standaloneBarDockControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.Name = "AccountBalancesForm";
            this.Text = "Poloniex Account Balances";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.poloniexAccountBalanceInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource poloniexAccountBalanceInfoBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colAvailable;
        private DevExpress.XtraGrid.Columns.GridColumn colOnOrders;
        private DevExpress.XtraGrid.Columns.GridColumn colBtcValue;
        private DevExpress.XtraGrid.Columns.GridColumn colDepositAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colNonZero;
        private DevExpress.XtraBars.BarCheckItem bcShowNonZero;
        private DevExpress.XtraBars.BarButtonItem bsiStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colAccount;
        private DevExpress.XtraGrid.Columns.GridColumn colUsdtValue;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colExchange;
        private DevExpress.XtraBars.BarCheckItem bcDeposites;
        private DevExpress.XtraGrid.Columns.GridColumn colUsdtPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colBalance;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.BarButtonItem biUpdateDeposite;
        private DevExpress.XtraBars.BarButtonItem biUpdateBalance;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.Columns.GridColumn colDepositTag;
        private DevExpress.XtraBars.BarButtonItem biCreateDeposit;
        private DevExpress.XtraGrid.Columns.GridColumn gcMethod;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
    }
}