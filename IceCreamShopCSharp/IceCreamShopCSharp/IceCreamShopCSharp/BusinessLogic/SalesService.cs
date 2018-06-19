using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using kitchanismo;
using SqlHelper;
using System.Threading.Tasks;
namespace IceCreamShopCSharp
{
    class SalesService : Sales
    {
        Helper helper                 = new Helper();
        IndexRow index                = new IndexRow();
        ProductService productService = new ProductService();

        public ListView listView { get; set; }

        public void addToCart()
        {

            if (stock <= 0)
            {
                Helper.Notification("Out of Stock!", Notify.Error);
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
                Helper.Notification("There is no enough stock!", Notify.Error);
                return;
            }

            var subTotal = price * inputedQuantity;

            updateCartItem(inputedQuantity, subTotal);
        }

      
        public void removeToCart()
        {
            var inputedQuantity = getQtyFromInputBox("REMOVE QUANTITY");

            if (inputedQuantity > quantity)
            {
                Helper.Notification("Quantity remove is higher!", Notify.Error);
                return;
            }

            var newSubtotal = inputedQuantity * price;

            updateCartItem(-inputedQuantity, -newSubtotal);
        }

        public string generateNewOR()
        {
            var ORno = 0;
            try
            {
                ORno = int.Parse(GetMaxOR().Substring(3)) + 1;
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
            mapStock();

            return true;
        }

        //private methods 
 
        private void mapStock()
        {
            var count = listView.Items.Count;
            for (int i = 0; i < count; i++)
            {
                productService.code  = listView.Items[i].SubItems[0].Text;
                productService.stock = productService.GetStock() - int.Parse(listView.Items[i].SubItems[3].Text);
                productService.DeductStock();
            }
        }

        private bool confirmProceed()
        {
            var description = "ORNo: " + ORno + "\nTotal: " + total + "\nCash: " + (total + change).ToString() + "\nChange: " + change;

            Helper.dimEnabled(true);
            var confirm = MessageBox.Show("Proceed? \n" + description, "Ice Cream Shop", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
           
            if (DialogResult.Yes == confirm)
            {
               
                return false;
            }

            Helper.dimEnabled(false);
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

                code          = listView.Items[i].SubItems[0].Text;
                price         = double.Parse(listView.Items[i].SubItems[2].Text);
                quantity      = int.Parse(listView.Items[i].SubItems[3].Text);
                subTotal      = double.Parse(listView.Items[i].SubItems[4].Text);
                datePurchased = currentDate;
                SaveSales();
            }
        }


        private int getQtyFromInputBox(string title)
        {
            var inputBox = new CustomInputBox();
            var description = "Product: " + itemName + "\nPrice: " + price;
           
            inputBox.Show(1, description, title);
            return inputBox.quantity;
        }

        //if item is not in cart list then return the current stock
        //else get the qty of the selected product in cart list
        //get the sum of qty inputed and qty in cart list then return
        private int getQtyInCart(int _quantity)
        {
         
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
                return (int) stock;
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
                      itemName, 
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

    }
}
