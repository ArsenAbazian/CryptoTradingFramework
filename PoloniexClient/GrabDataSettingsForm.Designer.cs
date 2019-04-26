namespace CryptoMarketClient {
    partial class GrabDataSettingsForm {
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
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.StarTimeDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.GrabTradeHistoryCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.GrabChartCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.buttonEdit1 = new DevExpress.XtraEditors.ButtonEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForStarTime = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForGrabTradeHistory = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForGrabChart = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.grabDataSettingsBindingSource = new System.Windows.Forms.BindingSource();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StarTimeDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StarTimeDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrabTradeHistoryCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrabChartCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForStarTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForGrabTradeHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForGrabChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grabDataSettingsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.StarTimeDateEdit);
            this.dataLayoutControl1.Controls.Add(this.GrabTradeHistoryCheckEdit);
            this.dataLayoutControl1.Controls.Add(this.GrabChartCheckEdit);
            this.dataLayoutControl1.Controls.Add(this.simpleButton1);
            this.dataLayoutControl1.Controls.Add(this.simpleButton2);
            this.dataLayoutControl1.Controls.Add(this.buttonEdit1);
            this.dataLayoutControl1.DataSource = this.grabDataSettingsBindingSource;
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Margin = new System.Windows.Forms.Padding(4);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.dataLayoutControl1.Size = new System.Drawing.Size(800, 440);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // StarTimeDateEdit
            // 
            this.StarTimeDateEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.grabDataSettingsBindingSource, "StarTime", true));
            this.StarTimeDateEdit.EditValue = null;
            this.StarTimeDateEdit.Location = new System.Drawing.Point(178, 12);
            this.StarTimeDateEdit.Margin = new System.Windows.Forms.Padding(4);
            this.StarTimeDateEdit.Name = "StarTimeDateEdit";
            this.StarTimeDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.StarTimeDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.StarTimeDateEdit.Size = new System.Drawing.Size(610, 40);
            this.StarTimeDateEdit.StyleController = this.dataLayoutControl1;
            this.StarTimeDateEdit.TabIndex = 4;
            // 
            // GrabTradeHistoryCheckEdit
            // 
            this.GrabTradeHistoryCheckEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.grabDataSettingsBindingSource, "GrabTradeHistory", true));
            this.GrabTradeHistoryCheckEdit.Location = new System.Drawing.Point(12, 100);
            this.GrabTradeHistoryCheckEdit.Margin = new System.Windows.Forms.Padding(4);
            this.GrabTradeHistoryCheckEdit.Name = "GrabTradeHistoryCheckEdit";
            this.GrabTradeHistoryCheckEdit.Properties.Caption = "Trade History";
            this.GrabTradeHistoryCheckEdit.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.GrabTradeHistoryCheckEdit.Size = new System.Drawing.Size(776, 38);
            this.GrabTradeHistoryCheckEdit.StyleController = this.dataLayoutControl1;
            this.GrabTradeHistoryCheckEdit.TabIndex = 5;
            // 
            // GrabChartCheckEdit
            // 
            this.GrabChartCheckEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.grabDataSettingsBindingSource, "GrabChart", true));
            this.GrabChartCheckEdit.Location = new System.Drawing.Point(12, 142);
            this.GrabChartCheckEdit.Margin = new System.Windows.Forms.Padding(4);
            this.GrabChartCheckEdit.Name = "GrabChartCheckEdit";
            this.GrabChartCheckEdit.Properties.Caption = "Candlesticks";
            this.GrabChartCheckEdit.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.GrabChartCheckEdit.Size = new System.Drawing.Size(776, 38);
            this.GrabChartCheckEdit.StyleController = this.dataLayoutControl1;
            this.GrabChartCheckEdit.TabIndex = 6;
            // 
            // simpleButton1
            // 
            this.simpleButton1.AutoWidthInLayoutControl = true;
            this.simpleButton1.Location = new System.Drawing.Point(443, 384);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Padding = new System.Windows.Forms.Padding(60, 0, 60, 0);
            this.simpleButton1.Size = new System.Drawing.Size(182, 44);
            this.simpleButton1.StyleController = this.dataLayoutControl1;
            this.simpleButton1.TabIndex = 7;
            this.simpleButton1.Text = "Grab";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.AutoWidthInLayoutControl = true;
            this.simpleButton2.Location = new System.Drawing.Point(629, 384);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Padding = new System.Windows.Forms.Padding(40, 0, 40, 0);
            this.simpleButton2.Size = new System.Drawing.Size(159, 44);
            this.simpleButton2.StyleController = this.dataLayoutControl1;
            this.simpleButton2.TabIndex = 8;
            this.simpleButton2.Text = "Cancel";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // buttonEdit1
            // 
            this.buttonEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.grabDataSettingsBindingSource, "DirectoryName", true));
            this.buttonEdit1.Location = new System.Drawing.Point(178, 56);
            this.buttonEdit1.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEdit1.Name = "buttonEdit1";
            this.buttonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEdit1.Size = new System.Drawing.Size(610, 40);
            this.buttonEdit1.StyleController = this.dataLayoutControl1;
            this.buttonEdit1.TabIndex = 9;
            this.buttonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit1_ButtonClick);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup1.Size = new System.Drawing.Size(800, 440);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AllowDrawBackground = false;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForStarTime,
            this.ItemForGrabTradeHistory,
            this.ItemForGrabChart,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "autoGeneratedGroup0";
            this.layoutControlGroup2.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup2.Size = new System.Drawing.Size(780, 420);
            // 
            // ItemForStarTime
            // 
            this.ItemForStarTime.Control = this.StarTimeDateEdit;
            this.ItemForStarTime.Location = new System.Drawing.Point(0, 0);
            this.ItemForStarTime.Name = "ItemForStarTime";
            this.ItemForStarTime.Size = new System.Drawing.Size(780, 44);
            this.ItemForStarTime.Text = "Star Time";
            this.ItemForStarTime.TextSize = new System.Drawing.Size(160, 25);
            // 
            // ItemForGrabTradeHistory
            // 
            this.ItemForGrabTradeHistory.Control = this.GrabTradeHistoryCheckEdit;
            this.ItemForGrabTradeHistory.Location = new System.Drawing.Point(0, 88);
            this.ItemForGrabTradeHistory.Name = "ItemForGrabTradeHistory";
            this.ItemForGrabTradeHistory.Size = new System.Drawing.Size(780, 42);
            this.ItemForGrabTradeHistory.Text = "Grab Trade History";
            this.ItemForGrabTradeHistory.TextSize = new System.Drawing.Size(0, 0);
            this.ItemForGrabTradeHistory.TextVisible = false;
            // 
            // ItemForGrabChart
            // 
            this.ItemForGrabChart.Control = this.GrabChartCheckEdit;
            this.ItemForGrabChart.Location = new System.Drawing.Point(0, 130);
            this.ItemForGrabChart.Name = "ItemForGrabChart";
            this.ItemForGrabChart.Size = new System.Drawing.Size(780, 42);
            this.ItemForGrabChart.Text = "Grab Chart";
            this.ItemForGrabChart.TextSize = new System.Drawing.Size(0, 0);
            this.ItemForGrabChart.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.simpleButton1;
            this.layoutControlItem1.Location = new System.Drawing.Point(431, 372);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(186, 48);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButton2;
            this.layoutControlItem2.Location = new System.Drawing.Point(617, 372);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(163, 48);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 172);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(780, 200);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.buttonEdit1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 44);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(780, 44);
            this.layoutControlItem3.Text = "Save to Directory";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(160, 25);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 372);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(431, 48);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // grabDataSettingsBindingSource
            // 
            this.grabDataSettingsBindingSource.DataSource = typeof(CryptoMarketClient.Common.GrabDataSettings);
            // 
            // GrabDataSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 440);
            this.Controls.Add(this.dataLayoutControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GrabDataSettingsForm";
            this.Text = "Grab Data Settings";
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StarTimeDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StarTimeDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrabTradeHistoryCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrabChartCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForStarTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForGrabTradeHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForGrabChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grabDataSettingsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource grabDataSettingsBindingSource;
        private DevExpress.XtraEditors.DateEdit StarTimeDateEdit;
        private DevExpress.XtraEditors.CheckEdit GrabTradeHistoryCheckEdit;
        private DevExpress.XtraEditors.CheckEdit GrabChartCheckEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem ItemForStarTime;
        private DevExpress.XtraLayout.LayoutControlItem ItemForGrabTradeHistory;
        private DevExpress.XtraLayout.LayoutControlItem ItemForGrabChart;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}