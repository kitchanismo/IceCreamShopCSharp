using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using kitchanismo;

namespace IceCreamShopCSharp
{
    class ProductService : Products
    {
        SharedGlobal shared = new SharedGlobal();
            
        public void read(ListView lv)
        {
            var reader = readProducts(ProductAction.Load);
            
            mapItems(reader, lv);
            
            //changing forecolor of listview
            shared.doChangeForeColor(lv);
        }

        public void search(ListView lv)
        {
            var reader = readProducts(ProductAction.Search);
            
            mapItems(reader, lv);

            shared.doChangeForeColor(lv);
        }

        //map items from datareader into listview
        private void mapItems(OleDbDataReader _reader, ListView lv)
        {
            var indexStart = 1; //starts at 1 to skip ID(0) to add on listview

            lv.Items.Clear();

            while (_reader.Read())
            {
                ListViewItem with_1 = lv.Items.Add((_reader[indexStart]).ToString());

                var len = lv.Columns.Count;

                for (int i = indexStart + 1; i < len + indexStart; i++)
                {
                    with_1.SubItems.Add(_reader[i].ToString());
                }
            }
        }
    }
}
