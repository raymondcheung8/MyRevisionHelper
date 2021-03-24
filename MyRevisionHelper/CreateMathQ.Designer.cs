namespace MyRevisionHelper
{
    partial class CreateMathQ
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
            this.equation_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.question_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ranges_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dp_textBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
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
            this.store_btn.Location = new System.Drawing.Point(179, 189);
            this.store_btn.Name = "store_btn";
            this.store_btn.Size = new System.Drawing.Size(93, 23);
            this.store_btn.TabIndex = 12;
            this.store_btn.Text = "Store question";
            this.store_btn.UseVisualStyleBackColor = true;
            this.store_btn.Click += new System.EventHandler(this.store_btn_Click);
            // 
            // equation_textBox
            // 
            this.equation_textBox.Location = new System.Drawing.Point(8, 85);
            this.equation_textBox.Name = "equation_textBox";
            this.equation_textBox.Size = new System.Drawing.Size(264, 20);
            this.equation_textBox.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Enter equation below:";
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
            // ranges_textBox
            // 
            this.ranges_textBox.Location = new System.Drawing.Point(8, 124);
            this.ranges_textBox.Name = "ranges_textBox";
            this.ranges_textBox.Size = new System.Drawing.Size(264, 20);
            this.ranges_textBox.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Enter ranges below:";
            // 
            // dp_textBox
            // 
            this.dp_textBox.Location = new System.Drawing.Point(8, 163);
            this.dp_textBox.Name = "dp_textBox";
            this.dp_textBox.Size = new System.Drawing.Size(264, 20);
            this.dp_textBox.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(230, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Enter the amount of dp the values are in below:";
            // 
            // CreateMathQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 221);
            this.Controls.Add(this.dp_textBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ranges_textBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.store_btn);
            this.Controls.Add(this.equation_textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.question_textBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.goBack_lbl);
            this.Name = "CreateMathQ";
            this.Text = "CreateMathQ";
            this.Load += new System.EventHandler(this.CreateMathQ_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label goBack_lbl;
        private System.Windows.Forms.Button store_btn;
        private System.Windows.Forms.TextBox equation_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox question_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ranges_textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox dp_textBox;
        private System.Windows.Forms.Label label4;
    }
}