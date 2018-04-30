using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IceCreamShopCSharp
{
    public partial class POSTab : UserControl
    {

        ProductService productService = new ProductService();
        SalesService salesService     = new SalesService();
        Helper helper                 = new Helper();

        public POSTab()
        {
            InitializeComponent();
            this.txtCash.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCash_Keypress);
        }

        private void POSForm_Load(object sender, EventArgs e)
        {
            productService.read(lvProduct);
            lblOR.Text = salesService.generateNewOR();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            productService.productName = txtSearch.Text;
            productService.search(lvProduct);
        }

        private void lvProduct_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
            foreach (ListViewItem item in lvProduct.SelectedItems)
            {
                salesService.productCode     = item.SubItems[0].Text;
                salesService.productCategory = item.SubItems[1].Text;
                salesService.productName     = item.SubItems[2].Text;
                salesService.productPrice    = double.Parse(item.SubItems[3].Text);
                salesService.productStock    = int.Parse(item.SubItems[4].Text);
            } 

            salesService.addToCart(lvCart);

            displayTotal();
            displayItemsNo();
            clearCashChange();
        }

        private void lvCart_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
            foreach (ListViewItem item in lvCart.SelectedItems)
            {
                salesService.productCode     = item.SubItems[0].Text;
                salesService.productName     = item.SubItems[1].Text;
                salesService.productPrice    = double.Parse(item.SubItems[2].Text);
                salesService.productQuantity = int.Parse(item.SubItems[3].Text);
            }

            salesService.removeProductToCart(lvCart);

            displayTotal();
            displayItemsNo();
            clearCashChange();
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {

        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            salesService.total  = lblTotal.Text;
            salesService.cash   = txtCash.Text;

            lblChange.Text      = salesService.computeChange().ToString("N");
            txtCash.ForeColor   = salesService.changeCashForeColor();
            btnPurchase.Enabled = helper.IsEnabled(txtCash.ForeColor);
        }

        private void txtCash_Keypress(object sender, KeyPressEventArgs e)
        {
            helper.NumbersSinglePointOnly(sender, e);
        }

        //local methods

        private void clearCashChange()
        {
           txtCash.Text   = "0.00";
           lblChange.Text = "0.00";
        }

        private void displayTotal()
        {
            var total     = helper.getColumnSum(lvCart, 5).ToString("N");
            lblTotal.Text = total;
        }

        private void displayItemsNo()
        {
            var noItems     = helper.getColumnSum(lvCart, 4).ToString();
            lblNoItems.Text = noItems;
        }


    }
}
