using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IceCreamShopCSharp
{
    class Sales : Connection
    {
        public string customerName { get; set; }
        public string productCode { get; set; }
        public double productPrice { get; set; }
        public double productSubTotal { get; set; }
        public int productQuantity { get; set; }
        public DateTime datePurchased { get; set; }

    }
}
