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
    public partial class Inventory : UserControl
    {

        IProductService productService;
        IProduct product;
 
        public Inventory()
        {
            InitializeComponent();
            productService = KitchanismoDInjector.Container.Resolve<ProductService>();
            product        = KitchanismoDInjector.Container.Resolve<Product>();
        }

        private void POSForm_Load(object sender, EventArgs e)
        {
            productService.listView = lvProduct;
            productService.hasAction = true;
            productService.Read(product);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            productService.Read(product);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            product.ItemName = txtSearch.Text.ToLower();
            product.Code     = txtSearch.Text.ToLower();
            product.Category = txtSearch.Text.ToLower();
           
            productService.Search(product);
        }

        private void lvProduct_MouseMove(object sender, MouseEventArgs e)
        {
            productService.ChangeMouseCursor(e);
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
