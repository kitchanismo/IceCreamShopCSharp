using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer
{
    public interface ISales : IProduct
    {
        string ORno { get; set; }
        string CustomerName { get; set; }
        double SubTotal { get; set; }
        double Total { get; set; }
        double Change { get; set; }
        int Quantity { get; set; }
        double Cash { get; set; }
        string GetMaxOR();
    }
}
