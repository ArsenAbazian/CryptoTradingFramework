namespace TileViewSelector {
    partial class ExchangeCollectionControl {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExchangeCollectionControl));
            this.gridSelector = new DevExpress.XtraGrid.GridControl();
            this.winExplorerView1 = new DevExpress.XtraGrid.Views.WinExplorer.WinExplorerView();
            this.colImage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCaption = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.winExplorerView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridSelector
            // 
            this.gridSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSelector.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(12);
            this.gridSelector.Location = new System.Drawing.Point(0, 0);
            this.gridSelector.MainView = this.winExplorerView1;
            this.gridSelector.Margin = new System.Windows.Forms.Padding(12);
            this.gridSelector.Name = "gridSelector";
            this.gridSelector.Size = new System.Drawing.Size(2388, 1262);
            this.gridSelector.TabIndex = 0;
            this.gridSelector.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.winExplorerView1,
            this.gridView1});
            // 
            // winExplorerView1
            // 
            this.winExplorerView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colImage,
            this.colCaption});
            this.winExplorerView1.GridControl = this.gridSelector;
            this.winExplorerView1.Name = "winExplorerView1";
            this.winExplorerView1.OptionsView.ContentHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            // 
            // 
            // 
            this.winExplorerView1.OptionsViewStyles.Medium.HtmlTemplate.Styles = resources.GetString("winExplorerView1.OptionsViewStyles.Medium.HtmlTemplate.Styles");
            this.winExplorerView1.OptionsViewStyles.Medium.HtmlTemplate.Template = resources.GetString("winExplorerView1.OptionsViewStyles.Medium.HtmlTemplate.Template");
            // 
            // colImage
            // 
            this.colImage.Caption = "Image";
            this.colImage.FieldName = "Image";
            this.colImage.MinWidth = 80;
            this.colImage.Name = "colImage";
            this.colImage.Visible = true;
            this.colImage.VisibleIndex = 0;
            this.colImage.Width = 300;
            // 
            // colCaption
            // 
            this.colCaption.Caption = "Caption";
            this.colCaption.FieldName = "Caption";
            this.colCaption.MinWidth = 80;
            this.colCaption.Name = "colCaption";
            this.colCaption.Visible = true;
            this.colCaption.VisibleIndex = 1;
            this.colCaption.Width = 300;
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 1294;
            this.gridView1.FixedLineWidth = 4;
            this.gridView1.GridControl = this.gridSelector;
            this.gridView1.Name = "gridView1";
            // 
            // ExchangeCollectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridSelector);
            this.Margin = new System.Windows.Forms.Padding(12);
            this.Name = "ExchangeCollectionControl";
            this.Size = new System.Drawing.Size(2388, 1262);
            ((System.ComponentModel.ISupportInitialize)(this.gridSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.winExplorerView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridSelector;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.WinExplorer.WinExplorerView winExplorerView1;
        private DevExpress.XtraGrid.Columns.GridColumn colImage;
        private DevExpress.XtraGrid.Columns.GridColumn colCaption;
    }
}
