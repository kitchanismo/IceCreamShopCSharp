using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using kitchanismo;
using System.Drawing;

namespace IceCreamShopCSharp
{
    class SalesService : Sales
    {
        Helper helper = new Helper();
          
        //quantity inputed validations 
        //if input qty is valid then either update qty or add row
        public void addToCart(ListView lv)
        {

            if (productStock <= 0)
            {
                helper.dimEnabled(true);
                MessageBox.Show("Out of Stock!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                helper.dimEnabled(false);
                return;
            }

            var inputedQuantity = getQtyFromInputBox("ADD QUANTITY");

            if (inputedQuantity == 0)
            {
                return;
            }

            var computedQty = getQtyInCart(lv, inputedQuantity);

            if (productStock < computedQty || inputedQuantity > computedQty)
            {
                helper.dimEnabled(true);
                MessageBox.Show("There is no enough stock!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                helper.dimEnabled(false);
                return;
            }

            double subTotal = productPrice * inputedQuantity;

            updateCartItem(lv, inputedQuantity, subTotal);
        }

        //minus quantity and subtotal into listview Cart 
        public void removeProductToCart(ListView lv)
        {
            var inputedQuantity = getQtyFromInputBox("REMOVE QUANTITY");

            if (inputedQuantity > productQuantity)
            {
                helper.dimEnabled(true);
                MessageBox.Show("Quantity remove is higher!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                helper.dimEnabled(false);
                return;
            }

            var newSubtotal = inputedQuantity * productPrice;

            updateCartItem(lv, -inputedQuantity, -newSubtotal);
        }


        public string generateNewOR()
        {
            var ORno = "";
            var reader = readSales(SalesAction.ReadOR);
            reader.Read();
            try 
	        {	        
		         ORno = (int.Parse(reader[0].ToString().Substring(3)) + 1).ToString();
                 return "OR-" + ORno; 
	        }
	        catch
	        {
		        return "OR-1000";
	        } 
        }

        public double computeChange()
        {
            string[] inputs = { cash, total };

            if (helper.IsEmpty(inputs))
            {
                
                return 0.00;
            }

            var _total = double.Parse(total);
            var _cash = double.Parse(cash);

            if (_total > _cash || _total == 0)
            {
                return 0.00;
            }
            else
            {
                var change = _cash - _total;
                return change;
            }
        }

        public Color changeCashForeColor()
        {
            string[] inputs = { cash, total };
            if (helper.IsEmpty(inputs))
            {
                return Color.Crimson;
            }
            
            var _total = double.Parse(total);
            var _cash = double.Parse(cash);

            if (_total > _cash)
            {
                return Color.Crimson;
            }
            else
            {
                return Color.Lime;
            }
        }


        //private methods
       

        //show input box then return tha value inputed
        private int getQtyFromInputBox(string title)
        {
            var inputBox = new CustomInputBox();
            var description = "Product: " + productName + "\nPrice: " + productPrice;
           
            inputBox.Show(1, description, title);
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
