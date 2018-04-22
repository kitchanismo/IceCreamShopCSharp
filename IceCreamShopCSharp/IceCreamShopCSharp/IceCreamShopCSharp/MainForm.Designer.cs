namespace IceCreamShopCSharp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelNavbar = new System.Windows.Forms.Panel();
            this.accentbar = new System.Windows.Forms.Panel();
            this.lblBrandName = new System.Windows.Forms.Label();
            this.btnSales = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.btnPOS = new System.Windows.Forms.Button();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.panelWrapper = new System.Windows.Forms.Panel();
            this.panelPOS = new System.Windows.Forms.Panel();
            this.panelInventory = new System.Windows.Forms.Panel();
            this.panelSales = new System.Windows.Forms.Panel();
            this.pointOfSale1 = new IceCreamShopCSharp.POSTab();
            this.inventoryForm1 = new IceCreamShopCSharp.InventoryTab();
            this.salesForm1 = new IceCreamShopCSharp.SalesTab();
            this.panelNavbar.SuspendLayout();
            this.panelWrapper.SuspendLayout();
            this.panelPOS.SuspendLayout();
            this.panelInventory.SuspendLayout();
            this.panelSales.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelNavbar
            // 
            this.panelNavbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelNavbar.Controls.Add(this.accentbar);
            this.panelNavbar.Controls.Add(this.lblBrandName);
            this.panelNavbar.Controls.Add(this.btnSales);
            this.panelNavbar.Controls.Add(this.btnInventory);
            this.panelNavbar.Controls.Add(this.btnPOS);
            this.panelNavbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNavbar.Location = new System.Drawing.Point(0, 0);
            this.panelNavbar.Margin = new System.Windows.Forms.Padding(4);
            this.panelNavbar.Name = "panelNavbar";
            this.panelNavbar.Size = new System.Drawing.Size(1022, 76);
            this.panelNavbar.TabIndex = 0;
            // 
            // accentbar
            // 
            this.accentbar.BackColor = System.Drawing.Color.Teal;
            this.accentbar.Location = new System.Drawing.Point(895, 66);
            this.accentbar.Name = "accentbar";
            this.accentbar.Size = new System.Drawing.Size(120, 5);
            this.accentbar.TabIndex = 4;
            // 
            // lblBrandName
            // 
            this.lblBrandName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBrandName.AutoSize = true;
            this.lblBrandName.Font = new System.Drawing.Font("Century Gothic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBrandName.ForeColor = System.Drawing.Color.White;
            this.lblBrandName.Location = new System.Drawing.Point(18, 20);
            this.lblBrandName.Name = "lblBrandName";
            this.lblBrandName.Size = new System.Drawing.Size(263, 36);
            this.lblBrandName.TabIndex = 3;
            this.lblBrandName.Text = "ICE CREAM SHOP";
            // 
            // btnSales
            // 
            this.btnSales.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSales.BackColor = System.Drawing.Color.Goldenrod;
            this.btnSales.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSales.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnSales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSales.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSales.ForeColor = System.Drawing.Color.White;
            this.btnSales.Location = new System.Drawing.Point(642, 8);
            this.btnSales.Margin = new System.Windows.Forms.Padding(4);
            this.btnSales.Name = "btnSales";
            this.btnSales.Size = new System.Drawing.Size(120, 54);
            this.btnSales.TabIndex = 2;
            this.btnSales.Text = "SALES";
            this.btnSales.UseVisualStyleBackColor = false;
            this.btnSales.Click += new System.EventHandler(this.btnSales_Click);
            // 
            // btnInventory
            // 
            this.btnInventory.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnInventory.BackColor = System.Drawing.Color.IndianRed;
            this.btnInventory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInventory.FlatAppearance.BorderColor = System.Drawing.Color.IndianRed;
            this.btnInventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInventory.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInventory.ForeColor = System.Drawing.Color.White;
            this.btnInventory.Location = new System.Drawing.Point(769, 8);
            this.btnInventory.Margin = new System.Windows.Forms.Padding(4);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(120, 54);
            this.btnInventory.TabIndex = 1;
            this.btnInventory.Text = "INVENTORY";
            this.btnInventory.UseVisualStyleBackColor = false;
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);
            // 
            // btnPOS
            // 
            this.btnPOS.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnPOS.BackColor = System.Drawing.Color.Teal;
            this.btnPOS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPOS.FlatAppearance.BorderColor = System.Drawing.Color.Teal;
            this.btnPOS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPOS.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPOS.ForeColor = System.Drawing.Color.White;
            this.btnPOS.Location = new System.Drawing.Point(895, 8);
            this.btnPOS.Margin = new System.Windows.Forms.Padding(4);
            this.btnPOS.Name = "btnPOS";
            this.btnPOS.Size = new System.Drawing.Size(120, 54);
            this.btnPOS.TabIndex = 0;
            this.btnPOS.Text = "P O S";
            this.btnPOS.UseVisualStyleBackColor = false;
            this.btnPOS.Click += new System.EventHandler(this.btnPOS_Click);
            // 
            // panelFooter
            // 
            this.panelFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelFooter.Location = new System.Drawing.Point(0, 701);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1022, 55);
            this.panelFooter.TabIndex = 1;
            // 
            // panelWrapper
            // 
            this.panelWrapper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panelWrapper.BackColor = System.Drawing.Color.Silver;
            this.panelWrapper.Controls.Add(this.panelPOS);
            this.panelWrapper.Controls.Add(this.panelInventory);
            this.panelWrapper.Controls.Add(this.panelSales);
            this.panelWrapper.Location = new System.Drawing.Point(0, 76);
            this.panelWrapper.Name = "panelWrapper";
            this.panelWrapper.Size = new System.Drawing.Size(1022, 627);
            this.panelWrapper.TabIndex = 2;
            // 
            // panelPOS
            // 
            this.panelPOS.BackColor = System.Drawing.Color.MediumTurquoise;
            this.panelPOS.Controls.Add(this.pointOfSale1);
            this.panelPOS.Location = new System.Drawing.Point(0, 0);
            this.panelPOS.Name = "panelPOS";
            this.panelPOS.Size = new System.Drawing.Size(1022, 625);
            this.panelPOS.TabIndex = 1;
            this.panelPOS.Paint += new System.Windows.Forms.PaintEventHandler(this.panelPOS_Paint);
            // 
            // panelInventory
            // 
            this.panelInventory.BackColor = System.Drawing.Color.IndianRed;
            this.panelInventory.Controls.Add(this.inventoryForm1);
            this.panelInventory.Location = new System.Drawing.Point(0, 0);
            this.panelInventory.Name = "panelInventory";
            this.panelInventory.Size = new System.Drawing.Size(1022, 625);
            this.panelInventory.TabIndex = 1;
            // 
            // panelSales
            // 
            this.panelSales.BackColor = System.Drawing.Color.Goldenrod;
            this.panelSales.Controls.Add(this.salesForm1);
            this.panelSales.Location = new System.Drawing.Point(0, 0);
            this.panelSales.Name = "panelSales";
            this.panelSales.Size = new System.Drawing.Size(1022, 625);
            this.panelSales.TabIndex = 0;
            // 
            // pointOfSale1
            // 
            this.pointOfSale1.BackColor = System.Drawing.Color.Teal;
            this.pointOfSale1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pointOfSale1.Location = new System.Drawing.Point(0, 1);
            this.pointOfSale1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pointOfSale1.Name = "pointOfSale1";
            this.pointOfSale1.Size = new System.Drawing.Size(1022, 573);
            this.pointOfSale1.TabIndex = 0;
            // 
            // inventoryForm1
            // 
            this.inventoryForm1.BackColor = System.Drawing.Color.IndianRed;
            this.inventoryForm1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inventoryForm1.Location = new System.Drawing.Point(0, 0);
            this.inventoryForm1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inventoryForm1.Name = "inventoryForm1";
            this.inventoryForm1.Size = new System.Drawing.Size(1020, 372);
            this.inventoryForm1.TabIndex = 0;
            // 
            // salesForm1
            // 
            this.salesForm1.BackColor = System.Drawing.Color.Goldenrod;
            this.salesForm1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.salesForm1.Location = new System.Drawing.Point(0, 0);
            this.salesForm1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.salesForm1.Name = "salesForm1";
            this.salesForm1.Size = new System.Drawing.Size(1007, 389);
            this.salesForm1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1022, 755);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelWrapper);
            this.Controls.Add(this.panelNavbar);
            this.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.panelNavbar.ResumeLayout(false);
            this.panelNavbar.PerformLayout();
            this.panelWrapper.ResumeLayout(false);
            this.panelPOS.ResumeLayout(false);
            this.panelInventory.ResumeLayout(false);
            this.panelSales.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelNavbar;
        private System.Windows.Forms.Button btnPOS;
        private System.Windows.Forms.Button btnSales;
        private System.Windows.Forms.Button btnInventory;
        private System.Windows.Forms.Label lblBrandName;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Panel panelWrapper;
        private System.Windows.Forms.Panel panelPOS;
        private System.Windows.Forms.Panel panelInventory;
        private System.Windows.Forms.Panel panelSales;
        private System.Windows.Forms.Panel accentbar;
        private InventoryTab inventoryForm1;
        private SalesTab salesForm1;
        private POSTab pointOfSale1;
    }
}