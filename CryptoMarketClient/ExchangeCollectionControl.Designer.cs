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
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition1 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition2 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition3 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition1 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition2 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableSpan tableSpan1 = new DevExpress.XtraEditors.TableLayout.TableSpan();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement1 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement2 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            this.colCaption = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colImage = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.gridSelector = new DevExpress.XtraGrid.GridControl();
            this.tileViewSelector = new DevExpress.XtraGrid.Views.Tile.TileView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileViewSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // colCaption
            // 
            this.colCaption.Caption = "Caption";
            this.colCaption.FieldName = "Caption";
            this.colCaption.MinWidth = 40;
            this.colCaption.Name = "colCaption";
            this.colCaption.Visible = true;
            this.colCaption.VisibleIndex = 1;
            this.colCaption.Width = 150;
            // 
            // colImage
            // 
            this.colImage.Caption = "Image";
            this.colImage.FieldName = "Image";
            this.colImage.MinWidth = 40;
            this.colImage.Name = "colImage";
            this.colImage.Visible = true;
            this.colImage.VisibleIndex = 0;
            this.colImage.Width = 150;
            // 
            // gridSelector
            // 
            this.gridSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSelector.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.gridSelector.Location = new System.Drawing.Point(0, 0);
            this.gridSelector.MainView = this.tileViewSelector;
            this.gridSelector.Margin = new System.Windows.Forms.Padding(6);
            this.gridSelector.Name = "gridSelector";
            this.gridSelector.Size = new System.Drawing.Size(1194, 656);
            this.gridSelector.TabIndex = 0;
            this.gridSelector.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.tileViewSelector,
            this.gridView1});
            // 
            // tileViewSelector
            // 
            this.tileViewSelector.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colImage,
            this.colCaption});
            this.tileViewSelector.DetailHeight = 673;
            this.tileViewSelector.GridControl = this.gridSelector;
            this.tileViewSelector.Name = "tileViewSelector";
            this.tileViewSelector.OptionsTiles.AllowItemHover = true;
            this.tileViewSelector.OptionsTiles.HighlightFocusedTileStyle = DevExpress.XtraGrid.Views.Tile.HighlightFocusedTileStyle.None;
            this.tileViewSelector.OptionsTiles.ItemPadding = new System.Windows.Forms.Padding(0);
            this.tileViewSelector.OptionsTiles.ItemSize = new System.Drawing.Size(300, 220);
            this.tileViewSelector.OptionsTiles.Orientation = System.Windows.Forms.Orientation.Vertical;
            tableColumnDefinition1.Length.Type = DevExpress.XtraEditors.TableLayout.TableDefinitionLengthType.Pixel;
            tableColumnDefinition1.Length.Value = 24D;
            tableColumnDefinition2.Length.Value = 258D;
            tableColumnDefinition3.Length.Type = DevExpress.XtraEditors.TableLayout.TableDefinitionLengthType.Pixel;
            tableColumnDefinition3.Length.Value = 24D;
            this.tileViewSelector.TileColumns.Add(tableColumnDefinition1);
            this.tileViewSelector.TileColumns.Add(tableColumnDefinition2);
            this.tileViewSelector.TileColumns.Add(tableColumnDefinition3);
            tableRowDefinition1.Length.Value = 149D;
            tableRowDefinition2.Length.Value = 71D;
            tableRowDefinition2.PaddingBottom = 22;
            tableRowDefinition2.PaddingTop = 22;
            this.tileViewSelector.TileRows.Add(tableRowDefinition1);
            this.tileViewSelector.TileRows.Add(tableRowDefinition2);
            tableSpan1.ColumnSpan = 3;
            this.tileViewSelector.TileSpans.Add(tableSpan1);
            tileViewItemElement1.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tileViewItemElement1.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement1.Column = this.colCaption;
            tileViewItemElement1.ColumnIndex = 1;
            tileViewItemElement1.RowIndex = 1;
            tileViewItemElement1.Text = "colCaption";
            tileViewItemElement1.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopLeft;
            tileViewItemElement2.Column = this.colImage;
            tileViewItemElement2.ColumnIndex = 1;
            tileViewItemElement2.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement2.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomOutside;
            tileViewItemElement2.Text = "colImage";
            tileViewItemElement2.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            this.tileViewSelector.TileTemplate.Add(tileViewItemElement1);
            this.tileViewSelector.TileTemplate.Add(tileViewItemElement2);
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 673;
            this.gridView1.FixedLineWidth = 4;
            this.gridView1.GridControl = this.gridSelector;
            this.gridView1.Name = "gridView1";
            // 
            // ExchangeCollectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridSelector);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ExchangeCollectionControl";
            this.Size = new System.Drawing.Size(1194, 656);
            ((System.ComponentModel.ISupportInitialize)(this.gridSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileViewSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridSelector;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Tile.TileView tileViewSelector;
        private DevExpress.XtraGrid.Columns.TileViewColumn colImage;
        private DevExpress.XtraGrid.Columns.TileViewColumn colCaption;
    }
}
