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


    public partial class Form1 : Form
    {


        // create lists
        List<int> Quantities = new List<int>();
        List<double> Prices = new List<double>();
        List<double> Totals= new List<double>();
        List<double> TotalsDiscounted = new List<double>();
        List<double> AddPrices = new List<double>();
        List<int> pID = new List<int>();

        public Form1()
        {
            InitializeComponent();
        }

        public bool addItem(int productID, double price) {
            if (comboProductID.Items.Contains(productID))
            {
                return false;
            }
            pID.Add(productID);
            AddPrices.Add(price);
            comboProductID.Items.Add(productID);
            txtQuantity.Enabled = true;
            comboProductID.Enabled = true;
            txtPrice.Enabled = true;
            comboProductID.SelectedIndex = comboProductID.Items.Count - 1;
            txtQuantity.Focus();
            return true;
        
        }

        private void btnAdd_Click(object sender, EventArgs e)
        { 
            AddNew formAdd = new AddNew(this);
            formAdd.ShowDialog();
           

        }

        private void btnAddCart_Click(object sender, EventArgs e)
        {
            int quantity, productID;
            double price;

            if (int.TryParse(txtQuantity.Text, out quantity) && double.TryParse(txtPrice.Text, out price))
            {
                quantity = int.Parse(txtQuantity.Text);
                price = double.Parse(txtPrice.Text);
                productID = int.Parse(comboProductID.Text);

                listOrders.Items.Add(quantity + " items of product ID " + productID + " @ $ " + price);
                Quantities.Add(quantity);
                Prices.Add(price);
                txtQuantity.Clear();
            }
            else {
                MessageBox.Show("Provide valid input!","warning",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            listDisplay.Items.Clear();
            Totals.Clear();
            TotalsDiscounted.Clear();
            if (Prices.Count <= 0)
            {
                MessageBox.Show("No items yet to calculate Order!", "warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            //loop through the Quantities and Prices list which are of the same size
            //and multiply the first item in the Prices list with corresponding item 
            //in the Quantity list.This gives the Original Price before discount. 
            for (int i = 0; i < Quantities.Count; i++) {
                int q = Quantities.ElementAt(i);
                double p = Prices.ElementAt(i);
                String s = "";
                double t = p * q;
                Totals.Add(t);

                s += q + " * " + p + " = " + t + " $ ";

                //calculate discount depending on item quantity
                double discount = 0;
                double d = 0;

                if (q > 10 && q <= 25) {
                    d = 0.1;
                    discount = q * d;
                    
                }
                else if (q > 25 && q <= 50)
                {
                    d = .25;
                    discount = q * d;
                    
                }
                else if (q > 50)
                {
                    d = .5;
                    discount = q * d;
                    d = .5;
                }
                else {
                    discount = 0;
                }

                if (discount == 0)
                {
                    s += " (no discount) ";
                }
                else {
                    s += " (Totals) " + q + " * "+ d +" = "+  discount + " (discount) " + t + " - " + discount + " = " + (t-(discount)) + " (Total with discounts) ";
                   
                   
                }
                TotalsDiscounted.Add((t - (discount)));
                listDisplay.Items.Add(s);
            }
            String grandTotal = "Grand Total = ";
            double totals=0;



            //loop over Totals without discounts
            for (int i = 0; i < Totals.Count; i++) {
                totals += Totals.ElementAt(i);
                if (Totals.Count == 1) {
                    continue;
                }
                grandTotal += Totals.ElementAt(i);
                if (i != Totals.Count - 1) {
                    grandTotal += " + ";
                        }
                          
              
            }
            grandTotal += " = "  + totals;
            listDisplay.Items.Add(grandTotal);

            String grandTotalWithDiscount = "Grand Total with Discount = ";
            double totalWithDiscount = 0;

            //loop over Totals with discounts
            for (int i = 0; i < TotalsDiscounted.Count; i++)
            {
                totalWithDiscount += TotalsDiscounted.ElementAt(i);
                if (TotalsDiscounted.Count == 1)
                {
                    continue;
                }
                grandTotalWithDiscount += TotalsDiscounted.ElementAt(i);
                if (i != TotalsDiscounted.Count - 1)
                {
                    grandTotalWithDiscount += " + ";
                }
               
            }
            grandTotalWithDiscount += "=" + totalWithDiscount;
            listDisplay.Items.Add(grandTotalWithDiscount);

        }

        private void comboProductID_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPrice.Text= AddPrices.ElementAt(comboProductID.SelectedIndex).ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtQuantity.Enabled=false;
            comboProductID.Enabled=false;
            txtPrice.Enabled=false;

        }
    }
}
