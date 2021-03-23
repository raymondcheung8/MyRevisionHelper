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

            // admin is initialised to false
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
                            getSalt().CopyTo(salt, 0);

                            // Generates a salted hash of the password
                            byte[] hPassword = new byte[UserLogin.hPasswordLength];
                            getSaltedHash("abcde", salt).CopyTo(hPassword, 0);

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

                        // Calls upon the getTableExists() function and will create a new table called tblNotes if there isn't already an existing one
                        if (!getTableExists(connection, "tblNotes"))
                        {
                            // A SQL query that creates a new table called "tblNotes" where "noteID" is the primary key
                            command.CommandText = @"
CREATE TABLE tblNotes
(
noteID          CHAR(36)    NOT NULL    PRIMARY KEY,
insertDate      DATETIME    NOT NULL,
noteDescr       TEXT        NOT NULL,
userID          CHAR(36)    NOT NULL    REFERENCES  tblUsers (userID)
);
";
                            // Runs the SQL code
                            command.ExecuteNonQuery();
                        }

                        /*
                        // Message boxes that test whether the function getTableExists() works
                        MessageBox.Show(getTableExists(connection, "tblCounters").ToString());
                        MessageBox.Show(getTableExists(connection, "tblQuestions").ToString());
                        MessageBox.Show(getTableExists(connection, "tblAnswers").ToString());
                        MessageBox.Show(getTableExists(connection, "tblUserAnswers").ToString());
                        */

                        // Creates a new counter table if it doesn't already exist
                        if (!getTableExists(connection, "tblCounters"))
                        {
                            // A SQL query that creates a counter table
                            command.CommandText = @"
-- Creates a counter table
CREATE TABLE tblCounters
(
counterType		VARCHAR(8)		NOT NULL    PRIMARY KEY,
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
                        if (!getTableExists(connection, "tblQuestions"))
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
                        if (!getTableExists(connection, "tblAnswers"))
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
                        if (!getTableExists(connection, "tblUserAnswers"))
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
weighting					FLOAT       NULL,
category                    INT         NULL,
specificWeight				FLOAT       NULL,
cdfPosition					FLOAT       NULL,
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

        /*
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
        */

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
                    // Generates a random salt
                    byte[] salt = new byte[32];
                    getSalt().CopyTo(salt, 0);

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
            SqlDataReader reader = command.ExecuteReader();

            // Returns the boolean value of whether or not reader has rows
            return reader.HasRows;
             */
        }

        // Method that opens a new form that allows the user to create a new account
        private void newLogin_lbl_Click(object sender, EventArgs e)
        {
            // Creates a new new user form
            using (NewUser newForm = new NewUser())
            {
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



                            // Calls the procedure initialiseEveryQUser()
                            initialiseEveryQForUser(connection, newForm.newUserID);



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
            // Instantiates a new object called createActivity
            CreateActivity createActivity = new CreateActivity();

            // FORMAT: createActivity.createQna(connection, "QUESTION", "ANSWER");
            // TEMPLATE: createActivity.createQna(connection, "", "");

            // Inserts default questions and answers into the database
            createActivity.createQna(connection, "What particle is the only stable baryon?", "PROTON");
            createActivity.createQna(connection, "What do you call a hadron that consists of 3 quarks?", "BARYON");
            createActivity.createQna(connection, "What do you call a hadron that consists of a quark-antiquark pair?", "MESON");
            createActivity.createQna(connection, "What baryon has a quark composition of udd?", "NEUTRON");
            createActivity.createQna(connection, "What baryon has a quark composition of uud?", "PROTON");
            createActivity.createQna(connection, "What is the antiparticle of an electron?", "POSITRON");
            createActivity.createQna(connection, "What particles don't experience the strong force?", "LEPTONS");
            createActivity.createQna(connection, "What do you call a wave where the oscillations travel perpendicular to the direction of propagation?", "TRANSVERSE");
            createActivity.createQna(connection, "What do you call a wave where the oscillations travel parallel to the direction of propagation?", "LONGITUDINAL");
            createActivity.createQna(connection, "What do you call two waves with the same wavelength?", "MONOCHROMATIC");
            createActivity.createQna(connection, "What do you call two waves with the same wavelength and a fixed phase difference?", "COHERENT");
            createActivity.createQna(connection, "What is the term used to describe the distance between two adjacent peaks on a wave?", "WAVELENGTH");
            createActivity.createQna(connection, "What is the term used to describe the maximum displacement of the wave from its equilibrium position?", "AMPLITUDE");
            createActivity.createQna(connection, "What do you call a point on a stationary wave where the displacement is 0?", "NODE");
            createActivity.createQna(connection, "What do you call a point on a stationary wave with maximum displacement?", "ANTINODE");
            createActivity.createQna(connection, "What is the term used to describe the spreading out of waves when they pass through or around a gap?", "DIFFRACTION");

            // FORMAT: createActivity.createTimedMC(connection, "QUESTION", "CORRECT ANSWER", "WRONG ANSWER", "WRONG ANSWER", "WRONG ANSWER");
            // TEMPLATE: createActivity.createTimedMC(connection, "", "", "", "", "");

            // Inserts default multiple choice questions and answers into the database
            createActivity.createTimedMC(connection, "An atom of Nitrogen (nucleon number = 16, proton number = 7) gains 3 electrons.\n\nWhat is the specific charge of the ion?",
                "−1.80 × 10^7 Ckg^–1",
                "1.80 × 10^7 Ckg^–1", "4.19 × 10^7 Ckg^–1", "-4.19 × 10^7 Ckg^–1");
            createActivity.createTimedMC(connection, "Electrons moving in a beam have the same de Broglie wavelength as protons in a separate beam moving at a speed of 2.8 × 10^4 ms^–1.\n\nWhat is the speed of the electrons?",
                "5.1 × 10^7 ms^−1",
                "1.5 × 10^1 ms^−1", "2.8 × 10^4 ms^−1", "1.2 × 10^6 ms^−1");
            createActivity.createTimedMC(connection, "Which statement is not correct for ultrasound and X-rays?",
                "Both can be polarised",
                "Both can be refracted", "Both can be diffracted", "Both can be reflected");
            createActivity.createTimedMC(connection, "When a monochromatic light source is incident on two slits of the same width an interference pattern is produced.\n\nOne slit is then covered with opaque black paper.\n\nWhat is the effect of covering one slit on the resulting interference pattern?",
                "Fewer maxima are observed",
                "The intensity of the central maximum will increase", "The width of the central maximum decreases", "The outer maxima become wider");
            createActivity.createTimedMC(connection, "When light of wavelength 5.0 × 10^−7 m is incident normally on a diffraction grating the fourth-order maximum is observed at an angle of 30°.\n\nWhat is the number of lines per mm on the diffraction grating?",
                "2.5 × 10^2",
                "2.5 × 10^5", "1.0 × 10^3", "1.0 × 10^6");
            createActivity.createTimedMC(connection, "A rocket of mass 12 000 kg accelerates vertically upwards from the surface of the Earth at 1.4 ms^−2.\n\nWhat is the thrust of the rocket?",
                "1.3 × 10^5 N",
                "1.7 × 10^4 N", "1.0 × 10^5 N", "1.6 × 10^5 N");
            createActivity.createTimedMC(connection, "A car of mass 580 kg collides with the rear of a stationary van of mass 1200 kg.\n\nFollowing the collision, the van moves with a velocity of 6.20 ms^−1 and the car recoils in the opposite direction with a velocity of 1.60 ms^−1.\n\nWhat is the initial speed of the car?",
                "11.2 ms^−1",
                "5.43 ms^−1", "12.8 ms^−1", "14.4 ms^−1");
            createActivity.createTimedMC(connection, "A sample of wire has a Young modulus E.  A second sample of wire made from an identical material has three times the length and half the diameter of the first sample.\n\nWhat is the Young modulus of the second sample of wire in terms of E?",
                "E",
                "0.25E", "6E", "12E");
            createActivity.createTimedMC(connection, "The combined resistance of n identical resistors connected in parallel is Rn.\n\nWhich statement correctly describes the variation of Rn as n increases?",
                "Rn decreases non-linearly as n increases",
                "Rn decreases linearly as n increases", "Rn increases linearly as n increases", "Rn increases non-linearly as n increases");

            // FORMAT: createActivity.createWordedQ(connection, "QUESTION", "MODEL ANSWER", LIST OF KEYWORDS);
            // TEMPLATE: createActivity.createWordedQ(connection, "", "", keyWords);

            // Inserts default questions that require worded answers, model answers and keywords into the database
            List<string> keyWords = new List<string>();

            keyWords.Clear();
            keyWords.Add("weak");
            keyWords.Add("lepton");
            keyWords.Add("hadron");
            createActivity.createWordedQ(connection, "Explain which fundamental interaction is responsible for the decay:\nPotassium + Electron -> Argon + Electron Neutrino",
                "The weak interaction is responsible for this decay since both hadrons and leptons are involved.",
                keyWords);
            keyWords.Clear();

            keyWords.Clear();
            keyWords.Add("beta");
            keyWords.Add("electron");
            keyWords.Add("antielectron neutrino");
            createActivity.createWordedQ(connection, "The potassium isotope can decay by a decay process to form a calcium-40 nuclide\n\nSuggest how the emissions from a nucleus of decaying potassium can be used to confirm which decay process is occurring.",
                "This decay is beta minus emission since an electron and an antielectron neutrino are released while no photons are released. The electron will be detected through the use of a cloud chamber or by electron absorption.",
                keyWords);
            keyWords.Clear();

            // FORMAT: createActivity.createMathQ(connection, "QUESTION", "EQUATION", "RANGES", "D.P.");
            // TEMPLATE: createActivity.createMathQ(connection, "", "", "", "");

            // Inserts default math questions, the equation used to calculate the answer, the ranges of values that could appear and the decimal places the values are in into the database
            createActivity.createMathQ(connection, "One decay mechanism for the decay of Potassium (nucleon number = 40, proton number = 19) results in the argon nucleus having an excess energy of {0} MeV.  It loses this energy by emitting a single gamma photon.\n\nCalculate the wavelength of the photon released by the argon nucleus in metres.",
                "6.63E-34 3E+8 * {0} 1.6E-13 * /",
                "1:3",
                "2");
            createActivity.createMathQ(connection, "A ray of monochromatic green light is incident normally on the curved surface of a semicircular glass block.\n\nThe angle of refraction of the ray at the plane surface is {0}° and the refractive index of the glass used = {1}\n\nCalculate the angle of incidence of the ray on the flat surface of the block in degrees.",
                "1 {0} SIN * {1} / ARCSIN",
                "0:90;1.5:1.6",
                "2");
            createActivity.createMathQ(connection, "Solar cells convert solar energy to useful electrical energy in the road sign with an efficiency of {0}%. The solar-cell supply used by the engineer has a total surface area of {1} cm^2.\n\nCalculate the minimum intensity, in Wm^–2, of the sunlight needed to provide the minimum current of {2} mA to the road sign when it has a resistance of {3} Ω.",
                "{2} 10 -3 ^ * 2 ^ {3} * {0} 10 -2 ^ * / {1} 10 -4 ^ * /",
                "1:100;10:50;50:100;1:10",
                "1");

            /*
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
            */
        }
        
        // Initialises every question for the user in the parameter
        private void initialiseEveryQForUser(SqlConnection connection, string aUserID)
        {
            // Opens a new connection if there isn't already a connection open (this just makes sure an error relating to no connection being open won't occur)
            if (connection.State != ConnectionState.Open) connection.Open();

            // Creates a new object called command that can allow SQL code to be run
            using (SqlCommand command = new SqlCommand())
            {
                // Initialises the connection
                command.Connection = connection;

                // A SQL query that initialises every question for the user in the parameter
                command.CommandText = @"
INSERT INTO tblUserAnswers
SELECT @userID, questionID, 0, 0, NULL, NULL, NULL, NULL
FROM tblQuestions;
";
                command.Parameters.AddWithValue("@userID", aUserID);

                // Runs the SQL code
                command.ExecuteNonQuery();

                // Clears the parameters
                command.Parameters.Clear();
            }
        }
    }
}