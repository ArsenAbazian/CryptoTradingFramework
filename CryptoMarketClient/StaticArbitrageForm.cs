using Crypto.Core;
using Crypto.Core.Bittrex;
using Crypto.Core.Common;
using DevExpress.XtraBars;
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
            Items = TriplePairArbitrageHelper.Default.GetItems();
            this.staticArbitrageInfoBindingSource.DataSource = Items;
            //UpdateBalances();
            //GenerateBalanceItems();
        }

        void UpdateBalances() {
            if(PoloniexExchange.Default.IsConnected) {
                for(int i = 0; i < 3; i++)
                    if(PoloniexExchange.Default.GetBalance(PoloniexExchange.Default.DefaultAccount, "USDT"))
                        break;
            }
            if(BittrexExchange.Default.IsConnected)
                for(int i = 0; i < 3; i++)
                    if(BittrexExchange.Default.GetBalance(BittrexExchange.Default.DefaultAccount, "USDT"))
                        break;
            foreach(TriplePairArbitrageInfo info in Items) {
                for(int i = 0; i < 3; i++) {
                    if(info.AltBase.UpdateBalance(info.AltBase.MarketCurrency)) {
                        info.AltBalanceInfo = info.AltBase.MarketBalanceInfo;
                        break;
                    }
                }
                for(int i = 0; i < 3; i++) {
                    if(info.BaseUsdt.UpdateBalance(info.BaseUsdt.MarketCurrency)) {
                        info.BaseBalanceInfo = info.BaseUsdt.MarketBalanceInfo;
                        break;
                    }
                }
                info.UsdtBalanceInfo = info.AltUsdt.BaseBalanceInfo;
            }
            UsdtBalances = TriplePairArbitrageHelper.Default.GetUsdtBalances();
        }

        public List<TriplePairArbitrageInfo> Items { get; private set; }
        public List<BalanceBase> UsdtBalances { get; private set; }
        protected bool ShouldProcessArbitrage { get; set; }

        Stopwatch timer = new Stopwatch();
        void OnUpdateTickers() {
            timer.Start();
            long lastGUIUpdateTime = 0;
            while(true) {
                for(int i = 0; i < Items.Count; i++) {
                    if(ShouldProcessArbitrage) {
                        Thread.Sleep(10);
                        continue;
                    }
                    if(timer.ElapsedMilliseconds - lastGUIUpdateTime > 2000) {
                        lastGUIUpdateTime = timer.ElapsedMilliseconds;
                        if(IsHandleCreated)
                            BeginInvoke(new Action(RefreshGUI));
                    }
                    if(this.bbMonitorSelected.Checked && !Items[i].IsSelected)
                        continue;
                    TriplePairArbitrageInfo current = Items[i];
                    if(current.IsUpdating)
                        continue;
                    if(!current.ObtainingData) {
                        ClassicArbitrageManager.Default.Update(current, this);
                        continue;
                    }
                    int currentUpdateTimeMS = (int)(timer.ElapsedMilliseconds - current.StartUpdateMs);
                    if(currentUpdateTimeMS > current.NextOverdueMs) {
                        current.UpdateTimeMs = currentUpdateTimeMS;
                        current.IsActual = false;
                        current.NextOverdueMs += 3000;
                        if(IsHandleCreated)
                            BeginInvoke(new Action<TriplePairArbitrageInfo>(RefreshGridRow), current);
                    }
                    continue;
                }
            }
        }
        public void RefreshGridRow(TriplePairArbitrageInfo info) {
            this.gridView1.RefreshRow(this.gridView1.GetRowHandle(Items.IndexOf(info)));
        }
        async Task UpdateArbitrageInfoTask(TriplePairArbitrageInfo info) {
            Task task = Task.Factory.StartNew(() => {
                ClassicArbitrageManager.Default.Update(info, this);
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

        void IStaticArbitrageUpdateListener.OnUpdateInfo(TriplePairArbitrageInfo info, bool useInvokeForUI) {
            info.Calculate();
            if(!ShouldProcessArbitrage && info.IsSelected) {
                ShouldProcessArbitrage = true;
                if(!info.MakeOperation()) {
                    info.IsErrorState = true;
                    XtraMessageBox.Show("Static Arbitrage Operation Failed. Resolve conflicts manually. " + info.Exchange + "-" + info.AltCoin + "-" + info.BaseCoin);
                }
                if(info.OperationExecuted) {
                    info.OperationExecuted = false;
                    BeginInvoke(new MethodInvoker(UpdateBalanceItems));
                }
                ShouldProcessArbitrage = false;
            }
            this.BeginInvoke(new Action<TriplePairArbitrageInfo>(RefreshGridRow), info);
            info.IsUpdating = false;
        }
        void UpdateBalanceItems() {
            //this.ribbonStatusBar1.BeginUpdate();
            try {
                foreach(BarItemLink link in this.ribbonStatusBar1.ItemLinks) {
                    UpdateBalanceItem(link.Item);
                }
            }
            finally {
                //this.ribbonStatusBar1.EndUpdate();
            }
        }

        private void bbShowHistory_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TriplePairArbitrageInfo info = (TriplePairArbitrageInfo)this.gridView1.GetFocusedRow();
            StaticArbitrageHistoryForm form = new StaticArbitrageHistoryForm();
            form.Info = info;
            form.MdiParent = MdiParent;
            form.Show();
        }
        void GenerateBalanceItems() {
            foreach(BalanceBase b in UsdtBalances) {
                
                BarStaticItem item = new BarStaticItem();
                item.ItemAppearance.Normal.FontSizeDelta = 3;
                item.Tag = b;
                this.ribbonControl1.Items.Add(item);
                item.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
                UpdateBalanceItem(item);
                this.ribbonStatusBar1.ItemLinks.Add(item);
            }
        }
        void UpdateBalanceItem(BarItem item) {
            BalanceBase b = (BalanceBase)item.Tag;
            string text = b.Exchange + ": <b>" + b.Available.ToString("0.00000000") + "</b>";
            item.Caption = text;
        }

        private void bbClearSelected_ItemClick(object sender, ItemClickEventArgs e) {
            foreach(TriplePairArbitrageInfo info in Items) { 
                info.IsSelected = false;
            }
            this.gridControl1.RefreshDataSource();
        }

        private void gridControl1_Click(object sender, EventArgs e) {

        }

        private void repositoryItemCheckEdit1_EditValueChanged(object sender, EventArgs e) {
            this.gridView1.CloseEditor();
        }

        private void bbShowLog_ItemClick(object sender, ItemClickEventArgs e) {
            LogManager.Default.ShowLogForm();
        }
    }
}
