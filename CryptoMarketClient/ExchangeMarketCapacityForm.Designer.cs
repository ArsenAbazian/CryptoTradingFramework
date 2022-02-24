
namespace CryptoMarketClient {
    partial class ExchangeMarketCapacityForm {
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
            DevExpress.XtraTreeMap.HatchFillStyle hatchFillStyle1 = new DevExpress.XtraTreeMap.HatchFillStyle();
            DevExpress.XtraTreeMap.HatchFillStyle hatchFillStyle2 = new DevExpress.XtraTreeMap.HatchFillStyle();
            DevExpress.XtraTreeMap.TreeMapPaletteColorizer treeMapPaletteColorizer1 = new DevExpress.XtraTreeMap.TreeMapPaletteColorizer();
            DevExpress.XtraTreeMap.TreeMapFlatDataAdapter treeMapFlatDataAdapter1 = new DevExpress.XtraTreeMap.TreeMapFlatDataAdapter();
            DevExpress.XtraTreeMap.TreeMapStripedLayoutAlgorithm treeMapStripedLayoutAlgorithm1 = new DevExpress.XtraTreeMap.TreeMapStripedLayoutAlgorithm();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExchangeMarketCapacityForm));
            DevExpress.Skins.SkinPaddingEdges skinPaddingEdges1 = new DevExpress.Skins.SkinPaddingEdges();
            DevExpress.Skins.SkinPaddingEdges skinPaddingEdges2 = new DevExpress.Skins.SkinPaddingEdges();
            this.tickerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.treeMapControl1 = new DevExpress.XtraTreeMap.TreeMapControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.beExchanges = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.beMarkets = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.biColors = new DevExpress.XtraBars.BarSubItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.commandBarGalleryDropDown7 = new DevExpress.XtraBars.Commands.CommandBarGalleryDropDown(this.components);
            this.chartBarController1 = new DevExpress.XtraCharts.UI.ChartBarController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tickerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBarController1)).BeginInit();
            this.SuspendLayout();
            // 
            // tickerBindingSource
            // 
            this.tickerBindingSource.DataSource = typeof(Crypto.Core.Ticker);
            // 
            // treeMapControl1
            // 
            this.treeMapControl1.Appearance.GroupStyle.TextGlowColor = System.Drawing.Color.Empty;
            this.treeMapControl1.Appearance.HighlightedLeafStyle.FillStyle = hatchFillStyle1;
            this.treeMapControl1.Appearance.SelectedLeafStyle.FillStyle = hatchFillStyle2;
            this.treeMapControl1.BorderOptions.Thickness = 2;
            treeMapPaletteColorizer1.LegendItemPattern = null;
            treeMapPaletteColorizer1.Palette = DevExpress.XtraTreeMap.Palette.Office2019Palette;
            this.treeMapControl1.Colorizer = treeMapPaletteColorizer1;
            treeMapFlatDataAdapter1.DataSource = this.tickerBindingSource;
            treeMapFlatDataAdapter1.GroupDataMembersSerializable = "BaseCurrency";
            treeMapFlatDataAdapter1.LabelDataMember = "MarketCurrency";
            treeMapFlatDataAdapter1.ValueDataMember = "BaseVolume";
            this.treeMapControl1.DataAdapter = treeMapFlatDataAdapter1;
            this.treeMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMapControl1.LayoutAlgorithm = treeMapStripedLayoutAlgorithm1;
            this.treeMapControl1.Location = new System.Drawing.Point(0, 82);
            this.treeMapControl1.Name = "treeMapControl1";
            this.treeMapControl1.Size = new System.Drawing.Size(1808, 928);
            this.treeMapControl1.TabIndex = 0;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.beMarkets,
            this.biColors,
            this.beExchanges});
            this.barManager1.MaxItemId = 29;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1,
            this.repositoryItemComboBox2});
            // 
            // bar1
            // 
            this.bar1.BarItemVertIndent = 12;
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.beExchanges, "", false, true, true, 161),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.beMarkets, "", false, true, true, 168),
            new DevExpress.XtraBars.LinkPersistInfo(this.biColors)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // beExchanges
            // 
            this.beExchanges.Caption = "Exchange";
            this.beExchanges.Edit = this.repositoryItemComboBox2;
            this.beExchanges.Id = 28;
            this.beExchanges.Name = "beExchanges";
            this.beExchanges.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.beExchanges.EditValueChanged += new System.EventHandler(this.beExchanges_EditValueChanged);
            // 
            // repositoryItemComboBox2
            // 
            this.repositoryItemComboBox2.AutoHeight = false;
            this.repositoryItemComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
            this.repositoryItemComboBox2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // beMarkets
            // 
            this.beMarkets.Caption = "Market";
            this.beMarkets.Edit = this.repositoryItemComboBox1;
            this.beMarkets.Id = 0;
            this.beMarkets.Name = "beMarkets";
            this.beMarkets.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.beMarkets.EditValueChanged += new System.EventHandler(this.beMarkets_EditValueChanged);
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            this.repositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // biColors
            // 
            this.biColors.Caption = "Colors";
            this.biColors.Id = 27;
            this.biColors.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biColors.ImageOptions.SvgImage")));
            this.biColors.Name = "biColors";
            this.biColors.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1808, 82);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 1010);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1808, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 82);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 928);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1808, 82);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 928);
            // 
            // commandBarGalleryDropDown7
            // 
            // 
            // 
            // 
            this.commandBarGalleryDropDown7.Gallery.AllowFilter = false;
            this.commandBarGalleryDropDown7.Gallery.Appearance.ItemCaptionAppearance.Hovered.Options.UseFont = true;
            this.commandBarGalleryDropDown7.Gallery.Appearance.ItemCaptionAppearance.Hovered.Options.UseTextOptions = true;
            this.commandBarGalleryDropDown7.Gallery.Appearance.ItemCaptionAppearance.Hovered.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.commandBarGalleryDropDown7.Gallery.Appearance.ItemCaptionAppearance.Normal.Options.UseFont = true;
            this.commandBarGalleryDropDown7.Gallery.Appearance.ItemCaptionAppearance.Normal.Options.UseTextOptions = true;
            this.commandBarGalleryDropDown7.Gallery.Appearance.ItemCaptionAppearance.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.commandBarGalleryDropDown7.Gallery.Appearance.ItemCaptionAppearance.Pressed.Options.UseFont = true;
            this.commandBarGalleryDropDown7.Gallery.Appearance.ItemCaptionAppearance.Pressed.Options.UseTextOptions = true;
            this.commandBarGalleryDropDown7.Gallery.Appearance.ItemCaptionAppearance.Pressed.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.commandBarGalleryDropDown7.Gallery.Appearance.ItemDescriptionAppearance.Hovered.Options.UseFont = true;
            this.commandBarGalleryDropDown7.Gallery.Appearance.ItemDescriptionAppearance.Hovered.Options.UseTextOptions = true;
            this.commandBarGalleryDropDown7.Gallery.Appearance.ItemDescriptionAppearance.Hovered.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.commandBarGalleryDropDown7.Gallery.Appearance.ItemDescriptionAppearance.Normal.Options.UseFont = true;
            this.commandBarGalleryDropDown7.Gallery.Appearance.ItemDescriptionAppearance.Normal.Options.UseTextOptions = true;
            this.commandBarGalleryDropDown7.Gallery.Appearance.ItemDescriptionAppearance.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.commandBarGalleryDropDown7.Gallery.Appearance.ItemDescriptionAppearance.Pressed.Options.UseFont = true;
            this.commandBarGalleryDropDown7.Gallery.Appearance.ItemDescriptionAppearance.Pressed.Options.UseTextOptions = true;
            this.commandBarGalleryDropDown7.Gallery.Appearance.ItemDescriptionAppearance.Pressed.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.commandBarGalleryDropDown7.Gallery.ColumnCount = 1;
            this.commandBarGalleryDropDown7.Gallery.ImageSize = new System.Drawing.Size(160, 10);
            this.commandBarGalleryDropDown7.Gallery.ItemImageLayout = DevExpress.Utils.Drawing.ImageLayoutMode.MiddleLeft;
            this.commandBarGalleryDropDown7.Gallery.ItemImageLocation = DevExpress.Utils.Locations.Right;
            skinPaddingEdges1.Bottom = -3;
            skinPaddingEdges1.Top = -3;
            this.commandBarGalleryDropDown7.Gallery.ItemImagePadding = skinPaddingEdges1;
            skinPaddingEdges2.Bottom = -3;
            skinPaddingEdges2.Top = -3;
            this.commandBarGalleryDropDown7.Gallery.ItemTextPadding = skinPaddingEdges2;
            this.commandBarGalleryDropDown7.Gallery.RowCount = 10;
            this.commandBarGalleryDropDown7.Gallery.ShowGroupCaption = false;
            this.commandBarGalleryDropDown7.Gallery.ShowItemText = true;
            this.commandBarGalleryDropDown7.Gallery.ShowScrollBar = DevExpress.XtraBars.Ribbon.Gallery.ShowScrollBar.Auto;
            this.commandBarGalleryDropDown7.Manager = this.barManager1;
            this.commandBarGalleryDropDown7.Name = "commandBarGalleryDropDown7";
            this.commandBarGalleryDropDown7.GalleryItemClick += new DevExpress.XtraBars.Ribbon.GalleryItemClickEventHandler(this.commandBarGalleryDropDown7_GalleryItemClick);
            // 
            // ExchangeMarketCapacityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1808, 1010);
            this.Controls.Add(this.treeMapControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ExchangeMarketCapacityForm";
            this.Text = "Exchange Market Capacity";
            ((System.ComponentModel.ISupportInitialize)(this.tickerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBarController1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTreeMap.TreeMapControl treeMapControl1;
        private System.Windows.Forms.BindingSource tickerBindingSource;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarEditItem beMarkets;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Commands.CommandBarGalleryDropDown commandBarGalleryDropDown7;
        private DevExpress.XtraCharts.UI.ChartBarController chartBarController1;
        private DevExpress.XtraBars.BarSubItem biColors;
        private DevExpress.XtraBars.BarEditItem beExchanges;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox2;
    }
}