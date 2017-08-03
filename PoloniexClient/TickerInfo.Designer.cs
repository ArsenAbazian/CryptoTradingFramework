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
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition1 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition2 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition3 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition4 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition5 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition6 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition7 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition1 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition2 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement1 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement2 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement3 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement4 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement5 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement6 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement7 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement8 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement9 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement10 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement11 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement12 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement13 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement14 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement15 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            this.colName = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colChange = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colLast = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colSpread = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colHighestBid = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colLowestAsk = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colHr24High = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colHr24Low = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colBidChange = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colAskChange = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tileView1 = new DevExpress.XtraGrid.Views.Tile.TileView();
            this.colBaseVolume = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colVolume = new DevExpress.XtraGrid.Columns.TileViewColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileView1)).BeginInit();
            this.SuspendLayout();
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.ReadOnly = true;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            // 
            // colChange
            // 
            this.colChange.DisplayFormat.FormatString = "F3";
            this.colChange.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colChange.FieldName = "Change";
            this.colChange.Name = "colChange";
            this.colChange.OptionsColumn.ReadOnly = true;
            this.colChange.Visible = true;
            this.colChange.VisibleIndex = 8;
            // 
            // colLast
            // 
            this.colLast.FieldName = "Last";
            this.colLast.Name = "colLast";
            this.colLast.OptionsColumn.ReadOnly = true;
            this.colLast.Visible = true;
            this.colLast.VisibleIndex = 3;
            // 
            // colSpread
            // 
            this.colSpread.Caption = "Spread";
            this.colSpread.FieldName = "Spread";
            this.colSpread.Name = "colSpread";
            this.colSpread.Visible = true;
            this.colSpread.VisibleIndex = 9;
            // 
            // colHighestBid
            // 
            this.colHighestBid.FieldName = "HighestBid";
            this.colHighestBid.Name = "colHighestBid";
            this.colHighestBid.OptionsColumn.ReadOnly = true;
            this.colHighestBid.Visible = true;
            this.colHighestBid.VisibleIndex = 1;
            // 
            // colLowestAsk
            // 
            this.colLowestAsk.FieldName = "LowestAsk";
            this.colLowestAsk.Name = "colLowestAsk";
            this.colLowestAsk.OptionsColumn.ReadOnly = true;
            this.colLowestAsk.Visible = true;
            this.colLowestAsk.VisibleIndex = 2;
            // 
            // colHr24High
            // 
            this.colHr24High.FieldName = "Hr24High";
            this.colHr24High.Name = "colHr24High";
            this.colHr24High.OptionsColumn.ReadOnly = true;
            this.colHr24High.Visible = true;
            this.colHr24High.VisibleIndex = 6;
            // 
            // colHr24Low
            // 
            this.colHr24Low.FieldName = "Hr24Low";
            this.colHr24Low.Name = "colHr24Low";
            this.colHr24Low.OptionsColumn.ReadOnly = true;
            this.colHr24Low.Visible = true;
            this.colHr24Low.VisibleIndex = 7;
            // 
            // colBidChange
            // 
            this.colBidChange.Caption = "Bid Change";
            this.colBidChange.DisplayFormat.FormatString = "F3";
            this.colBidChange.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBidChange.FieldName = "BidChange";
            this.colBidChange.Name = "colBidChange";
            this.colBidChange.Visible = true;
            this.colBidChange.VisibleIndex = 10;
            // 
            // colAskChange
            // 
            this.colAskChange.Caption = "Ask Change";
            this.colAskChange.DisplayFormat.FormatString = "F3";
            this.colAskChange.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAskChange.FieldName = "AskChange";
            this.colAskChange.Name = "colAskChange";
            this.colAskChange.Visible = true;
            this.colAskChange.VisibleIndex = 11;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.tileView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1303, 638);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.tileView1});
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(CryptoMarketClient.ITicker);
            // 
            // tileView1
            // 
            this.tileView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
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
            this.colAskChange});
            this.tileView1.GridControl = this.gridControl1;
            this.tileView1.Name = "tileView1";
            this.tileView1.OptionsTiles.IndentBetweenGroups = 0;
            this.tileView1.OptionsTiles.IndentBetweenItems = 0;
            this.tileView1.OptionsTiles.ItemSize = new System.Drawing.Size(1470, 125);
            this.tileView1.OptionsTiles.LayoutMode = DevExpress.XtraGrid.Views.Tile.TileViewLayoutMode.List;
            this.tileView1.OptionsTiles.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tileView1.OptionsTiles.Padding = new System.Windows.Forms.Padding(0);
            this.tileView1.OptionsTiles.RowCount = 0;
            tableColumnDefinition1.Length.Value = 377D;
            tableColumnDefinition2.Length.Value = 35D;
            tableColumnDefinition3.Length.Value = 206D;
            tableColumnDefinition4.Length.Value = 207D;
            tableColumnDefinition5.Length.Value = 207D;
            tableColumnDefinition6.Length.Value = 207D;
            tableColumnDefinition7.Length.Value = 207D;
            this.tileView1.TileColumns.Add(tableColumnDefinition1);
            this.tileView1.TileColumns.Add(tableColumnDefinition2);
            this.tileView1.TileColumns.Add(tableColumnDefinition3);
            this.tileView1.TileColumns.Add(tableColumnDefinition4);
            this.tileView1.TileColumns.Add(tableColumnDefinition5);
            this.tileView1.TileColumns.Add(tableColumnDefinition6);
            this.tileView1.TileColumns.Add(tableColumnDefinition7);
            tableRowDefinition1.Length.Value = 59D;
            tableRowDefinition2.Length.Value = 50D;
            this.tileView1.TileRows.Add(tableRowDefinition1);
            this.tileView1.TileRows.Add(tableRowDefinition2);
            tileViewItemElement1.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tileViewItemElement1.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement1.Column = this.colName;
            tileViewItemElement1.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement1.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement1.Text = "colName";
            tileViewItemElement1.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomLeft;
            tileViewItemElement2.ColumnIndex = 2;
            tileViewItemElement2.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement2.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement2.Text = "Bid";
            tileViewItemElement2.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomCenter;
            tileViewItemElement3.ColumnIndex = 3;
            tileViewItemElement3.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement3.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement3.Text = "Ask";
            tileViewItemElement3.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomCenter;
            tileViewItemElement4.AnchorAlignment = DevExpress.Utils.AnchorAlignment.Right;
            tileViewItemElement4.AnchorElementIndex = 4;
            tileViewItemElement4.AnchorIndent = 18;
            tileViewItemElement4.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tileViewItemElement4.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement4.Column = this.colChange;
            tileViewItemElement4.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement4.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement4.RowIndex = 1;
            tileViewItemElement4.Text = "colChange";
            tileViewItemElement4.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomLeft;
            tileViewItemElement5.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tileViewItemElement5.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement5.Column = this.colLast;
            tileViewItemElement5.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement5.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement5.RowIndex = 1;
            tileViewItemElement5.Text = "colLast";
            tileViewItemElement5.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomLeft;
            tileViewItemElement6.Column = this.colSpread;
            tileViewItemElement6.ColumnIndex = 4;
            tileViewItemElement6.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement6.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement6.RowIndex = 1;
            tileViewItemElement6.Text = "colSpread";
            tileViewItemElement6.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomCenter;
            tileViewItemElement7.Column = this.colHighestBid;
            tileViewItemElement7.ColumnIndex = 2;
            tileViewItemElement7.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement7.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement7.RowIndex = 1;
            tileViewItemElement7.Text = "colHighestBid";
            tileViewItemElement7.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomCenter;
            tileViewItemElement8.Column = this.colLowestAsk;
            tileViewItemElement8.ColumnIndex = 3;
            tileViewItemElement8.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement8.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement8.RowIndex = 1;
            tileViewItemElement8.Text = "colLowestAsk";
            tileViewItemElement8.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomCenter;
            tileViewItemElement9.ColumnIndex = 4;
            tileViewItemElement9.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement9.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement9.Text = "Spread";
            tileViewItemElement9.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomCenter;
            tileViewItemElement10.ColumnIndex = 5;
            tileViewItemElement10.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement10.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement10.Text = "High";
            tileViewItemElement10.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomCenter;
            tileViewItemElement11.ColumnIndex = 6;
            tileViewItemElement11.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement11.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement11.Text = "Low";
            tileViewItemElement11.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomCenter;
            tileViewItemElement12.Column = this.colHr24High;
            tileViewItemElement12.ColumnIndex = 5;
            tileViewItemElement12.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement12.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement12.RowIndex = 1;
            tileViewItemElement12.Text = "colHr24High";
            tileViewItemElement12.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomCenter;
            tileViewItemElement13.Column = this.colHr24Low;
            tileViewItemElement13.ColumnIndex = 6;
            tileViewItemElement13.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement13.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement13.RowIndex = 1;
            tileViewItemElement13.Text = "colHr24Low";
            tileViewItemElement13.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomCenter;
            tileViewItemElement14.AnchorAlignment = DevExpress.Utils.AnchorAlignment.Right;
            tileViewItemElement14.AnchorElementIndex = 6;
            tileViewItemElement14.Column = this.colBidChange;
            tileViewItemElement14.ColumnIndex = 2;
            tileViewItemElement14.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement14.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement14.RowIndex = 1;
            tileViewItemElement14.Text = "colBidChange";
            tileViewItemElement14.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement15.AnchorAlignment = DevExpress.Utils.AnchorAlignment.Right;
            tileViewItemElement15.AnchorElementIndex = 7;
            tileViewItemElement15.Column = this.colAskChange;
            tileViewItemElement15.ColumnIndex = 3;
            tileViewItemElement15.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement15.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement15.RowIndex = 1;
            tileViewItemElement15.Text = "colAskChange";
            tileViewItemElement15.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            this.tileView1.TileTemplate.Add(tileViewItemElement1);
            this.tileView1.TileTemplate.Add(tileViewItemElement2);
            this.tileView1.TileTemplate.Add(tileViewItemElement3);
            this.tileView1.TileTemplate.Add(tileViewItemElement4);
            this.tileView1.TileTemplate.Add(tileViewItemElement5);
            this.tileView1.TileTemplate.Add(tileViewItemElement6);
            this.tileView1.TileTemplate.Add(tileViewItemElement7);
            this.tileView1.TileTemplate.Add(tileViewItemElement8);
            this.tileView1.TileTemplate.Add(tileViewItemElement9);
            this.tileView1.TileTemplate.Add(tileViewItemElement10);
            this.tileView1.TileTemplate.Add(tileViewItemElement11);
            this.tileView1.TileTemplate.Add(tileViewItemElement12);
            this.tileView1.TileTemplate.Add(tileViewItemElement13);
            this.tileView1.TileTemplate.Add(tileViewItemElement14);
            this.tileView1.TileTemplate.Add(tileViewItemElement15);
            // 
            // colBaseVolume
            // 
            this.colBaseVolume.FieldName = "BaseVolume";
            this.colBaseVolume.Name = "colBaseVolume";
            this.colBaseVolume.OptionsColumn.ReadOnly = true;
            this.colBaseVolume.Visible = true;
            this.colBaseVolume.VisibleIndex = 4;
            // 
            // colVolume
            // 
            this.colVolume.FieldName = "Volume";
            this.colVolume.Name = "colVolume";
            this.colVolume.OptionsColumn.ReadOnly = true;
            this.colVolume.Visible = true;
            this.colVolume.VisibleIndex = 5;
            // 
            // TickerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.Name = "TickerInfo";
            this.Size = new System.Drawing.Size(1303, 638);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.BindingSource bindingSource;
        private DevExpress.XtraGrid.Views.Tile.TileView tileView1;
        private DevExpress.XtraGrid.Columns.TileViewColumn colName;
        private DevExpress.XtraGrid.Columns.TileViewColumn colHighestBid;
        private DevExpress.XtraGrid.Columns.TileViewColumn colLowestAsk;
        private DevExpress.XtraGrid.Columns.TileViewColumn colLast;
        private DevExpress.XtraGrid.Columns.TileViewColumn colBaseVolume;
        private DevExpress.XtraGrid.Columns.TileViewColumn colVolume;
        private DevExpress.XtraGrid.Columns.TileViewColumn colHr24High;
        private DevExpress.XtraGrid.Columns.TileViewColumn colHr24Low;
        private DevExpress.XtraGrid.Columns.TileViewColumn colChange;
        private DevExpress.XtraGrid.Columns.TileViewColumn colSpread;
        private DevExpress.XtraGrid.Columns.TileViewColumn colBidChange;
        private DevExpress.XtraGrid.Columns.TileViewColumn colAskChange;
    }
}
