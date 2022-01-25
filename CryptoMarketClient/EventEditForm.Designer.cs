
namespace CryptoMarketClient {
    partial class EventEditForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
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
            this.TimeDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.tickerEventBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.TextTextEdit = new DevExpress.XtraEditors.MemoEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForTime = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForColor = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForText = new DevExpress.XtraLayout.LayoutControlItem();
            this.ColorColorEdit = new DevExpress.XtraEditors.ColorPickEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tickerEventBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorColorEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Appearance.Control.FontSizeDelta = 2;
            this.dataLayoutControl1.Appearance.Control.Options.UseFont = true;
            this.dataLayoutControl1.Controls.Add(this.TimeDateEdit);
            this.dataLayoutControl1.Controls.Add(this.simpleButton1);
            this.dataLayoutControl1.Controls.Add(this.simpleButton2);
            this.dataLayoutControl1.Controls.Add(this.TextTextEdit);
            this.dataLayoutControl1.Controls.Add(this.ColorColorEdit);
            this.dataLayoutControl1.DataSource = this.tickerEventBindingSource;
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.dataLayoutControl1.Size = new System.Drawing.Size(682, 442);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // TimeDateEdit
            // 
            this.TimeDateEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.tickerEventBindingSource, "Time", true));
            this.TimeDateEdit.EditValue = null;
            this.TimeDateEdit.Location = new System.Drawing.Point(83, 14);
            this.TimeDateEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TimeDateEdit.Name = "TimeDateEdit";
            this.TimeDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TimeDateEdit.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.TimeDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TimeDateEdit.Properties.DisplayFormat.FormatString = "g";
            this.TimeDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.TimeDateEdit.Properties.EditFormat.FormatString = "g";
            this.TimeDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.TimeDateEdit.Properties.MaskSettings.Set("mask", "g");
            this.TimeDateEdit.Size = new System.Drawing.Size(587, 46);
            this.TimeDateEdit.StyleController = this.dataLayoutControl1;
            this.TimeDateEdit.TabIndex = 5;
            // 
            // tickerEventBindingSource
            // 
            this.tickerEventBindingSource.DataSource = typeof(Crypto.Core.Common.TickerEvent);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(393, 386);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(144, 44);
            this.simpleButton1.StyleController = this.dataLayoutControl1;
            this.simpleButton1.TabIndex = 7;
            this.simpleButton1.Text = "OK";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(541, 386);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(129, 44);
            this.simpleButton2.StyleController = this.dataLayoutControl1;
            this.simpleButton2.TabIndex = 8;
            this.simpleButton2.Text = "Cancel";
            // 
            // TextTextEdit
            // 
            this.TextTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.tickerEventBindingSource, "Text", true));
            this.TextTextEdit.Location = new System.Drawing.Point(83, 122);
            this.TextTextEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TextTextEdit.Name = "TextTextEdit";
            this.TextTextEdit.Size = new System.Drawing.Size(587, 258);
            this.TextTextEdit.StyleController = this.dataLayoutControl1;
            this.TextTextEdit.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(682, 442);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AllowDrawBackground = false;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForTime,
            this.ItemForColor,
            this.layoutControlItem1,
            this.emptySpaceItem2,
            this.layoutControlItem2,
            this.ItemForText});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "autoGeneratedGroup0";
            this.layoutControlGroup1.Size = new System.Drawing.Size(662, 422);
            // 
            // ItemForTime
            // 
            this.ItemForTime.AppearanceItemCaption.FontSizeDelta = 2;
            this.ItemForTime.AppearanceItemCaption.Options.UseFont = true;
            this.ItemForTime.Control = this.TimeDateEdit;
            this.ItemForTime.Location = new System.Drawing.Point(0, 0);
            this.ItemForTime.Name = "ItemForTime";
            this.ItemForTime.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 4, 4);
            this.ItemForTime.Size = new System.Drawing.Size(662, 54);
            this.ItemForTime.Text = "Time";
            this.ItemForTime.TextSize = new System.Drawing.Size(59, 31);
            // 
            // ItemForColor
            // 
            this.ItemForColor.AppearanceItemCaption.FontSizeDelta = 2;
            this.ItemForColor.AppearanceItemCaption.Options.UseFont = true;
            this.ItemForColor.Control = this.ColorColorEdit;
            this.ItemForColor.Location = new System.Drawing.Point(0, 54);
            this.ItemForColor.Name = "ItemForColor";
            this.ItemForColor.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 4, 4);
            this.ItemForColor.Size = new System.Drawing.Size(662, 54);
            this.ItemForColor.Text = "Color";
            this.ItemForColor.TextSize = new System.Drawing.Size(59, 31);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.simpleButton1;
            this.layoutControlItem1.Location = new System.Drawing.Point(381, 374);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(148, 48);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 374);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(381, 48);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButton2;
            this.layoutControlItem2.Location = new System.Drawing.Point(529, 374);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(133, 48);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // ItemForText
            // 
            this.ItemForText.AppearanceItemCaption.FontSizeDelta = 2;
            this.ItemForText.AppearanceItemCaption.Options.UseFont = true;
            this.ItemForText.Control = this.TextTextEdit;
            this.ItemForText.Location = new System.Drawing.Point(0, 108);
            this.ItemForText.Name = "ItemForText";
            this.ItemForText.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 4, 4);
            this.ItemForText.Size = new System.Drawing.Size(662, 266);
            this.ItemForText.Text = "Text";
            this.ItemForText.TextSize = new System.Drawing.Size(59, 31);
            // 
            // ColorColorEdit
            // 
            this.ColorColorEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.tickerEventBindingSource, "Color", true));
            this.ColorColorEdit.EditValue = System.Drawing.Color.Empty;
            this.ColorColorEdit.Location = new System.Drawing.Point(83, 68);
            this.ColorColorEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ColorColorEdit.Name = "ColorColorEdit";
            this.ColorColorEdit.Properties.AutomaticColor = System.Drawing.Color.Black;
            this.ColorColorEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ColorColorEdit.Size = new System.Drawing.Size(587, 46);
            this.ColorColorEdit.StyleController = this.dataLayoutControl1;
            this.ColorColorEdit.TabIndex = 6;
            // 
            // EventEditForm
            // 
            this.Appearance.FontSizeDelta = 2;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(29F, 64F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 442);
            this.Controls.Add(this.dataLayoutControl1);
            this.Font = new System.Drawing.Font("Tahoma", 19.875F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "EventEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Event";
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TimeDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tickerEventBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorColorEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private System.Windows.Forms.BindingSource tickerEventBindingSource;
        private DevExpress.XtraEditors.DateEdit TimeDateEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem ItemForText;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTime;
        private DevExpress.XtraLayout.LayoutControlItem ItemForColor;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.MemoEdit TextTextEdit;
        private DevExpress.XtraEditors.ColorPickEdit ColorColorEdit;
    }
}