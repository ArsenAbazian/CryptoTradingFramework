namespace CryptoMarketClient {
    partial class EnterApiKeyForm {
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule4 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet4 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet4 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon10 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon11 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon12 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule5 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet5 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet5 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon13 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon14 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon15 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule6 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet6 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet6 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon16 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon17 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon18 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            this.gridControl1 = new MyGridControl();
            this.apiKeyInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMarket = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApiKey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.colSecret = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.apiKeyInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.apiKeyInfoBindingSource;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.gridControl1.Location = new System.Drawing.Point(28, 67);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1016, 415);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // apiKeyInfoBindingSource
            // 
            this.apiKeyInfoBindingSource.DataSource = typeof(CryptoMarketClient.ApiKeyInfo);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMarket,
            this.colApiKey,
            this.colSecret});
            gridFormatRule4.Name = "FormatRulePercentChange";
            formatConditionIconSet4.CategoryName = "Positive/Negative";
            formatConditionIconSetIcon10.PredefinedName = "Arrows3_3.png";
            formatConditionIconSetIcon10.Value = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            formatConditionIconSetIcon10.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon11.PredefinedName = "Arrows3_2.png";
            formatConditionIconSetIcon11.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon12.PredefinedName = "Arrows3_1.png";
            formatConditionIconSet4.Icons.Add(formatConditionIconSetIcon10);
            formatConditionIconSet4.Icons.Add(formatConditionIconSetIcon11);
            formatConditionIconSet4.Icons.Add(formatConditionIconSetIcon12);
            formatConditionIconSet4.Name = "PositiveNegativeArrows";
            formatConditionIconSet4.ValueType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            formatConditionRuleIconSet4.IconSet = formatConditionIconSet4;
            gridFormatRule4.Rule = formatConditionRuleIconSet4;
            gridFormatRule5.Name = "FormatRuleBidDelta";
            formatConditionIconSet5.CategoryName = "Positive/Negative";
            formatConditionIconSetIcon13.PredefinedName = "Arrows3_3.png";
            formatConditionIconSetIcon13.Value = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            formatConditionIconSetIcon13.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon14.PredefinedName = "Arrows3_2.png";
            formatConditionIconSetIcon14.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon15.PredefinedName = "Arrows3_1.png";
            formatConditionIconSet5.Icons.Add(formatConditionIconSetIcon13);
            formatConditionIconSet5.Icons.Add(formatConditionIconSetIcon14);
            formatConditionIconSet5.Icons.Add(formatConditionIconSetIcon15);
            formatConditionIconSet5.Name = "PositiveNegativeArrows";
            formatConditionIconSet5.ValueType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            formatConditionRuleIconSet5.IconSet = formatConditionIconSet5;
            gridFormatRule5.Rule = formatConditionRuleIconSet5;
            gridFormatRule6.Name = "FormatRuleAskDelta";
            formatConditionIconSet6.CategoryName = "Positive/Negative";
            formatConditionIconSetIcon16.PredefinedName = "Arrows3_3.png";
            formatConditionIconSetIcon16.Value = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            formatConditionIconSetIcon16.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon17.PredefinedName = "Arrows3_2.png";
            formatConditionIconSetIcon17.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon18.PredefinedName = "Arrows3_1.png";
            formatConditionIconSet6.Icons.Add(formatConditionIconSetIcon16);
            formatConditionIconSet6.Icons.Add(formatConditionIconSetIcon17);
            formatConditionIconSet6.Icons.Add(formatConditionIconSetIcon18);
            formatConditionIconSet6.Name = "PositiveNegativeArrows";
            formatConditionIconSet6.ValueType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            formatConditionRuleIconSet6.IconSet = formatConditionIconSet6;
            gridFormatRule6.Rule = formatConditionRuleIconSet6;
            this.gridView1.FormatRules.Add(gridFormatRule4);
            this.gridView1.FormatRules.Add(gridFormatRule5);
            this.gridView1.FormatRules.Add(gridFormatRule6);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colMarket
            // 
            this.colMarket.FieldName = "Market";
            this.colMarket.Name = "colMarket";
            this.colMarket.OptionsColumn.AllowEdit = false;
            this.colMarket.Visible = true;
            this.colMarket.VisibleIndex = 0;
            this.colMarket.Width = 394;
            // 
            // colApiKey
            // 
            this.colApiKey.FieldName = "ApiKey";
            this.colApiKey.Name = "colApiKey";
            this.colApiKey.Visible = true;
            this.colApiKey.VisibleIndex = 1;
            this.colApiKey.Width = 725;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.simpleButton1);
            this.layoutControl1.Controls.Add(this.simpleButton2);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1829, 63, 1462, 900);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1072, 567);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(28, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(736, 29);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Please enter API Key for every market if you want to make trading...";
            // 
            // simpleButton1
            // 
            this.simpleButton1.AutoWidthInLayoutControl = true;
            this.simpleButton1.Location = new System.Drawing.Point(717, 492);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Padding = new System.Windows.Forms.Padding(56, 0, 56, 0);
            this.simpleButton1.Size = new System.Drawing.Size(159, 47);
            this.simpleButton1.StyleController = this.layoutControl1;
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "OK";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.AutoWidthInLayoutControl = true;
            this.simpleButton2.Location = new System.Drawing.Point(886, 492);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Padding = new System.Windows.Forms.Padding(37, 0, 37, 0);
            this.simpleButton2.Size = new System.Drawing.Size(158, 47);
            this.simpleButton2.StyleController = this.layoutControl1;
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "Cancel";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 7;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1072, 567);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 39);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1026, 425);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButton1;
            this.layoutControlItem2.Location = new System.Drawing.Point(689, 464);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(169, 57);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButton2;
            this.layoutControlItem3.Location = new System.Drawing.Point(858, 464);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(168, 57);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 464);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(689, 57);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.labelControl1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1026, 39);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // colSecret
            // 
            this.colSecret.FieldName = "Secret";
            this.colSecret.Name = "colSecret";
            this.colSecret.Visible = true;
            this.colSecret.VisibleIndex = 2;
            // 
            // EnterApiKeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 567);
            this.Controls.Add(this.layoutControl1);
            this.Name = "EnterApiKeyForm";
            this.Text = "Api Keys";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.apiKeyInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MyGridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource apiKeyInfoBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colMarket;
        private DevExpress.XtraGrid.Columns.GridColumn colApiKey;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn colSecret;
    }
}