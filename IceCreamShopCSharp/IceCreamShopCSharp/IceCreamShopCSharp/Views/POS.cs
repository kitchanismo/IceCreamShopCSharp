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
    public partial class POS : UserControl
    {

        ProductService productService = new ProductService();
        SalesService salesService     = new SalesService();
        Helper helper                 = new Helper();

        public POS()
        {
            InitializeComponent();
            
            InitializeListView();
        }

        private void InitializeListView()
        {
            salesService.listView = lvCart;
            productService.listView = lvProduct;
        }

        private void POSForm_Load(object sender, EventArgs e)
        {
            productService.read();
            lblOR.Text = salesService.generateNewOR();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            productService.productName = txtSearch.Text;
            productService.search();
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

            salesService.addToCart();

            displayTotal();
            displayItemsNo();
            displayChange(salesService);
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

            salesService.removeToCart();

            displayTotal();
            displayItemsNo();
            displayChange(salesService);
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            salesService.total         = lblTotal.Text;
            salesService.cash          = txtCash.Text;
            salesService.customerName  = txtCustomer.Text;
            salesService.ORno          = lblOR.Text;
            salesService.productChange = double.Parse(lblChange.Text);
            salesService.productTotal  = double.Parse(lblTotal.Text);
          
            if (salesService.hasPurchased())
            {
                helper.dimEnabled(true);
                MessageBox.Show("Successfully Purchased!", "Ice Cream Shop", MessageBoxButtons.OK, MessageBoxIcon.Information);
                helper.dimEnabled(false);
                resetForm();
            }
            
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            salesService.total  = lblTotal.Text;
            salesService.cash   = txtCash.Text;

            lblChange.Text      = salesService.computeChange().ToString("N");
            txtCash.ForeColor   = salesService.changeCashForeColor();
            btnPurchase.Enabled = helper.isEnabled(txtCash.ForeColor);
        }

        private void txtCash_Keypress(object sender, KeyPressEventArgs e)
        {
            helper.doNumbersSinglePointOnly(sender, e);
        }

        //local methods

        private void resetForm()
        {
           txtCash.Text     = "0.00";
           lblChange.Text   = "0.00";
           lblTotal.Text    = "0.00";
           lblNoItems.Text  = "0";
           txtCustomer.Text = "Walk-In";
        
           lblOR.Text       = salesService.generateNewOR();
           lvCart.Items.Clear();
           //lvProduct.Items.Clear();

           productService.read();
        }

        private void displayTotal()
        {
            var total = helper.getColumnSum(lvCart, 5).ToString("N");
            lblTotal.Text = total;
        }

        private void displayChange(SalesService _salesService)
        {
            var change = double.Parse(txtCash.Text) - double.Parse(lblTotal.Text);

            if (change < 0 || double.Parse(lblTotal.Text) == 0)
            {
                lblChange.Text = "0.00";
                txtCash.Text = "0.00";
                return;
            }

            lblChange.Text = change.ToString("N");;
        }

        private void displayItemsNo()
        {
            var noItems     = helper.getColumnSum(lvCart, 4).ToString();
            lblNoItems.Text = noItems;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            helper.dimEnabled(true);
            if (lvCart.Items.Count == 0)
            {
                helper.dimEnabled(false);
                return;
            }
            
            var confirm = MessageBox.Show("Cancel Transaction?", "Ice Cream Shop", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (DialogResult.Yes == confirm)
            {
                resetForm();
            }

            helper.dimEnabled(false);

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            productService.read();
        }


    }
}
