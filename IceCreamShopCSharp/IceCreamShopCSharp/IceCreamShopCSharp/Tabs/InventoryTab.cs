using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IceCreamShopCSharp
{
    public partial class InventoryTab : UserControl
    {
        
        Products product = new Products();

        public InventoryTab()
        {
            InitializeComponent();
        }

        private void POSForm_Load(object sender, EventArgs e)
        {
            product.read(lvProduct);
        }
    }
}
