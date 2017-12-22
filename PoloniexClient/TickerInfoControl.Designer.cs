namespace CryptoMarketClient {
    partial class TickerInfo {
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule3 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet3 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet3 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon7 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon8 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon9 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule4 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet4 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet4 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon10 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon11 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon12 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            this.colHighestBid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLowestAsk = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLast = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBaseVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHr24High = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHr24Low = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSpread = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBidChange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAskChange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLastUpdateTime = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // colHighestBid
            // 
            this.colHighestBid.Caption = "Bid";
            this.colHighestBid.FieldName = "HighestBidString";
            this.colHighestBid.Name = "colHighestBid";
            this.colHighestBid.OptionsColumn.ReadOnly = true;
            this.colHighestBid.Visible = true;
            this.colHighestBid.VisibleIndex = 2;
            this.colHighestBid.Width = 245;
            // 
            // colLowestAsk
            // 
            this.colLowestAsk.Caption = "Ask";
            this.colLowestAsk.FieldName = "LowestAskString";
            this.colLowestAsk.Name = "colLowestAsk";
            this.colLowestAsk.OptionsColumn.ReadOnly = true;
            this.colLowestAsk.Visible = true;
            this.colLowestAsk.VisibleIndex = 3;
            this.colLowestAsk.Width = 249;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1848, 592);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.PaintEx += new DevExpress.XtraGrid.PaintExEventHandler(this.gridControl1_PaintEx);
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(CryptoMarketClient.TickerBase);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.Transparent;
            this.gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Transparent;
            this.gridView1.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Appearance.Row.Options.UseTextOptions = true;
            this.gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.SelectedRow.BackColor = System.Drawing.Color.Transparent;
            this.gridView1.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colName,
            this.colHighestBid,
            this.colLowestAsk,
            this.colLast,
            this.colBaseVolume,
            this.colVolume,
            this.colHr24High,
            this.colHr24Low,
            this.colChange,
            this.colSpread,
            this.colBidChange,
            this.colAskChange,
            this.colLastUpdateTime});
            gridFormatRule3.Column = this.colHighestBid;
            gridFormatRule3.ColumnApplyTo = this.colHighestBid;
            gridFormatRule3.Name = "HighestBidRule";
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
            gridFormatRule4.Column = this.colLowestAsk;
            gridFormatRule4.ColumnApplyTo = this.colLowestAsk;
            gridFormatRule4.Name = "LowestAskRule";
            formatConditionIconSet4.CategoryName = "Positive/Negative";
            formatConditionIconSetIcon10.PredefinedName = "Arrows3_3.png";
            formatConditionIconSetIcon10.Value = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            formatConditionIconSetIcon10.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon11.PredefinedName = "Arrows3_2.png";
            formatConditionIconSetIcon11.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon12.PredefinedName = "Arrows3_1.png";
            formatConditionIconSet4.Icons.Add(formatConditionIconSetIcon10);
            formatConditionIconSet4.Icons.Add(formatConditionIconSetIcon11);
            formatConditionIconSet4.Icons.Add(formatConditionIconSetIcon12);
            formatConditionIconSet4.Name = "PositiveNegativeArrows";
            formatConditionIconSet4.ValueType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            formatConditionRuleIconSet4.IconSet = formatConditionIconSet4;
            gridFormatRule4.Rule = formatConditionRuleIconSet4;
            this.gridView1.FormatRules.Add(gridFormatRule3);
            this.gridView1.FormatRules.Add(gridFormatRule4);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.ReadOnly = true;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 207;
            // 
            // colLast
            // 
            this.colLast.FieldName = "Last";
            this.colLast.Name = "colLast";
            this.colLast.OptionsColumn.ReadOnly = true;
            this.colLast.Visible = true;
            this.colLast.VisibleIndex = 1;
            this.colLast.Width = 214;
            // 
            // colBaseVolume
            // 
            this.colBaseVolume.FieldName = "BaseVolume";
            this.colBaseVolume.Name = "colBaseVolume";
            this.colBaseVolume.OptionsColumn.ReadOnly = true;
            this.colBaseVolume.Visible = true;
            this.colBaseVolume.VisibleIndex = 6;
            this.colBaseVolume.Width = 279;
            // 
            // colVolume
            // 
            this.colVolume.FieldName = "Volume";
            this.colVolume.Name = "colVolume";
            this.colVolume.OptionsColumn.ReadOnly = true;
            this.colVolume.Visible = true;
            this.colVolume.VisibleIndex = 7;
            this.colVolume.Width = 383;
            // 
            // colHr24High
            // 
            this.colHr24High.FieldName = "Hr24High";
            this.colHr24High.Name = "colHr24High";
            this.colHr24High.OptionsColumn.ReadOnly = true;
            this.colHr24High.Visible = true;
            this.colHr24High.VisibleIndex = 4;
            this.colHr24High.Width = 302;
            // 
            // colHr24Low
            // 
            this.colHr24Low.FieldName = "Hr24Low";
            this.colHr24Low.Name = "colHr24Low";
            this.colHr24Low.OptionsColumn.ReadOnly = true;
            this.colHr24Low.Visible = true;
            this.colHr24Low.VisibleIndex = 5;
            this.colHr24Low.Width = 284;
            // 
            // colChange
            // 
            this.colChange.DisplayFormat.FormatString = "F3";
            this.colChange.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colChange.FieldName = "Change";
            this.colChange.Name = "colChange";
            this.colChange.OptionsColumn.ReadOnly = true;
            this.colChange.Width = 119;
            // 
            // colSpread
            // 
            this.colSpread.Caption = "Spread";
            this.colSpread.FieldName = "Spread";
            this.colSpread.Name = "colSpread";
            this.colSpread.Width = 124;
            // 
            // colBidChange
            // 
            this.colBidChange.Caption = "Bid Change";
            this.colBidChange.DisplayFormat.FormatString = "F3";
            this.colBidChange.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBidChange.FieldName = "BidChange";
            this.colBidChange.Name = "colBidChange";
            this.colBidChange.Width = 119;
            // 
            // colAskChange
            // 
            this.colAskChange.Caption = "Ask Change";
            this.colAskChange.DisplayFormat.FormatString = "F3";
            this.colAskChange.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAskChange.FieldName = "AskChange";
            this.colAskChange.Name = "colAskChange";
            this.colAskChange.Width = 119;
            // 
            // colLastUpdateTime
            // 
            this.colLastUpdateTime.FieldName = "LastUpdateTime";
            this.colLastUpdateTime.Name = "colLastUpdateTime";
            this.colLastUpdateTime.Visible = true;
            this.colLastUpdateTime.VisibleIndex = 8;
            // 
            // TickerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "TickerInfo";
            this.Size = new System.Drawing.Size(1848, 592);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.BindingSource bindingSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colHighestBid;
        private DevExpress.XtraGrid.Columns.GridColumn colLowestAsk;
        private DevExpress.XtraGrid.Columns.GridColumn colLast;
        private DevExpress.XtraGrid.Columns.GridColumn colBaseVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colHr24High;
        private DevExpress.XtraGrid.Columns.GridColumn colHr24Low;
        private DevExpress.XtraGrid.Columns.GridColumn colChange;
        private DevExpress.XtraGrid.Columns.GridColumn colSpread;
        private DevExpress.XtraGrid.Columns.GridColumn colBidChange;
        private DevExpress.XtraGrid.Columns.GridColumn colAskChange;
        private DevExpress.XtraGrid.Columns.GridColumn colLastUpdateTime;
    }
}
