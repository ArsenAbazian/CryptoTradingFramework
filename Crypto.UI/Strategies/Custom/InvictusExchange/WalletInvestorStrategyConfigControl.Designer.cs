namespace Crypto.UI.Strategies.Custom {
    partial class WalletInvestorStrategyConfigControl {
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
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.AllowTradeBinanceCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.walletInvestorForecastStrategyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.AllowTradeBittrexCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.CheckIntervalMinTextEdit = new DevExpress.XtraEditors.SpinEdit();
            this.textEdit1 = new DevExpress.XtraEditors.LabelControl();
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.spinEdit2 = new DevExpress.XtraEditors.SpinEdit();
            this.spinEdit3 = new DevExpress.XtraEditors.SpinEdit();
            this.spinEdit4 = new DevExpress.XtraEditors.SpinEdit();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit3 = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForCheckIntervalMin = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForAllowTradeBinance = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForAllowTradeBittrex = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AllowTradeBinanceCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.walletInvestorForecastStrategyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AllowTradeBittrexCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckIntervalMinTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForCheckIntervalMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForAllowTradeBinance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForAllowTradeBittrex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.AllowTradeBinanceCheckEdit);
            this.dataLayoutControl1.Controls.Add(this.AllowTradeBittrexCheckEdit);
            this.dataLayoutControl1.Controls.Add(this.CheckIntervalMinTextEdit);
            this.dataLayoutControl1.Controls.Add(this.textEdit1);
            this.dataLayoutControl1.Controls.Add(this.spinEdit1);
            this.dataLayoutControl1.Controls.Add(this.spinEdit2);
            this.dataLayoutControl1.Controls.Add(this.spinEdit3);
            this.dataLayoutControl1.Controls.Add(this.spinEdit4);
            this.dataLayoutControl1.Controls.Add(this.textEdit2);
            this.dataLayoutControl1.Controls.Add(this.textEdit3);
            this.dataLayoutControl1.DataSource = this.walletInvestorForecastStrategyBindingSource;
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.dataLayoutControl1.Size = new System.Drawing.Size(1212, 849);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // AllowTradeBinanceCheckEdit
            // 
            this.AllowTradeBinanceCheckEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.walletInvestorForecastStrategyBindingSource, "AllowTradeBinance", true));
            this.AllowTradeBinanceCheckEdit.Location = new System.Drawing.Point(12, 232);
            this.AllowTradeBinanceCheckEdit.Name = "AllowTradeBinanceCheckEdit";
            this.AllowTradeBinanceCheckEdit.Properties.Caption = "Allow Trade Binance";
            this.AllowTradeBinanceCheckEdit.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.AllowTradeBinanceCheckEdit.Size = new System.Drawing.Size(1188, 38);
            this.AllowTradeBinanceCheckEdit.StyleController = this.dataLayoutControl1;
            this.AllowTradeBinanceCheckEdit.TabIndex = 4;
            // 
            // walletInvestorForecastStrategyBindingSource
            // 
            this.walletInvestorForecastStrategyBindingSource.DataSource = typeof(Crypto.Core.Strategies.WalletInvestorForecastStrategy);
            // 
            // AllowTradeBittrexCheckEdit
            // 
            this.AllowTradeBittrexCheckEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.walletInvestorForecastStrategyBindingSource, "AllowTradeBittrex", true));
            this.AllowTradeBittrexCheckEdit.Location = new System.Drawing.Point(12, 274);
            this.AllowTradeBittrexCheckEdit.Name = "AllowTradeBittrexCheckEdit";
            this.AllowTradeBittrexCheckEdit.Properties.Caption = "Allow Trade Bittrex";
            this.AllowTradeBittrexCheckEdit.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.AllowTradeBittrexCheckEdit.Size = new System.Drawing.Size(1188, 38);
            this.AllowTradeBittrexCheckEdit.StyleController = this.dataLayoutControl1;
            this.AllowTradeBittrexCheckEdit.TabIndex = 5;
            // 
            // CheckIntervalMinTextEdit
            // 
            this.CheckIntervalMinTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.walletInvestorForecastStrategyBindingSource, "CheckIntervalHour", true));
            this.CheckIntervalMinTextEdit.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CheckIntervalMinTextEdit.Location = new System.Drawing.Point(315, 12);
            this.CheckIntervalMinTextEdit.Name = "CheckIntervalMinTextEdit";
            this.CheckIntervalMinTextEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.CheckIntervalMinTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.CheckIntervalMinTextEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CheckIntervalMinTextEdit.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.CheckIntervalMinTextEdit.Properties.Mask.EditMask = "N0";
            this.CheckIntervalMinTextEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.CheckIntervalMinTextEdit.Properties.MaxValue = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.CheckIntervalMinTextEdit.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CheckIntervalMinTextEdit.Size = new System.Drawing.Size(885, 40);
            this.CheckIntervalMinTextEdit.StyleController = this.dataLayoutControl1;
            this.CheckIntervalMinTextEdit.TabIndex = 6;
            // 
            // textEdit1
            // 
            this.textEdit1.Appearance.Options.UseTextOptions = true;
            this.textEdit1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.textEdit1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.textEdit1.Location = new System.Drawing.Point(12, 787);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(1188, 50);
            this.textEdit1.StyleController = this.dataLayoutControl1;
            this.textEdit1.TabIndex = 7;
            this.textEdit1.Text = "Trading will be unavailable without accounts, related to exchanges. You can speci" +
    "fy account using Api Key Form from Main Toolbar.";
            // 
            // spinEdit1
            // 
            this.spinEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.walletInvestorForecastStrategyBindingSource, "Day7MinPercent", true));
            this.spinEdit1.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEdit1.Location = new System.Drawing.Point(315, 100);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEdit1.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spinEdit1.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEdit1.Size = new System.Drawing.Size(885, 40);
            this.spinEdit1.StyleController = this.dataLayoutControl1;
            this.spinEdit1.TabIndex = 8;
            // 
            // spinEdit2
            // 
            this.spinEdit2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.walletInvestorForecastStrategyBindingSource, "Hour24MinPercent", true));
            this.spinEdit2.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEdit2.Location = new System.Drawing.Point(315, 56);
            this.spinEdit2.Name = "spinEdit2";
            this.spinEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEdit2.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spinEdit2.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEdit2.Size = new System.Drawing.Size(885, 40);
            this.spinEdit2.StyleController = this.dataLayoutControl1;
            this.spinEdit2.TabIndex = 9;
            // 
            // spinEdit3
            // 
            this.spinEdit3.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.walletInvestorForecastStrategyBindingSource, "Day14MinPercent", true));
            this.spinEdit3.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEdit3.Location = new System.Drawing.Point(315, 144);
            this.spinEdit3.Name = "spinEdit3";
            this.spinEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEdit3.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spinEdit3.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEdit3.Size = new System.Drawing.Size(885, 40);
            this.spinEdit3.StyleController = this.dataLayoutControl1;
            this.spinEdit3.TabIndex = 10;
            // 
            // spinEdit4
            // 
            this.spinEdit4.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.walletInvestorForecastStrategyBindingSource, "Month3MinPercent", true));
            this.spinEdit4.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEdit4.Location = new System.Drawing.Point(315, 188);
            this.spinEdit4.Name = "spinEdit4";
            this.spinEdit4.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEdit4.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spinEdit4.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEdit4.Size = new System.Drawing.Size(885, 40);
            this.spinEdit4.StyleController = this.dataLayoutControl1;
            this.spinEdit4.TabIndex = 11;
            // 
            // textEdit2
            // 
            this.textEdit2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.walletInvestorForecastStrategyBindingSource, "Login", true));
            this.textEdit2.Location = new System.Drawing.Point(315, 316);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Size = new System.Drawing.Size(885, 40);
            this.textEdit2.StyleController = this.dataLayoutControl1;
            this.textEdit2.TabIndex = 12;
            // 
            // textEdit3
            // 
            this.textEdit3.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.walletInvestorForecastStrategyBindingSource, "Password", true));
            this.textEdit3.Location = new System.Drawing.Point(315, 360);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Properties.PasswordChar = '*';
            this.textEdit3.Size = new System.Drawing.Size(885, 40);
            this.textEdit3.StyleController = this.dataLayoutControl1;
            this.textEdit3.TabIndex = 13;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1212, 849);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AllowDrawBackground = false;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForCheckIntervalMin,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.ItemForAllowTradeBinance,
            this.ItemForAllowTradeBittrex,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "autoGeneratedGroup0";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1192, 829);
            // 
            // ItemForCheckIntervalMin
            // 
            this.ItemForCheckIntervalMin.Control = this.CheckIntervalMinTextEdit;
            this.ItemForCheckIntervalMin.Location = new System.Drawing.Point(0, 0);
            this.ItemForCheckIntervalMin.Name = "ItemForCheckIntervalMin";
            this.ItemForCheckIntervalMin.Size = new System.Drawing.Size(1192, 44);
            this.ItemForCheckIntervalMin.Text = "Crypto Check Interval (hours)";
            this.ItemForCheckIntervalMin.TextSize = new System.Drawing.Size(300, 25);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.textEdit1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 775);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1192, 54);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.spinEdit1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 88);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1192, 44);
            this.layoutControlItem2.Text = "Minimum 7 Days Change (%)";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(300, 25);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.spinEdit2;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 44);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1192, 44);
            this.layoutControlItem3.Text = "Minimum 24 Hour Change (%)";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(300, 25);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.spinEdit3;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 132);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1192, 44);
            this.layoutControlItem4.Text = "Minimum 2 Weeks Change (%)";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(300, 25);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.spinEdit4;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 176);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(1192, 44);
            this.layoutControlItem5.Text = "Minimum 3 Months Change (%)";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(300, 25);
            // 
            // ItemForAllowTradeBinance
            // 
            this.ItemForAllowTradeBinance.Control = this.AllowTradeBinanceCheckEdit;
            this.ItemForAllowTradeBinance.Location = new System.Drawing.Point(0, 220);
            this.ItemForAllowTradeBinance.Name = "ItemForAllowTradeBinance";
            this.ItemForAllowTradeBinance.Size = new System.Drawing.Size(1192, 42);
            this.ItemForAllowTradeBinance.Text = "Allow Trade Binance";
            this.ItemForAllowTradeBinance.TextSize = new System.Drawing.Size(0, 0);
            this.ItemForAllowTradeBinance.TextVisible = false;
            // 
            // ItemForAllowTradeBittrex
            // 
            this.ItemForAllowTradeBittrex.Control = this.AllowTradeBittrexCheckEdit;
            this.ItemForAllowTradeBittrex.Location = new System.Drawing.Point(0, 262);
            this.ItemForAllowTradeBittrex.Name = "ItemForAllowTradeBittrex";
            this.ItemForAllowTradeBittrex.Size = new System.Drawing.Size(1192, 42);
            this.ItemForAllowTradeBittrex.Text = "Allow Trade Bittrex";
            this.ItemForAllowTradeBittrex.TextSize = new System.Drawing.Size(0, 0);
            this.ItemForAllowTradeBittrex.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.textEdit2;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 304);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(1192, 44);
            this.layoutControlItem6.Text = "Login/E-mail";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(300, 25);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.textEdit3;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 348);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(1192, 44);
            this.layoutControlItem7.Text = "Password";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(300, 25);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 392);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(1192, 383);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // WalletInvestorStrategyConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataLayoutControl1);
            this.Name = "WalletInvestorStrategyConfigControl";
            this.Size = new System.Drawing.Size(1212, 849);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AllowTradeBinanceCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.walletInvestorForecastStrategyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AllowTradeBittrexCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckIntervalMinTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForCheckIntervalMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForAllowTradeBinance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForAllowTradeBittrex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private System.Windows.Forms.BindingSource walletInvestorForecastStrategyBindingSource;
        private DevExpress.XtraEditors.CheckEdit AllowTradeBinanceCheckEdit;
        private DevExpress.XtraEditors.CheckEdit AllowTradeBittrexCheckEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem ItemForAllowTradeBinance;
        private DevExpress.XtraLayout.LayoutControlItem ItemForAllowTradeBittrex;
        private DevExpress.XtraLayout.LayoutControlItem ItemForCheckIntervalMin;
        private DevExpress.XtraEditors.SpinEdit CheckIntervalMinTextEdit;
        private DevExpress.XtraEditors.LabelControl textEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SpinEdit spinEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SpinEdit spinEdit2;
        private DevExpress.XtraEditors.SpinEdit spinEdit3;
        private DevExpress.XtraEditors.SpinEdit spinEdit4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.TextEdit textEdit3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
    }
}
