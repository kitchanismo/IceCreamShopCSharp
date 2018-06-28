using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;
using DataAccessLayer;

namespace MiddleLayer
{
    public class ProductService : IProductService
    {
        IProduct _product;

        static kitchanismo.ListViewForeColor lvForeColor = new kitchanismo.ListViewForeColor();

        public ListView listView { get; set; }

        public bool hasAction { get; set; }

        public void Read(IProduct product)
        {
            _product = product;
            addToListView(_product.GetProducts());
        }

        public void Search(IProduct product)
        {
            _product = product;
            addToListView(_product.GetSearchProducts());
        }

        public void Insert(IProduct product)
        {
            _product = product;
            //validations
            var date = new DateTime();
            _product.DatePurchased = DateTime.Parse(date.TimeOfDay.ToString());;
            _product.Save();
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

        public void ChangeMouseCursor(MouseEventArgs e)
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

        private void addToListView(List<Product> products)
        {

            listView.Items.Clear();

            for (int i = 0; i < products.Count; i++)
            {
                var item = listView.Items.Add(products[i].Code);

                item.SubItems.Add(products[i].Category);
                item.SubItems.Add(products[i].ItemName);
                item.SubItems.Add(products[i].Price.ToString());
                item.SubItems.Add(products[i].Stock.ToString());
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
                doChangeForeColor(listView);
            }
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

        private static void doChangeForeColor(ListView lv)
        {
            lvForeColor.ListView = lv;
            lvForeColor.Critical = 5;
            lvForeColor.Column = 5;
            lvForeColor.ColorStable = Color.Teal;
            lvForeColor.ColorWarning = Color.DarkOrange;
            lvForeColor.ColorDanger = Color.Crimson;

            lvForeColor.changeListViewForeColor();
        }
    }
}
