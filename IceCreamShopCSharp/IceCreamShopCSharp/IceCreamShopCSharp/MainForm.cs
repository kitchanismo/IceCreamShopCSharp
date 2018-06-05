using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kitchanismo;

namespace IceCreamShopCSharp
{
    public partial class MainForm : Form
    {
        KitchanismoTransition transition = new KitchanismoTransition();
        KitchanismoAnimation animation = new KitchanismoAnimation();
   
        public MainForm()
        {
            InitializeComponent();
            onInit();
        }

        private void onInit()
        {
            panelPOS.BringToFront();
            dockFill();
            InitializeTransition();
        }

        private void dockFill()
        {
            pointOfSale1.Dock   = DockStyle.Fill;
            inventoryForm1.Dock = DockStyle.Fill;
            salesForm1.Dock     = DockStyle.Fill;
        }

        //transition requirements
        private void InitializeTransition()
        {
            transition.TabsArray(panelPOS, panelInventory, panelSales);

            transition.Speed = 600;

            transition.Ease = Easing.CubicInOut;
            transition.Type = TypeTransition.Swap;
            transition.Shift = ShiftTransition.Y;
            transition.ReverseShift = true;
        }

        private void moveAccentBar(Button btn)
        {
            var point = new Point();

            point.Y = accentbar.Location.Y;
            point.X = btn.Location.X;

            animation.AccentSpeed = 25;
            animation.ColorSpeed = 800;
            animation.Location = point;
            animation.Target = accentbar;
            animation.Color = btn.BackColor;

            animation.MoveAccentBar();
            animation.ChangeBackColor();

            animation.Target = panelWrapper;
            animation.ChangeBackColor();
        }

        private void doTransition(Control active)
        {
            //panelWrapper.BackColor = active.BackColor;
            transition.TabActive = active;
            transition.Run();   
            
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
