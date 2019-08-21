namespace CryptoMarketClient {
    partial class AccountEditingForm {
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
            this.btCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btOk = new DevExpress.XtraEditors.SimpleButton();
            this.NameTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.TypeImageComboBoxEdit = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.ApiKeyTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.SecretTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForName = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForApiKey = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForSecret = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.ItemForType = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.exchangeAccountInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NameTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeImageComboBoxEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ApiKeyTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecretTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForApiKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSecret)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exchangeAccountInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Appearance.Control.FontSizeDelta = 2;
            this.dataLayoutControl1.Appearance.Control.Options.UseFont = true;
            this.dataLayoutControl1.Controls.Add(this.btCancel);
            this.dataLayoutControl1.Controls.Add(this.btOk);
            this.dataLayoutControl1.Controls.Add(this.NameTextEdit);
            this.dataLayoutControl1.Controls.Add(this.TypeImageComboBoxEdit);
            this.dataLayoutControl1.Controls.Add(this.ApiKeyTextEdit);
            this.dataLayoutControl1.Controls.Add(this.SecretTextEdit);
            this.dataLayoutControl1.DataSource = this.exchangeAccountInfoBindingSource;
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Margin = new System.Windows.Forms.Padding(5);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.dataLayoutControl1.Size = new System.Drawing.Size(777, 334);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // btCancel
            // 
            this.btCancel.AutoWidthInLayoutControl = true;
            this.btCancel.Location = new System.Drawing.Point(567, 278);
            this.btCancel.Name = "btCancel";
            this.btCancel.Padding = new System.Windows.Forms.Padding(52, 0, 52, 0);
            this.btCancel.Size = new System.Drawing.Size(198, 44);
            this.btCancel.StyleController = this.dataLayoutControl1;
            this.btCancel.TabIndex = 11;
            this.btCancel.Text = "Cancel";
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOk
            // 
            this.btOk.AutoWidthInLayoutControl = true;
            this.btOk.Location = new System.Drawing.Point(408, 278);
            this.btOk.Name = "btOk";
            this.btOk.Padding = new System.Windows.Forms.Padding(52, 0, 52, 0);
            this.btOk.Size = new System.Drawing.Size(155, 44);
            this.btOk.StyleController = this.dataLayoutControl1;
            this.btOk.TabIndex = 10;
            this.btOk.Text = "OK";
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // NameTextEdit
            // 
            this.NameTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.exchangeAccountInfoBindingSource, "Name", true));
            this.NameTextEdit.Location = new System.Drawing.Point(101, 62);
            this.NameTextEdit.Name = "NameTextEdit";
            this.NameTextEdit.Size = new System.Drawing.Size(664, 46);
            this.NameTextEdit.StyleController = this.dataLayoutControl1;
            this.NameTextEdit.TabIndex = 4;
            // 
            // TypeImageComboBoxEdit
            // 
            this.TypeImageComboBoxEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.exchangeAccountInfoBindingSource, "Type", true));
            this.TypeImageComboBoxEdit.Location = new System.Drawing.Point(101, 12);
            this.TypeImageComboBoxEdit.Name = "TypeImageComboBoxEdit";
            this.TypeImageComboBoxEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.TypeImageComboBoxEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.TypeImageComboBoxEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TypeImageComboBoxEdit.Properties.UseCtrlScroll = true;
            this.TypeImageComboBoxEdit.Size = new System.Drawing.Size(664, 46);
            this.TypeImageComboBoxEdit.StyleController = this.dataLayoutControl1;
            this.TypeImageComboBoxEdit.TabIndex = 7;
            // 
            // ApiKeyTextEdit
            // 
            this.ApiKeyTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.exchangeAccountInfoBindingSource, "ApiKey", true));
            this.ApiKeyTextEdit.Location = new System.Drawing.Point(101, 112);
            this.ApiKeyTextEdit.Name = "ApiKeyTextEdit";
            this.ApiKeyTextEdit.Size = new System.Drawing.Size(664, 46);
            this.ApiKeyTextEdit.StyleController = this.dataLayoutControl1;
            this.ApiKeyTextEdit.TabIndex = 8;
            // 
            // SecretTextEdit
            // 
            this.SecretTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.exchangeAccountInfoBindingSource, "Secret", true));
            this.SecretTextEdit.Location = new System.Drawing.Point(101, 162);
            this.SecretTextEdit.Name = "SecretTextEdit";
            this.SecretTextEdit.Size = new System.Drawing.Size(664, 46);
            this.SecretTextEdit.StyleController = this.dataLayoutControl1;
            this.SecretTextEdit.TabIndex = 9;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.emptySpaceItem2});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(777, 334);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AllowDrawBackground = false;
            this.layoutControlGroup2.AppearanceItemCaption.FontSizeDelta = 2;
            this.layoutControlGroup2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForName,
            this.ItemForApiKey,
            this.ItemForSecret,
            this.emptySpaceItem1,
            this.ItemForType});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "autoGeneratedGroup0";
            this.layoutControlGroup2.Size = new System.Drawing.Size(757, 266);
            // 
            // ItemForName
            // 
            this.ItemForName.Control = this.NameTextEdit;
            this.ItemForName.Location = new System.Drawing.Point(0, 50);
            this.ItemForName.Name = "ItemForName";
            this.ItemForName.Size = new System.Drawing.Size(757, 50);
            this.ItemForName.Text = "Name";
            this.ItemForName.TextSize = new System.Drawing.Size(86, 31);
            // 
            // ItemForApiKey
            // 
            this.ItemForApiKey.Control = this.ApiKeyTextEdit;
            this.ItemForApiKey.Location = new System.Drawing.Point(0, 100);
            this.ItemForApiKey.Name = "ItemForApiKey";
            this.ItemForApiKey.Size = new System.Drawing.Size(757, 50);
            this.ItemForApiKey.Text = "Api Key";
            this.ItemForApiKey.TextSize = new System.Drawing.Size(86, 31);
            // 
            // ItemForSecret
            // 
            this.ItemForSecret.Control = this.SecretTextEdit;
            this.ItemForSecret.Location = new System.Drawing.Point(0, 150);
            this.ItemForSecret.Name = "ItemForSecret";
            this.ItemForSecret.Size = new System.Drawing.Size(757, 50);
            this.ItemForSecret.Text = "Secret";
            this.ItemForSecret.TextSize = new System.Drawing.Size(86, 31);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 200);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(757, 66);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ItemForType
            // 
            this.ItemForType.Control = this.TypeImageComboBoxEdit;
            this.ItemForType.Location = new System.Drawing.Point(0, 0);
            this.ItemForType.Name = "ItemForType";
            this.ItemForType.Size = new System.Drawing.Size(757, 50);
            this.ItemForType.Text = "Type";
            this.ItemForType.TextSize = new System.Drawing.Size(86, 31);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btCancel;
            this.layoutControlItem2.Location = new System.Drawing.Point(555, 266);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(202, 48);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btOk;
            this.layoutControlItem1.Location = new System.Drawing.Point(396, 266);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(159, 48);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 266);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(396, 48);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // exchangeAccountInfoBindingSource
            // 
            this.exchangeAccountInfoBindingSource.DataSource = typeof(CryptoMarketClient.AccountInfo);
            // 
            // AccountEditingForm
            // 
            this.Appearance.FontSizeDelta = 2;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(962F, 2117F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 334);
            this.Controls.Add(this.dataLayoutControl1);
            this.Font = new System.Drawing.Font("Tahoma", 657.875F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "AccountEditingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Account";
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NameTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeImageComboBoxEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ApiKeyTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecretTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForApiKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSecret)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exchangeAccountInfoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource exchangeAccountInfoBindingSource;
        private DevExpress.XtraEditors.TextEdit NameTextEdit;
        private DevExpress.XtraEditors.ImageComboBoxEdit TypeImageComboBoxEdit;
        private DevExpress.XtraEditors.TextEdit ApiKeyTextEdit;
        private DevExpress.XtraEditors.TextEdit SecretTextEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem ItemForName;
        private DevExpress.XtraLayout.LayoutControlItem ItemForType;
        private DevExpress.XtraLayout.LayoutControlItem ItemForApiKey;
        private DevExpress.XtraLayout.LayoutControlItem ItemForSecret;
        private DevExpress.XtraEditors.SimpleButton btCancel;
        private DevExpress.XtraEditors.SimpleButton btOk;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    }
}