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
    public partial class CreateTimedMC : Form
    {
        public CreateTimedMC()
        {
            InitializeComponent();
        }

        // Main method
        private void CreateTimedMC_Load(object sender, EventArgs e)
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
                    if (question_textBox.Text != "" && answer_textBox.Text != "" && wAnswer1_textBox.Text != "" && wAnswer2_textBox.Text != "" && wAnswer3_textBox.Text != "")
                    {
                        if (getIsAns(answer_textBox.Text, wAnswer1_textBox.Text, wAnswer2_textBox.Text, wAnswer3_textBox.Text))
                        {
                            // Instantiates a new object called createActivity
                            CreateActivity createActivity = new CreateActivity();

                            // Calls a method of createActivity to try add the question to the database
                            if (createActivity.createTimedMC(connection, question_textBox.Text, answer_textBox.Text, wAnswer1_textBox.Text, wAnswer2_textBox.Text, wAnswer3_textBox.Text))
                            {
                                // Tells the user their question has been successfully added to the database
                                MessageBox.Show("Question successfully added to the database");

                                // Closes this form, redirecting the user back to the notes form
                                this.Close();
                            }
                        }
                        else
                        {
                            // Tells the user to make sure there isn't more than one of the same answer
                            MessageBox.Show("Please make sure there isn't more than one of the same answer");
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

        // A function that returns whether all the answers are valid
        private bool getIsAns(string a, string b, string c, string d)
        {
            // Checks whether all of the answers are different to one another and returns true or false accordingly
            return 
                (a != b && a != c && a != d &&
                b != c && c != d &&
                c != d)
                ? true
                : false;
        }
    }
}