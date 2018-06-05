﻿using System;
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
        ProductService productService = new ProductService();

        public ListView listView { get; set; }

        //SalesService salesService = new SalesService();
        //quantity inputed validations 
        //if input qty is valid then either update qty or add row
        public void addToCart()
        {

            if (stock <= 0)
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

            var computedQty = getQtyInCart(inputedQuantity);

            if (stock < computedQty || inputedQuantity > computedQty)
            {
                helper.dimEnabled(true);
                MessageBox.Show("There is no enough stock!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                helper.dimEnabled(false);
                return;
            }

            double subTotal = price * inputedQuantity;

            updateCartItem(inputedQuantity, subTotal);
        }

        //minus quantity and subtotal into listview Cart 
        public void removeToCart()
        {
            var inputedQuantity = getQtyFromInputBox("REMOVE QUANTITY");

            if (inputedQuantity > quantity)
            {
                helper.dimEnabled(true);
                MessageBox.Show("Quantity remove is higher!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                helper.dimEnabled(false);
                return;
            }

            var newSubtotal = inputedQuantity * price;

            updateCartItem(-inputedQuantity, -newSubtotal);
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
            string[] inputs = { cash.ToString(), total.ToString() };

            if (helper.isEmpty(inputs))
            {
                
                return 0.00;
            }

            var _total = double.Parse(total.ToString());
            var _cash = double.Parse(cash.ToString());

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
            string[] inputs = { cash.ToString(), total.ToString() };
            if (helper.isEmpty(inputs))
            {
                return Color.Crimson;
            }

            var _total = double.Parse(total.ToString());
            var _cash = double.Parse(cash.ToString());

            if (_total > _cash)
            {
                return Color.Crimson;
            }
            else
            {
                return Color.Lime;
            }
        }

        public bool hasPurchased()
        { 
            var count = listView.Items.Count;

            if (double.Parse(cash.ToString()) == 0 || count == 0)
            {
                return false;
            }

            if (confirmProceed())
            {
                return false;
            }

            saveSales();

            deductStock();

            helper.dimEnabled(false);

            return true;
        }

        //private methods
      

        private void deductStock()
        {
             var count = listView.Items.Count;
             for (int i = 0; i < count; i++)
             {
                 
                 productService.code = listView.Items[i].SubItems[0].Text;
                 var reader = productService.readProducts(ProductAction.GetStock);

                 var stock = 0;

                 reader.Read();

                 if (reader.HasRows)
                 {
                     stock = int.Parse(reader[0].ToString());
                 }

                 productService.stock = stock - int.Parse(listView.Items[i].SubItems[3].Text);
                 productService.executeProducts(ProductAction.DeductStock);
             }
        }


        private bool confirmProceed()
        {
            var description = "ORNo: " + orNum + "\nTotal: " + total + "\nCash: " + (total + change).ToString() + "\nChange: " + change;

            helper.dimEnabled(true);
            var confirm = MessageBox.Show("Proceed? \n" + description, "Ice Cream Shop", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
           
            if (DialogResult.Yes == confirm)
            {
               
                return false;
            }

            helper.dimEnabled(false);
            return true;
        }

       
        //show input box then return tha value inputed
        private void saveSales()
        {
            var count = listView.Items.Count; 

            var date = new DateTime();

            var currentDate = DateTime.Parse(date.TimeOfDay.ToString());

            for (int i = 0; i < count; i++)
            {
                
                code           = listView.Items[i].SubItems[0].Text;
                price          = double.Parse(listView.Items[i].SubItems[2].Text);
                quantity       = int.Parse(listView.Items[i].SubItems[3].Text);
                subTotal       = double.Parse(listView.Items[i].SubItems[4].Text);
                datePurchased  = currentDate;

                executeSales(SalesAction.SaveSales);
            }
        }


        private int getQtyFromInputBox(string title)
        {
            var inputBox = new CustomInputBox();
            var description = "Product: " + name + "\nPrice: " + price;
           
            inputBox.Show(1, description, title);
            return inputBox.quantity;
        }

        //if item is not in cart list then return the current stock
        //else get the qty of the selected product in cart list
        //get the sum of qty inputed and qty in cart list then return
        private int getQtyInCart(int _quantity)
        {
            var index = new IndexRow();
            index.Column = 1;
            index.ListView = listView;
            index.Key = code;

            if (index.hasRow())
            {
                var i = index.getListIndex();
                var qty = int.Parse(listView.Items[i].SubItems[3].Text);
                return qty + _quantity;
            }
            else
            {
                return stock;
            }
        }

        //update(add, minus) cart qty and subtotal
        //if product is existed in cart list then update qty and subtotal
        //else add another row to cart list
        private void updateCartItem(int _quantity, double _subTotal)
        {
            var index = new IndexRow();
            index.Column = 1;
            index.ListView = listView;
            index.Key = code;

            if (index.hasRow())
            {
                var i = index.getListIndex();

                var newQty = (int.Parse(listView.Items[i].SubItems[3].Text) + _quantity).ToString();
                var newSubTotal = (double.Parse(listView.Items[i].SubItems[4].Text) + _subTotal).ToString();

                listView.Items[i].SubItems[3].Text = newQty;
                listView.Items[i].SubItems[4].Text = newSubTotal;

                if (int.Parse(listView.Items[i].SubItems[3].Text) == 0)
                {
                    listView.Items.RemoveAt(i);
                }
            }
            else
            {
                string[] row = { 
                      code, 
                      name, 
                      price.ToString(),
                      _quantity.ToString(), 
                      _subTotal.ToString() 
                };

                var listViewItem = new ListViewItem(row);
                listView.Items.Add(listViewItem);
            }
        }


        //dead code

        private double getChange()
        {
            var change = double.Parse(cash.ToString()) - double.Parse(total.ToString());

            if (change < 0 || double.Parse(total.ToString()) == 0)
            {
                return 0;
                //txtCash.Text = "0.00";
            }
            return change;
        }

       private int getStock()
        {
            var reader = productService.readProducts(ProductAction.GetStock);

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
