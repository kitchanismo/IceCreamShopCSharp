using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kitchanismo;
using MiddleLayer;
using DataAccessLayer;

namespace IceCreamShopCSharp
{
    public partial class AddProduct : Form
    {
        KitchanismoAnimation animation = new KitchanismoAnimation();
        IProductService productService = new ProductService();
               
        public AddProduct()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelHeader_MouseMove(object sender, MouseEventArgs e)
        {
            animation.Move(this);
        }

        private void panelHeader_MouseUp(object sender, MouseEventArgs e)
        {
            animation.Release();
        }

        private void panelHeader_MouseDown(object sender, MouseEventArgs e)
        {
            animation.Grab(this);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            IProduct product = new Product();
                product.Code = txtCode.Text;
                product.Category = cboCategory.Text;
                product.ItemName = txtName.Text;
                product.Price = double.Parse(txtPrice.Text);
                product.Stock = int.Parse(txtStock.Text);

                productService.Insert(product);
                MessageBox.Show("Save Product!");
        }
    }
}
