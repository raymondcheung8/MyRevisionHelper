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
    public partial class CheckNotes : Form
    {
        // Declares two new arrays that store the noteIDs and notes respectively
        private string[] noteIDArray;
        private string[] noteArray;

        // Declares a new integer variable that holds index of the note the user is looking at
        private int index = 0;

        // Declares a new integer variable that holds the number of records in the table and initialises it as 0
        private int numberOfRecords = 0;

        public CheckNotes()
        {
            InitializeComponent();
        }

        // Main method
        private void CheckNotes_Load(object sender, EventArgs e)
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



                    // Creates a new object called command that can allow SQL code to be run
                    using (SqlCommand command = new SqlCommand())
                    {
                        // Initialises the connection
                        command.Connection = connection;

                        // A SQL query that checks the length of the table tblNotes
                        command.CommandText = @"
SELECT count(noteID)
FROM tblNotes
WHERE userID = @userID;
";
                        command.Parameters.AddWithValue("@userID", Program.userID);

                        // Stores the number of records in the table in the variable numberOfRecords
                        numberOfRecords = (int)command.ExecuteScalar();

                        // Clears the parameters
                        command.Parameters.Clear();

                        // An if statement that tells the user to first add notes before using this feature and closes the form if there are no notes currently on the database or otherwise stores the notes from the table in an array
                        if (numberOfRecords == 0)
                        {
                            MessageBox.Show("Please make some notes before using this feature");
                            this.Close();
                        }
                        else
                        {
                            // A SQL query that selects the notes made by the current user
                            command.CommandText = @"
SELECT noteID, noteDescr
FROM tblNotes
WHERE userID = @userID
ORDER BY insertDate ASC;
";
                            command.Parameters.AddWithValue("@userID", Program.userID);

                            // Runs the SQL code and stores the generated table in the reader
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                // Initialises the two arrays and sets the length of both as the number of records there are in the table
                                noteIDArray = new string[numberOfRecords];
                                noteArray = new string[numberOfRecords];

                                // Declares the integer variable count and initialises it as 0
                                int count = 0;

                                while (reader.Read())
                                {
                                    // Stores the ID of the note in the array noteID
                                    noteIDArray[count] = reader.GetString(0);

                                    // Stores the note in the array noteArray
                                    noteArray[count] = reader.GetString(1);

                                    // Increments count
                                    count++;
                                }

                                // Displays the note at a certain index
                                oldNote.Text = noteArray[index];

                                // Clears the parameters
                                command.Parameters.Clear();
                            }
                        }
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

        // Method for what happens when the goBack_lbl is clicked
        private void goBack_lbl_Click(object sender, EventArgs e)
        {
            // Closes this form, allowing the user to go back to the notes form
            this.Close();
        }

        // Method for what happens when the save note button is pressed
        private void saveNote_btn_Click(object sender, EventArgs e)
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



                    // Creates a new object called command that can allow SQL code to be run
                    using (SqlCommand command = new SqlCommand())
                    {
                        // Initialises the connection
                        command.Connection = connection;

                        // A SQL query that updates the table tblNotes in the database
                        command.CommandText = @"
UPDATE tblNotes
SET insertDate = GETDATE(), noteDescr = @noteDescr
WHERE noteID = @noteID;
";
                        command.Parameters.AddWithValue("@noteDescr", oldNote.Text);
                        command.Parameters.AddWithValue("@noteID", noteIDArray[index]);

                        // Runs the SQL code
                        command.ExecuteNonQuery();

                        // A message box will appear, telling the user the note has been updated and then the user will be redirected to the notes form
                        MessageBox.Show("Note has been updated");
                        this.Close();

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

        // Method for what happens when the previous note button is pressed
        private void previousNote_btn_Click(object sender, EventArgs e)
        {
            if (index > 0)
            {
                index--;
                oldNote.Text = noteArray[index];
            }
            else MessageBox.Show("There is no note before this");
        }

        // Method for what happens when the next note button is pressed
        private void nextNote_btn_Click(object sender, EventArgs e)
        {
            if (index < numberOfRecords - 1)
            {
                index++;
                oldNote.Text = noteArray[index];
            }
            else MessageBox.Show("There is no note after this");
        }

        private void deleteNote_btn_Click(object sender, EventArgs e)
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



                    // Creates a new object called command that can allow SQL code to be run
                    using (SqlCommand command = new SqlCommand())
                    {
                        // Initialises the connection
                        command.Connection = connection;

                        // A SQL query that deletes the note from the table
                        command.CommandText = @"
DELETE FROM tblNotes
WHERE noteID = @noteID;
";
                        command.Parameters.AddWithValue("@noteID", noteIDArray[index]);

                        // Runs the SQL code
                        command.ExecuteNonQuery();

                        // A message box will appear, telling the user the note has been deleted and then the user will be redirected to the notes form
                        MessageBox.Show("Note has been deleted");
                        this.Close();

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
}