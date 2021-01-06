using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Security.Cryptography;

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

        // Function that returns a random 32 character string that is used as the userID
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




    public class Activity
    {
        Activity()
        {
            
        }
    }
}