namespace CryptoMarketClient.Strategies.Signal {
    partial class MacdTrendStrategyDataForm {
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
            this.colTimeToBuy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTimeToSell = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.macdTrendStrategyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMacd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSignal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmaSlow = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmaFast = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDelta = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colSource = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tpData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.macdTrendStrategyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Size = new System.Drawing.Size(1367, 1004);
            // 
            // tpData
            // 
            this.tpData.Controls.Add(this.gridControl1);
            this.tpData.Size = new System.Drawing.Size(1355, 949);
            // 
            // colTimeToBuy
            // 
            this.colTimeToBuy.FieldName = "TimeToBuy";
            this.colTimeToBuy.MinWidth = 40;
            this.colTimeToBuy.Name = "colTimeToBuy";
            this.colTimeToBuy.Width = 150;
            // 
            // colTimeToSell
            // 
            this.colTimeToSell.FieldName = "TimeToSell";
            this.colTimeToSell.MinWidth = 40;
            this.colTimeToSell.Name = "colTimeToSell";
            this.colTimeToSell.Width = 150;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.macdTrendStrategyBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.gridControl1.Size = new System.Drawing.Size(1355, 949);
            this.gridControl1.TabIndex = 6;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Click += new System.EventHandler(this.gridControl1_Click_1);
            // 
            // macdTrendStrategyBindingSource
            // 
            this.macdTrendStrategyBindingSource.DataSource = typeof(CryptoMarketClient.Strategies.Signal.MacdTrendStrategyHistoryItem);
            // 
            // gridView1
            // 
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTime,
            this.colSource,
            this.colMacd,
            this.colSignal,
            this.colEmaSlow,
            this.colEmaFast,
            this.colDelta,
            this.colTimeToBuy,
            this.colTimeToSell});
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Column = this.colTimeToBuy;
            gridFormatRule1.Name = "TimeToBuy";
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.PredefinedName = "Green Fill";
            formatConditionRuleValue1.Value1 = true;
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.ApplyToRow = true;
            gridFormatRule2.Column = this.colTimeToSell;
            gridFormatRule2.Name = "TimeToSell";
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue2.PredefinedName = "Red Fill";
            formatConditionRuleValue2.Value1 = true;
            gridFormatRule2.Rule = formatConditionRuleValue2;
            this.gridView1.FormatRules.Add(gridFormatRule1);
            this.gridView1.FormatRules.Add(gridFormatRule2);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsScrollAnnotations.ShowCustomAnnotations = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.gridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.CustomScrollAnnotation += new System.EventHandler<DevExpress.XtraGrid.Views.Grid.GridCustomScrollAnnotationsEventArgs>(this.gridView1_CustomScrollAnnotation);
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // colTime
            // 
            this.colTime.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
            this.colTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTime.FieldName = "Time";
            this.colTime.MinWidth = 40;
            this.colTime.Name = "colTime";
            this.colTime.Visible = true;
            this.colTime.VisibleIndex = 0;
            this.colTime.Width = 150;
            // 
            // colMacd
            // 
            this.colMacd.DisplayFormat.FormatString = "0.########";
            this.colMacd.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMacd.FieldName = "Macd";
            this.colMacd.MinWidth = 40;
            this.colMacd.Name = "colMacd";
            this.colMacd.Visible = true;
            this.colMacd.VisibleIndex = 2;
            this.colMacd.Width = 150;
            // 
            // colSignal
            // 
            this.colSignal.DisplayFormat.FormatString = "0.########";
            this.colSignal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSignal.FieldName = "Signal";
            this.colSignal.MinWidth = 40;
            this.colSignal.Name = "colSignal";
            this.colSignal.Visible = true;
            this.colSignal.VisibleIndex = 3;
            this.colSignal.Width = 150;
            // 
            // colEmaSlow
            // 
            this.colEmaSlow.DisplayFormat.FormatString = "0.########";
            this.colEmaSlow.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colEmaSlow.FieldName = "EmaSlow";
            this.colEmaSlow.MinWidth = 40;
            this.colEmaSlow.Name = "colEmaSlow";
            this.colEmaSlow.Visible = true;
            this.colEmaSlow.VisibleIndex = 5;
            this.colEmaSlow.Width = 150;
            // 
            // colEmaFast
            // 
            this.colEmaFast.DisplayFormat.FormatString = "0.########";
            this.colEmaFast.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colEmaFast.FieldName = "EmaFast";
            this.colEmaFast.MinWidth = 40;
            this.colEmaFast.Name = "colEmaFast";
            this.colEmaFast.Visible = true;
            this.colEmaFast.VisibleIndex = 4;
            this.colEmaFast.Width = 150;
            // 
            // colDelta
            // 
            this.colDelta.DisplayFormat.FormatString = "0.########";
            this.colDelta.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDelta.FieldName = "Delta";
            this.colDelta.MinWidth = 40;
            this.colDelta.Name = "colDelta";
            this.colDelta.Visible = true;
            this.colDelta.VisibleIndex = 6;
            this.colDelta.Width = 150;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // colSource
            // 
            this.colSource.DisplayFormat.FormatString = "0.########";
            this.colSource.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSource.FieldName = "Source";
            this.colSource.MinWidth = 40;
            this.colSource.Name = "colSource";
            this.colSource.Visible = true;
            this.colSource.VisibleIndex = 1;
            this.colSource.Width = 150;
            // 
            // MacdTrendStrategyDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1367, 1004);
            this.Name = "MacdTrendStrategyDataForm";
            this.Text = "TripleRsiStrategyDataForm";
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tpData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.macdTrendStrategyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private System.Windows.Forms.BindingSource macdTrendStrategyBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colTime;
        private DevExpress.XtraGrid.Columns.GridColumn colTimeToBuy;
        private DevExpress.XtraGrid.Columns.GridColumn colTimeToSell;
        private DevExpress.XtraGrid.Columns.GridColumn colMacd;
        private DevExpress.XtraGrid.Columns.GridColumn colSignal;
        private DevExpress.XtraGrid.Columns.GridColumn colEmaFast;
        private DevExpress.XtraGrid.Columns.GridColumn colDelta;
        private DevExpress.XtraGrid.Columns.GridColumn colEmaSlow;
        private DevExpress.XtraGrid.Columns.GridColumn colSource;
    }
}