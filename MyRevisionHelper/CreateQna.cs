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
    public partial class CreateQna : Form
    {
        public CreateQna()
        {
            InitializeComponent();
        }

        // Main method
        private void CreateQna_Load(object sender, EventArgs e)
        {

        }

        // Method for what happens when the goBack_lbl is clicked
        private void goBack_lbl_Click(object sender, EventArgs e)
        {
            // Closes this form, allowing the user to go back to the notes form
            this.Close();
        }

        // Method for what happens when the store_btn_Click is clicked
        private void store_btn_Click(object sender, EventArgs e)
        {
            // Tries to create a connection to the database and otherwise catches the error and tells the user what the error is
            try
            {
                // Declaring the connection
                using (SqlConnection connection = new SqlConnection())
                {
                    // This allows us to connect to SQL Server
                    connection.ConnectionString = Program.connectionString;

                    // Opens the connection to the database
                    connection.Open();



                    // Makes sure the user doesn't leave the question and answer text boxes blank
                    if (question_textBox.Text != "" && answer_textBox.Text != "")
                    {
                        // Instantiates a new object called createActivity
                        CreateActivity createActivity = new CreateActivity();

                        // Calls a method of createActivity to try add the question to the database
                        if (createActivity.createQna(connection, question_textBox.Text, answer_textBox.Text))
                        {
                            // Tells the user their question has been successfully added to the database
                            MessageBox.Show("Question successfully added to the database");

                            // Closes this form, redirecting the user back to the notes form
                            this.Close();
                        }
                    }
                    else
                    {
                        // Tells the user to fill in all areas
                        MessageBox.Show("Please fill in all areas");
                    }



                    // Closes the connection to the database
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n\n" + ex);
            }
        }
    }
}