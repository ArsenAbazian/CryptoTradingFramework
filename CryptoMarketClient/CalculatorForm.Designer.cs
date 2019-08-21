namespace CryptoMarketClient {
    partial class CalculatorForm {
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
            this.tePrice = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.teMinimalSpread = new DevExpress.XtraEditors.TextEdit();
            this.teAmount = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.teSellPrice = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.teSpread = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.teProfit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.teUsdProfit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.teMinimalSell = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.tePrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teMinimalSpread.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teSellPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teSpread.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teProfit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUsdProfit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teMinimalSell.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tePrice
            // 
            this.tePrice.EditValue = "0";
            this.tePrice.Location = new System.Drawing.Point(45, 123);
            this.tePrice.Name = "tePrice";
            this.tePrice.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tePrice.Properties.Appearance.Options.UseFont = true;
            this.tePrice.Properties.Appearance.Options.UseTextOptions = true;
            this.tePrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tePrice.Properties.DisplayFormat.FormatString = "0.00000000";
            this.tePrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.tePrice.Properties.EditFormat.FormatString = "0.00000000";
            this.tePrice.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.tePrice.Properties.Mask.EditMask = "f8";
            this.tePrice.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.tePrice.Size = new System.Drawing.Size(454, 66);
            this.tePrice.TabIndex = 0;
            this.tePrice.EditValueChanged += new System.EventHandler(this.OnInputValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(183, 52);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(194, 65);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Buy Price";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(183, 607);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(170, 65);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Amount";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(103, 235);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(332, 65);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Minimal Spread";
            // 
            // teMinimalSpread
            // 
            this.teMinimalSpread.Location = new System.Drawing.Point(45, 306);
            this.teMinimalSpread.Name = "teMinimalSpread";
            this.teMinimalSpread.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teMinimalSpread.Properties.Appearance.Options.UseFont = true;
            this.teMinimalSpread.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.teMinimalSpread.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.teMinimalSpread.Properties.DisplayFormat.FormatString = "0.00000000";
            this.teMinimalSpread.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teMinimalSpread.Properties.EditFormat.FormatString = "0.00000000";
            this.teMinimalSpread.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teMinimalSpread.Properties.ReadOnly = true;
            this.teMinimalSpread.Size = new System.Drawing.Size(454, 66);
            this.teMinimalSpread.TabIndex = 4;
            // 
            // teAmount
            // 
            this.teAmount.EditValue = "0";
            this.teAmount.Location = new System.Drawing.Point(45, 678);
            this.teAmount.Name = "teAmount";
            this.teAmount.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teAmount.Properties.Appearance.Options.UseFont = true;
            this.teAmount.Properties.Appearance.Options.UseTextOptions = true;
            this.teAmount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.teAmount.Properties.DisplayFormat.FormatString = "0.00000000";
            this.teAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teAmount.Properties.EditFormat.FormatString = "0.00000000";
            this.teAmount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teAmount.Properties.Mask.EditMask = "f8";
            this.teAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.teAmount.Size = new System.Drawing.Size(454, 66);
            this.teAmount.TabIndex = 6;
            this.teAmount.EditValueChanged += new System.EventHandler(this.OnInputValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(694, 52);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(191, 65);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Sell Price";
            // 
            // teSellPrice
            // 
            this.teSellPrice.EditValue = "0";
            this.teSellPrice.Location = new System.Drawing.Point(556, 123);
            this.teSellPrice.Name = "teSellPrice";
            this.teSellPrice.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teSellPrice.Properties.Appearance.Options.UseFont = true;
            this.teSellPrice.Properties.Appearance.Options.UseTextOptions = true;
            this.teSellPrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.teSellPrice.Properties.DisplayFormat.FormatString = "0.00000000";
            this.teSellPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teSellPrice.Properties.EditFormat.FormatString = "0.00000000";
            this.teSellPrice.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teSellPrice.Properties.Mask.EditMask = "f8";
            this.teSellPrice.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.teSellPrice.Size = new System.Drawing.Size(454, 66);
            this.teSellPrice.TabIndex = 7;
            this.teSellPrice.EditValueChanged += new System.EventHandler(this.OnInputValueChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(713, 235);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(148, 65);
            this.labelControl5.TabIndex = 10;
            this.labelControl5.Text = "Spread";
            // 
            // teSpread
            // 
            this.teSpread.Location = new System.Drawing.Point(556, 306);
            this.teSpread.Name = "teSpread";
            this.teSpread.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teSpread.Properties.Appearance.Options.UseFont = true;
            this.teSpread.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.teSpread.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.teSpread.Properties.DisplayFormat.FormatString = "0.00000000";
            this.teSpread.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teSpread.Properties.EditFormat.FormatString = "0.00000000";
            this.teSpread.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teSpread.Properties.ReadOnly = true;
            this.teSpread.Size = new System.Drawing.Size(454, 66);
            this.teSpread.TabIndex = 9;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(727, 418);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(115, 65);
            this.labelControl6.TabIndex = 12;
            this.labelControl6.Text = "Profit";
            // 
            // teProfit
            // 
            this.teProfit.Location = new System.Drawing.Point(556, 489);
            this.teProfit.Name = "teProfit";
            this.teProfit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teProfit.Properties.Appearance.Options.UseFont = true;
            this.teProfit.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.teProfit.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.teProfit.Properties.DisplayFormat.FormatString = "0.00000000";
            this.teProfit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teProfit.Properties.EditFormat.FormatString = "0.00000000";
            this.teProfit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teProfit.Properties.ReadOnly = true;
            this.teProfit.Size = new System.Drawing.Size(454, 66);
            this.teProfit.TabIndex = 11;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(698, 607);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(187, 65);
            this.labelControl7.TabIndex = 14;
            this.labelControl7.Text = "US Profit";
            // 
            // teUsdProfit
            // 
            this.teUsdProfit.Location = new System.Drawing.Point(556, 678);
            this.teUsdProfit.Name = "teUsdProfit";
            this.teUsdProfit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teUsdProfit.Properties.Appearance.Options.UseFont = true;
            this.teUsdProfit.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.teUsdProfit.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.teUsdProfit.Properties.DisplayFormat.FormatString = "0.00000000";
            this.teUsdProfit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teUsdProfit.Properties.EditFormat.FormatString = "0.##";
            this.teUsdProfit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teUsdProfit.Properties.ReadOnly = true;
            this.teUsdProfit.Size = new System.Drawing.Size(454, 66);
            this.teUsdProfit.TabIndex = 13;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Location = new System.Drawing.Point(80, 418);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(375, 65);
            this.labelControl8.TabIndex = 16;
            this.labelControl8.Text = "Minimal Sell Price";
            // 
            // teMinimalSell
            // 
            this.teMinimalSell.Location = new System.Drawing.Point(45, 489);
            this.teMinimalSell.Name = "teMinimalSell";
            this.teMinimalSell.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teMinimalSell.Properties.Appearance.Options.UseFont = true;
            this.teMinimalSell.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.teMinimalSell.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.teMinimalSell.Properties.DisplayFormat.FormatString = "0.00000000";
            this.teMinimalSell.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teMinimalSell.Properties.EditFormat.FormatString = "0.00000000";
            this.teMinimalSell.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teMinimalSell.Properties.ReadOnly = true;
            this.teMinimalSell.Size = new System.Drawing.Size(454, 66);
            this.teMinimalSell.TabIndex = 15;
            // 
            // CalculatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 786);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.teMinimalSell);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.teUsdProfit);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.teProfit);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.teSpread);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.teSellPrice);
            this.Controls.Add(this.teAmount);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.teMinimalSpread);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.tePrice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CalculatorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calculator";
            ((System.ComponentModel.ISupportInitialize)(this.tePrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teMinimalSpread.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teSellPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teSpread.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teProfit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUsdProfit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teMinimalSell.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit tePrice;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit teMinimalSpread;
        private DevExpress.XtraEditors.TextEdit teAmount;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit teSellPrice;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit teSpread;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit teProfit;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit teUsdProfit;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit teMinimalSell;
    }
}