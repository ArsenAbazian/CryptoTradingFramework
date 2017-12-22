namespace CryptoMarketClient {
    partial class AnalyticsForm {
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
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            this.bidChangeChart = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.bidChangeChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            this.SuspendLayout();
            // 
            // arbitrageHistoryChart
            // 
            this.bidChangeChart.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.bidChangeChart.DataBindings = null;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisX.WholeRange.AutoSideMargins = false;
            xyDiagram1.AxisX.WholeRange.SideMarginsValue = 0D;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.bidChangeChart.Diagram = xyDiagram1;
            this.bidChangeChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bidChangeChart.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Right;
            this.bidChangeChart.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.bidChangeChart.Legend.MarkerMode = DevExpress.XtraCharts.LegendMarkerMode.CheckBoxAndMarker;
            this.bidChangeChart.Legend.Name = "Default Legend";
            this.bidChangeChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.bidChangeChart.Location = new System.Drawing.Point(0, 0);
            this.bidChangeChart.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.bidChangeChart.Name = "arbitrageHistoryChart";
            series1.Name = "Series 1";
            this.bidChangeChart.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.bidChangeChart.Size = new System.Drawing.Size(1872, 1223);
            this.bidChangeChart.TabIndex = 6;
            // 
            // AnalyticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1872, 1223);
            this.Controls.Add(this.bidChangeChart);
            this.Name = "AnalyticsForm";
            this.Text = "AnalyticsForm";
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bidChangeChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl bidChangeChart;
    }
}