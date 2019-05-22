namespace Crypto.UI.Strategies.Custom {
    partial class WalletInvestorDataForm {
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
            DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemCheckEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.walletInvestorItemsControl1 = new CryptoMarketClient.Forms.Instruments.WalletInvestorItemsControl();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tpData.SuspendLayout();
            this.tpChartPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit6)).BeginInit();
            this.xtraTabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpChart
            // 
            this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1});
            this.tabControl.Controls.SetChildIndex(this.xtraTabPage1, 0);
            this.tabControl.Controls.SetChildIndex(this.tpChartPage, 0);
            this.tabControl.Controls.SetChildIndex(this.tpTradeHistory, 0);
            this.tabControl.Controls.SetChildIndex(this.tpEvents, 0);
            this.tabControl.Controls.SetChildIndex(this.tpData, 0);
            // 
            // gcData
            // 
            repositoryItemTextEdit1.AutoHeight = false;
            repositoryItemTextEdit1.Name = "repositoryItemTextEdit3";
            repositoryItemTextEdit1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            repositoryItemCheckEdit1.AutoHeight = false;
            repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit5";
            repositoryItemCheckEdit2.AutoHeight = false;
            repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit6";
            this.gcData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            repositoryItemTextEdit1,
            repositoryItemCheckEdit1,
            repositoryItemCheckEdit2,
            this.repositoryItemTextEdit3,
            this.repositoryItemCheckEdit5,
            this.repositoryItemCheckEdit6});
            // 
            // chartControl
            // 
            this.chartControl.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Left;
            this.chartControl.Legend.Name = "Default Legend";
            this.chartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            // 
            // repositoryItemTextEdit3
            // 
            this.repositoryItemTextEdit3.AutoHeight = false;
            this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
            this.repositoryItemTextEdit3.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            // 
            // repositoryItemCheckEdit5
            // 
            this.repositoryItemCheckEdit5.AutoHeight = false;
            this.repositoryItemCheckEdit5.Name = "repositoryItemCheckEdit5";
            // 
            // repositoryItemCheckEdit6
            // 
            this.repositoryItemCheckEdit6.AutoHeight = false;
            this.repositoryItemCheckEdit6.Name = "repositoryItemCheckEdit6";
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.walletInvestorItemsControl1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1420, 988);
            this.xtraTabPage1.Text = "Items";
            // 
            // walletInvestorItemsControl1
            // 
            this.walletInvestorItemsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.walletInvestorItemsControl1.Items = null;
            this.walletInvestorItemsControl1.Location = new System.Drawing.Point(0, 0);
            this.walletInvestorItemsControl1.Name = "walletInvestorItemsControl1";
            this.walletInvestorItemsControl1.Size = new System.Drawing.Size(1420, 988);
            this.walletInvestorItemsControl1.Strategy = null;
            this.walletInvestorItemsControl1.TabIndex = 0;
            // 
            // WalletInvestorDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1432, 1043);
            this.Name = "WalletInvestorDataForm";
            this.Text = "WalletInvestorDataForm";
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tpData.ResumeLayout(false);
            this.tpChartPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit6)).EndInit();
            this.xtraTabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private CryptoMarketClient.Forms.Instruments.WalletInvestorItemsControl walletInvestorItemsControl1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit5;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit6;
    }
}