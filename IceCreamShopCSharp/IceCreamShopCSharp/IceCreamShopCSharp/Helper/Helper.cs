using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace IceCreamShopCSharp
{
    class Helper
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

        public double getColumnSum(ListView lv, int column)
        {
            double total = 0;
            for (int i = 0; i < lv.Items.Count; i++)
            {
                total += double.Parse(lv.Items[i].SubItems[column - 1].Text);
            }
            return total;
        }

        public bool IsEmpty(string[] str)
        {
            foreach (string _str in str)
            {
               if (_str == "" || _str == null)
               {
                   return true;
               }
               else if (double.Parse(_str) == 0)
               {
                   return true;
               }
               else
               {
                   return false;
               }
            }
            return false;
          
        }

        public void NumbersSinglePointOnly(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            TextBox txtDecimal = sender as TextBox;

            if (e.KeyChar == '.' && txtDecimal.Text.Contains("."))
            {
                e.Handled = true;
            }

        }

        public bool IsEnabled(Color _color)
        {
            if (_color == Color.Crimson)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        
    }
}
