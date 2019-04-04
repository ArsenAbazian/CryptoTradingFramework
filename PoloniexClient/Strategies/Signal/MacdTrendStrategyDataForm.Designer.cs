namespace CryptoMarketClient.Strategies.Signal {
    partial class MacdTrendStrategyDataForm {
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
            DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.macdTrendStrategyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tpChart)).BeginInit();
            this.tpChart.SuspendLayout();
            this.tpData.SuspendLayout();
            this.tpChartPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.macdTrendStrategyBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tpChart
            // 
            this.tpChart.SelectedTabPage = this.tpData;
            this.tpChart.Size = new System.Drawing.Size(1367, 1004);
            // 
            // tpData
            // 
            this.tpData.Size = new System.Drawing.Size(1355, 949);
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
            repositoryItemCheckEdit2});
            this.gcData.Size = new System.Drawing.Size(1355, 949);
            // 
            // chartControl
            // 
            this.chartControl.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Left;
            this.chartControl.Legend.Name = "Default Legend";
            this.chartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            // 
            // MacdTrendStrategyDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1367, 1004);
            this.Name = "MacdTrendStrategyDataForm";
            this.Text = "TripleRsiStrategyDataForm";
            ((System.ComponentModel.ISupportInitialize)(this.tpChart)).EndInit();
            this.tpChart.ResumeLayout(false);
            this.tpData.ResumeLayout(false);
            this.tpChartPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.macdTrendStrategyBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource macdTrendStrategyBindingSource;
    }
}