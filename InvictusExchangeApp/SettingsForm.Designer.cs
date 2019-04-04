using DevExpress.XtraDataLayout;
using DevExpress.XtraLayout;

namespace InvictusExchangeApp {
    partial class SettingsForm {
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
            this.LoginTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.PasswordTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.Min24HourChangeTextEdit = new DevExpress.XtraEditors.SpinEdit();
            this.Min7DaysChangeTextEdit = new DevExpress.XtraEditors.SpinEdit();
            this.Min14DayChangeTextEdit = new DevExpress.XtraEditors.SpinEdit();
            this.Min3MonthChangeTextEdit = new DevExpress.XtraEditors.SpinEdit();
            this.sbOk = new DevExpress.XtraEditors.SimpleButton();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForLogin = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForPassword = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForMin24HourChange = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForMin7DaysChange = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForMin14DayChange = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForMin3MonthChange = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.invictusSettingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LoginTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasswordTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Min24HourChangeTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Min7DaysChangeTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Min14DayChangeTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Min3MonthChangeTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMin24HourChange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMin7DaysChange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMin14DayChange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMin3MonthChange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.invictusSettingsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.LoginTextEdit);
            this.dataLayoutControl1.Controls.Add(this.PasswordTextEdit);
            this.dataLayoutControl1.Controls.Add(this.Min24HourChangeTextEdit);
            this.dataLayoutControl1.Controls.Add(this.Min7DaysChangeTextEdit);
            this.dataLayoutControl1.Controls.Add(this.Min14DayChangeTextEdit);
            this.dataLayoutControl1.Controls.Add(this.Min3MonthChangeTextEdit);
            this.dataLayoutControl1.Controls.Add(this.sbOk);
            this.dataLayoutControl1.Controls.Add(this.sbCancel);
            this.dataLayoutControl1.DataSource = this.invictusSettingsBindingSource;
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.dataLayoutControl1.Size = new System.Drawing.Size(845, 650);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // LoginTextEdit
            // 
            this.LoginTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.invictusSettingsBindingSource, "Login", true));
            this.LoginTextEdit.Location = new System.Drawing.Point(198, 12);
            this.LoginTextEdit.Name = "LoginTextEdit";
            this.LoginTextEdit.Size = new System.Drawing.Size(635, 40);
            this.LoginTextEdit.StyleController = this.dataLayoutControl1;
            this.LoginTextEdit.TabIndex = 4;
            // 
            // PasswordTextEdit
            // 
            this.PasswordTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.invictusSettingsBindingSource, "Password", true));
            this.PasswordTextEdit.Location = new System.Drawing.Point(198, 56);
            this.PasswordTextEdit.Name = "PasswordTextEdit";
            this.PasswordTextEdit.Properties.PasswordChar = '*';
            this.PasswordTextEdit.Size = new System.Drawing.Size(635, 40);
            this.PasswordTextEdit.StyleController = this.dataLayoutControl1;
            this.PasswordTextEdit.TabIndex = 5;
            // 
            // Min24HourChangeTextEdit
            // 
            this.Min24HourChangeTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.invictusSettingsBindingSource, "Min24HourChange", true));
            this.Min24HourChangeTextEdit.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Min24HourChangeTextEdit.Location = new System.Drawing.Point(198, 100);
            this.Min24HourChangeTextEdit.Name = "Min24HourChangeTextEdit";
            this.Min24HourChangeTextEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.Min24HourChangeTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Min24HourChangeTextEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Min24HourChangeTextEdit.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.Min24HourChangeTextEdit.Properties.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Min24HourChangeTextEdit.Properties.IsFloatValue = false;
            this.Min24HourChangeTextEdit.Properties.Mask.EditMask = "d";
            this.Min24HourChangeTextEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.Min24HourChangeTextEdit.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Min24HourChangeTextEdit.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Min24HourChangeTextEdit.Size = new System.Drawing.Size(635, 40);
            this.Min24HourChangeTextEdit.StyleController = this.dataLayoutControl1;
            this.Min24HourChangeTextEdit.TabIndex = 6;
            // 
            // Min7DaysChangeTextEdit
            // 
            this.Min7DaysChangeTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.invictusSettingsBindingSource, "Min7DaysChange", true));
            this.Min7DaysChangeTextEdit.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Min7DaysChangeTextEdit.Location = new System.Drawing.Point(198, 144);
            this.Min7DaysChangeTextEdit.Name = "Min7DaysChangeTextEdit";
            this.Min7DaysChangeTextEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.Min7DaysChangeTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Min7DaysChangeTextEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Min7DaysChangeTextEdit.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.Min7DaysChangeTextEdit.Properties.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Min7DaysChangeTextEdit.Properties.Mask.EditMask = "d";
            this.Min7DaysChangeTextEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.Min7DaysChangeTextEdit.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Min7DaysChangeTextEdit.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Min7DaysChangeTextEdit.Size = new System.Drawing.Size(635, 40);
            this.Min7DaysChangeTextEdit.StyleController = this.dataLayoutControl1;
            this.Min7DaysChangeTextEdit.TabIndex = 7;
            // 
            // Min14DayChangeTextEdit
            // 
            this.Min14DayChangeTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.invictusSettingsBindingSource, "Min14DayChange", true));
            this.Min14DayChangeTextEdit.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Min14DayChangeTextEdit.Location = new System.Drawing.Point(198, 188);
            this.Min14DayChangeTextEdit.Name = "Min14DayChangeTextEdit";
            this.Min14DayChangeTextEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.Min14DayChangeTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Min14DayChangeTextEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Min14DayChangeTextEdit.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.Min14DayChangeTextEdit.Properties.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Min14DayChangeTextEdit.Properties.Mask.EditMask = "d";
            this.Min14DayChangeTextEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.Min14DayChangeTextEdit.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Min14DayChangeTextEdit.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Min14DayChangeTextEdit.Size = new System.Drawing.Size(635, 40);
            this.Min14DayChangeTextEdit.StyleController = this.dataLayoutControl1;
            this.Min14DayChangeTextEdit.TabIndex = 8;
            // 
            // Min3MonthChangeTextEdit
            // 
            this.Min3MonthChangeTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.invictusSettingsBindingSource, "Min3MonthChange", true));
            this.Min3MonthChangeTextEdit.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Min3MonthChangeTextEdit.Location = new System.Drawing.Point(198, 232);
            this.Min3MonthChangeTextEdit.Name = "Min3MonthChangeTextEdit";
            this.Min3MonthChangeTextEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.Min3MonthChangeTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Min3MonthChangeTextEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Min3MonthChangeTextEdit.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.Min3MonthChangeTextEdit.Properties.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Min3MonthChangeTextEdit.Properties.Mask.EditMask = "d";
            this.Min3MonthChangeTextEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.Min3MonthChangeTextEdit.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Min3MonthChangeTextEdit.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Min3MonthChangeTextEdit.Size = new System.Drawing.Size(635, 40);
            this.Min3MonthChangeTextEdit.StyleController = this.dataLayoutControl1;
            this.Min3MonthChangeTextEdit.TabIndex = 9;
            // 
            // sbOk
            // 
            this.sbOk.AutoWidthInLayoutControl = true;
            this.sbOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.sbOk.Location = new System.Drawing.Point(505, 594);
            this.sbOk.Name = "sbOk";
            this.sbOk.Padding = new System.Windows.Forms.Padding(60, 0, 60, 0);
            this.sbOk.Size = new System.Drawing.Size(165, 44);
            this.sbOk.StyleController = this.dataLayoutControl1;
            this.sbOk.TabIndex = 10;
            this.sbOk.Text = "OK";
            // 
            // sbCancel
            // 
            this.sbCancel.AutoWidthInLayoutControl = true;
            this.sbCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbCancel.Location = new System.Drawing.Point(674, 594);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Padding = new System.Windows.Forms.Padding(40, 0, 40, 0);
            this.sbCancel.Size = new System.Drawing.Size(159, 44);
            this.sbCancel.StyleController = this.dataLayoutControl1;
            this.sbCancel.TabIndex = 11;
            this.sbCancel.Text = "Cancel";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup1.Size = new System.Drawing.Size(845, 650);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AllowDrawBackground = false;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForLogin,
            this.ItemForPassword,
            this.ItemForMin24HourChange,
            this.ItemForMin7DaysChange,
            this.ItemForMin14DayChange,
            this.ItemForMin3MonthChange,
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.emptySpaceItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "autoGeneratedGroup0";
            this.layoutControlGroup2.Size = new System.Drawing.Size(825, 630);
            // 
            // ItemForLogin
            // 
            this.ItemForLogin.Control = this.LoginTextEdit;
            this.ItemForLogin.Location = new System.Drawing.Point(0, 0);
            this.ItemForLogin.Name = "ItemForLogin";
            this.ItemForLogin.Size = new System.Drawing.Size(825, 44);
            this.ItemForLogin.Text = "Login";
            this.ItemForLogin.TextSize = new System.Drawing.Size(180, 25);
            // 
            // ItemForPassword
            // 
            this.ItemForPassword.Control = this.PasswordTextEdit;
            this.ItemForPassword.Location = new System.Drawing.Point(0, 44);
            this.ItemForPassword.Name = "ItemForPassword";
            this.ItemForPassword.Size = new System.Drawing.Size(825, 44);
            this.ItemForPassword.Text = "Password";
            this.ItemForPassword.TextSize = new System.Drawing.Size(180, 25);
            // 
            // ItemForMin24HourChange
            // 
            this.ItemForMin24HourChange.Control = this.Min24HourChangeTextEdit;
            this.ItemForMin24HourChange.Location = new System.Drawing.Point(0, 88);
            this.ItemForMin24HourChange.Name = "ItemForMin24HourChange";
            this.ItemForMin24HourChange.Size = new System.Drawing.Size(825, 44);
            this.ItemForMin24HourChange.Text = "Min24Hour Change";
            this.ItemForMin24HourChange.TextSize = new System.Drawing.Size(180, 25);
            // 
            // ItemForMin7DaysChange
            // 
            this.ItemForMin7DaysChange.Control = this.Min7DaysChangeTextEdit;
            this.ItemForMin7DaysChange.Location = new System.Drawing.Point(0, 132);
            this.ItemForMin7DaysChange.Name = "ItemForMin7DaysChange";
            this.ItemForMin7DaysChange.Size = new System.Drawing.Size(825, 44);
            this.ItemForMin7DaysChange.Text = "Min7Days Change";
            this.ItemForMin7DaysChange.TextSize = new System.Drawing.Size(180, 25);
            // 
            // ItemForMin14DayChange
            // 
            this.ItemForMin14DayChange.Control = this.Min14DayChangeTextEdit;
            this.ItemForMin14DayChange.Location = new System.Drawing.Point(0, 176);
            this.ItemForMin14DayChange.Name = "ItemForMin14DayChange";
            this.ItemForMin14DayChange.Size = new System.Drawing.Size(825, 44);
            this.ItemForMin14DayChange.Text = "Min14Day Change";
            this.ItemForMin14DayChange.TextSize = new System.Drawing.Size(180, 25);
            // 
            // ItemForMin3MonthChange
            // 
            this.ItemForMin3MonthChange.Control = this.Min3MonthChangeTextEdit;
            this.ItemForMin3MonthChange.Location = new System.Drawing.Point(0, 220);
            this.ItemForMin3MonthChange.Name = "ItemForMin3MonthChange";
            this.ItemForMin3MonthChange.Size = new System.Drawing.Size(825, 44);
            this.ItemForMin3MonthChange.Text = "Min3Month Change";
            this.ItemForMin3MonthChange.TextSize = new System.Drawing.Size(180, 25);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.sbOk;
            this.layoutControlItem1.Location = new System.Drawing.Point(493, 582);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(169, 48);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 264);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(825, 318);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbCancel;
            this.layoutControlItem2.Location = new System.Drawing.Point(662, 582);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(163, 48);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 582);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(493, 48);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // invictusSettingsBindingSource
            // 
            this.invictusSettingsBindingSource.DataSource = typeof(InvictusExchangeApp.InvictusSettings);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 650);
            this.Controls.Add(this.dataLayoutControl1);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LoginTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasswordTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Min24HourChangeTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Min7DaysChangeTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Min14DayChangeTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Min3MonthChangeTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMin24HourChange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMin7DaysChange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMin14DayChange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMin3MonthChange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.invictusSettingsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataLayoutControl dataLayoutControl1;
        private LayoutControlGroup layoutControlGroup1;
        private System.Windows.Forms.BindingSource invictusSettingsBindingSource;
        private DevExpress.XtraEditors.TextEdit LoginTextEdit;
        private DevExpress.XtraEditors.TextEdit PasswordTextEdit;
        private DevExpress.XtraEditors.SpinEdit Min24HourChangeTextEdit;
        private DevExpress.XtraEditors.SpinEdit Min7DaysChangeTextEdit;
        private DevExpress.XtraEditors.SpinEdit Min14DayChangeTextEdit;
        private DevExpress.XtraEditors.SpinEdit Min3MonthChangeTextEdit;
        private LayoutControlGroup layoutControlGroup2;
        private LayoutControlItem ItemForLogin;
        private LayoutControlItem ItemForPassword;
        private LayoutControlItem ItemForMin24HourChange;
        private LayoutControlItem ItemForMin7DaysChange;
        private LayoutControlItem ItemForMin14DayChange;
        private LayoutControlItem ItemForMin3MonthChange;
        private DevExpress.XtraEditors.SimpleButton sbOk;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private LayoutControlItem layoutControlItem1;
        private EmptySpaceItem emptySpaceItem1;
        private LayoutControlItem layoutControlItem2;
        private EmptySpaceItem emptySpaceItem2;
    }
}