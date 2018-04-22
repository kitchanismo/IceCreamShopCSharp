using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace IceCreamShopCSharp
{
    class Shared
    {
        public void doChangeForeColor(ListView lv)
        {
            var lvForeColor = new kitchanismo.ListViewForeColor();

            lvForeColor.ListView = lv;
            lvForeColor.Critical = 5;
            lvForeColor.Column = 5;
            lvForeColor.ColorStable = Color.Teal;
            lvForeColor.ColorWarning = Color.Orange;
            lvForeColor.ColorDanger = Color.Crimson;

            lvForeColor.changeListViewForeColor();
        }
    }
}
