using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlHelper;

namespace DataAccessLayer
{
    public class Sales : Product, ISales
    {
        public string   ORno { get; set; }
        public string   CustomerName { get; set; }
        public double   SubTotal { get; set; }
        public double   Total { get; set; }
        public double   Change { get; set; }
        public int      Quantity { get; set; }
        public double   Cash { get; set; }

        CRUD crud = new CRUD(Database.Connection());

        public override void Save()
        {
            var sql = "insert into sales (ORno,customerName,code,price,quantity,subTotal,datePurchased) values (@ORno,@customerName,@code,@price,@quantity,@subTotal,@datePurchased)";
            var parameters = new Dictionary<string, object>();
                parameters.Add("@ORno",          ORno);
                parameters.Add("@customerName",  CustomerName);
                parameters.Add("@code",          Code);
                parameters.Add("@price",         Price);
                parameters.Add("@quantity",      Quantity);
                parameters.Add("@subTotal",      SubTotal);
                parameters.Add("@datePurchased", DatePurchased);
                crud.Query<Sales>(sql, parameters);
        }

        public  string GetMaxOR()
        {
           return crud.GetMax<Sales>("orno");
        }

    }
}
