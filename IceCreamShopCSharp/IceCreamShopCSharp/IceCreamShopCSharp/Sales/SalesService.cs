using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using kitchanismo;

namespace IceCreamShopCSharp
{
    class SalesService : Product
    {
        SharedGlobal shared = new SharedGlobal();
          
        //quantity inputed validations 
        //if input qty is valid then either update qty or add row
        public void addCart(ListView lv)
        {

            if (productStock <= 0)
            {
                shared.dimEnabled(true);
                MessageBox.Show("Out of Stock!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                shared.dimEnabled(false);
                return;
            }

            var _inputedQuantity = getQtyFromInputBox();

            if (_inputedQuantity == 0)
            {
                return;
            }

            var computedQty = getQtyInCart(lv, _inputedQuantity);

            if (productStock < computedQty || _inputedQuantity > computedQty)
            {
                shared.dimEnabled(true);
                MessageBox.Show("There is no enough stock! \n\nQuantity ordered: " + productStock.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                shared.dimEnabled(false);
                return;
            }

            double subTotal = productPrice * _inputedQuantity;

            updateCartItem(lv, _inputedQuantity, subTotal);
        }

        //minus quantity and subtotal into listview Cart 
        public void returnProduct(ListView lv)
        {
            var _inputedQuantity = getQtyFromInputBox();

            if (_inputedQuantity > productQuantity)
            {
                shared.dimEnabled(true);
                MessageBox.Show("Quantity return is more than quantity ordered!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                shared.dimEnabled(false);
                return;
            }

            var newSubtotal = _inputedQuantity * productPrice;

            updateCartItem(lv, -_inputedQuantity, -newSubtotal);
        }

//private methods

        //show input box then return tha value inputed
        private int getQtyFromInputBox()
        {
            var inputBox = new CustomInputBox();
            var description = "Product: " + productName + "\nPrice: " + productPrice;
           
            inputBox.Show(1, description);
            return inputBox.quantity;
        }

        //if item is not in cart list then return the current stock
        //else get the qty of the selected product in cart list
        //get the sum of qty inputed and qty in cart list then return
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

        //update(add, minus) cart qty and subtotal
        //if product is existed in cart list then update qty and subtotal
        //else add another row to cart list
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

        //dead code
        public int getStock()
        {
            var reader = readProducts(ProductAction.GetStock);

            reader.Read();
            if (reader.HasRows)
            {
                return int.Parse(reader[0].ToString());
            }
            else
            {
                return 0;
            }
        }

    }
}
