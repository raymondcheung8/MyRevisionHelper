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
    public partial class MainMenu : Form
    {        
        public MainMenu()
        {
            InitializeComponent();
        }

        // Main method
        private void MainMenu_Load(object sender, EventArgs e)
        {
            // CONNECTION TEMPLATE
            /*
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



                    // Creates a new object called command that can allow SQL code to be run
                    using (SqlCommand command = new SqlCommand())
                    {
                        // Initialises the connection
                        command.Connection = connection;

                    }



                    // Closes the connection to the database
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n\n" + ex);
            }
            */

            // Setting button colours
            qna_btn.BackColor = Color.LightGreen;
            qna_btn.FlatStyle = FlatStyle.Flat;
            qna_btn.FlatAppearance.BorderColor = Color.Green;
            wordedQ_btn.BackColor = Color.LightGreen;
            wordedQ_btn.FlatStyle = FlatStyle.Flat;
            wordedQ_btn.FlatAppearance.BorderColor = Color.Green;
            mathQ_btn.BackColor = Color.LightGreen;
            mathQ_btn.FlatStyle = FlatStyle.Flat;
            mathQ_btn.FlatAppearance.BorderColor = Color.Green;
            timedMC_btn.BackColor = Color.LightGreen;
            timedMC_btn.FlatStyle = FlatStyle.Flat;
            timedMC_btn.FlatAppearance.BorderColor = Color.Green;
            create_btn.BackColor = Color.LightBlue;
            create_btn.FlatStyle = FlatStyle.Flat;
            create_btn.FlatAppearance.BorderColor = Color.Blue;
            notes_btn.BackColor = Color.LightYellow;
            notes_btn.FlatStyle = FlatStyle.Flat;
            notes_btn.FlatAppearance.BorderColor = Color.Yellow;
            break_btn.BackColor = Color.LightSalmon;
            break_btn.FlatStyle = FlatStyle.Flat;
            break_btn.FlatAppearance.BorderColor = Color.Orange;
            exit_btn.BackColor = Color.LightCoral;
            exit_btn.FlatStyle = FlatStyle.Flat;
            exit_btn.FlatAppearance.BorderColor = Color.Red;
            login_btn.BackColor = Color.LightSkyBlue;
            login_btn.FlatStyle = FlatStyle.Flat;
            login_btn.FlatAppearance.BorderColor = Color.DeepSkyBlue;

            // Setting form colour
            this.BackColor = Color.White;

            // Hides all the buttons except the login button and exit button
            qna_btn.Hide();
            wordedQ_btn.Hide();
            mathQ_btn.Hide();
            timedMC_btn.Hide();
            create_btn.Hide();
            notes_btn.Hide();
            break_btn.Hide();
        }

        // Method for what happens when login_btn is clicked
        private void login_btn_Click(object sender, EventArgs e)
        {
            // Creates a new login form
            using (Login newForm = new Login())
            {

                // Hides the main menu form
                this.Hide();

                // Displays the new login form
                if (newForm.ShowDialog() == DialogResult.OK)
                {
                    // Shows all the regular buttons
                    qna_btn.Show();
                    wordedQ_btn.Show();
                    mathQ_btn.Show();
                    timedMC_btn.Show();
                    break_btn.Show();

                    // Shows the notes button if the user isn't a guest
                    if (!Program.guest) notes_btn.Show();

                    // Hides the login button
                    login_btn.Hide();

                    // Shows the create button if the user is an admin
                    if (newForm.admin) create_btn.Show();

                    // Releases all resources used by the new form
                    newForm.Dispose();

                    // Inserts some data into tblUserAnswers
                    /*
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



                            // Creates a new object called command that can allow SQL code to be run
                            using (SqlCommand command = new SqlCommand())
                            {
                                // Initialises the connection
                                command.Connection = connection;

                                // A SQL query that inserts some data into tblUserAnswers
                                command.CommandText = @"
    INSERT INTO tblUserAnswers (userID, questionID, numberOfAttempts, numberOfCorrectAttempts)
    VALUES (@userID, 1, 16, 5);

    INSERT INTO tblUserAnswers (userID, questionID, numberOfAttempts, numberOfCorrectAttempts)
    VALUES (@userID, 2, 11, 4);

    INSERT INTO tblUserAnswers (userID, questionID, numberOfAttempts, numberOfCorrectAttempts)
    VALUES (@userID, 3, 15, 14);

    INSERT INTO tblUserAnswers (userID, questionID, numberOfAttempts, numberOfCorrectAttempts)
    VALUES (@userID, 4, 4, 1);

    INSERT INTO tblUserAnswers (userID, questionID, numberOfAttempts, numberOfCorrectAttempts)
    VALUES (@userID, 5, 5, 3);

    INSERT INTO tblUserAnswers (userID, questionID, numberOfAttempts, numberOfCorrectAttempts)
    VALUES (@userID, 6, 8, 4);

    INSERT INTO tblUserAnswers (userID, questionID, numberOfAttempts, numberOfCorrectAttempts)
    VALUES (@userID, 7, 7, 6);

    INSERT INTO tblUserAnswers (userID, questionID, numberOfAttempts, numberOfCorrectAttempts)
    VALUES (@userID, 8, 12, 1);

    INSERT INTO tblUserAnswers (userID, questionID, numberOfAttempts, numberOfCorrectAttempts)
    VALUES (@userID, 9, 15, 0);

    INSERT INTO tblUserAnswers (userID, questionID, numberOfAttempts, numberOfCorrectAttempts)
    VALUES (@userID, 10, 20, 0);

    INSERT INTO tblUserAnswers (userID, questionID, numberOfAttempts, numberOfCorrectAttempts)
    VALUES (@userID, 11, 15, 15);

    INSERT INTO tblUserAnswers (userID, questionID, numberOfAttempts, numberOfCorrectAttempts)
    VALUES (@userID, 12, 20, 20);

    INSERT INTO tblUserAnswers (userID, questionID, numberOfAttempts, numberOfCorrectAttempts)
    VALUES (@userID, 13, 0, 0);

    INSERT INTO tblUserAnswers (userID, questionID, numberOfAttempts, numberOfCorrectAttempts)
    VALUES (@userID, 14, 20, 2);

    INSERT INTO tblUserAnswers (userID, questionID, numberOfAttempts, numberOfCorrectAttempts)
    VALUES (@userID, 15, 24, 1);

    INSERT INTO tblUserAnswers (userID, questionID, numberOfAttempts, numberOfCorrectAttempts)
    VALUES (@userID, 16, 0, 0);
    ";
                                command.Parameters.AddWithValue("@userID", Program.userID);

                                // Runs the SQL code
                                command.ExecuteNonQuery();
                            
                                // Clears the parameters
                                command.Parameters.Clear();
                            }



                            // Closes the connection to the database
                            connection.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error:\n\n" + ex);
                    }
                    */

                    if (!Program.guest)
                    {
                        // This makes sure questions have been initialised for the current user in tblUserAnswers and otherwise initialises the tables for them
                        // (This is basically a fail-safe to make sure I won't get any errors relating to questions not being initialised for the current user)
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



                                // Creates a new object called command that can allow SQL code to be run
                                using (SqlCommand command = new SqlCommand())
                                {
                                    // Initialises the connection
                                    command.Connection = connection;

                                    // A SQL query that makes sure questions have been initialised for the current user in tblUserAnswers and otherwise initialises the tables for them
                                    // (This is basically a fail-safe to make sure I won't get any errors relating to questions not being initialised for the current user)
                                    command.CommandText = @"
INSERT tblUserAnswers
SELECT @userID, Q.questionID, 0, 0, NULL, NULL, NULL, NULL
FROM   tblQuestions Q
WHERE  NOT EXISTS (SELECT 1
                   FROM   tblUserAnswers A
				   WHERE  A.questionID = Q.questionID
				   AND    A.userID     = @userID);
";
                                    command.Parameters.AddWithValue("@userID", Program.userID);

                                    // Runs the SQL code
                                    command.ExecuteNonQuery();

                                    // Clears the parameters
                                    command.Parameters.Clear();
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

                // Makes the main menu form visible again
                this.Show();
            }
        }

        // Method for what happens when qna_btn is clicked
        private void qna_btn_Click(object sender, EventArgs e)
        {
            // Display a new form and hide the main menu
            using (Qna newForm = new Qna())
            {
                openNewForm(newForm);
            }
        }

        // Method for what happens when wordedQ_btn is clicked
        private void wordedQ_btn_Click(object sender, EventArgs e)
        {
            // Display a new form and hide the main menu
            using (WordedQ newForm = new WordedQ())
            {
                openNewForm(newForm);
            }
        }

        // Method for what happens when mathQ_btn is clicked
        private void mathQ_btn_Click(object sender, EventArgs e)
        {
            // Display a new form and hide the main menu
            using (MathQ newForm = new MathQ())
            {
                openNewForm(newForm);
            }
        }

        // Method for what happens when timedMC_btn is clicked
        private void timedMC_btn_Click(object sender, EventArgs e)
        {
            // Display a new form and hide the main menu
            using (TimedMC newForm = new TimedMC())
            {
                openNewForm(newForm);
            }
        }

        // Method for what happens when create_btn is clicked
        private void create_btn_Click(object sender, EventArgs e)
        {
            // Display a new form and hide the main menu
            using (Create newForm = new Create())
            {
                openNewForm(newForm);
            }
        }

        // Method for what happens when notes_btn is clicked
        private void notes_btn_Click(object sender, EventArgs e)
        {
            // Display a new form and hide the main menu
            using (Notes newForm = new Notes())
            {
                openNewForm(newForm);
            }
        }

        // Method for what happens when break_btn is clicked
        private void break_btn_Click(object sender, EventArgs e)
        {
            // Display a new form and hide the main menu
            using (Break newForm = new Break())
            {
                openNewForm(newForm);
            }
        }

        // Method for what happens when exit_btn is clicked
        private void exit_btn_Click(object sender, EventArgs e)
        {
            // Display a suitable message and closes the main menu, ending the program
            MessageBox.Show("Thanks for using my software\n-Raymond Cheung");
            this.Close();
        }

        // A procedure that opens a new form when the parameter is an existing form
        private void openNewForm(Form newForm)
        {
            // Redirects the user to the new form and then back to the main menu form when the new form is closed
            this.Hide();
            newForm.ShowDialog();
            this.Show();

            // Releases all resources used by the new form
            newForm.Dispose();
        }
    }
}