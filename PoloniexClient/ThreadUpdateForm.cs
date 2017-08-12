using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public class ThreadUpdateForm : Form {
        public ThreadUpdateForm() {
            AllowWorkThread = true;
        }

        protected virtual int UpdateInervalMs { get { return 1000; } }
        protected virtual bool AllowUpdateInactive { get { return false; } }
        protected Thread UpdateThread { get; private set; }
        protected bool AllowWorkThread { get; set; }

        void StartUpdateThread() {
            AllowWorkThread = true;
            if(UpdateThread != null && UpdateThread.IsAlive)
                return;
            UpdateThread = new Thread(ThreadWork);
            UpdateThread.Start();
        }

        protected override void OnActivated(EventArgs e) {
            base.OnActivated(e);
            if(!AllowUpdateInactive)
                StartUpdateThread();
        }

        protected override void OnDeactivate(EventArgs e) {
            base.OnDeactivate(e);
            if(!AllowUpdateInactive)
                StopUpdateThread();
        }
        void StopUpdateThread() {
            AllowWorkThread = false;
        }

        void ThreadWork() {
            while(AllowWorkThread) {
                OnThreadUpdate();
            }
        }
        async protected virtual void OnThreadUpdate() {

        }
    }
}
