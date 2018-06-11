using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;    
using kitchanismo;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace IceCreamShopCSharp
{
    
    class Product : Search
    {

        public string    code { get; set; }
        public string    category { get; set; }
        public string    name { get; set; }
        public double    price { get; set; }
        public int       stock { get; set; }
        private DateTime datePurchased { get; set; }

        private Database<OleDbDataReader> db = new Database<OleDbDataReader>();

        private Dictionary<ProductAction, string> product = new Dictionary<ProductAction, string> { };

        //just return DataReader to Data Services
        public OleDbDataReader read(ProductAction action)
        {
            product.Add(ProductAction.Load, loadQuery());
            product.Add(ProductAction.Search, searchQuery());
            product.Add(ProductAction.GetStock, getStockQuery());

            db.query = product[action];
            product.Clear();

            return db.CommandReader();
        }

        public void execute(ProductAction action)
        {
            product.Add(ProductAction.DeductStock, deductStockQuery());

            db.query = product[action];
            product.Clear();
         // 
            db.CommandExecute();
           
        }

        private string searchQuery()
        {
            var key = "'%" + searchKey + "%'";
            return "select * from tblproducts where productName like " + key + " or productCode like " + key + " or productCategory like " + key + " order by id";
        }

        private string loadQuery()
        {
            return "select * from tblproducts order by id";
        }

        private string insertQuery()
        {
            var _query = "insert into tblproducts (productCode,productCategory,productName,productPrice,productStock,datePurchased)";
            var _values = " values ('" + code + "','" + category + "','" + name + "'," + price + "," + stock + "," + datePurchased + ")";
            return _query + _values;
        }

        private string deductStockQuery()
        {
            var _query = "update tblproducts SET productStock = " + stock + " where productCode = '" + code + "'";
            return _query;
        }

        private string getStockQuery()
        {
            return "select productStock from tblproducts where productCode = '" + code + "'";
        }
 
    }

    class Search
    {
        public string searchKey { get; set; }    
    }

    enum ProductAction
    {
        Load,
        Search,
        Insert,
        GetStock,
        DeductStock
    }  
}
