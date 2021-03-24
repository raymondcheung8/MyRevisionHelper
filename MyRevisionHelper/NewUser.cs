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
    public partial class NewUser : Form
    {
        // A global string variable for newUserID is created so this value can be used by other forms
        public string newUserID { get; set; }

        public NewUser()
        {
            InitializeComponent();
        }
        
        // Main method
        private void NewUser_Load(object sender, EventArgs e)
        {
            // THIS IS A TEST TO SEE IF THE getUserID() FUNCTION WORKS
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

                        // Calls the getUserID() function and shows the returned string in a message box
                        MessageBox.Show(getUserID(connection));
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
        }

        // Method for what happens when the goBack_lbl is clicked
        private void goBack_lbl_Click(object sender, EventArgs e)
        {
            // Closes this form, allowing the user to go back to the login form
            this.Close();
        }

        // Method for what happens when the confirm button is clicked
        private void confirm_btn_Click(object sender, EventArgs e)
        {
            // Runs the code if both the username textbox and password textbox aren't ""
            if (username_textbox.Text != "" && password_textbox.Text != "")
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

                            // A SQL query that selects the username the user just typed in from tblUsers
                            command.CommandText = @"
SELECT username
FROM tblUsers
WHERE username = @username;
";
                            command.Parameters.AddWithValue("@username", username_textbox.Text);

                            // Runs the SQL code and stores the generated table in the reader
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                // An if statement that tells the user their username has already been taken if it is on tblUsers and otherwise adds the user to tblUsers
                                if (reader.HasRows)
                                {
                                    MessageBox.Show("Username is already taken, please choose another");
                                }
                                else
                                {
                                    // Disposes of the reader since I have no further use of it
                                    reader.Dispose();

                                    // Clears the parameters
                                    command.Parameters.Clear();

                                    // Generates a random salt
                                    byte[] salt = new byte[UserLogin.saltLength];
                                    salt = getSalt();

                                    // A SQL query that inserts a new record into tblUsers
                                    command.CommandText = @"
INSERT INTO tblUsers
(userID, username, hPassword, salt, userAdmin)
VALUES (@userID, @username, @hPassword, @salt, 0);
";
                                    // A test to see what the values of userID and username are before they are inserted into tblUsers
                                    // MessageBox.Show(getUserID(connection));
                                    // MessageBox.Show(username_textbox.Text);

                                    command.Parameters.AddWithValue("@userID", getUserID(connection));
                                    command.Parameters.AddWithValue("@username", username_textbox.Text);
                                    command.Parameters.AddWithValue("@hPassword", getSaltedHash(password_textbox.Text, salt));
                                    command.Parameters.AddWithValue("@salt", salt);

                                    // Runs the SQL code
                                    command.ExecuteNonQuery();

                                    // Clears the parameters
                                    command.Parameters.Clear();

                                    // A message box will appear to tell the user they have been added to the list of users
                                    MessageBox.Show("Your account has been successfully created");

                                    // Closes this form, allowing the user to go back to the login form
                                    this.Close();
                                }
                            }
                        }



                        // Closes the connection to the database
                        DialogResult = DialogResult.OK;
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

        // Function that returns the salted hash
        private byte[] getSaltedHash(string password, byte[] salt)
        {
            // Calls the function at application scope
            return UserLogin.getSaltedHash(password, salt);
        }

        // Function that returns a random byte array (the salt)
        private byte[] getSalt()
        {
            // Calls the function at application scope
            return UserLogin.getSalt();
        }
        
        // A function that returns the new user's userID and sets the global string variable newUserID to the new userID
        private string getUserID(SqlConnection connection)
        {
            // Calls the function getUserID() at application scope and stores it in the string variable newUserID
            newUserID = UserLogin.getUserID(connection);

            // Calls the function at application scope
            return newUserID;

            // MY OLD IMPLEMENTATION OF getUserID()
            /*
            // Opens a new connection if there isn't already a connection open (this just makes sure an error relating to no connection being open won't occur)
            if (connection.State != ConnectionState.Open) connection.Open();

            // Creates a new object called command that can allow SQL code to be run
            using (SqlCommand command = new SqlCommand())
            {
                // Initialises the connection
                command.Connection = connection;

                // A SQL query that gets the number of existing users in the table already
                command.CommandText = string.Format(@"
SELECT COUNT(userID) AS NumberOfExistingUsers FROM tblUsers;
");

                // Runs the SQL code and stores the generated table in the reader
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Reads the reader and generates a value for the userID
                    string userID = "";
                    while (reader.Read())
                    {
                        userID = reader.GetInt32(0).ToString();
                    }

                    // Tells me if there is an error with the code or the table
                    if (userID == "") MessageBox.Show("Error:\n\nReader not read");

                    // Returns the userID
                    return userID;
                }
            }
            */
        }
    }
}