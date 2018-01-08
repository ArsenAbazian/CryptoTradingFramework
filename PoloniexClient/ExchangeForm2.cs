using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraEditors;

namespace CryptoMarketClient {
    public partial class ExchangeForm2 : XtraForm {
        public ExchangeForm2(Exchange exchange) {
            Exchange = exchange;
            InitializeComponent();
        }

        public Exchange Exchange { get; private set; }

        void ExchangeForm2_Load(object sender, EventArgs e) {
            BaseDocument doc = widgetView1.AddDocument(new BalancesUserControl(Exchange));
            doc.Caption = "Balances";
            doc = widgetView1.AddDocument(new FavoritePairsUserControl(Exchange));
            doc.Caption = "Favorites";
        }
    }
}
