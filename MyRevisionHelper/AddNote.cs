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
    public partial class AddNote : Form
    {
        public AddNote()
        {
            InitializeComponent();
        }

        // Main method
        private void AddNote_Load(object sender, EventArgs e)
        {
            
        }

        // Method for what happens when the goBack_lbl is clicked
        private void goBack_lbl_Click(object sender, EventArgs e)
        {
            // Closes this form, allowing the user to go back to the notes form
            this.Close();
        }

        // Method for storing the note in the database when the store note button is pressed
        private void storeNote_btn_Click(object sender, EventArgs e)
        {
            // Runs the code if the textbox newNote isn't ""
            if (newNote.Text != "")
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

                            // A SQL query that inserts a new record into tblNotes
                            command.CommandText = @"
INSERT INTO tblNotes
(noteID, insertDate, noteDescr, userID)
VALUES (@noteID, GETDATE(), @noteDescr, @userID);
";
                            command.Parameters.AddWithValue("@noteID", getNoteID(connection));
                            command.Parameters.AddWithValue("@noteDescr", newNote.Text);
                            command.Parameters.AddWithValue("@userID", Program.userID);

                            // Runs the SQL code
                            command.ExecuteNonQuery();

                            // Clears the parameters
                            command.Parameters.Clear();

                            // A message box will appear to tell the user a new note has been added to the table of notes
                            MessageBox.Show("A new note has been created in the database");

                            // Closes this form, allowing the user to go back to the notes form
                            this.Close();
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
            else
            {
                MessageBox.Show("Please enter a note in the textbox");
            }
        }

        // Function that returns a random 36 character string that is used as the noteID
        private string getNoteID(SqlConnection connection)
        {
            // Initialises noteID as "ERROR"
            string noteID = "ERROR";

            // Creates a new boolean variable called validnoteID that becomes true once a unused noteID is generated
            bool validNoteID = false;
            while (!validNoteID)
            {
                // Stores a randomly generated noteID in a string variable
                noteID = Guid.NewGuid().ToString();

                // Creates a new object called command that can allow SQL code to be run
                using (SqlCommand command = new SqlCommand())
                {
                    // Initialises the connection
                    command.Connection = connection;

                    // A SQL query that selects the noteID generated from tblNotes
                    command.CommandText = @"
SELECT noteID
FROM tblNotes
WHERE noteID = @noteID;
";
                    command.Parameters.AddWithValue("@noteID", noteID);

                    // Runs the SQL code and stores the generated table in the reader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // An if statement that makes validNoteID true if the reader doesn't have rows
                        if (!reader.HasRows) validNoteID = true;

                        // Clears the parameters
                        command.Parameters.Clear();
                    }
                }
            }
            // Returns the noteID
            return noteID;
        }
    }
}