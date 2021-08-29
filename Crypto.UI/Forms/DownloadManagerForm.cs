using Crypto.Core;
using Crypto.Core.Helpers;
using Crypto.Core.Strategies;
using CryptoMarketClient;
using CryptoMarketClient.Strategies;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crypto.UI.Forms {
    public partial class DownloadManagerForm : XtraForm {
        public DownloadManagerForm() {
            InitializeComponent();
            InitializeData();
        }

        public List<Core.Strategies.TickerDownloadDataInfo> DataItems { get; set; } = new List<Core.Strategies.TickerDownloadDataInfo>();

        private void InitializeData() {
            string cd = CachedCandleStickData.Directory;
            string[] files = Directory.GetFiles(cd);
            List<Core.Strategies.TickerDownloadDataInfo> res = new List<Core.Strategies.TickerDownloadDataInfo>();
            foreach(string file in files) {
                string[] items = file.Split('_');
                items[0] = items[0].Substring(items[0].LastIndexOf('\\') + 1);
                Core.Strategies.TickerDownloadDataInfo item = new Core.Strategies.TickerDownloadDataInfo();
                item.Type = TickerDataType.Candlesticks;
                item.Exchange = (ExchangeType)Enum.Parse(typeof(ExchangeType), items[0]);
                item.BaseCurrency = items[1];
                item.MarketCurrency = items[2];
                item.KLineIntervalMin = int.Parse(items[3]);
                if(items.Length == 7) {
                    item.StartDate = TickerDownloadDataInfo.DateTimeFromString(items[4]);
                    item.EndDate = TickerDownloadDataInfo.DateTimeFromString(items[5]);
                }
                res.Add(item);
            }

            cd = CachedTradeHistory.Directory;
            files = Directory.GetFiles(cd);
            foreach(string file in files) {
                string[] items = file.Split('_');
                items[0] = items[0].Substring(items[0].LastIndexOf('\\') + 1);
                Core.Strategies.TickerDownloadDataInfo item = new Core.Strategies.TickerDownloadDataInfo();
                item.Type = TickerDataType.TradeHistory;
                item.Exchange = (ExchangeType)Enum.Parse(typeof(ExchangeType), items[0]);
                item.BaseCurrency = items[1];
                item.MarketCurrency = items[2];
                item.KLineIntervalMin = 0;
                if(items.Length == 6) {
                    item.StartDate = TickerDownloadDataInfo.DateTimeFromString(items[3]);
                    item.EndDate = TickerDownloadDataInfo.DateTimeFromString(items[4]);
                }
                res.Add(item);
            }
            DataItems = res;
            this.gridControl1.DataSource = res;
            this.gridView1.ExpandAllGroups();
            int bestWidth = 0;
            foreach(GridColumn item in this.gridView1.Columns) {
                if(!item.Visible)
                    continue;
                bestWidth += this.gridView1.CalcColumnBestWidth(item);
            }
            this.gridControl1.MaximumSize = new Size(bestWidth + 100, 0);
        }

        private void OnTickerDownloadProgressChanged(object sender, TickerDownloadProgressEventArgs e) {
            ProgressForm.SetProgress(e.DownloadText, (int)e.DownloadProgress);
            e.Cancel = DownloadCanceled;
        }

        protected bool DownloadCanceled { get; set; }
        protected DownloadProgressForm ProgressForm { get; set; }

        private void biDownload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            using(TickerDataDownloadForm dlg = new TickerDataDownloadForm() { Owner = this }) {
                if(dlg.ShowDialog() != DialogResult.OK)
                    return;
                DownloadCanceled = false;
                TradeHistoryIntensityInfo info = new TradeHistoryIntensityInfo();
                info.DownloadProgressChanged += OnTickerDownloadProgressChanged;
                info.Exchange = Exchange.Get(dlg.TickerInfo.Exchange);
                Core.Helpers.TickerDownloadData historyInfo = null;
                ProgressForm = new DownloadProgressForm();
                ProgressForm.Cancel += (d, ee) => {
                    DownloadCanceled = true;
                };

                Thread t = new Thread(() => {
                    historyInfo = info.DownloadItem(dlg.TickerInfo);
                    BeginInvoke(new MethodInvoker(() => ProgressForm.Close()));
                });
                t.Start();
                ProgressForm.ShowDialog(this);
                if(DownloadCanceled) {
                    XtraMessageBox.Show("Downloading Canceled.");
                    return;
                }
                if(historyInfo == null) {
                    XtraMessageBox.Show("Error downloading ticker trade history");
                    return;
                }
                info.Items.Add(historyInfo);
                info.Result.Add(historyInfo);

                //TickerDownloadDataInfo ki = new TickerDownloadDataInfo();
                //ki.Type = TickerDataType.Candlesticks;
                //ki.Exchange = dlg.TickerInfo.Exchange;
                //ki.BaseCurrency = dlg.TickerInfo.Ticker.BaseCurrency;
                //ki.MarketCurrency = dlg.TickerInfo.Ticker.MarketCurrency;
                //ki.KLineIntervalMin = dlg.TickerInfo.KlineIntervalMin;
                //ki.StartDate = dlg.TickerInfo.StartDate;
                //ki.EndDate = dlg.TickerInfo.EndDate;
                //ki.GenerateFileName();

                //var prevItem = DataItems.FirstOrDefault(i => i.FileName.EndsWith(ki.FileName));
                //if(prevItem != null)
                //    DataItems.Remove(prevItem);
                //DataItems.Add(ki);

                //TickerDownloadDataInfo ti = new TickerDownloadDataInfo();
                //ti.Type = TickerDataType.TradeHistory;
                //ti.Type = TickerDataType.Candlesticks;
                //ti.Exchange = dlg.TickerInfo.Exchange;
                //ti.BaseCurrency = dlg.TickerInfo.Ticker.BaseCurrency;
                //ti.MarketCurrency = dlg.TickerInfo.Ticker.MarketCurrency;
                //ti.KLineIntervalMin = dlg.TickerInfo.KlineIntervalMin;
                //ti.StartDate = dlg.TickerInfo.StartDate;
                //ti.EndDate = dlg.TickerInfo.EndDate;
                //ti.GenerateFileName();

                //prevItem = DataItems.FirstOrDefault(i => i.FileName.EndsWith(ti.FileName));
                //if(prevItem != null)
                //    DataItems.Remove(prevItem);
                //DataItems.Add(ti);
                InitializeData();
            }
        }

        private void gvData_RowStyle(object sender, RowStyleEventArgs e) {
            if(this.gridView1.FocusedRowHandle != e.RowHandle)
                return;
            e.Appearance.BackColor = Color.FromArgb(0x10, this.gridView1.PaintAppearance.FocusedRow.BackColor);
            e.HighPriority = true;
        }

        private void biRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            List<TickerDownloadDataInfo> items = DataItems.Where(i => i.Selected).ToList();
            if(items.Count == 0) {
                XtraMessageBox.Show("Nothing selected.");
                return;
            }
            if(XtraMessageBox.Show("Are you sure to remove selected items?", "Remove", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                return;
            foreach(var item in items) {
                if(item.FileName == null)
                    item.GenerateFileName();
                File.Delete(item.FileName);
            }
        }

        protected List<TickerDownloadDataInfo> GetTickersForData() {
            List<TickerDownloadDataInfo> items = DataItems.Where(i => i.Selected).ToList();
            List<TickerDownloadDataInfo> filtered = new List<TickerDownloadDataInfo>();
            foreach(var item in items) {
                if(filtered.FirstOrDefault(t => t.Exchange == item.Exchange && t.BaseCurrency == item.BaseCurrency && t.MarketCurrency == item.MarketCurrency) == null)
                    filtered.Add(item);
            }
            return filtered;
        }

        private void biTickerIntensity_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            List<TickerDownloadDataInfo> selected = GetTickersForData();
            TradeHistoryIntensityInfo info = new TradeHistoryIntensityInfo();
            info.DownloadProgressChanged += OnTickerDownloadProgressChanged;
            Crypto.Core.Helpers.TickerDownloadData historyInfo = null;
            ProgressForm = new DownloadProgressForm();
            ProgressForm.Cancel += (d, ee) => {
                DownloadCanceled = true;
            };

            Thread t = new Thread(() => {
                foreach(TickerDownloadDataInfo di in selected) {
                    Exchange ex = Exchange.Get(di.Exchange);
                    if(!ex.Connect())
                        continue;
                    TickerInputInfo ti = new TickerInputInfo() { Exchange = ex.Type, Ticker = ex.Ticker(di.BaseCurrency, di.MarketCurrency), StartDate = di.StartDate, EndDate = di.EndDate, KlineIntervalMin = di.KLineIntervalMin};
                    ti.TickerName = ti.Ticker.Name;
                    historyInfo = info.DownloadItem(ti, false);
                    info.Items.Add(historyInfo);
                    info.Result.Add(historyInfo);
                }
                BeginInvoke(new MethodInvoker(() => ProgressForm.Close()));
            });
            t.Start();
            ProgressForm.ShowDialog(this);
            if(DownloadCanceled) {
                XtraMessageBox.Show("Downloading Canceled.");
                return;
            }

            StrategyDataForm dataForm = new StrategyDataForm();
            dataForm.Visual = info;
            dataForm.Show();
        }
    }
}
