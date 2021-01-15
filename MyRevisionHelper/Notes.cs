using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

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
            // Tries to create a connection to the database and otherwise catches the error and tells the user what the error is
            try
            {
                // Declaring the connection
                using (OleDbConnection connection = new OleDbConnection())
                {

                    // This allows us to connect to Access 2007
                    connection.ConnectionString = Program.connectionString;

                    // Opens the connection to the database
                    connection.Open();



                    // Creates a new object called command that can allow SQL code to be run
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        command.Connection = connection;

                        // Calls upon the getTableExists() function and will create a new table called tblNotes if there isn't already an existing one
                        if (!getTableExists(connection, "tblNotes"))
                        {
                            // A SQL query that creates a new table called "tblNotes" where "noteID" is the primary key
                            command.CommandText = @"
CREATE TABLE tblNotes
(
noteID          CHAR(36)    NOT NULL    PRIMARY KEY,
insertDate      DATETIME    NOT NULL,
noteDescr       MEMO        NOT NULL,
userID          CHAR(36)    NOT NULL    REFERENCES  tblUsers (userID)
);
";
                            // Runs the SQL code
                            command.ExecuteNonQuery();
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

        // Method that closes the login form and displays the main menu form to the user
        private void homeIcon_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Method that redirects the user to a form where they can add notes
        private void addNote_btn_Click(object sender, EventArgs e)
        {
            AddNote newForm = new AddNote();
            this.Hide();
            newForm.ShowDialog();
            this.Show();
        }

        // Method that redirects the user to a form where they can check notes
        private void checkNotes_btn_Click(object sender, EventArgs e)
        {
            CheckNotes newForm = new CheckNotes();
            this.Hide();
            newForm.ShowDialog();
            this.Show();
        }

        // Function that returns whether a certain table exists
        private bool getTableExists(OleDbConnection connection, string tableName)
        {
            return Program.getTableExists(connection, tableName);
        }
    }
}
