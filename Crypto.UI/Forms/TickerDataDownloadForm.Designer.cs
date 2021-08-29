using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;

namespace Crypto.UI.Forms {
    partial class TickerDataDownloadForm {
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.leIntervals = new DevExpress.XtraEditors.LookUpEdit();
            this.candleStickIntervalInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cbExchangeTickers = new DevExpress.XtraEditors.GridLookUpEdit();
            this.exchangeTickersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMarketCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBaseCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMarketName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ceExchanges = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.sbOk = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.deStart = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.deEnd = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leIntervals.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.candleStickIntervalInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbExchangeTickers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exchangeTickersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceExchanges.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.leIntervals);
            this.layoutControl1.Controls.Add(this.cbExchangeTickers);
            this.layoutControl1.Controls.Add(this.ceExchanges);
            this.layoutControl1.Controls.Add(this.sbOk);
            this.layoutControl1.Controls.Add(this.simpleButton2);
            this.layoutControl1.Controls.Add(this.deStart);
            this.layoutControl1.Controls.Add(this.deEnd);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(671, 423);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // leIntervals
            // 
            this.leIntervals.Location = new System.Drawing.Point(181, 100);
            this.leIntervals.Name = "leIntervals";
            this.leIntervals.Properties.AutoHeight = false;
            this.leIntervals.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leIntervals.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Text", "Interval")});
            this.leIntervals.Properties.DataSource = this.candleStickIntervalInfoBindingSource;
            this.leIntervals.Properties.DisplayMember = "Text";
            this.leIntervals.Properties.Name = "repositoryItemLookUpEdit1";
            this.leIntervals.Properties.NullText = "Interval Not Selected";
            this.leIntervals.Properties.ShowHeader = false;
            this.leIntervals.Properties.ShowLines = false;
            this.leIntervals.Properties.ValueMember = "TotalMinutes";
            this.leIntervals.Size = new System.Drawing.Size(478, 40);
            this.leIntervals.StyleController = this.layoutControl1;
            this.leIntervals.TabIndex = 6;
            // 
            // candleStickIntervalInfoBindingSource
            // 
            this.candleStickIntervalInfoBindingSource.DataSource = typeof(Crypto.Core.CandleStickIntervalInfo);
            // 
            // cbExchangeTickers
            // 
            this.cbExchangeTickers.EditValue = "";
            this.cbExchangeTickers.Location = new System.Drawing.Point(181, 56);
            this.cbExchangeTickers.Name = "cbExchangeTickers";
            this.cbExchangeTickers.Properties.AutoHeight = false;
            this.cbExchangeTickers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbExchangeTickers.Properties.DataSource = this.exchangeTickersBindingSource;
            this.cbExchangeTickers.Properties.DisplayMember = "MarketName";
            this.cbExchangeTickers.Properties.Name = "cbExchangeTickers";
            this.cbExchangeTickers.Properties.NullText = "Ticker Not Selected";
            this.cbExchangeTickers.Properties.PopupView = this.repositoryItemGridLookUpEdit1View;
            this.cbExchangeTickers.Properties.ValueMember = "Name";
            this.cbExchangeTickers.Size = new System.Drawing.Size(478, 40);
            this.cbExchangeTickers.StyleController = this.layoutControl1;
            this.cbExchangeTickers.TabIndex = 5;
            // 
            // exchangeTickersBindingSource
            // 
            this.exchangeTickersBindingSource.DataSource = typeof(Crypto.Core.Ticker);
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMarketCurrency,
            this.colBaseCurrency});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.GroupCount = 1;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DrawFocusRectStyle.None;
            this.repositoryItemGridLookUpEdit1View.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colBaseCurrency, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colMarketCurrency
            // 
            this.colMarketCurrency.FieldName = "MarketCurrency";
            this.colMarketCurrency.Name = "colMarketCurrency";
            this.colMarketCurrency.Visible = true;
            this.colMarketCurrency.VisibleIndex = 0;
            // 
            // colBaseCurrency
            // 
            this.colBaseCurrency.FieldName = "BaseCurrency";
            this.colBaseCurrency.Name = "colBaseCurrency";
            this.colBaseCurrency.Visible = true;
            this.colBaseCurrency.VisibleIndex = 0;
            // 
            // colMarketName
            // 
            this.colMarketName.FieldName = "MarketName";
            this.colMarketName.Name = "colMarketName";
            this.colMarketName.Visible = true;
            this.colMarketName.VisibleIndex = 1;
            // 
            // ceExchanges
            // 
            this.ceExchanges.Location = new System.Drawing.Point(181, 12);
            this.ceExchanges.Name = "ceExchanges";
            this.ceExchanges.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ceExchanges.Size = new System.Drawing.Size(478, 40);
            this.ceExchanges.StyleController = this.layoutControl1;
            this.ceExchanges.TabIndex = 4;
            this.ceExchanges.SelectedIndexChanged += new System.EventHandler(this.ceExchanges_SelectedIndexChanged);
            // 
            // sbOk
            // 
            this.sbOk.AutoWidthInLayoutControl = true;
            this.sbOk.Location = new System.Drawing.Point(315, 367);
            this.sbOk.Name = "sbOk";
            this.sbOk.Padding = new System.Windows.Forms.Padding(60, 0, 60, 0);
            this.sbOk.Size = new System.Drawing.Size(165, 44);
            this.sbOk.StyleController = this.layoutControl1;
            this.sbOk.TabIndex = 7;
            this.sbOk.Text = "OK";
            this.sbOk.Click += new System.EventHandler(this.sbOk_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.AutoWidthInLayoutControl = true;
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(500, 367);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Padding = new System.Windows.Forms.Padding(40, 0, 40, 0);
            this.simpleButton2.Size = new System.Drawing.Size(159, 44);
            this.simpleButton2.StyleController = this.layoutControl1;
            this.simpleButton2.TabIndex = 8;
            this.simpleButton2.Text = "Cancel";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem5,
            this.emptySpaceItem1,
            this.layoutControlItem4,
            this.emptySpaceItem2,
            this.layoutControlItem6,
            this.layoutControlItem7});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(671, 423);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ceExchanges;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(651, 44);
            this.layoutControlItem1.Text = "Exchange";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(166, 25);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cbExchangeTickers;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 44);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(651, 44);
            this.layoutControlItem2.Text = "Ticker";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(166, 25);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.leIntervals;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 88);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(651, 44);
            this.layoutControlItem3.Text = "Kline Interval      ";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(166, 25);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.simpleButton2;
            this.layoutControlItem5.Location = new System.Drawing.Point(472, 355);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(18, 2, 2, 2);
            this.layoutControlItem5.Size = new System.Drawing.Size(179, 48);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 355);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(303, 48);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sbOk;
            this.layoutControlItem4.Location = new System.Drawing.Point(303, 355);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(169, 48);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 220);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(651, 135);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // deStart
            // 
            this.deStart.EditValue = null;
            this.deStart.Location = new System.Drawing.Point(181, 144);
            this.deStart.Name = "deStart";
            this.deStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStart.Size = new System.Drawing.Size(478, 40);
            this.deStart.StyleController = this.layoutControl1;
            this.deStart.TabIndex = 9;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.deStart;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 132);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(651, 44);
            this.layoutControlItem6.Text = "Start Date";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(166, 25);
            // 
            // deEnd
            // 
            this.deEnd.EditValue = null;
            this.deEnd.Location = new System.Drawing.Point(181, 188);
            this.deEnd.Name = "deEnd";
            this.deEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEnd.Size = new System.Drawing.Size(478, 40);
            this.deEnd.StyleController = this.layoutControl1;
            this.deEnd.TabIndex = 10;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.deEnd;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 176);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(651, 44);
            this.layoutControlItem7.Text = "End Date";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(166, 25);
            // 
            // TickerDataDownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 423);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TickerDataDownloadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Download Settings";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leIntervals.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.candleStickIntervalInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbExchangeTickers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exchangeTickersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceExchanges.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private LayoutControl layoutControl1;
        private LayoutControlGroup Root;
        private DevExpress.XtraEditors.ImageComboBoxEdit ceExchanges;
        private LayoutControlItem layoutControlItem1;
        private GridLookUpEdit cbExchangeTickers;
        private GridView gridLookUpEdit1View;
        private LayoutControlItem layoutControlItem2;
        private LookUpEdit leIntervals;
        private LayoutControlItem layoutControlItem3;
        protected System.Windows.Forms.BindingSource exchangeTickersBindingSource;
        protected System.Windows.Forms.BindingSource candleStickIntervalInfoBindingSource;
        private SimpleButton sbOk;
        private SimpleButton simpleButton2;
        private LayoutControlItem layoutControlItem5;
        private EmptySpaceItem emptySpaceItem1;
        private LayoutControlItem layoutControlItem4;
        private EmptySpaceItem emptySpaceItem2;
        private DXErrorProvider dxErrorProvider1;
        protected DevExpress.XtraGrid.Columns.GridColumn colMarketCurrency;
        protected DevExpress.XtraGrid.Columns.GridColumn colBaseCurrency;
        protected DevExpress.XtraGrid.Columns.GridColumn colMarketName;
        protected DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DateEdit deStart;
        private DateEdit deEnd;
        private LayoutControlItem layoutControlItem6;
        private LayoutControlItem layoutControlItem7;
    }
}