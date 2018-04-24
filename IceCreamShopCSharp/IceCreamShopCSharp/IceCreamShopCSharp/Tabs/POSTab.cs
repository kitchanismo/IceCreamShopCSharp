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
        SalesService salesService = new SalesService();

        public POSTab()
        {
            InitializeComponent();
        }

        private void POSForm_Load(object sender, EventArgs e)
        {
            productService.read(lvProduct);
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
                salesService.productCode = item.SubItems[0].Text;
                salesService.productCategory = item.SubItems[1].Text;
                salesService.productName = item.SubItems[2].Text;
                salesService.productPrice = double.Parse(item.SubItems[3].Text);
                salesService.productStock = int.Parse(item.SubItems[4].Text);
            } 
            salesService.addCart(lvCart);
        }

        private void lvCart_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
            foreach (ListViewItem item in lvCart.SelectedItems)
            {
                salesService.productCode = item.SubItems[0].Text;
                salesService.productName = item.SubItems[1].Text;
                salesService.productPrice = double.Parse(item.SubItems[2].Text);
                salesService.productQuantity = int.Parse(item.SubItems[3].Text);
            }

            salesService.returnProduct(lvCart);
        }

    }
}
