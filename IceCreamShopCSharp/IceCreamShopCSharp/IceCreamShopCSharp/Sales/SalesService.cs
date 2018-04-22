using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using kitchanismo;

namespace IceCreamShopCSharp
{
    class SalesService : Products
    {
       
        public void addCart(ListView lv)
        {

            if (productStock <= 0)
            {
                MessageBox.Show("Out of Stock!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var inputBox = new CustomInputBox();
            var description = "Product: " + productName + "\nPrice: " + productPrice;

            inputBox.Show(1, description);

            if (inputBox.quantity == 0)
            {
                return;
            }

            var computedQty = getQtyInCart(lv, inputBox.quantity);

            if (productStock < computedQty || inputBox.quantity > computedQty)
            {
                MessageBox.Show("There is no enough stock!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double subTotal = productPrice * inputBox.quantity;

            updateCartItem(lv, inputBox.quantity, subTotal);
        }

        public void returnProduct(ListView lv)
        {
            var inputBox = new CustomInputBox();
            var description = "Product: " + productName + "\nPrice: " + productPrice;

            inputBox.Show(1, description);

            if (inputBox.quantity > productQuantity)
            {
                MessageBox.Show("Quantity return is more than quantity ordered!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newSubtotal = inputBox.quantity * productPrice;

            updateCartItem(lv, -inputBox.quantity, -newSubtotal);
        }

        //private methods

        private int getQtyInCart(ListView lv, int _quantity)
        {
            var index = new IndexRow();
            index.Column = 1;
            index.ListView = lv;
            index.Key = productCode;

            if (index.hasRow())
            {
                var i = index.getListIndex();
                var qty = int.Parse(lv.Items[i].SubItems[3].Text);
                return qty + _quantity;
            }
            else
            {
                return productStock;
            }
        }

        private void updateCartItem(ListView lv, int _quantity, double _subTotal)
        {
            var index = new IndexRow();
            index.Column = 1;
            index.ListView = lv;
            index.Key = productCode;

            if (index.hasRow())
            {
                var i = index.getListIndex();

                var newQty = (int.Parse(lv.Items[i].SubItems[3].Text) + _quantity).ToString();
                var newSubTotal = (double.Parse(lv.Items[i].SubItems[4].Text) + _subTotal).ToString();

                lv.Items[i].SubItems[3].Text = newQty;
                lv.Items[i].SubItems[4].Text = newSubTotal;

                if (int.Parse(lv.Items[i].SubItems[3].Text) == 0)
                {
                    lv.Items.RemoveAt(i);
                }
            }
            else
            {
                string[] row = { 
                      productCode, 
                      productName, 
                      productPrice.ToString(),
                      _quantity.ToString(), 
                      _subTotal.ToString() 
                };

                var listViewItem = new ListViewItem(row);
                lv.Items.Add(listViewItem);
            }
        }

    }
}
