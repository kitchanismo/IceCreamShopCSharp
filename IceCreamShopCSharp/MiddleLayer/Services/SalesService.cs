using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;
using DataAccessLayer;

namespace MiddleLayer
{
    public class SalesService : ISalesService
    {
        ISales _sales;
      
        public ListView listView { get; set; }
       
        public void AddToCart(ISales sales)
        {
            _sales = sales;

            if (_sales.stock <= 0)
            {
                throw new Exception("Out of Stock!");
            }

            var inputedQuantity = getQtyFromInputBox("ADD QUANTITY");

            if (inputedQuantity == 0)
            {
                return;
            }

            var computedQty = getQtyInCart(inputedQuantity);

            if (_sales.stock < computedQty || inputedQuantity > computedQty)
            {
                throw new Exception("There is no enough stock!");
            }

            var subTotal = _sales.price * inputedQuantity;

            updateCartItem(inputedQuantity, subTotal);
        }


        public void RemoveToCart(ISales sales)
        {
            _sales = sales;

            var inputedQuantity = getQtyFromInputBox("REMOVE QUANTITY");

            if (inputedQuantity > _sales.quantity)
            {
                throw new Exception("Quantity remove is higher!");
            }

            var newSubtotal = inputedQuantity * _sales.price;

            updateCartItem(-inputedQuantity, -newSubtotal);
        }

        public string GenerateNewOR(ISales sales)
        {
            _sales = sales;
            var ORno = 0;
            try
            {
                ORno = int.Parse(_sales.GetMaxOR().Substring(3)) + 1;
                return "OR-" + ORno;
            }
            catch
            {
                return "OR-1000";
            } 
        }

        public double ComputeChange()
        {
            string[] inputs = { _sales.cash.ToString(), _sales.total.ToString() };

            if (isEmpty(inputs))
            {
                
                return 0.00;
            }

            var _total = double.Parse(_sales.total.ToString());
            var _cash = double.Parse(_sales.cash.ToString());

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

        public Color ChangeCashForeColor()
        {
            string[] inputs = { _sales.cash.ToString(), _sales.total.ToString() };
            
            if (isEmpty(inputs))
            {
                return Color.Crimson;
            }

            var _total = double.Parse(_sales.total.ToString());
            var _cash = double.Parse(_sales.cash.ToString());

            if (_total > _cash)
            {
                return Color.Crimson;
            }
            else
            {
                return Color.Lime;
            }
        }

        public bool HasPurchased(ISales sales)
        {
            _sales = sales;

            saveSales();
            mapStock();

            return true;
        }

        //private methods 

        private bool isEmpty(string[] str)
        {
            foreach (string _str in str)
            {
                if (_str == "" || _str == null)
                {
                    return true;
                }
                else if (double.Parse(_str) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;

        }

 
        private void mapStock()
        {
            var count = listView.Items.Count;
            for (int i = 0; i < count; i++)
            {
                _sales.code = listView.Items[i].SubItems[0].Text;
                _sales.stock = _sales.GetStock() - int.Parse(listView.Items[i].SubItems[3].Text);
                _sales.DeductStock();
            }
        }

      
        private void saveSales()
        {
            var date = new DateTime();
            var currentDate = DateTime.Parse(date.TimeOfDay.ToString());

            var count = listView.Items.Count; 
            for (int i = 0; i < count; i++)
            {
                _sales.code          = listView.Items[i].SubItems[0].Text;
                _sales.price         = double.Parse(listView.Items[i].SubItems[2].Text);
                _sales.quantity      = int.Parse(listView.Items[i].SubItems[3].Text);
                _sales.subTotal      = double.Parse(listView.Items[i].SubItems[4].Text);
                _sales.datePurchased = currentDate;
                _sales.SaveSales();
            }
        }

        private int getQtyFromInputBox(string title)
        {
            CustomInputBox _inputBox = new CustomInputBox();
            var description = "Product: " + _sales.itemName + "\nPrice: " + _sales.price;
            _inputBox.Show(1, description, title);
            return _inputBox.quantity;
        }


        private int getQtyInCart(int _quantity)
        {
            var i = getListIndex(0, _sales.code);

            if (i != -1)
            {
                var qty = int.Parse(listView.Items[i].SubItems[3].Text);
                return qty + _quantity;
            }
            else
            {
                return (int)_sales.stock;
            }
        }

        private int getListIndex(int i, string key)
        {
            int ctr = 0;
            while (ctr < listView.Items.Count)
	        {
	             if (listView.Items[ctr].SubItems[i].Text.ToLower() == key.ToLower())
                 {
                     return ctr;
                 }
                ctr++;
	        }
            return -1;
        }

        private void updateCartItem(int _quantity, double _subTotal)
        {
            var i = getListIndex(0, _sales.code);

            if (i != -1)
            {
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
                      _sales.code, 
                      _sales.itemName, 
                      _sales.price.ToString(),
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
            var change = double.Parse(_sales.cash.ToString()) - double.Parse(_sales.total.ToString());

            if (change < 0 || double.Parse(_sales.total.ToString()) == 0)
            {
                return 0;
            }
            return change;
        }

    }
}
