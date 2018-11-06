using Crypto.Core.Common.Arbitrages;
using DevExpress.Data;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crypto.Core.Arbitrages.Dependency {
    public partial class DependencyArbitrageChartForm : XtraForm {
        public DependencyArbitrageChartForm() {
            InitializeComponent();
            FormBorderEffect = FormBorderEffect.None;
        }

        StatisticalArbitrageStrategy arbitrage;
        public StatisticalArbitrageStrategy Arbitrage {
            get { return arbitrage; }
            set {
                if(Arbitrage == value)
                    return;
                StatisticalArbitrageStrategy prev = Arbitrage;
                arbitrage = value;
                OnArbitrageChanged(prev, Arbitrage);
            }
        }

        protected string UpperName { get; set; }
        protected string LowerName { get; set; }

        void OnArbitrageChanged(StatisticalArbitrageStrategy prev, StatisticalArbitrageStrategy current) {
            if(prev != null)
                prev.Tag = null;

            if(current != null)
                current.Tag = this;

            RealTimeSource source = new RealTimeSource() { DataSource = Arbitrage.History };

            UpperName = Arbitrage.SecondName.ToString() + " (upper)";
            LowerName = Arbitrage.FirstNames[0].ToString() + " (lower)";

            this.myChartControl1.Series["Upper"].DataSource = source;
            this.myChartControl1.Series["Upper"].ArgumentDataMember = "Time";
            this.myChartControl1.Series["Upper"].ValueDataMembers.AddRange("UpperBidChart");
            this.myChartControl1.Series["Upper"].Name = UpperName;
            
            this.myChartControl1.Series["Lower"].DataSource = source;
            this.myChartControl1.Series["Lower"].ArgumentDataMember = "Time";
            this.myChartControl1.Series["Lower"].ValueDataMembers.AddRange("LowerAskChart");
            this.myChartControl1.Series["Lower"].Name = LowerName;

            this.myChartControl1.Series["Deviation"].DataSource = source;
            this.myChartControl1.Series["Deviation"].ArgumentDataMember = "Time";
            this.myChartControl1.Series["Deviation"].ValueDataMembers.AddRange("MaxDeviationLog");

            this.gridControl1.DataSource = source;
        }

        private void gridControl1_Click(object sender, EventArgs e) {

        }
    }
}
