using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer
{
    public interface IProduct
    {
        string Code { get; set; }
        string Category { get; set; }
        string ItemName { get; set; }
        double Price { get; set; }
        double Stock { get; set; }
        DateTime DatePurchased { get; set; }
        List<Product> GetSearchProducts();
        List<Product> GetProducts();
        void Save();
        void DeductStock();
        int GetStock();
    }

}
