using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyRevisionHelper
{
    public partial class MainMenu : Form
    {        
        public MainMenu()
        {
            InitializeComponent();
        }

        // Main method
        private void MainMenu_Load(object sender, EventArgs e)
        {
            // Setting button colours
            qna_btn.BackColor = Color.LightGreen;
            qna_btn.FlatStyle = FlatStyle.Flat;
            qna_btn.FlatAppearance.BorderColor = Color.Green;
            wordedQ_btn.BackColor = Color.LightGreen;
            wordedQ_btn.FlatStyle = FlatStyle.Flat;
            wordedQ_btn.FlatAppearance.BorderColor = Color.Green;
            mathQ_btn.BackColor = Color.LightGreen;
            mathQ_btn.FlatStyle = FlatStyle.Flat;
            mathQ_btn.FlatAppearance.BorderColor = Color.Green;
            timedMC_btn.BackColor = Color.LightGreen;
            timedMC_btn.FlatStyle = FlatStyle.Flat;
            timedMC_btn.FlatAppearance.BorderColor = Color.Green;
            create_btn.BackColor = Color.LightBlue;
            create_btn.FlatStyle = FlatStyle.Flat;
            create_btn.FlatAppearance.BorderColor = Color.Blue;
            notes_btn.BackColor = Color.LightYellow;
            notes_btn.FlatStyle = FlatStyle.Flat;
            notes_btn.FlatAppearance.BorderColor = Color.Yellow;
            break_btn.BackColor = Color.LightSalmon;
            break_btn.FlatStyle = FlatStyle.Flat;
            break_btn.FlatAppearance.BorderColor = Color.Orange;
            exit_btn.BackColor = Color.LightCoral;
            exit_btn.FlatStyle = FlatStyle.Flat;
            exit_btn.FlatAppearance.BorderColor = Color.Red;
            login_btn.BackColor = Color.LightSkyBlue;
            login_btn.FlatStyle = FlatStyle.Flat;
            login_btn.FlatAppearance.BorderColor = Color.DeepSkyBlue;

            // Setting form colour
            this.BackColor = Color.White;

            // Hides all the buttons except the login button and exit button
            qna_btn.Hide();
            wordedQ_btn.Hide();
            mathQ_btn.Hide();
            timedMC_btn.Hide();
            create_btn.Hide();
            notes_btn.Hide();
            break_btn.Hide();
        }

        // Method for what happens when login_btn is clicked
        private void login_btn_Click(object sender, EventArgs e)
        {
            // Creates a new login form
            Login newForm = new Login();

            // Hides the main menu form
            this.Hide();

            // Displays the new login form
            if (newForm.ShowDialog() == DialogResult.OK)
            {
                // Shows all the regular buttons
                qna_btn.Show();
                wordedQ_btn.Show();
                mathQ_btn.Show();
                timedMC_btn.Show();
                break_btn.Show();

                // Shows the notes button if the user isn't a guest
                if (!Program.guest) notes_btn.Show();

                // Hides the login button
                login_btn.Hide();

                // Shows the create button if the user is an admin
                if (newForm.admin) create_btn.Show();

                // Releases all resources used by the new form
                newForm.Dispose();
            }

            // Makes the main menu form visible again
            this.Show();
        }

        // Method for what happens when qna_btn is clicked
        private void qna_btn_Click(object sender, EventArgs e)
        {
            // Display a new form and hide the main menu
            Qna newForm = new Qna();
            openNewForm(newForm);
        }

        // Method for what happens when wordedQ_btn is clicked
        private void wordedQ_btn_Click(object sender, EventArgs e)
        {
            // Display a new form and hide the main menu
            WordedQ newForm = new WordedQ();
            openNewForm(newForm);
        }

        // Method for what happens when mathQ_btn is clicked
        private void mathQ_btn_Click(object sender, EventArgs e)
        {
            // Display a new form and hide the main menu
            MathQ newForm = new MathQ();
            openNewForm(newForm);
        }

        // Method for what happens when timedMC_btn is clicked
        private void timedMC_btn_Click(object sender, EventArgs e)
        {
            // Display a new form and hide the main menu
            TimedMC newForm = new TimedMC();
            openNewForm(newForm);
        }

        // Method for what happens when create_btn is clicked
        private void create_btn_Click(object sender, EventArgs e)
        {
            // Display a new form and hide the main menu
            Create newForm = new Create();
            openNewForm(newForm);
        }

        // Method for what happens when notes_btn is clicked
        private void notes_btn_Click(object sender, EventArgs e)
        {
            // Display a new form and hide the main menu
            Notes newForm = new Notes();
            openNewForm(newForm);
        }

        // Method for what happens when break_btn is clicked
        private void break_btn_Click(object sender, EventArgs e)
        {
            // Display a new form and hide the main menu
            Break newForm = new Break();
            openNewForm(newForm);
        }

        // Method for what happens when exit_btn is clicked
        private void exit_btn_Click(object sender, EventArgs e)
        {
            // Display a suitable message and closes the main menu, ending the program
            MessageBox.Show("Thanks for using my software\n-Raymond Cheung");
            this.Close();
        }

        // A procedure that opens a new form when the parameter is an existing form
        private void openNewForm(Form newForm)
        {
            // Redirects the user to the new form and then back to the main menu form when the new form is closed
            this.Hide();
            newForm.ShowDialog();
            this.Show();

            // Releases all resources used by the new form
            newForm.Dispose();
        }
    }
}