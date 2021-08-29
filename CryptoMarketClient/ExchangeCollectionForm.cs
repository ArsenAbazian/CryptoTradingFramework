using Crypto.Core;
using CryptoMarketClient.Helpers;
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

namespace CryptoMarketClient {
    public partial class ExchangeCollectionForm : XtraForm {
        public ExchangeCollectionForm() {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            InitializeCollection();
        }

        void InitializeCollection() {
            List<LogoInfo> list = new List<LogoInfo>();
            foreach(Exchange e in Exchange.Registered) {
                list.Add(new LogoInfo(e));
            }
            this.exchangeCollectionControl1.SetData(list);
        }

        protected override void OnClosing(CancelEventArgs e) {
            base.OnClosing(e);
        }
    }

    public class LogoInfo {
        public LogoInfo(Exchange e) {
            Exchange = e;
        }
        public Exchange Exchange { get; set; }
        public bool IsConnected { get { return Exchange.IsConnected; } }
        Image image;
        public Image Image {
            get {
                if(image == null)
                    image = ExchangeLogoProvider.GetImage(Exchange);
                return image;
            }
        }
        public string Caption { get { return Exchange.Name.ToUpper(); } }
    }
}
