namespace Crypto.UI.Forms {
    partial class DataVisualiserForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataVisualiserForm));
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.biCustomize = new DevExpress.XtraBars.BarButtonItem();
            this.biReset = new DevExpress.XtraBars.BarButtonItem();
            this.bsPanes = new DevExpress.XtraBars.BarSubItem();
            this.bsIndex = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.biShowChart = new DevExpress.XtraBars.BarButtonItem();
            this.biShowTable = new DevExpress.XtraBars.BarButtonItem();
            this.biBoth = new DevExpress.XtraBars.BarButtonItem();
            this.pmShowData = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmShowData)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.MdiParent = this;
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
            this.biCustomize,
            this.biReset,
            this.bsPanes,
            this.bsIndex,
            this.biShowChart,
            this.biShowTable,
            this.biBoth});
            this.barManager1.MaxItemId = 7;
            // 
            // bar1
            // 
            this.bar1.BarAppearance.Hovered.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.bar1.BarAppearance.Hovered.Options.UseFont = true;
            this.bar1.BarAppearance.Normal.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.bar1.BarAppearance.Normal.Options.UseFont = true;
            this.bar1.BarAppearance.Pressed.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.bar1.BarAppearance.Pressed.Options.UseFont = true;
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar1.FloatLocation = new System.Drawing.Point(722, 393);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.biCustomize),
            new DevExpress.XtraBars.LinkPersistInfo(this.biReset),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsPanes),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsIndex)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // biCustomize
            // 
            this.biCustomize.Caption = "Customize";
            this.biCustomize.Id = 0;
            this.biCustomize.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biCustomize.ImageOptions.SvgImage")));
            this.biCustomize.Name = "biCustomize";
            this.biCustomize.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // biReset
            // 
            this.biReset.Caption = "Reset Defaults";
            this.biReset.Id = 1;
            this.biReset.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biReset.ImageOptions.SvgImage")));
            this.biReset.Name = "biReset";
            this.biReset.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // bsPanes
            // 
            this.bsPanes.Caption = "Panes";
            this.bsPanes.Id = 2;
            this.bsPanes.Name = "bsPanes";
            // 
            // bsIndex
            // 
            this.bsIndex.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.bsIndex.Caption = "DataItem Index";
            this.bsIndex.Id = 3;
            this.bsIndex.Name = "bsIndex";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(2086, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 975);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(2086, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 975);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(2086, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 975);
            // 
            // biShowChart
            // 
            this.biShowChart.Caption = "Show Chart";
            this.biShowChart.Id = 4;
            this.biShowChart.Name = "biShowChart";
            // 
            // biShowTable
            // 
            this.biShowTable.Caption = "Show Table";
            this.biShowTable.Id = 5;
            this.biShowTable.Name = "biShowTable";
            // 
            // biBoth
            // 
            this.biBoth.Caption = "Show Both";
            this.biBoth.Id = 6;
            this.biBoth.Name = "biBoth";
            // 
            // pmShowData
            // 
            this.pmShowData.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.biShowChart),
            new DevExpress.XtraBars.LinkPersistInfo(this.biShowTable),
            new DevExpress.XtraBars.LinkPersistInfo(this.biBoth)});
            this.pmShowData.Manager = this.barManager1;
            this.pmShowData.Name = "pmShowData";
            // 
            // DataVisualiserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2086, 975);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "DataVisualiserForm";
            this.Text = "DataVisualiserForm";
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmShowData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem biCustomize;
        private DevExpress.XtraBars.BarButtonItem biReset;
        private DevExpress.XtraBars.BarSubItem bsPanes;
        private DevExpress.XtraBars.BarStaticItem bsIndex;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem biShowChart;
        private DevExpress.XtraBars.BarButtonItem biShowTable;
        private DevExpress.XtraBars.BarButtonItem biBoth;
        private DevExpress.XtraBars.PopupMenu pmShowData;
    }
}