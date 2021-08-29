using DevExpress.XtraDataLayout;
using DevExpress.XtraLayout;

namespace Crypto.UI.Strategies.Arbitrages {
    partial class TriplePairStrategyConfigurationControl {
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
            this.BaseCurrencyTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.MonitoringCurrenciesTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.ExchangeImageComboBoxEdit = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForBaseCurrency = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForMonitoringCurrencies = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForExchange = new DevExpress.XtraLayout.LayoutControlItem();
            this.triplePairStrategyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BaseCurrencyTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MonitoringCurrenciesTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExchangeImageComboBoxEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForBaseCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMonitoringCurrencies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForExchange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.triplePairStrategyBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.BaseCurrencyTextEdit);
            this.dataLayoutControl1.Controls.Add(this.MonitoringCurrenciesTextEdit);
            this.dataLayoutControl1.Controls.Add(this.ExchangeImageComboBoxEdit);
            this.dataLayoutControl1.DataSource = this.triplePairStrategyBindingSource;
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.dataLayoutControl1.Size = new System.Drawing.Size(1177, 832);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // BaseCurrencyTextEdit
            // 
            this.BaseCurrencyTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.triplePairStrategyBindingSource, "BaseCurrency", true));
            this.BaseCurrencyTextEdit.Location = new System.Drawing.Point(223, 12);
            this.BaseCurrencyTextEdit.Name = "BaseCurrencyTextEdit";
            this.BaseCurrencyTextEdit.Size = new System.Drawing.Size(942, 40);
            this.BaseCurrencyTextEdit.StyleController = this.dataLayoutControl1;
            this.BaseCurrencyTextEdit.TabIndex = 4;
            // 
            // MonitoringCurrenciesTextEdit
            // 
            this.MonitoringCurrenciesTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.triplePairStrategyBindingSource, "MonitoringCurrencies", true));
            this.MonitoringCurrenciesTextEdit.Location = new System.Drawing.Point(223, 56);
            this.MonitoringCurrenciesTextEdit.Name = "MonitoringCurrenciesTextEdit";
            this.MonitoringCurrenciesTextEdit.Size = new System.Drawing.Size(942, 40);
            this.MonitoringCurrenciesTextEdit.StyleController = this.dataLayoutControl1;
            this.MonitoringCurrenciesTextEdit.TabIndex = 5;
            this.MonitoringCurrenciesTextEdit.ToolTip = "Please write CryptoCoins abbreviations separated by comma";
            // 
            // ExchangeImageComboBoxEdit
            // 
            this.ExchangeImageComboBoxEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.triplePairStrategyBindingSource, "Exchange", true));
            this.ExchangeImageComboBoxEdit.Location = new System.Drawing.Point(223, 100);
            this.ExchangeImageComboBoxEdit.Name = "ExchangeImageComboBoxEdit";
            this.ExchangeImageComboBoxEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.ExchangeImageComboBoxEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ExchangeImageComboBoxEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ExchangeImageComboBoxEdit.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Poloniex", Crypto.Core.ExchangeType.Poloniex, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Bittrex", Crypto.Core.ExchangeType.Bittrex, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("BitFinex", Crypto.Core.ExchangeType.BitFinex, 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Binance", Crypto.Core.ExchangeType.Binance, 3),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Yobit", Crypto.Core.ExchangeType.Yobit, 4),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Bitmex", Crypto.Core.ExchangeType.Bitmex, 5)});
            this.ExchangeImageComboBoxEdit.Properties.UseCtrlScroll = true;
            this.ExchangeImageComboBoxEdit.Size = new System.Drawing.Size(942, 40);
            this.ExchangeImageComboBoxEdit.StyleController = this.dataLayoutControl1;
            this.ExchangeImageComboBoxEdit.TabIndex = 6;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1177, 832);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AllowDrawBackground = false;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForBaseCurrency,
            this.ItemForMonitoringCurrencies,
            this.ItemForExchange});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "autoGeneratedGroup0";
            this.layoutControlGroup2.Size = new System.Drawing.Size(1157, 812);
            // 
            // ItemForBaseCurrency
            // 
            this.ItemForBaseCurrency.Control = this.BaseCurrencyTextEdit;
            this.ItemForBaseCurrency.Location = new System.Drawing.Point(0, 0);
            this.ItemForBaseCurrency.Name = "ItemForBaseCurrency";
            this.ItemForBaseCurrency.Size = new System.Drawing.Size(1157, 44);
            this.ItemForBaseCurrency.Text = "Base Currency";
            this.ItemForBaseCurrency.TextSize = new System.Drawing.Size(205, 25);
            // 
            // ItemForMonitoringCurrencies
            // 
            this.ItemForMonitoringCurrencies.Control = this.MonitoringCurrenciesTextEdit;
            this.ItemForMonitoringCurrencies.Location = new System.Drawing.Point(0, 44);
            this.ItemForMonitoringCurrencies.Name = "ItemForMonitoringCurrencies";
            this.ItemForMonitoringCurrencies.Size = new System.Drawing.Size(1157, 44);
            this.ItemForMonitoringCurrencies.Text = "Monitoring Currencies";
            this.ItemForMonitoringCurrencies.TextSize = new System.Drawing.Size(205, 25);
            // 
            // ItemForExchange
            // 
            this.ItemForExchange.Control = this.ExchangeImageComboBoxEdit;
            this.ItemForExchange.Location = new System.Drawing.Point(0, 88);
            this.ItemForExchange.Name = "ItemForExchange";
            this.ItemForExchange.Size = new System.Drawing.Size(1157, 724);
            this.ItemForExchange.Text = "Exchange";
            this.ItemForExchange.TextSize = new System.Drawing.Size(205, 25);
            // 
            // triplePairStrategyBindingSource
            // 
            this.triplePairStrategyBindingSource.DataSource = typeof(Crypto.Core.Strategies.Arbitrages.AltBtcUsdt.TriplePairStrategy);
            // 
            // TriplePairStrategyConfigurationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataLayoutControl1);
            this.Name = "TriplePairStrategyConfigurationControl";
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BaseCurrencyTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MonitoringCurrenciesTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExchangeImageComboBoxEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForBaseCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMonitoringCurrencies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForExchange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.triplePairStrategyBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataLayoutControl dataLayoutControl1;
        private LayoutControlGroup layoutControlGroup1;
        private System.Windows.Forms.BindingSource triplePairStrategyBindingSource;
        private DevExpress.XtraEditors.TextEdit BaseCurrencyTextEdit;
        private DevExpress.XtraEditors.TextEdit MonitoringCurrenciesTextEdit;
        private DevExpress.XtraEditors.ImageComboBoxEdit ExchangeImageComboBoxEdit;
        private LayoutControlGroup layoutControlGroup2;
        private LayoutControlItem ItemForBaseCurrency;
        private LayoutControlItem ItemForMonitoringCurrencies;
        private LayoutControlItem ItemForExchange;
    }
}
