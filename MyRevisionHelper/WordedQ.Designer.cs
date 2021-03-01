namespace MyRevisionHelper
{
    partial class WordedQ
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WordedQ));
            this.homeIcon = new System.Windows.Forms.PictureBox();
            this.question_lbl = new System.Windows.Forms.Label();
            this.answer_textbox = new System.Windows.Forms.RichTextBox();
            this.skip_btn = new System.Windows.Forms.Button();
            this.next_btn = new System.Windows.Forms.Button();
            this.check_btn = new System.Windows.Forms.Button();
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
            this.homeIcon.TabIndex = 11;
            this.homeIcon.TabStop = false;
            this.homeIcon.Click += new System.EventHandler(this.homeIcon_Click);
            // 
            // question_lbl
            // 
            this.question_lbl.Location = new System.Drawing.Point(27, 5);
            this.question_lbl.Name = "question_lbl";
            this.question_lbl.Size = new System.Drawing.Size(300, 104);
            this.question_lbl.TabIndex = 12;
            this.question_lbl.Text = "label1";
            // 
            // answer_textbox
            // 
            this.answer_textbox.Location = new System.Drawing.Point(30, 112);
            this.answer_textbox.Name = "answer_textbox";
            this.answer_textbox.Size = new System.Drawing.Size(297, 96);
            this.answer_textbox.TabIndex = 22;
            this.answer_textbox.Text = "";
            // 
            // skip_btn
            // 
            this.skip_btn.Location = new System.Drawing.Point(30, 214);
            this.skip_btn.Name = "skip_btn";
            this.skip_btn.Size = new System.Drawing.Size(79, 23);
            this.skip_btn.TabIndex = 25;
            this.skip_btn.Text = "Skip question";
            this.skip_btn.UseVisualStyleBackColor = true;
            this.skip_btn.Click += new System.EventHandler(this.skip_btn_Click);
            // 
            // next_btn
            // 
            this.next_btn.Location = new System.Drawing.Point(247, 214);
            this.next_btn.Name = "next_btn";
            this.next_btn.Size = new System.Drawing.Size(80, 23);
            this.next_btn.TabIndex = 24;
            this.next_btn.Text = "Next question";
            this.next_btn.UseVisualStyleBackColor = true;
            this.next_btn.Click += new System.EventHandler(this.next_btn_Click);
            // 
            // check_btn
            // 
            this.check_btn.Location = new System.Drawing.Point(115, 214);
            this.check_btn.Name = "check_btn";
            this.check_btn.Size = new System.Drawing.Size(126, 23);
            this.check_btn.TabIndex = 23;
            this.check_btn.Text = "Check answer";
            this.check_btn.UseVisualStyleBackColor = true;
            this.check_btn.Click += new System.EventHandler(this.check_btn_Click);
            // 
            // WordedQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 246);
            this.Controls.Add(this.skip_btn);
            this.Controls.Add(this.next_btn);
            this.Controls.Add(this.check_btn);
            this.Controls.Add(this.answer_textbox);
            this.Controls.Add(this.question_lbl);
            this.Controls.Add(this.homeIcon);
            this.Name = "WordedQ";
            this.Text = "WordedQ";
            this.Load += new System.EventHandler(this.WordedQ_Load);
            ((System.ComponentModel.ISupportInitialize)(this.homeIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox homeIcon;
        private System.Windows.Forms.Label question_lbl;
        private System.Windows.Forms.RichTextBox answer_textbox;
        private System.Windows.Forms.Button skip_btn;
        private System.Windows.Forms.Button next_btn;
        private System.Windows.Forms.Button check_btn;
    }
}