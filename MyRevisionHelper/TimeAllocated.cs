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
    public partial class TimeAllocated : Form
    {
        // A global integer variable for the time allocated value is created so this value can be used by other forms
        public int timeAllocatedVal { get; set; }

        public TimeAllocated()
        {
            InitializeComponent();
        }

        // Main method
        private void TimeAllocated_Load(object sender, EventArgs e)
        {
            // Sets the value of timeAllocatedInput to 60
            timeAllocatedInput.Value = 60;

            // Only allows values within a certain range to be stored in timeAllocatedInput.Value
            timeAllocatedInput.Minimum = 1;
            timeAllocatedInput.Maximum = 120;
        }

        // Method that closes the TimeAllocated form and displays the main menu form to the user
        private void homeIcon_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Method to only allow values within a certain range to be stored in timeAllocatedInput.Value
        private void timeAllocatedInput_ValueChanged(object sender, EventArgs e)
        {
            if (timeAllocatedInput.Value < timeAllocatedInput.Minimum) timeAllocatedInput.Value = timeAllocatedInput.Minimum;
            if (timeAllocatedInput.Value > timeAllocatedInput.Maximum) timeAllocatedInput.Value = timeAllocatedInput.Maximum;
        }

        // Method that confirms the time allocated and closes the form
        private void confirm_btn_Click(object sender, EventArgs e)
        {
            // Stores the value of timeAllocatedInput in the variable timeAllocatedVal
            timeAllocatedVal = (int)timeAllocatedInput.Value;

            // Closes the form
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}