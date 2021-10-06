using DevExpress.XtraBars;

namespace CryptoMarketClient {
    partial class MyTradesCollectionControl {
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

        #region Component Designer generated code

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyTradesCollectionControl));
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTrades = new DevExpress.XtraGrid.GridControl();
            this.tradeHistoryItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvTrades = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFill = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bbRefreshMyTrades = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcTrades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tradeHistoryItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTrades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // colType
            // 
            this.colType.FieldName = "Type";
            this.colType.MinWidth = 40;
            this.colType.Name = "colType";
            this.colType.Width = 150;
            // 
            // colRate
            // 
            this.colRate.DisplayFormat.FormatString = "0.00000000";
            this.colRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colRate.FieldName = "Rate";
            this.colRate.MinWidth = 40;
            this.colRate.Name = "colRate";
            this.colRate.Visible = true;
            this.colRate.VisibleIndex = 1;
            this.colRate.Width = 341;
            // 
            // gcTrades
            // 
            this.gcTrades.DataSource = this.tradeHistoryItemBindingSource;
            this.gcTrades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTrades.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(12);
            this.gcTrades.Location = new System.Drawing.Point(0, 46);
            this.gcTrades.MainView = this.gvTrades;
            this.gcTrades.Margin = new System.Windows.Forms.Padding(12);
            this.gcTrades.Name = "gcTrades";
            this.gcTrades.Size = new System.Drawing.Size(2261, 890);
            this.gcTrades.TabIndex = 1;
            this.gcTrades.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.gcTrades.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTrades});
            // 
            // tradeHistoryItemBindingSource
            // 
            this.tradeHistoryItemBindingSource.DataSource = typeof(Crypto.Core.TradeInfoItem);
            // 
            // gvTrades
            // 
            this.gvTrades.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 7.875F);
            this.gvTrades.Appearance.Row.Options.UseFont = true;
            this.gvTrades.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gvTrades.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTime,
            this.colType,
            this.colAmount,
            this.colRate,
            this.colTotal,
            this.colFee,
            this.colFill,
            this.colId});
            gridFormatRule1.Column = this.colType;
            gridFormatRule1.ColumnApplyTo = this.colRate;
            gridFormatRule1.Name = "FormatRulesTradeBuy";
            formatConditionRuleValue1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            formatConditionRuleValue1.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.Value1 = Crypto.Core.TradeType.Buy;
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.Column = this.colType;
            gridFormatRule2.ColumnApplyTo = this.colRate;
            gridFormatRule2.Name = "FormatRulesTradeSell";
            formatConditionRuleValue2.Appearance.ForeColor = System.Drawing.Color.Red;
            formatConditionRuleValue2.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue2.Value1 = Crypto.Core.TradeType.Sell;
            gridFormatRule2.Rule = formatConditionRuleValue2;
            this.gvTrades.FormatRules.Add(gridFormatRule1);
            this.gvTrades.FormatRules.Add(gridFormatRule2);
            this.gvTrades.GridControl = this.gcTrades;
            this.gvTrades.Name = "gvTrades";
            this.gvTrades.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gvTrades.OptionsBehavior.Editable = false;
            this.gvTrades.OptionsDetail.EnableMasterViewMode = false;
            this.gvTrades.OptionsView.EnableAppearanceEvenRow = true;
            this.gvTrades.OptionsView.ShowGroupPanel = false;
            this.gvTrades.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvTrades.OptionsView.ShowIndicator = false;
            // 
            // colTime
            // 
            this.colTime.DisplayFormat.FormatString = "hh:mm:ss.fff";
            this.colTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTime.FieldName = "Time";
            this.colTime.MinWidth = 40;
            this.colTime.Name = "colTime";
            this.colTime.Visible = true;
            this.colTime.VisibleIndex = 0;
            this.colTime.Width = 885;
            // 
            // colAmount
            // 
            this.colAmount.DisplayFormat.FormatString = "0.00000000";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "Amount";
            this.colAmount.MinWidth = 40;
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 2;
            this.colAmount.Width = 341;
            // 
            // colTotal
            // 
            this.colTotal.FieldName = "Total";
            this.colTotal.MinWidth = 40;
            this.colTotal.Name = "colTotal";
            this.colTotal.Visible = true;
            this.colTotal.VisibleIndex = 3;
            this.colTotal.Width = 228;
            // 
            // colFee
            // 
            this.colFee.FieldName = "Fee";
            this.colFee.MinWidth = 40;
            this.colFee.Name = "colFee";
            this.colFee.Visible = true;
            this.colFee.VisibleIndex = 4;
            this.colFee.Width = 466;
            // 
            // colFill
            // 
            this.colFill.FieldName = "Fill";
            this.colFill.MinWidth = 40;
            this.colFill.Name = "colFill";
            this.colFill.Width = 150;
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.MinWidth = 40;
            this.colId.Name = "colId";
            this.colId.Width = 150;
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
            this.bbRefreshMyTrades});
            this.barManager1.MaxItemId = 1;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbRefreshMyTrades)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // bbRefreshMyTrades
            // 
            this.bbRefreshMyTrades.Caption = "Refresh";
            this.bbRefreshMyTrades.Id = 0;
            this.bbRefreshMyTrades.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbRefreshMyTrades.ImageOptions.Image")));
            this.bbRefreshMyTrades.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbRefreshMyTrades.ImageOptions.LargeImage")));
            this.bbRefreshMyTrades.Name = "bbRefreshMyTrades";
            this.bbRefreshMyTrades.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbRefreshMyTrades.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbRefreshMyTrades_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(6);
            this.barDockControlTop.Size = new System.Drawing.Size(2261, 46);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 936);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(6);
            this.barDockControlBottom.Size = new System.Drawing.Size(2261, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 46);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(6);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 890);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(2261, 46);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(6);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 890);
            // 
            // MyTradesCollectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcTrades);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "MyTradesCollectionControl";
            this.Size = new System.Drawing.Size(2261, 936);
            ((System.ComponentModel.ISupportInitialize)(this.gcTrades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tradeHistoryItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTrades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcTrades;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTrades;
        private DevExpress.XtraGrid.Columns.GridColumn colTime;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colRate;
        private DevExpress.XtraGrid.Columns.GridColumn colTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colFill;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private System.Windows.Forms.BindingSource tradeHistoryItemBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colFee;
        private BarManager barManager1;
        private Bar bar1;
        private BarButtonItem bbRefreshMyTrades;
        private BarDockControl barDockControlTop;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
    }
}
