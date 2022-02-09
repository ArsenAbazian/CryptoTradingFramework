using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crypto.UI.Helpers {
    public class WithProgressPanel : IDisposable {
        public WithProgressPanel(Control owner) {
            if(owner.IsHandleCreated)
                OverlayHandle = SplashScreenManager.ShowOverlayForm(owner);
        }

        public IOverlaySplashScreenHandle OverlayHandle { get; private set; }

        public void Dispose() {
            if(OverlayHandle != null)
                SplashScreenManager.CloseOverlayForm(OverlayHandle);
        }
    }
}
