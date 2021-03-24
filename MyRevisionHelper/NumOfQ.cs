using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyRevisionHelper
{
    public partial class NumOfQ : Form
    {
        // A global integer variable for the number of questions value is created so this value can be used by other forms
        public int numOfQVal { get; set; }

        public NumOfQ()
        {
            InitializeComponent();
        }

        // Main method
        private void NumOfQ_Load(object sender, EventArgs e)
        {
            // Sets the value of numOfQInput to 1
            numOfQInput.Value = 1;

            // Only allows values within a certain range to be stored in numOfQInput.Value
            numOfQInput.Minimum = 1;
            numOfQInput.Maximum = 100;
        }

        // Method that closes the NumOfQ form and displays the main menu form to the user
        private void homeIcon_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Method to only allow values within a certain range to be stored in numOfQInput.Value
        private void numOfQInput_ValueChanged(object sender, EventArgs e)
        {
            if (numOfQInput.Value < numOfQInput.Minimum) numOfQInput.Value = numOfQInput.Minimum;
            if (numOfQInput.Value > numOfQInput.Maximum) numOfQInput.Value = numOfQInput.Maximum;
        }

        // Method that confirms the number of questions and closes the form
        private void confirm_btn_Click(object sender, EventArgs e)
        {
            // Stores the value of numOfQInput in the variable numOfQVal
            numOfQVal = (int) numOfQInput.Value;

            // Closes the form
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}