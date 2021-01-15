namespace MyRevisionHelper
{
    partial class Notes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Notes));
            this.homeIcon = new System.Windows.Forms.PictureBox();
            this.addNote_btn = new System.Windows.Forms.Button();
            this.checkNotes_btn = new System.Windows.Forms.Button();
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
            this.homeIcon.TabIndex = 9;
            this.homeIcon.TabStop = false;
            this.homeIcon.Click += new System.EventHandler(this.homeIcon_Click);
            // 
            // addNote_btn
            // 
            this.addNote_btn.Location = new System.Drawing.Point(12, 27);
            this.addNote_btn.Name = "addNote_btn";
            this.addNote_btn.Size = new System.Drawing.Size(75, 23);
            this.addNote_btn.TabIndex = 10;
            this.addNote_btn.Text = "Add note";
            this.addNote_btn.UseVisualStyleBackColor = true;
            this.addNote_btn.Click += new System.EventHandler(this.addNote_btn_Click);
            // 
            // checkNotes_btn
            // 
            this.checkNotes_btn.Location = new System.Drawing.Point(93, 27);
            this.checkNotes_btn.Name = "checkNotes_btn";
            this.checkNotes_btn.Size = new System.Drawing.Size(75, 23);
            this.checkNotes_btn.TabIndex = 11;
            this.checkNotes_btn.Text = "Check notes";
            this.checkNotes_btn.UseVisualStyleBackColor = true;
            this.checkNotes_btn.Click += new System.EventHandler(this.checkNotes_btn_Click);
            // 
            // Notes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(174, 61);
            this.Controls.Add(this.checkNotes_btn);
            this.Controls.Add(this.addNote_btn);
            this.Controls.Add(this.homeIcon);
            this.Name = "Notes";
            this.Text = "Notes";
            this.Load += new System.EventHandler(this.Notes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.homeIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox homeIcon;
        private System.Windows.Forms.Button addNote_btn;
        private System.Windows.Forms.Button checkNotes_btn;
    }
}