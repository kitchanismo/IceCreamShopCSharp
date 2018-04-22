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
        public void read(ListView lv)
        {
            var reader = readProducts(ProductAction.Load);
            mapItems(reader, lv);
        }

        public void search(ListView lv)
        {
            var reader = readProducts(ProductAction.Search);
            mapItems(reader, lv);
        }

        private void mapItems(OleDbDataReader _reader, ListView lv)
        {
            var cellStart = 1;

            lv.Items.Clear();

            while (_reader.Read())
            {
                ListViewItem with_1 = lv.Items.Add((_reader[cellStart]).ToString());

                var len = lv.Columns.Count;

                for (int i = cellStart + 1; i < len + cellStart; i++)
                {
                    with_1.SubItems.Add(_reader[i].ToString());
                }
            }

            var shared = new Shared();
            shared.doChangeForeColor(lv);
        }
    }
}
