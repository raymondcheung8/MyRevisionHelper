using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Security.Cryptography;
using System.Data;

namespace MyRevisionHelper
{
    static class Program
    {
        // Creates and sets the values for two new global constant strings that contain the databasePath and databaseName
        public const string databasePath = @"D:\Pers\WCGS\#U6 Hw\Computer Science\MyRevisionHelper" + "\\";
        public const string databaseName = "MyRevisionHelper.accdb";

        // Creates and sets the values for a new global constant string that contains the connection string
        public static string connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}{1};Persist Security Info=False;", databasePath, databaseName);

        // Creates a new global string that contains the userID and initialises this string to ""
        public static string userID = "";

        // Creates a new global boolean that tells the software whether the user is a guest and initialises it to false
        public static bool guest = false;

        // Function that returns whether a certain table exists
        public static bool getTableExists(OleDbConnection connection, string tableName)
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

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenu());
        }
    }

    public static class UserLogin
    {
        // Creates and sets the values for two new global constant integers that contain the length of the salt and hpassword
        public const int saltLength = 32;
        public const int hPasswordLength = 32;

        // Function that returns the salted hash
        public static byte[] getSaltedHash(string password, byte[] salt)
        {
            // Initiates a hash that implements PBKDF2
            Rfc2898DeriveBytes hash = new Rfc2898DeriveBytes(password, salt, iterations: 10000);

            // Sets the number of characters of the hashed password to 32
            int numberOfCharacters = 32;

            // Returns the salted hash as a string
            return hash.GetBytes(numberOfCharacters);
        }

        // Function that returns a random byte array (the salt)
        public static byte[] getSalt()
        {
            // Initiates a new random byte array generator
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();

            // Creates an empty salt array of length 32
            byte[] salt = new byte[32];

            // Store random bytes in the empty array
            random.GetNonZeroBytes(salt);

            // Returns the generated salt
            return salt;
        }

        // Function that returns a random 36 character string that is used as the userID
        public static string getUserID(OleDbConnection connection)
        {
            // Initialises userID as "ERROR"
            string userID = "ERROR";

            // Creates a new boolean variable called validUserID that becomes true once a unused userID is generated
            bool validUserID = false;
            while (!validUserID)
            {
                // Stores a randomly generated userID in a string variable
                userID = Guid.NewGuid().ToString();

                // Creates a new object called command that can allow SQL code to be run
                using (OleDbCommand command = new OleDbCommand())
                {
                    // Initialises the connection
                    command.Connection = connection;

                    // A SQL query that selects the userID generated from tblUsers
                    command.CommandText = @"
SELECT userID
FROM tblUsers
WHERE username = @userID;
";
                    command.Parameters.AddWithValue("@userID", userID);

                    // Runs the SQL code and stores the generated table in the reader
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        // An if statement that makes validUserID true if the reader doesn't have rows
                        if (!reader.HasRows) validUserID = true;

                        // Clears the parameters
                        command.Parameters.Clear();
                    }
                }
            }
            // Returns the userID
            return userID;
        }
    }

    public abstract class Activity
    {
        protected string question;

        public Activity()
        {

        }

        public string weight(OleDbConnection connection, string activityType)
        {
            // REPLACE WITH RETURNING THE QUESTION
            return null;
        }
    }

    public class QnaActivity : Activity
    {
        
    }
}