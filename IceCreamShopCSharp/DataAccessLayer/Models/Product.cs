using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;  
using System.Windows.Forms;
using System.Threading.Tasks;
using SqlHelper;

namespace DataAccessLayer
{
    public class Product : IProduct
    {
        public string    Code { get; set; }
        public string    Category { get; set; }
        public string    ItemName { get; set; }
        public double    Price { get; set; }
        public double    Stock { get; set; }
        public DateTime  DatePurchased { get; set; }

        CRUD crud = new CRUD(Database.Connection());

        public List<Product> GetProducts()
        {
            return crud.Query<Product>("select * from product");
        }

        public virtual void Save()
        {
            var sql = "insert into product (code,category,itemName,price,stock,datePurchased) values (@code,@category,@itemName,@price,@stock,@datePurchased)";
            var parameters = new Dictionary<string, object>();
            parameters.Add("@code", Code);
            parameters.Add("@category", Category);
            parameters.Add("@itemName", ItemName);
            parameters.Add("@price", Price);
            parameters.Add("@stock", Stock);
            parameters.Add("@datePurchased", DatePurchased);

            crud.Query<Product>(sql, parameters);
        }

        public List<Product> GetSearchProducts()
        {
            var searchKeys = new Dictionary<string, object>();
                searchKeys.Add("code",     "%" + Code + "%");
                searchKeys.Add("category", "%" + Category + "%");
                searchKeys.Add("itemName", "%" + ItemName + "%");

            return crud.GetAll<Product>(searchKeys);
        }

        public void DeductStock()
        {
            var sql = "update product SET stock = @stock where code = @code";
            var parameters = new Dictionary<string, object>();
                parameters.Add("@stock", Stock);
                parameters.Add("@code",  Code);
            crud.Query<Product>(sql, parameters);
        }
        
        public  int GetStock()
        {
            var findKey = new Dictionary<string, object>();
            findKey.Add("code", Code);

            Product product = crud.GetOne<Product>(findKey);
            return (int) product.Stock;
        }


        
    }

}
