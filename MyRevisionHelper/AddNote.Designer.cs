namespace MyRevisionHelper
{
    partial class AddNote
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
            this.newNote = new System.Windows.Forms.TextBox();
            this.storeNote_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // goBack_lbl
            // 
            this.goBack_lbl.AutoSize = true;
            this.goBack_lbl.Location = new System.Drawing.Point(5, 5);
            this.goBack_lbl.Name = "goBack_lbl";
            this.goBack_lbl.Size = new System.Drawing.Size(190, 13);
            this.goBack_lbl.TabIndex = 1;
            this.goBack_lbl.Text = "Click here to go back to previous page";
            this.goBack_lbl.Click += new System.EventHandler(this.goBack_lbl_Click);
            // 
            // newNote
            // 
            this.newNote.Location = new System.Drawing.Point(8, 21);
            this.newNote.Multiline = true;
            this.newNote.Name = "newNote";
            this.newNote.Size = new System.Drawing.Size(264, 199);
            this.newNote.TabIndex = 2;
            // 
            // storeNote_btn
            // 
            this.storeNote_btn.Location = new System.Drawing.Point(197, 226);
            this.storeNote_btn.Name = "storeNote_btn";
            this.storeNote_btn.Size = new System.Drawing.Size(75, 23);
            this.storeNote_btn.TabIndex = 3;
            this.storeNote_btn.Text = "Store note";
            this.storeNote_btn.UseVisualStyleBackColor = true;
            this.storeNote_btn.Click += new System.EventHandler(this.storeNote_btn_Click);
            // 
            // AddNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.storeNote_btn);
            this.Controls.Add(this.newNote);
            this.Controls.Add(this.goBack_lbl);
            this.Name = "AddNote";
            this.Text = "AddNote";
            this.Load += new System.EventHandler(this.AddNote_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label goBack_lbl;
        private System.Windows.Forms.TextBox newNote;
        private System.Windows.Forms.Button storeNote_btn;
    }
}