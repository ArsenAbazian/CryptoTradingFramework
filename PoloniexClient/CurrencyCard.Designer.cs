namespace CryptoMarketClient {
    partial class CurrencyCard {
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
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition3 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition4 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableSpan tableSpan1 = new DevExpress.XtraEditors.TableLayout.TableSpan();
            DevExpress.XtraEditors.TableLayout.TableSpan tableSpan2 = new DevExpress.XtraEditors.TableLayout.TableSpan();
            DevExpress.XtraEditors.TableLayout.TableSpan tableSpan3 = new DevExpress.XtraEditors.TableLayout.TableSpan();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement1 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement2 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement3 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement4 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement5 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement6 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement7 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.tileView1 = new DevExpress.XtraGrid.Views.Tile.TileView();
            this.gcCurrencyPair = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.gcHighestBid = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.gcLowestAsk = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.gcCurrentValue = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.gcPercentChange = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.gcBaseVolume = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.gcQuoteVolume = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.gcIsFrozen = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.gcHr24High = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.gcHr24Low = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.gcActual = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.gcIndicator = new DevExpress.XtraGrid.Columns.TileViewColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.tileView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(559, 241);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.tileView1});
            // 
            // tileView1
            // 
            this.tileView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcCurrencyPair,
            this.gcHighestBid,
            this.gcLowestAsk,
            this.gcCurrentValue,
            this.gcPercentChange,
            this.gcBaseVolume,
            this.gcQuoteVolume,
            this.gcIsFrozen,
            this.gcHr24High,
            this.gcHr24Low,
            this.gcActual,
            this.gcIndicator});
            this.tileView1.GridControl = this.gridControl1;
            this.tileView1.Name = "tileView1";
            this.tileView1.OptionsTiles.IndentBetweenGroups = 0;
            this.tileView1.OptionsTiles.IndentBetweenItems = 0;
            this.tileView1.OptionsTiles.ItemSize = new System.Drawing.Size(458, 96);
            this.tileView1.OptionsTiles.LayoutMode = DevExpress.XtraGrid.Views.Tile.TileViewLayoutMode.List;
            this.tileView1.OptionsTiles.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tileView1.OptionsTiles.Padding = new System.Windows.Forms.Padding(0);
            this.tileView1.OptionsTiles.RowCount = 0;
            this.tileView1.TileColumns.Add(tableColumnDefinition1);
            this.tileView1.TileColumns.Add(tableColumnDefinition2);
            this.tileView1.TileColumns.Add(tableColumnDefinition3);
            tableRowDefinition1.Length.Value = 27D;
            tableRowDefinition2.Length.Value = 19D;
            tableRowDefinition3.Length.Value = 15D;
            tableRowDefinition4.Length.Value = 19D;
            this.tileView1.TileRows.Add(tableRowDefinition1);
            this.tileView1.TileRows.Add(tableRowDefinition2);
            this.tileView1.TileRows.Add(tableRowDefinition3);
            this.tileView1.TileRows.Add(tableRowDefinition4);
            tableSpan1.RowSpan = 2;
            tableSpan2.ColumnIndex = 1;
            tableSpan2.RowSpan = 2;
            tableSpan3.ColumnSpan = 3;
            tableSpan3.RowIndex = 2;
            this.tileView1.TileSpans.Add(tableSpan1);
            this.tileView1.TileSpans.Add(tableSpan2);
            this.tileView1.TileSpans.Add(tableSpan3);
            tileViewItemElement1.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tileViewItemElement1.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement1.Column = this.gcCurrencyPair;
            tileViewItemElement1.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement1.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement1.Text = "gcCurrencyPair";
            tileViewItemElement1.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomLeft;
            tileViewItemElement2.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tileViewItemElement2.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement2.Column = this.gcPercentChange;
            tileViewItemElement2.ColumnIndex = 1;
            tileViewItemElement2.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement2.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement2.RowIndex = 1;
            tileViewItemElement2.Text = "gcPercentChange";
            tileViewItemElement2.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomLeft;
            tileViewItemElement3.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tileViewItemElement3.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement3.Column = this.gcHighestBid;
            tileViewItemElement3.ColumnIndex = 2;
            tileViewItemElement3.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement3.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement3.RowIndex = 1;
            tileViewItemElement3.Text = "gcHighestBid";
            tileViewItemElement3.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
            tileViewItemElement4.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tileViewItemElement4.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement4.Column = this.gcLowestAsk;
            tileViewItemElement4.ColumnIndex = 2;
            tileViewItemElement4.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement4.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement4.Text = "gcLowestAsk";
            tileViewItemElement4.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
            tileViewItemElement5.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tileViewItemElement5.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement5.Column = this.gcHr24Low;
            tileViewItemElement5.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement5.RowIndex = 3;
            tileViewItemElement5.Text = "gcHr24Low";
            tileViewItemElement5.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopLeft;
            tileViewItemElement6.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tileViewItemElement6.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement6.Column = this.gcHr24High;
            tileViewItemElement6.ColumnIndex = 2;
            tileViewItemElement6.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement6.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement6.RowIndex = 3;
            tileViewItemElement6.Text = "gcHr24High";
            tileViewItemElement6.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopRight;
            tileViewItemElement7.Column = this.gcIndicator;
            tileViewItemElement7.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement7.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement7.RowIndex = 2;
            tileViewItemElement7.Text = "gcIndicator";
            tileViewItemElement7.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            this.tileView1.TileTemplate.Add(tileViewItemElement1);
            this.tileView1.TileTemplate.Add(tileViewItemElement2);
            this.tileView1.TileTemplate.Add(tileViewItemElement3);
            this.tileView1.TileTemplate.Add(tileViewItemElement4);
            this.tileView1.TileTemplate.Add(tileViewItemElement5);
            this.tileView1.TileTemplate.Add(tileViewItemElement6);
            this.tileView1.TileTemplate.Add(tileViewItemElement7);
            // 
            // gcCurrencyPair
            // 
            this.gcCurrencyPair.Caption = "Name";
            this.gcCurrencyPair.FieldName = "CurrencyPair";
            this.gcCurrencyPair.Name = "gcCurrencyPair";
            this.gcCurrencyPair.Visible = true;
            this.gcCurrencyPair.VisibleIndex = 0;
            // 
            // gcHighestBid
            // 
            this.gcHighestBid.Caption = "Bid";
            this.gcHighestBid.FieldName = "HighestBid";
            this.gcHighestBid.Name = "gcHighestBid";
            this.gcHighestBid.Visible = true;
            this.gcHighestBid.VisibleIndex = 1;
            // 
            // gcLowestAsk
            // 
            this.gcLowestAsk.Caption = "Ask";
            this.gcLowestAsk.FieldName = "LowestAsk";
            this.gcLowestAsk.Name = "gcLowestAsk";
            this.gcLowestAsk.Visible = true;
            this.gcLowestAsk.VisibleIndex = 2;
            // 
            // gcCurrentValue
            // 
            this.gcCurrentValue.Caption = "Last";
            this.gcCurrentValue.FieldName = "Last";
            this.gcCurrentValue.Name = "gcCurrentValue";
            this.gcCurrentValue.Visible = true;
            this.gcCurrentValue.VisibleIndex = 3;
            // 
            // gcPercentChange
            // 
            this.gcPercentChange.Caption = "PercentChange";
            this.gcPercentChange.FieldName = "PercentChange";
            this.gcPercentChange.Name = "gcPercentChange";
            this.gcPercentChange.Visible = true;
            this.gcPercentChange.VisibleIndex = 4;
            // 
            // gcBaseVolume
            // 
            this.gcBaseVolume.Caption = "BaseVolume";
            this.gcBaseVolume.FieldName = "BaseVolume";
            this.gcBaseVolume.Name = "gcBaseVolume";
            this.gcBaseVolume.Visible = true;
            this.gcBaseVolume.VisibleIndex = 5;
            // 
            // gcQuoteVolume
            // 
            this.gcQuoteVolume.Caption = "QuoteVolume";
            this.gcQuoteVolume.FieldName = "QuoteVolume";
            this.gcQuoteVolume.Name = "gcQuoteVolume";
            this.gcQuoteVolume.Visible = true;
            this.gcQuoteVolume.VisibleIndex = 6;
            // 
            // gcIsFrozen
            // 
            this.gcIsFrozen.Caption = "IsFrozen";
            this.gcIsFrozen.FieldName = "IsFrozen";
            this.gcIsFrozen.Name = "gcIsFrozen";
            this.gcIsFrozen.Visible = true;
            this.gcIsFrozen.VisibleIndex = 7;
            // 
            // gcHr24High
            // 
            this.gcHr24High.Caption = "Hr24High";
            this.gcHr24High.FieldName = "Hr24High";
            this.gcHr24High.Name = "gcHr24High";
            this.gcHr24High.Visible = true;
            this.gcHr24High.VisibleIndex = 8;
            // 
            // gcHr24Low
            // 
            this.gcHr24Low.Caption = "Hr24Low";
            this.gcHr24Low.FieldName = "Hr24Low";
            this.gcHr24Low.Name = "gcHr24Low";
            this.gcHr24Low.Visible = true;
            this.gcHr24Low.VisibleIndex = 9;
            // 
            // gcActual
            // 
            this.gcActual.Caption = "ActualState";
            this.gcActual.FieldName = "IsActualOrderBook";
            this.gcActual.Name = "gcActual";
            this.gcActual.Visible = true;
            this.gcActual.VisibleIndex = 10;
            // 
            // gcIndicator
            // 
            this.gcIndicator.Caption = "Indicator";
            this.gcIndicator.FieldName = "gcIndicator";
            this.gcIndicator.Name = "gcIndicator";
            this.gcIndicator.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.gcIndicator.Visible = true;
            this.gcIndicator.VisibleIndex = 11;
            // 
            // CurrencyCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Name = "CurrencyCard";
            this.Size = new System.Drawing.Size(559, 241);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Tile.TileView tileView1;
        private DevExpress.XtraGrid.Columns.TileViewColumn gcCurrencyPair;
        private DevExpress.XtraGrid.Columns.TileViewColumn gcHighestBid;
        private DevExpress.XtraGrid.Columns.TileViewColumn gcLowestAsk;
        private DevExpress.XtraGrid.Columns.TileViewColumn gcCurrentValue;
        private DevExpress.XtraGrid.Columns.TileViewColumn gcPercentChange;
        private DevExpress.XtraGrid.Columns.TileViewColumn gcBaseVolume;
        private DevExpress.XtraGrid.Columns.TileViewColumn gcQuoteVolume;
        private DevExpress.XtraGrid.Columns.TileViewColumn gcIsFrozen;
        private DevExpress.XtraGrid.Columns.TileViewColumn gcHr24High;
        private DevExpress.XtraGrid.Columns.TileViewColumn gcHr24Low;
        private DevExpress.XtraGrid.Columns.TileViewColumn gcActual;
        private DevExpress.XtraGrid.Columns.TileViewColumn gcIndicator;
    }
}
