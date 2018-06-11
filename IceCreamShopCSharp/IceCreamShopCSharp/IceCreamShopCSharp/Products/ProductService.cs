using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using kitchanismo;

namespace IceCreamShopCSharp
{
    class ProductService: Product 
    {
        Helper shared = new Helper();

        public ListView listView { get; set; }

        public void read()
        {
            var reader = read(ProductAction.Load);
            mapItems(reader);
        }
     
        public void search()
        {
            var reader = read(ProductAction.Search);
            mapItems(reader);
        }

        //map items from datareader into listview
        private void mapItems(OleDbDataReader _reader)
        {
            var indexStart = 1; //starts at 1 to skip ID(0) to add on listview

            listView.Items.Clear();

            while (_reader.Read())
            {
                ListViewItem with_1 = listView.Items.Add((_reader[indexStart]).ToString());

                var len = listView.Columns.Count;

                for (int i = indexStart + 1; i < len + indexStart; i++)
                {
                    with_1.SubItems.Add(_reader[i].ToString());
                }
            }

            shared.doChangeForeColor(listView);
        }
    }
}
