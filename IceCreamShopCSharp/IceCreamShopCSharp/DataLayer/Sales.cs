using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlHelper;

namespace IceCreamShopCSharp
{
    class Sales: Product
    {
        public string   ORno { get; set; }
        public string   customerName { get; set; }
        public double   subTotal { get; set; }
        public double   total { get; set; }
        public double   change { get; set; }
        public int      quantity { get; set; }
        public double   cash { get; set; }
        public DateTime datePurchased { get; set; }

        CRUD crud = new CRUD(Database.Connection());

        public void SaveSales()
        {
            var sql = "insert into sales (ORno,customerName,code,price,quantity,subTotal,datePurchased) values (@ORno,@customerName,@code,@price,@quantity,@subTotal,@datePurchased)";
            var parameters = new Dictionary<string, object>();
                parameters.Add("@ORno",          ORno);
                parameters.Add("@customerName",  customerName);
                parameters.Add("@code",          code);
                parameters.Add("@price",         price);
                parameters.Add("@quantity",      quantity);
                parameters.Add("@subTotal",      subTotal);
                parameters.Add("@datePurchased", datePurchased);
            crud.Query<Sales>(sql, parameters);
        }

        public string GetMaxOR()
        {
           return crud.GetMax<Sales>("orno");
        }

    }
}
