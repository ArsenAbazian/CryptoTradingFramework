using Crypto.Core.Common;
using Crypto.Core.Common.Arbitrages;
using CryptoMarketClient;
using CryptoMarketClient.Strategies;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crypto.Core.Arbitrages.Deriatives {
    public partial class StatisticalArbitrageEditingForm : StrategySpecificConfigurationControlBase {
        public StatisticalArbitrageEditingForm() {
            InitializeComponent();
            InitializeExchangesCombo(this.exchangesCombo.Properties);
            InitializeExchangesCombo(this.rpiExchanges);
            this.FirstExchangeTickersCombo.Assign(this.tickersLookUp.Properties);
            this.tickersLookUp.EditValueChanged += OnSecondTickersEditValueChanged;
            this.tickersLookUp.ButtonClick += SecondTickersComboButtonClick;
            this.FirstExchangeTickersCombo.ButtonClick += FirstExchangeTickersCombo_ButtonClick;
            this.FirstExchangeTickersCombo.CustomDisplayText += this.FirstExchangeTickersCombo_CustomDisplayText;
        }

        void InitializeExchangesCombo(RepositoryItemComboBox properties) {
            foreach(Exchange e in Exchange.Registered) {
                properties.Items.Add(e.Type);
            }
        }

        private void exchangesCombo_EditValueChanged(object sender, EventArgs e) {
            ExchangeType exchangeType = (ExchangeType)((ComboBoxEdit)sender).EditValue;
            StatisticalStrategy.SecondName.Exchange = exchangeType;
            Exchange exchange = Exchange.Get(exchangeType);
            InitializeTickersLookUp(exchange, this.tickersLookUp.Properties);
            if(!string.IsNullOrEmpty(StatisticalStrategy.SecondName.Ticker))
                this.tickersLookUp.EditValue = StatisticalStrategy.SecondName.Ticker;
            else if(exchange.BtcUsdtTicker != null)
                this.tickersLookUp.EditValue = exchange.BtcUsdtTicker.Name;
            else
                this.tickersLookUp.EditValue = null;
        }

        void InitializeTickersLookUp(Exchange e, RepositoryItemGridLookUpEdit properties) {
            properties.DataSource = null;
            if(e == null)
                return;
            if(!ConnectExchange(e))
                properties.DataSource = null;
            else 
                properties.DataSource = e.Tickers;
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

        StatisticalArbitrageStrategy StatisticalStrategy { get { return (StatisticalArbitrageStrategy)Strategy; } }

        protected override void OnStrategyChanged() {
            Exchange second = Exchange.Get(StatisticalStrategy.SecondName.Exchange);
            second.Connect();

            StatisticalStrategy.FirstNames.ForEach(i => Exchange.Get(i.Exchange).Connect());
            this.firstItemsGridControl.DataSource = StatisticalStrategy.FirstNames;
            this.exchangesCombo.EditValue = second.Type;
        }
        
        private void biAddTicker_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            Exchange exchange = GetNonSelectedExchange(StatisticalStrategy.FirstNames);
            if(exchange == null) {
                XtraMessageBox.Show("There is no unusued exchanges left.");
                return;
            }
            TickerNameInfo info = new TickerNameInfo();
            info.Exchange = exchange.Type;
            Exchange.Get(info.Exchange).Connect();
            info.Ticker = GetPreferredTicker(exchange.Type);

            StatisticalStrategy.FirstNames.Add(info);
            this.gridView1.RefreshData();
            UpdateFirstExchangeTickersCombo();
        }
        string GetPreferredTicker(ExchangeType exchange) {
            if(StatisticalStrategy.FirstNames.Count > 0) {
                Ticker t = StatisticalStrategy.FirstNames.First().FindTickerFor(exchange);
                if(t == null)
                    return string.Empty;
                return t.Name;
            }
            return Exchange.Get(exchange).BtcUsdtTicker.Name;
        }

        private void OnSecondTickersEditValueChanged(object sender, EventArgs e) {
            GridLookUpEdit edit = (GridLookUpEdit)sender;
            StatisticalStrategy.SecondName.Ticker = edit.EditValue == null? null: (string)edit.EditValue;
        }

        Exchange GetNonSelectedExchange(List<TickerNameInfo> existingItems) {
            List<Exchange> unused = GetUnusedExchanges(existingItems);
            if(unused.Count == 0)
                return null;
            return unused.First();
        }

        List<Exchange> GetUnusedExchanges(List<TickerNameInfo> existing) {
            List<Exchange> list = new List<Exchange>();
            foreach(Exchange e in Exchange.Registered) {
                if(existing.FirstOrDefault(tt => tt.Exchange == e.Type) == null)
                    list.Add(e);
            }
            return list;
        }
        

        private void gridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e) {
            TickerNameInfo info = (TickerNameInfo)e.Row;
            string text = StatisticalStrategy.ValidateTickerInfo(info);
            e.Valid = string.IsNullOrEmpty(text);
            e.ErrorText = text;
        }

        private void FirstExchangesComboEditValueChanged(object sender, EventArgs e) {
            ComboBoxEdit edit = (ComboBoxEdit)sender;
            Exchange exchange = Exchange.Get((ExchangeType)edit.EditValue);
            UpdateFirstExchangeTickersCombo(exchange);
            this.gridView1.CloseEditor();
            TickerNameInfo info = (TickerNameInfo)this.gridView1.GetFocusedRow();
            info.Exchange = exchange.Type;
            this.gridView1.RefreshRow(this.gridView1.FocusedRowHandle);
        }

        private void SecondTickersComboButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            if(e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph) {
                Exchange exchange = Exchange.Get((ExchangeType)this.exchangesCombo.EditValue);
                if(!ConnectExchange(exchange))
                    this.tickersLookUp.Properties.DataSource = null;
                else 
                    this.tickersLookUp.Properties.DataSource = exchange.Tickers;
            }
        }

        private void FirstExchangeTickersCombo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            if(e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph) {
                UpdateFirstExchangeTickersCombo();
            }
        }
        void UpdateFirstExchangeTickersCombo() {
            UpdateFirstExchangeTickersCombo(Exchange.Get(((TickerNameInfo)this.gridView1.GetFocusedRow()).Exchange));
        }
        void UpdateFirstExchangeTickersCombo(Exchange newExchange) {
            TickerNameInfo info = (TickerNameInfo)this.gridView1.GetFocusedRow();
            if(!ConnectExchange(newExchange)) {
                this.FirstExchangeTickersCombo.DataSource = null;
            }
            else {
                this.FirstExchangeTickersCombo.DataSource = newExchange.Tickers;
                info.Ticker = GetPreferredTicker(newExchange.Type);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) {
            UpdateFirstExchangeTickersCombo(Exchange.Get(((TickerNameInfo)this.gridView1.GetFocusedRow()).Exchange));
        }

        private void FirstExchangeTickersCombo_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e) {
            e.DisplayText = e.Value == null ? "not selected" : e.Value.ToString();
        }
    }
}
