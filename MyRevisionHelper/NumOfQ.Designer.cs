namespace MyRevisionHelper
{
    partial class NumOfQ
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NumOfQ));
            this.homeIcon = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.confirm_btn = new System.Windows.Forms.Button();
            this.numOfQInput = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.homeIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOfQInput)).BeginInit();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "How many questions would you like to answer?";
            // 
            // confirm_btn
            // 
            this.confirm_btn.Location = new System.Drawing.Point(182, 25);
            this.confirm_btn.Name = "confirm_btn";
            this.confirm_btn.Size = new System.Drawing.Size(75, 20);
            this.confirm_btn.TabIndex = 12;
            this.confirm_btn.Text = "Confirm";
            this.confirm_btn.UseVisualStyleBackColor = true;
            this.confirm_btn.Click += new System.EventHandler(this.confirm_btn_Click);
            // 
            // numOfQInput
            // 
            this.numOfQInput.Location = new System.Drawing.Point(30, 25);
            this.numOfQInput.Name = "numOfQInput";
            this.numOfQInput.Size = new System.Drawing.Size(146, 20);
            this.numOfQInput.TabIndex = 13;
            this.numOfQInput.ValueChanged += new System.EventHandler(this.numOfQInput_ValueChanged);
            // 
            // NumOfQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 56);
            this.Controls.Add(this.numOfQInput);
            this.Controls.Add(this.confirm_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.homeIcon);
            this.Name = "NumOfQ";
            this.Text = "NumOfQ";
            this.Load += new System.EventHandler(this.NumOfQ_Load);
            ((System.ComponentModel.ISupportInitialize)(this.homeIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOfQInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox homeIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button confirm_btn;
        private System.Windows.Forms.NumericUpDown numOfQInput;
    }
}