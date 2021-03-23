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
    public partial class TimedMC : Form
    {
        // A local integer variable is created with a scope of the TimedMC form that holds the value of the number of questions
        private int numOfQVal;

        // A local integer variable is created with a scope of the TimedMC form that holds the next question number
        private int nextQNo;

        // A local integer variable is created with a scope of the TimedMC form that holds the number of questions answered correctly
        private int numOfQAnsCorr;

        // Creates an instance of the object TimedMCActivity
        private TimedMCActivity activity = new TimedMCActivity();

        // A new timer to countdown the time left until the break ends is created
        private Timer timer = new Timer();

        // Declares an integer variable that stores the value for the amount of time the user is allocated
        private int timeAllocated;

        // Declares an integer variable that stores the value for the time left before the time is up
        private int timeLeft;

        // Declares an integer variable that stores the position of the correct answer
        private int corrAns;

        // Instantiates a new object that generates pseudorandom integers using the system clock
        private Random rnd = new Random();

        // Constructor
        public TimedMC()
        {
            InitializeComponent();
        }

        // Main method
        private void TimedMC_Load(object sender, EventArgs e)
        {
            // Creates a new NumOfQ form
            using (NumOfQ newForm = new NumOfQ())
            {
                // Hides the Qna form
                this.Hide();

                // Displays the new NumOfQ form and if the NumOfQ form is closed without confirming the number of questions they want to answer, the user is redirected back to the main menu form
                if (newForm.ShowDialog() == DialogResult.OK)
                {
                    // This hides the skip and next button when the form is first loaded
                    skip_btn.Hide();
                    next_btn.Hide();

                    // Initialises the integer variable currentQNo to 2
                    nextQNo = 2;

                    // Initialises the integer variable numOfQAnsCorr to 0
                    numOfQAnsCorr = 0;

                    // This allows the timer to tick
                    timer.Tick += new EventHandler(timer_Tick);

                    // This sets the interval of the timer to 1s
                    timer.Interval = 1000;

                    // Creates a new TimeAllocated form
                    using (TimeAllocated newForm2 = new TimeAllocated())
                    {
                        // Displays the TimeAllocated form and if the TimeAllocated form is closed without confirming the time allocated, the user is redirected back to the main menu form
                        if (newForm2.ShowDialog() == DialogResult.OK)
                        {
                            // This initialises the integer variable timeAllocated to the amount of time the user has requested
                            timeAllocated = newForm2.timeAllocatedVal;

                            // This initialises the integer variable timeLeft to the value in the integer variable timeAllocated
                            timeLeft = timeAllocated;

                            // Releases all resources used by the new form
                            newForm2.Dispose();

                            // Updates the time left displayed in timer_display_lbl
                            timerDisplay_lbl.Text = "Time left: " + timeLeft.ToString();

                            // Initialises the integer variable numOfQVal to the number of questions that are to appear
                            numOfQVal = newForm.numOfQVal;

                            // Releases all resources used by the new form
                            newForm.Dispose();

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



                                    // Randomly selects a question from the database and stores it in the object
                                    activity.weight(connection);

                                    // Calls on a procedure that displays the questions and answers
                                    displayQsAndAs();



                                    // Closes the connection to the database
                                    connection.Close();

                                    // Starts the timer
                                    timer.Start();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error:\n\n" + ex);
                            }
                        }
                        else this.Close();
                    }
                }
                else this.Close();
            }
        }

        // A procedure that displays the questions and answers
        private void displayQsAndAs()
        {
            // Extracts the question from the object and displays it on the question_lbl
            question_lbl.Text = activity.getQuestion();

            // Generates a pseudorandom integer between 1 and 4
            corrAns = rnd.Next(1, 5);

            // Declares and initialises the string array wrongAnswers as the array stored in the activity class
            string[] wrongAnswers = activity.getWrongAnswers();

            // Code for how the questions are displayed
            switch (corrAns)
            {
                case 1:
                    answer1_checkBox.Text = activity.getAnswer();
                    answer2_checkBox.Text = wrongAnswers[0];
                    answer3_checkBox.Text = wrongAnswers[1];
                    answer4_checkBox.Text = wrongAnswers[2];
                    break;
                case 2:
                    answer2_checkBox.Text = activity.getAnswer();
                    answer1_checkBox.Text = wrongAnswers[0];
                    answer3_checkBox.Text = wrongAnswers[1];
                    answer4_checkBox.Text = wrongAnswers[2];
                    break;
                case 3:
                    answer3_checkBox.Text = activity.getAnswer();
                    answer1_checkBox.Text = wrongAnswers[0];
                    answer2_checkBox.Text = wrongAnswers[1];
                    answer4_checkBox.Text = wrongAnswers[2];
                    break;
                case 4:
                    answer4_checkBox.Text = activity.getAnswer();
                    answer1_checkBox.Text = wrongAnswers[0];
                    answer2_checkBox.Text = wrongAnswers[1];
                    answer3_checkBox.Text = wrongAnswers[2];
                    break;
                default:
                    MessageBox.Show("ERROR");
                    break;
            }
        }

        // Method that codes for the decrement of the timeLeft
        private void timer_Tick(object sender, EventArgs e)
        {
            // Decrements the integer variable timeLeft if the value of timeLeft is above 0 and otherwise stops the timer and tells the user the answer
            if (timeLeft > 0)
            {
                // Decrements the integer variable timeLeft
                timeLeft--;

                // Updates the time left displayed in timer_display_lbl
                timerDisplay_lbl.Text = "Time left: " + timeLeft;

                // Displays the skip button if there are only 5 seconds left on the timer
                if (timeLeft == 5) skip_btn.Show();
            }
            else
            {
                // Stops the timer
                timer.Stop();

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



                        // Declares a new integer variable called userAnswer
                        int userAnswer;

                        // Sets the integer variable userAnswer to 1 if answer1_checkBox is checked
                        if (answer1_checkBox.Checked) userAnswer = 1;
                        // Sets the integer variable userAnswer to 2 if answer2_checkBox is checked
                        else if (answer2_checkBox.Checked) userAnswer = 2;
                        // Sets the integer variable userAnswer to 3 if answer3_checkBox is checked
                        else if (answer3_checkBox.Checked) userAnswer = 3;
                        // Sets the integer variable userAnswer to 4 if answer4_checkBox is checked
                        else if (answer4_checkBox.Checked) userAnswer = 4;
                        // Sets the integer variable userAnswer to -1 if no checkboxes are checked
                        else userAnswer = -1;

                        // Increments the field numberOfAttempts in the database
                        if (!Program.guest) activity.incAttempts(connection);

                        if (userAnswer == corrAns)
                        {
                            // Increments the field numberOfCorrectAttempts in the database
                            if (!Program.guest) activity.incCorrectAttempts(connection);

                            // Increments the integer variable numOfQAnsCorr
                            numOfQAnsCorr++;

                            // Tells the user they answered correctly
                            MessageBox.Show("Correct");
                        }
                        else
                        {
                            // Displays a message box with the correct answer
                            MessageBox.Show(string.Format("The correct answer was:\n{0}", activity.getAnswer()));
                        }

                        // Makes the next button visible
                        next_btn.Show();

                        // Makes sure both the skip button and check button are hidden
                        if (skip_btn.Visible == true) skip_btn.Hide();
                        check_btn.Hide();



                        // Closes the connection to the database
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n\n" + ex);
                }
            }
        }

        // Method that closes the TimedMC form and displays the main menu form to the user
        private void homeIcon_Click(object sender, EventArgs e)
        {
            // Stops the timer
            timer.Stop();

            // Closes the form
            this.Close();
        }

        // Unchecks all of the other checkboxes if answer1_checkBox is checked
        private void answer1_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (answer1_checkBox.CheckState == CheckState.Checked)
            {
                answer2_checkBox.CheckState = CheckState.Unchecked;
                answer3_checkBox.CheckState = CheckState.Unchecked;
                answer4_checkBox.CheckState = CheckState.Unchecked;
            }
        }

        // Unchecks all of the other checkboxes if answer2_checkBox is checked
        private void answer2_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (answer2_checkBox.CheckState == CheckState.Checked)
            {
                answer1_checkBox.CheckState = CheckState.Unchecked;
                answer3_checkBox.CheckState = CheckState.Unchecked;
                answer4_checkBox.CheckState = CheckState.Unchecked;
            }
        }

        // Unchecks all of the other checkboxes if answer3_checkBox is checked
        private void answer3_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (answer3_checkBox.CheckState == CheckState.Checked)
            {
                answer1_checkBox.CheckState = CheckState.Unchecked;
                answer2_checkBox.CheckState = CheckState.Unchecked;
                answer4_checkBox.CheckState = CheckState.Unchecked;
            }
        }

        // Unchecks all of the other checkboxes if answer4_checkBox is checked
        private void answer4_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (answer4_checkBox.CheckState == CheckState.Checked)
            {
                answer1_checkBox.CheckState = CheckState.Unchecked;
                answer2_checkBox.CheckState = CheckState.Unchecked;
                answer3_checkBox.CheckState = CheckState.Unchecked;
            }
        }

        // Method that checks the user's answer
        private void check_btn_Click(object sender, EventArgs e)
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



                    // Declares a new integer variable called userAnswer
                    int userAnswer;

                    // Sets the integer variable userAnswer to 1 if answer1_checkBox is checked
                    if (answer1_checkBox.Checked) userAnswer = 1;
                    // Sets the integer variable userAnswer to 2 if answer2_checkBox is checked
                    else if (answer2_checkBox.Checked) userAnswer = 2;
                    // Sets the integer variable userAnswer to 3 if answer3_checkBox is checked
                    else if (answer3_checkBox.Checked) userAnswer = 3;
                    // Sets the integer variable userAnswer to 4 if answer4_checkBox is checked
                    else if (answer4_checkBox.Checked) userAnswer = 4;
                    // Sets the integer variable userAnswer to -1 if no checkboxes are checked
                    else userAnswer = -1;

                    // Only validates the user's answer if the user has given one
                    if (userAnswer != -1)
                    {
                        // Stops the timer
                        timer.Stop();

                        // Increments the field numberOfAttempts in the database
                        if (!Program.guest) activity.incAttempts(connection);

                        if (userAnswer == corrAns)
                        {
                            // Increments the field numberOfCorrectAttempts in the database
                            if (!Program.guest) activity.incCorrectAttempts(connection);

                            // Increments the integer variable numOfQAnsCorr
                            numOfQAnsCorr++;

                            // Tells the user they answered correctly
                            MessageBox.Show("Correct");
                        }
                        else
                        {
                            // Displays a message box with the correct answer
                            MessageBox.Show(string.Format("The correct answer was:\n{0}", activity.getAnswer()));
                        }

                        // Makes the next button visible
                        next_btn.Show();

                        // Makes sure both the skip button and check button are hidden
                        if (skip_btn.Visible == true) skip_btn.Hide();
                        check_btn.Hide();
                    }
                    else
                    {
                        // Tells the user to enter an answer
                        MessageBox.Show("Please enter an answer");
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

        // Method that selects another question
        private void next_btn_Click(object sender, EventArgs e)
        {
            if (nextQNo <= numOfQVal)
            {
                // This changes the value of the integer variable timeLeft to the value of the integer variable timeAllocated
                timeLeft = timeAllocated;

                // Updates the time left displayed in timer_display_lbl
                timerDisplay_lbl.Text = "Time left: " + timeLeft;

                // Starts the timer
                timer.Start();

                // Hides the next button
                next_btn.Hide();

                // Shows the check button
                check_btn.Show();

                // Tries to create a connection to the database and otherwise catches the error and tells the user what the error is
                try
                {
                    // Declaring the connection
                    using (SqlConnection connection = new SqlConnection())
                    {

                        // This allows us to connect to SQL Server
                        connection.ConnectionString = Program.connectionString;

                        // Unchecks all of the checkboxes
                        answer1_checkBox.CheckState = CheckState.Unchecked;
                        answer2_checkBox.CheckState = CheckState.Unchecked;
                        answer3_checkBox.CheckState = CheckState.Unchecked;
                        answer4_checkBox.CheckState = CheckState.Unchecked;

                        // Opens the connection to the database
                        connection.Open();



                        // Randomly selects a question from the database and stores it in the object
                        activity.weight(connection);

                        // Calls on a procedure that displays the questions and answers
                        displayQsAndAs();



                        // Closes the connection to the database
                        connection.Close();

                        // Increments nextQNo
                        nextQNo++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n\n" + ex);
                }
            }
            else
            {
                // Tells the user how many questions they managed to find an answer for
                MessageBox.Show(string.Format("End of activity, you scored:\n{0}/{1}", numOfQAnsCorr.ToString(), numOfQVal.ToString()));
                this.Close();
            }
        }

        // Method that skips the question
        private void skip_btn_Click(object sender, EventArgs e)
        {
            // Stops the timer
            timer.Stop();

            // Displays a message box with the correct answer
            MessageBox.Show(string.Format("The correct answer was:\n{0}", activity.getAnswer()));

            if (nextQNo <= numOfQVal)
            {
                // This changes the value of the integer variable timeLeft to the value of the integer variable timeAllocated
                timeLeft = timeAllocated;

                // Updates the time left displayed in timer_display_lbl
                timerDisplay_lbl.Text = "Time left: " + timeLeft;

                // Starts the timer
                timer.Start();

                // Hides the skip button
                skip_btn.Hide();

                // Shows the check button
                check_btn.Show();

                // Tries to create a connection to the database and otherwise catches the error and tells the user what the error is
                try
                {
                    // Declaring the connection
                    using (SqlConnection connection = new SqlConnection())
                    {
                        // This allows us to connect to SQL Server
                        connection.ConnectionString = Program.connectionString;

                        // Unchecks all of the checkboxes
                        answer1_checkBox.CheckState = CheckState.Unchecked;
                        answer2_checkBox.CheckState = CheckState.Unchecked;
                        answer3_checkBox.CheckState = CheckState.Unchecked;
                        answer4_checkBox.CheckState = CheckState.Unchecked;

                        // Opens the connection to the database
                        connection.Open();



                        // Randomly selects a question from the database and stores it in the object
                        activity.weight(connection);

                        // Calls on a procedure that displays the questions and answers
                        displayQsAndAs();



                        // Closes the connection to the database
                        connection.Close();

                        // Increments nextQNo
                        nextQNo++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n\n" + ex);
                }
            }
            else
            {
                // Tells the user how many questions they managed to find an answer for
                MessageBox.Show(string.Format("End of activity, you scored:\n{0}/{1}", numOfQAnsCorr.ToString(), numOfQVal.ToString()));
                this.Close();
            }
        }
    }
}