using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;  
using System.Windows.Forms;
using System.Threading.Tasks;
using SqlHelper;

namespace IceCreamShopCSharp
{
    class Product
    {
        public string    code { get; set; }
        public string    category { get; set; }
        public string    itemName { get; set; }
        public double    price { get; set; }
        public double    stock { get; set; }
        protected DateTime datePurchased { get; set; }

        CRUD crud = new CRUD(Database.Connection());

        public List<Product> GetProducts()
        {
            return crud.GetAll<Product>();
        }

        public void SaveProduct()
        {
            var sql = "insert into product (code,category,itemName,price,stock,datePurchased) values (@code,@category,@itemName,@price,@stock,@datePurchased)";
            var parameters = new Dictionary<string, object>();
            parameters.Add("@code", code);
            parameters.Add("@category", category);
            parameters.Add("@itemName", itemName);
            parameters.Add("@price", price);
            parameters.Add("@stock", stock);
            parameters.Add("@datePurchased", datePurchased);

            crud.Query<Product>(sql, parameters);
        }

        public List<Product> GetSearchProducts()
        {
            var searchKeys = new Dictionary<string, object>();
                searchKeys.Add("code",     "%" + code + "%");
                searchKeys.Add("category", "%" + category + "%");
                searchKeys.Add("itemName", "%" + itemName + "%");

            return crud.GetAll<Product>(searchKeys);
        }

        public void DeductStock()
        {
            var sql = "update product SET stock = @stock where code = @code";
            var parameters = new Dictionary<string, object>();
                parameters.Add("@stock", stock);
                parameters.Add("@code",  code);
            crud.Query<Product>(sql, parameters);
        }
        
        public int GetStock()
        {
            var findKey = new Dictionary<string, object>();
            findKey.Add("code", code);

            Product product = crud.GetOne<Product>(findKey);
            return (int) product.stock;
        }

    }

}
