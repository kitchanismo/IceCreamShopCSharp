using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MiddleLayer
{
    class CustomInputBox
    {
        public int quantity { get; set; }
        
        private TextBox txtQuantity = new TextBox();
        private Form inputBox = new Form();
        private Label lblDescription = new Label();
        private Label label1 = new Label();
        private Button okButton = new Button();
        private Button cancelButton = new Button();

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                changeQuantity();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            inputBox.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            changeQuantity();
        }

        private void changeQuantity()
        {
            if (txtQuantity.Text != "")
            {
                quantity = int.Parse(txtQuantity.Text);
                inputBox.Close();
            }
            else
            {
                quantity = 0;
            }
        }
       

        private void inputBox_FormClosing(object sender, EventArgs e)
        {
           
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public DialogResult Show(int input, string description = "", string title= "ENTER QUANTITY")
        {
           
            System.Drawing.Size size = new System.Drawing.Size(237, 125);
            lblDescription.AutoSize = true;
            lblDescription.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            
            lblDescription.Location = new System.Drawing.Point(13, 53);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new System.Drawing.Size(21, 20);
            lblDescription.TabIndex = 6;
            lblDescription.Text = description;
            // 
           
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(8, 22);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(153, 24);
            label1.TabIndex = 11;
            label1.ForeColor = System.Drawing.Color.Teal;
            label1.Text = "Enter Quantity";

            inputBox.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            inputBox.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            inputBox.BackColor = System.Drawing.Color.WhiteSmoke;
            inputBox.ClientSize = size;

            inputBox.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            inputBox.MaximizeBox = false;
            inputBox.MinimizeBox = false;
            inputBox.Name = "sampleDialogbox";
            inputBox.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            inputBox.Text = "Ice Cream Shop";

            
            txtQuantity.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtQuantity.Location = new System.Drawing.Point(12, 143);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new System.Drawing.Size(331, 37);
            txtQuantity.TabIndex = 5;
            txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            txtQuantity.Text = input.ToString();
            txtQuantity.ForeColor = System.Drawing.Color.Teal;

           
            // btnPurchase
            // 
            okButton.BackColor = System.Drawing.Color.White;
            okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            okButton.ForeColor = System.Drawing.Color.Teal;
            okButton.Location = new System.Drawing.Point(249, 22);
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(94, 42);
            okButton.TabIndex = 9;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = false;
          

            cancelButton.BackColor = System.Drawing.Color.White;
            cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cancelButton.ForeColor = System.Drawing.Color.Crimson;
            cancelButton.Location = new System.Drawing.Point(249, 69);
            cancelButton.Name = "button1";
            cancelButton.Size = new System.Drawing.Size(94, 37);
            cancelButton.TabIndex = 10;
            cancelButton.Text = "CANCEL";
            cancelButton.UseVisualStyleBackColor = false;

            inputBox.Controls.Add(cancelButton);
            inputBox.Controls.Add(txtQuantity);
            inputBox.Controls.Add(okButton);
            inputBox.Controls.Add(lblDescription);
            inputBox.Controls.Add(label1);

      
            okButton.Click += new System.EventHandler(okButton_Click);
            cancelButton.Click += new System.EventHandler(cancelButton_Click);
            txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtQuantity_KeyPress);
            txtQuantity.KeyDown += new System.Windows.Forms.KeyEventHandler(txtQuantity_KeyDown);
            inputBox.FormClosing += new System.Windows.Forms.FormClosingEventHandler(inputBox_FormClosing);
            label1.Text = title;

          
            DialogResult result = inputBox.ShowDialog();
     

            return result;
        }
    }
}
