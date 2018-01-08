namespace CryptoMarketClient {
    partial class FavoritePairsUserControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
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
            this.gridFavorites = new DevExpress.XtraGrid.GridControl();
            this.viewFavorites = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.searchLookUpEdit1 = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCurrencyTicker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridFavorites)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewFavorites)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // gridFavorites
            // 
            this.gridFavorites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridFavorites.Location = new System.Drawing.Point(0, 42);
            this.gridFavorites.MainView = this.viewFavorites;
            this.gridFavorites.Name = "gridFavorites";
            this.gridFavorites.Size = new System.Drawing.Size(1069, 563);
            this.gridFavorites.TabIndex = 2;
            this.gridFavorites.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewFavorites});
            // 
            // viewFavorites
            // 
            this.viewFavorites.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.viewFavorites.GridControl = this.gridFavorites;
            this.viewFavorites.Name = "viewFavorites";
            this.viewFavorites.OptionsBehavior.Editable = false;
            this.viewFavorites.OptionsCustomization.AllowGroup = false;
            this.viewFavorites.OptionsFilter.AllowColumnMRUFilterList = false;
            this.viewFavorites.OptionsFilter.AllowFilterEditor = false;
            this.viewFavorites.OptionsFilter.AllowFilterIncrementalSearch = false;
            this.viewFavorites.OptionsFind.AllowFindPanel = false;
            this.viewFavorites.OptionsMenu.EnableGroupPanelMenu = false;
            this.viewFavorites.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.viewFavorites.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.viewFavorites.OptionsView.ShowGroupPanel = false;
            this.viewFavorites.OptionsView.ShowIndicator = false;
            this.viewFavorites.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.searchLookUpEdit1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new System.Windows.Forms.Padding(3);
            this.panelControl1.Size = new System.Drawing.Size(1069, 42);
            this.panelControl1.TabIndex = 3;
            // 
            // searchLookUpEdit1
            // 
            this.searchLookUpEdit1.Location = new System.Drawing.Point(6, 11);
            this.searchLookUpEdit1.Name = "searchLookUpEdit1";
            this.searchLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpEdit1.Properties.DisplayMember = "CurrencyTicker";
            this.searchLookUpEdit1.Properties.NullText = "";
            this.searchLookUpEdit1.Properties.NullValuePromptShowForEmptyValue = true;
            this.searchLookUpEdit1.Properties.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.searchLookUpEdit1.Properties.PopupView = this.searchLookUpEdit1View;
            this.searchLookUpEdit1.Size = new System.Drawing.Size(305, 20);
            this.searchLookUpEdit1.TabIndex = 1;
            this.searchLookUpEdit1.Popup += new System.EventHandler(this.searchLookUpEdit1_Popup);
            this.searchLookUpEdit1.EditValueChanged += new System.EventHandler(this.searchLookUpEdit1_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCurrencyTicker,
            this.colCurrencyName});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsFind.FindDelay = 500;
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // colCurrencyTicker
            // 
            this.colCurrencyTicker.Caption = "Ticker";
            this.colCurrencyTicker.FieldName = "CurrencyTicker";
            this.colCurrencyTicker.Name = "colCurrencyTicker";
            this.colCurrencyTicker.Visible = true;
            this.colCurrencyTicker.VisibleIndex = 0;
            // 
            // colCurrencyName
            // 
            this.colCurrencyName.Caption = "Currency Name";
            this.colCurrencyName.FieldName = "CurrencyName";
            this.colCurrencyName.Name = "colCurrencyName";
            this.colCurrencyName.Visible = true;
            this.colCurrencyName.VisibleIndex = 1;
            // 
            // FavoritePairsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridFavorites);
            this.Controls.Add(this.panelControl1);
            this.Name = "FavoritePairsUserControl";
            this.Size = new System.Drawing.Size(1069, 605);
            this.Load += new System.EventHandler(this.FavoritePairsUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridFavorites)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewFavorites)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridFavorites;
        private DevExpress.XtraGrid.Views.Grid.GridView viewFavorites;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SearchLookUpEdit searchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyTicker;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyName;
    }
}
