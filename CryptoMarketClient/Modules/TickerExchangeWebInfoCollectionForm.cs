using CryptoMarketClient.Common;
using CryptoMarketClient.Helpers;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Modules {
    public partial class TickerExchangeWebInfoCollectionForm : ThreadUpdateForm {
        public TickerExchangeWebInfoCollectionForm() {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e) {
            StopThread();
            base.OnClosing(e);
        }

        public override void OnRibbonMerged(RibbonControl ownerRibbon) {
            base.OnRibbonMerged(ownerRibbon);
            ownerRibbon.SelectPage(this.ribbonControl1.Pages[0]);
        }

        protected override bool AutoStartThread => false;

        protected override void OnShown(EventArgs e) {
            string fileName = SettingsStore.Default.LastOpenedWebTickersFile;
            if(File.Exists(fileName))
                Info = TickerExchangeWebListInfo.FromFile(fileName);
            
            base.OnShown(e);
        }

        TickerExchangeWebListInfo info;
        protected TickerExchangeWebListInfo Info {
            get { return info; }
            set {
                if(Info == value)
                    return;
                info = value;
                OnInfoChanged();
            }
        }

        protected virtual void OnInfoChanged() {
            this.gridControl1.DataSource = Info == null? null: Info.Tickers;
        }

        private void biOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.xtraOpenFileDialog1.ShowDialog() == DialogResult.OK) {
                Info = TickerExchangeWebListInfo.FromFile(this.xtraOpenFileDialog1.FileName);
                if(Info == null)
                    Info = new TickerExchangeWebListInfo();
                else {
                    SettingsStore.Default.LastOpenedWebTickersFile = this.xtraOpenFileDialog1.FileName;
                    SettingsStore.Default.Save();
                }
            }
        }

        private void biNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            Info = new TickerExchangeWebListInfo();
        }

        private void biSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(!string.IsNullOrEmpty(Info.FullPath)) {
                Info.Save();
                XtraMessageBox.Show("Saved.");
                return;
            }
            if(Info != null && this.xtraSaveFileDialog1.ShowDialog() == DialogResult.OK) {
                string fileName = Path.GetFileName(this.xtraSaveFileDialog1.FileName);
                string path = Path.GetDirectoryName(this.xtraSaveFileDialog1.FileName);
                Info.FullPath = this.xtraSaveFileDialog1.FileName;
                Info.FileName = fileName;
                Info.Save();
            }
        }

        private void biAddTicker_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TickerExchangeWebInfo info = new TickerExchangeWebInfo();
            TickerWebInfoForm form = new TickerWebInfoForm();
            form.Ticker = info;
            if(form.ShowDialog() == DialogResult.OK) {
                Info.Tickers.Add(info);
                this.gridView1.RefreshData();
                Info.Save();
            }
        }

        private void biEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TickerExchangeWebInfo sel = (TickerExchangeWebInfo)this.gridView1.GetFocusedRow();
            if(sel == null)
                return;
            TickerExchangeWebInfo info = sel.Clone();
            TickerWebInfoForm form = new TickerWebInfoForm();
            form.Ticker = info;
            if(form.ShowDialog() == DialogResult.OK) {
                sel.AssignFrom(info);
                this.gridView1.RefreshData();
                Info.Save();
            }
        }

        List<TickerExchangeWebInfo> GetSelectedItems() {
            int[] rh = this.gridView1.GetSelectedRows();
            List<TickerExchangeWebInfo> items = new List<TickerExchangeWebInfo>();
            for(int i = 0; i < rh.Length; i++) {
                items.Add((TickerExchangeWebInfo)this.gridView1.GetRow(rh[i]));
            }
            return items;
        }

        private void biRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            var items = GetSelectedItems();
            if(XtraMessageBox.Show("Selected items will be removed. Are you sure?", "Deleting", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                return;
            foreach(var item in items) {
                Info.Tickers.Remove(item);
            }
            this.gridView1.RefreshData();
        }

        protected override void StartUpdateThread() {
            base.StartUpdateThread();
            this.biStatus.ImageIndex = 1;
            this.biStatus.Caption = "Updating Tickers";
        }

        protected override void StopUpdateThread() {
            base.StopUpdateThread();
            this.biStatus.ImageIndex = 2;
            this.biStatus.Caption = "Update Stopped";
        }

        private void biUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            StartUpdateThread();
        }

        private void biStop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            StopUpdateThread();
        }

        protected override void OnThreadUpdate() {
            if(Info == null)
                return;
            foreach(var ticker in Info.Tickers) {
                ticker.Update();
                if(IsHandleCreated)
                    BeginInvoke(new Action<TickerExchangeWebInfo>(UpdateTicker), ticker);
            }
        }

        void UpdateTicker(TickerExchangeWebInfo t) {
            int rh = this.gridView1.GetRowHandle(Info.Tickers.IndexOf(t));
            this.gridView1.RefreshRow(rh);
        }

        private void biCopyFrom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TickerExchangeWebInfo info = (TickerExchangeWebInfo)this.gridView1.GetFocusedRow();
            if(info == null)
                return;

            TickerExchangeWebInfo newInfo = new TickerExchangeWebInfo();
            newInfo.AssignFrom(info);

            TickerWebInfoForm form = new TickerWebInfoForm();
            form.Ticker = newInfo;
            if(form.ShowDialog() == DialogResult.OK) {
                Info.Tickers.Add(newInfo);
                this.gridView1.RefreshData();
                Info.Save();
            }
        }

        private void bbOpenWeb_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TickerExchangeWebInfo info = (TickerExchangeWebInfo)this.gridView1.GetFocusedRow();
            if(info == null)
                return;
            System.Diagnostics.Process.Start(info.WebPage);
        }

        private void bbUpdateBot_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TelegramBot.Default.Update();
        }
    }
}
