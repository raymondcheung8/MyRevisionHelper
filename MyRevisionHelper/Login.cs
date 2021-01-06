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
    public partial class Login : Form
    {
        // A global boolean variable for admin is created so this value can be used by other forms
        public bool admin { get; set; }

        public Login()
        {
            InitializeComponent();

            // Admin is initialised to false
            admin = false;
        }

        // Main method
        private void Login_Load(object sender, EventArgs e)
        {
            /*
            // A TEST FOR A METHOD TO STORE THE SALT
            test();

            // A SQL QUERY TO DROP tblTest
            using (OleDbConnection connection = new OleDbConnection())
            {
                connection.ConnectionString = Program.connectionString;
                connection.Open();
                using (OleDbCommand command = new OleDbCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "DROP TABLE tblTest;";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            */

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

                    // This tests the connection with the database
                    // MessageBox.Show("Connection Successful");

                    /*
                    // CODE BELOW RESETS THE TABLE
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "DROP TABLE tblUsers;";
                        command.ExecuteNonQuery();
                    }
                    */



                    // Creates a new object called command that can allow SQL code to be run
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        command.Connection = connection;

                        // Calls upon the getTableExists() function and will create a new table called tblUsers if there isn't already an existing one
                        if (!getTableExists(connection, "tblUsers"))
                        {
                            // A SQL query that creates a new table called "tblUsers" where "userID" is the primary key
                            command.CommandText = @"
CREATE TABLE tblUsers
(
userID          CHAR(36)    NOT NULL    PRIMARY KEY,
username        VARCHAR(64) NOT NULL,
hPassword       LONGBINARY  NOT NULL,
salt            LONGBINARY  NOT NULL,
admin           BIT         NOT NULL
);
";
                            // Runs the SQL code
                            command.ExecuteNonQuery();

                            // Generates a random salt
                            byte[] salt = new byte[UserLogin.saltLength];
                            salt = getSalt();

                            // Generates a salted hash of the password
                            byte[] hPassword = getSaltedHash("abcde", salt);

                            // TEST FOR AN OLD VERSION OF THE CODE THAT DIDN'T WORK
                            // MessageBox.Show(Encoding.UTF8.GetString(salt));
                            // MessageBox.Show(hPassword);

                            // A SQL query that inserts an admin user into the new table.
                            command.CommandText = string.Format(@"
INSERT INTO tblUsers
VALUES (@userID, 'admin', @hPassword, @salt, 1);
");
                            command.Parameters.AddWithValue("@userID", getUserID(connection));
                            command.Parameters.AddWithValue("@hPassword", hPassword);
                            command.Parameters.AddWithValue("@salt", salt);

                            // Runs the SQL code
                            command.ExecuteNonQuery();

                            // Clears the parameters
                            command.Parameters.Clear();
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

        /*
        // A TEST TO FIND THE BEST METHOD TO STORE THE SALT
        private void test()
        {
            using (OleDbConnection connection = new OleDbConnection())
            {
                // This allows us to connect to Access 2007
                connection.ConnectionString = Program.connectionString;

                // Opens a new connection if there isn't already a connection open (this just makes sure an error relating to no connection being open won't occur)
                if (connection.State != ConnectionState.Open) connection.Open();

                // Creates a new object called command that can allow SQL code to be run
                using (OleDbCommand command = new OleDbCommand())
                {
                    // Initialises the connection
                    command.Connection = connection;

                    // A SQL query that creates a new table called tblTest
                    command.CommandText = @"
CREATE TABLE tblTest
(
salt    LONGBINARY  NOT NULL
)
";
                    command.ExecuteNonQuery();

                    // A SQL query that inserts a byte array into the table
                    command.CommandText = @"
INSERT INTO tblTest
VALUES (@salt)
";
                    byte[] salt = getSalt();
                    command.Parameters.AddWithValue("@salt", salt);
                    command.ExecuteNonQuery();
                    
                    // Clears the parameters
                    command.Parameters.Clear();

                    // A SQL query that selects the saltValue from tblTest
                    command.CommandText = @"
SELECT saltValue
FROM tblTest;
";

                    // Runs the SQL code and stores the generated table in the reader
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        // Tries to find a match for username and password in tblUsers and returns true if found and false if not found
                        while (reader.Read())
                        {
                            byte[] salt2 = new byte[UserLogin.saltLength];
                            reader.GetBytes(0, 0, salt2, 0, UserLogin.saltLength);

                            if (BitConverter.ToString(salt) == BitConverter.ToString(salt2))
                            {
                                MessageBox.Show("yes");
                            }

                            MessageBox.Show(BitConverter.ToString(salt));
                            MessageBox.Show(BitConverter.ToString(salt2));
                        }
                    }
                }
            }
        }
        */

        // Function that returns the salted hash
        private byte[] getSaltedHash(string password, byte[] salt)
        {
            // Calls the function with application scope
            return UserLogin.getSaltedHash(password, salt);
        }

        // Function that returns a random byte array (the salt)
        private byte[] getSalt()
        {
            // Calls the function with application scope
            return UserLogin.getSalt();
        }

        // A function that returns the new user's userID
        private string getUserID(OleDbConnection connection)
        {
            // Calls the function at application scope
            return UserLogin.getUserID(connection);
        }

        // Method that closes the login form and displays the main menu form to the user
        private void homeIcon_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Function that returns whether a certain table exists
        private bool getTableExists(OleDbConnection connection, string tableName)
        {
            // Declares a new datatable that will contain all of the tables
            DataTable tableSchema;

            // Declares a new datatable that will contain the table that is being searched for
            DataRow[] myTable;

            // Opens a new connection if there isn't already a connection open (this just makes sure an error relating to no connection being open won't occur)
            if (connection.State != ConnectionState.Open) connection.Open();

            // Copies all the tables on the database to a local datatable
            tableSchema = connection.GetSchema("TABLES");

            // Copies all the tables that have the same name as the table we are searching for to a datarow array
            myTable = tableSchema.Select(string.Format("TABLE_NAME='{0}'", tableName));

            // Returns false if the datarow is empty and otherwise returns true
            if (myTable.Length == 0)
            {
                // Test to see if it will show the correct message when table doesn't exist
                //MessageBox.Show("TABLE DOESN'T EXIST");

                return false;
            }
            else
            {
                // Test to see if it will show the correct message when table doesn't exist
                //MessageBox.Show("TABLE EXISTS");

                return true;
            }

            // THE BELOW CODE DOESN'T WORK AS IT THROWS A PERMISSION ERROR FOR THE TABLE 'MSysObjects'
            /*
            // A SQL query that finds if a certain table already exists
            // Type 1, Type 4, Type 6 in MSysObjects are the user created tables
            // type 1 = Table - Local Access Tables
            // type 4 = Table - Linked ODBC Tables
            // type 6 = Table - Linked Access Tables
            command.CommandText = string.Format(@"
SELECT MSysObjects.Name
FROM MSysObjects
WHERE
MSysObjects.type In (1,4,6)
AND MSysObjects.Name = '{0}'
", tableName);

            // Runs the SQL code and stores the result in reader
            OleDbDataReader reader = command.ExecuteReader();

            // Returns the boolean value of whether or not reader has rows
            return reader.HasRows;
             */
        }

        // Method that opens a new form that allows the user to create a new account
        private void newLoginLbl_Click(object sender, EventArgs e)
        {
            NewUser newForm = new NewUser();
            this.Hide();
            newForm.ShowDialog();
            this.Show();
        }

        // Method that authenticates the user's login details
        private void login_btn_Click(object sender, EventArgs e)
        {
            // Runs the code if both the username textbox and password textbox aren't ""
            if (username_textbox.Text != "" && password_textbox.Text != "")
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



                        // Creates a new boolean variable called authenticated and calls upon the function, getIsAuthenticated() to see if the details the user has inputted are correct
                        bool authenticated = getIsAuthenticated(connection, username_textbox.Text, password_textbox.Text);

                        // Redirects the user back to the main menu if the details they entered match the one in the database and otherwise prompts the user to re-enter their details
                        if (authenticated)
                        {
                            DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Either the username or password is incorrect");
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
                MessageBox.Show("Please enter both your username and password");
            }
        }

        // Function that returns a boolean value depending on whether or not the user's details match the details in tblUsers
        private bool getIsAuthenticated(OleDbConnection connection, string username, string password)
        {
            // Opens a new connection if there isn't already a connection open (this just makes sure an error relating to no connection being open won't occur)
            if (connection.State != ConnectionState.Open) connection.Open();

            // Creates a new object called command that can allow SQL code to be run
            using (OleDbCommand command = new OleDbCommand())
            {
                // Initialises the connection
                command.Connection = connection;

                // A SQL query that authenticates the user and decides whether or not the user is an admin
                command.CommandText = @"
SELECT *
FROM tblUsers
WHERE username = @username;
";
                command.Parameters.AddWithValue("@username", username_textbox.Text);

                // Runs the SQL code and stores the generated table in the reader
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    // Creates a new boolean variable called match, which is initialised to false
                    bool match = false;
                    
                    // Tries to find a match for username and password in tblUsers and returns true if found and false if not found
                    while (reader.Read())
                    {
                        // TEST FOR AN OLD VERSION OF THE CODE THAT DIDN'T WORK
                        // MessageBox.Show(reader.GetString(2));
                        // MessageBox.Show(getSaltedHash(password, Encoding.UTF8.GetBytes(reader.GetString(2))));
                        // MessageBox.Show(getSaltedHash(password, salt));

                        // Initialises two new byte arrays that will store the values of hPassword and salt from tblUsers
                        byte[] hPassword = new byte[UserLogin.hPasswordLength];
                        byte[] salt = new byte[UserLogin.saltLength];
                        reader.GetBytes(2, 0, hPassword, 0, UserLogin.hPasswordLength);
                        reader.GetBytes(3, 0, salt, 0, UserLogin.saltLength);

                        // Compares the the username entered with the one in the database and compares the hex value of the hashed password entered with the hex value of the hashed password stored in the database
                        if (reader.GetString(1) == username && BitConverter.ToString(hPassword) == BitConverter.ToString(getSaltedHash(password, salt)))
                        {
                            match = true;

                            // Sets the global variable userID to be the userID of the user
                            Program.userID = reader.GetString(0);

                            // Sets the boolean variable admin to true if the user is an admin
                            if (reader.GetBoolean(4) == true) admin = true;
                        }
                    }

                    // Clears the parameters
                    command.Parameters.Clear();

                    // Returns a boolean value
                    return match;
                }
            }
        }

        // Method that sets the global boolean variable guest to true and redirects the user to the main menu form
        private void guestLbl_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Program.guest = true;
            this.Close();
        }
    }
}