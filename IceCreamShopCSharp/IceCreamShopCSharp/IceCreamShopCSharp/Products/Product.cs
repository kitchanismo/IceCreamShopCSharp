using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;    
using kitchanismo;
using System.Windows.Forms;

namespace IceCreamShopCSharp
{
    class Product 
    {

        public string productCode { get; set; }
        public string productCategory { get; set; }
        public string productName { get; set; }
        public double productPrice { get; set; }
        public int productStock { get; set; }
        public int productQuantity { get; set; }
        public DateTime productPurchased { get; set; }

        private Database db = new Database();

        //just return DataReader to Data Services
        public OleDbDataReader readProducts(ProductAction productAction)
        {
           
            switch (productAction)
            {
                case ProductAction.Load:
                    db.query = loadQuery();
                    break;
                case ProductAction.Search:
                    db.query = searchQuery();
                    break;
                case ProductAction.GetStock:
                    db.query = getStockQuery();
                    break;
            }
            return db.CommandReader();
        }

        public void executeProducts(ProductAction productAction)
        {
            switch (productAction)
            {
                case ProductAction.DeductStock:
                    db.query = deductStockQuery();
                    break;
            }
            db.CommandExecute();
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

        private string deductStockQuery()
        {
            var _query = "update tblproducts SET productStock = " + productQuantity + " where productCode = '" + productCode + "'";
            return _query;
        }

        private string getStockQuery()
        {
            return "select productStock from tblproducts where productCode = '" + productCode + "'";
        }
 
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
