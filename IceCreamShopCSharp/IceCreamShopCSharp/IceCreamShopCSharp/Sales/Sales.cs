using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

using System.Threading.Tasks;
namespace IceCreamShopCSharp
{
    class Sales: Product
    {

        public string   orNum { get; set; }
        public string   customerName { get; set; }
        public double   subTotal { get; set; }
        public double   total { get; set; }
        public double   change { get; set; }
        public int      quantity { get; set; }
        public double   cash { get; set; }
        public DateTime datePurchased { get; set; }

        private Database<OleDbDataReader> db = new Database<OleDbDataReader>();

        //just return DataReader to Data Services
      
        private Dictionary<SalesAction, string> sales = new Dictionary<SalesAction, string> { };

        public OleDbDataReader read(SalesAction action)
        {
            sales.Add(SalesAction.ReadOR, readORQuery());

            db.query = sales[action];
            sales.Clear();
            return db.CommandReader();
        }
        public void execute(SalesAction action)
        {
            sales.Add(SalesAction.SaveSales, saveSalesQuery());

            db.query = sales[action];
            sales.Clear();
            db.CommandExecute();
        }

        string readORQuery() 
        {
            return "select max(orNo) from tblsales";
        }

        string saveSalesQuery()
        {
            var _query = "insert into tblsales (ORno,customerName,productCode,productPrice,productQty,productSubtotal,datePurchased)";
            var _values = " values ('" + orNum + "','" + customerName + "','" + code + "'," + price + "," + quantity + "," + subTotal + ",#" + datePurchased + "#)";
            return _query + _values;
        }

    }

    enum SalesAction
    { 
        ReadOR,
        SaveSales
    }
}
