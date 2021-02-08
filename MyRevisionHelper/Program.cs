using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Data;

namespace MyRevisionHelper
{
    static class Program
    {
        // Creates and sets the values for a new global string that contains the connection string
        public static string connectionString = string.Format(@"Server=DELL4310\SQLEXPRESS;Database=MyRevisionHelper;Trusted_Connection=True;");

        // Creates a new global string that contains the userID and initialises this string to ""
        public static string userID = "";

        // Creates a new global boolean that tells the software whether the user is a guest and initialises it to false
        public static bool guest = false;

        // Function that returns whether a certain table exists
        public static bool getTableExists(SqlConnection connection, string tableName)
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
            // Creates a hash that implements PBKDF2
            Rfc2898DeriveBytes hash = new Rfc2898DeriveBytes(password, salt, iterations: 10000);

            // Sets the number of characters of the hashed password
            int numberOfCharacters = hPasswordLength;

            // Returns the salted hash as a string
            return hash.GetBytes(numberOfCharacters);
        }

        // Function that returns a random byte array (the salt)
        public static byte[] getSalt()
        {
            // Creates a new random byte array generator
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();

            // Creates an empty salt array
            byte[] salt = new byte[saltLength];

            // Store random bytes in the empty array
            random.GetNonZeroBytes(salt);

            // Returns the generated salt
            return salt;
        }

        // Function that returns a random 36 character string that is used as the userID
        public static string getUserID(SqlConnection connection)
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
                using (SqlCommand command = new SqlCommand())
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
                    using (SqlDataReader reader = command.ExecuteReader())
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
        // Declares two string variables that store the question and answer respectively
        protected string question;
        protected string answer;

        // Declares an integer variable that stores the questionID
        protected int questionID;

        // Constructor
        public Activity()
        {

        }
        
        // A procedure that updates the userAnswer table
        protected void updateTblUserAnswers(SqlConnection connection, string activityType)
        {
            // Opens a new connection if there isn't already a connection open (this just makes sure an error relating to no connection being open won't occur)
            if (connection.State != ConnectionState.Open) connection.Open();

            // Creates a new object called command that can allow SQL code to be run
            using (SqlCommand command = new SqlCommand())
            {
                // Initialises the connection
                command.Connection = connection;

                // A SQL query that updates the userAnswer table
                command.CommandText = @"
-- Declares the variable @minWeight which stores the minimum value a weighting can have and initialises it as 0.25
DECLARE @minWeight FLOAT;
SELECT @minWeight = 0.25;

-- Updates the field, weighting, from, the table, tblUserAnswers for questions that haven't been answered before
UPDATE U
SET weighting = 100
FROM tblUserAnswers U
JOIN tblQuestions Q ON U.questionID = Q.questionID
WHERE userID = @userID AND numberOfAttempts = 0 AND Q.questionType = @questionType;

-- Updates the field, weighting, from the table, tblUserAnswers
-- I SQUARED THE WEIGHTS SO THERE WAS A BETTER SPREAD OF RESULTS
UPDATE U
SET weighting = POWER(1 - (numberOfCorrectAttempts / CAST(numberOfAttempts AS FLOAT)), 2)
FROM tblUserAnswers U
JOIN tblQuestions Q ON U.questionID = Q.questionID
WHERE userID = @userID AND numberOfAttempts > 0 AND Q.questionType = @questionType;



/*
CATEGORY 1 = QUESTIONS THAT HAVEN'T BEEN ATTEMPTED BEFORE
CATEGORY 2 = QUESTIONS THAT HAVE BEEN ANSWERED DECENTLY OR POORLY
CATEGORY 3 = QUESTIONS THAT HAVE BEEN ANSWERED WELL
*/



-- Updates the field, category, from the table, tblUserAnswers, so that weightings greater than or equal to the minimum value allowed are placed in category 1
UPDATE U
SET category = 1
FROM tblUserAnswers U
JOIN tblQuestions Q ON U.questionID = Q.questionID
WHERE userID = @userID AND numberOfAttempts = 0 AND Q.questionType = @questionType;

-- Updates the field, category, from the table, tblUserAnswers, so that weightings greater than or equal to the minimum value allowed are placed in category 1
UPDATE U
SET category = 2
FROM tblUserAnswers U
JOIN tblQuestions Q ON U.questionID = Q.questionID
WHERE userID = @userID AND weighting >= @minWeight AND numberOfAttempts > 0 AND Q.questionType = @questionType;

-- Updates the field, category, from the table, tblUserAnswers, so that weightings less than the minimum value allowed are placed in category 2
UPDATE U
SET category = 3
FROM tblUserAnswers U
JOIN tblQuestions Q ON U.questionID = Q.questionID
WHERE userID = @userID AND weighting < @minWeight AND numberOfAttempts > 0 AND Q.questionType = @questionType;

-- Updates the field, weighting, from the table tblUserAnswers, so that weightings in category 2 are changed to the minimum value allowed 
UPDATE U
SET weighting = @minWeight
FROM tblUserAnswers U
JOIN tblQuestions Q ON U.questionID = Q.questionID
WHERE userID = @userID AND category = 3 AND Q.questionType = @questionType;



-- Creates a new temporary table called #weighting and uses the aggregate function SUM to calculate and store the sum of all the weightings for a particular user
SELECT userID, SUM(weighting) AS sumWeighting
INTO   #weighting
FROM   tblUserAnswers U
JOIN tblQuestions Q ON U.questionID = Q.questionID
WHERE  userID = @userID AND Q.questionType = @questionType
GROUP BY userID;

-- Updates the field, specificWeight, from the table, tblUserAnswers
UPDATE A
SET    specificWeight = A.weighting / W.sumWeighting
FROM   tblUserAnswers A
JOIN   #weighting W ON W.userID = A.userID
JOIN   tblQuestions Q ON A.questionID = Q.questionID
WHERE  A.userID = @userID AND Q.questionType = @questionType;

-- Drops the temporary table #weighting
DROP TABLE #weighting;

-- Creates a new temporary table called #cdf
CREATE TABLE #cdf
(
seqNo           INT IDENTITY(1,1),
userID          CHAR(36)    NOT NULL,
questionID      INT         NOT NULL,
specificWeight  FLOAT       NOT NULL,
cdfPosition     FLOAT       NOT NULL
);

-- Copies the fields userID, questionID and specific weight from tblUserAnswers into the temporary table #cdf while also placing the specificWeight values into the field cdfPosition
INSERT INTO #cdf
SELECT U.userID, U.questionID, U.specificWeight, U.specificWeight
FROM   tblUserAnswers U
JOIN   tblQuestions Q ON U.questionID = Q.questionID
WHERE  userID = @userID AND Q.questionType = @questionType;

-- Declares two new variables called @count and @maxRox and initialises @count as 2 and @maxRow as the largest number in the field seqNo
DECLARE @count  INT,
		@maxRow INT;
SELECT  @count = 2;
SELECT @maxRow = MAX(seqNo) FROM #cdf;

-- A while loop that keeps adding the specific weight of the previous record to the current record until a cdfPosition for every record has been established
WHILE (@count <= @maxRow)
BEGIN
		UPDATE C
		SET    cdfPosition = C.specificWeight + P.cdfPosition
		FROM   #cdf C
		JOIN   #cdf P ON P.seqNo = C.seqNo - 1
		WHERE  C.seqNo = @count;

		SELECT @count = @count + 1;
END;

-- Updates the field cdfPosition from the table tblUserAnswers with the cdfPosition from the temporary table #cdf
UPDATE A
SET cdfPosition = C.cdfPosition
FROM tblUserAnswers A
JOIN #cdf C ON A.questionID = C.questionID
JOIN tblQuestions Q ON A.questionID = Q.questionID
WHERE A.userID = @userID AND Q.questionType = @questionType;

-- Drops the temporary table #cdf
DROP TABLE #cdf;
";
                command.Parameters.AddWithValue("@userID", Program.userID);
                command.Parameters.AddWithValue("@questionType", activityType);

                // Runs the SQL code
                command.ExecuteNonQuery();

                // Clears the parameters
                command.Parameters.Clear();
            }



            // Closes the connection to the database
            connection.Close();
        }

        // A procedure that randomly picks a question depending on how well the user has previously answered the question
        public void weight(SqlConnection connection, string activityType)
        {
            // Updates the userAnswer table
            this.updateTblUserAnswers(connection, activityType);

            // Opens a new connection if there isn't already a connection open (this just makes sure an error relating to no connection being open won't occur)
            if (connection.State != ConnectionState.Open) connection.Open();

            // Creates a new object called command that can allow SQL code to be run
            using (SqlCommand command = new SqlCommand())
            {
                // Initialises the connection
                command.Connection = connection;

                // Declares and initialises the boolean variable found to false
                bool found = false;

                // A while loop that ensures the attributes question, answer and questionID are changed
                while (!found)
                {
                    // A SQL query that randomly picks a question depending on how well the user has previously answered the question
                    command.CommandText = @"
-- Declares the variable @randomNumber which stores a random number between 0 and 1 and initialises it as a random number between 0 and 1
DECLARE @randomNumber FLOAT;
SELECT @randomNumber = RAND();

-- Selects a random question and answer from tblQuestions and tblAnswers using the weighted algorithm
SELECT Q.question, A.answer, Q.questionID
FROM   tblUserAnswers U
JOIN tblQuestions Q ON Q.questionID = U.questionID
JOIN tblAnswers   A ON A.questionID = U.questionID
WHERE  cdfPosition   >= @randomNumber 
AND    cdfPosition    = (SELECT MIN(cdfPosition)
                         FROM tblUserAnswers
                         WHERE cdfPosition    >= @randomNumber
						 AND    userID         = @userID
						 AND    Q.questionType = @questionType)
AND    userID         = @userID
AND    Q.questionType = @questionType;
";
                    command.Parameters.AddWithValue("@userID", Program.userID);
                    command.Parameters.AddWithValue("@questionType", activityType);

                    // Runs the SQL code and stores the generated table in the reader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Stores the question, answer and questionID obtained from the SELECT statement
                        while (reader.Read())
                        {
                            this.question = reader.GetString(0);
                            this.answer = reader.GetString(1);
                            this.questionID = reader.GetInt32(2);

                            // Changes the value of the boolean variable found to true
                            found = true;
                        }

                        // Clears the parameters
                        command.Parameters.Clear();

                        // Test to see if the right question and answer is stored
                        // MessageBox.Show(question + Environment.NewLine + answer);
                    }
                }
            }



            // Closes the connection to the database
            connection.Close();
        }

        // A function that returns the value stored in the string variable question
        public string getQuestion()
        {
            return this.question;
        }

        // A function that returns the value stored in the string variable answer
        public string getAnswer()
        {
            return this.answer;
        }

        // A function that returns the value stored in the string variable questionID
        public int getQuestionID()
        {
            return this.questionID;
        }

        // A procedure that increments the number of attempts for a certain question
        public void incAttempts(SqlConnection connection)
        {
            // Opens a new connection if there isn't already a connection open (this just makes sure an error relating to no connection being open won't occur)
            if (connection.State != ConnectionState.Open) connection.Open();

            // Creates a new object called command that can allow SQL code to be run
            using (SqlCommand command = new SqlCommand())
            {
                // Initialises the connection
                command.Connection = connection;

                // A SQL query that increments the number of attempts of the question
                command.CommandText = @"
-- Increments numberOfAttempts
UPDATE tblUserAnswers
SET numberOfAttempts = numberOfAttempts + 1
WHERE questionID = @questionID
AND userID = @userID;
";
                command.Parameters.AddWithValue("@questionID", this.questionID);
                command.Parameters.AddWithValue("@userID", Program.userID);

                // Runs the SQL code
                command.ExecuteNonQuery();

                // Clears the parameters
                command.Parameters.Clear();
            }
        }

        // A procedure that increments the number of correct attempts for a certain question
        public void incCorrectAttempts(SqlConnection connection)
        {
            // Opens a new connection if there isn't already a connection open (this just makes sure an error relating to no connection being open won't occur)
            if (connection.State != ConnectionState.Open) connection.Open();

            // Creates a new object called command that can allow SQL code to be run
            using (SqlCommand command = new SqlCommand())
            {
                // Initialises the connection
                command.Connection = connection;

                // A SQL query that increments the number of correct attempts of the question
                command.CommandText = @"
-- Increments numberOfCorrectAttempts
UPDATE tblUserAnswers
SET numberOfCorrectAttempts = numberOfCorrectAttempts + 1
WHERE questionID = @questionID
AND userID = @userID;
";
                command.Parameters.AddWithValue("@questionID", this.questionID);
                command.Parameters.AddWithValue("@userID", Program.userID);

                // Runs the SQL code
                command.ExecuteNonQuery();

                // Clears the parameters
                command.Parameters.Clear();
            }
        }
    }

    public class QnaActivity : Activity
    {
        // Constructor that inherits the constructor from the base class
        public QnaActivity() : base()
        {

        }
    }
}