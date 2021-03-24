namespace MyRevisionHelper
{
    partial class CheckNotes
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
            this.goBack_lbl = new System.Windows.Forms.Label();
            this.saveNote_btn = new System.Windows.Forms.Button();
            this.deleteNote_btn = new System.Windows.Forms.Button();
            this.nextNote_btn = new System.Windows.Forms.Button();
            this.previousNote_btn = new System.Windows.Forms.Button();
            this.oldNote = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // goBack_lbl
            // 
            this.goBack_lbl.AutoSize = true;
            this.goBack_lbl.Location = new System.Drawing.Point(5, 5);
            this.goBack_lbl.Name = "goBack_lbl";
            this.goBack_lbl.Size = new System.Drawing.Size(190, 13);
            this.goBack_lbl.TabIndex = 2;
            this.goBack_lbl.Text = "Click here to go back to previous page";
            this.goBack_lbl.Click += new System.EventHandler(this.goBack_lbl_Click);
            // 
            // saveNote_btn
            // 
            this.saveNote_btn.Location = new System.Drawing.Point(208, 226);
            this.saveNote_btn.Name = "saveNote_btn";
            this.saveNote_btn.Size = new System.Drawing.Size(64, 23);
            this.saveNote_btn.TabIndex = 4;
            this.saveNote_btn.Text = "Save note";
            this.saveNote_btn.UseVisualStyleBackColor = true;
            this.saveNote_btn.Click += new System.EventHandler(this.saveNote_btn_Click);
            // 
            // deleteNote_btn
            // 
            this.deleteNote_btn.Location = new System.Drawing.Point(132, 226);
            this.deleteNote_btn.Name = "deleteNote_btn";
            this.deleteNote_btn.Size = new System.Drawing.Size(70, 23);
            this.deleteNote_btn.TabIndex = 7;
            this.deleteNote_btn.Text = "Delete note";
            this.deleteNote_btn.UseVisualStyleBackColor = true;
            this.deleteNote_btn.Click += new System.EventHandler(this.deleteNote_btn_Click);
            // 
            // nextNote_btn
            // 
            this.nextNote_btn.Location = new System.Drawing.Point(70, 226);
            this.nextNote_btn.Name = "nextNote_btn";
            this.nextNote_btn.Size = new System.Drawing.Size(56, 23);
            this.nextNote_btn.TabIndex = 5;
            this.nextNote_btn.Text = "Next";
            this.nextNote_btn.UseVisualStyleBackColor = true;
            this.nextNote_btn.Click += new System.EventHandler(this.nextNote_btn_Click);
            // 
            // previousNote_btn
            // 
            this.previousNote_btn.Location = new System.Drawing.Point(8, 226);
            this.previousNote_btn.Name = "previousNote_btn";
            this.previousNote_btn.Size = new System.Drawing.Size(56, 23);
            this.previousNote_btn.TabIndex = 6;
            this.previousNote_btn.Text = "Previous";
            this.previousNote_btn.UseVisualStyleBackColor = true;
            this.previousNote_btn.Click += new System.EventHandler(this.previousNote_btn_Click);
            // 
            // oldNote
            // 
            this.oldNote.Location = new System.Drawing.Point(8, 21);
            this.oldNote.Multiline = true;
            this.oldNote.Name = "oldNote";
            this.oldNote.Size = new System.Drawing.Size(264, 199);
            this.oldNote.TabIndex = 3;
            // 
            // CheckNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.deleteNote_btn);
            this.Controls.Add(this.previousNote_btn);
            this.Controls.Add(this.nextNote_btn);
            this.Controls.Add(this.saveNote_btn);
            this.Controls.Add(this.oldNote);
            this.Controls.Add(this.goBack_lbl);
            this.Name = "CheckNotes";
            this.Text = "CheckNotes";
            this.Load += new System.EventHandler(this.CheckNotes_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label goBack_lbl;
        private System.Windows.Forms.Button saveNote_btn;
        private System.Windows.Forms.Button deleteNote_btn;
        private System.Windows.Forms.Button nextNote_btn;
        private System.Windows.Forms.Button previousNote_btn;
        private System.Windows.Forms.TextBox oldNote;
    }
}