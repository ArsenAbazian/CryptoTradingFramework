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
            this.Min7DaysChangeTextEdit = new DevExpress.XtraEditors.SpinEdit();
            this.Min14DayChangeTextEdit = new DevExpress.XtraEditors.SpinEdit();
            this.Min3MonthChangeTextEdit = new DevExpress.XtraEditors.SpinEdit();
            this.sbOk = new DevExpress.XtraEditors.SimpleButton();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.spinEdit2 = new DevExpress.XtraEditors.SpinEdit();
            this.spinEdit3 = new DevExpress.XtraEditors.SpinEdit();
            this.spinEdit4 = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.tabbedControlGroup1 = new DevExpress.XtraLayout.TabbedControlGroup();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForMin3MonthChange = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForMin14DayChange = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForMin7DaysChange = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForPassword = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForLogin = new DevExpress.XtraLayout.LayoutControlItem();
            this.invictusSettingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LoginTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasswordTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Min7DaysChangeTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Min14DayChangeTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Min3MonthChangeTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMin3MonthChange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMin14DayChange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMin7DaysChange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.invictusSettingsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.LoginTextEdit);
            this.dataLayoutControl1.Controls.Add(this.PasswordTextEdit);
            this.dataLayoutControl1.Controls.Add(this.Min7DaysChangeTextEdit);
            this.dataLayoutControl1.Controls.Add(this.Min14DayChangeTextEdit);
            this.dataLayoutControl1.Controls.Add(this.Min3MonthChangeTextEdit);
            this.dataLayoutControl1.Controls.Add(this.sbOk);
            this.dataLayoutControl1.Controls.Add(this.sbCancel);
            this.dataLayoutControl1.Controls.Add(this.spinEdit1);
            this.dataLayoutControl1.Controls.Add(this.spinEdit2);
            this.dataLayoutControl1.Controls.Add(this.spinEdit3);
            this.dataLayoutControl1.Controls.Add(this.spinEdit4);
            this.dataLayoutControl1.DataSource = this.invictusSettingsBindingSource;
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Margin = new System.Windows.Forms.Padding(2);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.dataLayoutControl1.Size = new System.Drawing.Size(422, 338);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // LoginTextEdit
            // 
            this.LoginTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.invictusSettingsBindingSource, "Login", true));
            this.LoginTextEdit.Location = new System.Drawing.Point(128, 46);
            this.LoginTextEdit.Margin = new System.Windows.Forms.Padding(2);
            this.LoginTextEdit.Name = "LoginTextEdit";
            this.LoginTextEdit.Size = new System.Drawing.Size(270, 20);
            this.LoginTextEdit.StyleController = this.dataLayoutControl1;
            this.LoginTextEdit.TabIndex = 4;
            // 
            // PasswordTextEdit
            // 
            this.PasswordTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.invictusSettingsBindingSource, "Password", true));
            this.PasswordTextEdit.Location = new System.Drawing.Point(128, 70);
            this.PasswordTextEdit.Margin = new System.Windows.Forms.Padding(2);
            this.PasswordTextEdit.Name = "PasswordTextEdit";
            this.PasswordTextEdit.Properties.PasswordChar = '*';
            this.PasswordTextEdit.Size = new System.Drawing.Size(270, 20);
            this.PasswordTextEdit.StyleController = this.dataLayoutControl1;
            this.PasswordTextEdit.TabIndex = 5;
            // 
            // Min7DaysChangeTextEdit
            // 
            this.Min7DaysChangeTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.invictusSettingsBindingSource, "Min7DaysChange", true));
            this.Min7DaysChangeTextEdit.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Min7DaysChangeTextEdit.Location = new System.Drawing.Point(128, 94);
            this.Min7DaysChangeTextEdit.Margin = new System.Windows.Forms.Padding(2);
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
            1000,
            0,
            0,
            -2147483648});
            this.Min7DaysChangeTextEdit.Size = new System.Drawing.Size(270, 20);
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
            this.Min14DayChangeTextEdit.Location = new System.Drawing.Point(128, 118);
            this.Min14DayChangeTextEdit.Margin = new System.Windows.Forms.Padding(2);
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
            1000,
            0,
            0,
            -2147483648});
            this.Min14DayChangeTextEdit.Size = new System.Drawing.Size(270, 20);
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
            this.Min3MonthChangeTextEdit.Location = new System.Drawing.Point(128, 142);
            this.Min3MonthChangeTextEdit.Margin = new System.Windows.Forms.Padding(2);
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
            1000,
            0,
            0,
            -2147483648});
            this.Min3MonthChangeTextEdit.Size = new System.Drawing.Size(270, 20);
            this.Min3MonthChangeTextEdit.StyleController = this.dataLayoutControl1;
            this.Min3MonthChangeTextEdit.TabIndex = 9;
            // 
            // sbOk
            // 
            this.sbOk.AutoWidthInLayoutControl = true;
            this.sbOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.sbOk.Location = new System.Drawing.Point(242, 304);
            this.sbOk.Margin = new System.Windows.Forms.Padding(2);
            this.sbOk.Name = "sbOk";
            this.sbOk.Padding = new System.Windows.Forms.Padding(30, 0, 30, 0);
            this.sbOk.Size = new System.Drawing.Size(83, 22);
            this.sbOk.StyleController = this.dataLayoutControl1;
            this.sbOk.TabIndex = 10;
            this.sbOk.Text = "OK";
            // 
            // sbCancel
            // 
            this.sbCancel.AutoWidthInLayoutControl = true;
            this.sbCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbCancel.Location = new System.Drawing.Point(329, 304);
            this.sbCancel.Margin = new System.Windows.Forms.Padding(2);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.sbCancel.Size = new System.Drawing.Size(81, 22);
            this.sbCancel.StyleController = this.dataLayoutControl1;
            this.sbCancel.TabIndex = 11;
            this.sbCancel.Text = "Cancel";
            // 
            // spinEdit1
            // 
            this.spinEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.invictusSettingsBindingSource, "MinCp24HourChange", true));
            this.spinEdit1.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEdit1.Location = new System.Drawing.Point(128, 46);
            this.spinEdit1.Margin = new System.Windows.Forms.Padding(2);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEdit1.Properties.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spinEdit1.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spinEdit1.Properties.MinValue = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.spinEdit1.Size = new System.Drawing.Size(270, 20);
            this.spinEdit1.StyleController = this.dataLayoutControl1;
            this.spinEdit1.TabIndex = 12;
            // 
            // spinEdit2
            // 
            this.spinEdit2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.invictusSettingsBindingSource, "MinCp7DayChange", true));
            this.spinEdit2.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEdit2.Location = new System.Drawing.Point(128, 70);
            this.spinEdit2.Margin = new System.Windows.Forms.Padding(2);
            this.spinEdit2.Name = "spinEdit2";
            this.spinEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEdit2.Properties.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spinEdit2.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spinEdit2.Properties.MinValue = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.spinEdit2.Size = new System.Drawing.Size(270, 20);
            this.spinEdit2.StyleController = this.dataLayoutControl1;
            this.spinEdit2.TabIndex = 13;
            // 
            // spinEdit3
            // 
            this.spinEdit3.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.invictusSettingsBindingSource, "MinCp4WeekChange", true));
            this.spinEdit3.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEdit3.Location = new System.Drawing.Point(128, 94);
            this.spinEdit3.Margin = new System.Windows.Forms.Padding(2);
            this.spinEdit3.Name = "spinEdit3";
            this.spinEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEdit3.Properties.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spinEdit3.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spinEdit3.Properties.MinValue = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.spinEdit3.Size = new System.Drawing.Size(270, 20);
            this.spinEdit3.StyleController = this.dataLayoutControl1;
            this.spinEdit3.TabIndex = 14;
            // 
            // spinEdit4
            // 
            this.spinEdit4.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.invictusSettingsBindingSource, "MinCp3MonthChange", true));
            this.spinEdit4.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEdit4.Location = new System.Drawing.Point(128, 118);
            this.spinEdit4.Name = "spinEdit4";
            this.spinEdit4.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEdit4.Properties.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spinEdit4.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spinEdit4.Properties.MinValue = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.spinEdit4.Size = new System.Drawing.Size(270, 20);
            this.spinEdit4.StyleController = this.dataLayoutControl1;
            this.spinEdit4.TabIndex = 15;
            this.spinEdit4.EditValueChanged += new System.EventHandler(this.spinEdit4_EditValueChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup1.Size = new System.Drawing.Size(422, 338);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AllowDrawBackground = false;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.emptySpaceItem2,
            this.tabbedControlGroup1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "autoGeneratedGroup0";
            this.layoutControlGroup2.Size = new System.Drawing.Size(402, 318);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.sbOk;
            this.layoutControlItem1.Location = new System.Drawing.Point(230, 292);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(87, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 166);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(402, 126);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbCancel;
            this.layoutControlItem2.Location = new System.Drawing.Point(317, 292);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(85, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 292);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(230, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // tabbedControlGroup1
            // 
            this.tabbedControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.tabbedControlGroup1.Name = "tabbedControlGroup1";
            this.tabbedControlGroup1.SelectedTabPage = this.layoutControlGroup4;
            this.tabbedControlGroup1.Size = new System.Drawing.Size(402, 166);
            this.tabbedControlGroup1.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.layoutControlGroup4});
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(378, 120);
            this.layoutControlGroup4.Text = "CryptoPredictor";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.spinEdit1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(378, 24);
            this.layoutControlItem3.Text = "Min 1 Day Change";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(98, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.spinEdit2;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(378, 24);
            this.layoutControlItem4.Text = "Min 7 Days Change";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(98, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.spinEdit3;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(378, 24);
            this.layoutControlItem5.Text = "Min 4 Week Change";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(98, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.spinEdit4;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(378, 48);
            this.layoutControlItem6.Text = "Min 3 Month Change";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(98, 13);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForMin3MonthChange,
            this.ItemForMin14DayChange,
            this.ItemForMin7DaysChange,
            this.ItemForPassword,
            this.ItemForLogin});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(378, 120);
            this.layoutControlGroup3.Text = "WalletInvestor";
            // 
            // ItemForMin3MonthChange
            // 
            this.ItemForMin3MonthChange.Control = this.Min3MonthChangeTextEdit;
            this.ItemForMin3MonthChange.Location = new System.Drawing.Point(0, 96);
            this.ItemForMin3MonthChange.Name = "ItemForMin3MonthChange";
            this.ItemForMin3MonthChange.Size = new System.Drawing.Size(378, 24);
            this.ItemForMin3MonthChange.Text = "Min3Month Change";
            this.ItemForMin3MonthChange.TextSize = new System.Drawing.Size(98, 13);
            // 
            // ItemForMin14DayChange
            // 
            this.ItemForMin14DayChange.Control = this.Min14DayChangeTextEdit;
            this.ItemForMin14DayChange.Location = new System.Drawing.Point(0, 72);
            this.ItemForMin14DayChange.Name = "ItemForMin14DayChange";
            this.ItemForMin14DayChange.Size = new System.Drawing.Size(378, 24);
            this.ItemForMin14DayChange.Text = "Min14Day Change";
            this.ItemForMin14DayChange.TextSize = new System.Drawing.Size(98, 13);
            // 
            // ItemForMin7DaysChange
            // 
            this.ItemForMin7DaysChange.Control = this.Min7DaysChangeTextEdit;
            this.ItemForMin7DaysChange.Location = new System.Drawing.Point(0, 48);
            this.ItemForMin7DaysChange.Name = "ItemForMin7DaysChange";
            this.ItemForMin7DaysChange.Size = new System.Drawing.Size(378, 24);
            this.ItemForMin7DaysChange.Text = "Min7Days Change";
            this.ItemForMin7DaysChange.TextSize = new System.Drawing.Size(98, 13);
            // 
            // ItemForPassword
            // 
            this.ItemForPassword.Control = this.PasswordTextEdit;
            this.ItemForPassword.Location = new System.Drawing.Point(0, 24);
            this.ItemForPassword.Name = "ItemForPassword";
            this.ItemForPassword.Size = new System.Drawing.Size(378, 24);
            this.ItemForPassword.Text = "Password";
            this.ItemForPassword.TextSize = new System.Drawing.Size(98, 13);
            // 
            // ItemForLogin
            // 
            this.ItemForLogin.Control = this.LoginTextEdit;
            this.ItemForLogin.Location = new System.Drawing.Point(0, 0);
            this.ItemForLogin.Name = "ItemForLogin";
            this.ItemForLogin.Size = new System.Drawing.Size(378, 24);
            this.ItemForLogin.Text = "Login";
            this.ItemForLogin.TextSize = new System.Drawing.Size(98, 13);
            // 
            // invictusSettingsBindingSource
            // 
            this.invictusSettingsBindingSource.DataSource = typeof(InvictusExchangeApp.InvictusSettings);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 338);
            this.Controls.Add(this.dataLayoutControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LoginTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasswordTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Min7DaysChangeTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Min14DayChangeTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Min3MonthChangeTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMin3MonthChange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMin14DayChange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMin7DaysChange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.invictusSettingsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataLayoutControl dataLayoutControl1;
        private LayoutControlGroup layoutControlGroup1;
        private System.Windows.Forms.BindingSource invictusSettingsBindingSource;
        private DevExpress.XtraEditors.TextEdit LoginTextEdit;
        private DevExpress.XtraEditors.TextEdit PasswordTextEdit;
        private DevExpress.XtraEditors.SpinEdit Min7DaysChangeTextEdit;
        private DevExpress.XtraEditors.SpinEdit Min14DayChangeTextEdit;
        private DevExpress.XtraEditors.SpinEdit Min3MonthChangeTextEdit;
        private LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraEditors.SimpleButton sbOk;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private LayoutControlItem layoutControlItem1;
        private EmptySpaceItem emptySpaceItem1;
        private LayoutControlItem layoutControlItem2;
        private EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.SpinEdit spinEdit1;
        private DevExpress.XtraEditors.SpinEdit spinEdit2;
        private DevExpress.XtraEditors.SpinEdit spinEdit3;
        private TabbedControlGroup tabbedControlGroup1;
        private LayoutControlGroup layoutControlGroup4;
        private LayoutControlItem layoutControlItem3;
        private LayoutControlItem layoutControlItem4;
        private LayoutControlItem layoutControlItem5;
        private LayoutControlGroup layoutControlGroup3;
        private LayoutControlItem ItemForMin3MonthChange;
        private LayoutControlItem ItemForMin14DayChange;
        private LayoutControlItem ItemForMin7DaysChange;
        private LayoutControlItem ItemForPassword;
        private LayoutControlItem ItemForLogin;
        private DevExpress.XtraEditors.SpinEdit spinEdit4;
        private LayoutControlItem layoutControlItem6;
    }
}