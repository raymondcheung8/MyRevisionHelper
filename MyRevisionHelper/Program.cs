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

        // Declares a boolean variable that stores whether or not questions will be displayed using weights
        protected bool useWeights;

        // Constructor
        public Activity()
        {
            // Initialises the boolean variable useWeights to the result of the function getUseWeights()
            useWeights = getUseWeights();
        }

        // Function that returns true if questions will be displayed using weights and false if questions will be displayed entirely randomly
        public bool getUseWeights()
        {
            // If the user isn't a guest, this function will ask them if they want to use weights to display questions
            if (!Program.guest)
            {
                // Displays a message box asking the user if they want to use weights to display questions
                DialogResult dialogResult = MessageBox.Show("Do you want to display questions using weights?", "WeightsOption", MessageBoxButtons.YesNo);

                // Returns true if the dialog result is yes and otherwise returns false
                if (dialogResult == DialogResult.Yes)
                {
                    return true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    return false;
                }
                else
                {
                    MessageBox.Show("ERROR with receiving dialog result");
                    return false;
                }
            }
            else
            {
                return false;
            }
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

        // A procedure that returns the command text for picking a question completely randomly without using weights
        public string getNoWeightCT(SqlConnection connection)
        {
            return @"
-- Creates a new temporary table called #tblSelectedQuestions
CREATE TABLE #tblSelectedQuestions
(
seqID           INT IDENTITY(1,1),
questionID      INT         NOT NULL,
question        TEXT        NOT NULL,
questionType    VARCHAR(8)  NOT NULL
);

-- Declares the variables @Lower, @upper and @randomNumber
-- They store the lower bound and the upper bound of the random range as well as a random integer between the upper and lower bounds specified respectively
DECLARE @lower INT, @upper INT, @randomNumber INT;

-- Inserts the values in tblQuestions into the temporary table tblSelectedQuestions
INSERT INTO #tblSelectedQuestions (questionID, question, questionType)
SELECT questionID, question, questionType
FROM tblQuestions
WHERE questionType = @questionType;

-- Initialises the variable @Upper as the number of rows of the temporary table #tblSelectedQuestions
-- The number of rows inserted is the max seqID
select @upper = @@rowcount

-- Initialises the variable @Lower to 1
SELECT @lower = 1;

-- Initialises the variable @randomNumber to a random number between the upper and lower bounds specified
SELECT @randomNumber = ROUND(((@upper - @lower) * RAND() + @lower), 0);

-- Selects a completely random question and answer from tblQuestions and tblAnswers
SELECT Q.question, A.answer, Q.questionID, A.answerType
FROM   #tblSelectedQuestions Q
JOIN   tblAnswers   A ON A.questionID = Q.questionID
AND    Q.seqID       = @randomNumber;

-- Drops the temporary table #tblSelectedQuestions
DROP TABLE #tblSelectedQuestions;
";
        }
    }

    public class QnaActivity : Activity
    {
        // Constructor that inherits the constructor from the base class
        public QnaActivity() : base()
        {

        }

        // A procedure that randomly picks a question depending on how well the user has previously answered the question
        public void weight(SqlConnection connection)
        {
            // Updates the userAnswer table
            this.updateTblUserAnswers(connection, "QNA");

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
                    if (useWeights)
                    {
                        // A SQL query that randomly picks a question depending on how well the user has previously answered the question
                        command.CommandText = @"
-- Declares the variable @randomNumber which stores a random number between 0 and 1 and initialises it as a random number between 0 and 1
DECLARE @randomNumber FLOAT;
SELECT @randomNumber = RAND();

-- Selects a random question and answer from tblQuestions and tblAnswers using the weighted algorithm
SELECT Q.question, A.answer, Q.questionID, A.answerType
FROM   tblUserAnswers U
JOIN tblQuestions Q ON Q.questionID = U.questionID AND Q.questionType = @questionType
JOIN tblAnswers   A ON A.questionID = U.questionID
WHERE  cdfPosition   >= @randomNumber
AND    cdfPosition    = (SELECT MIN(cdfPosition)
                        FROM tblUserAnswers U2
						JOIN   tblQuestions Q1 ON Q1.questionID = U2.questionID AND Q1.questionType = @questionType
                        WHERE  U2.cdfPosition    >= @randomNumber
						AND    U2.userID          = U.userID)
AND    U.userID       = @userID;
";
                        command.Parameters.AddWithValue("@userID", Program.userID);
                    }
                    else
                    {
                        // Calls a function that returns a SQL query that picks a question completely randomly without using weights
                        command.CommandText = this.getNoWeightCT(connection);
                    }
                    command.Parameters.AddWithValue("@questionType", "QNA");

                    // Runs the SQL code and stores the generated table in the reader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Stores the question, answer and questionID obtained from the SELECT statement
                        while (reader.Read())
                        {
                            // Stores the question, answer and questionID in the attributes of this class and sets the boolean variable found to true
                            if (reader.GetString(3) == "QNA")
                            {
                                this.question = reader.GetString(0);
                                this.answer = reader.GetString(1);
                                this.questionID = reader.GetInt32(2);

                                // Changes the value of the boolean variable found to true
                                found = true;
                            }
                            else
                            {
                                MessageBox.Show("ERROR with answer type stored in database");
                            }
                        }

                        // Clears the parameters
                        command.Parameters.Clear();

                        // Test to see if the right question and answer is stored
                        // MessageBox.Show(question + Environment.NewLine + answer);
                    }
                }
            }
        }
    }

    public class TimedMCActivity : Activity
    {
        // Declares a new string array called wrongAnswers
        private string[] wrongAnswers;

        // Constructor that inherits the constructor from the base class
        public TimedMCActivity() : base()
        {
            // Initialises the string array wrongAnswers
            this.wrongAnswers = new string[3];
        }

        // A procedure that randomly picks a question depending on how well the user has previously answered the question
        public void weight(SqlConnection connection)
        {
            // Updates the userAnswer table
            this.updateTblUserAnswers(connection, "MULT");

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
                    if (useWeights)
                    {
                        // A SQL query that randomly picks a question depending on how well the user has previously answered the question
                        command.CommandText = @"
-- Declares the variable @randomNumber which stores a random number between 0 and 1 and initialises it as a random number between 0 and 1
DECLARE @randomNumber FLOAT;
SELECT @randomNumber = RAND();

-- Selects a random question and answer from tblQuestions and tblAnswers using the weighted algorithm
SELECT Q.question, A.answer, Q.questionID, A.answerType
FROM   tblUserAnswers U
JOIN tblQuestions Q ON Q.questionID = U.questionID AND Q.questionType = @questionType
JOIN tblAnswers   A ON A.questionID = U.questionID
WHERE  cdfPosition   >= @randomNumber
AND    cdfPosition    = (SELECT MIN(cdfPosition)
                    FROM tblUserAnswers U2
					JOIN   tblQuestions Q1 ON Q1.questionID = U2.questionID AND Q1.questionType = @questionType
                    WHERE  U2.cdfPosition    >= @randomNumber
					AND    U2.userID          = U.userID)
AND    U.userID       = @userID;
";
                        command.Parameters.AddWithValue("@userID", Program.userID);
                    }
                    else
                    {
                        // Calls a function that returns a SQL query that picks a question completely randomly without using weights
                        command.CommandText = this.getNoWeightCT(connection);
                    }
                    command.Parameters.AddWithValue("@questionType", "MULT");

                    // Runs the SQL code and stores the generated table in the reader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Declares and initialises the integer variable count to 0
                        int count = 0;

                        // Stores the question, answer and questionID obtained from the SELECT statement
                        while (reader.Read())
                        {
                            // Stores the correct and incorrect answers
                            if (reader.GetString(3) == "MULTT")
                            {
                                // Stores the question, answer and questionID obtained from the SELECT statement
                                this.question = reader.GetString(0);
                                this.answer = reader.GetString(1);
                                this.questionID = reader.GetInt32(2);

                                // Changes the value of the boolean variable found to true
                                found = true;
                            }
                            else if (reader.GetString(3) == "MULTF")
                            {
                                // Stores the wrong answers obtained from the SELECT statement
                                this.wrongAnswers[count] = reader.GetString(1);

                                // Increments the integer variable count
                                count++;
                            }
                            else
                            {
                                MessageBox.Show("ERROR with answer type stored in database");
                            }
                        }

                        // Clears the parameters
                        command.Parameters.Clear();
                    }
                }
            }
        }

        // A function that returns the values stored in the string array wrongAnswers
        public string[] getWrongAnswers()
        {
            return this.wrongAnswers;
        }
    }

    public class WordedQActivity : Activity
    {
        // Declares an new string list called keyWords
        private List<string> keyWords;

        // Constructor that inherits the constructor from the base class
        public WordedQActivity() : base()
        {
            // Instatialises the string list
            keyWords = new List<string>();
        }

        // A procedure that randomly picks a question depending on how well the user has previously answered the question
        public void weight(SqlConnection connection)
        {
            // This clears the string list keyWords
            this.keyWords.Clear();

            // Updates the userAnswer table
            this.updateTblUserAnswers(connection, "WQ");

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
                    if (useWeights)
                    {
                        // A SQL query that randomly picks a question depending on how well the user has previously answered the question
                        command.CommandText = @"
-- Declares the variable @randomNumber which stores a random number between 0 and 1 and initialises it as a random number between 0 and 1
DECLARE @randomNumber FLOAT;
SELECT @randomNumber = RAND();

-- Selects a random question and answer from tblQuestions and tblAnswers using the weighted algorithm
SELECT Q.question, A.answer, Q.questionID, A.answerType
FROM   tblUserAnswers U
JOIN tblQuestions Q ON Q.questionID = U.questionID AND Q.questionType = @questionType
JOIN tblAnswers   A ON A.questionID = U.questionID
WHERE  cdfPosition   >= @randomNumber
AND    cdfPosition    = (SELECT MIN(cdfPosition)
                    FROM tblUserAnswers U2
					JOIN   tblQuestions Q1 ON Q1.questionID = U2.questionID AND Q1.questionType = @questionType
                    WHERE  U2.cdfPosition    >= @randomNumber
					AND    U2.userID          = U.userID)
AND    U.userID       = @userID;
";
                        command.Parameters.AddWithValue("@userID", Program.userID);
                    }
                    else
                    {
                        // Calls a function that returns a SQL query that picks a question completely randomly without using weights
                        command.CommandText = this.getNoWeightCT(connection);
                    }
                    command.Parameters.AddWithValue("@questionType", "WQ");

                    // Runs the SQL code and stores the generated table in the reader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Stores the question, answer and questionID obtained from the SELECT statement
                        while (reader.Read())
                        {
                            // Stores the model answer and the keywords
                            if (reader.GetString(3) == "WQA")
                            {
                                // Stores the question, answer and questionID obtained from the SELECT statement
                                this.question = reader.GetString(0);
                                this.answer = reader.GetString(1);
                                this.questionID = reader.GetInt32(2);

                                // Changes the value of the boolean variable found to true
                                found = true;
                            }
                            else if (reader.GetString(3) == "WQK")
                            {
                                // Stores the keywords obtained from the SELECT statement
                                this.keyWords.Add(reader.GetString(1));
                            }
                            else
                            {
                                MessageBox.Show("ERROR with answer type stored in database");
                            }
                        }

                        // Clears the parameters
                        command.Parameters.Clear();
                    }
                }
            }
        }

        // A function that returns the values stored in the string list keyWords
        public List<string> getKeyWords()
        {
            return this.keyWords;
        }

        // A function that returns the number of keywords needed in the answer
        public int getNumOfKW()
        {
            return this.keyWords.Count();
        }
    }

    public class MathQActivity : Activity
    {
        // Instantiates a new object that generates pseudorandom integers using the system clock
        private Random rnd = new Random();

        // Constructor that inherits the constructor from the base class
        public MathQActivity() : base()
        {
            
        }

        // A procedure that randomly picks a question depending on how well the user has previously answered the question
        public void weight(SqlConnection connection)
        {
            // Updates the userAnswer table
            this.updateTblUserAnswers(connection, "MQ");

            // Opens a new connection if there isn't already a connection open (this just makes sure an error relating to no connection being open won't occur)
            if (connection.State != ConnectionState.Open) connection.Open();

            // Creates a new object called command that can allow SQL code to be run
            using (SqlCommand command = new SqlCommand())
            {
                // Initialises the connection
                command.Connection = connection;

                // Declares a new string variable called equation and initialises it to ""
                string equation = "";

                // Instantiates a new string list called values
                List<string> values = new List<string>();

                // Declares a new string variable called ranges and initialises it to ""
                string ranges = "";

                // Declares a new integer variable called dp and initialises it to 0
                int dp = 0;

                // Declares and initialises the boolean variable found to false
                bool found = false;

                // A while loop that ensures the attributes question, answer and questionID are changed
                while (!found)
                {
                    if (useWeights)
                    {
                        // A SQL query that randomly picks a question depending on how well the user has previously answered the question
                        command.CommandText = @"
-- Declares the variable @randomNumber which stores a random number between 0 and 1 and initialises it as a random number between 0 and 1
DECLARE @randomNumber FLOAT;
SELECT @randomNumber = RAND();

-- Selects a random question and answer from tblQuestions and tblAnswers using the weighted algorithm
SELECT Q.question, A.answer, Q.questionID, A.answerType
FROM   tblUserAnswers U
JOIN tblQuestions Q ON Q.questionID = U.questionID AND Q.questionType = @questionType
JOIN tblAnswers   A ON A.questionID = U.questionID
WHERE  cdfPosition   >= @randomNumber
AND    cdfPosition    = (SELECT MIN(cdfPosition)
                    FROM tblUserAnswers U2
					JOIN   tblQuestions Q1 ON Q1.questionID = U2.questionID AND Q1.questionType = @questionType
                    WHERE  U2.cdfPosition    >= @randomNumber
					AND    U2.userID          = U.userID)
AND    U.userID       = @userID;
";
                        command.Parameters.AddWithValue("@userID", Program.userID);
                    }
                    else
                    {
                        // Calls a function that returns a SQL query that picks a question completely randomly without using weights
                        command.CommandText = this.getNoWeightCT(connection);
                    }
                    command.Parameters.AddWithValue("@questionType", "MQ");

                    // Runs the SQL code and stores the generated table in the reader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Stores the question, answer and questionID obtained from the SELECT statement
                        while (reader.Read())
                        {
                            // Stores the model answer and the keywords
                            if (reader.GetString(3) == "MQE")
                            {
                                // Stores the question, answer and questionID obtained from the SELECT statement
                                this.question = reader.GetString(0);
                                equation = reader.GetString(1);
                                this.questionID = reader.GetInt32(2);

                                // Changes the value of the boolean variable found to true
                                found = true;
                            }
                            else if (reader.GetString(3) == "MQR")
                            {
                                // Stores the ranges obtained from the SELECT statement in the string variable ranges
                                ranges = reader.GetString(1);
                            }
                            else if (reader.GetString(3) == "MQDP")
                            {
                                // Stores the value of decimal places in the integer variable dp
                                if (!Int32.TryParse(reader.GetString(1), out dp))
                                {
                                    MessageBox.Show("ERROR with parsing decimal places");
                                }
                            }
                            else
                            {
                                MessageBox.Show("ERROR with answer type stored in database");
                            }
                        }

                        // Clears the parameters
                        command.Parameters.Clear();
                    }
                }

                // Ranges will be formatted like this: "10:20;60:70;35:45"

                // Stores the ranges obtained from the SELECT statement in a new string array called rangesArray
                string[] rangesArray = ranges.Split(';');

                // Splits the ranges up into bounds for each range
                foreach (string item in rangesArray)
                {
                    // Splits the ranges up into bounds
                    string[] bounds = item.Split(':');

                    // Checks whether there are two bounds stored in the array, i.e. the lower and upper bound
                    if (bounds.Length == 2)
                    {
                        // Declares and initialises the upper and lower bounds to 0
                        float lBound = 0;
                        float uBound = 0;

                        // Tries to parse the bounds from string to float and otherwise returns an error
                        if (float.TryParse(bounds[0], out lBound) && float.TryParse(bounds[1], out uBound))
                        {
                            // Generates a random value between the lower and upper bound to the number of decimal places stated and stores it in the string list values
                            values.Add((
                                (float)rnd.Next(
                                (Int32)(lBound * Math.Pow(10, dp)),
                                (Int32)(uBound * Math.Pow(10, dp) + 1))
                                * Math.Pow(10, -dp)).ToString());
                        }
                        else
                        {
                            MessageBox.Show("ERROR with parsing bounds");
                        }
                    }
                    else
                    {
                        MessageBox.Show("ERROR with bounds notation");
                    }
                }

                // Declares an integer variable called numOfParam and initialises it to 0
                int numOfParam = 0;

                // Declares an boolean variable called numOfParamFound and initialises it to false
                bool numOfParamFound = false;

                // Finds the number of parameters in the equation
                while (!numOfParamFound)
                {
                    if (equation.Contains("{" + numOfParam.ToString() + "}")) numOfParam++;
                    else numOfParamFound = true;
                }

                // Updates the question and answer
                if (numOfParam == values.Count())
                {
                    // Updates the text stored in the attribute question so that values replace the parameters
                    this.question = string.Format(this.question, values.Cast<object>().ToArray());

                    // Calculates and stores the answer
                    this.answer = RPN_Calc(equation, values);
                }
                else
                {
                    MessageBox.Show("ERROR as number of parameters doesn't match number of values");
                }
            }
        }

        // A function that calculates the answer and returns it
        private string RPN_Calc(string equation, List<string> values)
        {
            // Equations will be formatted in RPN like this: "{0} {1} - {2} +"

            // Replaces the parameters in the equation with values
            equation = string.Format(equation, values.Cast<object>().ToArray());

            // Splits the string, equation into a string array that is declared as equationArray
            string[] equationArray = equation.Split(' ');

            // Declares and instantiates a new string stack called calc
            Stack<float> calc = new Stack<float>();

            foreach (string item in equationArray)
            {
                // Declares a float variable called floatItem and initialises it as 0.
                float floatItem = 0;

                // Pushes the item into the stack if it is a number or operates on existing items in the stack
                if (float.TryParse(item, out floatItem))
                {
                    // Pushes the item into the stack
                    calc.Push(floatItem);
                }
                else
                {
                    // Checks whether there is currently at least one value in the stack
                    if (calc.Count() >= 1)
                    {
                        // Declares a new boolean variable called operated and initialises it as false
                        bool operated = false;

                        // Checks whether there are currently at least two values in the stack and whether the operation has already been carried out
                        if (calc.Count() >= 2)
                        {
                            // Pops the top two items from the stack and stores the values in two float variables called x and y
                            float y = calc.Pop();
                            float x = calc.Pop();

                            // Operates on the two values on x and y depending on the operator and then pushes the value back onto the stack
                            switch (item)
                            {
                                case "+":
                                    calc.Push(x + y);
                                    operated = true;
                                    break;
                                case "-":
                                    calc.Push(x - y);
                                    operated = true;
                                    break;
                                case "*":
                                    calc.Push(x * y);
                                    operated = true;
                                    break;
                                case "/":
                                    calc.Push(x / y);
                                    operated = true;
                                    break;
                                case "^":
                                    calc.Push((float) Math.Pow(x, y));
                                    operated = true;
                                    break;
                                case "%":
                                    calc.Push(x % y);
                                    operated = true;
                                    break;
                                case "LOG":
                                    calc.Push((float) Math.Log(x, y));
                                    operated = true;
                                    break;
                                default:
                                    calc.Push(x);
                                    calc.Push(y);
                                    break;
                            }
                        }

                        // Checks whether an operation has already been carried out
                        if (!operated)
                        {
                            // Pops the top item from the stack and stores the value in a float variable called z
                            float z = calc.Pop();

                            // Operates on the value on z depending on the operator and then pushes the value back onto the stack
                            switch (item)
                            {
                                // I have also had to convert from radians to degrees as the Math function returns values in radians
                                case "SIN":
                                    calc.Push((float)Math.Sin(z * 2 * Math.PI / 360));
                                    break;
                                case "COS":
                                    calc.Push((float)Math.Cos(z * 2 * Math.PI / 360));
                                    break;
                                case "TAN":
                                    calc.Push((float)Math.Tan(z * 2 * Math.PI / 360));
                                    break;
                                case "ARCSIN":
                                    calc.Push((float)(Math.Asin(z) * 360 / (2 * Math.PI)));
                                    break;
                                case "ARCCOS":
                                    calc.Push((float)(Math.Acos(z) * 360 / (2 * Math.PI)));
                                    break;
                                case "ARCTAN":
                                    calc.Push((float)(Math.Atan(z) * 360 / (2 * Math.PI)));
                                    break;
                                default:
                                    calc.Push(z);
                                    MessageBox.Show("ERROR as an invalid operator has been used");
                                    break;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("ERROR as there are not enough items in the stack or an invalid operator has been used");
                    }
                }
            }

            // Returns the answer
            if (calc.Count() == 1)
            {
                // Returns the calculated answer to 3 significant figures (G3 is 3 s.f. in general format, e.g. "6.63E-34")
                return calc.Pop().ToString("G3");
            }
            else
            {
                MessageBox.Show("ERROR with finding answer in stack");
                return null;
            }
        }
    }

    public class CreateActivity
    {
        private int questionID;

        // Constructor
        public CreateActivity()
        {

        }

        // Returns a counter ID for the counterType in the parameter
        private int getID(SqlConnection connection, string counterType)
        {
            // Opens a new connection if there isn't already a connection open (this just makes sure an error relating to no connection being open won't occur)
            if (connection.State != ConnectionState.Open) connection.Open();

            // Creates a new object called command that can allow SQL code to be run
            using (SqlCommand command = new SqlCommand())
            {
                // Initialises the connection
                command.Connection = connection;

                // A SQL query that returns a scalar value for the counter
                command.CommandText = @"
SELECT MAX(counterID) FROM tblCounters WHERE counterType = @counterType;

-- Increments the question counter
UPDATE C
SET counterID = counterID + 1
FROM tblCounters C
WHERE counterType = @counterType;
";
                command.Parameters.AddWithValue("@counterType", counterType);

                // Returns the counter value
                return (Int32) command.ExecuteScalar();
            }
        }

        // Initialises the question for every existing user, i.e. add it into tblUserAnswers
        private void initialiseQForEveryUser(SqlConnection connection)
        {
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
                            initialiseQ(connection2, reader.GetString(0));



                            // Closes the connection to the database
                            connection2.Close();
                        }
                    }
                }
            }
        }

        // Initialises the question given in as a parameter for the user given in the parameter
        private void initialiseQ(SqlConnection connection, string aUserID)
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
INSERT INTO tblUserAnswers (userID, questionID, numberOfAttempts, numberOfCorrectAttempts)
VALUES (@userID, @questionID, 0, 0);
";
                command.Parameters.AddWithValue("@userID", aUserID);
                command.Parameters.AddWithValue("@questionID", this.questionID);

                // Runs the SQL code
                command.ExecuteNonQuery();

                // Clears the parameters
                command.Parameters.Clear();
            }
        }

        // Method that adds a new question to the database
        private void createQ(SqlConnection connection, string question, string questionType)
        {
            // Updates the attribute questionID with the question ID
            this.questionID = this.getID(connection, "QID");

            // Opens a new connection if there isn't already a connection open (this just makes sure an error relating to no connection being open won't occur)
            if (connection.State != ConnectionState.Open) connection.Open();

            // Creates a new object called command that can allow SQL code to be run
            using (SqlCommand command = new SqlCommand())
            {
                // Initialises the connection
                command.Connection = connection;

                // A SQL query that inputs the question
                command.CommandText = @"
-- Inserts a new question into the question table
INSERT INTO tblQuestions (questionID, question, questionType)
VALUES (@questionCount, @question, @questionType);
";
                command.Parameters.AddWithValue("@questionCount", this.questionID);
                command.Parameters.AddWithValue("@question", question);
                command.Parameters.AddWithValue("@questionType", questionType);

                // Runs the SQL code
                command.ExecuteNonQuery();

                // Initialises the question for every user
                initialiseQForEveryUser(connection);
            }
        }

        // Method that adds a new answer to the database
        private void createA(SqlConnection connection, string answer, string answerType)
        {
            // Opens a new connection if there isn't already a connection open (this just makes sure an error relating to no connection being open won't occur)
            if (connection.State != ConnectionState.Open) connection.Open();

            // Creates a new object called command that can allow SQL code to be run
            using (SqlCommand command = new SqlCommand())
            {
                // Initialises the connection
                command.Connection = connection;

                // A SQL query that inputs the answer
                command.CommandText = @"
-- Inserts a new answer into the answer table
INSERT INTO tblAnswers (answerID, answer, answerType, questionID)
VALUES(@answerCount, @answer, @answerType, @questionCount);
";
                command.Parameters.AddWithValue("@answerCount", getID(connection, "AID"));
                command.Parameters.AddWithValue("@questionCount", this.questionID);
                command.Parameters.AddWithValue("@answer", answer);
                command.Parameters.AddWithValue("@answerType", answerType);

                // Runs the SQL code
                command.ExecuteNonQuery();
            }
        }

        // A procedure that adds a new question and answer to the database
        public void createQna(SqlConnection connection, string question, string answer)
        {
            createQ(connection, question, "QNA");
            createA(connection, answer.ToUpper(), "QNA");
        }

        // A procedure that adds a new question and four new answers to the database
        public void createTimedMC(SqlConnection connection, string question, string answerT, string answerF1, string answerF2, string answerF3)
        {
            createQ(connection, question, "MULT");
            createA(connection, answerT, "MULTT");
            createA(connection, answerF1, "MULTF");
            createA(connection, answerF2, "MULTF");
            createA(connection, answerF3, "MULTF");
        }

        // A procedure that adds a new question, keywords and a model answer to the database
        public void createWordedQ(SqlConnection connection, string question, string mAnswer, List<string> keyWords)
        {
            createQ(connection, question, "WQ");
            createA(connection, mAnswer, "WQA");

            foreach (string item in keyWords)
            {
                createA(connection, item.ToUpper(), "WQK");
            }
        }

        // A procedure that adds a new question, the equation, the ranges and the decimal places of the ranges
        public void createMathQ(SqlConnection connection, string question, string equation, string ranges, string dp)
        {
            // Equations will be formatted in RPN like this: "{0} {1} - {2} +"
            // Ranges will be formatted like this: "10:20;60:70;35:45"

            createQ(connection, question, "MQ");
            createA(connection, equation.ToUpper(), "MQE");
            createA(connection, ranges, "MQR");
            createA(connection, dp, "MQDP");
        }
    }
}