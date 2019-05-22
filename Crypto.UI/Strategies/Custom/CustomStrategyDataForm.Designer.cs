namespace CryptoMarketClient.Strategies.Custom {
    partial class CustomStrategyDataForm {
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
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpChart
            // 
            this.tabControl.SelectedTabPage = this.tpData;
            this.tabControl.Size = new System.Drawing.Size(1203, 781);
            // 
            // tpData
            // 
            this.tpData.Size = new System.Drawing.Size(1191, 726);
            // 
            // tpChartPage
            // 
            this.tpChartPage.Size = new System.Drawing.Size(1191, 726);
            // 
            // CustomStrategyDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 781);
            this.Name = "CustomStrategyDataForm";
            this.Text = "CustomStrategyDataForm";
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}