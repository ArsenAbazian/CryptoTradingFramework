namespace CryptoMarketClient {
    partial class ExchangeCollectionForm {
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
            this.exchangeCollectionControl1 = new TileViewSelector.ExchangeCollectionControl();
            this.SuspendLayout();
            // 
            // exchangeCollectionControl1
            // 
            this.exchangeCollectionControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exchangeCollectionControl1.Location = new System.Drawing.Point(0, 0);
            this.exchangeCollectionControl1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.exchangeCollectionControl1.Name = "exchangeCollectionControl1";
            this.exchangeCollectionControl1.Size = new System.Drawing.Size(1361, 892);
            this.exchangeCollectionControl1.TabIndex = 0;
            // 
            // ExchangeCollectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1361, 892);
            this.Controls.Add(this.exchangeCollectionControl1);
            this.Name = "ExchangeCollectionForm";
            this.Text = "Exchanges";
            this.ResumeLayout(false);

        }

        #endregion

        private TileViewSelector.ExchangeCollectionControl exchangeCollectionControl1;
    }
}