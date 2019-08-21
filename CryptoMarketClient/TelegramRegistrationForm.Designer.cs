using DevExpress.XtraLayout;

namespace CryptoMarketClient {
    partial class TelegramRegistrationForm {
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.simpleButton1);
            this.layoutControl1.Controls.Add(this.simpleButton2);
            this.layoutControl1.Controls.Add(this.textEdit1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(107, 352, 1300, 800);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(974, 267);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // simpleButton1
            // 
            this.simpleButton1.AutoWidthInLayoutControl = true;
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.simpleButton1.Location = new System.Drawing.Point(614, 211);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Padding = new System.Windows.Forms.Padding(60, 0, 60, 0);
            this.simpleButton1.Size = new System.Drawing.Size(165, 44);
            this.simpleButton1.StyleController = this.layoutControl1;
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "OK";
            // 
            // simpleButton2
            // 
            this.simpleButton2.AutoWidthInLayoutControl = true;
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(803, 211);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Padding = new System.Windows.Forms.Padding(40, 0, 40, 0);
            this.simpleButton2.Size = new System.Drawing.Size(159, 44);
            this.simpleButton2.StyleController = this.layoutControl1;
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "Cancel";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(12, 61);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new System.Drawing.Size(950, 40);
            this.textEdit1.StyleController = this.layoutControl1;
            this.textEdit1.TabIndex = 6;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.simpleLabelItem1,
            this.layoutControlItem3,
            this.simpleLabelItem2});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(974, 267);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 199);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(602, 48);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButton2;
            this.layoutControlItem2.Location = new System.Drawing.Point(791, 199);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(163, 48);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.simpleButton1;
            this.layoutControlItem1.Location = new System.Drawing.Point(602, 199);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(189, 48);
            this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 20, 0, 0);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 142);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(954, 57);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseTextOptions = true;
            this.simpleLabelItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 0);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(954, 49);
            this.simpleLabelItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 20);
            this.simpleLabelItem1.StartNewLine = true;
            this.simpleLabelItem1.Text = "Please find bot \'@ultra_crypto_bot\', copy and send it this command";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(633, 25);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.textEdit1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 49);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(954, 44);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseTextOptions = true;
            this.simpleLabelItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.simpleLabelItem2.Location = new System.Drawing.Point(0, 93);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(954, 49);
            this.simpleLabelItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 20, 0);
            this.simpleLabelItem2.Text = "Then press OK button";
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(633, 25);
            // 
            // TelegramRegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 267);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TelegramRegistrationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Telegram Bot Registration";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private LayoutControl layoutControl1;
        private LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private EmptySpaceItem emptySpaceItem2;
        private LayoutControlItem layoutControlItem2;
        private LayoutControlItem layoutControlItem1;
        private EmptySpaceItem emptySpaceItem1;
        private SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private LayoutControlItem layoutControlItem3;
        private SimpleLabelItem simpleLabelItem2;
    }
}