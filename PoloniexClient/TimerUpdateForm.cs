using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public class TimerUpdateForm : XtraForm {

        protected virtual int UpdateInervalMs { get { return 1000; } }
        protected virtual bool AllowUpdateInactive { get { return false; } }

        Timer timer;
        public Timer Timer {
            get {
                if(timer == null) {
                    timer = new Timer();
                    timer.Interval = UpdateInervalMs;
                    timer.Tick += OnTimerUpdate;
                }
                return timer;
            }
        }

        protected override void OnActivated(EventArgs e) {
            base.OnActivated(e);
            if(!AllowUpdateInactive)
                Timer.Start();
        }

        protected override void OnDeactivate(EventArgs e) {
            base.OnDeactivate(e);
            if(!AllowUpdateInactive)
                Timer.Stop();
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            Timer.Start();
        }

        protected virtual void OnTimerUpdate(object sender, EventArgs e) {
            
        }
    }
}
