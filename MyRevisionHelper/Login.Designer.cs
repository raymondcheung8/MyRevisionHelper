namespace MyRevisionHelper
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.homeIcon = new System.Windows.Forms.PictureBox();
            this.newLoginLbl = new System.Windows.Forms.Label();
            this.password_textbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.username_textbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.login_btn = new System.Windows.Forms.Button();
            this.guestLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.homeIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // homeIcon
            // 
            this.homeIcon.Image = ((System.Drawing.Image)(resources.GetObject("homeIcon.Image")));
            this.homeIcon.Location = new System.Drawing.Point(5, 5);
            this.homeIcon.Name = "homeIcon";
            this.homeIcon.Size = new System.Drawing.Size(16, 16);
            this.homeIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.homeIcon.TabIndex = 8;
            this.homeIcon.TabStop = false;
            this.homeIcon.Click += new System.EventHandler(this.homeIcon_Click);
            // 
            // newLoginLbl
            // 
            this.newLoginLbl.AutoSize = true;
            this.newLoginLbl.Location = new System.Drawing.Point(12, 99);
            this.newLoginLbl.Name = "newLoginLbl";
            this.newLoginLbl.Size = new System.Drawing.Size(156, 13);
            this.newLoginLbl.TabIndex = 9;
            this.newLoginLbl.Text = "Click here to create a new login";
            this.newLoginLbl.Click += new System.EventHandler(this.newLoginLbl_Click);
            // 
            // password_textbox
            // 
            this.password_textbox.Location = new System.Drawing.Point(76, 47);
            this.password_textbox.Name = "password_textbox";
            this.password_textbox.Size = new System.Drawing.Size(100, 20);
            this.password_textbox.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Password:";
            // 
            // username_textbox
            // 
            this.username_textbox.Location = new System.Drawing.Point(76, 21);
            this.username_textbox.Name = "username_textbox";
            this.username_textbox.Size = new System.Drawing.Size(100, 20);
            this.username_textbox.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Username:";
            // 
            // login_btn
            // 
            this.login_btn.Location = new System.Drawing.Point(76, 73);
            this.login_btn.Name = "login_btn";
            this.login_btn.Size = new System.Drawing.Size(100, 23);
            this.login_btn.TabIndex = 14;
            this.login_btn.Text = "Login";
            this.login_btn.UseVisualStyleBackColor = true;
            this.login_btn.Click += new System.EventHandler(this.login_btn_Click);
            // 
            // guestLbl
            // 
            this.guestLbl.AutoSize = true;
            this.guestLbl.Location = new System.Drawing.Point(12, 117);
            this.guestLbl.Name = "guestLbl";
            this.guestLbl.Size = new System.Drawing.Size(156, 13);
            this.guestLbl.TabIndex = 15;
            this.guestLbl.Text = "Or click here to login as a guest";
            this.guestLbl.Click += new System.EventHandler(this.guestLbl_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 136);
            this.Controls.Add(this.guestLbl);
            this.Controls.Add(this.login_btn);
            this.Controls.Add(this.password_textbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.username_textbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newLoginLbl);
            this.Controls.Add(this.homeIcon);
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.homeIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox homeIcon;
        private System.Windows.Forms.Label newLoginLbl;
        private System.Windows.Forms.TextBox password_textbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox username_textbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button login_btn;
        private System.Windows.Forms.Label guestLbl;

    }
}