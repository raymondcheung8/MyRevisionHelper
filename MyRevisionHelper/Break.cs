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
    public partial class Break : Form
    {
        // A new timer to countdown the time left until the break ends is created
        public Timer breakTimer = new Timer();

        public Break()
        {
            InitializeComponent();
        }

        // Main method
        private void Break_Load(object sender, EventArgs e)
        {
            // This allows the timer to tick
            breakTimer.Tick += new EventHandler(breakTimer_Tick);
            // This sets the interval of the timer to 1s
            breakTimer.Interval = 1000;
        }

        // Method to only allow values within a certain range to be stored in hrs.Value
        private void hrs_ValueChanged(object sender, EventArgs e)
        {
            if (hrs.Value < 0) hrs.Value = 0;
            if (hrs.Value >= 24) hrs.Value = 23;
        }

        // Method to only allow values within a certain range to be stored in mins.Value
        private void mins_ValueChanged(object sender, EventArgs e)
        {
            if (mins.Value < 0) mins.Value = 0;
            if (mins.Value >= 60) mins.Value = 59;
        }

        // Method to only allow values within a certain range to be stored in secs.Value
        private void secs_ValueChanged(object sender, EventArgs e)
        {
            if (secs.Value < 0) secs.Value = 0;
            if (secs.Value >= 60) secs.Value = 59;
        }

        // Method that closes the break form and displays the main menu form to the user
        private void homeIcon_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Method that starts the timer unless the timer hasn't been set
        private void start_btn_Click(object sender, EventArgs e)
        {
            if (secs.Value == 0 && mins.Value == 0 && hrs.Value == 0)
                MessageBox.Show("Please set the timer first!");

            else breakTimer.Start();
        }

        // Method that pauses the timer
        private void pause_btn_Click(object sender, EventArgs e)
        {
            breakTimer.Stop();
        }

        // Method that resets the timer
        private void reset_btn_Click(object sender, EventArgs e)
        {
            breakTimer.Stop();
            secs.Value = 0;
            mins.Value = 0;
            hrs.Value = 0;
        }

        // Method that codes for the decrement of the values of seconds, minutes and hours
        private void breakTimer_Tick(object sender, EventArgs e)
        {
            if (secs.Value > 0)
            {
                secs.Value--;
            }
            else if (secs.Value == 0 && mins.Value == 0 && hrs.Value == 0)
            {
                breakTimer.Stop();
                MessageBox.Show("Break is over");
            }
            else
            {
                secs.Value = 59;
                if (mins.Value > 0)
                {
                    mins.Value--;
                }
                else
                {
                    mins.Value = 59;
                    hrs.Value--;
                }
            }
        }
    }
}