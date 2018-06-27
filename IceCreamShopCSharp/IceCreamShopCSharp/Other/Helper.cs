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
        static TransparentForm transparent = new TransparentForm();

        public static void Notification(string message, Notify notify)
        {
            dimEnabled(true);
            if (Notify.Success == notify)
            {
                MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Notify.Question == notify)
            {
                MessageBox.Show(message, "Question", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else if (Notify.Error == notify)
            {
                MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dimEnabled(false);
        }

        public static void dimEnabled(bool b)
        {

            var x = MainForm.ActiveForm.DesktopLocation.X;
            var y = MainForm.ActiveForm.DesktopLocation.Y;

            transparent.Size = MainForm.ActiveForm.Size;
            transparent.SetDesktopLocation(x, y);

            if (b)
            {
                transparent.Show();
            }
            else
            {
                transparent.Hide();
            }
        }

        public static void doNumbersSinglePointOnly(object sender, KeyPressEventArgs e)
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

        public static double getColumnSum(ListView lv, int column)
        {
            double total = 0;
            for (int i = 0; i < lv.Items.Count; i++)
            {
                total += double.Parse(lv.Items[i].SubItems[column - 1].Text);
            }
            return total;
        }


        public static bool isEnabled(Color _color)
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

    public enum Notify
    {
        Success,
        Error,
        Question
    }
}
