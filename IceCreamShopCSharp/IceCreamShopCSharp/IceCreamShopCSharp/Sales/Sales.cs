using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
namespace IceCreamShopCSharp
{
    class Sales : Product
    {
        public string customerName { get; set; }
        public string productCode { get; set; }
        public double productPrice { get; set; }
        public double productSubTotal { get; set; }
        public int productQuantity { get; set; }
        public string total { get; set; }
        public string cash { get; set; }
        public DateTime datePurchased { get; set; }

        //just return DataReader to Data Services
        protected OleDbDataReader readSales(SalesAction salesAction)
        {
            switch (salesAction)
            {
                case SalesAction.ReadOR:
                    query = readORQuery();
                    break;
            }
            return CommandReader(query);
        }
        string readORQuery() 
        {
            return "select max(orNo) from tblsales";
        }
    }

    enum SalesAction
    { 
        ReadOR
    }
}
