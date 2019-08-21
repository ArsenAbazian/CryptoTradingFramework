using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public partial class CalculatorForm : XtraForm {
        public CalculatorForm() {
            InitializeComponent();
        }

        public double BuyPrice {
            get { return Convert.ToDouble(this.tePrice.EditValue); }
            set { this.tePrice.EditValue = value; }
        }

        public double SellPrice {
            get { return Convert.ToDouble(this.teSellPrice.EditValue); }
            set { this.teSellPrice.EditValue = value; }
        }

        public double Amount {
            get { return Convert.ToDouble(this.teAmount.EditValue); }
            set { this.teAmount.EditValue = value; }
        }

        public double UsdRate { get; set; }

        private void OnInputValueChanged(object sender, EventArgs e) {
            double buyPrice = string.IsNullOrEmpty(this.tePrice.Text.Trim()) ? 0: Convert.ToDouble(this.tePrice.EditValue);
            double sellPrice = string.IsNullOrEmpty(this.teSellPrice.Text.Trim())? 0: Convert.ToDouble(this.teSellPrice.EditValue);
            double amount = string.IsNullOrEmpty(this.teAmount.Text.Trim())? 0: Convert.ToDouble(this.teAmount.EditValue);
            if(buyPrice == 0) {
                this.teMinimalSpread.Text = "";
                this.teSpread.Text = "";
                this.teProfit.Text = "";
                this.teUsdProfit.Text = "";
                this.teMinimalSell.Text = "";
                return;
            }
            if(amount == 0 || sellPrice == 0) {
                this.teSpread.Text = "";
                this.teProfit.Text = "";
                this.teUsdProfit.Text = "";
            }
            if(amount == 0) {
                this.teProfit.Text = "";
                this.teUsdProfit.Text = "";
            }
            double volume = buyPrice * amount;
            double fee = buyPrice * (0.25 / 100);
            double sellFee = sellPrice == 0 ? buyPrice : sellPrice;
            fee += sellFee * (0.25 / 100);
            this.teSpread.EditValue = sellPrice - buyPrice;
            this.teMinimalSell.EditValue = buyPrice + fee;

            double minSpread = fee;
            this.teMinimalSpread.EditValue = minSpread;
            if(buyPrice != 0 && sellPrice != 0 && amount != 0) {
                double profit = (sellPrice - buyPrice) * amount - fee;
                this.teProfit.Text = Convert.ToString(profit);
                this.teUsdProfit.Text = Convert.ToString(profit * UsdRate);
            }
        }
    }
}
