namespace MyRevisionHelper
{
    partial class MainMenu
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
            this.qna_btn = new System.Windows.Forms.Button();
            this.wordedQ_btn = new System.Windows.Forms.Button();
            this.mathQ_btn = new System.Windows.Forms.Button();
            this.timedMC_btn = new System.Windows.Forms.Button();
            this.notes_btn = new System.Windows.Forms.Button();
            this.break_btn = new System.Windows.Forms.Button();
            this.exit_btn = new System.Windows.Forms.Button();
            this.create_btn = new System.Windows.Forms.Button();
            this.login_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // qna_btn
            // 
            this.qna_btn.Location = new System.Drawing.Point(12, 12);
            this.qna_btn.Name = "qna_btn";
            this.qna_btn.Size = new System.Drawing.Size(100, 100);
            this.qna_btn.TabIndex = 0;
            this.qna_btn.Text = "Simple question and answer activity";
            this.qna_btn.UseVisualStyleBackColor = true;
            this.qna_btn.Click += new System.EventHandler(this.qna_btn_Click);
            // 
            // wordedQ_btn
            // 
            this.wordedQ_btn.Location = new System.Drawing.Point(118, 12);
            this.wordedQ_btn.Name = "wordedQ_btn";
            this.wordedQ_btn.Size = new System.Drawing.Size(100, 100);
            this.wordedQ_btn.TabIndex = 1;
            this.wordedQ_btn.Text = "Exam style questions activity that require worded answers";
            this.wordedQ_btn.UseVisualStyleBackColor = true;
            this.wordedQ_btn.Click += new System.EventHandler(this.wordedQ_btn_Click);
            // 
            // mathQ_btn
            // 
            this.mathQ_btn.Location = new System.Drawing.Point(224, 12);
            this.mathQ_btn.Name = "mathQ_btn";
            this.mathQ_btn.Size = new System.Drawing.Size(100, 100);
            this.mathQ_btn.TabIndex = 2;
            this.mathQ_btn.Text = "Exam style questions activity that are mathematical problems";
            this.mathQ_btn.UseVisualStyleBackColor = true;
            this.mathQ_btn.Click += new System.EventHandler(this.mathQ_btn_Click);
            // 
            // timedMC_btn
            // 
            this.timedMC_btn.Location = new System.Drawing.Point(330, 12);
            this.timedMC_btn.Name = "timedMC_btn";
            this.timedMC_btn.Size = new System.Drawing.Size(100, 100);
            this.timedMC_btn.TabIndex = 3;
            this.timedMC_btn.Text = "Quickfire multiple choice activity";
            this.timedMC_btn.UseVisualStyleBackColor = true;
            this.timedMC_btn.Click += new System.EventHandler(this.timedMC_btn_Click);
            // 
            // notes_btn
            // 
            this.notes_btn.Location = new System.Drawing.Point(12, 147);
            this.notes_btn.Name = "notes_btn";
            this.notes_btn.Size = new System.Drawing.Size(75, 23);
            this.notes_btn.TabIndex = 4;
            this.notes_btn.Text = "Notes";
            this.notes_btn.UseVisualStyleBackColor = true;
            this.notes_btn.Click += new System.EventHandler(this.notes_btn_Click);
            // 
            // break_btn
            // 
            this.break_btn.Location = new System.Drawing.Point(93, 147);
            this.break_btn.Name = "break_btn";
            this.break_btn.Size = new System.Drawing.Size(256, 23);
            this.break_btn.TabIndex = 5;
            this.break_btn.Text = "Break";
            this.break_btn.UseVisualStyleBackColor = true;
            this.break_btn.Click += new System.EventHandler(this.break_btn_Click);
            // 
            // exit_btn
            // 
            this.exit_btn.Location = new System.Drawing.Point(355, 147);
            this.exit_btn.Name = "exit_btn";
            this.exit_btn.Size = new System.Drawing.Size(75, 23);
            this.exit_btn.TabIndex = 6;
            this.exit_btn.Text = "Exit";
            this.exit_btn.UseVisualStyleBackColor = true;
            this.exit_btn.Click += new System.EventHandler(this.exit_btn_Click);
            // 
            // create_btn
            // 
            this.create_btn.Location = new System.Drawing.Point(12, 118);
            this.create_btn.Name = "create_btn";
            this.create_btn.Size = new System.Drawing.Size(418, 23);
            this.create_btn.TabIndex = 7;
            this.create_btn.Text = "Create more questions and answers";
            this.create_btn.UseVisualStyleBackColor = true;
            this.create_btn.Click += new System.EventHandler(this.create_btn_Click);
            // 
            // login_btn
            // 
            this.login_btn.Location = new System.Drawing.Point(12, 12);
            this.login_btn.Name = "login_btn";
            this.login_btn.Size = new System.Drawing.Size(418, 129);
            this.login_btn.TabIndex = 8;
            this.login_btn.Text = "Login";
            this.login_btn.UseVisualStyleBackColor = true;
            this.login_btn.Click += new System.EventHandler(this.login_btn_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 181);
            this.Controls.Add(this.create_btn);
            this.Controls.Add(this.exit_btn);
            this.Controls.Add(this.break_btn);
            this.Controls.Add(this.notes_btn);
            this.Controls.Add(this.timedMC_btn);
            this.Controls.Add(this.mathQ_btn);
            this.Controls.Add(this.wordedQ_btn);
            this.Controls.Add(this.qna_btn);
            this.Controls.Add(this.login_btn);
            this.Name = "MainMenu";
            this.Text = "Main Menu";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button qna_btn;
        private System.Windows.Forms.Button wordedQ_btn;
        private System.Windows.Forms.Button mathQ_btn;
        private System.Windows.Forms.Button timedMC_btn;
        private System.Windows.Forms.Button notes_btn;
        private System.Windows.Forms.Button break_btn;
        private System.Windows.Forms.Button exit_btn;
        private System.Windows.Forms.Button create_btn;
        private System.Windows.Forms.Button login_btn;
    }
}

