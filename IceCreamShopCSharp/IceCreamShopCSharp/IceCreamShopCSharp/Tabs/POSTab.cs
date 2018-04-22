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
        Products products = new Products();

        public POSTab()
        {
            InitializeComponent();
        }

        private void POSForm_Load(object sender, EventArgs e)
        {
            products.read(lvProduct);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            products.productName = txtSearch.Text;
            products.search(lvProduct);
        }

        private void lvProduct_MouseDoubleClick(object sender, EventArgs e)
        {
            ListViewItem with = lvProduct.Items[0];
            products.productCode =  with.SubItems[0].Text;
            products.productCategory = with.SubItems[1].Text;
            products.productName = with.SubItems[2].Text;
            products.productPrice = double.Parse(with.SubItems[3].Text);
            products.productStock = int.Parse(with.SubItems[4].Text);
            
        }

    }
}
