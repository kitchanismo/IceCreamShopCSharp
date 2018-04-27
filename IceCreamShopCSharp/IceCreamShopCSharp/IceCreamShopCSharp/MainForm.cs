using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using kitchanismo_transition;

namespace IceCreamShopCSharp
{
    public partial class MainForm : Form
    {
        Kitchanismo chan = new Kitchanismo();
   
        public MainForm()
        {
            InitializeComponent();
            onInit();
        }

        private void onInit()
        {
            panelPOS.BringToFront();
            dockFill();
            TProperties.initLocation();
        }

        private void dockFill()
        {
            pointOfSale1.Dock   = DockStyle.Fill;
            inventoryForm1.Dock = DockStyle.Fill;
            salesForm1.Dock     = DockStyle.Fill;
        }
   
        private void initTransition(Control target)
        {
            TProperties.nav   = target;
            TProperties.nav1  = panelPOS;
            TProperties.nav2  = panelInventory;
            TProperties.nav3  = panelSales;
            TProperties.nav4  = new Panel();
            TProperties.nav5  = new Panel();
            TProperties.loc   = 0;
            TProperties.speed = 800;
            TProperties.ease  = IEasing.quintinout;
        }

        private void moveAccentBar(Button btn)
        {
            var point = new Point();

            point.Y = accentbar.Location.Y;
            point.X = btn.Location.X;

            chan.move_animation(accentbar, point, 30);
            chan.changebackcolor_control(accentbar, btn.BackColor);
        }

        private void doTransition(Control target)
        {
            initTransition(target);
            chan.y_wipe(true);   
            panelWrapper.BackColor = target.BackColor;
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            doTransition(panelSales);
            moveAccentBar(btnSales);
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            doTransition(panelInventory);
            moveAccentBar(btnInventory);
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            doTransition(panelPOS);
            moveAccentBar(btnPOS);
        }

    
    }
}
