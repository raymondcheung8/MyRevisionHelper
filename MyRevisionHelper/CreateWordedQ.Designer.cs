namespace MyRevisionHelper
{
    partial class CreateWordedQ
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
            this.store_btn = new System.Windows.Forms.Button();
            this.keywords_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.question_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.answer_textBox = new System.Windows.Forms.RichTextBox();
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
            // store_btn
            // 
            this.store_btn.Location = new System.Drawing.Point(179, 226);
            this.store_btn.Name = "store_btn";
            this.store_btn.Size = new System.Drawing.Size(93, 23);
            this.store_btn.TabIndex = 12;
            this.store_btn.Text = "Store question";
            this.store_btn.UseVisualStyleBackColor = true;
            this.store_btn.Click += new System.EventHandler(this.store_btn_Click);
            // 
            // keywords_textBox
            // 
            this.keywords_textBox.Location = new System.Drawing.Point(8, 85);
            this.keywords_textBox.Name = "keywords_textBox";
            this.keywords_textBox.Size = new System.Drawing.Size(264, 20);
            this.keywords_textBox.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(264, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Enter keywords below (use commas to separate them):";
            // 
            // question_textBox
            // 
            this.question_textBox.Location = new System.Drawing.Point(8, 46);
            this.question_textBox.Name = "question_textBox";
            this.question_textBox.Size = new System.Drawing.Size(264, 20);
            this.question_textBox.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Enter question below:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Enter model answer below:";
            // 
            // answer_textBox
            // 
            this.answer_textBox.Location = new System.Drawing.Point(8, 124);
            this.answer_textBox.Name = "answer_textBox";
            this.answer_textBox.Size = new System.Drawing.Size(264, 96);
            this.answer_textBox.TabIndex = 23;
            this.answer_textBox.Text = "";
            // 
            // CreateWordedQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.answer_textBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.store_btn);
            this.Controls.Add(this.keywords_textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.question_textBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.goBack_lbl);
            this.Name = "CreateWordedQ";
            this.Text = "CreateWordedQ";
            this.Load += new System.EventHandler(this.CreateWordedQ_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label goBack_lbl;
        private System.Windows.Forms.Button store_btn;
        private System.Windows.Forms.TextBox keywords_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox question_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox answer_textBox;
    }
}