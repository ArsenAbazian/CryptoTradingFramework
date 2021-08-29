namespace CryptoMarketClient {
    partial class CurrencyMonitoringForm {
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule3 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue3 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule4 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue4 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.Sparkline.BarSparklineView barSparklineView3 = new DevExpress.Sparkline.BarSparklineView();
            DevExpress.Sparkline.BarSparklineView barSparklineView4 = new DevExpress.Sparkline.BarSparklineView();
            DevExpress.Sparkline.AreaSparklineView areaSparklineView3 = new DevExpress.Sparkline.AreaSparklineView();
            DevExpress.Sparkline.AreaSparklineView areaSparklineView4 = new DevExpress.Sparkline.AreaSparklineView();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbSendNotifications = new DevExpress.XtraBars.BarCheckItem();
            this.ribbonPage5 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup10 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bittrexTickerBindingSource = new System.Windows.Forms.BindingSource();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIsSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBaseCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMarketCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsFrozen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsActual = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMarketCurrencyEnabled = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.repositoryItemCheckEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemSparklineEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit();
            this.repositoryItemSparklineEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit();
            this.repositoryItemSparklineEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit();
            this.repositoryItemSparklineEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit();
            this.bbUpdateBotStatus = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bittrexTickerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit4)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.AllowGlyphSkinning = true;
            this.ribbonControl1.AutoSizeItems = true;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.bbSendNotifications,
            this.bbUpdateBotStatus});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Margin = new System.Windows.Forms.Padding(6);
            this.ribbonControl1.MaxItemId = 27;
            this.ribbonControl1.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.PageCategoryAlignment = DevExpress.XtraBars.Ribbon.RibbonPageCategoryAlignment.Right;
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage5});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.OfficeUniversal;
            this.ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.Size = new System.Drawing.Size(1529, 103);
            // 
            // bbSendNotifications
            // 
            this.bbSendNotifications.Caption = "Send Notifications";
            this.bbSendNotifications.Id = 25;
            this.bbSendNotifications.Name = "bbSendNotifications";
            this.bbSendNotifications.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bbSendNotifications_CheckedChanged);
            // 
            // ribbonPage5
            // 
            this.ribbonPage5.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup10});
            this.ribbonPage5.Name = "ribbonPage5";
            this.ribbonPage5.Text = "Currency Monitoring";
            // 
            // ribbonPageGroup10
            // 
            this.ribbonPageGroup10.ItemLinks.Add(this.bbSendNotifications);
            this.ribbonPageGroup10.ItemLinks.Add(this.bbUpdateBotStatus);
            this.ribbonPageGroup10.Name = "ribbonPageGroup10";
            this.ribbonPageGroup10.Text = "ribbonPageGroup10";
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bittrexTickerBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.gridControl1.Location = new System.Drawing.Point(0, 103);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(6);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2,
            this.repositoryItemCheckEdit3,
            this.repositoryItemProgressBar1,
            this.repositoryItemCheckEdit4,
            this.repositoryItemSparklineEdit1,
            this.repositoryItemSparklineEdit2,
            this.repositoryItemSparklineEdit3,
            this.repositoryItemSparklineEdit4});
            this.gridControl1.Size = new System.Drawing.Size(1529, 849);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // bittrexTickerBindingSource
            // 
            this.bittrexTickerBindingSource.DataSource = typeof(Crypto.Core.Ticker);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIsSelected,
            this.colBaseCurrency,
            this.colMarketCurrency,
            this.colIsFrozen,
            this.colIsActual,
            this.colMarketCurrencyEnabled});
            gridFormatRule3.Name = "FormatNotActual";
            formatConditionRuleValue3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            formatConditionRuleValue3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            formatConditionRuleValue3.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue3.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue3.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue3.Value1 = false;
            gridFormatRule3.Rule = formatConditionRuleValue3;
            gridFormatRule4.ApplyToRow = true;
            gridFormatRule4.Column = this.colMarketCurrencyEnabled;
            gridFormatRule4.Name = "MarketCurrencyDisabledRule";
            formatConditionRuleValue4.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            formatConditionRuleValue4.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue4.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue4.Value1 = false;
            gridFormatRule4.Rule = formatConditionRuleValue4;
            this.gridView1.FormatRules.Add(gridFormatRule3);
            this.gridView1.FormatRules.Add(gridFormatRule4);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            // 
            // colIsSelected
            // 
            this.colIsSelected.FieldName = "IsSelected";
            this.colIsSelected.Name = "colIsSelected";
            this.colIsSelected.Visible = true;
            this.colIsSelected.VisibleIndex = 0;
            // 
            // colBaseCurrency
            // 
            this.colBaseCurrency.FieldName = "BaseCurrency";
            this.colBaseCurrency.Name = "colBaseCurrency";
            this.colBaseCurrency.Visible = true;
            this.colBaseCurrency.VisibleIndex = 1;
            // 
            // colMarketCurrency
            // 
            this.colMarketCurrency.FieldName = "MarketCurrency";
            this.colMarketCurrency.Name = "colMarketCurrency";
            this.colMarketCurrency.Visible = true;
            this.colMarketCurrency.VisibleIndex = 2;
            // 
            // colIsFrozen
            // 
            this.colIsFrozen.FieldName = "IsFrozen";
            this.colIsFrozen.Name = "colIsFrozen";
            this.colIsFrozen.Visible = true;
            this.colIsFrozen.VisibleIndex = 3;
            // 
            // colIsActual
            // 
            this.colIsActual.FieldName = "IsActual";
            this.colIsActual.Name = "colIsActual";
            this.colIsActual.Visible = true;
            this.colIsActual.VisibleIndex = 4;
            // 
            // colMarketCurrencyEnabled
            // 
            this.colMarketCurrencyEnabled.FieldName = "MarketCurrencyEnabled";
            this.colMarketCurrencyEnabled.Name = "colMarketCurrencyEnabled";
            this.colMarketCurrencyEnabled.Visible = true;
            this.colMarketCurrencyEnabled.VisibleIndex = 5;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // repositoryItemCheckEdit3
            // 
            this.repositoryItemCheckEdit3.AutoHeight = false;
            this.repositoryItemCheckEdit3.Name = "repositoryItemCheckEdit3";
            // 
            // repositoryItemProgressBar1
            // 
            this.repositoryItemProgressBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.repositoryItemProgressBar1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.repositoryItemProgressBar1.Appearance.ForeColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.repositoryItemProgressBar1.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.repositoryItemProgressBar1.AppearanceReadOnly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.repositoryItemProgressBar1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.repositoryItemProgressBar1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.repositoryItemProgressBar1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
            this.repositoryItemProgressBar1.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.repositoryItemProgressBar1.ShowTitle = true;
            this.repositoryItemProgressBar1.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            // 
            // repositoryItemCheckEdit4
            // 
            this.repositoryItemCheckEdit4.AutoHeight = false;
            this.repositoryItemCheckEdit4.Name = "repositoryItemCheckEdit4";
            // 
            // repositoryItemSparklineEdit1
            // 
            this.repositoryItemSparklineEdit1.Name = "repositoryItemSparklineEdit1";
            this.repositoryItemSparklineEdit1.ValueRange.IsAuto = false;
            barSparklineView3.Color = System.Drawing.Color.Green;
            this.repositoryItemSparklineEdit1.View = barSparklineView3;
            // 
            // repositoryItemSparklineEdit2
            // 
            this.repositoryItemSparklineEdit2.Name = "repositoryItemSparklineEdit2";
            barSparklineView4.Color = System.Drawing.Color.Red;
            this.repositoryItemSparklineEdit2.View = barSparklineView4;
            // 
            // repositoryItemSparklineEdit3
            // 
            this.repositoryItemSparklineEdit3.Name = "repositoryItemSparklineEdit3";
            areaSparklineView3.Color = System.Drawing.Color.Green;
            this.repositoryItemSparklineEdit3.View = areaSparklineView3;
            // 
            // repositoryItemSparklineEdit4
            // 
            this.repositoryItemSparklineEdit4.Name = "repositoryItemSparklineEdit4";
            areaSparklineView4.Color = System.Drawing.Color.Red;
            this.repositoryItemSparklineEdit4.View = areaSparklineView4;
            // 
            // bbUpdateBotStatus
            // 
            this.bbUpdateBotStatus.Caption = "Update Bot Status";
            this.bbUpdateBotStatus.Id = 26;
            this.bbUpdateBotStatus.Name = "bbUpdateBotStatus";
            this.bbUpdateBotStatus.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbUpdateBotStatus_ItemClick);
            // 
            // CurrencyMonitoringForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1529, 952);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "CurrencyMonitoringForm";
            this.Text = "CurrencyMonitoringForm";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bittrexTickerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSparklineEdit4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarCheckItem bbSendNotifications;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage5;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup10;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit repositoryItemSparklineEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit repositoryItemSparklineEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit repositoryItemSparklineEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit repositoryItemSparklineEdit4;
        private System.Windows.Forms.BindingSource bittrexTickerBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colIsSelected;
        private DevExpress.XtraGrid.Columns.GridColumn colBaseCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colMarketCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colIsFrozen;
        private DevExpress.XtraGrid.Columns.GridColumn colIsActual;
        private DevExpress.XtraGrid.Columns.GridColumn colMarketCurrencyEnabled;
        private DevExpress.XtraBars.BarButtonItem bbUpdateBotStatus;
    }
}