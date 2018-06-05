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

        public string ORno { get; set; }
        public string customerName { get; set; }
        public double productSubTotal { get; set; }
        public double productTotal { get; set; }
        public double productChange { get; set; }
        public string total { get; set; }
        public string cash { get; set; }
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

            var t = new Task(db.CommandExecute);

            t.Start();
            t.Wait();

           // db.CommandExecute();
        }

        string readORQuery() 
        {
            return "select max(orNo) from tblsales";
        }

        string saveSalesQuery()
        {
            var _query = "insert into tblsales (ORno,customerName,productCode,productPrice,productQty,productSubtotal,datePurchased)";
            var _values = " values ('" + ORno + "','" + customerName + "','" + productCode + "'," + productPrice + "," + productQuantity + "," + productSubTotal + ",#" + datePurchased + "#)";
            return _query + _values;
        }

    }

    enum SalesAction
    { 
        ReadOR,
        SaveSales
    }
}
