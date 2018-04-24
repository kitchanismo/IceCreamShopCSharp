using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace IceCreamShopCSharp
{
    class SharedGlobal
    {
        transparentForm transparent = new transparentForm();

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

        public void dimEnabled(bool b)
        {
           
            var x = MainForm.ActiveForm.DesktopLocation.X;
            var y = MainForm.ActiveForm.DesktopLocation.Y;

            transparent.Size = MainForm.ActiveForm.Size;
            transparent.SetDesktopLocation(x,y);

            if (b)
            {
                transparent.Show();
            }
            else
            {
                transparent.Hide();
            }
        }


    }
}
