using CryptoMarketClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Helpers {
    public class CmLogManager : LogManager {
        static CmLogManager() {
            LogManager.Default = new CmLogManager();
        }
        protected LogForm Form { get; set; }
        public override void ShowLogForm() {
            if(Form == null || Form.IsDisposed)
                Form = new LogForm();
            Form.Messages = Messages;
            Form.Show();
            Form.Activate();
        }

        protected override void RefreshVisual() {
            if(Form != null && !Form.IsDisposed)
                Form.Invoke(new MethodInvoker(Form.RefreshData));
        }
    }
}
