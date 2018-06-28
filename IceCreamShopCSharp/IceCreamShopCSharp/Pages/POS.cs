using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MiddleLayer;
using DataAccessLayer;

namespace IceCreamShopCSharp
{
    public partial class POS : UserControl
    {
        
        IProductService productService;
        ISalesService salesService;
        ISales sales;
        IProduct product;

        public POS()
        {
            InitializeComponent();

            productService = KitchanismoDInjector.Container.Resolve<ProductService>();
            salesService   = KitchanismoDInjector.Container.Resolve<SalesService>();
            sales          = KitchanismoDInjector.Container.Resolve<Sales>();
            product        = KitchanismoDInjector.Container.Resolve<Product>();
        }

        private void InitializeService()
        {
            salesService.listView = lvCart;
            productService.listView = lvProduct;
            productService.hasAction = false;
        }

        private void POSForm_Load(object sender, EventArgs e)
        {
            InitializeService();
            productService.Read(product);
            lblOR.Text = salesService.GenerateNewOR(new Sales());
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            product.ItemName = txtSearch.Text.ToLower();
            product.Code     = txtSearch.Text.ToLower();
            product.Category = txtSearch.Text.ToLower();
            productService.Search(product);
        }

        private void lvProduct_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                foreach (ListViewItem item in lvProduct.SelectedItems)
                {
                    sales.Code     = item.SubItems[0].Text;
                    sales.Category = item.SubItems[1].Text;
                    sales.ItemName = item.SubItems[2].Text;
                    sales.Price    = double.Parse(item.SubItems[3].Text);
                    sales.Stock    = int.Parse(item.SubItems[4].Text);
                }

                Helper.dimEnabled(true);
                salesService.AddToCart(sales);
                Helper.dimEnabled(false);

                displayTotal();
                displayItemsNo();
                displayChange(salesService);
            }
            catch (Exception ex)
            {
                 Helper.Notification(ex.Message, Notify.Error);
            } 
        
        }

        private void lvCart_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                foreach (ListViewItem item in lvCart.SelectedItems)
                {
                    sales.Code     = item.SubItems[0].Text;
                    sales.ItemName = item.SubItems[1].Text;
                    sales.Price    = double.Parse(item.SubItems[2].Text);
                    sales.Quantity = int.Parse(item.SubItems[3].Text);
                }

                 Helper.dimEnabled(true);
                salesService.RemoveToCart(sales);
                 Helper.dimEnabled(false);

                displayTotal();
                displayItemsNo();
                displayChange(salesService);
            }
            catch (Exception ex)
            {
                 Helper.Notification(ex.Message, Notify.Error);
            } 
        }

        private bool confirmProceed()
        {
            var count = lvCart.Items.Count;

            if (double.Parse(txtCash.Text) == 0 || count == 0)
            {
                return false;
            }

            var description = "ORNo: " + lblOR.Text + "\nTotal: " + lblTotal.Text + "\nCash: " + (double.Parse(lblTotal.Text) + double.Parse(lblChange.Text)).ToString() + "\nChange: " + lblChange.Text;

            Helper.dimEnabled(true);
            var confirm = MessageBox.Show("Proceed? \n" + description, "Ice Cream Shop", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (DialogResult.Yes == confirm)
            {
                return false;
            }

            Helper.dimEnabled(false);
            return true;
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            if (confirmProceed())
            {
                return;
            }

            sales.CustomerName  = txtCustomer.Text;
            sales.ORno          = lblOR.Text;
            sales.Cash          = double.Parse(txtCash.Text);
            sales.Change        = double.Parse(lblChange.Text);
            sales.Total         = double.Parse(lblTotal.Text);

            if (salesService.HasPurchased(sales))
            {
                Helper.Notification("Successfully Purchased!", Notify.Success);
                resetTransaction();
            }
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            sales.Total         = double.Parse(lblTotal.Text);
            sales.Cash          = (txtCash.Text == "") ? 0 : double.Parse(txtCash.Text);
            lblChange.Text      = salesService.ComputeChange().ToString("N");
            txtCash.ForeColor   = salesService.ChangeCashForeColor();
            btnPurchase.Enabled = Helper.isEnabled(txtCash.ForeColor);
        }

        private void txtCash_Keypress(object sender, KeyPressEventArgs e)
        {
            Helper.doNumbersSinglePointOnly(sender, e);
        }

        //local methods

        private void resetTransaction()
        {
          
           txtCash.Text     = "0.00";
           lblChange.Text   = "0.00";
           lblTotal.Text    = "0.00";
           lblNoItems.Text  = "0";
           txtCustomer.Text = "Walk-In";
           lblOR.Text       = salesService.GenerateNewOR(new Sales());

           lvCart.Items.Clear();
           productService.Read(product);
        }

        private void displayTotal()
        {
            var total = Helper.getColumnSum(lvCart, 5).ToString("N");
            lblTotal.Text = total;
        }

        private void displayChange(ISalesService _salesService)
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
            var noItems = Helper.getColumnSum(lvCart, 4).ToString();
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
            productService.Read(product);
        }

    }
}
