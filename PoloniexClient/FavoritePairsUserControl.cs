using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptoMarketClient.Bittrex;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraGrid.Views.Grid;

namespace CryptoMarketClient {
    public partial class FavoritePairsUserControl : UserControl {
        public FavoritePairsUserControl(Exchange exchange) {
            Exchange = exchange;
            InitializeComponent();
        }

        public Exchange Exchange { get; private set; }

        async void FavoritePairsUserControl_Load(object sender, EventArgs e) {
            await Task<bool>.Factory.StartNew(() => Exchange.GetCurrenciesInfo(), TaskCreationOptions.LongRunning);
            searchLookUpEdit1.Properties.DataSource = ((BittrexExchange)Exchange).Currencies;
        }

        void searchLookUpEdit1_Popup(object sender, EventArgs e) {
            PopupSearchLookUpEditForm popupForm = searchLookUpEdit1.GetPopupEditForm();
            popupForm.KeyDown -= OnSearchLookupPopupFormKeyDown;
            popupForm.KeyDown += OnSearchLookupPopupFormKeyDown;
        }

        void OnSearchLookupPopupFormKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode != Keys.Enter)
                return;
            PopupSearchLookUpEditForm popupForm = ((PopupSearchLookUpEditForm)sender);
            GridView view = popupForm.OwnerEdit.Properties.View;
            if (view.RowCount == 1) {
                view.FocusedRowHandle = 0;
                popupForm.OwnerEdit.ClosePopup();
            }
        }

        void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e) {

        }
    }
}
