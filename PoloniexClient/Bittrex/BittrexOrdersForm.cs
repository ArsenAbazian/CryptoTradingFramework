using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Bittrex {
    public partial class BittrexOrdersForm : ThreadUpdateForm {
        public BittrexOrdersForm() {
            InitializeComponent();
            this.bittrexOrderInfoBindingSource.DataSource = BittrexModel.Default.Orders;
        }

        protected override int UpdateInervalMs => 3000;
        protected override bool AllowUpdateInactive => true;

        async protected override void OnThreadUpdate() {
            BittrexModel.Default.StartUpdateOrders();
            int index = 0;
            for(index = 0; index < BittrexModel.Default.Markets.Count; index += 4) {
                Task<string> task1 = BittrexModel.Default.GetOpenOrders(BittrexModel.Default.Markets[index + 0]);
                Task<string> task2 = BittrexModel.Default.GetOpenOrders(BittrexModel.Default.Markets[index + 1]);
                Task<string> task3 = BittrexModel.Default.GetOpenOrders(BittrexModel.Default.Markets[index + 2]);
                Task<string> task4 = BittrexModel.Default.GetOpenOrders(BittrexModel.Default.Markets[index + 3]);

                await task1;
                await task2;
                await task3;
                await task4;

                BittrexModel.Default.OnAppendOpenOrders(task1.Result);
                BittrexModel.Default.OnAppendOpenOrders(task2.Result);
                BittrexModel.Default.OnAppendOpenOrders(task3.Result);
                BittrexModel.Default.OnAppendOpenOrders(task4.Result);
            }
            for(; index < BittrexModel.Default.Markets.Count; index++) {
                Task<string> task1 = BittrexModel.Default.GetOpenOrders(BittrexModel.Default.Markets[index + 0]);
                await task1;
                BittrexModel.Default.OnAppendOpenOrders(task1.Result);
            }
            BittrexModel.Default.EndUpdateOrders();
            Invoke(new Action(RefreshGrid));
        }
        void RefreshGrid() {
            this.gridControl1.RefreshDataSource();
        }
    }
}
