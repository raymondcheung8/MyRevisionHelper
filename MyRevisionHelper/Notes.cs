using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MyRevisionHelper
{
    public partial class Notes : Form
    {
        public Notes()
        {
            InitializeComponent();
        }

        // Main method
        private void Notes_Load(object sender, EventArgs e)
        {

        }

        // Method that closes the notes form and displays the main menu form to the user
        private void homeIcon_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Method that redirects the user to a form where they can add notes
        private void addNote_btn_Click(object sender, EventArgs e)
        {
            // Code for showing the newform
            using (AddNote newForm = new AddNote())
            {
                this.Hide();
                newForm.ShowDialog();
                this.Show();

                // Releases all resources used by the new form
                newForm.Dispose();
            }
        }

        // Method that redirects the user to a form where they can check notes
        private void checkNotes_btn_Click(object sender, EventArgs e)
        {
            // Code for showing the newform
            using (CheckNotes newForm = new CheckNotes())
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