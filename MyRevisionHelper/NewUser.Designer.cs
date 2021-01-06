namespace MyRevisionHelper
{
    partial class NewUser
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
            this.goBackLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.username_textbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.password_textbox = new System.Windows.Forms.TextBox();
            this.confirm_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // goBackLbl
            // 
            this.goBackLbl.AutoSize = true;
            this.goBackLbl.Location = new System.Drawing.Point(5, 5);
            this.goBackLbl.Name = "goBackLbl";
            this.goBackLbl.Size = new System.Drawing.Size(172, 13);
            this.goBackLbl.TabIndex = 0;
            this.goBackLbl.Text = "Click here to go back to login page";
            this.goBackLbl.Click += new System.EventHandler(this.goBackLbl_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username:";
            // 
            // username_textbox
            // 
            this.username_textbox.Location = new System.Drawing.Point(76, 27);
            this.username_textbox.Name = "username_textbox";
            this.username_textbox.Size = new System.Drawing.Size(100, 20);
            this.username_textbox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // password_textbox
            // 
            this.password_textbox.Location = new System.Drawing.Point(76, 53);
            this.password_textbox.Name = "password_textbox";
            this.password_textbox.Size = new System.Drawing.Size(100, 20);
            this.password_textbox.TabIndex = 4;
            // 
            // confirm_btn
            // 
            this.confirm_btn.Location = new System.Drawing.Point(76, 79);
            this.confirm_btn.Name = "confirm_btn";
            this.confirm_btn.Size = new System.Drawing.Size(101, 23);
            this.confirm_btn.TabIndex = 5;
            this.confirm_btn.Text = "Create new user";
            this.confirm_btn.UseVisualStyleBackColor = true;
            this.confirm_btn.Click += new System.EventHandler(this.confirm_btn_Click);
            // 
            // NewUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 111);
            this.Controls.Add(this.confirm_btn);
            this.Controls.Add(this.password_textbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.username_textbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.goBackLbl);
            this.Name = "NewUser";
            this.Text = "NewUser";
            this.Load += new System.EventHandler(this.NewUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label goBackLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox username_textbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox password_textbox;
        private System.Windows.Forms.Button confirm_btn;
    }
}