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
    // ADMIN ACCOUNT DETAILS:
    // USERNAME - admin
    // PASSWORD - abcde

    public partial class Login : Form
    {
        // A global boolean variable for admin is created so this value can be used by other forms
        public bool admin { get; set; }

        public Login()
        {
            InitializeComponent();
        }

        // Main method
        private void Login_Load(object sender, EventArgs e)
        {
            /*
            // A TEST FOR A METHOD TO STORE THE SALT
            test();

            // A SQL QUERY TO DROP tblTest
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = Program.connectionString;
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "DROP TABLE tblTest;";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            */

            // Admin is initialised to false
            admin = false;

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

                    // This tests the connection with the database
                    // MessageBox.Show("Connection Successful");

                    /*
                    // CODE BELOW RESETS THE TABLE
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "DROP TABLE tblUsers;";
                        command.ExecuteNonQuery();
                    }
                    */



                    // Creates a new object called command that can allow SQL code to be run
                    using (SqlCommand command = new SqlCommand())
                    {
                        // Initialises the connection
                        command.Connection = connection;

                        // Calls upon the getTableExists() function and will create a new table called tblUsers if there isn't already an existing one
                        if (!getTableExists(connection, "tblUsers"))
                        {
                            // A SQL query that creates a new table called "tblUsers" where "userID" is the primary key
                            command.CommandText = @"
CREATE TABLE tblUsers
(
userID          CHAR(36)        NOT NULL    PRIMARY KEY,
username        VARCHAR(32)     NOT NULL,
hPassword       BINARY(32)		NOT NULL,
salt            BINARY(32)      NOT NULL,
userAdmin       BIT             NOT NULL
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

                        /*
                        // Message boxes that test whether the function getTableExists() works
                        MessageBox.Show(Program.getTableExists(connection, "tblCounters").ToString());
                        MessageBox.Show(Program.getTableExists(connection, "tblQuestions").ToString());
                        MessageBox.Show(Program.getTableExists(connection, "tblAnswers").ToString());
                        MessageBox.Show(Program.getTableExists(connection, "tblUserAnswers").ToString());
                        */

                        // Creates a new counter table if it doesn't already exist
                        if (!Program.getTableExists(connection, "tblCounters"))
                        {
                            // A SQL query that creates a counter table
                            command.CommandText = @"
-- Creates a counter table
CREATE TABLE tblCounters
(
counterType		VARCHAR(8)		NOT NULL,
counterID		INT				NOT NULL
);

-- Initialises the two counters as 1
INSERT INTO tblCounters VALUES ('QID', 1);
INSERT INTO tblCounters VALUES ('AID', 1);
";
                            // Runs the SQL code
                            command.ExecuteNonQuery();
                        }



                        // Creates a new question table if it doesn't already exist
                        if (!Program.getTableExists(connection, "tblQuestions"))
                        {
                            // A SQL query that creates a question table
                            command.CommandText = @"
-- Creates a question table
CREATE TABLE tblQuestions
(
questionID      INT         NOT NULL    PRIMARY KEY,
question        TEXT        NOT NULL,
questionType    VARCHAR(8)  NOT NULL
);
";
                            // Runs the SQL code
                            command.ExecuteNonQuery();
                        }



                        // Creates a new answer table if it doesn't already exist
                        if (!Program.getTableExists(connection, "tblAnswers"))
                        {
                            // A SQL query that creates an answer table
                            command.CommandText = @"
-- Creates an answer table
CREATE TABLE tblAnswers
(
answerID        INT         NOT NULL    PRIMARY KEY,
answer          TEXT        NOT NULL,
answerType      VARCHAR(8)  NOT NULL,
questionID      INT         NOT NULL REFERENCES tblQuestions (questionID),
);
";
                            // Runs the SQL code
                            command.ExecuteNonQuery();
                        }



                        // Creates a new userAnswers table if it doesn't already exist
                        if (!Program.getTableExists(connection, "tblUserAnswers"))
                        {
                            // A SQL query that creates an answer table
                            command.CommandText = @"
-- Creates a userAnswer table
CREATE TABLE tblUserAnswers
(
userID						CHAR(36)    NOT NULL	REFERENCES  tblUsers (userID),
questionID					INT         NOT NULL    REFERENCES  tblQuestions (questionID),
numberOfAttempts			INT			NOT NULL,
numberOfCorrectAttempts		INT			NOT NULL,
weighting					FLOAT,
category                    INT,
specificWeight				FLOAT,
cdfPosition					FLOAT,
PRIMARY KEY (userID, questionID)
);
";
                            // Runs the SQL code
                            command.ExecuteNonQuery();

                            // Calls upon the procedure addDefaultQs() to add the default questions to the database
                            addDefaultQs(connection);
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

        // A procedure that adds questions and answers into the database
        private void createQNA(SqlConnection connection, string question, string answer)
        {
            // Opens a new connection if there isn't already a connection open (this just makes sure an error relating to no connection being open won't occur)
            if (connection.State != ConnectionState.Open) connection.Open();

            // Creates a new object called command that can allow SQL code to be run
            using (SqlCommand command = new SqlCommand())
            {
                // Initialises the connection
                command.Connection = connection;

                // A SQL query that inputs data
                command.CommandText = @"
-- Declares a new variable called @questionCount and initialises it as the value in the question counter table
DECLARE @questionCount INT;
SELECT @questionCount = MAX(counterID) FROM tblCounters WHERE counterType = 'QID';

-- Inserts a new question into the question table
INSERT INTO tblQuestions (questionID, question, questionType)
VALUES (@questionCount, @question, @questionType);

-- Increments the question counter
UPDATE C
SET counterID = counterID + 1
FROM tblCounters C
WHERE counterType = 'QID';

-- Declares a new variable called @answerCount and initialises it as the value in the answer counter table
DECLARE @answerCount INT;
SELECT @answerCount = MAX(counterID) FROM tblCounters WHERE counterType = 'AID';

-- Inserts a new answer into the answer table
INSERT INTO tblAnswers (answerID, answer, answerType, questionID)
VALUES(@answerCount, @answer, @answerType, @questionCount);

-- Increments the answer counter
UPDATE C
SET counterID = counterID + 1
FROM tblCounters C
WHERE counterType = 'AID';
";
                command.Parameters.AddWithValue("@question", question);
                command.Parameters.AddWithValue("@questionType", "QNA");
                command.Parameters.AddWithValue("@answer", answer);
                command.Parameters.AddWithValue("@answerType", "QNA");

                // Runs the SQL code
                command.ExecuteNonQuery();
            }
        }

        /*
        // A TEST TO FIND THE BEST METHOD TO STORE THE SALT
        private void test()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                // This allows us to connect to SQL Server
                connection.ConnectionString = Program.connectionString;

                // Opens a new connection if there isn't already a connection open (this just makes sure an error relating to no connection being open won't occur)
                if (connection.State != ConnectionState.Open) connection.Open();

                // Creates a new object called command that can allow SQL code to be run
                using (SqlCommand command = new SqlCommand())
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
                    using (SqlDataReader reader = command.ExecuteReader())
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
        private string getUserID(SqlConnection connection)
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
        private bool getTableExists(SqlConnection connection, string tableName)
        {
            return Program.getTableExists(connection, tableName);
        }

        // Method that opens a new form that allows the user to create a new account
        private void newLogin_lbl_Click(object sender, EventArgs e)
        {
            // Creates a new new user form
            NewUser newForm = new NewUser();

            // Hides the main menu form
            this.Hide();

            if (newForm.ShowDialog() == DialogResult.OK)
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



                        // Calls the procedure initialiseQs()
                        initialiseQs(connection, newForm.newUserID, 1, 16);



                        // Closes the connection to the database
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n\n" + ex);
                }
            }

            // Releases all resources used by the new form
            newForm.Dispose();

            // Makes the login form visible again
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
                    using (SqlConnection connection = new SqlConnection())
                    {
                        // This allows us to connect to SQL Server
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
        private bool getIsAuthenticated(SqlConnection connection, string username, string password)
        {
            // Opens a new connection if there isn't already a connection open (this just makes sure an error relating to no connection being open won't occur)
            if (connection.State != ConnectionState.Open) connection.Open();

            // Creates a new object called command that can allow SQL code to be run
            using (SqlCommand command = new SqlCommand())
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
                using (SqlDataReader reader = command.ExecuteReader())
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
        private void guest_lbl_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Program.guest = true;
            this.Close();
        }

        // Adds the default questions and initialises them for each user
        private void addDefaultQs(SqlConnection connection)
        {
            // Inserts default questions and answers into the database
            createQNA(connection, "What particle is the only stable baryon?", "PROTON");
            createQNA(connection, "What do you call a hadron that consists of 3 quarks?", "BARYON");
            createQNA(connection, "What do you call a hadron that consists of a quark-antiquark pair?", "MESON");
            createQNA(connection, "What baryon has a quark composition of udd?", "NEUTRON");
            createQNA(connection, "What baryon has a quark composition of uud?", "PROTON");
            createQNA(connection, "What is the antiparticle of an electron?", "POSITRON");
            createQNA(connection, "What particles don't experience the strong force?", "LEPTONS");
            createQNA(connection, "What do you call a wave where the oscillations travel perpendicular to the direction of propagation?", "TRANSVERSE");
            createQNA(connection, "What do you call a wave where the oscillations travel parallel to the direction of propagation?", "LONGITUDINAL");
            createQNA(connection, "What do you call two waves with the same wavelength?", "MONOCHROMATIC");
            createQNA(connection, "What do you call two waves with the same wavelength and a fixed phase difference?", "COHERENT");
            createQNA(connection, "What is the term used to describe the distance between two adjacent peaks on a wave?", "WAVELENGTH");
            createQNA(connection, "What is the term used to describe the maximum displacement of the wave from its equilibrium position?", "AMPLITUDE");
            createQNA(connection, "What do you call a point on a stationary wave where the displacement is 0?", "NODE");
            createQNA(connection, "What do you call a point on a stationary wave with maximum displacement?", "ANTINODE");
            createQNA(connection, "What is the term used to describe the spreading out of waves when they pass through or around a gap?", "DIFFRACTION");

            // Opens a new connection if there isn't already a connection open (this just makes sure an error relating to no connection being open won't occur)
            if (connection.State != ConnectionState.Open) connection.Open();

            // Creates a new object called command that can allow SQL code to be run
            using (SqlCommand command = new SqlCommand())
            {
                // Initialises the connection
                command.Connection = connection;

                // A SQL query that selects all the users from the table tblusers
                command.CommandText = @"
SELECT userID
FROM tblUsers;
";
                // Runs the SQL code and stores the generated table in the reader
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Creates new rows for each user for each default question
                    while (reader.Read())
                    {
                        // Declaring a new connection
                        using (SqlConnection connection2 = new SqlConnection())
                        {
                            // This allows us to connect to SQL Server
                            connection2.ConnectionString = Program.connectionString;

                            // Opens the connection to the database
                            connection2.Open();



                            // Calls the procedure initialiseQs()
                            initialiseQs(connection2, reader.GetString(0), 1, 16);


                            
                            // Closes the connection to the database
                            connection2.Close();
                        }
                    }
                }
            }
        }

        // Initialises the questions within the range given in as parameters for the user given in the parameter
        private void initialiseQs(SqlConnection connection, string aUserID, int minQ, int maxQ)
        {
            // Opens a new connection if there isn't already a connection open (this just makes sure an error relating to no connection being open won't occur)
            if (connection.State != ConnectionState.Open) connection.Open();

            // Creates a new object called command that can allow SQL code to be run
            using (SqlCommand command = new SqlCommand())
            {
                // Initialises the connection
                command.Connection = connection;

                // A SQL query that initialises the questions
                command.CommandText = @"
WHILE @min <= @max
BEGIN
    INSERT INTO tblUserAnswers (userID, questionID, numberOfAttempts, numberOfCorrectAttempts)
    VALUES (@userID, @min, 0, 0);
    SELECT @min = @min + 1;
END
";
                command.Parameters.AddWithValue("@userID", aUserID);
                command.Parameters.AddWithValue("@min", minQ);
                command.Parameters.AddWithValue("@max", maxQ);

                // Runs the SQL code
                command.ExecuteNonQuery();

                // Clears the parameters
                command.Parameters.Clear();
            }
        }
    }
}