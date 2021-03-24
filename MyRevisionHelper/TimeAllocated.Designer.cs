namespace MyRevisionHelper
{
    partial class TimeAllocated
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeAllocated));
            this.homeIcon = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timeAllocatedInput = new System.Windows.Forms.NumericUpDown();
            this.confirm_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.homeIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeAllocatedInput)).BeginInit();
            this.SuspendLayout();
            // 
            // homeIcon
            // 
            this.homeIcon.Image = ((System.Drawing.Image)(resources.GetObject("homeIcon.Image")));
            this.homeIcon.Location = new System.Drawing.Point(5, 5);
            this.homeIcon.Name = "homeIcon";
            this.homeIcon.Size = new System.Drawing.Size(16, 16);
            this.homeIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.homeIcon.TabIndex = 10;
            this.homeIcon.TabStop = false;
            this.homeIcon.Click += new System.EventHandler(this.homeIcon_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(27, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 26);
            this.label1.TabIndex = 11;
            this.label1.Text = "How much time do you want to be allocated per question (max is 120s)?";
            // 
            // timeAllocatedInput
            // 
            this.timeAllocatedInput.Location = new System.Drawing.Point(30, 34);
            this.timeAllocatedInput.Name = "timeAllocatedInput";
            this.timeAllocatedInput.Size = new System.Drawing.Size(161, 20);
            this.timeAllocatedInput.TabIndex = 12;
            this.timeAllocatedInput.ValueChanged += new System.EventHandler(this.timeAllocatedInput_ValueChanged);
            // 
            // confirm_btn
            // 
            this.confirm_btn.Location = new System.Drawing.Point(197, 34);
            this.confirm_btn.Name = "confirm_btn";
            this.confirm_btn.Size = new System.Drawing.Size(75, 20);
            this.confirm_btn.TabIndex = 13;
            this.confirm_btn.Text = "Confirm";
            this.confirm_btn.UseVisualStyleBackColor = true;
            this.confirm_btn.Click += new System.EventHandler(this.confirm_btn_Click);
            // 
            // TimeAllocated
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 61);
            this.Controls.Add(this.confirm_btn);
            this.Controls.Add(this.timeAllocatedInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.homeIcon);
            this.Name = "TimeAllocated";
            this.Text = "TimeAllocated";
            this.Load += new System.EventHandler(this.TimeAllocated_Load);
            ((System.ComponentModel.ISupportInitialize)(this.homeIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeAllocatedInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox homeIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown timeAllocatedInput;
        private System.Windows.Forms.Button confirm_btn;
    }
}