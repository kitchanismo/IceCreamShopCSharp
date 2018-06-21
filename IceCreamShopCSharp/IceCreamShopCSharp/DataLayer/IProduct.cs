using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IceCreamShopCSharp
{
     interface IProduct
    {
        string code { get; set; }
        string category { get; set; }
        string itemName { get; set; }
        double price { get; set; }
        double stock { get; set; }
        DateTime datePurchased { get; set; }

        List<Product> GetSearchProducts();
        List<Product> GetProducts();
        void SaveProduct();
        void DeductStock();
        int GetStock();
    }
}
