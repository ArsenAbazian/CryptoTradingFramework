using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public class ThreadUpdateForm : XtraForm {
        public ThreadUpdateForm() {
            AllowWorkThread = true;
        }

        protected virtual int UpdateInervalMs { get { return 1000; } }
        protected virtual bool AllowUpdateInactive { get { return false; } }
        protected Thread UpdateThread { get; private set; }
        protected bool AllowWorkThread { get; set; }

        protected virtual Thread CheckStartThread(Thread current, ThreadStart method) {
            if(current != null && current.IsAlive)
                return current;
            current = new Thread(method);
            current.Start();
            return current;
        } 
        protected virtual void StartUpdateThread() {
            AllowWorkThread = true;
            UpdateThread = CheckStartThread(UpdateThread, ThreadWork);
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            StartUpdateThread();
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
        protected virtual void StopUpdateThread() {
            AllowWorkThread = false;
        }

        void ThreadWork() {
            Stopwatch w = new Stopwatch();
            w.Start();
            while(AllowWorkThread) {
                Thread.Sleep(UpdateInervalMs);
                OnThreadUpdate();
            }
        }
        protected virtual void OnThreadUpdate() {
            
        }
    }
}
