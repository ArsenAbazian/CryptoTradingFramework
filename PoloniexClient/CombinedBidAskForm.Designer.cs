namespace CryptoMarketClient {
    partial class CombinedBidAskForm {
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
            this.arbitrageHistoryChart = new MyChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.arbitrageHistoryChart)).BeginInit();
            this.SuspendLayout();
            // 
            // arbitrageHistoryChart
            // 
            this.arbitrageHistoryChart.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.arbitrageHistoryChart.DataBindings = null;
            this.arbitrageHistoryChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.arbitrageHistoryChart.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Right;
            this.arbitrageHistoryChart.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.arbitrageHistoryChart.Legend.MarkerMode = DevExpress.XtraCharts.LegendMarkerMode.CheckBoxAndMarker;
            this.arbitrageHistoryChart.Legend.Name = "Default Legend";
            this.arbitrageHistoryChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.arbitrageHistoryChart.Location = new System.Drawing.Point(0, 0);
            this.arbitrageHistoryChart.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.arbitrageHistoryChart.Name = "arbitrageHistoryChart";
            this.arbitrageHistoryChart.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.arbitrageHistoryChart.Size = new System.Drawing.Size(1775, 1004);
            this.arbitrageHistoryChart.TabIndex = 6;
            // 
            // CombinedBidAskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1775, 1004);
            this.Controls.Add(this.arbitrageHistoryChart);
            this.Name = "CombinedBidAskForm";
            this.Text = "CombinedBidAsk";
            ((System.ComponentModel.ISupportInitialize)(this.arbitrageHistoryChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MyChartControl arbitrageHistoryChart;
    }
}