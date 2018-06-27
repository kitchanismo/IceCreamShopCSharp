using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataAccessLayer;

namespace MiddleLayer
{
    public interface IProductService
    {
        bool hasAction { get; set; }
        ListView listView { get; set; }
        void Read(IProduct product);
        void Search(IProduct product);
        void Insert(IProduct product);
        void ClickAction(MouseEventArgs e);
        void ChangeMouseCursor(MouseEventArgs e);
    }
}
