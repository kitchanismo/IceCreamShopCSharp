using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace IceCreamShopCSharp
{
    class ProductService: Product 
    {
        Helper helper = new Helper();

        public ListView listView { get; set; }

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
			}

            helper.doChangeForeColor(listView);
        }
    }
}
