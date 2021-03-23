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
    public partial class Create : Form
    {
        public Create()
        {
            InitializeComponent();
        }

        // Main method
        private void Create_Load(object sender, EventArgs e)
        {

        }

        // Method that closes the Create form and displays the main menu form to the user
        private void homeIcon_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Method that codes for what happens when the createQna_btn is clicked
        private void createQna_btn_Click(object sender, EventArgs e)
        {
            // Code for showing the newform
            using (CreateQna newForm = new CreateQna())
            {
                this.Hide();
                newForm.ShowDialog();
                this.Show();

                // Releases all resources used by the new form
                newForm.Dispose();
            }
        }

        // Method that codes for what happens when the createWordedQ_btn is clicked
        private void createWordedQ_btn_Click(object sender, EventArgs e)
        {
            // Code for showing the newform
            using (CreateWordedQ newForm = new CreateWordedQ())
            {
                this.Hide();
                newForm.ShowDialog();
                this.Show();

                // Releases all resources used by the new form
                newForm.Dispose();
            }
        }

        // Method that codes for what happens when the createMathQ_btn is clicked
        private void createMathQ_btn_Click(object sender, EventArgs e)
        {
            // Code for showing the newform
            using (CreateMathQ newForm = new CreateMathQ())
            {
                this.Hide();
                newForm.ShowDialog();
                this.Show();

                // Releases all resources used by the new form
                newForm.Dispose();
            }
        }

        // Method that codes for what happens when the createTimedMC_btn is clicked
        private void createTimedMC_btn_Click(object sender, EventArgs e)
        {
            // Code for showing the newform
            using (CreateTimedMC newForm = new CreateTimedMC())
            {
                this.Hide();
                newForm.ShowDialog();
                this.Show();

                // Releases all resources used by the new form
                newForm.Dispose();
            }
        }
    }
}