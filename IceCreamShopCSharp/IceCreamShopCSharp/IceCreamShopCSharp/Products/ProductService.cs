using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;    

namespace IceCreamShopCSharp
{
    class ProductService : Connection
    {
       
        public string productCode { get; set; }
        public string productCategory { get; set; }
        public string productName { get; set; }
        public double productPrice { get; set; }
        public int productStock { get; set; }
        public DateTime productPurchased { get; set; }

        protected OleDbDataReader readProducts(Product product)
        {  
            switch (product)
            {
                case Product.Load:
                    query = loadQuery();
                    break;
                case Product.Search:
                    query = searchQuery();
                    break;
                case Product.Insert:
                    query = insertQuery();
                    break;
            }
            return CommandReader(query);
        }

        private string searchQuery()
        {
            var key = "'%" + productName.ToLower() + "%'";
            return "select * from tblproducts where productName like " + key + " order by id";
        }

        private string loadQuery()
        {
            return "select * from tblproducts order by id";
        }

        private string insertQuery()
        {
            var _query = "insert into tblproducts (productCode,productCategory,productName,productPrice,productStock,datePurchased)";
            var _values = " values ('" + productCode + "','" + productCategory + "','" + productName + "'," + productPrice + "," + productStock + "," + productPurchased + ")";
            return _query + _values;
        }

 
    }
    enum Product
    {
        Load,
        Search,
        Insert
    }
}
