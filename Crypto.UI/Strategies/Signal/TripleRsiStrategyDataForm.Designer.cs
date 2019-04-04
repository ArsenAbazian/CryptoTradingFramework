namespace CryptoMarketClient.Strategies.Signal {
    partial class TripleRsiStrategyDataForm {
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
            this.tripleRsiStrategyHistoryItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRsiFast = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRsiMiddle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRsiSlow = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.tpChart)).BeginInit();
            this.tpChart.SuspendLayout();
            this.tpData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tripleRsiStrategyHistoryItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.tpChart.Size = new System.Drawing.Size(1367, 1004);
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
            this.gridControl1.DataSource = this.tripleRsiStrategyHistoryItemBindingSource;
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
            // 
            // tripleRsiStrategyHistoryItemBindingSource
            // 
            this.tripleRsiStrategyHistoryItemBindingSource.DataSource = typeof(CryptoMarketClient.Strategies.Signal.TripleRsiStrategyHistoryItem);
            // 
            // gridView1
            // 
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTime,
            this.colRsiFast,
            this.colRsiMiddle,
            this.colRsiSlow,
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
            // colRsiFast
            // 
            this.colRsiFast.FieldName = "RsiFast";
            this.colRsiFast.MinWidth = 40;
            this.colRsiFast.Name = "colRsiFast";
            this.colRsiFast.Visible = true;
            this.colRsiFast.VisibleIndex = 1;
            this.colRsiFast.Width = 150;
            // 
            // colRsiMiddle
            // 
            this.colRsiMiddle.FieldName = "RsiMiddle";
            this.colRsiMiddle.MinWidth = 40;
            this.colRsiMiddle.Name = "colRsiMiddle";
            this.colRsiMiddle.Visible = true;
            this.colRsiMiddle.VisibleIndex = 2;
            this.colRsiMiddle.Width = 150;
            // 
            // colRsiSlow
            // 
            this.colRsiSlow.FieldName = "RsiSlow";
            this.colRsiSlow.MinWidth = 40;
            this.colRsiSlow.Name = "colRsiSlow";
            this.colRsiSlow.Visible = true;
            this.colRsiSlow.VisibleIndex = 3;
            this.colRsiSlow.Width = 150;
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
            // TripleRsiStrategyDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1367, 1004);
            this.Name = "TripleRsiStrategyDataForm";
            this.Text = "TripleRsiStrategyDataForm";
            ((System.ComponentModel.ISupportInitialize)(this.tpChart)).EndInit();
            this.tpChart.ResumeLayout(false);
            this.tpData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tripleRsiStrategyHistoryItemBindingSource)).EndInit();
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
        private System.Windows.Forms.BindingSource tripleRsiStrategyHistoryItemBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colTime;
        private DevExpress.XtraGrid.Columns.GridColumn colRsiFast;
        private DevExpress.XtraGrid.Columns.GridColumn colRsiMiddle;
        private DevExpress.XtraGrid.Columns.GridColumn colRsiSlow;
        private DevExpress.XtraGrid.Columns.GridColumn colTimeToBuy;
        private DevExpress.XtraGrid.Columns.GridColumn colTimeToSell;
    }
}