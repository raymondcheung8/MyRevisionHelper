namespace MyRevisionHelper
{
    partial class Break
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Break));
            this.label1 = new System.Windows.Forms.Label();
            this.hrs = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.mins = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.secs = new System.Windows.Forms.NumericUpDown();
            this.start_btn = new System.Windows.Forms.Button();
            this.homeIcon = new System.Windows.Forms.PictureBox();
            this.pause_btn = new System.Windows.Forms.Button();
            this.reset_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.hrs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.secs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.homeIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hours";
            // 
            // hrs
            // 
            this.hrs.Location = new System.Drawing.Point(68, 16);
            this.hrs.Name = "hrs";
            this.hrs.Size = new System.Drawing.Size(33, 20);
            this.hrs.TabIndex = 1;
            this.hrs.ValueChanged += new System.EventHandler(this.hrs_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Minutes";
            // 
            // mins
            // 
            this.mins.Location = new System.Drawing.Point(157, 16);
            this.mins.Name = "mins";
            this.mins.Size = new System.Drawing.Size(33, 20);
            this.mins.TabIndex = 3;
            this.mins.ValueChanged += new System.EventHandler(this.mins_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(196, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Seconds";
            // 
            // secs
            // 
            this.secs.Location = new System.Drawing.Point(251, 16);
            this.secs.Name = "secs";
            this.secs.Size = new System.Drawing.Size(33, 20);
            this.secs.TabIndex = 5;
            this.secs.ValueChanged += new System.EventHandler(this.secs_ValueChanged);
            // 
            // start_btn
            // 
            this.start_btn.Location = new System.Drawing.Point(290, 16);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(50, 20);
            this.start_btn.TabIndex = 6;
            this.start_btn.Text = "Start";
            this.start_btn.UseVisualStyleBackColor = true;
            this.start_btn.Click += new System.EventHandler(this.start_btn_Click);
            // 
            // homeIcon
            // 
            this.homeIcon.Image = ((System.Drawing.Image)(resources.GetObject("homeIcon.Image")));
            this.homeIcon.Location = new System.Drawing.Point(5, 5);
            this.homeIcon.Name = "homeIcon";
            this.homeIcon.Size = new System.Drawing.Size(16, 16);
            this.homeIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.homeIcon.TabIndex = 7;
            this.homeIcon.TabStop = false;
            this.homeIcon.Click += new System.EventHandler(this.homeIcon_Click);
            // 
            // pause_btn
            // 
            this.pause_btn.Location = new System.Drawing.Point(346, 16);
            this.pause_btn.Name = "pause_btn";
            this.pause_btn.Size = new System.Drawing.Size(50, 20);
            this.pause_btn.TabIndex = 8;
            this.pause_btn.Text = "Pause";
            this.pause_btn.UseVisualStyleBackColor = true;
            this.pause_btn.Click += new System.EventHandler(this.pause_btn_Click);
            // 
            // reset_btn
            // 
            this.reset_btn.Location = new System.Drawing.Point(402, 16);
            this.reset_btn.Name = "reset_btn";
            this.reset_btn.Size = new System.Drawing.Size(50, 20);
            this.reset_btn.TabIndex = 9;
            this.reset_btn.Text = "Reset";
            this.reset_btn.UseVisualStyleBackColor = true;
            this.reset_btn.Click += new System.EventHandler(this.reset_btn_Click);
            // 
            // Break
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 46);
            this.Controls.Add(this.reset_btn);
            this.Controls.Add(this.pause_btn);
            this.Controls.Add(this.homeIcon);
            this.Controls.Add(this.start_btn);
            this.Controls.Add(this.secs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mins);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.hrs);
            this.Controls.Add(this.label1);
            this.Name = "Break";
            this.Text = "Break";
            this.Load += new System.EventHandler(this.Break_Load);
            ((System.ComponentModel.ISupportInitialize)(this.hrs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.secs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.homeIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown hrs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown mins;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown secs;
        private System.Windows.Forms.Button start_btn;
        private System.Windows.Forms.PictureBox homeIcon;
        private System.Windows.Forms.Button pause_btn;
        private System.Windows.Forms.Button reset_btn;
    }
}