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
    public partial class Inventory : UserControl
    {
        ProductService productService = new ProductService();

        public Inventory()
        {
            InitializeComponent();
            productService.listView = lvProduct;
            productService.hasAction = true;
        }

        private void POSForm_Load(object sender, EventArgs e)
        {
            productService.read();

         
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            productService.read();

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            productService.itemName = txtSearch.Text.ToLower();
            productService.code     = txtSearch.Text.ToLower();
            productService.category = txtSearch.Text.ToLower();

            productService.search();

        }


        private void lvProduct_MouseMove(object sender, MouseEventArgs e)
        {
            productService.changeMouseCursor(e);
        }

        private void lvProduct_MouseUp(object sender, MouseEventArgs e)
        {
            productService.ClickAction(e);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var addForm = new AddProduct();
            Helper.dimEnabled(true);
            addForm.ShowDialog();
            Helper.dimEnabled(false);
        }

    }
}
