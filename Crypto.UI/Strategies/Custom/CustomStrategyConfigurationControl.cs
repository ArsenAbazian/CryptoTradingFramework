using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Crypto.Core.Strategies;
using DevExpress.XtraEditors;
using Crypto.Core.Common;
using Crypto.Core.Strategies.Custom;
using DevExpress.XtraEditors.Repository;
using System.Reflection;
using DevExpress.Utils.Behaviors;
using DevExpress.XtraVerticalGrid.Rows;
using Crypto.Core.Helpers;
using Crypto.Core;
using DevExpress.XtraGrid;

namespace CryptoMarketClient.Strategies.Custom {
    public partial class CustomStrategyConfigurationControl : StrategySpecificConfigurationControlBase {
        public CustomStrategyConfigurationControl() {
            InitializeComponent();
            InitializeExchangesCombo(this.rpiExchanges);
            this.cbExchangeTickers.ButtonClick += FirstExchangeTickersCombo_ButtonClick;
            this.cbExchangeTickers.CustomDisplayText += FirstExchangeTickersCombo_CustomDisplayText;
        }

        void InitializeExchangesCombo(RepositoryItemComboBox properties) {
            foreach(Exchange e in Exchange.Registered) {
                properties.Items.Add(e.Type);
            }
        }

        private void FirstExchangeTickersCombo_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e) {
            e.DisplayText = e.Value == null ? "not selected" : e.Value.ToString();
        }

        private void FirstExchangeTickersCombo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            if(e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph) {
                UpdateFirstExchangeTickersCombo();
            }
        }

        void UpdateFirstExchangeTickersCombo() {
            UpdateFirstExchangeTickersCombo(Exchange.Get(((TickerNameInfo)this.gridView1.GetFocusedRow()).Exchange));
        }

        protected bool ConnectExchange(Exchange e) {
            try {
                e.Connect();
            }
            catch(Exception ee) {
                Telemetry.Default.TrackException(ee);
                XtraMessageBox.Show("error connecting exchange. try again later.");
                return false;
            }
            return true;
        }

        public new CustomTickerStrategy Strategy { get { return (CustomTickerStrategy)base.Strategy; } set { base.Strategy = value; } }

        string GetPreferredTicker(ExchangeType exchange) {
            if(Strategy.StrategyInfo.Tickers.Count > 0) {
                Ticker t = Exchange.Get(exchange).Tickers.FirstOrDefault(tt => tt.Name == Strategy.StrategyInfo.Tickers[0].TickerName);
                if(t == null)
                    return string.Empty;
                return t.Name;
            }
            return Exchange.Get(exchange).BtcUsdtTicker.Name;
        }

        void UpdateFirstExchangeTickersCombo(Exchange newExchange) {
            TickerNameInfo info = (TickerNameInfo)this.gridView1.GetFocusedRow();
            if(!ConnectExchange(newExchange)) {
                this.cbExchangeTickers.DataSource = null;
            }
            else {
                this.cbExchangeTickers.DataSource = newExchange.Tickers;
                info.Ticker = GetPreferredTicker(newExchange.Type);
            }
        }

        protected override void OnStrategyChanged() {
            base.OnStrategyChanged();
            //this.exchangeTickersBindingSource.DataSource = Exchange.GetTickersNameInfo();
            this.tickerInputInfoBindingSource.DataSource = Strategy.StrategyInfo.Tickers;
            FilterGridColumns();
            InitializePropertyTabs();
            SubscribePropertyGridEvents();
            this.propertyGridControl1.SelectedObject = Strategy;
        }

        private void FilterGridColumns() {
            if(!Strategy.AllowKline) {
                this.colKlineIntervalMin.Visible = false;
                this.colUseKline.Visible = false;
            }
            if(!Strategy.AllowSimulationFile) {
                this.colSimulationDataFile.Visible = false;
            }
            if(!Strategy.AllowTradeHistory) {
                this.colUseTradeHistory.Visible = false;
            }
            if(!Strategy.AllowOrderBook) {
                this.colUseOrderBook.Visible = false;
                this.colOrderBookDepth.Visible = false;
            }
        }

        private void SubscribePropertyGridEvents() {
            this.propertyGridControl1.CustomRecordCellEdit += OnPropertyGridCustomRecordCellEdit;
        }

        private void OnPropertyGridCustomRecordCellEdit(object sender, DevExpress.XtraVerticalGrid.Events.GetCustomRowCellEditEventArgs e) {
            PropertyDescriptor desc = this.propertyGridControl1.GetPropertyDescriptor(e.Row);
            FileNameAttribute attr = GetFileNameAttribute(desc);
            if(attr == null)
                return;
            e.RepositoryItem = this.reFileEditorButton;
        }

        private void InitializePropertyTabs() {
            PropertyInfo[] props = Strategy.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            IEnumerable<IGrouping<string, PropertyInfo>> groups = props.Where(p => {
                BrowsableAttribute a = p.GetCustomAttribute<BrowsableAttribute>();
                Crypto.Core.Strategies.StrategyPropertyAttribute pt = p.GetCustomAttribute<Crypto.Core.Strategies.StrategyPropertyAttribute>();
                if(a != null)
                    return a.Browsable;
                if(pt != null)
                    return pt.Browsable;
                return true;
            }).ToList().GroupBy(p => {
                Crypto.Core.Strategies.StrategyPropertyAttribute pt = p.GetCustomAttribute<Crypto.Core.Strategies.StrategyPropertyAttribute>();
                return pt == null ? "Common" : pt.TabName;
            });
            this.propertyGridControl1.BeginUpdate();
            try {
                foreach(var group in groups) {
                    DevExpress.XtraVerticalGrid.Tab tab = new DevExpress.XtraVerticalGrid.Tab(); tab.Caption = group.Key;
                    foreach(PropertyInfo p in group)
                        tab.FieldNames.Add(p.Name);
                    this.propertyGridControl1.Tabs.Insert(0, tab);
                }
            }
            finally {
                this.propertyGridControl1.EndUpdate();
            }
        }

        private void repositoryItemGridLookUpEdit1_EditValueChanged(object sender, EventArgs e) {
            TickerInputInfo info = (TickerInputInfo)this.gridView1.GetFocusedRow();
            info.TickerName = Convert.ToString(((GridLookUpEdit)sender).EditValue);
        }

        protected virtual TickerInputInfo CreateDefaultTickerInputInfo() {
            return new TickerInputInfo();
        }

        private void biAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ((CustomTickerStrategy)Strategy).StrategyInfo.Tickers.Add(CreateDefaultTickerInputInfo());
            this.gridView1.RefreshData();
        }

        private void biRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TickerInputInfo info = (TickerInputInfo)this.gridView1.GetFocusedRow();
            ((CustomTickerStrategy)Strategy).StrategyInfo.Tickers.Remove(info);
            this.gridView1.RefreshData();
        }

        private void reCheckEditAutoClose_EditValueChanged(object sender, EventArgs e) {
            this.gridView1.CloseEditor();
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e) {
            if(this.gridView1.FocusedColumn == this.colTicker) {
                TickerInputInfo info = (TickerInputInfo)this.gridView1.GetFocusedRow();
                Exchange ee = Exchange.Get(info.Exchange);
                ee.Connect();
                this.exchangeTickersBindingSource.DataSource = ee.Tickers;
            }
            else if(this.gridView1.FocusedColumn == this.colKlineIntervalMin) {
                TickerInputInfo info = (TickerInputInfo)this.gridView1.GetFocusedRow();
                Exchange ee = Exchange.Get(info.Exchange);
                ee.Connect();
                this.candleStickIntervalInfoBindingSource.DataSource = ee.GetAllowedCandleStickIntervals();
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            TickerInputInfo info = (TickerInputInfo)this.gridView1.GetFocusedRow();
            if(e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis) {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Zip files (*.zip)|*.zip|All files (*.*)|*.*";
                dialog.Title = "Open zip packed ticker's captured data file";
                if(dialog.ShowDialog() != DialogResult.OK)
                    return;
                if(!dialog.FileName.ToLower().Contains(info.TickerName.ToLower())) {
                    XtraMessageBox.Show("This simulation data file do not belonge to specified ticker.");
                    return;
                }
                
                ((ButtonEdit)sender).Text = dialog.FileName;
            }
            else
                ((ButtonEdit)sender).EditValue = null;
            this.gridView1.CloseEditor();
        }

        private void biMoveUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TickerInputInfo info = (TickerInputInfo)this.gridView1.GetFocusedRow();
            if(info == null)
                return;
            CustomTickerStrategy s = (CustomTickerStrategy)Strategy;
            int index = s.StrategyInfo.Tickers.IndexOf(info);
            if(index < 1)
                return;
            index--;
            s.StrategyInfo.Tickers.Remove(info);
            s.StrategyInfo.Tickers.Insert(index, info);
            this.gridView1.RefreshData();
        }

        private void biMoveDown_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TickerInputInfo info = (TickerInputInfo)this.gridView1.GetFocusedRow();
            if(info == null)
                return;
            CustomTickerStrategy s = (CustomTickerStrategy)Strategy;
            int index = s.StrategyInfo.Tickers.IndexOf(info);
            if(index > s.StrategyInfo.Tickers.Count - 2)
                return;
            index++;
            s.StrategyInfo.Tickers.Remove(info);
            s.StrategyInfo.Tickers.Insert(index, info);
            this.gridView1.RefreshData();
        }

        private void reFileEditorButton_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            PropertyDescriptor desc = this.propertyGridControl1.GetPropertyDescriptor(this.propertyGridControl1.FocusedRow);
            FileNameAttribute attr = GetFileNameAttribute(desc);
            if(attr == null)
                throw new Exception("FileAttribute == null");
            if(e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis) {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Open Simulation File";
                dlg.Filter = attr.FilterString;
                if(dlg.ShowDialog() == DialogResult.OK) {
                    this.propertyGridControl1.SetCellValue(this.propertyGridControl1.FocusedRow, 0, dlg.FileName);
                    this.propertyGridControl1.CloseEditor();
                }
            }
        }

        private FileNameAttribute GetFileNameAttribute(PropertyDescriptor desc) {
            foreach(var attr in desc.Attributes) {
                if(attr is FileNameAttribute)
                    return (FileNameAttribute)attr;
            }
            return null;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e) {
            if(e.Column == this.colExchange) {
                TickerInputInfo info = (TickerInputInfo)this.gridView1.GetRow(e.RowHandle);
                if(info == null)
                    return;
                info.TickerName = "";
                info.KlineIntervalMin = 0;
                this.gridView1.RefreshRow(e.RowHandle);
            }
        }

        private void repositoryItemLookUpEdit1_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e) {
            
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e) {
            if(e.Column != this.colKlineIntervalMin || e.ListSourceRowIndex == GridControl.InvalidRowHandle)
                return;
            var ticker = Strategy.StrategyInfo.Tickers[e.ListSourceRowIndex];
            var cdata = Exchange.Get(ticker.Exchange).GetAllowedCandleStickIntervals().FirstOrDefault(i => i.TotalMinutes == (int)e.Value);
            if(cdata != null)
                e.DisplayText = cdata.Text;
        }

        private void rpiExchanges_EditValueChanged(object sender, EventArgs e) {
            if(this.gridView1.ActiveEditor != null)
                this.gridView1.CloseEditor();
        }
    }
}
