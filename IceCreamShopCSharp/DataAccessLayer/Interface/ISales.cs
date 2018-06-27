using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer
{
    public interface ISales : IProduct
    {
        string ORno { get; set; }
        string customerName { get; set; }
        double subTotal { get; set; }
        double total { get; set; }
        double change { get; set; }
        int quantity { get; set; }
        double cash { get; set; }

        void SaveSales();
        string GetMaxOR();
    }
}
