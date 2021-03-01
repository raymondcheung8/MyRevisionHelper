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
    public partial class WordedQ : Form
    {
        // A local integer variable is created with a scope of the WordedQ form that holds the value of the number of questions
        private int numOfQVal;

        // A local integer variable is created with a scope of the WordedQ form that holds the next question number
        private int nextQNo;

        // A local integer variable is created with a scope of the WordedQ form that holds the number of questions answered correctly
        private int numOfQAnsCorr;

        // Creates an instance of the object WordedQActivity
        private WordedQActivity activity = new WordedQActivity();

        public WordedQ()
        {
            InitializeComponent();
        }

        // Main method
        private void WordedQ_Load(object sender, EventArgs e)
        {
            // This hides the skip and next button when the form is first loaded
            skip_btn.Hide();
            next_btn.Hide();

            // Initialises the integer variable currentQNo to 2
            nextQNo = 2;

            // Initialises the integer variable numOfQAnsCorr to 0
            numOfQAnsCorr = 0;

            // Creates a new NumOfQ form
            using (NumOfQ newForm = new NumOfQ())
            {
                // Hides the Qna form
                this.Hide();

                // Displays the new NumOfQ form and if the NumOfQ form is closed without confirming the number of questions they want to answer, the user is redirected back to the main menu form
                if (newForm.ShowDialog() == DialogResult.OK)
                {
                    // Initialises the integer variable numOfQVal to the number of questions that are to appear
                    numOfQVal = newForm.numOfQVal;

                    // Releases all resources used by the new form
                    newForm.Dispose();

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



                            // Randomly selects a question from the database and stores it in the object
                            activity.weight(connection);

                            // Extracts the question from the object and displays it on the question_lbl
                            question_lbl.Text = activity.getQuestion();



                            // Closes the connection to the database
                            connection.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error:\n\n" + ex);
                    }
                }
                else this.Close();
            }
        }

        // Method that checks the user's answer
        private void check_btn_Click(object sender, EventArgs e)
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



                    if (answer_textbox.Text != "")
                    {
                        // Increments the field numberOfAttempts in the database
                        if (!Program.guest) activity.incAttempts(connection);

                        // Declares a new list called keyWords and initialises it with the keywords in the database
                        List<string> keyWords = activity.getKeyWords();

                        // Declares a new integer variable called numOfCorrKW that stores the number of correct keywords the user has used
                        int numOfCorrKW = 0;

                        // Iterates through the list and counts how many keywords are used in the user's answer
                        foreach (string item in keyWords)
                        {
                            if (answer_textbox.Text.ToUpper().Contains(item))
                            {
                                numOfCorrKW++;
                            }
                        }

                        if (numOfCorrKW / activity.getNumOfKW() == 1)
                        {
                            // Increments the field numberOfCorrectAttempts in the database
                            if (!Program.guest) activity.incCorrectAttempts(connection);

                            // Increments the integer variable numOfQAnsCorr
                            numOfQAnsCorr++;

                            // Tells the user they have used all of the keywords
                            MessageBox.Show("All keywords have been used");

                            // Displays a message box with the model answer
                            MessageBox.Show(string.Format("The model answer:\n\n{0}", activity.getAnswer()));

                            // Makes the next button visible
                            next_btn.Show();

                            // Makes sure both the skip button and check button are hidden
                            if (skip_btn.Visible == true) skip_btn.Hide();
                            check_btn.Hide();
                        }
                        else
                        {
                            // Tells the user they answered incorrectly
                            MessageBox.Show(string.Format("{0} keywords have been used out of a possible {1}, please try again", numOfCorrKW.ToString(), activity.getNumOfKW().ToString()));

                            // Makes the skip button visible after the user answers the question incorrectly and the skip button is not visible
                            if (skip_btn.Visible == false) skip_btn.Show();
                        }
                    }
                    else
                    {
                        // Tells the user to enter an answer
                        MessageBox.Show("Please enter an answer");
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

        // Method that selects another question
        private void next_btn_Click(object sender, EventArgs e)
        {
            if (nextQNo <= numOfQVal)
            {
                // Hides the next button
                next_btn.Hide();

                // Shows the check button
                check_btn.Show();

                // Tries to create a connection to the database and otherwise catches the error and tells the user what the error is
                try
                {
                    // Declaring the connection
                    using (SqlConnection connection = new SqlConnection())
                    {

                        // This allows us to connect to SQL Server
                        connection.ConnectionString = Program.connectionString;

                        // Clears the answer textbox
                        answer_textbox.Clear();

                        // Opens the connection to the database
                        connection.Open();



                        // Randomly selects a question from the database and stores it in the object
                        activity.weight(connection);

                        // Extracts the question from the object and displays it on the question_lbl
                        question_lbl.Text = activity.getQuestion();



                        // Closes the connection to the database
                        connection.Close();

                        // Increments nextQNo
                        nextQNo++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n\n" + ex);
                }
            }
            else
            {
                // Tells the user how many questions they managed to find an answer for
                MessageBox.Show(string.Format("End of activity, you scored:\n{0}/{1}\n(The number of questions where you wrote all the required keywords)", numOfQAnsCorr.ToString(), numOfQVal.ToString()));
                this.Close();
            }
        }

        // Method that skips the question
        private void skip_btn_Click(object sender, EventArgs e)
        {
            // Displays a message box with the correct answer
            MessageBox.Show(string.Format("The correct answer was:\n\n{0}", activity.getAnswer()));

            if (nextQNo <= numOfQVal)
            {
                // Hides the skip button
                skip_btn.Hide();

                // Shows the check button
                check_btn.Show();

                // Tries to create a connection to the database and otherwise catches the error and tells the user what the error is
                try
                {
                    // Declaring the connection
                    using (SqlConnection connection = new SqlConnection())
                    {

                        // This allows us to connect to SQL Server
                        connection.ConnectionString = Program.connectionString;

                        // Clears the answer textbox
                        answer_textbox.Clear();

                        // Opens the connection to the database
                        connection.Open();



                        // Randomly selects a question from the database and stores it in the object
                        activity.weight(connection);

                        // Extracts the question from the object and displays it on the question_lbl
                        question_lbl.Text = activity.getQuestion();



                        // Closes the connection to the database
                        connection.Close();

                        // Increments nextQNo
                        nextQNo++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n\n" + ex);
                }
            }
            else
            {
                // Tells the user how many questions they managed to find an answer for
                MessageBox.Show(string.Format("End of activity, you scored:\n{0}/{1}\n(The number of questions where you wrote all the required keywords)", numOfQAnsCorr.ToString(), numOfQVal.ToString()));
                this.Close();
            }
        }

        // Method that closes the WordedQ form and displays the main menu form to the user
        private void homeIcon_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}