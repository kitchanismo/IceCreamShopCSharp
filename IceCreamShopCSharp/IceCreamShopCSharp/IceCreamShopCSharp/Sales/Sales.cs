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

        private Database db = new Database();

        //just return DataReader to Data Services
        public OleDbDataReader readSales(SalesAction salesAction)
        {
            switch (salesAction)
            {
                case SalesAction.ReadOR:
                    db.query = readORQuery();
                    break;
                case SalesAction.SaveSales:
                    db.query = saveSalesQuery();
                    break;
            }
            return db.CommandReader();
        }

        public void executeSales(SalesAction salesAction)
        {
            switch (salesAction)
            {
                case SalesAction.SaveSales:
                    db.query = saveSalesQuery();
                    break;
            }

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
