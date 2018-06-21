﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;

namespace IceCreamShopCSharp
{
    class ProductService: Product 
    {
        Helper helper = new Helper();

        public ListView listView { get; set; }

        public bool hasAction { get; set; }

        public void read()
        {
            addToListView(GetProducts());
        }
     
        public void search()
        {
            addToListView(GetSearchProducts());
        }

        private void addToListView(List<Product> products)
        {

            listView.Items.Clear();

            for (int i = 0; i < products.Count; i++)
            {
                var item = listView.Items.Add(products[i].code);

                item.SubItems.Add(products[i].category);
                item.SubItems.Add(products[i].itemName);
                item.SubItems.Add(products[i].price.ToString());
                item.SubItems.Add(products[i].stock.ToString());
                if (hasAction == true)
                {
                    item.SubItems.Add("Edit");
                    item.SubItems.Add("Delete");
                }
            }

            if (hasAction == true)
            {
                changeToColumnColor(5, Color.Gold);
                changeToColumnColor(6, Color.Crimson);
            }
            else
            {
                helper.doChangeForeColor(listView);
            }
        }


        public void Insert()
        {
            //validations
            var date = new DateTime();
            var currentDate = DateTime.Parse(date.TimeOfDay.ToString());
            datePurchased = currentDate;
            SaveProduct();
            Helper.dimEnabled(true);
            Helper.Notification("Save Product!", Notify.Success);
        }

        private void changeToColumnColor(int ColumnIndex, Color color)
        {
            foreach (ListViewItem item in listView.Items)
            {
                item.SubItems[ColumnIndex].Font = new Font("Century Gothic", 12, FontStyle.Regular);
                item.SubItems[ColumnIndex].ForeColor = color;
                item.UseItemStyleForSubItems = false;
            }
        }

        public void ClickAction(MouseEventArgs e)
        {
            var hit = listView.HitTest(e.Location);
            var item = listView.SelectedItems;

            if (hit.SubItem != null && hit.SubItem == hit.Item.SubItems[5])
            {
                MessageBox.Show(item[0].SubItems[5].Text);
            }

            if (hit.SubItem != null && hit.SubItem == hit.Item.SubItems[6])
            {
                MessageBox.Show(item[0].SubItems[6].Text);
                item[0].SubItems.Clear();
            }

            listView.SelectedItems.Clear();
        }

        public void changeMouseCursor(MouseEventArgs e)
        {
            var hit = listView.HitTest(e.Location);
            if ((hit.SubItem != null && hit.SubItem == hit.Item.SubItems[5]) || (hit.SubItem != null && hit.SubItem == hit.Item.SubItems[6]))
            {
                listView.Cursor = Cursors.Hand;
            }
            else
            {
                listView.Cursor = Cursors.Default;
            }
        }
    }
}