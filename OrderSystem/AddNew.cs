using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OrderSystem
{
    public partial class AddNew : Form
    {
        Form1 f;
        public AddNew(Form1 form)
        {
            InitializeComponent();
            f = form;

        }

        private void AddNew_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            int productID;
            double price;
            if (int.TryParse(txtID.Text, out productID) && double.TryParse(txtPrice.Text, out price))
            {
                productID = int.Parse(txtID.Text);
                price = double.Parse(txtPrice.Text);
                bool validd = f.addItem(productID, price);
                if (validd) { this.Close(); }
                else { MessageBox.Show("An item with the specified id already exists"); }
                
            }
            else
            {
                MessageBox.Show("Provide valid input!", "warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
