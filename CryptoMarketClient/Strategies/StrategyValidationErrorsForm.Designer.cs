namespace CryptoMarketClient.Strategies {
    partial class StrategyValidationErrorsForm {
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
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.strategyValidationErrorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colPropertyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataObject = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.strategyValidationErrorBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.strategyValidationErrorBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.gridControl1.Size = new System.Drawing.Size(1770, 884);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPropertyName,
            this.colValue,
            this.colDescription,
            this.colDataObject});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.gridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            // 
            // strategyValidationErrorBindingSource
            // 
            this.strategyValidationErrorBindingSource.DataSource = typeof(Crypto.Core.Strategies.StrategyValidationError);
            // 
            // colPropertyName
            // 
            this.colPropertyName.FieldName = "PropertyName";
            this.colPropertyName.MinWidth = 40;
            this.colPropertyName.Name = "colPropertyName";
            this.colPropertyName.Visible = true;
            this.colPropertyName.VisibleIndex = 0;
            this.colPropertyName.Width = 150;
            // 
            // colValue
            // 
            this.colValue.FieldName = "Value";
            this.colValue.MinWidth = 40;
            this.colValue.Name = "colValue";
            this.colValue.Visible = true;
            this.colValue.VisibleIndex = 1;
            this.colValue.Width = 150;
            // 
            // colDescription
            // 
            this.colDescription.FieldName = "Description";
            this.colDescription.MinWidth = 40;
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 2;
            this.colDescription.Width = 150;
            // 
            // colDataObject
            // 
            this.colDataObject.FieldName = "DataObject";
            this.colDataObject.MinWidth = 40;
            this.colDataObject.Name = "colDataObject";
            this.colDataObject.Visible = true;
            this.colDataObject.VisibleIndex = 3;
            this.colDataObject.Width = 150;
            // 
            // StrategyValidationErrorsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1770, 884);
            this.Controls.Add(this.gridControl1);
            this.Name = "StrategyValidationErrorsForm";
            this.Text = "Strategy Validation Errors";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.strategyValidationErrorBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.BindingSource strategyValidationErrorBindingSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colPropertyName;
        private DevExpress.XtraGrid.Columns.GridColumn colValue;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colDataObject;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
    }
}