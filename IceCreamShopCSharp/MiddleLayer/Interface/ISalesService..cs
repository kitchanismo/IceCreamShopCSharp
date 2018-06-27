using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using DataAccessLayer;

namespace MiddleLayer
{
    public interface ISalesService 
    {
        ListView listView { get; set; }
        
        void AddToCart(ISales sales);

        void RemoveToCart(ISales sales);

        string GenerateNewOR(ISales sales);

        double ComputeChange();

        Color ChangeCashForeColor();

        bool HasPurchased(ISales sales);
    }
}
