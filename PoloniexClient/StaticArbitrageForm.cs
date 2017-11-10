using CryptoMarketClient.Common;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public partial class StaticArbitrageForm : ThreadUpdateForm, IStaticArbitrageUpdateListener {
        public StaticArbitrageForm() {
            InitializeComponent();
            Items = StaticArbitrageHelper.Default.GetItems();
            this.staticArbitrageInfoBindingSource.DataSource = Items;
        }

        public List<StaticArbitrageInfo> Items { get; private set; }
        protected bool ShouldProcessArbitrage { get; set; }

        int concurrentTickersCount = 0;
        Stopwatch timer = new Stopwatch();
        void OnUpdateTickers() {
            timer.Start();
            long lastGUIUpdateTime = 0;
            while(true) {
                for(int i = 0; i < Items.Count; i++) {
                    if(ShouldProcessArbitrage)
                        ProcessSelectedArbitrageInfo();
                    if(timer.ElapsedMilliseconds - lastGUIUpdateTime > 2000) {
                        lastGUIUpdateTime = timer.ElapsedMilliseconds;
                        if(IsHandleCreated)
                            BeginInvoke(new Action(RefreshGUI));
                    }
                    if(this.bbMonitorSelected.Checked && !Items[i].IsSelected)
                        continue;
                    StaticArbitrageInfo current = Items[i];
                    if(current.IsUpdating)
                        continue;
                    if(!current.ObtainingData) {
                        TickerCollectionUpdateHelper.Default.Update(current, this);
                        continue;
                    }
                    int currentUpdateTimeMS = (int)(timer.ElapsedMilliseconds - current.StartUpdateMs);
                    if(currentUpdateTimeMS > current.NextOverdueMs) {
                        current.UpdateTimeMs = currentUpdateTimeMS;
                        current.IsActual = false;
                        current.NextOverdueMs += 3000;
                        if(IsHandleCreated)
                            BeginInvoke(new Action<StaticArbitrageInfo>(RefreshGridRow), current);
                    }
                    continue;
                }
            }
        }
        public void RefreshGridRow(StaticArbitrageInfo info) {
            this.gridView1.RefreshRow(this.gridView1.GetRowHandle(Items.IndexOf(info)));
        }
        async Task UpdateArbitrageInfoTask(StaticArbitrageInfo info) {
            Task task = Task.Factory.StartNew(() => {
                TickerCollectionUpdateHelper.Default.Update(info, this);
            });
            await task;
        }

        void RefreshGUI() {
        }

        void ProcessSelectedArbitrageInfo() {
        }

        protected override void OnThreadUpdate() {
            OnUpdateTickers();
        }

        private void StaticArbitrageForm_Load(object sender, EventArgs e) {

        }

        void IStaticArbitrageUpdateListener.OnUpdateInfo(StaticArbitrageInfo info, bool useInvokeForUI) {
            info.Calculate();
            RefreshGridRow(info);
            info.IsUpdating = false;
        }

        private void bbShowHistory_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            StaticArbitrageInfo info = (StaticArbitrageInfo)this.gridView1.GetFocusedRow();
            StaticArbitrageHistoryForm form = new StaticArbitrageHistoryForm();
            form.Info = info;
            form.MdiParent = MdiParent;
            form.Show();
        }
    }
}
