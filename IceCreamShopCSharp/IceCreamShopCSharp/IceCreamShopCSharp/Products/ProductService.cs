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
           // var p = new Product();
            var reader = readProducts(ProductAction.Load);
            
            mapItems(reader);

            //changing forecolor of listview
            shared.doChangeForeColor(listView);
        }
     
        public void search()
        {
            //var p = new Product();
            var reader = readProducts(ProductAction.Search);
          
            mapItems(reader);

            shared.doChangeForeColor(listView);
        
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
        }
    }
}
