using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;

namespace CryptoMarketClient.Forms.Instruments {
    partial class WalletInvestorDataForm {
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
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue2 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WalletInvestorDataForm));
            this.colRise = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChange24 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.walletInvestorDataItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLastPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colListedOnBinance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colListedOnPoloniex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMarketCap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.biRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.biForecast = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.siStatus = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.biForecastSelected = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu2 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.colForecast7Day = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colForecast14Day = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colForecast3Month = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.walletInvestorDataItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).BeginInit();
            this.SuspendLayout();
            // 
            // colRise
            // 
            this.colRise.FieldName = "Rise";
            this.colRise.MinWidth = 40;
            this.colRise.Name = "colRise";
            this.colRise.Width = 150;
            // 
            // colChange24
            // 
            this.colChange24.FieldName = "Change24";
            this.colChange24.MinWidth = 40;
            this.colChange24.Name = "colChange24";
            this.colChange24.Visible = true;
            this.colChange24.VisibleIndex = 1;
            this.colChange24.Width = 150;
            // 
            // gridControl
            // 
            this.gridControl.DataSource = this.walletInvestorDataItemBindingSource;
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.gridControl.Location = new System.Drawing.Point(0, 60);
            this.gridControl.MainView = this.gridView1;
            this.gridControl.Margin = new System.Windows.Forms.Padding(6);
            this.gridControl.Name = "gridControl";
            this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gridControl.Size = new System.Drawing.Size(1440, 803);
            this.gridControl.TabIndex = 2;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // walletInvestorDataItemBindingSource
            // 
            this.walletInvestorDataItemBindingSource.DataSource = typeof(CryptoMarketClient.Forms.Instruments.WalletInvestorDataItem);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colName,
            this.colLastPrice,
            this.colChange24,
            this.colRise,
            this.colVolume,
            this.colListedOnBinance,
            this.colListedOnPoloniex,
            this.colMarketCap,
            this.colForecast7Day,
            this.colForecast14Day,
            this.colForecast3Month});
            this.gridView1.DetailHeight = 673;
            this.gridView1.FixedLineWidth = 4;
            gridFormatRule1.Column = this.colRise;
            gridFormatRule1.ColumnApplyTo = this.colChange24;
            gridFormatRule1.Name = "Rise";
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.PredefinedName = "Green Bold Text";
            formatConditionRuleValue1.Value1 = true;
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.Column = this.colRise;
            gridFormatRule2.ColumnApplyTo = this.colChange24;
            gridFormatRule2.Name = "Fall";
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue2.PredefinedName = "Red Bold Text";
            formatConditionRuleValue2.Value1 = false;
            gridFormatRule2.Rule = formatConditionRuleValue2;
            this.gridView1.FormatRules.Add(gridFormatRule1);
            this.gridView1.FormatRules.Add(gridFormatRule2);
            this.gridView1.GridControl = this.gridControl;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colChange24, DevExpress.Data.ColumnSortOrder.Descending)});
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.MinWidth = 40;
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 150;
            // 
            // colLastPrice
            // 
            this.colLastPrice.FieldName = "LastPrice";
            this.colLastPrice.MinWidth = 40;
            this.colLastPrice.Name = "colLastPrice";
            this.colLastPrice.Visible = true;
            this.colLastPrice.VisibleIndex = 4;
            this.colLastPrice.Width = 150;
            this.colLastPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colLastPrice.DisplayFormat.FormatString = "0.########";
            // 
            // colVolume
            // 
            this.colVolume.FieldName = "Volume";
            this.colVolume.MinWidth = 40;
            this.colVolume.Name = "colVolume";
            this.colVolume.Visible = false;
            this.colVolume.VisibleIndex = -1;
            this.colVolume.Width = 150;
            // 
            // colListedOnBinance
            // 
            this.colListedOnBinance.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colListedOnBinance.FieldName = "ListedOnBinance";
            this.colListedOnBinance.MinWidth = 40;
            this.colListedOnBinance.Name = "colListedOnBinance";
            this.colListedOnBinance.Visible = true;
            this.colListedOnBinance.VisibleIndex = 10;
            this.colListedOnBinance.Width = 150;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.SvgFlag1;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // colListedOnPoloniex
            // 
            this.colListedOnPoloniex.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colListedOnPoloniex.FieldName = "ListedOnPoloniex";
            this.colListedOnPoloniex.MinWidth = 40;
            this.colListedOnPoloniex.Name = "colListedOnPoloniex";
            this.colListedOnPoloniex.Visible = true;
            this.colListedOnPoloniex.VisibleIndex = 11;
            this.colListedOnPoloniex.Width = 150;
            // 
            // colMarketCap
            // 
            this.colMarketCap.FieldName = "MarketCap";
            this.colMarketCap.MinWidth = 40;
            this.colMarketCap.Name = "colMarketCap";
            this.colMarketCap.Visible = false;
            this.colMarketCap.VisibleIndex = -1;
            this.colMarketCap.Width = 150;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.siStatus,
            this.biRefresh,
            this.barEditItem1,
            this.barButtonItem1,
            this.biForecast,
            this.biForecastSelected});
            this.barManager1.MaxItemId = 6;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEdit1});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.biRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.barEditItem1, "", false, true, true, 114),
            new DevExpress.XtraBars.LinkPersistInfo(this.biForecast),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // biRefresh
            // 
            this.biRefresh.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.biRefresh.Caption = "<b>Refresh</b>";
            this.biRefresh.Id = 1;
            this.biRefresh.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biRefresh.ImageOptions.SvgImage")));
            this.biRefresh.ItemAppearance.Hovered.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Information;
            this.biRefresh.ItemAppearance.Hovered.Options.UseForeColor = true;
            this.biRefresh.ItemAppearance.Normal.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Information;
            this.biRefresh.ItemAppearance.Normal.Options.UseForeColor = true;
            this.biRefresh.ItemAppearance.Pressed.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Information;
            this.biRefresh.ItemAppearance.Pressed.Options.UseForeColor = true;
            this.biRefresh.Name = "biRefresh";
            this.biRefresh.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biRefresh_ItemClick);
            // 
            // barEditItem1
            // 
            this.barEditItem1.Caption = "Min Change24h";
            this.barEditItem1.Edit = this.repositoryItemSpinEdit1;
            this.barEditItem1.EditValue = 20;
            this.barEditItem1.Id = 2;
            this.barEditItem1.Name = "barEditItem1";
            this.barEditItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit1.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // biForecast
            // 
            this.biForecast.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.biForecast.Caption = "<b>Update Forecast</b>";
            this.biForecast.Id = 4;
            this.biForecast.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biForecast.ImageOptions.SvgImage")));
            this.biForecast.ItemAppearance.Hovered.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Information;
            this.biForecast.ItemAppearance.Hovered.Options.UseForeColor = true;
            this.biForecast.ItemAppearance.Normal.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Information;
            this.biForecast.ItemAppearance.Normal.Options.UseForeColor = true;
            this.biForecast.ItemAppearance.Pressed.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Information;
            this.biForecast.ItemAppearance.Pressed.Options.UseForeColor = true;
            this.biForecast.Name = "biForecast";
            this.biForecast.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biForecast.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biForecast_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.barButtonItem1.Caption = "<b>Stop!</b>";
            this.barButtonItem1.Id = 3;
            this.barButtonItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem1.ImageOptions.SvgImage")));
            this.barButtonItem1.ItemAppearance.Hovered.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            this.barButtonItem1.ItemAppearance.Hovered.Options.UseForeColor = true;
            this.barButtonItem1.ItemAppearance.Normal.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            this.barButtonItem1.ItemAppearance.Normal.Options.UseForeColor = true;
            this.barButtonItem1.ItemAppearance.Pressed.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            this.barButtonItem1.ItemAppearance.Pressed.Options.UseForeColor = true;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.siStatus)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawBorder = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // siStatus
            // 
            this.siStatus.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.siStatus.Caption = "<b>Starting</b>";
            this.siStatus.Id = 0;
            this.siStatus.ItemAppearance.Normal.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            this.siStatus.ItemAppearance.Normal.Options.UseForeColor = true;
            this.siStatus.Name = "siStatus";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1440, 60);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 863);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1440, 52);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 60);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 803);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1440, 60);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 803);
            // 
            // biForecastSelected
            // 
            this.biForecastSelected.Caption = "Update Forecast";
            this.biForecastSelected.Id = 5;
            this.biForecastSelected.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biForecastSelected.ImageOptions.SvgImage")));
            this.biForecastSelected.Name = "biForecastSelected";
            // 
            // popupMenu2
            // 
            this.popupMenu2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.biForecastSelected)});
            this.popupMenu2.Manager = this.barManager1;
            this.popupMenu2.Name = "popupMenu2";
            // 
            // colForecast7Day
            // 
            this.colForecast7Day.FieldName = "Forecast7Day";
            this.colForecast7Day.MinWidth = 40;
            this.colForecast7Day.Name = "colForecast7Day";
            this.colForecast7Day.Visible = true;
            this.colForecast7Day.VisibleIndex = 7;
            this.colForecast7Day.Width = 150;
            this.colForecast7Day.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colForecast7Day.DisplayFormat.FormatString = "0.###";
            // 
            // colForecast14Day
            // 
            this.colForecast14Day.FieldName = "Forecast14Day";
            this.colForecast14Day.MinWidth = 40;
            this.colForecast14Day.Name = "colForecast14Day";
            this.colForecast14Day.Visible = true;
            this.colForecast14Day.VisibleIndex = 8;
            this.colForecast14Day.Width = 150;
            this.colForecast14Day.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colForecast14Day.DisplayFormat.FormatString = "0.###";
            // 
            // colForecast3Month
            // 
            this.colForecast3Month.FieldName = "Forecast3Month";
            this.colForecast3Month.MinWidth = 40;
            this.colForecast3Month.Name = "colForecast3Month";
            this.colForecast3Month.Visible = true;
            this.colForecast3Month.VisibleIndex = 9;
            this.colForecast3Month.Width = 150;
            this.colForecast3Month.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colForecast3Month.DisplayFormat.FormatString = "0.###";
            // 
            // WalletInvestorDataForm
            // 
            this.ClientSize = new System.Drawing.Size(1440, 915);
            this.Controls.Add(this.gridControl);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "WalletInvestorDataForm";
            this.Text = "walletinvestor.com statistics";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.walletInvestorDataItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource walletInvestorDataItemBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colLastPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colChange24;
        private DevExpress.XtraGrid.Columns.GridColumn colRise;
        private DevExpress.XtraGrid.Columns.GridColumn colVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colMarketCap;
        private DevExpress.XtraGrid.Columns.GridColumn colListedOnBinance;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colListedOnPoloniex;
        private BarManager barManager1;
        private Bar bar1;
        private BarButtonItem biRefresh;
        private Bar bar3;
        private BarStaticItem siStatus;
        private BarDockControl barDockControlTop;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarEditItem barEditItem1;
        private RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private BarButtonItem barButtonItem1;
        private BarButtonItem biForecast;
        private BarButtonItem biForecastSelected;
        private PopupMenu popupMenu2;
        private DevExpress.XtraGrid.Columns.GridColumn colForecast7Day;
        private DevExpress.XtraGrid.Columns.GridColumn colForecast14Day;
        private DevExpress.XtraGrid.Columns.GridColumn colForecast3Month;
    }
}