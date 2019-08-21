namespace CryptoMarketClient.Strategies.Signal {
    partial class SignalNotificationDataForm {
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.combinedSignalDataItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIndex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRsi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMacdFast = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMacdSlow = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMacd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMacdSignal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStochK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStochD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.combinedSignalDataItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.combinedSignalDataItemBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.gridControl1.Size = new System.Drawing.Size(1432, 1043);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Click += new System.EventHandler(this.gridControl1_Click);
            // 
            // combinedSignalDataItemBindingSource
            // 
            this.combinedSignalDataItemBindingSource.DataSource = typeof(CryptoMarketClient.Strategies.Signal.CombinedSignalDataItem);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIndex,
            this.colTime,
            this.colRsi,
            this.colMacdFast,
            this.colMacdSlow,
            this.colMacd,
            this.colMacdSignal,
            this.colStochK,
            this.colStochD});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.gridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // colIndex
            // 
            this.colIndex.FieldName = "Index";
            this.colIndex.MinWidth = 40;
            this.colIndex.Name = "colIndex";
            this.colIndex.Visible = true;
            this.colIndex.VisibleIndex = 0;
            this.colIndex.Width = 150;
            // 
            // colTime
            // 
            this.colTime.DisplayFormat.FormatString = "dd.MM HH:mm";
            this.colTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTime.FieldName = "Time";
            this.colTime.MinWidth = 40;
            this.colTime.Name = "colTime";
            this.colTime.Visible = true;
            this.colTime.VisibleIndex = 1;
            this.colTime.Width = 150;
            // 
            // colRsi
            // 
            this.colRsi.DisplayFormat.FormatString = "0.########";
            this.colRsi.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colRsi.FieldName = "Rsi";
            this.colRsi.MinWidth = 40;
            this.colRsi.Name = "colRsi";
            this.colRsi.Visible = true;
            this.colRsi.VisibleIndex = 2;
            this.colRsi.Width = 150;
            // 
            // colMacdFast
            // 
            this.colMacdFast.DisplayFormat.FormatString = "0.########";
            this.colMacdFast.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMacdFast.FieldName = "MacdFast";
            this.colMacdFast.MinWidth = 40;
            this.colMacdFast.Name = "colMacdFast";
            this.colMacdFast.Visible = true;
            this.colMacdFast.VisibleIndex = 3;
            this.colMacdFast.Width = 150;
            // 
            // colMacdSlow
            // 
            this.colMacdSlow.DisplayFormat.FormatString = "0.########";
            this.colMacdSlow.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMacdSlow.FieldName = "MacdSlow";
            this.colMacdSlow.MinWidth = 40;
            this.colMacdSlow.Name = "colMacdSlow";
            this.colMacdSlow.Visible = true;
            this.colMacdSlow.VisibleIndex = 4;
            this.colMacdSlow.Width = 150;
            // 
            // colMacd
            // 
            this.colMacd.DisplayFormat.FormatString = "0.########";
            this.colMacd.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMacd.FieldName = "Macd";
            this.colMacd.MinWidth = 40;
            this.colMacd.Name = "colMacd";
            this.colMacd.Visible = true;
            this.colMacd.VisibleIndex = 5;
            this.colMacd.Width = 150;
            // 
            // colMacdSignal
            // 
            this.colMacdSignal.DisplayFormat.FormatString = "0.########";
            this.colMacdSignal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMacdSignal.FieldName = "MacdSignal";
            this.colMacdSignal.MinWidth = 40;
            this.colMacdSignal.Name = "colMacdSignal";
            this.colMacdSignal.Visible = true;
            this.colMacdSignal.VisibleIndex = 6;
            this.colMacdSignal.Width = 150;
            // 
            // colStochK
            // 
            this.colStochK.DisplayFormat.FormatString = "0.###";
            this.colStochK.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colStochK.FieldName = "StochK";
            this.colStochK.MinWidth = 40;
            this.colStochK.Name = "colStochK";
            this.colStochK.Visible = true;
            this.colStochK.VisibleIndex = 7;
            this.colStochK.Width = 150;
            // 
            // colStochD
            // 
            this.colStochD.DisplayFormat.FormatString = "0.###";
            this.colStochD.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colStochD.FieldName = "StochD";
            this.colStochD.MinWidth = 40;
            this.colStochD.Name = "colStochD";
            this.colStochD.Visible = true;
            this.colStochD.VisibleIndex = 8;
            this.colStochD.Width = 150;
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
            // SignalNotificationDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1432, 1043);
            this.Controls.Add(this.gridControl1);
            this.Name = "SignalNotificationDataForm";
            this.Text = "SignalNotificationDataForm";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.combinedSignalDataItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private System.Windows.Forms.BindingSource combinedSignalDataItemBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colTime;
        private DevExpress.XtraGrid.Columns.GridColumn colRsi;
        private DevExpress.XtraGrid.Columns.GridColumn colMacdFast;
        private DevExpress.XtraGrid.Columns.GridColumn colMacdSlow;
        private DevExpress.XtraGrid.Columns.GridColumn colMacd;
        private DevExpress.XtraGrid.Columns.GridColumn colMacdSignal;
        private DevExpress.XtraGrid.Columns.GridColumn colStochK;
        private DevExpress.XtraGrid.Columns.GridColumn colStochD;
        private DevExpress.XtraGrid.Columns.GridColumn colIndex;
    }
}