namespace CryptoMarketClient {
    partial class BuySettingsControl {
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
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.BuyPriceTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.tralingSettingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.AmoutTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.StopLossPricePercentTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.TakeProfitPercentTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.TakeProfitStartPercentTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForBuyPrice = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForAmout = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForStopLossPricePercent = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForTakeProfitPercent = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForTakeProfitStartPercent = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.checkEdit2 = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BuyPriceTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tralingSettingsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmoutTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StopLossPricePercentTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TakeProfitPercentTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TakeProfitStartPercentTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForBuyPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForAmout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForStopLossPricePercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTakeProfitPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTakeProfitStartPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Appearance.Control.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataLayoutControl1.Appearance.Control.Options.UseFont = true;
            this.dataLayoutControl1.Controls.Add(this.simpleButton1);
            this.dataLayoutControl1.Controls.Add(this.BuyPriceTextEdit);
            this.dataLayoutControl1.Controls.Add(this.AmoutTextEdit);
            this.dataLayoutControl1.Controls.Add(this.StopLossPricePercentTextEdit);
            this.dataLayoutControl1.Controls.Add(this.TakeProfitPercentTextEdit);
            this.dataLayoutControl1.Controls.Add(this.TakeProfitStartPercentTextEdit);
            this.dataLayoutControl1.Controls.Add(this.textEdit1);
            this.dataLayoutControl1.Controls.Add(this.checkEdit1);
            this.dataLayoutControl1.DataSource = this.tralingSettingsBindingSource;
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.dataLayoutControl1.Size = new System.Drawing.Size(866, 533);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // simpleButton1
            // 
            this.simpleButton1.AutoWidthInLayoutControl = true;
            this.simpleButton1.Location = new System.Drawing.Point(737, 257);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Padding = new System.Windows.Forms.Padding(40, 0, 40, 0);
            this.simpleButton1.Size = new System.Drawing.Size(115, 28);
            this.simpleButton1.StyleController = this.dataLayoutControl1;
            this.simpleButton1.TabIndex = 8;
            this.simpleButton1.Text = "Buy";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // BuyPriceTextEdit
            // 
            this.BuyPriceTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.tralingSettingsBindingSource, "BuyPrice", true));
            this.BuyPriceTextEdit.Location = new System.Drawing.Point(137, 12);
            this.BuyPriceTextEdit.Name = "BuyPriceTextEdit";
            // 
            // 
            // 
            this.BuyPriceTextEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.BuyPriceTextEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.BuyPriceTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.BuyPriceTextEdit.Properties.DisplayFormat.FormatString = "0.########";
            this.BuyPriceTextEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.BuyPriceTextEdit.Properties.Mask.EditMask = "f8";
            this.BuyPriceTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.BuyPriceTextEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.BuyPriceTextEdit.Properties.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.BuyPriceTextEdit.Size = new System.Drawing.Size(717, 28);
            this.BuyPriceTextEdit.StyleController = this.dataLayoutControl1;
            this.BuyPriceTextEdit.TabIndex = 4;
            // 
            // tralingSettingsBindingSource
            // 
            this.tralingSettingsBindingSource.DataSource = typeof(CryptoMarketClient.Common.TrailingSettings);
            // 
            // AmoutTextEdit
            // 
            this.AmoutTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.tralingSettingsBindingSource, "Amount", true));
            this.AmoutTextEdit.Location = new System.Drawing.Point(137, 44);
            this.AmoutTextEdit.Name = "AmoutTextEdit";
            // 
            // 
            // 
            this.AmoutTextEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.AmoutTextEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.AmoutTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.AmoutTextEdit.Properties.DisplayFormat.FormatString = "0.########";
            this.AmoutTextEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.AmoutTextEdit.Properties.Mask.EditMask = "f8";
            this.AmoutTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.AmoutTextEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.AmoutTextEdit.Properties.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.AmoutTextEdit.Size = new System.Drawing.Size(717, 28);
            this.AmoutTextEdit.StyleController = this.dataLayoutControl1;
            this.AmoutTextEdit.TabIndex = 5;
            // 
            // StopLossPricePercentTextEdit
            // 
            this.StopLossPricePercentTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.tralingSettingsBindingSource, "StopLossPricePercent", true));
            this.StopLossPricePercentTextEdit.Enabled = false;
            this.StopLossPricePercentTextEdit.Location = new System.Drawing.Point(137, 147);
            this.StopLossPricePercentTextEdit.Name = "StopLossPricePercentTextEdit";
            // 
            // 
            // 
            this.StopLossPricePercentTextEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.StopLossPricePercentTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.StopLossPricePercentTextEdit.Properties.Mask.EditMask = "P";
            this.StopLossPricePercentTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.StopLossPricePercentTextEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.StopLossPricePercentTextEdit.Properties.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.StopLossPricePercentTextEdit.Size = new System.Drawing.Size(717, 28);
            this.StopLossPricePercentTextEdit.StyleController = this.dataLayoutControl1;
            this.StopLossPricePercentTextEdit.TabIndex = 6;
            // 
            // TakeProfitPercentTextEdit
            // 
            this.TakeProfitPercentTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.tralingSettingsBindingSource, "TakeProfitPercent", true));
            this.TakeProfitPercentTextEdit.Enabled = false;
            this.TakeProfitPercentTextEdit.Location = new System.Drawing.Point(137, 211);
            this.TakeProfitPercentTextEdit.Name = "TakeProfitPercentTextEdit";
            // 
            // 
            // 
            this.TakeProfitPercentTextEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.TakeProfitPercentTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.TakeProfitPercentTextEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.TakeProfitPercentTextEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.TakeProfitPercentTextEdit.Properties.Mask.EditMask = "P";
            this.TakeProfitPercentTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.TakeProfitPercentTextEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.TakeProfitPercentTextEdit.Properties.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.TakeProfitPercentTextEdit.Size = new System.Drawing.Size(717, 28);
            this.TakeProfitPercentTextEdit.StyleController = this.dataLayoutControl1;
            this.TakeProfitPercentTextEdit.TabIndex = 8;
            // 
            // TakeProfitStartPercentTextEdit
            // 
            this.TakeProfitStartPercentTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.tralingSettingsBindingSource, "TakeProfitStartPercent", true));
            this.TakeProfitStartPercentTextEdit.Enabled = false;
            this.TakeProfitStartPercentTextEdit.Location = new System.Drawing.Point(137, 179);
            this.TakeProfitStartPercentTextEdit.Name = "TakeProfitStartPercentTextEdit";
            // 
            // 
            // 
            this.TakeProfitStartPercentTextEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.TakeProfitStartPercentTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.TakeProfitStartPercentTextEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.TakeProfitStartPercentTextEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.TakeProfitStartPercentTextEdit.Properties.Mask.EditMask = "P";
            this.TakeProfitStartPercentTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.TakeProfitStartPercentTextEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.TakeProfitStartPercentTextEdit.Properties.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.TakeProfitStartPercentTextEdit.Size = new System.Drawing.Size(717, 28);
            this.TakeProfitStartPercentTextEdit.StyleController = this.dataLayoutControl1;
            this.TakeProfitStartPercentTextEdit.TabIndex = 7;
            // 
            // textEdit1
            // 
            this.textEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.tralingSettingsBindingSource, "TotalSpendInBaseCurrency", true));
            this.textEdit1.Location = new System.Drawing.Point(137, 76);
            this.textEdit1.Name = "textEdit1";
            // 
            // 
            // 
            this.textEdit1.Properties.Appearance.Options.UseTextOptions = true;
            this.textEdit1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEdit1.Properties.DisplayFormat.FormatString = "0.########";
            this.textEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textEdit1.Properties.Mask.EditMask = "f8";
            this.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEdit1.Properties.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.textEdit1.Size = new System.Drawing.Size(717, 28);
            this.textEdit1.StyleController = this.dataLayoutControl1;
            this.textEdit1.TabIndex = 10;
            // 
            // checkEdit1
            // 
            this.checkEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.tralingSettingsBindingSource, "EnableIncrementalStopLoss", true));
            this.checkEdit1.Location = new System.Drawing.Point(139, 122);
            this.checkEdit1.Name = "checkEdit1";
            // 
            // 
            // 
            this.checkEdit1.Properties.Caption = "";
            this.checkEdit1.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.checkEdit1.Size = new System.Drawing.Size(713, 19);
            this.checkEdit1.StyleController = this.dataLayoutControl1;
            this.checkEdit1.TabIndex = 11;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup1.Size = new System.Drawing.Size(866, 533);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AllowDrawBackground = false;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForBuyPrice,
            this.ItemForAmout,
            this.ItemForStopLossPricePercent,
            this.ItemForTakeProfitPercent,
            this.ItemForTakeProfitStartPercent,
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "autoGeneratedGroup0";
            this.layoutControlGroup2.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup2.Size = new System.Drawing.Size(846, 513);
            // 
            // ItemForBuyPrice
            // 
            this.ItemForBuyPrice.Control = this.BuyPriceTextEdit;
            this.ItemForBuyPrice.Location = new System.Drawing.Point(0, 0);
            this.ItemForBuyPrice.Name = "ItemForBuyPrice";
            this.ItemForBuyPrice.Size = new System.Drawing.Size(846, 32);
            this.ItemForBuyPrice.Text = "Buy Price";
            this.ItemForBuyPrice.TextSize = new System.Drawing.Size(119, 13);
            // 
            // ItemForAmout
            // 
            this.ItemForAmout.Control = this.AmoutTextEdit;
            this.ItemForAmout.Location = new System.Drawing.Point(0, 32);
            this.ItemForAmout.Name = "ItemForAmout";
            this.ItemForAmout.Size = new System.Drawing.Size(846, 32);
            this.ItemForAmout.Text = "Amout";
            this.ItemForAmout.TextSize = new System.Drawing.Size(119, 13);
            // 
            // ItemForStopLossPricePercent
            // 
            this.ItemForStopLossPricePercent.Control = this.StopLossPricePercentTextEdit;
            this.ItemForStopLossPricePercent.Enabled = false;
            this.ItemForStopLossPricePercent.Location = new System.Drawing.Point(0, 135);
            this.ItemForStopLossPricePercent.Name = "ItemForStopLossPricePercent";
            this.ItemForStopLossPricePercent.Size = new System.Drawing.Size(846, 32);
            this.ItemForStopLossPricePercent.Text = "Stop Loss Price Percent";
            this.ItemForStopLossPricePercent.TextSize = new System.Drawing.Size(119, 13);
            // 
            // ItemForTakeProfitPercent
            // 
            this.ItemForTakeProfitPercent.Control = this.TakeProfitPercentTextEdit;
            this.ItemForTakeProfitPercent.Enabled = false;
            this.ItemForTakeProfitPercent.Location = new System.Drawing.Point(0, 199);
            this.ItemForTakeProfitPercent.Name = "ItemForTakeProfitPercent";
            this.ItemForTakeProfitPercent.Size = new System.Drawing.Size(846, 32);
            this.ItemForTakeProfitPercent.Text = "Take Profit Percent";
            this.ItemForTakeProfitPercent.TextSize = new System.Drawing.Size(119, 13);
            // 
            // ItemForTakeProfitStartPercent
            // 
            this.ItemForTakeProfitStartPercent.Control = this.TakeProfitStartPercentTextEdit;
            this.ItemForTakeProfitStartPercent.Enabled = false;
            this.ItemForTakeProfitStartPercent.Location = new System.Drawing.Point(0, 167);
            this.ItemForTakeProfitStartPercent.Name = "ItemForTakeProfitStartPercent";
            this.ItemForTakeProfitStartPercent.Size = new System.Drawing.Size(846, 32);
            this.ItemForTakeProfitStartPercent.Text = "Take Profit Start Percent";
            this.ItemForTakeProfitStartPercent.TextSize = new System.Drawing.Size(119, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 231);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(723, 282);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.simpleButton1;
            this.layoutControlItem1.Location = new System.Drawing.Point(723, 231);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 16, 4);
            this.layoutControlItem1.Size = new System.Drawing.Size(123, 282);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.textEdit1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 64);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(846, 32);
            this.layoutControlItem3.Text = "Spend BTC";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(119, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.checkEdit1;
            this.layoutControlItem2.CustomizationFormText = "Enable Trailing (local)";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 16, 4);
            this.layoutControlItem2.Size = new System.Drawing.Size(846, 39);
            this.layoutControlItem2.Text = "Enable Trailing (local)";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(119, 13);
            // 
            // BuySettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataLayoutControl1);
            this.Name = "BuySettingsControl";
            this.Size = new System.Drawing.Size(866, 533);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BuyPriceTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tralingSettingsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmoutTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StopLossPricePercentTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TakeProfitPercentTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TakeProfitStartPercentTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForBuyPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForAmout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForStopLossPricePercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTakeProfitPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTakeProfitStartPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private System.Windows.Forms.BindingSource tralingSettingsBindingSource;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit BuyPriceTextEdit;
        private DevExpress.XtraEditors.TextEdit AmoutTextEdit;
        private DevExpress.XtraEditors.TextEdit StopLossPricePercentTextEdit;
        private DevExpress.XtraEditors.TextEdit TakeProfitPercentTextEdit;
        private DevExpress.XtraEditors.TextEdit TakeProfitStartPercentTextEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem ItemForBuyPrice;
        private DevExpress.XtraLayout.LayoutControlItem ItemForAmout;
        private DevExpress.XtraLayout.LayoutControlItem ItemForStopLossPricePercent;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTakeProfitPercent;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTakeProfitStartPercent;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.CheckEdit checkEdit2;
    }
}