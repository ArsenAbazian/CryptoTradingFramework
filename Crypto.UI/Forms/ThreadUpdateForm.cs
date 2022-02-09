using Crypto.Core.Common;
using DevExpress.XtraBars.Ribbon;
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
        protected Thread UpdateThread { get; set; }
        protected bool AllowWorkThread { get; set; }

        protected virtual Thread CheckStartThread(Thread current, ThreadStart method) {
            if(current != null && current.IsAlive)
                return current;
            current = new Thread(method);
            current.Start();
            LogManager.Default.Add(GetType().Name + ": Start Thread");
            return current;
        } 
        protected virtual void StartUpdateThread() {
            AllowWorkThread = true;
            UpdateThread = CheckStartThread(UpdateThread, ThreadWork);
        }

        protected virtual bool AutoStartThread { get { return true; } }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            if(AutoStartThread)
                StartUpdateThread();
        }

        protected override void OnActivated(EventArgs e) {
            base.OnActivated(e);
            if(!AllowUpdateInactive && AutoStartThread)
                StartUpdateThread();
        }

        protected override void OnDeactivate(EventArgs e) {
            base.OnDeactivate(e);
            if(!AllowUpdateInactive && AutoStartThread)
                StopUpdateThread();
        }
        protected virtual void StopUpdateThread() {
            AllowWorkThread = false;
        }
        public void StopThread() {
            AllowWorkThread = false;
        }

        protected void ThreadWork() {
            Stopwatch w = new Stopwatch();
            w.Start();
            while(AllowWorkThread) {
                OnThreadUpdate();
                Thread.Sleep(UpdateInervalMs);
            }
            LogManager.Default.Add(GetType().Name + ": Stop Thread");
        }
        protected virtual void OnThreadUpdate() {
            
        }

        public virtual void OnRibbonMerged(RibbonControl ownerRibbon) {
        }
    }
}
