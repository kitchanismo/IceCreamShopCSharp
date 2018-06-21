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
        //IProduct _product = new Product();

        ProductService productService = new ProductService(new Product());

        SalesService salesService = new SalesService(new Product());
        Helper helper                 = new Helper();

        public POS()
        {
            InitializeComponent();
        }

        private void InitializeListView()
        {
            salesService.listView = lvCart;
            productService.listView = lvProduct;
        }

        private void POSForm_Load(object sender, EventArgs e)
        {
            InitializeListView();
            productService.read();
            lblOR.Text = salesService.generateNewOR();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            productService.itemName = txtSearch.Text.ToLower();
            productService.code     = txtSearch.Text.ToLower();
            productService.category = txtSearch.Text.ToLower();
            productService.search();
        }

        private void lvProduct_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
            foreach (ListViewItem item in lvProduct.SelectedItems)
            {
                salesService.code     = item.SubItems[0].Text;
                salesService.category = item.SubItems[1].Text;
                salesService.itemName = item.SubItems[2].Text;
                salesService.price    = double.Parse(item.SubItems[3].Text);
                salesService.stock    = int.Parse(item.SubItems[4].Text);
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
                salesService.code     = item.SubItems[0].Text;
                salesService.itemName = item.SubItems[1].Text;
                salesService.price    = double.Parse(item.SubItems[2].Text);
                salesService.quantity = int.Parse(item.SubItems[3].Text);
            }

            salesService.removeToCart();

            displayTotal();
            displayItemsNo();
            displayChange(salesService);
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {

            salesService.customerName  = txtCustomer.Text;
            salesService.ORno          = lblOR.Text;
            salesService.cash          = double.Parse(txtCash.Text);
            salesService.change        = double.Parse(lblChange.Text);
            salesService.total         = double.Parse(lblTotal.Text);

            if (salesService.hasPurchased())
            {
                Helper.Notification("Successfully Purchased!", Notify.Success);
                resetTransaction();
            }
        
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            salesService.total  = double.Parse(lblTotal.Text);
            salesService.cash = (txtCash.Text == "") ? 0 : double.Parse(txtCash.Text);

            lblChange.Text      = salesService.computeChange().ToString("N");
            txtCash.ForeColor   = salesService.changeCashForeColor();
            btnPurchase.Enabled = helper.isEnabled(txtCash.ForeColor);
        }

        private void txtCash_Keypress(object sender, KeyPressEventArgs e)
        {
            helper.doNumbersSinglePointOnly(sender, e);
        }

        //local methods

        private void resetTransaction()
        {
           txtCash.Text     = "0.00";
           lblChange.Text   = "0.00";
           lblTotal.Text    = "0.00";
           lblNoItems.Text  = "0";
           txtCustomer.Text = "Walk-In";
           lblOR.Text       = salesService.generateNewOR();

           lvCart.Items.Clear();
           productService.read();
        }

        private void displayTotal()
        {
            var total = helper.getColumnSum(lvCart, 5).ToString("N");
            lblTotal.Text = total;
        }

        private void displayChange(SalesService _salesService)
        {
            var change = (txtCash.Text == "") ? 0 : double.Parse(txtCash.Text) - double.Parse(lblTotal.Text);

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
            Helper.dimEnabled(true);
            if (lvCart.Items.Count == 0)
            {
                Helper.dimEnabled(false);
                return;
            }
            
            var confirm = MessageBox.Show("Cancel Transaction?", "Ice Cream Shop", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (DialogResult.Yes == confirm)
            {
                resetTransaction();
            }

            Helper.dimEnabled(false);

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            productService.read();
        }


    }
}
